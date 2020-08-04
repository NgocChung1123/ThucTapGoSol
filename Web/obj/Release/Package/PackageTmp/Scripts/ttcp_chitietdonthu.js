var chitietTemplate = "";

function ChiTietDonThu(donthuid, xulydonid, type, isShowQuaTrinhGQ,nhomKNID) {
    alert(1);
    if (isShowQuaTrinhGQ == null) isShowQuaTrinhGQ = false;

    $("#MainContent_hdfDonThuID").val(donthuid);
    $("#MainContent_hdfXuLyDonID").val(xulydonid);
    $("#MainContent_hdfNhomKNID").val(nhomKNID);
    $.post("/Handler/ChiTietDonThu/GetDonThuByID.ashx", { DonThuID: donthuid,XuLyDonID: xulydonid}).done(function (data) {
        var info = JSON.parse(data);

        if (info == null) {
            $("#divThongTinXyLy").hide();
            $("#divThongTinGQ").hide();
            $("#divLuongDon").hide();
            $("#div_filedinhkem").hide();
            $("#div_doituongbkn").hide();
            $("#thongTinXuLyLan1").hide();

            $("#lblNguonDonDen").html("Trực tiếp");

            $.post("/Handler/ChiTietDonThu/GetListCaNhanKN.ashx", { NhomKNID: nhomKNID }).done(function (data) {
                var canhankns = JSON.parse(data)
                if (canhankns.length > 0) {

                    for (var i = 0; i < canhankns.length; i++) {
                        var gioitinh = "Nữ";
                        if (canhankns[i].GioiTinh == 0) {
                            gioitinh = "Nam";
                        }
                        $("#ltrNguoiDaiDien").html($("#ltrNguoiDaiDien").html() + "<div class='nguoidaidien'><b>Người đại diện " + (i + 1) + "</b><br/><label>Họ tên: </label><span class='info'>" + canhankns[i].HoTen + "</span><br/><label>CMND: </label><span class='info'>" + canhankns[i].CMND + "</span><br/><label>Giới tính: </label><span class='info'>" + gioitinh + "</span><br/><label>Nghề nghiệp: </label><span class='info'>" + canhankns[i].NgheNghiep + "</span><br/><label>Quốc tịch: </label><span class='info'>" + canhankns[i].TenQuocTich + "</span><br/><label>Dân tộc: </label><span class='info'>" + canhankns[i].TenDanToc + "</span><br/> <label>Địa chỉ: </label><span class='info'>" + canhankns[i].DiaChiCT + "</span><br/></div>");

                    }
                }
            });

            $.post("/Handler/ChiTietDonThu/GetDoiTuongKN.ashx", { NhomKNID: nhomKNID }).done(function (data) {
                var nhomkn = JSON.parse(data);
                if (nhomkn != null) {
                    if (nhomkn.StringLoaiDoiTuongKN == "CaNhan") {
                        $("#lblDoiTuongKhieuNai").html("Cá nhân");
                    }
                    if (nhomkn.StringLoaiDoiTuongKN == "CoQuan") {
                        $("#lblDoiTuongKhieuNai").html("Cơ quan, tổ chức");
                        $("#ltrDoiTuongKhieuNai").html("<label>Tên cơ quan</label><span class='info'>" + nhomkn.TenCQ + "</span><br/>" + "<label>Địa chỉ cơ quan:</label><span class='info'>" + nhomkn.DiaChiCQ + "</span><br/>");
                    }
                    if (nhomkn.StringLoaiDoiTuongKN == "TapThe") {
                        $("#lblDoiTuongKhieuNai").html("Tập thể");
                        $("#ltrDoiTuongKhieuNai").html("<label>Số lượng:</label><span class='info'>" + nhomkn.SoLuong + "</span><br/>");
                    }
                }
            });

            $.post("/Handler/ChiTietDonThu/GetTiepDanKhongDonByID.ashx", { NhomKNID: nhomKNID }).done(function (data) {
                var infoTD = JSON.parse(data);          
                if (infoTD != null) {
                    $("#lblNgayTiepNhan").html(infoTD.NgayTiepStr); 
                    $("#lblCanBoTiepNhan").html(infoTD.TenCanBoTiep);
                    $("#lblSoDon").html(infoTD.SoDon)

                    $("#lblLoaiKhieuTo").html(infoTD.TenLoaiKhieuTo1);
                    if (infoTD.TenLoaiKhieuTo2 != "") {
                        $("#lblLoaiKhieuTo").html($("#lblLoaiKhieuTo").html() + " > " + infoTD.TenLoaiKhieuTo2);
                    }
                    if (infoTD.TenLoaiKhieuTo3 != "") {
                        $("#lblLoaiKhieuTo").html($("#lblLoaiKhieuTo").html() + " > " + infoTD.TenLoaiKhieuTo3);
                    }

                }
            });

        }     
        else {
            $("#lblSoBienNhanMotCua").html(info.SoBienNhanMotCua);
            $("#lblMaHoSoMotCua").html(info.MaHoSoMotCua);
            $("#lblNgayHenTraMotCua").html(info.NgayHenTraMotCuaStr);
            $("#lblHoTenDoiTuongBiKNTC").html(info.TenDoiTuongBiKN);
            $("#lblCanBoTiepNhan").html(info.TenCanBoTiepNhan);
            $("#lblCQTiepNhan").html(info.TenCoQuanTiepNhan);
            $("#lblThongTinCQGQ").html(info.TenCoQuanGQ);
            $("#lblNgayTiepNhan").html(info.NgayTiepNhan);
            $("#lblTenCQDaGQ").html(info.CQDaGiaiQuyetID);                  
            $("#lblSoCongVan").html(info.SoCVBaoCaoGQ);
            $("#lblNgayGuiCongVan").html(info.NgayCVBaoCaoGQStr);
            $("#lblCQGiao").html(info.TenCQPhan);
                
            //xu ly don
            var hanXuLy = $("#MainContent_hdfHanXuLy").val();
            var hanGiaiQuyet = $("#MainContent_hdfHanGiaiQuyet").val();
            

            $("#lblHanXuLy").html(hanXuLy);//.html(info.HanXuLyStr);
            $("#lblCoQuanXuLy").html(info.TenCoQuanXL);
            $("#lblPhongBanXuLy").html(info.TenPhongBanXuLy);
            $("#lblCanBoXuLy").html(info.TenCanBoXuLy);
            $("#lblHuongXL").html(info.TenHuongGiaiQuyet);
            if (info.TenHuongGiaiQuyet == "Chuyển đơn") {
                if (info.TenHuongGiaiQuyet != null) {
                    $("#lblHuongXL").html($("#lblHuongXL").html() + " <br/> " + "<span>- CQ chuyển: </span>" + info.CoQuanChuyenDonDi + "" + " <br/> " + "<span>- Ngày chuyển: </span>" + info.NgayChuyenDonSangCQKhacStr + "");
                }
            }

            //$("#lblKetQuaXL").html();
            $("#lblNgayXuLy").html(info.NgayXuLyStr);
            if (info.NgayPhanStr != 0) {
                $("#lblNgayPhanCong").html(info.NgayPhanStr);
            }
            else {
                $("#lblNgayPhanCong").html("");
            }
            $("#lblSoDon").html(info.SoDonThu);
            $("#divTTXuLy1").show();
            //GQ Don
            // $("#lblCQGQ").html(info.TenCoQuanGQ);

            $("#lblTTHanGiaiQuyet").html(hanGiaiQuyet);

            $("#lblLoaiKhieuTo").html(info.TenLoaiKhieuTo1);
            if (info.TenLoaiKhieuTo2 != "") {
                $("#lblLoaiKhieuTo").html($("#lblLoaiKhieuTo").html() + " > " + info.TenLoaiKhieuTo2);
            }
            if (info.TenLoaiKhieuTo3 != "") {
                $("#lblLoaiKhieuTo").html($("#lblLoaiKhieuTo").html() + " > " + info.TenLoaiKhieuTo3);
            }

            $("#lblNoiDungDon").html(info.NoiDungDon);

            $("#lblNguonDonDen").html(info.TenNguonDonDen);
            if (info.TenNguonDonDen == "Cơ quan khác chuyển đến") {
                if (info.TenCQChuyenDonDen != null) {
                    $("#lblNguonDonDen").html($("#lblNguonDonDen").html() + " <br/> " + "<span>- CQ chuyển: </span>" + info.TenCQChuyenDonDen + "" + " <br/> " + "<span>- Ngày chuyển: </span>" + info.NgayChuyenDonStr + "" + " <br/> " + "<span>- Số công văn: </span>" + info.SoCongVan + "");
                }
            }
            
            if (info.LanGiaiQuyet === 2) {
                $("#thongTinXuLyLan1").show();
            }
            else
                $("#thongTinXuLyLan1").hide();

            $("#lblHuongXuLy").html(info.TenHuongGiaiQuyet);
            if (info.NoiDungHuongDan != "") {
                $("#lblHuongXuLy").append(". " + info.NoiDungHuongDan);
                $("#lblGhiChuChuyenDon").html(info.NoiDungHuongDan);
            }


            $.post("/Handler/ChiTietDonThu/GetListCaNhanKN.ashx", {
                NhomKNID: info.NhomKNID
            }).done(function (data) {
                var canhankns = JSON.parse(data)
                if (canhankns.length > 0) {

                    for (var i = 0; i < canhankns.length; i++) {
                        var gioitinh = "Nữ";
                        if (canhankns[i].GioiTinh == 0) {
                            gioitinh = "Nam";
                        }
                        $("#ltrNguoiDaiDien").html($("#ltrNguoiDaiDien").html() + "<div class='nguoidaidien'><b>Người đại diện " + (i + 1) + "</b><br/><label>Họ tên: </label><span class='info'>" + canhankns[i].HoTen + "</span><br/><label>CMND: </label><span class='info'>" + canhankns[i].CMND + "</span><br/><label>Giới tính: </label><span class='info'>" + gioitinh + "</span><br/><label>Nghề nghiệp: </label><span class='info'>" + canhankns[i].NgheNghiep + "</span><br/><label>Quốc tịch: </label><span class='info'>" + canhankns[i].TenQuocTich + "</span><br/><label>Dân tộc: </label><span class='info'>" + canhankns[i].TenDanToc + "</span><br/> <label>Địa chỉ: </label><span class='info'>" + canhankns[i].DiaChiCT + "</span><br/></div>");

                    }
                }
            });

            $.post("/Handler/ChiTietDonThu/GetDoiTuongKN.ashx", { NhomKNID: info.NhomKNID }).done(function (data) {
                var nhomkn = JSON.parse(data);
                if (nhomkn != null) {
                    if (nhomkn.StringLoaiDoiTuongKN == "CaNhan") {
                        $("#lblDoiTuongKhieuNai").html("Cá nhân");
                    }
                    if (nhomkn.StringLoaiDoiTuongKN == "CoQuan") {
                        $("#lblDoiTuongKhieuNai").html("Cơ quan, tổ chức");
                        $("#ltrDoiTuongKhieuNai").html("<label>Tên cơ quan</label><span class='info'>" + nhomkn.TenCQ + "</span><br/>" + "<label>Địa chỉ cơ quan:</label><span class='info'>" + nhomkn.DiaChiCQ + "</span><br/>");
                    }
                    if (nhomkn.StringLoaiDoiTuongKN == "TapThe") {
                        $("#lblDoiTuongKhieuNai").html("Tập thể");
                        $("#ltrDoiTuongKhieuNai").html("<label>Số lượng:</label><span class='info'>" + nhomkn.SoLuong + "</span><br/>");
                    }
                }
            });

            //list can bo giai quyet
            $.post("/Handler/ChiTietDonThu/GetCanBoGiaiQuyetByXLDonID.ashx", {
                XuLyDonID: xulydonid
            }).done(function (data) {
                var liscanbo = JSON.parse(data);
                if (liscanbo.length > 0) {

                    var htmlTemplateCanBoPhoiHop = $("#templateCanBoPhoiHop").html();
                    var htmlTemplateCanBoTheoDoi = $("#templateCanBoTheoDoi").html();

                    for (var i = 0; i < liscanbo.length; i++) {
                        if (liscanbo[i].VaiTroXacMinh == 1) {
                            $("#lblCanBoPhuTrach").html(liscanbo[i].TenCanBo);
                        }
                        else if (liscanbo[i].VaiTroXacMinh == 2) {
                            $("#ltrCanBoPhoiHop").html($("#ltrCanBoPhoiHop").html() + htmlTemplateCanBoPhoiHop.replace("%TENCANBO%", liscanbo[i].TenCanBo));
                        }

                        else if (liscanbo[i].VaiTroXacMinh == 3) {
                            $("#ltrCanBoTheoDoi").html($("#ltrCanBoTheoDoi").html() + htmlTemplateCanBoTheoDoi.replace("%TENCANBO%", liscanbo[i].TenCanBo));
                        }
                    }
                }
            });

            // get doi tuong bi khieu nai 
            $.post("/Handler/ChiTietDonThu/GetDoiTuongBiKN.ashx", { DoiTuongBiKNID: info.DoiTuongBiKNID }).done(function (data) {
                var doituongbikn = JSON.parse(data);
                if (doituongbikn != null) {
                    if (doituongbikn.StringLoaiDoiTuong == "CaNhan") {
                        $("#lblDoiTuongBiKN").html("Cá nhân");
                        $("#lblTenDoiTuongBiKN").html(doituongbikn.TenDoiTuongBiKN);
                        //$("#lblDiaChiDoiTuongBiKN").html(doituongbikn.DiaChiCT + " - " + doituongbikn.TenXa + " - " + doituongbikn.TenHuyen + " - " + doituongbikn.TenTinh);
                        $("#lblDiaChiDoiTuongBiKN").html(doituongbikn.DiaChiCT);

                        $.post("/Handler/ChiTietDonThu/GetCaNhanBiKN.ashx", { DoiTuongBiKNID: info.DoiTuongBiKNID }).done(function (data) {
                            var canhanbikn = JSON.parse(data);

                            $("#ltrNguoiBiKhieuNai").html("");
                            if (canhanbikn.CaNhanBiKNID != 0) {
                                //<h4>Thông tin cá nhân bị khiếu nại</h4>
                                var tenchucvu = "";
                                if (canhanbikn.TenChucVu != null) {
                                    // alert(canhanbikn.TenChucVu);
                                    tenchucvu = canhanbikn.TenChucVu;
                                }
                                $("#ltrNguoiBiKhieuNai").html("<label>Nghề nghiệp: </label><span class='info'>" + canhanbikn.NgheNghiep + "</span><br/><label>Nơi công tác: </label><span class='info'>" + canhanbikn.NoiCongTac + "</span><br/><label>Chức vụ: </label><span class='info'>" + tenchucvu + "</span><br/><label>Quốc tịch: </label><span class='info'>" + canhanbikn.TenQuocTich + "</span><br/><label>Dân tộc: </label><span class='info'>" + canhanbikn.TenDanToc + "</span><br/>");
                            }

                        });
                    }
                    else {
                        $("#lblDoiTuongBiKN").html("Cơ quan,tổ chức");
                        $("#lblTenDoiTuongBiKN").html(doituongbikn.TenDoiTuongBiKN);
                        $("#lblDiaChiDoiTuongBiKN").html(doituongbikn.DiaChiCT);
                    }
                    $("#div_doituongbkn").show();
                }
                else {
                    $("#div_doituongbkn").hide();
                }
            });

            //lay thong tin trang thai xu ly don thu
            $.post("/Handler/ChiTietDonThu/GetTrangThaiDon.ashx", { XuLyDonID: xulydonid }).done(function (data) {
                $("#lblTrangThaiXuLy").html(data);
            });
            // lay thong tin trang thai giai quyet don thu
            $.post("/Handler/ChiTietDonThu/GetTrangThaiGQDon.ashx", { XuLyDonID: xulydonid }).done(function (data) {
                $("#lblHuongGiaiQuyet").html(data);
            });
            //lay thong tin file ho so
            $.post("/Handler/ChiTietDonThu/GetFileHoSoDinhKem.ashx", {
                XuLyDonID: xulydonid
            }).done(function (data) {
                var filedinhkems = JSON.parse(data);
                $("#tbFileDinhKem >tbody").html("");
                if (filedinhkems.length != 0) {
                    for (var i = 0; i < filedinhkems.length; i++) {
                        $("#tbFileDinhKem >tbody").append("<tr><td style='text-align:center'>" + (i + 1) + "</td><td style='text-align:left'>" + filedinhkems[i].TenFile + "</td><td style='text-align:center'>" + filedinhkems[i].NgayUps + "</td><td style='text-align:center'>" + filedinhkems[i].TomTat + "</td><td style='text-align:center'><img alt='Tai-ve-may' class='img_button' style='cursor: pointer' id='Img1' title='Tải về máy' src='images/download.png' onclick='downloadFileScanNow(" + filedinhkems[i].FileHoSoID + ")' /></td></tr>");
                    }
                    $("#div_filedinhkem").show();
                }
                else {
                    $("#div_filedinhkem").hide();
                }
            });

            //lay thong tin don thu lan 1
            $.post("/Handler/ChiTietDonThu/GetDonThuLan1ByID.ashx", {
                DonThuID: donthuid
            }).done(function (data) {
                var donThuLan1 = JSON.parse(data);
                if (donThuLan1.length != 0) {
                    $("#lblNgayTiepLan1").html(donThuLan1.NgayTiepNhan);
                    $("#lblCQTiepNhanLan1").html(donThuLan1.TenCoQuanTiepNhan);
                    $("#lblHuongXuLyLan1").html(donThuLan1.HuongXuLy);
                    $("#lblKetQuaXuLyLan1").html(donThuLan1.YKienXuLy);
                    //$("#lblHanGiaiQuyetLan1").html(donThuLan1.NgayQuaHanGQStr);
                    $("#lblCoQuanGiaiQuyetLan1").html(donThuLan1.TenCoQuanGQ);
                    $("#lblKetQuaGQLan1").html(donThuLan1.YKienGiaiQuyet);
                }

            });

            //lay thong tin chuyendon
            $.post("/Handler/ChiTietDonThu/GetThongTinChuyenDon.ashx", { XuLyDonID: xulydonid }).done(function (data) {
                if (data != 'null') {
                    $("#chuyendonDiv").show();
                    var chuyendonInfo = JSON.parse(data);
                    $("#lblNgayChuyenDon").html(formatDate(chuyendonInfo.NgayChuyen));

                    if (chuyendonInfo.FileDinhKem != null && chuyendonInfo.FileDinhKem != "") {
                        var fileUrlArr = chuyendonInfo.FileDinhKem.split("/");
                        var fileUrl = fileUrlArr[fileUrlArr.length - 1];

                        $("#fileQDChuyenDon").html("<a style='text-decoration: underline; color: #0077ED' href='/UploadFiles/filechuyengq/" + fileUrl + "' target='_blank'>Tải xuống</a>");
                    }
                }

            });

            //lay thong tin rut don
            $.post("/Handler/ChiTietDonThu/GetThongTinRutDon.ashx", { XuLyDonID: xulydonid }).done(function (data) {
                if (data != 'null') {
                    $("#fileRutDonDiv").show();
                    var rutdonInfo = JSON.parse(data);
                    $("#lblLyDoRut").html(rutdonInfo.LyDo);
                    $("#fileRutDon").html("<a style='text-decoration: underline; color: #0077ED' href='" + rutdonInfo.FileQD + "' target='_blank'>Tải xuống</a>");
                }

            });

            ////lay thong tin qd phan giai quyet
            //$.post("/Handler/ChiTietDonThu/GetPhanGQ.ashx", { XuLyDonID: xulydonid }).done(function (data) {
            //    if (data != 'null') {
            //        var kqInfo = JSON.parse(data);
            //        $("#phanGQDiv").show();
            //        $("#lblGhiChuPhanGQ").html(kqInfo.GhiChu);
            //        if (kqInfo.FileUrl != "" && kqInfo.FileUrl != null) {
            //            $("#fileQDPhanGQ").html("<a style='text-decoration: underline; color: #0077ED' href='" + kqInfo.FileUrl + "' target='_blank'>Tải xuống</a>");
            //        }
            //        else $("#fileQDPhanGQ").html("Không có");
            //    }

            //});

            //lay thong tin ket qua thi hanh
            $.post("/Handler/ChiTietDonThu/GetKetQuaThiHanhByID.ashx", { XuLyDonID: xulydonid }).done(function (data) {
                if (data != 'null') {                  
                    var kqInfo = JSON.parse(data);
                    $("#lblCoQuanThucThi").html(kqInfo.TenCoQuanThiHanh);
                    $("#lblNgayThucThi").html(kqInfo.NgayThiHanhStr);
                    $("#lblKetQuaThiHanhTien").html(kqInfo.TienDaThu);
                    $("#lblKetQuaThiHanhDat").html(kqInfo.DatDaThu);
                    if (kqInfo.FileKetQua != "") {
                        $("#lblFileKQThiHanh").html("<a style='text-decoration: underline; color: #0077ED' href='" + kqInfo.FileKetQua + "' target='_blank'>Tải xuống</a>");
                    }
                    else $("#lblFileKQThiHanh").html("Không có");
                    $("#divThongTinQuaTrinhThiHanh").show();
                }

            });

            //lay thong tin quyet dinh giai quyet don
            $.post("/Handler/ChiTietDonThu/GetQuyetDinhGQ.ashx", { XuLyDonID: xulydonid }).done(function (data) {
                if (data != 'null') {
                    var kqInfo = JSON.parse(data);
                    $("#quyetDinhDiv").show();
                    $("#lblLoaiKetQua").html(kqInfo.TenLoaiKetQua);
                    $("#lblCQRaKetQua").html(kqInfo.TenCoQuan);
                    $("#lblNgayRaKQ").html(kqInfo.NgayRaKQStr);
                    $("#lblKetQuaTien").html(kqInfo.SoTien);

                    $("#lblKetQuaDat").html(kqInfo.SoDat);


                    if (kqInfo.FileUrl != "") {
                        $("#lblFileQuyetDinh").html("<a style='text-decoration: underline; color: #0077ED' href='" + kqInfo.FileUrl + "' target='_blank'>Tải xuống</a>");
                    }
                    else $("#lblFileQuyetDinh").html("Không có");
                    $("#divThongTinQDGQDon").show();
                }

            });
            // lay thong tin qua trinh xac minh
            $.post("/Handler/ChiTietDonThu/GetQuaTrinhXacMinhNoiDungDon.ashx", { XuLyDonID: xulydonid }).done(function (data) {
                if (data != '[]') {
                    var kqXMList = JSON.parse(data);
                    for (var i = 0; i < kqXMList.length; i++) {
                        var str = "";
                        var fileDinhKemStr = "";

                        if (kqXMList[i].DuongDanFile != "") {
                            fileDinhKemStr = "<a href='" + kqXMList[i].DuongDanFile + "' target='_blank'><img src='images/ic_attach_file.png' class='image-icon'/></a>";
                        }
                        else fileDinhKemStr = "";

                        $("#dtXacMinhTable > tbody").append("<tr> <td>" + (i + 1) + "</td> <td style='text-align: left'>" + kqXMList[i].GhiChu + "</td> <td style='text-align: center'>" + formatDate(kqXMList[i].NgayCapNhat) + "</td> <td style='text-align: center'>" + fileDinhKemStr + "</td> </tr>");
                        $("#divQuaTrinhXacMinh").show();
                    }
                }
                else $("#divQuaTrinhXacMinh").hide();
            });

            // lay thong tin quyet dinh giao xac minh
            $.post("/Handler/ChiTietDonThu/GetQuyetDinhGiaoXacMinh.ashx", { XuLyDonID: xulydonid }).done(function (data) {
                if (data != '[]') {
                    var qGiaoXM = JSON.parse(data);
                    for (var i = 0; i < qGiaoXM.length; i++) {
                        var str = "";
                        var fileDinhKemStr = "";

                        if (qGiaoXM[i].FileUrl != "") {
                            fileDinhKemStr = "<a href='" + qGiaoXM[i].FileUrl + "' target='_blank'><img src='images/ic_attach_file.png' class='image-icon'/>" + qGiaoXM[i].TenFile + "</a>";
                        }
                        else fileDinhKemStr = "";

                        $("#quyetDinhGiaoXacMinhTable > tbody").append("<tr> <td>" + (i + 1) + "</td> <td style='text-align: center'>" + fileDinhKemStr + "</td> <td style='text-align: left'>" + qGiaoXM[i].TenCoQuanPhan + "</td> <td style='text-align: center'>" + formatDate(qGiaoXM[i].NgayChuyen) + "</td> </tr>");
                        $("#divQuyetDinhGiaoXacMinh").show();
                    }
                }
                else $("#divQuyetDinhGiaoXacMinh").hide();
            });



            //  sua y kien xu ly
            $.post("/Handler/ChiTietDonThu/YKienXuLyGetByID.ashx", { XuLyDonID: xulydonid }).done(function (data) {
                if (data != '[]') {
                    var lsYKienXL = JSON.parse(data);
                    for (var i = 0; i < lsYKienXL.length; i++) {                
                        var hiendownload = "";
                        var tenfile = "", text = "";
                        if (lsYKienXL[i].FileUrl != "") {
                            
                            tenfile = lsYKienXL[i].TenFile;//.substring(0, 20) + "...";
                            hiendownload = " <a id='hdfFileID" + i + "' href='" + lsYKienXL[i].FileUrl + "' download='" + tenfile + "' title='" + lsYKienXL[i].TenFile + "'><img src='images/download.png' class='image' style='float:left'/> <span style='float:left'>&nbsp" + tenfile + "</span> </a>"
                        }
                        $("#tbketquaxulydon_CT >tbody").append("<tr><td>" + (i + 1) + "</td> <td>" + lsYKienXL[i].TenCanBoXuLy + "</td> <td style='text-align:center'>" + lsYKienXL[i].NgayXuLyStr + "</td> <td style='text-align:left'>" + lsYKienXL[i].YKienXuLy + "</td></tr>");
                    }
                    $("#divKetQuaXuLyDon").show();
                }
                else $("#divKetQuaXuLyDon").hide();
            });

            
            //file y kien xl
            $.post("/Handler/ChiTietDonThu/FileYKienXuLyGetByID.ashx", { XuLyDonID: xulydonid }).done(function (data) {
                if (data != '[]') {
                    var lsFileYKienXL = JSON.parse(data);
                    for (var i = 0; i < lsFileYKienXL.length; i++) {
                        var hiendownload = "";
                        var tenfile = "", text = "";
                        if (lsFileYKienXL[i].FileURL != "") {

                            tenfile = lsFileYKienXL[i].TenFileYKienXL;//.substring(0, 20) + "...";
                            hiendownload = " <a id='hdfFileID" + i + "' href='#' onclick='downloadFileYKienXuLy(" + lsFileYKienXL[i].FileYKienXuLyID + ")' download='" + tenfile + "' title='" + lsFileYKienXL[i].TenFileYKienXL + "'><img src='images/download.png' class='image' style='float:left'/> <span style='float:left'>&nbsp" + tenfile + "</span> </a>"
                        }
                        $("#tbfileykienxuly_CT >tbody").append("<tr><td>" + (i + 1) + "</td>  <td>" + lsFileYKienXL[i].TenFileYKienXL + "</td> <td style='text-align:center'>" + lsFileYKienXL[i].NgayUps + "</td> <td  style=' text-align:center'>" + hiendownload + "</td></tr>");
                    }
                    $("#divFileYKienXL").show();
                }
                else $("#divFileYKienXL").hide();
            });

            //  sua y kien giai quyet
            $.post("/Handler/ChiTietDonThu/YKienGiaiQuyetGetByID.ashx", { XuLyDonID: xulydonid }).done(function (data) {
                if (data != '[]') {
                    var lsYKienGQ = JSON.parse(data);
                    for (var i = 0; i < lsYKienGQ.length; i++) {
                        var hiendownload = "";
                        var tenfile = "", text = "";
                        if (lsYKienGQ[i].FileUrl != "") {
                            tenfile = lsYKienGQ[i].TenFile.substring(0, 20) + "...";
                            hiendownload = " <a id='hdfFileID" + i + "' href='" + lsYKienGQ[i].FileUrl + "' download='" + tenfile + "' title='" + lsYKienGQ[i].TenFile + "'><img src='images/download.png' class='image' style='float:left'/> <span style='float:left'>&nbsp" + tenfile + "</span> </a>"
                        }
                        $("#tbketquagiaiquyetdon >tbody").append("<tr><td>" + (i + 1) + "</td> <td  style=' text-align:center'>" + hiendownload + "</td> <td>" + lsYKienGQ[i].TenCanBoGiaiQuyet + "</td> <td style='text-align:center'>" + lsYKienGQ[i].NgayGiaiQuyetStr + "</td> <td style='text-align:left'>" + lsYKienGQ[i].YKienGiaiQuyet + "</td></tr>");
                    }
                    $("#divKetQuaGiaiQuyetDon").show();
                }
                else $("#divKetQuaGiaiQuyetDon").hide();
            });

            //lay thong tin qua trinh giai quyet
            $.post("/Handler/ChiTietDonThu/GetQuaTrinhGiaiQuyet.ashx", { XuLyDonID: xulydonid }).done(function (data) {
                if (data != '[]') {
                    var qtGiaiQuyetList = JSON.parse(data);

                    for (var i = 0; i < qtGiaiQuyetList.length; i++) {
                        var str = "";
                        var fileDinhKemStr = "";

                        if (qtGiaiQuyetList[i].DuongDanFile != "") {
                            fileDinhKemStr = "<a href='" + qtGiaiQuyetList[i].DuongDanFile + "' target='_blank'><img src='images/ic_attach_file.png' class='image-icon'/></a>";
                        }

                        $("#qtGiaiQuyetTable > tbody").append("<tr><td>" + (i + 1) + "</td><td style='text-align: left'>" + qtGiaiQuyetList[i].GhiChu + "</td><td style='text-align: center'>" + formatDate(qtGiaiQuyetList[i].NgayCapNhat) + "</td> <td style='text-align: center'>" + fileDinhKemStr + "</td></tr>");
                    }
                }
                else {
                }
            });

            //Lay thong tin luong don
            $.post("/Handler/ChiTietDonThu/GetLuongDonByID.ashx", { XuLyDonID: xulydonid }).done(function (data) {

                if (data != '[]') {
                    var lsLuongDon = JSON.parse(data);
                    for (var i = 0; i < lsLuongDon.length; i++) {
                        $("#luongDonTable > tbody").append("<tr> <td>" + (i + 1) + "</td> <td style='text-align: left'>" + lsLuongDon[i].BuocThucHien + "</td> <td style='text-align: center'>" + formatDate(lsLuongDon[i].ThoiGianThucHien) + "</td> <td style='text-align: left'>" + lsLuongDon[i].CanBoThucHien + "</td> <td style='text-align: center'>" + formatDate(lsLuongDon[i].DueDate) + "</td>   <td style='text-align: left'>" + lsLuongDon[i].ThaoTac + "</td> <td style='text-align: left'>" + lsLuongDon[i].YKienCanBo + "</td> </tr>");
                    }
                    $("#divThongTinLuongDon").show();
                }
                else {
                    $("#divThongTinLuongDon").hide();
                }
            });
        } 
    });

    if (type == 1) {
        
        showChiTietDon1(xulydonid);
        $("#hdfFlagClose").val(1);
    }
    else {
        $("#hdfFlagClose").val(2);
        showChiTietDon2();
    }
}

