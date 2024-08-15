<%@ Page Title="" Language="C#" MasterPageFile="~/CoS/CoSMaster.Master" AutoEventWireup="true" CodeBehind="CoSHome.aspx.cs" Inherits="CMS_Sampada.CoS.CoSHome" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <section class="content-header">
        <div class="container-fluid">
            <div class="row mb-2">
                <div class="col-sm-6">
                    <h5>Dashboard</h5>
                    <asp:Label ID="lblOfficecode" runat="server"></asp:Label>
                </div>
                <%--<div class="col-sm-6">
                    <div class="float-sm-right">
                        <div class="row">
                            <div class="col-3"></div>
                            <div class="col-3">
                                <div class="input-group date" id="fromdate" data-target-input="nearest">
                                    <input type="text" class="form-control datetimepicker-input" data-target="#fromdate"
                                        placeholder="From Date">
                                    <div class="input-group-append" data-target="#fromdate" data-toggle="datetimepicker">
                                        <div class="input-group-text"><i class="fa fa-calendar"></i></div>
                                    </div>
                                </div>
                            </div>
                          <div class="col-3">
                                <div class="input-group date" id="todate" data-target-input="nearest">
                                    <input type="text" class="form-control datetimepicker-input" data-target="#todate"
                                        placeholder="To Date">
                                    <div class="input-group-append" data-target="#todate" data-toggle="datetimepicker">
                                        <div class="input-group-text"><i class="fa fa-calendar"></i></div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-3">
                                <button type="submit" class="btn btn-info  t-btn">Ok</button>
                            </div>
                        </div>
                    </div>
                </div>--%>
            </div>
        </div>
    </section>
    <hr>
    <section class="content">
        <div class="container-fluid">

            <%--            <div class="form-group">
                <div class="row">
                    <div class="col-6"></div>
                    <div class="col-2">
                        <div class="input-group date" id="fromdate" data-target-input="nearest">
                            <input type="text" class="form-control datetimepicker-input" data-target="#fromdate"
                                placeholder="From Date">
                            <div class="input-group-append" data-target="#fromdate" data-toggle="datetimepicker">
                                <div class="input-group-text"><i class="fa fa-calendar"></i></div>
                            </div>
                        </div>
                    </div>
                    <div class="col-2">
                        <div class="input-group date" id="todate" data-target-input="nearest">
                            <input type="text" class="form-control datetimepicker-input" data-target="#todate"
                                placeholder="To Date">
                            <div class="input-group-append" data-target="#todate" data-toggle="datetimepicker">
                                <div class="input-group-text"><i class="fa fa-calendar"></i></div>
                            </div>
                        </div>
                    </div>

                    <div class="col-2">
                        <button type="submit" class="btn btn-info  t-btn">Ok</button>
                    </div>
                </div>
            </div>--%>

            <section class="content">
                <div class="container-fluid">
                    <div class="row">

                      <div class="col-md-8" style="margin-bottom: 22px;">
       <asp:TextBox ID="txtHearingDate" runat="server" ClientIDMode="Static" CssClass="form-control" Visible="false"></asp:TextBox>

       <asp:Calendar ID="Calendar1" class="calendar" runat="server" OnDayRender="Calendar1_DayRender" OnSelectionChanged="Calendar1_SelectionChanged" BackColor="#f0f8ff" BorderColor="#dee2e6" BorderWidth="1px" DayNameFormat="Shortest" Font-Names="Verdana" Font-Size="14pt" ForeColor="#663399" Height="490px" ShowGridLines="True" Width="100%">
           <DayHeaderStyle BackColor="#FFCC66" Font-Bold="True" Height="1px" />
           <NextPrevStyle Font-Size="9pt" ForeColor="#FFFFCC" CssClass="text-center" Font-Bold="true" />
           <OtherMonthDayStyle ForeColor="#CC9966" />
           <SelectedDayStyle BackColor="#CCCCFF" Font-Bold="True" />
           <SelectorStyle BackColor="#FFCC66" />
           <TitleStyle BackColor="#e9564e" Height="47px" Font-Bold="True" Font-Size="9pt" ForeColor="#FFFFCC"  />
           <TodayDayStyle BackColor="#FFCC66" ForeColor="White" />
       </asp:Calendar>
   </div>

                        <%--<div class="col-md-8">
                            <div class="card card-primary">
                                <div class="card-body p-0">
                                    <div id="calendar"></div>
                                </div>
                            </div>
                        </div>--%>

                        <div class="col-md-4">

                            <div class="card">
                                <div class="card-header scroll">
                                    <h3 class="card-title">Upcoming Hearing</h3>

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
                                        <span class="product-description">  Hearing Date : <a href="javascript:void(0)" class="product-title"> <asp:Label ID="lblhearing" runat="server" Text='<%# Eval("HearingDate", "{0:dd/MM/yyyy}") %>' Font-Bold="true" /> </a> </span>
                                        </div>

                                        <div class="product-info">
                                        <span class="product-description"> Basis of Impound : <a href="javascript:void(0)" class="product-title"> <asp:Label ID="lblComment" runat="server" Text='<%#Eval("ImpoundingReasons_En") %>' Font-Bold="true" /> </a> </span>
                                        </div>
                                        </li>
                                       
                                       </ItemTemplate>
                                      </asp:Repeater>
                                            </div>
                                          </ul>

                                    </div>
                                </asp:Panel>



                                <%-- <div class="card-body p-0 dmpl">
                <ul class="products-list product-list-in-card pl-2 pr-2">
                    <div class="abt-slider">
                  <li class="item">
                    <div class="product-info">
                      <a href="javascript:void(0)" class="product-title"> 11-09-2023 </a>
                      <span class="product-description">
                        Let theme shine like a star
                      </span>
                    </div>
                  </li>
                       
                  <!-- /.item -->
                  <li class="item">
                    <div class="product-info">
                      <a href="javascript:void(0)" class="product-title"> 12-09-2023 </a>
                      <span class="product-description">
                       Let theme shine like a star
                      </span>
                    </div>
                  </li>
                        
                  <!-- /.item -->
                 <li class="item">
                    <div class="product-info">
                      <a href="javascript:void(0)" class="product-title"> 13-09-2023 </a>
                      <span class="product-description">
                       Let theme shine like a star
                      </span>
                    </div>
                  </li>
                  <!-- /.item -->
                <li class="item">
                    <div class="product-info">
                      <a href="javascript:void(0)" class="product-title"> 14-09-2023 </a>
                      <span class="product-description">
                        Let theme shine like a star
                      </span>
                    </div>
                  </li>
                     <li class="item">
                    <div class="product-info">
                      <a href="javascript:void(0)" class="product-title"> 15-09-2023 </a>
                      <span class="product-description">
                        Let theme shine like a star
                      </span>
                    </div>
                  </li>
                     <li class="item">
                    <div class="product-info">
                      <a href="javascript:void(0)" class="product-title"> 16-09-2023 </a>
                      <span class="product-description">
                       Let theme shine like a star
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
                </div>
            </section>

            <div class="row">
                <div class="col-lg-3 col-6">
                    <div class="small-box bg-secondary-1">
                        <div class="inner">
                            <h3>
                                <asp:Label ID="lblNewProposal" runat="server" ></asp:Label></h3>
                            <p>New Proposal</p>
                        </div>
                        <div class="icon">
                            <i class="ion ion-pie-graph"></i>
                        </div>
                        <a href="AcceptRejectCases" class="small-box-footer">More info <i
                            class="fas fa-arrow-circle-right"></i></a>
                    </div>
                </div>

                <div class="col-lg-3 col-6">
                    <!-- small box -->
                    <div class="small-box bg-light-1">
                        <div class="inner">
                            <h3><asp:Label ID="lblTodaysHearingCases" runat="server" ></asp:Label></h3>
                            <p>Todays Hearing Cases</p>
                        </div>
                        <div class="icon">
                            <i class="ion ion-stats-bars"></i>
                        </div>
                        <a href="Cases_In_Hearing" class="small-box-footer">More info <i class="fas fa-arrow-circle-right"></i></a>
                    </div>
                </div>
                <div class="col-lg-3 col-6">
                    <!-- small box -->
                    <div class="small-box bg-secondary-1">
                        <div class="inner">
                            <h3><asp:Label ID="lblpendingcases" runat="server" ></asp:Label></h3>

                            <p>Total Pending Cases</p>
                        </div>
                        <div class="icon">
                            <i class="ion ion-person-add"></i>
                        </div>
                        <a href="Ordersheet_Pending" class="small-box-footer">More info <i class="fas fa-arrow-circle-right"></i></a>
                    </div>
                </div>
                <div class="col-lg-3 col-6">
                    <!-- small box -->
                    <div class="small-box bg-light-1">
                        <div class="inner">
                            <h3><asp:Label ID="lblordered" runat="server" ></asp:Label></h3>
                            <!-- <sup style="font-size: 20px">%</sup> -->
                            <p>Total Ordered Cases</p>
                        </div>
                        <div class="icon">
                            <i class="ion ion-stats-bars"></i>
                        </div>
                        <a href="TotalOrderedCases.aspx" class="small-box-footer">More info <i class="fas fa-arrow-circle-right"></i></a>
                    </div>
                </div>


            </div>

            <div class="row">
                <div class="col-lg-3 col-6">
                   <div class="small-box bg-light-1">
                        <div class="inner">
                            <h3><asp:Label ID="lblFinalOrderCount" runat="server" ></asp:Label></h3>
                            
                            <p>Pending Cases for Final Order</p>
                        </div>
                        <div class="icon">
                            <i class="ion ion-person-add"></i>
                        </div>
                        <a href="FinalOrder_PendingList.aspx" class="small-box-footer">More info <i class="fas fa-arrow-circle-right"></i></a>
                        <%--<a href="#" class="small-box-footer">More info <i class="fas fa-arrow-circle-right"></i></a>--%>
                    </div>
                </div>
               <%-- <div class="col-lg-3 col-6">
                    <!-- small box -->
                    <div class="small-box bg-secondary-1">
                        <div class="inner">
                            <h3>00</h3>
                            <p>User Registrations</p>
                        </div>
                        <div class="icon">
                            <i class="ion ion-person-add"></i>
                        </div>
                        <a href="#" class="small-box-footer">More info <i class="fas fa-arrow-circle-right"></i></a>
                    </div>
                </div>--%>
                  <div class="col-lg-3 col-6">
                    <!-- small box -->
                    <div class="small-box bg-secondary-1">
                        <div class="inner">
                            <h3><asp:Label ID="lblearlyhearing" runat="server" ></asp:Label></h3>
                            <p>Early Hearing Cases/Party Reply</p>
                        </div>
                        <div class="icon">
                            <i class="ion ion-bag"></i>
                        </div>
                        <a href="EarlyHearingListCases.aspx" class="small-box-footer">More info <i class="fas fa-arrow-circle-right"></i></a>
                    </div>
                </div>
                <div class="col-lg-3 col-6">
                    <!-- small box -->
                    <div class="small-box bg-light-1">
                        <div class="inner">
                            <h3><asp:Label ID="lblCasesmovedtorrc" runat="server" ></asp:Label></h3>
                            <p>Cases Moved to RRC</p>
                        </div>
                        <div class="icon">
                            <i class="ion ion-stats-bars"></i>
                        </div>
                        <a href="CasesTo_MoveRRC.aspx" class="small-box-footer">More info <i class="fas fa-arrow-circle-right"></i></a>
                    </div> 
                </div>


                <div class="col-lg-3 col-6">
                    <!-- small box -->
                    <div class="small-box bg-secondary-1">
                        <div class="inner">
                            <h3><asp:Label ID="LblPayment" runat="server" Text="0"></asp:Label></h3>
                            <p>Payment Status</p>
                        </div>
                        <div class="icon">
                            <i class="ion ion-pie-graph"></i>
                        </div>
                        <a href="Payment_List.aspx" class="small-box-footer">More info <i class="fas fa-arrow-circle-right"></i></a>
                    </div>
                </div>
            </div>

            <div class="row">

                <div class="col-lg-3 col-6">

                     <div class="small-box bg-secondary-1">
                        <div class="inner">
                            <h3><asp:Label ID="lblTemplate" runat="server" Text="0" ></asp:Label></h3>
                            <p>Add Template</p>
                        </div>
                        <div class="icon">
                            <i class="ion ion-person-add"></i>
                        </div>
                        <a href="CosTemplate.aspx" class="small-box-footer">More info <i class="fas fa-arrow-circle-right"></i></a>
                    </div>
                    <!-- small box -->
                  <%-- <div class="small-box bg-light-1">
                        <div class="inner">
                            <h3><asp:Label ID="lblAppealCases" runat="server" Text="0" ></asp:Label></h3>
                            <p>Appeal Cases</p>
                        </div>
                        <div class="icon">
                            <i class="ion ion-stats-bars"></i>
                        </div>
                        <a href="#" class="small-box-footer">More info <i class="fas fa-arrow-circle-right"></i></a>
                    </div>--%>
                </div>
                <div class="col-lg-3 col-6">
                    <!-- small box -->
                     <div class="small-box bg-light-1">
                        <div class="inner">
                            <h3><asp:Label ID="lblRRCCertificate" runat="server" Text="0" ></asp:Label></h3>
                            <p>RRC Certificate</p>
                        </div>
                        <div class="icon">
                            <i class="ion ion-stats-bars"></i>
                        </div>
                        <a href="CasesForRRC_Certificate.aspx" class="small-box-footer">More info <i class="fas fa-arrow-circle-right"></i></a>
                    </div>
                </div>
              
                 <div class="col-lg-3 col-6">
                    <!-- small box -->
                    <div class="small-box bg-secondary-1">
                        <div class="inner">
                            <h3>
                                <asp:Label ID="lblClosedCases" runat="server" Text="0"></asp:Label></h3>
                            <p>Closed Cases</p>
                        </div>
                        <div class="icon">
                            <i class="ion ion-stats-bars"></i>
                        </div>
                          <a href="ClosedCases_List" class="small-box-footer">More info <i class="fas fa-arrow-circle-right"></i></a>
                      <%--  <a href="Cos_Generate_FinalOrderList" class="small-box-footer">More info <i class="fas fa-arrow-circle-right"></i></a>--%>
                    </div>
                </div>

                <div class="col-lg-3 col-6">
                    <!-- small box -->
                      <div class="small-box bg-light-1">
                        <div class="inner">
                          <%--  <h3><asp:Label ID="lblFinalOrderSend" runat="server" Text="0" ></asp:Label></h3>--%>
                            <h3><asp:Label ID="lblOffline" runat="server" Text="0" ></asp:Label></h3>
                            <p>Offline Cases</p>
                        </div>
                        <div class="icon">
                            <i class="ion ion-person-add"></i>
                        </div>
                       <%-- <a href="COS_Cases_Pending_ToSend_FinalOrder.aspx" class="small-box-footer">More info <i class="fas fa-arrow-circle-right"></i></a>--%>
                          <a href="CollectorStampLegacyCases.aspx?caseType=Offline" class="small-box-footer">More info <i class="fas fa-arrow-circle-right"></i></a>
                    </div>
                  
                </div>
               

                 <div class="col-lg-3 col-6">
                    <!-- small box -->
                    <div class="small-box bg-light-1">
                        <div class="inner">
                            <h3><asp:Label ID="lblregistered" runat="server" Text="0" ></asp:Label></h3>
                            <p>MIS</p>
                        </div>
                        <div class="icon">
                            <i class="ion ion-bag"></i>
                        </div>
                        <a href="MIS_Report.aspx" class="small-box-footer">More info <i class="fas fa-arrow-circle-right"></i></a>
                    </div>
                </div>

                <div class="col-lg-3 col-6">
                    <!-- small box -->
                    <div class="small-box bg-secondary-1">
                        <div class="inner">
                            <h3><asp:Label ID="lbLegacyCases" runat="server" Text="0" ></asp:Label></h3>
                            <p>Legacy Cases</p>
                        </div>
                        <div class="icon">
                            <i class="ion ion-bag"></i>
                        </div>
                        <a href="CollectorStampLegacyCases.aspx?caseType=Legacy" class="small-box-footer">More info <i class="fas fa-arrow-circle-right"></i></a>
                    </div>
                </div>

                <div class="col-lg-3 col-6">
                    <!-- small box -->
                    <div class="small-box bg-secondary-1">
                        <div class="inner">
                            <h3><asp:Label ID="Label1" runat="server" Text="0" ></asp:Label></h3>
                            <p>Refuse</p>
                        </div>
                        <div class="icon">
                            <i class="ion ion-bag"></i>
                        </div>
                        <a href="#" class="small-box-footer">More info <i class="fas fa-arrow-circle-right"></i></a>
                    </div>
                </div>

            </div>

        </div>
        <div class="callout callout-danger">
            <h5><strong>Note -</strong> The data display on dashboard is from 01-April-2024</h5>
        </div>
    </section>





</asp:Content>


