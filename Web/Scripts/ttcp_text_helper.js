/**
 * @author trungnm
 */

if (! window.chuanHoaChuoi)
{
	// chuan hoa chuoi
	// src: "   ĐI  họC RẤT    LÀ VuI   NhỂ     "
	// result: "ĐI học RẤT LÀ VuI NhỂ"
	function chuanHoaChuoi(s)
	{
		s = $.trim(s);
		
		// loai bo khoang trong thua o giua cac tu
		var sArr = s.split(" ");
		s = "";
		for (i = 0; i < sArr.length; i++)
		{
			if ($.trim(sArr[i]) == "")
				continue;
			s = s + sArr[i] + " ";
		}
		s = $.trim(s);
		return s;
	}
}

if (! window.chuanHoaChuoiLowercase)
{
	// chuan hoa chuoi
	// src: "   ĐI  họC RẤT    LÀ VuI   NhỂ     "
	// result: "Đi học rất là vui nhể"
	function chuanHoaChuoiLowercase(s)
	{
		s = $.trim(s);
		
		// loai bo khoang trong thua o giua cac tu
		var sArr = s.split(" ");
		s = "";
		for (i = 0; i < sArr.length; i++)
		{
			if ($.trim(sArr[i]) == "")
				continue;
			s = s + sArr[i] + " ";
		}
		s = $.trim(s);
		// chuan hoa cac tu
		sArr = s.split(" ");
		var i = 0;
		s = "";
		for (i = 0; i < sArr.length; i++)
		{
			if (i==0)
			{
				var s1 = sArr[i].substr(0,1).toUpperCase();
				var s2 = sArr[i].substr(1,sArr[i].length-1).toLowerCase();
				sArr[i] = s1 + s2 + " ";
			}
			else
				sArr[i] = sArr[i].toLowerCase() + " ";			
			s = s + sArr[i];
		}
		return $.trim(s);
	}
}

if (! window.chuanHoaTen)
{
	// chuan hoa chuoi
	// src: "   ĐI  họC RẤT    LÀ VuI   NhỂ     "
	// result: "Đi Học Rất Là Vui Nhể"
	function chuanHoaTen(s)
	{
		s = $.trim(s);
		
		// loai bo khoang trong thua o giua cac tu
		var sArr = s.split(" ");
		s = "";
		for (i = 0; i < sArr.length; i++)
		{
			if ($.trim(sArr[i]) == "")
				continue;
			s = s + sArr[i] + " ";
		}
		s = $.trim(s);
		// chuan hoa cac tu
		sArr = s.split(" ");
		var i = 0;
		s = "";
		for (i = 0; i < sArr.length; i++)
		{
			var s1 = sArr[i].substr(0,1).toUpperCase();
			var s2 = sArr[i].substr(1,sArr[i].length-1).toLowerCase();
			sArr[i] = s1 + s2 + " ";
			s = s + sArr[i];
		}
		return $.trim(s);
	}
}

