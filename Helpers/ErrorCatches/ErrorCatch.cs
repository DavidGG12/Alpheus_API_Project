using Alpheus_API.Helpers.ErrorCatches.Interfaces;
using Alpheus_API.Models.Responses;
using NLog;
using System.Diagnostics;

namespace Alpheus_API.Helpers.ErrorCatches
{
    public class ErrorCatch : IErrorCatch
    {
        private static ErrorCatch _instance;
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        private ErrorCatch() { }

        public static ErrorCatch GetInsErrorCatch()
        {
            if(_instance != null)
                return _instance;

            _instance = new ErrorCatch();
            return _instance;
        }

        public T GenInfoError<T>(string message, string? errorMessage, Stopwatch stopWatch) where T : new()
        {
            try
            {
                var info = new rp_Alpheus_API_General_Model();
                var model = new T();
                var properties = typeof(T).GetProperties();

                info.Status = "B";
                info.Message = message;

                foreach(var property in properties)
                {
                    if(property.Name.Equals("Info"))
                        property.SetValue(model, info);
                    else
                        property.SetValue(model, null);
                }

                logger.Error(message);

                if(string.IsNullOrEmpty(errorMessage))
                    logger.Error(errorMessage);

                stopWatch.Stop();
                var ts = stopWatch.Elapsed;

                logger.Info($"Time of execution: {ts.TotalMilliseconds} ms");

                return model;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
