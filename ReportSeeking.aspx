<%@ Page Title="" Language="C#" MasterPageFile="~/CoS/CoSMaster.Master" AutoEventWireup="true" CodeBehind="ReportSeeking.aspx.cs" Inherits="CMS_Sampada.CoS.ReportSeeking" ValidateRequest="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Styles/sweetalert.css" rel="stylesheet" />
    <script src="../Scripts/sweetalert.min.js"></script>
    <script src="../assets/js/3.3.1/sweetalert2@11.js"></script>

    <style>
        .lineheight {
            line-height: 13px;
        }

        .nav-link {
            color: white;
        }

        .fa-download, .fa-print {
            color: white;
        }

        #lblToAdd {
            white-space: pre-line; /* Preserves whitespace and line breaks */
        }
    </style>
    <script type="text/javascript">
        /code: 48-57 Numbers/
        function restrictAlphabets(e) {


            var x = e.which || e.keycode;
            if ((x >= 48 && x <= 57))
                return true;
            else
                return false;
        }
    </script>
    <script type="text/javascript">


        function ShowDocList() {
            debugger;

            $("#List_pnl").show();
            $("#View_pnl").hide();
        }
        function openSrDoc(FILE_PATH) {

            //alert(FILE_PATH);
            //debugger;
            $("#List_pnl").hide();
            $("#View_pnl").show();
            $('#ifrDisplay').attr('src', FILE_PATH);

        }
        function openProposalDoc(PROPOSALPATH_FIRSTFORMATE) {

            //alert(PROPOSALPATH_FIRSTFORMATE);
            //debugger;
            $("#List_pnl").hide();
            $("#View_pnl").show();
            $('#ifrDisplay').attr('src', PROPOSALPATH_FIRSTFORMATE);


        }
        function openOrdersheetDoc(DocPath) {
            //alert(DocPath);
            //debugger;
            $("#List_pnl").hide();
            $("#View_pnl").show();
            $('#ifrDisplay').attr('src', DocPath);
        }

        function openNoticeDoc(NOTICE_DOCS) {
            //alert(NOTICE_DOCS);
            //debugger;
            $("#List_pnl").hide();
            $("#View_pnl").show();
            $('#ifrDisplay').attr('src', NOTICE_DOCS);
        }
        function openPartyDoc(REPLY_DOCS) {
            //alert("Rakesh bhai");
            //debugger;
            document.getElementById("toggleButton").addEventListener("click", function () {
                var div = document.getElementById("View_PartyReply");
                if (div.style.display === "none" || div.style.display === "") {
                    div.style.display = "block";
                } else {
                    div.style.display = "none";
                }
                $("#List_pnl").hide();
                $("#View_pnl").show();
                $('#ifrDisplay').attr('src', REPLY_DOCS);
            });

        }
        function openCoSUploadedDoc(DOCS_PATH) {
            //alert(DOCS_PATH);
            //debugger;
            $("#List_pnl").hide();
            $("#View_pnl").show();
            $('#ifrDisplay').attr('src', DOCS_PATH);
        }


        function DoCUploadMsg() {
            debugger;

            $("#custom-tabs-four-profile-tab").removeClass("active");
            $("#custom-tabs-four-profile").removeClass("active");

            $("#custom-tabs-four-home-tab").addClass("active");
            $("#custom-tabs-four-home").addClass("active");
            $("#custom-tabs-four-tabContent").addClass("active");





            Swal.fire({
                icon: 'success',
                title: 'Your file has been successfully uploaded to the server. Thank you',
                showCancelButton: false,
                confirmButtonText: 'OK',
                allowOutsideClick: false,
            });
        }

        function DocTypeErrorMsg() {

            Swal.fire({
                icon: 'error',
                title: 'Please selelct .doc, .docx, .pdf only  ..!',
                showCancelButton: false,
                confirmButtonText: 'OK',
                allowOutsideClick: false,
            });
        }

        function NoFileMessage() {


            Swal.fire({
                icon: 'error',
                title: 'Sorry, Please attached file',
                showCancelButton: false,
                confirmButtonText: 'OK',
                allowOutsideClick: false,
            });
        }
        function NoPartySelect() {

            //alert("Please Enter Copy Add Details...");
            Swal.fire({

                icon: 'info',
                title: 'Please Select Reason for Report !',
                showCancelButton: false,
                confirmButtonText: 'OK',
                allowOutsideClick: false,

            });




        }


        function SelectAuthority() {

            //alert("Please Enter Copy Add Details...");
            Swal.fire({

                icon: 'info',
                title: 'Please Select Authority for Report !',
                showCancelButton: false,
                confirmButtonText: 'OK',
                allowOutsideClick: false,

            });




        }


        function ValidateOtherDetails() {

            //alert("Please Enter Copy Add Details...");
            Swal.fire({

                icon: 'error',
                title: 'Please Enter Authority Details !',
                showCancelButton: false,
                confirmButtonText: 'OK',





            });


        }



        function SelectReasonCurrentSR() {

            //alert("Please Enter Copy Add Details...");
            Swal.fire({

                icon: 'info',
                title: 'Please Select Reason for Report !',
                showCancelButton: false,
                confirmButtonText: 'OK',
                allowOutsideClick: false,

            });

        }

        function SelectCurrentSR() {

            //alert("Please Enter Copy Add Details...");
            Swal.fire({

                icon: 'info',
                title: 'Please Select SR for Report !',
                showCancelButton: false,
                confirmButtonText: 'OK',
                allowOutsideClick: false,

            });

        }


    </script>
    <script type="text/javascript">
        //function ShowDocList() {
        //    //alert("Hello");
        //    $("#pnl2").show();
        //    $("#pnl3").hide();
        //}
        //function openPopup(PROPOSALPATH_FIRSTFORMATE) {
        //    //alert("Hello");
        //    //alert(PROPOSALPATH_FIRSTFORMATE);
        //    debugger;
        //    $("#pnl2").hide();
        //    $("#pnl3").show();
        //    $('#ifrDisplay').attr('src', PROPOSALPATH_FIRSTFORMATE);


        //    //$('#myModalDoc').modal('show');
        //    //$('#ifrDisplay').attr('src', PROPOSALPATH_FIRSTFORMATE);


        //}
        function AddReport_OtherSR() {

            $("#Other_tab").hide();
            $("#custom_tabs_one_other_tab").addClass("active");
            $("#custom-tabs-one-other").addClass("active show");

            

            $("#custom-tabs-one-otherr-tab").removeClass("active");
            $("#custom-tabs-one-original-tab").removeClass("active");
            $("#custom-tabs-one-edit").removeClass("active");

            document.getElementById("pContent_OtherSR").innerHTML = document.getElementById("summernote3").value;
            //alert("ssss");
        }


        function AddReport() {
            //var myElement = document.getElementById('custom-tabs-one-RegisteredForm');
            //href = "#custom-tabs-one-RegisteredForm";
            //alert(document.getElementById("pContent_OtherSR").innerHTML = document.getElementById("summernote3").value);
            document.getElementById("pContent").innerHTML = document.getElementById("summernote3").value;
            document.getElementById("pSRContent").innerHTML = document.getElementById("summernote3").value;
            document.getElementById("pContent_OtherSR").innerHTML = document.getElementById("summernote3").value;
            $("#custom-tabs-one-original-tab").removeClass("active");
            $("#custom_tabs_on_profile_tab,#custom-tabs-one-RegisteredForm").addClass("active");
            $("#custom-tabs-one-ProposalForm").removeClass("active");
            $("#custom-tabs-one-edit-tab,#custom-tabs-one-edit").removeClass("active");



        }

        function AddReport_other() {
            //var myElement = document.getElementById('custom-tabs-one-RegisteredForm');
            //href = "#custom-tabs-one-RegisteredForm";
            //alert(document.getElementById("pContent_OtherSR").innerHTML = document.getElementById("summernote3").value);
            document.getElementById("pContent").innerHTML = document.getElementById("summernote3").value;
            document.getElementById("pSRContent").innerHTML = document.getElementById("summernote3").value;
            document.getElementById("pContent_OtherSR").innerHTML = document.getElementById("summernote3").value;
            $("#custom-tabs-one-original-tab").removeClass("active");
            $("#custom_tabs_on_profile_tab").addClass("active");
            $("#custom-tabs-one-ProposalForm").removeClass("active");
            $("#custom-tabs-one-edit-tab,#custom-tabs-one-edit").removeClass("active");



        }


        function AddReport1() {
            //var myElement = document.getElementById('custom-tabs-one-RegisteredForm');
            //href = "#custom-tabs-one-RegisteredForm";



            $("#original-tab").hide();

            $("#ContentPlaceHolder1_custom_tabs_on_profile_tab").addClass("active");
            $("#custom-tabs-one-RegisteredForm").addClass("active");

            $("#custom-tabs-one-original-tab").removeClass("active");
            $("#custom-tabs-one-edit").removeClass("active");
            document.getElementById("pContent").innerHTML = document.getElementById("summernote3").value;
            //document.getElementById("pSRContent").innerHTML = document.getElementById("summernote3").value;
            //document.getElementById("pContent_OtherSR").innerHTML = document.getElementById("summernote3").value;

            $("#custom-tabs-one-edit-tab").removeClass("active");
            $("#Current_Report").hide();

        }



        function AddReport_CurrentSR() {

            /*$("#custom_tabs_one_home_tab").addClass("active");*/
            $("#ContentPlaceHolder1_custom_tabs_one_home_tab").addClass("active");
            $("#custom-tabs-one-ProposalForm").addClass("active show");
            $("#Current_Report").hide();


            $("#custom-tabs-one-original-tab").removeClass("active");
            $("#custom-tabs-one-edit").removeClass("active");
            $("#custom-tabs-one-edit-tab").removeClass("active");

            document.getElementById("pSRContent").innerHTML = document.getElementById("summernote3").value;
            

        }

        function OrignalToCurrent() {
            //var myElement = document.getElementById('custom-tabs-one-RegisteredForm');
            //href = "#custom-tabs-one-RegisteredForm";


            $("#custom_tabs_one_home_tab").addClass("active");
            $("#custom-tabs-one-tabContent").addClass("active");

            $("#ContentPlaceHolder1_custom_tabs_on_profile_tab").removeClass("active");
            $("#custom-tabs-one-RegisteredForm").removeClass("active");

        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <asp:ScriptManager ID="SCMNG" runat="server"></asp:ScriptManager>

    <div style="display: none;">
        <asp:AjaxFileUpload ID="AjaxFileUpload1" runat="server" />
    </div>
    <section class="content-header headercls">
        <div class="container-fluid">
            <div>
                <div class="col-sm-4">
                    <h5>Report Seeking</h5>
                </div>
            </div>

        </div>
    </section>

    <section class="content-header">
        <div class="container-fluid">

            <div class="row">
                <div class="col-sm-4">
                    <h6>Proposal No -
                        <asp:Label ID="lblProposalNo" Text="" runat="server"></asp:Label></h6>
                </div>
                <div class="col-sm-4">
                    <h6>Case No -
                        <asp:Label ID="lblCaseNumber" Text="" runat="server"></asp:Label></h6>
                </div>
                <div class="col-sm-4">
                    <h6>Case Registered Date -  
                        <asp:Label ID="lblRegisteredDate" runat="server"></asp:Label>
                    </h6>
                </div>
            </div>
        </div>
    </section>

    <section class="content">
        <div class="container-fluid">
            <div class="row">

                <div class="col-md-12">
                    <div class="card">
                        <div class="card-header p-2">
                            <ul class="nav nav-pills">
                                <li class="nav-item"><a class="nav-link disabled" href="#">Proposal</a></li>
                                <li class="nav-item"><a class="nav-link disabled" href="#">Order Sheet</a></li>
                                <li class="nav-item"><a class="nav-link disabled" href="#">Notice</a></li>
                                <li class="nav-item"><a class="nav-link active disabled" href="#">Seek Report</a></li>
                                <li class="nav-item"><a class="nav-link disabled" href="#">Notice Proceeding</a></li>
                                <li class="nav-item"><a class="nav-link disabled" href="#">Hearing</a></li>
                                <li class="nav-item"><a class="nav-link disabled" href="#">Final Order</a></li>
                                <li class="nav-item"><a class="nav-link disabled" href="#">Payment</a></li>
                                <li class="nav-item"><a class="nav-link disabled" href="#">Closed Cases</a></li>
                                <li>
                                    <asp:Button ID="btnSkip" class="btn btn-success" Text="Skip Seek Report" runat="server" OnClick="btnSkip_Click" Visible="true" />
                                </li>
                            </ul>
                        </div>
                        <div class="card-body">
                            <div class="tab-content">


                                <div class="tab-pane active" id="ReportSeeking">
                                    <div class="row">

                                        <div class="col-12 col-sm-6">
                                            <div class="card card-tabs h-100">
                                                <div class="card-header p-0 pt-1">
                                                    <ul class="nav nav-tabs" id="custom-tabs-one-tab" role="tablist">

                                                        <li class="nav-item">
                                                            <a class="nav-link active" id="custom_tabs_on_profile_tab" data-toggle="pill"
                                                                href="#custom-tabs-one-RegisteredForm" role="tab"
                                                                aria-controls="custom-tabs-one-profile" aria-selected="true" runat="server">Original SR</a>
                                                        </li>

                                                        <li class="nav-item">
                                                            <a class="nav-link" id="custom_tabs_one_home_tab" data-toggle="pill"
                                                                href="#custom-tabs-one-ProposalForm" role="tab" aria-controls="custom-tabs-one-home"
                                                                aria-selected="false" runat="server">Current SR </a>
                                                        </li>

                                                        <li class="nav-item">
                                                            <a class="nav-link" id="custom_tabs_one_other_tab" data-toggle="pill"
                                                                href="#custom-tabs-one-other" role="tab"
                                                                aria-controls="custom-tabs-one-other" aria-selected="true">Other </a>
                                                        </li>
                                                        <li class="nav-item" id="original-tab" style="display: none">
                                                            <a class="nav-link" id="custom-tabs-one-original-tab" data-toggle="pill"
                                                                href="#custom-tabs-one-edit" role="tab"
                                                                aria-controls="custom-tabs-one-edit" aria-selected="true">Edit Report </a>
                                                        </li>
                                                        <li class="nav-item" id="Current_Report" style="display: none">
                                                            <a class="nav-link" id="custom-tabs-one-edit-tab" data-toggle="pill"
                                                                href="#custom-tabs-one-edit" role="tab"
                                                                aria-controls="custom-tabs-one-edit" aria-selected="true">Current Edit Report </a>
                                                        </li>

                                                        <li class="nav-item" id="Other_tab" style="display: none">
                                                            <a class="nav-link" id="custom-tabs-one-otherr-tab" data-toggle="pill"
                                                                href="#custom-tabs-one-edit" role="tab"
                                                                aria-controls="custom-tabs-one-edit" aria-selected="true">Other Edit Report </a>
                                                        </li>
                                                    </ul>
                                                </div>

                                                <div class="card-body">
                                                    <div class="tab-content" id="custom-tabs-one-tabContent">

                                                        <div class="tab-pane active show" id="custom-tabs-one-RegisteredForm" role="tabpanel"
                                                            aria-labelledby="custom_tabs_on_profile_tab">
                                                            <!-- table -->
                                                            <%-- <div class="mycheck">
                                                                <div class="custom-control custom-checkbox mycb">

                                                                    <asp:CheckBox ID="chkCurrentSR" Checked="true" runat="server" />
                                                                    <label for="customCheckbox1">Original SR</label>
                                                                </div>
                                                                <div class="custom-control custom-checkbox">

                                                                    <asp:CheckBox ID="chkOriginalSR" runat="server" />
                                                                    <label for="customCheckbox2">Current SR</label>
                                                                </div>
                                                                
                                                                      <div class="col-sm-4">
                                                                 
                                                                   </div>
                                                               

                                                            </div>--%>

                                                            <div>

                                                                <div class="form-group row">
                                                                    <label for="inputEmail3" class="col-sm-2 col-form-label">SRO ID</label>
                                                                    <div class="col-sm-4">
                                                                        <%--<input type="text" class="form-control" id="input3">--%>
                                                                        <asp:TextBox ID="txtOrgSROID" ReadOnly="true" class="form-control" runat="server"></asp:TextBox>
                                                                    </div>
                                                                    <label for="inputEmail3" class="col-sm-2 col-form-label">Desigation</label>
                                                                    <div class="col-sm-4">
                                                                        <%--<input type="text" class="form-control" id="input4">--%>
                                                                        <asp:TextBox ID="txtOrgSRDesignation" ReadOnly="true" class="form-control" runat="server"></asp:TextBox>
                                                                    </div>
                                                                </div>

                                                                <div class="form-group row">
                                                                    <label for="inputEmail3" class="col-sm-2 col-form-label">Email ID</label>

                                                                    <div class="col-sm-4">
                                                                        <%--<input type="email" class="form-control" id="input5">--%>
                                                                        <asp:TextBox ID="txtOrgSREmail" ReadOnly="true" class="form-control" runat="server"></asp:TextBox>
                                                                    </div>
                                                                    <label for="inputEmail3" class="col-sm-2 col-form-label">Mobile No</label>
                                                                    <div class="col-sm-4">
                                                                        <%--<input type="text" class="form-control" id="input6">--%>
                                                                        <asp:TextBox ID="txtOrgSRMobile" ReadOnly="true" class="form-control" runat="server"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                                <div class="form-group row">
                                                                    <label for="inputEmail3" class="col-sm-2 col-form-label">SRO Name</label>
                                                                    <div class="col-sm-4 col-md-6 col-lg-10">
                                                                        <%--<input type="text" class="form-control" id="input6">--%>
                                                                        <asp:TextBox ID="txtOrgSROName" ReadOnly="true" class="form-control" runat="server"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                                <div class="form-group row">
                                                                    <label for="inputEmail3" class="col-sm-2 col-form-label">Office Add</label>
                                                                    <div class="col-sm-4 col-md-6 col-lg-10">
                                                                        <%--<input type="text" class="form-control" id="input6">--%>
                                                                        <asp:TextBox ID="txtOrgSROfficeAdd" ReadOnly="true" class="form-control" runat="server"></asp:TextBox>
                                                                    </div>
                                                                </div>

                                                                <div class="form-group row">
                                                                    <label for="inputEmail3" class="col-sm-2 col-form-label lineheight">Reason for Report</label>
                                                                    <div class="col-sm-4 col-md-6 col-lg-10">
                                                                        <%-- <select type="text" class="form-control" id="input1" placeholder="Select Reason for Report">
                                                                            <option>Select Reason for Report</option>
                                                                        </select>--%>


                                                                        <asp:DropDownList ID="ddlOrgReason" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlReason_SelectedIndexChanged" AutoPostBack="true">
                                                                            <%--<asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                                                            <asp:ListItem Text="For missing documents in proposal" Value="1"></asp:ListItem>
                                                                            <asp:ListItem Text="Regarding notice reply from party" Value="2"></asp:ListItem>--%>
                                                                        </asp:DropDownList>

                                                                    </div>
                                                                    <%-- <label for="inputEmail3" class="col-sm-2 col-form-label">Authority</label>
                                                                    <div class="col-sm-4">
                                                                        <select type="text" class="form-control" id="input2" placeholder="Select Authority">
                                                                            <option>Select Authority</option>
                                                                        </select>
                                                                    </div>--%>
                                                                </div>




                                                                <asp:Panel ID="pnlReason" Visible="false" runat="server">
                                                                    <div class="form-group row">
                                                                        <label for="inputEmail3" class="col-sm-2 col-form-label">Enter Reason</label>
                                                                        <div class="col-sm-4 col-md-6 col-lg-10">
                                                                            <%--<input type="text" class="form-control" id="input6">--%>
                                                                            <asp:TextBox ID="txtOrgOtherReason" class="form-control" runat="server"></asp:TextBox>
                                                                        </div>
                                                                    </div>
                                                                </asp:Panel>

                                                                <div class="row text-left">
                                                                    <div class="col-12">
                                                                        <asp:Button ID="btnCreateReport" runat="server" ClientIDMode="Static" OnClientClick="return validateReason()" class="btn btn-success" Text="Create Report" OnClick="btnCreateReport_Click" />

                                                                        <asp:Button ID="btnEdit_Report_OriginalSR" runat="server" Visible="false" OnClientClick="return EditOriginalSRReport()" class="btn btn-success" Text="Edit Report" />
                                                                    </div>
                                                                </div>
                                                                <br />
                                                                <div>
                                                                    <asp:Panel ID="pnlReport" runat="server" Visible="false">
                                                                        <div class="main-box html_box htmldoc">
                                                                            <h4 style="font-size: 18px; margin: 0; font-weight: 600;">न्यायालय कलेक्टर ऑफ़ स्टाम्प्स, 
                                                                                <asp:Label ID="lblHeadingDist" runat="server"></asp:Label>
                                                                                (म.प्र.)</h4>
                                                                            <%--<h4 style='font-size: 18px; margin: 0; font-weight: 600;'>न्यायालय कलेक्टर ऑफ़ स्टाम्प्स, गुना (म.प्र.)</h4>--%>

                                                                            <br />
                                                                            <div style="float: left;">
                                                                                <div class="row">
                                                                                    <p style="margin-top: 10px; float: left; padding-left: 20px">
                                                                                        <%--<b>To</b>--%>
                                                                                        <b>प्रति</b>
                                                                                    </p>
                                                                                </div>
                                                                                <div class="row" style="align-items: baseline; display: flex; flex-direction: column; line-height: 12px; padding-left: 90px">
                                                                                    <p>
                                                                                        <asp:Label ID="lblToDesig" runat="server"></asp:Label>
                                                                                    </p>
                                                                                    <p>
                                                                                        <asp:Label ID="lblToSRO" runat="server"></asp:Label>
                                                                                    </p>
                                                                                    <p style="margin: 0; padding: 0; line-height: 1.5; text-align: left;">
                                                                                        <asp:Label ID="lblToAdd" ClientIDMode="Static" runat="server"></asp:Label>
                                                                                    </p>
                                                                                </div>
                                                                                <br />
                                                                                <div>

                                                                                    <textarea id="summernotesubject" clientidmode="Static" visible="false" runat="server">
                                                             
                                                                                    </textarea>

                                                                                      <p style="text-align: left;"><strong>विषय :</strong>
                                                                                            <asp:Label ID="lblReasonSub_OrgSR" runat="server" />
                                                                                             
                                                                                          
                                                                                        </p>
                                                                                   

                                                                                     <%--<p id="pSubject"  style="margin-bottom: 4px; margin: 0;" runat="server" clientidmode="Static"><strong>विषय :</strong>
                                                                                        <asp:Label ID="lblReasonSub" runat="server" />
                                                                                        प्रस्ताव आईडी- 
                                                                                        <asp:Label ID="lblProposalSub" runat="server" />
                                                                                        के संदर्भ में,  प्रकरण क्रमांक -
                                                                                        <asp:Label ID="lblCaseNoSub" runat="server" />
                                                                                    </p>--%>


                                                                                </div>
                                                                                <br />
                                                                                <div class="row" style="align-items: baseline; display: flex; flex-direction: column; line-height: 26px; padding-left: 0px">
                                                                                    <p id="pContent" style="text-align: justify; margin-bottom: 4px;" runat="server" clientidmode="Static">
                                                                                    </p>
                                                                                </div>
                                                                                <p>&nbsp;</p>
                                                                                <div class="row">
                                                                                    <div class="col-6">
                                                                                        <p style="margin-top: 10px; float: left;">
                                                                                            दिनांक :
                                                                                    <asp:Label ID="lblTodate" runat="server"></asp:Label>
                                                                                        </p>
                                                                                    </div>
                                                                                    <div class="col-6">
                                                                                        <p style="margin-top: 10px;">
                                                                                            <%--<b>Collector of Stamp</b><br />--%>
                                                                                            <b>कलेक्टर ऑफ़ स्टाम्प्स</b><br />
                                                                                            <asp:Label ID="lblCOSOfficeNameHi" runat="server"></asp:Label>
                                                                                            <%-- <b>Guna</b>--%>
                                                                                        </p>
                                                                                    </div>
                                                                                </div>
                                                                            </div>

                                                                        </div>

                                                                    </asp:Panel>
                                                                </div>
                                                                <asp:Panel ID="pnlBtnSaveReport" Visible="false" runat="server">


<%--                                                                    <asp:Button ID="btnSaveReport" runat="server" ClientIDMode="Static" class="btn btn-primary" OnClick="btnSaveReport_Click" Text="Save Report" OnClientClick="Loader();" />--%>
                                                                    <asp:Button ID="btnSaveReport" runat="server" ClientIDMode="Static" class="btn btn-primary" OnClick="btnSaveReport_Click" Text="Save Report" UseSubmitBehavior="false" OnClientClick="this.disabled='true'; this.value='Please wait...'; Loader();" />


                                                                </asp:Panel>



                                                                <%--  <div class="certi_info bg-light">eSign on Order Sheet </div>--%>
                                                                <br>

                                                                <asp:Panel ID="pnlEsignDSC" Visible="false" runat="server">
                                                                    <div class="certi_info bg-light m16">eSign / DSC</div>
                                                                    <br />
                                                                    <div class="form-group row">
                                                                        <div class="col-sm-5">
                                                                            <asp:Label runat="server" Visible="false" ID="lblStatus" Text=""></asp:Label>
                                                                            <asp:DropDownList ID="ddl_SignOption" runat="server" CssClass="form-control">
                                                                                <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                                                                <asp:ListItem Text="eSign (using Aadhaar)" Value="1"></asp:ListItem>
                                                                                <asp:ListItem Text="DSC" Value="2"></asp:ListItem>

                                                                            </asp:DropDownList>
                                                                        </div>
                                                                        <div class="col-sm-5">
                                                                            <asp:TextBox ID="TxtLast4Digit" placeholder="Enter last 4 digit of Adhar Card" MaxLength="4" Enabled="true" onkeypress="return NumberOnly(event)" ClientIDMode="Static" CssClass="form-control" runat="server" AutoCompleteType="None" autocomplete="off"></asp:TextBox>
                                                                        </div>
                                                                        <div class="col-sm-2">

                                                                            <asp:Button ID="btnEsignDSC" runat="server" OnClick="btnEsignDSC_Click" class="btn btn-success" Text="eSign/DSC" UseSubmitBehavior="false" OnClientClick="this.disabled='true'; this.value='Please wait...'; Loader();" />
                                                                        </div>
                                                                    </div>
                                                                </asp:Panel>


                                                                <asp:Panel ID="pnlSendCurSR" runat="server" Visible="false">
                                                                    <h6><b>Send Report Through</b></h6>


                                                                    <div class="mycheck mul_check">
                                                                        <div class="custom-control custom-checkbox mycb">
                                                                            <%-- <input class="custom-control-input" type="checkbox" id="customCheckbox01" value="option1">
                                                                                <label for="customCheckbox01" class="custom-control-label">Mobile No</label>--%>
                                                                            <asp:CheckBox ID="checksms" runat="server" Text="SMS" Checked="true" />
                                                                        </div>
                                                                        <div class="custom-control custom-checkbox mycb">
                                                                            <%-- <input class="custom-control-input" type="checkbox" id="customCheckbox02" value="option1">
                                                                                <label for="customCheckbox02" class="custom-control-label">Email</label>--%>
                                                                            <asp:CheckBox ID="chkEmail" runat="server" Text="Email" Checked="true" />
                                                                        </div>
                                                                        <div class="custom-control custom-checkbox">
                                                                            <%-- <input class="custom-control-input" type="checkbox" id="customCheckbox03" value="option1">
                                                                                <label for="customCheckbox03" class="custom-control-label">WhatsApp No</label>--%>
                                                                            <asp:CheckBox ID="chechwhats" runat="server" Text="WhatsApp" Checked="true" />
                                                                        </div>
                                                                    </div>
                                                                    <div class="table-responsive listtabl">
                                                                        <br>

                                                                        <asp:GridView ID="grdPartyDisplay" CssClass="table table-bordered table-condensed table-striped table-hover"
                                                                            DataKeyNames="sro_id"
                                                                            runat="server" CellPadding="4" ForeColor="#333333" GridLines="None"
                                                                            AutoGenerateColumns="False" Visible="true" Width="100%" EmptyDataText="No record found">
                                                                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                                                            <Columns>
                                                                                <asp:TemplateField HeaderText="SNo." HeaderStyle-VerticalAlign="Top" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="false">
                                                                                    <ItemTemplate>
                                                                                        <itemtemplate><%#Container.DataItemIndex+1  %> </itemtemplate>
                                                                                    </ItemTemplate>
                                                                                    <ItemStyle Width="4%" HorizontalAlign="Center" />
                                                                                    <HeaderStyle Width="4%" HorizontalAlign="Center" />
                                                                                </asp:TemplateField>

                                                                                <asp:BoundField HeaderText="Name" DataField="OFFICE_NAME_HI" HeaderStyle-Font-Bold="false" />
                                                                                <asp:BoundField HeaderText="SMS" DataField="officeContactNumber" HeaderStyle-Font-Bold="false" />
                                                                                <asp:BoundField HeaderText="WhatsAPP" DataField="officeContactNumber" HeaderStyle-Font-Bold="false" />
                                                                                <asp:BoundField HeaderText="Email" DataField="email" HeaderStyle-Font-Bold="false" />



                                                                            </Columns>
                                                                            <EditRowStyle BackColor="#999999" />
                                                                            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                                                            <HeaderStyle BackColor="#e9564e" Font-Bold="True" ForeColor="White" />
                                                                            <%-- <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                                                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                                                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                                                    <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                                                    <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                                                    <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                                                    <SortedDescendingHeaderStyle BackColor="#6F8DAE" />--%>
                                                                        </asp:GridView>

                                                                    </div>
                                                                    <br />
                                                                    <asp:Button ID="btnSendOriginalSR" runat="server" ClientIDMode="Static" Text="Send" OnClick="btnSendOriginalSR_Click" UseSubmitBehavior="false" OnClientClick="this.disabled='true'; this.value='Please wait...'; Loader();"  class="btn btn-success" />
                                                                </asp:Panel>
                                                                <br>
                                                            </div>

                                                        </div>

                                                        <div class="tab-pane fade " id="custom-tabs-one-ProposalForm" role="tabpanel"
                                                            aria-labelledby="custom_tabs_one_home_tab">
                                                            <!-- code here -->
                                                            <asp:UpdatePanel ID="upnl1" runat="server">
                                                                <ContentTemplate>
                                                                    <div class="mycheck">
                                                                        <%-- <div class="custom-control custom-checkbox mycb">

                                                                    <asp:CheckBox ID="CheckBox1" Checked="true" runat="server" />
                                                                    <label for="customCheckbox1">Original SR</label>
                                                                </div>
                                                                <div class="custom-control custom-checkbox">

                                                                    <asp:CheckBox ID="CheckBox2" runat="server" />
                                                                    <label for="customCheckbox2">Current SR</label>
                                                                </div>
                                                                        --%>

                                                                        <div class="col-sm-12">
                                                                            <asp:DropDownList ID="ddlSRName" CssClass="form-control" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlSRName_SelectedIndexChanged">
                                                                            </asp:DropDownList>
                                                                        </div>




                                                                    </div>
                                                                    <hr />
                                                                    <br />
                                                                    <div>

                                                                        <div class="form-group row">
                                                                            <label for="inputEmail3" class="col-sm-2 col-form-label">SRO ID</label>
                                                                            <div class="col-sm-4">
                                                                                <%--<input type="text" class="form-control" id="input3">--%>
                                                                                <asp:TextBox ID="txtSROID" ReadOnly="true" class="form-control" runat="server"></asp:TextBox>
                                                                            </div>
                                                                            <label for="inputEmail3" class="col-sm-2 col-form-label">Desigation</label>
                                                                            <div class="col-sm-4">
                                                                                <%--<input type="text" class="form-control" id="input4">--%>
                                                                                <asp:TextBox ID="txtSRDesignation" ReadOnly="true" class="form-control" runat="server"></asp:TextBox>
                                                                            </div>
                                                                        </div>

                                                                        <div class="form-group row">
                                                                            <label for="inputEmail3" class="col-sm-2 col-form-label">Email ID</label>

                                                                            <div class="col-sm-4">
                                                                                <%--<input type="email" class="form-control" id="input5">--%>
                                                                                <asp:TextBox ID="txtSREmail" ReadOnly="true" class="form-control" runat="server"></asp:TextBox>
                                                                            </div>
                                                                            <label for="inputEmail3" class="col-sm-2 col-form-label">Mobile No</label>
                                                                            <div class="col-sm-4">
                                                                                <%--<input type="text" class="form-control" id="input6">--%>
                                                                                <asp:TextBox ID="txtSRMobile" ReadOnly="true" class="form-control" runat="server"></asp:TextBox>
                                                                            </div>
                                                                        </div>
                                                                        <div class="form-group row">
                                                                            <label for="inputEmail3" class="col-sm-2 col-form-label">SRO Name</label>
                                                                            <div class="col-sm-4 col-md-6 col-lg-10">
                                                                                <%--<input type="text" class="form-control" id="input6">--%>
                                                                                <asp:TextBox ID="txtSROName" ReadOnly="true" class="form-control" runat="server"></asp:TextBox>
                                                                            </div>
                                                                        </div>
                                                                        <div class="form-group row">
                                                                            <label for="inputEmail3" class="col-sm-2 col-form-label">Office Add</label>
                                                                            <div class="col-sm-4 col-md-6 col-lg-10">
                                                                                <%--<input type="text" class="form-control" id="input6">--%>
                                                                                <asp:TextBox ID="txtSROOfficeAdd" ReadOnly="true" class="form-control" runat="server"></asp:TextBox>
                                                                            </div>
                                                                        </div>

                                                                        <div class="form-group row">
                                                                            <label for="inputEmail3" class="col-sm-2 col-form-label lineheight">Reason for Report</label>
                                                                            <div class="col-sm-4 col-md-6 col-lg-10">
                                                                                <%-- <select type="text" class="form-control" id="input1" placeholder="Select Reason for Report">
                                                                            <option>Select Reason for Report</option>
                                                                        </select>--%>

                                                                                <asp:DropDownList ID="ddlSRReason" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlSRReason_SelectedIndexChanged" AutoPostBack="true">
                                                                                    <%--<asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                                                            <asp:ListItem Text="For missing documents in proposal" Value="1"></asp:ListItem>
                                                                            <asp:ListItem Text="Regarding notice reply from party" Value="2"></asp:ListItem>--%>
                                                                                </asp:DropDownList>


                                                                            </div>
                                                                        </div>


                                                                        <asp:Panel ID="PnlCurrentOtherRsn" Visible="false" runat="server">
                                                                            <div class="form-group row">
                                                                                <label for="inputEmail3" class="col-sm-2 col-form-label">Enter Reason</label>
                                                                                <div class="col-sm-4 col-md-6 col-lg-10">
                                                                                    <%--<input type="text" class="form-control" id="input6">--%>
                                                                                    <asp:TextBox ID="txtCrntOtherReason" class="form-control" runat="server"></asp:TextBox>
                                                                                </div>
                                                                            </div>
                                                                        </asp:Panel>

                                                                        <%-- <label for="inputEmail3" class="col-sm-2 col-form-label">Authority</label>
                                                                    <div class="col-sm-4">
                                                                        <select type="text" class="form-control" id="input2" placeholder="Select Authority">
                                                                            <option>Select Authority</option>
                                                                        </select>
                                                                    </div>--%>

                                                                        <asp:Button ID="btnCreateReportCurrent" runat="server" OnClientClick="return validateReason()" class="btn btn-success" Text="Create Report" OnClick="btnCreateReportCurrent_Click" />


                                                                        <asp:Button ID="btnEdit_Report_CurrentSR" runat="server" Visible="false" OnClientClick="return EditCurrentSRReport()" class="btn btn-success" Text="Edit Report" />
                                                                        <br />
                                                                        <asp:Panel ID="pnlSRReport" runat="server" Visible="false">
                                                                            <div>

                                                                                <div class="main-box html_box htmldoc">
                                                                                    <%--<h4 style='font-size: 18px; margin: 0; font-weight: 600;'>न्यायालय कलेक्टर ऑफ़ स्टाम्प्स, गुना (म.प्र.)</h4>--%>
                                                                                    <h4 style="font-size: 18px; margin: 0; font-weight: 600;">न्यायालय कलेक्टर ऑफ़ स्टाम्प्स, 
                                                                                <asp:Label ID="lblHeadingDist1" runat="server"></asp:Label>
                                                                                        (म.प्र.)</h4>
                                                                                    <br />
                                                                                    <div style="float: left;">
                                                                                        <div class="row">
                                                                                            <p style="margin-top: 10px; float: left; padding-left: 20px">
                                                                                                <%--<b>To</b>--%>
                                                                                                <b>प्रति</b>
                                                                                            </p>
                                                                                        </div>
                                                                                        <div class="row" style="align-items: baseline; display: flex; flex-direction: column; line-height: 12px; padding-left: 90px">
                                                                                            <p>
                                                                                                <asp:Label ID="lblSRDesign" runat="server"></asp:Label>
                                                                                            </p>

                                                                                            <p>
                                                                                                <asp:Label ID="lblToSROOffice" runat="server"></asp:Label>
                                                                                            </p>

                                                                                            <p style="margin: 0; padding: 0; line-height: 1.5; text-align: left;">
                                                                                                <asp:Label ID="lblSRAddress" ClientIDMode="Static" runat="server"></asp:Label>
                                                                                            </p>

                                                                                        </div>
                                                                                        <br />

                                                                                        <div>

                                                                                                                                                                         

                                                                                    <%--<p id="p2" style="text-align: justify; margin-bottom: 4px;" runat="server" clientidmode="Static">
                                                                                        In reference to the Proposal ID-<asp:Label ID="lblSRPROID" runat="server" />
                                                                                        regarding
                                                                                   <asp:Label ID="lblSRSUB" runat="server" />, Case Number -
                                                                                   <asp:Label ID="lblSRCaseNo" runat="server" />

                                                                                    </p>--%>


                                                                                            <p style="text-align: left;"><strong>विषय :</strong>
                                                                                            <asp:Label ID="lblReasonSub_CurrntSR" runat="server" />
                                                                                             
                                                                                          
                                                                                        </p>

                                                                                            <%--<p id="pSubject_Current" style="text-align: justify; margin-bottom: 4px;" runat="server" clientidmode="Static"><strong>विषय :</strong>
                                                                                                <asp:Label ID="lblSRSUB" runat="server" />
                                                                                                प्रस्ताव आईडी- 
                                                                                                <asp:Label ID="lblProposalSub_CURRENTSR" runat="server" />
                                                                                                के संदर्भ में,  प्रकरण क्रमांक -
                                                                                                <asp:Label ID="lblSRCaseNo" runat="server" />
                                                                                            </p>--%>



                                                                                        </div>
                                                                                        <br />
                                                                                        <div class="row" style="align-items: baseline; display: flex; flex-direction: column; line-height: 26px; padding-left: 0px">
                                                                                            <p id="pSRContent" style="text-align: justify; margin-bottom: 4px;" runat="server" clientidmode="Static">
                                                                                            </p>
                                                                                        </div>

                                                                                        <div class="row">
                                                                                            <div class="col-6">
                                                                                                <p style="margin-top: 10px; float: left;">
                                                                                                    दिनांक :
                                                                                    <asp:Label ID="lblDate" runat="server"></asp:Label>
                                                                                                </p>
                                                                                            </div>
                                                                                            <div class="col-6">
                                                                                                <p style="margin-top: 10px;">
                                                                                                    <%--<b>Collector of Stamp</b><br />--%>
                                                                                                    <b>कलेक्टर ऑफ़ स्टाम्प्स</b><br />
                                                                                                    <asp:Label ID="lblCOSOfficeNameHi1" runat="server"></asp:Label>
                                                                                                    <%--<b>Guna</b>--%>
                                                                                                </p>
                                                                                            </div>
                                                                                        </div>
                                                                                    </div>

                                                                                </div>

                                                                            </div>

                                                                             <asp:Panel ID="pnlBtnSaveReport_crnt" Visible="false" runat="server">


                                                                    <asp:Button ID="btnSRSaveReport" runat="server" ClientIDMode="Static" class="btn btn-primary" OnClick="btnSRSaveReport_Click" Text="Save Report" UseSubmitBehavior="false" OnClientClick="this.disabled='true'; this.value='Please wait...'; Loader();" />



                                                                </asp:Panel>

                                                                            

                                                                        </asp:Panel>






                                                                        <asp:Panel ID="pnlCurSRESign" Visible="false" runat="server">
                                                                            <div class="certi_info bg-light m16">eSign / DSC</div>
                                                                            <br />
                                                                            <div class="form-group row">
                                                                                <div class="col-sm-5">
                                                                                    <asp:Label runat="server" Visible="false" ID="Label1" Text=""></asp:Label>
                                                                                    <asp:DropDownList ID="ddl_SignOptionCurSR" runat="server" CssClass="form-control">
                                                                                        <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                                                                        <asp:ListItem Text="eSign (using Aadhaar)" Value="1"></asp:ListItem>
                                                                                        <asp:ListItem Text="DSC" Value="2"></asp:ListItem>

                                                                                    </asp:DropDownList>
                                                                                </div>
                                                                                <div class="col-sm-5">
                                                                                    <asp:TextBox ID="TxtLast4DigitCurSR" placeholder="Enter last 4 digit of Adhar Card" MaxLength="4" Enabled="true" onkeypress="return NumberOnly(event)" ClientIDMode="Static" CssClass="form-control" runat="server" AutoCompleteType="None" autocomplete="off"></asp:TextBox>
                                                                                </div>
                                                                                <div class="col-sm-2">

                                                                                    <asp:Button ID="btnSREsignDSC" runat="server" OnClick="btnSREsignDSC_Click" class="btn btn-success" Text="eSign/DSC" UseSubmitBehavior="false" OnClientClick="this.disabled='true'; this.value='Please wait...'; Loader();" />
                                                                                </div>
                                                                            </div>
                                                                        </asp:Panel>


                                                                        <asp:Panel ID="pnlSRSendParty" runat="server" Visible="false">
                                                                            <h6><b>Send Report Through</b></h6>


                                                                            <%--<div class="mycheck mul_check">
                                                                                <div class="custom-control custom-checkbox mycb">
                                                                                    <input class="custom-control-input" type="checkbox" id="customCheckbox11" value="option1">
                                                                                    <label for="customCheckbox1" class="custom-control-label">SMS</label>
                                                                                </div>
                                                                                <div class="custom-control custom-checkbox mycb">
                                                                                    <input class="custom-control-input" type="checkbox" id="customCheckbox22" value="option1">
                                                                                    <label for="customCheckbox2" class="custom-control-label">Email</label>
                                                                                </div>
                                                                                <div class="custom-control custom-checkbox">
                                                                                    <input class="custom-control-input" type="checkbox" id="customCheckbox33" value="option1">
                                                                                    <label for="customCheckbox2" class="custom-control-label">WhatsApp</label>
                                                                                </div>
                                                                            </div>--%>
                                                                            <div class="mycheck mul_check">
                                                                                <div class="custom-control custom-checkbox mycb">

                                                                                    <asp:CheckBox ID="chkSMS_CurrentSR" runat="server" Text="SMS" Checked="true" />
                                                                                </div>
                                                                                <div class="custom-control custom-checkbox mycb">

                                                                                    <asp:CheckBox ID="chkEmail_CurrentSR" runat="server" Text="Email" Checked="true" />
                                                                                </div>
                                                                                <div class="custom-control custom-checkbox">

                                                                                    <asp:CheckBox ID="chkWhatsApp_CurrentSR" runat="server" Text="WhatsApp" Checked="true" />
                                                                                </div>
                                                                            </div>

                                                                            <div class="table-responsive listtabl">
                                                                                <br>

                                                                                <asp:GridView ID="grdPartyDisplay_CurrentSR" CssClass="table table-bordered table-condensed table-striped table-hover"
                                                                                    DataKeyNames=""
                                                                                    runat="server" CellPadding="4" ForeColor="#333333" GridLines="None"
                                                                                    AutoGenerateColumns="False" Visible="true" Width="100%" EmptyDataText="No record found">
                                                                                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                                                                    <Columns>
                                                                                        <asp:TemplateField HeaderText="SNo." HeaderStyle-VerticalAlign="Top" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="false">
                                                                                            <ItemTemplate>
                                                                                                <itemtemplate><%#Container.DataItemIndex+1  %> </itemtemplate>
                                                                                            </ItemTemplate>
                                                                                            <ItemStyle Width="4%" HorizontalAlign="Center" />
                                                                                            <HeaderStyle Width="4%" HorizontalAlign="Center" />
                                                                                        </asp:TemplateField>

                                                                                        <asp:BoundField HeaderText="Name" DataField="office_name_hi" HeaderStyle-Font-Bold="false" />
                                                                                        <asp:BoundField HeaderText="SMS" DataField="MOBILE_NO" HeaderStyle-Font-Bold="false" />
                                                                                        <asp:BoundField HeaderText="WhatsAPP" DataField="MOBILE_NO" HeaderStyle-Font-Bold="false" />
                                                                                        <asp:BoundField HeaderText="Email" DataField="EMAIL" HeaderStyle-Font-Bold="false" />



                                                                                    </Columns>
                                                                                    <EditRowStyle BackColor="#999999" />
                                                                                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                                                                    <HeaderStyle BackColor="#e9564e" Font-Bold="True" ForeColor="White" />
                                                                                    <%-- <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                                                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                                                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                                                    <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                                                    <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                                                    <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                                                    <SortedDescendingHeaderStyle BackColor="#6F8DAE" />--%>
                                                                                </asp:GridView>

                                                                            </div>

                                                                            <br />
                                                                            <asp:Button ID="BtnSend_CurrentSR" runat="server" ClientIDMode="Static" Text="Send" OnClick="BtnSend_CurrentSR_Click" class="btn btn-success" UseSubmitBehavior="false" OnClientClick="this.disabled='true'; this.value='Please wait...'; Loader();" />
                                                                            <%--<asp:Button ID="BtnSendSR" runat="server" Text="Send" class="btn btn-success" />--%>
                                                                        </asp:Panel>
                                                                        <br>
                                                                    </div>
                                                                </ContentTemplate>
                                                                <Triggers>
                                                                    <asp:PostBackTrigger ControlID="btnSREsignDSC" />
                                                                </Triggers>
                                                            </asp:UpdatePanel>
                                                        </div>



                                                        <div class="tab-pane" id="custom-tabs-one-other" role="tabpanel"
                                                            aria-labelledby="custom_tabs_one_other_tab">

                                                            <asp:UpdatePanel ID="UpdPnl_OtherSR" runat="server">
                                                                <ContentTemplate>

                                                                    <div>

                                                                        <div class="form-group row">
                                                                            <label for="inputEmail3" class="col-sm-2 col-form-label">Authority</label>
                                                                            <div class="col-sm-4 col-md-6 col-lg-10">
                                                                                <asp:DropDownList ID="ddlAuthority" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlAuthority_SelectedIndexChanged" AutoPostBack="true">
                                                                                </asp:DropDownList>
                                                                            </div>
                                                                        </div>

                                                                        <asp:Panel ID="PnlAuthority" Visible="false" runat="server">
                                                                            <div class="form-group row">
                                                                                <label for="lblOtherAuthority" class="col-sm-2 col-form-label">Enter Authority</label>
                                                                                <div class="col-sm-4 col-md-6 col-lg-10">

                                                                                    <asp:TextBox ID="txtOtherAuthority" class="form-control" runat="server"></asp:TextBox>
                                                                                </div>
                                                                            </div>
                                                                        </asp:Panel>

                                                                        <hr>


                                                                        <div class="form-group row mb-0">

                                                                            <label for="inputEmail3" class="col-sm-2 col-form-label">Name</label>
                                                                            <div class="col-sm-4">

                                                                                <asp:TextBox ID="txtName" class="form-control" placeholder="Enter Name" runat="server"></asp:TextBox>
                                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ValidationGroup="AddCopyValidate" ForeColor="Red"
                                                                                    ControlToValidate="txtName" ErrorMessage=" Name required"></asp:RequiredFieldValidator>
                                                                            </div>


                                                                            <label for="inputEmail3" class="col-sm-2 col-form-label">Degisnation</label>
                                                                            <div class="col-sm-4">

                                                                                <asp:TextBox ID="txtDegisnation" class="form-control" placeholder="Enter Degisnation" runat="server"></asp:TextBox>
                                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ValidationGroup="AddCopyValidate" ForeColor="Red"
                                                                                    ControlToValidate="txtDegisnation" ErrorMessage=" Name required"></asp:RequiredFieldValidator>
                                                                            </div>




                                                                        </div>

                                                                        <div class="form-group row mb-lg-n4">
                                                                            <label for="inputEmail3" class="col-sm-2 col-form-label">Email ID</label>
                                                                            <div class="col-sm-4">
                                                                                <asp:TextBox ID="txtemail" class="form-control" placeholder="Enter Email ID" runat="server"></asp:TextBox>
                                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ValidationGroup="AddCopyValidate" ForeColor="Red"
                                                                                    ControlToValidate="txtemail" ErrorMessage=" Name required"></asp:RequiredFieldValidator>
                                                                                <asp:RegularExpressionValidator ID="REVEmail" runat="server" ValidationGroup="AddCopyValidate" ControlToValidate="txtemail" ErrorMessage="Enter Correct MailID" ForeColor="Red" ValidationExpression="^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$"></asp:RegularExpressionValidator>

                                                                            </div>
                                                                            <label for="inputEmail3" class="col-sm-2 col-form-label">Phone no</label>
                                                                            <div class="col-sm-4">
                                                                                <asp:TextBox ID="txtphoneNo" class="form-control" MaxLength="10" Enabled="true" ClientIDMode="Static" AutoCompleteType="None" onkeypress='return restrictAlphabets(event)' autocomplete="off" placeholder="Phone no" runat="server"></asp:TextBox>
                                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ValidationGroup="AddCopyValidate" ForeColor="Red"
                                                                                    ControlToValidate="txtphoneNo" ErrorMessage=" Name required"></asp:RequiredFieldValidator>

                                                                            </div>
                                                                        </div>

                                                                        <div class="form-group row mb-0">
                                                                            <label for="inputEmail3" class="col-sm-2 col-form-label">WhatsApp No</label>
                                                                            <div class="col-sm-4 col-md-6 col-lg-10">
                                                                                <asp:TextBox ID="txtWhatsapp" class="form-control" MaxLength="10" Enabled="true" ClientIDMode="Static" AutoCompleteType="None" onkeypress='return restrictAlphabets(event)' autocomplete="off" placeholder="Enter WhatsApp No" runat="server"></asp:TextBox>
                                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ValidationGroup="AddCopyValidate" ForeColor="Red"
                                                                                    ControlToValidate="txtWhatsapp" ErrorMessage=" Name required"></asp:RequiredFieldValidator>
                                                                            </div>
                                                                        </div>

                                                                        <div class="form-group row ">
                                                                            <label for="inputEmail3" class="col-sm-2 col-form-label">Office Add</label>
                                                                            <div class="col-sm-4 col-md-6 col-lg-10">
                                                                                <asp:TextBox ID="txtAddress" class="form-control" placeholder="Enter Office Address" runat="server"></asp:TextBox>

                                                                            </div>
                                                                        </div>

                                                                        <%-- <div class="form-group row">
                                                                    <label for="inputEmail3" class="col-sm-2 col-form-label">Reason for Report</label>
                                                                    <div class="col-sm-4">
                                                                        <select type="text" class="form-control" id="input1" placeholder="Select Reason for Report">
                                                                            <option>Select Reason for Report</option>
                                                                        </select>
                                                                    </div>
                                                                    
                                                                </div>--%>

                                                                        <div class="form-group row ">
                                                                            <label for="inputEmail3" class="col-sm-2 col-form-label lineheight">Reason for Report</label>
                                                                            <div class="col-sm-4 col-md-6 col-lg-10">


                                                                                <asp:DropDownList ID="ddlOrgReason_other" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlOrgReason_other_SelectedIndexChanged" AutoPostBack="true">
                                                                                </asp:DropDownList>

                                                                            </div>

                                                                        </div>


                                                                        <asp:Panel ID="PnlOtherRsn_other" Visible="false" runat="server">
                                                                            <div class="form-group row">
                                                                                <label for="inputEmail3" class="col-sm-2 col-form-label">Enter Reason</label>
                                                                                <div class="col-sm-4 col-md-6 col-lg-10">

                                                                                    <asp:TextBox ID="txtOrgOtherReason_OtherSR" class="form-control" runat="server"></asp:TextBox>
                                                                                </div>
                                                                            </div>
                                                                        </asp:Panel>


                                                                        <asp:Button ID="btnCreateReportOtherSR" runat="server" ClientIDMode="Static" ValidationGroup="AddCopyValidate" OnClientClick="return validateReason()" class="btn btn-success" Text="Create Report" OnClick="btnCreateReportOtherSR_Click" />
                                                                        <asp:Button ID="btnEditSROther" runat="server" Visible="false" OnClientClick="return EditOtherSRReport()" class="btn btn-success" Text="Edit Report" />
                                                                        <asp:Panel ID="pnlReport_OtherSR" runat="server" Visible="false">
                                                                            <div>

                                                                                <div class="main-box html_box htmldoc">
                                                                                    <%--<h4 style='font-size: 18px; margin: 0; font-weight: 600;'>न्यायालय कलेक्टर ऑफ़ स्टाम्प्स, गुना (म.प्र.)</h4>--%>
                                                                                    <h4 style="font-size: 18px; margin: 0; font-weight: 600;">न्यायालय कलेक्टर ऑफ़ स्टाम्प्स, 
                                                                                <asp:Label ID="lblHeadingDist2" runat="server"></asp:Label>
                                                                                        (म.प्र.)</h4>
                                                                                    <br />
                                                                                    <div style="float: left;">
                                                                                        <div class="row">
                                                                                            <p style="margin-top: 10px; float: left; padding-left: 20px">
                                                                                               <%-- <b>To</b>--%>
                                                                                                 <b>प्रति</b>
                                                                                            </p>
                                                                                        </div>
                                                                                        <div class="row" style="align-items: baseline; display: flex; flex-direction: column; line-height: 12px; padding-left: 90px">
                                                                                            <p>
                                                                                                <%--<asp:Label ID="lblToDesig_otherSR" runat="server"></asp:Label>--%>
                                                                                                <asp:Label ID="lblToDesig_otherSR" runat="server"></asp:Label>
                                                                                            </p>

                                                                                            <p>
                                                                                                <asp:Label ID="lblToSRO_otherSR" runat="server"></asp:Label>
                                                                                            </p>
                                                                                            <p style="margin: 0; padding: 0; line-height: 1.5; text-align: left;">
                                                                                                <asp:Label ID="lblToAdd_otherSR" runat="server"></asp:Label>
                                                                                            </p>
                                                                                        </div>
                                                                                        <br />
                                                                                       <%-- <p style="text-align: left;">
                                                                                            <b>Subject:  </b><span>In reference to the Proposal ID -
                                                                                        <asp:Label ID="lblProposalSub_OtherSR" runat="server" />
                                                                                                regarding
                                                                                        <asp:Label ID="lblReasonSub_OtherSR" runat="server" />, Case Number -
                                                                                        <asp:Label ID="lblCaseNoSub_OtherSR" runat="server" />
                                                                                            </span>
                                                                                        </p>--%>

                                                                                         <p style="text-align: left;"><strong>विषय :</strong>
                                                                                            <asp:Label ID="lblReasonSub_OtherSR" runat="server" />
                                                                                                <%--प्रस्ताव आईडी- 
                                                                                                <asp:Label ID="lblProposalSub_OtherSR" runat="server" />
                                                                                                के संदर्भ में,  प्रकरण क्रमांक -
                                                                                                <asp:Label ID="lblCaseNoSub_OtherSR" runat="server" />--%>
                                                                                            </span>
                                                                                        </p>


                                                                                        <br />
                                                                                        <div class="row" style="align-items: baseline; display: flex; flex-direction: column; line-height: 26px; padding-left: 0px">
                                                                                            <p id="pContent_OtherSR" style="text-align: justify; margin-bottom: 4px;" runat="server" clientidmode="Static">
                                                                                            </p>
                                                                                        </div>

                                                                                        <div class="row">
                                                                                            <div class="col-6">
                                                                                                <p style="margin-top: 10px; float: left;">
                                                                                                    दिनांक :
                                                                                    <asp:Label ID="lblDate_OtherSR" runat="server"></asp:Label>
                                                                                                </p>
                                                                                            </div>
                                                                                            <div class="col-6">
                                                                                                <p style="margin-top: 10px;">
                                                                                                    <%--<b>Collector of Stamp</b><br />--%>
                                                                                                    <b>कलेक्टर ऑफ़ स्टाम्प्स</b><br />
                                                                                                    <asp:Label ID="lblCOSOfficeNameHi2" runat="server"></asp:Label>
                                                                                                    <%--<b>Guna</b>--%>
                                                                                                </p>
                                                                                            </div>
                                                                                        </div>
                                                                                    </div>

                                                                                </div>

                                                                            </div>



                                                                            <asp:Button ID="btnSaveReport_OtherSR" runat="server" ClientIDMode="Static" class="btn btn-primary" OnClick="btnSaveReport_OtherSR_Click" Text="Save Report" UseSubmitBehavior="false" OnClientClick="this.disabled='true'; this.value='Please wait...'; Loader();" />


                                                                        </asp:Panel>




                                                                        <br>

                                                                        <asp:Panel ID="PnlEsign_OtherSR" Visible="false" runat="server">
                                                                            <div class="certi_info bg-light m16">eSign / DSC</div>
                                                                            <br />
                                                                            <div class="form-group row">
                                                                                <div class="col-sm-5">
                                                                                    <asp:Label runat="server" Visible="false" ID="Label2" Text=""></asp:Label>
                                                                                    <asp:DropDownList ID="ddl_SignOptionOther" runat="server" CssClass="form-control">
                                                                                        <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                                                                        <asp:ListItem Text="eSign (using Aadhaar)" Value="1"></asp:ListItem>
                                                                                        <asp:ListItem Text="DSC" Value="2"></asp:ListItem>

                                                                                    </asp:DropDownList>
                                                                                </div>
                                                                                <div class="col-sm-5">
                                                                                    <asp:TextBox ID="TxtLast4DigitOther" placeholder="Enter last 4 digit of Adhar Card" MaxLength="4" Enabled="true" onkeypress="return NumberOnly(event)" ClientIDMode="Static" CssClass="form-control" runat="server" AutoCompleteType="None" autocomplete="off"></asp:TextBox>
                                                                                </div>
                                                                                <div class="col-sm-2">

                                                                                    <asp:Button ID="btnEsignDSC_OtherSR" runat="server" OnClick="btnEsignDSC_OtherSR_Click" class="btn btn-success" Text="eSign/DSC" UseSubmitBehavior="false" OnClientClick="this.disabled='true'; this.value='Please wait...'; Loader();" />
                                                                                </div>
                                                                            </div>
                                                                        </asp:Panel>


                                                                        <asp:Panel ID="PnlSend_OtherSR" runat="server" Visible="false">
                                                                            <h6><b>Send Report Through</b></h6>


                                                                            <div class="mycheck mul_check">
                                                                                <div class="custom-control custom-checkbox mycb">

                                                                                    <asp:CheckBox ID="chkSMS_OtherSR" runat="server" Text="SMS" Checked="true" />
                                                                                </div>
                                                                                <div class="custom-control custom-checkbox mycb">

                                                                                    <asp:CheckBox ID="chkEmail_OtherSR" runat="server" Text="Email" Checked="true" />
                                                                                </div>
                                                                                <div class="custom-control custom-checkbox">

                                                                                    <asp:CheckBox ID="chkWhatsApp_OtherSR" runat="server" Text="WhatsApp" Checked="true" />
                                                                                </div>
                                                                            </div>
                                                                            <div class="table-responsive listtabl">
                                                                                <br>

                                                                                <asp:GridView ID="grdOtherSrDetails" CssClass="table table-bordered table-condensed table-striped table-hover"
                                                                                    DataKeyNames="APP_ID"
                                                                                    runat="server" CellPadding="4" ForeColor="#333333" GridLines="None"
                                                                                    AutoGenerateColumns="False" Visible="true" Width="100%" EmptyDataText="No record found">
                                                                                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                                                                    <Columns>
                                                                                        <asp:TemplateField HeaderText="SNo." HeaderStyle-VerticalAlign="Top" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="false">
                                                                                            <ItemTemplate>
                                                                                                <itemtemplate><%#Container.DataItemIndex+1  %> </itemtemplate>
                                                                                            </ItemTemplate>
                                                                                            <ItemStyle Width="4%" HorizontalAlign="Center" />
                                                                                            <HeaderStyle Width="4%" HorizontalAlign="Center" />
                                                                                        </asp:TemplateField>

                                                                                        <asp:BoundField HeaderText="Name" DataField="AUTHORITY_NAME" HeaderStyle-Font-Bold="false" />
                                                                                        <asp:BoundField HeaderText="SMS" DataField="PHONE_NO" HeaderStyle-Font-Bold="false" />
                                                                                        <asp:BoundField HeaderText="WhatsAPP" DataField="WHATSAPP_NO" HeaderStyle-Font-Bold="false" />
                                                                                        <asp:BoundField HeaderText="Email" DataField="EMAIL_ID" HeaderStyle-Font-Bold="false" />



                                                                                    </Columns>
                                                                                    <EditRowStyle BackColor="#999999" />
                                                                                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                                                                    <HeaderStyle BackColor="#e9564e" Font-Bold="True" ForeColor="White" />
                                                                                    <%-- <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                                                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                                                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                                                    <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                                                    <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                                                    <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                                                    <SortedDescendingHeaderStyle BackColor="#6F8DAE" />--%>
                                                                                </asp:GridView>

                                                                            </div>
                                                                            <br />
                                                                            <asp:Button ID="btnSendOtherSR" runat="server" ClientIDMode="Static" Text="Send" OnClick="btnSendOtherSR_Click" class="btn btn-success" UseSubmitBehavior="false" OnClientClick="this.disabled='true'; this.value='Please wait...'; Loader();" />
                                                                        </asp:Panel>




                                                                        <br>
                                                                    </div>
                                                                </ContentTemplate>

                                                                <Triggers>
                                                                    <asp:PostBackTrigger ControlID="btnEsignDSC_OtherSR" />
                                                                </Triggers>


                                                            </asp:UpdatePanel>

                                                        </div>

                                                        <div class="tab-pane" id="custom-tabs-one-edit" role="tabpanel"
                                                            aria-labelledby="custom_tabs_one_other_tab">

                                                            <div>

                                                                <h6>Report Detail :</h6>
                                                                <textarea id="summernote3" clientidmode="Static" runat="server">
                                                             
                                                                </textarea>
                                                                <a onclick="AddReport1()" id="ReportOrginalSR" href="#custom_tabs_on_profile_tab" style="display: none" class="btn btn-success">Save Original SR Report</a>
                                                                <a onclick="AddReport_CurrentSR()" id="ReportCurrentSR" href="#custom_tabs_one_home_tab" style="display: none" class="btn btn-success">Save Current SR Report</a>
                                                                <a onclick="AddReport_OtherSR()" id="ReportOtherSR" href="#custom_tabs_one_other_tab" style="display: none" class="btn btn-success">Save Other SR Report</a>

                                                            </div>

                                                        </div>



                                                    </div>
                                                    <%-- <button type="button" class="btn btn-success">Send </button>--%>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="col-12 col-sm-6">
                                            <div class="card  card-tabs h-100">
                                                <div class="card-header p-0 pt-1 d-flex align-items-center">
                                                    <div class="col-sm-8 p-0">
                                                        <ul class="nav nav-tabs" id="custom-tabs-two-tab" role="tablist">
                                                            <li class="nav-item">
                                                                <a class="nav-link active" id="custom-tabs-two-home-tab" data-toggle="pill"
                                                                    href="#custom-tabs-two-home" role="tab" aria-controls="custom-tabs-two-home"
                                                                    aria-selected="true">Document </a>
                                                            </li>

                                                        </ul>
                                                    </div>
                                                    <div class="col-sm-4">
                                                        <div class="align-items-end d-flex justify-content-end mr-3 w-100">
                                                            <a href="#" id="btnShowDownloadOption" onclick="ShowDownloadOption()"><i class="fa fa-download mr-3 "></i></a>
                                                            <a href="#" onclick="printPDF()"><i class="fa fa-print"></i></a>
                                                        </div>
                                                    </div>
                                                </div>



                                                <div class="card-body">
                                                    <div class="tab-content" id="custom-tabs-two-tabContent">
                                                        <div class="tab-pane fade show active" id="custom-tabs-two-home" role="tabpanel"
                                                            aria-labelledby="custom-tabs-two-home-tab">

                                                            <div class="col-12 col-lg-12">
                                                                <div class="card  card-outline card-outline-tabs">
                                                                    <div class="card-header p-0 border-bottom-0 a-clr">
                                                                        <ul class="nav nav-tabs" id="custom-tabs-four-tab" role="tablist">
                                                                            <li class="nav-item">
                                                                                <%--<a class="nav-link" id="custom-tabs-four-home-tab"  data-toggle="pill" href="#custom-tabs-four-home" role="tab" aria-controls="custom-tabs-four-home" aria-selected="false">TOC</a>--%>
                                                                                <a class="nav-link" id="custom-tabs-four-home-tab" onclick="ShowDocList()" data-toggle="pill" href="#custom-tabs-four-home" role="tab" aria-controls="custom-tabs-four-home" aria-selected="false">Index</a>
                                                                            </li>
                                                                            <li class="nav-item">
                                                                                <a class="nav-link active" id="custom-tabs-four-profile-tab" data-toggle="pill" href="#custom-tabs-four-profile" role="tab" aria-controls="custom-tabs-four-profile" aria-selected="false">Recent</a>
                                                                            </li>
                                                                            <li class="nav-item">
                                                                                <a class="nav-link" id="custom-tabs-four-messages-tab" data-toggle="pill" href="#custom-tabs-four-messages" role="tab" aria-controls="custom-tabs-four-messages" aria-selected="false">All</a>
                                                                            </li>
                                                                            <li class="nav-item">
                                                                                <a class="nav-link" id="custom-tabs-four-settings-tab" style="pointer-events: none" data-toggle="pill" href="#custom-tabs-four-settings" role="tab" aria-controls="custom-tabs-four-settings" aria-selected="true">Previous Proceeding</a>
                                                                            </li>
                                                                        </ul>
                                                                    </div>
                                                                    <div class="card-body">
                                                                        <div class="tab-content" id="custom-tabs-four-tabContent">
                                                                            <div class="tab-pane" id="custom-tabs-four-home" role="tabpanel" aria-labelledby="custom-tabs-four-home-tab">


                                                                                <h5>List of Documents </h5>
                                                                                <hr />

                                                                                <div id="List_pnl">
                                                                                    <asp:UpdatePanel runat="server">
                                                                                        <ContentTemplate>

                                                                                            <asp:GridView ID="grdSRDoc" CssClass="table table-bordered table-condensed table-striped table-hover"
                                                                                                DataKeyNames="srn"
                                                                                                runat="server" CellPadding="4" ForeColor="#333333" GridLines="Vertical" AllowSorting="true" AutoGenerateColumns="false" Visible="true" EmptyDataText="No record found">
                                                                                                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                                                                                <Columns>
                                                                                                    <asp:TemplateField HeaderText="SNo." HeaderStyle-VerticalAlign="Top" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="false">
                                                                                                        <ItemTemplate>
                                                                                                            <itemtemplate><%#Container.DataItemIndex+1%> </itemtemplate>
                                                                                                        </ItemTemplate>
                                                                                                        <ItemStyle Width="5%" HorizontalAlign="Center" />
                                                                                                        <HeaderStyle Width="5%" HorizontalAlign="Center" />
                                                                                                    </asp:TemplateField>
                                                                                                    <asp:TemplateField HeaderText="Document Name" HeaderStyle-Font-Bold="false">
                                                                                                        <ItemTemplate>
                                                                                                            <%#Eval("DocumentType") %>
                                                                                                        </ItemTemplate>
                                                                                                        <HeaderStyle Width="15%" HorizontalAlign="Center" />
                                                                                                        <ItemStyle Width="15%" HorizontalAlign="Center" />
                                                                                                    </asp:TemplateField>
                                                                                                    <%--<asp:TemplateField HeaderText="Document Name" SortExpression="DocName" HeaderStyle-Font-Bold="false">
                                                                                                                <ItemTemplate>
                                                                                                                    <%#Eval("DOC_NAME") %>
                                                                                                                </ItemTemplate>
                                                                                                                <HeaderStyle Width="10%" HorizontalAlign="Center" />

                                                                                                                <ItemStyle Width="10%" HorizontalAlign="Center" />
                                                                                                            </asp:TemplateField>--%>
                                                                                                    <asp:TemplateField HeaderText="Uploaded By" SortExpression="DocName" HeaderStyle-Font-Bold="false">
                                                                                                        <ItemTemplate>
                                                                                                            <%#Eval("UploadedBy") %>
                                                                                                        </ItemTemplate>
                                                                                                        <HeaderStyle Width="10%" HorizontalAlign="Center" />

                                                                                                        <ItemStyle Width="10%" HorizontalAlign="Center" />
                                                                                                    </asp:TemplateField>
                                                                                                    <asp:TemplateField HeaderText="Attached On" SortExpression="INSDATE" HeaderStyle-Font-Bold="false">
                                                                                                        <ItemTemplate>
                                                                                                            <%-- <%#Eval("INSDATE") %>--%>
                                                                                                            <%# ((DateTime)Eval("INSDATE")).ToString("dd-MM-yyyy") %>
                                                                                                        </ItemTemplate>
                                                                                                        <HeaderStyle Width="10%" HorizontalAlign="Center" />

                                                                                                        <ItemStyle Width="10%" HorizontalAlign="Center" />
                                                                                                    </asp:TemplateField>

                                                                                                    <asp:TemplateField HeaderText="Pages" HeaderStyle-Font-Bold="false">

                                                                                                        <HeaderStyle Width="10%" HorizontalAlign="Center" />

                                                                                                        <ItemStyle Width="10%" HorizontalAlign="Center" />
                                                                                                    </asp:TemplateField>


                                                                                                    <asp:TemplateField HeaderText="View" HeaderStyle-Font-Bold="false">
                                                                                                        <ItemTemplate>

                                                                                                            <a href="#" class="gridViewToolTip" id="btnModalPopup" onclick='showPdfBYHendler("<%# Eval("FILE_PATH")%>","<%# Eval("docType")%>","<%# Eval("EREG_ID")%>")'>
                                                                                                                <button type="button" class="btn btn-info">
                                                                                                                    <i class="fas fa-eye"></i>
                                                                                                                </button>
                                                                                                            </a>
                                                                                                            <%--<a href="#" class="gridViewToolTip" id="btnModalPopup" onclick='showPdfBYHendler("<%# Eval("FILE_PATH")%>","<%# Eval("docType")%>")'>

                                                                                                                        <button type="button" class="btn btn-info"><i class="fas fa-eye"></i></button>
                                                                                                                    </a>--%>

                                                                                                            <%-- <a href="#" class="gridViewToolTip" id="btnModalPopup" onclick='openPopup("<%# Eval("FILE_PATH")%>")'>

                                                                                                        <button type="button" class="btn btn-info"><i class="fas fa-eye"></i></button>
                                                                                                    </a>--%>
                                                                                                        </ItemTemplate>
                                                                                                        <HeaderStyle Width="10%" HorizontalAlign="Center" />
                                                                                                        <ItemStyle Width="10%" HorizontalAlign="Center" />
                                                                                                    </asp:TemplateField>
                                                                                                </Columns>
                                                                                                <EditRowStyle BackColor="#999999" />
                                                                                                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                                                                                <HeaderStyle BackColor="#e9564e" Font-Bold="false" ForeColor="White" />
                                                                                                <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                                                                                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                                                                                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                                                                                <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                                                                                <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                                                                                <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                                                                                <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                                                                                            </asp:GridView>

                                                                                            <%--  <asp:GridView ID="grdSRDoc" CssClass="table table-bordered table-condensed table-striped table-hover"
                                                                                                DataKeyNames="srno"
                                                                                                runat="server" CellPadding="4" ForeColor="#333333" GridLines="Vertical" AllowSorting="true" OnSorting="grdSRDoc_Sorted" AutoGenerateColumns="false" Visible="true" EmptyDataText="No record found">
                                                                                                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                                                                                <Columns>
                                                                                                    <asp:TemplateField HeaderText="S No." HeaderStyle-VerticalAlign="Top" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="false">
                                                                                                        <ItemTemplate>
                                                                                                            <itemtemplate><%#Container.DataItemIndex+1%> </itemtemplate>
                                                                                                        </ItemTemplate>
                                                                                                        <ItemStyle Width="5%" HorizontalAlign="Center" />
                                                                                                        <HeaderStyle Width="5%" HorizontalAlign="Center" />
                                                                                                    </asp:TemplateField>
                                                                                                    <asp:TemplateField HeaderText="Document Name" SortExpression="DocName" HeaderStyle-Font-Bold="false">
                                                                                                        <ItemTemplate>
                                                                                                            <%#Eval("DocName") %>
                                                                                                        </ItemTemplate>
                                                                                                        <HeaderStyle Width="10%" HorizontalAlign="Center" />

                                                                                                        <ItemStyle Width="10%" HorizontalAlign="Center" />
                                                                                                    </asp:TemplateField>
                                                                                                    <asp:TemplateField HeaderText="Uploaded By" SortExpression="DocName" HeaderStyle-Font-Bold="false">
                                                                                                        <ItemTemplate>
                                                                                                            <%#Eval("UploadedBy") %>
                                                                                                        </ItemTemplate>
                                                                                                        <HeaderStyle Width="10%" HorizontalAlign="Center" />

                                                                                                        <ItemStyle Width="10%" HorizontalAlign="Center" />
                                                                                                    </asp:TemplateField>
                                                                                                    <asp:TemplateField HeaderText="Attached On" SortExpression="INSDATE" HeaderStyle-Font-Bold="false">
                                                                                                        <ItemTemplate>
                                                                                                            <%-- <%#Eval("INSDATE") %>--%>
                                                                                            <%# ((DateTime)Eval("INSDATE")).ToString("dd-MM-yyyy") %>
                                                                                            <%--  </ItemTemplate>
                                                                                                        <HeaderStyle Width="10%" HorizontalAlign="Center" />

                                                                                                        <ItemStyle Width="10%" HorizontalAlign="Center" />
                                                                                                    </asp:TemplateField>

                                                                                                    <asp:TemplateField HeaderText="Pages" HeaderStyle-Font-Bold="false">

                                                                                                        <HeaderStyle Width="10%" HorizontalAlign="Center" />

                                                                                                        <ItemStyle Width="10%" HorizontalAlign="Center" />
                                                                                                    </asp:TemplateField>
                                                                                                    <asp:TemplateField HeaderText="View" HeaderStyle-Font-Bold="false">
                                                                                                        <ItemTemplate>
                                                                                                            <a href="#" class="gridViewToolTip" id="btnModalPopup" onclick='openSrDoc("<%# Eval("ORDRSHEETPATH")%>")'>
                                                                                                                <button type="button" class="btn btn-info btn-sm"><i class="fas fa-eye"></i></button>
                                                                                                            </a>

                                                                                                        </ItemTemplate>
                                                                                                        <HeaderStyle Width="10%" HorizontalAlign="Center" />
                                                                                                        <ItemStyle Width="10%" HorizontalAlign="Center" />
                                                                                                    </asp:TemplateField>
                                                                                                </Columns>
                                                                                                <EditRowStyle BackColor="#999999" />
                                                                                                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                                                                                <HeaderStyle BackColor="#e9564e" Font-Bold="false" ForeColor="White" />
                                                                                                <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                                                                                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                                                                                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                                                                                <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                                                                                <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                                                                                <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                                                                                <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                                                                                            </asp:GridView>--%>
                                                                                        </ContentTemplate>

                                                                                    </asp:UpdatePanel>


                                                                                </div>
                                                                                <div id="View_pnl" style="display: none">
                                                                                    <div id="View_PartyReply">
                                                                                        <textarea id="txtReadOnly" runat="server" style="height: 67px; width: 602px;" adonly="readonly" visible="false"></textarea>
                                                                                    </div>


                                                                                    <iframe id="ifrDisplay" class="embed-responsive-item" height="750"></iframe>
                                                                                </div>


                                                                                <asp:UpdatePanel runat="server">
                                                                                    <ContentTemplate>

                                                                                        <asp:Button ID="Attached" runat="server" Text="Attach" OnClientClick="Loader();" OnClick="Attached_Click" class="btn btn-success float-right" />
                                                                                        <asp:Panel ID="Panel2" Visible="false" runat="server">
                                                                                            <div class="card-body">
                                                                                                <div class="row" style="margin-top: 50px;">
                                                                                                    <div class="col-md-12" style="width: 100%; border: 1px solid #000;">
                                                                                                        <h5 class="new-heading text-center">Documents</h5>
                                                                                                        <%--<div class="mb-4 mt-3">--%>
                                                                                                        <div class="form-group row">
                                                                                                            <asp:Label class="col-sm-4 col-form-label" runat="server"> Document Name :</asp:Label>
                                                                                                            <div class="col-sm-8">
                                                                                                                <asp:TextBox class="form-control" runat="server" ID="txtUploadDocType"></asp:TextBox>
                                                                                                            </div>
                                                                                                        </div>

                                                                                                        <%--    <div class="form-group row mb-0">
                                                                            <label for="inputEmail3" class="col-sm-4 col-form-label">Name</label>
                                                                            <div class="col-sm-8">

                                                                                <input name="ctl00$ContentPlaceHolder1$txtName" type="text" value="fgcv" readonly="readonly" id="ContentPlaceHolder1_txtName" class="form-control" placeholder="Enter Name" fdprocessedid="g2kvkf">
                                                                                <span id="ContentPlaceHolder1_RequiredFieldValidator1" style="color:Red;visibility:hidden;"> Name required</span>
                                                                            </div>
                                                                                                            </div>--%>

                                                                                                        <div class="form-group row mb-0">
                                                                                                            <label class="col-sm-4 col-form-label">Upload Document</label>
                                                                                                            <div class="col-sm-8">
                                                                                                                <asp:FileUpload ID="CoSUpload_Doc" runat="server" />
                                                                                                            </div>
                                                                                                        </div>
                                                                                                        <div class="row col-12 text-center">
                                                                                                            <asp:Button ID="Button3" runat="server" OnClientClick="return setActiveTab();" OnClick="Button3_Click" Text="Document Upload" class="btn btn-primary" />
                                                                                                        </div>
                                                                                                        <label class="text-danger f-right">Note:- file should be  .doc, .docx, .pdf only  and max size should be 5 MB</label>
                                                                                                    </div>
                                                                                                </div>
                                                                                            </div>

                                                                                        </asp:Panel>

                                                                                    </ContentTemplate>
                                                                                    <Triggers>
                                                                                        <asp:PostBackTrigger ControlID="Button3" />
                                                                                    </Triggers>
                                                                                </asp:UpdatePanel>

                                                                                <%--<button type="button" class="btn btn-success float-right" data-toggle="modal"  data-target="#exampleModalCenter">Attach </button>--%>
                                                                            </div>
                                                                            <div class="tab-pane fade active show" id="custom-tabs-four-profile" role="tabpanel" aria-labelledby="custom-tabs-four-profile-tab">

                                                                                <div>
                                                                                    <%--<img src="../dist/img/Doc_Sample_pic.jpg" style="width: 708px;" />--%>
                                                                                    <%--  <iframe id="ifRecent" runat="server" height='750' visible="false"></iframe>--%>
                                                                                    <iframe id="docPath" runat="server" visible="true" height='750'></iframe>
                                                                                    <iframe id="IfProceeding" clientidmode="Static" runat="server" visible="false" scrolling="auto" height='750'></iframe>
                                                                                    <iframe id="IfProceedingCrnt" clientidmode="Static" runat="server" visible="false" scrolling="auto" height='750'></iframe>
                                                                                    <iframe id="IfProceedingOther" clientidmode="Static" runat="server" visible="false" scrolling="auto" height='750'></iframe>
                                                                                </div>
                                                                            </div>
                                                                            <div class="tab-pane fade" id="custom-tabs-four-messages" role="tabpanel" aria-labelledby="custom-tabs-four-messages-tab">
                                                                                <iframe id="RecentdocPath" runat="server" width='550' height='750'></iframe>
                                                                                <iframe id="ifProposal1" clientidmode="Static" runat="server" scrolling="auto" height='750'></iframe>
                                                                                <iframe id="RecentAttachedDoc" runat="server" width='550' height='750'></iframe>
                                                                                <iframe id="ifPDFViewerAll" clientidmode="Static" runat="server" scrolling="auto" height='750'></iframe>

                                                                            </div>
                                                                            <div class="tab-pane fade " id="custom-tabs-four-settings" role="tabpanel" aria-labelledby="custom-tabs-four-settings-tab">

                                                                                <div>
                                                                                    <%--<img src="../dist/img/Doc_Sample_pic.jpg" style="width: 708px;" />--%>
                                                                                </div>

                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <!-- /.card -->
                                                                </div>
                                                            </div>

                                                        </div>

                                                    </div>
                                                </div>
                                                <!-- /.card -->
                                            </div>
                                        </div>

                                    </div>
                                </div>


                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <asp:HiddenField ID="hdnvalue" ClientIDMode="Static" runat="server" />
            <asp:HiddenField ID="hdnfAppld" ClientIDMode="Static" runat="server" />
            <asp:HiddenField ID="hdnfAppNo" ClientIDMode="Static" runat="server" />
            <asp:HiddenField ID="hdnfldCaseNo" ClientIDMode="Static" runat="server" />
            <asp:HiddenField ID="hdnOfficeNameHi" runat="server" />

            <asp:HiddenField ID="hdnfldCaseNo_1" ClientIDMode="Static" runat="server" />
            <asp:HiddenField ID="hdnfAppld_1" ClientIDMode="Static" runat="server" />
            <asp:HiddenField ID="hdnfAppNo_1" ClientIDMode="Static" runat="server" />
        </div>
    </section>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.7.1/jquery.min.js"></script>
    <script type="text/javascript">

        $(document).ready(function () {
            $("#custom_tabs_on_profile_tab").click(function () {
               
                $("#original-tab").show();
                $("#Edit_Report").hide();
                $("#Current_Report").hide();
                $("#Other_tab").hide();
            });
            $("#custom_tabs_one_home_tab").click(function () {
                
                $("#original-tab").hide();
                $("#Edit_Report").hide();
                $("#Other_tab").hide();
                $("#Current_Report").show();
            });
            $("#custom_tabs_one_other_tab").click(function () {
               
                $("#original-tab").hide();
                $("#Edit_Report").hide();
                $("#Current_Report").hide();
                //$("#Other_tab").show();
            });
            $("#btnSendCurSR").click(function () {
                $("#pSubject").innerHTML;
                $("#hdnvalue").value($("#pSubject").innerHTML);
            });
            $("#btnCreateReport").click(function () {
                $("#pSubject").innerHTML;
                $("#hdnvalue").value($("#pSubject").innerHTML);
            });
        })
        function validateReason() {
            alert(document.getElementById("ddlReason").value);
            if (document.getElementById("ddlReason").value == "Select Reason") {
                //Swal.fire('Please select section');
                Swal.fire({
                    icon: 'info',
                    title: 'Please select reason'

                })
                return false;
            }

        }



        $(document).ready(function () {
            $('#summernote3').summernote();
        });

       




        function EditCurrentSRReport() {

            $("#ReportOrginalSR").hide();
            $("#ReportCurrentSR").show();
            $("#ReportOtherSR").hide();
            $("#Current_Report").show();

            //alert(document.getElementById("summernote3").value);
            //alert(document.getElementById("pSRContent").innerHTML);

            var content = document.getElementById("pSRContent").innerHTML;
            $('#summernote3').summernote('code', content);
            //document.getElementById("pSRContent").innerHTML = document.getElementById("summernote3").value;
            /*document.getElementById("summernote3").innerHTML = content;*/
            $("#ContentPlaceHolder1_custom_tabs_one_home_tab").removeClass("active");
            $("#custom-tabs-one-ProposalForm").removeClass("active");

            $("#custom-tabs-one-original-tab").addClass("active");
            $("#custom-tabs-one-edit").addClass("active");
            $("#custom-tabs-one-edit-tab").addClass("active");
        }

        function EditOriginalSRReport() {
            //alert("sss");
            //debugger;
            $("#ReportOrginalSR").show();
            $("#ReportCurrentSR").hide();
            $("#ReportOtherSR").hide();
            $("#original-tab").show();

            document.getElementById("pContent").innerHTML = document.getElementById("summernote3").value;

            $("#ContentPlaceHolder1_custom_tabs_on_profile_tab").removeClass("active");
            $("#custom-tabs-one-RegisteredForm").removeClass("active");

            $("#custom-tabs-one-original-tab").addClass("active");
            $("#custom-tabs-one-edit").addClass("active");
            $("#custom-tabs-one-original-tab").addClass("active");
            
            return false;

        }

        function EditOtherSRReport() {

            //debugger;
            $("#ReportOrginalSR").hide(); 
            $("#ReportCurrentSR").hide();
            $("#ReportOtherSR").show();
            $("#Other_tab").show();


            var content = document.getElementById("pContent_OtherSR").innerHTML;
            /*alert(content);*/
            $('#summernote3').summernote('code', content);

            //document.getElementById("pContent_OtherSR").innerHTML = document.getElementById("summernote3").value;
            /*alert("sss");*/
            //$("#ContentPlaceHolder1_custom-tabs-one-other-tab").removeClass("active");
            $("#custom_tabs_one_other_tab").removeClass("active");
            $("#custom-tabs-one-other").removeClass("active");

            $("#custom-tabs-one-original-tab").addClass("active");
            $("#custom-tabs-one-edit").addClass("active");
            $("#custom-tabs-one-otherr-tab").addClass("active");
            return false;

        }

        //function ShowMessageYesNo() {
        //    alert("ssss");
        //}


        function ShowMessageYesNo() {
            //debugger;
            var case_number = document.getElementById('hdnfldCaseNo_1').value;
            var App_ld = document.getElementById('hdnfAppld_1').value;
            var App_No = document.getElementById('hdnfAppNo_1').value;
            //alert(case_number + '-----' + App_ld + '-----' + App_No);

            Swal.fire({
                title: 'Do you want to additional Seek report ?',
                showDenyButton: true,

                confirmButtonText: 'Yes',

                customClass: {
                    actions: 'my-actions',
                    cancelButton: 'order-1 right-gap',
                    confirmButton: 'order-2',
                    denyButton: 'order-3',
                }
            }).then((result) => {
                if (result.isConfirmed) {


                }
                else {
                    /*window.location = 'Ordersheet.aspx?Case_Number=' + case_number + '&App_Id=' + App_ld + '&AppNo=' + App_No;*/
                    window.location = 'Ordersheet.aspx';
                }

            })

            $("#ContentPlaceHolder1_custom_tabs_one_home_tab").addClass("active");
            $("#custom-tabs-one-ProposalForm").addClass("active show");

            $("#ContentPlaceHolder1_custom_tabs_on_profile_tab").removeClass("active");
            $("#custom-tabs-one-RegisteredForm").removeClass("active");
        }



        function OriginalSatutus() {
            $("#ContentPlaceHolder1_custom_tabs_one_home_tab").addClass("active");
            $("#custom-tabs-one-ProposalForm").addClass("active show");

            $("#ContentPlaceHolder1_custom_tabs_on_profile_tab").removeClass("active");
            $("#custom-tabs-one-RegisteredForm").removeClass("active");
        }


        function OtherStatus() {
            //$("#custom-tabs-one-RegisteredForm").addClass("active");
            //$("#custom-tabs-one-other").addClass("active show");

            //$("#ContentPlaceHolder1_custom_tabs_one_home_tab").removeClass("active");
            //$("#custom-tabs-one-ProposalForm").removeClass("active");

            //$("#custom_tabs_one_other_tab").removeClass("active");

            $("#custom_tabs_one_other_tab").addClass("active");
            $("#custom-tabs-one-other").addClass("active show");

            $("#ContentPlaceHolder1_custom_tabs_one_home_tab").removeClass("active");
            $("#custom-tabs-one-ProposalForm").removeClass("active");

            $("#ContentPlaceHolder1_custom_tabs_on_profile_tab").removeClass("active");
            $("#custom-tabs-one-RegisteredForm").removeClass("active");




        }

        function ShowMessageYesNo_CurrentSR() {

            var case_number = document.getElementById('hdnfldCaseNo').value;
            var App_ld = document.getElementById('hdnfAppld').value;
            var App_No = document.getElementById('hdnfAppNo').value;
            //alert(case_number + '-----' + App_ld + '-----' + App_No);

            Swal.fire({
                title: 'Do you want to additional Seek report ?',
                showDenyButton: true,

                confirmButtonText: 'Yes',

                customClass: {
                    actions: 'my-actions',
                    cancelButton: 'order-1 right-gap',
                    confirmButton: 'order-2',
                    denyButton: 'order-3',
                }
            }).then((result) => {
                if (result.isConfirmed) {


                }
                else {
                    AmagiLoader.show();
                    //window.location = 'Ordersheet.aspx?Case_Number=' + case_number + '&App_Id=' + App_ld + '&AppNo=' + App_No;
                    window.location = 'Ordersheet.aspx';
                }

            })



            $("#custom_tabs_one_other_tab").addClass("active");
            $("#custom-tabs-one-other").addClass("active");

            $("#ContentPlaceHolder1_custom_tabs_one_home_tab").removeClass("active");
            $("#custom-tabs-one-ProposalForm").removeClass("active show");
        }

        function ShowMessageYesNo_Skip() {
            var case_number = document.getElementById('hdnfldCaseNo_1').value;
            var App_ld = document.getElementById('hdnfAppld_1').value;
            var App_No = document.getElementById('hdnfAppNo_1').value;
            var Status = 2;
            /*alert(case_number + App_ld + App_No)*/
            Swal.fire({
                title: 'Do you want to skip Seek Report?',
                showDenyButton: true,

                confirmButtonText: 'Yes',

                customClass: {
                    actions: 'my-actions',
                    cancelButton: 'order-1 right-gap',
                    confirmButton: 'order-2',
                    denyButton: 'order-3',
                }
            }).then((result) => {
                if (result.isConfirmed) {
                    //debugger;
                    //window.location = 'Ordersheet.aspx?Case_Number=' + case_number + '&App_Id=' + App_ld + '&Proposalno=' + App_No + '&Case_Status=' + Status;
                    window.location = 'Ordersheet.aspx?Case_Status=' + Status;

                }

            })
        }



        function Loader() {
            AmagiLoader.show();
        }
        function Loaderstop() {
            AmagiLoader.hide();
        }


        function ShowMessageYesNo_Other() {
            //debugger;
            var case_number = document.getElementById('hdnfldCaseNo_1').value;
            var App_ld = document.getElementById('hdnfAppld_1').value;
            var App_No = document.getElementById('hdnfAppNo_1').value;
            //alert(case_number + '-----' + App_ld + '-----' + App_No);

            Swal.fire({
                title: 'Seek Report Saved Do U want to Exit ?',
                showDenyButton: true,

                confirmButtonText: 'Yes',

                customClass: {
                    actions: 'my-actions',
                    cancelButton: 'order-1 right-gap',
                    confirmButton: 'order-2',
                    denyButton: 'order-3',
                }
            }).then((result) => {
                if (result.isConfirmed) {
                    //window.location = 'Ordersheet.aspx?Case_Number=' + case_number + '&App_Id=' + App_ld + '&AppNo=' + App_No;
                    window.location = 'Ordersheet.aspx';

                }
                else {

                }

            })

            $("#custom_tabs_one_other_tab").addClass("active");
            $("#custom-tabs-one-other").addClass("active");

            $("#ContentPlaceHolder1_custom_tabs_one_home_tab").removeClass("active");
            $("#custom-tabs-one-ProposalForm").removeClass("active show");
        }

        //function ShowMessageYesNo_OtherSR() {

        //    var case_number = document.getElementById('hdnfldCaseNo').value;
        //    var App_ld = document.getElementById('hdnfAppld').value;
        //    var App_No = document.getElementById('hdnfAppNo').value;
        //    //alert(case_number + '-----' + App_ld + '-----' + App_No);
        //    alter('dkjfkdsnfklndsklfnlsfnkm');
        //    Swal.fire({
        //        title: 'Do you want to additional Seek report ?',
        //        showDenyButton: true,

        //        confirmButtonText: 'Yes',

        //        customClass: {
        //            actions: 'my-actions',
        //            cancelButton: 'order-1 right-gap',
        //            confirmButton: 'order-2',
        //            denyButton: 'order-3',
        //        }
        //    }).then((result) => {
        //        if (result.isConfirmed) {


        //        }
        //        else {
        //            window.location = 'Ordersheet.aspx?Case_Number=' + case_number + '&App_Id=' + App_ld + '&AppNo=' + App_No;
        //        }

        //    })



        //    $("#custom_tabs_one_other_tab").addClass("active");
        //    $("#custom-tabs-one-other").addClass("active");

        //    $("#ContentPlaceHolder1_custom_tabs_one_home_tab").removeClass("active");
        //    $("#custom-tabs-one-ProposalForm").removeClass("active show");
        //}


        function CurrentSatutus() {
            
            $("#custom_tabs_one_other_tab").addClass("active");
            $("#custom-tabs-one-other").addClass("active");

            $("#ContentPlaceHolder1_custom_tabs_one_home_tab").removeClass("active");
            $("#custom-tabs-one-ProposalForm").removeClass("active show");
            $("#custom-tabs-one-RegisteredForm").removeClass("active show");

        }
        function showPdfBYHendler(PROPOSALPATH_FIRSTFORMATE, docType, EREG_ID) {


            $("#List_pnl").hide();
            $("#View_pnl").show();
            var Tocan = document.getElementById("hdTocan").value;
            $('#ifrDisplay').attr('src', '')
            var jasonData = '{"PDFPath": "' + PROPOSALPATH_FIRSTFORMATE + '","DocType": "' + docType + '","EREG_ID": "' + EREG_ID + '","Tocan": "' + Tocan + '" }';

            if (docType == "RegistryDocument" || docType == "ProposalDocument" || docType == "AdditionalDocument") {
                $.ajax({
                    type: "POST",
                    url: '<%=Page.ResolveClientUrl("~/CoS/AcceptRejectCases_details.aspx/show_Pdf_BY_Hendler_Deed_Doc") %>',

                    data: jasonData,
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (msg) {

                        /* console.log("Data :-"+msg.d);*/

                        var base64Pdf = msg.d;
                        if (base64Pdf.length > 0) {
                            var binaryString = window.atob(base64Pdf);
                            var len = binaryString.length;
                            var bytes = new Uint8Array(len);
                            for (var i = 0; i < len; i++) {
                                bytes[i] = binaryString.charCodeAt(i);
                            }

                            // Create Blob object
                            var blob = new Blob([bytes], { type: 'application/pdf' });

                            // Create object URL
                            var url = URL.createObjectURL(blob);

                            // Set iframe src attribute to the object URL
                            $('#ifrDisplay').attr('src', url);
                            $('#ifrDisplay').attr('enabled', 'enabled');
                        }
                        else {
                            $("#List_pnl").show();
                            $("#View_pnl").hide();
                            // $("#pnl3").hide();
                            Swal.fire({
                                icon: 'info',
                                title: 'No Document attached',
                                //showCancelButton: true,
                                confirmButtonText: 'OK',
                            })
                            base64Pdf = "";
                        }




                    },
                    error: function (xhr, status, error) {
                        console.log("error " + error);
                        console.log("status " + error);
                        console.log("xhr " + xhr);
                        //LoadNotice();
                    }
                });
            }
            else {
                $.ajax({
                    type: "POST",
                    url: '<%=Page.ResolveClientUrl("~/CoS/AcceptRejectCases_details.aspx/show_Pdf_BY_Hendler_Deed_Doc") %>',

                    data: jasonData,
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (msg) {
                        $('#ifrDisplay').attr('src', msg.d)
                        //setTemlateVal(msg.d);
                    },
                    error: function (e) {
                        console.log(e);
                        //LoadNotice();
                    }
                });
            }



        }

    </script>

    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.6.4/jquery.min.js"></script>
    <asp:HiddenField runat="server" ClientIDMode="Static" ID="hdappid" />
    <asp:HiddenField runat="server" ClientIDMode="Static" ID="hdAppno" />
    <asp:HiddenField runat="server" ClientIDMode="Static" ID="hdTocan" />
    <asp:HiddenField ID="hdnbase64" ClientIDMode="Static" runat="server" />
    <script type="text/javascript">
        $(document).ready(function () {

            if ($("#hdnbase64").val().length > 0) {
                var base64Pdf = $("#hdnbase64").val();
                var binaryString = window.atob(base64Pdf);
                var len = binaryString.length;
                var bytes = new Uint8Array(len);
                for (var i = 0; i < len; i++) {
                    bytes[i] = binaryString.charCodeAt(i);
                }
                var blob = new Blob([bytes], { type: 'application/pdf' });
                var url = URL.createObjectURL(blob);
                $('#RecentdocPath').attr('src', url);
            }
            BindTable();
            LoadPdfFile();
            ShowMessage();


            // BindOrderSheet();
        });


        function BindTable() {

            //alert("111");
            var id = "#tbl";
            var appid = document.getElementById("hdappid").value;
            var Appno = document.getElementById("hdAppno").value;
            var jasonData = '{ "appid": ' + appid + ', "Appno": "' + Appno + '" }';
            $.ajax({
                type: "POST",

                url: '<%=Page.ResolveClientUrl("~/CoS/AcceptRejectCases_details.aspx/DocFile") %>',
                data: jasonData,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {

                    $("#dynamic-table tbody").empty();
                    var json = JSON.parse(msg.d);
                    //var ResponceData = JSON.parse(data.d);

                    //for (var i = 0; i < json.; i++) {
                    //    var row = '<tr><td align="center">' + json[i].DOC_NAME + '</td> <td align="center">' + json[i].FILE_PATH + ' </td> <td align="center">' + json[i].File_Path + '</td></tr>'
                    //    $("#dynamic-table tbody").append(row);<td align="center"><a href="' + url + '" target="_blank" >Download</a></td>
                    //}
                    var index = 1;


                    var output = json.map(i => "<tr><td>" + index++ + "</td><td>" + i.DOC_NAME + "</td><a><td>" + i.FILE_PATH + "</a></td></tr>");


                    $("#dynamic-table tbody").append(output);


                },
                error: function (e) {
                    // alert("Error");
                    console.log(e);


                }


            });

        }





        function ShowMessageHeadSection() {
            var head = document.getElementById('hdnfldHead').value;
            var section = document.getElementById('hdnfldSection').value;
            var case_number = document.getElementById('hdnfldCase').value;
            //alert(case_number);
            Swal.fire({
                icon: 'success',
                title: 'Selected Head - ' + head + 'and Section' + section,
                showCancelButton: true,
                confirmButtonText: 'OK',
            }).then((result) => {
                if (result.value) {
                    /*window.location = 'Ordersheet.aspx?Case_Number=' + case_number;*/
                    /* setTimeout( test() , 6000);*/
                } else {
                    alert('user click on Cancel button');
                }
            });
        }


    </script>

</asp:Content>
