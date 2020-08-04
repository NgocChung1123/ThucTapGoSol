using Com.Gosol.CMS.Model;
using Com.Gosol.CMS.Model.DonThu;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;


namespace SyncDonThuService
{
    public class SyncDonThu
    {
        public void Sync()
        {
            try
            {
                
                string urlQDGQ = ConfigurationSettings.AppSettings["APIUrl_GetPortalSyncQuyetDinhGiaiQuyet"].ToString();
                string urlVBTL = ConfigurationSettings.AppSettings["APIUrl_GetPortalSyncVanBanTraLoi"].ToString();
                string urlResponse = ConfigurationSettings.AppSettings["APIUrl_UpdateSyncStatus"].ToString();
                string pathServer = ConfigurationSettings.AppSettings["PathServer"].ToString();
                var api = new ApiService();
                var DonThuDAL = new Com.Gosol.CMS.DAL.DonThu.DonThu();

                Utilities.WriteLog("######################################################## Begining ########################################################");

                bool isContiueQDGQ = true;
                bool isContiueVBTL = true;
                // ghi log chi tiet phải có thời gian nhé
                // ví dụ:
                // []26/12/2018 12:22:33] 
                // nếu em tạo 1 thư viện log riêng thì phải cso cơ chê ngắt file khi file > x MB  hoăch  file có số bản ghi > 10.000 hoặc 1000 gì gì ấy
                // vì sau nay có làm 1 cái gì đấy ghi log liên tục thì file nó ko lớn và mở ra nó ko lag.
                while (isContiueQDGQ || isContiueVBTL)
                {
                    string listDonThuId = "";
                    string listDonThuIdFail = "";
                    if (isContiueQDGQ)
                    {
                        Utilities.WriteLog("Send Api QDGQ");
                        var response = api.GetApi(urlQDGQ);
                        var json = response.Content.ReadAsStringAsync().Result;

                        var result = JsonConvert.DeserializeObject<List<DonThuModel>>(json);

                        Utilities.WriteLog("Json Result: " + result.Count);

                        if (result.Count < 1)
                        {
                            isContiueQDGQ = false;
                            Utilities.WriteLog(" ================================ Đồng bộ đơn thư quyết định giải quyết xong! ================================ ");
                        }
                        else
                        {
                            foreach (var item in result)
                            {
                                var temp = ConvertDonThu(item);
                                temp.CongKhai = "1";

                                var kq = DonThuDAL.Insert(temp);
                                if (kq != 0)
                                {
                                    for (int i = 0; i < item.lsFileQuyetDinhGD.Count; i++)
                                    {
                                        FileHoSoInfo infoFileHoSo = new FileHoSoInfo();
                                        infoFileHoSo.DonThuID = Convert.ToInt32(kq);
                                        infoFileHoSo.FileURL = item.lsFileQuyetDinhGD[i].FileURL;
                                        infoFileHoSo.TenFile = item.lsFileQuyetDinhGD[i].TenFile;
                                        string path = pathServer + item.lsFileQuyetDinhGD[i].FileURL;
                                        bool exists = System.IO.Directory.Exists(pathServer + "/UploadFiles/FileBanHanhQD/");
                                        if (!exists)
                                            System.IO.Directory.CreateDirectory(pathServer + "/UploadFiles/FileBanHanhQD/");
                                        if (!String.IsNullOrEmpty(item.lsFileQuyetDinhGD[i].FileBase64))
                                        {
                                            Byte[] bytes = Convert.FromBase64String(item.lsFileQuyetDinhGD[i].FileBase64);
                                            File.WriteAllBytes(path, bytes);
                                        }
                                        DonThuDAL.InsertFileKetQua(infoFileHoSo);
                                    }
                                    if (listDonThuId == "")
                                    {
                                        listDonThuId += temp.XuLyDonID;
                                    }
                                    else
                                    {
                                        listDonThuId += ";" + temp.XuLyDonID;
                                    }

                                }
                                else
                                {
                                    listDonThuIdFail += ";" + temp.XuLyDonID;
                                }
                                //Utilities.WriteLog("Insert: " + JsonConvert.SerializeObject(item));

                            }
                        }
                    }

                    //==============
                    if (isContiueVBTL)
                    {
                        Utilities.WriteLog("Send Api VBTL");
                        var response = api.GetApi(urlVBTL);
                        var json = response.Content.ReadAsStringAsync().Result;

                        var result = JsonConvert.DeserializeObject<List<DonThuModel>>(json);

                        Utilities.WriteLog("Json Result: " + result.Count);

                        if (result.Count < 1)
                        {
                            isContiueVBTL = false;
                            Utilities.WriteLog(" ================================ Đồng bộ đơn thư văn bản trả lời xong! ================================ ");
                        }
                        else
                        {
                            foreach (var item in result)
                            {
                                var temp = ConvertDonThu(item);
                                temp.CongKhai = "1";

                                var kq = DonThuDAL.Insert(temp);
                                if (kq != 0)
                                {
                                    for (int i = 0; i < item.lsFileYKienXuLy.Count; i++)
                                    {
                                        FileHoSoInfo infoFileHoSo = new FileHoSoInfo();
                                        infoFileHoSo.XuLyDonID = item.XuLyDonID;
                                        infoFileHoSo.FileURL = item.lsFileYKienXuLy[i].FileURL;
                                        infoFileHoSo.TenFile = item.lsFileYKienXuLy[i].TenFile;
                                        string path = pathServer + item.lsFileYKienXuLy[i].FileURL;
                                        bool exists = System.IO.Directory.Exists(pathServer + "/UploadFiles/FileYKienXuLy/");
                                        if (!exists)
                                            System.IO.Directory.CreateDirectory(pathServer + "/UploadFiles/FileYKienXuLy/");
                                        if (!String.IsNullOrEmpty(item.lsFileYKienXuLy[i].FileBase64))
                                        {
                                            Byte[] bytes = Convert.FromBase64String(item.lsFileYKienXuLy[i].FileBase64);
                                            File.WriteAllBytes(path, bytes);
                                        }
                                        DonThuDAL.InsertFileYKienXuLy(infoFileHoSo);
                                    }
                                    if (listDonThuId == "")
                                    {
                                        listDonThuId += temp.XuLyDonID;
                                    }
                                    else
                                    {
                                        listDonThuId += ";" + temp.XuLyDonID;
                                    }

                                }
                                else
                                {
                                    listDonThuIdFail += ";" + temp.XuLyDonID;
                                }
                                //Utilities.WriteLog("Insert: " + JsonConvert.SerializeObject(item));

                            }
                        }

                    }

                    if (listDonThuId != "")
                    {
                        var credentials = new FormUrlEncodedContent(new[] {
                      new KeyValuePair<string, string>("XLDIDstr", listDonThuId)
                    });

                        var res = api.PostApi("", "application/json", urlResponse, credentials);
                        if (res.IsSuccessStatusCode)
                        {
                            Utilities.WriteLog("Đồng bộ thành công: " + listDonThuId);
                            if (listDonThuIdFail != "")
                                Utilities.WriteLog("Đồng bộ thất bại: " + listDonThuIdFail);
                        }
                        else
                        {
                            Utilities.WriteLog("Đồng bộ thất bại");
                        }
                    }
                    Utilities.WriteLog("######################################################## End ########################################################");
                }


            }
            catch (Exception ex)
            {
                Utilities.WriteLog(ex);
                throw;
            }


        }
        public DonThuInfo ConvertDonThu(DonThuModel DonThu)
        {
            var temp = new DonThuInfo();
            temp.XuLyDonID = DonThu.XuLyDonID;
            temp.SoDonThu = DonThu.SoDonThu;
            temp.NgayBanHanh = DonThu.NgayNhapDonStr;
            temp.NgayTiepNhan = DonThu.NgayNhapDonStr;
            temp.CanBoTiepNhan = DonThu.TenCanBoTiepNhan;
            temp.CoQuanID = DonThu.CoQuanID;
            temp.CoQuanTiepNhan = DonThu.TenCoQuanTiepNhan;
            temp.NoiDungDon = DonThu.NoiDungDon;
            temp.DoiTuongKhieuNai = DonThu.NhomKNInfo.StringLoaiDoiTuongKN;

            temp.CoQuanGiaiQuyet = DonThu.TenCoQuanGQ;
            temp.TrangThaiDonThu = DonThu.TrangThaiDonThu;
            temp.TrangThaiDonID = DonThu.TrangThaiDonID;
            //temp.NguoiDaiDien = DonThu.
            var doituong = DonThu.lsDoiTuongKN.FirstOrDefault();
            if (doituong != null)
            {
                temp.NguoiDaiDien = doituong.HoTen;
                temp.DiaChi = doituong.DiaChiCT;

            }
            temp.CoQuanBanHanhID = DonThu.CoQuanBanHanhID;
            temp.CoQuanXuLyID = DonThu.CoQuanXuLyID;
            temp.PhongBanXuLy = DonThu.PhongBanXuLy;
            temp.CoQuanGiaiQuyetID = DonThu.CoQuanGiaiQuyetID;
            temp.HuongXuLy = DonThu.HuongXuLy;
            temp.SoTienPhaiThu = DonThu.SoTienPhaiThu;
            temp.SoDatPhaiThu = DonThu.SoDatPhaiThu;
            temp.SoDoiTuongBiXuLy = DonThu.SoDoiTuongBiXuLy;
            temp.NgayXuLyStr = DonThu.NgayXuLy;
          
     
            //temp.TenQuyetDinh = DonThu.
            return temp;
        }
    }
}
