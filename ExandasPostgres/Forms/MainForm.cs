﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

using ExandasPostgres.Components;
using ExandasPostgres.Core;
using ExandasPostgres.Dao;
using ExandasPostgres.Native;
using ExandasPostgres.Properties;

namespace ExandasPostgres.Forms
{
    public partial class MainForm : Form
    {
        readonly Dictionary<int, Action> _menuActionDict;
        UserControl currentControl;
        ConnectionParamsListPanel connectionParamsListPanel;
        ComparisonSetListPanel comparisonSetListPanel;

        const int _ID_CLOSE = 11;
        const int _ID_LOCAL_DATABASE_SIZE = 21;
        const int _ID_COMPACT_LOCAL_DATABASE = 22;
        const int _ID_EXPORT = 231;
        const int _ID_IMPORT = 232;
        const int _ID_ABOUT = 31;
        const int _ID_CONNECTION_LIST = 101;
        const int _ID_COMPARISON_SET_LIST = 102;

        public MainForm()
        {
            InitializeComponent();

            this.Size = new Size(1300, 900);
            
            this._menuActionDict = BuildMenuActionDict();

            quitToolStripMenuItem.Tag = _ID_CLOSE;
            localDatabaseSizeToolStripMenuItem.Tag = _ID_LOCAL_DATABASE_SIZE;
            compactLocalDatabaseToolStripMenuItem.Tag = _ID_COMPACT_LOCAL_DATABASE;
            exportToolStripMenuItem.Tag = _ID_EXPORT;
            importToolStripMenuItem.Tag = _ID_IMPORT;
            aboutToolStripMenuItem.Tag = _ID_ABOUT;
            connectionsLinkLabel.Tag = _ID_CONNECTION_LIST;
            comparisonSetsLinkLabel.Tag = _ID_COMPARISON_SET_LIST;

            // localization
            fileToolStripMenuItem.Text = Strings.File;
            quitToolStripMenuItem.Text = Strings.Quit;
            toolsToolStripMenuItem.Text = Strings.Tools;
            localDatabaseSizeToolStripMenuItem.Text = Strings.LocalDatabaseSize;
            compactLocalDatabaseToolStripMenuItem.Text = Strings.CompactLocalDatabaseMenu;
            connectionsComparisonSetsToolStripMenuItem.Text = Strings.ConnectionsComparisonSets;
            exportToolStripMenuItem.Text = Strings.ExportMenu;
            importToolStripMenuItem.Text = Strings.ImportMenu;
            helpToolStripMenuItem.Text = Strings.Help;
            aboutToolStripMenuItem.Text = Strings.AboutMenu;
            connectionsLinkLabel.Text = Strings.ServerConnections;
            comparisonSetsLinkLabel.Text = Strings.ComparisonSets;

            Thread.Sleep(2000);
        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == NativeMethods.WM_SHOWME)
            {
                ShowMe();
            }
            base.WndProc(ref m);
        }

