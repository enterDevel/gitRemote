using System;
using System.Configuration;
using Oracle.ManagedDataAccess.Client;

namespace celec.bin.@base
{
    [Serializable]
    public class baseCode
    {
        #region Connections
        public String GetConn(String _userName, String _userPassword) {
            return BuildConnectionString(_userName, _userPassword);
        }
        public String GetConn(String _userName, String _userPassword, String _dataSource)
        {
            return BuildConnectionString(_userName, _userPassword,_dataSource);
        }
        private static String BuildConnectionString(String userName, String userPassword) {

            ConnectionStringSettings settings = ConfigurationManager.ConnectionStrings["Mantenimiento_ConnectionString"];
            if (settings != null)
            {
                String _ConnectionString = settings.ConnectionString;
                OracleConnectionStringBuilder builder = new OracleConnectionStringBuilder(_ConnectionString);
                builder.UserID = userName;
                builder.Password = userPassword;

                return builder.ConnectionString.ToString() ;
            }
            return String.Empty;
        }
        private static String BuildConnectionString(String userName, String userPassword, String dataSource)
        {

            ConnectionStringSettings settings = ConfigurationManager.ConnectionStrings["Mantenimiento_ConnectionString"];
            if (settings != null)
            {
                String _ConnectionString = settings.ConnectionString;
                OracleConnectionStringBuilder builder = new OracleConnectionStringBuilder(_ConnectionString);
                builder.UserID = userName;
                builder.Password = userPassword;
                builder.DataSource = dataSource;

                return builder.ConnectionString.ToString();
            }
            return String.Empty;
        }
        #endregion
        #region Log history
        private static OracleParameter[] GetLogParameters() {
            OracleParameter[] parms;
            parms = new OracleParameter[]
                {
                    new OracleParameter("parTablaLog",OracleDbType.Varchar2,50),
                    new OracleParameter("parTabla", OracleDbType.Varchar2,50),
                    new OracleParameter("parUserId", OracleDbType.Varchar2, 50),
                    new OracleParameter("parKey", OracleDbType.Varchar2, 4000),
                    new OracleParameter("parAccion", OracleDbType.Varchar2,20),
                    new OracleParameter("parRegistro", OracleDbType.TimeStamp)
                };
            return parms;
        }
        private static void InsertLog(String tableNameLog, String tableName, String entityName, String description, String action, DateTime registerDate, String informationSec) {
            OracleConnection conn = new OracleConnection(informationSec);
            OracleParameter[] parms = GetLogParameters();
            
            parms[0].Value = tableNameLog;
            parms[1].Value = tableName;
            parms[2].Value = entityName;
            parms[3].Value = description;
            parms[4].Value = action;
            parms[5].Value = registerDate;

            OracleCommand cmd = new OracleCommand("K_CET_PRY_MANT_TABS.LogTransaction", conn);
            cmd.Parameters.AddRange(parms);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
                cmd.Dispose();
                conn.Close();
            }
            catch
            {
                cmd.Dispose();
                conn.Close();
            }
        }
        public void InsertTransaccion(String tableNameLog, String tableName, String entityName, String description, String action, DateTime registerDate, String informationSec)
        {
            InsertLog(tableNameLog, tableName, entityName, description, action, registerDate, informationSec);
        }
        #endregion
    }
}
