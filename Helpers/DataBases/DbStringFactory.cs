using Alpheus_API.Helpers.DataBases.ConnectionStrings;
using Alpheus_API.Helpers.DataBases.Interfaces;

namespace Alpheus_API.Helpers.DataBases
{
    public class DbStringFactory
    {
        private static DbStringFactory? _instance;
        private static IConfiguration _config;

        private DbStringFactory(IConfiguration config)
        {
            _config = config;
        }

        public static DbStringFactory GetInstDbFactory(IConfiguration config)
        {
            if (_instance != null) 
                return _instance;

            _instance = new DbStringFactory(config);
            return _instance;
        }

        public void CloseInsDbFactory()
        {
            if (_instance != null)
                _instance = null;
        }

        public static IDbGenStringConn GetInsDBString(string? option)
        {
            if(string.IsNullOrEmpty(option))
                throw new ArgumentNullException($"Parameter '{nameof(option)}' was declare null.");

            switch(option)
            {
                case "DB01":
                    return DB01.GetInsDB01(_config);

                default:
                    throw new ArgumentException($"The parameter {option} is not a validate value.");
            }
        }
    }
}
