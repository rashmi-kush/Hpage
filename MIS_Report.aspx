<%@ Page Title="" Language="C#" MasterPageFile="~/CoS/CoSMaster.Master" AutoEventWireup="true" CodeBehind="MIS_Report.aspx.cs" Inherits="CMS_Sampada.CoS.MIS_Report" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <section class="content-header headercls">
        <div class="container-fluid">
            <div class="row">
                <div class="col-sm-3">
                    <h5>MIS Report</h5>
                </div>
            </div>
        </div>
    </section>

    <section class="content">
        <div class="container-fluid">
            <div class="row">

                <div class="col-12">
                    <div class="card">

                        <div class="card-header p-2 pt-1">

                            <div class="col-sm-6 p-0">
                                <ul class="nav nav-pills">
                                </ul>

                            </div>
                            <div class="align-items-end col-sm-12 w-100">
                                <%--<div class="text-right">--%>
                                <div class="row">
                                    <div class="col-2 my-3">
                                         <div class="input-group date" id="HDate" data-target-input="nearest">
                                            <asp:TextBox ID="txtHearingDate" runat="server" CssClass="form-control datetimepicker-input" data-target="#HDate" placeholder="Hearing Date" runat="server" OnTextChanged="btnShow_Click" AutoPostBack="true"></asp:TextBox>
                                            <div class="input-group-append" data-target="#HDate" data-toggle="datetimepicker">
                                                <div class="input-group-text"><i class="fa fa-calendar"></i></div>
                                            </div>
                                        </div>
                                       
                                    </div>

                                    <div class="col-2 my-3">
                                        <asp:TextBox ID="txtCaseOrigin" runat="server" class="form-control pl-15 bg-transparent" placeholder="Case Origin" Visible="false"></asp:TextBox>
                                         <asp:DropDownList ID="ddlCaseOrigin" runat="server" class="form-control pl-15 bg-transparent" runat="server" OnSelectedIndexChanged="btnShow_Click" AutoPostBack="true"></asp:DropDownList>
                                    </div>
                                    <div class="col-2 my-3">
                                        <asp:DropDownList ID="ddlCaseType" runat="server" class="form-control pl-15 bg-transparent" runat="server" OnSelectedIndexChanged="btnShow_Click" AutoPostBack="true"></asp:DropDownList>
                                    </div>
                                    <div class="col-2 my-3">
                                        <asp:DropDownList ID="ddlNoticeCount" runat="server" class="form-control pl-15 bg-transparent" runat="server" OnSelectedIndexChanged="btnShow_Click" AutoPostBack ="true"></asp:DropDownList>
                                    </div>
                                    <div class="col-2 my-3">
                                        <asp:TextBox ID="txtPaymentStatus" runat="server" class="form-control pl-15 bg-transparent" Visible="false" placeholder="Payment Status"></asp:TextBox>
                                         <asp:DropDownList ID="ddlPaymentStatus" runat="server" class="form-control pl-15 bg-transparent" runat="server" OnSelectedIndexChanged="btnShow_Click" AutoPostBack="true">
                                             <asp:ListItem Text ="Payment Status" Value="0"></asp:ListItem>
                                             <asp:ListItem Text ="PENDING" Value="1"></asp:ListItem>
                                              <asp:ListItem Text ="PARTIAL" Value="2"></asp:ListItem>
                                              <asp:ListItem Text ="FULL" Value="3"></asp:ListItem>
                                         </asp:DropDownList>

                                    </div>
                                    <div class="col-2 my-3">
                                        <asp:DropDownList ID="ddlStatus" runat="server" class="form-control pl-15 bg-transparent" Runat="server" OnSelectedIndexChanged="btnShow_Click" AutoPostBack="true"></asp:DropDownList>
                                    </div>
                                    <div class="col-2 my-3">
                                        <div class="input-group date" id="fromdate" data-target-input="nearest">
                                            <asp:TextBox ID="txtfromdate" runat="server" CssClass="form-control datetimepicker-input" data-target="#fromdate" placeholder="Case Registered From Date" runat="server" AutoPostBack="true" OnTextChanged="btnShow_Click"></asp:TextBox>
                                            <div class="input-group-append" data-target="#fromdate" data-toggle="datetimepicker">
                                                <div class="input-group-text"><i class="fa fa-calendar"></i></div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-2 my-3">
                                        <div class="input-group date" id="todate" data-target-input="nearest">
                                            <asp:TextBox ID="txttodate" runat="server" CssClass="form-control datetimepicker-input" data-target="#todate" placeholder="Case Registered To Date" runat="server" AutoPostBack="true" OnTextChanged="btnShow_Click"></asp:TextBox>
                                            <div class="input-group-append" data-target="#todate" data-toggle="datetimepicker">
                                                <div class="input-group-text"><i class="fa fa-calendar"></i></div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-6 my-3">
                                        <asp:Button ID="btnShow" runat="server" CssClass="btn btn-success" Text="Show" OnClick="btnShow_Click" />
                                    </div>

                                    <div class="col-2 my-3">
                                        <asp:TextBox ID="txtSearch" CssClass="form-control" onkeyup="Search_Gridview(this)" runat="server" ClientIDMode="Static" placeholder="Text Search"></asp:TextBox>
                                    </div>
                                </div>
                                <%--</div>--%>
                            </div>
                        </div>

                        <div class="card-body">
                            <div class="table-responsive listtabl">
                                <asp:GridView ID="grdMISReport" CssClass="table table-bordered table-condensed table-striped table-hover"
                                    DataKeyNames="App_ID" runat="server" CellPadding="4" ForeColor="#e9564e" GridLines="None" AutoGenerateColumns="False" Visible="true" Width="100%" EmptyDataText="No Record Found">
                                    <AlternatingRowStyle BackColor="White" ForeColor="#284775"/>
                                    <Columns>
                                        <asp:TemplateField HeaderText="SNo." HeaderStyle-VerticalAlign="Top" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="false">
                                            <ItemTemplate>
                                                <itemtemplate><%#Container.DataItemIndex+1  %> </itemtemplate>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Proposal No." HeaderStyle-VerticalAlign="Top" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="false">
                                            <ItemTemplate>
                                                <%#Eval("application_no") %>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="Impound Date" HeaderStyle-VerticalAlign="Top" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="false">
                                            <ItemTemplate>
                                                <%#Eval("Impound_Date") %>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Case No." HeaderStyle-VerticalAlign="Top" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="false">
                                            <ItemTemplate>
                                                <%#Eval("Case_Number") %>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                       
                                       
                                        <asp:TemplateField HeaderText="Case Registered Date" HeaderStyle-VerticalAlign="Top" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="false">
                                            <ItemTemplate>
                                                <%#Eval("Registered_Date") %>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="Case Type" HeaderStyle-VerticalAlign="Top" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="false">
                                            <ItemTemplate>
                                                <%#Eval("Case_type") %>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Case Origin" HeaderStyle-VerticalAlign="Top" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="false">
                                            <ItemTemplate>
                                                <%#Eval("Case_Origin") %>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="First OrderSheet Date" HeaderStyle-VerticalAlign="Top" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="false">
                                            <ItemTemplate>
                                                <%#Eval("First_Ordersheet_Date") %>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Notice Count" HeaderStyle-VerticalAlign="Top" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="false">
                                            <ItemTemplate>
                                                <%#Eval("Notice_Count") %>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="Notice Date" HeaderStyle-VerticalAlign="Top" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="false">
                                            <ItemTemplate>
                                              <%#Eval("NOTICEDATE") %>  
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Next Hearing Date" HeaderStyle-VerticalAlign="Top" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="false">
                                            <ItemTemplate>
                                                <%#Eval("Next_HearingDate") %>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Final Order Date" HeaderStyle-VerticalAlign="Top" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="false">
                                            <ItemTemplate>
                                                <%#Eval("FINAL_ORDER_DATE") %>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Payment Status" HeaderStyle-VerticalAlign="Top" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="false">
                                            <ItemTemplate>
                                                <%#Eval("PAYMENT_STATUS") %>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="Payment Date" HeaderStyle-VerticalAlign="Top" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="false">
                                            <ItemTemplate>
                                                 <%#Eval("Payment_Date") %>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="Endorsement Date" HeaderStyle-VerticalAlign="Top" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="false">
                                            <ItemTemplate>
                                                <%#Eval("ENDORSEMENT_DATE") %>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center"/> 
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="RRC Certificate Date" HeaderStyle-VerticalAlign="Top" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="false">
                                            <ItemTemplate>
                                                <%#Eval("RRC_Certificate_Date") %>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="Move to RRC Date" HeaderStyle-VerticalAlign="Top" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="false">
                                            <ItemTemplate>
                                                   <%#Eval("Moveto_rrc_dt") %>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Case Status" HeaderStyle-VerticalAlign="Top" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="false">
                                            <ItemTemplate>
                                                <%#Eval("status") %>
                                                
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Left" />
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

    <script type="text/javascript">
        function Search_Gridview(strKey) {
            var strData = strKey.value.toLowerCase().split(" ");
            var tblData = document.getElementById("<%=grdMISReport.ClientID %>");
            var rowData;
            for (var i = 1; i < tblData.rows.length; i++) {
                rowData = tblData.rows[i].innerHTML;
                var styleDisplay = 'none';
                for (var j = 0; j < strData.length; j++) {
                    if (rowData.toLowerCase().indexOf(strData[j]) >= 0)
                        styleDisplay = '';
                    else {
                        styleDisplay = 'none';
                        break;
                    }
                }
                tblData.rows[i].style.display = styleDisplay;
            }
        }
    </script>

</asp:Content>
