using System.Collections.Generic;

using ExandasPostgres.Domain;

namespace ExandasPostgres.Dao
{
    public interface IReferenceDao
    {
        bool NeedInitialization();

        void InitializeReferences();

        List<EntityReference> GetEntityReferenceList();

        List<PropertyReference> GetPropertyReferenceListByEntity(EntityReference er);

    }
}
