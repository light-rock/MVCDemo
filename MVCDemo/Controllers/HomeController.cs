using MVCDemo.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCDemo.Controllers
{
    [Route("")]
    public class HomeController : Controller
    {
        // GET: Home
        [HttpGet]
        [Route("")]
        [Route("index")]
        [Route("~/")]
        public ActionResult Index()
        {
            List<Models.Person> list = new List<Models.Person>();
            try
            {
                // runs stored procedure and returns data to main page
                using (SqlConnection con = new SqlConnection())
                {
                    String sql = @"exec uspPersonSearch '%'";
                    con.ConnectionString = "Server=DESKTOP-H5KQ9UK\\SQLEXPRESS;Database=TestAPP;Trusted_Connection=True;";
                    DataTable dt = new DataTable();
                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = new SqlCommand(sql, con);

                    da.Fill(dt);

                    foreach (DataRow row in dt.Rows)
                    {
                        var person = new Models.Person();
                        person.person_id = Convert.ToInt16(row["person_id"]);
                        person.first_name = row["first_name"].ToString();
                        person.last_name = row["last_name"].ToString();
                        person.state_id = Convert.ToInt16(row["state_id"]);
                        person.state_code = row["state_code"].ToString();
                        person.gender = Convert.ToChar(row["gender"]);
                        list.Add(person);
                    }
                    con.Close();
                }
                return View(list);
            }
            catch
            {
                return View("Error");
            }

        }

        [HttpPost]
        [Route("Add")]
        [Route("/Add")]
        public ActionResult Add(Person p)
        {
            try
            {
                // runs stored procedure and returns data to main page
                using (SqlConnection con = new SqlConnection())
                {
                    String sql = @"exec uspPersonUpsert ";
                    con.ConnectionString = "Server=DESKTOP-H5KQ9UK\\SQLEXPRESS;Database=TestAPP;Trusted_Connection=True;";
                    DataTable dt = new DataTable();
                    SqlDataAdapter da = new SqlDataAdapter();
                    sql += "@personid = '" + Convert.ToString(p.person_id) + "', ";
                    sql += "@firstname = '" + p.first_name + "', ";
                    sql += "@lastname  = '" + p.last_name + "', ";
                    sql += "@stateid = '" + Convert.ToString(p.state_id) + "', ";
                    sql += "@gender = '" + p.gender + "', ";
                    sql += "@dob = '" + p.dob;

                    da.SelectCommand = new SqlCommand(sql, con);
                    con.Close();

                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View("Error");
            }
        }
    }
}