function downloadFileScanNow(id) {

    window.open("/DownloadFileHoSo.aspx?ma_file=" + id);
}

function hideChiTietDon2() {
    $("#fade1").hide();
    $("#chitiet_div").hide();
}

function showChiTietDon1(xulydonid) {
    
    $('input[type=checkbox]').removeAttr('checked');
    $('tr').removeClass('selected_hl');
    $("#" + xulydonid).addClass('selected_hl');
    $("#CheckBox" + xulydonid).attr('checked', 'checked');

    if ($("#tabs").html() != "")
        chitietTemplate = $("#tabs").html();

    $('.div_ContentCTDonThu').html(chitietTemplate);
    
    //$(".div_ContentCTDonThu").tabs();

    //Tab o CT don thu
    $(".div_ContentCTDonThu ul li a").click(function () {

        $(this).addClass("active");
        $(this).parent().siblings("li").children("a").removeClass("active");

        var tabId = $(this).attr('href');
        $(tabId).siblings("div").removeClass("active");
        $(tabId).addClass("active");
        return false;
    });        
    
    showThaoTac();
    $('.div_ChiTietDonThu').show();
    $('.list-info').hide();

    $("#tabs").html("");

    

}

function hideChiTietDon() {
    
    var flag = $("#hdfFlagClose").val();
    
    if (flag == 1) {
        hideThaoTac();
        $('.div_ChiTietDonThu').hide();
        $('.list-info').show();
    }
    else {
        hideChiTietDon2();
    }
}

