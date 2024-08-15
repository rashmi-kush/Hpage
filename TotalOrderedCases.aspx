<%@ Page Title="" Language="C#" MasterPageFile="~/CoS/CoSMaster.Master" AutoEventWireup="true" CodeBehind="TotalOrderedCases.aspx.cs" Inherits="CMS_Sampada.CoS.TotalOrderedCases" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <link href="../Styles/sweetalert.css" rel="stylesheet" />
    <script src="../Scripts/sweetalert.min.js"></script>
    <script src="../assets/js/3.3.1/sweetalert2@11.js"></script>

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
                    <%-- <h5>Impound Proposal</h5>--%>
                    <h5>Total Ordered Cases</h5>
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
                                            <div class="input-group date" id="search" data-target-input="nearest">
                                                <input type="text" class="form-control" data-target="#search" placeholder="Search...">
                                            </div>
                                        </div>
                                        <div class="col-3">
                                            <div class="input-group date" id="fromdate" data-target-input="nearest">
                                                <input type="text" class="form-control datetimepicker-input" data-target="#fromdate" placeholder="From Date">
                                                <div class="input-group-append" data-target="#fromdate" data-toggle="datetimepicker">
                                                    <div class="input-group-text"><i class="fa fa-calendar"></i></div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-3 d-flex">
                                            <div class="input-group date" id="todate" data-target-input="nearest">
                                                <input type="text" class="form-control datetimepicker-input" data-target="#todate" placeholder="To Date">
                                                <div class="input-group-append" data-target="#todate" data-toggle="datetimepicker">
                                                    <div class="input-group-text"><i class="fa fa-calendar"></i></div>
                                                </div>
                                            </div>

                                        </div>
                                        <div class="col-2">
                                            <button type="submit" class="btn btn-info t-btn w-100">Ok</button>
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
                                    AutoGenerateColumns="False" Visible="true" Width="100%" EmptyDataText="No Ordered Cases">
                                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="SNo." HeaderStyle-VerticalAlign="Top" HeaderStyle-Font-Bold="false" HeaderStyle-HorizontalAlign="Center">
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


                                        <asp:TemplateField HeaderText="Hearing Date" HeaderStyle-Font-Bold="false">
                                            <ItemTemplate>
                                                <%#Eval("HearingDate") %>
                                            </ItemTemplate>
                                            <HeaderStyle Width="10%" HorizontalAlign="Center" />
                                            <ItemStyle Width="10%" HorizontalAlign="Center" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Action" HeaderStyle-Font-Bold="false">
                                            <ItemStyle Width="4%" HorizontalAlign="Right" />
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkSelect" runat="server" CommandArgument='<%#Eval("App_ID") %>' CommandName="SelectApplication" OnClick="lnkSelect_Click" CssClass="btn btn-secondary ">Select</asp:LinkButton>

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
</asp:Content>
