using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using DatabaseHelper;
using System.Data;

namespace BusinessObjects
{
    public class PhoneTypeList : Event
    {
        #region Private Members
        private BindingList<PhoneType> _List;
        #endregion

        #region Public Properties
        public BindingList<PhoneType> List
        {
            get
            {
                return _List;
            }
        }
        #endregion

        #region  Private Methods 

        #endregion

        #region  Public Methods 
        public PhoneTypeList GetAll()
        {
            Database database = new Database("Employer");
            database.Command.Parameters.Clear();
            database.Command.CommandType = System.Data.CommandType.StoredProcedure;
            database.Command.CommandText = "tblPhoneTypeGetAll";
            DataTable dt = database.ExecuteQuery();
            foreach (DataRow dr in dt.Rows)
            {
                PhoneType pt = new PhoneType();
                pt.Initialize(dr);
                pt.InitializeBusinessData(dr);
                pt.IsNew = false;
                pt.IsDirty = false;
                pt.Savable += PhoneType_Savable;
                _List.Add(pt);
            }
            return this;
        }

        public PhoneTypeList Save()
        {
            foreach (PhoneType em in _List)
            {
                if (em.IsSavable() == true)
                {
                    em.Save();
                }
            }
            return this;
        }

        #endregion

        #region  Public Events 
        private void PhoneType_Savable(SavableEventArgs e)
        {
            RaiseEvent(e);
        }
        #endregion

        #region  Public Event Handlers 
        private void _List_AddingNew(object sender, AddingNewEventArgs e)
        {
            e.NewObject = new PhoneType();
            PhoneType phoneType = (PhoneType)e.NewObject;
            phoneType.Savable += PhoneType_Savable;

        }
        #endregion

        #region Construction 
        public PhoneTypeList()
        {
            _List = new BindingList<PhoneType>();
            _List.AddingNew += _List_AddingNew;
        }
        #endregion
    }
}
