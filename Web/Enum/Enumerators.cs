using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Com.Gosol.CMS.Web.Enum
{
    public enum PhieuInEnum
    {
        TuChoiTiepCongDan = 1,
        GiayBienNhanTLBC = 2,
        PhieuDeXuatThuLy = 3,
        PhieuHen = 4,
        PhieuHDNguoiKN = 5,
        PhieuHDNguoiTC = 6,
        DonDocGiaiQuyet = 7,
        ChuyenDonKNPA = 8
    }

    public enum HuongXuLyEnum
    {
        HuongDan = 30,
        DeXuatThuLy = 31,
        ChuyenDon = 32,
        TraDon = 33,
        RaVBDonDoc = 34,
        RaVBThongBao = 35
    }

    public enum RoleEnum
    {
        LanhDao = 1,
        LanhDaoPhong = 2,
        ChuyenVien = 3
    }
}
