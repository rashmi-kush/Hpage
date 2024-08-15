<%@ Page Title="" Language="C#" MasterPageFile="~/CoS/CoSMaster.Master" AutoEventWireup="true" CodeBehind="CosTemplate.aspx.cs" Inherits="CMS_Sampada.CoS.CosTemplate" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style type="text/css">
        .cst-help th {
    height: 35px;
    background-color: #e9564e;
    color: #fff;
    font-size: 16px;
}
    </style>
     <section class="content-header">
        <div class="container-fluid">
            <div class="row mb-2">
                <div class="col-sm-6">
                    <h5>ADD COS Template</h5>
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
        <div class="card">
            <div class="container-fluid">
            
            <div style="border: 1px solid grey">
                <asp:Panel ID="pnl1" Visible="true" runat="server">
                    <div class="form-group row m-4 ">
                        <div class="col-sm-3">
                            <asp:Label runat="server" Text="Select Tempalte Type and Category:"></asp:Label>
                        </div>
                        <div class="col-3">

                            <asp:DropDownList ID="ddl_hearing" runat="server" CssClass="form-control" EnableTheming="false">
                            </asp:DropDownList>
                        </div>
                        <div class="col-3">

                            <asp:DropDownList ID="ddl_Template" runat="server" CssClass="form-control" EnableTheming="false">
                            </asp:DropDownList>

                        </div>


                    </div>
                </asp:Panel>


                <asp:Panel ID="Pnl2" Visible="true" runat="server">
                    <div class="row m-3">
                        <div class="col-sm-3">
                            <asp:Label runat="server" Text="Template Name:"></asp:Label>
                        </div>
                        <div class="col-sm-3">

                            <asp:TextBox ID="txtTemplateName" Enabled="true" onkeypress="return  onlyAlphabets(event,this);" ClientIDMode="Static" CssClass="form-control" runat="server" AutoCompleteType="None" autocomplete="off" placeholder="Name"></asp:TextBox>
                        </div>

                    </div>
                    <div class="form-group row m-3">
                        <div class="col-sm-3">
                            <asp:Label runat="server" Text="Template Description:"></asp:Label>
                        </div>
                        <div class="col-sm-6">

                            <textarea id="txtTemplateDescription" style="min-height: 100px;" oninput="autoResize(this)" onkeypress="return onlyAlphabets(event,this);" class="form-control" runat="server" autocompletetype="None" autocomplete="off" placeholder="Description.."></textarea>
                        </div>

                    </div>
                    <div class="form-group row m-1 d-flex justify-content-center">

                        <asp:Button ID="btnAdd" runat="server" CssClass="btn btn-primary" Text="Add" Width="100px" Style="margin: 30px" OnClick="btnTemplateSave_Click" />

                    </div>
                </asp:Panel>

            </div>
            <div class="row my-3 d-flex justify-content-center">

                <div class="col-auto">
                    <div class="input-group date" id="search">
                        <asp:TextBox ID="txtsearch" runat="server" CssClass="form-control" placeholder="Search ...."></asp:TextBox>
                    </div>
                </div>
                <div class="col-2">
                    <asp:Button ID="btnsearch" runat="server" CssClass="btn btn-info t-btn w-100" Text="Search Template Name" OnClick="btnsearch_Click" />

                </div>
                <div class="col-auto">
                    <asp:Label ID="btnsrchlbl" runat="server"> </asp:Label>
                </div>


            </div>
            <div class="row">
                <div class="col">
                    <div class="table table-responsive cst-help mb-0">
                        <asp:GridView ID="Grd_Template" runat="server" AutoGenerateColumns="false" Style="font-size: small; max-width: 100%;"
                            CssClass="table table-hover table-bordered table table-striped" BorderWidth="1px" CellPadding="4" ForeColor="#484848" OnRowCommand="Grd_Template_RowCommand" OnRowEditing="Grd_Template_RowEditing">
                            <Columns>
                                  <asp:TemplateField HeaderText="Select" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Top">
                                    <HeaderStyle CssClass="centered-header" />
                                    <ItemTemplate>
                                        <div style="text-align: center">
                                             <asp:CheckBox ID="CheckBoxTemplate" runat="server" CssClass="custom-checkbox" />
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="SNo" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Top">
                                    <HeaderStyle CssClass="centered-header" />
                                    <ItemTemplate>
                                        <%# Container.DataItemIndex + 1%>
                                        <asp:HiddenField ID="hdnTemplateID" runat="server" Value='<%#Eval("TEMP_ID") %>'></asp:HiddenField>
                                       
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="3%" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Type" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Top">
                                    <HeaderStyle CssClass="centered-header" />
                                    <ItemTemplate>
                                        <div style="text-align: center">
                                            <asp:Label ID="lbltype" runat="server" Text='<%# Eval("template_type_en") %>'></asp:Label>
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Category" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Top">
                                    <HeaderStyle CssClass="centered-header" />
                                    <ItemTemplate>
                                        <div style="text-align: center">
                                            <asp:Label ID="lblCategory" runat="server" Text='<%# Eval("template_category_en") %>'></asp:Label>
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Template Name" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Top">
                                    <ItemTemplate>
                                        <div style="text-align: center">
                                            <asp:Label ID="lblTemplateName" runat="server" Text='<%# Eval("template_name") %>'></asp:Label>
                                        </div>
                                    </ItemTemplate>

                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Template Description" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Top" ItemStyle-Width="30%">
                                    <ItemTemplate>
                                        <div style="text-align: center">
                                            <asp:Label ID="lblTemplateDescription" runat="server" Text='<%# TruncateText(Eval("template_discritpion"),50) %>'></asp:Label>
                                        </div>
                                    </ItemTemplate>

                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Action" HeaderStyle-VerticalAlign="Top" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="false">
                                    <ItemStyle HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkview" runat="server" CommandName="View" CommandArgument='<%# Eval("TEMP_ID") %>' CssClass="btn btn-secondary">View</asp:LinkButton>
                                        <asp:LinkButton ID="lnkedit" runat="server" CommandName="Edit" CommandArgument='<%# Eval("TEMP_ID") %>' CssClass="btn btn-secondary">Edit</asp:LinkButton>
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


            <div class="row my-3  d-flex justify-content-center">

                <div class="col-sm-auto">

                    <asp:Button ID="btndelete" runat="server" CssClass="btn btn-danger" Text="Delete" Width="100px" OnClick="btndelete_Click" />
                </div>

                <div class="col-sm-auto">
                    <a href="CoSHome.aspx" role="button" id="btnClose" runat="server" class="btn btn-info" width="100px" style="padding-left: 30px; padding-right: 30px; margin-left: 5px">Close</a>
                    <%--<asp:Button ID="btnSave" runat="server" CssClass="btn btn-info" Text="Close" Width="100px"></asp:Button>--%>
                </div>
            </div>
        </div>
        </div>
    </section>
    <script src="https://cdn.jsdelivr.net/npm/sweetalert2@10"></script>
</asp:Content>

