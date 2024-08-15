<%@ Page Title="" Language="C#" MasterPageFile="~/CoS/CoSMaster.Master" AutoEventWireup="true" CodeBehind="Cause_List_Upload.aspx.cs" Inherits="CMS_Sampada.CoS.Cause_List_Upload" %>

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
                    <h5>Upload Cause List</h5>
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
                                <asp:Label runat="server" Text="Cause List Date:"></asp:Label>
                            </div>
                            <div class="col-3">

                                <div class="input-group date" id="fromdate" data-target-input="nearest">
                                    <asp:TextBox ID="txtCLDate" runat="server" CssClass="form-control datetimepicker-input" ClientIDMode="Static" onblur="setFormate(this)" data-target="#fromdate" data-format="dd/mm/yyyy" placeholder="Date"></asp:TextBox>
                                    <div class="input-group-append" data-target="#fromdate" data-toggle="datetimepicker">
                                        <div class="input-group-text"><i class="fa fa-calendar"></i></div>
                                    </div>
                                </div>

                                <asp:DropDownList ID="ddl_hearing" runat="server" Visible="false" CssClass="form-control" EnableTheming="false">
                                </asp:DropDownList>
                            </div>
                            <div class="col-3">

                                <asp:DropDownList ID="ddl_Template" runat="server" Visible="false" CssClass="form-control" EnableTheming="false">
                                </asp:DropDownList>

                            </div>


                        </div>
                    </asp:Panel>


                    <asp:Panel ID="Pnl2" Visible="true" runat="server">
                        <div class="row m-3">
                            <div class="col-sm-3">
                                <asp:Label runat="server" Text="Cause List Name:"></asp:Label>
                            </div>
                            <div class="col-sm-3">
                                <asp:TextBox ID="txtCLName" Enabled="true" onkeypress="return  onlyAlphabets(event,this);" ClientIDMode="Static" CssClass="form-control" runat="server" AutoCompleteType="None" autocomplete="off" placeholder="Name"></asp:TextBox>
                                <asp:TextBox ID="txtTemplateName" Visible="false" Enabled="true" onkeypress="return  onlyAlphabets(event,this);" ClientIDMode="Static" CssClass="form-control" runat="server" AutoCompleteType="None" autocomplete="off" placeholder="Name"></asp:TextBox>
                            </div>

                        </div>
                        <div class="form-group row m-3">
                            <div class="col-sm-3">
                                <asp:Label runat="server" Text="Upload File (pdf):"></asp:Label>
                            </div>
                            <div class="col-sm-6">
                                <asp:FileUpload ID="CoSUpload_Doc" runat="server" />
                                
                            </div>

                        </div>
                        <div class="form-group row m-1 d-flex justify-content-center">


                            <asp:Button ID="btnUpload" runat="server" Text="Upload" CssClass="btn btn-primary" Width="100px" Style="margin: 30px" OnClick="btnUpload_Click" />
                        </div>
                    </asp:Panel>

                </div>

                <div class="row mt-30">
                    <div class="col-auto">
                        <div class="input-group date" id="ddlsearch">
                            <asp:DropDownList ID="ddlSearchType" onchange="SetFilterTypeVisiblity(this)" runat="server" CssClass="form-control">
                                <asp:ListItem Text="--- Search Type ---" Value="0"></asp:ListItem>
                                <asp:ListItem Text="Cause List Name" Value="1"></asp:ListItem>
                                <asp:ListItem Text="Cause List Date" Value="2"></asp:ListItem>
                                <asp:ListItem Text="Cause List Upload Date" Value="3"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="row">

                        <div id="calDiv" class="col-6" style="display: none">
                            <div class="input-group date" id="todate" data-target-input="nearest">
                                <asp:TextBox ID="txtsearchdate" runat="server" CssClass="form-control datetimepicker-input" ClientIDMode="Static" onblur="setFormate(this)" data-target="#todate" data-format="dd/mm/yyyy" placeholder="Date"></asp:TextBox>
                                <div class="input-group-append" data-target="#todate" data-toggle="datetimepicker">
                                    <div class="input-group-text"><i class="fa fa-calendar"></i></div>
                                </div>
                            </div>
                        </div>
                        <div id="srhDiv" class="col-6">
                            <div class="input-group date" id="search">
                                <asp:TextBox ID="txtsearch" runat="server" CssClass="form-control" placeholder="Cause List Name"></asp:TextBox>
                            </div>


                        </div>
                        <div class="col-6">
                            <asp:Button ID="btnsearch" runat="server" CssClass="btn btn-info t-btn w-100" Text="Search" OnClick="btnsearch_Click" />

                        </div>

                    </div>

                </div>

                <div class="row mt-3">
                    <div class="col-12">
                         <div class="table table-responsive cst-help mb-0">
                        <asp:GridView ID="Grd_CauseList" DataKeyNames="causelist_id" runat="server" AutoGenerateColumns="false" Style="font-size: small; max-width: 100%;"
                            CssClass="table table-hover table-bordered table table-striped" BorderWidth="1px" CellPadding="4" ForeColor="#484848" OnRowCommand="Grd_CauseList_RowCommand">
                            <Columns>
                                <asp:TemplateField HeaderText="Select" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="false" HeaderStyle-VerticalAlign="Top">
                                    <HeaderStyle CssClass="centered-header" />
                                    <ItemTemplate>
                                        <div style="text-align: center">
                                            <asp:CheckBox ID="CheckBoxCauseList" runat="server" CssClass="custom-checkbox" />
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="SNo" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="false" HeaderStyle-VerticalAlign="Top">
                                    <HeaderStyle CssClass="centered-header" />
                                    <ItemTemplate>
                                        <%# Container.DataItemIndex + 1%>
                                        <asp:HiddenField ID="hdnCauseListID" runat="server" Value='<%#Eval("CAUSELIST_ID") %>'></asp:HiddenField>

                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="3%" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Cause List Name" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="false" HeaderStyle-VerticalAlign="Top">
                                    <HeaderStyle CssClass="centered-header" />
                                    <ItemTemplate>
                                        <div style="text-align: center" width="30%">
                                            <asp:Label ID="lblCLName" runat="server" Text='<%# Eval("CAUSE_LIST_NAME") %>'></asp:Label>
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Cause List Date" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="false" HeaderStyle-VerticalAlign="Top">
                                    <HeaderStyle CssClass="centered-header" />
                                    <ItemTemplate>
                                        <div style="text-align: center" width="15%">
                                            <asp:Label ID="lblCLDate" runat="server" Text='<%# Eval("CAUSE_LIST_DATE") %>'></asp:Label>
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>

                               
                                <asp:TemplateField HeaderText="Cause List Upload Date" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="false" HeaderStyle-VerticalAlign="Top" >
                                    <ItemTemplate>
                                        <div style="text-align: center" width="15%">
                                            <asp:Label ID="lblCLUploadDate" runat="server" Text='<%# Eval("INS_DATE") %>'></asp:Label>
                                        </div>
                                    </ItemTemplate>

                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="DRO ID" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="false" HeaderStyle-VerticalAlign="Top" >
                                    <ItemTemplate>
                                        <div style="text-align: center" width="15%">
                                            <asp:Label ID="lblDROID" runat="server" Text='<%# Eval("DRO_ID") %>'></asp:Label>
                                        </div>
                                    </ItemTemplate>

                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="View" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="false" HeaderStyle-VerticalAlign="Top" >
                                    <ItemTemplate>
                                        <div style="text-align: center" width="15%">
                                           <%-- <asp:Label ID="lblCLView" runat="server" Text='<%# Eval("DIST_ID") %>'></asp:Label>--%>
                                            <asp:LinkButton ID="lbtnCauselistDocPath" Text="View" runat="server" CommandName="Open" CommandArgument='<%# String.Format("{0}", Eval("causelist_id")) %>'></asp:LinkButton>
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
                <div class="row">
                    <div class="col-12">
                        <div class="col-3">
                            <asp:Button ID="btndelete" runat="server" CssClass="btn btn-danger" Text="Delete" Width="100px" OnClick="btndelete_Click" />
                            <a href="~/Sampada_Dashboard.aspx" role="button" id="btnClose" runat="server" class="btn btn-info" width="100px" style="padding-left: 30px; padding-right: 30px; margin-left: 5px">Close</a>
                        </div>

                    </div>               
                </div>
                <div class="row my-3 d-flex justify-content-center">
                   <iframe id="CauseList_iframe" runat="server" visible="false" style="width: 90%; height: 600px; border: 1px solid black;"></iframe>                   
                </div>
            </div>
        </div>
    </section>
    <script src="https://cdn.jsdelivr.net/npm/sweetalert2@10"></script>
    <script type="text/javascript">
        function setFormate(ths) {
            //alert("Hello");
            var dateVale = ths.value;
            if (dateVale != "") {
                let text = dateVale;
                const myArray = text.split("/");
                let mon = myArray[0];
                let dat = myArray[1];
                let yr = myArray[2];

                ths.value = dat + "/" + mon + "/" + yr
            }
        }
        function SetFilterTypeVisiblity(ths) {
            //alert(ths.value);
            if (ths.value == "1") {
                document.getElementById('calDiv').style.display = "None";
                document.getElementById('srhDiv').style.display = "Block";
            }
            else {
                document.getElementById('calDiv').style.display = "Block";
                document.getElementById('srhDiv').style.display = "None";
            }
        }
    </script>
</asp:Content>

