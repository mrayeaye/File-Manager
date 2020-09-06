using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace File_Manager
{
    public partial class Upload : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Refresh();
            Label1.Visible = true;
        }

        public void Refresh()
        {
            BindingSource bi = new BindingSource();
            DataTable dt = new DataTable();
            dt.Columns.Add("File", typeof(string));
            dt.Columns.Add("Size", typeof(string));
            dt.Columns.Add("Type", typeof(string));
            string cs = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand("Select * FROM files", con);
                con.Open();
                SqlCommand check = new SqlCommand("SELECT Count(Id) FROM files", con);
                int rows = (int)check.ExecuteScalar();
                if (rows > 0)
                {
                    SqlDataReader data = cmd.ExecuteReader();
                    bi.DataSource = data;                  
                    GridView1.DataSource = bi;
                    GridView1.DataBind();
                }
            }
            //foreach (string file in Directory.GetFiles(Server.MapPath("~/Data")))
            //{
            //    FileInfo fi = new FileInfo(file);
            //    double length = (double)fi.Length / 1024;
            //    var length_round = Math.Truncate(length);
            //    dt.Rows.Add(fi.Name, length_round, fi.Extension);
            //}
            //GridView1.DataSource = dt;
            //GridView1.DataBind();
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            if (FileUpload1.HasFile)
            {
                //FileUpload1.PostedFile.SaveAs(Server.MapPath("~/Data/") + FileUpload1.FileName);
                HttpPostedFile postedFile = FileUpload1.PostedFile;
                string fName = Path.GetFileName(postedFile.FileName);
                string fExtension = Path.GetExtension(fName);
                int fSize = postedFile.ContentLength;

                Stream stream = postedFile.InputStream;
                BinaryReader binaryreader = new BinaryReader(stream);
               Byte[] bytes = binaryreader.ReadBytes((int)stream.Length);
                string cs = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
                using (SqlConnection con = new SqlConnection(cs))
                {
                    SqlCommand cmd = new SqlCommand("upload",con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    SqlParameter pName = new SqlParameter()
                    {
                        ParameterName = "@Name",
                        Value = fName

                    };
                    cmd.Parameters.Add(pName);

                    SqlParameter pSize = new SqlParameter()
                    {
                        ParameterName = "@Size",
                        Value = fSize

                    };
                    cmd.Parameters.Add(pSize);

                    SqlParameter pImageData = new SqlParameter()
                    {
                        ParameterName = "@ImageData",
                        Value = bytes

                    };
                    cmd.Parameters.Add(pImageData);

                    SqlParameter pId = new SqlParameter()
                    {
                        ParameterName = "@NewId",
                        Value = -1,
                        Direction = ParameterDirection.Output

                    };
                    cmd.Parameters.Add(pId);

                    con.Open();
                    cmd.ExecuteNonQuery();

                    Label1.Visible = true;
                    Label1.Text = "Uploaded";
                    
                }
                Refresh();
            }
            

        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {

        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            //if (e.CommandName == "Download")
            //{
            //    Response.Clear();
            //    Response.ContentType = "application/octect-stream";
            //    Response.AppendHeader("Content-Disposition", "filename=" + e.CommandArgument);
            //    Response.TransmitFile(Server.MapPath("~/Data/") + e.CommandArgument);
            //    Response.End();


            //}
        }
    }
}