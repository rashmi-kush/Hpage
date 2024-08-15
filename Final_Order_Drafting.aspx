<%@ Page Title="" Language="C#" MasterPageFile="~/CoS/CoSMaster.Master" ValidateRequest="false" AutoEventWireup="true" CodeBehind="Final_Order_Drafting.aspx.cs" Inherits="CMS_Sampada.CoS.Final_Order_Drafting" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="EngToHin_Js/google_jsapi.js" type="text/javascript"></script>
    <script src="EngToHin_Js/jquery.min.js" type="text/javascript"></script>

    <link href="../Styles/sweetalert.css" rel="stylesheet" />
    <script src="../Scripts/sweetalert.min.js"></script>
    <script src="../assets/js/3.3.1/sweetalert2@11.js"></script>
    <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.7/jquery.min.js" type="text/javascript"></script>
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <%--<script type="text/javascript">
    $(function () {
        $("#btnDraft").click(function () {
            //Set the Valid Flag to True if one RadioButton from the Group of RadioButtons is checked.
            var isValid = $("input[name=Fruit]").is(":checked");

            //Display error message if no RadioButton is checked.
            $("#spnError")[0].style.display = isValid ? "none" : "block";
        });
    });
</script>--%>
    <style>
        .htmldoc h6 {
            font-weight: bold;
        }
    </style>

    <script type="text/javascript">
        function AddNotice() {

            document.getElementById("pContent").innerHTML = document.getElementById("summernote").value;
            /* document.getElementById("lblSRProposal").innerHTML = "";*/

            document.getElementById("pContent1").innerHTML = document.getElementById("txtSRProposal").value;
            document.getElementById("pContent2").innerHTML = document.getElementById("txtCOSDecision").value;
            document.getElementById("pContent3").innerHTML = document.getElementById("txtFinalDecision").value;

            $("#custom-tabs-one-ProposalForm").addClass("active");
            $("#custom-tabs-one-edit_notice").removeClass("active");
            $("#custom-tabs-one-profile-tab").removeClass("active");
            $("#custom-tabs-one-createNotice").addClass("active");




        }
    </script>

    <script type="text/javascript">
        function ValidateDecision() {


            //Referencing and fetching the TextBox values.onclick="ValidateValidateDecision()"
            var SRProposal = document.getElementById("summernote").value;
            var PartyReply = document.getElementById("txtSRProposal").value;
            var Nishkarsh = document.getElementById("txtCOSDecision").value;
            var AntimTipaddi = document.getElementById("txtFinalDecision").value;
            //var ValuationDecision = document.getElementById("rdbtnReportYes").value;
            //var ValuationDecision = document.getElementById("rdbtnReportNo").value;
            //alert(!document.getElementById('rdbtnReportYes').checked);
            //alert(!document.getElementById('rdbtnReportNo').checked);


            if (SRProposal == "") {
                //alert("Please select Hearing date");
                Swal.fire({
                    icon: 'info',
                    title: 'Please Enter Prastav !!!'

                })
                return false;
            }
            if (PartyReply == "") {
                //alert("Please select Hearing date");
                Swal.fire({
                    icon: 'info',
                    title: 'Please Enter Party Jabab !!!'

                })
                return false;
            }
            if (Nishkarsh == "") {
                //alert("Please select Hearing date");
                Swal.fire({
                    icon: 'info',
                    title: 'Please Enter Nishkarsh !!!'

                })
                return false;
            }
            if (AntimTipaddi == "") {
                //alert("Please select Hearing date");
                Swal.fire({
                    icon: 'info',
                    title: 'Please Enter Antim Tippani !!!'

                })
                return false;
            }
            if ((!document.getElementById('rdbtnReportYes').checked) && (!document.getElementById('rdbtnReportNo').checked)) {
                //alert("helo");
                Swal.fire({
                    icon: 'info',
                    title: 'संपत्ति के मूल्यांकन के लिए "हां" या "नहीं" सिलेक्ट करें!'

                })
                return false;
            }
           



        }
    </script>
    <script type="text/javascript">

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

        function SetSignOption(val) {
            //alert(val);
            if (val == 1) {
                document.getElementById("TxtLast4Digit").style.display = "Block";
                document.getElementById("ddleAuthMode").style.display = "Block";
            }
            else if (val == 2) {
                document.getElementById("TxtLast4Digit").style.display = "None";
                document.getElementById("ddleAuthMode").style.display = "None";
            }
            else if (val == 3) {
                document.getElementById("TxtLast4Digit").style.display = "Block";
                document.getElementById("ddleAuthMode").style.display = "Block";
            }
        }
    </script>
    <script type="text/javascript">

        function ShowMessage() {
            //debugger;

            Swal.fire({
                icon: 'success',
                title: 'Create OrderSheet Successfully',
                showCancelButton: false,
                confirmButtonText: 'OK',
            }).then((result) => {
                if (result.value) {
                    window.location = '';
                }
                else {
                    alert('user click on Cancel button');
                }
            });
        }
        function ShowMessageNotVerified() {



            Swal.fire({

                icon: 'error',
                title: 'Please Select Details !',
                showCancelButton: false,
                confirmButtonText: 'OK',





            });
        }


        function FinalOrderSend() {


            Swal.fire({
                icon: 'success',
                title: 'Final Order Send To Party Successfully ! ',
                showCancelButton: false,
                confirmButtonText: 'OK',
            }).then((result) => {
                if (result.value) {
                    AmagiLoader.show();
                    window.location = 'Cases_In_Hearing.aspx';
                }
                else {
                    AmagiLoader.show();
                    window.location = 'Cases_In_Hearing.aspx';
                }
            });

        }

        function ShowMessageDSC(loc) {

            //alert("hello");
            Swal.fire({
                icon: 'success',
                title: 'Signed final order saved successfully',
                showCancelButton: false,
                confirmButtonText: 'OK',
            }).then((result) => {
                if (result.value) {
                    window.location = loc;
                }
                else {
                    alert('user click on cancel button');
                }
            });
        }

        function ShowErrorMessageDSC(msg) {

            //alert("hello");
            Swal.fire({
                icon: 'info',
                title: 'Something went wrong, Please try again...',
                showCancelButton: false,
                confirmButtonText: 'OK',
            }).then((result) => {
                if (result.value) {
                    window.location = loc;
                }
                else {
                    alert('user click on cancel button');
                }
            });
        }

        function ValidateEsignRecord() {
            //debugger;
            var SignOption = $("#ContentPlaceHolder1_ddl_SignOption").val();

            if (SignOption == "0") {

                Swal.fire({
                    icon: 'info',
                    title: 'Please select eSign or DSC in dropdown'

                })
                return false;
            }
            else if (((SignOption == "1") || (SignOption == "3")) && document.getElementById("TxtLast4Digit").value == "") {

                Swal.fire({
                    icon: 'info',
                    title: 'Please enter last 4 digit of Adhar Card'

                })
                return false;

            }


        }
    </script>

    <script type="text/javascript">
        //......................................Start Google Translate...............................//
        google.load("elements", "1",
            {
                packages: "transliteration"
            });

        function onLoad() {
            var options = {
                sourceLanguage:
                    google.elements.transliteration.LanguageCode.ENGLISH,
                destinationLanguage:
                    [google.elements.transliteration.LanguageCode.HINDI],
                shortcutKey: 'ctrl+g',
                transliterationEnabled: true
            };

            // Create an instance on TransliterationControl with the required 
            // options. 
            var control = new google.elements.transliteration.TransliterationControl(options);

            // Enable transliteration in the textbox with id 
            // 'transliterateTextarea'.
            //   alert(#txtBenNameHin);

            control.makeTransliteratable(['txtTran']);
            control.makeTransliteratable(['txtAddress']);
            control.makeTransliteratable(['summernote']);
            control.c.qc.t13n.c[3].c.d.keyup[0].ia.F.p = 'https://www.google.com';
        }
        google.setOnLoadCallback(onLoad);

        // here we make the handlers for after the UpdatePanel update
        // var prm = Sys.WebForms.PageRequestManager.getInstance();
        // prm.add_initializeRequest(InitializeRequest);
        //  prm.add_endRequest(EndRequest);

        function InitializeRequest(sender, args) {
        }

        // this is called to re-init the google after update panel updates.
        function EndRequest(sender, args) {
            onLoad();
        }
        //......................................End Google Translate...............................//     //......................................Start Google Translate...............................//
        google.load("elements", "1",
            {
                packages: "transliteration"
            });

        function onLoad() {
            var options = {
                sourceLanguage:
                    google.elements.transliteration.LanguageCode.ENGLISH,
                destinationLanguage:
                    [google.elements.transliteration.LanguageCode.HINDI],
                shortcutKey: 'ctrl+g',
                transliterationEnabled: true
            };

            // Create an instance on TransliterationControl with the required 
            // options. 
            var control = new google.elements.transliteration.TransliterationControl(options);

            // Enable transliteration in the textbox with id 
            // 'transliterateTextarea'.
            //   alert(#txtBenNameHin);

            control.makeTransliteratable(['txtTran']);
            control.makeTransliteratable(['txtAddress']);
            control.makeTransliteratable(['summernote']);
            control.c.qc.t13n.c[3].c.d.keyup[0].ia.F.p = 'https://www.google.com';
        }
        google.setOnLoadCallback(onLoad);

        // here we make the handlers for after the UpdatePanel update
        // var prm = Sys.WebForms.PageRequestManager.getInstance();
        // prm.add_initializeRequest(InitializeRequest);
        //  prm.add_endRequest(EndRequest);

        function InitializeRequest(sender, args) {
        }

        // this is called to re-init the google after update panel updates.
        function EndRequest(sender, args) {
            onLoad();
        }
        //......................................End Google Translate...............................//
    </script>
    <script>
        function valid() {

            // alert("h");
            var p = /^\d{10}$/;
            var val = document.getElementById('txtMobile');
            if (val.value.length != 10) {
                alert("Plz Enter 10 Digit Only!!");
            }
            else {
                if (val.value.match(p)) {
                    alert("Submitted Successfully..");
                }
                else {
                    alert("Wrong format");
                }
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="SCMNG" runat="server"></asp:ScriptManager>

    <section class="content-header headercls">
        <div class="container-fluid">
            <div class="row">
                <div class="col-sm-3">
                    <h5>Final Order </h5>
                </div>
            </div>
        </div>
    </section>

    <section class="content-header">
        <div class="container-fluid">
            <div class="row">
                <div class="col-sm-3">
                    <h6>Proposal No -<asp:Label ID="lblProposalIdHeading" Text="" runat="server"></asp:Label></h6>
                </div>
                <div class="col-sm-3">
                    <h6>Case No -
                        <asp:Label ID="lblCase_Number" Text="" runat="server"></asp:Label>
                    </h6>
                </div>
                <div class="col-sm-3">
                    <h6>Case Registered Date -
                        <asp:Label ID="lblRegisteredDate" Text="" runat="server"></asp:Label></h6>
                </div>
                <div class="col-sm-3">
                    <h6>Case Hearing Date - 
                        <asp:Label ID="lblHearingDt" Text="" runat="server"></asp:Label>
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
                                <li class="nav-item"><a class="nav-link disabled" href="#">Seek Report</a></li>
                                 <li class="nav-item"><a class="nav-link disabled" href="#">Notice Proceeding</a></li>
                                <li class="nav-item"><a class="nav-link disabled" href="#">Hearing</a></li>
                                <li class="nav-item"><a class="nav-link active disabled" href="#">Final Order</a>
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
                                                            <a class="nav-link active" id="Draft_Order" data-toggle="pill"
                                                                href="#custom-tabs-one-Draft_Order" role="tab"
                                                                aria-controls="custom-tabs-one-Draft_Order" aria-selected="true">Draft Final Order</a>
                                                        </li>
                                                        <li class="nav-item">
                                                            <a class="nav-link" id="Edit_Final_Order" data-toggle="pill"
                                                                href="#custom-tabs-one-editFinalOrder" role="tab" runat="server"
                                                                aria-controls="custom-tabs-one-editFinalOrder" aria-selected="true">Prastav</a>
                                                        </li>
                                                        <li class="nav-item">
                                                            <a class="nav-link" id="Edit_Party_Reply" data-toggle="pill"
                                                                href="#custom-tabs-one-PartyReply" role="tab" runat="server"
                                                                aria-controls="custom-tabs-one-editPartyReply" aria-selected="true">Party Jabab</a>
                                                        </li>
                                                        <li class="nav-item">
                                                            <a class="nav-link" id="Edit_COS_Decision" data-toggle="pill"
                                                                href="#custom-tabs-one-editCOSDecision" role="tab" runat="server"
                                                                aria-controls="custom-tabs-one-editCOS_Decision" aria-selected="true">Niskarsh/Vivechana</a>
                                                        </li>
                                                        <li class="nav-item">
                                                            <a class="nav-link" id="Edit_Final_Decision" data-toggle="pill"
                                                                href="#custom-tabs-one-editFinalDecision" role="tab" runat="server"
                                                                aria-controls="custom-tabs-one-editFinalDecision" aria-selected="true">Antim Tippani</a>
                                                        </li>

                                                    </ul>
                                                </div>

                                                <div class="card-body">

                                                    <div class="tab-content" id="custom-tabs-one-tabContent">
                                                        <div class="tab-pane active show" id="custom-tabs-one-Draft_Order" role="tabpanel"
                                                            aria-labelledby="Proceeding">
                                                            <!-- code here -->

                                                            <%--<div class="">
                                                            <button type="button" class="btn btn-success" id="add_green_proceeding"> Edit Final Order1</button>
                                                              </div>--%>

                                                            <div class="main-box htmldoc" id="htmldoc" style="width: 100%; margin: 0 auto; text-align: center; border: 1px solid #ccc; padding: 30px 30px 30px 30px; margin-top: 5px;">
                                                                <div>
                                                                    <h2 style="font-size: 18px; margin: 0; font-weight: 600;">न्यायालय कलेक्टर ऑफ़ स्टाम्प्स, 
                                                                                <asp:Label ID="lblHeadingDist" runat="server"></asp:Label>
                                                                        (म.प्र.)</h2>

                                                                    <h2 style="margin: 0; margin: 10px; font-size: 16px;">
                                                                        <asp:Label ID="lbltext" runat="server"></asp:Label></h2>
                                                                    <h3 style="margin: 0; margin: 10px; font-size: 16px;">
                                                                        <asp:Label ID="lblHeadingDistAdd" runat="server"></asp:Label>
                                                                    </h3>
                                                                    <h4 style="margin: 0; margin: 10px; font-size: 13px;">
                                                                        <asp:Label ID="lblEmail" Text="" runat="server"></asp:Label></h4>

                                                                    <h2 style="margin: 0; margin: 10px; font-size: 16px;">आदेश </h2>
                                                                    <div>
                                                                        <h3 style="margin: 0; margin: 10px; font-size: 16px;">मध्य प्रदेश शासन</h3>
                                                                        <h2 style="margin: 0; margin: 10px; font-size: 16px;">विरुद्ध</h2>
                                                                        <asp:GridView ID="grdNoticeParty" CssClass="table table-condensed table-hover dastavej" DataKeyNames="Party_ID"
                                                                            runat="server" CellPadding="4" BorderStyle="Solid" BorderColor="Black" BorderWidth="1px" AutoGenerateColumns="false" Visible="true" EmptyDataText="No record found">
                                                                            <AlternatingRowStyle BackColor="White" />
                                                                            <Columns>
                                                                                <asp:TemplateField HeaderText="क्रमांक " HeaderStyle-VerticalAlign="Top" HeaderStyle-HorizontalAlign="Center">
                                                                                    <ItemTemplate>
                                                                                        <itemtemplate><%#Container.DataItemIndex+1  %> </itemtemplate>
                                                                                    </ItemTemplate>
                                                                                    <ItemStyle Width="2%" HorizontalAlign="Center" />
                                                                                    <HeaderStyle Width="2%" HorizontalAlign="Center" />
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="नाम">
                                                                                    <ItemTemplate>
                                                                                        <%#Eval("Name_Hi") %>
                                                                                    </ItemTemplate>
                                                                                    <HeaderStyle Width="10%" HorizontalAlign="Center" />
                                                                                    <ItemStyle Width="10%" HorizontalAlign="Center" />
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="पता">
                                                                                    <ItemTemplate>
                                                                                        <%#Eval("Address_Hi") %>
                                                                                    </ItemTemplate>
                                                                                    <HeaderStyle Width="10%" HorizontalAlign="Center" />
                                                                                    <ItemStyle Width="10%" HorizontalAlign="Center" />
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="पिता / पति">
                                                                                    <ItemTemplate>
                                                                                        <%#Eval("Father / Husband Name Hindi") %>
                                                                                    </ItemTemplate>
                                                                                    <HeaderStyle Width="10%" HorizontalAlign="Center" />
                                                                                    <ItemStyle Width="10%" HorizontalAlign="Center" />
                                                                                </asp:TemplateField>


                                                                            </Columns>
                                                                            <%--  <EditRowStyle BackColor="#999999" />--%>
                                                                            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                                                            <HeaderStyle ForeColor="black" Font-Size="14px" />
                                                                            <%-- <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                                                                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />--%>
                                                                            <%--  <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                                                                    <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                                                                    <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                                                                    <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                                                                    <SortedDescendingHeaderStyle BackColor="#6F8DAE" />--%>
                                                                        </asp:GridView>
                                                                        <%--<h2 style="margin: 0; margin: 10px; font-size: 16px;">पक्षकार : Sub Registrar</h2>--%>
                                                                    </div>
                                                                    <br>

                                                                    <div style="display: flex;">
                                                                        <h6 style="text-align: justify; width: 40%; font-size: 15px;">प्रकरण की संख्या: </h6>
                                                                        <h6 style="text-align: left; width: 60%; font-size: 15px;">
                                                                            <asp:Label ID="lblCaseNo" runat="server"></asp:Label>
                                                                        </h6>
                                                                    </div>
                                                                    <div style="display: flex;">
                                                                        <h6 style="text-align: justify; width: 40%; font-size: 15px;">प्रकरण का स्रोत: </h6>
                                                                        <h6 style="text-align: left; width: 60%; font-size: 15px;">
                                                                            <asp:Label ID="lblSource" runat="server"></asp:Label>
                                                                        </h6>
                                                                    </div>
                                                                    <div style="display: flex;">
                                                                        <h6 style="text-align: justify; width: 40%; font-size: 15px;">प्रकरण का प्रकार : </h6>
                                                                        <h6 style="text-align: left; width: 60%; font-size: 15px;">
                                                                            <asp:Label ID="lblTypeCase" runat="server"></asp:Label></h6>
                                                                    </div>
                                                                    <div style="display: flex;">
                                                                        <h6 style="text-align: justify; width: 40%; font-size: 15px;">दस्तावेज़ का प्रकार : </h6>
                                                                        <h6 style="text-align: left; width: 60%; font-size: 15px;">
                                                                            <asp:Label ID="lblTypeDoc" runat="server"></asp:Label>
                                                                        </h6>
                                                                    </div>
                                                                    <div style="display: flex;">
                                                                        <h6 style="text-align: justify; width: 40%; font-size: 15px;">निष्पादन की तारीख : </h6>
                                                                        <h6 style="text-align: left; width: 60%; font-size: 15px;">
                                                                            <asp:Label ID="lblExeDt" runat="server"></asp:Label>
                                                                        </h6>
                                                                    </div>
                                                                    <div style="display: flex;">
                                                                        <h6 style="text-align: justify; width: 40%; font-size: 15px;">प्रस्तुति / पंजीकरण की तिथि : </h6>
                                                                        <h6 style="text-align: left; width: 60%; font-size: 15px;">
                                                                            <asp:Label ID="lblDtReg" runat="server"></asp:Label>
                                                                        </h6>
                                                                    </div>
                                                                    <div style="display: flex;">
                                                                        <h6 style="text-align: justify; width: 40%; font-size: 15px;"><b>प्रकरण का संक्षिप्त विवरण :</b> </h6>

                                                                    </div>

                                                                    <div style="padding-top: 5px; padding-bottom: 25px">
                                                                        <p style="text-align: justify;">
                                                                            <asp:Label ID="lblOrderProceeding" runat="server"></asp:Label>
                                                                        </p>

                                                                        <%-- <textarea > उप पंजीयक भोपाल-2 द्वारा एक पंजीकृत दस्तावेज दान पत्र विलेख क्रमांक दिनांक 25-05-2023 को न्यून मुद्रांकित मानते हुए उक्त दस्तावेज पर मुद्रा एवं पंजीयन शुल्क वसूली हेतु भेजा गया है। उप पंजीयक द्वारा दस्तावेज की मूल प्रति प्रेषित की गई है जिसे भारतीय स्टाम्प अधिनियम 1899 की धारा 33 के अंतर्गत दर्ज किया गया। </textarea>--%>
                                                                        <b>
                                                                            <h6 style="text-align: justify; width: 100%; font-size: 15px;"><b>उपपंजीयक/लोकाधिकारी का प्रस्ताव:</b></h6>
                                                                        </b>
                                                                        <p id="pContent" style="text-align: justify">
                                                                        </p>

                                                                        <asp:Label ID="lblSRProposal" runat="server" ClientIDMode="Static" Visible="false"></asp:Label>


                                                                    </div>

                                                                    <%-- <table style="width: 100%; border: 1px solid black; border-collapse: collapse;">
                                                                        <tr style="background: #e9e9e9;">
                                                                            <th style="border: 1px solid black; border-collapse: collapse; line-height: 20px; padding: 5px; font-size: 14px;">सीरियल नंबर</th>
                                                                            <th style="border: 1px solid black; border-collapse: collapse; line-height: 20px; padding: 5px; font-size: 14px;">दस्तावेज़ का विवरण</th>
                                                                            <th style="border: 1px solid black; border-collapse: collapse; line-height: 20px; padding: 5px; font-size: 14px;">पार्टियों के अनुसार दस्तावेज/ दस्तावेजों का विवरण</th>
                                                                            <th style="border: 1px solid black; border-collapse: collapse; line-height: 20px; padding: 5px; font-size: 14px;">एसआरपीओ का प्रस्ताव</th>
                                                                            <th style="border: 1px solid black; border-collapse: collapse; line-height: 20px; padding: 5px; font-size: 14px;">मूल्य का अंतर</th>
                                                                            <th style="border: 1px solid black; border-collapse: collapse; line-height: 20px; padding: 5px; font-size: 14px;">रिमार्क</th>
                                                                        </tr>

                                                                        <tr>
                                                                            <td style="border: 1px solid black; border-collapse: collapse; padding: 5px; font-size: 14px;">1</td>
                                                                            <td style="border: 1px solid black; border-collapse: collapse; padding: 5px; font-size: 14px;">दस्तावेज़ का प्रकार</td>
                                                                            <td style="border: 1px solid black; border-collapse: collapse; padding: 5px; font-size: 14px;">Sale Deed</td>
                                                                            <td style="border: 1px solid black; border-collapse: collapse; padding: 5px; font-size: 14px;">Mortgage deed</td>
                                                                            <td style="border: 1px solid black; border-collapse: collapse; padding: 5px; font-size: 14px;"></td>
                                                                            <td style="border: 1px solid black; border-collapse: collapse; padding: 5px; font-size: 14px;">Documents Changed</td>

                                                                        </tr>
                                                                        <tr>
                                                                            <td style="border: 1px solid black; border-collapse: collapse; padding: 5px; font-size: 14px;">2</td>
                                                                            <td style="border: 1px solid black; border-collapse: collapse; padding: 5px; font-size: 14px;">दस्तावेज़ पंजी. संख्या</td>
                                                                            <td style="border: 1px solid black; border-collapse: collapse; padding: 5px; font-size: 14px;">321654987369</td>
                                                                            <td style="border: 1px solid black; border-collapse: collapse; padding: 5px; font-size: 14px;"></td>
                                                                            <td style="border: 1px solid black; border-collapse: collapse; padding: 5px; font-size: 14px;"></td>
                                                                            <td style="border: 1px solid black; border-collapse: collapse; padding: 5px; font-size: 14px;"></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="border: 1px solid black; border-collapse: collapse; padding: 5px; font-size: 14px;">3</td>
                                                                            <td style="border: 1px solid black; border-collapse: collapse; padding: 5px; font-size: 14px;">संपत्ति का मार्गदर्शिका मूल्य</td>
                                                                            <td style="border: 1px solid black; border-collapse: collapse; padding: 5px; font-size: 14px;">Rs. 895.50</td>
                                                                            <td style="border: 1px solid black; border-collapse: collapse; padding: 5px; font-size: 14px;">Rs. 1050.50</td>
                                                                            <td style="border: 1px solid black; border-collapse: collapse; padding: 5px; font-size: 14px;">Rs. 155.00</td>
                                                                            <td style="border: 1px solid black; border-collapse: collapse; padding: 5px; font-size: 14px;"></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="border: 1px solid black; border-collapse: collapse; padding: 5px; font-size: 14px;">4</td>
                                                                            <td style="border: 1px solid black; border-collapse: collapse; padding: 5px; font-size: 14px;">प्रतिफल राशि / प्रतिभूति राशि</td>
                                                                            <td style="border: 1px solid black; border-collapse: collapse; padding: 5px; font-size: 14px;">Rs 102.85</td>
                                                                            <td style="border: 1px solid black; border-collapse: collapse; padding: 5px; font-size: 14px;">Rs 150.00</td>
                                                                            <td style="border: 1px solid black; border-collapse: collapse; padding: 5px; font-size: 14px;"></td>
                                                                            <td style="border: 1px solid black; border-collapse: collapse; padding: 5px; font-size: 14px;">At the mark position</td>
                                                                        </tr>

                                                                        <tr>
                                                                            <td style="border: 1px solid black; border-collapse: collapse; padding: 5px; font-size: 14px;">5</td>
                                                                            <td style="border: 1px solid black; border-collapse: collapse; padding: 5px; font-size: 14px;">प्रिंसिपल स्टाम्प ड्यूटी</td>
                                                                            <td style="border: 1px solid black; border-collapse: collapse; padding: 5px; font-size: 14px;">Rs 62,500.00</td>
                                                                            <td style="border: 1px solid black; border-collapse: collapse; padding: 5px; font-size: 14px;">Rs 74,246.25</td>
                                                                            <td style="border: 1px solid black; border-collapse: collapse; padding: 5px; font-size: 14px;"></td>
                                                                            <td style="border: 1px solid black; border-collapse: collapse; padding: 5px; font-size: 14px;"></td>

                                                                        </tr>
                                                                        <tr>
                                                                            <td style="border: 1px solid black; border-collapse: collapse; padding: 5px; font-size: 14px;">6</td>
                                                                            <td style="border: 1px solid black; border-collapse: collapse; padding: 5px; font-size: 14px;">मुन्सिपल स्टाम्प ड्यूटी</td>
                                                                            <td style="border: 1px solid black; border-collapse: collapse; padding: 5px; font-size: 14px;">Rs 62,500.00</td>
                                                                            <td style="border: 1px solid black; border-collapse: collapse; padding: 5px; font-size: 14px;">Rs 74,246.25</td>
                                                                            <td style="border: 1px solid black; border-collapse: collapse; padding: 5px; font-size: 14px;"></td>
                                                                            <td style="border: 1px solid black; border-collapse: collapse; padding: 5px; font-size: 14px;"></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="border: 1px solid black; border-collapse: collapse; padding: 5px; font-size: 14px;">7</td>
                                                                            <td style="border: 1px solid black; border-collapse: collapse; padding: 5px; font-size: 14px;">जनपद स्टाम्प ड्यूटी</td>
                                                                            <td style="border: 1px solid black; border-collapse: collapse; padding: 5px; font-size: 14px;">Rs 62,500.00</td>
                                                                            <td style="border: 1px solid black; border-collapse: collapse; padding: 5px; font-size: 14px;">Rs 74,246.25</td>
                                                                            <td style="border: 1px solid black; border-collapse: collapse; padding: 5px; font-size: 14px;"></td>
                                                                            <td style="border: 1px solid black; border-collapse: collapse; padding: 5px; font-size: 14px;"></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="border: 1px solid black; border-collapse: collapse; padding: 5px; font-size: 14px;">8</td>
                                                                            <td style="border: 1px solid black; border-collapse: collapse; padding: 5px; font-size: 14px;">उपकर</td>
                                                                            <td style="border: 1px solid black; border-collapse: collapse; padding: 5px; font-size: 14px;">Rs 62,500.00</td>
                                                                            <td style="border: 1px solid black; border-collapse: collapse; padding: 5px; font-size: 14px;">Rs 74,246.25</td>
                                                                            <td style="border: 1px solid black; border-collapse: collapse; padding: 5px; font-size: 14px;"></td>
                                                                            <td style="border: 1px solid black; border-collapse: collapse; padding: 5px; font-size: 14px;"></td>
                                                                        </tr>

                                                                        <tr>
                                                                            <td style="border: 1px solid black; border-collapse: collapse; padding: 5px; font-size: 14px;">9</td>
                                                                            <td style="border: 1px solid black; border-collapse: collapse; padding: 5px; font-size: 14px;">कुल स्टाम्प ड्यूटी</td>
                                                                            <td style="border: 1px solid black; border-collapse: collapse; padding: 5px; font-size: 14px;">Rs 2,50,000.00</td>
                                                                            <td style="border: 1px solid black; border-collapse: collapse; padding: 5px; font-size: 14px;">Rs 2,96,985.00</td>
                                                                            <td style="border: 1px solid black; border-collapse: collapse; padding: 5px; font-size: 14px;">Rs 46,985.00</td>
                                                                            <td style="border: 1px solid black; border-collapse: collapse; padding: 5px; font-size: 14px;">Not ennough duty paid</td>
                                                                        </tr>

                                                                        <tr>
                                                                            <td style="border: 1px solid black; border-collapse: collapse; padding: 5px; font-size: 14px;">10</td>
                                                                            <td style="border: 1px solid black; border-collapse: collapse; padding: 5px; font-size: 14px;">रजिस्ट्रेशन फीस</td>
                                                                            <td style="border: 1px solid black; border-collapse: collapse; padding: 5px; font-size: 14px;">Rs 1,55,000.00</td>
                                                                            <td style="border: 1px solid black; border-collapse: collapse; padding: 5px; font-size: 14px;">Rs 1,79,680.00</td>
                                                                            <td style="border: 1px solid black; border-collapse: collapse; padding: 5px; font-size: 14px;">Rs 24,680.00</td>
                                                                            <td style="border: 1px solid black; border-collapse: collapse; padding: 5px; font-size: 14px;">Unpaid balance amount</td>
                                                                        </tr>

                                                                        <tr>
                                                                            <td style="border: 1px solid black; border-collapse: collapse; padding: 5px; font-size: 14px;"><b>11</b></td>
                                                                            <td style="border: 1px solid black; border-collapse: collapse; padding: 5px; font-size: 14px;"><b>कुल राशि</b></td>
                                                                            <td style="border: 1px solid black; border-collapse: collapse; padding: 5px; font-size: 14px;"><b>Rs 8,70,998.35</b></td>
                                                                            <td style="border: 1px solid black; border-collapse: collapse; padding: 5px; font-size: 14px;"><b>Rs 10,16,905.50</b></td>
                                                                            <td style="border: 1px solid black; border-collapse: collapse; padding: 5px; font-size: 14px;"><b>Rs 1,45,860.00</b></td>
                                                                            <td style="border: 1px solid black; border-collapse: collapse; padding: 5px; font-size: 14px;"></td>
                                                                        </tr>
                                                                    </table>--%>
                                                                    <div class="table-responsive">
                                                                        <table style="width: 100%; border: 1px solid black; border-collapse: collapse; text-align: left; white-space: nowrap">
                                                                            <tr>

                                                                                <th style="border: 1px solid black; border-collapse: collapse; line-height: 25px; height: 40px; white-space: nowrap; padding: 10px; background: #cccccc85;">क्रमांक
                                                                                <br />
                                                                                </th>
                                                                                <th style="border: 1px solid black; border-collapse: collapse; line-height: 25px; height: 40px; padding: 10px; background: #cccccc85;">दस्तावेज़ का
                                                                                <br />
                                                                                    विवरण</th>
                                                                                <th style="border: 1px solid black; border-collapse: collapse; line-height: 25px; height: 40px; white-space: nowrap; padding: 10px; background: #cccccc85;">पार्टियों के अनुसार /<br />
                                                                                    दस्तावेजों का  विवरण</th>
                                                                                <th style="border: 1px solid black; border-collapse: collapse; line-height: 25px; height: 40px; padding: 10px; background: #cccccc85;">एसआर/पीओ का
                                                                                <br />
                                                                                    प्रस्ताव</th>
                                                                                <th style="border: 1px solid black; border-collapse: collapse; line-height: 25px; height: 40px; padding: 10px; background: #cccccc85;">मूल्य का
                                                                                <br />
                                                                                    अंतर</th>
                                                                                <th style="border: 1px solid black; border-collapse: collapse; line-height: 25px; height: 40px; white-space: nowrap; padding: 10px; background: #cccccc85;">रिमार्क </th>
                                                                            </tr>

                                                                            <tbody>
                                                                                <tr>
                                                                                    <td style="border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;">1.
                                                                                    </td>
                                                                                    <td style="border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;">दस्तावेज़ का प्रकार
                                                                                    </td>
                                                                                    <td style="border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px; white-space: normal">
                                                                                        <asp:Label ID="lblDocParty" runat="server"></asp:Label></td>
                                                                                    <td style="border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px; white-space: normal">
                                                                                        <asp:Label ID="lblSRPro" runat="server"></asp:Label>
                                                                                    </td>
                                                                                    <td style="border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;">
                                                                                        <asp:Label ID="lblDefict" runat="server" Visible="false"></asp:Label>
                                                                                    </td>
                                                                                    <td style="border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;">
                                                                                        <asp:Label ID="lblRemark" runat="server"></asp:Label></td>



                                                                                </tr>
                                                                                <%-- <tr>
    <td style="border: 1px solid black;
  border-collapse: collapse;    height: 40px;padding: 10px;">
     2.
  </td>
    <td style="border: 1px solid black;
  border-collapse: collapse;    height: 40px;padding: 10px;">
डीड 
  </td>
  <td style="border: 1px solid black;
  border-collapse: collapse;    height: 40px;padding: 10px;"></td>
    <td style="border: 1px solid black;
  border-collapse: collapse;    height: 40px;padding: 10px;">
     
  </td>
    <td style="border: 1px solid black;
  border-collapse: collapse;    height: 40px;padding: 10px;">
 
  </td>
  <td style="border: 1px solid black;
  border-collapse: collapse;    height: 40px;padding: 10px;"> &nbsp</td>

  
 
  </tr>
  <tr>
    <td style="border: 1px solid black;
  border-collapse: collapse;    height: 40px;padding: 10px;">
     3.
  </td>
    <td style="border: 1px solid black;
  border-collapse: collapse;    height: 40px;padding: 10px;">
 इंस्ट्रूमेंट
  </td>
  <td style="border: 1px solid black;
  border-collapse: collapse;    height: 40px;padding: 10px;"></td>
    <td style="border: 1px solid black;
  border-collapse: collapse;    height: 40px;padding: 10px;">
   
  </td>
    <td style="border: 1px solid black;
  border-collapse: collapse;    height: 40px;padding: 10px;">
 
  </td>
  <td style="border: 1px solid black;
  border-collapse: collapse;    height: 40px;padding: 10px;"> &nbsp</td>

  
 
  </tr>--%>

                                                                                <tr>
                                                                                    <td style="border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;">2.
                                                                                    </td>
                                                                                    <td style="border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;">दस्तावेज़ पंजीकरण संख्या
                                                                                    </td>
                                                                                    <td colspan="4" style="border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;">
                                                                                        <asp:Label ID="lblRegNo" runat="server"></asp:Label>
                                                                                    </td>




                                                                                </tr>

                                                                                <tr>
                                                                                    <td style="border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px; min-width: 60px;">3.
                                                                                    </td>
                                                                                    <td style="border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;">संपत्ति का मार्गदर्शिका मूल्य
                                                                                    </td>
                                                                                    <td style="border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;">
                                                                                        <asp:Label ID="lblGuideValue" runat="server"></asp:Label></td>
                                                                                    <td style="border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px; min-width: 60px;">
                                                                                        <asp:Label ID="lblSROGuide" runat="server"></asp:Label>
                                                                                    </td>

                                                                                    <td style="border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px; min-width: 60px;">
                                                                                        <asp:Label ID="lblGuideDefict" runat="server"></asp:Label>
                                                                                    </td>
                                                                                    <td style="border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px; min-width: 60px;">
                                                                                        <asp:Label ID="lblGuideRemark" runat="server"></asp:Label>
                                                                                    </td>



                                                                                </tr>
                                                                                <tr>
                                                                                    <td style="border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px; min-width: 60px;">4.
                                                                                    </td>
                                                                                    <td style="border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;">प्रतिफल राशि / प्रतिभूति राशि 
                                                                                    </td>
                                                                                    <td style="border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px; min-width: 60px;">
                                                                                        <asp:Label ID="lblConValue" runat="server"></asp:Label></td>
                                                                                    <td style="border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px; min-width: 60px;">
                                                                                        <asp:Label ID="lblSRCon" runat="server"></asp:Label>
                                                                                    </td>
                                                                                    <td style="border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px; min-width: 60px;">
                                                                                        <asp:Label ID="lblConDefict" runat="server"></asp:Label>
                                                                                    </td>
                                                                                    <td style="border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px; min-width: 60px;">
                                                                                        <asp:Label ID="lblConRemark" runat="server"></asp:Label></td>



                                                                                </tr>

                                                                                <tr>
                                                                                    <td style="border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px; min-width: 60px;">5.
                                                                                    </td>
                                                                                    <td style="border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;">प्रिंसिपल स्टाम्प ड्यूटी
                                                                                    </td>
                                                                                    <td style="border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px; min-width: 60px;">
                                                                                        <asp:Label ID="lblPrinStamDoc" runat="server"></asp:Label></td>
                                                                                    <td style="border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px; min-width: 60px;">
                                                                                        <asp:Label ID="lblPrinStampPro" runat="server"></asp:Label>
                                                                                    </td>
                                                                                    <td style="border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px; min-width: 60px;">&nbsp
                                                                                    </td>
                                                                                    <td style="border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px; min-width: 60px;">&nbsp</td>



                                                                                </tr>

                                                                                <tr>
                                                                                    <td style="border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px; min-width: 60px;">6.
                                                                                    </td>
                                                                                    <td style="border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;">मुन्सिपल स्टाम्प ड्यूटी
                                                                                    </td>
                                                                                    <td style="border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px; min-width: 60px;">
                                                                                        <asp:Label ID="lblMStamp" runat="server"></asp:Label></td>
                                                                                    <td style="border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px; min-width: 60px;">
                                                                                        <asp:Label ID="lblMStampPro" runat="server"></asp:Label>
                                                                                    </td>
                                                                                    <td style="border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px; min-width: 60px;">&nbsp
                                                                                    </td>
                                                                                    <td style="border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px; min-width: 60px;">&nbsp</td>



                                                                                </tr>

                                                                                <tr>
                                                                                    <td style="border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px; min-width: 60px;">7.
                                                                                    </td>
                                                                                    <td style="border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;">जनपद स्टाम्प ड्यूटी
                                                                                    </td>
                                                                                    <td style="border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px; min-width: 60px;">
                                                                                        <asp:Label ID="lblJanpad" runat="server"></asp:Label></td>
                                                                                    <td style="border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px; min-width: 60px;">
                                                                                        <asp:Label ID="lblJanpadPro" runat="server"></asp:Label>
                                                                                    </td>
                                                                                    <td style="border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px; min-width: 60px;">&nbsp
                                                                                    </td>
                                                                                    <td style="border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px; min-width: 60px;">&nbsp</td>



                                                                                </tr>

                                                                                <tr>
                                                                                    <td style="border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px; min-width: 60px;">8.
                                                                                    </td>
                                                                                    <td style="border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;">उपकर 
                                                                                    </td>
                                                                                    <td style="border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px; min-width: 60px;">
                                                                                        <asp:Label ID="lblUpkarDoc" runat="server"></asp:Label></td>
                                                                                    <td style="border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px; min-width: 60px;">
                                                                                        <asp:Label ID="lblUpkarPro" runat="server"></asp:Label>
                                                                                    </td>
                                                                                    <td style="border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px; min-width: 60px;">&nbsp
                                                                                    </td>
                                                                                    <td style="border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px; min-width: 60px;">&nbsp</td>



                                                                                </tr>
                                                                                <tr>
                                                                                    <td style="border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px; min-width: 60px;">9.
                                                                                    </td>
                                                                                    <td style="border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;">कुल स्टाम्प ड्यूटी
                                                                                    </td>
                                                                                    <td style="border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px; min-width: 60px;">
                                                                                        <asp:Label ID="lbl_TStamp_Party" runat="server"></asp:Label></td>
                                                                                    <td style="border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px; min-width: 60px;">
                                                                                        <asp:Label ID="lbl_TStamp_SR" runat="server"></asp:Label>
                                                                                    </td>
                                                                                    <td style="border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px; min-width: 60px;">
                                                                                        <asp:Label ID="lbl_TStamp_Deficit" runat="server"></asp:Label>
                                                                                    </td>
                                                                                    <td style="border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px; min-width: 60px;">
                                                                                        <asp:Label ID="lbl_TStamp_Remark" runat="server"></asp:Label></td>



                                                                                </tr>
                                                                                <tr>
                                                                                    <td style="border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px; min-width: 60px;">10.
                                                                                    </td>
                                                                                    <td style="border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;">छूट प्राप्त राशि 
                                                                                    </td>
                                                                                    <td style="border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px; min-width: 60px;">
                                                                                        <asp:Label ID="lblExcepted_Party" runat="server"></asp:Label></td>
                                                                                    <td style="border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px; min-width: 60px;">
                                                                                        <asp:Label ID="lblExempted_SR" runat="server"></asp:Label>
                                                                                    </td>
                                                                                    <td style="border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px; min-width: 60px;">
                                                                                        <asp:Label ID="lblExempted_Deficit" runat="server"></asp:Label>
                                                                                    </td>
                                                                                    <td style="border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px; min-width: 60px;">
                                                                                        <asp:Label ID="lblExempted_Remark" runat="server"></asp:Label></td>



                                                                                </tr>
                                                                                <tr>
                                                                                    <td style="border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px; min-width: 60px;">11.
                                                                                    </td>
                                                                                    <td style="border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;">भुगतान की गई स्टाम्प ड्यूटी 
                                                                                    </td>
                                                                                    <td style="border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px; min-width: 60px;">
                                                                                        <asp:Label ID="lbl_Paid_Party" runat="server"></asp:Label></td>
                                                                                    <td style="border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px; min-width: 60px;">
                                                                                        <asp:Label ID="lbl_Paid_SR" runat="server"></asp:Label>
                                                                                    </td>
                                                                                    <td style="border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px; min-width: 60px;">
                                                                                        <asp:Label ID="lbl_Paid_Deficit" runat="server"></asp:Label>
                                                                                    </td>
                                                                                    <td style="border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px; min-width: 60px;">
                                                                                        <asp:Label ID="lbl_Paid_Remark" runat="server"></asp:Label></td>



                                                                                </tr>
                                                                                <tr>
                                                                                    <td style="border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px; min-width: 60px;">12.
                                                                                    </td>
                                                                                    <td style="border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;">शुद्ध स्टाम्प ड्यूटी 
                                                                                    </td>
                                                                                    <td style="border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px; min-width: 60px;">
                                                                                        <asp:Label ID="lblTStamDoc" runat="server"></asp:Label></td>
                                                                                    <td style="border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px; min-width: 60px;">
                                                                                        <asp:Label ID="lblTStamppro" runat="server"></asp:Label>
                                                                                    </td>
                                                                                    <td style="border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px; min-width: 60px;">
                                                                                        <asp:Label ID="lblTStampdeficit" runat="server"></asp:Label>
                                                                                    </td>
                                                                                    <td style="border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px; min-width: 60px;">
                                                                                        <asp:Label ID="lblTStampRemark" runat="server"></asp:Label></td>



                                                                                </tr>
                                                                                <tr>
                                                                                    <td style="border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px; min-width: 60px;">13.
                                                                                    </td>
                                                                                    <td style="border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;">रजिस्ट्रेशन फीस 
                                                                                    </td>
                                                                                    <td style="border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px; min-width: 60px;">
                                                                                        <asp:Label ID="lblTRegDoc" runat="server"></asp:Label></td>
                                                                                    <td style="border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px; min-width: 60px;">
                                                                                        <asp:Label ID="lblTRegPro" runat="server"></asp:Label>
                                                                                    </td>
                                                                                    <td style="border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px; min-width: 60px;">
                                                                                        <asp:Label ID="lblTRegDeficit" runat="server" Visible="false"></asp:Label>
                                                                                    </td>
                                                                                    <td style="border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px; min-width: 60px;">
                                                                                        <asp:Label ID="lblTRegRemark" runat="server"></asp:Label></td>



                                                                                </tr>
                                                                                <tr>
                                                                                    <td style="border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px; min-width: 60px;">14.
                                                                                    </td>
                                                                                    <td style="border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;">छूट प्राप्त राशि 
                                                                                    </td>
                                                                                    <td style="border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px; min-width: 60px;">
                                                                                        <asp:Label ID="lblExemtptedRegParty" runat="server"></asp:Label></td>
                                                                                    <td style="border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px; min-width: 60px;">
                                                                                        <asp:Label ID="lblExemtptedRegSR" runat="server"></asp:Label>
                                                                                    </td>
                                                                                    <td style="border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;">
                                                                                        <asp:TextBox ID="TextBox3" runat="server" Visible="false"></asp:TextBox>
                                                                                        <asp:Label ID="Label18" runat="server"></asp:Label>

                                                                                    </td>
                                                                                    <td style="border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px; min-width: 60px;">
                                                                                        <asp:Label ID="Label19" runat="server" Visible="false"></asp:Label>
                                                                                    </td>



                                                                                </tr>

                                                                                <tr>
                                                                                    <td style="border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px; min-width: 60px;">15.
                                                                                    </td>
                                                                                    <td style="border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;">भुगतान की गई रजिस्ट्रेशन फीस 
                                                                                    </td>
                                                                                    <td style="border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px; min-width: 60px;">
                                                                                        <asp:Label ID="lbl_PaidReg_Party" runat="server"></asp:Label></td>
                                                                                    <td style="border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px; min-width: 60px;">
                                                                                        <asp:Label ID="lbl_PaidReg_SR" runat="server"></asp:Label>
                                                                                    </td>
                                                                                    <td style="border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;">
                                                                                        <asp:TextBox ID="TextBox5" runat="server" Visible="false"></asp:TextBox>
                                                                                        <asp:Label ID="lbl_PaidReg_Deficit" runat="server"></asp:Label>

                                                                                    </td>
                                                                                    <td style="border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px; min-width: 60px;">
                                                                                        <asp:Label ID="lbl_PaidReg_Remark" runat="server" Visible="false"></asp:Label>
                                                                                    </td>



                                                                                </tr>


                                                                                <tr>
                                                                                    <td style="border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px; min-width: 60px;">16.
                                                                                    </td>
                                                                                    <td style="border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;">शुद्ध रजिस्ट्रेशन फीस  
                                                                                    </td>
                                                                                    <td style="border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px; min-width: 60px;">
                                                                                        <asp:Label ID="lblNetPartyReg" runat="server"></asp:Label></td>
                                                                                    <td style="border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px; min-width: 60px;">
                                                                                        <asp:Label ID="lblNetSRReg" runat="server"></asp:Label>
                                                                                    </td>
                                                                                    <td style="border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px; min-width: 60px;">
                                                                                        <asp:Label ID="lblNetDeficitReg" runat="server"></asp:Label>
                                                                                    </td>
                                                                                    <td style="border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px; min-width: 60px;">
                                                                                        <asp:Label ID="Label29" runat="server"></asp:Label></td>



                                                                                </tr>


                                                                                <tr>
                                                                                    <td style="border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px; min-width: 60px;">17.
                                                                                    </td>
                                                                                    <td style="border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;">कुल राशि
                                                                                    </td>
                                                                                    <td style="border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px; min-width: 60px;">
                                                                                        <asp:Label ID="lblTAmtParty" runat="server"></asp:Label></td>
                                                                                    <td style="border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px; min-width: 60px;">
                                                                                        <asp:Label ID="lblTAmtSRO" runat="server"></asp:Label>
                                                                                    </td>
                                                                                    <td style="border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px; min-width: 60px;">
                                                                                        <asp:Label ID="lblTAmtDeficit" runat="server"></asp:Label>
                                                                                    </td>
                                                                                    <td style="border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px; min-width: 60px;">
                                                                                        <asp:Label ID="lblTAmtRemark" runat="server"></asp:Label></td>



                                                                                </tr>
                                                                            </tbody>
                                                                        </table>
                                                                    </div>
                                                                </div>

                                                                <br />
                                                                <br />

                                                                <div>
                                                                    <%-- <h3 style="font-size: 18px; margin: 0; font-weight: 600;">कलेक्टर ऑफ़ स्टाम्प,न्यायालय </h3>
                                                                    <h4 style="margin: 0; margin: 10px; font-size: 13px;">cos.bhopal@mp.gov.in</h4>
                                                                    <h2 style="margin: 0; margin: 10px; font-size: 16px;">भोपाल 2</h2>
                                                                    <h2 style="margin: 0; margin: 10px; font-size: 16px;">आदेश </h2>--%>
                                                                    <br>



                                                                    <div style="padding-top: 15px;">
                                                                        <h6 style="text-align: justify; width: 100%; font-size: 15px;"><b>पक्षकार/पक्षकारों का ज़वाब :</b></h6>
                                                                        <p id="pContent1" style="text-align: justify">
                                                                        </p>
                                                                        <asp:Label ID="lblPartyReply" runat="server" Visible="false"></asp:Label>
                                                                        <%--<textarea style="width: 100%; font-size: 14px;">  </textarea>--%>
                                                                        <h6 style="text-align: justify; width: 100%; font-size: 15px;"><b>निष्कर्ष :</b></h6>
                                                                        <p id="pContent2" style="text-align: justify">
                                                                        </p>
                                                                        <asp:Label ID="lblNishkarsh" runat="server" Visible="false"></asp:Label>
                                                                    </div>

                                                                    <%-- <div style="display: flex; padding: 15px 0px 30px 0;">
                                                                        <h6 style="text-align: justify; width: 40%; font-size: 15px;">क्या आप संपत्ति का मूल्य और आदेश के लिए शुल्क की गणना करना चाहते हैं? </h6>
                                                                        <h6 style="text-align: center; width: 60%; font-size: 15px; display: flex; flex-direction: row; justify-content: center; align-items: baseline;">
                                                                            <input type="radio" id="yes" name="fav_language" value="yes">
                                                                            <label for="html">हा</label><br>
                                                                            <input type="radio" id="no" name="fav_language" value="no">
                                                                            <label for="css">नहीं</label><br>
                                                                        </h6>
                                                                    </div>--%>

                                                                    <asp:UpdatePanel ID="upnl1" runat="server">
                                                                        <ContentTemplate>
                                                                            <asp:Panel ID="pnlOption" runat="server" Visible="true">
                                                                                <p style="font-size: 18px; line-height: 30px; text-align: left;">
                                                                                    <div class="row pr0" id="UploadDiv">
                                                                                        <div class="col-md-8" style="text-align: justify; width: 100%; font-size: 15px;">
                                                                                            <%--<b>क्या आप संपत्ति का मूल्य और आदेश के लिए शुल्क की गणना करना चाहते हैं?</b>--%>
                                                                                            <%-- <b>दस्तावेज पर पृथक से प्रभार्य शुल्क एवं फीस की गणना करना चाहते हैं?</b>--%>
                                                                                            <b>क्या आप संपत्ति का मूल्यांकन करना चाहते हैं ?</b>

                                                                                        </div>
                                                                                        <div class="col-md-4">
                                                                                            <asp:CheckBoxList ID="chkblPartys" runat="server" Visible="false" RepeatDirection="Horizontal" CssClass="table form-check-input"></asp:CheckBoxList>
                                                                                            <asp:RadioButton ID="rdbtnReportYes" CssClass="mr-4" ClientIDMode="Static" GroupName="SeekReport" Text="हाँ" runat="server" AutoPostBack="true" OnCheckedChanged="rdbChangeNature_CheckedChanged" />
                                                                                            <asp:RadioButton ID="rdbtnReportNo" GroupName="SeekReport" ClientIDMode="Static" Text="नहीं" runat="server" AutoPostBack="true" OnCheckedChanged="rdbtnReportNo_CheckedChanged" />

                                                                                        </div>


                                                                                    </div>

                                                                                </p>
                                                                            </asp:Panel>
                                                                            <asp:Panel ID="pnlChange" runat="server" Visible="false">
                                                                                <p style="font-size: 18px; line-height: 30px; text-align: left;">
                                                                                    <div class="row pr0" id="UploadDiv1">
                                                                                        <div class="col-md-8" style="text-align: left;">
                                                                                            <%-- <b>क्या आप आदेश के लिए संपत्ति और दस्तावेज़ की प्रकृति को बदलना चाहते हैं?</b>--%>
                                                                                            <b>संपत्ति का मूल्यांकन करने के लिए हाँ पर क्लिक करे |</b>
                                                                                        </div>
                                                                                        <div class="col-md-4">
                                                                                            <asp:CheckBoxList ID="CheckBoxList1" runat="server" RepeatDirection="Horizontal" CssClass="table form-check-input"></asp:CheckBoxList>
                                                                                            <asp:RadioButton ID="rdbChangeNature" runat="server" Text="हाँ" AutoPostBack="true" name="Fruit" OnCheckedChanged="rdbChangeNature_CheckedChanged" />
                                                                                            <%--<asp:RadioButton ID="RadioButton2" GroupName="SeekReport" Text="नहीं" runat="server" AutoPostBack="true" />--%>
                                                                                        </div>


                                                                                    </div>

                                                                                </p>
                                                                            </asp:Panel>

                                                                            <asp:Panel ID="pnlCalNo" runat="server" Visible="false">
                                                                                <p style="font-size: 18px; line-height: 30px; text-align: left;">
                                                                                    <div id="UploadDiv2" class="text-left">
                                                                                        <div class="row pr0">
                                                                                            <div class="col-md-4 text-center">
                                                                                                <asp:CheckBoxList ID="CheckBoxList2" runat="server" AutoPostBack="true" RepeatDirection="Horizontal" CssClass="table form-check-input"></asp:CheckBoxList>
                                                                                                <asp:RadioButton ID="RadioButton2" GroupName="FNSeekReport" Text="" name="Fruit" runat="server" AutoPostBack="true" OnCheckedChanged="RadioButton2_CheckedChanged" />
                                                                                            </div>
                                                                                            <div class="col-md-8">
                                                                                                <%--<b>क्या आप एसआर/पीओ प्रस्ताव को स्वीकार करना चाहते हैं?</b> --%>
                                                                                                <b>उपपंजीयक के प्रस्ताव से सहमत</b>
                                                                                            </div>

                                                                                        </div>
                                                                                        <div class="row pr0">
                                                                                            <div class="col-md-4 text-center">
                                                                                                <asp:CheckBoxList ID="CheckBoxList3" runat="server" RepeatDirection="Horizontal" CssClass="table form-check-input" OnSelectedIndexChanged="CheckBoxList3_SelectedIndexChanged"></asp:CheckBoxList>
                                                                                                <asp:RadioButton ID="RadioButton3" GroupName="FNSeekReport" Text="" name="Fruit" runat="server" AutoPostBack="true" OnCheckedChanged="RadioButton3_CheckedChanged" />
                                                                                            </div>

                                                                                            <div class="col-md-8">
                                                                                                <%--<b>क्या आप पार्टी दस्तावेज़ विवरण स्वीकार करना चाहते हैं?</b> --%>
                                                                                                <b>यह दस्तावेज उचित रूप से मुद्रांकित है </b>
                                                                                            </div>

                                                                                        </div>
                                                                                    </div>
                                                                                </p>
                                                                            </asp:Panel>



                                                                            <div>
                                                                                <h6 style="text-align: justify; width: 100%; font-size: 15px;">निर्णय : </h6>
                                                                                <%--  <table style="width: 100%; border: 1px solid black; border-collapse: collapse;">
                                                                            <tr style="background: #e9e9e9;">
                                                                                <th style="border: 1px solid black; border-collapse: collapse; line-height: 20px; padding: 5px; font-size: 14px;">सीरियल नंबर</th>
                                                                                <th style="border: 1px solid black; border-collapse: collapse; line-height: 20px; padding: 5px; font-size: 14px;">दस्तावेज़ का विवरण</th>
                                                                                <th style="border: 1px solid black; border-collapse: collapse; line-height: 20px; padding: 5px; font-size: 14px;">पार्टियों के अनुसार दस्तावेज/ दस्तावेजों का विवरण</th>
                                                                                <th style="border: 1px solid black; border-collapse: collapse; line-height: 20px; padding: 5px; font-size: 14px;">एसआरपीओ का प्रस्ताव</th>
                                                                                <th style="border: 1px solid black; border-collapse: collapse; line-height: 20px; padding: 5px; font-size: 14px;">सी ओ एस मूल्य</th>
                                                                                <th style="border: 1px solid black; border-collapse: collapse; line-height: 20px; padding: 5px; font-size: 14px;">मूल्य का अंतर</th>
                                                                                <th style="border: 1px solid black; border-collapse: collapse; line-height: 20px; padding: 5px; font-size: 14px;">दंड की राशि</th>
                                                                            </tr>

                                                                            <tr>
                                                                                <td style="border: 1px solid black; border-collapse: collapse; padding: 5px; font-size: 14px;">1</td>
                                                                                <td style="border: 1px solid black; border-collapse: collapse; padding: 5px; font-size: 14px;">दस्तावेज़ का प्रकार</td>
                                                                                <td style="border: 1px solid black; border-collapse: collapse; padding: 5px; font-size: 14px;">Sale Deed</td>
                                                                                <td style="border: 1px solid black; border-collapse: collapse; padding: 5px; font-size: 14px;">Mortgage deed</td>
                                                                                <td style="border: 1px solid black; border-collapse: collapse; padding: 5px; font-size: 14px;"></td>
                                                                                <td style="border: 1px solid black; border-collapse: collapse; padding: 5px; font-size: 14px;"></td>
                                                                                <td style="border: 1px solid black; border-collapse: collapse; padding: 5px; font-size: 14px;"></td>

                                                                            </tr>

                                                                            <tr>
                                                                                <td style="border: 1px solid black; border-collapse: collapse; padding: 5px; font-size: 14px;">2</td>
                                                                                <td style="border: 1px solid black; border-collapse: collapse; padding: 5px; font-size: 14px;">दस्तावेज़ पंजी. संख्या</td>
                                                                                <td style="border: 1px solid black; border-collapse: collapse; padding: 5px; font-size: 14px;">321654987369</td>
                                                                                <td style="border: 1px solid black; border-collapse: collapse; padding: 5px; font-size: 14px;"></td>
                                                                                <td style="border: 1px solid black; border-collapse: collapse; padding: 5px; font-size: 14px;"></td>
                                                                                <td style="border: 1px solid black; border-collapse: collapse; padding: 5px; font-size: 14px;"></td>
                                                                                <td style="border: 1px solid black; border-collapse: collapse; padding: 5px; font-size: 14px;"></td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td style="border: 1px solid black; border-collapse: collapse; padding: 5px; font-size: 14px;">3</td>
                                                                                <td style="border: 1px solid black; border-collapse: collapse; padding: 5px; font-size: 14px;">संपत्ति का मार्गदर्शिका मूल्य</td>
                                                                                <td style="border: 1px solid black; border-collapse: collapse; padding: 5px; font-size: 14px;">Rs. 895.50</td>
                                                                                <td style="border: 1px solid black; border-collapse: collapse; padding: 5px; font-size: 14px;">Rs. 1050.50</td>
                                                                                <td style="border: 1px solid black; border-collapse: collapse; padding: 5px; font-size: 14px;"></td>
                                                                                <td style="border: 1px solid black; border-collapse: collapse; padding: 5px; font-size: 14px;">Rs. 155.00</td>
                                                                                <td style="border: 1px solid black; border-collapse: collapse; padding: 5px; font-size: 14px;"></td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td style="border: 1px solid black; border-collapse: collapse; padding: 5px; font-size: 14px;">4</td>
                                                                                <td style="border: 1px solid black; border-collapse: collapse; padding: 5px; font-size: 14px;">प्रतिफल राशि / प्रतिभूति राशि</td>
                                                                                <td style="border: 1px solid black; border-collapse: collapse; padding: 5px; font-size: 14px;">Rs 102.85</td>
                                                                                <td style="border: 1px solid black; border-collapse: collapse; padding: 5px; font-size: 14px;">Rs 150.00</td>
                                                                                <td style="border: 1px solid black; border-collapse: collapse; padding: 5px; font-size: 14px;"></td>
                                                                                <td style="border: 1px solid black; border-collapse: collapse; padding: 5px; font-size: 14px;"></td>
                                                                                <td style="border: 1px solid black; border-collapse: collapse; padding: 5px; font-size: 14px;"></td>
                                                                            </tr>

                                                                            <tr>
                                                                                <td style="border: 1px solid black; border-collapse: collapse; padding: 5px; font-size: 14px;">5</td>
                                                                                <td style="border: 1px solid black; border-collapse: collapse; padding: 5px; font-size: 14px;">प्रिंसिपल स्टाम्प ड्यूटी</td>
                                                                                <td style="border: 1px solid black; border-collapse: collapse; padding: 5px; font-size: 14px;">Rs 62,500.00</td>
                                                                                <td style="border: 1px solid black; border-collapse: collapse; padding: 5px; font-size: 14px;">Rs 74,246.25</td>
                                                                                <td style="border: 1px solid black; border-collapse: collapse; padding: 5px; font-size: 14px;"></td>
                                                                                <td style="border: 1px solid black; border-collapse: collapse; padding: 5px; font-size: 14px;"></td>
                                                                                <td style="border: 1px solid black; border-collapse: collapse; padding: 5px; font-size: 14px;"></td>

                                                                            </tr>
                                                                            <tr>
                                                                                <td style="border: 1px solid black; border-collapse: collapse; padding: 5px; font-size: 14px;">6</td>
                                                                                <td style="border: 1px solid black; border-collapse: collapse; padding: 5px; font-size: 14px;">मुन्सिपल स्टाम्प ड्यूटी</td>
                                                                                <td style="border: 1px solid black; border-collapse: collapse; padding: 5px; font-size: 14px;">Rs 62,500.00</td>
                                                                                <td style="border: 1px solid black; border-collapse: collapse; padding: 5px; font-size: 14px;">Rs 74,246.25</td>
                                                                                <td style="border: 1px solid black; border-collapse: collapse; padding: 5px; font-size: 14px;"></td>
                                                                                <td style="border: 1px solid black; border-collapse: collapse; padding: 5px; font-size: 14px;"></td>
                                                                                <td style="border: 1px solid black; border-collapse: collapse; padding: 5px; font-size: 14px;"></td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td style="border: 1px solid black; border-collapse: collapse; padding: 5px; font-size: 14px;">7</td>
                                                                                <td style="border: 1px solid black; border-collapse: collapse; padding: 5px; font-size: 14px;">जनपद स्टाम्प ड्यूटी</td>
                                                                                <td style="border: 1px solid black; border-collapse: collapse; padding: 5px; font-size: 14px;">Rs 62,500.00</td>
                                                                                <td style="border: 1px solid black; border-collapse: collapse; padding: 5px; font-size: 14px;">Rs 74,246.25</td>
                                                                                <td style="border: 1px solid black; border-collapse: collapse; padding: 5px; font-size: 14px;"></td>
                                                                                <td style="border: 1px solid black; border-collapse: collapse; padding: 5px; font-size: 14px;"></td>
                                                                                <td style="border: 1px solid black; border-collapse: collapse; padding: 5px; font-size: 14px;"></td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td style="border: 1px solid black; border-collapse: collapse; padding: 5px; font-size: 14px;">8</td>
                                                                                <td style="border: 1px solid black; border-collapse: collapse; padding: 5px; font-size: 14px;">उपकर</td>
                                                                                <td style="border: 1px solid black; border-collapse: collapse; padding: 5px; font-size: 14px;">Rs 62,500.00</td>
                                                                                <td style="border: 1px solid black; border-collapse: collapse; padding: 5px; font-size: 14px;">Rs 74,246.25</td>
                                                                                <td style="border: 1px solid black; border-collapse: collapse; padding: 5px; font-size: 14px;"></td>
                                                                                <td style="border: 1px solid black; border-collapse: collapse; padding: 5px; font-size: 14px;"></td>
                                                                                <td style="border: 1px solid black; border-collapse: collapse; padding: 5px; font-size: 14px;"></td>
                                                                            </tr>

                                                                            <tr>
                                                                                <td style="border: 1px solid black; border-collapse: collapse; padding: 5px; font-size: 14px;">9</td>
                                                                                <td style="border: 1px solid black; border-collapse: collapse; padding: 5px; font-size: 14px;">कुल स्टाम्प ड्यूटी</td>
                                                                                <td style="border: 1px solid black; border-collapse: collapse; padding: 5px; font-size: 14px;">Rs 2,50,000.00</td>
                                                                                <td style="border: 1px solid black; border-collapse: collapse; padding: 5px; font-size: 14px;">Rs 2,96,985.00</td>
                                                                                <td style="border: 1px solid black; border-collapse: collapse; padding: 5px; font-size: 14px;"></td>
                                                                                <td style="border: 1px solid black; border-collapse: collapse; padding: 5px; font-size: 14px;">Rs 46,985.00</td>
                                                                                <td style="border: 1px solid black; border-collapse: collapse; padding: 5px; font-size: 14px;"></td>
                                                                            </tr>

                                                                            <tr>
                                                                                <td style="border: 1px solid black; border-collapse: collapse; padding: 5px; font-size: 14px;">10</td>
                                                                                <td style="border: 1px solid black; border-collapse: collapse; padding: 5px; font-size: 14px;">रजिस्ट्रेशन फीस</td>
                                                                                <td style="border: 1px solid black; border-collapse: collapse; padding: 5px; font-size: 14px;">Rs 1,55,000.00</td>
                                                                                <td style="border: 1px solid black; border-collapse: collapse; padding: 5px; font-size: 14px;">Rs 1,79,680.00</td>
                                                                                <td style="border: 1px solid black; border-collapse: collapse; padding: 5px; font-size: 14px;"></td>
                                                                                <td style="border: 1px solid black; border-collapse: collapse; padding: 5px; font-size: 14px;">Rs 24,680.00</td>
                                                                                <td style="border: 1px solid black; border-collapse: collapse; padding: 5px; font-size: 14px;"></td>
                                                                            </tr>

                                                                            <tr>
                                                                                <td style="border: 1px solid black; border-collapse: collapse; padding: 5px; font-size: 14px;"><b>11</b></td>
                                                                                <td style="border: 1px solid black; border-collapse: collapse; padding: 5px; font-size: 14px;"><b>कुल राशि</b></td>
                                                                                <td style="border: 1px solid black; border-collapse: collapse; padding: 5px; font-size: 14px;"><b>Rs 8,70,998.35</b></td>
                                                                                <td style="border: 1px solid black; border-collapse: collapse; padding: 5px; font-size: 14px;"><b>Rs 10,16,905.50</b></td>
                                                                                <td style="border: 1px solid black; border-collapse: collapse; padding: 5px; font-size: 14px;"></td>
                                                                                <td style="border: 1px solid black; border-collapse: collapse; padding: 5px; font-size: 14px;"><b>Rs 1,45,860.00</b></td>
                                                                                <td style="border: 1px solid black; border-collapse: collapse; padding: 5px; font-size: 14px;"></td>
                                                                            </tr>
                                                                        </table>--%>
                                                                                <div class="table-responsive">
                                                                                    <table id="tblCOSDec" style="width: 100%; border: 1px solid black; border-collapse: collapse; text-align: left; white-space: nowrap" runat="server">
                                                                                        <tr>

                                                                                            <th style="border: 1px solid black; border-collapse: collapse; line-height: 25px; height: 40px; white-space: nowrap; padding: 10px; background: #cccccc85;">क्रमांक
                            <br />
                                                                                            </th>
                                                                                            <th style="border: 1px solid black; border-collapse: collapse; line-height: 25px; height: 40px; padding: 10px; background: #cccccc85;">दस्तावेज़ का
                            <br />
                                                                                                विवरण</th>
                                                                                            <th style="border: 1px solid black; border-collapse: collapse; line-height: 25px; height: 40px; white-space: nowrap; padding: 10px; background: #cccccc85;">पार्टियों के अनुसार 
                            <br />
                                                                                                दस्तावेजों का  विवरण</th>
                                                                                            <th style="border: 1px solid black; border-collapse: collapse; line-height: 25px; height: 40px; padding: 10px; background: #cccccc85;">एसआर/पीओ का
                            <br />
                                                                                                प्रस्ताव</th>

                                                                                            <th style="border: 1px solid black; border-collapse: collapse; line-height: 25px; height: 40px; padding: 10px; background: #cccccc85;">आदेश अनुसार प्रभार्य शुल्क
                            <br />
                                                                                            </th>
                                                                                            <th style="border: 1px solid black; border-collapse: collapse; line-height: 25px; height: 40px; padding: 10px; background: #cccccc85;">कमी शूल्क
                            <br />
                                                                                            </th>
                                                                                            <th style="border: 1px solid black; border-collapse: collapse; line-height: 25px; height: 40px; padding: 10px; background: #cccccc85;">दंड की राशि
                                                                                            </th>

                                                                                            <th style="border: 1px solid black; border-collapse: collapse; line-height: 25px; height: 40px; padding: 10px; background: #cccccc85;">कुल राशि
                                                                                            </th>

                                                                                        </tr>

                                                                                        <tbody>
                                                                                            <tr>
                                                                                                <td style="border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;">1.
                                                                                                </td>
                                                                                                <td style="border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;">दस्तावेज़ का प्रकार :
                                                                                                </td>
                                                                                                <td style="border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px; white-space: normal">
                                                                                                    <asp:Label ID="lblDocParty1" runat="server"></asp:Label></td>
                                                                                                <td style="border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px; white-space: normal">
                                                                                                    <asp:Label ID="lblSRPro1" runat="server"></asp:Label>
                                                                                                </td>
                                                                                                <td style="border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;"></td>
                                                                                                <td style="border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;">
                                                                                                    <asp:Label ID="lblDefict1" runat="server" Visible="false"></asp:Label>
                                                                                                </td>

                                                                                                <td style="border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;">
                                                                                                    <asp:Label ID="lbldocPenality" runat="server"></asp:Label></td>
                                                                                                <td style="border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;">
                                                                                                    <asp:Label ID="Label1" runat="server"></asp:Label></td>



                                                                                            </tr>
                                                                                            <%-- <tr>
    <td style="border: 1px solid black;
  border-collapse: collapse;    height: 40px;padding: 10px;">
     2.
  </td>
    <td style="border: 1px solid black;
  border-collapse: collapse;    height: 40px;padding: 10px;">
डीड 
  </td>
  <td style="border: 1px solid black;
  border-collapse: collapse;    height: 40px;padding: 10px;"></td>
    <td style="border: 1px solid black;
  border-collapse: collapse;    height: 40px;padding: 10px;">
     
  </td>
    <td style="border: 1px solid black;
  border-collapse: collapse;    height: 40px;padding: 10px;">
 
  </td>
  <td style="border: 1px solid black;
  border-collapse: collapse;    height: 40px;padding: 10px;"> &nbsp</td>

  
 
  </tr>
  <tr>
    <td style="border: 1px solid black;
  border-collapse: collapse;    height: 40px;padding: 10px;">
     3.
  </td>
    <td style="border: 1px solid black;
  border-collapse: collapse;    height: 40px;padding: 10px;">
 इंस्ट्रूमेंट
  </td>
  <td style="border: 1px solid black;
  border-collapse: collapse;    height: 40px;padding: 10px;"></td>
    <td style="border: 1px solid black;
  border-collapse: collapse;    height: 40px;padding: 10px;">
   
  </td>
    <td style="border: 1px solid black;
  border-collapse: collapse;    height: 40px;padding: 10px;">
 
  </td>
  <td style="border: 1px solid black;
  border-collapse: collapse;    height: 40px;padding: 10px;"> &nbsp</td>

  
 
  </tr>--%>

                                                                                            <tr>
                                                                                                <td style="border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;">2.
                                                                                                </td>
                                                                                                <td style="border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;">दस्तावेज़ पंजीकरण संख्या
                                                                                                </td>
                                                                                                <td colspan="6" style="border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;">
                                                                                                    <asp:Label ID="lblRegNo1" runat="server"></asp:Label>
                                                                                                </td>


                                                                                            </tr>

                                                                                            <tr>
                                                                                                <td style="border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px; min-width: 60px;">3.
                                                                                                </td>
                                                                                                <td style="border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;">संपत्ति का मार्गदर्शिका मूल्य
                                                                                                </td>
                                                                                                <td style="border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;">
                                                                                                    <asp:Label ID="lblGuideValue1" runat="server"></asp:Label></td>
                                                                                                <td style="border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px; min-width: 60px;">
                                                                                                    <asp:Label ID="lblSROGuide1" runat="server"></asp:Label>
                                                                                                </td>
                                                                                                <td style="border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;">
                                                                                                    <asp:TextBox ID="txtPartyProGidVale" runat="server" Visible="false" OnTextChanged="txtPartyProGidVale_TextChanged"></asp:TextBox>
                                                                                                    <asp:Label ID="lblCOSGidVale" runat="server"></asp:Label>
                                                                                                    <%-- <asp:DropDownList runat="server">
                                    <asp:ListItem Text="MORTGAGE DEED"></asp:ListItem>
                                </asp:DropDownList>--%>
                                                                                                </td>
                                                                                                <td style="border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px; min-width: 60px;">
                                                                                                    <asp:Label ID="lblGuideDefict1" runat="server" Visible="false"></asp:Label>
                                                                                                </td>

                                                                                                <td style="border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px; min-width: 60px;">
                                                                                                    <asp:Label ID="lblGuidePenality" runat="server" Visible="false"></asp:Label></td>

                                                                                                <td style="border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;">
                                                                                                    <asp:Label ID="Label3" runat="server"></asp:Label></td>

                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td style="border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px; min-width: 60px;">4.
                                                                                                </td>
                                                                                                <td style="border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;">प्रतिफल राशि /प्रतिभूति राशि 
                                                                                                </td>
                                                                                                <td style="border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px; min-width: 60px;">
                                                                                                    <asp:Label ID="lblConValue1" runat="server"></asp:Label></td>
                                                                                                <td style="border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px; min-width: 60px;">
                                                                                                    <asp:Label ID="lblSRCon1" runat="server"></asp:Label>
                                                                                                </td>
                                                                                                <td style="border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;">
                                                                                                    <%--<asp:TextBox ID="txtPratifal" runat="server" Visible="false"></asp:TextBox>--%>
                                                                                                    <asp:TextBox ID="txtPratifal" Visible="false" CssClass="form-control" runat="server" MaxLength="6" Enabled="true" ClientIDMode="Static" AutoCompleteType="None" onkeypress='return restrictAlphabets(event)' autocomplete="off"></asp:TextBox>
                                                                                                    <%--  <asp:RequiredFieldValidator ID="RequiredFieldValidator7"  runat="server" ValidationGroup="AddCopyValidate" ForeColor="Red"
                                                                                        ControlToValidate="txtPratifal" ErrorMessage=" Field Required"></asp:RequiredFieldValidator>--%>
                                                                                                    <asp:Label ID="lblPratifal" runat="server"></asp:Label>
                                                                                                    <%-- <asp:DropDownList runat="server">
                                    <asp:ListItem Text="MORTGAGE DEED"></asp:ListItem>
                                </asp:DropDownList>--%>
                                                                                                </td>
                                                                                                <td style="border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px; min-width: 60px;">
                                                                                                    <asp:Label ID="lblConDefict1" runat="server" Visible="false"></asp:Label>
                                                                                                </td>

                                                                                                <td style="border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px; min-width: 60px;">
                                                                                                    <asp:Label ID="lblProPenality" runat="server" Visible="false"></asp:Label></td>

                                                                                                <td style="border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;">
                                                                                                    <asp:Label ID="Label4" runat="server"></asp:Label></td>

                                                                                            </tr>

                                                                                            <tr>
                                                                                                <td style="border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px; min-width: 60px;">5.
                                                                                                </td>
                                                                                                <td style="border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;">प्रिंसिपल स्टाम्प ड्यूटी
                                                                                                </td>
                                                                                                <td style="border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px; min-width: 60px;">
                                                                                                    <asp:Label ID="lblPrinStamDoc2" runat="server"></asp:Label></td>
                                                                                                <td style="border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px; min-width: 60px;">
                                                                                                    <asp:Label ID="lblPrinStampPro2" runat="server"></asp:Label>
                                                                                                </td>
                                                                                                <td style="border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;">
                                                                                                    <%--<asp:TextBox ID="txtPStampCOS" runat="server" Visible="false"></asp:TextBox>--%>
                                                                                                    <asp:TextBox ID="txtPStampCOS" Visible="false" CssClass="form-control" runat="server" MaxLength="6" Enabled="true" ClientIDMode="Static" AutoCompleteType="None" onkeypress='return restrictAlphabets(event)' autocomplete="off"></asp:TextBox>
                                                                                                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator6"  runat="server" ValidationGroup="AddCopyValidate" ForeColor="Red"
                                                                                        ControlToValidate="txtPStampCOS" ErrorMessage=" Field Required"></asp:RequiredFieldValidator>--%>
                                                                                                    <asp:Label ID="lblPStampCOS" runat="server"></asp:Label>
                                                                                                    <%-- <asp:DropDownList runat="server">
                                    <asp:ListItem Text="MORTGAGE DEED"></asp:ListItem>
                                </asp:DropDownList>--%>
                                                                                                </td>
                                                                                                <td style="border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px; min-width: 60px;">
                                                                                                    <asp:Label ID="lblPrincipledeficit" runat="server" Visible="false"></asp:Label>
                                                                                                </td>

                                                                                                <td style="border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px; min-width: 60px;">
                                                                                                    <asp:Label ID="lblStampPenality" runat="server" Visible="false"></asp:Label></td>

                                                                                                <td style="border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;">
                                                                                                    <asp:Label ID="Label5" runat="server"></asp:Label></td>

                                                                                            </tr>

                                                                                            <tr>
                                                                                                <td style="border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px; min-width: 60px;">6.
                                                                                                </td>
                                                                                                <td style="border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;">मुन्सिपल स्टाम्प ड्यूटी
                                                                                                </td>
                                                                                                <td style="border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px; min-width: 60px;">
                                                                                                    <asp:Label ID="lblMStamp2" runat="server"></asp:Label></td>
                                                                                                <td style="border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px; min-width: 60px;">
                                                                                                    <asp:Label ID="lblMStampPro2" runat="server"></asp:Label>
                                                                                                </td>
                                                                                                <td style="border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;">
                                                                                                    <%--<asp:TextBox ID="txtStampMuniciple" runat="server" Visible="false"></asp:TextBox>--%>
                                                                                                    <asp:TextBox ID="txtStampMuniciple" Visible="false" CssClass="form-control" runat="server" MaxLength="6" Enabled="true" ClientIDMode="Static" AutoCompleteType="None" onkeypress='return restrictAlphabets(event)' autocomplete="off"></asp:TextBox>
                                                                                                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator8"  runat="server" ValidationGroup="AddCopyValidate" ForeColor="Red"
                                                                                        ControlToValidate="txtStampMuniciple" ErrorMessage=" Field Required"></asp:RequiredFieldValidator>--%>
                                                                                                    <asp:Label ID="lblStampMuniciple" runat="server"></asp:Label>
                                                                                                    <%-- <asp:DropDownList runat="server">
                                    <asp:ListItem Text="MORTGAGE DEED"></asp:ListItem>
                                </asp:DropDownList>--%>
                                                                                                </td>
                                                                                                <td style="border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px; min-width: 60px;">
                                                                                                    <asp:Label ID="lblMuncipleDeficit" runat="server" Visible="false"></asp:Label>
                                                                                                </td>

                                                                                                <td style="border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px; min-width: 60px;">
                                                                                                    <asp:Label ID="lblMunciplePenality" runat="server" Visible="false"></asp:Label></td>


                                                                                                <td style="border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;">
                                                                                                    <asp:Label ID="Label6" runat="server"></asp:Label></td>
                                                                                            </tr>

                                                                                            <tr>
                                                                                                <td style="border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px; min-width: 60px;">7.
                                                                                                </td>
                                                                                                <td style="border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;">जनपद एसडी 
                                                                                                </td>
                                                                                                <td style="border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px; min-width: 60px;">
                                                                                                    <asp:Label ID="lblJanpad2" runat="server"></asp:Label></td>
                                                                                                <td style="border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px; min-width: 60px;">
                                                                                                    <asp:Label ID="lblJanpadPro2" runat="server"></asp:Label>
                                                                                                </td>
                                                                                                <td style="border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;">
                                                                                                    <%--<asp:TextBox ID="txtJanpad" runat="server" Visible="false"></asp:TextBox>--%>
                                                                                                    <asp:TextBox ID="txtJanpad" Visible="false" CssClass="form-control" runat="server" MaxLength="6" Enabled="true" ClientIDMode="Static" AutoCompleteType="None" onkeypress='return restrictAlphabets(event)' autocomplete="off"></asp:TextBox>
                                                                                                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator9"  runat="server" ValidationGroup="AddCopyValidate" ForeColor="Red"
                                                                                        ControlToValidate="txtJanpad" ErrorMessage=" Field Required"></asp:RequiredFieldValidator>--%>
                                                                                                    <asp:Label ID="lblJanpadD" runat="server"></asp:Label>
                                                                                                    <%-- <asp:DropDownList runat="server">
                                    <asp:ListItem Text="MORTGAGE DEED"></asp:ListItem>
                                </asp:DropDownList>--%>
                                                                                                </td>
                                                                                                <td style="border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px; min-width: 60px;">
                                                                                                    <asp:Label ID="lblJanpadDe" runat="server" Visible="false"></asp:Label>
                                                                                                </td>

                                                                                                <td style="border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px; min-width: 60px;">
                                                                                                    <asp:Label ID="lblJanpadPenality" runat="server" Visible="false"></asp:Label></td>

                                                                                                <td style="border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;">
                                                                                                    <asp:Label ID="Label7" runat="server"></asp:Label></td>

                                                                                            </tr>

                                                                                            <tr>
                                                                                                <td style="border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px; min-width: 60px;">8.
                                                                                                </td>
                                                                                                <td style="border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;">उपकर 
                                                                                                </td>
                                                                                                <td style="border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px; min-width: 60px;">
                                                                                                    <asp:Label ID="lblUpkarDoc2" runat="server"></asp:Label></td>
                                                                                                <td style="border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px; min-width: 60px;">
                                                                                                    <asp:Label ID="lblUpkarPro2" runat="server"></asp:Label>
                                                                                                </td>
                                                                                                <td style="border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;">
                                                                                                    <%--<asp:TextBox ID="txtupkar" runat="server" Visible="false" AutoPostBack="true" OnTextChanged="txtupkar_TextChanged"></asp:TextBox>--%>
                                                                                                    <asp:TextBox ID="txtupkar" Visible="false" AutoPostBack="true" OnTextChanged="txtupkar_TextChanged" CssClass="form-control" runat="server" MaxLength="6" Enabled="true" ClientIDMode="Static" AutoCompleteType="None" onkeypress='return restrictAlphabets(event)' autocomplete="off"></asp:TextBox>
                                                                                                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator10"  runat="server" ValidationGroup="AddCopyValidate" ForeColor="Red"
                                                                                        ControlToValidate="txtupkar" ErrorMessage=" Field Required"></asp:RequiredFieldValidator>--%>
                                                                                                    <asp:Label ID="lblupkar" runat="server"></asp:Label>
                                                                                                    <%--<asp:DropDownList runat="server">
                                    <asp:ListItem Text="MORTGAGE DEED"></asp:ListItem>
                                </asp:DropDownList>--%>
                                                                                                </td>
                                                                                                <td style="border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px; min-width: 60px;">
                                                                                                    <asp:Label ID="lblUpkarDe" runat="server" Visible="false"></asp:Label>
                                                                                                </td>

                                                                                                <td style="border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px; min-width: 60px;">
                                                                                                    <asp:Label ID="lblUpkarPenality" runat="server" Visible="false"></asp:Label></td>
                                                                                                <td style="border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;">
                                                                                                    <asp:Label ID="Label8" runat="server"></asp:Label></td>


                                                                                            </tr>


                                                                                            <tr>
                                                                                                <td style="border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px; min-width: 60px;">9.
                                                                                                </td>
                                                                                                <td style="border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;">कुल स्टाम्प ड्यूटी 
                                                                                                </td>
                                                                                                <td style="border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px; min-width: 60px;">
                                                                                                    <asp:Label ID="lblPaid_TStamp_Party_evaul" runat="server"></asp:Label></td>
                                                                                                <td style="border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px; min-width: 60px;">
                                                                                                    <asp:Label ID="lblPaid_TStamp_SR_evaul" runat="server"></asp:Label>
                                                                                                </td>
                                                                                                <td style="border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;">
                                                                                                    <%--<asp:TextBox ID="txtupkar" runat="server" Visible="false" AutoPostBack="true" OnTextChanged="txtupkar_TextChanged"></asp:TextBox>--%>
                                                                                                    <asp:TextBox ID="TextBox6" Visible="false" AutoPostBack="true" OnTextChanged="txtupkar_TextChanged" CssClass="form-control" runat="server" MaxLength="6" Enabled="true" ClientIDMode="Static" AutoCompleteType="None" onkeypress='return restrictAlphabets(event)' autocomplete="off"></asp:TextBox>
                                                                                                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator10"  runat="server" ValidationGroup="AddCopyValidate" ForeColor="Red"
                                                                                        ControlToValidate="txtupkar" ErrorMessage=" Field Required"></asp:RequiredFieldValidator>--%>
                                                                                                    <asp:Label ID="lblPaid_TStamp_COS_evaul" runat="server"></asp:Label>
                                                                                                    <%--<asp:DropDownList runat="server">
                                    <asp:ListItem Text="MORTGAGE DEED"></asp:ListItem>
                                </asp:DropDownList>--%>
                                                                                                </td>
                                                                                                <td style="border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px; min-width: 60px;">
                                                                                                    <asp:Label ID="lblPaid_TStamp_Deficit_evaul" runat="server" Visible="false"></asp:Label>
                                                                                                </td>

                                                                                                <td style="border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px; min-width: 60px;">
                                                                                                    <asp:Label ID="lblPaid_TStamp_Penality_evaul" runat="server" Visible="false"></asp:Label></td>
                                                                                                <td style="border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;">
                                                                                                    <asp:Label ID="lblPaid_TStamp_Total_evaul" runat="server"></asp:Label></td>


                                                                                            </tr>


                                                                                            <tr>
                                                                                                <td style="border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px; min-width: 60px;">10.
                                                                                                </td>
                                                                                                <td style="border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;">छूट प्राप्त राशि 
                                                                                                </td>
                                                                                                <td style="border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px; min-width: 60px;">
                                                                                                    <asp:Label ID="lblEx_Party" runat="server"></asp:Label></td>
                                                                                                <td style="border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px; min-width: 60px;">
                                                                                                    <asp:Label ID="lblEx_SR" runat="server"></asp:Label>
                                                                                                </td>
                                                                                                <td style="border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;">
                                                                                                    <asp:TextBox ID="TextBox1" runat="server" Visible="false"></asp:TextBox>
                                                                                                    <asp:Label ID="lblEx_Order" runat="server"></asp:Label>

                                                                                                </td>
                                                                                                <td style="border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px; min-width: 60px;">
                                                                                                    <asp:Label ID="lblEx_Deficit" runat="server" Visible="false"></asp:Label>
                                                                                                </td>

                                                                                                <td style="border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px; min-width: 60px;">
                                                                                                    <asp:Label ID="lblEx_Penality" runat="server" Visible="false"></asp:Label></td>

                                                                                                <td style="border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;">
                                                                                                    <asp:Label ID="lblEx_Total" runat="server"></asp:Label></td>

                                                                                            </tr>

                                                                                            <tr>
                                                                                                <td style="border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px; min-width: 60px;">11.
                                                                                                </td>
                                                                                                <td style="border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;">भुगतान की गई स्टाम्प ड्यूटी
                                                                                                </td>
                                                                                                <td style="border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px; min-width: 60px;">
                                                                                                    <asp:Label ID="lbl_Stamp_Paid_Party" runat="server"></asp:Label></td>
                                                                                                <td style="border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px; min-width: 60px;">
                                                                                                    <asp:Label ID="lbl_Stamp_Paid_SR" runat="server"></asp:Label>
                                                                                                </td>
                                                                                                <td style="border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;">
                                                                                                    <asp:TextBox ID="TextBox7" runat="server" Visible="false"></asp:TextBox>
                                                                                                    <asp:Label ID="lbl_Stamp_Paid_COS" runat="server"></asp:Label>

                                                                                                </td>
                                                                                                <td style="border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px; min-width: 60px;">
                                                                                                    <asp:Label ID="lbl_Stamp_Paid_Deficit" runat="server" Visible="false"></asp:Label>
                                                                                                </td>

                                                                                                <td style="border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px; min-width: 60px;">
                                                                                                    <asp:Label ID="lbl_Stamp_Paid_Penality" runat="server" Visible="false"></asp:Label></td>

                                                                                                <td style="border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;">
                                                                                                    <asp:Label ID="lbl_Stamp_Paid_Total" runat="server"></asp:Label></td>

                                                                                            </tr>

                                                                                            <tr>
                                                                                                <td style="border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px; min-width: 60px;">12.
                                                                                                </td>
                                                                                                <td style="border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;">शुद्ध स्टाम्प ड्यूटी 
                                                                                                </td>
                                                                                                <td style="border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px; min-width: 60px;">
                                                                                                    <asp:Label ID="lblTStamDoc2" runat="server"></asp:Label></td>
                                                                                                <td style="border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px; min-width: 60px;">
                                                                                                    <asp:Label ID="lblTStamppro2" runat="server"></asp:Label>
                                                                                                </td>
                                                                                                <td style="border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;">
                                                                                                    <asp:TextBox ID="txtToralStamp" runat="server" Visible="false"></asp:TextBox>
                                                                                                    <asp:Label ID="lblToralStamp" runat="server"></asp:Label>

                                                                                                </td>
                                                                                                <td style="border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px; min-width: 60px;">
                                                                                                    <asp:Label ID="lblTStampdeficit2" runat="server" Visible="false"></asp:Label>
                                                                                                </td>

                                                                                                <td style="border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px; min-width: 60px;">
                                                                                                    <asp:Label ID="lblTstampPenality" runat="server" Visible="false"></asp:Label></td>

                                                                                                <td style="border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;">
                                                                                                    <asp:Label ID="lblTotalDefePenality" runat="server"></asp:Label></td>

                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td style="border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px; min-width: 60px;">13.
                                                                                                </td>
                                                                                                <td style="border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;">रजिस्ट्रेशन फीस 
                                                                                                </td>
                                                                                                <td style="border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px; min-width: 60px;">
                                                                                                    <asp:Label ID="lblTRegDoc2" runat="server"></asp:Label></td>
                                                                                                <td style="border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px; min-width: 60px;">
                                                                                                    <asp:Label ID="lblTRegPro2" runat="server"></asp:Label>
                                                                                                </td>
                                                                                                <td style="border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;">
                                                                                                    <asp:TextBox ID="txtRegFee" runat="server" Visible="false"></asp:TextBox>
                                                                                                    <asp:Label ID="lblRegFee" runat="server"></asp:Label>
                                                                                                    <%--<asp:DropDownList runat="server">
                                    <asp:ListItem Text="MORTGAGE DEED"></asp:ListItem>
                                </asp:DropDownList>--%>
                                                                                                </td>
                                                                                                <td style="border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px; min-width: 60px;">
                                                                                                    <asp:Label ID="lblTRegDeficit2" runat="server" Visible="false"></asp:Label>
                                                                                                </td>

                                                                                                <td style="border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px; min-width: 60px;">
                                                                                                    <asp:Label ID="lblRegPenality" runat="server" Visible="false"></asp:Label></td>

                                                                                                <td style="border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;">
                                                                                                    <asp:Label ID="Label10" runat="server"></asp:Label></td>

                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td style="border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px; min-width: 60px;">14.
                                                                                                </td>
                                                                                                <td style="border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;">छूट प्राप्त राशि 
                                                                                                </td>
                                                                                                <td style="border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px; min-width: 60px;">
                                                                                                    <asp:Label ID="lblRegExemParty" runat="server"></asp:Label></td>
                                                                                                <td style="border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px; min-width: 60px;">
                                                                                                    <asp:Label ID="lblRegExemSR" runat="server"></asp:Label>
                                                                                                </td>
                                                                                                <td style="border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;">
                                                                                                    <asp:TextBox ID="TextBox2" runat="server" Visible="false"></asp:TextBox>
                                                                                                    <asp:Label ID="lblRegExem" runat="server"></asp:Label>

                                                                                                </td>
                                                                                                <td style="border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px; min-width: 60px;">
                                                                                                    <asp:Label ID="Label13" runat="server" Visible="false"></asp:Label>
                                                                                                </td>

                                                                                                <td style="border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px; min-width: 60px;">
                                                                                                    <asp:Label ID="Label14" runat="server" Visible="false"></asp:Label></td>

                                                                                                <td style="border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;">
                                                                                                    <asp:Label ID="Label15" runat="server"></asp:Label></td>

                                                                                            </tr>

                                                                                            <tr>
                                                                                                <td style="border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px; min-width: 60px;">15.
                                                                                                </td>
                                                                                                <td style="border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;">भुगतान की गई रजिस्ट्रेशन फीस 
                                                                                                </td>
                                                                                                <td style="border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px; min-width: 60px;">
                                                                                                    <asp:Label ID="lbl_Reg_Paid_Party" runat="server"></asp:Label></td>
                                                                                                <td style="border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px; min-width: 60px;">
                                                                                                    <asp:Label ID="lbl_Reg_Paid_SR" runat="server"></asp:Label>
                                                                                                </td>
                                                                                                <td style="border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;">
                                                                                                    <asp:TextBox ID="TextBox8" runat="server" Visible="false"></asp:TextBox>
                                                                                                    <asp:Label ID="lbl_Reg_Paid_COS" runat="server"></asp:Label>

                                                                                                </td>
                                                                                                <td style="border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px; min-width: 60px;">
                                                                                                    <asp:Label ID="lbl_Reg_Paid_Deficit" runat="server" Visible="false"></asp:Label>
                                                                                                </td>

                                                                                                <td style="border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px; min-width: 60px;">
                                                                                                    <asp:Label ID="lbl_Reg_Paid_Penality" runat="server" Visible="false"></asp:Label></td>

                                                                                                <td style="border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;">
                                                                                                    <asp:Label ID="lbl_Reg_Paid_Total" runat="server"></asp:Label></td>

                                                                                            </tr>

                                                                                            <tr>
                                                                                                <td style="border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px; min-width: 60px;">16.
                                                                                                </td>
                                                                                                <td style="border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;">शुद्ध रजिस्ट्रेशन फीस 
                                                                                                </td>
                                                                                                <td style="border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px; min-width: 60px;">
                                                                                                    <asp:Label ID="lblNetRegParty" runat="server"></asp:Label></td>
                                                                                                <td style="border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px; min-width: 60px;">
                                                                                                    <asp:Label ID="lblNetRegSR" runat="server"></asp:Label>
                                                                                                </td>
                                                                                                <td style="border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;">
                                                                                                    <asp:TextBox ID="TextBox4" runat="server" Visible="false"></asp:TextBox>
                                                                                                    <asp:Label ID="lblNetRegFee" runat="server"></asp:Label>
                                                                                                    <%--<asp:DropDownList runat="server">
                                    <asp:ListItem Text="MORTGAGE DEED"></asp:ListItem>
                                </asp:DropDownList>--%>
                                                                                                </td>
                                                                                                <td style="border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px; min-width: 60px;">
                                                                                                    <asp:Label ID="lblNetRegDeficit" runat="server"></asp:Label>
                                                                                                </td>

                                                                                                <td style="border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px; min-width: 60px;">
                                                                                                    <asp:Label ID="Label24" runat="server" Visible="false"></asp:Label></td>

                                                                                                <td style="border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;">
                                                                                                    <asp:Label ID="lblTotalRegfee" runat="server"></asp:Label></td>

                                                                                            </tr>



                                                                                            <tr>
                                                                                                <td style="border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px; min-width: 60px;">17.
                                                                                                </td>
                                                                                                <td style="border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;">कुल राशि
                                                                                                </td>
                                                                                                <td style="border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px; min-width: 60px;">
                                                                                                    <asp:Label ID="lblTAmtParty2" runat="server"></asp:Label></td>
                                                                                                <td style="border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px; min-width: 60px;">
                                                                                                    <asp:Label ID="lblTAmtSRO2" runat="server"></asp:Label>
                                                                                                </td>
                                                                                                <td style="border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;">
                                                                                                    <asp:TextBox ID="txtTotalAmt" runat="server" Visible="false"></asp:TextBox>
                                                                                                    <asp:Label ID="lblTotalAmt" runat="server"></asp:Label>
                                                                                                    <%-- <asp:DropDownList runat="server">
                                    <asp:ListItem Text="MORTGAGE DEED"></asp:ListItem>
                                </asp:DropDownList>--%>
                                                                                                </td>
                                                                                                <td style="border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px; min-width: 60px;">
                                                                                                    <asp:Label ID="lblTAmtDeficit2" runat="server" Visible="false"></asp:Label>
                                                                                                </td>

                                                                                                <td style="border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px; min-width: 60px;">
                                                                                                    <asp:Label ID="TPenality" runat="server" Visible="false"></asp:Label></td>

                                                                                                <td style="border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;">
                                                                                                    <asp:Label ID="lblTotalPayable" runat="server"></asp:Label></td>

                                                                                            </tr>
                                                                                        </tbody>
                                                                                    </table>
                                                                                </div>
                                                                            </div>
                                                                        </ContentTemplate>
                                                                    </asp:UpdatePanel>
                                                                    <div style="padding: 20px 0 0 0;">
                                                                        <h6 style="text-align: justify; width: 100%; font-size: 15px;">अंतिम टिप्पणी :</h6>
                                                                        <p id="pContent3" style="text-align: justify">
                                                                        </p>
                                                                        <asp:Label ID="lblFinalRemark" runat="server" Visible="false"></asp:Label>
                                                                    </div>

                                                                    
                                                                    <asp:Panel ID="PnlPratilipi" runat="server" Visible="false" class="text-left">
                                                                        <b>
                                                                            <asp:Label ID="lblPratilip" runat="server" Text="प्रतिलिपि"></asp:Label></b>
                                                                        <asp:GridView ID="GrdAddCopy_Details" BorderStyle="None" CssClass="table table-bordered table-condensed table-striped table-hover"
                                                                            runat="server" CellPadding="4" ForeColor="#333333" GridLines="None" AutoGenerateColumns="False" Visible="true" Width="100%" EmptyDataText="No recoed found">
                                                                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                                                            <Columns>
                                                                                <asp:TemplateField HeaderText="क्र." HeaderStyle-VerticalAlign="Top" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="false">
                                                                                    <ItemTemplate>
                                                                                        <itemtemplate><%#Container.DataItemIndex+1  %> </itemtemplate>
                                                                                    </ItemTemplate>
                                                                                    <ItemStyle Width="3%" HorizontalAlign="Center" />
                                                                                    <HeaderStyle Width="3%" HorizontalAlign="Center" />
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="सूचनार्थ प्रेषित/विवरण" HeaderStyle-Font-Bold="false">
                                                                                    <ItemTemplate>
                                                                                        <%#Eval("CopyContent") %>
                                                                                    </ItemTemplate>
                                                                                    <HeaderStyle Width="50%" HorizontalAlign="Left" />
                                                                                    <ItemStyle Width="50%" HorizontalAlign="Left" />
                                                                                </asp:TemplateField>


                                                                            </Columns>
                                                                            <EditRowStyle BackColor="#999999" />
                                                                            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />

                                                                            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                                                            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                                                            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                                                            <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                                                            <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                                                            <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                                                            <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                                                                        </asp:GridView>
                                                                    </asp:Panel>
                                                                     <br />
                                                                                <br />
                                                                            <b class="stamp">स्थान- न्यायालय कलेक्टर ऑफ स्टाम्प 
                                                                                <div class="tg-rf">
                                                                                    एवं जिला पंजीयक कार्यालय
                                                                                </div>
                                                                               <asp:Label ID="lblDROfficeName" runat="server"></asp:Label>
                                                                               
                                                                                <br />
                                                                                <br />
                                                                            </b>
                                                                </div>



                                                            </div>
                                                            <asp:Panel ID="pnlAddCopy" runat="server">
                                                                <div class="row">
                                                                    <div id="hearing1" style="width: 100%;">
                                                                        <div class="mycheck mul_check m-2">
                                                                            <h6><b style="padding-right: 12px;">क्या आप कोई प्रतिलिपि जोड़ना चाहेंगे।</b></h6>
                                                                            <div class="custom-control custom-radio mycb">
                                                                                <input class="custom-control-input" type="radio" id="yes" name="customRadio">
                                                                                <label for="yes" class="custom-control-label">हां </label>
                                                                            </div>
                                                                            <div class="custom-control custom-radio">
                                                                                <input class="custom-control-input" type="radio" id="no" name="customRadio" checked="checked">
                                                                                <label for="no" class="custom-control-label">नहीं</label>
                                                                            </div>
                                                                        </div>

                                                                        <div id="ifselectyes">
                                                                            <div id="addnewrow1">
                                                                                <br>

                                                                                <div class="row">
                                                                                    <div class="col-12">
                                                                                        <div class="form-group">
                                                                                            <label>प्रतिलिपि प्रेषित किये जाने हेतु जानकारी दें</label>
                                                                                            <%--<asp:TextBox ID="txt" CssClass="form-control" runat="server" placeholder="Enter Content"></asp:TextBox>--%>
                                                                                            <asp:TextBox ID="txtTran" ClientIDMode="Static" runat="server" CssClass="form-control" placeholder="प्रतिलिपि प्रेषित करने सम्बंदित का नाम एवं सम्बंधित विवरण लिखे" Style="font-family: Verdana; font-size: Medium; height: 79px;"></asp:TextBox>
                                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ValidationGroup="AddCopyValidate" ForeColor="Red"
                                                                                                ControlToValidate="txtTran" ErrorMessage="प्रतिलिपि दर्ज करें।"></asp:RequiredFieldValidator>
                                                                                        </div>
                                                                                    </div>
                                                                                </div>

                                                                                <div class="row mt-xl-n4 mb-xl-n3">
                                                                                    <div class="col-12">
                                                                                        <div class="form-group">
                                                                                            <label>प्रतिलिपि भेजने वाले व्यक्ति का विवरण दर्ज करें।</label>
                                                                                        </div>
                                                                                    </div>
                                                                                </div>

                                                                                <div class="row">

                                                                                    <div class="col-12">
                                                                                        <asp:TextBox ID="txtCopyName" CssClass="form-control" runat="server" placeholder="व्यक्ति का विवरण दर्ज करें"></asp:TextBox>
                                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ValidationGroup="AddCopyValidate" ForeColor="Red"
                                                                                            ControlToValidate="txtCopyName" ErrorMessage="व्यक्ति का विवरण दर्ज करें।"></asp:RequiredFieldValidator>

                                                                                    </div>
                                                                                </div>
                                                                                <div class="row">
                                                                                    <div class="col-3">

                                                                                        <asp:TextBox ID="txtCopyEmail" CssClass="form-control" runat="server" TextMode="Email" placeholder="ईमेल दर्ज करें"></asp:TextBox>
                                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ValidationGroup="AddCopyValidate"
                                                                                            ControlToValidate="txtCopyEmail" ErrorMessage="ईमेल दर्ज करें।" ForeColor="Red"></asp:RequiredFieldValidator>
                                                                                        <asp:RegularExpressionValidator ID="REVEmail" runat="server" ValidationGroup="AddCopyValidate" ControlToValidate="txtCopyEmail" ErrorMessage="सही ईमेल दर्ज करें" ForeColor="Red" ValidationExpression="^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$"></asp:RegularExpressionValidator>
                                                                                    </div>

                                                                                    <div class="col-3">
                                                                                        <%--<asp:TextBox ID="txtMobile" CssClass="form-control" runat="server" MaxLength="10" Enabled="true"  onkeypress="return NumberOnly(event)"  AutoCompleteType="None" autocomplete="off" placeholder="Enter Mobile"></asp:TextBox>--%>
                                                                                        <asp:TextBox ID="txtMobile" CssClass="form-control" runat="server" MaxLength="10" Enabled="true" ClientIDMode="Static" AutoCompleteType="None" onkeypress='return restrictAlphabets(event)' autocomplete="off" placeholder="मोबाइल नंबर दर्ज करें"></asp:TextBox>
                                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ValidationGroup="AddCopyValidate" ForeColor="Red"
                                                                                            ControlToValidate="txtMobile" ErrorMessage="मोबाइल नंबर दर्ज करें।"></asp:RequiredFieldValidator>

                                                                                    </div>

                                                                                    <div class="col-3">
                                                                                        <asp:TextBox ID="txtWhatsApp" CssClass="form-control" runat="server" MaxLength="10" Enabled="true" ClientIDMode="Static" AutoCompleteType="None" onkeypress='return restrictAlphabets(event)' autocomplete="off" placeholder="व्हॉट्स एप्प नंबर दर्ज करें"></asp:TextBox>
                                                                                        <%--<asp:TextBox ID="txtWhatsApp" CssClass="form-control" runat="server" MaxLength="10" Enabled="true"  onkeypress="return NumberOnly(event)"  AutoCompleteType="None" autocomplete="off"  placeholder="Enter WhatsApp No"></asp:TextBox>--%>
                                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ValidationGroup="AddCopyValidate" ForeColor="Red"
                                                                                            ControlToValidate="txtWhatsApp" ErrorMessage="व्हॉट्स एप्प नंबर दर्ज करें।"></asp:RequiredFieldValidator>
                                                                                    </div>

                                                                                    <div class="col-3 text-center">
                                                                                        <asp:Button ID="btnSaveCopy" runat="server" class="btn btn-success" ValidationGroup="AddCopyValidate" OnClick="btnSaveCopy_Click"
                                                                                            Text="Add" />
                                                                                        <%--<button type="button" class="btn btn-success">Save</button>--%>
                                                                                        <%--<button type="button" class="btn btn-info float-right ">Add More </button>--%>
                                                                                    </div>
                                                                                </div>


                                                                            </div>


                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </asp:Panel>




                                                                           

                                                            <div class="col-sm-4 ml-lg-auto mt-2" style="text-align: end;">
                                                                <asp:Button ID="btnDraft" runat="server" class="btn btn-success" Text="Draft Final Order" OnClick="btnDraft_Click" ClientIDMode="static" OnClientClick="return ValidateDecision()" />

                                                                <asp:Button ID="btnFinalSubmit" runat="server" class="btn btn-success" Visible="false" Text="Save Final Order" OnClick="btnFinalSubmit_Click" />
                                                            </div>

                                                            <div id="hearing">

                                                                <%--   <div class="mycheck mul_check">
                                                                    <div class="custom-control custom-radio mycb">
                                                                        <input class="custom-control-input" type="radio" id="yes" name="customRadio">
                                                                        <label for="yes" class="custom-control-label">Yes</label>
                                                                    </div>
                                                                    <div class="custom-control custom-radio">
                                                                        <input class="custom-control-input" type="radio" id="no" name="customRadio">
                                                                        <label for="no" class="custom-control-label">No</label>
                                                                    </div>
                                                                </div>--%>
                                                                <br>

                                                                <div id="">
                                                                    <asp:Panel ID="pnlEsignDSC" runat="server" Visible="false">
                                                                        <div class="certi_info bg-light m16">eSign / DSC</div>
                                                                        <br />
                                                                        <div class="form-group row">
                                                                            <div class="col-sm-3">
                                                                                <%-- <select type="text" class="form-control" id="input7">
                                                                                    <option>Select</option>
                                                                                    <option>Aadhar eSign </option>
                                                                                    <option>DSC</option>
                                                                                </select>--%>
                                                                                <asp:Label runat="server" Visible="false" ID="lblStatus" Text=""></asp:Label>
                                                                                <asp:DropDownList ID="ddl_SignOption" runat="server" onChange="SetSignOption(this.value)" CssClass="form-control">
                                                                                     <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                                                                    <asp:ListItem Text="Aadhar eSign (CDAC)" Value="1"></asp:ListItem>
                                                                                    <asp:ListItem Text="Aadhar eSign (eMudra)" Value="3"></asp:ListItem>
                                                                                    <asp:ListItem Text="DSC" Value="2"></asp:ListItem>
                                                                                    <%--<asp:ListItem Text="(2) - Digital Signature (using DSC Dongle)" Value="2"></asp:ListItem>
                                                                                    <asp:ListItem Text="(3) - Download  Sign and Upload" Value="3"></asp:ListItem>--%>
                                                                                </asp:DropDownList>
                                                                            </div>

                                                                             <div class="col-sm-3">
                                                                                <asp:DropDownList ID="ddleAuthMode" ClientIDMode="Static" runat="server" CssClass="form-control">

                                                                                    <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                                                                    <asp:ListItem Text="OTP" Value="1"></asp:ListItem>
                                                                                    <asp:ListItem Text="Biometric" Value="2"></asp:ListItem>
                                                                                </asp:DropDownList>
                                                                                <%-- <asp:Label ID="Label1" runat="server"></asp:Label>
                                                                                <asp:TextBox ID="TextBox1" placeholder="Enter last 4 digit of Adhar Card" MaxLength="4" Enabled="true" onkeypress="return NumberOnly(event)" ClientIDMode="Static" CssClass="form-control" runat="server" AutoCompleteType="None" autocomplete="off"></asp:TextBox>--%>
                                                                            </div>
                                                                            <div class="col-sm-3">
                                                                                <asp:TextBox ID="TxtLast4Digit" placeholder="Enter last 4 digit of Adhar Card" MaxLength="4" Enabled="true" onkeypress="return NumberOnly(event)" ClientIDMode="Static" CssClass="form-control" runat="server" AutoCompleteType="None" autocomplete="off"></asp:TextBox>
                                                                            </div>
                                                                            <div class="col-sm-3">
                                                                                <%-- <button type="button" class="btn btn-success">eSign/DSC</button>--%>
                                                                                <asp:Label ID="hsmMsg" runat="server"></asp:Label>
                                                                                <asp:Button ID="btnEsignDSC" runat="server" OnClick="btnEsignDSC_Click" class="btn btn-success" Text="eSign/DSC" OnClientClick="return ValidateEsignRecord()" />
                                                                            </div>
                                                                        </div>
                                                                    </asp:Panel>
                                                                    <br>
                                                                    <asp:Panel ID="pnlSendOrder" Visible="false" runat="server">
                                                                        <div class="certi_info bg-light">Send Final Order Through </div>

                                                                        <%--  <div class="mycheck mul_check " style="margin: 6px 0px 6px 0;">
                                                                            <div class="custom-control custom-checkbox mycb">
                                                                                <input class="custom-control-input" type="checkbox" id="customCheckbox01" value="option1">
                                                                                <label for="customCheckbox01" class="custom-control-label">SMS</label>
                                                                            </div>
                                                                            <div class="custom-control custom-checkbox mycb">
                                                                                <input class="custom-control-input" type="checkbox" id="customCheckbox02" value="option1">
                                                                                <label for="customCheckbox02" class="custom-control-label">Email</label>
                                                                            </div>
                                                                            <div class="custom-control custom-checkbox">
                                                                                <input class="custom-control-input" type="checkbox" id="customCheckbox03" value="option1">
                                                                                <label for="customCheckbox03" class="custom-control-label">WhatsApp</label>
                                                                            </div>
                                                                        </div>--%>

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

                                                                        <%-- <div class="table-responsive listtabl">
                                                                        <table id="party" class="table table-bordered table-striped">
                                                                            <thead>
                                                                                <tr>
                                                                                    <th>SNo.</th>
                                                                                    <th>Name</th>
                                                                                    <th>WhatsApp</th>
                                                                                    <th>SMS</th>
                                                                                    <th>Email</th>
                                                                                </tr>
                                                                            </thead>
                                                                            <tbody>
                                                                                <tr>
                                                                                    <td>1</td>
                                                                                    <td>Seller </td>
                                                                                    <td>4564567777</td>
                                                                                    <td>4564567777</td>
                                                                                    <td>xyx@gmail.com </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>2</td>
                                                                                    <td>Seller </td>
                                                                                    <td>4564567777</td>
                                                                                    <td>4564567777</td>
                                                                                    <td>Dim12@gmail.com</td>
                                                                                </tr>
                                                                            </tbody>
                                                                        </table>
                                                                    </div>--%>
                                                                        <div class="table-responsive">
                                                                            <asp:GridView ID="grdPartyDisplay" CssClass="table table-bordered table-condensed table-striped table-hover"
                                                                                DataKeyNames="Party_ID"
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

                                                                                    <asp:BoundField HeaderText="Name" DataField="Name" HeaderStyle-Font-Bold="false" />
                                                                                    <asp:BoundField HeaderText="SMS" DataField="SMS" HeaderStyle-Font-Bold="false" />
                                                                                    <asp:BoundField HeaderText="WhatsAPP" DataField="WhatsAPP" HeaderStyle-Font-Bold="false" />
                                                                                    <asp:BoundField HeaderText="Email" DataField="Email" HeaderStyle-Font-Bold="false" />



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
                                                                        <asp:Button ID="btnSendFinalOrder" runat="server" class="btn btn-success" OnClick="btnSendFinalOrder_Click" Text="Send Final Order" />
                                                                        <br>
                                                                    </asp:Panel>
                                                                </div>

                                                            </div>


                                                        </div>


                                                        <div class="tab-pane " id="custom-tabs-one-editFinalOrder" role="tabpanel"
                                                            aria-labelledby="Proceeding">
                                                            <div id="editproceeding1">
                                                                <textarea id="summernote" runat="server" clientidmode="Static" onblur="onLeave(this)"></textarea>
                                                                <div class="form-group row">
                                                                    <div class="col-sm-4">
                                                                        <a data-toggle="pill" id="save2" onclick="AddNotice()"
                                                                            href="#custom-tabs-one-Draft_Order" aria-controls="custom-tabs-one-Draft_Order" role="tabpanel" aria-selected="true" class="btn btn-success"
                                                                            aria-labelledby="Draft_Order">Save</a>
                                                                        <%--<asp:Button ID="btnSubmit" runat="server" Text="Save" class="btn btn-success" OnClick="" ClientIDMode="static" OnClientClick="return ValidateRecord()" />--%>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="tab-pane " id="custom-tabs-one-PartyReply" role="tabpanel"
                                                            aria-labelledby="Proceeding">
                                                            <div id="">
                                                                <textarea id="txtSRProposal" runat="server" clientidmode="Static" onblur="onLeave(this)"></textarea>
                                                                <div class="form-group row">
                                                                    <div class="col-sm-4">
                                                                        <a data-toggle="pill" id="save3" onclick="AddNotice()"
                                                                            href="#custom-tabs-one-Draft_Order" aria-controls="custom-tabs-one-Draft_Order" role="tabpanel" aria-selected="true" class="btn btn-success"
                                                                            aria-labelledby="Draft_Order">Save</a>
                                                                        <%--<asp:Button ID="btnSubmit" runat="server" Text="Save" class="btn btn-success" OnClick="" ClientIDMode="static" OnClientClick="return ValidateRecord()" />--%>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="tab-pane " id="custom-tabs-one-editCOSDecision" role="tabpanel"
                                                            aria-labelledby="Proceeding">
                                                            <div id="">
                                                                <textarea id="txtCOSDecision" runat="server" clientidmode="Static" onblur="onLeave(this)"></textarea>
                                                                <div class="form-group row">
                                                                    <div class="col-sm-4">
                                                                        <a data-toggle="pill" id="save4" onclick="AddNotice()"
                                                                            href="#custom-tabs-one-Draft_Order" aria-controls="custom-tabs-one-Draft_Order" role="tabpanel" aria-selected="true" class="btn btn-success"
                                                                            aria-labelledby="Draft_Order">Save</a>
                                                                        <%--<asp:Button ID="btnSubmit" runat="server" Text="Save" class="btn btn-success" OnClick="" ClientIDMode="static" OnClientClick="return ValidateRecord()" />--%>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="tab-pane " id="custom-tabs-one-editFinalDecision" role="tabpanel"
                                                            aria-labelledby="Proceeding">
                                                            <div id="">
                                                                <textarea id="txtFinalDecision" runat="server" clientidmode="Static" onblur="onLeave(this)"></textarea>
                                                                <div class="form-group row">
                                                                    <div class="col-sm-4">
                                                                        <a data-toggle="pill" id="save5" onclick="AddNotice()"
                                                                            href="#custom-tabs-one-Draft_Order" aria-controls="custom-tabs-one-Draft_Order" role="tabpanel" aria-selected="true" class="btn btn-success"
                                                                            aria-labelledby="Draft_Order">Save</a>
                                                                        <%--<asp:Button ID="btnSubmit" runat="server" Text="Save" class="btn btn-success" OnClick="" ClientIDMode="static" OnClientClick="return ValidateRecord()" />--%>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>


                                                    </div>
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
                                                            <!-- <li class="nav-item">
                                  <a class="nav-link" id="custom-tabs-two-profile-tab" data-toggle="pill"
                                    href="#custom-tabs-two-profile" role="tab" aria-controls="custom-tabs-two-profile"
                                    aria-selected="false">Profile</a>
                                </li> -->
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

                                                                                <h5>List of Documents </h5>
                                                                                <hr />

                                                                                <div id="List_pnl">
                                                                                    <asp:UpdatePanel runat="server">
                                                                                        <ContentTemplate>



                                                                                            <asp:GridView ID="grdSRDoc" CssClass="table table-bordered table-condensed table-striped table-hover"
                                                                                                DataKeyNames="App_ID"
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
                                                                                                    <asp:TemplateField HeaderText="Document Name" SortExpression="DocumentType" HeaderStyle-Font-Bold="false">
                                                                                                        <ItemTemplate>
                                                                                                            <%#Eval("DocumentType") %>
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
                                                                                                        </ItemTemplate>
                                                                                                        <HeaderStyle Width="10%" HorizontalAlign="Center" />

                                                                                                        <ItemStyle Width="10%" HorizontalAlign="Center" />
                                                                                                    </asp:TemplateField>

                                                                                                    <%--<asp:TemplateField HeaderText="Pages" HeaderStyle-Font-Bold="false">

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
                                                                                    <div id="View_PartyReply">
                                                                                        <textarea id="txtReadOnly" runat="server" style="height: 67px; width: 602px;" adonly="readonly" visible="false"></textarea>
                                                                                    </div>


                                                                                    <iframe id="ifrDisplay" class="embed-responsive-item" height="750"></iframe>
                                                                                </div>

                                                                                <%--<button type="button" class="btn btn-success float-right" data-toggle="modal" data-target="#exampleModalCenter">Attach </button>--%>
                                                                            </div>
                                                                            <div class="tab-pane fade active show" id="custom-tabs-four-profile" role="tabpanel" aria-labelledby="custom-tabs-four-profile-tab">

                                                                                <div>
                                                                                    <%--<img src="../dist/img/Doc_Sample_pic.jpg" style="width: 708px;" />--%>
                                                                                    <iframe id="ifRecent" runat="server" height='750'></iframe>

                                                                                </div>
                                                                            </div>
                                                                            <div class="tab-pane fade" id="custom-tabs-four-messages" role="tabpanel" aria-labelledby="custom-tabs-four-messages-tab">

                                                                                <iframe id="RecentdocPath" clientidmode="Static" runat="server" width='550' height='750'></iframe>
                                                                                <iframe id="ifProposal1" clientidmode="Static" runat="server" scrolling="auto" height='750'></iframe>
                                                                                <iframe id="RecentAttachedDoc" runat="server" width='550' height='750'></iframe>
                                                                                <iframe id="ifPDFViewerAll" clientidmode="Static" runat="server" scrolling="auto" height='750'></iframe>

                                                                            </div>
                                                                            <div class="tab-pane fade " id="custom-tabs-four-settings" role="tabpanel" aria-labelledby="custom-tabs-four-settings-tab">

                                                                                <div>
                                                                                    <iframe id="ifPrevious" clientidmode="Static" runat="server" scrolling="auto" height='750'></iframe>
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
        </div>

        <asp:HiddenField ID="hdnCOSOfficeNameHi1" runat="server" />
        <asp:HiddenField ID="hdnToDate" runat="server" />
        <asp:HiddenField runat="server" ClientIDMode="Static" ID="hdTocan" />
    </section>

    <asp:HiddenField ID="HiddenField1" runat="server" />
    <asp:HiddenField ID="HiddenField2" runat="server" />
    <asp:HiddenField runat="server" ClientIDMode="Static" ID="HiddenField3" />
    <asp:HiddenField ID="hdnbase64" ClientIDMode="Static" runat="server" />
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

            //BindTable();
            //LoadPdfFile();
            //ShowMessage();
            // BindOrderSheet();
        });

    </script>
    <div id="modal_dialog" style="display: none"></div>

</asp:Content>
