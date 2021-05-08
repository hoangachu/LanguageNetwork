using LanguageNetwork.Models.Question;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using RoundTheCode.GoogleAuthentication;
using RoundTheCode.GoogleAuthentication.Controllers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace LanguageNetwork.Controllers
{
    public interface IQuestionController
    {

        ActionResult RegistQuestion(string title, string body, string tag);

    }
    //[Authorize]
    //[ValidateAntiForgeryToken]
    [Route("Question")]
    public class QuestionController : BaseController,IQuestionController
    {
        private readonly IConfiguration _config;
        private IHomeController _ihomeController;
        private readonly IHttpContextAccessor _httpcontextaccessor;
        public QuestionController(IHttpContextAccessor httpcontextaccessor, IConfiguration configuration, IHomeController ihomeController) : base(httpcontextaccessor, configuration)
        {
            _httpcontextaccessor = httpcontextaccessor;
            _config = configuration;
            _ihomeController = ihomeController;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        [Route("Ask")]
        public IActionResult Ask()
        {

            return View();
        }
        [HttpGet]
        [Route("RegistQuestion")]
        public ActionResult RegistQuestion(string title, string body, string tag)
        {
            using (SqlConnection con = new SqlConnection(Startup.connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("Insert_Question", con))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    SqlParameter QuestionID = new SqlParameter("@QuestionID", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };

                    cmd.Parameters.Add(QuestionID);
                    cmd.Parameters.Add("@title", SqlDbType.NVarChar).Value = title;
                    cmd.Parameters.Add("@body", SqlDbType.NVarChar).Value = body;
                    cmd.Parameters.Add("@UserID", SqlDbType.Int).Value = UserID;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    int questionID = int.Parse(QuestionID.Value.ToString());
                    List<string> listtag = tag.Split(',').Select(x => x).ToList();
                    InsertTagQuestion(questionID, listtag);
                    con.Close();
                }
            }

            return Ok();
        }
        public void InsertTagQuestion(int questionID, List<string> listTag)
        {
            using (SqlConnection con = new SqlConnection(Startup.connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("Insert_QuestionTag", con))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    var table = new DataTable();
                    table.Columns.Add("TagText", typeof(string));
                    listTag.ForEach(x => table.Rows.Add(x));
                    var pList = new SqlParameter("@list", SqlDbType.Structured);
                    pList.TypeName = "dbo.Tag";
                    pList.Value = table;
                    cmd.Parameters.Add(pList);
                    cmd.Parameters.Add("@QuestionID", SqlDbType.Int).Value = questionID;
                    con.Open();
                    //cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
