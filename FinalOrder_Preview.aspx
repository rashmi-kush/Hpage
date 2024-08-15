<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FinalOrder_Preview.aspx.cs" Inherits="CMS_Sampada.CoS.FinalOrder_Preview" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>


<link rel="stylesheet"
        href="https://fonts.googleapis.com/css?family=Source+Sans+Pro:300,400,400i,700&display=fallback">
    <link rel="stylesheet" href="../dist/plugins/fontawesome-free/css/all.min.css">
    <link rel="stylesheet" href="https://code.ionicframework.com/ionicons/2.0.1/css/ionicons.min.css">
    <link rel="stylesheet" href="../dist/plugins/tempusdominus-bootstrap-4/css/tempusdominus-bootstrap-4.min.css">
    <link rel="stylesheet" href="../dist/plugins/icheck-bootstrap/icheck-bootstrap.min.css">
    <link rel="stylesheet" href="../dist/plugins/jqvmap/jqvmap.min.css">
    <link rel="stylesheet" href="../dist/css/adminlte.min.css">
    <link rel="stylesheet" href="../dist/plugins/overlayScrollbars/css/OverlayScrollbars.min.css">
    <link rel="stylesheet" href="../dist/plugins/daterangepicker/daterangepicker.css">
    <link rel="stylesheet" href="../dist/plugins/summernote/summernote-bs4.min.css">
    <link rel="stylesheet" href="../dist/plugins/datatables-bs4/css/dataTables.bootstrap4.min.css">
    <link rel="stylesheet" href="../dist/plugins/datatables-responsive/css/responsive.bootstrap4.min.css">
    <link rel="stylesheet" href="../dist/plugins/datatables-buttons/css/buttons.bootstrap4.min.css">

<script src="../Scripts/sweetalert.min.js"></script>
<link href="../Styles/sweetalert.css" rel="stylesheet" />
<script src="../assets/js/sweetalert2@11.js"></script>
<script src="../dist/plugins/jquery/jquery.min.js"></script>



<script>

    window.onload = function () {
        document.getElementById("dmpl").style.display = "none";
        /*alert("1");*/
    };

</script>


<style>
    .row {
        margin-right: 0px !important;
        margin-left: 0px !important;
    }
</style>

<script src="../assets/jquery-3.4.0.min.js"></script>



<section class="content-header">
    <div class="container-fluid">
        <div class="row mb-2">
            <%--  <div class="col-sm-6">
                    <h5>Impound Proposal</h5>
                </div>--%>
        </div>
    </div>
</section>

<nav class="main-header navbar navbar-expand navbar-white navbar-light" style="margin-left: 10px;" >
    <!-- Left navbar links -->
    <ul class="navbar-nav" id="dmpl">
        <li class="nav-item">
            <a class="nav-link" data-widget="pushmenu" href="#" role="button"><i class="fas fa-bars"></i></a>
        </li>
        <li class="nav-item d-none d-sm-inline-block">
            <a href="PartyHome" class="nav-link">Home</a>
        </li>
    </ul>

    <!-- Right navbar links -->
    <ul class="navbar-nav ml-auto">
        <li class="nav-item">
            <div class="user-panel d-flex">
                <div class="info">
                    

                </div>
                
            </div>
        </li>
    </ul>

</nav>

<section class="content">
    <div class="container-fluid">
        <div class="row">
            <div class="col-12">
                <div class="card">
                    <div class="card-body">
                        <div class="row">
                            <div class="col-2"></div>
                            <div class="col-8">
                                <div class="card card-primary">

                                    <div class="card-header" style="padding: 12px;">

                                        <div class="row">
                                            <div class="col-6">
                                                <h3 class="card-title mt-3" style="font-size: 16px;">Final Order : Preview </h3>
                                            </div>
                                            <div class="col-6">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="card-body">
                                        <div class="row p-2 pt-1 d-flex align-items-center">

                                            <iframe id="NoticePath" runat="server" style="width: 100%; height: 600px; border: 1px solid black;"></iframe>

                                            <%--<iframe id="NoticePath1" src="~/COS_Notice/IGRSCMS1000104_2023Oct12125131_Notice_Signed.pdf" runat="server" style="width: 100%; height: 600px; border: 1px solid black;"></iframe>--%>
                                        </div>

                                        <div class="row" id="btn_Ack">
                                            <div class="col-lg-4"></div>
                                            <div class="col-lg-4">
                                            </div>
                                            <div class="col-lg-4"></div>
                                        </div>
                                    </div>


                                </div>





                            </div>
                            <div class="col-2"></div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</section>



