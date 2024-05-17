using API_Scoring.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API_Scoring.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccessTokenController : ControllerBase
    {
        // GET api/<AccessTokenController>
        [HttpGet]
        public IActionResult Get()
        {
            ResultCrud res = new ResultCrud();
            string QueryData = "";

            
            // Specific for web module
            //the system will encrypt the combination of user id(whether teacher id or student id) with user type and 
            //the timestamp(e.g. “1-student-2020-10-01 10:10:00.000”). 
            //string FormatHash = HelperDB.user_id + "-" + UA.user_type + "-" + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss.fff");
            //string TokenHash = HelperDB.ComputeHash(FormatHash);

            // Just for android module
            string FormatHash = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            string TokenHash = HelperDB.ComputeHash(FormatHash);
            string TokenExpired = DateTime.Now.AddMinutes(30).ToString("yyyy-MM-dd HH:mm:ss");
            string TokenGenerated = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            QueryData += " insert into access_token values ( ";
            QueryData += " '" + TokenHash + "',";
            QueryData += " '" + TokenExpired + "',";
            QueryData += " '" + TokenGenerated +"'";
            QueryData += " )";
            HelperDB.ExecuteDB(QueryData);

            return Ok(new {
                token = TokenHash, 
                expired_at = DateTime.Parse(TokenExpired),
                created_at = DateTime.Parse(TokenGenerated) 
            });
        }

        // POST api/<AccessTokenController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<AccessTokenController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<AccessTokenController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
