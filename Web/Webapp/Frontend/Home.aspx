﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Frontend.Master" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="Com.Gosol.CMS.Web.Webapp.Frontend.Home" %>

<%@ Register Src="~/Webapp/Frontend/SideBarTinNoiBat.ascx" TagPrefix="uc1" TagName="SideBarTinNoiBat" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <style>
        .form-control {
            height: 34px !important;
        }

        .panel-heading {
            border-radius: 0px !important;
        }

        .panel {
            border-radius: 0px !important;
        }

        select 
        {
            border: 1px solid #ccc;
            height:28px !important;
        }

        #u41_div {
            border-width: 0px;
            position: absolute;
            left: 0px;
            top: 120px;
            width: 830px;
            height: 30px;
            background: inherit;
            background-color: rgba(22, 155, 213, 1);
            border: none;
            border-radius: 10px;
            -moz-box-shadow: none;
            -webkit-box-shadow: none;
            box-shadow: none;
            font-family: 'Varela Round Regular', 'Varela Round';
            font-weight: 400;
            font-style: normal;
        }

        #u41 {
            border-width: 0px;
            position: absolute;
            left: 0px;
            top: 0px;
            width: 830px;
            height: 30px;
            font-family: 'Varela Round Regular', 'Varela Round';
            font-weight: 400;
            font-style: normal;
        }

        #u41_text {
            border-width: 0px;
            position: absolute;
            left: 0px;
            top: 0px;
            width: 0px;
            visibility: hidden;
            word-wrap: break-word;
        }

        #u42_div {
            border-width: 0px;
            position: absolute;
            left: 0px;
            top: 0px;
            width: 830px;
            height: 30px;
            background: inherit;
            background-color: rgba(174, 174, 174, 1);
            border: none;
            border-radius: 10px;
            -moz-box-shadow: none;
            -webkit-box-shadow: none;
            box-shadow: none;
            font-family: 'Varela Round Regular', 'Varela Round';
            font-weight: 400;
            font-style: normal;
        }

        #u42 {
            border-width: 0px;
            position: absolute;
            left: 0px;
            top: 150px;
            width: 830px;
            height: 30px;
            font-family: 'Varela Round Regular', 'Varela Round';
            font-weight: 400;
            font-style: normal;
        }

        #u42_text {
            border-width: 0px;
            position: absolute;
            left: 0px;
            top: 0px;
            width: 0px;
            visibility: hidden;
            word-wrap: break-word;
        }

        #u43_div {
            border-width: 0px;
            position: absolute;
            left: 0px;
            top: 0px;
            width: 61px;
            height: 17px;
            background: inherit;
            background-color: rgba(255, 255, 255, 0);
            border: none;
            border-radius: 0px;
            -moz-box-shadow: none;
            -webkit-box-shadow: none;
            box-shadow: none;
            font-family: 'Varela Round Bold', 'Varela Round' !important;
            font-weight: 700 !important;
            font-style: normal;
            color: #FFFFFF;
        }

        #u43 {
            border-width: 0px;
            position: absolute;
            left: 27px;
            top: 128px;
            width: 61px;
            height: 17px;
            font-family: 'Varela Round Bold', 'Varela Round';
            font-weight: 700;
            font-style: normal;
            color: #FFFFFF;
        }

        #u43_text {
            font-size: 14px;
            font-weight: bold;
            border-width: 0px;
            position: absolute;
            left: 0px;
            top: 0px;
            width: 61px;
            white-space: nowrap;
        }

        #u44_div {
            border-width: 0px;
            position: absolute;
            left: 0px;
            top: 0px;
            width: 65px;
            height: 17px;
            background: inherit;
            background-color: rgba(255, 255, 255, 0);
            border: none;
            border-radius: 0px;
            -moz-box-shadow: none;
            -webkit-box-shadow: none;
            box-shadow: none;
            font-family: 'Varela Round Bold', 'Varela Round';
            font-weight: 700;
            font-style: normal;
            color: #FFFFFF;
        }

        #u44 {
            border-width: 0px;
            position: absolute;
            left: 135px;
            top: 128px;
            width: 65px;
            height: 17px;
            font-family: 'Varela Round Bold', 'Varela Round';
            font-weight: 700;
            font-style: normal;
            color: #FFFFFF;
        }

        #u44_text {
            font-size: 14px;
            font-weight: bold;
            border-width: 0px;
            position: absolute;
            left: 0px;
            top: 0px;
            width: 65px;
            white-space: nowrap;
        }

        #u45_div {
            border-width: 0px;
            position: absolute;
            left: 0px;
            top: 0px;
            width: 92px;
            height: 17px;
            background: inherit;
            background-color: rgba(255, 255, 255, 0);
            border: none;
            border-radius: 0px;
            -moz-box-shadow: none;
            -webkit-box-shadow: none;
            box-shadow: none;
            font-family: 'Varela Round Bold', 'Varela Round';
            font-weight: 700;
            font-style: normal;
            color: #FFFFFF;
        }

        #u45 {
            border-width: 0px;
            position: absolute;
            left: 240px;
            top: 128px;
            width: 92px;
            height: 17px;
            font-family: 'Varela Round Bold', 'Varela Round';
            font-weight: 700;
            font-style: normal;
            color: #FFFFFF;
        }

        #u45_text {
            font-size: 14px;
            font-weight: bold;
            border-width: 0px;
            position: absolute;
            left: 0px;
            top: 0px;
            width: 92px;
            white-space: nowrap;
        }

        #u46_div {
            border-width: 0px;
            position: absolute;
            left: 0px;
            top: 0px;
            width: 74px;
            height: 17px;
            background: inherit;
            background-color: rgba(255, 255, 255, 0);
            border: none;
            border-radius: 0px;
            -moz-box-shadow: none;
            -webkit-box-shadow: none;
            box-shadow: none;
            font-family: 'Varela Round Bold', 'Varela Round';
            font-weight: 700;
            font-style: normal;
            color: #FFFFFF;
        }

        #u46 {
            border-width: 0px;
            position: absolute;
            left: 375px;
            top: 128px;
            width: 74px;
            height: 17px;
            font-family: 'Varela Round Bold', 'Varela Round';
            font-weight: 700;
            font-style: normal;
            color: #FFFFFF;
        }

        #u46_text {
            font-size: 14px;
            font-weight: bold;
            border-width: 0px;
            position: absolute;
            left: 0px;
            top: 0px;
            width: 74px;
            white-space: nowrap;
        }

        #u47_div {
            border-width: 0px;
            position: absolute;
            left: 0px;
            top: 0px;
            width: 162px;
            height: 17px;
            background: inherit;
            background-color: rgba(255, 255, 255, 0);
            border: none;
            border-radius: 0px;
            -moz-box-shadow: none;
            -webkit-box-shadow: none;
            box-shadow: none;
            font-family: 'Varela Round Bold', 'Varela Round';
            font-weight: 700;
            font-style: normal;
            color: #FFFFFF;
        }

        #u47 {
            border-width: 0px;
            position: absolute;
            left: 480px;
            top: 128px;
            width: 162px;
            height: 17px;
            font-family: 'Varela Round Bold', 'Varela Round';
            font-weight: 700;
            font-style: normal;
            color: #FFFFFF;
        }

        #u47_text {
            font-size: 14px;
            font-weight: bold;
            border-width: 0px;
            position: absolute;
            left: 0px;
            top: 0px;
            width: 162px;
            white-space: nowrap;
        }

        #u48_div {
            border-width: 0px;
            position: absolute;
            left: 0px;
            top: 0px;
            width: 131px;
            height: 17px;
            background: inherit;
            background-color: rgba(255, 255, 255, 0);
            border: none;
            border-radius: 0px;
            -moz-box-shadow: none;
            -webkit-box-shadow: none;
            box-shadow: none;
            font-family: 'Varela Round Bold', 'Varela Round';
            font-weight: 700;
            font-style: normal;
            color: #FFFFFF;
        }

        #u48 {
            border-width: 0px;
            position: absolute;
            left: 670px;
            top: 128px;
            width: 131px;
            height: 17px;
            font-family: 'Varela Round Bold', 'Varela Round';
            font-weight: 700;
            font-style: normal;
            color: #FFFFFF;
        }

        #u48_text {
            font-size: 14px;
            font-weight: bold;
            border-width: 0px;
            position: absolute;
            left: 0px;
            top: 0px;
            width: 131px;
            white-space: nowrap;
        }

        #u49_div {
            border-width: 0px;
            position: absolute;
            left: 0px;
            top: 0px;
            width: 33px;
            height: 17px;
            background: inherit;
            background-color: rgba(255, 255, 255, 0);
            border: none;
            border-radius: 0px;
            -moz-box-shadow: none;
            -webkit-box-shadow: none;
            box-shadow: none;
            font-family: 'Varela Round Bold', 'Varela Round';
            font-weight: 700;
            font-style: normal;
            color: #FFFFFF;
        }

        #u49 {
            border-width: 0px;
            position: absolute;
            left: 38px;
            top: 157px;
            width: 33px;
            height: 17px;
            font-family: 'Varela Round Bold', 'Varela Round';
            font-weight: 700;
            font-style: normal;
            color: #FFFFFF;
        }

        #u49_text {
            font-size: 15px;
            font-weight: bold;
            border-width: 0px;
            position: absolute;
            left: 0px;
            top: 0px;
            width: 33px;
            white-space: nowrap;
        }

        #u50_div {
            border-width: 0px;
            position: absolute;
            left: 0px;
            top: 0px;
            width: 33px;
            height: 17px;
            background: inherit;
            background-color: rgba(255, 255, 255, 0);
            border: none;
            border-radius: 0px;
            -moz-box-shadow: none;
            -webkit-box-shadow: none;
            box-shadow: none;
            font-family: 'Varela Round Bold', 'Varela Round';
            font-weight: 700;
            font-style: normal;
            color: #FFFFFF;
        }

        #u50 {
            border-width: 0px;
            position: absolute;
            left: 150px;
            top: 157px;
            width: 33px;
            height: 17px;
            font-family: 'Varela Round Bold', 'Varela Round';
            font-weight: 700;
            font-style: normal;
            color: #FFFFFF;
        }

        #u50_text {
            font-size: 15px;
            font-weight: bold;
            border-width: 0px;
            position: absolute;
            left: 0px;
            top: 0px;
            width: 33px;
            white-space: nowrap;
        }

        #u51_div {
            border-width: 0px;
            position: absolute;
            left: 0px;
            top: 0px;
            width: 25px;
            height: 17px;
            background: inherit;
            background-color: rgba(255, 255, 255, 0);
            border: none;
            border-radius: 0px;
            -moz-box-shadow: none;
            -webkit-box-shadow: none;
            box-shadow: none;
            font-family: 'Varela Round Bold', 'Varela Round';
            font-weight: 700;
            font-style: normal;
            color: #FFFFFF;
        }

        #u51 {
            border-width: 0px;
            position: absolute;
            left: 275px;
            top: 157px;
            width: 25px;
            height: 17px;
            font-family: 'Varela Round Bold', 'Varela Round';
            font-weight: 700;
            font-style: normal;
            color: #FFFFFF;
        }

        #u51_text {
            font-size: 15px;
            font-weight: bold;
            border-width: 0px;
            position: absolute;
            left: 0px;
            top: 0px;
            width: 25px;
            white-space: nowrap;
        }

        #u52_div {
            border-width: 0px;
            position: absolute;
            left: 0px;
            top: 0px;
            width: 17px;
            height: 17px;
            background: inherit;
            background-color: rgba(255, 255, 255, 0);
            border: none;
            border-radius: 0px;
            -moz-box-shadow: none;
            -webkit-box-shadow: none;
            box-shadow: none;
            font-family: 'Varela Round Bold', 'Varela Round';
            font-weight: 700;
            font-style: normal;
            color: #FFFFFF;
        }

        #u52 {
            border-width: 0px;
            position: absolute;
            left: 405px;
            top: 157px;
            width: 17px;
            height: 17px;
            font-family: 'Varela Round Bold', 'Varela Round';
            font-weight: 700;
            font-style: normal;
            color: #FFFFFF;
        }

        #u52_text {
            font-size: 15px;
            font-weight: bold;
            border-width: 0px;
            position: absolute;
            left: 0px;
            top: 0px;
            width: 17px;
            white-space: nowrap;
        }

        #u53_div {
            border-width: 0px;
            position: absolute;
            left: 0px;
            top: 0px;
            width: 25px;
            height: 17px;
            background: inherit;
            background-color: rgba(255, 255, 255, 0);
            border: none;
            border-radius: 0px;
            -moz-box-shadow: none;
            -webkit-box-shadow: none;
            box-shadow: none;
            font-family: 'Varela Round Bold', 'Varela Round';
            font-weight: 700;
            font-style: normal;
            color: #FFFFFF;
        }

        #u53 {
            border-width: 0px;
            position: absolute;
            left: 550px;
            top: 157px;
            width: 25px;
            height: 17px;
            font-family: 'Varela Round Bold', 'Varela Round';
            font-weight: 700;
            font-style: normal;
            color: #FFFFFF;
        }

        #u53_text {
            font-size: 15px;
            font-weight: bold;
            border-width: 0px;
            position: absolute;
            left: 0px;
            top: 0px;
            width: 25px;
            white-space: nowrap;
        }

        #u54_div {
            border-width: 0px;
            position: absolute;
            left: 0px;
            top: 0px;
            width: 17px;
            height: 17px;
            background: inherit;
            background-color: rgba(255, 255, 255, 0);
            border: none;
            border-radius: 0px;
            -moz-box-shadow: none;
            -webkit-box-shadow: none;
            box-shadow: none;
            font-family: 'Varela Round Bold', 'Varela Round';
            font-weight: 700;
            font-style: normal;
            color: #FFFFFF;
        }

        #u54 {
            border-width: 0px;
            position: absolute;
            left: 735px;
            top: 157px;
            width: 17px;
            height: 17px;
            font-family: 'Varela Round Bold', 'Varela Round';
            font-weight: 700;
            font-style: normal;
            color: #FFFFFF;
        }

        #u54_text {
            font-size: 15px;
            font-weight: bold;
            border-width: 0px;
            position: absolute;
            left: 0px;
            top: 0px;
            width: 17px;
            white-space: nowrap;
        }

        .tab-content {
            background: #e6e6e6;
            padding: 1px;
            padding-bottom: 10px;
        }

        .nav-pills > li.active > a, .nav-pills > li.active > a:hover, .nav-pills > li.active > a:focus {
            border-top-color: #169BD5 !important;
        }

            .nav-pills > li.active > a, .nav-pills > li.active > a:focus, .nav-pills > li.active > a:hover {
                background-color: #169BD5 !important;
            }

        .nav-pills > li > a {
            background-color: #ccc !important;
            border-top-left-radius: 8px !important;
            border-top-right-radius: 8px !important;
        }

        .label-input-form {
            font-size: 12pt;
            width: 120px;
        }

        .label-input-form-lg {
            font-size: 12pt;
            width: 150px;
        }

        .input-form {
            padding: 5px;
            height: 30px;
            width: calc(100% - 130px);
            border-radius: 4px;
            border: 1px solid #999;
        }

        .btn-action {
            width: 120px;
            height: 35px;
            border-radius: 10px !important;
        }

        #txtSearchSoDon2 {
            width: 100%;
        }

        .datepick-form {
            width: calc(50% - 10px);
            border: 1px solid #999;
            border-radius: 4px;
            height: 30px;
        }

        .select-form {
            width: 100% !important;
            border: solid 1px #999 !important;
            border-radius: 4px !important;
            height: 40px !important;
        }

        .box-table {
            margin-top: 20px;
            width: 100%;
            padding: 5px;
        }

        .custom-table td {
            border: solid 1px #ccc;
            padding: 5px;
        }

        .custom-table th {
            border: solid 1px #ccc;
            text-align: center;
            padding: 5px;
        }

        .custom-table trh {
            height: 30px;
        }

        .custom-table {
            width: 100%;
        }

        .page-item {
            cursor: pointer;
        }


        .disabled {
            cursor: not-allowed;
        }

        .box-chitiet {
            margin-top: 40px;
        }

        .chitiet-header {
            width: 100%;
            border-bottom: 1px solid #0099ff
        }

        .chitiet-body {
            margin-top: 20px;
            padding-left: 15px;
        }

            .chitiet-body label {
                margin-bottom: 0;
            }

        .table-body {
            width: 100%;
        }

            .table-body tr {
                height: 25px;
            }

            .table-body td {
                padding: 0 5px;
            }

        .chitiet-trangthai {
            width: 100%;
            padding: 40px 30px 0px 30px;
            display: inline-flex;
            justify-content: center;
        }

        .chitiet-trangthai-label {
            width: 100%;
            padding: 5px 30px 5px 30px;
            display: inline-flex;
            justify-content: center;
            margin-bottom: 20px;
        }

            .chitiet-trangthai-label div {
                width: 155px;
                text-align: center;
                font-size: 11pt;
                font-weight: bold;
            }

        .bar-unprocess {
            background: url("/images/frontend/barunprocess.png") no-repeat;
            height: 5px;
            width: 120px;
            position: relative;
            top: 15px;
            left: -1px;
        }

        .icon-unprocess {
            background: url("/images/frontend/iconunprocess.png") no-repeat;
            height: 36px;
            width: 36px;
        }

        .bar-process {
            background: url("/images/frontend/barprocess.png") no-repeat;
            height: 5px;
            width: 120px;
            position: relative;
            top: 15px;
            left: -1px;
        }

        .icon-process {
            background: url("/images/frontend/iconprocess.png") no-repeat;
            height: 36px;
            width: 36px;
        }

        @media screen and (max-width: 1500px) and (min-width: 990px) {
            .bar-process {
                width: 100px;
            }

            .bar-unprocess {
                width: 100px;
            }
        }

        .no-result {
            width: 100%;
            /*margin: 10px;*/
            text-align: center;
            color: red;
            background-color: #e6e6e6;
        }
    </style>
     
    <div class="row">
        <div class="col-lg-9 col-md-9 col-xs-12 col-sm-12">
            <%--<div class="box box-solid" style="height:190px">
                <div class="box-header">
                    <h1 class="box-title"><b>Tình hình tiếp dân, xử lý đơn, giải quyết đơn khiếu nại tố cáo</b></h1>
                </div>
                <div class="box-body">
                    <div class="form-horizontal">
                        <div id="divThang" style="padding-left: 15px; padding-top: 10px; padding-bottom: 10px;">
                            <label style="margin-top: 6px; margin-right: 8px;" id="lblDuLieu">Dữ liệu: </label>
                            <select id="ddlPhamViDuLieu" style="width: 130px; padding-left: 10px; border: 1px solid #48b9dd; border-radius: 5px; margin-right: 8px;" onchange="ddlPhamViDuLieuOnChange();"></select>
                            <select id="ddlCoQuan" style="padding-left: 10px; margin-right: 8px;border: 1px solid #48b9dd; border-radius: 5px;" class="chosen" onchange="ddlOnChange();"></select>
                            <label style="margin-top: 6px; margin-right: 8px; padding-left: 6px;">Loại thời gian: </label>
                            <select id="ddlLoaiThoiGian" style="width: 130px; padding-left: 10px; border: 1px solid #48b9dd; border-radius: 5px; margin-right: 8px;" onchange="ShowThoiGianBaoCao();ddlOnChange();">
                                <option value="3">Năm</option>
                                <option value="2">Quý</option>
                                <option value="1">Tháng</option>
                            </select>
                            <label style="margin-top: 6px; margin-right: 8px;">Năm: </label>
                            <select id="ddlNam" style="width: 130px; padding-left: 10px; border: 1px solid #48b9dd; border-radius: 5px; margin-right: 8px;" onchange="ddlOnChange();"></select>
                            <label style="margin-top: 6px; margin-right: 8px;" class="ThoiGianThang">Tháng: </label>
                            <select class="ThoiGianThang" id="ddlThang" style="width: 130px; padding-left: 10px; border: 1px solid #48b9dd; border-radius: 5px; margin-right: 8px;" onchange="ddlOnChange();"></select>
                            <label style="margin-top: 6px; margin-right: 8px;" class="ThoiGianQuy">Qúy: </label>
                            <select class="ThoiGianQuy" id="ddlQuy" style="width: 130px; padding-left: 10px; border: 1px solid #48b9dd; border-radius: 5px; margin-right: 8px;" onchange="ddlOnChange();">
                                <option value="1">Quý 1</option>
                                <option value="2">Quý 2</option>
                                <option value="3">Quý 3</option>
                                <option value="4">Quý 4</option>
                            </select>
                        </div>     
                    </div>
                </div>
                <div class="box-body">
                    <div class="form-horizontal">
                        <!-- Unnamed (Rectangle) -->
                        <div id="u41" class="ax_default box_3">
                            <div id="u41_div" class=""></div>
                        </div>
                        <div id="u42" class="ax_default box_3">
                            <div id="u42_div" class=""></div>
                        </div>
                        <!-- Unnamed (Rectangle) -->
                        <div id="u43" class="ax_default label">
                            <div id="u43_div" class=""></div>
                            <div id="u43_text" class="text ">
                                <p><span>Lượt tiếp</span></p>
                            </div>
                        </div>
                        <div id="u44" class="ax_default label">
                            <div id="u44_div" class=""></div>
                            <div id="u44_text" class="text ">
                                <p><span>Xử lý đơn</span></p>
                            </div>
                        </div>
                        <div id="u45" class="ax_default label">
                            <div id="u45_div" class=""></div>
                            <div id="u45_text" class="text ">
                                <p><span>Đơn khiếu nại</span></p>
                            </div>
                        </div>
                        <div id="u46" class="ax_default label">
                            <div id="u46_div" class=""></div>
                            <div id="u46_text" class="text ">
                                <p><span>Đơn tố cáo</span></p>
                            </div>
                        </div>
                        <div id="u47" class="ax_default label">
                            <div id="u47_div" class=""></div>
                            <div id="u47_text" class="text ">
                                <p><span>Đơn kiến nghị, phản ánh</span></p>
                            </div>
                        </div>
                        <div id="u48" class="ax_default label">
                            <div id="u48_div" class=""></div>
                            <div id="u48_text" class="text ">
                                <p><span>Vụ việc đông người</span></p>
                            </div>
                        </div>
                        <!-- Unnamed (Rectangle) -->
                        <div id="u49" class="ax_default label">
                            <div id="u49_div" class=""></div>
                            <div id="u49_text" class="text ">
                                <p><span id="soluottiepdan">0</span></p>
                            </div>
                        </div>
                        <div id="u50" class="ax_default label">
                            <div id="u50_div" class=""></div>
                            <div id="u50_text" class="text ">
                                <p><span id="sodonxuly">0</span></p>
                            </div>
                        </div>
                        <div id="u51" class="ax_default label">
                            <div id="u51_div" class=""></div>
                            <div id="u51_text" class="text ">
                                <p><span id="sodonkn">0</span></p>
                            </div>
                        </div>
                        <div id="u52" class="ax_default label">
                            <div id="u52_div" class=""></div>
                            <div id="u52_text" class="text ">
                                <p><span id="sodongtc">0</span></p>
                            </div>
                        </div>
                        <div id="u53" class="ax_default label">
                            <div id="u53_div" class=""></div>
                            <div id="u53_text" class="text ">
                                <p><span id="sodonkiennghipa">0</span></p>
                            </div>
                        </div>
                        <div id="u54" class="ax_default label">
                            <div id="u54_div" class=""></div>
                            <div id="u54_text" class="text ">
                                <p><span id="sovuviecdongnguoi">0</span></p>
                            </div>
                        </div>

                        <br />
                        <br />
                    </div>
                </div>
            </div>--%>
        </div>
        <div class="col-lg-9 col-md-9 col-xs-12 col-sm-12">
            <%-- <div class="box box-solid">
                <div class="box-header">
                    <i class="fa fa-search"></i>
                    <h1 class="box-title">TRA CỨU TRẠNG THÁI ĐƠN THƯ</h1>
                </div>
                <div class="box-body">
                    <div class="form-horizontal">
                        <div class="form-group">
                            <div class="col-lg-8 col-md-8 col-xs-11 col-sm-11">
                                <div class="input-group" style="margin-bottom: 5px;">
                                    <input id="txtSearchTrangThai" type="text" name="message" placeholder="Nhập số đơn thư" class="form-control" />
                                    <span class="input-group-btn">
                                        <button type="button" onclick="searchTrangThai()" class="btn btn-primary btn-flat">Tra cứu</button>
                                    </span>
                                </div>
                                <div style="color: #a7a7a7; cursor: default;">
                                    * Nhập số đơn thư Ông (bà) muốn tra cứu. VD: BTD127, TTT124.
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div style="display:none">
                <div class="box-header">
                    <i class="fa fa-search"></i>
                    <h1 class="box-title">TRA CỨU VĂN BẢN TRẢ LỜI ĐƠN THƯ KHIẾU NẠI, TỐ CÁO</h1>

                </div>
                <div class="box-body">
                    <div class="form-horizontal">
                        <div class="form-group">
                            <div class="col-lg-8 col-md-8 col-xs-11 col-sm-11">
                                <div class="input-group" style="margin-bottom: 5px;">
                                    <input id="txtSearchVB" type="text" name="message" placeholder="Nhập số đơn thư" class="form-control" />
                                    <span class="input-group-btn">
                                        <button type="button" onclick="searchVB();" class="btn btn-warning btn-flat">Tra cứu</button>
                                    </span>
                                </div>
                                <div style="color: #a7a7a7; cursor: default;">
                                    * Nhập số đơn thư Ông (bà) muốn tra cứu. VD: BTD127, TTT124.
                                </div>

                            </div>
                        </div>
                    </div>
                </div>

                <div class="box-header">
                    <i class="fa fa-search"></i>
                    <h1 class="box-title">TRA CỨU KẾT QUẢ GIẢI QUYẾT ĐƠN THƯ</h1>
                </div>
                <div class="box-body">
                    <div class="form-horizontal">
                        <div class="form-group">
                            <div class="col-lg-8 col-md-8 col-xs-11 col-sm-11">
                                <div class="input-group" style="margin-bottom: 5px;">
                                    <input id="txtSearchQD" type="text" name="message" placeholder="Nhập số đơn thư" class="form-control" />
                                    <span class="input-group-btn">
                                        <button type="button" onclick="searchQD();" class="btn btn-default btn-flat">Tra cứu</button>
                                    </span>
                                </div>
                                <div style="color: #a7a7a7; cursor: default;">
                                    * Nhập số đơn thư Ông (bà) muốn tra cứu. VD: BTDCĐ183, BTDXM166
                                </div>

                            </div>
                        </div>
                    </div>
                </div>
                </div> 

                
            <div class="box-header">
                <i class="fa fa-search"></i>
                <h1 class="box-title">TRA CỨU KẾT QUẢ</h1>

            </div>
            <div class="box-body">
                <div class="form-horizontal">
                    <div class="form-group">
                        <div class="col-lg-8 col-md-8 col-xs-11 col-sm-11">
                            <div class="input-group" style="margin-bottom: 5px;">
                                <input id="txtSearchDonThu" type="text" name="message" placeholder="Nhập số đơn thư" class="form-control" />
                                <span class="input-group-btn">
                                    <button type="button" onclick="searchDonthu();" class="btn btn-default btn-flat">Tra cứu</button>
                                </span>
                            </div>
                            <div style="color: #a7a7a7; cursor: default;">
                                * Nhập số đơn thư Ông (bà) muốn tra cứu. VD: BTDCĐ183, BTDXM166
                            </div>

                        </div>
                    </div>
                </div>
            </div>

                <div class="box-header" style="text-align: center;">
                    <div style="font-size: 20px; color: #515151; margin-bottom: 15px;">CỔNG THÔNG TIN CÔNG BỐ KẾT QUẢ GIẢI QUYẾT KHIẾU NẠI TỐ CÁO</div>
                    <div style="font-weight: bold; line-height: 15px; color: #48b5ee; font-size: 30px; margin-bottom: 15px;">THANH TRA TỈNH BÀ RỊA - VŨNG TÀU</div>
                </div>
                <div class="box-body">
                    <div class="form-horizontal">
                        <div class="form-group">
                            <div class="col-lg-1 col-md-1 col-xs-1 col-sm-1"></div>
                            <div class="col-lg-10 col-md-10 col-xs-10 col-sm-10">
                                Hệ thống hỗ trợ tra cứu thông tin trạng thái, văn bản trả lời, 
                                các quyết định giải quyết hồ sơ đơn thư khiếu nại, tố cáo, phản ánh, kiến nghị của công dân.
                            </div>
                        </div>
                    </div>
                </div>


            </div>--%>

            <div class="box box-solid" style="height:190px">
                <div class="box-header">
                    <h1 class="box-title"><b>Tình hình tiếp dân, xử lý đơn, giải quyết đơn khiếu nại tố cáo</b></h1>
                </div>
                <div class="box-body">
                    <div class="form-horizontal">
                        <div>
                            <table>
                                <tr>
                                    <td><label style="margin-top: 6px; margin-right: 8px;" id="lblDuLieu">Dữ liệu: </label></td>
                                    <td><select id="ddlPhamViDuLieu" style="width: 130px; padding-left: 10px; border: 1px solid #48b9dd; border-radius: 5px; margin-right: 8px;" onchange="ddlPhamViDuLieuOnChange();"></select></td>
                                    <td><select id="ddlCoQuan" style="padding-left: 10px; margin-right: 8px;border: 1px solid #48b9dd; border-radius: 5px;" class="chosen" onchange="ddlOnChange();"></select></td>
                                    <td><label style="margin-top: 6px; margin-right: 8px; padding-left: 6px;">Loại thời gian: </label></td>
                                    <td>
                                        <select id="ddlLoaiThoiGian" style="width: 130px; padding-left: 10px; border: 1px solid #48b9dd; border-radius: 5px; margin-right: 8px;" onchange="ShowThoiGianBaoCao();ddlOnChange();">
                                            <option value="3">Năm</option>
                                            <option value="2">Quý</option>
                                            <option value="1">Tháng</option>
                                        </select>
                                    </td>
                                    <td><label style="margin-top: 6px; margin-right: 8px;">Năm: </label></td>
                                    <td><select id="ddlNam" style="width: 130px; padding-left: 10px; border: 1px solid #48b9dd; border-radius: 5px; margin-right: 8px;" onchange="ddlOnChange();"></select></td>
                                    <td><label style="margin-top: 6px; margin-right: 8px;" class="ThoiGianThang">Tháng: </label></td>
                                    <td><select class="ThoiGianThang" id="ddlThang" style="width: 130px; padding-left: 10px; border: 1px solid #48b9dd; border-radius: 5px; margin-right: 8px;" onchange="ddlOnChange();"></select></td>
                                    <td><label style="margin-top: 6px; margin-right: 8px;" class="ThoiGianQuy">Qúy: </label></td>
                                    <td>
                                        <select class="ThoiGianQuy" id="ddlQuy" style="width: 130px; padding-left: 10px; border: 1px solid #48b9dd; border-radius: 5px; margin-right: 8px;" onchange="ddlOnChange();">
                                            <option value="1">Quý 1</option>
                                            <option value="2">Quý 2</option>
                                            <option value="3">Quý 3</option>
                                            <option value="4">Quý 4</option>
                                        </select>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </div>
                <div class="box-body">
                    <div class="form-horizontal">
                        <!-- Unnamed (Rectangle) -->
                        <div id="u41" class="ax_default box_3">
                            <div id="u41_div" class=""></div>
                        </div>
                        <div id="u42" class="ax_default box_3">
                            <div id="u42_div" class=""></div>
                        </div>
                        <!-- Unnamed (Rectangle) -->
                        <div id="u43" class="ax_default label">
                            <div id="u43_div" class=""></div>
                            <div id="u43_text" class="text ">
                                <p><span>Lượt tiếp</span></p>
                            </div>
                        </div>
                        <div id="u44" class="ax_default label">
                            <div id="u44_div" class=""></div>
                            <div id="u44_text" class="text ">
                                <p><span>Xử lý đơn</span></p>
                            </div>
                        </div>
                        <div id="u45" class="ax_default label">
                            <div id="u45_div" class=""></div>
                            <div id="u45_text" class="text ">
                                <p><span>Đơn khiếu nại</span></p>
                            </div>
                        </div>
                        <div id="u46" class="ax_default label">
                            <div id="u46_div" class=""></div>
                            <div id="u46_text" class="text ">
                                <p><span>Đơn tố cáo</span></p>
                            </div>
                        </div>
                        <div id="u47" class="ax_default label">
                            <div id="u47_div" class=""></div>
                            <div id="u47_text" class="text ">
                                <p><span>Đơn kiến nghị, phản ánh</span></p>
                            </div>
                        </div>
                        <div id="u48" class="ax_default label">
                            <div id="u48_div" class=""></div>
                            <div id="u48_text" class="text ">
                                <p><span>Vụ việc đông người</span></p>
                            </div>
                        </div>
                        <!-- Unnamed (Rectangle) -->
                        <div id="u49" class="ax_default label">
                            <div id="u49_div" class=""></div>
                            <div id="u49_text" class="text ">
                                <p><span id="soluottiepdan">0</span></p>
                            </div>
                        </div>
                        <div id="u50" class="ax_default label">
                            <div id="u50_div" class=""></div>
                            <div id="u50_text" class="text ">
                                <p><span id="sodonxuly">0</span></p>
                            </div>
                        </div>
                        <div id="u51" class="ax_default label">
                            <div id="u51_div" class=""></div>
                            <div id="u51_text" class="text ">
                                <p><span id="sodonkn">0</span></p>
                            </div>
                        </div>
                        <div id="u52" class="ax_default label">
                            <div id="u52_div" class=""></div>
                            <div id="u52_text" class="text ">
                                <p><span id="sodongtc">0</span></p>
                            </div>
                        </div>
                        <div id="u53" class="ax_default label">
                            <div id="u53_div" class=""></div>
                            <div id="u53_text" class="text ">
                                <p><span id="sodonkiennghipa">0</span></p>
                            </div>
                        </div>
                        <div id="u54" class="ax_default label">
                            <div id="u54_div" class=""></div>
                            <div id="u54_text" class="text ">
                                <p><span id="sovuviecdongnguoi">0</span></p>
                            </div>
                        </div>

                        <br />
                        <br />
                    </div>
                </div>
            </div>

            <div class="box box-solid">
                <div class="box-header">
                    <%--<i class="fa fa-search"></i>--%>
                    <h1 class="box-title">Tra cứu đơn thư</h1>
                </div>
                <ul class="nav nav-pills">
                    <li class="nav-item active">
                        <a class="nav-link active" data-toggle="tab" href="#tracuudonthu">Tra cứu theo số đơn thư</a>
                    </li>
                    <li class="nav-item">
                        <a class="nav-link" data-toggle="tab" href="#tracuucoquan">Tra cứu theo cơ quan tiếp nhận</a>
                    </li>
                </ul>
                <div class="tab-content">
                    <div class="tab-pane container active" id="tracuudonthu">
                        <div class="col-lg-12" style="margin-top: 20px">
                            <div class="form-horizontal">
                                <div class="form-group">
                                    <div class="col-lg-8 col-md-8 col-xs-11 col-sm-11">
                                        <label for="txtSearchSoDon" class="label-input-form">
                                            Số đơn thư
                                        </label>
                                        <input id="txtSearchSoDon" type="text" name="message" placeholder="Nhập số đơn thư muốn tra cứu. VD: BTD127, TTT124" class="input-form" onfocus="changeSearch(1)" />
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="col-lg-8 col-md-8 col-xs-11 col-sm-11">
                                        <label for="txtSearchCMND" class="label-input-form">
                                            Số CMND
                                        </label>
                                        <input id="txtSearchCMND" autocomplete="off" type="text" name="message" placeholder="Nhập số CMND của chủ đơn thư" class="input-form" onfocus="changeSearch(2)" />
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="col-lg-8 col-md-8 col-xs-11 col-sm-11">
                                        <label class="label-input-form"></label>
                                        <button class="btn btn-primary btn-action" onclick="TraCuuDonThu(1);return false;"><i class="fa fa-search"></i>&ensp; Tra cứu</button>
                                        <button class="btn btn-warning btn-action" onclick="QRcode();return false;"><i class="fa fa-qrcode"></i>&ensp; QR code</button>
                                        <button class="btn btn-info btn-action" onclick="huongdan();return false;"><i class="fa fa-question"></i>&ensp; Hướng dẫn</button>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="tab-pane container fade" id="tracuucoquan">
                        <div class="col-lg-12" style="margin-top: 20px">
                            <div class="form-horizontal">
                                <div class="form-group">
                                    <div class="col-lg-8 col-md-8 col-xs-11 col-sm-11">
                                        <label class="col-lg-3 col-md-3 col-sm-2 col-xs-2 control-label" style="font-size: 12pt">Cơ quan</label>
                                        <div class="col-lg-9 col-md-9 col-sm-12 col-xs-12">
                                            <asp:DropDownList ID="ddlCoQuanTiep" runat="server" DataTextField="TenCoQuan" CssClass="chosen form-control select2 select-form" DataValueField="CoQuanID" Style="width: 100%"></asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="col-lg-8 col-md-8 col-xs-11 col-sm-11">
                                        <label class="col-lg-3 col-md-3 col-sm-2 col-xs-2 control-label" style="font-size: 12pt">
                                            Số đơn thư
                                        </label>
                                        <div class="col-lg-9 col-md-9 col-sm-12 col-xs-12">
                                            <input id="txtSearchSoDon2" type="text" name="message" placeholder="Nhập số đơn thư muốn tra cứu. VD: BTD127, TTT124" class="input-form" />
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="col-lg-8 col-md-8 col-xs-11 col-sm-11">
                                        <label class="col-lg-3 col-md-3 col-sm-2 col-xs-2 control-label" style="font-size: 12pt">
                                            Thời gian tiếp nhận
                                        </label>
                                        <div class="col-lg-9 col-md-9 col-sm-12 col-xs-12">
                                            <asp:TextBox ID="txtTuNgayFilter" class='date datepicker3' runat="server" CssClass="datepicker datepick-form" placeholder="Từ ngày"></asp:TextBox>
                                            &emsp;
                                        <asp:TextBox ID="txtDenNgayFilter" class='date datepicker3' runat="server" CssClass="datepicker datepick-form" placeholder="Đến ngày"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="col-lg-8 col-md-8 col-xs-11 col-sm-11">
                                        <label class="col-lg-3 col-md-3 col-sm-2 col-xs-2 control-label" style="font-size: 12pt"></label>
                                        <div class="col-lg-9 col-md-9 col-sm-12 col-xs-12">
                                            <button class="btn btn-primary btn-action" onclick="TraCuuCoQuan(1);return false;"><i class="fa fa-search"></i>&ensp; Tra cứu</button>
                                            <button class="btn btn-info btn-action" onclick="huongdan();return false;"><i class="fa fa-question"></i>&ensp; Hướng dẫn</button>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="loadding_qrcode" style="display: none"></div>
                <div class="no-result">
                    <label id="text-result"></label>
                </div>
                <div class="box-table" style="display: none" id="box-table">
                    <table class="custom-table" id="ketquatracuu">
                        <thead>
                            <tr>
                                <th style="width: 5%">STT</th>
                                <th style="width: 10%">Số đơn</th>
                                <th style="width: 10%">Ngày tiếp nhận</th>
                                <th style="width: 15%">Cơ quan tiếp nhận</th>
                                <th style="width: 15%">Tên chủ đơn</th>
                                <th style="width: 25%">Nội dung vụ việc</th>
                                <th style="width: 10%">Ngày ban hành quyết định</th>
                            </tr>
                        </thead>
                        <tbody id="ketquatracuubody"></tbody>
                    </table>
                    <div style="width: 100%; text-align: right; display: none" id="paging-container">
                        <ul class="pagination" id="divpaging">
                        </ul>
                    </div>
                    <table style="display: none" id="template_table">
                        <tr style="table-layout: fixed; display: table; width: 100%;">
                            <td style="text-align: center">_STT_</td>
                            <td style="text-align: center; cursor: pointer; color: #0099ff" onclick="showChiTiet(_DONTHUID_)">_SODON_</td>
                            <td style="text-align: center">_NGAYTIEPNHAN_</td>
                            <td>_COQUAN_</td>
                            <td>_TENCHUDON_</td>
                            <td>_NOIDUNG_</td>
                            <td style="text-align: center">_NGAYBANHANH_</td>
                        </tr>
                    </table>
                </div>
                <div class="box-chitiet" id="box-chitiet" style="display: none">
                    <table class="chitiet-header" style="border-top: 4px solid #0099ff">
                        <tr style="height: 50px">
                            <td style="width: 50%; padding: 0 30px; font-size: 16pt">Trạng thái đơn thư</td>
                            <td style="width: 50%; text-align: right; padding: 0 30px; font-size: 16pt"><a style="cursor: pointer; color: #999; font-weight: bold" onclick="hideChiTiet()">X</a></td>
                        </tr>
                    </table>
                    <div class="chitiet-trangthai">
                        <div id="icon1"></div>
                        <div id="bar2"></div>
                        <div id="icon2"></div>
                        <div id="bar3"></div>
                        <div id="icon3"></div>
                        <div id="bar4"></div>
                        <div id="icon4"></div>
                        <div id="bar5"></div>
                        <div id="icon5"></div>
                    </div>
                    <div class="chitiet-trangthai-label">
                        <div>Chưa xử lý</div>
                        <div>Đã xử lý</div>
                        <div>Chưa giải quyết</div>
                        <div>Đang giải quyết</div>
                        <div>Đã giải quyết</div>
                    </div>
                    <table class="chitiet-header">
                        <tr style="height: 50px">
                            <td style="padding: 0 30px; font-size: 16pt">Thông tin chi tiết đơn thư</td>
                        </tr>
                    </table>
                    <div class="chitiet-body">
                        <table class="table-body" id="thongtinchitiet">
                            <tr>
                                <td style="width: 130px; color: #0099ff; text-align: right">Số đơn thư: </td>
                                <td>
                                    <label id="lblSoDonThu"></label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 130px; color: #0099ff; text-align: right">Ngày tiếp nhận: </td>
                                <td>
                                    <label id="lblNgayTiepNhan"></label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 130px; color: #0099ff; text-align: right">Cơ quan tiếp nhận: </td>
                                <td>
                                    <label id="lblCoQuanTiepNhan"></label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 130px; color: #0099ff; text-align: right">Họ và tên công dân: </td>
                                <td>
                                    <label id="lblHoVaTen"></label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 130px; color: #0099ff; text-align: right">Địa chỉ: </td>
                                <td>
                                    <label id="lblDiaChi"></label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 130px; color: #0099ff; text-align: right">Nội dung: </td>
                                <td>
                                    <label id="lblNoiDung"></label>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <table class="chitiet-header">
                        <tr style="height: 50px">
                            <td style="padding: 0 30px; font-size: 16pt">Thông tin kết quả xử lý</td>
                        </tr>
                    </table>
                    <div class="chitiet-body">
                        <table class="table-body" id="ketquaxuly">
                            <tr>
                                <td style="width: 130px; color: #0099ff; text-align: right">Cơ quan xử lý:
                               
                                </td>
                                <td>
                                    <label id="lblCoQuanXuLy"></label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 130px; color: #0099ff; text-align: right">Hướng xử lý:
                                
                                </td>
                                <td>
                                    <label id="lblHuongXuLy"></label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 130px; color: #0099ff; text-align: right">Tệp đính kèm kết quả xử lý:
                                
                                </td>
                                <td>
                                    <label id="lblTepKQXuLy"></label>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <table class="chitiet-header">
                        <tr style="height: 50px">
                            <td style="padding: 0 30px; font-size: 16pt">Thông tin kết quả giải quyết</td>
                        </tr>
                    </table>
                    <div class="chitiet-body">
                        <table class="table-body" id="ketquagiaiquyet">
                            <tr>
                                <td style="width: 130px; color: #0099ff; text-align: right">Cơ quan giải quyết:
                                </td>
                                <td>
                                    <label id="lblCoQuanGiaiQuyet"></label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 130px; color: #0099ff; text-align: right">Tệp tin đính kèm kết quả giải quyết:
                                </td>
                                <td>
                                    <label id="lblTepKQGiaiQuyet"></label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 130px; color: #0099ff; text-align: right">Ngày ban hành:
                                </td>
                                <td>
                                    <label id="lblNgayBanHanh"></label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 130px; color: #0099ff; text-align: right">Cơ quan ban hành:
                                </td>
                                <td>
                                    <label id="lblCoQuanBanHanh"></label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 130px; color: #0099ff; text-align: right">Số tiền phải thu:
                                </td>
                                <td>
                                    <label id="lblSoTienPhaiThu"></label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 130px; color: #0099ff; text-align: right">Số đất phải thu:
                                </td>
                                <td>
                                    <label id="lblSoDatPhaiThu"></label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 130px; color: #0099ff; text-align: right">Số đối tượng bị xử lý:
                                </td>
                                <td>
                                    <label id="lblDoiTuongXuLy"></label>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </div>
        </div>

        <div class="col-lg-3 col-md-3  col-xs-12 col-sm-12 side-bar" style="padding-left: 0px;">
            <uc1:SideBarTinNoiBat runat="server" ID="SideBarTinNoiBat" />
            <div class="clearfix"></div>
        </div>
    </div>
    <asp:HiddenField ID="hdftypeSearch" Value="1" runat="server" />
    <script type="text/javascript">
        $(document).ready(function () {
            $("#dulieuthongke").hide();
            InitBaoCao();
            //console.log("InitBaoCao");
        });

        $(".datepicker").inputmask("99/99/9999");

        function searchVB() {
            window.location.href = "/Webapp/Frontend/TraCuuVBTraLoi.aspx?keyword=" + $("#txtSearchVB").val();
        }
        function searchQD() {
            window.location.href = "/Webapp/Frontend/TraCuuQDGiaiQuyet.aspx?keyword=" + $("#txtSearchQD").val();
        }
        function searchTrangThai() {

            window.location.href = "/Webapp/Frontend/TraCuuTrangThaiHoSo.aspx?keyword=" + $("#txtSearchTrangThai").val();
        }
        function searchDonthu() {
            window.location.href = "/Webapp/Frontend/TraCuuDonThu.aspx?keyword=" + $("#txtSearchDonThu").val();
        }

        function InitBaoCao() {
            ShowThoiGianBaoCao();
            var yearNow = new Date().getFullYear();
            var monthNow = parseInt(new Date().getMonth()) + 1;
            for (var namX = yearNow; namX > 2017; namX--) {
                var html = "<option value='" + namX + "'>" + namX + "</option>";
                $("#ddlNam").append(html);
            }
            if (monthNow < 10)
                monthNow = '0' + monthNow;
            $("#ddlThang").val(monthNow);
            $("#ddlNam").val(yearNow);

            var optionToanTinh = $('<option value = "2">Toàn tỉnh</option>');
            var optionSo = $('<option value = "3">Cấp sở</option>');
            var optionHuyen = $('<option value = "4">Cấp huyện</option>');
            var optionXa = $('<option value = "5">Cấp xã</option>');
            $("#ddlPhamViDuLieu").append(optionToanTinh);
            $("#ddlPhamViDuLieu").append(optionSo);
            $("#ddlPhamViDuLieu").append(optionHuyen);
            $("#ddlPhamViDuLieu").append(optionXa);

            $("#ddlCoQuan").hide();
            $("#ddlCoQuan_chosen").hide();

            ddlOnChange();
        }
        function ShowThoiGianBaoCao() {
            loaiThoiGian = $("#ddlLoaiThoiGian").val();
            $('#ddlThang').empty();
            if (loaiThoiGian == 3) {
                var newOption = $('<option value = "00">Chọn tháng</option>');
                var newOption1 = $('<option value = "17">6 tháng đầu năm</option>');
                var newOption2 = $('<option value = "18">6 tháng cuối năm</option>');
                $('#ddlThang').append(newOption);
                $('#ddlThang').append(newOption1);
                $('#ddlThang').append(newOption2);
                $(".ThoiGianThang").show();
                $(".ThoiGianQuy").hide();
                $("#ddlThang").addClass("active");     
                $("#ddlQuy").removeClass("active");
                $("#ddlNam").addClass("active");
            }
            else if (loaiThoiGian == 2) {
                $(".ThoiGianThang").hide();
                $(".ThoiGianQuy").show();
                $("#ddlThang").removeClass("active");
                $("#ddlQuy").addClass("active");
                $("#ddlNam").addClass("active");
            }
            else if (loaiThoiGian == 1) {
                for (var i = 1; i <= 12; i++) {
                    if (i < 10) {
                        var newOption1 = $('<option value=0' + i + '>' + "Tháng 0" + i + '</option>');
                        $('#ddlThang').append(newOption1);
                    }
                    else {
                        var newOption1 = $('<option value=' + i + '>' + "Tháng " + i + '</option>');
                        $('#ddlThang').append(newOption1);
                    }


                }
                $(".ThoiGianThang").show();
                $(".ThoiGianQuy").hide();
                $("#ddlThang").addClass("active");
                $("#ddlQuy").removeClass("active");
                $("#ddlNam").addClass("active");
            }
        }
        function ddlOnChange() {
            var namBC = $("#ddlNam").val();
            var thangBC = $("#ddlThang").val();
            if (thangBC == null) {
                thangBC = '00';
            }
            var quyBC = $("#ddlQuy").val();

            var tuNgay = '01-01-' + namBC;
            var denNgay = '31-12-' + namBC;
            if ($("#ddlThang").hasClass("active")) {
                var dsThang30 = ',04,06,09,11';
                if (thangBC == '00') {
                    var tuNgay = '01-01-' + namBC;
                    var denNgay = '31-12-' + namBC;
                }
                else if (thangBC == '17') {
                    var tuNgay = '01-01-' + namBC;
                    var denNgay = '30-06-' + namBC;
                }
                else if (thangBC == '18') {
                    var tuNgay = '01-07-' + namBC;
                    var denNgay = '31-12-' + namBC;
                }
                else if (thangBC == '02') {
                    if (namBC % 4 == 0) {
                        tuNgay = '01-02-' + namBC;
                        denNgay = '29-02-' + namBC
                    }
                    else {
                        tuNgay = '01-02-' + namBC;
                        denNgay = '28-02-' + namBC
                    }
                }
                else if (dsThang30.includes(thangBC)) {
                    tuNgay = '01-' + thangBC + '-' + namBC;
                    denNgay = '30-' + thangBC + '-' + namBC;
                }
                else {
                    tuNgay = '01-' + thangBC + '-' + namBC;
                    denNgay = '31-' + thangBC + '-' + namBC;
                }
            }

            if ($("#ddlQuy").hasClass("active")) {
                if (quyBC == 1) {
                    tuNgay = '01-01-' + namBC;
                    denNgay = '31-03-' + namBC;
                }
                else if (quyBC == 2) {
                    tuNgay = '01-04-' + namBC;
                    denNgay = '30-06-' + namBC;
                }
                else if (quyBC == 3) {
                    tuNgay = '01-07-' + namBC;
                    denNgay = '30-09-' + namBC;
                }
                if (quyBC == 4) {
                    tuNgay = '01-10-' + namBC;
                    denNgay = '31-12-' + namBC;
                }
            }

            var pCapIDSelect = $("#ddlPhamViDuLieu").val();
            var pCoQuanIDSelect = $("#ddlCoQuan").val();
            var url = '<%= System.Configuration.ConfigurationManager.AppSettings["APIUrl_GetSoLieuTongHop"].ToString() %>';
            $.ajax({
                url: url + tuNgay + "/" + denNgay + "/" + pCapIDSelect + "/" + pCoQuanIDSelect,
                type: "GET",
                success: function (data) {
                    $("#soluottiepdan").text(data.TongTiepDan);
                    $("#sodonxuly").text(data.TongXuLyDon);
                    $("#sodongtc").text(data.TongDonThuBHGQ_TC);
                    $("#sodonkn").text(data.TongDonThuBHGQ_KN);
                    $("#sodonkiennghipa").text(data.TongDonThuBHGQ_KNPA);
                    $("#sovuviecdongnguoi").text(data.TongTiepDanNhomKN);
                },
                error: function (data) {
                    $(".loadding_qrcode").hide();
                    $("#divMsgError").html("Không có dữ liệu tìm kiếm!");
                    $(".div-content").hide();
                }
            });
        }

        function QRcode() {

        };

        function huongdan() {

        };

        function TraCuuDonThu(page) {
            var typeSearch = $("#MainContent_hdftypeSearch").val();
            var textSoDon = $("#txtSearchSoDon").val();
            var textCMND = $("#txtSearchCMND").val();
            if (textSoDon == "" && textCMND == "") {
                showTextResult('Chưa nhập dữ liệu tìm kiếm!');
                return;
            }
            if (typeSearch == 1) {
                if (textSoDon == "") {
                    showTextResult('Chưa nhập dữ liệu tìm kiếm!');
                    return;
                }
            }
            else {
                if (textCMND == "") {
                    showTextResult('Chưa nhập dữ liệu tìm kiếm!');
                    return;
                }
            }
            $("#box-table").hide();
            $(".loadding_qrcode").show();
            if (typeSearch == 1) {
                var url = '<%= System.Configuration.ConfigurationManager.AppSettings["APIUrl_TraCuuTrangThai"].ToString() %>';
                $.ajax({
                    url: `${url}${textSoDon}/0`,
                    type: "GET",
                    success: function (data) {
                        $(".loadding_qrcode").hide();
                        if (data) {
                            //Kết quả
                            $("#ketquatracuu tbody").html('');
                            var templateTr = "<tr>" + $("#template_table tr:first-child").html() + "</tr>";
                            templateTr = templateTr.replace(/_STT_/g, 1);
                            templateTr = templateTr.replace(/_SODON_/g, data.SoDonThu ? data.SoDonThu : "");
                            templateTr = templateTr.replace(/_DONTHUID_/g, data.DonThuID);
                            templateTr = templateTr.replace(/_NGAYTIEPNHAN_/g, data.NgayNhapDonStr ? data.NgayNhapDonStr : "");
                            templateTr = templateTr.replace(/_COQUAN_/g, data.TenCoQuanTiepNhan ? data.TenCoQuanTiepNhan : "");
                            templateTr = templateTr.replace(/_TENCHUDON_/g, data.lsDoiTuongKN ? getTenChuDon(data.lsDoiTuongKN) : "");
                            templateTr = templateTr.replace(/_NOIDUNG_/g, data.NoiDungDon ? data.NoiDungDon : "");
                            templateTr = templateTr.replace(/_NGAYBANHANH_/g, data.NgayBanHanh ? data.NgayBanHanh : "");
                            $("#ketquatracuu tbody").append(templateTr);
                            hideChiTiet();
                            //Chi tiết
                            clearChiTiet();
                            fillDataChiTiet(data);
                        }
                        else {
                            $("#ketquatracuu tbody").html('');
                            $("#box-table").hide();
                            $(".loadding_qrcode").hide();
                            showTextResult('Không có dữ liệu tra cứu!');
                        }
                    },
                    error: function (data) {
                        $("#ketquatracuu tbody").html('');
                        $("#box-table").hide();
                        $(".loadding_qrcode").hide();
                        showTextResult('Không có dữ liệu tra cứu!');
                    }
                });
            }
            else {
                var url = '<%= System.Configuration.ConfigurationManager.AppSettings["APIUrl_TraCuuTrangThaiByCMND"].ToString() %>';
                var pagesize = 10;
                var start = (page - 1) * pagesize + 1;
                var end = (page * pagesize) + 1;
                $.ajax({
                    url: `${url}${textCMND}/0/${start}/${end}`,
                    type: "GET",
                    success: function (data) {
                        if (data.length > 0) {
                            var stt = (page - 1) * pagesize;
                            dataTraCuu = data;
                            $(".loadding_qrcode").hide();
                            $("#ketquatracuu tbody").html('');
                            var stt = 0;
                            for (var i = 0; i < data.length; i++) {
                                stt++;
                                var templateTr = "<tr>" + $("#template_table tr:first-child").html() + "</tr>";
                                templateTr = templateTr.replace(/_STT_/g, stt);
                                templateTr = templateTr.replace(/_SODON_/g, data[i].SoDonThu ? data[i].SoDonThu : "");
                                templateTr = templateTr.replace(/_DONTHUID_/g, data[i].DonThuID);
                                templateTr = templateTr.replace(/_NGAYTIEPNHAN_/g, data[i].NgayNhapDonStr ? data[i].NgayNhapDonStr : "");
                                templateTr = templateTr.replace(/_COQUAN_/g, data[i].TenCoQuanTiepNhan ? data[i].TenCoQuanTiepNhan : "");
                                templateTr = templateTr.replace(/_TENCHUDON_/g, data[i].lsDoiTuongKN ? getTenChuDon(data[i].lsDoiTuongKN) : "");
                                templateTr = templateTr.replace(/_NOIDUNG_/g, data[i].NoiDungDon ? data[i].NoiDungDon : "");
                                templateTr = templateTr.replace(/_NGAYBANHANH_/g, data[i].NgayBanHanh ? data[i].NgayBanHanh : "");
                                $("#ketquatracuu tbody").append(templateTr);
                            }
                            hideChiTiet();
                            BindPaging(pagesize, page, data[0].TongDonThu);
                        }
                        else {
                            $("#ketquatracuu tbody").html('');
                            $("#box-table").hide();
                            $(".loadding_qrcode").hide();
                            showTextResult('Không có dữ liệu tra cứu!');
                        }
                    },
                    error: function (data) {
                        $("#ketquatracuu tbody").html('');
                        $("#box-table").hide();
                        $(".loadding_qrcode").hide();
                        showTextResult('Không có dữ liệu tra cứu!');
                    }
                });
            }
        }

        function TraCuuCoQuan(page) {
            $("#MainContent_hdftypeSearch").val(3)
            var today = new Date();
            var firstDateOfYear = new Date(today.getFullYear(), 0, 1);
            var CoQuanID = $("#MainContent_ddlCoQuanTiep").val();
            CoQuanID = CoQuanID == "Chọn cơ quan" ? 0 : CoQuanID;
            var SoDonThu = $("#txtSearchSoDon2").val();
            SoDonThu = SoDonThu == "" ? 0 : SoDonThu;
            var TuNgay = $("#MainContent_txtTuNgayFilter").val();
            var DenNgay = $("#MainContent_txtDenNgayFilter").val();
            if (TuNgay == "" || DenNgay == "") {
                showTextResult("Chưa chọn thời gian tra cứu");
                return;
            }
            else {
                TuNgay = moment(TuNgay, "DD/MM/YYYY").format("DD-MM-YYYY");
                DenNgay = moment(DenNgay, "DD/MM/YYYY").format("DD-MM-YYYY");
            }

            var url = '<%= System.Configuration.ConfigurationManager.AppSettings["APIUrl_TraCuuTrangThaiByCoQuan"].ToString() %>';
            var pagesize = 10;
            var start = (page - 1) * pagesize + 1;
            var end = (page * pagesize) + 1;

            $("#box-table").hide();
            $(".loadding_qrcode").show();
            $.ajax({
                url: `${url}${CoQuanID}/${SoDonThu}/${TuNgay}/${DenNgay}/${start}/${end}`,
                type: "GET",
                success: function (data) {
                    if (data.length > 0) {
                        var stt = (page - 1) * pagesize;
                        dataTraCuu = data;
                        $(".loadding_qrcode").hide();
                        $("#ketquatracuu tbody").html('');
                        for (var i = 0; i < data.length; i++) {
                            stt++;
                            var templateTr = "<tr>" + $("#template_table tr:first-child").html() + "</tr>";
                            templateTr = templateTr.replace(/_STT_/g, stt);
                            templateTr = templateTr.replace(/_SODON_/g, data[i].SoDonThu ? data[i].SoDonThu : "");
                            templateTr = templateTr.replace(/_DONTHUID_/g, data[i].DonThuID);
                            templateTr = templateTr.replace(/_NGAYTIEPNHAN_/g, data[i].NgayNhapDonStr ? data[i].NgayNhapDonStr : "");
                            templateTr = templateTr.replace(/_COQUAN_/g, data[i].TenCoQuanTiepNhan ? data[i].TenCoQuanTiepNhan : "");
                            templateTr = templateTr.replace(/_TENCHUDON_/g, data[i].lsDoiTuongKN ? getTenChuDon(data[i].lsDoiTuongKN) : "");
                            templateTr = templateTr.replace(/_NOIDUNG_/g, data[i].NoiDungDon ? data[i].NoiDungDon : "");
                            templateTr = templateTr.replace(/_NGAYBANHANH_/g, data[i].NgayBanHanh ? data[i].NgayBanHanh : "");
                            $("#ketquatracuu tbody").append(templateTr);
                        }
                        hideChiTiet();
                        BindPaging(pagesize, page, data[0].TongDonThu);
                    }
                    else {
                        $("#ketquatracuu tbody").html('');
                        $("#box-table").hide();
                        $(".loadding_qrcode").hide();
                        showTextResult('Không có dữ liệu tra cứu!');
                    }
                },
                error: function (data) {
                    $("#ketquatracuu tbody").html('');
                    $("#box-table").hide();
                    $(".loadding_qrcode").hide();
                    showTextResult('Không có dữ liệu tra cứu!');
                }
            });
        }

        function clearChiTiet() {
            $("#lblSoDonThu").text("");
            $("#lblNgayTiepNhan").text("");
            $("#lblCoQuanTiepNhan").text("");
            $("#lblHoVaTen").text("");
            $("#lblDiaChi").text("");
            $("#lblNoiDung").text("");

            $("#lblCoQuanXuLy").text("");
            $("#lblHuongXuLy").text("");
            $("#lblTepKQXuLy").text("");

            $("#lblCoQuanGiaiQuyet").text("");
            $("#lblTepKQGiaiQuyet").text();
            $("#lblNgayBanHanh").text("");
            $("#lblCoQuanBanHanh").text("");
            $("#lblSoTienPhaiThu").text("");
            $("#lblSoDatPhaiThu").text("");
            $("#lblDoiTuongXuLy").text("");

            setStatus(-1);
        }

        function getTenChuDon(list) {
            var tenChuDon = "";
            for (var i = 0; i < list.length; i++) {
                if (i == 0) {
                    tenChuDon += list[i].HoTen;
                }
                else {
                    tenChuDon += `, ${list[i].HoTen}`;
                }
            };
            return tenChuDon;
        }

        function showTextResult(mess) {
            $("#text-result").show();
            $("#text-result").text(mess);
            setTimeout(function () {
                $("#text-result").hide();
                $("#text-result").text('');
            }, 4000);
        }

        function BindPaging(pagesize, currentpage, countsearch) {
            var Pageall = 1;
            if (countsearch > pagesize) {
                if (countsearch % pagesize == 0) {
                    Pageall = countsearch / pagesize;
                }
                else {
                    Pageall = (countsearch - (countsearch % pagesize)) / pagesize + 1;
                }
            }
            if (Pageall > 1) {
                $("#paging-container").show();
            }
            else {
                return;
            }
            var bindString = "";
            if (currentpage != 1) {
                bindString += `<li class="page-item"><a class="page-link" onClick="MovingPageDS(1)">Trang đầu</a></li>`;
            }
            if (Pageall < 10) {
                for (var i = 0; i < Pageall; i++) {
                    if (currentpage == i + 1) {
                        bindString += `<li class="page-item active"><a class="page-link"">${currentpage}</a></li>`
                    }
                    else {
                        bindString += `<li class="page-item active"><a class="page-link" onClick="MovingPageDS(${i + 1})">${i + 1}</a></li>`
                    }
                }
            }
            else {
                if (Pageall - currentpage > 5) {
                    if (currentpage < 6) {
                        for (var i = 0; i < 10; i++) {
                            if (currentpage == i + 1) {
                                bindString += `<li class="page-item active"><a class="page-link">${i + 1}</a></li>`
                            }
                            else {
                                bindString += `<li class="page-item"><a class="page-link" onClick="MovingPageDS(${i + 1})">${i + 1}</a></li>`
                            }
                        }
                    }
                    else {
                        for (var i = currentpage - 5; i < currentpage + 5; i++) {
                            if (currentpage == i + 1) {
                                bindString += `<li class="page-item active"><a class="page-link">${i + 1}</a></li>`
                            }
                            else {
                                bindString += `<li class="page-item"><a class="page-link" onClick="MovingPageDS(${i + 1})">${i + 1}</a></li>`
                            }
                        }
                    }
                }
                else {
                    for (var i = Pageall - 10; i < Pageall; i++) {
                        if (currentpage == i + 1) {
                            bindString += `<li class="page-item active"><a class="page-link">${i + 1}</a></li>`
                        }
                        else {
                            bindString += `<li class="page-item"><a class="page-link" onClick="MovingPageDS(${i + 1})">${i + 1}</a></li>`
                        }
                    }
                }
            }

            if (currentpage != Pageall) {
                bindString += `<li class="page-item"><a class="page-link" onClick="MovingPageDS(${Pageall})">Trang cuối</a></li>`;
            }
            $("#divpaging").html("");
            $("#divpaging").html(bindString);
        }

        function MovingPageDS(page) {
            if (page < 1) {
                return;
            }
            var typeSearch = $("#MainContent_hdftypeSearch").val();
            if (typeSearch != 3) {
                TraCuuDonThu(page);
            }
            else {
                TraCuuCoQuan(page);
            }
        }

        function hideChiTiet() {
            $("#box-chitiet").hide();
            $("#box-table").show();
        }

        function showChiTiet(DonThuID) {
            var typeSearch = $("#MainContent_hdftypeSearch").val();
            if (typeSearch == 1) {
                $("#box-chitiet").show();
                $("#box-table").hide();
            }
            else {
                //console.log(dataTraCuu, 'show');
                var dataChiTiet = dataTraCuu.filter(item => item.DonThuID == DonThuID)[0];
                clearChiTiet();
                fillDataChiTiet(dataChiTiet);
                $("#box-chitiet").show();
                $("#box-table").hide();
            }
        }

        function setStatus(status) {
            //remove class
            $("#icon1").removeClass();
            $("#bar2").removeClass();
            $("#icon2").removeClass();
            $("#bar3").removeClass();
            $("#icon3").removeClass();
            $("#bar4").removeClass();
            $("#icon4").removeClass();
            $("#bar5").removeClass();
            $("#icon5").removeClass();
            switch (status) {
                case 1:
                    //add class
                    $("#icon1").addClass('icon-process')
                    $("#bar2").addClass('bar-process');
                    $("#icon2").addClass('icon-unprocess');
                    $("#bar3").addClass('bar-unprocess');
                    $("#icon3").addClass('icon-unprocess');
                    $("#bar4").addClass('bar-unprocess');
                    $("#icon4").addClass('icon-unprocess');
                    $("#bar5").addClass('bar-unprocess');
                    $("#icon5").addClass('icon-unprocess');
                    break;
                case 2:
                    //add class
                    $("#icon1").addClass('icon-process')
                    $("#bar2").addClass('bar-process');
                    $("#icon2").addClass('icon-process');
                    $("#bar3").addClass('bar-unprocess');
                    $("#icon3").addClass('icon-unprocess');
                    $("#bar4").addClass('bar-unprocess');
                    $("#icon4").addClass('icon-unprocess');
                    $("#bar5").addClass('bar-unprocess');
                    $("#icon5").addClass('icon-unprocess');
                    break;
                case 3:
                    //add class
                    $("#icon1").addClass('icon-process')
                    $("#bar2").addClass('bar-process');
                    $("#icon2").addClass('icon-process');
                    $("#bar3").addClass('bar-process');
                    $("#icon3").addClass('icon-process');
                    $("#bar4").addClass('bar-unprocess');
                    $("#icon4").addClass('icon-unprocess');
                    $("#bar5").addClass('bar-unprocess');
                    $("#icon5").addClass('icon-unprocess');
                    break;
                case 4:
                    //add class
                    $("#icon1").addClass('icon-process')
                    $("#bar2").addClass('bar-process');
                    $("#icon2").addClass('icon-process');
                    $("#bar3").addClass('bar-process');
                    $("#icon3").addClass('icon-process');
                    $("#bar4").addClass('bar-process');
                    $("#icon4").addClass('icon-process');
                    $("#bar5").addClass('bar-unprocess');
                    $("#icon5").addClass('icon-unprocess');
                    break;
                case 5:
                    //add class
                    $("#icon1").addClass('icon-process')
                    $("#bar2").addClass('bar-process');
                    $("#icon2").addClass('icon-process');
                    $("#bar3").addClass('bar-process');
                    $("#icon3").addClass('icon-process');
                    $("#bar4").addClass('bar-process');
                    $("#icon4").addClass('icon-process');
                    $("#bar5").addClass('bar-process');
                    $("#icon5").addClass('icon-process');
                    break;
                default:
                    $("#icon1").addClass('icon-unprocess')
                    $("#bar2").addClass('bar-unprocess');
                    $("#icon2").addClass('icon-unprocess');
                    $("#bar3").addClass('bar-unprocess');
                    $("#icon3").addClass('icon-unprocess');
                    $("#bar4").addClass('bar-unprocess');
                    $("#icon4").addClass('icon-unprocess');
                    $("#bar5").addClass('bar-unprocess');
                    $("#icon5").addClass('icon-unprocess');
            }

        }

        function changeSearch(type) {
            $("#MainContent_hdftypeSearch").val(type);
            if (type == 1) {
                $("#txtSearchCMND").val("");
                $("#txtSearchCMND").attr("readonly", "readonly");
                $("#txtSearchSoDon").removeAttr("readonly");
            }
            else {
                $("#txtSearchSoDon").val("");
                $("#txtSearchSoDon").attr("readonly", "readonly");
                $("#txtSearchCMND").removeAttr("readonly");
            }
        }

        function fillDataChiTiet(data) {
            $("#lblSoDonThu").text(data.SoDonThu);
            $("#lblNgayTiepNhan").text(data.NgayNhapDonStr);
            $("#lblCoQuanTiepNhan").text(data.TenCoQuanTiepNhan);
            $("#lblHoVaTen").text(data.lsDoiTuongKN ? getTenChuDon(data.lsDoiTuongKN) : "");
            $("#lblDiaChi").text(data.lsDoiTuongKN ? data.lsDoiTuongKN[0].DiaChiCT : "");
            $("#lblNoiDung").text(data.NoiDungDon);

            $("#lblCoQuanXuLy").text(data.TenCoQuanXL);
            $("#lblHuongXuLy").text(data.HuongXuLy ? data.HuongXuLy : "");
            $("#lblTepKQXuLy").html(renderFileDinhKem(data.lsFileYKienXuLy));

            $("#lblCoQuanGiaiQuyet").text(data.TenCoQuanGQ);
            $("#lblTepKQGiaiQuyet").html(renderFileDinhKem(data.lsFileQuyetDinhGD));
            $("#lblNgayBanHanh").text(data.NgayBanHanh ? data.NgayBanHanh : "");
            $("#lblCoQuanBanHanh").text(data.TenCoQuanBanHanh);
            $("#lblSoTienPhaiThu").text(`${data.SoTienPhaiThu} đồng`);
            $("#lblSoDatPhaiThu").text(`${data.SoDatPhaiThu} m2`);
            $("#lblDoiTuongXuLy").text(data.SoDoiTuongBiXuLy);

            setStatus(data.TrangThaiDonID);
        }

        function renderFileDinhKem(listFile) {
            var filepath = '<%= System.Configuration.ConfigurationManager.AppSettings["HOSTPATH_FILEKNTC"].ToString() %>';
            var stringFile = "";
            for (var i = 0; i < listFile.length; i++) {
                if (i != 0) {
                    stringFile += "; "
                }
                stringFile += `<a href="${filepath}/${listFile[i].FileURL}" style="color:blue; margin-left: 10px"><i class="fa fa-download"></i> ${listFile[i].TenFile}</a>`
            }
            return stringFile;
        }
        var flagTogge = false;
        function ddlPhamViDuLieuOnChange() {
            var phamViID = $("#ddlPhamViDuLieu").val();
            if (phamViID == 2) {
                $("#ddlCoQuan").hide();
                $("#ddlCoQuan_chosen").hide();
                flagTogge = false;
            }
            else if (flagTogge == false) {
                $("#ddlCoQuan_chosen").show();

                flagTogge = true;
            }
            console.log("phamViID", phamViID);
            if (phamViID > 0) {
                var url = '<%= System.Configuration.ConfigurationManager.AppSettings["APIUrl_GetCoQuanByPhamViID"].ToString() %>';
                $.ajax({
                    url: url + phamViID,
                    type: "GET",
                    success: function (data) {
                        var json = data;
                        console.log("data_ddlPhamViDuLieuOnChange", data);
                        if (json != null) {
                            $("#ddlCoQuan").empty();
                            if (phamViID == 4 || phamViID == 5) {
                                if (json.length > 1) {
                                    $("#ddlCoQuan").append('<option value=0>' + "Chọn đơn vị" + '</option>');
                                }
                                for (var i = 0; i < json.length; i++) {
                                    var newOption1 = $('<option value="' + json[i].HuyenID + '">' + json[i].TenHuyen + '</option>');
                                    $("#ddlCoQuan").append(newOption1);
                                }
                            }
                            else {
                                $("#ddlCoQuan").append('<option value=0>' + "Chọn đơn vị" + '</option>');
                                for (var i = 0; i < json.length; i++) {
                                    var newOption1 = $('<option value="' + json[i].CoQuanID + '">' + json[i].TenCoQuan + '</option>');
                                    $("#ddlCoQuan").append(newOption1);
                                }
                            }


                            $(".chosen").chosen();
                            $(".chosen").trigger("chosen:updated");
                            $("#ddlCoQuan_chosen").attr("style", "width:140px !important;");
                            if (phamViID == 2) {
                                $("#ddlCoQuan_chosen").hide();
                            }
                            ddlOnChange();
                        }
                    },
                    error: function (data) {

                    }
                });

                //$.ajax({
                //    type: "POST",
                //    url: "Default.aspx/GetCoQuanByPhamViID",
                //    data: '{phamViIDs:"' + phamViID + '"}',
                //    dataType: "json",
                //    async: "true",
                //    contentType: "application/json; charset=utf-8",
                //    success: function (data) {
                //        var json = eval('(' + data.d + ')');
                //        if (json != null) {
                //            $("#ddlCoQuan").empty();
                //            if (phamViID == 4 || phamViID == 5) {
                //                if (json.length > 1) {
                //                    $("#ddlCoQuan").append('<option value=0>' + "Chọn đơn vị" + '</option>');
                //                }
                //                for (var i = 0; i < json.length; i++) {
                //                    var newOption1 = $('<option value="' + json[i].HuyenID + '">' + json[i].TenHuyen + '</option>');
                //                    $("#ddlCoQuan").append(newOption1);
                //                }
                //            }
                //            else {
                //                $("#ddlCoQuan").append('<option value=0>' + "Chọn đơn vị" + '</option>');
                //                for (var i = 0; i < json.length; i++) {
                //                    var newOption1 = $('<option value="' + json[i].CoQuanID + '">' + json[i].TenCoQuan + '</option>');
                //                    $("#ddlCoQuan").append(newOption1);
                //                }
                //            }


                //            $(".chosen").chosen();
                //            $(".chosen").trigger("chosen:updated");
                //            $("#ddlCoQuan_chosen").attr("style", "width:150px !important;float:right;");
                //            if (phamViID == 2) {
                //                $("#ddlCoQuan_chosen").hide();
                //            }
                //            ShowBaoCaoTinhHinhByThoiGian();
                //        }
                //    }
                //});
            }
            else {

            }
        }

    </script>

</asp:Content>