        void ShowMe()
        {
            if (WindowState == FormWindowState.Minimized)
            {
                WindowState = FormWindowState.Normal;
            }
            // get our current "TopMost" value (ours will always be false though)
            bool top = TopMost;
            // make our form jump to the top of everything
            TopMost = true;
            // set it back to whatever it was
            TopMost = top;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            Text = Defs.APPLICATION_TITLE;
            try
            {
                DaoFactory.Instance.Initialization();
                DoActionConnectionList();
                Activate();
            }
            catch (Exception ex)
            {
                Activate();
                MessageBox.Show(ex.Message, Strings.ExandasPostgresError, MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }

        Dictionary<int, Action> BuildMenuActionDict()
        {
            var dict = new Dictionary<int, Action>
            {
                { _ID_CLOSE, new Action(DoActionClose) },
                { _ID_LOCAL_DATABASE_SIZE, new Action(DoActionLocalDatabaseSize) },
                { _ID_COMPACT_LOCAL_DATABASE, new Action(DoActionCompactLocalDatabase) },
                { _ID_EXPORT, new Action(DoActionExport) },
                { _ID_IMPORT, new Action(DoActionImport) },
                { _ID_ABOUT, new Action(DoActionAbout) },
                { _ID_CONNECTION_LIST, new Action(DoActionConnectionList) },
                { _ID_COMPARISON_SET_LIST, new Action(DoActionComparisonSetList) }
            };

            return dict;
        }

        void ClearAndSwitch(UserControl userControl)
        {
            if (userControl == null || userControl == currentControl) return;

            NativeMethods.LockWindowUpdate(this.Handle);

            foreach (Control control in mainPanel.Controls)
            {
                if (userControl != control) mainPanel.Controls.Remove(control);
            }
            mainPanel.Controls.Add(userControl);
            userControl.Dock = DockStyle.Fill;
            currentControl = userControl;

            Refresh();
            NativeMethods.LockWindowUpdate(IntPtr.Zero);
        }

        void MenuAction_Click(object sender, EventArgs e)
        {
            if (sender is ToolStripMenuItem item)
            {
                if (item.Tag != null)
                {
                    int key = (int)item.Tag;
                    Action action = _menuActionDict[key];
                    action();
                }
            }
        }

        #region menu actions methods

        void DoActionClose()
        {
            Close();
        }
        void DoActionLocalDatabaseSize()
        {
            var message = Strings.LocalDatabaseColon + Environment.NewLine +
                DaoFactory.Instance.LocalDatabaseFullPath + Environment.NewLine + Environment.NewLine +
                Strings.FileSizeColon + DaoFactory.Instance.LocalDatabaseSize;
            MessageBox.Show(message, Defs.APPLICATION_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        void DoActionCompactLocalDatabase()
        {
            using (var frm = new CompactLocalDatabaseForm())
            {
                frm.ShowDialog(this);
            }
        }
        void DoActionExport()
        {
            exportSaveFileDialog.Title = Strings.ExportConnectionsComparisonSets;
            exportSaveFileDialog.Filter = "JSON File|*.json";
            exportSaveFileDialog.FileName = "ConnectionsComparisonSets";
            exportSaveFileDialog.InitialDirectory = DaoFactory.Instance.LocalDatabaseDirectoryFullPath;

            if (exportSaveFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    DataHandler.SerializeConnectionsComparisonSets(exportSaveFileDialog.FileName);
                    var message = Strings.ExportPerformed + Environment.NewLine + Environment.NewLine + exportSaveFileDialog.FileName;
                    MessageBox.Show(message, Defs.APPLICATION_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, Strings.ExandasPostgresError, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        void DoActionImport()
        {
            importOpenFileDialog.Title = Strings.ImportConnectionsComparisonSets;
            importOpenFileDialog.Filter = "JSON File|*.json";
            importOpenFileDialog.FileName = "";
            importOpenFileDialog.InitialDirectory = DaoFactory.Instance.LocalDatabaseDirectoryFullPath;

            if (importOpenFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    DataHandler.DeserializeConnectionsComparisonSets(importOpenFileDialog.FileName);
                    var message = Strings.ImportPerformed + Environment.NewLine + Environment.NewLine +
                        Strings.ApplicationRestarted;
                    MessageBox.Show(message, Defs.APPLICATION_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Application.Restart();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, Strings.ExandasPostgresError, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        void DoActionAbout()
        {
            using (var frm = new AboutBox())
            {
                frm.ShowDialog(this);
            }
        }
        void DoActionConnectionList()
        {
            if (connectionParamsListPanel == null) connectionParamsListPanel = new ConnectionParamsListPanel();
            ClearAndSwitch(connectionParamsListPanel);
        }
        void DoActionComparisonSetList()
        {
            if (comparisonSetListPanel == null) comparisonSetListPanel = new ComparisonSetListPanel();
            ClearAndSwitch(comparisonSetListPanel);
        }

        #endregion

        private void LinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (sender is LinkLabel label)
            {
                if (label.Tag != null)
                {
                    int key = (int)label.Tag;
                    Action action = _menuActionDict[key];
                    action();
                }
            }
        }

    }
}
