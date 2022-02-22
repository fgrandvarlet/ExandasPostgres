using System.Windows.Forms;

using ExandasPostgres.Properties;

namespace ExandasPostgres.Components
{
    public partial class ComparisonSetUserControl : UserControl
    {
        public ComparisonSetUserControl()
        {
            InitializeComponent();

            // localization
            this.userLabel.Text = Strings.UserName;
            this.hostLabel.Text = Strings.Hostname;
            this.databaseLabel.Text = Strings.Database;
        }
    }
}
