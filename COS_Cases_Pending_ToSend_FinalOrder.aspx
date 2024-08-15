<%@ Page Title="" Language="C#" MasterPageFile="~/CoS/CoSMaster.Master" AutoEventWireup="true" CodeBehind="COS_Cases_Pending_ToSend_FinalOrder.aspx.cs" Inherits="CMS_Sampada.CoS.COS_Cases_Pending_ToSend_FinalOrder" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section class="content-header">
        <div class="container-fluid">
            <div class="row mb-2">
                <div class="col-sm-6">
                    <h5>Cases Pending to Send Final Order</h5>
                </div>

            </div>
        </div>
    </section>
    <section class="content">
        <div class="container-fluid">
            <div class="row">
                <div class="col-12">
                    <div class="card">

                        <div class="col-lg-offset-12 col-md-offset-12 col-lg-12 col-md-12 col-sm-12 col-xs-12">
                            <div class="table-responsive listtabl">
                                <asp:GridView ID="grdCaseList" CssClass="table table-bordered table-condensed table-striped table-hover"
                                    DataKeyNames="APP_ID,APPLICATION_NUMBER,CASE_NUMBER,FINAL_ORDER_DATE,FINAL_PAYABLE_AMOUNT,PAYMENT_STATUS,PAYMENT_DATE,Apeal_Status"
                                    runat="server" CellPadding="4" ForeColor="#333333" GridLines="None"
                                    AutoGenerateColumns="False" Visible="true" Width="100%" EmptyDataText="Cases Not Found">
                                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="S No." HeaderStyle-VerticalAlign="Top" HeaderStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <itemtemplate><%#Container.DataItemIndex+1  %> </itemtemplate>
                                            </ItemTemplate>
                                            <ItemStyle Width="2%" HorizontalAlign="Center" />
                                            <HeaderStyle Width="2%" HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="APP ID" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Top">
                                            <HeaderStyle CssClass="centered-header" />
                                            <ItemTemplate>
                                                <div style="text-align: center">
                                                    <asp:Label ID="lblAPP_ID" runat="server" Text='<%# Eval("APP_ID") %>'></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="APPLICATION NUMBER" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Top">
                                            <HeaderStyle CssClass="centered-header" />
                                            <ItemTemplate>
                                                <div style="text-align: center">
                                                    <asp:Label ID="lblAPPLICATION_NUMBER" runat="server" Text='<%# Eval("APPLICATION_NUMBER") %>'></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="CASE NUMBER" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Top">
                                            <HeaderStyle CssClass="centered-header" />
                                            <ItemTemplate>
                                                <div style="text-align: center">
                                                    <asp:Label ID="lblCASE_NUMBER" runat="server" Text='<%# Eval("CASE_NUMBER") %>'></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="FINAL ORDER DATE" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Top">
                                            <HeaderStyle CssClass="centered-header" />
                                            <ItemTemplate>
                                                <div style="text-align: center">
                                                    <asp:Label ID="lblFINAL_ORDER_DATE" runat="server" Text='<%# Eval("FINAL_ORDER_DATE") %>'></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        
                                        <asp:TemplateField HeaderText="FINAL PAYABLE AMOUNT" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Top">
                                            <HeaderStyle CssClass="centered-header" />
                                            <ItemTemplate>
                                                <div style="text-align: center">
                                                    <asp:Label ID="lblFINAL_PAYABLE_AMOUNT" runat="server" Text='<%# Eval("FINAL_PAYABLE_AMOUNT") %>'></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        
                                        <asp:TemplateField HeaderText="PAYMENT STATUS" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Top">
                                            <HeaderStyle CssClass="centered-header" />
                                            <ItemTemplate>
                                                <div style="text-align: center">
                                                    <asp:Label ID="lblPAYMENT_STATUS" runat="server" Text='<%# Eval("PAYMENT_STATUS") %>'></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="PAYMENT DATE" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Top">
                                            <HeaderStyle CssClass="centered-header" />
                                            <ItemTemplate>
                                                <div style="text-align: center">
                                                    <asp:Label ID="lblPAYMENT_DATE" runat="server" Text='<%# Eval("PAYMENT_DATE") %>'></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Apeal Status" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Top">
                                            <HeaderStyle CssClass="centered-header" />
                                            <ItemTemplate>
                                                <div style="text-align: center">
                                                    <asp:Label ID="lblApeal_Status" runat="server" Text='<%# Eval("Apeal_Status") %>'></asp:Label>
                                                </div>
                                            </ItemTemplate>
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
    </section>
</asp:Content>