using API_Scoring.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace API_Scoring.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class SubjectController : ParentController
    {

        [HttpGet]
        public IActionResult Get([FromHeader] string authorization)
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

            List<Subject> listData = new List<Subject>();
            DataTable dt = HelperDB.GetDataDB("select * from subject");

            foreach (DataRow dataRow in dt.Rows)
            {
                Subject sbj = new Subject();
                sbj.id = int.Parse(dataRow["id"].ToString());
                sbj.name = dataRow["name"].ToString();
                sbj.level = dataRow["level"].ToString();
                sbj.semester = dataRow["semester"].ToString();
                sbj.major = dataRow["major"].ToString();
                sbj.description = dataRow["description"].ToString();
                listData.Add(sbj);
            }
            return Ok(listData);
        }

        // GET api/<SubjectController>/5
        [HttpGet("{id}")]
        public IActionResult Get([FromHeader] string authorization,int id)
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

            Subject sbj = new Subject();
            DataRow dr = HelperDB.GetSingleData("select * from subject where id ='" + id + "'");
            if(dr == null)
            {
                res.Messages = "Data not found.";
                return NotFound(res);
            }

            sbj.id = int.Parse(dr["id"].ToString());
            sbj.name = dr["name"].ToString();
            sbj.level = dr["level"].ToString();
            sbj.semester = dr["semester"].ToString();
            sbj.major = dr["major"].ToString();
            sbj.description = dr["description"].ToString();

            return Ok(sbj);
        }

        // POST api/<SubjectController>
        [HttpPost]
        public IActionResult Post([FromHeader] string authorization, [FromBody] Subject sbj)
        {
            ResultCrud res = new ResultCrud();
            string QueryData = "";

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

            QueryData += " insert into subject values ( ";
            QueryData += " '" + sbj.name + "',";
            QueryData += " '" + sbj.level + "',";
            QueryData += " '" + sbj.semester + "',";
            QueryData += " '" + sbj.major + "',";
            QueryData += " '" + sbj.description + "',";
            QueryData += " '" + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") + "',";
            QueryData += " '" + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") + "'";
            QueryData += " )";
            HelperDB.ExecuteDB(QueryData);

            res.Messages = "Success insert data.";
            return Ok(res);
        }

        // PUT api/<SubjectController>/5
        [HttpPut("{id}")]
        public IActionResult Put([FromHeader] string authorization, [FromBody] Subject sbj, int id)
        {
            ResultCrud res = new ResultCrud();
            string QueryData = "";

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

            DataRow dr = HelperDB.GetSingleData("select * from subject where id ='" + id + "'");
            if (dr == null)
            {
                res.Messages = "Data not found.";
                return NotFound(res);
            }
            QueryData += " update subject set  ";
            QueryData += " name = '" + sbj.name + "',";
            QueryData += " level = '" + sbj.level + "',";
            QueryData += " semester = '" + sbj.semester + "',";
            QueryData += " major = '" + sbj.major + "',";
            QueryData += " description = '" + sbj.description + "'";
            QueryData += " where id = '" + id + "'";
            HelperDB.ExecuteDB(QueryData);

            res.Messages = "Success update data.";
            return Ok(res);
        }

        // DELETE api/<SubjectController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete([FromHeader] string authorization, int id)
        {
            ResultCrud res = new ResultCrud();
            string QueryData = "";

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

            DataRow dr = HelperDB.GetSingleData("select * from subject where id ='" + id + "'");
            if (dr == null)
            {
                res.Messages = "Data not found.";
                return NotFound(res);
            }
            QueryData += " delete subject  ";
            QueryData += " where id = '" + id + "'";
            HelperDB.ExecuteDB(QueryData);

            res.Messages = "Success delete data.";
            return Ok(res);
        }
    }
}
