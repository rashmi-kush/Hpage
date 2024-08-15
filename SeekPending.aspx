<%@ Page Title="" Language="C#" MasterPageFile="~/CoS/CoSMaster.Master" AutoEventWireup="true" CodeBehind="SeekPending.aspx.cs" Inherits="CMS_Sampada.CoS.SeekPending" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <section class="content">
        <div class="container-fluid">
            <div class="row">
                <div class="col-12">
                    <div class="card">
                        <div class="card-header">
                            <h3 class="card-title">Received Pending Order</h3>
                        </div>
                        <div class="card-header p-2">
                            <ul class="nav nav-pills">
                                <li class="nav-item"><a class="nav-link disabled" href="">Proposal</a></li>
                                        <li class="nav-item"><a class="nav-link disabled" href="#CreateOrderSheet" data-toggle="tab">Order Sheet</a></li>
                                        <li class="nav-item"><a class="nav-link disabled" href="#Notice" data-toggle="tab">Notice</a></li>
                                        <li class="nav-item"><a class="nav-link active disabled" href="ReportSeeking">Seek Report</a></li>
                                        <li class="nav-item"><a class="nav-link disabled" href="#EarlyHearing" data-toggle="tab">Hearing</a>
                                        <li class="nav-item"><a class="nav-link disabled" href="#EarlyHearing" data-toggle="tab">Final Order</a>

                                        <%--<li class="nav-item"><a class="nav-link disabled" href="#EarlyHearing" data-toggle="tab">Early
                        Hearing</a>
                                        </li>
                                        <li class="nav-item"><a class="nav-link disabled" href="#SendToAppeal" data-toggle="tab">Send to
                        Appeal</a>
                                        </li>
                                        <li class="nav-item"><a class="nav-link" href="AcceptRejectCases_details">Send Back</a>
                                        </li>
                                        <li class="nav-item"><a class="nav-link disabled" href="#Reply" data-toggle="tab">Reply</a></li>
                                        <li class="nav-item"><a class="nav-link disabled" href="#Attachement"
                                            data-toggle="tab">Attachement</a></li>--%>
                            </ul>
                        </div>
                        <div class="col-lg-offset-12 col-md-offset-12 col-lg-12 col-md-12 col-sm-12 col-xs-12">
                            <div class="table-responsive listtabl">
                                <asp:GridView ID="grdCaseList" CssClass="table table-bordered table-condensed table-striped table-hover"
                                    DataKeyNames="Registered_CaseNO"
                                    runat="server" CellPadding="4" ForeColor="#e9564e" GridLines="None" AutoGenerateColumns="False" Visible="true" Width="100%" EmptyDataText="No recoed found">
                                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="S No." HeaderStyle-VerticalAlign="Top" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="false">
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
                                        <asp:TemplateField HeaderText="Date of Impound" HeaderStyle-VerticalAlign="Top" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="false">
                                            <ItemTemplate>
                                                <%#Eval("Impound_Date") %>
                                            </ItemTemplate>
                                            <HeaderStyle Width="5%" HorizontalAlign="Left" />
                                            <ItemStyle Width="5%" HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Case Origin" HeaderStyle-VerticalAlign="Top" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="false">
                                            <ItemTemplate>
                                                <%#Eval("Case_Origin") %>
                                            </ItemTemplate>
                                            <HeaderStyle Width="10%" HorizontalAlign="Center" />
                                            <ItemStyle Width="10%" HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="SRO Name" HeaderStyle-VerticalAlign="Top" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="false">
                                            <ItemTemplate>
                                                <%#Eval("SRO_Name") %>
                                            </ItemTemplate>
                                            <HeaderStyle Width="10%" HorizontalAlign="Center" />
                                            <ItemStyle Width="10%" HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="Registered CaseNO" HeaderStyle-VerticalAlign="Top" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="false">
                                            <ItemTemplate>
                                                <%#Eval("Registered_CaseNO") %>
                                            </ItemTemplate>
                                            <HeaderStyle Width="10%" HorizontalAlign="Center" />
                                            <ItemStyle Width="10%" HorizontalAlign="Left" />
                                        </asp:TemplateField> <asp:TemplateField HeaderText="Case Registred Date" HeaderStyle-VerticalAlign="Top" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="false">
                                            <ItemTemplate>
                                                <%#Eval("Case_Registred_Date") %>
                                            </ItemTemplate>
                                            <HeaderStyle Width="10%" HorizontalAlign="Center" />
                                            <ItemStyle Width="10%" HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Action" HeaderStyle-VerticalAlign="Top" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="false">
                                            <ItemStyle Width="1%" HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkSelect" runat="server" OnClick="lnkSelect_Click" CssClass="btn btn-secondary">Select</asp:LinkButton>
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
