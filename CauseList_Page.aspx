<%@ Page Title="" Language="C#" MasterPageFile="~/CoS/CoSMaster.Master" EnableEventValidation="false" AutoEventWireup="true" CodeBehind="CauseList_Page.aspx.cs" Inherits="CMS_Sampada.CoS.CauseList_Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <link href="../Styles/sweetalert.css" rel="stylesheet" />
    <script src="../Scripts/sweetalert.min.js"></script>
    <script src="../assets/js/3.3.1/sweetalert2@11.js"></script>

    <script src="https://code.jquery.com/jquery-3.6.4.min.js"></script>
    <link rel="stylesheet" href="https://code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css">
    <script src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"></script>
   
    <script>
        $(function () {
            $(".datePicks").datepicker({
                dateFormat: 'dd/mm/yy',
                changeMonth: true,
                changeYear: true,
                minDate: +1
            });
        });
    </script>
        <script>
            $(function () {
                $(".datePick").datepicker({
                    dateFormat: 'dd/mm/yy',
                    changeMonth: true,
                    changeYear: true,
                    
                });
            });
        </script>
    
    <script type="text/javascript">
        function showPopup(element) {
            var text = $(element).val();
            if (text.length > 20) {
                //alert(text);
                //var modal = document.querySelector(".cd-popup");
                //var span = document.querySelector(".cd-popup-close");

                //span.onclick = function () {
                //    modal.style.display = "none";
                //}

                //function customalert(message) {

                //    var modalText = document.querySelector("#cd-text");

                //    modalText.innerHTML = message;
                //    /*  alert(modalText.innerHTML);*/
                //    modal.style.display = "block";
                //}

                //customalert(text);
            }
        }
    </script>


    <style>
        /* Popup Styles */
        .cd-popup {
            display: none;
            position: fixed;
            left: 500px;
            top: 100px;
            width: 30%;
            height: 30%;
            background-color: rgba(0, 0, 0, 0.5);
            z-index: 1;
        }

        .cd-popup-container {
            position: relative;
            width: 100%;
            height: 100%;
            max-width: 400px;
            margin: 100px auto;
            background: #fff;
            padding: 20px;
            border-radius: 5px;
            box-shadow: 0 0 10px rgba(0, 0, 0, 0.3);
            text-align: center;
        }

        .cd-popup-close {
            position: absolute;
            top: 10px;
            right: 10px;
            width: 20px;
            height: 20px;
            line-height: 20px;
            text-align: center;
            cursor: pointer;
            color: #ff6a00;
            text-decoration: none;
        }

        /* Additional Styles */
        .alert-primary {
            background-color: rgba(0, 0, 0, 0.3);
            color: #000000;
        }

            .alert-primary p {
                margin-bottom: 15px;
            }
    </style>

    <%-- <style type="text/css">
        .Calendar {
            border: none;
        }

            .Calendar img {
                border: none;
            }

            .Calendar .Title {
                background-color: #7D9459;
                background-image: url(../Images/title_bg.gif);
                border: 1px solid black;
                border-bottom-width: 0px;
            }

                .Calendar .Title td {
                    font-family: verdana;
                    font-size: 11px;
                    font-weight: bold;
                    color: White;
                    padding-top: 1px;
                    padding-bottom: 1px;
                }

            .Calendar .DayHeader {
                background-color: #E3E0CD;
                background-image: url(../Images/header_bg.gif);
                color: #504C39;
                font-family: Verdana;
                font-size: 11px;
                text-align: center;
                border-top: solid 1px #FFFFFF;
                border-left: solid 1px #FFFFFF;
                border-bottom: solid 1px #ACA899;
                border-right: solid 1px #C6C1AC;
                padding: 4px;
                font-weight: normal;
            }

            .Calendar .Day {
                width: 350px;
                height: 70px;
                text-align: center;
                vertical-align: top;
                font-family: Verdana;
                font-size: 11px;
                color: Black;
                background-color: #FFFFFF;
                border: solid 1px #C6C1AC;
                padding: 2px;
            }

            .Calendar .OtherMonthDay {
                background-color: #F5F3E5;
            }
    </style>--%>
    <style>
        .template {
            height: 397px;
            /* overflow-y: scroll;*/
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="SCMNG" runat="server"></asp:ScriptManager>
    <section class="content-header">
        <div class="container-fluid">
            <div class="row mb-2">
                <div class="col-sm-6">
                    <h5>Cause List</h5>
                </div>
                <div class="col-sm-6">
                    <ol class="breadcrumb float-sm-right">
                        <li class="breadcrumb-item"><a href="#">Home</a></li>
                        <li class="breadcrumb-item active">Cases In Hearing</li>
                    </ol>
                </div>
            </div>
        </div>
    </section>
    <section class="content">
        <div class="container-fluid">

            <div class="row">

                <div class="col-md-8">
                    <asp:TextBox ID="txtHearingDate" runat="server" ClientIDMode="Static" CssClass="form-control" Visible="false"></asp:TextBox>
                    <asp:Calendar ID="Calendar1" runat="server" OnDayRender="Calendar1_DayRender" OnSelectionChanged="Calendar1_SelectionChanged" BackColor="#f0f8ff" BorderColor="#dee2e6" BorderWidth="1px" DayNameFormat="Shortest" Font-Names="Verdana" Font-Size="14pt" ForeColor="#663399" Height="490px" ShowGridLines="True" Width="940px">
                        <DayHeaderStyle BackColor="#FFCC66" Font-Bold="True" Height="1px" />
                        <NextPrevStyle Font-Size="9pt" ForeColor="#FFFFCC" />
                        <OtherMonthDayStyle ForeColor="#CC9966" />
                        <SelectedDayStyle BackColor="#CCCCFF" Font-Bold="True" />
                        <SelectorStyle BackColor="#FFCC66" />
                        <TitleStyle BackColor="#e9564e" Height="47px" Font-Bold="True" Font-Size="9pt" ForeColor="#FFFFCC" />
                        <TodayDayStyle BackColor="#FFCC66" ForeColor="White" />
                    </asp:Calendar>
                </div>


                <div class="col-md-4">

                    <div class="card">
                        <div class="card-header scroll">
                            <h3 class="card-title">Upcoming</h3>

                        </div>
                        <!-- /.card-header -->
                        <asp:Panel ID="pnlMultipleBeneficiaries" Style="height: 397px" runat="server">
                            <div class="card-body p-0 dmpl">
                                <ul class="products-list product-list-in-card pl-2 pr-2">
                                    <div class="abt-slider">
                                        <asp:Repeater ID="RepDetails" runat="server">
                                            <ItemTemplate>

                                                <li class="item">
                                                    <div class="product-info">
                                                        <span class="product-description">Hearing Date : <a href="javascript:void(0)" class="product-title">
                                                            <asp:Label ID="lblhearing" runat="server" Text='<%# Eval("HearingDate", "{0:dd/MM/yyyy}") %>' Font-Bold="true" />
                                                        </a></span>
                                                    </div>

                                                    <div class="product-info">
                                                        <span class="product-description">Basis of Impound : <a href="javascript:void(0)" class="product-title">
                                                            <asp:Label ID="lblComment" runat="server" Text='<%#Eval("ImpoundingReasons_En") %>' Font-Bold="true" />
                                                        </a></span>
                                                    </div>
                                                </li>

                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </div>
                                </ul>

                            </div>
                        </asp:Panel>

                        <%--  <div class="card-body p-0 dmpl">
                        <ul class="products-list product-list-in-card pl-2 pr-2">
                            <div class="abt-slider">
                                <li class="item">
                                    <div class="product-info">
                                        <a href="javascript:void(0)" class="product-title">20-07-2023 </a>
                                        <span class="product-description">Let theme shine like a star
                                        </span>
                                    </div>
                                </li>

                                <!-- /.item -->
                                <li class="item">
                                    <div class="product-info">
                                        <a href="javascript:void(0)" class="product-title">20-07-2023 </a>
                                        <span class="product-description">Let theme shine like a star
                                        </span>
                                    </div>
                                </li>

                                <!-- /.item -->
                                <li class="item">
                                    <div class="product-info">
                                        <a href="javascript:void(0)" class="product-title">20-07-2023 </a>
                                        <span class="product-description">Let theme shine like a star
                                        </span>
                                    </div>
                                </li>
                                <!-- /.item -->
                                <li class="item">
                                    <div class="product-info">
                                        <a href="javascript:void(0)" class="product-title">20-07-2023 </a>
                                        <span class="product-description">Let theme shine like a star
                                        </span>
                                    </div>
                                </li>
                                <li class="item">
                                    <div class="product-info">
                                        <a href="javascript:void(0)" class="product-title">20-07-2023 </a>
                                        <span class="product-description">Let theme shine like a star
                                        </span>
                                    </div>
                                </li>
                                <li class="item">
                                    <div class="product-info">
                                        <a href="javascript:void(0)" class="product-title">20-07-2023 </a>
                                        <span class="product-description">Let theme shine like a star
                                        </span>
                                    </div>
                                </li>
                                <!-- /.item -->
                            </div>

                        </ul>
                    </div>--%>
                        <!-- /.card-body -->
                        <div class="card-footer text-center">
                            <a href="javascript:void(0)" class="uppercase">View All</a>
                        </div>
                        <!-- /.card-footer -->
                    </div>

                </div>

            </div>

            <div class="row">
                <div class="col-12">
                    <div class="card">
                        <div class="card-header p-2 pt-1 d-flex align-items-center">
                            <div class="col-sm-6 p-0">
                                <ul class="nav nav-pills">
                                    <li class="card-header lign-items-center" style="font-size: larger;">Cause List for COS</li>
                                    <%-- <li class="nav-item"><a class="nav-link active" href="Ordersheet_Pending">Order Sheet</a></li>
                                    <li class="nav-item"><a class="nav-link" href="Notice_Pending">Notice</a></li>
                                     <li class="nav-item"><a class="nav-link" href="FinalOrder_PendingList">Final Order</a></li>--%>
                                    <%-- <li class="nav-item"><a class="nav-link" href="ReportSeeking">Seek
                        Report</a></li>
                                    <li class="nav-item"><a class="nav-link disabled" href="EarlyHearing" data-toggle="tab">Early
                        Hearing</a>
                                    </li>
                                    <li class="nav-item"><a class="nav-link disabled" href="SendToAppeal" data-toggle="tab">Send to
                        Appeal</a>
                                    </li>--%>
                                </ul>

                            </div>
                            <div class="align-items-end col-sm-6 w-100">
                                <div class="text-right">
                                    <div class="row">
                                        <div class="col-2">
                                            <%--<div class="input-group date" id="search" data-target-input="nearest">
                                                <input type="text" class="form-control" data-target="#search" placeholder="Search...">
                                            </div>--%>
                                        </div>
                                        <div class="col-3">
                                            <asp:TextBox ID="txtfromdateCos" runat="server" CssClass="form-control datePick" placeholder="dd/mm/yyyy"></asp:TextBox>

                                            <%-- <div class="input-group date" id="fromdate" data-target-input="nearest">
                                               <asp:TextBox ID="txtfromdateCos" runat="server" CssClass="form-control" data-target="#fromdate" TextMode="Date" placeholder="From Date"></asp:TextBox>
                                                <div class="input-group-append" data-target="#fromdate" data-toggle="Date">
                                                    <div class="input-group-text"></div>
                                                </div>
                                            </div>--%>
                                        </div>
                                        <div class="col-3 d-flex">
                                            <%--<asp:TextBox ID="txttodateCos" runat="server" CssClass="form-control"  TextMode="Date" placeholder="To Date"></asp:TextBox>--%>
                                            <asp:TextBox ID="txttodateCos" runat="server" CssClass="form-control datePick" placeholder="dd/mm/yyyy"></asp:TextBox>

                                            <%--<div class="input-group date" id="todate" data-target-input="nearest">
                                                <asp:TextBox ID="txttodateCos" runat="server" CssClass="form-control" data-target="#todate" TextMode="Date" placeholder="To Date"></asp:TextBox>
                                                <div class="input-group-append" data-target="#todate" data-toggle="Date">
                                                    <div class="input-group-text"></div>
                                                </div>
                                            </div>--%>
                                        </div>
                                        <div class="col-2">
                                            <asp:Button ID="btnsearchCOS" runat="server" CssClass="btn btn-info t-btn w-100" Text="Search" OnClick="btnsearchCOS_Click" />
                                        </div>
                                        <div class="col-2">
                                            <asp:Button ID="btnPrintPDFCOS" CssClass="btn btn-info t-btn w-100" runat="server" OnClick="btnPrintPDFCOS_Click" Text="Download" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>


                        <%--<div class="card-header">
                            <h3 class="card-title">Cases In Hearing For COS</h3>
                            
                        </div>--%>

                        <div class="col-lg-offset-12 col-md-offset-12 col-lg-12 col-md-12 col-sm-12 col-xs-12">
                            <div class="table-responsive listtabl">
                                <asp:GridView ID="grdCaseList" CssClass="table table-bordered table-condensed table-striped table-hover"
                                    DataKeyNames="Case_Number,HearingDate,NOTICE_ID,Hearing_ID,application_no,app_id"
                                    runat="server" CellPadding="4" ForeColor="#333333" GridLines="None"
                                    AutoGenerateColumns="False" Visible="true" Width="100%" EmptyDataText="No Hearing Today" OnRowCommand="grdCaseList_RowCommand">
                                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="SNo." HeaderStyle-VerticalAlign="Top" HeaderStyle-Font-Bold="false" HeaderStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <itemtemplate><%#Container.DataItemIndex+1  %> </itemtemplate>
                                            </ItemTemplate>
                                            <ItemStyle Width="2%" HorizontalAlign="Center" />
                                            <HeaderStyle Width="2%" HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Case Number" HeaderStyle-Font-Bold="false">
                                            <ItemTemplate>
                                                <%#Eval("Case_Number") %>
                                            </ItemTemplate>
                                            <HeaderStyle Width="10%" HorizontalAlign="Center" />
                                            <ItemStyle Width="10%" HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Case Register Date" HeaderStyle-Font-Bold="false">
                                            <ItemTemplate>
                                                <%#Eval("Case_RegistrationDate") %>
                                            </ItemTemplate>
                                            <HeaderStyle Width="10%" HorizontalAlign="Center" />
                                            <ItemStyle Width="10%" HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Details of Applicant" HeaderStyle-Font-Bold="false">
                                            <ItemTemplate>
                                                <%#Eval("ApplicantDetails") %>
                                            </ItemTemplate>
                                            <HeaderStyle Width="10%" HorizontalAlign="Center" />
                                            <ItemStyle Width="10%" HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Details of Respondent" HeaderStyle-Font-Bold="false">
                                            <ItemTemplate>
                                                <%#Eval("RespondentDetails") %>
                                            </ItemTemplate>
                                            <HeaderStyle Width="10%" HorizontalAlign="Center" />
                                            <ItemStyle Width="10%" HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Subject" HeaderStyle-Font-Bold="false">
                                            <ItemTemplate>
                                                <%#Eval("Subject") %>
                                            </ItemTemplate>
                                            <HeaderStyle Width="10%" HorizontalAlign="Center" />
                                            <ItemStyle Width="10%" HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="NoticeId" HeaderStyle-Font-Bold="false" Visible="false">
                                            <ItemTemplate>
                                                <%#Eval("NOTICE_ID") %>
                                            </ItemTemplate>
                                            <HeaderStyle Width="10%" HorizontalAlign="Center" />
                                            <ItemStyle Width="10%" HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="HearingId" HeaderStyle-Font-Bold="false" Visible="false">
                                            <ItemTemplate>
                                                <%#Eval("Hearing_ID") %>
                                            </ItemTemplate>
                                            <HeaderStyle Width="10%" HorizontalAlign="Center" />
                                            <ItemStyle Width="10%" HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Hearing Date" HeaderStyle-Font-Bold="false">
                                            <ItemTemplate>
                                                <%#Eval("HearingDate") %>
                                            </ItemTemplate>
                                            <HeaderStyle Width="10%" HorizontalAlign="Center" />
                                            <ItemStyle Width="10%" HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="Application Number" HeaderStyle-Font-Bold="false" Visible="false">
                                            <ItemTemplate>
                                                <%#Eval("application_no") %>
                                            </ItemTemplate>
                                            <HeaderStyle Width="10%" HorizontalAlign="Center" />
                                            <ItemStyle Width="10%" HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="App ID" HeaderStyle-Font-Bold="false" Visible="false">
                                            <ItemTemplate>
                                                <%#Eval("app_id") %>
                                            </ItemTemplate>
                                            <HeaderStyle Width="10%" HorizontalAlign="Center" />
                                            <ItemStyle Width="10%" HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Next Hearing Date" HeaderStyle-Font-Bold="false">
                                            <ItemTemplate>
                                                <asp:TextBox runat="server" placeholder="dd/mm/yyyy" ID="txtNHdateCOS" ClientIDMode="AutoID" CssClass="form-control datePicks"
                                                    Text='<%# String.IsNullOrEmpty(Eval("NEXTHEARINGDATE").ToString()) ? "" : Eval("NEXTHEARINGDATE").ToString() %>' />

                                            </ItemTemplate>
                                            <HeaderStyle Width="10%" HorizontalAlign="Center" />
                                            <ItemStyle Width="10%" HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <%--                                        <asp:TemplateField HeaderText="Next Hearing Date" HeaderStyle-Font-Bold="false">

                                            <ItemTemplate>
                                                <asp:TextBox runat="server" placeholder="dd/mm/yyyy" ID="txtNHdateCOS" CssClass="form-control datePick" />
                                                <%--<div class="input-group date" id="NHdateCOS" data-target-input="nearest">
                                                <input type="text" class="form-control datetimepicker-input" data-target="#NHdateCOS" placeholder="Next Date">
                                                <div class="input-group-append" data-target="#NHdateCOS" data-toggle="datetimepicker">
                                                    <div class="input-group-text"><i class="fa fa-calendar"></i></div>
                                                </div>
                                            </div>--%>

                                        <%-- <%#Eval("NEXTHEARINGDATE") %>
                                            </ItemTemplate>
                                            <HeaderStyle Width="10%" HorizontalAlign="Center" />
                                            <ItemStyle Width="10%" HorizontalAlign="Center" />
                                        </asp:TemplateField>--%>
                                        <asp:TemplateField HeaderText="Remark" HeaderStyle-Font-Bold="false">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtRemarkCOS" class="form-control" runat="server" Rows="2" TextMode="MultiLine" Width="100%"
                                                    Text='<%# Eval("REMARK") %>' ReadOnly='<%# !String.IsNullOrEmpty(Eval("REMARK").ToString()) %>'></asp:TextBox>

                                                <asp:LinkButton ID="lbSaveRemarkCOS" runat="server" CommandName="SaveRemarkCOS"
                                                    Text='<%# String.IsNullOrEmpty(Eval("REMARK").ToString()) ? "Add" : "Edit" %>' class="btn btn-info t-btn w-10"
                                                    CommandArgument='<%# Eval("App_ID") %>' Visible="true"></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>



                                        <asp:TemplateField HeaderText="Action" HeaderStyle-Font-Bold="false">
                                            <ItemStyle Width="4%" HorizontalAlign="Right" />
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkSelect" runat="server" CommandArgument='<%#Eval("App_ID") %>' CommandName="SelectApplication" OnClick="lnkSelect_Click" CssClass="btn btn-secondary ">Select</asp:LinkButton>

                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <%-----------------------------------------------------------------%>
                                        <%-- <asp:TemplateField HeaderText="Proposal No">
                                            <ItemTemplate>
                                                <%#Eval("application_no") %>
                                            </ItemTemplate>
                                            <HeaderStyle Width="10%" HorizontalAlign="Center" />
                                            <ItemStyle Width="10%" HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Case No">
                                            <ItemTemplate>
                                                <%#Eval("Case_Number") %>
                                            </ItemTemplate>
                                            <HeaderStyle Width="10%" HorizontalAlign="Center" />
                                            <ItemStyle Width="10%" HorizontalAlign="Center" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Date of Registration">
                                            <ItemTemplate>
                                                <%#Eval("InsertedDate") %>
                                            </ItemTemplate>
                                            <HeaderStyle Width="10%" HorizontalAlign="Center" />
                                            <ItemStyle Width="10%" HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Case Origin">
                                            <ItemTemplate>
                                                <%#Eval("department_name") %>
                                            </ItemTemplate>
                                            <HeaderStyle Width="10%" HorizontalAlign="Center" />
                                            <ItemStyle Width="10%" HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Order Sheet (Date)">
                                            <ItemTemplate>
                                                <%#Eval("OrderSheetInsertDate") %>
                                            </ItemTemplate>
                                            <HeaderStyle Width="10%" HorizontalAlign="Center" />
                                            <ItemStyle Width="10%" HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Notice">
                                            <ItemTemplate>
                                                <%#Eval("Notice_sequence") %>
                                            </ItemTemplate>
                                            <HeaderStyle Width="10%" HorizontalAlign="Center" />
                                            <ItemStyle Width="10%" HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                        <asp:BoundField HeaderStyle-Width="10%" ItemStyle-Width="10%" DataField="EARLY_HEARING_RESPONSE" NullDisplayText='<span style="color:red;">N/A</span>' HeaderText="Request for Early Hearing" />


                                        <asp:TemplateField HeaderText="Hearing Date">
                                            <ItemTemplate>
                                                <%#Eval("HearingDate") %>
                                            </ItemTemplate>
                                            <HeaderStyle Width="10%" HorizontalAlign="Center" />
                                            <ItemStyle Width="10%" HorizontalAlign="Center" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText=" COS Action">
                                            <ItemStyle Width="4%" HorizontalAlign="Right" />
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkSelect" runat="server" CommandArgument='<%#Eval("App_ID") %>' CommandName="SelectApplication" OnClick="lnkSelect_Click" CssClass="btn btn-secondary ">Select</asp:LinkButton>

                                            </ItemTemplate>
                                        </asp:TemplateField>--%>
                                        <%-----------------------------------------------------------------%>
                                    </Columns>
                                    <%-- <EditRowStyle BackColor="#999999" />
                                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />--%>
                                    <HeaderStyle BackColor="#e9564e" ForeColor="White" />
                                    <%--<PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                    <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                    <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                    <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                    <SortedDescendingHeaderStyle BackColor="#6F8DAE" />--%>
                                </asp:GridView>

                                <%--<table id="example11" class="table table-bordered table-striped">
                                    <thead>
                                        <tr>
                                            <th>SNo.</th>
                                            <th>Case No </th>
                                            <th>Date of Registration </th>
                                            <th>Case Origin</th>
                                            <th>Order Sheet (Date)</th>
                                            <th>Notice</th>
                                            <th>Hearing Date</th>
                                            <th>Action</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr>
                                            <td>1</td>
                                            <td>Seller </td>
                                            <td>Vaishali Rathore</td>
                                            <td>Individual</td>
                                            <td>1cvbcvb</td>
                                            <td>Seller </td>
                                            <td>Vaishali Rathore</td>
                                            <td><a href="Hearing">
                                                <button type="button" class="btn t-btn">Select</button>
                                            </a></td>
                                        </tr>
                                        <tr>
                                            <td>2</td>
                                            <td>Seller </td>
                                            <td>Mohan Gupta</td>
                                            <td>Organisation</td>
                                            <td>gfhdgfh</td>
                                            <td>Seller </td>
                                            <td>Vaishali Rathore</td>
                                            <td><a href="Hearing">
                                                <button type="button" class="btn t-btn">Select</button>
                                            </a></td>
                                        </tr>
                                    </tbody>
                                </table>--%>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-12">
                    <div class="card">
                        <div class="card-header p-2 pt-1 d-flex align-items-center">
                            <div class="col-sm-6 p-0">
                                <ul class="nav nav-pills">
                                    <li class="card-header lign-items-center" style="font-size: larger;">Cause List for RRC</li>

                                    <%-- <li class="nav-item"><a class="nav-link active" href="Ordersheet_Pending">Order Sheet</a></li>
                                    <li class="nav-item"><a class="nav-link" href="Notice_Pending">Notice</a></li>
                                     <li class="nav-item"><a class="nav-link" href="FinalOrder_PendingList">Final Order</a></li>--%>
                                    <%-- <li class="nav-item"><a class="nav-link" href="ReportSeeking">Seek
                        Report</a></li>
                                    <li class="nav-item"><a class="nav-link disabled" href="EarlyHearing" data-toggle="tab">Early
                        Hearing</a>
                                    </li>
                                    <li class="nav-item"><a class="nav-link disabled" href="SendToAppeal" data-toggle="tab">Send to
                        Appeal</a>
                                    </li>--%>
                                </ul>

                            </div>
                            <div class="align-items-end col-sm-6 w-100">
                                <div class="text-right">
                                    <div class="row">
                                        <div class="col-2">
                                            <%--<div class="input-group date" id="search" data-target-input="nearest">
                                                <input type="text" class="form-control" data-target="#search" placeholder="Search...">
                                            </div>--%>
                                        </div>
                                        <div class="col-3">
                                            <%--  <asp:TextBox ID="txtFromDateRRC" runat="server" CssClass="form-control" data-target="#todateRRC" TextMode="Date" placeholder="From Date"></asp:TextBox>--%>
                                            <asp:TextBox ID="txtFromDateRRC" runat="server" CssClass="form-control datePick" data-target="#todateRRC" Placeholder="dd/mm/yyyy"></asp:TextBox>

                                            <%--<div class="input-group date" id="fromdateRRC" data-target-input="nearest">
                                               <asp:TextBox ID="txtFromDateRRC" runat="server" CssClass="form-control" data-target="#todateRRC" TextMode="Date" placeholder="From Date"></asp:TextBox>
                                                <div class="input-group-append" data-target="#fromdateRRC" data-toggle="datetimepicker">
                                                    <div class="input-group-text"></div>
                                                </div>
                                            </div>--%>
                                        </div>
                                        <div class="col-3 d-flex">
                                            <asp:TextBox ID="txtFromToRRC" runat="server" CssClass="form-control datePick" data-target="#todateRRC" Placeholder="dd/mm/yyyy">  </asp:TextBox>

                                            <%--<div class="input-group date" id="todateRRC" data-target-input="nearest">
                                               <asp:TextBox ID="txtFromToRRC" runat="server" CssClass="form-control" data-target="#todateRRC" TextMode="Date" placeholder="To Date"></asp:TextBox>
                                                <div class="input-group-append" data-target="#todateRRC" data-toggle="datetimepicker">
                                                    <div class="input-group-text"></div>
                                                </div>
                                            </div>--%>
                                        </div>
                                        <div class="col-2">
                                            <asp:Button ID="btnsearchRRC" runat="server" CssClass="btn btn-info t-btn w-100" Text="Search" OnClick="btnsearchRRC_Click" />
                                        </div>
                                        <div class="col-2">
                                            <asp:Button ID="btnPrintdPDFRRC" CssClass="btn btn-info t-btn w-100" OnClick="btnPrintdPDFRRC_Click" runat="server" Text="Download" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <%--  <div class="card-header">
                            <h3 class="card-title">Cases In Hearing For RRC</h3>
                        </div>--%>

                        <div class="col-lg-offset-12 col-md-offset-12 col-lg-12 col-md-12 col-sm-12 col-xs-12">
                            <div class="table-responsive listtabl">
                                <asp:GridView ID="GrdRRCCaseList" CssClass="table table-bordered table-condensed table-striped table-hover"
                                    DataKeyNames="Case_Number, App_ID, HearingDate,RRC_Case_ID,INSERTEDDATE,CASE_ACTIONDATE"
                                    runat="server" CellPadding="4" ForeColor="#333333" GridLines="None"
                                    AutoGenerateColumns="False" Visible="true" Width="100%" EmptyDataText="No Hearing Today" OnRowCommand="GrdRRCCaseList_RowCommand">
                                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="SNo." HeaderStyle-VerticalAlign="Top" HeaderStyle-Font-Bold="false" HeaderStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <itemtemplate><%#Container.DataItemIndex+1  %> </itemtemplate>
                                            </ItemTemplate>
                                            <ItemStyle Width="2%" HorizontalAlign="Center" />
                                            <HeaderStyle Width="2%" HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Case Number" HeaderStyle-Font-Bold="false">
                                            <ItemTemplate>
                                                <%#Eval("Case_Number") %>
                                            </ItemTemplate>
                                            <HeaderStyle Width="10%" HorizontalAlign="Center" />
                                            <ItemStyle Width="10%" HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Case Register Date" HeaderStyle-Font-Bold="false">
                                            <ItemTemplate>
                                                <%#Eval("Case_RegistrationDate") %>
                                            </ItemTemplate>
                                            <HeaderStyle Width="10%" HorizontalAlign="Center" />
                                            <ItemStyle Width="10%" HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Details of Applicant" HeaderStyle-Font-Bold="false">
                                            <ItemTemplate>
                                                <%#Eval("ApplicantDetails") %>
                                            </ItemTemplate>
                                            <HeaderStyle Width="10%" HorizontalAlign="Center" />
                                            <ItemStyle Width="10%" HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Details of Respondent" HeaderStyle-Font-Bold="false">
                                            <ItemTemplate>
                                                <%#Eval("RespondentDetails") %>
                                            </ItemTemplate>
                                            <HeaderStyle Width="10%" HorizontalAlign="Center" />
                                            <ItemStyle Width="10%" HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Subject" HeaderStyle-Font-Bold="false">
                                            <ItemTemplate>
                                                <%#Eval("Subject") %>
                                            </ItemTemplate>
                                            <HeaderStyle Width="10%" HorizontalAlign="Center" />
                                            <ItemStyle Width="10%" HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="RRC_Registrered Date" HeaderStyle-Font-Bold="false" Visible="false">
                                            <ItemTemplate>
                                                <%#Eval("INSERTEDDATE") %>
                                            </ItemTemplate>
                                            <HeaderStyle Width="10%" HorizontalAlign="Center" />
                                            <ItemStyle Width="10%" HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="final Order Date" HeaderStyle-Font-Bold="false" Visible="false">
                                            <ItemTemplate>
                                                <%#Eval("CASE_ACTIONDATE") %>
                                            </ItemTemplate>
                                            <HeaderStyle Width="10%" HorizontalAlign="Center" />
                                            <ItemStyle Width="10%" HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                          <asp:TemplateField HeaderText="RRCID" HeaderStyle-Font-Bold="false" Visible="false">
                                            <ItemTemplate>
                                                <%#Eval("RRC_Case_ID") %>
                                            </ItemTemplate>
                                            <HeaderStyle Width="10%" HorizontalAlign="Center" />
                                            <ItemStyle Width="10%" HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Hearing Date" HeaderStyle-Font-Bold="false">
                                            <ItemTemplate>
                                                <%#Eval("HearingDate") %>
                                            </ItemTemplate>
                                            <HeaderStyle Width="10%" HorizontalAlign="Center" />
                                            <ItemStyle Width="10%" HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                           <asp:TemplateField HeaderText="Next Hearing Date" HeaderStyle-Font-Bold="false">
                                            <ItemTemplate>
                                                <asp:TextBox runat="server" placeholder="dd/mm/yyyy" ID="txtNHdateRRC" ClientIDMode="AutoID"  CssClass="form-control datePicks"
                                                    Text='<%# String.IsNullOrEmpty(Eval("Next_HearDate_FromRemark").ToString()) ? "" : Eval("Next_HearDate_FromRemark").ToString() %>' />

                                            </ItemTemplate>
                                            <HeaderStyle Width="10%" HorizontalAlign="Center" />
                                            <ItemStyle Width="10%" HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                          <asp:TemplateField HeaderText="Remark" HeaderStyle-Font-Bold="false">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtRemarkRRC" class="form-control" runat="server" Rows="2" TextMode="MultiLine" Width="100%"
                                                    Text='<%# Eval("remark") %>' ReadOnly='<%# !String.IsNullOrEmpty(Eval("remark").ToString()) %>'></asp:TextBox>

                                                <asp:LinkButton ID="lbSaveRemarkRRC" runat="server" CommandName="SaveRemarkRRC"
                                                    Text='<%# String.IsNullOrEmpty(Eval("remark").ToString()) ? "Add" : "Edit" %>' class="btn btn-info t-btn w-10"
                                                    CommandArgument='<%# Eval("App_ID") %>' Visible="true"></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        
                                        <asp:TemplateField HeaderText="Action" HeaderStyle-Font-Bold="false">
                                            <ItemStyle Width="4%" HorizontalAlign="Right" />
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkSelectRRC" runat="server" CommandArgument='<%#Eval("App_ID") %>' CommandName="SelectApplication" OnClick="lnkSelectRRC_Click" CssClass="btn btn-secondary ">Select</asp:LinkButton>

                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <%-------------------------------------------------------------------------------%>

                                        <%-- <asp:TemplateField HeaderText="Proposal No">
                                            <ItemTemplate>
                                                <%#Eval("application_no") %>
                                            </ItemTemplate>
                                            <HeaderStyle Width="10%" HorizontalAlign="Center" />
                                            <ItemStyle Width="10%" HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Case No">
                                            <ItemTemplate>
                                                <%#Eval("rrc_casenumber") %>
                                            </ItemTemplate>
                                            <HeaderStyle Width="10%" HorizontalAlign="Center" />
                                            <ItemStyle Width="10%" HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Date of Registration">
                                            <ItemTemplate>
                                                <%#Eval("rrccase_InsertedDate") %>
                                            </ItemTemplate>
                                            <HeaderStyle Width="10%" HorizontalAlign="Center" />
                                            <ItemStyle Width="10%" HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Case Origin">
                                            <ItemTemplate>
                                                <%#Eval("department_name") %>
                                            </ItemTemplate>
                                            <HeaderStyle Width="10%" HorizontalAlign="Center" />
                                            <ItemStyle Width="10%" HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Order Sheet (Date)">
                                            <ItemTemplate>
                                                <%#Eval("rrc_OrderSheetInsertDate") %>
                                            </ItemTemplate>
                                            <HeaderStyle Width="10%" HorizontalAlign="Center" />
                                            <ItemStyle Width="10%" HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Notice">
                                            <ItemTemplate>
                                                <%#Eval("Notice_sequence") %>
                                            </ItemTemplate>
                                            <HeaderStyle Width="10%" HorizontalAlign="Center" />
                                            <ItemStyle Width="10%" HorizontalAlign="Center" />
                                        </asp:TemplateField>--%>

                                        <%--<asp:BoundField HeaderStyle-Width="10%" ItemStyle-Width="10%" DataField="EARLY_HEARING_RESPONSE" NullDisplayText='<span style="color:red;">N/A</span>' HeaderText="Request for Early Hearing" />--%>

                                        <%--   <asp:TemplateField HeaderText="Hearing Date">
                                            <ItemTemplate>
                                                <%#Eval("rrc_HearingDate") %>
                                            </ItemTemplate>
                                            <HeaderStyle Width="10%" HorizontalAlign="Center" />
                                            <ItemStyle Width="10%" HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText=" COS Action">
                                            <ItemStyle Width="4%" HorizontalAlign="Right" />
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkSelectRRC" runat="server" CommandArgument='<%#Eval("App_ID") %>' CommandName="SelectApplication" OnClick="lnkSelectRRC_Click" CssClass="btn btn-secondary ">Select</asp:LinkButton>

                                            </ItemTemplate>
                                        </asp:TemplateField>--%>
                                        <%-------------------------------------------------------------------------------%>
                                    </Columns>
                                    <%-- <EditRowStyle BackColor="#999999" />
                                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />--%>
                                    <HeaderStyle BackColor="#e9564e" ForeColor="White" />

                                </asp:GridView>


                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </section>

    <div class="cd-popup alert alert-primary" role="alert">
        <div class="cd-popup-container">
            <p id="cd-text"></p>
            <a href="#0" class="cd-popup-close img-replace">Close</a>
        </div>

    </div>
    


</asp:Content>

