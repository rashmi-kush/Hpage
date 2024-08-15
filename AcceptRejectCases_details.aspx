<%@ Page Title="" Language="C#" MasterPageFile="~/CoS/CoSMaster.Master" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="AcceptRejectCases_details.aspx.cs" Inherits="CMS_Sampada.CoS.AcceptRejectCases_details" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Styles/sweetalert.css" rel="stylesheet" />
    <script src="../Scripts/sweetalert.min.js"></script>
    <script src="../assets/js/3.3.1/sweetalert2@11.js"></script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style>
        .casefontcustm h2#swal2-title {
            font-size: 1rem;
        }
    </style>
    <asp:ScriptManager ID="SCMNG" runat="server"></asp:ScriptManager>
    <section class="content-header">
        <div class="container-fluid">
            <div class="row">
                <div class="col-sm-6">
                    <h5>Proposal No -
                        <asp:Label ID="lblProposalIdHeading" Text="" runat="server"></asp:Label>
                    </h5>
                </div>
                <div class="col-sm-4">
                    <h5>Proposal Impound Date -
                        <asp:Label ID="lblProImpoundDt" runat="server"></asp:Label>
                    </h5>
                </div>
                <div class="col-sm-4">
                    <h5>
                        <asp:Label ID="lblTodate" Visible="false" runat="server"></asp:Label>
                    </h5>
                </div>
                <%-- 
                <div class="col-sm-6">
                    <ol class="breadcrumb float-sm-right">
                        <li class="breadcrumb-item">
                            <a href="#">Home</a>
                        </li>
                        <li class="breadcrumb-item active">proposal List</li>
                    </ol>
                </div>--%>
            </div>
        </div>
    </section>
    <style type="text/css">
        .top-main-head {
            height: 30px;
            margin-top: 1rem;
        }

        td.forset.value-txt {
            position: relative;
            top: 16px;
        }

        .vertical-align-top {
            vertical-align: top;
        }
    </style>
    <section class="content">
        <div class="container-fluid">
            <div class="row">
                <div class="col-md-12">
                    <div class="card">
                        <div class="card-header p-2">
                            <ul class="nav nav-pills">
                                <li class="nav-item"><a class="nav-link active disabled" href="#">Proposal</a></li>
                                <li class="nav-item"><a class="nav-link disabled" href="#">Order Sheet</a></li>
                                <li class="nav-item"><a class="nav-link disabled" href="#">Notice</a></li>
                                <li class="nav-item"><a class="nav-link disabled" href="#">Seek Report</a></li>
                                <li class="nav-item"><a class="nav-link disabled" href="#">Notice Proceeding</a></li>
                                <li class="nav-item"><a class="nav-link disabled" href="#">Hearing</a></li>
                                <li class="nav-item"><a class="nav-link disabled" href="#">Final Order</a></li>
                                <li class="nav-item"><a class="nav-link disabled" href="#">Payment</a></li>
                                <li class="nav-item"><a class="nav-link disabled" href="#">Closed Cases</a></li>
                            </ul>
                        </div>
                        <div class="card-body">
                            <div class="tab-content">
                                <%------------------------------------------------------------------------------------------%>
                                <div class="tab-pane active" id="SendBack">
                                    <div class="row">
                                        <div class="col-12 col-sm-6">
                                            <div class="card card-primary card-tabs h-100">
                                                <div class="card-header p-0 pt-1">
                                                    <ul class="nav nav-tabs" id="custom-tabs-one-tab" role="tablist">
                                                        <li class="nav-item">
                                                            <a class="nav-link active" id="custom-tabs-one-profile-tab" data-toggle="pill" href="#custom-tabs-one-RegisteredForm" role="tab" aria-controls="custom-tabs-one-profile" aria-selected="true">Proposal Form </a>
                                                        </li>
                                                    </ul>
                                                </div>
                                                <div class="card-body">
                                                    <div class="tab-content" id="custom-tabs-one-tabContent">
                                                        <div class="tab-pane active show" id="custom-tabs-one-RegisteredForm" role="tabpanel" aria-labelledby="custom-tabs-one-profile-tab">
                                                            <!-- table -->
                                                            <div class="col-md-12">
                                                                <div class="card card-primary">
                                                                    <div class="card-header">
                                                                        <h3 class="card-title">Party Details </h3>
                                                                        <div class="card-tools">
                                                                            <button type="button" class="btn btn-tool" data-card-widget="collapse">
                                                                                <i class="fas fa-minus"></i>
                                                                            </button>
                                                                        </div>
                                                                    </div>
                                                                    <div class="card-body">
                                                                        <asp:GridView ID="grdPartyDetails" CssClass="table table-bordered table-condensed table-striped table-hover" DataKeyNames="party_id" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None" AutoGenerateColumns="false" Visible="true" EmptyDataText="No record found">
                                                                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                                                            <Columns>
                                                                                <asp:TemplateField HeaderText="SNo." HeaderStyle-VerticalAlign="Top" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="false">
                                                                                    <ItemTemplate>
                                                                                        <itemtemplate><%#Container.DataItemIndex+1  %> </itemtemplate>
                                                                                    </ItemTemplate>
                                                                                    <ItemStyle Width="5%" HorizontalAlign="Center" />
                                                                                    <HeaderStyle Width="5%" HorizontalAlign="Center" />
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Applicant Type" HeaderStyle-Font-Bold="false">
                                                                                    <ItemTemplate><%#Eval("APPLICANT_TYPE_NAME_EN") %> </ItemTemplate>
                                                                                    <HeaderStyle Width="10%" HorizontalAlign="Center" />
                                                                                    <ItemStyle Width="10%" HorizontalAlign="Left" />
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Party Type" HeaderStyle-Font-Bold="false">
                                                                                    <ItemTemplate><%#Eval("PARTY_TYPE_NAME_EN") %> </ItemTemplate>
                                                                                    <HeaderStyle Width="10%" HorizontalAlign="Left" />
                                                                                    <ItemStyle Width="10%" HorizontalAlign="Left" />
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Party Name" HeaderStyle-Font-Bold="false">
                                                                                    <ItemTemplate><%#Eval("Party_Name") %> </ItemTemplate>
                                                                                    <HeaderStyle Width="10%" HorizontalAlign="Left" />
                                                                                    <ItemStyle Width="10%" HorizontalAlign="Left" />
                                                                                </asp:TemplateField>
                                                                                <%-- 
                                                                                <asp:TemplateField HeaderText="Father Name">
                                                                                    <ItemTemplate><%#Eval("PartyFather_Name") %> </ItemTemplate>
                                        <HeaderStyle Width="10%" HorizontalAlign="Left" />
                                        <ItemStyle Width="10%" HorizontalAlign="Left" />
                                        </asp:TemplateField>--%><%-- 
                                                                                <asp:TemplateField HeaderText="Email ID" HeaderStyle-Font-Bold="false">
                                                                                    <ItemTemplate><%#Eval("Email_Id") %> </ItemTemplate>
                                        <HeaderStyle Width="10%" HorizontalAlign="Center" />
                                        <ItemStyle Width="10%" HorizontalAlign="Left" />
                                        </asp:TemplateField>--%>
                                                                                <asp:TemplateField HeaderText="Mobile" HeaderStyle-Font-Bold="false">
                                                                                    <ItemTemplate><%#Eval("Mob_No") %> </ItemTemplate>
                                                                                    <HeaderStyle Width="10%" HorizontalAlign="Center" />
                                                                                    <ItemStyle Width="10%" HorizontalAlign="Left" />
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Whats App Number" Visible="false" HeaderStyle-Font-Bold="false">
                                                                                    <ItemTemplate><%#Eval("Whatsapp_No") %> </ItemTemplate>
                                                                                    <HeaderStyle Width="10%" HorizontalAlign="Center" />
                                                                                    <ItemStyle Width="10%" HorizontalAlign="Left" />
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Action" HeaderStyle-Font-Bold="false">
                                                                                    <ItemStyle Width="4%" HorizontalAlign="Center" />
                                                                                    <ItemTemplate>
                                                                                        <asp:LinkButton ID="lnkPartyView" runat="server" OnClick="lnkPartyView_Click" CssClass="btn btn-secondary">View</asp:LinkButton>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                            </Columns>
                                                                            <EditRowStyle BackColor="#999999" />
                                                                            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                                                            <HeaderStyle BackColor="#e9564e" Font-Bold="false" ForeColor="White" />
                                                                            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                                                            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                                                            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                                                            <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                                                            <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                                                            <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                                                            <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                                                                        </asp:GridView>
                                                                        <%--
                                                                        <br>--%>
                                                                        <asp:Panel ID="pnlPartyDetails" runat="server" Visible="false">
                                                                            <div class="col-md-6 col-lg-12">
                                                                                <div class="card card-outline card-success">
                                                                                    <div class="card-tools">
                                                                                        <button type="button" class="btn btn-tool" data-card-widget="remove">
                                                                                            <i class="fas fa-times"></i>
                                                                                        </button>
                                                                                    </div>
                                                                                    <div class="card-body">
                                                                                        <div class="row">
                                                                                            <div class="col-sm-4 border-right">
                                                                                                <div class="description-block">
                                                                                                    <h5 class="description-header">Transaction through POA</h5>
                                                                                                    <asp:Label ID="lblTRANSACTION_THROUGH_POA" runat="server"></asp:Label>
                                                                                                </div>
                                                                                            </div>
                                                                                            <div class="col-sm-4 border-right">
                                                                                                <div class="description-block">
                                                                                                    <h5 class="description-header">Father Name </h5>
                                                                                                    <asp:Label ID="lblPartyFName" runat="server"></asp:Label>
                                                                                                </div>
                                                                                            </div>
                                                                                            <div class="col-sm-4">
                                                                                                <div class="description-block">
                                                                                                    <h5 class="description-header">Whatsapp No</h5>
                                                                                                    <asp:Label ID="lblWhatsappNo" runat="server"></asp:Label>
                                                                                                </div>
                                                                                            </div>
                                                                                        </div>
                                                                                        <hr>
                                                                                        <div class="row">
                                                                                            <div class="col-sm-4 border-right">
                                                                                                <div class="description-block">
                                                                                                    <h5 class="description-header">Address</h5>
                                                                                                    <asp:Label ID="lblPartyAddress" runat="server"></asp:Label>
                                                                                                </div>
                                                                                            </div>
                                                                                            <div class="col-sm-4 border-right">
                                                                                                <div class="description-block">
                                                                                                    <h5 class="description-header">District </h5>
                                                                                                    <asp:Label ID="lblPartyDist" runat="server"></asp:Label>
                                                                                                </div>
                                                                                            </div>
                                                                                            <div class="col-sm-4">
                                                                                                <div class="description-block">
                                                                                                    <h5 class="description-header">Owner Name </h5>
                                                                                                    <asp:Label ID="lblOwnerName" runat="server"></asp:Label>
                                                                                                </div>
                                                                                            </div>
                                                                                        </div>
                                                                                        <hr>
                                                                                        <div class="row">
                                                                                            <div class="col-sm-4 border-right">
                                                                                                <div class="description-block">
                                                                                                    <h5 class="description-header">Email ID</h5>
                                                                                                    <asp:Label ID="lblEmailID" runat="server"></asp:Label>
                                                                                                </div>
                                                                                            </div>
                                                                                            <div class="col-sm-4 border-right">
                                                                                                <div class="description-block"></div>
                                                                                            </div>
                                                                                            <div class="col-sm-4">
                                                                                                <div class="description-block"></div>
                                                                                            </div>
                                                                                        </div>
                                                                                        <hr />
                                                                                        <div class="row">
                                                                                            <div class="col-sm-4 border-right">
                                                                                                <div class="description-block">
                                                                                                    <h5 class="description-header">Owner Mobile no</h5>
                                                                                                    <asp:Label ID="lblOwnerMob" runat="server"></asp:Label>
                                                                                                </div>
                                                                                            </div>
                                                                                            <div class="col-sm-4 border-right">
                                                                                                <div class="description-block">
                                                                                                    <h5 class="description-header">Owner whatsapp no</h5>
                                                                                                    <asp:Label ID="lblOwnerWhatsapp" runat="server"></asp:Label>
                                                                                                </div>
                                                                                            </div>
                                                                                            <div class="col-sm-4">
                                                                                                <div class="description-block">
                                                                                                    <h5 class="description-header">Owner Email ID</h5>
                                                                                                    <asp:Label ID="lblOwnerEmail" runat="server"></asp:Label>
                                                                                                </div>
                                                                                            </div>
                                                                                        </div>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                        </asp:Panel>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="col-md-12">
                                                                <div class="card card-primary collapsed-card">
                                                                    <div class="card-header">
                                                                        <h3 class="card-title">Basis of Impound / Referral</h3>
                                                                        <div class="card-tools">
                                                                            <button type="button" class="btn btn-tool" data-card-widget="collapse">
                                                                                <i class="fas fa-plus"></i>
                                                                            </button>
                                                                        </div>
                                                                    </div>
                                                                    <div class="card-body">
                                                                        <div class="row">
                                                                            <div class="col-12">
                                                                                <%--Unduly Stamped--%>
                                                                                <asp:Label ID="lblReasonImpound" runat="server" Text=""></asp:Label>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="col-md-12">
                                                                <div class="card card-primary collapsed-card">
                                                                    <div class="card-header">
                                                                        <h3 class="card-title">Property Details</h3>
                                                                        <div class="card-tools">
                                                                            <button type="button" class="btn btn-tool" data-card-widget="collapse">
                                                                                <i class="fas fa-plus"></i>
                                                                            </button>
                                                                        </div>
                                                                    </div>
                                                                    <div class="card-body">
                                                                        <div class="table-responsive">
                                                                            <asp:GridView ID="GVPropertyDetail" CssClass="table table-bordered table-condensed table-striped table-hover" DataKeyNames="Property_ID" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None" AutoGenerateColumns="false" Visible="true" EmptyDataText="No record found">
                                                                                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                                                                <Columns>
                                                                                    <asp:TemplateField HeaderText="SNo." HeaderStyle-VerticalAlign="Top" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="false">
                                                                                        <ItemTemplate>
                                                                                            <itemtemplate><%#Container.DataItemIndex+1  %> </itemtemplate>
                                                                                        </ItemTemplate>
                                                                                        <ItemStyle Width="5%" HorizontalAlign="Center" />
                                                                                        <HeaderStyle Width="5%" HorizontalAlign="Center" />
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Property Id" HeaderStyle-Font-Bold="false">
                                                                                        <ItemTemplate><%#Eval("Property_Given_ID") %> </ItemTemplate>
                                                                                        <HeaderStyle Width="10%" HorizontalAlign="Left" />
                                                                                        <ItemStyle Width="10%" HorizontalAlign="Left" />
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Pre Registration Id" Visible="false" HeaderStyle-Font-Bold="false">
                                                                                        <ItemTemplate><%#Eval("Pre_RegistrationID") %> </ItemTemplate>
                                                                                        <HeaderStyle Width="10%" HorizontalAlign="Left" />
                                                                                        <ItemStyle Width="10%" HorizontalAlign="Left" />
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Property Type" HeaderStyle-Font-Bold="false">
                                                                                        <ItemTemplate><%#Eval("Property_Type") %> </ItemTemplate>
                                                                                        <HeaderStyle Width="10%" HorizontalAlign="Center" />
                                                                                        <ItemStyle Width="10%" HorizontalAlign="Left" />
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Property Sub Type" HeaderStyle-Font-Bold="false">
                                                                                        <ItemTemplate><%#Eval("Property_SubType") %> </ItemTemplate>
                                                                                        <HeaderStyle Width="10%" HorizontalAlign="Center" />
                                                                                        <ItemStyle Width="10%" HorizontalAlign="Left" />
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Property Transacting Area" HeaderStyle-Font-Bold="false">
                                                                                        <ItemTemplate><%#Eval("Property_TransactingArea") %> </ItemTemplate>
                                                                                        <HeaderStyle Width="10%" HorizontalAlign="Center" />
                                                                                        <ItemStyle Width="10%" HorizontalAlign="Left" />
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Total Property Area" HeaderStyle-Font-Bold="false">
                                                                                        <ItemTemplate><%#Eval("Property_Area") %> </ItemTemplate>
                                                                                        <HeaderStyle Width="10%" HorizontalAlign="Center" />
                                                                                        <ItemStyle Width="10%" HorizontalAlign="Left" />
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Transacting Partial Area of Property" HeaderStyle-Font-Bold="false">
                                                                                        <ItemTemplate><%#Eval("Transacting_Partial_AreaOfProperty") %> </ItemTemplate>
                                                                                        <HeaderStyle Width="10%" HorizontalAlign="Left" />
                                                                                        <ItemStyle Width="10%" HorizontalAlign="Left" />
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Action" HeaderStyle-Font-Bold="false">
                                                                                        <ItemStyle Width="4%" HorizontalAlign="Center" />
                                                                                        <ItemTemplate>
                                                                                            <asp:LinkButton ID="lnkPropertyView" runat="server" OnClick="lnkPropertyView_Click" CssClass="btn btn-secondary">View</asp:LinkButton>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                </Columns>
                                                                                <EditRowStyle BackColor="#999999" />
                                                                                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                                                                <HeaderStyle BackColor="#e9564e" Font-Bold="false" ForeColor="White" />
                                                                                <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                                                                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                                                                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                                                                <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                                                                <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                                                                <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                                                                <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                                                                            </asp:GridView>
                                                                            <%--
                                                                                        <br/>--%>
                                                                            <asp:Panel ID="pnlPropertyDetails" runat="server" Visible="false">
                                                                                <div class="col-md-6 col-lg-12">
                                                                                    <div class="card card-outline card-success">
                                                                                        <div class="card-tools">
                                                                                            <button type="button" class="btn btn-tool" data-card-widget="remove">
                                                                                                <i class="fas fa-times"></i>
                                                                                            </button>
                                                                                        </div>
                                                                                        <div class="card-body">
                                                                                            <div class="row">
                                                                                                <div class="col-sm-4 border-right">
                                                                                                    <div class="description-block">
                                                                                                        <h5 class="description-header">Land Diversion</h5>
                                                                                                        <%--
                                                                                                                    <asp:TextBox ID="txtLandDiversion" required="required" runat="server" class="form-control" ReadOnly="true"></asp:TextBox>--%>
                                                                                                        <asp:Label ID="lblLandDiversion" runat="server"></asp:Label>
                                                                                                    </div>
                                                                                                </div>
                                                                                                <div class="col-sm-4 border-right">
                                                                                                    <div class="description-block">
                                                                                                        <h5 class="description-header">Land Diversion Declaration</h5>
                                                                                                        <%--
                                                                                                                    <asp:TextBox ID="txtLandDiversionDetails" required="required" runat="server" class="form-control" ReadOnly="true"></asp:TextBox>--%>
                                                                                                        <asp:Label ID="lblLandDiversionDetails" runat="server"></asp:Label>
                                                                                                    </div>
                                                                                                </div>
                                                                                                <div class="col-sm-4">
                                                                                                    <div class="description-block">
                                                                                                        <h5 class="description-header">Property Sub type 2</h5>
                                                                                                        <%--
                                                                                                                    <asp:TextBox ID="txtPropertySubType2" required="required" runat="server" class="form-control" ReadOnly="true"></asp:TextBox>--%>
                                                                                                        <asp:Label ID="lblPropertySubType2" runat="server"></asp:Label>
                                                                                                    </div>
                                                                                                </div>
                                                                                            </div>
                                                                                            <hr>
                                                                                            <div class="row">
                                                                                                <div class="col-sm-12 border-right">
                                                                                                    <div class="description-block">
                                                                                                        <h5 class="description-header">Property Address</h5>
                                                                                                        <%--
                                                                                                                        <asp:TextBox ID="txtPropertyAddress" required="required" runat="server" class="form-control" ReadOnly="true"></asp:TextBox>--%>
                                                                                                        <asp:Label ID="lblPropertyAddress" runat="server"></asp:Label>
                                                                                                    </div>
                                                                                                </div>

                                                                                            </div>
                                                                                            <%-- <hr>
                                                                                            <div class="row">
                                                                                                 <div class="col-sm-4 border-right">
                                                                                                    <div class="description-block">
                                                                                                        <h5 class="description-header">Property Image</h5>
                                                                                                        <asp:Image ID="imgProperty" runat="server" ImageUrl="~/assets/images/banner.png" Width="100px" Height="50px" />
                                                                                                    </div>
                                                                                                </div>
                                                                                                <div class="col-sm-4">
                                                                                                    <div class="description-block">
                                                                                                        <h5 class="description-header">Map of Property</h5>
                                                                                                        <asp:Image ID="imgPropertyMap" runat="server" ImageUrl="~/assets/images/banner.png" Width="100px" Height="50px" />
                                                                                                    </div>
                                                                                                </div>
                                                                                           </div>--%>
                                                                                        </div>
                                                                                    </div>
                                                                                </div>
                                                                            </asp:Panel>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="col-md-12">
                                                                <div class="card card-primary collapsed-card">
                                                                    <div class="card-header">
                                                                        <h3 class="card-title">Proposal Details</h3>
                                                                        <div class="card-tools">
                                                                            <button type="button" class="btn btn-tool" data-card-widget="collapse">
                                                                                <i class="fas fa-plus"></i>
                                                                            </button>
                                                                        </div>
                                                                    </div>

                                                                    <style type="text/css">
                                                                        .top-main-head-table {
                                                                            padding: 20px 0px;
                                                                        }

                                                                        h6.sub-inner-value {
                                                                            padding-top: 19px;
                                                                            margin: 0;
                                                                            height: 38px;
                                                                        }


                                                                        h6.sub-inner-txt {
                                                                            padding: 6px 0px;
                                                                        }

                                                                        td.tbl-row-first {
                                                                            padding-top: 37px !important;
                                                                        }


                                                                        td.tlb-second-row {
                                                                            padding-top: 25px !important;
                                                                        }

                                                                        h6.sub-inner-value-two {
                                                                            padding-top: 0px;
                                                                            margin: 0;
                                                                            height: 44px;
                                                                        }
                                                                    </style>



                                                                    <div class="card-body">
                                                                        <div class="table-responsive">
                                                                            <table id="example11" class="table table-bordered table-striped">
                                                                                <thead>
                                                                                    <tr>
                                                                                        <th>
                                                                                            <asp:Label ID="lblRegInitEStampID" Text="" runat="server"></asp:Label>
                                                                                        </th>
                                                                                        <asp:Panel ID="pnlRegInitDate" runat="server">
                                                                                            <th>
                                                                                                <asp:Label ID="lblRegInitDate" Text="" runat="server"></asp:Label>
                                                                                            </th>
                                                                                        </asp:Panel>
                                                                                        <th>Date of Presentation</th>
                                                                                        <th>Date of Execution</th>

                                                                                    </tr>
                                                                                </thead>
                                                                                <tbody>
                                                                                    <tr>
                                                                                        <td>
                                                                                            <asp:Label ID="lblPropertyRegNoInitIdEStampId" Text="" runat="server"></asp:Label>
                                                                                        </td>
                                                                                        <asp:Panel ID="pnlRegNoInitDate" runat="server">
                                                                                            <td>
                                                                                                <asp:Label ID="lblProRegDt" Text="" runat="server"></asp:Label>
                                                                                            </td>
                                                                                        </asp:Panel>
                                                                                        <td>
                                                                                            <asp:Label ID="lblDateofPresent" Text="" runat="server"></asp:Label>
                                                                                        </td>
                                                                                        <td>
                                                                                            <asp:Label ID="lblDateofExecution" Text="" runat="server"></asp:Label>
                                                                                        </td>
                                                                                    </tr>
                                                                                </tbody>
                                                                            </table>
                                                                        </div>
                                                                        <br>
                                                                        <table class="table table-bordered table-hover">
                                                                            <thead>
                                                                                <tr>
                                                                                    <th style="width: 200px;">Particulars</th>
                                                                                    <th>Details of Document as Presented by Party</th>
                                                                                    <th>Proposal ofRegistering Officer</th>
                                                                                    <th>Deficit Duty s/ Variation</th>
                                                                                    <th>Remark</th>
                                                                                </tr>
                                                                            </thead>
                                                                            <tbody>
                                                                                <tr>
                                                                                    <td>Nature of Document</td>
                                                                                    <td>
                                                                                        <asp:Label ID="lblNatureDoc" Text="" runat="server"></asp:Label></td>
                                                                                    <td>
                                                                                        <asp:Label ID="lblNatureDocRegOff" Text="" runat="server"></asp:Label></td>
                                                                                    <td>
                                                                                        <asp:Label ID="lblNatureDocDeficit" Text="" runat="server"></asp:Label></td>
                                                                                    <td>
                                                                                        <asp:Label ID="lblNatureDocRemark" Text="" runat="server"></asp:Label></td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>Consideration Value (₹)</td>
                                                                                    <td>
                                                                                        <asp:Label ID="lblConsidProperty" Text="" runat="server"></asp:Label></td>
                                                                                    <td>
                                                                                        <asp:Label ID="lblConsidPropertyRegOff" Text="" runat="server"></asp:Label></td>
                                                                                    <td>
                                                                                        <asp:Label ID="lblConsidPropertyDeficit" Text="" runat="server"></asp:Label></td>
                                                                                    <td>
                                                                                        <asp:Label ID="lblConsidPropertyRemark" Text="" runat="server"></asp:Label></td>
                                                                                </tr>

                                                                                <tr>
                                                                                    <td>Guideline Value of Property (₹)</td>
                                                                                    <td>
                                                                                        <asp:Label ID="lblGuideValue" Text="" runat="server"></asp:Label></td>
                                                                                    <td>
                                                                                        <asp:Label ID="lblGuideValueRegOff" Text="" runat="server"></asp:Label></td>
                                                                                    <td>
                                                                                        <asp:Label ID="lblGuideValueRegDefcit" Text="" runat="server"></asp:Label></td>
                                                                                    <td>
                                                                                        <asp:Label ID="lblGuideValueRemark" Text="" runat="server"></asp:Label></td>
                                                                                </tr>

                                                                                <%--<tr>
               <th colspan="5">99999</th>
               
            </tr>
           <tr>
    <td>sdjbsdjh</td>
    
 </tr>--%>

                                                                                <%--When Column or data 3 row will apply this code start--%>
                                                                                <tr>
                                                                                    <td>
                                                                                        <div class="top-main-head-table">Stamp Duty Bifurcation:</div>

                                                                                        <div class="row justify-content-center">
                                                                                            <h6 class="sub-inner-txt">Janpad (₹)</h6>
                                                                                        </div>

                                                                                        <div class="row justify-content-center">
                                                                                            <h6 class="sub-inner-txt">Upkar (₹)</h6>
                                                                                        </div>

                                                                                        <div class="row justify-content-center">
                                                                                            <h6 class="sub-inner-txt">Muncipal (₹)</h6>
                                                                                        </div>

                                                                                        <div class="row justify-content-center">
                                                                                            <h6 class="sub-inner-txt">Principle (₹)</h6>
                                                                                        </div>



                                                                                        <td class="tbl-row-first">
                                                                                            <h6 class="sub-inner-value">
                                                                                                <asp:Label ID="lblStampDutyClassJanpad" Text="" runat="server"></asp:Label></h6>
                                                                                            <h6 class="sub-inner-value">
                                                                                                <asp:Label ID="lblStampDutyClassUpkar" Text="" runat="server"></asp:Label></h6>
                                                                                            <h6 class="sub-inner-value">
                                                                                                <asp:Label ID="lblStampDutyClassMuncipal" Text="" runat="server"></asp:Label></h6>
                                                                                            <h6 class="sub-inner-value">
                                                                                                <asp:Label ID="lblStampDutyClassPrinciple" Text="" runat="server"></asp:Label></h6>
                                                                                        </td>

                                                                                        <td class="tbl-row-first">
                                                                                            <h6 class="sub-inner-value">
                                                                                                <asp:Label ID="lblProClassJanpad" Text="" runat="server"></asp:Label></h6>
                                                                                            <h6 class="sub-inner-value">
                                                                                                <asp:Label ID="lblProClassUpkar" Text="" runat="server"></asp:Label></h6>
                                                                                            <h6 class="sub-inner-value">
                                                                                                <asp:Label ID="lblProClassMuncipal" Text="" runat="server"></asp:Label></h6>
                                                                                            <h6 class="sub-inner-value">
                                                                                                <asp:Label ID="lblProClassPrinciple" Text="" runat="server"></asp:Label></h6>
                                                                                        </td>

                                                                                        <td class="tbl-row-first">
                                                                                            <h6 class="sub-inner-value">
                                                                                                <asp:Label ID="lblDeficitJanpad" Text="" Visible="false" runat="server"></asp:Label></h6>
                                                                                            <h6 class="sub-inner-value">
                                                                                                <asp:Label ID="lblDeficitUpkar" Text="" Visible="false" runat="server"></asp:Label></h6>
                                                                                            <h6 class="sub-inner-value">
                                                                                                <asp:Label ID="lblDeficitMuncipal" Text="" Visible="false" runat="server"></asp:Label></h6>
                                                                                            <h6 class="sub-inner-value">
                                                                                                <asp:Label ID="lblDeficitPrinciple" Text="" Visible="false" runat="server"></asp:Label></h6>
                                                                                        </td>

                                                                                        <td class="tbl-row-first">
                                                                                            <h6 class="sub-inner-value">
                                                                                                <asp:Label ID="lblPenalityJanpad" Text="" Visible="false" runat="server"></asp:Label></h6>
                                                                                            <h6 class="sub-inner-value">
                                                                                                <asp:Label ID="lblPenalityUpkar" Text="" Visible="false" runat="server"></asp:Label></h6>
                                                                                            <h6 class="sub-inner-value">
                                                                                                <asp:Label ID="lblPenalityMuncipal" Text="" Visible="false" runat="server"></asp:Label></h6>
                                                                                            <h6 class="sub-inner-value">
                                                                                                <asp:Label ID="lblPenalityPrinciple" Text="" Visible="false" runat="server"></asp:Label></h6>
                                                                                        </td>



                                                                                    </td>
                                                                                </tr>

                                                                                <%--When Column or data 3 row will apply this code end--%>

                                                                                <tr>
                                                                                    <td>Total Stamp Duty (₹)</td>
                                                                                    <td>
                                                                                        <asp:Label ID="lblTotalStampDuty" Text="" runat="server"></asp:Label></td>
                                                                                    <td>
                                                                                        <asp:Label ID="lblTotalStampDutyRO" Text="" runat="server"></asp:Label></td>
                                                                                    <td>
                                                                                        <asp:Label ID="lblTotalStampDutyDeficit" Text="" runat="server"></asp:Label></td>
                                                                                    <td>
                                                                                        <asp:Label ID="lblTotalStampDutyRem" Text="" runat="server"></asp:Label></td>
                                                                                </tr>


                                                                                <tr>
                                                                                    <td class="whn_two_fild">
                                                                                        <div class="row justify-content-center">
                                                                                            <h6 class="sub-inner-txt">Exempted Amount (₹)</h6>
                                                                                        </div>

                                                                                        <div class="row justify-content-center">
                                                                                            <h6 class="sub-inner-txt">Already Paid Duty (₹)</h6>
                                                                                        </div>



                                                                                        <td class="tbl-row-first tlb-second-row">
                                                                                            <h6 class="sub-inner-value sub-inner-value-two">
                                                                                                <asp:Label ID="lblStampDutyExemptedAmt" Text="" runat="server"></asp:Label>
                                                                                            </h6>
                                                                                            <h6 class="sub-inner-value sub-inner-value-two">
                                                                                                <asp:Label ID="lblAlreadyPaidDuty" Text="" runat="server"></asp:Label></h6>
                                                                                        </td>
                                                                                        <td class="tbl-row-first tlb-second-row">
                                                                                            <h6 class="sub-inner-value sub-inner-value-two">
                                                                                                <asp:Label ID="lblProExemptedAmt" Text="" runat="server"></asp:Label></h6>
                                                                                            <h6 class="sub-inner-value sub-inner-value-two">
                                                                                                <asp:Label ID="lblAlreadyPaidDutyRO" Text="" runat="server"></asp:Label></h6>

                                                                                        </td>

                                                                                        <td class="tbl-row-first tlb-second-row">
                                                                                            <h6 class="sub-inner-value sub-inner-value-two">
                                                                                                <asp:Label ID="Label24" Text="" Visible="false" runat="server"></asp:Label></h6>
                                                                                            <h6 class="sub-inner-value sub-inner-value-two">
                                                                                                <asp:Label ID="Label25" Text="" Visible="false" runat="server"></asp:Label></h6>

                                                                                        </td>

                                                                                        <td class="tbl-row-first tlb-second-row">
                                                                                            <h6 class="sub-inner-value sub-inner-value-two">
                                                                                                <asp:Label ID="Label28" Text="" Visible="false" runat="server"></asp:Label></h6>
                                                                                            <h6 class="sub-inner-value sub-inner-value-two">
                                                                                                <asp:Label ID="Label29" Text="" Visible="false" runat="server"></asp:Label>
                                                                                            </h6>

                                                                                        </td>



                                                                                    </td>
                                                                                </tr>



                                                                                <tr>
                                                                                    <td>Net Stamp Duty (₹)</td>
                                                                                    <td>
                                                                                        <asp:Label ID="lblStamDuty" Text="" runat="server"></asp:Label></td>
                                                                                    <td>
                                                                                        <asp:Label ID="lblProRecStmapDuty" Text="" runat="server"></asp:Label></td>
                                                                                    <td>
                                                                                        <asp:Label ID="lblDeficitDuty" Text="" runat="server"></asp:Label></td>
                                                                                    <td>
                                                                                        <asp:Label ID="lblStampDutyRemark" Text="" runat="server"></asp:Label></td>



                                                                                </tr>

                                                                                <tr>
                                                                                    <td>

                                                                                        <div class="top-main-head-table">Registration Fee Bifurcation:</div>

                                                                                        <div class="row justify-content-center">
                                                                                            <h6 class="sub-inner-txt">Total Registration Fee (₹)</h6>
                                                                                        </div>

                                                                                        <div class="row justify-content-center">
                                                                                            <h6 class="sub-inner-txt">Exempted Amount (₹)</h6>
                                                                                        </div>

                                                                                        <div class="row justify-content-center">
                                                                                            <h6 class="sub-inner-txt">Already Paid Reg Fee (₹)</h6>
                                                                                        </div>



                                                                                        <td class="tbl-row-first">
                                                                                            <h6 class="sub-inner-value">
                                                                                                <asp:Label ID="lblRegiFee" Text="" runat="server"></asp:Label></h6>
                                                                                            <h6 class="sub-inner-value">
                                                                                                <asp:Label ID="lblRegiExemptedAmt" Text="" runat="server"></asp:Label></h6>
                                                                                            <h6 class="sub-inner-value">
                                                                                                <asp:Label ID="lblAlreadyPaidRegFee" Text="" runat="server"></asp:Label></h6>
                                                                                        </td>
                                                                                        <td class="tbl-row-first">
                                                                                            <h6 class="sub-inner-value">
                                                                                                <asp:Label ID="lblProRegiFee" Text="" runat="server"></asp:Label></h6>
                                                                                            <h6 class="sub-inner-value">
                                                                                                <asp:Label ID="lblProRegiExemptedAmt" Text="" runat="server"></asp:Label></h6>
                                                                                            <h6 class="sub-inner-value">
                                                                                                <asp:Label ID="lblAlreadyPaidRegFeeRO" Text="" runat="server"></asp:Label></h6>

                                                                                        </td>

                                                                                        <td class="tbl-row-first">
                                                                                            <h6 class="sub-inner-value">
                                                                                                <asp:Label ID="Label12" Text="" runat="server"></asp:Label></h6>
                                                                                            <h6 class="sub-inner-value">
                                                                                                <asp:Label ID="Label13" Text="" runat="server"></asp:Label></h6>
                                                                                            <h6 class="sub-inner-value">
                                                                                                <asp:Label ID="Label1" Text="" runat="server"></asp:Label></h6>

                                                                                        </td>

                                                                                        <td class="tbl-row-first">
                                                                                            <h6 class="sub-inner-value">
                                                                                                <asp:Label ID="Label16" Text="" runat="server"></asp:Label></h6>
                                                                                            <h6 class="sub-inner-value">
                                                                                                <asp:Label ID="Label17" Text="" runat="server"></asp:Label></h6>
                                                                                            <h6 class="sub-inner-value">
                                                                                                <asp:Label ID="Label2" Text="" runat="server"></asp:Label></h6>

                                                                                        </td>



                                                                                    </td>
                                                                                </tr>

                                                                                <tr>
                                                                                    <td>Net Registration Fees (₹)</td>
                                                                                    <td>
                                                                                        <asp:Label ID="lblRegFee" Text="" runat="server"></asp:Label></td>
                                                                                    <td>
                                                                                        <asp:Label ID="lblRecoverRegfee" Text="" runat="server"></asp:Label></td>
                                                                                    <td>
                                                                                        <asp:Label ID="lblDeficitRegFee" Text="" runat="server"></asp:Label></td>
                                                                                    <td>
                                                                                        <asp:Label ID="lblRegFeeRemark" Text="" runat="server"></asp:Label></td>
                                                                                </tr>





                                                                            </tbody>
                                                                        </table>

                                                                        <div class="table-responsive">
                                                                            <%-- 
                                                                                                <table id="example11" class="table table-bordered table-striped">
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
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="col-md-12">
                                                                <div class="card card-primary collapsed-card">
                                                                    <div class="card-header">
                                                                        <%--
                                                                                            <h3 class="card-title">Sub Registrar Details</h3>--%>
                                                                        <h3 class="card-title">
                                                                            <asp:Label ID="lblHeading" runat="server"></asp:Label>
                                                                        </h3>
                                                                        <div class="card-tools">
                                                                            <button type="button" class="btn btn-tool" data-card-widget="collapse">
                                                                                <i class="fas fa-plus"></i>
                                                                            </button>
                                                                        </div>
                                                                    </div>
                                                                    <div class="card-body">
                                                                        <div class="table-responsive">
                                                                            <asp:UpdatePanel ID="upnlSR" runat="server">
                                                                                <ContentTemplate>
                                                                                    <table id="tbl2" class="table table-hover gid-view-tab table-bordered ">
                                                                                        <thead>
                                                                                            <tr>
                                                                                                <th>SRO Id</th>
                                                                                                <th>Sub Registrar Name</th>
                                                                                                <th>SRO Name</th>
                                                                                                <th>Proposal ID</th>
                                                                                                <th>Date of Impound</th>
                                                                                                <%--
                                                                                                                    <th>
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
                                                                                                    <asp:Label ID="lblSROID" Text="" runat="server"></asp:Label>
                                                                                                </td>
                                                                                                <td>
                                                                                                    <asp:Label ID="lblSRName" Text="" runat="server"></asp:Label>
                                                                                                </td>
                                                                                                <td>
                                                                                                    <asp:Label ID="lblSROName" Text="" runat="server"></asp:Label>
                                                                                                </td>
                                                                                                <td>
                                                                                                    <asp:Label ID="lblProposalId" Text="" runat="server"></asp:Label>
                                                                                                </td>
                                                                                                <td>
                                                                                                    <asp:Label ID="lblProposalDate" Text="" runat="server"></asp:Label>
                                                                                                </td>
                                                                                                <asp:Panel ID="pnlAuditIdandDate" runat="server" Visible="false">
                                                                                                    <td>
                                                                                                        <asp:Label ID="lblAuditId" Text="" runat="server"></asp:Label>
                                                                                                    </td>
                                                                                                    <td>
                                                                                                        <asp:Label ID="lblAuditDt" Text="" runat="server"></asp:Label>
                                                                                                    </td>
                                                                                                </asp:Panel>
                                                                                                <td><%--
                                                                                                                        <asp:Label ID="lblHeadbySR" Text="" runat="server"></asp:Label>--%>
                                                                                                    <asp:DropDownList ID="ddlHead1" ClientIDMode="Static" Width="85px" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlHead1_SelectedIndexChanged"></asp:DropDownList><%-- 
                                                                                                                        <ajaxToolkit:CascadingDropDown ID="CasDistrict" SelectedValue="20A" runat="server" TargetControlID="ddlHead1" Category="district" ServicePath="~/Districthirarcy.asmx" LoadingText="[Loading State...]" ServiceMethod="BindStateDetails" />--%> </td>
                                                                                                <td><%--
                                                                                                                        <asp:Label ID="lblSecbySR" Text="" runat="server"></asp:Label>--%>
                                                                                                    <asp:DropDownList ID="ddlSec1" Width="126px" ClientIDMode="Static" runat="server" AutoPostBack="true" CssClass="form-control" OnSelectedIndexChanged="ddlSec1_SelectedIndexChanged"></asp:DropDownList><%--
                                                                                                                        <ajaxToolkit:CascadingDropDown ID="CasSubDivision" SelectedValue="19A" runat="server" TargetControlID="ddlSec1" ServicePath="~/Districthirarcy.asmx" ParentControlID="ddlHead1" PromptText="Select" ServiceMethod="BindDistrictDetailsOnstateID" Category="subdivision" Enabled="True" />--%> </td>
                                                                                            </tr>
                                                                                        </tbody>
                                                                                    </table>
                                                                                </ContentTemplate>
                                                                            </asp:UpdatePanel>
                                                                        </div>
                                                                        <table id="tbl3" class="table table-hover gid-view-tab table-bordered ">
                                                                            <thead>
                                                                                <tr>
                                                                                    <th>
                                                                                        <asp:Label ID="lblOfficeName" runat="server"></asp:Label>
                                                                                    </th>
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
                                                                </div>
                                                            </div>
                                                            <div class="col-md-12" style="display: none;">
                                                                <div class="card card-primary collapsed-card">
                                                                    <div class="card-header">
                                                                        <h3 class="card-title">Comment</h3>
                                                                        <div class="card-tools">
                                                                            <button type="button" class="btn btn-tool" data-card-widget="collapse">
                                                                                <i class="fas fa-plus"></i>
                                                                            </button>
                                                                        </div>
                                                                    </div>
                                                                    <div class="card-body">
                                                                        <div class="row">
                                                                            <div class="col-12">
                                                                                <textarea id="inputDescription" class="form-control" rows="3">Property ...................................</textarea>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="text-right mr-2">
                                                                <asp:Button ID="btnAccept" class="btn btn-success float-right" runat="server" OnClientClick="return validateSection()" Text="Register Case" OnClick="btnAccept_Click" /><%--
                                                                                    <button type="button" class="btn btn-success float-right" id="RegisteredCase">Registered Case</button>--%><%--
                                                                                    <button onclick="myFunction()">Try it</button>--%>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <asp:HiddenField ID="hdnfldHead" ClientIDMode="Static" runat="server" />
                                        <asp:HiddenField ID="hdnfldSection" ClientIDMode="Static" runat="server" />
                                        <asp:HiddenField ID="hdnfldCase" ClientIDMode="Static" runat="server" />
                                        <asp:HiddenField ID="hdnAppID" ClientIDMode="Static" runat="server" />
                                        <div class="col-12 col-sm-6">
                                            <div class="card card-primary card-tabs h-100">
                                                <div class="card-header p-0 pt-1 d-flex align-items-center">
                                                    <div class="col-sm-8 p-0">
                                                        <ul class="nav nav-tabs" id="custom-tabs-two-tab" role="tablist">
                                                            <li class="nav-item">
                                                                <a class="nav-link active" id="custom-tabs-two-home-tab" data-toggle="pill" href="#custom-tabs-two-home" role="tab" aria-controls="custom-tabs-two-home" aria-selected="true">Document </a>
                                                            </li>
                                                            <!-- <li class="nav-item"><a class="nav-link" id="custom-tabs-two-profile-tab" data-toggle="pill"
                                    href="#custom-tabs-two-profile" role="tab" aria-controls="custom-tabs-two-profile"
                                    aria-selected="false">Profile</a></li> -->
                                                        </ul>
                                                    </div>
                                                    <div class="col-sm-4">
                                                        <div class="align-items-end d-flex justify-content-end mr-3 w-100">
                                                            <a href="#" id="btnShowDownloadOption" onclick="ShowDownloadOption()">
                                                                <i class="fa fa-download mr-3 "></i>
                                                            </a>
                                                            <a href="#" onclick="printPDF()">
                                                                <i class="fa fa-print"></i>
                                                            </a>
                                                        </div>
                                                    </div>
                                                </div>
                                                <%--    
                                                                    <div class="text-right newbtndp">
                                                                        <button type="button"  class="btn btn-secondary btn-sm" >Download </button>
                                                    &nbsp;
                                                    
                                                                        <button type="button" class="btn btn-info btn-sm float-right" >Print </button>
                                                    &nbsp;
                                                
                                                                    </div>--%>
                                                <div class="card-body">
                                                    <div class="tab-content" id="custom-tabs-two-tabContent">
                                                        <div class="tab-pane fade show active" id="custom-tabs-two-home" role="tabpanel" aria-labelledby="custom-tabs-two-home-tab">
                                                            <div class="col-12 col-lg-12">
                                                                <div class="card card-primary card-outline card-outline-tabs">
                                                                    <div class="card-header p-0 border-bottom-0 a-clr">
                                                                        <ul class="nav nav-tabs" id="custom-tabs-four-tab" role="tablist">
                                                                            <li class="nav-item">
                                                                                <a class="nav-link" id="custom-tabs-four-home-tab" onclick="ShowDocList()" data-toggle="pill" href="#custom-tabs-four-home" role="tab" aria-controls="custom-tabs-four-home" aria-selected="false">Index</a>
                                                                            </li>
                                                                            <li class="nav-item">
                                                                                <a class="nav-link active" id="custom-tabs-four-profile-tab" data-toggle="pill" href="#custom-tabs-four-profile" role="tab" aria-controls="custom-tabs-four-profile" aria-selected="false">Recent</a>
                                                                            </li>
                                                                            <li class="nav-item"><%--
                                                                                                    <a class="nav-link" id="custom-tabs-four-messages-tab" data-toggle="pill" href="#custom-tabs-four-messages" role="tab" aria-controls="custom-tabs-four-messages" aria-selected="false">All</a>--%> <a class="nav-link" id="custom-tabs-four-messages-tab" data-toggle="pill" href="#custom-tabs-four-profile" role="tab" aria-controls="custom-tabs-four-messages" aria-selected="false">All</a>
                                                                            </li>
                                                                            <li class="nav-item">
                                                                                <a class="nav-link" id="custom-tabs-four-settings-tab" style="pointer-events: none;" data-toggle="pill" href="#custom-tabs-four-settings" role="tab" aria-controls="custom-tabs-four-settings" aria-selected="true">Previous Proceeding</a>
                                                                            </li>
                                                                        </ul>
                                                                    </div>
                                                                    <div class="card-body">
                                                                        <div class="tab-content" id="custom-tabs-four-tabContent">
                                                                            <div class="tab-pane fade" id="custom-tabs-four-home" role="tabpanel" aria-labelledby="custom-tabs-four-home-tab">
                                                                                <%--  
                                                                                                    <h6>List of Documents </h6>--%>
                                                                                <h5 style="text-align: center">List of Documents </h5>
                                                                                <div id="pnl2">
                                                                                    <asp:GridView ID="grdSRDoc" CssClass="table table-bordered table-condensed table-striped table-hover" DataKeyNames="App_ID" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None" AutoGenerateColumns="false" Visible="true" EmptyDataText="No record found" OnRowDataBound="grdSRDoc_RowDataBound">
                                                                                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                                                                        <Columns>
                                                                                            <asp:TemplateField HeaderText="SNo." HeaderStyle-VerticalAlign="Top" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="false">
                                                                                                <ItemTemplate>
                                                                                                    <itemtemplate><%#Container.DataItemIndex+1%> </itemtemplate>
                                                                                                </ItemTemplate>
                                                                                                <ItemStyle Width="5%" HorizontalAlign="Center" />
                                                                                                <HeaderStyle Width="5%" HorizontalAlign="Center" />
                                                                                            </asp:TemplateField>
                                                                                            <asp:TemplateField HeaderText="Document Name" HeaderStyle-Font-Bold="false">
                                                                                                <ItemTemplate><%#Eval("DocumentType") %> </ItemTemplate>
                                                                                                <HeaderStyle Width="15%" HorizontalAlign="Center" />
                                                                                                <ItemStyle Width="15%" HorizontalAlign="Center" />
                                                                                            </asp:TemplateField>
                                                                                            <%--
                                                                                                                <asp:TemplateField HeaderText="Document Name" SortExpression="DocName" HeaderStyle-Font-Bold="false">
                                                                                                                    <ItemTemplate><%#Eval("DOC_NAME") %> </ItemTemplate>
                                              <HeaderStyle Width="10%" HorizontalAlign="Center" />
                                              <ItemStyle Width="10%" HorizontalAlign="Center" />
                                              </asp:TemplateField>--%>
                                                                                            <asp:TemplateField HeaderText="Uploaded By" SortExpression="DocName" HeaderStyle-Font-Bold="false">
                                                                                                <ItemTemplate><%#Eval("UploadedBy") %> </ItemTemplate>
                                                                                                <HeaderStyle Width="10%" HorizontalAlign="Center" />
                                                                                                <ItemStyle Width="10%" HorizontalAlign="Center" />
                                                                                            </asp:TemplateField>
                                                                                            <asp:TemplateField HeaderText="Attached On" SortExpression="INSDATE" HeaderStyle-Font-Bold="false">
                                                                                                <ItemTemplate><%-- <%#Eval("INSDATE") %>--%><%# ((DateTime)Eval("INSDATE")).ToString("dd-MM-yyyy") %> </ItemTemplate>
                                                                                                <HeaderStyle Width="10%" HorizontalAlign="Center" />
                                                                                                <ItemStyle Width="10%" HorizontalAlign="Center" />
                                                                                            </asp:TemplateField>
                                                                                            <%-- <asp:TemplateField HeaderText="Pages" HeaderStyle-Font-Bold="false">
                                                <HeaderStyle Width="10%" HorizontalAlign="Center" />
                                                <ItemStyle Width="10%" HorizontalAlign="Center" />
                                              </asp:TemplateField>--%>
                                                                                            <asp:TemplateField HeaderText="View" HeaderStyle-Font-Bold="false">
                                                                                                <ItemTemplate>
                                                                                                    <a href="#" class="gridViewToolTip" id="btnModalPopup" onclick='showPdfBYHendler("<%# Eval("FILE_PATH")%>","<%# Eval("docType")%>","<%# Eval("EREG_ID")%>")'>
                                                                                                        <button type="button" class="btn btn-info">
                                                                                                            <i class="fas fa-eye"></i>
                                                                                                        </button>
                                                                                                    </a><%-- 
                                                                                                                        <a href="#" class="gridViewToolTip" id="btnModalPopup" onclick='openPopup("<%# Eval("FILE_PATH")%>")'> <button type="button" class="btn btn-info">
                                                    <i class="fas fa-eye"></i>
                                                  </button>
                                                  </a>--%>
                                                                                                </ItemTemplate>
                                                                                                <HeaderStyle Width="10%" HorizontalAlign="Center" />
                                                                                                <ItemStyle Width="10%" HorizontalAlign="Center" />
                                                                                            </asp:TemplateField>
                                                                                        </Columns>
                                                                                        <EditRowStyle BackColor="#999999" />
                                                                                        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                                                                        <HeaderStyle BackColor="#e9564e" Font-Bold="false" ForeColor="White" />
                                                                                        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                                                                        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                                                                        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                                                                        <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                                                                        <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                                                                        <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                                                                        <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                                                                                    </asp:GridView>
                                                                                </div>
                                                                                <div id="pnl3" style="display: none">
                                                                                    <%--
                                                                                                        <embed id="ifrDisplay" class="embed-responsive-item" height='750'>--%>
                                                                                    <iframe id="ifrDisplay" class="embed-responsive-item" height='750'></iframe>
                                                                                </div>
                                                                                <%--
                                                                                                        <button type="button" class="btn btn-success float-right" data-toggle="modal" data-target="#exampleModalCenter">Attach </button>--%>
                                                                            </div>
                                                                            <div class="tab-pane fade active show" id="custom-tabs-four-profile" role="tabpanel" aria-labelledby="custom-tabs-four-profile-tab">
                                                                                <div id="Recent_DocPdfShowTab">
                                                                                    <div style="height: 754px; overflow-y: auto">

                                                                                        <iframe id="RecentdocPath" runat="server" clientidmode="Static" width='550' height='750'></iframe>

                                                                                        <iframe id="RecentProposalDoc" runat="server" width='550' height='750'></iframe>

                                                                                        <iframe id="RecentAttachedDoc" runat="server" width='550' height='750'></iframe>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                            <div class="tab-pane fade active show" id="custom-tabs-four-messages" role="tabpanel" aria-labelledby="custom-tabs-four-messages-tab">
                                                                                <div id="All_DocPdfShowTab">
                                                                                    <%-- 
                                                                                                            <iframe id="AlldocPath" runat="server" width='550' height='750'></iframe>
                                                                                                            <iframe src="~/Documents/sample.pdf" runat="server" width='550' height='750'></iframe>--%>
                                                                                </div>
                                                                            </div>
                                                                            <div class="tab-pane fade " id="custom-tabs-four-settings" role="tabpanel" aria-labelledby="custom-tabs-four-settings-tab">
                                                                                <div>
                                                                                    <img src="../dist/img/Doc_Sample_pic.jpg" style="width: 708px;" />
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
                        <textarea id="inputDescription3" class="form-control" rows="4"></textarea>
                    </div>
                    <div class="form-group">
                        <label for="inputEstimatedBudget">Additional Document Submitted by </label>
                        <input type="number" id="inputEstimatedBudget" class="form-control">
                    </div>
                    <div class="form-group">
                        <label for="inputEstimatedBudget">Document Name </label>
                        <input type="number" id="inputEstimatedBudget1" class="form-control">
                    </div>
                    <div class="form-group">
                        <input type="file" id="inputEstimatedBudget2" class="">
                    </div>
                </div>
                <div class="modal-footer">
                    <%-- 
                                                        <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>--%>
                    <button type="button" class="btn btn-primary">Add</button>
                </div>
            </div>
        </div>
    </div>
    <asp:Button ID="Button1" runat="server" Visible="false" OnClick="Button1_Click" Text="Formate 1" />
    <asp:Button ID="Button2" runat="server" Visible="false" OnClick="Button2_Click" Text="formate 2" />
    <asp:Button ID="Button3" runat="server" Visible="false" OnClick="Button2_Click1" Text="test" />
    <asp:Panel ID="pnlContents" Visible="false" runat="server">
        <div class="main-box" style="width: 70%; margin: 0 auto; text-align: center; padding: 60px 60px 200px 60px; margin-top: 50px;">
            <h2 style="font-size: 32px; margin: 0;">
                <asp:Label ID="lblOfficeHeading" Text="" Font-Bold="true" runat="server"></asp:Label>
            </h2>
            <h2 style="font-size: 24px; margin: 10px;">Form-I </h2>
            <h2 style="font-size: 22px; margin-top: 10px;">[See rule 5 (1)] </h2>
            <div class="section">
                <div class="point-1">
                    <h2 style="font-size: 20px; margin-top: 50px; text-align: left; margin-left: 15px; background: #cccccc70; padding: 5px; width: 99%; letter-spacing: 0.2px; box-shadow: 0 0 4px rgba(0,0,0,0.4">1. Name and Address of Executants ( Seller / Doner / Releasor / Leaser )</h2>
                    <asp:PlaceHolder ID="PlaceHolder2" runat="server"></asp:PlaceHolder>
                </div>
                <div class="point-2">
                    <h2 style="font-size: 20px; margin-top: 50px; text-align: left; margin-left: 15px; background: #cccccc70; padding: 5px; width: 99%; letter-spacing: 0.2px; box-shadow: 0 0 4px rgba(0,0,0,0.4">2. Name and Address of Claimants ( Buyer / Recipient / Releasee / Lessee )</h2>
                    <asp:PlaceHolder ID="PlaceHolder1" runat="server"></asp:PlaceHolder>
                </div>
                <div class="point-2">
                    <h2 style="font-size: 20px; margin-top: 50px; text-align: left; margin-left: 15px; background: #cccccc70; padding: 5px; width: 99%; letter-spacing: 0.2px; box-shadow: 0 0 4px rgba(0,0,0,0.4">3. Case Details</h2>
                    <table style='width: 100%; margin-left: 20px; margin-top: -40px; border: 1px solid black; border-collapse: collapse; margin-top: 25px; font-family: sans-serif; margin-left: 2%;'>
                        <tr>
                            <th style="border-collapse: collapse; border: 1px solid black; width: 50%">Proposal No.</th>
                            <th style="border-collapse: collapse; border: 1px solid black; width: 50%">
                                <asp:Label ID="lblRegInitEStampID_P" Text="" runat="server"></asp:Label>
                            </th>
                        </tr>
                        <tr>
                            <td style='border: 1px solid black; padding: 5px 15px;'>
                                <asp:Label ID="lblProposalId_P" Text="" runat="server"></asp:Label>
                            </td>
                            <td style='border: 1px solid black; padding: 5px 15px;'>
                                <asp:Label ID="lblPropertyRegNoInitIdEStampId_P" Text="" runat="server"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </div>
                <div id="pr_d">
                    <h2 style="font-size: 20px; margin-top: 50px; text-align: left; margin-left: 15px; background: #cccccc70; padding: 5px; width: 99%; letter-spacing: 0.2px; box-shadow: 0 0 4px rgba(0,0,0,0.4">4. Proposal Details</h2>
                    <table style="width: 100%; border: 1px solid black; text-align: left; border-collapse: collapse; margin-top: 50px; font-family: sans-serif; margin-left: 2%;">
                        <tbody>
                            <tr>
                                <th style="border: 1px solid black; border-collapse: collapse; line-height: 25px; height: 40px; padding: 0 1px 0 30px;">Sr. No.</th>
                                <th style="border: 1px solid black; border-collapse: collapse; line-height: 25px; height: 40px; padding: 0 1px 0 50px;">Particulars</th>
                                <th style="border: 1px solid black; border-collapse: collapse; line-height: 25px; height: 40px; padding: 0 1px 0 30px;">Details</th>
                            </tr>
                            <tr>
                                <td style="border: 1px solid black; border-collapse: collapse; line-height: 25px; padding: 0 50px 0 50px; font-weight: bold">1</td>
                                <td style="border: 1px solid black; border-collapse: collapse; line-height: 25px; padding: 0 50px 0 50px; font-weight: bold">Date of Executants</td>
                                <td style="border: 1px solid black; border-collapse: collapse; line-height: 25px; padding: 0 50px 0 50px;">
                                    <b style="white-space: nowrap; height: 50px; display: flex; justify-content: left; align-items: center; font-weight: normal;">
                                        <asp:Label ID="lblDateofExecution_P" Text="" runat="server"></asp:Label>
                                    </b>
                                </td>
                            </tr>
                            <tr>
                                <td style="border: 1px solid black; border-collapse: collapse; line-height: 25px; padding: 0 50px 0 50px; font-weight: bold">2</td>
                                <td style="border: 1px solid black; border-collapse: collapse; line-height: 25px; padding: 0 50px 0 50px; font-weight: bold">Date of Presentation</td>
                                <td style="border: 1px solid black; border-collapse: collapse; line-height: 25px; padding: 0 50px 0 50px;">
                                    <b style="white-space: nowrap; height: 50px; display: flex; justify-content: left; align-items: center; font-weight: normal">
                                        <asp:Label ID="lblDateofPresent_P" Text="" runat="server"></asp:Label>
                                    </b>
                                </td>
                            </tr>
                            <tr>
                                <td style="border: 1px solid black; border-collapse: collapse; line-height: 25px; padding: 0 50px 0 50px; font-weight: bold">3</td>
                                <td style="border: 1px solid black; border-collapse: collapse; line-height: 25px; padding: 0 50px 0 50px; font-weight: bold">Nature of Document</td>
                                <td style="border: 1px solid black; border-collapse: collapse; line-height: 25px; padding: 0 50px 0 50px;">
                                    <b style="white-space: nowrap; height: 50px; display: flex; justify-content: left; align-items: center; font-weight: normal">
                                        <asp:Label ID="lblNatureDoc_P" Text="" runat="server"></asp:Label>
                                    </b>
                                </td>
                            </tr>
                            <tr>
                                <td style="border: 1px solid black; border-collapse: collapse; line-height: 25px; padding: 0 50px 0 50px; font-weight: bold">4</td>
                                <td style="border: 1px solid black; border-collapse: collapse; line-height: 25px; padding: 0 50px 0 50px; font-weight: bold">Duty paid on the document</td>
                                <td style="border: 1px solid black; border-collapse: collapse; line-height: 25px; padding: 0 50px 0 50px;">
                                    <b style="white-space: nowrap; height: 50px; display: flex; justify-content: left; align-items: center; font-weight: normal">
                                        <asp:Label ID="lblStamDuty_P" Text="" runat="server"></asp:Label>
                                    </b>
                                </td>
                            </tr>
                            <tr>
                                <td style="border: 1px solid black; border-collapse: collapse; line-height: 25px; padding: 0 50px 0 50px; font-weight: bold">5</td>
                                <td style="border: 1px solid black; border-collapse: collapse; line-height: 25px; padding: 0 50px 0 50px; font-weight: bold">Nature of the document and duty chargeable their upon as in the opinion of the Registering Officer</td>
                                <td style="border: 1px solid black; border-collapse: collapse; line-height: 25px; padding: 0 50px 0 50px;">
                                    <b style="white-space: nowrap; height: 50px; display: flex; justify-content: left; align-items: center; font-weight: normal">
                                        <asp:Label ID="lblNatureDocRegOff_P" Text="" runat="server"></asp:Label>
                                        &nbsp;and &nbsp;
                                        <asp:Label ID="lblProRecStmapDuty_P" Text="" runat="server"></asp:Label>
                                    </b>
                                </td>
                            </tr>
                            <tr>
                                <td style="border: 1px solid black; border-collapse: collapse; line-height: 25px; padding: 0 50px 0 50px; font-weight: bold">6</td>
                                <td style="border: 1px solid black; border-collapse: collapse; line-height: 25px; padding: 0 50px 0 50px; font-weight: bold">Basis of duty calculation by Registering officer. </td>
                                <td style="border: 1px solid black; border-collapse: collapse; line-height: 25px; padding: 0 50px 0 50px;">
                                    <b style="white-space: nowrap; height: 50px; display: flex; justify-content: left; align-items: center; font-weight: normal"></b>
                                </td>
                            </tr>
                            <tr>
                                <td style="border: 1px solid black; border-collapse: collapse; line-height: 25px; padding: 0 50px 0 50px; font-weight: bold">7</td>
                                <td style="border: 1px solid black; border-collapse: collapse; line-height: 25px; padding: 0 50px 0 50px; font-weight: bold">Deficit duty as opinioned by the Registering Officer </td>
                                <td style="border: 1px solid black; border-collapse: collapse; line-height: 25px; padding: 0 50px 0 50px;">
                                    <b style="white-space: nowrap; height: 50px; display: flex; justify-content: left; align-items: center; font-weight: normal">
                                        <asp:Label ID="lblDeficitDuty_P" Text="" runat="server"></asp:Label>
                                    </b>
                                </td>
                            </tr>
                            <tr>
                                <td style="border: 1px solid black; border-collapse: collapse; line-height: 25px; padding: 0 50px 0 50px; font-weight: bold">8</td>
                                <td style="border: 1px solid black; border-collapse: collapse; line-height: 25px; padding: 0 50px 0 50px; font-weight: bold">Remarks (If any) </td>
                                <td style="border: 1px solid black; border-collapse: collapse; line-height: 25px; padding: 0 50px 0 50px;">
                                    <b style="white-space: nowrap; height: 50px; display: flex; justify-content: left; align-items: center; font-weight: normal"><%--
                   <asp:Label ID="lblStampDutyRemark" Text="" runat="server"></asp:Label>--%>
                                        <asp:Label ID="lblSRComments_P" Text="" runat="server"></asp:Label>
                                    </b>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </div>
            </div>
            <div style="text-align: left; font-weight: bold; padding: 20px;">
                <p class="note" style="text-align: left">
                    <span style="text-align: left; text-align: left; width: auto; float: left;">Note: Reference section 17 of Indian Stamp act 1899 says on instrument chargeable with duty and executed by any person in India shall stamped before or at the time of execution</span>
                </p>
            </div>
            <div style="display: flex; justify-content: space-between; margin-top: 100px; font-weight: bold;">
                <div>
                    <p style="font-size: 19px; line-height: 30px; margin-top: 20px; text-align: justify;">Place................................... </p>
                    <p style="font-size: 19px; line-height: 30px; margin-top: 20px; text-align: justify;">Date.................................... </p>
                </div>
                <div>
                    <p style="font-size: 19px; line-height: 30px; margin-top: 20px; text-align: justify;">
                        Name of Registering Officer:
                        <asp:Label ID="lblSRName_P" Text="" runat="server"></asp:Label>
                    </p>
                    <p style="font-size: 19px; line-height: 30px; margin-top: 20px; text-align: justify;">
                        <asp:Label ID="lblSROName_P" Text="" Font-Bold="true" runat="server"></asp:Label>
                    </p>
                </div>
            </div>
        </div>
    </asp:Panel>
    <asp:Panel ID="Pcontent" Visible="false" runat="server">
        <div>
            <div class="main-box" style="width: 97%; margin: 0 auto; padding: 25px; margin-top: 40px; padding-bottom: 200px; position: relative;">
                <div style="text-align: center;">
                    <h2 class="ps" style="display: inline-block; background: #d3d3d3; padding: 5px 15px; font-size: 22px; font-family: sans-serif; border-radius: 7px; margin: 0;">प्रस्ताव पत्र</h2>
                </div>
                <div class="newmain">
                    <h2 class="pd" style="background: #d3d3d3; padding: 5px 15px; margin: 0; font-size: 20px; font-family: sans-serif; margin-top: 25px;">पार्टी विवरण</h2>
                    <asp:PlaceHolder ID="PlaceHolder3" runat="server"></asp:PlaceHolder>
                    <h2 class="pd" style="background: #d3d3d3; padding: 5px 15px; margin: 0; font-size: 20px; font-family: sans-serif; margin-top: 50px;">परिबद्ध/ संदर्भ का आधार</h2>
                    <p style="width: 97%; margin-left: 15px; margin-top: 15px; font-family: sans-serif; font-size: 18px">
                        <asp:Label ID="lblReasonImpound_P2" runat="server"></asp:Label>
                    </p>
                    <h2 class="pd" style="background: #d3d3d3; padding: 5px 15px; margin: 0; font-size: 20px; font-family: sans-serif; margin-top: 50px;">संपत्ति ब्यौरा</h2>
                    <asp:PlaceHolder ID="PlaceHolder4" runat="server"></asp:PlaceHolder>
                    <h2 class="pd" style="background: #d3d3d3; padding: 5px 15px; margin: 0; font-size: 20px; font-family: sans-serif; margin-top: 50px;">प्रस्ताव विवरण</h2>
                    <table style="width: 100%; border: 1px solid black; border-collapse: collapse; margin-top: 25px; font-family: sans-serif;">
                        <tbody>
                            <tr>
                                <th style="border: 1px solid black; border-collapse: collapse; line-height: 25px;">
                                    <asp:Label ID="lblRegInitEStampID_P2" Text="" Font-Bold="true" runat="server"></asp:Label>
                                </th>
                                <asp:Panel ID="pnlRegInitDate_P2" Font-Bold="true" runat="server">
                                    <th style="border: 1px solid black; border-collapse: collapse; line-height: 25px; padding: 0 50px 0 50px;">
                                        <asp:Label ID="lblRegInitDate_P2" Font-Bold="true" Text="" runat="server"></asp:Label>
                                    </th>
                                </asp:Panel>

                                <th style="border: 1px solid black; border-collapse: collapse; line-height: 25px;">प्रस्तुति की तिथि</th>
                                <th style="border: 1px solid black; border-collapse: collapse; line-height: 25px;">निष्पादन की तिथि</th>
                            </tr>
                        </tbody>
                        <tbody>
                            <tr>
                                <td style="border: 1px solid black; border-collapse: collapse; padding: 5px 15px;">
                                    <p style="margin: 5px">
                                        <%--
                                                                            <asp:Label ID="lblPropertyRegNoInitId" Text="" Font-Bold="true" runat="server"></asp:Label>--%>
                                        <asp:Label ID="lblPropertyRegNoInitIdEStampId_P2" Text="" Font-Bold="true" runat="server"></asp:Label>
                                    </p>
                                </td>
                                <asp:Panel ID="pnlRegNoInitDate_P2" runat="server">
                                    <td style="border: 1px solid black; border-collapse: collapse; padding: 5px 15px;">
                                        <p style="margin: 5px">
                                            <asp:Label ID="lblProRegDt_P2" Font-Bold="true" Text="" runat="server"></asp:Label><%--
                                                                                <asp:Label ID="lblRegInitDate" Text="" Font-Bold="true" runat="server"></asp:Label>--%>
                                        </p>
                                    </td>
                                </asp:Panel>
                                <td style="border: 1px solid black; border-collapse: collapse; padding: 5px 15px;">
                                    <p style="margin: 5px">
                                        <asp:Label ID="lblDateofPresent_P2" Text="" Font-Bold="true" runat="server"></asp:Label>
                                    </p>
                                </td>

                                <td style="border: 1px solid black; border-collapse: collapse; padding: 5px 15px;">
                                    <p style="margin: 5px">
                                        <asp:Label ID="lblDateofExecution_P2" Text="" Font-Bold="true" runat="server"></asp:Label>
                                    </p>
                                </td>

                            </tr>
                        </tbody>
                    </table>
                    <table style="width: 100%; border: 1px solid black; border-collapse: collapse; margin-top: 25px; font-family: sans-serif; table-layout: fixed;">
                        <tbody>
                            <tr>
                                <th style="border: 1px solid black; border-collapse: collapse; line-height: 25px; width: 20%;">विवरण</th>
                                <th style="border: 1px solid black; border-collapse: collapse; line-height: 25px; width: 20%;">पार्टी द्वारा प्रस्तुत दस्तावेज़ का विवरण</th>
                                <th style="border: 1px solid black; border-collapse: collapse; line-height: 25px; padding: 0 50px 0 50px; width: 20%;">पंजीयन अधिकारी का प्रस्ताव</th>
                                <th style="border: 1px solid black; border-collapse: collapse; line-height: 25px; width: 10%;">शुल्क/परिवर्तन में कमी</th>
                                <%-- <th style="border: 1px solid black; border-collapse: collapse; line-height: 25px;">Penality</th>--%>
                                <th style="border: 1px solid black; border-collapse: collapse; line-height: 25px; width: 30%;">रिमार्क</th>
                            </tr>
                        </tbody>
                        <tbody>
                            <tr>
                                <td style="border: 1px solid black; border-collapse: collapse; padding: 5px 15px;">
                                    <p style="margin: 5px">
                                        दस्तावेज का स्वरूप
                                        <asp:Label ID="Label7" Text="" Font-Bold="true" runat="server"></asp:Label>
                                    </p>
                                </td>
                                <td style="border: 1px solid black; border-collapse: collapse; padding: 5px 15px;">
                                    <p style="margin: 5px">
                                        <asp:Label ID="lblNatureDoc_P2" Text="" Font-Bold="true" runat="server"></asp:Label>
                                    </p>
                                </td>
                                <td style="border: 1px solid black; border-collapse: collapse; padding: 5px 15px;">
                                    <p style="margin: 5px">
                                        <asp:Label ID="lblNatureDocRegOff_P2" Text="" Font-Bold="true" runat="server"></asp:Label>
                                    </p>
                                </td>
                                <td style="border: 1px solid black; border-collapse: collapse; padding: 5px 15px;">
                                    <p style="margin: 5px">
                                        <asp:Label ID="lblNatureDocDeficit_P2" Text="" Font-Bold="true" runat="server"></asp:Label>
                                    </p>
                                </td>
                                <%-- 
                                                                    <td style="border: 1px solid black; border-collapse: collapse; padding: 5px 15px;">
                                                                        <p style="margin: 5px">
                                                                            <asp:Label ID="lblNatureDocPenality_P2" Text="" Font-Bold="true" runat="server"></asp:Label>
                                                                        </p>
                                                                    </td>--%>
                                <td style="border: 1px solid black; border-collapse: collapse; padding: 5px 15px;">
                                    <p style="margin: 5px">
                                        <asp:Label ID="lblNatureDocRemark_P2" Text="" Font-Bold="true" runat="server"></asp:Label>
                                    </p>
                                </td>
                            </tr>
                            <tr>
                                <td style="border: 1px solid black; border-collapse: collapse; padding: 5px 15px;">
                                    <p style="margin: 5px">
                                        संपत्ति का प्रतिफल मूल्य(₹)
                                        <asp:Label ID="Label18" Text="" Font-Bold="true" runat="server"></asp:Label>
                                    </p>
                                </td>
                                <td style="border: 1px solid black; border-collapse: collapse; padding: 5px 15px;">
                                    <p style="margin: 5px">
                                        <asp:Label ID="lblConsidProperty_P2" Text="" Font-Bold="true" runat="server"></asp:Label>
                                    </p>
                                </td>
                                <td style="border: 1px solid black; border-collapse: collapse; padding: 5px 15px;">
                                    <p style="margin: 5px">
                                        <asp:Label ID="lblConsidPropertyRegOff_P2" Text="" Font-Bold="true" runat="server"></asp:Label>
                                    </p>
                                </td>
                                <td style="border: 1px solid black; border-collapse: collapse; padding: 5px 15px;">
                                    <p style="margin: 5px"></p>
                                </td>
                                <%--
                                                                    <td style="border: 1px solid black; border-collapse: collapse; padding: 5px 15px;">
                                                                        <p style="margin: 5px"></p>
                                                                    </td>--%>
                                <td style="border: 1px solid black; border-collapse: collapse; padding: 5px 15px;">
                                    <p style="margin: 5px">
                                        <asp:Label ID="lblConsidPropertyRemark_P2" Text="" Font-Bold="true" runat="server"></asp:Label>
                                    </p>
                                </td>
                            </tr>
                            <tr>
                                <td style="border: 1px solid black; border-collapse: collapse; padding: 5px 15px;">
                                    <p style="margin: 5px">
                                        संपत्ति का मार्गदर्शिका मूल्य (₹)
                                        <asp:Label ID="Label22" Text="" Font-Bold="true" runat="server"></asp:Label>&nbsp;(Sq ft.)
                                    </p>
                                </td>
                                <td style="border: 1px solid black; border-collapse: collapse; padding: 5px 15px;">
                                    <p style="margin: 5px">
                                        <asp:Label ID="lblGuideValue_P2" Text="" Font-Bold="true" runat="server"></asp:Label>
                                    </p>
                                </td>
                                <td style="border: 1px solid black; border-collapse: collapse; padding: 5px 15px;">
                                    <p style="margin: 5px">
                                        <asp:Label ID="lblGuideValueRegOff_P2" Text="" Font-Bold="true" runat="server"></asp:Label>
                                    </p>
                                </td>
                                <td style="border: 1px solid black; border-collapse: collapse; padding: 5px 15px;">
                                    <p style="margin: 5px">
                                        <asp:Label ID="lblGuideValueRegDefcit_P2" Text="" Font-Bold="true" runat="server"></asp:Label>
                                    </p>
                                </td>
                                <%-- 
                                                                    <td style="border: 1px solid black; border-collapse: collapse; padding: 5px 15px;">
                                                                        <p style="margin: 5px">
                                        ₹
                                            
                                                                            <asp:Label ID="lblGuideValuePenality_P2" Text="" Font-Bold="true" runat="server"></asp:Label>
                                                                        </p>
                                                                    </td>--%>
                                <td style="border: 1px solid black; border-collapse: collapse; padding: 5px 15px;">
                                    <p style="margin: 5px">
                                        <asp:Label ID="lblGuideValueRemark_P2" Text="" Font-Bold="true" runat="server"></asp:Label>
                                    </p>
                                </td>
                            </tr>
                            <tr>
                                <td class="forset" style="border: 1px solid black; border-collapse: collapse; padding: 5px 15px;">
                                    <p style="margin-bottom: 0">स्टाम्प शुल्क विभाजन:</p>
                                    <br />
                                    <p style="margin: 0">जनपद (₹) </p>
                                    <br />
                                    <p style="margin: 0">उपकर (₹) </p>
                                    <br />
                                    <p style="margin: 0">नगरीय (₹) </p>
                                    <br />
                                    <p style="margin: 0">मूल (₹) </p>
                                </td>
                                <td class="forset" style="border: 1px solid black; border-collapse: collapse; padding: 5px 15px;">
                                    <p style="margin-bottom: 0">&nbsp;</p>
                                    <br />
                                    <p style="margin: 0">
                                        <asp:Label ID="lblStampDutyClassJanpad_P2" Text="" Font-Bold="true" runat="server"></asp:Label>
                                    </p>
                                    <br />
                                    <p style="margin: 0">
                                        <asp:Label ID="lblStampDutyClassUpkar_P2" Text="" Font-Bold="true" runat="server"></asp:Label>
                                    </p>
                                    <br />
                                    <p style="margin: 0">
                                        <asp:Label ID="lblStampDutyClassMuncipal_P2" Text="" Font-Bold="true" runat="server"></asp:Label>
                                    </p>
                                    <br />
                                    <p style="margin: 0">
                                        <asp:Label ID="lblStampDutyClassPrinciple_P2" Text="" Font-Bold="true" runat="server"></asp:Label>
                                    </p>
                                </td>
                                <td class="forset" style="border: 1px solid black; border-collapse: collapse; padding: 5px 15px;">
                                    <p style="margin-bottom: 0">&nbsp;</p>
                                    <br />
                                    <p style="margin: 0">
                                        <asp:Label ID="lblProClassJanpad_P2" Text="" Font-Bold="true" runat="server"></asp:Label>
                                    </p>
                                    <br />
                                    <p style="margin: 0">
                                        <asp:Label ID="lblProClassUpkar_P2" Text="" Font-Bold="true" runat="server"></asp:Label>
                                    </p>
                                    <br />
                                    <p style="margin: 0">
                                        <asp:Label ID="lblProClassMuncipal_P2" Text="" Font-Bold="true" runat="server"></asp:Label>
                                    </p>
                                    <br />
                                    <p style="margin: 0">
                                        <asp:Label ID="lblProClassPrinciple_P2" Text="" Font-Bold="true" runat="server"></asp:Label>
                                    </p>
                                </td>
                                <td class="forset" style="border: 1px solid black; border-collapse: collapse; padding: 5px 15px;">
                                    <p style="margin-bottom: 0">&nbsp;</p>
                                    <br />
                                    <p style="margin: 0">
                                        <asp:Label ID="lblDeficitJanpad_P2" Text="" Visible="false" Font-Bold="true" runat="server"></asp:Label>
                                    </p>
                                    <br />
                                    <p style="margin: 0">
                                        <asp:Label ID="lblDeficitUpkar_P2" Text="" Visible="false" Font-Bold="true" runat="server"></asp:Label>
                                    </p>
                                    <br />
                                    <p style="margin: 0">
                                        <asp:Label ID="lblDeficitMuncipal_P2" Text="" Visible="false" Font-Bold="true" runat="server"></asp:Label>
                                    </p>
                                    <br />
                                    <p style="margin: 0">
                                        <asp:Label ID="lblDeficitPrinciple_P2" Text="" Visible="false" Font-Bold="true" runat="server"></asp:Label>
                                    </p>
                                </td>
                                <td class="forset" style="border: 1px solid black; border-collapse: collapse; padding: 5px 15px;">
                                    <p style="margin-bottom: 0">&nbsp;</p>
                                    <br />
                                    <p style="margin: 0">
                                        <asp:Label ID="lblPenalityJanpad_P2" Text="" Visible="false" Font-Bold="true" runat="server"></asp:Label>
                                    </p>
                                    <br />
                                    <p style="margin: 0">
                                        <asp:Label ID="lblPenalityUpkar_P2" Text="" Visible="false" Font-Bold="true" runat="server"></asp:Label>
                                    </p>
                                    <br />
                                    <p style="margin: 0">
                                        <asp:Label ID="lblPenalityMuncipal_P2" Text="" Visible="false" Font-Bold="true" runat="server"></asp:Label>
                                    </p>
                                    <br />
                                    <p style="margin: 0">
                                        <asp:Label ID="lblPenalityPrinciple_P2" Text="" Visible="false" Font-Bold="true" runat="server"></asp:Label>
                                    </p>
                                </td>
                                <%-- 
                                                                    <td style="border: 1px solid black; border-collapse: collapse; padding: 5px 15px;">
                                                                        <p style="margin: 5px"></p>
                                                                    </td>--%>
                            </tr>
                            <tr>
                                <td style="border: 1px solid black; border-collapse: collapse; padding: 5px 15px;">
                                    <p style="margin: 5px">
                                        कुल स्टांप शुल्क  (₹) <%--
                                                                            <asp:Label ID="lblStampDutyPenality_P2" Text="" Font-Bold="true" runat="server"></asp:Label>--%>
                                    </p>
                                </td>
                                <td style="border: 1px solid black; border-collapse: collapse; padding: 5px 15px;">
                                    <p style="margin: 5px">
                                        <asp:Label ID="lblTotalStampDuty_p2" Text="" Font-Bold="true" runat="server"></asp:Label>
                                    </p>
                                </td>
                                <td style="border: 1px solid black; border-collapse: collapse; padding: 5px 15px;">
                                    <p style="margin: 5px">
                                        <asp:Label ID="lblTotalStampDutyRO_p2" Text="" Font-Bold="true" runat="server"></asp:Label>
                                    </p>
                                </td>
                                <td style="border: 1px solid black; border-collapse: collapse; padding: 5px 15px;">
                                    <p style="margin: 5px">
                                        <asp:Label ID="Label6" Text="" Font-Bold="true" runat="server"></asp:Label>
                                    </p>
                                </td>
                                <td style="border: 1px solid black; border-collapse: collapse; padding: 5px 15px;">
                                    <p style="margin: 5px">
                                        <asp:Label ID="Label8" Text="" Font-Bold="true" runat="server"></asp:Label>
                                    </p>
                                </td>
                            </tr>
                            <tr>
                                <td class="forset" style="border: 1px solid black; border-collapse: collapse; padding: 5px 15px;">
                                    <p style="margin: 0">छूट प्राप्त राशि (₹)</p>
                                    <p style="margin: 0">पहले से भुगतान किया हुआ शुल्क (₹) </p>
                                </td>
                                <td class="forset" style="border: 1px solid black; border-collapse: collapse; padding: 5px 15px;">
                                    <p>
                                        <asp:Label ID="lblStampDutyExempted_P2" Text="" Font-Bold="true" runat="server"></asp:Label>
                                    </p>
                                    <p class="sd">
                                        <asp:Label ID="lblAlreadyPaidDuty_P2" Text="1" Font-Bold="true" runat="server"></asp:Label>
                                    </p>
                                </td>
                                <td class="forset" style="border: 1px solid black; border-collapse: collapse; padding: 5px 15px;">
                                    <p>
                                        <asp:Label ID="lblProStampDuty_P2" Text="" Font-Bold="true" runat="server"></asp:Label>
                                    </p>

                                    <p class="uty">
                                        <asp:Label ID="lblAlreadyPaidDutyRO_P2" Text="2" Font-Bold="true" runat="server"></asp:Label>
                                    </p>
                                </td>
                                <td class="forset" style="border: 1px solid black; border-collapse: collapse; padding: 5px 15px;">
                                    <p style="margin: 0">
                                        <asp:Label ID="Label30" Text="" Visible="false" Font-Bold="true" runat="server"></asp:Label>
                                    </p>
                                    <br />
                                    <p style="margin: 0">
                                        <asp:Label ID="Label31" Text="" Visible="false" Font-Bold="true" runat="server"></asp:Label>
                                    </p>
                                </td>
                                <td class="forset" style="border: 1px solid black; border-collapse: collapse; padding: 5px 15px;">
                                    <p style="margin: 0">
                                        <asp:Label ID="Label34" Text="" Visible="false" Font-Bold="true" runat="server"></asp:Label>
                                    </p>
                                    <br />
                                    <p style="margin: 0">
                                        <asp:Label ID="Label35" Text="" Visible="false" Font-Bold="true" runat="server"></asp:Label>
                                    </p>
                                </td>
                                <%-- 
                                                                    <td style="border: 1px solid black; border-collapse: collapse; padding: 5px 15px;">
                                                                        <p style="margin: 5px"></p>
                                                                    </td>--%>
                            </tr>
                            <tr>
                                <td style="border: 1px solid black; border-collapse: collapse; padding: 5px 15px;">
                                    <p style="margin: 5px">
                                        निवल स्टाम्प शुल्क (₹) <%--
                                                                            <asp:Label ID="lblStampDutyPenality_P2" Text="" Font-Bold="true" runat="server"></asp:Label>--%>
                                    </p>
                                </td>
                                <td style="border: 1px solid black; border-collapse: collapse; padding: 5px 15px;">
                                    <p style="margin: 5px">
                                        <asp:Label ID="lblStamDuty_P2" Text="" Font-Bold="true" runat="server"></asp:Label>
                                    </p>
                                </td>
                                <td style="border: 1px solid black; border-collapse: collapse; padding: 5px 15px;">
                                    <p style="margin: 5px">
                                        <asp:Label ID="lblProRecStmapDuty_P2" Text="" Font-Bold="true" runat="server"></asp:Label>
                                    </p>
                                </td>
                                <td style="border: 1px solid black; border-collapse: collapse; padding: 5px 15px;">
                                    <p style="margin: 5px">
                                        <asp:Label ID="lblDeficitDuty_P2" Text="" Font-Bold="true" runat="server"></asp:Label>
                                    </p>
                                </td>
                                <%--  
                                                                    <td style="border: 1px solid black; border-collapse: collapse; padding: 5px 15px;">
                                                                        <p style="margin: 5px">
                                        ₹
                                            
                                                                            <asp:Label ID="lblStampDutyPenality_P2" Text="" Font-Bold="true" runat="server"></asp:Label>
                                                                        </p>
                                                                    </td>--%>
                                <td style="border: 1px solid black; border-collapse: collapse; padding: 5px 15px;">
                                    <p style="margin: 5px">
                                        <asp:Label ID="lblStampDutyRemark_P2" Text="" Font-Bold="true" runat="server"></asp:Label>
                                    </p>
                                </td>
                            </tr>
                            <tr>
                                <td style="border: 1px solid black; border-collapse: collapse; padding: 5px 15px;">
                                    <p style="margin: 5px">
                                        पंजीयन शुल्क (₹)
                                        <asp:Label ID="Label50" Text="" Font-Bold="true" runat="server"></asp:Label>
                                    </p>
                                </td>
                                <td style="border: 1px solid black; border-collapse: collapse; padding: 5px 15px;">
                                    <p style="margin: 5px">
                                        <asp:Label ID="lblRegFee_P2" Text="" Font-Bold="true" runat="server"></asp:Label>
                                    </p>
                                </td>
                                <td style="border: 1px solid black; border-collapse: collapse; padding: 5px 15px;">
                                    <p style="margin: 5px">
                                        <asp:Label ID="lblRecoverRegfee_P2" Text="" Font-Bold="true" runat="server"></asp:Label>
                                    </p>
                                </td>
                                <td style="border: 1px solid black; border-collapse: collapse; padding: 5px 15px;">
                                    <p style="margin: 5px">
                                        <asp:Label ID="lblDeficitRegFee_P2" Text="" Font-Bold="true" runat="server"></asp:Label>
                                    </p>
                                </td>
                                <%-- 
                                                                    <td style="border: 1px solid black; border-collapse: collapse; padding: 5px 15px;">
                                                                        <p style="margin: 5px">
                                                                            <asp:Label ID="lblRegFeePenality_P2" Text="" Font-Bold="true" runat="server"></asp:Label>
                                                                        </p>
                                                                    </td>--%>
                                <td style="border: 1px solid black; border-collapse: collapse; padding: 5px 15px;">
                                    <p style="margin: 5px">
                                        <asp:Label ID="lblRegFeeRemark_P2" Text="" Font-Bold="true" runat="server"></asp:Label>
                                    </p>
                                </td>
                            </tr>
                            <tr>
                                <td style="border: 1px solid black; border-collapse: collapse; padding: 5px 15px;">
                                    <p style="margin: 5px">छूट प्राप्त राशि (₹)</p>
                                    <p style="margin: 5px">पहले से भुगतान किया हुआ शुल्क (₹)</p>
                                </td>
                                <td style="border: 1px solid black; border-collapse: collapse; padding: 5px 15px;">
                                    <p>
                                        <asp:Label ID="lblEXRegParty" Text="" Font-Bold="true" runat="server"></asp:Label>
                                    </p>

                                    <p>
                                        <asp:Label ID="lblEXRegParty12" Text="" Font-Bold="true" runat="server"></asp:Label>
                                    </p>
                                </td>
                                <td style="border: 1px solid black; border-collapse: collapse; padding: 5px 15px;">
                                    <p style="">
                                        <asp:Label ID="lblEXRegSR" Text="" Font-Bold="true" runat="server"></asp:Label>
                                    </p>

                                    <p style="">
                                        <asp:Label ID="lblEXRegSR13" Text="" Font-Bold="true" runat="server"></asp:Label>
                                    </p>
                                </td>
                                <td style="border: 1px solid black; border-collapse: collapse; padding: 5px 15px;">
                                    <p style="margin: 5px"></p>
                                </td>
                                <%-- 
                                                                    <td style="border: 1px solid black; border-collapse: collapse; padding: 5px 15px;">
                                                                        <p style="margin: 5px">
                                                                            <asp:Label ID="lblRegFeePenality_P2" Text="" Font-Bold="true" runat="server"></asp:Label>
                                                                        </p>
                                                                    </td>--%>
                                <td style="border: 1px solid black; border-collapse: collapse; padding: 5px 15px;">
                                    <p style="margin: 5px">
                                        <asp:Label ID="Label5" Text="" Font-Bold="true" runat="server"></asp:Label>
                                    </p>
                                </td>
                            </tr>
                            <tr>
                                <td style="border: 1px solid black; border-collapse: collapse; padding: 5px 15px;">
                                    <p style="margin: 5px">निवल पंजीयन फीस (₹) </p>
                                </td>
                                <td style="border: 1px solid black; border-collapse: collapse; padding: 5px 15px;">
                                    <p style="margin: 5px">
                                        <asp:Label ID="lblNetRegParty" Text="" Font-Bold="true" runat="server"></asp:Label>
                                    </p>
                                </td>
                                <td style="border: 1px solid black; border-collapse: collapse; padding: 5px 15px;">
                                    <p style="margin: 5px">
                                        <asp:Label ID="lblNetRegSR" Text="" Font-Bold="true" runat="server"></asp:Label>
                                    </p>
                                </td>
                                <td style="border: 1px solid black; border-collapse: collapse; padding: 5px 15px;">
                                    <p style="margin: 5px">
                                        <asp:Label ID="lblNetRegDeficit" Text="" Font-Bold="true" runat="server"></asp:Label>
                                    </p>
                                </td>
                                <%-- 
                                                                    <td style="border: 1px solid black; border-collapse: collapse; padding: 5px 15px;">
                                                                        <p style="margin: 5px">
                                        ₹
                                            
                                                                            <asp:Label ID="lblRegFeePenality_P2" Text="" Font-Bold="true" runat="server"></asp:Label>
                                                                        </p>
                                                                    </td>--%>
                                <td style="border: 1px solid black; border-collapse: collapse; padding: 5px 15px;">
                                    <p style="margin: 5px">
                                        <asp:Label ID="Label11" Text="" Font-Bold="true" runat="server"></asp:Label>
                                    </p>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                    <h2 class="pd" style="background: #d3d3d3; padding: 5px 15px; margin: 0; font-size: 20px; font-family: sans-serif; margin-top: 50px;">
                        <asp:Label ID="lblHeading_P2" runat="server"></asp:Label>
                    </h2>
                    <table style="width: 100%; border: 1px solid black; border-collapse: collapse; margin-top: 25px; font-family: sans-serif;">
                        <tbody>
                            <tr>
                                <th style="border: 1px solid black; border-collapse: collapse; line-height: 25px;">एस आर ओ आई -डी  </th>
                                <th style="border: 1px solid black; border-collapse: collapse; line-height: 25px; padding: 0 50px 0 50px;">उप रजिस्ट्रार का नाम </th>
                                <th style="border: 1px solid black; border-collapse: collapse; line-height: 25px;">एस आर ओ का नाम </th>
                                <th style="border: 1px solid black; border-collapse: collapse; line-height: 25px;">प्रस्ताव आईडी
                                    <asp:Label ID="lblDepName" runat="server"></asp:Label>
                                </th>
                                <th style="border: 1px solid black; border-collapse: collapse; line-height: 25px;">परिवद्ध करने कि तिथि </th>
                                <asp:Panel ID="pnlAuditIdDate_P2" runat="server" Visible="false">
                                    <th style="border: 1px solid black; border-collapse: collapse; line-height: 25px;">अंकेक्षण आई -डी</th>
                                    <th style="border: 1px solid black; border-collapse: collapse; line-height: 25px;">अंकेक्षण तिथि</th>
                                </asp:Panel>
                                <th style="border: 1px solid black; border-collapse: collapse; line-height: 25px; width: 100px;">शीर्ष  </th>
                                <th style="border: 1px solid black; border-collapse: collapse; line-height: 25px; width: 100px;">धारा</th>
                            </tr>
                        </tbody>
                        <tbody>
                            <tr>
                                <td style="border: 1px solid black; border-collapse: collapse; padding: 5px 15px;">
                                    <asp:Label ID="lblSROID_P2" Text="" runat="server"></asp:Label>
                                </td>
                                <td style="border: 1px solid black; border-collapse: collapse; padding: 5px 15px;">
                                    <p style="margin: 5px">
                                        <asp:Label ID="lblSRName_P2" Text="" runat="server"></asp:Label>
                                    </p>
                                </td>
                                <td style="border: 1px solid black; border-collapse: collapse; padding: 5px 15px;">
                                    <p style="margin: 5px">
                                        <asp:Label ID="lblSROName_P2" Text="" runat="server"></asp:Label>
                                    </p>
                                </td>
                                <td style="border: 1px solid black; border-collapse: collapse; padding: 5px 15px;">
                                    <p style="margin: 5px">
                                        <asp:Label ID="lblProposalId_P2" Text="" runat="server"></asp:Label>
                                    </p>
                                </td>
                                <td style="border: 1px solid black; border-collapse: collapse; padding: 5px 15px;">
                                    <p style="margin: 5px">
                                        <asp:Label ID="lblProposalDate_P2" Text="" runat="server"></asp:Label>
                                    </p>
                                </td>
                                <asp:Panel ID="pnlAuditIdandDate_P2" runat="server" Visible="false">
                                    <td style="border: 1px solid black; border-collapse: collapse; padding: 5px 15px;">
                                        <p style="margin: 5px">
                                            <asp:Label ID="lblAuditId_P2" Text="" runat="server"></asp:Label>
                                        </p>
                                    </td>
                                    <td style="border: 1px solid black; border-collapse: collapse; padding: 5px 15px;">
                                        <p style="margin: 5px">
                                            <asp:Label ID="lblAuditDt_P2" Text="" runat="server"></asp:Label>
                                        </p>
                                    </td>
                                </asp:Panel>
                                <td style="border: 1px solid black; border-collapse: collapse; padding: 5px 15px;">
                                    <p style="margin: 5px">
                                        <asp:Label ID="lblHeadbySR" Text="" runat="server"></asp:Label>
                                    </p>
                                </td>
                                <td style="border: 1px solid black; border-collapse: collapse; padding: 5px 15px;">
                                    <p style="margin: 5px">
                                        <asp:Label ID="lblSecbySR" Text="" runat="server"></asp:Label>
                                    </p>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                    <table style="width: 100%; border: 1px solid black; border-collapse: collapse; margin-top: 25px; font-family: sans-serif;">
                        <tbody>
                            <tr>
                                <th style="border: 1px solid black; border-collapse: collapse; text-align: left; line-height: 25px;">
                                    <asp:Label ID="lblOfficeName_P2" runat="server"></asp:Label>
                                </th>
                            </tr>
                        </tbody>
                        <tbody>
                            <tr>
                                <td style="border: 1px solid black; border-collapse: collapse; padding: 5px 15px;">
                                    <p style="margin: 5px">
                                        <asp:Label ID="lblSRComments_P2" Text="" runat="server"></asp:Label>
                                    </p>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </div>
            </div>
        </div>
    </asp:Panel>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.6.4/jquery.min.js"></script>
    <asp:HiddenField runat="server" ClientIDMode="Static" ID="hdappid" />
    <asp:HiddenField runat="server" ClientIDMode="Static" ID="hdAppno" />
    <asp:HiddenField runat="server" ClientIDMode="Static" ID="hdTocan" />
    <asp:HiddenField ID="hdnbase64" ClientIDMode="Static" runat="server" />
    <script type="text/javascript">
        $(document).ready(function () {

            if ($("#hdnbase64").val().length > 0) {
                var base64Pdf = $("#hdnbase64").val();
                var binaryString = window.atob(base64Pdf);
                var len = binaryString.length;
                var bytes = new Uint8Array(len);
                for (var i = 0; i < len; i++) {
                    bytes[i] = binaryString.charCodeAt(i);
                }
                var blob = new Blob([bytes], { type: 'application/pdf' });
                var url = URL.createObjectURL(blob);
                $('#RecentdocPath').attr('src', url);
            }
            BindTable();
            LoadPdfFile();
            ShowMessage();


            // BindOrderSheet();
        });


        function BindTable() {

            //alert("111");
            var id = "#tbl";
            var appid = document.getElementById("hdappid").value;
            var Appno = document.getElementById("hdAppno").value;
            var jasonData = '{ "appid": ' + appid + ', "Appno": "' + Appno + '" }';
            $.ajax({
                type: "POST",

                url: '<%=Page.ResolveClientUrl("~/CoS/AcceptRejectCases_details.aspx/DocFile") %>',
                data: jasonData,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {

                    $("#dynamic-table tbody").empty();
                    var json = JSON.parse(msg.d);
                    //var ResponceData = JSON.parse(data.d);

                    //for (var i = 0; i < json.; i++) {
                    //    var row = '<tr><td align="center">' + json[i].DOC_NAME + '</td> <td align="center">' + json[i].FILE_PATH + ' </td> <td align="center">' + json[i].File_Path + '</td></tr>'
                    //    $("#dynamic-table tbody").append(row);<td align="center"><a href="' + url + '" target="_blank" >Download</a></td>
                    //}
                    var index = 1;


                    var output = json.map(i => "<tr><td>" + index++ + "</td><td>" + i.DOC_NAME + "</td><a><td>" + i.FILE_PATH + "</a></td></tr>");


                    $("#dynamic-table tbody").append(output);


                },
                error: function (e) {
                    // alert("Error");
                    console.log(e);


                }


            });

        }

        function showPdfBYHendler(PROPOSALPATH_FIRSTFORMATE, docType, EREG_ID) {


            $("#pnl2").hide();
            $("#pnl3").show();
            var Tocan = document.getElementById("hdTocan").value;

            var jasonData = '{"PDFPath": "' + PROPOSALPATH_FIRSTFORMATE + '","DocType": "' + docType + '","EREG_ID": "' + EREG_ID + '","Tocan": "' + Tocan + '" }';
            $.ajax({
                type: "POST",
                url: '<%=Page.ResolveClientUrl("~/CoS/AcceptRejectCases_details.aspx/show_Pdf_BY_Hendler_Deed_Doc") %>',

                data: jasonData,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {

                    /* console.log("Data :-"+msg.d);*/

                    var base64Pdf = msg.d;
                    if (base64Pdf.length > 0) {
                        var binaryString = window.atob(base64Pdf);
                        var len = binaryString.length;
                        var bytes = new Uint8Array(len);
                        for (var i = 0; i < len; i++) {
                            bytes[i] = binaryString.charCodeAt(i);
                        }

                        // Create Blob object
                        var blob = new Blob([bytes], { type: 'application/pdf' });

                        // Create object URL
                        var url = URL.createObjectURL(blob);

                        // Set iframe src attribute to the object URL
                        $('#ifrDisplay').attr('src', url);
                        $('#ifrDisplay').attr('enabled', 'enabled');
                    }
                    else {
                        $("#pnl3").hide();
                        Swal.fire({
                            icon: 'info',
                            title: 'No Document Attached',
                            //showCancelButton: true,
                            confirmButtonText: 'OK',
                        })
                        base64Pdf = "";
                    }




                },
                error: function (xhr, status, error) {
                    console.log("error " + error);
                    console.log("status " + error);
                    console.log("xhr " + xhr);
                    //LoadNotice();
                }
            });


        }




        function ShowMessageHeadSection() {
            var head = document.getElementById('hdnfldHead').value;
            var section = document.getElementById('hdnfldSection').value;
            var case_number = document.getElementById('hdnfldCase').value;
            //alert(case_number);
            Swal.fire({
                icon: 'success',
                title: 'Selected Head - ' + head + 'and Section' + section,
                showCancelButton: true,
                confirmButtonText: 'OK',
            }).then((result) => {
                if (result.value) {
                    /*window.location = 'Ordersheet.aspx?Case_Number=' + case_number;*/
                    /* setTimeout( test() , 6000);*/
                } else {
                    alert('user click on Cancel button');
                }
            });
        }

        function test() {
            //alert("hi");
        }

        function test1() {
            // alert("hello");
        }

        function ShowMessageCaseReg() {
            var case_number = document.getElementById('hdnfldCase').value;
            var App_ID = document.getElementById('hdnAppID').value;
            //alert(case_number);
            Swal.fire({
                icon: 'success',
                title: 'Case Registered Successfully,<br/><br/> Case Number is : ' + case_number,
                showCancelButton: false,
                confirmButtonText: 'OK',
                customClass: "casefontcustm"
            }).then((result) => {
                if (result.value) {
                    AmagiLoader.show();
                    window.location = 'Ordersheet.aspx';
                } else {
                    AmagiLoader.show();
                    window.location = 'Ordersheet.aspx';
                }
            });
        }
    </script>
    <script type="text/javascript">
        function ShowDocList() {
            //alert("Hello");
            $("#pnl2").show();
            $("#pnl3").hide();
        }

        function validateSection() {
            //alert(document.getElementById("ddlSec1").value);
            if (document.getElementById("ddlSec1").value == "-Select-") {
                //Swal.fire('Please select section');
                Swal.fire({
                    icon: 'info',
                    title: 'Please select section'
                })
                return false;
            }
            AmagiLoader.show();
        }

        function openPopup(FILE_PATH) {
            //alert(FILE_PATH);
            $("#pnl2").hide();
            $("#pnl3").show();
            $('#ifrDisplay').attr('src', FILE_PATH);
        }

        function test2() {
            // alert("1");
        }

        function test3() {
            //alert("2");
        }
    </script>
</asp:Content>
