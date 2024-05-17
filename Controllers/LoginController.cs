using API_Scoring.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace API_Scoring.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        [HttpPost]
        public IActionResult Get([FromHeader] string authorization, [FromBody] UserAccount UA)
        {
            ResultCrud res = new ResultCrud();
            // Check Token
            if (authorization == null)
                return BadRequest(new { Message = "No token authorization value." });
            authorization = authorization.Replace("Bearer ", "");
            string CheckToken = HelperDB.CheckValidToken(authorization);
            if (CheckToken != "")
            {
                res.Messages = CheckToken;
                return Unauthorized(res);
            }
            DataRow dr;
            if (UA.user_type == "student")
            {
                dr = HelperDB.GetSingleData("select * from student where email = '" + UA.email + "' and password = '" + UA.password + "'");
            }
            else
            {
                dr = HelperDB.GetSingleData("select * from teacher where email = '" + UA.email + "' and password = '" + UA.password + "'");
            }
            if (dr == null)
            {
                return BadRequest(new { Message = "Username or password is wrong. Please check again!" });
            }
            return Ok(new { user_id = dr["id"], message = "Success login with " + UA.email });
        }

        [HttpGet]
        public IActionResult Get([FromHeader] string authorization, [FromQuery] string email, [FromQuery] string password, [FromQuery] string user_type)
        {
            ResultCrud res = new ResultCrud();
            // Check Token
            if (authorization == null)
                return BadRequest(new { Message = "No token authorization value." });
            authorization = authorization.Replace("Bearer ", "");
            string CheckToken = HelperDB.CheckValidToken(authorization);
            if (CheckToken != "")
            {
                res.Messages = CheckToken;
                return Unauthorized(res);
            }
            DataRow dr;
            if (user_type == "student")
            {
                dr = HelperDB.GetSingleData("select * from student where email = '" + email + "' and password = '" + password + "'");
            }
            else
            {
                dr = HelperDB.GetSingleData("select * from teacher where email = '" + email + "' and password = '" + password + "'");
            }
            if (dr == null)
            {
                return BadRequest(new { Message = "Username or password is wrong. Please check again!" });
            }
            return Ok(new { user_id = dr["id"], message = "Success login with " + email });
        }
    }
}
