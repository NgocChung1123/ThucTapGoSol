using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Com.Gosol.CMS.Utility;
using Com.Gosol.CMS.Model;

namespace Com.Gosol.CMS.Web
{
    /// <summary>
    /// luu thong tin dia chi ajax
    /// </summary>
    public class DiaChiSaveData : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            DiaChiInfo info = new DiaChiInfo();

            String tempID = context.Request.Form["id"].Replace("tinh_", "").Replace("huyen_", "").Replace("xa_", "");
            int id = Utils.ConvertToInt32(tempID, 0); //id neu co

            info.TenDiaChi = context.Request.Form["name"]; //ten dia danh
            info.DiaChiCha = Utils.ConvertToInt32(context.Request.Form["parentId"], 0);//dia chi cha
            info.Cap = Utils.ConvertToInt32(context.Request.Form["level"], 1); //cap bac
            info.TenDayDu = context.Request.Form["fullName"];//ten day du

            int level = Utils.ConvertToInt32(context.Request.Form["level"], 1); 

            string loaiDiaChi = context.Request.Form["typeValue"];
            if (loaiDiaChi != "khác")
                info.TenDayDu = loaiDiaChi + " " + info.TenDiaChi;
            else
                info.TenDayDu = info.TenDiaChi;

            string[] str = info.TenDayDu.Split(new Char[] { ' ' });
            string[] str1 = info.TenDiaChi.Split(new Char[] { ' ' });
            
            string cap = str[0];
            
            if (str1[0] == str[1])
                cap = str[0];
            else
                cap = str[0] + " " + str[1];

            bool checkExists;

            string json = "{\"message\":\"Dữ liệu truyền lên không hợp lệ !!!\",\"status\":false}";
            if (string.IsNullOrEmpty(info.TenDiaChi))
            {
                json = "{\"message\":\"Tên địa danh không được bỏ trống \",\"status\":false}";
            }
            else
            {
                try
                {
                    checkExists = new DAL.DiaChi().checkExistsDiaChi(id, info.TenDiaChi, info.DiaChiCha, level,info.TenDayDu);
                }
                catch
                {

                    throw;
                }

                if (info != null)
                {
                    if (checkExists)
                    {
                        json = "{\"message\":\"Tên này đã tồn tại ! xin vui lòng nhập lại \",\"status\":false}";
                    }
                    else
                    {
                        if (id <= 0)
                        {
                            //insert new
                            id = new DAL.DiaChi().Insert(info);
                        }
                        else
                        {
                            info.DiaChiID = id;
                            new DAL.DiaChi().Update(info);
                        }

                        json = "{\"id\":" + id + ",\"parentId\":" + info.DiaChiCha + ",\"level\":" + info.Cap + ",\"status\":true,\"cap\":\"" + cap + "\"}";
                    }
                }
                //{"id":6678804,"parentId":6678800,"level":2,"status":true,"cap":"huyện"}
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