<%@ Page Title="" Language="C#" MasterPageFile="~/CoS/CoSMaster.Master" AutoEventWireup="true" CodeBehind="CosViewLegacyCase.aspx.cs" Inherits="CMS_Sampada.CoS.CosViewLegacyCase" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" type="text/css" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.css">

    <script src="https://code.jquery.com/jquery-3.6.4.min.js"></script>
    <link rel="stylesheet" href="https://code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css">
    <script src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"></script>



    <link href="../Styles/sweetalert.css" rel="stylesheet" />
    <script src="../Scripts/sweetalert.min.js"></script>
    <script src="../assets/js/3.3.1/sweetalert2@11.js"></script>
    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
    <script src="https://code.jquery.com/ui/1.12.1/jquery-ui.min.js"></script>
    <!-- SweetAlert for alerts -->
    <script src="https://cdn.jsdelivr.net/npm/sweetalert2@10"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

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
                                <li class="nav-item">
                                    <a class="nav-link active disabled" href="">Proposal</a>
                                </li>
                                <li class="nav-item">
                                    <a class="nav-link disabled" href="OrdersheetPending">Order Sheet</a>
                                </li>
                                <li class="nav-item">
                                    <a class="nav-link disabled" href="#Notice" data-toggle="tab">Notice</a>
                                </li>
                                <li class="nav-item">
                                    <a class="nav-link disabled" href="ReportSeeking">Seek Report</a>
                                </li>

                            </ul>
                        </div>



                        <div class="card-body">
                            <div class="col-md-12 d-flex">
                                <div class="tab-content" style="width: 100%;display:flex;">
                                    <%------------------------------------------------------------------------------------------%>
                                    <div class="tab-pane active" id="SendBack" style="width:50%;">
                                        <div class="col-md-12">
                                            <div class="">
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
                                                                            <h3 class="card-title">Case Details</h3>
                                                                            <div class="card-tools">
                                                                                <button type="button" class="btn btn-tool" data-card-widget="collapse">
                                                                                    <i class="fas fa-minus"></i>
                                                                                </button>
                                                                            </div>
                                                                        </div>
                                                                        <div class="card-body">
                                                                            <div class="table-responsive">
                                                                                <asp:GridView ID="GrdCaseRegDetails" runat="server" AutoGenerateColumns="false" Style="font-size: small; max-width: 100%;"
                                                                                    CssClass="table table-hover table-bordered table table-striped"
                                                                                    BorderWidth="1px" CellPadding="4" ForeColor="#484848" EmptyDataText="No Data Found">
                                                                                    <Columns>
                                                                                        <asp:TemplateField HeaderText="SNo." HeaderStyle-VerticalAlign="Top" HeaderStyle-Font-Bold="false" HeaderStyle-HorizontalAlign="Center">
                                                                                            <ItemTemplate>
                                                                                                <itemtemplate><%#Container.DataItemIndex+1  %> </itemtemplate>
                                                                                            </ItemTemplate>
                                                                                            <ItemStyle Width="2%" HorizontalAlign="Center" />
                                                                                            <HeaderStyle Width="2%" HorizontalAlign="Center" />
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Document No" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Top">
                                                                                            <ItemTemplate>
                                                                                                <div style="text-align: center">
                                                                                                    <asp:Label ID="lblDocument_no" runat="server" Text='<%# Eval("Document_No") %>'></asp:Label>
                                                                                                </div>
                                                                                            </ItemTemplate>

                                                                                        </asp:TemplateField>

                                                                                        <asp:TemplateField HeaderText="Impounded By" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Top">
                                                                                            <HeaderStyle CssClass="centered-header" />
                                                                                            <ItemTemplate>
                                                                                                <div style="text-align: center">
                                                                                                    <asp:Label ID="lblImpounded_By" runat="server" Text='<%# Eval("Impounded_By") %>'></asp:Label>
                                                                                                </div>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>


                                                                                        <asp:TemplateField HeaderText="Registered Document" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Top">
                                                                                            <ItemTemplate>
                                                                                                <div style="text-align: center">
                                                                                                    <asp:Label ID="lblRegistered_Document" runat="server" Text='<%# Eval("Registered_Document") %>'></asp:Label>
                                                                                                </div>
                                                                                            </ItemTemplate>

                                                                                        </asp:TemplateField>

                                                                                        <asp:TemplateField HeaderText="Reason for Impound" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Top">
                                                                                            <ItemTemplate>
                                                                                                <div style="text-align: center">
                                                                                                    <asp:Label ID="lblReason_for_Impound" runat="server" Text='<%# Eval("Reason_Of_Impounding") %>'></asp:Label>
                                                                                                </div>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>

                                                                                        <asp:TemplateField HeaderText="Deed Category" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Top">
                                                                                            <ItemTemplate>
                                                                                                <div style="text-align: center">

                                                                                                    <asp:Label ID="lblDeedCategory" runat="server" Text='<%# Eval("deed_category_name_en") %>'></asp:Label>
                                                                                                </div>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>

                                                                                        <asp:TemplateField HeaderText="Deed" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Top">
                                                                                            <ItemTemplate>
                                                                                                <div style="text-align: center">

                                                                                                    <asp:Label ID="lblDeed" runat="server" Text='<%# Eval("deed_type_name_en") %>'></asp:Label>
                                                                                                </div>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>

                                                                                        <asp:TemplateField HeaderText="Instrument" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Top">
                                                                                            <ItemTemplate>
                                                                                                <div style="text-align: center">

                                                                                                    <asp:Label ID="lblInstrument" runat="server" Text='<%# Eval("Instrument_name_En") %>'></asp:Label>
                                                                                                </div>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>



                                                                                    </Columns>

                                                                                    <FooterStyle BackColor="#FFFFCC" ForeColor="#330099" />
                                                                                    <HeaderStyle CssClass="custom-table" ForeColor="#3a87ad" Font-Size="14px" />
                                                                                    <PagerStyle BackColor="#FFFFCC" ForeColor="#330099" HorizontalAlign="Center" />
                                                                                    <RowStyle BackColor="White" ForeColor="#484848" Font-Bold="true" Font-Size="12px" />
                                                                                    <SortedAscendingCellStyle BackColor="#FEFCEB" />
                                                                                    <SortedAscendingHeaderStyle BackColor="#AF0101" />
                                                                                    <SortedDescendingCellStyle BackColor="#F6F0C0" />
                                                                                    <SortedDescendingHeaderStyle BackColor="#7E0000" />
                                                                                </asp:GridView>
                                                                            </div>

                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="col-md-12">
                                                                    <div class="card card-primary collapsed-card">
                                                                        <div class="card-header">
                                                                            <h3 class="card-title">Party Details</h3>
                                                                            <div class="card-tools">
                                                                                <button type="button" class="btn btn-tool" data-card-widget="collapse">
                                                                                    <i class="fas fa-plus"></i>
                                                                                </button>
                                                                            </div>
                                                                        </div>
                                                                        <div class="card-body">

                                                                            <table class="table table-striped table-bordered table-all-common">
                                                                                <asp:GridView ID="GrdPartyDetails" runat="server" AutoGenerateColumns="false" Style="font-size: small; max-width: 100%;" DataKeyNames="party_id"
                                                                                    CssClass="table table-hover table-bordered table table-striped" BorderWidth="1px" CellPadding="4" ForeColor="#484848" EmptyDataText="No Data found">
                                                                                    <Columns>
                                                                                        <asp:TemplateField HeaderText="SNo." HeaderStyle-VerticalAlign="Top" HeaderStyle-Font-Bold="false" HeaderStyle-HorizontalAlign="Center">
                                                                                            <ItemTemplate>
                                                                                                <itemtemplate><%#Container.DataItemIndex+1  %> </itemtemplate>
                                                                                            </ItemTemplate>
                                                                                            <ItemStyle Width="2%" HorizontalAlign="Center" />
                                                                                            <HeaderStyle Width="2%" HorizontalAlign="Center" />
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Party Name" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Top">
                                                                                            <ItemTemplate>
                                                                                                <div style="text-align: center">
                                                                                                    <asp:Label ID="lblPartyName" runat="server" Text='<%# Eval("Party_Name") %>'></asp:Label>
                                                                                                </div>
                                                                                            </ItemTemplate>

                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="ID" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Top" Visible="false">
                                                                                            <HeaderStyle CssClass="centered-header" />
                                                                                            <ItemTemplate>
                                                                                                <%# Container.DataItemIndex + 1%>
                                                                                                <asp:HiddenField ID="hdnPartyID" runat="server" Value='<%#Eval("party_id") %>'></asp:HiddenField>

                                                                                            </ItemTemplate>
                                                                                            <ItemStyle HorizontalAlign="Center" Width="3%" />
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Phone No" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Top">
                                                                                            <HeaderStyle CssClass="centered-header" />
                                                                                            <ItemTemplate>
                                                                                                <div style="text-align: center">
                                                                                                    <asp:Label ID="lblPhoneNo" runat="server" Text='<%# Eval("Mob_No") %>'></asp:Label>
                                                                                                </div>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>


                                                                                        <asp:TemplateField HeaderText="Email ID" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Top">
                                                                                            <ItemTemplate>
                                                                                                <div style="text-align: center">
                                                                                                    <asp:Label ID="lblEmailID" runat="server" Text='<%# Eval("Email_Id") %>'></asp:Label>
                                                                                                </div>
                                                                                            </ItemTemplate>

                                                                                        </asp:TemplateField>


                                                                                        <asp:TemplateField HeaderText="Father/Husband/Guardian Name" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Top">
                                                                                            <ItemTemplate>
                                                                                                <div style="text-align: center">
                                                                                                    <asp:Label ID="lblFatherName" runat="server" Text='<%# Eval("PARTY_FATHER_OR_HUSBAND_OR_GUARDIAN_NAME") %>'></asp:Label>
                                                                                                </div>
                                                                                            </ItemTemplate>

                                                                                        </asp:TemplateField>

                                                                                        <asp:TemplateField HeaderText="PartyType" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Top">
                                                                                            <ItemTemplate>
                                                                                                <div style="text-align: center">
                                                                                                    <asp:Label ID="lblPartyType" runat="server" Text='<%# Eval("PARTY_TYPE_NAME_EN") %>'></asp:Label>
                                                                                                </div>
                                                                                            </ItemTemplate>

                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Qwner/Applicant Type" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Top">
                                                                                            <ItemTemplate>
                                                                                                <div style="text-align: center">
                                                                                                    <asp:Label ID="lblType" runat="server" Text='<%# Eval("APPLICANT_TYPE_NAME_EN") %>'></asp:Label>
                                                                                                </div>
                                                                                            </ItemTemplate>

                                                                                        </asp:TemplateField>

                                                                                        <asp:TemplateField HeaderText="Address" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Top">
                                                                                            <ItemTemplate>
                                                                                                <div style="text-align: center">
                                                                                                    <asp:Label ID="lblAddress" runat="server" Text='<%# Eval("Party_Address") %>'></asp:Label>
                                                                                                </div>
                                                                                            </ItemTemplate>

                                                                                        </asp:TemplateField>


                                                                                    </Columns>

                                                                                    <FooterStyle BackColor="#FFFFCC" ForeColor="#330099" />
                                                                                    <HeaderStyle CssClass="custom-table" ForeColor="#3a87ad" Font-Size="14px" />
                                                                                    <PagerStyle BackColor="#FFFFCC" ForeColor="#330099" HorizontalAlign="Center" />
                                                                                    <RowStyle BackColor="White" ForeColor="#484848" Font-Bold="true" Font-Size="12px" />
                                                                                    <SortedAscendingCellStyle BackColor="#FEFCEB" />
                                                                                    <SortedAscendingHeaderStyle BackColor="#AF0101" />
                                                                                    <SortedDescendingCellStyle BackColor="#F6F0C0" />
                                                                                    <SortedDescendingHeaderStyle BackColor="#7E0000" />
                                                                                </asp:GridView>
                                                                            </table>

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

                                                                                <table class="table table-striped table-bordered table-all-common">
                                                                                    <asp:GridView ID="GrdPropertyDetails" runat="server" AutoGenerateColumns="false" Style="font-size: small; max-width: 100%;" DataKeyNames="Property_ID"
                                                                                        CssClass="table table-hover table-bordered table table-striped" BorderWidth="1px" CellPadding="4" ForeColor="#484848" EmptyDataText="no data found">

                                                                                        <Columns>
                                                                                            <asp:TemplateField HeaderText="Sno." HeaderStyle-VerticalAlign="top" HeaderStyle-Font-Bold="false" HeaderStyle-HorizontalAlign="center">
                                                                                                <ItemTemplate>
                                                                                                    <itemtemplate><%#Container.DataItemIndex+1  %> </itemtemplate>
                                                                                                </ItemTemplate>
                                                                                                <ItemStyle Width="2%" HorizontalAlign="center" />
                                                                                                <HeaderStyle Width="2%" HorizontalAlign="center" />
                                                                                            </asp:TemplateField>
                                                                                            <asp:TemplateField HeaderText="Property Id" HeaderStyle-HorizontalAlign="center" HeaderStyle-VerticalAlign="top">
                                                                                                <ItemTemplate>
                                                                                                    <div style="text-align: center">
                                                                                                        <asp:Label ID="lblPropertyId" runat="server" Text='<%# Eval("Property_Given_ID") %>'></asp:Label>
                                                                                                    </div>
                                                                                                </ItemTemplate>

                                                                                            </asp:TemplateField>
                                                                                            <asp:TemplateField HeaderText="PropertySequenceId" HeaderStyle-HorizontalAlign="center" HeaderStyle-VerticalAlign="top" Visible="false">
                                                                                                <HeaderStyle CssClass="centered-header" />
                                                                                                <ItemTemplate>
                                                                                                    <%# Container.DataItemIndex + 1%>
                                                                                                    <asp:HiddenField ID="hdnPropertyId" runat="server" Value='<%# Eval("Property_ID") %>'></asp:HiddenField>

                                                                                                </ItemTemplate>
                                                                                                <ItemStyle HorizontalAlign="center" Width="3%" />
                                                                                            </asp:TemplateField>
                                                                                            <asp:TemplateField HeaderText="Property Type" HeaderStyle-HorizontalAlign="center" HeaderStyle-VerticalAlign="top">
                                                                                                <HeaderStyle CssClass="centered-header" />
                                                                                                <ItemTemplate>
                                                                                                    <div style="text-align: center">
                                                                                                        <asp:Label ID="lblPropertyType" runat="server" Text='<%# Eval("Property_Type") %>'></asp:Label>
                                                                                                    </div>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>


                                                                                            <asp:TemplateField HeaderText="Property Sub Type" HeaderStyle-HorizontalAlign="center" HeaderStyle-VerticalAlign="top">
                                                                                                <ItemTemplate>
                                                                                                    <div style="text-align: center">
                                                                                                        <asp:Label ID="lblPropertySubType" runat="server" Text='<%# Eval("Property_SubType") %>'></asp:Label>
                                                                                                    </div>
                                                                                                </ItemTemplate>

                                                                                            </asp:TemplateField>


                                                                                            <asp:TemplateField HeaderText="Transacting Partial Property Area" HeaderStyle-HorizontalAlign="center" HeaderStyle-VerticalAlign="top">
                                                                                                <ItemTemplate>
                                                                                                    <div style="text-align: center">
                                                                                                        <asp:Label ID="lblTransactingPartialArea" runat="server" Text='<%# Eval("Transacting_Partial_Area_Of_Property") %>'></asp:Label>
                                                                                                    </div>
                                                                                                </ItemTemplate>

                                                                                            </asp:TemplateField>

                                                                                            <asp:TemplateField HeaderText="Total Transacting Property Area" HeaderStyle-HorizontalAlign="center" HeaderStyle-VerticalAlign="top">
                                                                                                <ItemTemplate>
                                                                                                    <div style="text-align: center">
                                                                                                        <asp:Label ID="lblPropertyTransactingArea" runat="server" Text='<%# Eval("Property_Transacting_Area") %>'></asp:Label>
                                                                                                    </div>
                                                                                                </ItemTemplate>

                                                                                            </asp:TemplateField>


                                                                                            <asp:TemplateField HeaderText="Total Property Area" HeaderStyle-HorizontalAlign="center" HeaderStyle-VerticalAlign="top">
                                                                                                <ItemTemplate>
                                                                                                    <div style="text-align: center">
                                                                                                        <asp:Label ID="lblTotalPropertyArea" runat="server" Text='<%# Eval("Property_Area") %>'></asp:Label>
                                                                                                    </div>
                                                                                                </ItemTemplate>

                                                                                            </asp:TemplateField>
                                                                                            <asp:TemplateField HeaderText="Property Address" HeaderStyle-HorizontalAlign="center" HeaderStyle-VerticalAlign="top">
                                                                                                <ItemTemplate>
                                                                                                    <div style="text-align: center">
                                                                                                        <asp:Label ID="lblPropertyAddress" runat="server" Text='<%# Eval("Property_Address") %>'></asp:Label>
                                                                                                    </div>
                                                                                                </ItemTemplate>

                                                                                            </asp:TemplateField>


                                                                                        </Columns>

                                                                                        <FooterStyle BackColor="#ffffcc" ForeColor="#330099" />
                                                                                        <HeaderStyle CssClass="custom-table" ForeColor="#3a87ad" Font-Size="14px" />
                                                                                        <PagerStyle BackColor="#ffffcc" ForeColor="#330099" HorizontalAlign="center" />
                                                                                        <RowStyle BackColor="white" ForeColor="#484848" Font-Bold="true" Font-Size="12px" />
                                                                                        <SortedAscendingCellStyle BackColor="#fefceb" />
                                                                                        <SortedAscendingHeaderStyle BackColor="#af0101" />
                                                                                        <SortedDescendingCellStyle BackColor="#f6f0c0" />
                                                                                        <SortedDescendingHeaderStyle BackColor="#7e0000" />
                                                                                    </asp:GridView>
                                                                                </table>

                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="col-md-12">
                                                                    <div class="card card-primary collapsed-card">
                                                                        <div class="card-header">
                                                                            <h3 class="card-title">Proposal Dates</h3>
                                                                            <div class="card-tools">
                                                                                <button type="button" class="btn btn-tool" data-card-widget="collapse">
                                                                                    <i class="fas fa-plus"></i>
                                                                                </button>
                                                                            </div>
                                                                        </div>
                                                                        <div class="card-body">
                                                                            <div class="table-responsive">
                                                                                <table class="table table-striped table-bordered table-all-common">
                                                                                    <asp:GridView ID="GrdProposalDates" runat="server" AutoGenerateColumns="false" Style="font-size: small; max-width: 100%;"
                                                                                        CssClass="table table-hover table-bordered table table-striped" BorderWidth="1px" CellPadding="4" ForeColor="#484848" EmptyDataText="No Data found">
                                                                                        <Columns>

                                                                                            <asp:TemplateField HeaderText="Registry Initiation Date" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Top">
                                                                                                <ItemTemplate>
                                                                                                    <div style="text-align: center">
                                                                                                        <asp:Label ID="lblRegistryInitiationDate" runat="server" Text='<%# Eval("registry_initiation_date") %>'></asp:Label>
                                                                                                    </div>
                                                                                                </ItemTemplate>

                                                                                            </asp:TemplateField>

                                                                                            <asp:TemplateField HeaderText="Execution Date" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Top">
                                                                                                <HeaderStyle CssClass="centered-header" />
                                                                                                <ItemTemplate>
                                                                                                    <div style="text-align: center">
                                                                                                        <asp:Label ID="lblExecutionDate" runat="server" Text='<%# Eval("date_of_execution") %>'></asp:Label>
                                                                                                    </div>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>


                                                                                            <asp:TemplateField HeaderText="Date of Presentation" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Top">
                                                                                                <ItemTemplate>
                                                                                                    <div style="text-align: center">
                                                                                                        <asp:Label ID="lblPresentationDate" runat="server" Text='<%# Eval("date_of_presentation") %>'></asp:Label>
                                                                                                    </div>
                                                                                                </ItemTemplate>

                                                                                            </asp:TemplateField>

                                                                                            <asp:TemplateField HeaderText="Document Registration Date" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Top">
                                                                                                <ItemTemplate>
                                                                                                    <div style="text-align: center">
                                                                                                        <asp:Label ID="lblDocumentRegistrationDate" runat="server" Text='<%# Eval("property_reg_date") %>'></asp:Label>
                                                                                                    </div>
                                                                                                </ItemTemplate>

                                                                                            </asp:TemplateField>
                                                                                            <asp:TemplateField HeaderText="RegisteredDocumentNo" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Top">
                                                                                                <ItemTemplate>
                                                                                                    <div style="text-align: center">
                                                                                                        <asp:Label ID="lblRegisteredDocumentNo" runat="server" Text='<%# Eval("DOCUMENT_REG_NO") %>'></asp:Label>
                                                                                                    </div>
                                                                                                </ItemTemplate>

                                                                                            </asp:TemplateField>

                                                                                        </Columns>

                                                                                        <FooterStyle BackColor="#FFFFCC" ForeColor="#330099" />
                                                                                        <HeaderStyle CssClass="custom-table" ForeColor="#3a87ad" Font-Size="14px" />
                                                                                        <PagerStyle BackColor="#FFFFCC" ForeColor="#330099" HorizontalAlign="Center" />
                                                                                        <RowStyle BackColor="White" ForeColor="#484848" Font-Bold="true" Font-Size="12px" />
                                                                                        <SortedAscendingCellStyle BackColor="#FEFCEB" />
                                                                                        <SortedAscendingHeaderStyle BackColor="#AF0101" />
                                                                                        <SortedDescendingCellStyle BackColor="#F6F0C0" />
                                                                                        <SortedDescendingHeaderStyle BackColor="#7E0000" />
                                                                                    </asp:GridView>
                                                                                </table>
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
                                                                        <div class="card-body">
                                                                            <div class="table-responsive">
                                                                                <table class="table  table-bordered">
                                                                                    <thead>
                                                                                        <tr>
                                                                                            <th>Particulars</th>
                                                                                            <th>Details of Document as Presented by Party</th>
                                                                                            <th>Proposal of Registering Officer</th>
                                                                                            <th>Deficit Duty/ Variation</th>
                                                                                            <th>Remark</th>
                                                                                        </tr>
                                                                                    </thead>
                                                                                    <tbody>
                                                                                        <tr>
                                                                                            <td>Nature of Document</td>
                                                                                            <td>

                                                                                                <asp:TextBox ID="txtNatureDocParty" data-type="party" runat="server" ClientIDMode="AutoID" ReadOnly="true" CssClass="form-control bg-white" placeholder=""></asp:TextBox>


                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:TextBox ID="txtNatureDocRegOff" data-type="regOff" runat="server" ClientIDMode="AutoID" ReadOnly="true" CssClass="form-control bg-white" placeholder=""></asp:TextBox>

                                                                                            </td>
                                                                                            <td>
                                                                                                <%-- <asp:textbox id="txtnaturedocdeficit"  data-type="deficit"  readonly="true"   runat="server" clientidmode="autoid" ReadOnly="true" CssClass="form-control bg-white" placeholder=""></asp:textbox>--%></td>

                                                                                            <td>
                                                                                                <asp:TextBox ID="txtNatureDocRemark" runat="server" ReadOnly="true" CssClass="form-control bg-white" placeholder=""></asp:TextBox></td>
                                                                                        </tr>

                                                                                        <tr>
                                                                                            <td>Consideration Value (₹)</td>
                                                                                            <td>
                                                                                                <asp:TextBox ID="txtConsiderationParty" data-type="party" runat="server" ClientIDMode="AutoID" onchange="calculateDifference(this.id);" ReadOnly="true" CssClass="form-control bg-white bg-white" placeholder="" onkeypress="return isNumber(event);"></asp:TextBox></td>
                                                                                            <td>
                                                                                                <asp:TextBox ID="txtConsiderationRegOff" data-type="regOff" runat="server" ClientIDMode="AutoID" onchange="calculateDifference(this.id);" ReadOnly="true" CssClass="form-control bg-white" placeholder="" onkeypress="return isNumber(event);"></asp:TextBox></td>
                                                                                            <td>
                                                                                                <asp:TextBox ID="txtConsiderationDeficit" data-type="deficit" runat="server" ClientIDMode="AutoID" ReadOnly="true" CssClass="form-control bg-white" placeholder=""></asp:TextBox></td>

                                                                                            <td>
                                                                                                <asp:TextBox ID="txtConsiderationRemark" runat="server" ReadOnly="true" CssClass="form-control bg-white" placeholder=""></asp:TextBox></td>
                                                                                        </tr>

                                                                                        <tr>
                                                                                            <td>Guideline Value of Property (₹)</td>
                                                                                            <td>
                                                                                                <asp:TextBox ID="txtGuidePropertyParty" data-type="party" runat="server" ClientIDMode="AutoID" onchange="calculateDifference(this.id);" ReadOnly="true" CssClass="form-control bg-white" placeholder="" onkeypress="return isNumber(event);"></asp:TextBox></td>
                                                                                            <td>
                                                                                                <asp:TextBox ID="txtGuidePropertyRegOff" data-type="regOff" runat="server" ClientIDMode="AutoID" onchange="calculateDifference(this.id);" ReadOnly="true" CssClass="form-control bg-white" placeholder="" onkeypress="return isNumber(event);"></asp:TextBox></td>
                                                                                            <td>
                                                                                                <asp:TextBox ID="txtGuidePropertyDeficit" data-type="deficit" runat="server" ClientIDMode="AutoID" ReadOnly="true" CssClass="form-control bg-white" placeholder="" onkeypress="return isNumber(event);"></asp:TextBox></td>
                                                                                            <td>
                                                                                                <asp:TextBox ID="txtGuidePropertyRemark" runat="server" ReadOnly="true" CssClass="form-control bg-white" placeholder=""></asp:TextBox></td>
                                                                                        </tr>

                                                                                        <tr>
                                                                                            <td>Stamp Duty Bifurcation</td>
                                                                                            <td><%--<input type="text" placeholder="" class="form-control bg-white">--%></td>
                                                                                            <td></td>
                                                                                            <td></td>
                                                                                            <td></td>
                                                                                        </tr>

                                                                                        <tr>
                                                                                            <td>Principle (₹)</td>
                                                                                            <td>
                                                                                                <asp:TextBox ID="txtPrincipalParty" data-type="party" runat="server" ClientIDMode="AutoID" onchange="calculateDifference(this.id);" ReadOnly="true" CssClass="form-control bg-white" placeholder="" onkeypress="return isNumber(event);"></asp:TextBox></td>
                                                                                            <td>
                                                                                                <asp:TextBox ID="txtPrincipalRegOff" data-type="regOff" runat="server" ClientIDMode="AutoID" onchange="calculateDifference(this.id);" ReadOnly="true" CssClass="form-control bg-white" placeholder="" onkeypress="return isNumber(event);"></asp:TextBox></td>
                                                                                            <td>
                                                                                                <asp:TextBox ID="txtPrincipalDeficit" data-type="deficit" runat="server" ClientIDMode="AutoID" ReadOnly="true" CssClass="form-control bg-white" placeholder="" onkeypress="return isNumber(event);"></asp:TextBox></td>
                                                                                            <td>
                                                                                                <asp:TextBox ID="txtPrincipalRemark" runat="server" ReadOnly="true" CssClass="form-control bg-white" placeholder=""></asp:TextBox></td>
                                                                                        </tr>


                                                                                        <tr>
                                                                                            <td>Muncipal (₹)</td>
                                                                                            <td>
                                                                                                <asp:TextBox ID="txtMuncipalParty" data-type="party" runat="server" ClientIDMode="AutoID" onchange="calculateDifference(this.id);" ReadOnly="true" CssClass="form-control bg-white" placeholder="" onkeypress="return isNumber(event);"></asp:TextBox></td>
                                                                                            <td>
                                                                                                <asp:TextBox ID="txtMuncipalRegOff" data-type="regOff" runat="server" ClientIDMode="AutoID" onchange="calculateDifference(this.id);" ReadOnly="true" CssClass="form-control bg-white" placeholder="" onkeypress="return isNumber(event);"></asp:TextBox></td>
                                                                                            <td>
                                                                                                <asp:TextBox ID="txtMuncipalDeficit" data-type="deficit" runat="server" ClientIDMode="AutoID" ReadOnly="true" CssClass="form-control bg-white" placeholder="" onkeypress="return isNumber(event);"></asp:TextBox></td>
                                                                                            <td>
                                                                                                <asp:TextBox ID="txtMuncipalRemark" runat="server" ReadOnly="true" CssClass="form-control bg-white" placeholder=""></asp:TextBox></td>
                                                                                        </tr>


                                                                                        <tr>
                                                                                            <td>Janpad (₹)</td>
                                                                                            <td>
                                                                                                <asp:TextBox ID="txtJanpadParty" data-type="party" runat="server" ClientIDMode="AutoID" onchange="calculateDifference(this.id);" ReadOnly="true" CssClass="form-control bg-white" placeholder="" onkeypress="return isNumber(event);"></asp:TextBox></td>
                                                                                            <td>
                                                                                                <asp:TextBox ID="txtJanpadRegOff" data-type="regOff" runat="server" ClientIDMode="AutoID" onchange="calculateDifference(this.id);" ReadOnly="true" CssClass="form-control bg-white" placeholder="" onkeypress="return isNumber(event);"></asp:TextBox></td>
                                                                                            <td>
                                                                                                <asp:TextBox ID="txtJanpadDeficit" data-type="deficit" runat="server" ClientIDMode="AutoID" ReadOnly="true" CssClass="form-control bg-white" placeholder="" onkeypress="return isNumber(event);"></asp:TextBox></td>
                                                                                            <td>
                                                                                                <asp:TextBox ID="txtJanpadRemark" runat="server" ReadOnly="true" CssClass="form-control bg-white" placeholder=""></asp:TextBox></td>
                                                                                        </tr>

                                                                                        <tr>
                                                                                            <td>Upkar (₹)</td>
                                                                                            <td>
                                                                                                <asp:TextBox ID="txtUpkarParty" data-type="party" runat="server" ClientIDMode="AutoID" onchange="calculateDifference(this.id);" ReadOnly="true" CssClass="form-control bg-white" placeholder="" onkeypress="return isNumber(event);"></asp:TextBox></td>
                                                                                            <td>
                                                                                                <asp:TextBox ID="txtUpkarRegOff" data-type="regOff" runat="server" ClientIDMode="AutoID" onchange="calculateDifference(this.id);" ReadOnly="true" CssClass="form-control bg-white" placeholder="" onkeypress="return isNumber(event);"></asp:TextBox></td>
                                                                                            <td>
                                                                                                <asp:TextBox ID="txtUpkarDeficit" data-type="deficit" runat="server" ClientIDMode="AutoID" ReadOnly="true" CssClass="form-control bg-white" placeholder="" onkeypress="return isNumber(event);"></asp:TextBox></td>
                                                                                            <td>
                                                                                                <asp:TextBox ID="txtUpkarRemark" runat="server" ReadOnly="true" CssClass="form-control bg-white" placeholder=""></asp:TextBox></td>
                                                                                        </tr>

                                                                                        <tr>
                                                                                            <td>Total Stamp Duty (₹)</td>
                                                                                            <td>

                                                                                                <asp:Label ID="lblTLStampDutyParty" data-type="party" runat="server" ClientIDMode="AutoID" Text=""></asp:Label></td>
                                                                                            <td>
                                                                                                <asp:Label ID="lblTLStampDutyRegOff" data-type="regOff" runat="server" ClientIDMode="AutoID" Text=""></asp:Label></td>
                                                                                            <td>
                                                                                                <asp:Label ID="lblTLStampDutyDeficit" data-type="deficit" runat="server" ClientIDMode="AutoID" Text=""></asp:Label></td>
                                                                                            <td>
                                                                                                <asp:TextBox ID="txtTLStampDutyRemark" runat="server" ReadOnly="true" CssClass="form-control bg-white" placeholder=""></asp:TextBox>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td>Already paid Stamp Duty (₹)</td>
                                                                                            <td>
                                                                                                <asp:TextBox ID="txtPaidStampDutyParty" data-type="party" runat="server" ReadOnly="true" CssClass="form-control bg-white" placeholder="" onkeypress="return isNumber(event);"></asp:TextBox></td>
                                                                                            <td>
                                                                                                <asp:TextBox ID="txtPaidStampDutyRegOff" data-type="regOff" runat="server" ReadOnly="true" CssClass="form-control bg-white" placeholder="" onkeypress="return isNumber(event);"></asp:TextBox></td>
                                                                                            <td>
                                                                                                <asp:TextBox ID="txtPaidStampDutyDeficit" data-type="deficit" runat="server" ReadOnly="true" CssClass="form-control bg-white" placeholder="" onkeypress="return isNumber(event);"></asp:TextBox></td>
                                                                                            <td>
                                                                                                <asp:TextBox ID="txtPaidStampDutyRemark" runat="server" ReadOnly="true" CssClass="form-control bg-white" placeholder=""></asp:TextBox></td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td>Exempted Amount (₹)</td>
                                                                                            <td>
                                                                                                <asp:TextBox ID="txtExemptedAmtParty" data-type="party" runat="server" ReadOnly="true" CssClass="form-control bg-white" placeholder="" onkeypress="return isNumber(event);"></asp:TextBox></td>
                                                                                            <td>
                                                                                                <asp:TextBox ID="txtExemptedAmtRegOff" data-type="regOff" runat="server" ReadOnly="true" CssClass="form-control bg-white" placeholder="" onkeypress="return isNumber(event);"></asp:TextBox></td>
                                                                                            <td>
                                                                                                <asp:TextBox ID="txtExemptedAmtDeficit" data-type="deficit" runat="server" ReadOnly="true" CssClass="form-control bg-white" placeholder="" onkeypress="return isNumber(event);"></asp:TextBox></td>
                                                                                            <td>
                                                                                                <asp:TextBox ID="txtExemptedAmtRemark" runat="server" ReadOnly="true" CssClass="form-control bg-white" placeholder=""></asp:TextBox></td>

                                                                                            <tr>
                                                                                                <td>Net Stamp Duty (₹)</td>
                                                                                                <td>
                                                                                                    <asp:Label ID="lblNetStamDutyParty" data-type="party" runat="server" Text=""></asp:Label></td>
                                                                                                <td>
                                                                                                    <asp:Label ID="lblNetStamDutyRegOff" data-type="regOff" runat="server" Text=""></asp:Label></td>
                                                                                                <td>
                                                                                                    <asp:Label ID="lblNetStamDutyDeficit" data-type="deficit" runat="server"></asp:Label></td>
                                                                                                <td>
                                                                                                    <asp:TextBox ID="txtNetStamDutyRemark" runat="server" ReadOnly="true" CssClass="form-control bg-white" placeholder=""></asp:TextBox></td>

                                                                                            </tr>

                                                                                        <tr>
                                                                                            <td>Registration Fee Bifurcation</td>
                                                                                            <td><%--<input type="text" placeholder="" class="form-control bg-white">--%></td>
                                                                                            <td></td>
                                                                                            <td></td>
                                                                                            <td></td>
                                                                                        </tr>

                                                                                        <tr>
                                                                                            <td>Registration Fee (₹)</td>
                                                                                            <td>
                                                                                                <asp:TextBox ID="txtRegFeeParty" data-type="party" runat="server" ReadOnly="true" CssClass="form-control bg-white" placeholder="" onkeypress="return isNumber(event);"></asp:TextBox></td>
                                                                                            <td>
                                                                                                <asp:TextBox ID="txtRegFeeRegOff" data-type="regOff" runat="server" ReadOnly="true" CssClass="form-control bg-white" placeholder="" onkeypress="return isNumber(event);"></asp:TextBox></td>
                                                                                            <td>
                                                                                                <asp:TextBox ID="txtRegFeeDeficit" data-type="deficit" runat="server" ReadOnly="true" CssClass="form-control bg-white" placeholder="" onkeypress="return isNumber(event);"></asp:TextBox></td>
                                                                                            <td>
                                                                                                <asp:TextBox ID="txtRegFeeRemark" runat="server" ReadOnly="true" CssClass="form-control bg-white" placeholder=""></asp:TextBox></td>
                                                                                        </tr>

                                                                                        <tr>
                                                                                            <td>Already Paid Registration Fee (₹)</td>
                                                                                            <td>
                                                                                                <asp:TextBox ID="txtPaidRegFeeParty" data-type="party" runat="server" ReadOnly="true" CssClass="form-control bg-white" placeholder="" onkeypress="return isNumber(event);"></asp:TextBox></td>
                                                                                            <td>
                                                                                                <asp:TextBox ID="txtPaidRegFeeRegOff" data-type="regOff" runat="server" ReadOnly="true" CssClass="form-control bg-white" placeholder="" onkeypress="return isNumber(event);"></asp:TextBox></td>
                                                                                            <td>
                                                                                                <asp:TextBox ID="txtPaidRegFeeDeficit" data-type="deficit" runat="server" ReadOnly="true" CssClass="form-control bg-white" placeholder="" onkeypress="return isNumber(event);"></asp:TextBox></td>
                                                                                            <td>
                                                                                                <asp:TextBox ID="txtPaidRegFeeRemark" runat="server" ReadOnly="true" CssClass="form-control bg-white" placeholder=""></asp:TextBox></td>
                                                                                        </tr>

                                                                                        <tr>
                                                                                            <td>Exempted Amount (₹)</td>
                                                                                            <td>
                                                                                                <asp:TextBox ID="txtRegFeeExemtAmtParty" data-type="party" runat="server" ReadOnly="true" CssClass="form-control bg-white" placeholder="" onkeypress="return isNumber(event);"></asp:TextBox></td>
                                                                                            <td>
                                                                                                <asp:TextBox ID="txtRegFeeExemtAmtRegOff" data-type="regOff" runat="server" ReadOnly="true" CssClass="form-control bg-white" placeholder="" onkeypress="return isNumber(event);"></asp:TextBox></td>
                                                                                            <td>
                                                                                                <asp:TextBox ID="txtRegFeeExemtAmtDeficit" data-type="deficit" runat="server" ReadOnly="true" CssClass="form-control bg-white" placeholder="" onkeypress="return isNumber(event);"></asp:TextBox></td>
                                                                                            <td>
                                                                                                <asp:TextBox ID="txtRegFeeExemtAmtRemark" runat="server" ReadOnly="true" CssClass="form-control bg-white" placeholder=""></asp:TextBox></td>
                                                                                        </tr>

                                                                                        <tr>
                                                                                            <td>Net Registration Fees (₹)</td>
                                                                                            <td>
                                                                                                <asp:Label ID="lblNetRegFeeParty" data-type="party" runat="server" Text=""></asp:Label></td>
                                                                                            <td>
                                                                                                <asp:Label ID="lblNetRegFeeRegOff" data-type="regOff" runat="server" Text=""></asp:Label></td>
                                                                                            <td>
                                                                                                <asp:Label ID="lblNetRegFeeDeficit" data-type="deficit" runat="server" Text=""></asp:Label></td>
                                                                                            <td>
                                                                                                <asp:TextBox ID="txtNetRegFeeRemark" runat="server" ReadOnly="true" CssClass="form-control bg-white" Text=""></asp:TextBox></td>

                                                                                            <tr>
                                                                                                <td>Total (Stamp Duty+ Registration fees (₹)</td>
                                                                                                <td>
                                                                                                    <asp:Label ID="lblTotalParty" data-type="party" runat="server" Text=""></asp:Label></td>
                                                                                                <td>
                                                                                                    <asp:Label ID="lblTotalRegOff" data-type="regOff" runat="server" Text=""></asp:Label></td>
                                                                                                <td>
                                                                                                    <asp:Label ID="lblTotalDeficit" data-type="deficit" runat="server" Text=""></asp:Label></td>
                                                                                                <td>
                                                                                                    <asp:TextBox ID="txtTotalRemark" runat="server" ReadOnly="true" CssClass="form-control bg-white" placeholder=""></asp:TextBox></td>
                                                                                            </tr>
                                                                                    </tbody>
                                                                                </table>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>

                                                                <div class="col-md-12">
                                                                    <div class="card card-primary collapsed-card">
                                                                        <div class="card-header">
                                                                            <h3 class="card-title">Sub Registrar Details</h3>
                                                                            <div class="card-tools">
                                                                                <button type="button" class="btn btn-tool" data-card-widget="collapse">
                                                                                    <i class="fas fa-plus"></i>
                                                                                </button>
                                                                            </div>
                                                                        </div>
                                                                        <div class="card-body">
                                                                            <div class="table-responsive">
                                                                                <table class="table table-striped table-bordered table-all-common">

                                                                                    <asp:GridView ID="GrdSroDetails" runat="server" AutoGenerateColumns="false" Style="font-size: small; max-width: 100%;"
                                                                                        CssClass="table table-hover table-bordered table table-striped" BorderWidth="1px" CellPadding="4" ForeColor="#484848" EmptyDataText="No Data found">
                                                                                        <Columns>

                                                                                            <asp:TemplateField HeaderText="SROId" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Top">
                                                                                                <ItemTemplate>
                                                                                                    <div style="text-align: center">
                                                                                                        <asp:Label ID="lblSROId" runat="server" Text='<%# Eval("SRO_ID") %>'></asp:Label>
                                                                                                    </div>
                                                                                                </ItemTemplate>

                                                                                            </asp:TemplateField>

                                                                                            <asp:TemplateField HeaderText="SRO Name" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Top">
                                                                                                <HeaderStyle CssClass="centered-header" />
                                                                                                <ItemTemplate>
                                                                                                    <div style="text-align: center">
                                                                                                        <asp:Label ID="lblSROName" runat="server" Text='<%# Eval("OFFICE") %>'></asp:Label>
                                                                                                    </div>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>


                                                                                        </Columns>

                                                                                        <FooterStyle BackColor="#FFFFCC" ForeColor="#330099" />
                                                                                        <HeaderStyle CssClass="custom-table" ForeColor="#3a87ad" Font-Size="14px" />
                                                                                        <PagerStyle BackColor="#FFFFCC" ForeColor="#330099" HorizontalAlign="Center" />
                                                                                        <RowStyle BackColor="White" ForeColor="#484848" Font-Bold="true" Font-Size="12px" />
                                                                                        <SortedAscendingCellStyle BackColor="#FEFCEB" />
                                                                                        <SortedAscendingHeaderStyle BackColor="#AF0101" />
                                                                                        <SortedDescendingCellStyle BackColor="#F6F0C0" />
                                                                                        <SortedDescendingHeaderStyle BackColor="#7E0000" />
                                                                                    </asp:GridView>
                                                                                </table>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>


                                                                <div class="col-md-12">
                                                                    <div class="card card-primary collapsed-card">
                                                                        <div class="card-header">
                                                                            <h3 class="card-title">Case Status    </h3>
                                                                            <div class="card-tools">
                                                                                <button type="button" class="btn btn-tool" data-card-widget="collapse">
                                                                                    <i class="fas fa-plus"></i>
                                                                                </button>
                                                                            </div>
                                                                        </div>
                                                                        <div class="card-body">
                                                                            
                                                                            <div class="table-responsive">
                                                                                <asp:UpdatePanel ID="upnlcaseDetails" runat="server">
                                                                                    <ContentTemplate>
                                                                                        <table id="tbl2" class="table table-hover gid-view-tab table-bordered ">
                                                                                            <thead>
                                                                                                <tr>
                                                                                                    <th>No of Notice Send</th>
                                                                                                    <th>Date of Impound</th>
                                                                                                    <th>Next Hearing Date</th>
                                                                                                    <th>Year Case Registered</th>
                                                                                                    <th>Head</th>
                                                                                                    <th>Section</th>
                                                                                                </tr>
                                                                                            </thead>
                                                                                            <tbody>
                                                                                                <tr>
                                                                                                    <td>
                                                                                                        <asp:Label ID="lblNumNoticeSend" Text="" runat="server"></asp:Label>
                                                                                                    </td>
                                                                                                    <td>
                                                                                                        <asp:Label ID="lblImpoundDate" Text="" runat="server"></asp:Label>
                                                                                                    </td>
                                                                                                    <td>
                                                                                                        <asp:Label ID="lblNextHearingDate" Text="" runat="server"></asp:Label>
                                                                                                    </td>
                                                                                                    <td>
                                                                                                        <asp:Label ID="lblRegisteredCaseYear" Text="" runat="server"></asp:Label>
                                                                                                    </td>
                                                                                                    <td>
                                                                                                        <asp:DropDownList ID="ddl_Head" ClientIDMode="Static" Width="85px" runat="server" CssClass="form-control" AutoPostBack="true" ></asp:DropDownList>
                                                                                                    </td>
                                                                                                    <td>
                                                                                                        <asp:DropDownList ID="ddl_Section" Width="126px" ClientIDMode="Static" runat="server" AutoPostBack="true" CssClass="form-control"></asp:DropDownList>
                                                                                                    </td>
                                                                                                </tr>
                                                                                            </tbody>
                                                                                        </table>
                                                                                    </ContentTemplate>
                                                                                </asp:UpdatePanel>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-md-12">
                                                                        <div class="row">


                                                                            <asp:Repeater ID="rptComments" runat="server">
                                                                                <ItemTemplate>
                                                                                    <div class="col-md-12">

                                                                                        <div class="form-group row">
                                                                                            <div class="col-md-2">
                                                                                                <asp:Label ID="Label1" runat="server" CssClass="form-control" Text='<%# Eval("UPLOADBY") %>' BorderStyle="None" Style="background-color: gainsboro"></asp:Label>
                                                                                            </div>
                                                                                            <div class="col-md-10">
                                                                                                <asp:Label ID="lblComment" runat="server" CssClass="form-control" Text='<%# Eval("COMMENTS") %>'></asp:Label>
                                                                                            </div>

                                                                                            <br />
                                                                                        </div>

                                                                                    </div>
                                                                                </ItemTemplate>
                                                                            </asp:Repeater>

                                                                        </div>
                                                                        <div class="row">
                                                                            <div class="col-md-12">
                                                                                <div class="form-group">

                                                                                    <asp:TextBox ID="txtCoscomment" runat="server" CssClass="form-control" placeholder="Enter Comment "></asp:TextBox>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>

                                                                    <div class="col-md-12 align-items-center">
                                                                        <div class="row">
                                                                            <div class="col-md-6 d-flex align-items-center mt-3 justify-content-end">
                                                                                <div class="text-right mr-2">

                                                                                    <asp:Button ID="btnSendBack" class="btn btn-success float-left" runat="server" OnClientClick="return validateComment();" Text="SendBack" OnClick="btn_SaveCosComment" />
                                                                                </div>
                                                                            </div>
                                                                            <div class="col-md-6 d-flex align-items-center mt-3 justify-content-start">
                                                                                <div class="text-left mr-2">
                                                                                    <asp:Button ID="btnRegister" class="btn btn-success float-right" runat="server" Text="Register" OnClick="btnRegisterCase_Click" />

                                                                                </div>
                                                                            </div>

                                                                        </div>

                                                                    </div>
                                                                </div>


                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-12 col-sm-6">
                                                        <div class="card card-primary card-tabs h-100">
                                                            <div class="card-header p-0 pt-1 d-flex align-items-center">
                                                                <div class="col-sm-8 p-0">
                                                                    <ul class="nav nav-tabs" id="custom-tabs-two-tab" role="tablist">
                                                                        <li class="nav-item">
                                                                            <a class="nav-link active" id="custom-tabs-two-home-tab" data-toggle="pill" href="#custom-tabs-two-home" role="tab" aria-controls="custom-tabs-two-home" aria-selected="true">Document </a>
                                                                        </li>

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

                                                            <div class="card-body">
                                                                <div class="tab-content" id="custom-tabs-two-tabContent">
                                                                    <div class="tab-pane fade show active" id="custom-tabs-two-home" role="tabpanel"
                                                                        aria-labelledby="custom-tabs-two-home-tab">

                                                                        <div class="col-12 col-lg-12">
                                                                            <div class="card card-primary card-outline card-outline-tabs">
                                                                                <div class="card-header p-0 border-bottom-0 a-clr">
                                                                                    <ul class="nav nav-tabs" id="custom-tabs-four-tab" role="tablist">
                                                                                        <li class="nav-item">
                                                                                            <a class="nav-link" id="custom-tabs-four-home-tab" data-toggle="pill" href="#custom-tabs-four-home" role="tab" aria-controls="custom-tabs-four-home" aria-selected="false">Index</a>
                                                                                        </li>
                                                                                        <li class="nav-item">
                                                                                            <a class="nav-link active" id="custom-tabs-four-profile-tab" data-toggle="pill" href="#custom-tabs-four-profile" role="tab" aria-controls="custom-tabs-four-profile" aria-selected="false">Recent</a>
                                                                                        </li>
                                                                                        <li class="nav-item">
                                                                                            <a class="nav-link" id="custom-tabs-four-messages-tab" data-toggle="pill" href="#custom-tabs-four-messages" role="tab" aria-controls="custom-tabs-four-messages" aria-selected="false">All</a>
                                                                                        </li>
                                                                                        <li class="nav-item">
                                                                                            <a class="nav-link" id="custom-tabs-four-settings-tab" data-toggle="pill" href="#custom-tabs-four-settings" role="tab" aria-controls="custom-tabs-four-settings" aria-selected="true">Previous Proceeding</a>
                                                                                        </li>
                                                                                    </ul>
                                                                                </div>
                                                                                <div class="card-body d_mycard">
                                                                                    <div class="tab-content" id="custom-tabs-four-tabContent">
                                                                                        <div class="tab-pane fade" id="custom-tabs-four-home" role="tabpanel" aria-labelledby="custom-tabs-four-home-tab">
                                                                                            <fieldset>

                                                                                                <div id="pnl2">
                                                                                                    <h6 class="text-left"><strong>List of Documents </strong></h6>
                                                                                                    <div class="table-responsive listtabl">
                                                                                                        <asp:GridView ID="grdSRDoc" CssClass="table table-bordered table-condensed table-striped table-hover"
                                                                                                            DataKeyNames="App_ID"
                                                                                                            runat="server" CellPadding="4" ForeColor="#333333" GridLines="None" AutoGenerateColumns="false"
                                                                                                            Visible="true" EmptyDataText="No record found">
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
                                                                                                                    <ItemTemplate>
                                                                                                                        <%#Eval("DocumentType") %>
                                                                                                                    </ItemTemplate>
                                                                                                                    <HeaderStyle Width="20%" HorizontalAlign="Center" />
                                                                                                                    <ItemStyle Width="20%" HorizontalAlign="Center" />
                                                                                                                </asp:TemplateField>
                                                                                                                <%--<asp:TemplateField HeaderText="Document Name" SortExpression="DocName" HeaderStyle-Font-Bold="false">
                                                                                                                <ItemTemplate>
                                                                                                                    <%#Eval("DOC_NAME") %>
                                                                                                                </ItemTemplate>
                                                                                                                <HeaderStyle Width="10%" HorizontalAlign="Center" />

                                                                                                                <ItemStyle Width="10%" HorizontalAlign="Center" />
                                                                                                            </asp:TemplateField>--%>
                                                                                                                <asp:TemplateField HeaderText="Uploaded By" SortExpression="DocName" HeaderStyle-Font-Bold="false">
                                                                                                                    <ItemTemplate>
                                                                                                                        <%#Eval("UploadedBy") %>
                                                                                                                    </ItemTemplate>
                                                                                                                    <HeaderStyle Width="10%" HorizontalAlign="Center" />

                                                                                                                    <ItemStyle Width="10%" HorizontalAlign="Center" />
                                                                                                                </asp:TemplateField>
                                                                                                                <asp:TemplateField HeaderText="Attached On" SortExpression="INSDATE" HeaderStyle-Font-Bold="false">
                                                                                                                    <ItemTemplate>
                                                                                                                        <%# ((DateTime)Eval("INSDATE")).ToString("dd-MM-yyyy") %>
                                                                                                                    </ItemTemplate>
                                                                                                                    <HeaderStyle Width="10%" HorizontalAlign="Center" />

                                                                                                                    <ItemStyle Width="10%" HorizontalAlign="Center" />
                                                                                                                </asp:TemplateField>

                                                                                                                <asp:TemplateField Visible="false" HeaderText="Pages" HeaderStyle-Font-Bold="false">

                                                                                                                    <HeaderStyle Width="10%" HorizontalAlign="Center" />

                                                                                                                    <ItemStyle Width="10%" HorizontalAlign="Center" />
                                                                                                                </asp:TemplateField>


                                                                                                                <asp:TemplateField HeaderText="View" HeaderStyle-Font-Bold="false">
                                                                                                                    <ItemTemplate>
                                                                                                                        <a href="#" class="gridViewToolTip" id="btnModalPopup" onclick='showPdfBYHendler("<%# Eval("FILE_PATH")%>")'>

                                                                                                                            <button type="button" class="btn btn-info"><i class="fas fa-eye"></i></button>
                                                                                                                        </a>


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
                                                                                                    <h6 class="text-left" style="display: none"><strong>COS Document</strong></h6>
                                                                                                    <div class="table-responsive listtabl" style="display: none">
                                                                                                        <asp:GridView ID="grdProposalDoc" CssClass="table table-bordered table-condensed table-striped table-hover"
                                                                                                            DataKeyNames="App_ID"
                                                                                                            runat="server" CellPadding="4" ForeColor="#333333" GridLines="None" AutoGenerateColumns="false" Visible="true" EmptyDataText="No record found">
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
                                                                                                                    <ItemTemplate>
                                                                                                                        <%#Eval("Doc_Name") %>
                                                                                                                    </ItemTemplate>
                                                                                                                    <HeaderStyle Width="10%" HorizontalAlign="Center" />
                                                                                                                    <ItemStyle Width="10%" HorizontalAlign="Center" />
                                                                                                                </asp:TemplateField>
                                                                                                                <asp:TemplateField HeaderText="Provided by" HeaderStyle-Font-Bold="false">
                                                                                                                    <ItemTemplate>
                                                                                                                        <%--<%#Eval("document_name") %>--%>
                                                                                                                        <b>COS</b>
                                                                                                                    </ItemTemplate>
                                                                                                                    <HeaderStyle Width="10%" HorizontalAlign="Center" />
                                                                                                                    <ItemStyle Width="10%" HorizontalAlign="Center" />
                                                                                                                </asp:TemplateField>
                                                                                                                <asp:TemplateField HeaderText="Uploaded by" HeaderStyle-Font-Bold="false">
                                                                                                                    <ItemTemplate>
                                                                                                                        <%--<%#Eval("document_name") %>--%>
                                                                                                                        <b>COS</b>
                                                                                                                    </ItemTemplate>
                                                                                                                    <HeaderStyle Width="10%" HorizontalAlign="Center" />
                                                                                                                    <ItemStyle Width="10%" HorizontalAlign="Center" />
                                                                                                                </asp:TemplateField>

                                                                                                                <asp:TemplateField HeaderText="View">
                                                                                                                    <ItemTemplate>

                                                                                                                        <a href="#" class="gridViewToolTip" id="btnModalPopup" onclick='openPopup("<%# Eval("file_path")%>")'>
                                                                                                                            <button type="button" class="btn btn-info btn-sm"><i class="fas fa-eye"></i></button>
                                                                                                                        </a>

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


                                                                                                </div>

                                                                                                <div id="pnl3" style="display: none">
                                                                                                    <a href="#" onclick="ViewGrd()" style="font-size: larger; color: red">x</a>
                                                                                                    <iframe id="ifrDisplay" class="embed-responsive-item" height="750"></iframe>
                                                                                                </div>
                                                                                            </fieldset>

                                                                                        </div>

                                                                                        <div class="tab-pane fade active show" id="custom-tabs-four-profile" role="tabpanel" aria-labelledby="custom-tabs-four-profile-tab">

                                                                                            <div class="col-md-12">
                                                                                                <div class="card card-outline newclr">
                                                                                                    <div class="card-body">

                                                                                                        <iframe id="ifPDFViewer" runat="server" clientidmode="Static" frameborder="0" scrolling="auto" class="mydiv"></iframe>

                                                                                                    </div>

                                                                                                </div>
                                                                                            </div>



                                                                                        </div>

                                                                                        <div class="tab-pane fade" id="custom-tabs-four-messages" role="tabpanel" aria-labelledby="custom-tabs-four-messages-tab">

                                                                                            <div class="col-md-12">
                                                                                                <div class="card card-outline newclr">
                                                                                                    <div class="card-body">
                                                                                                        <div style="height: 560px; overflow-y: auto">
                                                                                                            <%-- <iframe id="RecentdocPath" clientidmode="Static" runat="server" width='550' height='750' frameborder="0" scrolling="auto" class="mydiv"></iframe>
                                                                                                            <iframe id="RecentProposalDoc" clientidmode="Static" runat="server" width='550' height='750' frameborder="0" scrolling="auto" class="mydiv"></iframe>
                                                                                                            <iframe id="RecentAttachedDoc" clientidmode="Static" runat="server" width='550' height='750' frameborder="0" scrolling="auto" class="mydiv"></iframe>--%>
                                                                                                            <iframe id="ifPDFViewerAll" clientidmode="Static" runat="server" width='550' height='750' frameborder="0" scrolling="auto" class="mydiv"></iframe>
                                                                                                        </div>
                                                                                                    </div>
                                                                                                </div>
                                                                                            </div>
                                                                                        </div>

                                                                                        <div class="tab-pane fade " id="custom-tabs-four-settings" role="tabpanel" aria-labelledby="custom-tabs-four-settings-tab">

                                                                                            <div class="col-md-12">
                                                                                                <div class="card  card-outline newclr">
                                                                                                    <div class="card-body">

                                                                                                        <iframe id="ifPDFViewPreviousProceeding" clientidmode="Static" runat="server" frameborder="0" scrolling="auto" class="mydiv"></iframe>

                                                                                                    </div>

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
                                                        <!-- /.card -->
                                                    </div>
                                                </div>
                                            </div>
                                            <asp:HiddenField ID="hdnfldHead" ClientIDMode="Static" runat="server" />
                                            <asp:HiddenField ID="hdnfldSection" ClientIDMode="Static" runat="server" />
                                            <asp:HiddenField ID="hdnfldCase" ClientIDMode="Static" runat="server" />
                                            <asp:HiddenField ID="hdnAppID" ClientIDMode="Static" runat="server" />

                                        </div>

                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.6.4/jquery.min.js"></script>
        <asp:HiddenField runat="server" ClientIDMode="Static" ID="hdappid" />
        <asp:HiddenField runat="server" ClientIDMode="Static" ID="hdAppno" />
        <asp:HiddenField runat="server" ClientIDMode="Static" ID="hdTocan" />
        <asp:HiddenField ID="hdnbase64" ClientIDMode="Static" runat="server" />

        <script type="text/javascript">
            function showPdfBYHendler(filePath) {
                alert("Hello");
                debugger;
                if (filePath && filePath.trim() !== '') {
                    // Replace backslashes with forward slashes and remove the tilde character
                    var cleanedPath = filePath.replace(/\\/g, "/").replace("~/", "");
                    var serverPath = '<%= ResolveUrl("~/") %>' + cleanedPath; // Combine server path and cleaned path
                    document.getElementById('ifrDisplay').src = serverPath; // Set the src attribute of the iframe
                    document.getElementById('pnl2').style.display = 'none'; // Hide the GridView panel
                    document.getElementById('pnl3').style.display = 'block'; // Show the iframe panel
                } else {
                    document.getElementById('pnl3').style.display = 'none'; // Ensure the iframe panel is hidden
                    Swal.fire({
                        icon: 'info',
                        title: 'No Document Attached',
                        confirmButtonText: 'OK',
                    });
                }
            }
        </script>

        <script type="text/javascript">

            function validateComment() {
                debugger;
                var isvalid = true;

                if ($("#<%= txtCoscomment.ClientID %>").val() == "") {
                    swal.fire('warning!', 'Please enter the comment!', 'warning');
                    isvalid = false;
                    return false;
                }

                // allow form submission if validation pass
                return isvalid;

            }

            function ViewGrd() {

                $('#pnl3').hide();
                $('#pnl2').show();

            }

        </script>

    </section>

</asp:Content>
