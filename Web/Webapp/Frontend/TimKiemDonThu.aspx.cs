using Com.Gosol.CMS.DAL.DonThu;
using Com.Gosol.CMS.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Com.Gosol.CMS.Web.Webapp.Frontend
{
    public partial class TimKiemDonThu : System.Web.UI.Page
    {
        private int stt = 1;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int page = Utils.ConvertToInt32(Request.Params["page"], 1);
                if (Session["CurrentPage"] == null)
                {
                    Session.Add("CurrentPage", page);
                }
                else
                {
                    Session["CurrentPage"] = page;
                }
                BindRepeater();
            }
        }

        protected void BindRepeater()
        {
            int currentPage = Utils.ConvertToInt32(Session["CurrentPage"], 0);
            string keyword = Utils.ConvertToString(Session["Keyword" + Request.Url.AbsolutePath], string.Empty);
            txtSearch.Text = keyword;

            if (currentPage == 0)
            {
                currentPage = 1;
            }

            int start = (currentPage - 1) * IdentityHelper.GetPageSize();
            int end = currentPage * IdentityHelper.GetPageSize();

            try
            {
                //keyword = "%" + keyword + "%";
                //if (capID == (int)CapQuanLy.CapUBNDTinh)
                //{
                rptDonThu.DataSource = new DonThu().GetBySearchFrontEnd(keyword, start, end);
                //}
                //else
                //{
                //    rptLichSu.DataSource = new LichSuTraCuu().GetBySearch_CoQuanChaID(keyword, start, end, coQuanUserID);
                //}
            }
            catch (Exception ex)
            {

            }
            rptDonThu.DataBind();

            if (rptDonThu.Items.Count == 0 && currentPage != 1)
            {
                currentPage = 1;
                Session.Add("CurrentPage", currentPage);
                Uri pageUri = new Uri(Request.Url.AbsoluteUri);
                Response.Redirect(pageUri.GetLeftPart(UriPartial.Path) + "?page=" + currentPage.ToString());
            }

            if (rptDonThu.Items.Count == 0)
            {
                pnTable.Visible = false;
            }
            else
            {
                pnTable.Visible = true;
            }
        }

        protected void rptDonThu_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            //ImageButton btnEdit = (ImageButton)e.Item.FindControl("btnEdit");
            //ImageButton btnDelete = (ImageButton)e.Item.FindControl("btnDelete");
            Label lblSTT = (Label)e.Item.FindControl("lblSTT");

            int currentPage = Utils.ConvertToInt32(Session["CurrentPage"], 1);
            lblSTT.Text = (stt + (currentPage - 1) * IdentityHelper.GetPageSize()).ToString();
            stt++;

            //if (!AccessControl.User.HasPermission(ChucNangEnum.HoSoCanBo, AccessLevel.Edit))
            //{
            //    btnEdit.Enabled = false;
            //    btnEdit.ToolTip = Constant.ToolTip;
            //    btnEdit.CssClass += " disable";
            //}

            //if (!AccessControl.User.HasPermission(ChucNangEnum.HoSoCanBo, AccessLevel.Delete))
            //{
            //    btnDelete.Enabled = false;
            //    btnDelete.ToolTip = Constant.ToolTip;
            //    btnDelete.CssClass += "disable";
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string keyword = txtSearch.Text.Trim();
            if (Session["Keyword" + Request.Url.AbsolutePath] == null)
            {
                Session.Add("Keyword" + Request.Url.AbsolutePath, keyword);
            }
            else
            {
                Session["Keyword" + Request.Url.AbsolutePath] = keyword;
            }
            Session.Add("CurrentPage" + Request.Url.AbsolutePath, "1");

            BindRepeater();
        }
    }
}