if (! window.loai_bo_dau_tieng_viet_cho_sort)
{
	//loại bỏ dấu tiếng việt cho việc sắp xếp theo quy tắc
	// a < à < ả < ã < á < ạ < ă < ... < â
	// e < ê
	// i		
	// o < ô < ơ		
	// u < ư
	// y
	// d < đ
	function loai_bo_dau_tieng_viet_cho_sort(s)
	{	
		//alert(s.indexOf("Ấ"));
		//console.log(s.indexOf("Ấ"));
		s = chuanHoaChuoi(s);
		s = s.toLowerCase();
		var src = new Array();
		var replace = new Array();
	
		src.push("đ"); replace.push("dw");
		
		src.push("à"); replace.push("awa2");
		src.push("ả"); replace.push("awa3");
		src.push("ã"); replace.push("awa4");
		src.push("á"); replace.push("awa5");
		src.push("ạ"); replace.push("awa6");
	
		src.push("ă"); replace.push("awc1");
		src.push("ằ"); replace.push("awc2");
		src.push("ẳ"); replace.push("awc3");
		src.push("ẵ"); replace.push("awc4");
		src.push("ắ"); replace.push("awc5");
		src.push("ặ"); replace.push("awc6");
		
		src.push("â"); replace.push("awd1");
		src.push("ầ"); replace.push("awd2");
		src.push("ẩ"); replace.push("awd3");
		src.push("ẫ"); replace.push("awd4");
		src.push("ấ"); replace.push("awd5");
		src.push("ậ"); replace.push("awd6");		
	
		src.push("è"); replace.push("ewa2");
		src.push("ẻ"); replace.push("ewa3");
		src.push("ẽ"); replace.push("ewa4");
		src.push("é"); replace.push("ewa5");
		src.push("ẹ"); replace.push("ewa6");
	
		src.push("ê"); replace.push("ewb1");
		src.push("ề"); replace.push("ewb2");
		src.push("ể"); replace.push("ewb3");
		src.push("ễ"); replace.push("ewb4");
		src.push("ế"); replace.push("ewb5");
		src.push("ệ"); replace.push("ewb6");
	
		src.push("ì"); replace.push("iwa2");
		src.push("ỉ"); replace.push("iwa3");
		src.push("ĩ"); replace.push("iwa4");
		src.push("í"); replace.push("iwa5");
		src.push("ị"); replace.push("iwa6");
	
		src.push("ò"); replace.push("owa2");
		src.push("ỏ"); replace.push("owa3");
		src.push("õ"); replace.push("owa4");
		src.push("ó"); replace.push("owa5");
		src.push("ọ"); replace.push("owa6");
	
		src.push("ô"); replace.push("owb1");
		src.push("ồ"); replace.push("owb2");
		src.push("ổ"); replace.push("owb3");
		src.push("ỗ"); replace.push("owb4");
		src.push("ố"); replace.push("owb5");
		src.push("ộ"); replace.push("owb6");
	
		src.push("ơ"); replace.push("owc1");
		src.push("ờ"); replace.push("owc2");
		src.push("ở"); replace.push("owc3");
		src.push("ỡ"); replace.push("owc4");
		src.push("ớ"); replace.push("owc5");
		src.push("ợ"); replace.push("owc6");
	
		src.push("ù"); replace.push("uwa2");
		src.push("ủ"); replace.push("uwa3");
		src.push("ũ"); replace.push("uwa4");
		src.push("ú"); replace.push("uwa5");
		src.push("ụ"); replace.push("uwa6");
	
		src.push("ư"); replace.push("uwb1");
		src.push("ừ"); replace.push("uwb2");
		src.push("ử"); replace.push("uwb3");
		src.push("ữ"); replace.push("uwb4");
		src.push("ứ"); replace.push("uwb5");
		src.push("ự"); replace.push("uwb6");
		
		src.push("ỳ"); replace.push("ywa2");
		src.push("ỷ"); replace.push("ywa3");
		src.push("ỹ"); replace.push("ywa4");
		src.push("ý"); replace.push("ywa5");
		src.push("ỵ"); replace.push("ywa6");
	
		var i = 0;
		for (i=0; i < src.length; i++)
		{
			s = s.replace(src[i],replace[i]);
		}
		return s;
	}
}

if (! window.so_sanh_chuoi)
{
	// so sánh 2 chuỗi tiếng việt với nhau 
	// tra ve 1 neu chuoi s1 > s2
	// tra ve -1 neu chuoi s1 <= s2
	function so_sanh_chuoi(s1, s2)
	{
		var s1_t = loai_bo_dau_tieng_viet_cho_sort(s1);
		var s2_t = loai_bo_dau_tieng_viet_cho_sort(s2);
		return s1_t > s2_t ? 1 : -1;
	}
}

if (! window.so_sanh_ten)
{
	// so sánh 2 ten tiếng việt với nhau
	// so sanh ten roi so sanh ho + ten dem
	// 	tra ve 1 neu ten s1 > s2
	// 	tra ve -1 neu ten s1 <= s2
	function so_sanh_ten(s1a, s2a)
	{
		var s1 = chuanHoaChuoi(s1a);
		s1 = s1.toLowerCase();
		var s2 = chuanHoaChuoi(s2a);
		s2 = s2.toLowerCase();
		
		if (s1 == "")
			return -1;
		if (s2 == "")
			return 1;
		
		var a1 = s1.split(" ");
		var a2 = s2.split(" ");
		
		var ten1 = a1[a1.length - 1];
		var ten2 = a2[a2.length - 1];
		
		if (ten1 != ten2)
		{
			return so_sanh_chuoi(ten1, ten2);
		}
		
		var i;
		var ho1 = "", ho2 = "";
		for (i = 0; i < a1.length-1; i++)
		{
			ho1 += a1[i] + " ";
		}
		for (i = 0; i < a2.length-1; i++)
		{
			ho2 += a2[i] + " ";
		}
		return so_sanh_chuoi(ho1, ho2);
		
	}
}
