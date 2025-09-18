using Alpheus_API.Helpers.Crypto;
using Alpheus_API.Helpers.DataBases.Interfaces;
using Microsoft.AspNetCore.Hosting.Server;

namespace Alpheus_API.Helpers.DataBases.ConnectionStrings
{
    public class DB01 : IDbGenStringConn
    {
        private static DB01? _instance;
        private static IConfiguration _config;

        private DB01(IConfiguration config) 
        { 
            _config = config;
        }

        public static DB01 GetInsDB01(IConfiguration? config)
        {
            if (_instance != null)
                return _instance;

            if(config == null)
                throw new ArgumentNullException("Parameter 'config' was declare null.");

            _instance = new DB01(config);
            return _instance;
        }

        public static void CloseInsDB01()
        {
            if (_instance != null)
                _instance = null;
        }

        public string GetConnectionString()
        {
            try
            {
                var crypto = Crypto.Crypto.GetInsCrypto();
                var dbServer = crypto.Decrypt(_config.GetConnectionString("DBSvr"));
                var dbUser = crypto.Decrypt(_config.GetConnectionString("DBUser"));
                var dbPwd = crypto.Decrypt(_config.GetConnectionString("DBPwd"));
                var dbNm = crypto.Decrypt(_config.GetConnectionString("DBNm"));

                return string.Format("Data Source={0};Initial Catalog={1};Persist Security Info=True;User ID={2};Password={3};TrustServerCertificate=True;", dbServer, dbNm, dbUser, dbPwd);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
