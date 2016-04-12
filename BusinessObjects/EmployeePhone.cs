using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatabaseHelper;
using System.Data;

namespace BusinessObjects
{
    public class EmployeePhone : HeaderData
    {
        #region Private Members
        private Guid _EmployeeId = Guid.Empty;
        private string _Phone = string.Empty;
        private Guid _PhoneTypeId = Guid.Empty;
        #endregion

        #region Public Properties
        public Guid EmployeeId
        {
            get
            {
                return _EmployeeId;
            }
            set
            {
                if (_EmployeeId != value)
                {
                    _EmployeeId = value;
                    base.IsDirty = true;
                    bool Savable = IsSavable();
                    SavableEventArgs e = new SavableEventArgs(Savable);
                    RaiseEvent(e);
                }
            }
        }
        public string Phone
        {
            get
            {
                return _Phone;
            }
            set
            {
                if (_Phone != value)
                {
                    _Phone = value;
                    base.IsDirty = true;
                    bool Savable = IsSavable();
                    SavableEventArgs e = new SavableEventArgs(Savable);
                    RaiseEvent(e);
                }
            }
        }
        public Guid PhoneTypeId
        {
            get
            {
                return _PhoneTypeId;
            }
            set
            {
                if (_PhoneTypeId != value)
                {
                    _PhoneTypeId = value;
                    base.IsDirty = true;
                    bool Savable = IsSavable();
                    SavableEventArgs e = new SavableEventArgs(Savable);
                    RaiseEvent(e);
                }
            }
        }
        #endregion

        #region  Private Methods 
        private bool Insert(Database database)
        {
            bool result = true;
            try
            {
                database.Command.Parameters.Clear();
                database.Command.CommandType = System.Data.CommandType.StoredProcedure;
                database.Command.CommandText = "tblEmployeePhoneINSERT";
                database.Command.Parameters.Add("@EmployeeId", SqlDbType.UniqueIdentifier).Value = _EmployeeId;
                database.Command.Parameters.Add("@Phone", SqlDbType.VarChar).Value = _Phone;
                database.Command.Parameters.Add("@PhoneTypeId", SqlDbType.UniqueIdentifier).Value = _PhoneTypeId;
                //PROVIDES THE EMPTY BUCKETS
                base.Initialize(database, Guid.Empty);
                database.ExecuteNonQueryWithTransaction();
                //UNLOADS THE FULL BUCKETS INTO THE OBJECT
                base.Initialize(database.Command);
            }
            catch (Exception e)
            {
                result = false;
                throw;
            }
            return result;

        }
        private bool Update(Database database)
        {
            bool result = true;
            try
            {
                database.Command.Parameters.Clear();
                database.Command.CommandType = System.Data.CommandType.StoredProcedure;
                database.Command.CommandText = "tblEmployeePhoneUPDATE";
                database.Command.Parameters.Add("@EmployeeId", SqlDbType.UniqueIdentifier).Value = _EmployeeId;
                database.Command.Parameters.Add("@Phone", SqlDbType.VarChar).Value = _Phone;
                database.Command.Parameters.Add("@PhoneTypeId", SqlDbType.UniqueIdentifier).Value = _PhoneTypeId;
                //PROVIDES THE EMPTY BUCKETS
                base.Initialize(database, base.Id);
                database.ExecuteNonQueryWithTransaction();
                //UNLOADS THE FULL BUCKETS INTO THE OBJECT
                base.Initialize(database.Command);
            }
            catch (Exception e)
            {
                result = false;
                throw;
            }
            return result;


        }
        private bool Delete(Database database)
        {
            bool result = true;
            try
            {
                database.Command.Parameters.Clear();
                database.Command.CommandType = System.Data.CommandType.StoredProcedure;
                database.Command.CommandText = "tblEmployeePhoneDELETE";
                //PROVIDES THE EMPTY BUCKETS
                base.Initialize(database, base.Id);
                database.ExecuteNonQueryWithTransaction();
                //UNLOADS THE FULL BUCKETS INTO THE OBJECT
                base.Initialize(database.Command);
            }
            catch (Exception e)
            {
                result = false;
                throw;
            }
            return result;

        }

        private bool IsValid()
        {
            bool result = true;

            //INTENTIONALLY LEAVING OUT EMPLOYEEID

            if (_Phone == null || _Phone.Trim() == string.Empty)
            {
                result = false;
            }

            if (_Phone == null || _Phone.Length > 20)
            {
                result = false;
            }

            if (_PhoneTypeId == null || _PhoneTypeId != Guid.Empty)
            {
                result = false;
            }
            return result;
        }

        #endregion

        #region  Public Methods 
        public EmployeePhone GetById(Guid id)
        {

            Database database = new Database("Employer");
            DataTable dt = new DataTable();
            database.Command.CommandType = CommandType.StoredProcedure;
            database.Command.CommandText = "tblEmployeePhoneGetById";
            base.Initialize(database, base.Id);
            dt = database.ExecuteQuery();
            if (dt != null && dt.Rows.Count == 1)
            {
                DataRow dr = dt.Rows[0];
                base.Initialize(dr);
                InitializeBusinessData(dr);
                base.IsNew = false;
                base.IsDirty = false;
            }
            return this;
        }

        public void InitializeBusinessData(DataRow dr)
        {
            _EmployeeId = (Guid) dr["EmployeeId"];
            _Phone = dr["Phone"].ToString();
            _PhoneTypeId = (Guid) dr["PhoneTypeId"];
        }

        public bool IsSavable()
        {
            bool result = false;
            if (base.IsDirty == true && IsValid() == true)
            {
                result = true;
            }
            return result;
        }

        public EmployeePhone Save(Database database)
        {
            bool result = true;
            //Database database = new Database("Employer");
            if (base.IsNew == true && IsSavable() == true)
            {
                result = Insert(database);
            }
            else if (base.Deleted == true && base.IsDirty == true)
            {
                result = Delete(database);
            }
            else if (base.IsNew == false && IsSavable() == true)
            {
                result = Update(database);
            }

            if (result == true)
            {
                base.IsDirty = false;
                base.IsNew = false;
            }
            return this;


        }
        #endregion

        #region  Public Events 

        #endregion

        #region  Public Event Handlers 

        #endregion

        #region Construction 
        public EmployeePhone()
        {

        }


        #endregion
    }
}