function showChiTietDon2() {
    $("#fade1").show();
    $("#chitiet_div").show();
}

// an chi tiet don cua nhung man hinh lay nhieu don
function hideCTDon() {
    //var trinhTPDuyetXL = 5;
    //var trinhLDDuyetXL = 6;
    //var trinhLDDuyetXLTrucTiep = 7;

    var stateLDPhanXL = 1;
    var stateTPPhanXL = 12;

    var transitionID = $("#Maincontent_hdfTransitionID").val();
    var roleID = $("#MainContent_hdfRoles").val();
    var xulydonid = ("#MainContent_hdfXuLyDonID").val();
    var stateID = $("#MainContent_hdfStateID").val();

    //if (roleID == 1) {
    //    if (transitionID == trinhLDDuyetXL || transitionID == trinhLDDuyetXLTrucTiep) {
    //        $('.image-button').show();
    //    } else {
    //        $('.image-button').hide();
    //    }
    //}
    //if (roleID == 2) {
    //    if (transitionID == trinhTPDuyetXL) {
    //        $('.image-button').show();
    //    } else {
    //        $('.image-button').hide();
    //    }
    //}
    //if (roleID == 1) {
    //    if (stateID == 6 ) {
    //        $('#MainContent_btnPheDuyet_CT').show();
    //    } else {
    //        $('#MainContent_btnPheDuyet_CT').hide();
    //    }

    //    if (stateID == stateLDPhanXL) {
    //        $('#MainContent_btnPhanXuLy_CT').show();
    //    } else {
    //        $('#MainContent_btnPhanXuLy_CT').hide();
    //    }
    //}
    //if (roleID == 2) {
    //    if (stateID == 5) {
    //        $('#MainContent_btnPheDuyet_CT').show();
    //    } else {
    //        $('#MainContent_btnPheDuyet_CT').hide();
    //    }

    //    if (stateID == stateTPPhanXL) {
    //        $('#MainContent_btnPhanXuLy_CT').show();
    //    } else {
    //        $('#MainContent_btnPhanXuLy_CT').hide();
    //    }
    //}


    //$('input[type=checkbox]').removeAttr('checked');
    //$('tr').removeClass('selected_hl');
    //$("#" + xulydonid).addClass('selected_hl');
    //$("#CheckBox" + xulydonid).attr('checked', 'checked');
}

