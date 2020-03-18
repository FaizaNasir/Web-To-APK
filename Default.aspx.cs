using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Web2APK
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
           

        }


        public void downloadAPK()
        {
            Directory.SetCurrentDirectory(AppDomain.CurrentDomain.BaseDirectory);
            string directory = Directory.GetCurrentDirectory();
            string path = directory +@"\WEB_to_APK\app\build\outputs\apk\debug\app-debug.apk";
            string name = Path.GetFileName(path);
            string ext = Path.GetExtension(path);
            string type = "";

            if (ext != null)
            {
                switch (ext.ToLower())
                {
                    case ".htm":
                    case ".html":
                        type = "text/HTML";
                        break;

                    case ".txt":
                        type = "text/plain";
                        break;

                    case ".apk":
                        type = "Application/APK";
                        break;

                    case ".png":
                        type = "image/PNG";
                        break;

                    case ".pdf":
                        type = "Application/pdf";
                        break;

                    case ".doc":
                    case ".rtf":
                        type = "Application/msword";
                        break;

                        Default:
                        type = "";
                        break;
                }
            }

            Response.AppendHeader("content-disposition", "attachment; filename=" + name);

            if (type != "")
                Response.ContentType = type;
            Response.WriteFile(path);
            Response.End();
        }
        public void executeBat()
        {
            Process proc = null;
            try
            {
                Directory.SetCurrentDirectory(AppDomain.CurrentDomain.BaseDirectory);
                string directory = Directory.GetCurrentDirectory();
                proc = new Process();
                proc.StartInfo.WorkingDirectory = directory;
                proc.StartInfo.FileName = "web2apk.bat";
                proc.StartInfo.CreateNoWindow = true;
                proc.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                proc.Start();
                proc.WaitForExit();
                lblMsg.Text = "Bat file executed !!";
                downloadAPK();
            }
            catch(Exception ex)
            {
                lblMsg.Text = ex.Message;
            }
        }

        public void ReplaceText(string filePath, string replaceText)
        {

            var content = string.Empty;
            string targetfile = filePath + "template.txt";
            string destinationfilel = filePath + "MainActivity.java";
            string searchText = "URL_TEXT";
            File.Copy(targetfile, destinationfilel,true);
            using (StreamReader reader = new StreamReader(destinationfilel))
            {
                content = reader.ReadToEnd();
                reader.Close();
            }

            content = Regex.Replace(content, searchText, replaceText);

            using (StreamWriter writer = new StreamWriter(destinationfilel))
            {
                writer.Write(content);
                writer.Close();
            }

        }

        protected void btnSubmitUrl_Click(object sender, EventArgs e)
        {
            Directory.SetCurrentDirectory(AppDomain.CurrentDomain.BaseDirectory);
            string directory = Directory.GetCurrentDirectory();
            var path = directory+ @"\WebTOAPK\app\src\main\java\com\example\web2apk\";
            string text = txtURL.Text;
            ReplaceText(path, text);
            lblMsg.Text = "Text written in file successfully !!";
            executeBat();

        }
    }
}