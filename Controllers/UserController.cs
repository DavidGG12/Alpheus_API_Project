using Alpheus_API.Helpers.DataBases.DBConnection;
using Alpheus_API.Models.Responses;
using Alpheus_API.Models.Responses.UserReponses.Get;
using Alpheus_API.Models.Users;
using Microsoft.AspNetCore.Mvc;
using NLog;
using System.Diagnostics;

namespace Alpheus_API.Controllers
{
    [Route("API/Users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private static IConfiguration _config;
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();
        private readonly Stopwatch stopWatch;

        public UserController(IConfiguration config) 
        { 
            _config = config;
            stopWatch = new Stopwatch();
        }

        #region GET

        [HttpGet("/allRoles")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status408RequestTimeout)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetAllRoles()
        {
            stopWatch.Start();
            logger.Info($"Start for the {nameof(this.GetAllRoles)} method execution.");
            
            var rpGetAllRoles = new rp_Get_AllRoles_Model();
            var rpInfo = new rp_Alpheus_API_General_Model();
            var rpRoles = new List<Tbl_Alpheus_Role_Model>();

            try
            {
                var dbSql = DbSQLServer.GetInsSQLServer(_config, "DB01");
                rpRoles = dbSql.GetDataList<Tbl_Alpheus_Role_Model>("sp_Alpheus_SelAllRoles", null);

                if(rpRoles == null)
                {
                    logger.Warn($"No role information.");

                    stopWatch.Stop();
                    var ts = stopWatch.Elapsed;

                    logger.Info($"Time of execution: {ts.TotalMilliseconds} ms");

                    return NoContent();
                }

                rpInfo.Status = "S";
                rpInfo.Message = "Information found.";

                rpGetAllRoles.Info = rpInfo;
                rpGetAllRoles.Roles = rpRoles;

                return Ok(rpGetAllRoles);
            }
            catch (Exception ex)
            {
                rpInfo.Status = "B";
                rpInfo.Message = $"There are a problem with the execution of {nameof(GetAllRoles)} method.";

                rpGetAllRoles.Info = rpInfo;
                rpGetAllRoles.Roles = null;

                logger.Error($"There are a problem with the execution of {nameof(GetAllRoles)} method.");
                logger.Error($"Error: {ex.Message}");

                stopWatch.Stop();
                var ts = stopWatch.Elapsed;

                logger.Info($"Time of execution: {ts.TotalMilliseconds} ms");

                return BadRequest(rpGetAllRoles);
            }
        }

        [HttpGet("/rol")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status408RequestTimeout)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetRol(string? idRol, string? nameRol)
        {
            stopWatch.Start();
            logger.Info($"Start for the {nameof(this.GetRol)} method execution.");
            
            var rpGetRol = new rp_Get_Rol_Model();
            var rpInfo = new rp_Alpheus_API_General_Model();
            var rpRol = new Tbl_Alpheus_Role_Model();

            try
            {
                if(string.IsNullOrEmpty(idRol) && string.IsNullOrEmpty(nameRol))
                {
                    logger.Warn($"Both parameters: {nameof(idRol)} and {nameof(nameRol)} are null or empty.");

                    rpInfo.Status = "B";
                    rpInfo.Message = "Check the parameters 'cause it recovers empty values or null.";

                    rpGetRol.Info = rpInfo;
                    rpGetRol.Rol = null;

                    stopWatch.Stop();
                    var ts = stopWatch.Elapsed;

                    logger.Info($"Time of execution: {ts.TotalMilliseconds} ms");

                    return BadRequest(rpGetRol);
                }

                var dbSql = DbSQLServer.GetInsSQLServer(_config, "DB01");
                var parameters = new Dictionary<string, object>()
                {
                    { "@idRol", idRol },
                    { "@nmRol", nameRol }
                };
                rpRol = dbSql.GetData<Tbl_Alpheus_Role_Model>("sp_Alpheus_SelRolById", parameters);

                if (rpRol == null)
                {
                    logger.Warn($"No role information.");

                    stopWatch.Stop();
                    var ts = stopWatch.Elapsed;

                    logger.Info($"Time of execution: {ts.TotalMilliseconds} ms");

                    return NoContent();
                }

                rpInfo.Status = "S";
                rpInfo.Message = "Information found.";

                rpGetRol.Info = rpInfo;
                rpGetRol.Rol = rpRol;

                return Ok(rpGetRol);
            }
            catch(Exception ex)
            {
                rpInfo.Status = "B";
                rpInfo.Message = $"There are a problem with the execution of {nameof(GetRol)} method.";

                rpGetRol.Info = rpInfo;
                rpGetRol.Rol = null;

                logger.Error($"There are a problem with the execution of {nameof(GetRol)} method.");
                logger.Error($"Error: {ex.Message}");

                stopWatch.Stop();
                var ts = stopWatch.Elapsed;

                logger.Info($"Time of execution: {ts.TotalMilliseconds} ms");

                return BadRequest(rpGetRol);
            }
        }

        #endregion


        #region POST

        //public IActionResult 

        #endregion
    }
}
