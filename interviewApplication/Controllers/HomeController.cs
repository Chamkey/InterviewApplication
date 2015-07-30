using System.Web.Mvc;
using System.Data;
using System.Web.Script.Serialization;
using interviewApplication.Models;
using System.Text.RegularExpressions;

namespace interviewApplication.Controllers
{
    public class HomeController : Controller
    {

        public ActionResult Index()
        {
            var myData = ConvertDataTableToHTML(dttable());
            ViewBag.data = myData;
            return View();
        }

        public static DataTable dttable()
        {
            /* 
            *  This json file unfortunately has a few properties that I do not know how to serialize at this moment in time.
            *  I managed to get it to display everything except the "job-name" and "job-number". I will be using regex to
            *  modify the file so that it displays as "jobname" and "jobnumber" ie. without any spaces.
            *  I will however be showing you the file as it was originally. You will then get to test my
            *  regex for yourself and see if it works in making the file better.
            *  Do not get me wrong, it is MUCH easier to parse the json file in its entirety using javascript, but the instructions
            *  were to use mvc to create the table and that is what I'm attempting to do. I will only make it searchable and paginated
            *  using another language.
            */

            string json_file_path = System.Web.HttpContext.Current.Server.MapPath("~/Content/jobs.json");

            System.IO.File.WriteAllText(json_file_path, Regex.Replace(System.IO.File.ReadAllText(json_file_path), "job-number", "jobnumber"));
            System.IO.File.WriteAllText(json_file_path, Regex.Replace(System.IO.File.ReadAllText(json_file_path), "job-name", "jobname"));

            DataTable table = new DataTable();

            table.Columns.Add("Job Number");
            table.Columns.Add("Client");
            table.Columns.Add("Job Name");
            table.Columns.Add("Due");
            table.Columns.Add("Status");

            getJsonData data = new JavaScriptSerializer().Deserialize<getJsonData>(System.IO.File.ReadAllText(json_file_path));

            foreach (var item in data.jobs)
            {
                table.Rows.Add(item.jobNumber, item.client, item.jobName, item.due, item.status);
            }
            return table;
        }

        public static string ConvertDataTableToHTML(DataTable dt)
        {

            string html = "<thead>";
            html += "<tr>";
            for (int i = 0; i < dt.Columns.Count; i++)
                html += "<td>" + dt.Columns[i].ColumnName + "</td>";
            html += "</tr>";

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (i == 0)
                {
                    html += "</thead>";
                    html += "<tbody>";
                } 
                if(i == i%2)   
                html += "<tr>";

                for (int j = 0; j < dt.Columns.Count; j++)
                    html += "<td>" + dt.Rows[i][j].ToString() + "</td>";
                html += "</tr>";
            }
            html += "</tbody>";

            return html;
        }
    }
}