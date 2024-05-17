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
    public class TeacherController : ControllerBase
    {

        // GET api/<TeacherController>/5
        [HttpGet("{id}")]
        public IActionResult Get([FromHeader] string authorization, int id)
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

            Teacher sbj = new Teacher();
            DataRow dr = HelperDB.GetSingleData("select * from teacher where id ='" + id + "'");
            if (dr == null)
            {
                res.Messages = "Data not found.";
                return NotFound(res);
            }

            sbj.id = int.Parse(dr["id"].ToString());
            sbj.name = dr["name"].ToString();
            sbj.email = dr["email"].ToString();
            sbj.registered_number = dr["registered_number"].ToString();
            sbj.image_path = dr["image_path"].ToString();

            return Ok(sbj);
        }

    }
}
