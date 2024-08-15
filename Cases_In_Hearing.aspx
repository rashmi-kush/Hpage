<%@ Page Title="" Language="C#" MasterPageFile="~/CoS/CoSMaster.Master" AutoEventWireup="true" CodeBehind="Cases_In_Hearing.aspx.cs" Inherits="CMS_Sampada.CoS.Cases_In_Hearing" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
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
    <script>
        function Loader() {
            AmagiLoader.show();
        }
        function Loaderstop() {
            AmagiLoader.hide();
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="SCMNG" runat="server"></asp:ScriptManager>
    <section class="content-header">
        <div class="container-fluid">
            <div class="row mb-2">
                <div class="col-sm-6">
                    <h5>Cases In Hearing</h5>
                </div>
                <%-- <div class="col-sm-6">
                    <ol class="breadcrumb float-sm-right">
                        <li class="breadcrumb-item"><a href="#">Home</a></li>
                        <li class="breadcrumb-item active">Cases In Hearing</li>
                    </ol>
                </div>--%>
            </div>
        </div>
    </section>

    <section class="content">
        <div class="container-fluid">

            <div class="row">

                <%--  <div class="col-md-8" style="margin-bottom: 22px;">
                    <asp:TextBox ID="txtHearingDate" runat="server" ClientIDMode="Static" CssClass="form-control" Visible="false"></asp:TextBox>
                    <asp:Calendar ID="Calendar1" runat="server" OnDayRender="Calendar1_DayRender" OnSelectionChanged="Calendar1_SelectionChanged" BackColor="#f0f8ff" BorderColor="#dee2e6" BorderWidth="1px" DayNameFormat="Shortest" Font-Names="Verdana" Font-Size="14pt" ForeColor="#663399" Height="490px" ShowGridLines="True" Width="100%">
                        <DayHeaderStyle BackColor="#FFCC66" Font-Bold="True" Height="1px" />
                        <NextPrevStyle Font-Size="9pt" ForeColor="#FFFFCC" />
                        <OtherMonthDayStyle ForeColor="#CC9966" />
                        <SelectedDayStyle BackColor="#CCCCFF" Font-Bold="True" />
                        <SelectorStyle BackColor="#FFCC66" />
                        <TitleStyle BackColor="#e9564e" Height="47px" Font-Bold="True" Font-Size="9pt" ForeColor="#FFFFCC" />
                        <TodayDayStyle BackColor="#FFCC66" ForeColor="White" />
                    </asp:Calendar>
                   </div>--%>
                <div class="col-md-8" style="margin-bottom: 22px;">
                    <asp:TextBox ID="txtHearingDate" runat="server" ClientIDMode="Static" CssClass="form-control" Visible="false"></asp:TextBox>
                    <asp:Calendar ID="Calendar1" class="calendar" runat="server" OnSelectionChanged="Calendar1_SelectionChanged" OnDayRender="Calendar1_DayRender" BackColor="#f0f8ff" BorderColor="#dee2e6" BorderWidth="1px" DayNameFormat="Shortest" Font-Names="Verdana" Font-Size="14pt" ForeColor="#663399" Height="490px" ShowGridLines="True" Width="100%">
                        <DayHeaderStyle BackColor="#FFCC66" Font-Bold="True" Height="1px" />
                        <NextPrevStyle Font-Size="9pt" ForeColor="#FFFFCC" CssClass="text-center" Font-Bold="true" />
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
                        <div class="card-header">
                            <h3 class="card-title">Cases In Hearing</h3>
                        </div>

                        <div class="col-lg-offset-12 col-md-offset-12 col-lg-12 col-md-12 col-sm-12 col-xs-12">
                            <div class="table-responsive listtabl">

                                <asp:GridView ID="grdCaseList" CssClass="table table-bordered table-condensed table-striped table-hover"
                                    DataKeyNames="Notice_id,App_ID,Case_Number,HearingDate,hearing_id,application_no"
                                    runat="server" CellPadding="4" ForeColor="#333333" GridLines="None"
                                    AutoGenerateColumns="False" Visible="true" Width="100%" EmptyDataText="Today No Hearing">
                                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="SNo." HeaderStyle-VerticalAlign="Top" HeaderStyle-Font-Bold="false" HeaderStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <itemtemplate><%#Container.DataItemIndex+1  %> </itemtemplate>
                                            </ItemTemplate>
                                            <ItemStyle Width="2%" HorizontalAlign="Center" />
                                            <HeaderStyle Width="2%" HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Proposal No." HeaderStyle-Font-Bold="false">
                                            <ItemTemplate>
                                                <%#Eval("application_no") %>
                                            </ItemTemplate>
                                            <HeaderStyle Width="10%" HorizontalAlign="Center" />
                                            <ItemStyle Width="10%" HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Case No." HeaderStyle-Font-Bold="false">
                                            <ItemTemplate>
                                                <%#Eval("Case_Number") %>
                                            </ItemTemplate>
                                            <HeaderStyle Width="10%" HorizontalAlign="Center" />
                                            <ItemStyle Width="10%" HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Notice" HeaderStyle-Font-Bold="false">
                                            <ItemTemplate>
                                                <%#Eval("Notice_id") %>
                                            </ItemTemplate>
                                            <HeaderStyle Width="10%" HorizontalAlign="Center" />
                                            <ItemStyle Width="10%" HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Date of Registration" HeaderStyle-Font-Bold="false">
                                            <ItemTemplate>
                                                <%#Eval("InsertedDate") %>
                                            </ItemTemplate>
                                            <HeaderStyle Width="10%" HorizontalAlign="Center" />
                                            <ItemStyle Width="10%" HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Case Origin" HeaderStyle-Font-Bold="false">
                                            <ItemTemplate>
                                                <%#Eval("department_name") %>
                                            </ItemTemplate>
                                            <HeaderStyle Width="10%" HorizontalAlign="Center" />
                                            <ItemStyle Width="10%" HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Order Sheet (Date)" HeaderStyle-Font-Bold="false">
                                            <ItemTemplate>
                                                <%#Eval("OrderSheetInsertDate") %>
                                            </ItemTemplate>
                                            <HeaderStyle Width="10%" HorizontalAlign="Center" />
                                            <ItemStyle Width="10%" HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Notice" HeaderStyle-Font-Bold="false">
                                            <ItemTemplate>
                                                <%#Eval("Max_Notice_sequence") %>
                                            </ItemTemplate>
                                            <HeaderStyle Width="10%" HorizontalAlign="Center" />
                                            <ItemStyle Width="10%" HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                        <asp:BoundField HeaderStyle-Width="10%" ItemStyle-Width="10%" DataField="EARLY_HEARING_RESPONSE" NullDisplayText='<span style="color:red;">N/A</span>' HeaderText="Request for Early Hearing" HeaderStyle-Font-Bold="false" />

                                        <asp:TemplateField HeaderText="Notice Date" HeaderStyle-Font-Bold="false">
                                            <ItemTemplate>

                                                <%#Eval("NOTICEDATE") %>
                                            </ItemTemplate>
                                            <HeaderStyle Width="10%" HorizontalAlign="Center" />
                                            <ItemStyle Width="10%" HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Hearing Date" HeaderStyle-Font-Bold="false">
                                            <ItemTemplate>
                                                <%# DateTime.Parse(Eval("HearingDate").ToString()).ToString("dd/MM/yyyy") %>
                                                <%--<%#Eval("HearingDate") %>--%>
                                            </ItemTemplate>
                                            <HeaderStyle Width="10%" HorizontalAlign="Center" />
                                            <ItemStyle Width="10%" HorizontalAlign="Center" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Action" HeaderStyle-Font-Bold="false">
                                            <ItemStyle Width="4%" HorizontalAlign="Right" />
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkSelect" runat="server" CommandArgument='<%#Eval("App_ID") %>' CommandName="SelectApplication" OnClientClick="Loader();" OnClick="lnkSelect_Click" CssClass="btn btn-secondary ">Select</asp:LinkButton>

                                            </ItemTemplate>
                                        </asp:TemplateField>
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
        </div>
    </section>

</asp:Content>
