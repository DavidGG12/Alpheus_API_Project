using Alpheus_API.Helpers.DataBases.DBConnection;
using Alpheus_API.Helpers.ErrorCatches;
using Alpheus_API.Helpers.ErrorCatches.Interfaces;
using Alpheus_API.Models.Requests.UserRequests.Post;
using Alpheus_API.Models.Requests.UserRequests.Put;
using Alpheus_API.Models.Responses;
using Alpheus_API.Models.Responses.UserReponses.Get;
using Alpheus_API.Models.Responses.UserReponses.Post;
using Alpheus_API.Models.Responses.UserReponses.Put;
using Alpheus_API.Models.Users;
using Microsoft.AspNetCore.Http.HttpResults;
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

        [HttpGet("/AllRoles")]
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
            var errorCatch = ErrorCatch.GetInsErrorCatch();

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
                var message = $"There are a problem with the execution of {nameof(this.GetAllRoles)}";
                rpGetAllRoles.Info = errorCatch.GenInfoError<rp_Alpheus_API_General_Model>(message, null, stopWatch);
                rpGetAllRoles.Roles = null;

                return BadRequest(rpGetAllRoles);
            }
        }

        [HttpGet("/Rol")]
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
            var errorCatch = ErrorCatch.GetInsErrorCatch();

            try
            {

                if(string.IsNullOrEmpty(idRol) && string.IsNullOrEmpty(nameRol))
                {
                    var message = $"Both parameters: {nameof(idRol)} and {nameof(nameRol)} are null or empty.";
                    
                    return BadRequest(errorCatch.GenInfoError<rp_Get_Rol_Model>(message, null, stopWatch));
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
                var message = $"There are a problem with the execution of {nameof(GetRol)} method.";
                
                return BadRequest(errorCatch.GenInfoError<rp_Get_Rol_Model>(message, ex.Message, stopWatch));
            }
        }

        #endregion


        #region POST

        [HttpPost("/CreateRol")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status408RequestTimeout)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult CreateRol(rq_Post_CreateRol_Model? rqRol)
        {
            stopWatch.Start();
            logger.Info($"Start for the {nameof(this.GetRol)} method execution.");

            var rpCreateRol = new rp_Post_CreateRol_Model();
            var rpInfo = new rp_Alpheus_API_General_Model();
            var rpRol = new Tbl_Alpheus_Role_Model();
            var errorCatch = ErrorCatch.GetInsErrorCatch();

            try
            {
                if(rqRol == null || string.IsNullOrEmpty(rqRol.RoleName))
                {
                    var message = "It's not provided the json.";
                    return BadRequest(errorCatch.GenInfoError<rp_Post_CreateRol_Model>(message, null, stopWatch));
                }

                var parameters = new Dictionary<string, object>()
                {
                    { "@nmRol", rqRol.RoleName }
                };
                var dbSql = DbSQLServer.GetInsSQLServer(_config, "DB01");
                rpRol = dbSql.GetData<Tbl_Alpheus_Role_Model>("sp_Alpheus_InRol", parameters);

                if(rpRol == null || rpRol.ErrorMessage != null)
                {
                    var message = "Sorry! Rol was not created.";
                    logger.Debug(rpRol.ErrorMessage);
                    return BadRequest(errorCatch.GenInfoError<rp_Post_CreateRol_Model>(message, null, stopWatch));
                }

                rpInfo.Status = "S";
                rpInfo.Message = "Rol created";

                rpCreateRol.Info = rpInfo;
                rpCreateRol.IDRol = rpRol.IDRol;

                return Ok(rpCreateRol);
            }
            catch (Exception ex)
            {
                var message = $"There are a problem with the execution of {nameof(this.CreateRol)} method.";

                return BadRequest(errorCatch.GenInfoError<rp_Post_CreateRol_Model>(message, ex.Message, stopWatch));
            }
        }

        #endregion

        #region PUT

        [HttpPut("/UpdateRol")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status408RequestTimeout)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult UpdateRol(rq_Put_Rol_Model? rqRol)
        {
            stopWatch.Start();
            logger.Info($"Start for the {nameof(this.GetRol)} method execution.");

            var rpPutRol = new rp_Put_Rol_Model();
            var rpInfo = new rp_Alpheus_API_General_Model();
            var rpRol = new Tbl_Alpheus_Role_Model();
            var errorCatch = ErrorCatch.GetInsErrorCatch();

            try
            {
                if(rqRol == null)
                {
                    var message = "It's not provided the json.";
                    return BadRequest(errorCatch.GenInfoError<rp_Put_Rol_Model>(message, null, stopWatch));
                }

                if (string.IsNullOrEmpty(rqRol.IDRol) || string.IsNullOrEmpty(rqRol.NameRol) || string.IsNullOrEmpty(rqRol.StateRol))
                {
                    var message = "Some of the properties of the json are empty.";
                    return BadRequest(errorCatch.GenInfoError<rp_Put_Rol_Model>(message, null, stopWatch));
                }

                var parameters = new Dictionary<string, object>()
                {
                    { "@IDRol", rqRol.IDRol },
                    { "@NmRol", rqRol.NameRol },
                    { "@StRol", rqRol.StateRol }
                };
                var dbSql = DbSQLServer.GetInsSQLServer(_config, "DB01");
                rpRol = dbSql.GetData<Tbl_Alpheus_Role_Model>("sp_Alpheus_UpdRol", parameters);

                if(rpRol == null || rpRol.ErrorMessage != null)
                {
                    var message = "Has a problem to update the data.";
                    logger.Debug(rpRol.ErrorMessage);
                    return BadRequest(errorCatch.GenInfoError<rp_Put_Rol_Model>(message, null, stopWatch));
                }

                rpInfo.Status = "S";
                rpInfo.Message = "Rol updated.";

                rpPutRol.Info = rpInfo;
                rpPutRol.IDRol = rpRol.IDRol;

                return Ok(rpPutRol);
            }
            catch(Exception ex)
            {
                var message = $"There are a problem with the execution of {nameof(this.UpdateRol)} method.";
                return BadRequest(errorCatch.GenInfoError<rp_Put_Rol_Model>(message, ex.Message, stopWatch));
            }
        }

        #endregion
    }
}
