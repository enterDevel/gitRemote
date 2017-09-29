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
    }
}
