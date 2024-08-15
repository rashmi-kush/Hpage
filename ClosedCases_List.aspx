<%@ Page Title="" Language="C#" MasterPageFile="~/CoS/CoSMaster.Master" AutoEventWireup="true" CodeBehind="ClosedCases_List.aspx.cs" Inherits="CMS_Sampada.CoS.ClosedCases_List" %>

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
                    <%-- <h5>Impound Proposal</h5>--%>
                    <h5>Closed Cases</h5>
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

                <div class="col-md-12">
                    <div class="card">
                        <div class="card-header p-2 pt-1 d-flex align-items-center">
                            <div class="col-sm-6 p-0">
                                <ul class="nav nav-pills">
                                    <%--<li class="nav-item "><a class="nav-link" href="Ordersheet_Pending" >Order Sheet</a></li>
                                    <li class="nav-item"><a class="nav-link active" href="Notice_Pending" >Notice</a></li>
                                    --%>
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
                    </div>
                    <div class="card-body">

                        <div class="col-lg-offset-12 col-md-offset-12 col-lg-12 col-md-12 col-sm-12 col-xs-12">
                            <div class="table-responsive listtabl">
                                <asp:GridView ID="grdCaseList" CssClass="table table-bordered table-condensed table-striped table-hover"
                                    DataKeyNames="Notice_id,App_ID,Case_Number,HearingDate,hearing_id"
                                    runat="server" CellPadding="4" ForeColor="#333333" GridLines="None"
                                    AutoGenerateColumns="False" Visible="true" Width="100%" EmptyDataText="No Closed cases" HeaderStyle-Font-Bold="false" OnRowCommand="grdCaseList_RowCommand">
                                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="SNo." HeaderStyle-VerticalAlign="Top" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="false">
                                            <ItemTemplate>
                                                <itemtemplate><%#Container.DataItemIndex+1  %> </itemtemplate>
                                            </ItemTemplate>
                                            <ItemStyle Width="2%" HorizontalAlign="Center" />
                                            <HeaderStyle Width="2%" HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Proposal No" HeaderStyle-Font-Bold="false">
                                            <ItemTemplate>
                                                <%#Eval("application_no") %>
                                            </ItemTemplate>
                                            <HeaderStyle Width="10%" HorizontalAlign="Center" />
                                            <ItemStyle Width="10%" HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Case No" HeaderStyle-Font-Bold="false">
                                            <ItemTemplate>
                                                <%#Eval("Case_Number") %>
                                            </ItemTemplate>
                                            <HeaderStyle Width="10%" HorizontalAlign="Center" />
                                            <ItemStyle Width="10%" HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Notice" Visible="false" HeaderStyle-Font-Bold="false">
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

                                        <asp:TemplateField HeaderText="Order Sheet (Date)" Visible="false" HeaderStyle-Font-Bold="false">
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

                                        <asp:BoundField HeaderStyle-Width="10%" ItemStyle-Width="10%" DataField="EARLY_HEARING_RESPONSE" NullDisplayText='<span style="color:red;">N/A</span>' HeaderText="Request for Early Hearing" Visible="false" />


                                        <asp:TemplateField HeaderText="Hearing Date" Visible="false" HeaderStyle-Font-Bold="false">
                                            <ItemTemplate>
                                                <%#Eval("HearingDate") %>
                                            </ItemTemplate>
                                            <HeaderStyle Width="10%" HorizontalAlign="Center" />
                                            <ItemStyle Width="10%" HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Final Order Date" HeaderStyle-Font-Bold="false">
                                            <ItemTemplate>
                                                <%#Eval("FinalOrderDate") %>
                                            </ItemTemplate>
                                            <HeaderStyle Width="10%" HorizontalAlign="Center" />
                                            <ItemStyle Width="10%" HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Paid Amount" HeaderStyle-Font-Bold="false">
                                            <ItemTemplate>
                                                <%#Eval("paid_amount") %>
                                            </ItemTemplate>
                                            <HeaderStyle Width="10%" HorizontalAlign="Center" />
                                            <ItemStyle Width="10%" HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Payment Date" HeaderStyle-Font-Bold="false">
                                            <ItemTemplate>
                                                <%#Eval("payment_date") %>
                                            </ItemTemplate>
                                            <HeaderStyle Width="10%" HorizontalAlign="Center" />
                                            <ItemStyle Width="10%" HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Endorsement Status" HeaderStyle-Font-Bold="false">
                                            <ItemTemplate>
                                                <%#Eval("endors_status") %>
                                            </ItemTemplate>
                                            <HeaderStyle Width="10%" HorizontalAlign="Center" />
                                            <ItemStyle Width="10%" HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Endorsement Date" HeaderStyle-Font-Bold="false">
                                            <ItemTemplate>
                                                <%#Eval("endors_date") %>
                                            </ItemTemplate>
                                            <HeaderStyle Width="10%" HorizontalAlign="Center" />
                                            <ItemStyle Width="10%" HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Action" HeaderStyle-Font-Bold="false">
                                            <ItemStyle Width="4%" HorizontalAlign="Right" />
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkSelect" runat="server" CommandArgument='<%#Eval("App_ID")+","+Eval("Case_Number")+","+ Eval("application_no") %>' CommandName="SelectApplication" CssClass="btn btn-secondary ">Select</asp:LinkButton>
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
                            </div>
                        </div>

                    </div>
                </div>
            </div>
        </div>

    </section>


    <%-- <script>
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
    </script>--%>
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
