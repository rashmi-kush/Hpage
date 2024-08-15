<%@ Page Title="" Language="C#" MasterPageFile="~/CoS/CoSMaster.Master" AutoEventWireup="true" CodeBehind="CollectorStampLegacyCases.aspx.cs" Inherits="CMS_Sampada.CoS.CollectorStampLegacyCases" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Styles/sweetalert.css" rel="stylesheet" />
    <script src="../Scripts/sweetalert.min.js"></script>
    <script src="../assets/js/3.3.1/sweetalert2@11.js"></script>
    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
    <script src="https://code.jquery.com/ui/1.12.1/jquery-ui.min.js"></script>
    <link rel="stylesheet" href="https://code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css">
    <!-- SweetAlert for alerts -->
    <script src="https://cdn.jsdelivr.net/npm/sweetalert2@10"></script>

    <style>
        .htmldoc {
            height: 790px !important;
        }
    </style>
    <script>
        $(function () {
            $(".datePick").datepicker({
                dateFormat: 'dd/mm/yy',
                changeMonth: true,
                changeYear: true,
                maxDate: 0
            });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section class="content-header">
        <div class="container-fluid">
            <div class="row mb-2">
                <div class="col-sm-6">
                    <h5>Impound Proposal</h5>
                </div>
                <%-- <div class="col-sm-6">
                    <ol class="breadcrumb float-sm-right">
                        <li class="breadcrumb-item"><a href="#">Home</a></li>
                        <li class="breadcrumb-item active">proposal List</li>
                    </ol>
                </div>--%>
            </div>
        </div>
    </section>

    <section class="content">
        <div class="container-fluid">
            <div class="row">
                <div class="col-12">
                    <div class="card">
                        <%-- <div class="card-header">
                            <h3 class="card-title">Received Pending Proposal</h3>
                        </div>--%>

                        <div class="card-header p-2 pt-1 d-flex align-items-center">

                            <div class="align-items-end col-sm-12 w-100">
                                <div class="text-right">
                                    <div class="row">
                                        <div class="col-2">
                                            <div class="input-group date" id="search" data-target-input="nearest">
                                                <asp:TextBox ID="txtCaseNo" runat="server" CssClass="form-control" placeholder="Case No."></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-2">
                                            <asp:TextBox ID="txtfromdate" runat="server" CssClass="form-control datePick" placeholder="From Date"></asp:TextBox>
                                        </div>
                                        <div class="col-2">
                                            <asp:TextBox ID="txttodate" runat="server" CssClass="form-control datePick" placeholder="To Date"></asp:TextBox>
                                        </div>

                                        <div class="col-md-2">
                                            <asp:DropDownList ID="ddl_CaseOrigin" runat="server" CssClass="form-control" EnableTheming="false">
                                            </asp:DropDownList>

                                        </div>

                                        <div class="col-md-2">
                                            <asp:DropDownList ID="ddl_SROOffice" runat="server" CssClass="form-control" EnableTheming="false">
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-2 d-flex">
                                            <asp:Button ID="btnsearch" runat="server" CssClass="btn btn-info t-btn w-100 mx-2" OnClientClick="return ValidateSearch();" OnClick="btnsearch_Click" Text="Search" />

                                            <asp:Button ID="btnclear" runat="server" CssClass="btn btn-secondary w-100" OnClientClick="clearAndReload(); return false;" Text="Clear" />
                                        </div>
                                    </div>
                                </div>
                            </div>

                        </div>

                        <div class="col-lg-offset-12 col-md-offset-12 col-lg-12 col-md-12 col-sm-12 col-xs-12">
                            <div class="table-responsive listtabl my-2">
                                <table class="table table-striped table-bordered table-all-common">
                                    <asp:GridView ID="GrdLegacyCaseList" runat="server" AutoGenerateColumns="false" Style="font-size: small; max-width: 100%;" DataKeyNames="Document_No,app_id,status_id"
                                        CssClass="table table-hover table-bordered table table-striped" BorderWidth="1px" CellPadding="4" ForeColor="#484848" EmptyDataText="No Data Found"
                                        AllowPaging="True" PageSize="4" OnPageIndexChanging="GrdLegacyCase_PageIndexChanging">
                                        <Columns>
                                            <asp:TemplateField HeaderText="SNo." HeaderStyle-VerticalAlign="Top" HeaderStyle-Font-Bold="false" HeaderStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <itemtemplate><%#Container.DataItemIndex+1  %> </itemtemplate>
                                                </ItemTemplate>
                                                <ItemStyle Width="2%" HorizontalAlign="Center" />
                                                <HeaderStyle Width="2%" HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="ID" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Top" Visible="false">
                                                <HeaderStyle CssClass="centered-header" />
                                                <ItemTemplate>
                                                    <%# Container.DataItemIndex + 1%>
                                                    <asp:HiddenField ID="AppID" runat="server" Value='<%#Eval("app_id") %>'></asp:HiddenField>

                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" Width="3%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Old case no" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Top">
                                                <HeaderStyle CssClass="centered-header" />
                                                <ItemTemplate>
                                                    <div style="text-align: center">
                                                        <asp:Label ID="lblOldcaseno" runat="server" Text='<%# Eval("Old_Case_No") %>'></asp:Label>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Document No" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Top">
                                                <ItemTemplate>
                                                    <div style="text-align: center">
                                                        <asp:Label ID="lblDocument_no" runat="server" Text='<%# Eval("Document_No") %>'></asp:Label>
                                                    </div>
                                                </ItemTemplate>

                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Date of Impond" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Top">
                                                <HeaderStyle CssClass="centered-header" />
                                                <ItemTemplate>
                                                    <div style="text-align: center">
                                                        <asp:Label ID="lblDateofImpond" runat="server" Text='<%# Eval("Impound_Date") %>'></asp:Label>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Case Origin" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Top">
                                                <ItemTemplate>
                                                    <div style="text-align: center">
                                                        <asp:Label ID="lblCaseOrigin" runat="server" Text='<%# Eval("CaseOrigin") %>'></asp:Label>
                                                    </div>
                                                </ItemTemplate>

                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Case Type" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Top">
                                                <ItemTemplate>
                                                    <div style="text-align: center">
                                                        <asp:Label ID="lblCaseType" runat="server" Text='<%# Eval("CaseType") %>'></asp:Label>
                                                    </div>
                                                </ItemTemplate>

                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="SRO Name" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Top">
                                                <ItemTemplate>
                                                    <div style="text-align: center">
                                                        <asp:Label ID="lblSROName" runat="server" Text='<%# Eval("SRO") %>'></asp:Label>
                                                    </div>
                                                </ItemTemplate>

                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderStyle-Font-Bold="false" HeaderText="Status">
                                                <ItemTemplate>
                                                    <%# GetStatusText(Eval("status_id")) %>
                                                </ItemTemplate>
                                                <HeaderStyle Width="10%" HorizontalAlign="Center" />
                                                <ItemStyle Width="10%" HorizontalAlign="Center" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Action" HeaderStyle-VerticalAlign="Top" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="false">
                                                <ItemStyle HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkSelect" runat="server" CommandArgument='<%#Eval("app_id") %>' CommandName="SelectApplication" OnClick="lnkSelect_Click" CssClass="btn btn-secondary ">Select</asp:LinkButton>
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
            </div>
        </div>
        <script type="text/javascript">

            function ValidateSearch() {
                debugger;
                var isValid = true;
                var txtCaseNo = $("#<%= txtCaseNo.ClientID %>");
                var txtfromdate = $("#<%= txtfromdate.ClientID %>");
                var txttodate = $("#<%= txttodate.ClientID %>");
                var ddl_SROOffice = $("#<%= ddl_SROOffice.ClientID %>");
                var ddl_CaseOrigin = $("#<%= ddl_CaseOrigin.ClientID %>");


                if (txtCaseNo.val().trim() === "" && txtfromdate.val().trim() === "" && txttodate.val().trim() === ""
                    && ddl_SROOffice.val() === "0" && ddl_CaseOrigin.val() === "0") {
                    Swal.fire('Warning!', 'Please enter data for the search!', 'warning');
                    isValid = false;
                }
                
                return isValid; // allow form submission if validation passes
            }

            function clearAndReload()
            {   
                window.location.href = 'CollectorStampLegacyCases.aspx';
            }

        </script>


    </section>
</asp:Content>
