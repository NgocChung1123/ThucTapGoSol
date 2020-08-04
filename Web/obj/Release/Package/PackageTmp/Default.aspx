<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="True"
    CodeBehind="Default.aspx.cs" Inherits="Com.Gosol.CMS.Web._Default" %>

<asp:Content ID="MainContent" runat="server" ContentPlaceHolderID="MainContent">
    <!-- Content Header (Page header) -->
    <section class="content-header">
      <h1>
        Trang chủ
        <small></small>
      </h1>
      <ol class="breadcrumb">
        <li><a href="/Default.aspx"><i class="fa fa-dashboard"></i> Trang chủ</a></li>
      </ol>
    </section>

    <!-- Main content -->
    <section class="content">
      <div class="row">
        <section class="col-lg-8 connectedSortable" id="show"> 
          <div class="box box-success">
            <div class="box-header">
              <i class="fa fa-comments-o"></i>

              <h1 class="box-title"><asp:Label ID="lblTitle" runat="server"></asp:Label></h1>

              <%--<div class="box-tools pull-right" data-toggle="tooltip" title="Status">
                <div class="btn-group" data-toggle="btn-toggle">
                  <button type="button" class="btn btn-default btn-sm active"><i class="fa fa-square text-green"></i>
                  </button>
                  <button type="button" class="btn btn-default btn-sm"><i class="fa fa-square text-red"></i></button>
                </div>
              </div>--%>
            </div>
            <div class="box-body chat" id="chat-box">
                <div class="table-responsive">
                    <table id="table" class="table table-bordered table-hover" style="margin-top: 15px; width: 100%">
                        <thead>
                            <tr>
                                <th style="width: 5%; text-align: center">STT
                                </th>
                                <th style="text-align: center;">Tên kế hoạch thanh tra
                                </th>
                                <th style="text-align: center; width: 15%">Tên cơ quan
                                </th>
                                <th style="text-align: center; width: 15%">Ngày nộp
                                </th>                                                 
                            </tr>
                        </thead>
                        <tbody>
                        </tbody>
                    </table>
                    <div class="paginations" style="margin-top: 15px; margin-bottom: 15px">
                                        <asp:PlaceHolder ID="plhPaging" runat="server"></asp:PlaceHolder>
                                    </div>
                </div>
            </div>
            <!-- /.chat -->
            <div class="box-footer">
              <%--<div class="input-group">
                <input class="form-control" placeholder="Type message...">

                <div class="input-group-btn">
                  <button type="button" class="btn btn-success"><i class="fa fa-plus"></i></button>
                </div>
              </div>--%>
            </div>
          </div>
        </section>
        <section class="col-lg-4 connectedSortable" id="hiden">
          <!-- Calendar -->
          <div class="box box-success bg-green-gradient">
            <div class="box-header">
              <i class="fa fa-calendar"></i>
              <h3 class="box-title" style="color:#fff;">Calendar</h3>
              <!-- tools box -->
              <div class="pull-right box-tools">
                <button type="button" class="btn btn-success btn-sm" data-widget="collapse"><i class="fa fa-minus"></i>
                </button>
                <%--<button type="button" class="btn btn-success btn-sm" onclick="clickRemove();return false;"><i class="fa fa-times"></i>
                </button>--%>
              </div>
              <!-- /. tools -->
            </div>
            <!-- /.box-header -->
            <div class="box-body no-padding" style="color:#444">
              <!--The calendar -->
                <div class="box">
                        <asp:Literal ID="ltrCalendar" runat="server"></asp:Literal>
                    </div> 
              <div id="calendar" style="width: 100%"></div>
            </div>
            <!-- /.box-body -->
            <div class="box-footer text-black">
              <div class="row">
                  <div class="col-sm-12">         
                  </div>
              </div>
              <!-- /.row -->
            </div>
          </div>

          <div class="box box-solid bg-green-gradient">
              <div class="box-header">
                  <img src="/images/document.png" alt="" style="width: 24px" />
                   <span>Tài liệu hướng dẫn sử dụng
                    </span> 
                  <div class="pull-right box-tools">
                <button type="button" class="btn btn-success btn-sm" data-widget="collapse"><i class="fa fa-minus"></i>
                </button>                                
              </div>   
                  </div>
            <div class="box-body no-padding" style="overflow-x: auto; width: 188px">
               <div style="width: 100%">
                    <ul>
                        <asp:Repeater ID="rptTaiLieu" runat="server">
                            <ItemTemplate>
                                <li>
                                    <div style="width: 200px">
                                        <span><a href="#" onclick='downloadFileScanNow(<%# Eval("FileTaiLieuID") %>)' style="cursor: pointer;">
                                        <img src="/images/icon_list_donbt_kntc.png" alt="" style="margin-right: 5px; width: 16px" /><%#Eval("TenFile") %></a></span>
                                    </div>
                                </li>
                            </ItemTemplate>
                        </asp:Repeater>
                    </ul>
              </div>             
            </div>            
           
          </div>

          <div class="box box-solid ">
              <div class="box-header bg-green-gradient">
                  <img src="/images/icon_tit_help_kntc.png" alt="" />
                  <span>Trợ giúp </span>
                  <div class="pull-right box-tools">
                        <button type="button" class="btn btn-success btn-sm" data-widget="collapse"><i class="fa fa-minus"></i>
                        </button>
                  </div>
              </div>                    
             <div class="box-body">
                <div style="width:100%;">
                    <img class="icon" src="/images/icon_phone_kntc.png" style="width: 30px" />
                    <span style="margin-left:15px;">
                        <asp:Label ID="lblHomePhone" runat="server"></asp:Label>
                        <%--<asp:Label ID="lblPhone" runat="server"></asp:Label>--%>
                    </span>                                                      
                </div>                           
                <div class="clear"></div>
            </div>
        </div>           
          <!-- /.box -->


        </section>
      </div>
    </section>
    <!-- /.content -->
    <script type="text/javascript">
        $(document).ready(function () {
            $(".calendar-date").click(function () {
                $(".calendar-note").hide();
                var noteDiv = $(this).find(".calendar-note").eq(0);
                if (noteDiv != null) noteDiv.show();
            });

            $('html').click(function (event) {
                if ($(event.target).closest('.calendar-date').length == 0) {
                    $('.calendar-note').hide()
                }
            });

        });
   
        function clickRemove() {
            $("#hiden").hide();
            $("#show").removeClass("col-lg-8");
            $("#show").addClass("col-lg-12");
        }
    </script>

    <style type="text/css">
        .table table-bordered tr th {
            background-color: red;
        }

        .table table-bordered tr td.calendar-date span.today {
            background-color: #64DB34;
            color: White;
        }
    </style>
</asp:Content>
