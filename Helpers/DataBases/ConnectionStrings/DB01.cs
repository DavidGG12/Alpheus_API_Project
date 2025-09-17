using Alpheus_API.Helpers.DataBases.Interfaces;

namespace Alpheus_API.Helpers.DataBases.ConnectionStrings
{
    public class DB01 : IDbGenStringConn
    {
        private static DB01? _instance;

        private DB01() { }

        public static DB01 GetInsDB01()
        {
            if (_instance != null)
                return _instance;

            _instance = new DB01();
            return _instance;
        }

        public static void CloseInsDB01()
        {
            if (_instance != null)
                _instance = null;
        }

        public string GetConnectionString()
        {
            throw new NotImplementedException();
        }
    }
}
