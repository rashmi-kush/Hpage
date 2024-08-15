<%@ Page Title="" Language="C#" MasterPageFile="~/CoS/CoSMaster.Master" AutoEventWireup="true" CodeBehind="AcceptRejectCases_details.aspx.cs" Inherits="CMS_Sampada.CoS.AcceptRejectCases_details" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <section class="content-header">
        <div class="container-fluid">
            <div class="row">
                <div class="col-sm-6">
                    <h5>Proposal No - IGRSCMS1000108 </h5>
                </div>
                <div class="col-sm-6">
                    <ol class="breadcrumb float-sm-right">
                        <li class="breadcrumb-item"><a href="#">Home</a></li>
                        <li class="breadcrumb-item active">proposal List</li>
                    </ol>
                </div>
            </div>
        </div>
    </section>

    <section class="content">
        <div class="container-fluid">
            <div class="row">

                <div class="col-md-12">
                    <div class="card">
                        <div class="card-header p-2">
                            <ul class="nav nav-pills">
                                <li class="nav-item"><a class="nav-link disabled" href="#CreateOrderSheet" data-toggle="tab">Create Order Sheet</a></li>
                                <li class="nav-item"><a class="nav-link disabled" href="#Notice" data-toggle="tab">Notice</a></li>
                                <li class="nav-item"><a class="nav-link disabled" href="#SeekReport" data-toggle="tab">Seek
                        Report</a></li>
                                <li class="nav-item"><a class="nav-link disabled" href="#EarlyHearing" data-toggle="tab">Early
                        Hearing</a>
                                </li>
                                <li class="nav-item"><a class="nav-link disabled" href="#SendToAppeal" data-toggle="tab">Send to
                        Appeal</a>
                                </li>
                                <li class="nav-item"><a class="nav-link active" href="#SendBack" data-toggle="tab">Send Back</a>
                                </li>
                                <li class="nav-item"><a class="nav-link disabled" href="#Reply" data-toggle="tab">Reply</a></li>
                                <li class="nav-item"><a class="nav-link disabled" href="#Attachement"
                                    data-toggle="tab">Attachement</a></li>
                            </ul>
                        </div>
                        <div class="card-body">
                            <div class="tab-content">

                                <div class="tab-pane active" id="SendBack">
                                    <div class="row">
                                        <div class="col-12 col-sm-6">
                                            <div class="card card-primary card-tabs">
                                                <div class="card-header p-0 pt-1">
                                                    <ul class="nav nav-tabs" id="custom-tabs-one-tab" role="tablist">
                                                        <%--                              <li class="nav-item">
                                  <a class="nav-link disabled" id="custom-tabs-one-home-tab" data-toggle="pill"
                                    href="#custom-tabs-one-ProposalForm" role="tab" aria-controls="custom-tabs-one-home"
                                    aria-selected="false">Proposal Form</a>
                                </li>--%>
                                                        <li class="nav-item">
                                                            <a class="nav-link active" id="custom-tabs-one-profile-tab" data-toggle="pill"
                                                                href="#custom-tabs-one-RegisteredForm" role="tab"
                                                                aria-controls="custom-tabs-one-profile" aria-selected="true">Proposal Form </a>
                                                        </li>
                                                    </ul>
                                                </div>
                                                <div class="card-body">
                                                    <div class="tab-content" id="custom-tabs-one-tabContent">

                                                        <div class="tab-pane active show" id="custom-tabs-one-RegisteredForm" role="tabpanel"
                                                            aria-labelledby="custom-tabs-one-profile-tab">
                                                            <!-- table -->
                                                            <h5>Party Details </h5>
                                                            
                                                            <%--<table id="example11" class="table table-bordered table-striped">
                                    <thead>
                                      <tr>
                                        <th>SNo.</th>
                                        <th>Party Type</th>
                                        <th>Party Name</th>
                                        <th>Gender</th>
                                        <th>Owner/ Applicant type</th>
                                        <th>Email ID</th>
                                        <th>Mobile No</th>
                                        <th>Action</th>
                                      </tr>
                                    </thead>
                                    <tbody>
                                      <tr>
                                        <td>1</td>
                                        <td>454544 </td>
                                        <td>gdf</td>
                                        <td>kjhjh</td>
                                        <td> xyz </td>
                                        <td>454544 </td>
                                        <td>dfsdf</td>
                                        <td> <button type="button" class="btn t-btn" id="viewbtn">View</button> </td>
                                      </tr>
                                    </tbody>
                                  </table>--%>

                                                            <asp:GridView ID="grdPartyDetails" CssClass="table table-bordered table-condensed table-striped table-hover"
                                                                DataKeyNames="Party_ID"
                                                                runat="server" CellPadding="4" ForeColor="#333333" GridLines="None" AutoGenerateColumns="false" Visible="true" EmptyDataText="No record found">
                                                                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="S No." HeaderStyle-VerticalAlign="Top" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="false">
                                                                        <ItemTemplate>
                                                                            <itemtemplate><%#Container.DataItemIndex+1  %> </itemtemplate>
                                                                        </ItemTemplate>
                                                                        <ItemStyle Width="5%" HorizontalAlign="Center" />
                                                                        <HeaderStyle Width="5%" HorizontalAlign="Center" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Party Type">
                                                                        <ItemTemplate>
                                                                            <%#Eval("PARTY_TYPE_NAME_EN") %>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle Width="10%" HorizontalAlign="Left" />
                                                                        <ItemStyle Width="10%" HorizontalAlign="Left" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Party Name">
                                                                        <ItemTemplate>
                                                                            <%#Eval("Party_Name") %>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle Width="10%" HorizontalAlign="Left" />
                                                                        <ItemStyle Width="10%" HorizontalAlign="Left" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Father Name">
                                                                        <ItemTemplate>
                                                                            <%#Eval("PartyFather_Name") %>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle Width="10%" HorizontalAlign="Left" />
                                                                        <ItemStyle Width="10%" HorizontalAlign="Left" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Email ID">
                                                                        <ItemTemplate>
                                                                            <%#Eval("Email_Id") %>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle Width="10%" HorizontalAlign="Center" />
                                                                        <ItemStyle Width="10%" HorizontalAlign="Left" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Mobile">
                                                                        <ItemTemplate>
                                                                            <%#Eval("Mob_No") %>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle Width="10%" HorizontalAlign="Center" />
                                                                        <ItemStyle Width="10%" HorizontalAlign="Left" />
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="Whats App Number" Visible="false">
                                                                        <ItemTemplate>
                                                                            <%#Eval("Whatsapp_No") %>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle Width="10%" HorizontalAlign="Center" />
                                                                        <ItemStyle Width="10%" HorizontalAlign="Left" />
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="Action">
                                                                        <ItemStyle Width="4%" HorizontalAlign="Center" />
                                                                        <ItemTemplate>
                                                                            <asp:LinkButton ID="lnkPartyView" runat="server" OnClick="lnkPartyView_Click" CssClass="btn btn-secondary">View</asp:LinkButton>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>

                                                                </Columns>
                                                                <EditRowStyle BackColor="#999999" />
                                                                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                                                <HeaderStyle BackColor="#0090a2" Font-Bold="True" ForeColor="White" />
                                                                <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                                                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                                                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                                                <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                                                <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                                                <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                                                <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                                                            </asp:GridView>



                                                            <br>

                                                            <asp:Panel ID="pnlPartyDetails" runat="server" Visible="false">




                                                                <div class="col-md-6 col-lg-12">
                                                                    <div class="card card-outline card-success" id="detail_card">
                                                                        <div class="card-tools">
                                                                            <button type="button" class="btn btn-tool" data-card-widget="remove">
                                                                                <i
                                                                                    class="fas fa-times"></i>
                                                                            </button>
                                                                        </div>
                                                                        <div class="card-body">
                                                                            <div class="row">
                                                                                <div class="col-sm-4 border-right">
                                                                                    <div class="description-block">
                                                                                        <h5 class="description-header">Transaction through PAO</h5>
                                                                                        <span class="description-text">Power of Attorney Holder </span>
                                                                                    </div>
                                                                                </div>
                                                                                <div class="col-sm-4 border-right">
                                                                                    <div class="description-block">
                                                                                        <h5 class="description-header">Father Name </h5>
                                                                                        <%--<span class="description-text">Aman Rathore</span>--%>
                                                                                        <asp:TextBox ID="txtPartyFName" required="required" runat="server" class="form-control" ReadOnly="true"></asp:TextBox>
                                                                                    </div>
                                                                                </div>
                                                                                <div class="col-sm-4">
                                                                                    <div class="description-block">
                                                                                        <h5 class="description-header">Whatsapp No</h5>
                                                                                        <span class="description-text">8985476254</span>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                            <hr>
                                                                            <div class="row">
                                                                                <div class="col-sm-4 border-right">
                                                                                    <div class="description-block">
                                                                                        <h5 class="description-header">Address</h5>
                                                                                        <asp:TextBox ID="txtPartyAddress" required="required" runat="server" class="form-control" ReadOnly="true"></asp:TextBox>
                                                                                    </div>
                                                                                </div>
                                                                                <div class="col-sm-4 border-right">
                                                                                    <div class="description-block">
                                                                                        <h5 class="description-header">District </h5>
                                                                                        <asp:TextBox ID="txtPartyDist" required="required" runat="server" class="form-control" ReadOnly="true"></asp:TextBox>
                                                                                    </div>
                                                                                </div>
                                                                                <div class="col-sm-4">
                                                                                    <div class="description-block">
                                                                                        <h5 class="description-header">Owner Name </h5>
                                                                                        <span class="description-text">Power of Attorney Holder </span>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                            <hr>
                                                                            <div class="row">
                                                                                <div class="col-sm-4 border-right">
                                                                                    <div class="description-block">
                                                                                        <h5 class="description-header">Owner Mobile no</h5>
                                                                                        <span class="description-text">9865231457</span>
                                                                                    </div>
                                                                                </div>
                                                                                <div class="col-sm-4 border-right">
                                                                                    <div class="description-block">
                                                                                        <h5 class="description-header">Owner whatsapp no</h5>
                                                                                        <span class="description-text">9658714258</span>
                                                                                    </div>
                                                                                </div>
                                                                                <div class="col-sm-4">
                                                                                    <div class="description-block">
                                                                                        <h5 class="description-header">Owner Email ID</h5>
                                                                                        <span class="description-text">xyz@gmail.com</span>
                                                                                    </div>
                                                                                </div>
                                                                            </div>

                                                                        </div>
                                                                    </div>
                                                                </div>



                                                            </asp:Panel>
                                                            <hr>

                                                            <h5>Basis of Impound /Referral</h5>
                                                            <div class="row">
                                                                <div class="col-12">
                                                                    <div class="card">
                                                                        <div class="card-body">
                                                                            <%--Unduly Stamped--%>
                                                                            <asp:Label ID="lblReasonImpound" runat="server" Text=""></asp:Label>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>

                                                            <hr>
                                                            <h5>Property Details </h5>
                                                            <asp:GridView ID="GVPropertyDetail" CssClass="table table-bordered table-condensed table-striped table-hover"
                                                                DataKeyNames="Property_ID"
                                                                runat="server" CellPadding="4" ForeColor="#333333" GridLines="None" AutoGenerateColumns="false" Visible="true" EmptyDataText="No record found">
                                                                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="S No." HeaderStyle-VerticalAlign="Top" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="false">
                                                                        <ItemTemplate>
                                                                            <itemtemplate><%#Container.DataItemIndex+1  %> </itemtemplate>
                                                                        </ItemTemplate>
                                                                        <ItemStyle Width="5%" HorizontalAlign="Center" />
                                                                        <HeaderStyle Width="5%" HorizontalAlign="Center" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Property Id">
                                                                        <ItemTemplate>
                                                                            <%#Eval("Property_Given_ID") %>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle Width="10%" HorizontalAlign="Left" />
                                                                        <ItemStyle Width="10%" HorizontalAlign="Left" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Transacting Partial Area of Proporty">
                                                                        <ItemTemplate>
                                                                            <%#Eval("Transacting_Partial_AreaOfProperty") %>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle Width="10%" HorizontalAlign="Left" />
                                                                        <ItemStyle Width="10%" HorizontalAlign="Left" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Pre Registration Id">
                                                                        <ItemTemplate>
                                                                            <%#Eval("Pre_RegistrationID") %>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle Width="10%" HorizontalAlign="Left" />
                                                                        <ItemStyle Width="10%" HorizontalAlign="Left" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Property Type">
                                                                        <ItemTemplate>
                                                                            <%#Eval("Property_Type") %>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle Width="10%" HorizontalAlign="Center" />
                                                                        <ItemStyle Width="10%" HorizontalAlign="Left" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Property Sub Type">
                                                                        <ItemTemplate>
                                                                            <%#Eval("Property_SubType") %>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle Width="10%" HorizontalAlign="Center" />
                                                                        <ItemStyle Width="10%" HorizontalAlign="Left" />
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="Total Property Area">
                                                                        <ItemTemplate>
                                                                            <%#Eval("Property_Area") %>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle Width="10%" HorizontalAlign="Center" />
                                                                        <ItemStyle Width="10%" HorizontalAlign="Left" />
                                                                    </asp:TemplateField>


                                                                    <asp:TemplateField HeaderText="Property Transacting Area">
                                                                        <ItemTemplate>
                                                                            <%#Eval("Property_TransactingArea") %>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle Width="10%" HorizontalAlign="Center" />
                                                                        <ItemStyle Width="10%" HorizontalAlign="Left" />
                                                                    </asp:TemplateField>


                                                                    <%--<asp:TemplateField HeaderText="Action">
                                                                    <ItemStyle Width="4%" HorizontalAlign="Center" />
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="lnkPropertyView" runat="server" OnClick="lnkPropertyView_Click" CssClass="btn btn-secondary">View</asp:LinkButton>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>--%>
                                                                </Columns>
                                                                <EditRowStyle BackColor="#999999" />
                                                                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                                                <HeaderStyle BackColor="#0090a2" Font-Bold="True" ForeColor="White" />
                                                                <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                                                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                                                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                                                <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                                                <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                                                <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                                                <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                                                            </asp:GridView>
                                                            <br>

                                                            <div class="row">
                                                                <div class="col-12">
                                                                    <div class="card">
                                                                        <div class="card-body">
                                                                            <form>
                                                                                <div class="col-12" style="display: flex;">
                                                                                    <div class="col-4 form-group">
                                                                                        <label for="inputEstimatedBudget">Property Address</label>
                                                                                        <input type="number" id="inputEstimatedBudget" class="form-control">
                                                                                    </div>
                                                                                    <div class="col-4 form-group">
                                                                                        <label for="inputEstimatedBudget">Land Diversion</label>
                                                                                        <input type="number" id="inputEstimatedBudget" class="form-control">
                                                                                    </div>
                                                                                    <div class="col-4 form-group">
                                                                                        <label for="inputEstimatedBudget">Property Type</label>
                                                                                        <input type="number" id="inputEstimatedBudget" class="form-control">
                                                                                    </div>
                                                                                </div>

                                                                                <div class="col-12" style="display: flex;">
                                                                                    <div class="col-4 form-group">
                                                                                        <label for="inputEstimatedBudget">Ward Colony/Village colony</label>
                                                                                        <input type="number" id="inputEstimatedBudget" class="form-control">
                                                                                    </div>
                                                                                    <div class="col-4 form-group">
                                                                                        <label for="inputEstimatedBudget">Property Image</label>
                                                                                        <input type="number" id="inputEstimatedBudget" class="form-control">
                                                                                    </div>
                                                                                    <div class="col-4 form-group">
                                                                                        <label for="inputEstimatedBudget">Map of Property</label>
                                                                                        <input type="number" id="inputEstimatedBudget" class="form-control">
                                                                                    </div>
                                                                                </div>

                                                                            </form>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <hr>
                                                            <h5>Proposal Details </h5>
                                                            <div class="table-responsive">

                                                                <table id="example11" class="table table-bordered table-striped">
                                                                    <thead>
                                                                        <tr>
                                                                            <%--<th>Document Registration Number/Intitation ID</th>--%>
                                                                            <asp:Label ID="lblRegInitEStampID" Text="" runat="server"></asp:Label>

                                                                            <asp:Panel ID="pnlRegInitDate" runat="server">
                                                                                <th>
                                                                                    <asp:Label ID="lblRegInitDate" Text="" runat="server"></asp:Label>

                                                                                    <%--Date of Registry/ Date of Intitation--%>

                                                                                </th>
                                                                            </asp:Panel>


                                                                            <th>Date of Execution</th>
                                                                            <th>Date of Presentation</th>
                                                                        </tr>
                                                                    </thead>
                                                                    <tbody>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:Label ID="lblPropertyRegNoInitIdEStampId" Text="" runat="server"></asp:Label></td>
                                                                            <asp:Panel ID="pnlRegNoInitDate" runat="server">
                                                                                <td>
                                                                                    <asp:Label ID="lblProRegDt" Text="" runat="server"></asp:Label></td>
                                                                            </asp:Panel>
                                                                            <td>
                                                                                <asp:Label ID="lblDateofExecution" Text="" runat="server"></asp:Label></td>
                                                                            <td>
                                                                                <asp:Label ID="lblDateofPresent" Text="" runat="server"></asp:Label></td>
                                                                        </tr>
                                                                    </tbody>
                                                                </table>
                                                           
                                                                </div>
                                                            <br>

                                                            <div class="table-responsive">
                                                                <%-- <table id="example11" class="table table-bordered table-striped">
                                                                    <thead>
                                                                        <tr>
                                                                            <th colspan="2">Details of document as presented by party</th>
                                                                            <th colspan="2">Proposal of registring officer</th>
                                                                            <th>Deficit Duty / Variation Amount (INR)</th>
                                                                            <th>Penalty Amount (INR)</th>
                                                                            <th>Remark</th>
                                                                        </tr>
                                                                        <tr class="sub-tr-bc">
                                                                            <th>Description</th>
                                                                            <th>Amount (INR)</th>
                                                                            <th>Description</th>
                                                                            <th>Amount (INR)</th>
                                                                            <th></th>
                                                                            <th></th>
                                                                            <th></th>
                                                                        </tr>
                                                                    </thead>
                                                                    <tbody>
                                                                        <tr>
                                                                            <td>-</td>
                                                                            <td>- </td>
                                                                            <td>- </td>
                                                                            <td>-</td>
                                                                            <td>- </td>
                                                                            <td>-</td>
                                                                            <td>- </td>
                                                                        </tr>
                                                                    </tbody>
                                                                </table>--%>


                                                                <table id="tbl1" class="table table-hover gid-view-tab table-bordered ">
                                                                    <thead>
                                                                        <tr>
                                                                            <th style="width: 200px">Particulars</th>
                                                                            <th>Details of Document
                                                        <br />
                                                                                as Presented by Party</th>
                                                                            <th>Proposal of
                                                        <br />
                                                                                Registering Officer</th>
                                                                            <th>Deficit Duty
                                                        <br />
                                                                                / Variation</th>
                                                                            <th>TBD</th>
                                                                            <th>Remark</th>

                                                                        </tr>
                                                                    </thead>
                                                                    <tbody>

                                                                        <tr>
                                                                            <td>Nature of Document</td>
                                                                            <td>
                                                                                <asp:Label ID="lblNatureDoc" Text="" runat="server"></asp:Label></td>
                                                                            <td>
                                                                                <asp:PlaceHolder ID="PlaceHolder1" runat="server"></asp:PlaceHolder>

                                                                                <asp:Label ID="lblNatureDocRegOff" Text="" runat="server"></asp:Label></td>
                                                                            <td>
                                                                                <asp:Label ID="lblNatureDocDeficit" Text="" runat="server"></asp:Label></td>
                                                                            <td>
                                                                                <asp:Label ID="lblNatureDocPenality" Text="" runat="server"></asp:Label></td>
                                                                            <td>
                                                                                <asp:Label ID="lblNatureDocRemark" Text="" runat="server"></asp:Label></td>
                                                                        </tr>

                                                                        <%--<tr>
                                                    <td> <asp:Label ID="lblYear" Text="" Font-Bold="true" runat="server"></asp:Label></td>
                                                    <td> <asp:Label ID="lblMonths" Text="" Font-Bold="true" runat="server"></asp:Label></td>
                                                    <td> <asp:Label ID="lblDays" Text="" Font-Bold="true" runat="server"></asp:Label></td>
                                                    <td> <asp:Label ID="lblTDays" Text="" Font-Bold="true" runat="server"></asp:Label></td>
                                                    <td></td>
                                                </tr>--%>

                                                                        <tr>
                                                                            <td>Consideration Value</td>
                                                                            <td>₹
                                                        <asp:Label ID="lblConsidProperty" Text="" runat="server"></asp:Label></td>
                                                                            <td>₹
                                                        <asp:Label ID="lblConsidPropertyRegOff" Text="" runat="server"></asp:Label></td>
                                                                            <td>
                                                                                <asp:Label ID="lblConsidPropertyDeficit" Text="" runat="server"></asp:Label></td>
                                                                            <td>
                                                                                <asp:Label ID="lblConsidPropertyPenality" Text="" runat="server"></asp:Label></td>
                                                                            <td>
                                                                                <asp:Label ID="lblConsidPropertyRemark" Text="" runat="server"></asp:Label></td>

                                                                        </tr>
                                                                        <tr>
                                                                            <td>Guideline Value of Property</td>
                                                                            <td>₹ 
                                                        <asp:Label ID="lblGuideValue" Text="" runat="server"></asp:Label></td>
                                                                            <td>₹
                                                        <asp:Label ID="lblGuideValueRegOff" Text="" runat="server"></asp:Label></td>
                                                                            <td>₹
                                                        <asp:Label ID="lblGuideValueRegDefcit" Text="" runat="server"></asp:Label></td>
                                                                            <td>₹
                                                        <asp:Label ID="lblGuideValuePenality" Text="" runat="server"></asp:Label></td>
                                                                            <td>
                                                                                <asp:Label ID="lblGuideValueRemark" Text="" runat="server"></asp:Label></td>
                                                                        </tr>
                                                                        <%-- <tr>
                                                    <td>Market value of Property:
                                                        <asp:Label ID="lblProValue" Text="" Font-Bold="true" runat="server"></asp:Label></td>
                                                    <td>Proposed Property Value:
                                                        <asp:Label ID="lblProposPropValu" Text="" Font-Bold="true" runat="server"></asp:Label></td>
                                                    <td>
                                                        <asp:Label ID="lblProposPropValuDefcit" Text="" Font-Bold="true" runat="server"></asp:Label></td>
                                                    <td></td>
                                                    <td>Hike in prices</td>

                                                </tr>--%>



                                                                        <tr>
                                                                            <td class="forset">Stamp Duty Bifurcation:
                                                         <br />
                                                                                <br />
                                                                                <p>
                                                                                    Janpad
                                                                                </p>
                                                                                <br />
                                                                                <p>
                                                                                    Upkar
                                                                                </p>
                                                                                <br />
                                                                                <p>
                                                                                    Muncipal
                                                                                </p>
                                                                                <br />
                                                                                <p>
                                                                                    Principle
                                                                                </p>
                                                                                <br />
                                                                                <p>
                                                                                    Exempted Amount
                                                                                </p>
                                                                            </td>
                                                                            <td class="forset">
                                                                                <br />
                                                                                <br />

                                                                                <p>
                                                                                    ₹
                                                            <asp:Label ID="lblStampDutyClassJanpad" Text="" runat="server"></asp:Label>
                                                                                </p>
                                                                                <br />
                                                                                <p>
                                                                                    ₹
                                                            <asp:Label ID="lblStampDutyClassUpkar" Text="" runat="server"></asp:Label>
                                                                                </p>
                                                                                <br />
                                                                                <p>
                                                                                    ₹
                                                            <asp:Label ID="lblStampDutyClassMuncipal" Text="" runat="server"></asp:Label>
                                                                                </p>
                                                                                <br />
                                                                                <p>
                                                                                    ₹
                                                            <asp:Label ID="lblStampDutyClassPrinciple" Text="" runat="server"></asp:Label>
                                                                                </p>
                                                                                <br />
                                                                                <p>
                                                                                    ₹
                                                            <asp:Label ID="lblStampDutyExemptedAmt" Text="" runat="server"></asp:Label>
                                                                                </p>
                                                                            </td>
                                                                            <td class="forset">
                                                                                <br />
                                                                                <br />
                                                                                <p>
                                                                                    ₹
                                                            <asp:Label ID="lblProClassJanpad" Text="" runat="server"></asp:Label>
                                                                                </p>
                                                                                <br />
                                                                                <p>
                                                                                    ₹
                                                            <asp:Label ID="lblProClassUpkar" Text="" runat="server"></asp:Label>
                                                                                </p>
                                                                                <br />
                                                                                <p>
                                                                                    ₹
                                                            <asp:Label ID="lblProClassMuncipal" Text="" runat="server"></asp:Label>
                                                                                </p>
                                                                                <br />
                                                                                <p>
                                                                                    ₹
                                                            <asp:Label ID="lblProClassPrinciple" Text="" runat="server"></asp:Label>
                                                                                </p>
                                                                                <br />
                                                                                <p>
                                                                                    ₹
                                                            <asp:Label ID="lblProExemptedAmt" Text="" runat="server"></asp:Label>
                                                                                </p>
                                                                            </td>
                                                                            <td class="forset">
                                                                                <br />
                                                                                <br />
                                                                                <p>

                                                                                    <asp:Label ID="lblDeficitJanpad" Text="" Visible="false" runat="server"></asp:Label>
                                                                                </p>
                                                                                <br />
                                                                                <p>

                                                                                    <asp:Label ID="lblDeficitUpkar" Text="" Visible="false" runat="server"></asp:Label>
                                                                                </p>
                                                                                <br />
                                                                                <p>

                                                                                    <asp:Label ID="lblDeficitMuncipal" Text="" Visible="false" runat="server"></asp:Label>
                                                                                </p>
                                                                                <br />
                                                                                <p>

                                                                                    <asp:Label ID="lblDeficitPrinciple" Text="" Visible="false" runat="server"></asp:Label>
                                                                                </p>
                                                                            </td>
                                                                            <td class="forset">
                                                                                <br />
                                                                                <br />
                                                                                <p>

                                                                                    <asp:Label ID="lblPenalityJanpad" Text="" Visible="false" runat="server"></asp:Label>
                                                                                </p>
                                                                                <br />
                                                                                <p>

                                                                                    <asp:Label ID="lblPenalityUpkar" Text="" Visible="false" runat="server"></asp:Label>
                                                                                </p>
                                                                                <br />
                                                                                <p>

                                                                                    <asp:Label ID="lblPenalityMuncipal" Text="" Visible="false" runat="server"></asp:Label>
                                                                                </p>
                                                                                <br />
                                                                                <p>

                                                                                    <asp:Label ID="lblPenalityPrinciple" Text="" Visible="false" runat="server"></asp:Label>
                                                                                </p>
                                                                            </td>
                                                                            <td></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>Net Stamp Duty</td>
                                                                            <td>₹
                                                        <asp:Label ID="lblStamDuty" Text="" runat="server"></asp:Label></td>
                                                                            <td>₹
                                                        <asp:Label ID="lblProRecStmapDuty" Text="" runat="server"></asp:Label></td>
                                                                            <td>₹
                                                        <asp:Label ID="lblDeficitDuty" Text="" runat="server"></asp:Label></td>
                                                                            <td>₹
                                                        <asp:Label ID="lblStampDutyPenality" Text="" runat="server"></asp:Label></td>
                                                                            <td>
                                                                                <asp:Label ID="lblStampDutyRemark" Text="Not enough duty paid" runat="server"></asp:Label></td>

                                                                        </tr>

                                                                        <tr>
                                                                            <td class="forset">Registration Fee Bifurcation:
                                                         <br />
                                                                                <br />
                                                                                <p>
                                                                                    Registration Fee
                                                                                </p>
                                                                                <br />
                                                                                <p>
                                                                                    Exempted Amount
                                                                                </p>
                                                                            </td>
                                                                            <td class="forset">
                                                                                <br />
                                                                                <br />

                                                                                <p>
                                                                                    ₹
                                                            <asp:Label ID="lblRegiFee" Text="" runat="server"></asp:Label>
                                                                                </p>
                                                                                <br />
                                                                                <p>
                                                                                    ₹
                                                            <asp:Label ID="lblRegiExemptedAmt" Text="" runat="server"></asp:Label>
                                                                                </p>

                                                                            </td>
                                                                            <td class="forset">
                                                                                <br />
                                                                                <br />
                                                                                <p>
                                                                                    ₹
                                                            <asp:Label ID="lblProRegiFee" Text="" runat="server"></asp:Label>
                                                                                </p>
                                                                                <br />
                                                                                <p>
                                                                                    ₹
                                                            <asp:Label ID="lblProRegiExemptedAmt" Text="" runat="server"></asp:Label>
                                                                                </p>
                                                                            </td>
                                                                            <td class="forset">
                                                                                <br />
                                                                                <br />
                                                                                <p>

                                                                                    <asp:Label ID="Label12" Text="" runat="server"></asp:Label>
                                                                                </p>
                                                                                <br />
                                                                                <p>

                                                                                    <asp:Label ID="Label13" Text="" runat="server"></asp:Label>
                                                                                </p>


                                                                            </td>
                                                                            <td class="forset">
                                                                                <br />
                                                                                <br />
                                                                                <p>

                                                                                    <asp:Label ID="Label16" Text="" runat="server"></asp:Label>
                                                                                </p>
                                                                                <br />
                                                                                <p>

                                                                                    <asp:Label ID="Label17" Text="" runat="server"></asp:Label>
                                                                                </p>

                                                                            </td>
                                                                            <td></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>Net Registration Fees</td>
                                                                            <td>₹ 
                                                        <asp:Label ID="lblRegFee" Text="" runat="server"></asp:Label></td>
                                                                            <td>₹
                                                        <asp:Label ID="lblRecoverRegfee" Text="" runat="server"></asp:Label></td>
                                                                            <td>₹
                                                        <asp:Label ID="lblDeficitRegFee" Text="" runat="server"></asp:Label></td>
                                                                            <td>₹
                                                        <asp:Label ID="lblRegFeePenality" Text="" runat="server"></asp:Label></td>
                                                                            <td>
                                                                                <asp:Label ID="lblRegFeeRemark" Text="" runat="server"></asp:Label></td>
                                                                        </tr>


                                                                        <%-- <tr>
                                                    <td><b>TOTAL</b></td>
                                                    <td>₹
                                                        <asp:Label ID="totalPaidbyParty" Text="" Font-Bold="true" runat="server"></asp:Label></td>
                                                    <td>₹
                                                        <asp:Label ID="totalProposedbyRegOff" Text="" Font-Bold="true" runat="server"></asp:Label></td>
                                                    <td>₹
                                                        <asp:Label ID="totalDeficit" Text="" Font-Bold="true" runat="server"></asp:Label></td>
                                                    <td>₹
                                                        <asp:Label ID="totalPenality" Text="" Font-Bold="true" runat="server"></asp:Label></td>
                                                    <td>
                                                        <asp:Label ID="Label5" Text="" Font-Bold="true" runat="server"></asp:Label></td>
                                                </tr>--%>
                                                                    </tbody>
                                                                </table>





                                                            </div>
                                                            <hr>
                                                            <%--<h5>Sub Registrar Details </h5>--%>
                                                            <button class="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#collapseTwo" aria-expanded="false" aria-controls="collapseTwo">
                                                                <asp:Label ID="lblHeading" runat="server"></asp:Label>
                                                            </button>

                                                            <div class="table-responsive">
                                                                <%-- <table id="example11" class="table table-bordered table-striped">
                                                                    <thead>
                                                                        <tr>
                                                                            <th>SRO ID</th>
                                                                            <th>Sub Registrar Name</th>
                                                                            <th>SRO Name</th>
                                                                            <th>Proposal ID</th>
                                                                            <th>Date of Impound</th>
                                                                            <th>Head</th>
                                                                            <th>Section</th>
                                                                        </tr>
                                                                    </thead>
                                                                    <tbody>
                                                                        <tr>
                                                                            <td>SRO101</td>
                                                                            <td>Sub-Registrar </td>
                                                                            <td>Sub-Registrar Office Bhopal Zone-I </td>
                                                                            <td>IGRSCMS1000116</td>
                                                                            <td>15/03/2022 </td>
                                                                            <td>
                                                                                <select class="form-control">
                                                                                    <option>B102</option>
                                                                                    <option>B104</option>
                                                                                </select>
                                                                            </td>
                                                                            <td>
                                                                                <select class="form-control">
                                                                                    <option>32</option>
                                                                                    <option>33</option>
                                                                                </select>
                                                                            </td>
                                                                        </tr>
                                                                    </tbody>
                                                                </table>--%>



                                                                <table id="tbl2" class="table table-hover gid-view-tab table-bordered ">
                                                                    <thead>
                                                                        <tr>
                                                                            <th>SRO Id</th>
                                                                            <th>Sub Registrar Name</th>
                                                                            <th>SRO Name</th>
                                                                            <th>Proposal ID</th>
                                                                            <th>Date of Impound</th>
                                                                            <%--<th>
                                                    <asp:Label ID="lblDepName" runat="server"></asp:Label>
                                                    
                                                </th>--%>

                                                                            <asp:Panel ID="pnlAuditIdDate" runat="server" Visible="false">
                                                                                <th>Audit Id</th>
                                                                                <th>Audit Date</th>
                                                                            </asp:Panel>
                                                                            <th>Head</th>
                                                                            <th>Section</th>

                                                                        </tr>
                                                                    </thead>
                                                                    <tbody>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:Label ID="lblSROID" Text="" runat="server"></asp:Label></td>
                                                                            <td>
                                                                                <asp:Label ID="lblSRName" Text="" runat="server"></asp:Label></td>
                                                                            <td>
                                                                                <asp:Label ID="lblSROName" Text="" runat="server"></asp:Label></td>
                                                                            <td>
                                                                                <asp:Label ID="lblProposalId" Text="" runat="server"></asp:Label></td>
                                                                            <td>
                                                                                <asp:Label ID="lblProposalDate" Text="" runat="server"></asp:Label></td>

                                                                            <asp:Panel ID="pnlAuditIdandDate" runat="server" Visible="false">
                                                                                <td>
                                                                                    <asp:Label ID="lblAuditId" Text="" runat="server"></asp:Label></td>
                                                                                <td>
                                                                                    <asp:Label ID="lblAuditDt" Text="" runat="server"></asp:Label></td>
                                                                            </asp:Panel>
                                                                            <td>
                                                                                <asp:Label ID="lblHeadbySR" Text="" runat="server"></asp:Label>
                                                                                <%-- <asp:DropDownList ID="ddlHead1" ClientIDMode="Static" Width="80px" runat="server" CssClass="form-control" ></asp:DropDownList>
                                                                                --%>
                                                                                <%--<ajaxToolkit:CascadingDropDown ID="CasDistrict" runat="server" TargetControlID="ddlHead1" Category="district" ServicePath="~/Districthirarcy.asmx" LoadingText="[Loading State...]" ServiceMethod="BindStateDetails" />
                                                                                --%>
                                                                            </td>
                                                                            <td>
                                                                                <asp:Label ID="lblSecbySR" Text="" runat="server"></asp:Label>
                                                                                <%--<asp:DropDownList ID="ddlSec1" Width="80px" runat="server" CssClass="form-control" ></asp:DropDownList>--%>
                                                                                <%--<ajaxToolkit:CascadingDropDown ID="CasSubDivision" runat="server" TargetControlID="ddlSec1" ServicePath="~/Districthirarcy.asmx" ParentControlID="ddlHead1" PromptText="Select Section" ServiceMethod="BindDistrictDetailsOnstateID" Category="subdivision" Enabled="True" />
                                                                                --%>     </td>
                                                                        </tr>


                                                                    </tbody>
                                                                </table>


                                                                <table id="tbl3" class="table table-hover gid-view-tab table-bordered ">
                                                                    <thead>
                                                                        <tr>
                                                                            <th>
                                                                                <asp:Label ID="lblOfficeName" runat="server"></asp:Label></th>

                                                                        </tr>
                                                                    </thead>
                                                                    <tbody>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:Label ID="lblSRComments" Text="" runat="server"></asp:Label>
                                                                            </td>
                                                                        </tr>

                                                                    </tbody>

                                                                </table>



                                                            </div>
                                                            <hr>

                                                            <%-- <h5>Comment</h5>
                                                            <div class="row">
                                                                <div class="col-12">
                                                                    <textarea id="inputDescription" class="form-control"
                                                                        rows="3">Property ...................................</textarea>
                                                                </div>
                                                            </div>--%>
                                                            <br>

                                                            <div class="text-right">
                                                                <button type="button" class="btn btn-success float-right">Registered Case</button>
                                                            </div>

                                                        </div>

                                                    </div>
                                                </div>
                                            </div>

                                        </div>



                                        <div class="col-12 col-sm-6">
                                            <div class="card card-primary card-tabs">
                                                <div class="card-header p-0 pt-1">
                                                    <ul class="nav nav-tabs" id="custom-tabs-two-tab" role="tablist">
                                                        <li class="nav-item">
                                                            <a class="nav-link active" id="custom-tabs-two-home-tab" data-toggle="pill"
                                                                href="#custom-tabs-two-home" role="tab" aria-controls="custom-tabs-two-home"
                                                                aria-selected="true">Document </a>
                                                        </li>
                                                        <!-- <li class="nav-item">
                                  <a class="nav-link" id="custom-tabs-two-profile-tab" data-toggle="pill"
                                    href="#custom-tabs-two-profile" role="tab" aria-controls="custom-tabs-two-profile"
                                    aria-selected="false">Profile</a>
                                </li> -->
                                                    </ul>
                                                </div>
                                                <div class="card-body">
                                                    <div class="tab-content" id="custom-tabs-two-tabContent">
                                                        <div class="tab-pane fade show active" id="custom-tabs-two-home" role="tabpanel"
                                                            aria-labelledby="custom-tabs-two-home-tab">

                                                            <div class="col-12 col-lg-12">
                                                                <div class="card card-primary card-outline card-outline-tabs">
                                                                    <div class="card-header p-0 border-bottom-0 a-clr">
                                                                        <ul class="nav nav-tabs" id="custom-tabs-four-tab" role="tablist">
                                                                            <li class="nav-item">
                                                                                <a class="nav-link" id="custom-tabs-four-home-tab" data-toggle="pill" href="#custom-tabs-four-home" role="tab" aria-controls="custom-tabs-four-home" aria-selected="false">TOC</a>
                                                                            </li>
                                                                            <li class="nav-item">
                                                                                <a class="nav-link active" id="custom-tabs-four-profile-tab" data-toggle="pill" href="#custom-tabs-four-profile" role="tab" aria-controls="custom-tabs-four-profile" aria-selected="false">Recent</a>
                                                                            </li>
                                                                            <li class="nav-item">
                                                                                <a class="nav-link" id="custom-tabs-four-messages-tab" data-toggle="pill" href="#custom-tabs-four-messages" role="tab" aria-controls="custom-tabs-four-messages" aria-selected="false">All</a>
                                                                            </li>
                                                                            <li class="nav-item">
                                                                                <a class="nav-link" id="custom-tabs-four-settings-tab" data-toggle="pill" href="#custom-tabs-four-settings" role="tab" aria-controls="custom-tabs-four-settings" aria-selected="true">Previous Document</a>
                                                                            </li>
                                                                        </ul>
                                                                    </div>
                                                                    <div class="card-body">
                                                                        <div class="tab-content" id="custom-tabs-four-tabContent">
                                                                            <div class="tab-pane fade" id="custom-tabs-four-home" role="tabpanel" aria-labelledby="custom-tabs-four-home-tab">

                                                                                <h5>List of Documents </h5>
                                                                                <table id="example11" class="table table-bordered table-striped">
                                                                                    <thead>
                                                                                        <tr>
                                                                                            <th>SNo.</th>
                                                                                            <th>Document Name </th>
                                                                                            <th>Provided by</th>
                                                                                            <th>Uploaded by</th>
                                                                                            <th>Uploaded Date</th>
                                                                                        </tr>
                                                                                    </thead>
                                                                                    <tbody>
                                                                                        <tr>
                                                                                            <td>1</td>
                                                                                            <td><a href="#">Registry copy</a> </td>
                                                                                            <td>Party</td>
                                                                                            <td>Party</td>
                                                                                            <td>06-05-2023 </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td>2</td>
                                                                                            <td><a href="#">XYZ </a></td>
                                                                                            <td>SR</td>
                                                                                            <td>SR</td>
                                                                                            <td>02-04-2023 </td>
                                                                                        </tr>
                                                                                    </tbody>
                                                                                </table>

                                                                                <button type="button" class="btn btn-success float-right" data-toggle="modal" data-target="#exampleModalCenter">Attach </button>

                                                                            </div>
                                                                            <div class="tab-pane fade active show" id="custom-tabs-four-profile" role="tabpanel" aria-labelledby="custom-tabs-four-profile-tab">

                                                                                <div>
                                                                                    <img src="../dist/img/Doc_Sample_pic.jpg" />
                                                                                </div>

                                                                            </div>
                                                                            <div class="tab-pane fade" id="custom-tabs-four-messages" role="tabpanel" aria-labelledby="custom-tabs-four-messages-tab">

                                                                                <div>
                                                                                    <img src="../dist/img/Doc_Sample_pic.jpg" />
                                                                                </div>

                                                                            </div>
                                                                            <div class="tab-pane fade " id="custom-tabs-four-settings" role="tabpanel" aria-labelledby="custom-tabs-four-settings-tab">

                                                                                <div>
                                                                                    <img src="../dist/img/Doc_Sample_pic.jpg" />
                                                                                </div>

                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <!-- /.card -->
                                                                </div>
                                                            </div>

                                                        </div>

                                                    </div>
                                                </div>
                                                <!-- /.card -->
                                            </div>
                                        </div>
                                    </div>
                                </div>

                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </section>

    <!-- Modal -->
    <div class="modal fade" id="exampleModalCenter" tabindex="-1" role="dialog" aria-labelledby="exampleModalCenterTitle" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <div class="form-group">
                        <label for="inputDescription">Additional Reply</label>
                        <textarea id="inputDescription" class="form-control" rows="4"></textarea>
                    </div>

                    <div class="form-group">
                        <label for="inputEstimatedBudget">Additional Document Submitted by </label>
                        <input type="number" id="inputEstimatedBudget" class="form-control">
                    </div>
                    <div class="form-group">
                        <label for="inputEstimatedBudget">Document name  </label>
                        <input type="number" id="inputEstimatedBudget" class="form-control">
                    </div>
                    <div class="form-group">
                        <input type="file" id="inputEstimatedBudget" class="">
                    </div>

                </div>
                <div class="modal-footer">
                    <%-- <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>--%>
                    <button type="button" class="btn btn-primary">Add</button>
                </div>
            </div>
        </div>
    </div>


     <asp:HiddenField ID="hfuserid" runat="server" />
        <asp:HiddenField ID="hfcasenumber" runat="server" />


    <script>
        $(function () {
            $("#example1").DataTable({
                "responsive": true, "lengthChange": false, "autoWidth": false,
                "buttons": ["csv", "excel", "pdf", "print"]
            }).buttons().container().appendTo('#example1_wrapper .col-md-6:eq(0)');
            $('#example2').DataTable({
                "paging": true,
                "lengthChange": false,
                "searching": false,
                "ordering": true,
                "info": true,
                "autoWidth": false,
                "responsive": true,
            });
        });


        function BindTable(appid, Appno) {
            alert("111");

            $.ajax({
                type: "POST",
                url: "AcceptRejectCases_details.aspx/DocFile",
                data: '{appid: "' + appid + '",Appno:"' + Appno + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    $("#dynamic-table tbody").empty();
                    var json = JSON.parse(msg.d);
                    var ffd = '';
                    $.each(json, function (index, obj) {
                        

                        var row = '<tr><td align="center">' + obj.Doc_Name + '</td> <td align="center">' + obj.File_Path + ' </td> <td align="center">' + obj.File_Path + '</td></tr>'
                        $("#dynamic-table tbody").append(row);

                    });
                  
                }

            });

        }



    </script>

</asp:Content>
