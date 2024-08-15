<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ResponseFromeSign_AddNoticeProceeding.aspx.cs" Inherits="CMS_Sampada.CoS.ResponseFromeSign_AddNoticeProceeding" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
     <title>:: eSign Document ::</title>
    <link href="../bootstrap/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="../bootstrap/css/bootstrap-theme.css" rel="stylesheet" />
    <link href="../CSS/jquery-ui.css" rel="stylesheet" type="text/css" />
    <link href="CSS/common.css" rel="stylesheet" type="text/css" />
    <script src="../Script/jquery-1.9.1.js" type="text/javascript"></script>
    <script src="../Script/jquery-ui.js" type="text/javascript"></script>
    <script src="../bootstrap/js/bootstrap.js" type="text/javascript"></script>
    <script src="../Script/jquery.base64.js"></script>

    <script src="../Scripts/sweetalert.min.js"></script>
    <link href="../Styles/sweetalert.css" rel="stylesheet" />
</head>
<body>
   <form id="form1" runat="server">
        <div class="container text-center" style="margin-top: 40px">
            <div class="row">
                <div class="col-xs-8 col-xs-offset-2">
                    <div class="row form-group">
                        <div class="col-xs-12">
                            <label>
                                <asp:Label runat="server" Visible="false" ID="lblStatus" Text=""></asp:Label>
                            </label>
                        </div>
                    </div>
                    <div class="row form-group">
                        <div class="col-xs-12">
                           <%-- <asp:Button CssClass="btn-primary" Visible="false" runat="server" ID="btnDownload" Text="Download" OnClick="btnDownload_Click" />--%>
                        </div>
                    </div>
                   <%-- <div class="row form-group">
                        <div class="col-xs-12">
                            <a href="eSignDocument.aspx">Back</a>
                        </div>
                    </div>--%>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
