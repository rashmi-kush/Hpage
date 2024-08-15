<%@ Page Title="" Language="C#" MasterPageFile="~/CoS/CoSMaster.Master" AutoEventWireup="true" CodeBehind="Payment_List.aspx.cs" Inherits="CMS_Sampada.CoS.Payment_List" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section class="content-header">
        <div class="container-fluid">
            <div class="row mb-2">
                <div class="col-sm-6">
                    <h5>Payment Status</h5>
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
                             
                            <div class="col-sm-6 p-0">
                                <ul class="nav nav-pills">
                                    <%-- <li class="nav-item"><a class="nav-link" href="CreateOrderSheet" data-toggle="tab">Order Sheet</a></li>
                                        <li class="nav-item"><a class="nav-link disabled" href="Notice" data-toggle="tab">Notice</a></li>
                                        <li class="nav-item"><a class="nav-link" href="ReportSeeking">Seek
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
                                        <div class="col-4">
                                            <div class="input-group date" id="search" data-target-input="nearest">
                                               <asp:TextBox ID="txtsearch" runat="server" CssClass="form-control"  placeholder="Search by proposal no..."></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-3">
                                            <div class="input-group date" id="fromdate" data-target-input="nearest">
                                                <asp:TextBox ID="txtfromdate" runat="server" CssClass="form-control datetimepicker-input" data-target="#fromdate" placeholder="From Date"></asp:TextBox>
                                                <div class="input-group-append" data-target="#fromdate" data-toggle="datetimepicker">
                                                    <div class="input-group-text"><i class="fa fa-calendar"></i></div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-3 d-flex">
                                            <div class="input-group date" id="todate" data-target-input="nearest">
                                                <asp:TextBox ID="txttodate" runat="server" CssClass="form-control datetimepicker-input" data-target="#todate" placeholder="To Date"></asp:TextBox>
                                                <div class="input-group-append" data-target="#todate" data-toggle="datetimepicker">
                                                    <div class="input-group-text"><i class="fa fa-calendar"></i></div>
                                                </div>
                                            </div>

                                        </div>
                                        <div class="col-2">
                                            <asp:Button ID="btnsearch" runat="server" CssClass="btn btn-info t-btn w-100" Text="Search" OnClick="btnsearch_Click" />
                                        </div>

                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-offset-12 col-md-offset-12 col-lg-12 col-md-12 col-sm-12 col-xs-12">
                            <div class="table-responsive listtabl">
                                <asp:GridView ID="grdCaseList" CssClass="table table-bordered table-condensed table-striped table-hover"
                                    DataKeyNames="App_ID,Proposal_No,Case_Number,FinalOrder_Date"
                                    runat="server" CellPadding="4" ForeColor="#e9564e" GridLines="None" AutoGenerateColumns="False" Visible="true" Width="100%" EmptyDataText="No recoed found">
                                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="SNo." HeaderStyle-VerticalAlign="Top" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="false">
                                            <ItemTemplate>
                                                <itemtemplate><%#Container.DataItemIndex+1  %> </itemtemplate>
                                            </ItemTemplate>
                                            <ItemStyle Width="2%" HorizontalAlign="Center" />
                                            <HeaderStyle Width="2%" HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Proposal No." HeaderStyle-VerticalAlign="Top" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="false">
                                            <ItemTemplate>
                                                <%#Eval("Proposal_No") %>
                                            </ItemTemplate>
                                            <HeaderStyle Width="5%" HorizontalAlign="Left" />
                                            <ItemStyle Width="5%" HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Case No." HeaderStyle-VerticalAlign="Top" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="false">
                                            <ItemTemplate>
                                                <%#Eval("Case_Number") %>
                                            </ItemTemplate>
                                            <HeaderStyle Width="5%" HorizontalAlign="Left" />
                                            <ItemStyle Width="5%" HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Date Of Impound" HeaderStyle-VerticalAlign="Top" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="false">
                                            <ItemTemplate>
                                                <%#Eval("Impound_Date") %>
                                            </ItemTemplate>
                                            <HeaderStyle Width="10%" HorizontalAlign="Center" />
                                            <ItemStyle Width="10%" HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Case Origin" HeaderStyle-VerticalAlign="Top" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="false">
                                            <ItemTemplate>
                                                <%#Eval("Case_Origin") %>
                                            </ItemTemplate>
                                            <HeaderStyle Width="10%" HorizontalAlign="Center" />
                                            <ItemStyle Width="10%" HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="Final Order Date" HeaderStyle-VerticalAlign="Top" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="false">
                                            <ItemTemplate>
                                                <%#Eval("FinalOrder_Date") %>
                                            </ItemTemplate>
                                            <HeaderStyle Width="10%" HorizontalAlign="Center" />
                                            <ItemStyle Width="10%" HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="Final Paybal Amount" HeaderStyle-VerticalAlign="Top" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="false">
                                            <ItemTemplate>
                                                <%#Eval("FINAL_PAYABLE_AMOUNT") %>
                                            </ItemTemplate>
                                            <HeaderStyle Width="10%" HorizontalAlign="Center" />
                                            <ItemStyle Width="10%" HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="Purpose" HeaderStyle-VerticalAlign="Top" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="false">
                                            <ItemTemplate>
                                                <%#Eval("PURPOSE") %>
                                            </ItemTemplate>
                                            <HeaderStyle Width="10%" HorizontalAlign="Center" />
                                            <ItemStyle Width="10%" HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="Paid Amount" HeaderStyle-VerticalAlign="Top" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="false">
                                            <ItemTemplate>
                                                <%#Eval("PAID_AMOUNT") %>
                                            </ItemTemplate>
                                            <HeaderStyle Width="10%" HorizontalAlign="Center" />
                                            <ItemStyle Width="10%" HorizontalAlign="Left" />
                                        </asp:TemplateField>

                                         <asp:TemplateField HeaderText="Payment Type" HeaderStyle-VerticalAlign="Top" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="false">
                                            <ItemTemplate>
                                                <%#Eval("AMOUNT_TYPE") %>
                                            </ItemTemplate>
                                            <HeaderStyle Width="10%" HorizontalAlign="Center" />
                                            <ItemStyle Width="10%" HorizontalAlign="Left" />
                                        </asp:TemplateField>

                                         <asp:TemplateField HeaderText="Payment Status" HeaderStyle-VerticalAlign="Top" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="false">
                                            <ItemTemplate>
                                                <%#Eval("PAYMENT_STATUS") %>
                                            </ItemTemplate>
                                            <HeaderStyle Width="10%" HorizontalAlign="Center" />
                                            <ItemStyle Width="10%" HorizontalAlign="Left" />
                                        </asp:TemplateField>

                                         <asp:TemplateField HeaderText="Payment Date" HeaderStyle-VerticalAlign="Top" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="false">
                                            <ItemTemplate>
                                                <%#Eval("Payment_Date") %>
                                            </ItemTemplate>
                                            <HeaderStyle Width="10%" HorizontalAlign="Center" />
                                            <ItemStyle Width="10%" HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="Payment Mode" HeaderStyle-VerticalAlign="Top" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="false">
                                            <ItemTemplate>
                                                <%#Eval("PAYMENT_MODE") %>
                                            </ItemTemplate>
                                            <HeaderStyle Width="10%" HorizontalAlign="Center" />
                                            <ItemStyle Width="10%" HorizontalAlign="Left" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Action" HeaderStyle-VerticalAlign="Top" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="false">
                                            <ItemStyle Width="1%" HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <asp:Button ID="lnkSelect" ClientIDMode="Static" CommandName="Open" Enabled='<%# Convert.ToBoolean(Eval("FINAL_PAYABLE_AMOUNT").ToString() == Eval("PAID_AMOUNT").ToString() ? "true" : "false") || Convert.ToBoolean(Eval("FINAL_PAYABLE_AMOUNT").ToString() == "0" ? "true" : "false") %>' runat="server" OnClick="lnkSelect_Click"  CssClass="btn btn-secondary" Text="Select" />
<%--                                                <asp:LinkButton ID="lnkSelect" runat="server" OnClick="lnkSelect_Click" CssClass="btn btn-secondary">Select</asp:LinkButton>--%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <EditRowStyle BackColor="#999999" />
                                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                    <HeaderStyle BackColor="#e9564e" Font-Bold="True" ForeColor="White" />
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
                    </div>
                </div>
            </div>
        </div>
    </section>
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
    </script>

</asp:Content>
