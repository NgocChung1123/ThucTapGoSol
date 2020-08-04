using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Com.Gosol.CMS.Model;
using Com.Gosol.CMS.Utility;

namespace Com.Gosol.CMS.Web
{
    /// <summary>
    /// Edit Loai khieu to
    /// </summary>
    public class CoQuanEdit : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            CoQuanInfo info = new CoQuanInfo();

            int id = Utils.ConvertToInt32(context.Request.Form["id"], 0); //id neu co           

            String json = "{\"message\":\"Không thể edit dữ liệu !!!\",\"status\":false}";
            if (id > 0)
            {

                info = new DAL.CoQuan().GetCoQuanByID(id);
                json = "{\"id\":" + id + ",\"name\":\"" + info.TenCoQuan + "\",\"coquanchaID\":" + info.CoQuanChaID + ",\"maCQ\":\"" + info.MaCQ + "\",\"capID\":" + info.CapID + ",\"thamquyenID\":" + info.ThamQuyenID + ",\"tinhID\":" + info.TinhID + ",\"huyenID\":" + info.HuyenID + ",\"workFlowID\":" + info.WorkFlowID + ",\"wFTienHanhTTID\":" + info.WFTienHanhTTID + ",\"xaID\":" + info.XaID + ",\"sudungPM\":" + info.SuDungPM.ToString().ToLower() + ",\"cQCoHieuLuc\":" + info.CQCoHieuLuc.ToString().ToLower() + ",\"status\":true}";
            }
            context.Response.ContentType = "application/json";
            context.Response.Write(json);
        }

        public bool IsReusable
        {
            get
            {
                return true;
            }
        }
    }
}