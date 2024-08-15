<%@ Page Title="" Language="C#" MasterPageFile="~/CoS/CoSMaster.Master" AutoEventWireup="true" CodeBehind="Hearing.aspx.cs" ValidateRequest="false" Inherits="CMS_Sampada.CoS.Hearing" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>



<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../dist/plugins/jquery/jquery.min.js"></script>
    <link href="../Styles/sweetalert.css" rel="stylesheet" />
    <script src="../Scripts/sweetalert.min.js"></script>
    <link href="../dist/css/Calender-Style.css" rel="stylesheet" />
    <script src="../assets/js/3.3.1/sweetalert2@11.js"></script>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css">

    <style>
        .note-editing-area {
            height: 350px;
        }

        #addnewrow table tbody tr td {
            display: flex;
            flex-direction: row;
            align-items: baseline;
        }

            #addnewrow table tbody tr td label {
                padding-left: 10px;
            }

        #ContentPlaceHolder1_chkContent {
            text-align: justify;
        }

        #addnewrow table tbody tr td {
            display: table-cell !important;
        }

        #ifselectyes_Template table tbody tr td label {
            margin-left: 10px;
        }

        #ifselectyes_Template table tbody tr td span label {
            margin-left: 10px;
        }

        #ifselectyes_Template table tbody tr td {
            display: flex;
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
        function DisableNoticeRbtn1() {

            document.getElementById('edit_notice').style.display = 'none';
            
            var NoticeRadio = document.getElementById("Send_Notice");
            var FinalRadio = document.getElementById("Final_Order");

            if (FinalRadio.checked) {
                NoticeRadio.disabled = true;
                FinalRadio.disabled = true;
            }
        }
        function DisableRbtn_NextNotice() {

            document.getElementById('edit_notice').style.display = 'none';

            var NoticeRadio = document.getElementById("Send_Notice");
            var FinalRadio = document.getElementById("Final_Order");

            if (NoticeRadio.checked) {
                NoticeRadio.disabled = true;
                FinalRadio.disabled = true;
            }
        }

        function DisableFnlOrderRbtn_1() {

            var status = document.getElementById('hdnStatus').value;
           
            document.getElementById('Final_Order').checked = true;
            var NoticeRadio = document.getElementById("Send_Notice");
            var FinalRadio = document.getElementById("Final_Order");

            if (FinalRadio.checked) {
                NoticeRadio.disabled = true;
                FinalRadio.disabled = true;
                
            }

            document.getElementById('today').checked = true;
            document.getElementById('edit_notice').style.display = 'none';
           
            var todayRadio = document.getElementById("today");
            var save_for_LaterRadio = document.getElementById("save_for_Later");
            todayRadio.disabled = true;
            save_for_LaterRadio.disabled = true;


        }

        function DisableFnlOrderRbtn() {

            
            
            
            document.getElementById('edit_notice').style.display = 'none';
           
            var todayRadio = document.getElementById("today");
            var save_for_LaterRadio = document.getElementById("save_for_Later");
           

            if (todayRadio.checked) {
                todayRadio.disabled = true;
                save_for_LaterRadio.disabled = true;
            }
        }
        function disableradioBtn1() {
           
            document.getElementById('S_Notice2').style.display = 'block';
            document.getElementById('edit_notice').style.display = 'none';
            document.getElementById('hearingdate').style.display = 'none';
        }
        function Loaderstop1() {
            console.log("Loaderstop1 called");
            // Your code here
        }

        //function Loaderstop1() {
        //    alert("3");
        //    AmagiLoader.hide();
        //}

    </script>




    <script type="text/javascript">


        function ShowDocList() {

            $("#List_pnl").show();
            $("#View_pnl").hide();
            $("#View_pnlPartyReply").hide();

        }
        function openSrDoc(FILE_PATH, PartyReplay) {

            /*alert(PartyReplay);*/
            if (PartyReplay == 0) {

                //alert(FILE_PATH);

                $("#List_pnl").hide();
                $("#View_pnl").show();
                $('#ifrDisplay').attr('src', FILE_PATH);

            }

            if (PartyReplay == 1) {
                //alert(PartyReplay);


                //alert(FILE_PATH);

                $("#List_pnl").hide();
                $("#View_pnl").hide();
                $("#View_pnlPartyReply").show();
                $('#ifrDisplayParty').attr('src', FILE_PATH);

            }



        }
        function openProposalDoc(PROPOSALPATH_FIRSTFORMATE) {

            //alert(PROPOSALPATH_FIRSTFORMATE);

            $("#List_pnl").hide();
            $("#View_pnl").show();
            $('#ifrDisplay').attr('src', PROPOSALPATH_FIRSTFORMATE);


        }
        function openOrdersheetDoc(DocPath) {
            //alert(DocPath);

            $("#List_pnl").hide();
            $("#View_pnl").show();
            $('#ifrDisplay').attr('src', DocPath);
        }

        function openNoticeDoc(NOTICE_DOCS) {
            //alert(NOTICE_DOCS);

            $("#List_pnl").hide();
            $("#View_pnl").show();
            $('#ifrDisplay').attr('src', NOTICE_DOCS);
        }
        function openPartyDoc(REPLY_DOCS) {
            //alert("Rakesh bhai");

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

            $("#List_pnl").hide();
            $("#View_pnl").show();
            $('#ifrDisplay').attr('src', DOCS_PATH);
        }




        function HearingDtError() {

            Swal.fire({
                icon: 'error',
                title: 'Selected Date should greater than previous Hearing..!',
                showCancelButton: false,
                confirmButtonText: 'OK'
            });
        }


        function ValidateEsignRecord() {

            var SignOption = $("#ContentPlaceHolder1_ddl_SignOption").val();

            if (SignOption == "0") {

                Swal.fire({
                    icon: 'info',
                    title: 'Please select eSign or DSC in dropdown'

                })
                return false;
            }
            else if (document.getElementById("TxtLast4Digit").value == "") {

                Swal.fire({
                    icon: 'info',
                    title: 'Please enter last 4 digit of Adhar Card'

                })
                return false;

            }


        }

        function ValidateLaterEsignRecord() {

            var SignOption = $("#ContentPlaceHolder1_ddlLaterE").val();

            if (SignOption == "0") {

                Swal.fire({
                    icon: 'info',
                    title: 'Please select eSign or DSC in dropdown'

                })
                return false;
            }
            else if (document.getElementById("txtLaterAddhar").value == "") {

                Swal.fire({
                    icon: 'info',
                    title: 'Please enter last 4 digit of Adhar Card'

                })
                return false;

            }


        }



        function ValidateEsignRecord_Notice() {

            var SignOption = $("#ContentPlaceHolder1_ddl_SignOption_Notice").val();

            if (SignOption == "0") {

                Swal.fire({
                    icon: 'info',
                    title: 'Please select eSign or DSC in dropdown'

                })
                return false;
            }
            else if (document.getElementById("TxtLast4Digit_Notice").value == "") {

                Swal.fire({
                    icon: 'info',
                    title: 'Please enter last 4 digit of Adhar Card'

                })
                return false;

            }


        }

    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="SCMNG" runat="server" EnablePageMethods="true"></asp:ScriptManager>
    <%--<asp:ToolkitScriptManager runat="server" />--%>
    <div style="display: none;">
        <asp:AjaxFileUpload ID="AjaxFileUpload1" runat="server" />
    </div>

    <asp:HiddenField ID="hdnStatus" ClientIDMode="Static" runat="server" />
    <asp:HiddenField ID="hdnfCseNunmber" ClientIDMode="Static" runat="server" />
    <asp:HiddenField ID="hdnfApp_Number" ClientIDMode="Static" runat="server" />
    <asp:HiddenField ID="hdlblHearingDt" ClientIDMode="Static" runat="server" />
    <asp:HiddenField runat="server" ClientIDMode="Static" ID="hdTocan" />

    <asp:HiddenField ID="hdnHearingDt" ClientIDMode="Static" runat="server" />
    <asp:HiddenField ID="hdnHearingDt1" ClientIDMode="Static" runat="server" />
    <asp:HiddenField ID="hdnbase64" ClientIDMode="Static" runat="server" />
    <asp:PlaceHolder runat="server" ID="ph" />

    <section class="content-header headercls">
        <div class="container-fluid">
            <div class="row">
                <div class="col-sm-3">
                    <h5>Hearing </h5>



                </div>
            </div>
        </div>
    </section>

    <section class="content-header">
        <div class="container-fluid">
            <div class="row">
                <div class="col-sm-3">
                    <h6>Proposal No -
                        <asp:Label ID="lblProposalIdHeading" Text="" runat="server"></asp:Label></h6>
                </div>
                <div class="col-sm-3">

                    <h6>Case No -  
                        <asp:Label ID="lblCase_Number" runat="server"></asp:Label>
                    </h6>
                </div>
                <div class="col-sm-3">
                    <h6>Case Registered Date -  
                        <%--<asp:Label ID="lblRegisteredDate" runat="server"></asp:Label>--%>
                        <asp:Label runat="server" Text='<%# (DateTime.Parse(Eval("inserteddate").ToString()).ToString("MM/dd/yyyy")) %>' ID="lblRegisteredDate"></asp:Label>
                    </h6>
                </div>
                <div class="col-sm-3">
                    <h6>Case Hearing Date -  
                       <%-- <asp:Label ID="" runat="server"></asp:Label>--%>
                        <asp:Label ID="lblHearingdateHeading" runat="server"></asp:Label>
                        <%--                        <asp:Label runat="server" Text='<%# (DateTime.Parse(Eval("hearingdate").ToString()).ToString("MM/dd/yyyy")) %>' ID="lblHearingdateHeading"></asp:Label>--%>
                    </h6>

                </div>
                <%--<div class="col-sm-3">
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
                        <div class="card-header p-2">
                            <ul class="nav nav-pills">
                                <li class="nav-item"><a class="nav-link disabled" href="#">Proposal</a></li>
                                <li class="nav-item"><a class="nav-link disabled" href="#">Order Sheet</a></li>
                                <li class="nav-item"><a class="nav-link disabled" href="#">Notice</a></li>
                                <li class="nav-item"><a class="nav-link disabled" href="#">Seek Report</a></li>
                                <li class="nav-item"><a class="nav-link disabled" href="#">Notice Proceeding</a></li>
                                <li class="nav-item"><a class="nav-link active disabled" href="#">Hearing</a></li>
                                <li class="nav-item"><a class="nav-link disabled" href="#">Final Order</a>
                                <li class="nav-item"><a class="nav-link disabled" href="#">Payment</a></li>
                                <li class="nav-item"><a class="nav-link disabled" href="#">Closed Cases</a></li>
                            </ul>
                        </div>
                        <div class="card-body">
                            <div class="tab-content">




                                <%------------------------------------------------------------------------------------------%>



                                <div class="tab-pane active" id="Hearing">
                                    <div class="row">

                                        <div class="col-12 col-sm-6">
                                            <div class="card card-primary card-tabs h-100">
                                                <div class="card-header p-0 pt-1">
                                                    <ul class="nav nav-tabs" id="custom-tabs-one-tab" role="tablist">

                                                        <li class="nav-item">
                                                            <a class="nav-link active" id="Proceeding" data-toggle="pill"
                                                                href="#custom_tabs_one_Order_Sheet" role="tab" onclick="openPopup()"
                                                                aria-controls="custom-tabs-one-profile" aria-selected="true">Proceeding  </a>
                                                        </li>

                                                        <li class="nav-item">
                                                            <a class="nav-link" style="display: none" onclick="ddd()" id="edit_notice" data-toggle="pill"
                                                                href="#custom-tabs-one-edit_notice_1" role="tab" aria-controls="custom-tabs-one-home"
                                                                aria-selected="false">Edit Proceeding </a>
                                                        </li>



                                                        <%--<li class="nav-item">
                                                            <a class="nav-link" id="Order_Sheet" data-toggle="pill"
                                                                href="#custom-tabs-one-Order-Sheet" role="tab" aria-controls="custom-tabs-one-home"
                                                                aria-selected="false">Order Sheet </a>
                                                        </li>--%>

                                                        <%-- <li class="nav-item">
                                                            <a class="nav-link" id="Next_Hearing_Date" data-toggle="pill"
                                                                href="#custom-tabs-one-Next_Hearing_Date" role="tab" aria-controls="custom-tabs-one-home"
                                                                aria-selected="false">Next Hearing Date </a>
                                                        </li>--%>
                                                        <%--<li class="nav-item">
                                                            <a class="nav-link" id="Attached_Reply" data-toggle="pill"
                                                                href="#custom-tabs-one-ProposalForm" role="tab" aria-controls="custom-tabs-one-home"
                                                                aria-selected="false">Attached Reply / Document </a>
                                                        </li>--%>
                                                    </ul>
                                                </div>

                                                <div class="card-body">

                                                    <asp:Panel runat="server" ID="PnlHearing_P1" Visible="true">
                                                        <div class="tab-content" id="custom-tabs-one-tabContent">


                                                            <div class="tab-pane active show" id="custom-tabs-one-Proceeding" role="tabpanel"
                                                                aria-labelledby="Proceeding">
                                                                <!-- code here -->

                                                                <div id="pnl_Proceding">

                                                                    <div class="displaydiv">

                                                                        <button type="button" class="btn btn-success" id="add_green_proceeding">Add Proceeding </button>


                                                                        <br />
                                                                        <div class="main-box html_box htmldoc greenbox" style="background: rgba(202, 249, 130, 0.67);">
                                                                            <h4>न्यायालय कलेक्टर ऑफ़ स्टाम्प्स,
                                                                                <asp:Label ID="lblDRoffice" runat="server"></asp:Label>
                                                                                (म.प्र.)</h4>
                                                                            <asp:Repeater ID="RepDetails" runat="server">
                                                                                <ItemTemplate>
                                                                                    <table>

                                                                                        <br />
                                                                                        <tr style="text-align: left;">
                                                                                            <td>Proceeding # <%# Container.ItemIndex + 1 %></td>
                                                                                        </tr>
                                                                                        <tr style="">
                                                                                            <td>
                                                                                                <%--<asp:Label ID="lblhearing" runat="server" Text='<%#Eval("hearingdate") %>' Font-Bold="true" />--%>
                                                                                                <asp:Label ID="lblhearing" runat="server" Text='<%# Eval("inserteddate", "{0:dd/MM/yyyy}") %>' Font-Bold="true" />

                                                                                            </td>
                                                                                        </tr>
                                                                                    </table>

                                                                                    <span style="padding: 30px"></span>
                                                                                    <table>
                                                                                        <tr>
                                                                                            <td style="text-align: justify">
                                                                                                <asp:Label ID="lblComment" runat="server" Text='<%#Eval("PROCEEDING") %>' />

                                                                                            </td>
                                                                                        </tr>
                                                                                    </table>
                                                                                    <p>&nbsp;&nbsp;&nbsp;</p>
                                                                                    <table>
                                                                                        <tr style="text-align: left;">
                                                                                            <td>पेशी दिनांक:                                                                                                
                                                                                            <%--<asp:Label ID="lblDate" runat="server" Font-Bold="true" Text='<%#Eval("inserteddate") %>' /></td>--%>
                                                                                                <asp:Label ID="lblDate" runat="server" Text='<%# Eval("hearingdate", "{0:dd/MM/yyyy}") %>' Font-Bold="true" />


                                                                                            <td></td>

                                                                                            <td style="text-align: right; width: 72%;">
                                                                                                <div>
                                                                                                    कलेक्टर ऑफ़ स्टाम्प्स
                                                                                        <br />
                                                                                                    <asp:Label ID="lblDRoffice4" runat="server"></asp:Label>
                                                                                                </div>
                                                                                            </td>

                                                                                        </tr>

                                                                                    </table>
                                                                                    <br />
                                                                                    <hr />


                                                                                </ItemTemplate>
                                                                                <FooterTemplate>
                                                                                    </table>
                                                                                </FooterTemplate>






                                                                            </asp:Repeater>

                                                                            <br />

                                                                            <br />

                                                                            <asp:Panel ID="PnlNotice" Visible="false" runat="server">

                                                                                <asp:Repeater ID="Repeater_Notice" runat="server">
                                                                                    <ItemTemplate>
                                                                                        <table>
                                                                                            <tr style="text-align: left">
                                                                                                <td>Proceeding # <%# Container.ItemIndex + 2 %></td>
                                                                                            </tr>
                                                                                            <tr style="">
                                                                                                <td>
                                                                                                    <asp:Label ID="lblhearing" runat="server" Text='<%# Eval("inserteddate", "{0:dd/MM/yyyy}") %>' Font-Bold="true" />
                                                                                                    <%--<asp:Label ID="lblhearing" runat="server" Text='<%#Eval("HEARINGDATE") %>' Font-Bold="true" />--%>
                                                                                                </td>
                                                                                            </tr>
                                                                                        </table>

                                                                                        <span style="padding: 30px"></span>
                                                                                        <table>
                                                                                            <tr>
                                                                                                <td style="text-align: justify">
                                                                                                    <asp:Label ID="lblComment" runat="server" Text='<%#Eval("Notice_PROCEEDING") %>' />
                                                                                                </td>
                                                                                            </tr>
                                                                                        </table>
                                                                                        <p>&nbsp;&nbsp;&nbsp;</p>
                                                                                        <table>
                                                                                            <tr style="text-align: left;">
                                                                                                <td>पेशी दिनांक:                                                                                                
                                                                                                    <%--                                                                                            <asp:Label ID="lblDate" runat="server" Font-Bold="true" Text='<%#Eval("LMDT") %>' /></td>--%>
                                                                                                    <asp:Label ID="lblDate" runat="server" Text='<%# Eval("hearingdate", "{0:dd/MM/yyyy}") %>' Font-Bold="true" />
                                                                                                <td></td>

                                                                                                <td style="text-align: right; width: 72%;">
                                                                                                    <div>
                                                                                                        कलेक्टर ऑफ़ स्टाम्प्स
                                                                                        <br />
                                                                                                        <asp:Label ID="lblDRoffice5" runat="server"></asp:Label>
                                                                                                    </div>
                                                                                                </td>

                                                                                            </tr>

                                                                                        </table>

                                                                                    </ItemTemplate>
                                                                                    <FooterTemplate>
                                                                                        </table>
                                                                                    </FooterTemplate>




                                                                                </asp:Repeater>



                                                                            </asp:Panel>
                                                                        </div>
                                                                        <br>
                                                                    </div>
                                                                </div>





                                                                <div id="pnl_Ordersheet" class="mb-3">
                                                                    <%--<asp:DropDownList ID="DropDownList" runat="server" OnChange="SetTemplate(this.value)" CssClass="form-control">
                                                                        <asp:ListItem Text="-Select Template-" Value="0"></asp:ListItem>
                                                                        <asp:ListItem Text="Template 1" Value="1"></asp:ListItem>
                                                                        <asp:ListItem Text="Template 2" Value="2"></asp:ListItem>
                                                                        <asp:ListItem Text="Template 3" Value="3"></asp:ListItem>
                                                                        <asp:ListItem Text="Template 4" Value="4"></asp:ListItem>
                                                                        <asp:ListItem Text="Template 5" Value="5"></asp:ListItem>
                                                                    </asp:DropDownList>--%>
                                                                    <asp:DropDownList ID="ddlTemplates1" runat="server" OnChange="SetTemplate(this.value)" CssClass="form-control" EnableTheming="false">
                                                                    </asp:DropDownList>
                                                                </div>
                                                                <div id="editproceeding" clientidmode="Static" runat="server" class="showhide">
                                                                    <textarea id="summernote" runat="server" clientidmode="Static"></textarea>
                                                                    <div class="text-right">
                                                                        <a onclick="AddNotice()" data-toggle="pill"
                                                                            href="#custom_tabs_one_Order_Sheet" aria-controls="custom-tabs-one-home" aria-selected="true" class="btn btn-success" role="tabpanel"
                                                                            aria-labelledby="Proceeding">Save</a>
                                                                        <%--<asp:Button ID="btnSubmit" runat="server" Text="Save" class="btn btn-success" OnClick="" ClientIDMode="static" OnClientClick="return ValidateRecord()" />--%>
                                                                    </div>
                                                                </div>




                                                            </div>

                                                            <div class="tab-pane fade" id="custom_tabs_one_Order_Sheet" role="tabpanel"
                                                                aria-labelledby="Order_Sheet">

                                                                <asp:Panel runat="server" ID="HearingPnl">

                                                                    <div class="main-box htmldoc" runat="server" id="OrdersheetDiv" style="width: 100%; margin: 0 auto; text-align: center; border: 1px solid #ccc; padding: 30px 30px 30px 30px;">

                                                                        <h2 style="font-size: 18px; margin: 0; font-weight: 600;">न्यायालय कलेक्टर ऑफ़ स्टाम्प्स,
                                                                            <asp:Label ID="lblDRoffice1" runat="server"></asp:Label>
                                                                            (म.प्र.)</h2>
                                                                        <h3 style="margin: 0; margin: 10px; font-size: 16px;">प्ररूप-अ</h3>
                                                                        <h2 style="font-size: 16px; margin: 0; margin-bottom: 10px;">(परिपत्र दो-1 की कंडिका 1)</h2>
                                                                        <h3 style="margin: 0; margin: 10px; font-size: 16px;">राजस्व आदेशपत्र</h3>
                                                                        <h2 style="font-size: 16px; margin: 0; margin-bottom: 10px;">कलेक्टर ऑफ़ स्टाम्प,
                                                                            <asp:Label ID="lblDRoffice2" runat="server"></asp:Label>
                                                                            के न्यायालय में मामला क्रमांक-  
                                                                        <asp:Label ID="lblCaseNumber" runat="server"></asp:Label>
                                                                        </h2>
                                                                        <br>

                                                                        <table style="width: 100%; border: 1px solid black; border-collapse: collapse;">
                                                                            <tr>
                                                                                <th style="border: 1px solid black; border-collapse: collapse; line-height: 20px; padding: 5px; font-size: 14px;">आदेश क्रमांक कार्यवाही
                                                                                <br>
                                                                                    की तारीख एवं स्थान    </th>
                                                                                <th style="border: 1px solid black; border-collapse: collapse; line-height: 20px; padding: 5px; font-size: 14px;">पीठासीन अधिकारी के हस्ताक्षर सहित आदेश पत्र अथवा कार्यवाही
                                                                                  <br />
                                                                                    मध्यप्रदेश शासन विरूद्ध 
                                                                                <asp:Label ID="lblPartyName" runat="server" Visible="false"></asp:Label>
                                                                                </th>
                                                                                <th style="border: 1px solid black; border-collapse: collapse; line-height: 20px; padding: 5px; font-size: 14px;">पक्षों/वकीलों
                                                                                <br>
                                                                                    आदेश  पालक  लिपिक के हस्ताक्षर</th>
                                                                            </tr>



                                                                            <tr>
                                                                                <td style="border: 1px solid black; border-collapse: collapse; padding: 5px; font-size: 14px;">
                                                                                    <div class="content" style="padding: 15px">
                                                                                        <asp:Label ID="lblToday" runat="server"></asp:Label>


                                                                                    </div>
                                                                                </td>

                                                                                <td style="border: 1px solid black; border-collapse: collapse; padding: 5px; font-size: 14px;">
                                                                                    <div style="padding: 2px;">
                                                                                        <p>
                                                                                            Case Number : (<asp:Label ID="lblCaseNumNo" runat="server"></asp:Label>)
                                                                                         
                                                                                          <br />

                                                                                            <asp:Label ID="lblAppID" runat="server" Visible="false"></asp:Label>

                                                                                            <asp:Label ID="lblTodatDt" runat="server" Visible="false"></asp:Label>
                                                                                        </p>
                                                                                        <%--<p id="pContent">
                                                                                        </p>--%>
                                                                                        <p id="pContent" style="text-align: justify;" runat="server" clientidmode="Static">
                                                                                        </p>

                                                                                        <asp:UpdatePanel runat="server">
                                                                                            <ContentTemplate>
                                                                                                <asp:Panel runat="server" ID="CommentAddOnSheetPnl" Visible="false" Style="text-align: start">

                                                                                                    <asp:Label ID="lbltext" runat="server" ClientIDMode="Static" />
                                                                                                </asp:Panel>
                                                                                                <br />
                                                                                            </ContentTemplate>


                                                                                        </asp:UpdatePanel>


                                                                                        <%--                                                                                        <b style="float: left; text-align: center; padding: 2px 0 5px 0;">पेशी दिनांक
                                                                                        <br />
                                                                                            <asp:Label ID="lblHearingDt" runat="server" ClientIDMode="Static"></asp:Label>
                                                                                            <%--<asp:Label ID="Label1" runat="server" ClientIDMode="Static"></asp:Label>--%>
                                                                                        <%--<label id="txthearingdate"></label>
                                                                                        </b>
                                                                                        <p>
                                                                                        </p>--%>
                                                                                        <div id="DivTodayDetails" style="text-align: start">
                                                                                            <asp:UpdatePanel runat="server">
                                                                                                <ContentTemplate>
                                                                                                    <asp:Panel runat="server" ID="pnlTodayDetails">

                                                                                                        <asp:Label ID="lblTodayDetails" runat="server" />
                                                                                                    </asp:Panel>
                                                                                                    <br />
                                                                                                </ContentTemplate>


                                                                                            </asp:UpdatePanel>

                                                                                        </div>

                                                                                        <div id="DivHearing">
                                                                                            <asp:UpdatePanel runat="server">
                                                                                                <ContentTemplate>
                                                                                                    <b style="float: left; text-align: center; padding: 2px 0 5px 0;">पेशी दिनांक
                                                                                        <br />
                                                                                                        <asp:Label ID="lblHearingDt" runat="server" ClientIDMode="Static"></asp:Label>
                                                                                                        <%--<asp:Label ID="Label1" runat="server" ClientIDMode="Static"></asp:Label>--%>
                                                                                                        <%--<label id="txthearingdate"></label>--%>
                                                                                                    </b>
                                                                                                    <p>
                                                                                                    </p>
                                                                                                </ContentTemplate>


                                                                                            </asp:UpdatePanel>
                                                                                        </div>

                                                                                        <div id="DivExecution">

                                                                                            <b style="float: left; text-align: center; padding: 2px 0 5px 0;">आदेश के लिए नियत दिनांक
                                                                                        <br />
                                                                                                <asp:Label ID="lblFurtherDt" runat="server" ClientIDMode="Static"></asp:Label>
                                                                                                <%--<asp:Label ID="Label1" runat="server" ClientIDMode="Static"></asp:Label>--%>
                                                                                                <label id="lblExecutionDt"></label>
                                                                                            </b>
                                                                                            <p>
                                                                                            </p>
                                                                                        </div>
                                                                                        <div id="DivToday">

                                                                                            <b style="float: left; text-align: center; padding: 2px 0 5px 0;">दिनांक
                                                                                        <br />
                                                                                                <asp:Label ID="lblTodate" runat="server" ClientIDMode="Static"></asp:Label>
                                                                                                <%--<asp:Label ID="Label1" runat="server" ClientIDMode="Static"></asp:Label>--%>
                                                                                            
                                                                                            </b>
                                                                                            <p>
                                                                                            </p>
                                                                                        </div>

                                                                                        <b style="float: right; text-align: center; padding: 2px 0 5px 0;">कलेक्टर ऑफ़ स्टाम्प्स,<br />
                                                                                            <asp:Label ID="lblDRoffice3" runat="server"> </asp:Label>
                                                                                            <asp:HiddenField ID="hdnSROfficeNameHi" runat="server" />

                                                                                            <br />
                                                                                            <br />
                                                                                        </b>
                                                                                    </div>
                                                                                </td>

                                                                                <td>
                                                                                    <asp:Label ID="lblVerifiedParty" runat="server" ClientIDMode="Static"></asp:Label>
                                                                                    <br />
                                                                                    <br />
                                                                                    <asp:Label ID="lblVerifiedParty1" runat="server" ClientIDMode="Static"></asp:Label>
                                                                                    <div id="message111"></div>

                                                                                </td>

                                                                                <%--<td style="border: 1px solid black; border-collapse: collapse; padding: 5px; font-size: 14px;">
                                                                                    <asp:Label ID="lblVerifiedParty" runat="server" ClientIDMode="Static"></asp:Label>
                                                                                    <asp:Label runat="server" ID="lblVerifiedParty1"></asp:Label>
                                                                                    <asp:Label runat="server" ID="lblVerifiedParty2"></asp:Label>
                                                                                </td>--%>
                                                                            </tr>
                                                                        </table>


                                                                        <br />
                                                                        <br />


                                                                    </div>



                                                                </asp:Panel>
                                                                <br />
                                                                <asp:UpdatePanel ID="UpdatePanel211" runat="server">
                                                                    <ContentTemplate>
                                                                        <asp:Button ID="btnDraftSave" runat="server" Text="Save ordersheet Draft" class="btn btn-success" Visible="true" OnClick="btnDraftSave_Click" ClientIDMode="static" OnClientClick="Loader();" />

                                                                    </ContentTemplate>

                                                                </asp:UpdatePanel>


                                                                <div id="hearing">
                                                                    <div id="PartyVerify_Pnl" style="display: none;">



                                                                        <h6><b>Parties Mark Appearance</b></h6>
                                                                        <div class="mycheck mul_check">
                                                                            <div class="custom-control custom-radio mycb">
                                                                                <input class="custom-control-input" type="radio" id="yes" name="customRadio">
                                                                                <label for="yes" class="custom-control-label">Yes</label>
                                                                            </div>
                                                                            <div class="custom-control custom-radio">
                                                                                <input class="custom-control-input" type="radio" id="no" name="customRadio">
                                                                                <label for="no" class="custom-control-label">No</label>
                                                                            </div>
                                                                        </div>

                                                                    </div>



                                                                    <div id="ifselectyes">

                                                                        <div id="ifPartiesVerify">
                                                                            <br>
                                                                            <div class="certi_info bg-light">Parties Verification </div>
                                                                            <br>

                                                                            <div class="table-responsive listtabl">

                                                                                <asp:UpdatePanel ID="upnl1" runat="server">
                                                                                    <ContentTemplate>

                                                                                        <asp:Label ID="lblMessage" Font-Bold="true" runat="server" ClientIDMode="Static" Visible="false" class="message" />

                                                                                        <asp:Label ID="lblotp" Visible="true" Font-Bold="true" runat="server" class="message" />


                                                                                        <asp:GridView ID="grdPartyDetails" CssClass="table table-bordered table-condensed table-striped table-hover"
                                                                                            DataKeyNames="Party_ID"
                                                                                            runat="server" CellPadding="4" ForeColor="#333333" GridLines="None" AutoGenerateColumns="false" Visible="true" EmptyDataText="No record found" OnRowCommand="grdPartyDetails_RowCommand">
                                                                                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                                                                            <Columns>
                                                                                                <asp:TemplateField HeaderText="SNo." HeaderStyle-VerticalAlign="Top" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="false">
                                                                                                    <ItemTemplate>
                                                                                                        <itemtemplate><%#Container.DataItemIndex+1  %> </itemtemplate>
                                                                                                    </ItemTemplate>
                                                                                                    <ItemStyle Width="5%" HorizontalAlign="Center" />
                                                                                                    <HeaderStyle Width="5%" HorizontalAlign="Center" />
                                                                                                </asp:TemplateField>
                                                                                                <asp:TemplateField HeaderText="ID" HeaderStyle-Font-Bold="false">
                                                                                                    <ItemTemplate>
                                                                                                        <%#Eval("party_id") %>
                                                                                                    </ItemTemplate>
                                                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                                                </asp:TemplateField>
                                                                                                <asp:TemplateField HeaderText="Party Type" HeaderStyle-Font-Bold="false">
                                                                                                    <ItemTemplate>
                                                                                                        <%#Eval("PARTY_TYPE_NAME_EN") %>
                                                                                                    </ItemTemplate>
                                                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                                                </asp:TemplateField>
                                                                                                <asp:TemplateField HeaderText="Party Name" HeaderStyle-Font-Bold="false">
                                                                                                    <ItemTemplate>
                                                                                                        <%#Eval("Party_Name") %>
                                                                                                    </ItemTemplate>
                                                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                                                </asp:TemplateField>
                                                                                                <asp:TemplateField HeaderText="Email" HeaderStyle-Font-Bold="false">
                                                                                                    <ItemTemplate>
                                                                                                        <%#Eval("Email_Id") %>
                                                                                                    </ItemTemplate>
                                                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                                                </asp:TemplateField>
                                                                                                <asp:TemplateField HeaderText="Mobile No." HeaderStyle-Font-Bold="false">
                                                                                                    <ItemTemplate>
                                                                                                        <%#Eval("Mob_No") %>
                                                                                                    </ItemTemplate>
                                                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                                                </asp:TemplateField>

                                                                                                <asp:TemplateField HeaderText="Verify" Visible="true" HeaderStyle-Font-Bold="false">

                                                                                                    <ItemTemplate>
                                                                                                        <asp:LinkButton ID="lnkSendSMS" CssClass="btn btn-success btn-sm" runat="server" Text="Send OTP" CommandName="SendOTP" OnClientClick="Loader();" CommandArgument='<%#Eval("Mob_No")+","+Eval("party_id")+","+Eval("Party_Name") %>'></asp:LinkButton>
                                                                                                        <%--OR <asp:CheckBox ID="chkItems" runat="server" CssClass="item" />--%>
                                                                                                        <asp:Image ImageUrl="../dist/img/verify.gif" ID="imgverify" runat="server" Visible="false" Width="25px" />


                                                                                                    </ItemTemplate>
                                                                                                </asp:TemplateField>

                                                                                                <asp:TemplateField HeaderText="Status" HeaderStyle-Font-Bold="false">
                                                                                                    <ItemTemplate>

                                                                                                        <asp:Label ID="LblStatus" runat="server" Text='<%# Eval("OTP_status") %>'></asp:Label>
                                                                                                    </ItemTemplate>
                                                                                                    <HeaderStyle Width="10%" HorizontalAlign="Left" />
                                                                                                    <ItemStyle Width="10%" HorizontalAlign="Left" />
                                                                                                </asp:TemplateField>

                                                                                                <asp:TemplateField HeaderText="Behalf of attendees" HeaderStyle-Font-Bold="false">
                                                                                                    <ItemTemplate>

                                                                                                        <asp:Label ID="LblADTNL_PARTY_NAME" runat="server" Text='<%# Eval("ADTNL_PARTY_NAME") %>'></asp:Label>
                                                                                                    </ItemTemplate>
                                                                                                    <HeaderStyle Width="10%" HorizontalAlign="Left" />
                                                                                                    <ItemStyle Width="10%" HorizontalAlign="Left" />
                                                                                                </asp:TemplateField>

                                                                                                <%--  <asp:TemplateField HeaderText="Verify">

                                                                                            <ItemTemplate>
                                                                                            </ItemTemplate>


                                                                                            <ItemTemplate>


                                                                                                <asp:Panel ID="OTP_Pnl" runat="server" Visible="false">
                                                                                                     <table class="table table-bordered alert-success" width="100%">
                                                                                                    <tr>

                                                                                                        <td style="text-align: right" width="25%">

                                                                                                            <span class="ui-widget">OTP: <span style="color: red">*</span>:</span>

                                                                                                        </td>
                                                                                                        <td align="left" class="LabelBold" width="25%">

                                                                                                            <asp:TextBox ID="txtOTP" runat="server" placeholder="Enter OTP" Width="70%" CssClass="form-control intro-x login__input py-3 px-4" TextMode="Password" MaxLength="4"></asp:TextBox>

                                                                                                            <asp:Button ID="Button6" runat="server" OnClick="Btn_VerifyOTP" class="btn btn-success" Text="Verify OTP" />
                                                                                                            <br />
                                                                                                            <asp:Label ID="lblMessage" runat="server" Style="color: #FF0000" Text=""></asp:Label>
                                                                                                            <br />

                                                                                                        </td>
                                                                                                    </tr>

                                                                                                </table>

                                                                                                </asp:Panel>
                                                                                            </ItemTemplate>

                                                                                        </asp:TemplateField>--%>
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

                                                                                    </ContentTemplate>

                                                                                </asp:UpdatePanel>



                                                                                <asp:UpdatePanel runat="server">
                                                                                    <ContentTemplate>
                                                                                        <asp:Panel runat="server" ID="otppnl"></asp:Panel>

                                                                                        <div class="modal fade" id="Receive_OTP1" role="dialog">
                                                                                            <div class="modal-dialog">
                                                                                                <div class="modal-content">
                                                                                                    <div class="modal-header text-center">
                                                                                                        <a class="btn pull-right" data-dismiss="modal"><span>&times;</span></a>
                                                                                                        <h3 class="register_header"><strong>Please enter the OTP sent to your registered mobile # 
                                                                                                    <asp:Label ID="lblmobnum" runat="server"></asp:Label>
                                                                                                        </strong></h3>
                                                                                                    </div>

                                                                                                    <div class="modal-body">
                                                                                                        <asp:TextBox ID="txtOTP1" CssClass="form-control" placeholder="One Time Password" runat="server" TextMode="Password" MaxLength="4"></asp:TextBox>
                                                                                                    </div>
                                                                                                    <div class="modal-footer">
                                                                                                        <h4 style="font-size: 0.8rem;">This OTP will expire in 15 minutes.</h4>
                                                                                                        <asp:Button runat="server" Text="Validate OTP" class="btn btn-success otpvalidate" ID="otpvalidate" OnClientClick="Loader();" OnClick="verifyOTP_Click" UseSubmitBehavior="false" data-dismiss="modal" />

                                                                                                        <asp:HyperLink NavigateUrl="navigateurl" runat="server" Text="Resend OTP" />



                                                                                                    </div>
                                                                                                </div>
                                                                                            </div>
                                                                                        </div>

                                                                                        <%-- <div id="Receive_OTP" class="modal fade" role="dialog">
                                                                                            <div class="modal-dialog">
                                                                                                <div class="modal-content">
                                                                                                    <div class="modal-header">

                                                                                                        <h5 class="modal-title">OTP Verification</h5>
                                                                                                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                                                                                            <span aria-hidden="true">&times;</span>
                                                                                                        </button>
                                                                                                    </div>
                                                                                                    <div class="modal-body">
                                                                                                        <h4 class="register_header"><strong>Please enter the OTP sent to your registered mobile  
                       <asp:Label ID="Label1" runat="server"></asp:Label>
                                                                                                        </strong></h4>

                                                                                                        <asp:TextBox ID="txtOTP" CssClass="form-control" placeholder="One Time Password" runat="server" TextMode="Password" MaxLength="4"></asp:TextBox>
                                                                                                        <span style="color: red;">
                                                                                                            <h6>This OTP will expire in 15 minutes.</h6>
                                                                                                        </span>
                                                                                                    </div>


                                                                                                    <div class="row">
                                                                                                        <div class="col-md-6">

                                                                                                            <div id="countdown"></div>


                                                                                                            <asp:Button ID="ResendButton" runat="server" Text="Resend OTP" class="btn btn-success" OnClientClick="return resendOTP();" OnClick="ResendButton_Click" Style="display: none;" />



                                                                                                        </div>

                                                                                                    </div>

                                                                                                    <div class="modal-footer">
                                                                                                        <asp:Button runat="server" Text="Validate OTP" class="btn btn-primary otpvalidate" ID="Button2" OnClick="verifyOTP_Click" OnClientClick="Loader();" UseSubmitBehavior="false" data-dismiss="modal" />
                                                                                                        <%-- <asp:HyperLink NavigateUrl="navigateurl" runat="server" Text="Resend OTP" class="btn btn-primary" />
                                                                                                    </div>
                                                                                                </div>
                                                                                            </div>
                                                                                        </div>--%>

                                                                                        <div id="Receive_OTP" class="modal fade" role="dialog">
                                                                                            <div class="modal-dialog">
                                                                                                <div class="modal-content">
                                                                                                    <div class="modal-header">
                                                                                                        <h5 class="modal-title">OTP Verification</h5>
                                                                                                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                                                                                            <span aria-hidden="true">&times;</span>
                                                                                                        </button>
                                                                                                    </div>
                                                                                                    <div class="modal-body">
                                                                                                        <div id="radioButtons" runat="server" style="display: block;">
                                                                                                            <asp:RadioButton ID="RbtnOldNum" runat="server" Text="Registered Mobile No.  " GroupName="Options" OnCheckedChanged="RadioButton_CheckedChanged" Checked="true" AutoPostBack="true" />

                                                                                                            <asp:RadioButton ID="RbtnNewNum" runat="server" Text="New Mobile No.  " GroupName="Options" OnCheckedChanged="RadioButton_CheckedChanged" OnClientClick="Loader();" AutoPostBack="true" />
                                                                                                        </div>

                                                                                                        <!-- OTP Section -->
                                                                                                        <div id="OtpDiv" style="display: block;" runat="server">
                                                                                                            <h4 class="register_header">
                                                                                                                <strong>Please enter the OTP sent to your registered mobile  
                           
                                                                                                                    <asp:Label ID="Label1" runat="server"></asp:Label>
                                                                                                                </strong>
                                                                                                            </h4>
                                                                                                            <asp:TextBox ID="txtOTP" CssClass="form-control" placeholder="One Time Password" runat="server" TextMode="Password" MaxLength="4"></asp:TextBox>
                                                                                                            <span style="color: red;">
                                                                                                                <h6>This OTP will expire in 15 minutes.</h6>
                                                                                                            </span>
                                                                                                            <div class="row">
                                                                                                                <div class="col-md-6">
                                                                                                                    <div id="countdown"></div>
                                                                                                                    <asp:Button ID="ResendButton" runat="server" Text="Resend OTP" class="btn btn-success" OnClientClick="return resendOTP();" OnClick="ResendButton_Click" Style="display: none;" />
                                                                                                                </div>
                                                                                                            </div>
                                                                                                        </div>

                                                                                                        <!-- New Mobile Number Section -->
                                                                                                        <div id="dvNewMobileNum" style="display: none;" runat="server">
                                                                                                            <div class="form-group my-2">
                                                                                                                <asp:Label ID="lblNewMob" runat="server">Please Enter New Mobile Number </asp:Label>
                                                                                                                <asp:TextBox ID="txtNewMob" runat="server" ClientIDMode="Static" CssClass="form-control rounded-0" MaxLength="10" onkeypress="return isNumber(event)" placeholder="Enter Mobile Number"></asp:TextBox>
                                                                                                            </div>
                                                                                                        </div>

                                                                                                        <div id="msgdisplay" runat="server" style="display: none;">
                                                                                                            <asp:Label ID="lblMsg" Font-Bold="true" runat="server" ClientIDMode="Static" class="message" />
                                                                                                        </div>
                                                                                                    </div>
                                                                                                    <div class="modal-footer">
                                                                                                        <asp:Button ID="BtnReSend" runat="server" Text="Resend OTP" class="btn btn-success" OnClientClick="return resendOTP();" OnClick="ResendButton_Click" Style="display: none;" />
                                                                                                        <asp:Button runat="server" Text="Validate OTP" class="btn btn-primary otpvalidate" ID="BtnValidate" OnClick="verifyOTP_Click" OnClientClick="Loader();" UseSubmitBehavior="false" data-dismiss="modal" Style="display: block;" />
                                                                                                        <asp:Button ID="btnNewMobSave" runat="server" Text="Send OTP" CssClass="btn btn-primary btn-large" OnClick="btnNewMobSave_Click" EnableTheming="false" Style="display: none;" />
                                                                                                        <a href="#" class="btn" data-dismiss="modal" aria-hidden="true" onclick="window.focus();">Close</a>
                                                                                                    </div>
                                                                                                </div>
                                                                                            </div>
                                                                                        </div>

                                                                                    </ContentTemplate>

                                                                                </asp:UpdatePanel>

                                                                                <%-- <asp:Label ID="lblMsg" runat="server" Text="Label"></asp:Label>--%>
                                                                            </div>

                                                                        </div>



                                                                        <br />
                                                                        <br />

                                                                        <div id="AddComment">
                                                                            <div class="mt-3">
                                                                                <asp:UpdatePanel runat="server">
                                                                                    <ContentTemplate>
                                                                                        <asp:Panel ID="CommentPnl" Visible="false" runat="server">

                                                                                            <div class="certi_info bg-light" style="margin-top: -56px;">Additional Parties </div>
                                                                                            <br>

                                                                                            <asp:GridView ID="GvAddParty" CssClass="table table-bordered table-condensed table-striped table-hover"
                                                                                                DataKeyNames="APP_ID"
                                                                                                runat="server" CellPadding="4" ForeColor="#333333" GridLines="None" AutoGenerateColumns="false" Visible="true" EmptyDataText="No record found" OnRowCommand="grdPartyDetails_RowCommand">
                                                                                                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                                                                                <Columns>
                                                                                                    <asp:TemplateField HeaderText="SNo." HeaderStyle-VerticalAlign="Top" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="false">
                                                                                                        <ItemTemplate>
                                                                                                            <itemtemplate><%#Container.DataItemIndex+1  %> </itemtemplate>
                                                                                                        </ItemTemplate>
                                                                                                        <ItemStyle Width="5%" HorizontalAlign="Center" />
                                                                                                        <HeaderStyle Width="5%" HorizontalAlign="Center" />
                                                                                                    </asp:TemplateField>
                                                                                                    <asp:TemplateField HeaderText="Name" HeaderStyle-Font-Bold="false">
                                                                                                        <ItemTemplate>
                                                                                                            <%#Eval("ADTNL_PARTY_NAME") %>
                                                                                                        </ItemTemplate>
                                                                                                        <HeaderStyle Width="10%" HorizontalAlign="Left" />
                                                                                                        <ItemStyle Width="10%" HorizontalAlign="Left" />
                                                                                                    </asp:TemplateField>
                                                                                                    <asp:TemplateField HeaderText="Party Type">
                                                                                                        <ItemTemplate>
                                                                                                            <%#Eval("ADTNL_PARTY_TYPE") %>
                                                                                                        </ItemTemplate>
                                                                                                        <HeaderStyle Width="10%" HorizontalAlign="Left" />
                                                                                                        <ItemStyle Width="10%" HorizontalAlign="Left" />
                                                                                                    </asp:TemplateField>
                                                                                                    <asp:TemplateField HeaderText="Party By" HeaderStyle-Font-Bold="false">
                                                                                                        <ItemTemplate>
                                                                                                            <%#Eval("PARTY_BY") %>
                                                                                                        </ItemTemplate>
                                                                                                        <HeaderStyle Width="10%" HorizontalAlign="Left" />
                                                                                                        <ItemStyle Width="10%" HorizontalAlign="Left" />
                                                                                                    </asp:TemplateField>

                                                                                                    <asp:TemplateField HeaderText="Mobile No." HeaderStyle-Font-Bold="false">
                                                                                                        <ItemTemplate>
                                                                                                            <%#Eval("PHONE_NO") %>
                                                                                                        </ItemTemplate>
                                                                                                        <HeaderStyle Width="10%" HorizontalAlign="Left" />
                                                                                                        <ItemStyle Width="10%" HorizontalAlign="Left" />
                                                                                                    </asp:TemplateField>
                                                                                                </Columns>
                                                                                                <EditRowStyle BackColor="#999999" />
                                                                                                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                                                                                <HeaderStyle BackColor="#e9564e" Font-Bold="True" ForeColor="White" />

                                                                                            </asp:GridView>
                                                                                        </asp:Panel>
                                                                                    </ContentTemplate>

                                                                                </asp:UpdatePanel>
                                                                            </div>
                                                                        </div>
                                                                        <div id="ifAdditionalParties">



                                                                            <h6><b>Have Any Additional Parties ?</b></h6>
                                                                            <div class="mycheck mul_check">
                                                                                <div class="custom-control custom-radio mycb">
                                                                                    <input class="custom-control-input" type="radio" id="yes11" name="customRadio11">
                                                                                    <label for="yes11" class="custom-control-label">Yes</label>
                                                                                </div>
                                                                                <div class="custom-control custom-radio">
                                                                                    <input class="custom-control-input" type="radio" id="no11" name="customRadio11">
                                                                                    <label for="no11" class="custom-control-label">No</label>
                                                                                </div>
                                                                            </div>

                                                                            <div id="ifselectyes1" class="row float-right mt-1">
                                                                                <asp:Panel ID="Pnl_BtnAddPary" Visible="true" runat="server" class="mr-2">
                                                                                    <%-- <div class="row  float-right mt-3 mr-0">--%>
                                                                                    <button type="button" class="btn btn-info" id="addrow">Add Party</button>
                                                                                    <%--</div>--%>
                                                                                </asp:Panel>
                                                                            </div>




                                                                        </div>

                                                                        <div id="addnewrow" class="mt-5">
                                                                            <asp:UpdatePanel runat="server">
                                                                                <ContentTemplate>
                                                                                    <asp:Panel ID="PartyPnl" Visible="true" runat="server">

                                                                                        <div class="row mt-3">
                                                                                            <div class="col-6">
                                                                                                <asp:TextBox ID="txtName" CssClass="form-control" runat="server" ValidationGroup="a" placeholder="Enter Name"></asp:TextBox>
                                                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                                                                                                    ControlToValidate="txtName" ValidationGroup="a" CssClass="text-red" ErrorMessage="* Name required"></asp:RequiredFieldValidator>
                                                                                            </div>
                                                                                            <div class="col-6">
                                                                                                <asp:TextBox ID="txtType" CssClass="form-control" runat="server" ValidationGroup="a" placeholder="Enter Type"></asp:TextBox>

                                                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                                                                                                    ControlToValidate="txtType" ValidationGroup="a" CssClass="text-red" ErrorMessage="* Type required"></asp:RequiredFieldValidator>
                                                                                            </div>
                                                                                        </div>
                                                                                        <div class="row mt-3">
                                                                                            <div class="col-4">
                                                                                                <%--<asp:TextBox ID="txtMobile" runat="server" MaxLength="10" CssClass="form-control" placeholder="Enter Mobile"></asp:TextBox>--%>
                                                                                                <asp:TextBox ID="txtMobile" CssClass="form-control" runat="server" MaxLength="10" Enabled="true" ClientIDMode="Static" AutoCompleteType="None" onkeypress='return restrictAlphabets(event)' autocomplete="off" placeholder="Enter Mobile"></asp:TextBox>
                                                                                                <%--<asp:TextBox ID="txtMobile1" CssClass="form-control" runat="server" MaxLength="10" type="number" placeholder="Enter Mobile"></asp:TextBox>--%>

                                                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server"
                                                                                                    ControlToValidate="txtMobile" ValidationGroup="a" CssClass="text-red" ErrorMessage="* Mobile No required"></asp:RequiredFieldValidator>
                                                                                            </div>
                                                                                            <div class="col-4">
                                                                                                <asp:DropDownList ID="ddlPartyBy" runat="server" CssClass="form-select form-control" AutoPostBack="true">
                                                                                                </asp:DropDownList>
                                                                                                <asp:RequiredFieldValidator ErrorMessage="Required" CssClass="text-red" ValidationGroup="a" ControlToValidate="ddlPartyBy"
                                                                                                    InitialValue="0" runat="server" ForeColor="Red" />

                                                                                            </div>
                                                                                            <div class="col-4 float-right mr-0 mb-3">
                                                                                                <asp:Button ID="btnAddParty" runat="server" Text="Add" CssClass="btn btn-info" ValidationGroup="a" OnClientClick="Loader();" CausesValidation="true" OnClick="btnAddParty_Click" />
                                                                                                <%--<button type="button" class="btn btn-success">Save</button>--%>
                                                                                            </div>
                                                                                        </div>




                                                                                        <asp:GridView ID="GrdAddParty" DataKeyNames="Name,PartyType,Mobile_No,Party_By,Party_id,QFlag,Sno,Type" CssClass="table table-striped table-bordered table-hover" runat="server" OnRowDeleting="GrdAddParty_RowDeleting1" AutoGenerateColumns="false">
                                                                                            <Columns>

                                                                                                <asp:BoundField DataField="Name" HeaderText="Name" />
                                                                                                <asp:BoundField DataField="PartyType" HeaderText="Party Type" />
                                                                                                <asp:BoundField DataField="Mobile_No" HeaderText="Mobile No" />
                                                                                                <asp:BoundField DataField="Party_By" HeaderText="Party By" />
                                                                                                <asp:BoundField DataField="Party_id" HeaderText="Party id" />

                                                                                                <asp:TemplateField HeaderText="Action">
                                                                                                    <ItemStyle Width="10%" HorizontalAlign="Center" />
                                                                                                    <ItemTemplate>
                                                                                                        <asp:LinkButton ID="LinkButton1" runat="server" CssClass="btn btn-danger" CommandName="delete" ToolTip="Delete" OnClientClick="return confirm('Are you sure you want to delete this entry?');">Delete </asp:LinkButton>
                                                                                                        <%--<asp:LinkButton ID="LinkButton1" runat="server" CssClass="btn btn-danger btn-sm" CommandName="delete" ToolTip="Delete" OnClientClick="return fnConfirmDelete(this);">Delete </asp:LinkButton>--%>
                                                                                                    </ItemTemplate>
                                                                                                </asp:TemplateField>
                                                                                            </Columns>
                                                                                        </asp:GridView>

                                                                                        <div class="row">
                                                                                        </div>
                                                                                        <div class=" float-right">
                                                                                            <asp:Button Text="Submit" ID="PartyBntSubmit" CssClass="btn btn-success" Visible="false" OnClientClick="Loader();" OnClick="btnSubmit_AddParty" runat="server" />
                                                                                        </div>
                                                                                        <br>
                                                                                        <br>
                                                                                    </asp:Panel>
                                                                                </ContentTemplate>

                                                                            </asp:UpdatePanel>

                                                                        </div>


                                                                    </div>
                                                                    <br>
                                                                    <div id="ifselectyes_Template">
                                                                        <asp:UpdatePanel runat="server">
                                                                            <ContentTemplate>
                                                                                <div class="certi_info bg-light" style="margin-top: -15px;">Add Template </div>
                                                                                <br>


                                                                                <asp:Label ID="lbltext1" runat="server" />
                                                                                <asp:CheckBoxList ID="chkContent" runat="server">
                                                                                </asp:CheckBoxList>
                                                                                <div class="float-right">
                                                                                    <asp:Button runat="server" ID="AddComtBtn" Visible="true" Text="Submit" OnClientClick="Loader();" OnClick="Unnamed_Click1" CssClass="btn btn-info" />
                                                                                    <asp:Button runat="server" ID="EsignSubmitBtn" Visible="false" Text="Submit" OnClientClick="Loader();" OnClick="SaveHearingBtn_Click" CssClass="btn btn-info" />

                                                                                </div>
                                                                            </ContentTemplate>

                                                                        </asp:UpdatePanel>
                                                                    </div>
                                                                    <br>
                                                                    <button type="button" style="display: none" class="btn btn-success" id="nexthearing1">Next </button>
                                                                </div>





                                                                <div class="tab-content" style="display: none" id="custom-tabs-one-tabContent2">
                                                                    <div class="row">
                                                                        <div class="col-lg-12 text-center">
                                                                            <iframe height="450" width="450" scrolling="auto" runat="server" id="iFrame1" />
                                                                        </div>
                                                                    </div>
                                                                </div>

                                                                <div id="newhearing">

                                                                    <div class="mycheck mul_check">
                                                                        <div class="custom-control custom-radio mycb">
                                                                            <input class="custom-control-input" type="radio" id="Send_Notice" name="notice">
                                                                            <label for="Send_Notice" class="custom-control-label">Send Notice</label>
                                                                        </div>
                                                                        <div class="custom-control custom-radio">
                                                                            <input class="custom-control-input" type="radio" id="Final_Order" name="notice">
                                                                            <label for="Final_Order" class="custom-control-label">Final Order</label>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <asp:UpdatePanel ID="upnlDateCalender" runat="server">

                                                                    <ContentTemplate>
                                                                        <div class="mt-3 divshow" runat="server" id="hearingdate" style="display: none">
                                                                            <div class="form-group row" id="DateDiv">

                                                                                <div class="col-sm-6">
                                                                                    <asp:Panel runat="server" ID="txtHearingDt"></asp:Panel>
                                                                                    <label for="inputEmail3" class="col-sm-6 col-form-label">Select Hearing Date</label>

                                                                                    <asp:TextBox ID="txtHearingDate" CssClass="form-control" runat="server" onChange="SetHearingDate(this)" onclick="generateCalenderByCode();" placeHolder="dd/mm/yyyy"></asp:TextBox>
                                                                                    <%--<asp:TextBox ID="txtHearingDate" CssClass="form-control" runat="server" onChange="SetHearingDate(this) " TextMode="Date"></asp:TextBox>--%>
                                                                                </div>
                                                                                <div class="col-sm-4 mt-auto text-left">
                                                                                    <%--<a onclick="AddOrderSheet()" data-toggle="pill"
                                                                                        href="#custom_tabs_one_Order_Sheet" aria-controls="custom-tabs-one-home" aria-selected="true" class="btn btn-success" role="tabpanel"
                                                                                        aria-labelledby="Proceeding">Save</a>--%>


                                                                                    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                                                                        <ContentTemplate>
                                                                                            <div class="text-right" style="padding-left: 30px;">
                                                                                                <asp:Button Text="Save" ID="BtnNextNotice" CssClass="btn btn-success" OnClick="BtnNextNotice_Click" runat="server" />

                                                                                            </div>
                                                                                        </ContentTemplate>
                                                                                    </asp:UpdatePanel>

                                                                                </div>
                                                                            </div>
                                                                            <div id="divCalender" class="calendar-section" runat="server" style="display: none">
                                                                                <div class="calendar-inner-sec">
                                                                                    <span class="der"></span>
                                                                                    <asp:Calendar ID="Calendar1" runat="server" OnClientClick="OnCalendarSelectionChanged();" ClientIDMode="Static" OnVisibleMonthChanged="Calendar1_VisibleMonthChanged" OnDayRender="Calendar1_DayRender" OnSelectionChanged="Calendar1_SelectionChanged"></asp:Calendar>
                                                                                </div>
                                                                            </div>
                                                                            <div class="col-sm-3">
                                                                                <div class="form-group">
                                                                                </div>
                                                                            </div>
                                                                        </div>

                                                                    </ContentTemplate>
                                                                </asp:UpdatePanel>
                                                                <div id="S_Notice">
                                                                    <div id="S_Notice2" style="display: none">
                                                                        <div class="certi_info bg-light">eSign / DSC</div>
                                                                        <br>
                                                                        <div class="form-group row">
                                                                            <div class="col-sm-3">
                                                                                <asp:Label runat="server" Visible="false" ID="lblStatus_Notice" Text=""></asp:Label>
                                                                                <asp:DropDownList ID="ddl_SignOption_Notice" runat="server" CssClass="form-control">
                                                                                   <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                                                                    <asp:ListItem Text="Aadhar eSign (CDAC)" Value="1"></asp:ListItem>
                                                                                    <asp:ListItem Text="Aadhar eSign (eMudra)" Value="3"></asp:ListItem>
                                                                                    <asp:ListItem Text="DSC" Value="2"></asp:ListItem>
                                                                                    <%--<asp:ListItem Text="(2) - Digital Signature (using DSC Dongle)" Value="2"></asp:ListItem>
                                                                                    <asp:ListItem Text="(3) - Download  Sign and Upload" Value="3"></asp:ListItem>--%>

                                                                                </asp:DropDownList>
                                                                            </div>
                                                                             <div class="col-sm-3">
                                                                                <asp:DropDownList ID="ddleAuthMode_Notice" ClientIDMode="Static" runat="server" CssClass="form-control">

                                                                                    <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                                                                    <asp:ListItem Text="OTP" Value="1"></asp:ListItem>
                                                                                    <asp:ListItem Text="Biometric" Value="2"></asp:ListItem>
                                                                                </asp:DropDownList>
                                                                                <%-- <asp:Label ID="Label1" runat="server"></asp:Label>
                                                                                <asp:TextBox ID="TextBox1" placeholder="Enter last 4 digit of Adhar Card" MaxLength="4" Enabled="true" onkeypress="return NumberOnly(event)" ClientIDMode="Static" CssClass="form-control" runat="server" AutoCompleteType="None" autocomplete="off"></asp:TextBox>--%>
                                                                            </div>
                                                                            <div class="col-sm-3">
                                                                                <asp:TextBox ID="TxtLast4Digit_Notice" placeholder="Enter last 4 digit of Adhar Card" MaxLength="4" Enabled="true" onkeypress="return NumberOnly(event)" ClientIDMode="Static" CssClass="form-control" runat="server" AutoCompleteType="None" autocomplete="off"></asp:TextBox>
                                                                            </div>
                                                                            <div class="col-sm-3">

                                                                                <asp:Button ID="btnEsignDSC_Notice" runat="server" OnClick="btnEsignDSC_Notice_Click" class="btn btn-success" Text="eSign/DSC" OnClientClick="return ValidateEsignRecord_Notice()" />
                                                                            </div>
                                                                        </div>


                                                                    </div>



                                                                </div>
                                                                <div id="F_Notice">
                                                                    <br>
                                                                    <div class="mycheck ml-2">
                                                                        <div class="custom-control custom-radio mycb">
                                                                            <input class="custom-control-input custom-control-input-teal" type="radio" id="today" name="oredr_date">
                                                                            <label for="today" class="custom-control-label">Today</label>
                                                                        </div>
                                                                        <div class="custom-control custom-radio">
                                                                            <input class="custom-control-input custom-control-input-teal" type="radio" id="save_for_Later" name="oredr_date">
                                                                            <label for="save_for_Later" class="custom-control-label">Save for Later</label>
                                                                        </div>

                                                                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                                            <ContentTemplate>
                                                                                <div class="text-right" style="padding-left: 30px;">
                                                                                    <asp:Button Text="Save" ID="BtnFinalOrder" CssClass="btn btn-success" OnClick="BtnFinalOrder_Click" runat="server" />

                                                                                </div>
                                                                            </ContentTemplate>


                                                                        </asp:UpdatePanel>



                                                                    </div>
                                                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                                        <ContentTemplate>

                                                                            <div class="mt-3" id="ExecutionDt" style="display: none">
                                                                                <div class="form-group row" id="DivFurtherDt">

                                                                                    <div class="col-sm-6">
                                                                                        <asp:Panel runat="server" ID="Panel1"></asp:Panel>
                                                                                        <label for="inputEmail3" class="col-sm-12 col-form-label">Select Further Execution Date</label>
                                                                                        <asp:TextBox ID="txtFurtherDt" CssClass="form-control" runat="server" onChange="SetHearingDate(this)" onclick="generateCalenderByCode();" TextMode="Date" placeHolder="dd/mm/yyyy"></asp:TextBox>
                                                                                        <%--<asp:TextBox ID="txtHearingDate" CssClass="form-control" runat="server" onChange="SetHearingDate(this) " TextMode="Date"></asp:TextBox>--%>
                                                                                    </div>

                                                                                    <div class="col-sm-4 mt-auto">
                                                                                        <asp:Button runat="server" ID="btnSaveLater" Text="Save Execution Date" OnClick="btnSaveLater_Click" CssClass="btn btn-info" />


                                                                                    </div>

                                                                                </div>

                                                                            </div>
                                                                        </ContentTemplate>
                                                                    </asp:UpdatePanel>

                                                                    <%-- <div id="for_today">
                                                                        <br>
                                                                        <div class="certi_info bg-light">Digital Signature Certificates </div>

                                                                        <br>
                                                                        <div class="text-right">
                                                                            <asp:Button Text="Esign/DSC" ID="BtnFinalOrder" CssClass="btn btn-success" OnClick="BtnFinalOrder_Click" runat="server" />

                                                                        </div>


                                                                    </div>--%>

                                                                    <%-- <div id="s_for_later">
                                                                        <br>
                                                                        <div class="certi_info bg-light">Digital Signature Certificates </div>
                                                                        
                                                                        <div class="text-right">
                                                                            <asp:Button Text="DSC & Save" ID="Button4" CssClass="btn btn-success" OnClick="btnsave_dsc_Click" runat="server" />
                                                                        </div>
                                                                        <br>
                                                                        <br>
                                                                        
                                                                    </div>--%>


                                                                    <asp:Panel ID="pnlEsignDSC" Style="display: none" ClientIDMode="Static" runat="server">


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

                                                                                <asp:Button ID="btnEsignDSC" runat="server" OnClick="btnEsignDSC_Click" class="btn btn-success" Text="eSign/DSC" OnClientClick="return ValidateEsignRecord()" />
                                                                            </div>
                                                                        </div>
                                                                    </asp:Panel>

                                                                    <asp:Panel ID="PnlLaterEsign" Style="display: none" ClientIDMode="Static" runat="server">


                                                                        <div class="certi_info bg-light m16">eSign / DSC</div>
                                                                        <br />
                                                                        <div class="form-group row">
                                                                            <div class="col-sm-5">
                                                                                <asp:Label runat="server" Visible="false" ID="Label2" Text=""></asp:Label>
                                                                                <asp:DropDownList ID="ddlLaterEsign" runat="server" CssClass="form-control">
                                                                                    <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                                                                    <asp:ListItem Text="eSign (using Aadhaar)" Value="1"></asp:ListItem>
                                                                                    <asp:ListItem Text="DSC" Value="2"></asp:ListItem>

                                                                                </asp:DropDownList>
                                                                            </div>
                                                                            <div class="col-sm-5">
                                                                                <asp:TextBox ID="txtLaterAddhar" placeholder="Enter last 4 digit of Adhar Card" MaxLength="4" Enabled="true" onkeypress="return NumberOnly(event)" ClientIDMode="Static" CssClass="form-control" runat="server" AutoCompleteType="None" autocomplete="off"></asp:TextBox>
                                                                            </div>
                                                                            <div class="col-sm-2">

                                                                                <asp:Button ID="btnLaterEsign" runat="server" OnClick="btnLaterEsign_Click" class="btn btn-success" Text="eSign/DSC" OnClientClick="return ValidateLaterEsignRecord()" />
                                                                            </div>
                                                                        </div>
                                                                    </asp:Panel>


                                                                </div>
                                                            </div>

                                                            <div class="tab-pane fade" id="custom-tabs-one-edit_notice_1" role="tabpanel"
                                                                aria-labelledby="Proceeding">
                                                                <!-- code here -->
                                                                <div id="Ordersheet_1" class="mb-3">
                                                                    <%--<asp:DropDownList ID="DropDownList1" runat="server" OnChange="SetTemplate(this.value)" CssClass="form-control">
                                                                        <asp:ListItem Text="-Select Template-" Value="0"></asp:ListItem>
                                                                        <asp:ListItem Text="Template 1" Value="1"></asp:ListItem>
                                                                        <asp:ListItem Text="Template 2" Value="2"></asp:ListItem>
                                                                        <asp:ListItem Text="Template 3" Value="3"></asp:ListItem>
                                                                        <asp:ListItem Text="Template 4" Value="4"></asp:ListItem>
                                                                        <asp:ListItem Text="Template 5" Value="5"></asp:ListItem>
                                                                    </asp:DropDownList>--%>
                                                                    <asp:DropDownList ID="ddlTemplates2" runat="server" OnChange="SetTemplate(this.value)" CssClass="form-control" EnableTheming="false">
                                                                    </asp:DropDownList>
                                                                </div>
                                                                <div id="editproceeding_1">
                                                                    <textarea id="Textarea_2" runat="server" clientidmode="Static"></textarea>
                                                                    <div class="text-right">
                                                                        <a onclick="AddNotice2()" data-toggle="pill"
                                                                            href="#custom_tabs_one_Order_Sheet" aria-controls="custom-tabs-one-home" aria-selected="true" class="btn btn-success" role="tabpanel"
                                                                            aria-labelledby="Proceeding">Save</a>
                                                                        <%--<asp:Button ID="btnSubmit" runat="server" Text="Save" class="btn btn-success" OnClick="" ClientIDMode="static" OnClientClick="return ValidateRecord()" />--%>
                                                                    </div>
                                                                </div>

                                                            </div>





                                                        </div>
                                                    </asp:Panel>


                                                </div>
                                            </div>

                                        </div>



                                        <div class="col-12 col-sm-6">
                                            <div class="card card-primary card-tabs h-100">
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
                                                                <div class="card card-primary card-outline card-outline-tabs">
                                                                    <div class="card-header p-0 border-bottom-0 a-clr">
                                                                        <ul class="nav nav-tabs" id="custom-tabs-four-tab" role="tablist">
                                                                            <li class="nav-item">
                                                                                <a class="nav-link" id="custom-tabs-four-home-tab" onclick="ShowDocList()" data-toggle="pill" href="#custom-tabs-four-home" role="tab" aria-controls="custom-tabs-four-home" aria-selected="false">Index</a>
                                                                            </li>
                                                                            <li class="nav-item">
                                                                                <a class="nav-link active" id="custom-tabs-four-profile-tab" data-toggle="pill" href="#custom-tabs-four-profile" role="tab" aria-controls="custom-tabs-four-profile" aria-selected="false">Recent</a>
                                                                            </li>
                                                                            <li class="nav-item">
                                                                                <a class="nav-link" id="custom-tabs-four-messages-tab" data-toggle="pill" href="#custom-tabs-four-messages" role="tab" aria-controls="custom-tabs-four-messages" aria-selected="false">All</a>
                                                                            </li>
                                                                            <li class="nav-item">
                                                                                <a class="nav-link" id="custom-tabs-four-settings-tab" data-toggle="pill" href="#custom-tabs-four-settings" role="tab" aria-controls="custom-tabs-four-settings" aria-selected="true">Previous Proceeding</a>
                                                                            </li>
                                                                        </ul>
                                                                    </div>
                                                                    <div class="card-body">
                                                                        <div class="tab-content" id="custom-tabs-four-tabContent">
                                                                            <div class="tab-pane fade" id="custom-tabs-four-home" role="tabpanel" aria-labelledby="custom-tabs-four-home-tab">


                                                                                <h5 style="text-align: center">List of Documents </h5>
                                                                                <hr />
                                                                                <div id="List_pnl">
                                                                                    <asp:UpdatePanel runat="server">
                                                                                        <ContentTemplate>



                                                                                            <asp:GridView ID="grdSRDoc" CssClass="table table-bordered table-condensed table-striped table-hover" DataKeyNames="App_ID" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None" AutoGenerateColumns="false" Visible="true" EmptyDataText="No record found">
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
                                                                                                        <ItemTemplate><%#Eval("DocumentType") %> </ItemTemplate>
                                                                                                        <HeaderStyle Width="15%" HorizontalAlign="Center" />
                                                                                                        <ItemStyle Width="15%" HorizontalAlign="Center" />
                                                                                                    </asp:TemplateField>
                                                                                                    <%--
                                                                                                                <asp:TemplateField HeaderText="Document Name" SortExpression="DocName" HeaderStyle-Font-Bold="false">
                                                                                                                    <ItemTemplate><%#Eval("DOC_NAME") %> </ItemTemplate>
                                              <HeaderStyle Width="10%" HorizontalAlign="Center" />
                                              <ItemStyle Width="10%" HorizontalAlign="Center" />
                                              </asp:TemplateField>--%>
                                                                                                    <asp:TemplateField HeaderText="Uploaded By" SortExpression="DocName" HeaderStyle-Font-Bold="false">
                                                                                                        <ItemTemplate><%#Eval("UploadedBy") %> </ItemTemplate>
                                                                                                        <HeaderStyle Width="10%" HorizontalAlign="Center" />
                                                                                                        <ItemStyle Width="10%" HorizontalAlign="Center" />
                                                                                                    </asp:TemplateField>
                                                                                                    <asp:TemplateField HeaderText="Attached On" SortExpression="INSDATE" HeaderStyle-Font-Bold="false">
                                                                                                        <ItemTemplate><%-- <%#Eval("INSDATE") %>--%><%# ((DateTime)Eval("INSDATE")).ToString("dd-MM-yyyy") %> </ItemTemplate>
                                                                                                        <HeaderStyle Width="10%" HorizontalAlign="Center" />
                                                                                                        <ItemStyle Width="10%" HorizontalAlign="Center" />
                                                                                                    </asp:TemplateField>
                                                                                                    <%-- <asp:TemplateField HeaderText="Pages" HeaderStyle-Font-Bold="false">
                                                <HeaderStyle Width="10%" HorizontalAlign="Center" />
                                                <ItemStyle Width="10%" HorizontalAlign="Center" />
                                              </asp:TemplateField>--%>
                                                                                                    <asp:TemplateField HeaderText="View" HeaderStyle-Font-Bold="false">
                                                                                                        <ItemTemplate>
                                                                                                            <a href="#" class="gridViewToolTip" id="btnModalPopup" onclick='showPdfBYHendler("<%# Eval("FILE_PATH")%>","<%# Eval("docType")%>","<%# Eval("EREG_ID")%>")'>
                                                                                                                <button type="button" class="btn btn-info">
                                                                                                                    <i class="fas fa-eye"></i>
                                                                                                                </button>
                                                                                                            </a><%-- 
                                                                                                                        <a href="#" class="gridViewToolTip" id="btnModalPopup" onclick='openPopup("<%# Eval("FILE_PATH")%>")'> <button type="button" class="btn btn-info">
                                                    <i class="fas fa-eye"></i>
                                                  </button>
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

                                                                                        </ContentTemplate>

                                                                                    </asp:UpdatePanel>


                                                                                </div>
                                                                                <div id="View_pnl" style="display: none">



                                                                                    <iframe id="ifrDisplay" class="embed-responsive-item" height="750"></iframe>
                                                                                </div>

                                                                                <div id="View_pnlPartyReply" style="display: none">

                                                                                    <textarea id="txtReadOnly" runat="server" style="height: 67px; width: 602px;" adonly="readonly"></textarea>


                                                                                    <iframe id="ifrDisplayParty" class="embed-responsive-item" height="750"></iframe>
                                                                                </div>



                                                                                <asp:UpdatePanel runat="server">
                                                                                    <ContentTemplate>

                                                                                        <asp:Button ID="Attached" runat="server" Text="Attach" OnClientClick="Loader();" OnClick="Attached_Click" class="btn btn-success float-right" />
                                                                                        <asp:Panel ID="Panel2" Visible="false" runat="server">
                                                                                            <div class="card-body">
                                                                                                <div class="row" style="margin-top: 50px;">
                                                                                                    <div class="col-md-12" style="width: 100%; border: 1px solid #000;">
                                                                                                        <h5 class="new-heading">Documents</h5>
                                                                                                        <asp:FileUpload ID="CoSUpload_Doc" runat="server" />

                                                                                                        <asp:Button ID="Button3" runat="server" OnClientClick="return setActiveTab();" OnClick="Button3_Click" Text="Document Upload" class="btn btn-primary" />
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

                                                                            </div>
                                                                            <div class="tab-pane fade active show" id="custom-tabs-four-profile" role="tabpanel" aria-labelledby="custom-tabs-four-profile-tab">

                                                                                <iframe id="RecentdocPath" runat="server" height='750'></iframe>

                                                                            </div>
                                                                            <div class="tab-pane fade" id="custom-tabs-four-messages" role="tabpanel" aria-labelledby="custom-tabs-four-messages-tab">
                                                                                <iframe id="Iframe2" clientidmode="Static" runat="server" width='550' height='750'></iframe>
                                                                                <iframe id="ifProposal1" clientidmode="Static" runat="server" scrolling="auto" height='750'></iframe>
                                                                                <iframe id="RecentAttachedDoc" runat="server" width='550' height='750'></iframe>
                                                                                <iframe id="ifPDFViewerAll_Hearing" clientidmode="Static" runat="server" scrolling="auto" height='750'></iframe>

                                                                               

                                                                            </div>
                                                                            <div class="tab-pane fade" id="custom-tabs-four-settings" role="tabpanel" aria-labelledby="custom-tabs-four-settings-tab">

                                                                                <div>
                                                                                    <%--<img src="../dist/img/Doc_Sample_pic.jpg" style="width: 708px;" />--%>
                                                                                    <iframe id="Iframeprevious" runat="server" height='750'></iframe>
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
            <div class="modal fade" id="myModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <%-- <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>--%>
                        <div class="modal-header">

                            <h4 class="modal-title" id="myModalLabel" style="float: left">Document Download Option</h4>
                            <button type="button" data-dismiss="modal" style="float: right; font-size: x-large">x</button>

                        </div>
                        <div class="modal-body">
                            <div class="form-group">
                                <div class="custom-control custom-radio">

                                    <asp:RadioButton ID="chkRecentDoc" runat="server" ClientIDMode="Static" GroupName="customRadioHr" Text="Recent" />

                                </div>

                                <div class="custom-control custom-radio">
                                    <asp:RadioButton ID="chkAll" runat="server" ClientIDMode="Static" Checked="true" Text="All" GroupName="customRadioHr" />

                                </div>
                                <div class="custom-control custom-radio">
                                    <asp:RadioButton ID="chkAllOrderSheet" runat="server" ClientIDMode="Static" Checked="true" Text="Previous Proceeding" GroupName="customRadioHr" />

                                </div>
                            </div>

                        </div>
                        <div class="modal-footer">
                            <%--<asp:Button ID="btnDownload" runat="server" ClientIDMode="Static" class="btn btn-primary" OnClick="btnDownload_Click" Text="Submit" />--%>

                            <input type="button" value="Print" id="btnPrintSeletedPDF" class="btn btn-info" onclick="PrintSeletedPDF()" />

                        </div>
                    </div>
                </div>
            </div>

            <div class="modal fade" id="myModal_Downlaod" tabindex="-1" role="dialog" aria-labelledby="myModalLabel_Downlaod" aria-hidden="true">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <%-- <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>--%>
                        <div class="modal-header">

                            <h4 class="modal-title" id="myModalLabel_Downlaod" style="float: left">Document Download Option</h4>
                            <button type="button" data-dismiss="modal" style="float: right; font-size: x-large">x</button>

                        </div>
                        <div class="modal-body">
                            <div class="form-group">
                                <div class="custom-control custom-radio">

                                    <asp:RadioButton ID="chkRecentDocDnld" runat="server" ClientIDMode="Static" GroupName="customRadioHrDnld" Text="Recent" />

                                </div>

                                <div class="custom-control custom-radio">
                                    <asp:RadioButton ID="chkAllDnld" runat="server" ClientIDMode="Static" Checked="true" Text="All" GroupName="customRadioHrDnld" />

                                </div>
                                <div class="custom-control custom-radio">
                                    <asp:RadioButton ID="chkAllOrderSheetDnld" runat="server" ClientIDMode="Static" Checked="true" Text="Previous Proceeding" GroupName="customRadioHrDnld" />

                                </div>
                            </div>

                        </div>
                        <div class="modal-footer">
                            <asp:Button ID="Button1" runat="server" ClientIDMode="Static" class="btn btn-primary" OnClick="btnDownload_Click" Text="Download" />



                        </div>
                    </div>
                </div>
            </div>
        </div>

    </section>

    <!-- Modal -->
    <div class="modal fade" id="ModalCenter" tabindex="-1" role="dialog" aria-labelledby="exampleModalCenterTitle" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <div class="table-responsive listtabl">
                        <table id="example1" class="table table-bordered table-striped">
                            <thead>
                                <tr>
                                    <th>S No.</th>
                                    <th>Party Type</th>
                                    <th>Party Name</th>
                                    <th>Mobile no</th>
                                    <th>Verify </th>
                                </tr>
                            </thead>
                            <tbody>
                                <tr>
                                    <td>1</td>
                                    <td>Seller </td>
                                    <td>xyz</td>
                                    <td>1234567890 </td>
                                    <td>
                                        <div class="custom-control custom-checkbox">
                                            <input class="custom-control-input" type="checkbox" id="customCheckbox22" value="option1">
                                            <label for="customCheckbox22" class="custom-control-label">Select </label>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td>2</td>
                                    <td>Seller </td>
                                    <td>xyz</td>
                                    <td>1234567890 </td>
                                    <td>
                                        <div class="custom-control custom-checkbox">
                                            <input class="custom-control-input" type="checkbox" id="customCheckbox33" value="option1">
                                            <label for="customCheckbox33" class="custom-control-label">Select </label>
                                        </div>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </div>

                </div>
                <div class="modal-footer">
                    <%-- <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>--%>
                    <button type="button" class="btn btn-primary">Done</button>
                </div>
            </div>
        </div>
    </div>
    <asp:HiddenField ID="hdnUserID" runat="server" Value="0" />
    <asp:HiddenField ID="hdnProceeding" runat="server" ClientIDMode="Static" />
    <script>
        document.addEventListener("DOMContentLoaded", function () {
            // Your code here
        });
    </script>


    <script type="text/javascript">
        $(document).ready(function () {
            $("#<%= txtMobile.ClientID %>").on("input", function () {
                var inputValue = $(this).val();
                inputValue = inputValue.replace(/[^0-9]/g, ''); // Remove non-numeric characters
                if (inputValue.length > 10) {
                    inputValue = inputValue.substring(0, 10); // Limit to 10 digits
                }
                $(this).val(inputValue);
            });
        });
    </script>

    <script type="text/javascript">

        function setActiveTab() {

            $('#custom-tabs-four-profile-tab').removeClass('active');
            $('#custom-tabs-four-home-tab').addClass('active');

            return true; // Allow the server-side click event to proceed
        }

        function AddOrderSheet() {


            var HearingDate = $("#ContentPlaceHolder1_txtHearingDate").val();


            var last_HearingDt = document.getElementById('hdnHearingDt1').value;



            var parsedHearingDate = new Date(HearingDate.split('/').reverse().join('-'));
            var parsedLastHearingDt = new Date(last_HearingDt.split('/').reverse().join('-'));

            //alert(parsedHearingDate);
            //alert(parsedLastHearingDt);

            if (parsedHearingDate < parsedLastHearingDt) {
                Swal.fire({
                    icon: 'error',
                    title: 'Selected Date should greater than previous Hearing..!',
                    showCancelButton: false,
                    confirmButtonText: 'OK'

                });
                return false;
                document.getElementById('edit_notice').style.display = 'none';
            }

            else {
                if (HearingDate == "") {
                    Swal.fire({
                        icon: 'error',
                        title: 'Please Select Hearing Date',
                        showCancelButton: false,
                        confirmButtonText: 'OK'
                    });
                    return false;
                }
                else {
                    //alert("ssdsdfsd");
                    document.getElementById('S_Notice2').style.cssText = "display: block;";


                }
            }




        }

        function fnConfirmDelete() {

            Swal.fire({
                title: 'Are you sure?',
                text: "You won't be able to revert this!",
                icon: 'warning',
                showCancelButton: true,
                confirmButtonColor: '#3085d6',
                cancelButtonColor: '#d33',
                confirmButtonText: 'Yes, delete it!'
            }).then((result) => {
                if (result.isConfirmed) {
                    Swal.fire(
                        'Deleted!',
                        'Your file has been deleted.',
                        'success'
                    )
                    return true;
                }
                else {
                    return false;
                }
            })

        }

        function DoCUploadMsg() {

            Swal.fire({
                icon: 'success',
                title: 'Your file has been successfully uploaded to the server. Thank you',
                showCancelButton: false,
                confirmButtonText: 'OK'
            });
            return false;
        }

        function SetIndexVisibalTrue() {


            document.getElementById("custom-tabs-four-home-tab").ariaSelected = "true";
            document.getElementById("custom-tabs-four-home-tab").classList.add("active");
            document.getElementById("custom-tabs-four-home").ariaSelected = "true";
            document.getElementById("custom-tabs-four-home").classList.add("active");
            document.getElementById("custom-tabs-four-home").classList.add("show");
            document.getElementById("custom-tabs-four-profile-tab").ariaSelected = "false";
            document.getElementById("custom-tabs-four-profile-tab").classList.remove("active");
            document.getElementById("custom-tabs-four-profile").ariaSelected = "false";
            document.getElementById("custom-tabs-four-profile").classList.remove("active");
            document.getElementById("custom-tabs-four-profile").classList.remove("show");
        }




        function DocTypeErrorMsg() {

            Swal.fire({
                icon: 'error',
                title: 'Please selelct .doc, .docx, .pdf only  ..!',
                showCancelButton: false,
                confirmButtonText: 'OK'
            });
        }

        function NoFileMessage() {


            Swal.fire({
                icon: 'error',
                title: 'Sorry, Please attached file',
                showCancelButton: false,
                confirmButtonText: 'OK'
            });
        }


        function OrderSheetSave() {

            /*alert("fghj");*/
            Swal.fire({
                icon: 'success',
                title: 'Ordersheet Draft Saved successfully !',
                showCancelButton: false,
                confirmButtonText: 'OK',
            });
        }

        function UpdateVerifiedUser(memText) {
            //alert(memText);
            document.getElementById('lblVerifiedParty').innerHTML = memText;
        }
        function UpdateVerifiedUse_behalf(memText) {
            //alert(memText);
            document.getElementById('lblVerifiedParty1').innerHTML = memText;
        }


        function dasf() {

            $('#hearing').show();
        }

        function PartyPnl() {

            document.getElementById('PartyVerify_Pnl').style.display = 'block';




        }



        function AddNotice() {

            /*alert("Seta DaA");*/

            document.getElementById('edit_notice').style.display = 'block';
            document.getElementById("pContent").innerHTML = document.getElementById("summernote").value;

            $("#custom_tabs_one_Order_Sheet").addClass("active");
            $("#Order_Sheet").addClass("active");
            $("#Proceeding").addClass("active");
            $("#custom-tabs-one-Proceeding").removeClass("active");

        }


        function SaveProcedding() {
            var procedding = document.getElementById('hdnProceeding').value;

            document.getElementById("Textarea_2").value = procedding;


        }


        function ddd() {
            //debugger;

            var labelValue = document.getElementById('<%= lblTodayDetails.ClientID %>').innerText;

            //alert(labelValue);

            //alert(labelValue);

            /*var aaaaa = document.getElementById("lblTodayDetails").innerHTML;*/
            var pContentValue = document.getElementById("pContent").innerHTML;

            var lbltextElement = document.getElementById("lbltext");
            var lbltextValue = "";
            //var labelValue = "";


            //alert(lbltextElement);
            //debugger;
            if (lbltextElement !== null) {

                if (document.getElementById('today').checked) {
                    lbltextValue = lbltextElement.innerHTML;


                    var combinedValue = pContentValue + "\n" + lbltextValue + "\n" + "\n" + labelValue;

                    document.getElementById("Textarea_2").value = combinedValue;

                    document.getElementById("lbltext").innerHTML = "";
                    document.getElementById('<%= lblTodayDetails.ClientID %>').innerText = "";
                }

                else {
                    lbltextValue = lbltextElement.innerHTML;

                    var combinedValue = pContentValue + "\n" + lbltextValue;

                    document.getElementById("Textarea_2").value = combinedValue;

                    document.getElementById("lbltext").innerHTML = "";
                }





            } else {
                var SingleValue = pContentValue;
                document.getElementById("Textarea_2").value = SingleValue;
            }



            $(".note-editable.card-block").html(document.getElementById("Textarea_2").value);



        }

        function hide_x() {
            $('#editproceeding,#pnl_Ordersheet').hide();
            $('.showhide').hide();
        }





        function partyPnl_11() {
            /*alert("Party");*/



            //document.getElementById("pContent").innerHTML = document.getElementById("Textarea_2").value;
            $("#custom_tabs_one_Order_Sheet").addClass("active");
            $("#custom_tabs_one_Order_Sheet").addClass("show");
            $("#Proceeding").addClass("active");
            $("#custom-tabs-one-edit_notice_1").removeClass("active");
            $("#edit_notice").removeClass("active");
            $("#Order_Sheet").addClass("active");
            document.getElementById("btnDraftSave").style.display = "block";

            $("#pnl_Proceding").hide();
            $(".showhide").hide();
            $("#edit_notice").show();
            document.getElementById('editproceeding').style.display = 'none';

            $('input:radio[id=yes]').attr('checked', true);


            $('#ifselectyes').show();


            $('#ifselectyes_Template').hide();
            /*$('.showhide').hide();*/

        }

        function newshownhide() {


            $("#pnl_Proceding").hide();
            $(".showhide").hide();
            $("#edit_notice").show();
            document.getElementById('editproceeding').style.display = 'none';
            //  document.getElementById('edit_notice').style.display = 'block';
        }

        function AddNotice_new() {

            // document.getElementById("pContent").innerHTML = document.getElementById("Textarea_2").value;
            $("#custom_tabs_one_Order_Sheet").addClass("active");
            $("#custom_tabs_one_Order_Sheet").addClass("show");
            $("#Proceeding").addClass("active");
            $("#custom-tabs-one-edit_notice_1").removeClass("active");
            $("#edit_notice").removeClass("active");
            $("#Order_Sheet").addClass("active");
            document.getElementById("btnDraftSave").style.display = "block";

        }



        function AddNotice2() {
            document.getElementById("pContent").innerHTML = "";
            document.getElementById("pContent").innerHTML = document.getElementById("Textarea_2").value;





            $("#custom_tabs_one_Order_Sheet").addClass("active");

            $("#custom_tabs_one_Order_Sheet").addClass("show");


            $("#Proceeding").addClass("active");

            $("#custom-tabs-one-edit_notice_1").removeClass("active");
            $("#edit_notice").removeClass("active");

            $("#Order_Sheet").addClass("active");

            /*document.getElementById("btnDraftSave").style.display = "block";*/
            //showDraftSaveButton();
        }

        function showDraftSaveButton() {
            // Call server-side function to set the visibility of btnDraftSave to true
            // This function is defined in the code-behind file (C# or VB.NET)
            PageMethods.ShowDraftSaveButton(onSuccess, onError);
        }

        function onSuccess() {
            // This function is called if the server-side function is successful
        }

        function onError() {
            // This function is called if there's an error with the server-side function
        }


        function viewEsignDSC() {
            //alert("hello");
            document.getElementById("pnlEsignDSC").style.display = "block";
            //document.getElementById("BtnFinalOrder").style.display = "none";
        }
        function viewLaterEsignDSC() {
            //alert("hello");
            document.getElementById("PnlLaterEsign").style.display = "block";
            //document.getElementById("BtnFinalOrder").style.display = "none";
        }
        function SetHearingDate(ths) {

            //alert(ths);
            document.getElementById("lblHearingDt").value = "";
            document.getElementById("lblFurtherDt").value = "";

            var year = ths.value.split("-")[0];
            var Mon = ths.value.split("-")[1];
            var dat = ths.value.split("-")[2];

            var changeDate = (dat + '-' + Mon + '-' + year).toString();
            //alert(changeDate);
            $('#lblHearingDt').text(changeDate);
            $('#lblHearingDt1').text(changeDate);
            $('#lblFurtherDt').text(changeDate);
            document.getElementById("lblHearingDt").value = (dat + '/' + Mon + '/' + year);
            document.getElementById("lblHearingDt1").value = (dat + '/' + Mon + '/' + year);
            document.getElementById("lblFurtherDt").value = (dat + '/' + Mon + '/' + year);
            //document.getElementById("divDate").style.visibility = "visible";

        }

        function disableradioBtn() {
            var NoticeRadio = document.getElementById("Send_Notice");
            var FinalRadio = document.getElementById("Final_Order");

            if (NoticeRadio.checked) {
                NoticeRadio.disabled = true;
                FinalRadio.disabled = true;
            }
        }

        function DisableNoticeRbtn() {
            document.getElementById('S_Notice2').style.display = 'block';
            document.getElementById('edit_notice').style.display = 'none';
            document.getElementById('hearingdate').style.display = 'none';
            //document.getElementById('S_Notice2').style.display = "block";
            //document.getElementById('edit_notice').style.display = "none";
            //document.getElementById("hearingdate").style.display = "none";



            //var yesRadio = document.getElementById("Send_Notice");
            //var noRadio = document.getElementById("Final_Order");

            //if (yesRadio.checked) {
            //    yesRadio.disabled = true;
            //    noRadio.disabled = true;
            //}
            // Perform other form submission actions if needed



        }

        function S_Notice() {
            alert("S_Notice");
            document.getElementById('S_Notice2').style.display = "block";
            document.getElementById('edit_notice').style.display = "none";
        }

        function ShowMessage() {
            debugger;
            $('#hearingdate').show();
            $('#hearingdate').css('display', 'block');

            Swal.fire({
                icon: 'error',
                title: 'Please Select  Hearing Date',
                showCancelButton: false,
                confirmButtonText: 'OK'
            });
            return false;
        }

        function ShowMessageGreater() {

            Swal.fire({
                icon: 'error',
                title: 'Selected Date should greater than previous Hearing..!',
                showCancelButton: false,
                confirmButtonText: 'OK'
            });
            return false;

        }

        function ValidateRecord() {
            var HearingDate = $("#ContentPlaceHolder1_txtHearingDate").val();

            alert(HearingDate);

            if (HearingDate == "") {
                Swal.fire({
                    icon: 'success',
                    title: 'Please Select  Hearing Date',
                    showCancelButton: false,
                    confirmButtonText: 'OK'
                });
                return false;
            }

        }

        function SetHearingDateForNotice(ths) {
            //alert(ths);
            document.getElementById("lblHearingDt").value = "";

            var year = ths.value.split("-")[0];
            var Mon = ths.value.split("-")[1];
            var dat = ths.value.split("-")[2];

            var changeDate = (dat + '-' + Mon + '-' + year).toString();
            //alert(changeDate);
            $('#lblHearingDt').text(changeDate);
            document.getElementById("lblHearingDt").value = (dat + '/' + Mon + '/' + year);
            //document.getElementById("divDate").style.visibility = "visible";

        }


        //function openPopup() {         

        //    $("#pnl_Proceding").show();
        //    $("#pnl_Ordersheet").hide();
        //}


        $(document).ready(function () {
            if ($("#hdnbase64").val().length > 0) {
                console.log("hdnbase64_value" + $("#hdnbase64").val().length);
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
            var chkAll = $('.header').click(function () {
                //Check header and item's checboxes on click of header checkbox
                chkItem.prop('checked', $(this).is(':checked'));
            });
            var chkItem = $(".item").click(function () {
                //If any of the item's checkbox is unchecked then also uncheck header's checkbox
                chkAll.prop('checked', chkItem.filter(':not(:checked)').length == 0);
            });




        });


        function submitForm() {
            var yesRadio = document.getElementById("yes");
            var noRadio = document.getElementById("no");

            if (yesRadio.checked) {
                yesRadio.disabled = true;
                noRadio.disabled = true;
            }
            // Perform other form submission actions if needed
        }

        function ShowMessageOtpVerified() {
            //var case_number = document.getElementById('hdnfldCase').value;

            //alert(case_number);
            Swal.fire({
                icon: 'success',
                title: 'OTP VERIFIED !',
                showCancelButton: false,
                confirmButtonText: 'OK'
            });
        }

        function ShowMessageNotVerified() {

            Swal.fire({
                icon: 'error',
                title: 'OTP NOT VERIFIED !',
                showCancelButton: false,
                confirmButtonText: 'OK'
            });
        }

        function test() {
            alert("OTP NOT COREECT")
        }

        function PartyAlert() {
            Swal.fire({
                icon: 'error',
                title: 'Please Enter Party Details',
                showCancelButton: false,
                confirmButtonText: 'OK'
            });
            return false;
        }


        function Loader() {
            AmagiLoader.show();
        }
        function Loaderstop() {
            AmagiLoader.hide();
        }





    </script>



    <script type="text/javascript">
        window.onload = function () {
            var label = document.getElementById('<%= lblMessage.ClientID %>');

            label.style.display = 'block';

            setTimeout(function () {
                label.style.display = 'none';
            }, 5000); // 5000 milliseconds = 5 seconds
        };
    </script>


    <script type="text/javascript">



        $("#btnAddParty111").click(function () {
            // alert("sdad");
        });


        $(".otpvalidate").click(function () {
            //alert("12222");
            //HideLabel();
        });


        function HideLabel() {
            var seconds = 5;
         <%--   setTimeout(function () {
                document.getElementById("<%=lblMessage.ClientID %>").style.display = "none";
            }, seconds * 1000);--%>


            setTimeout(function () {
                //alert('hi')
            }, 2000);
        };
    </script>

    <script type="text/javascript">
        function showAlert() {

            /*alert("1111");*/
            $('#hearing').hide();
            $('#OrdersheetDiv').show();
            //$('#DateDiv').hide();
            $('#newhearing').show();
            //document.getElementById("custom-tabs-one-tabContent2").style.display = "block";
            //document.getElementById('custom-tabs-one-tabContent2').style.cssText = "display: block;";
        }
        function TempShow() {
            $('#ifselectyes_Template').show();
            $('#ifAdditionalParties').hide();
        }
        function disableCheckbox() {
            document.getElementById('<%= chkContent.ClientID %>').disabled = false;
            return true;
        }
    </script>


    <script type="text/javascript">
        function SetTemplate(val) {
            //alert(val);
            //var TemplateType = '{"TemId":"' + val+'"}';
            $.ajax({
                type: "POST",

                url: '<%=Page.ResolveClientUrl("~/CoS/Hearing.aspx/GetTemplate_Notice") %>',
                data: '{"TemId":' + val + '}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    setTemlateVal(msg.d);
                },
                error: function (e) {
                    console.log(e);
                    //LoadNotice();
                }
            });
        }
        function setTemlateVal(d) {
            $(".note-editable.card-block").html(d);  //show for editore
            //alert(d);
            document.getElementById("summernote").value = d; // set value
            //  alert(document.getElementById("summernote  .note-editing-area p").value);


        }
    </script>
    <script type="text/javascript">

        function SubmitLoader() {

            AmagiLoader.show();

        }
        function ShowOption() {
            //alert("Hello");
            $('#myModal').modal('show');
        }

        function ShowDownloadOption() {
            //alert("Hello");
            $('#myModal_Downlaod').modal('show');
        }

        function OnCalendarSelectionChanged(sender, args) {
            //alert('hi');
            // Get the reference to your div element
            var divElement = document.getElementById("DateDiv");
            var hearingdatediv = document.getElementById('hearingdate');
            // Check if the div is not null
            if (hearingdatediv) {

                hearingdatediv.style.display = "block";
                $('#DateDiv').css('display', 'block');
                $('#DateDiv input').css('display', 'block');
                $('#hearingdate').css('display', 'block');
                $('.calendar-section').css('display', 'none');
                document.getElementById('hearingdate').style.display = "block";
                if ($("#Send_Notice").prop("checked", true)) {
                    $('#hearingdate').show();
                    $('#hearingdate').css('display', 'block');
                }
            }
        }


        function printPDF() {

            //debugger;
            //document.getElementById('btnDownload').style.visibility = 'hidden';
            //document.getElementById('btnPrintSeletedPDF').style.visibility = 'visible';
            ShowOption();
        }
        function PrintSeletedPDF() {

            if (document.getElementById('chkRecentDoc').checked) {
                //alert("chkRecentDoc");
                var frameRecent = document.getElementById('RecentdocPath');
                frameRecent.contentWindow.focus();
                frameRecent.contentWindow.print();
            }
            else if (document.getElementById('chkAll').checked) {
                var frameAll = document.getElementById('ifPDFViewerAll_Hearing');
                frameAll.contentWindow.focus();
                frameAll.contentWindow.print();
            }
            else if (document.getElementById('chkAllOrderSheet').checked) {
                var frameAllOrder = document.getElementById('Iframeprevious');
                frameAllOrder.contentWindow.focus();
                frameAllOrder.contentWindow.print();
            }


        }
        function generateCalenderByCode() {

            document.getElementById('ContentPlaceHolder1_divCalender').style.display = "block";
            document.getElementById('ContentPlaceHolder1_txtHearingDate').style.display = "block";
            document.getElementById('hearingdate').style.display = "block";
            document.getElementByClass('hearingdate').style.display = "block";
            if ($("#Send_Notice").prop("checked", true)) {
                $('#hearingdate').show();
                $('#hearingdate').css('display', 'block');
            }
        }



    </script>


    <script>
        function pageLoad(sender, args) {
            if (args.get_isPartialLoad()) {
                var seconds = 10; // Set the countdown duration in seconds

                function startCountdown() {



                    var countdownElement = document.getElementById("countdown");
                    /*alert(countdownElement);*/
                    var resendButton = document.getElementById("<%= ResendButton.ClientID %>");
                    var timer = setInterval(function () {
                        countdownElement.innerHTML = "Resend OTP : " + seconds + " seconds";

                        if (seconds <= 0) {
                            clearInterval(timer);
                            countdownElement.innerHTML = "";
                            resendButton.style.display = "block"; // Show the Resend button
                        } else {
                            seconds--;
                        }
                    }, 1000); // Update every 1 second
                }

                function resendOTP() {
                    // Logic to resend OTP (can be an AJAX call to the server)
                    // For demonstration purposes, you can reset the timer here
                    seconds = 10;
                    document.getElementById("countdown").innerHTML = "Time left: " + seconds + " seconds";
                    document.getElementById("<%= ResendButton.ClientID %>").style.display = "none"; // Hide the Resend button again
            <%--document.getElementById("<%= ResendButton.ClientID %>").style.disabled = false; --%>

                }

                startCountdown(); // Start the countdown when the page loads
            }
        }


    </script>

    <script>
        function isNumber(evt) {
            evt = (evt) ? evt : window.event;
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                return false;
            }
            return true;
        }

    </script>
    <script type="text/javascript">
        function disableRadioButtons() {
            // Disable radio buttons by their IDs
            document.getElementById("yes").disabled = true;
            document.getElementById("no").disabled = true;
        }




        //function test111() {
        //    document.getElementById('edit_notice').style.display = 'block';
        //    document.getElementById("pContent").innerHTML = document.getElementById("summernote").value;
        //    alert('JJJ');
        //    $('.displaydiv').hide();
        //    $('#pnl_Proceding').hide();

        //    $("#add_green_proceeding").hide();
        //    $('#editproceeding,#pnl_Ordersheet').show();

        //}
    </script>


    <script>
        //function fun1() {
        //    alert("fun1");
        //    $('#editproceeding,#pnl_Ordersheet').hide(); $('#ifselectyes').hide();
        //    $('#ifselectyes1').hide();
        //    $("#add_green_proceeding").click(function () {
        //        $('.displaydiv').hide();
        //        $("#add_green_proceeding").hide();
        //        $('#editproceeding,#pnl_Ordersheet').show();
        //    });

        //    $("#edit_notice").click(function () {
        //        //alert("aaaaa");
        //        //$('.displaydiv').hide();
        //        //$("#add_green_proceeding,#PnlHearing_P1").hide();
        //        // $('#editproceeding,#pnl_Ordersheet').show();
        //    });
        //}


        function fun2() {
            //alert("fun2");

            $('#add_green_proceeding').hide();
            $('.greenbox').hide();

            $('#pnl_Ordersheet').hide();

            $('#ifselectyes1').show();

            AddNotice();

        }




    </script>
    <script src="../dist/plugins/jquery/jquery.min.js"></script>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.7.2/jquery.min.js"></script>
    <script src="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.8.9/jquery-ui.js" type="text/javascript"></script>
    <link href="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.8.9/themes/start/jquery-ui.css"
        rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        //$("[id*=btnModalPopup]").live("click", function () {
        //    $("#modal_dialog").dialog({
        //        title: "jQuery Modal Dialog Popup",
        //        buttons: {
        //            Close: function () {
        //                $(this).dialog('close');
        //            }
        //        },
        //        modal: true
        //    });
        //    return false;
        //});

        function showPdfBYHendler(PROPOSALPATH_FIRSTFORMATE, docType, EREG_ID) {


            $("#List_pnl").hide();
            $("#View_pnl").show();

            var Tocan = document.getElementById("hdTocan").value;

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





    <div id="modal_dialog" style="display: none"></div>


</asp:Content>
