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
        #endregion
    }
}
