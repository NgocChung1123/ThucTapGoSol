using Com.Gosol.CMS.DAL;
using Com.Gosol.CMS.Model;
using Com.Gosol.CMS.Utility;
using System;
using System.Configuration;
using System.Net.Mail;
using System.Text;

namespace Com.Gosol.CMS.Web
{
    public partial class QuenMatKhau : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSend_Click(object sender, EventArgs e)
        {
            SystemConfigInfo fromEmail = new SystemConfig().GetByKey("EMAIL_NAME"); // ConfigurationSettings.AppSettings["Email"];
            SystemConfigInfo passWord = new SystemConfig().GetByKey("EMAIL_PASSWORD"); // ConfigurationSettings.AppSettings["PassEmail"];

            SystemConfigInfo smtpServer = new SystemConfig().GetByKey("SMTP_SERVER");
            SystemConfigInfo smtpPort = new SystemConfig().GetByKey("SMTP_PORT");
            string userName = txtTenNguoiDung.Text;
            string emailN = txtEmail.Text;
            NguoiDungInfo ndinfo = new NguoiDung().GetNguoiDungByUserName(userName);
            if (ndinfo != null && ndinfo.CanBoID != 0)
            {

                CanBoInfo cbinfo = new CanBo().GetCanBoByID(ndinfo.CanBoID);
                if (cbinfo != null)
                {



                    if (cbinfo.Email == emailN)
                    {
                        string defaultPassword = "vungtau123";
                        SystemConfigInfo passinfo = new DAL.SystemConfig().GetByKey("MAT_KHAU_MAC_DINH");
                        if (passinfo != null)
                        {
                            defaultPassword = passinfo.ConfigValue;
                        }
                        string emailTitle = "[KNTC-Portal] Đặt lại mật khẩu";
                        string emailContent = "Xin chào" + cbinfo.TenCanBo + ",";
                        emailContent += "<br />";
                        emailContent += "Mật khẩu mới của bạn là " + defaultPassword;
                        emailContent += "<br />";
                        emailContent += "Cảm ơn.";

                        ndinfo.MatKhau = Utils.HashFile(Encoding.ASCII.GetBytes(defaultPassword)).ToUpper(); ;
                        new NguoiDung().Update(ndinfo);

                        if (smtpServer != null)
                        {
                            string emailServer = smtpServer.ConfigValue;
                            if (smtpPort != null)
                            {
                                int port = Utils.ConvertToInt32(smtpPort.ConfigValue, 0);
                                MailMessage message = new System.Net.Mail.MailMessage(fromEmail.ConfigValue, txtEmail.Text, emailTitle, emailContent);

                                SmtpClient smtp = new SmtpClient();

                                smtp.Host = emailServer;
                                smtp.Port = port;

                                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                                smtp.Credentials = new System.Net.NetworkCredential(fromEmail.ConfigValue, passWord.ConfigValue);
                                smtp.EnableSsl = true;

                                message.IsBodyHtml = true;

                                smtp.Send(message);
                            }
                            else
                            {
                                MailMessage message = new System.Net.Mail.MailMessage(fromEmail.ConfigValue, txtEmail.Text, emailTitle, emailContent);

                                SmtpClient smtp = new SmtpClient();

                                smtp.Host = "smtp.gmail.com";
                                smtp.Port = 587;


                                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                                smtp.Credentials = new System.Net.NetworkCredential(fromEmail.ConfigValue, passWord.ConfigValue);
                                smtp.EnableSsl = true;

                                message.IsBodyHtml = true;

                                smtp.Send(message);
                            }
                        }
                        else
                        {
                            MailMessage message = new System.Net.Mail.MailMessage(fromEmail.ConfigValue, txtEmail.Text, emailTitle, emailContent);

                            SmtpClient smtp = new SmtpClient();

                            smtp.Host = "smtp.gmail.com";
                            smtp.Port = 587;


                            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                            smtp.Credentials = new System.Net.NetworkCredential(fromEmail.ConfigValue, passWord.ConfigValue);
                            smtp.EnableSsl = true;

                            message.IsBodyHtml = true;

                            smtp.Send(message);
                        }
                        lblMessage.Text = "Mật khẩu đã được gửi! Hãy kiểm tra email của bạn.";
                    }
                    else
                    {
                        lblError.Text = "Email chưa được khai báo.";
                    }
                }
                else
                {
                    lblError.Text = "Thông tin khoản không chính xác.";
                }
            }
            else
            {
                lblError.Text = "Thông tin khoản không chính xác.";
            }
        }
    }
}