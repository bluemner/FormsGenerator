using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace FormsGeneratorWebApplication.Utilities
{
    public class DownloadFileActionResult : ActionResult
    {
        public GridView gridView = new GridView();
        public string fileName;

        public DownloadFileActionResult(DataTable dt, string fileName)
        {
            gridView.DataSource = dt;
            gridView.DataBind();
            this.fileName = fileName;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            HttpContext currentContext = HttpContext.Current;
            currentContext.Response.Clear();
            currentContext.Response.AddHeader("content-disposition", "attachment;filename=" + this.fileName);
            currentContext.Response.Charset = "";
            currentContext.Response.Cache.SetCacheability(HttpCacheability.NoCache);
            currentContext.Response.ContentType = "application/vnd.ms-excel";

            StringWriter writer = new StringWriter();
            HtmlTextWriter htmlWriter = new HtmlTextWriter(writer);
            gridView.RenderControl(htmlWriter);

            currentContext.Response.Output.Write(writer.ToString());
            currentContext.Response.Flush();
            currentContext.Response.End();
        }
    }
}