using Com.Gosol.CMS.DAL;
using Com.Gosol.CMS.Model;
using Com.Gosol.CMS.Model.LichTiepDan;
using Com.Gosol.CMS.Utility;
using HSCB.Helper;
using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;

namespace Com.Gosol.CMS.Web.Webapp.Frontend
{
    public partial class TraCuu : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadCoQuan();
            }
        }

        public void LoadCoQuan()
        {
            ddlCoQuanTiep.DataSource = new DAL.CoQuan().GetAllCoQuan();
            ddlCoQuanTiep.DataBind();
            ddlCoQuanTiep.Items.Insert(0, "Chọn cơ quan");
        }
    }
}