using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Com.Gosol.CMS.DAL;
using Com.Gosol.CMS.Model;
using Com.Gosol.CMS.Utility;
using Com.Gosol.CMS.Security;

namespace Com.Gosol.CMS.Web
{
    public partial class CoQuan : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!AccessControl.User.HasPermission(ChucNangEnum.DanhMucCoQuanDonVi, AccessLevel.Read))
            {
                Response.Redirect("~");
            }
            if (!AccessControl.User.HasPermission(ChucNangEnum.DanhMucCoQuanDonVi, AccessLevel.Create))
            {
                //them_coQuan.Attributes["onclick"] = "return false";
                //them_coQuan.Attributes["class"] += " disable";
                //btnThem.Visible = false;
            }
            //ContentPlaceHolder placeHolder = Master.FindControl("SiteBarMenu") as ContentPlaceHolder;
            //placeHolder.Visible = true;

            //HyperLink menuLink = placeHolder.FindControl("DM_CoQuan") as HyperLink;
            //menuLink.CssClass = "icon_posts current";
            if (!IsPostBack)
            {
                BindTinhDDL();
                BindHuyenDDL();
                BindXaDDL();
                BindCoQuanDLL();
                BindCapDLL();
                BindThamQuyenDLL();
                BindWorkFlow();

                SetupInit();
            }
        }

        private void SetupInit()
        {
            int capID = IdentityHelper.GetCapID();
            hdfCapID.Value = capID.ToString();
            hdfCoQuanChaID.Value = IdentityHelper.GetCoQuanID().ToString();

            if (capID != (int)CapQuanLy.CapUBNDTinh)
            {
                btnThemMoi.Visible = false;
            }
        }

        private void BindCapDLL()
        {
            try
            {
                ddlCap.DataSource = new DAL.Cap().GetAll();
                ddlCap.DataBind();
            }
            catch
            {
            }
            ddlCap.Items.Insert(0, new ListItem("Chọn cấp", "0"));
        }

        private void BindThamQuyenDLL()
        {
            try
            {
                //ddlThamQuyen.DataSource = new DAL.ThamQuyen().GetAll();
                //ddlThamQuyen.DataBind();
            }
            catch
            {
            }
            //ddlThamQuyen.Items.Insert(0, new ListItem("Chọn thẩm quyền", "0"));
        }

        private void BindCoQuanDLL()
        {
            try
            {
                ddlCoQuanCha.DataSource = new DAL.CoQuan().GetAllHaveNull();
                ddlCoQuanCha.DataBind();
            }
            catch
            {
            }
            ddlCoQuanCha.Items.Insert(0, new ListItem("Chọn cơ quan cha", "0"));
        }

        private void BindWorkFlow()
        {
            try
            {
                ddlWorkFlow.DataSource = new DAL.CoQuan().GetAllWorkFlow();
                ddlWorkFlow.DataBind();

                ddlWorkFlowTienHanhTT.DataSource = new DAL.CoQuan().GetAllWorkFlow();
                ddlWorkFlowTienHanhTT.DataBind();
                
            }
            catch
            {
            }
            ddlWorkFlow.Items.Insert(0, new ListItem("Chọn luồng nghiệp vụ", ""));
            ddlWorkFlowTienHanhTT.Items.Insert(0, new ListItem("Chọn luồng tiến hành thanh tra"));
        }

        private void BindTinhDDL()
        {
            try
            {
                ddlTinh.DataSource = new DAL.Tinh().GetAll();
                ddlTinh.DataBind();
            }
            catch
            {
            }
            ddlTinh.Items.Insert(0, new ListItem("Chọn tỉnh", ""));
        }

        private void BindHuyenDDL()
        {
            try
            {
                int tinhID = Utils.ConvertToInt32(ddlTinh.SelectedValue, 0);
                ddlHuyen.DataSource = new DAL.Huyen().GetByTinh(tinhID);
                ddlHuyen.DataBind();
                ddlHuyen.Items.Insert(0, new ListItem("Chọn huyện", "0"));
            }
            catch
            {
            }
            ddlHuyen.Items.Insert(0, new ListItem("Chọn huyện", "0"));
        }

        private void BindXaDDL()
        {
            try
            {
                int huyenID = Utils.ConvertToInt32(ddlHuyen.SelectedValue, 0);
                ddlXa.DataSource = new DAL.Xa().GetByHuyen(huyenID).ToList();
                ddlXa.DataBind();
                ddlXa.Items.Insert(0, new ListItem("Chọn xã", "0"));
            }
            catch
            {
            }
            ddlXa.Items.Insert(0, new ListItem("Chọn xã", "0"));
        }


        protected void btnLuu_Click(object sender, EventArgs e)
        {
            CoQuanInfo info = new CoQuanInfo();

            int id = Utils.ConvertToInt32(id_frm.Value, 0); //id neu co

            info.TenCoQuan = Utils.ConvertToString(name_frm.Value, string.Empty); //ten co quan
            //info.CoQuanChaID = Utils.ConvertToInt32(ddlCoQuanCha.SelectedValue, 0);//co quan cha id
            info.CoQuanChaID = Utils.ConvertToInt32(hdfCoQuanChaEditID.Value, 0);
            info.CapID = Utils.ConvertToInt32(ddlCap.SelectedValue, 0); //cap id
            //info.ThamQuyenID = Utils.ConvertToInt32(ddlThamQuyen.SelectedValue, 0); //tham quyen id
            info.TinhID = Utils.ConvertToInt32(ddlTinh.SelectedValue, 0); //tinh id            
            info.HuyenID = Utils.ConvertToInt32(hdHuyenID.Value, 0);
            info.XaID = Utils.ConvertToInt32(hdXaID.Value, 0); //xa id
            info.WorkFlowID = Utils.ConvertToInt32(ddlWorkFlow.SelectedValue, 0);
            info.WFTienHanhTTID = Utils.ConvertToInt32(ddlWorkFlowTienHanhTT.SelectedValue, 0);
            //info.CapUBND = Utils.ConvertToBoolean(cbxCapUBND.Checked, false);
            //info.CapThanhTra = Utils.ConvertToBoolean(cbxCapThanhTra.Checked, false);
            info.SuDungPM = Utils.ConvertToBoolean(cbxSuDungPM.Checked, false);
            info.MaCQ = Utils.ConvertToString(code_frm.Value, string.Empty);
            //info.QuyTrinhVanThuTiepNhan = Utils.ConvertToBoolean(cbxQuyTrinhVanThuTiepNhan.Checked, false);
            //info.SuDungQuyTrinh = Utils.ConvertToBoolean(cbxSuDungQuyTrinh.Checked, false);
            //info.SuDungQuyTrinhGQ = Utils.ConvertToBoolean(cbxSuDungQuyTrinhGQ.Checked, false);
            //info.QTVanThuTiepDan = Utils.ConvertToBoolean(cbxQuyTinhCBTiepDan.Checked, false);
            info.CQCoHieuLuc = Utils.ConvertToBoolean(cbxCQCoHieuLuc.Checked, false);
            
            if (id <= 0)
            {
                //insert new                  
                try
                {
                    id = new DAL.CoQuan().Insert(info);
                    LogHelper.Log(IdentityHelper.GetCanBoID(), Constant.THEMMOI +" danh mục cơ quan "+info.TenCoQuan);
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "SuccessAddCoQuan", "SuccessAddCoQuan()", true);
                }
                catch
                {
                    //throw;
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "ErrorAddCoQuan", "ErrorAddCoQuan()", true);
                }
            }
            else
            {
                info.CoQuanID = id;
                try
                {
                    new DAL.CoQuan().Update(info);
                    LogHelper.Log(IdentityHelper.GetCanBoID(), Constant.CAPNHAT + " danh mục cơ quan " + info.TenCoQuan);
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "SuccessAddCoQuan", "SuccessAddCoQuan()", true);
                }
                catch
                {
                    //throw;
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "ErrorAddCoQuan", "ErrorAddCoQuan()", true);
                }
            }

            BindCoQuanDLL();

            //ScriptManager.RegisterStartupScript(this, this.GetType(), "hideCoQuanForm", "hideCoQuanForm()", true);         
            
        }

        protected string GetDeleteDeny()
        {
            if (AccessControl.User.HasPermission(ChucNangEnum.DanhMucCoQuanDonVi, AccessLevel.Delete)) return "false";
            else return "true";
        }

        protected string GetCreateDeny()
        {
            if (AccessControl.User.HasPermission(ChucNangEnum.DanhMucCoQuanDonVi, AccessLevel.Create)) return "false";
            else return "true";
        }

        protected string GetEditDeny()
        {
            if (AccessControl.User.HasPermission(ChucNangEnum.DanhMucCoQuanDonVi, AccessLevel.Edit)) return "false";
            else return "true";
        }
    }
}