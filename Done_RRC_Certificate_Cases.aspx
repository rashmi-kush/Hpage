<%@ Page Title="" Language="C#" MasterPageFile="~/CoS/CoSMaster.Master" AutoEventWireup="true" CodeBehind="Done_RRC_Certificate_Cases.aspx.cs" Inherits="CMS_Sampada.CoS.Done_RRC_Certificate_Cases" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Styles/sweetalert.css" rel="stylesheet" />
    <script src="../Scripts/sweetalert.min.js"></script>
    <script src="../assets/js/3.3.1/sweetalert2@11.js"></script>
    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
    <script src="https://code.jquery.com/ui/1.12.1/jquery-ui.min.js"></script>
    <link rel="stylesheet" href="https://code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css">
    <!-- SweetAlert for alerts -->
    <script src="https://cdn.jsdelivr.net/npm/sweetalert2@10"></script>
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
    <style>
        .htmldoc {
            height: 790px !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section class="content-header">
        <div class="container-fluid">
            <div class="row mb-2">
                <div class="col-sm-6">
 
                    <h5>Done RRC Certificate Cases</h5>
                </div>
         
            </div>
        </div>
    </section>

    <section class="content">
        <div class="container-fluid">
            <div class="row">

                <div class="col-md-12">
                    <div class="card">
                        <div class="card-header p-2 pt-1 d-flex align-items-center">
                            <div class="col-sm-6 p-0">
                                <ul class="nav nav-pills">
                                    <li class="nav-item "><a class="nav-link " href="CasesForRRC_Certificate">Pending RRC Certificate</a></li>
                                    <li class="nav-item"><a class="nav-link active" href="Done_RRC_Certificate_Cases">Done RRC Certificate</a></li>

                                </ul>

                            </div>
                            <div class="align-items-end col-sm-6 w-100">
                                <div class="text-right">
                                    <div class="row">
                                        <div class="col-4">

                                            <asp:TextBox ID="txtsearch" runat="server" CssClass="form-control" placeholder="Proposal/Case No."></asp:TextBox>
                                        </div>
                                        <div class="col-3">
                                            <asp:TextBox ID="txtfromdate" runat="server" CssClass="form-control datePick" placeholder="From Date"></asp:TextBox>
                                        </div>
                                        <div class="col-3 ">
                                            <asp:TextBox ID="txttodate" runat="server" CssClass="form-control datePick" placeholder="To Date"></asp:TextBox>
                                        </div>
                                        <div class="col-2">
                                            <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-info t-btn w-100" Text="Ok" OnClick="btn_Search_click" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="card-body">

                            <div class="col-lg-offset-12 col-md-offset-12 col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                <div class="table-responsive listtabl">
                                    <asp:GridView ID="grdCaseList" CssClass="table table-bordered table-condensed table-striped table-hover"
                                        DataKeyNames="Notice_id,App_ID,Case_Number,HearingDate,hearing_id,app_id,INITIATION_ID,OrderSheetInsertDate,FinalOrder_Date,InsertedDate"
                                        runat="server" CellPadding="4" ForeColor="#333333" GridLines="None" HeaderStyle-Font-Bold="false"
                                        AutoGenerateColumns="False" Visible="true" Width="100%" EmptyDataText="No RRC cases" >
                                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                        <Columns>
                                            <asp:TemplateField HeaderStyle-Font-Bold="false" HeaderText="SNo." HeaderStyle-VerticalAlign="Top" HeaderStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <itemtemplate><%#Container.DataItemIndex+1  %> </itemtemplate>
                                                </ItemTemplate>
                                                <ItemStyle Width="2%" HorizontalAlign="Center" />
                                                <HeaderStyle Width="2%" HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-Font-Bold="false" HeaderText="Proposal No">
                                                <ItemTemplate>
                                                    <%#Eval("application_no") %>
                                                </ItemTemplate>
                                                <HeaderStyle Width="10%" HorizontalAlign="Center" />
                                                <ItemStyle Width="10%" HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-Font-Bold="false" HeaderText="Case No">
                                                <ItemTemplate>
                                                    <%#Eval("Case_Number") %>
                                                </ItemTemplate>
                                                <HeaderStyle Width="10%" HorizontalAlign="Center" />
                                                <ItemStyle Width="10%" HorizontalAlign="Center" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderStyle-Font-Bold="false" HeaderText="App ID" Visible="false">
                                                <ItemTemplate>
                                                    <%#Eval("app_id") %>
                                                </ItemTemplate>
                                                <HeaderStyle Width="10%" HorizontalAlign="Center" />
                                                <ItemStyle Width="10%" HorizontalAlign="Center" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderStyle-Font-Bold="false" HeaderText="Initation ID " Visible="false">
                                                <ItemTemplate>
                                                    <%#Eval("INITIATION_ID") %>
                                                </ItemTemplate>
                                                <HeaderStyle Width="10%" HorizontalAlign="Center" />
                                                <ItemStyle Width="10%" HorizontalAlign="Center" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderStyle-Font-Bold="false" HeaderText="Ordersheet ID" Visible="false">
                                                <ItemTemplate>
                                                    <%#Eval("OrderSheetInsertDate") %>
                                                </ItemTemplate>
                                                <HeaderStyle Width="10%" HorizontalAlign="Center" />
                                                <ItemStyle Width="10%" HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-Font-Bold="false" HeaderText="Notice" Visible="false">
                                                <ItemTemplate>
                                                    <%#Eval("Notice_id") %>
                                                </ItemTemplate>
                                                <HeaderStyle Width="10%" HorizontalAlign="Center" />
                                                <ItemStyle Width="10%" HorizontalAlign="Center" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderStyle-Font-Bold="false" HeaderText="Case Origin">
                                                <ItemTemplate>
                                                    <%#Eval("department_name") %>
                                                </ItemTemplate>
                                                <HeaderStyle Width="10%" HorizontalAlign="Center" />
                                                <ItemStyle Width="10%" HorizontalAlign="Center" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderStyle-Font-Bold="false" HeaderText="Date of Registration">
                                                <ItemTemplate>
                                                    <%#Eval("InsertedDate") %>
                                                </ItemTemplate>
                                                <HeaderStyle Width="10%" HorizontalAlign="Center" />
                                                <ItemStyle Width="10%" HorizontalAlign="Center" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderStyle-Font-Bold="false" HeaderText="Hearing Date">
                                                <ItemTemplate>
                                                    <%#Eval("HearingDate") %>
                                                </ItemTemplate>
                                                <HeaderStyle Width="10%" HorizontalAlign="Center" />
                                                <ItemStyle Width="10%" HorizontalAlign="Center" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderStyle-Font-Bold="false" HeaderText="Ordersheet Date">
                                                <ItemTemplate>
                                                    <%#Eval("OrderSheetInsertDate") %>
                                                </ItemTemplate>
                                                <HeaderStyle Width="10%" HorizontalAlign="Center" />
                                                <ItemStyle Width="10%" HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-Font-Bold="false" HeaderText="Finalorder Date">
                                                <ItemTemplate>
                                                    <%#Eval("FinalOrder_Date") %>
                                                </ItemTemplate>
                                                <HeaderStyle Width="10%" HorizontalAlign="Center" />
                                                <ItemStyle Width="10%" HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-Font-Bold="false" HeaderText="HearingID" Visible="false">
                                                <ItemTemplate>
                                                    <%#Eval("hearing_id") %>
                                                </ItemTemplate>
                                                <HeaderStyle Width="10%" HorizontalAlign="Center" />
                                                <ItemStyle Width="10%" HorizontalAlign="Center" />
                                            </asp:TemplateField>

                                              <asp:TemplateField HeaderStyle-Font-Bold="false" HeaderText="Status RRC_Certificate">
                                            <ItemTemplate>
                                                <%#Eval("RRC_Certificate_status") %>
                                            </ItemTemplate>
                                            <HeaderStyle Width="10%" HorizontalAlign="Center" />
                                            <ItemStyle Width="10%" HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                             <asp:TemplateField HeaderStyle-Font-Bold="false" HeaderText="RRC_Certificate Date">
                                            <ItemTemplate>
                                                <%#Eval("RRC_Certificate_Date") %>
                                            </ItemTemplate>
                                            <HeaderStyle Width="10%" HorizontalAlign="Center" />
                                            <ItemStyle Width="10%" HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        </Columns>
                                      
                                        <HeaderStyle BackColor="#e9564e" ForeColor="White" />
                               
                                    </asp:GridView>
                                </div>
                            </div>

                        </div>
                    </div>
                </div>
            </div>
        </div>
    </section>

    <script type="text/javascript">
        $(document).ready(function () {

            var txtsearch = $("#<%= txtsearch.ClientID %>");
            var txtfromdate = $("#<%= txtfromdate.ClientID %>");
            var txttodate = $("#<%= txttodate.ClientID %>");

            $("#<%= btnSearch.ClientID %>").click(function (e) {
                if (!ValidateSearch()) {
                    e.preventDefault(); // prevent form submission if validation fails
                }
            });

            function ValidateSearch() {
                var isValid = true;

                if (txtsearch.val().trim() === "" && txtfromdate.val().trim() === "" && txttodate.val().trim() === "") {
                    Swal.fire('Warning!', 'Please enter Case/Proposal number or select the dates for the search!', 'warning');
                    isValid = false;
                }

                return isValid; // allow form submission if validation passes
            }

        });
    </script>


</asp:Content>
