using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatabaseHelper;
using System.Data;

namespace BusinessObjects
{
    public class PhoneType : HeaderData
    {
        #region Private Members
        private string _Type = string.Empty;
        #endregion

        #region Public Properties
        public string Type
        {
            get
            {
                return _Type;
            }
            set
            {
                if (_Type !=value)
                {
                    _Type = value;
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
                database.Command.CommandText = "tblPhoneTypeINSERT";
                database.Command.Parameters.Add("@Type", SqlDbType.VarChar).Value = _Type;
                //PROVIDES THE EMPTY BUCKETS
                base.Initialize(database, Guid.Empty);
                database.ExecuteJQuery();
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
                database.Command.CommandText = "tblPhoneTypeUPDATE";
                database.Command.Parameters.Add("@Type", SqlDbType.VarChar).Value = _Type;
                //PROVIDES THE EMPTY BUCKETS
                base.Initialize(database, base.Id);
                database.ExecuteJQuery();
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
                database.Command.CommandText = "tblPhoneTypeDELETE";
                //PROVIDES THE EMPTY BUCKETS
                base.Initialize(database, base.Id);
                database.ExecuteJQuery();
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

            if (_Type == null || _Type.Trim() == string.Empty)
            {
                result = false;
            }


            if (_Type == null || _Type.Length > 20)
            {
                result = false;
            }
            return result;
        }

        #endregion

        #region  Public Methods 
        public PhoneType GetById(Guid id)
        {

            Database database = new Database("Employer");
            DataTable dt = new DataTable();
            database.Command.CommandType = CommandType.StoredProcedure;
            database.Command.CommandText = "tblPhoneTypeGetById";
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
            _Type = dr["Type"].ToString();
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

        public PhoneType Save()
        {
            bool result = true;
            Database database = new Database("Employer");
            database.BeginTransaction();

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
        public PhoneType()
        {

        }


        #endregion
    }
}
