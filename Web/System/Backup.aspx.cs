using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Com.Gosol.CMS.Security;
using Com.Gosol.CMS.Utility;
using System.IO;
using System.Web.Configuration;

namespace Com.Gosol.CMS.Web
{
    public partial class Backup : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!AccessControl.User.HasPermission(ChucNangEnum.SaoLuuVaPhucHoiDuLieu, AccessLevel.Read))
            {
                Response.Redirect("~");
            }

            if (!IsPostBack)
            {
                String type = Utils.ConvertToString(Request.Params["type"], String.Empty);
                if (type == "default") txtBackupFile.Text = "default_backup_" + DateTime.Now.ToString("ddMMyyyy");

                BindBackupFiles();
            }

            //MenuHelper.CreateSideMenu(ltrSideMenu, "Hệ thống");
        }

        private void BindBackupFiles()
        {
            ddlBackupFile.Items.Clear();

            String pathString = String.Empty;
            try
            {
                pathString = new DAL.SystemConfig().GetByKey("THU_MUC_BACKUP").ConfigValue;
            }
            catch
            {
                pathString = @"D:\KNTC\Data";
            }

            if (Directory.Exists(pathString))
            {
                DirectoryInfo dirInfo = new DirectoryInfo(pathString);
                FileInfo[] files = dirInfo.GetFiles("*.bak").OrderByDescending(p => p.LastWriteTime).ThenByDescending(p => p.Name).ToArray();
                
                foreach (FileInfo file in files)
                {
                    ListItem li = new ListItem();
                    String filename = file.Name;
                    li.Text = filename;
                    li.Value = file.FullName;
                    ddlBackupFile.Items.Add(li);
                }
                if (ddlBackupFile.Items.Count == 0)
                {                    
                    btnRestore.Enabled = false;
                    btnRestore.CssClass += " disable-button";
                }
            }
        }

        protected void btnBackup_Click(object sender, EventArgs e)
        {
            String pathString = String.Empty;
            try
            {
                pathString = new DAL.SystemConfig().GetByKey("THU_MUC_BACKUP").ConfigValue;
            }
            catch
            {
                pathString = @"D:\KNTC\Data";
            }

            try
            {
                System.IO.Directory.CreateDirectory(pathString);
            }
            catch
            {
            }

            DateTime dt = DateTime.Now;

            String filename = txtBackupFile.Text + ".bak";

            String filePath = pathString + @"\" + filename;

            if (System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
            }

            String dbName = WebConfigurationManager.AppSettings["DBName"];

            try {
                SQLHelper.ExecuteNonQuery(SQLHelper.CONN_BACKEND, System.Data.CommandType.Text, @"BACKUP DATABASE " + dbName + " TO DISK='" + filePath + @"'", null);
                SQLHelper.ExecuteNonQuery(SQLHelper.CONN_BACKEND, System.Data.CommandType.Text, "UPDATE BackupLog SET LastBackupDate = N'" + dt.ToString("yyyy/MM/dd") + "'");
            }
            catch 
            {
                lblError.Visible = true;
                lblError.Text = "Có lỗi xảy ra trong quá trình backup dữ liệu";
                return;
            }

            //success.Attributes.CssStyle["display"] = "block";
            lblContentSuccess.Text = "Dữ liệu đã được lưu trữ thành công";
            lblContentErr.Text = "";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "showthongBaoSuccess", "showthongBaoSuccess()", true);
        }

        protected void btnRestoreConfirm_Click(object sender, EventArgs e)
        {
            String filepath = ddlBackupFile.SelectedValue;

            if (!String.IsNullOrEmpty(filepath))
            {
                String dbName = WebConfigurationManager.AppSettings["DBName"];
                String queryString = @"Use master alter database " + dbName + " Set Single_User WITH Rollback Immediate; restore database " + dbName + " From Disk='" + filepath + @"'; alter database " + dbName + " Set Multi_User";
                try
                {
                    SQLHelper.ExecuteNonQuery(SQLHelper.CONN_BACKEND, System.Data.CommandType.Text, queryString, null);
                    lblMessage.ForeColor = System.Drawing.Color.Green;
                    lblMessage.Text = "Khôi phục dữ liệu thành công";
                    lblMessage.Visible = true;
                }
                catch
                {
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                    lblMessage.Text = "Xảy ra lỗi trong quá trình khôi phục dữ liệu";
                    lblMessage.Visible = true;
                }
            }
            else
            {
                lblMessage.ForeColor = System.Drawing.Color.Red;
                lblMessage.Text = "Chưa chọn file backup muốn khôi phục";
                lblMessage.Visible = true;
            }
        }
    }
}