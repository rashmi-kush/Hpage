<%@ Page Title="" Language="C#" MasterPageFile="~/CoS/CoSMaster.Master" ValidateRequest="false" AutoEventWireup="true" CodeBehind="PendingFor_Final_Order.aspx.cs" Inherits="CMS_Sampada.CoS.PendingFor_Final_Order" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
      <script src="EngToHin_Js/google_jsapi.js" type="text/javascript"></script>
    <script src="EngToHin_Js/jquery.min.js" type="text/javascript"></script>

     <link href="../Styles/sweetalert.css" rel="stylesheet" />
    <script src="../Scripts/sweetalert.min.js"></script>
    <script src="../assets/js/3.3.1/sweetalert2@11.js"></script>
    <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.7/jquery.min.js" type="text/javascript"></script>
    <style>
        .htmldoc h6{
            font-weight: bold;
        }
       
    </style>
    <script type="text/javascript">
        function AddNotice() {

            document.getElementById("pContent").innerHTML = document.getElementById("summernote").value;
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

      </script>
      <script type="text/javascript">
          //function ShowDocList() {
          //    //alert("Hello");
          //    $("#pnl2").show();
          //    $("#pnl3").hide();
          //}
          function openPopup(PROPOSALPATH_FIRSTFORMATE) {
              //alert("Hello");
              //alert(PROPOSALPATH_FIRSTFORMATE);
              //debugger;
              $("#pnl2").hide();
              $("#pnl3").show();
              $('#ifrDisplay').attr('src', PROPOSALPATH_FIRSTFORMATE);


              //$('#myModalDoc').modal('show');
              //$('#ifrDisplay').attr('src', PROPOSALPATH_FIRSTFORMATE);


          }

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

             //alert("h");
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
                                <li class="nav-item"><a class="nav-link" href="Ordersheet">Create Order Sheet</a></li>
                                <li class="nav-item"><a class="nav-link" href="Notice">Notice</a></li>
                                <li class="nav-item"><a class="nav-link" href="ReportSeeking">Seek
                        Report</a></li>
                                <li class="nav-item"><a class="nav-link active" href="#Hearing" data-toggle="tab">Hearing</a>
                                </li>
                                <li class="nav-item"><a class="nav-link disabled" href="#EarlyHearing" data-toggle="tab">Early
                        Hearing</a>
                                </li>
                                <li class="nav-item"><a class="nav-link disabled" href="#SendToAppeal" data-toggle="tab">Send to
                        Appeal</a>
                                </li>
                                <li class="nav-item"><a class="nav-link " href="#SendBack" data-toggle="tab">Send Back</a>
                                </li>
                                <li class="nav-item"><a class="nav-link disabled" href="#Reply" data-toggle="tab">Reply</a></li>
                                <li class="nav-item"><a class="nav-link disabled" href="#Attachement"
                                    data-toggle="tab">Attachement</a></li>
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
                                                                aria-controls="custom-tabs-one-Draft_Order" aria-selected="true">Draft Order </a>
                                                        </li>
                                                        <li class="nav-item">
                                                            <a class="nav-link" id="Edit_Final_Order" data-toggle="pill"
                                                                href="#custom-tabs-one-editFinalOrder" role="tab"
                                                                aria-controls="custom-tabs-one-editFinalOrder" aria-selected="true">Edit SR Proposal</a>
                                                        </li>
                                                        <li class="nav-item">
                                                            <a class="nav-link" id="Edit_Party_Reply" data-toggle="pill"
                                                                href="#custom-tabs-one-PartyReply" role="tab"
                                                                aria-controls="custom-tabs-one-editPartyReply" aria-selected="true">Edit Party Reply</a>
                                                        </li>
                                                        <li class="nav-item">
                                                            <a class="nav-link" id="Edit_COS_Decision" data-toggle="pill"
                                                                href="#custom-tabs-one-editCOSDecision" role="tab"
                                                                aria-controls="custom-tabs-one-editCOS_Decision" aria-selected="true">Edit COS Decision</a>
                                                        </li>
                                                        <li class="nav-item">
                                                            <a class="nav-link" id="Edit_Final_Decision" data-toggle="pill"
                                                                href="#custom-tabs-one-editFinalDecision" role="tab"
                                                                aria-controls="custom-tabs-one-editFinalDecision" aria-selected="true">Edit Final Decision</a>
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
                                                                    <h3 style="font-size: 18px; margin: 0; font-weight: 600;">कलेक्टर ऑफ़ स्टाम्प,न्यायालय </h3>
                                                                    <h4 style="margin: 0; margin: 10px; font-size: 13px;">cos.bhopal@mp.gov.in</h4>
                                                                    <h2 style="margin: 0; margin: 10px; font-size: 16px;">भोपाल 2</h2>

                                                                    <h3 style="margin: 0; margin: 10px; font-size: 16px;">ISBT चेतक ब्रिज के पास, भोपाल (म.प्र.)</h3>
                                                                    <h2 style="margin: 0; margin: 10px; font-size: 16px;">आदेश </h2>
                                                                    <div>
                                                                        <h3 style="margin: 0; margin: 10px; font-size: 16px;">मध्य प्रदेश शासन</h3>
                                                                        <h2 style="margin: 0; margin: 10px; font-size: 16px;">विरुद्ध : </h2>
                                                                        <h2 style="margin: 0; margin: 10px; font-size: 16px;">पक्षकार : Sub Registrar</h2>
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
                                                                       <p> <asp:Label ID="lblOrderProceeding" runat="server" Style="width: 100%; font-size: 14px;" rows="4"></asp:Label></p>
                                                                        <%-- <textarea > उप पंजीयक भोपाल-2 द्वारा एक पंजीकृत दस्तावेज दान पत्र विलेख क्रमांक दिनांक 25-05-2023 को न्यून मुद्रांकित मानते हुए उक्त दस्तावेज पर मुद्रा एवं पंजीयन शुल्क वसूली हेतु भेजा गया है। उप पंजीयक द्वारा दस्तावेज की मूल प्रति प्रेषित की गई है जिसे भारतीय स्टाम्प अधिनियम 1899 की धारा 33 के अंतर्गत दर्ज किया गया। </textarea>--%>
                                                                        <b>
                                                                            <h6 style="text-align: justify; width: 100%; font-size: 15px;"><b>उपपंजीयक/लोकाधिकारी का प्रस्ताव:</b></h6>
                                                                        </b>
                                                                        <p id="pContent">
                                                                        </p>
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

                                                                    <table style="width: 100%; border: 1px solid black; border-collapse: collapse; text-align: left; white-space: nowrap">
                                                                        <tr>

                                                                            <th style="border: 1px solid black; border-collapse: collapse; line-height: 25px; height: 40px; white-space: nowrap; padding: 10px; background: #cccccc85;">सीरियल
                                                                                <br />
                                                                                नंबर  </th>
                                                                            <th style="border: 1px solid black; border-collapse: collapse; line-height: 25px; height: 40px; padding: 10px; background: #cccccc85;">दस्तावेज़ का
                                                                                <br />
                                                                                विवरण</th>
                                                                            <th style="border: 1px solid black; border-collapse: collapse; line-height: 25px; height: 40px; white-space: nowrap; padding: 10px; background: #cccccc85;">पार्टियों के अनुसार दस्तावेज /<br />
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
                                                                                <td style="border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;">दस्तावेज़ का प्रकार :
                                                                                </td>
                                                                                <td style="border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;">
                                                                                    <asp:Label ID="lblDocParty" runat="server"></asp:Label></td>
                                                                                <td style="border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;">
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
                                                                                <td style="border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px; min-width: 60px;"></td>

                                                                                <asp:Label ID="lblGuideRemark" runat="server" Visible="false"></asp:Label>

                                                                            </tr>
                                                                            <tr>
                                                                                <td style="border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px; min-width: 60px;">4.
                                                                                </td>
                                                                                <td style="border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;">प्रतिफल राशि /प्रतिभूति राशि 
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
                                                                                <td style="border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px; min-width: 60px;">10.
                                                                                </td>
                                                                                <td style="border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;">रजिस्ट्रेशन फीस 
                                                                                </td>
                                                                                <td style="border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px; min-width: 60px;">
                                                                                    <asp:Label ID="lblTRegDoc" runat="server"></asp:Label></td>
                                                                                <td style="border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px; min-width: 60px;">
                                                                                    <asp:Label ID="lblTRegPro" runat="server"></asp:Label>
                                                                                </td>
                                                                                <td style="border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px; min-width: 60px;">
                                                                                    <asp:Label ID="lblTRegDeficit" runat="server"></asp:Label>
                                                                                </td>
                                                                                <td style="border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px; min-width: 60px;">
                                                                                    <asp:Label ID="lblTRegRemark" runat="server"></asp:Label></td>



                                                                            </tr>



                                                                            <tr>
                                                                                <td style="border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px; min-width: 60px;">11.
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

                                                                <br />
                                                                <br />

                                                                <div>
                                                                    <h3 style="font-size: 18px; margin: 0; font-weight: 600;">कलेक्टर ऑफ़ स्टाम्प,न्यायालय </h3>
                                                                    <h4 style="margin: 0; margin: 10px; font-size: 13px;">cos.bhopal@mp.gov.in</h4>
                                                                    <h2 style="margin: 0; margin: 10px; font-size: 16px;">भोपाल 2</h2>
                                                                    <h2 style="margin: 0; margin: 10px; font-size: 16px;">आदेश </h2>
                                                                    <br>



                                                                    <div style="padding-top: 15px;">
                                                                        <h6 style="text-align: justify; width: 100%; font-size: 15px;"><b>पक्षकार/पक्षकारों का ज़वाब :</b></h6>
                                                                        <p id="pContent1">
                                                                        </p>
                                                                        <%--<textarea style="width: 100%; font-size: 14px;">  </textarea>--%>
                                                                        <h6 style="text-align: justify; width: 100%; font-size: 15px;"><b>सी ओ एस का निष्कर्ष :</b></h6>
                                                                        <p id="pContent2">
                                                                        </p>
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
                                                                            <p style="font-size: 18px; line-height: 30px; text-align: left;">
                                                                                <div class="row pr0" id="UploadDiv">
                                                                                    <div class="col-md-8" style="text-align: justify; width: 100%; font-size: 15px;">
                                                                                        <b>क्या आप संपत्ति का मूल्य और आदेश के लिए शुल्क की गणना करना चाहते हैं?</b>
                                                                                    </div>
                                                                                    <div class="col-md-4">
                                                                                        <asp:CheckBoxList ID="chkblPartys" runat="server" Visible="false" RepeatDirection="Horizontal" CssClass="table form-check-input"></asp:CheckBoxList>
                                                                                        <asp:RadioButton ID="rdbtnReportYes" CssClass="mr-4" GroupName="SeekReport" Text="हाँ" runat="server" AutoPostBack="true" OnCheckedChanged="rdbtnReportYes_CheckedChanged" />
                                                                                        <asp:RadioButton ID="rdbtnReportNo" GroupName="SeekReport" Text="नहीं" runat="server" AutoPostBack="true" OnCheckedChanged="rdbtnReportNo_CheckedChanged" />
                                                                                   
                                                                                    </div>
                                                                                   

                                                                                </div>

                                                                            </p>

                                                                            <asp:Panel ID="pnlChange" runat="server" Visible="false">
                                                                                <p style="font-size: 18px; line-height: 30px; text-align: left;">
                                                                                    <div class="row pr0" id="UploadDiv1">
                                                                                        <div class="col-md-8" style="text-align: left;">
                                                                                            <b>क्या आप आदेश के लिए संपत्ति और दस्तावेज़ की प्रकृति को बदलना चाहते हैं?</b>
                                                                                        </div>
                                                                                        <div class="col-md-4">
                                                                                            <asp:CheckBoxList ID="CheckBoxList1" runat="server" RepeatDirection="Horizontal" CssClass="table form-check-input"></asp:CheckBoxList>
                                                                                            <asp:RadioButton ID="RadioButton1" GroupName="SeekReport" runat="server" Text="हाँ" AutoPostBack="true" />
                                                                                               <%--<asp:RadioButton ID="RadioButton2" GroupName="SeekReport" Text="नहीं" runat="server" AutoPostBack="true" />--%>
                                                                                        </div>
                                                                                        

                                                                                    </div>

                                                                                </p>
                                                                            </asp:Panel>
                                                                           
                                                                            <asp:Panel ID="pnlCalNo" runat="server" Visible="false">
                                                                                <p style="font-size: 18px; line-height: 30px; text-align: left;">
                                                                                    <div id="UploadDiv2" class="text-left">
                                                                                        <div  class="row pr0">
                                                                                        <div class="col-md-8" >
                                                                                            <b>क्या आप एसआर/पीओ प्रस्ताव को स्वीकार करना चाहते हैं?</b> 
                                                                                           </div>
                                                                                            <div class="col-md-4 text-center" >
                                                                                            <asp:CheckBoxList ID="CheckBoxList2" runat="server" AutoPostBack="true" RepeatDirection="Horizontal" CssClass="table form-check-input"></asp:CheckBoxList>
                                                                                            <asp:RadioButton ID="RadioButton2" GroupName="SeekReport" Text="हाँ" runat="server" AutoPostBack="true" OnCheckedChanged="RadioButton2_CheckedChanged" />
                                                                                        </div>
                                                                                        </div>
                                                                                        <div class="row pr0">
                                                                                        <div class="col-md-8">
                                                                                            <b>क्या आप पार्टी दस्तावेज़ विवरण स्वीकार करना चाहते हैं?</b> 
                                                                                    </div>
                                                                                             <div class="col-md-4 text-center">
                                                                                            <asp:CheckBoxList ID="CheckBoxList3" runat="server" RepeatDirection="Horizontal" CssClass="table form-check-input" OnSelectedIndexChanged="CheckBoxList3_SelectedIndexChanged"></asp:CheckBoxList>
                                                                                            <asp:RadioButton ID="RadioButton3" GroupName="SeekReport" Text="हाँ" runat="server" AutoPostBack="true" OnCheckedChanged="RadioButton3_CheckedChanged" />
                                                                                        </div>

                                                                                    </div>
                                                                                    </div>
                                                                                </p>
                                                                            </asp:Panel>



                                                                            <div>
                                                                                <h6 style="text-align: justify; width: 100%; font-size: 15px;">सी ओ एस का निर्णय : </h6>
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
                                                                                <table id="tblCOSDec" style="width: 100%; border: 1px solid black; border-collapse: collapse; text-align: left; white-space: nowrap" runat="server">
                                                                                    <tr>

                                                                                        <th style="border: 1px solid black; border-collapse: collapse; line-height: 25px; height: 40px; white-space: nowrap; padding: 10px; background: #cccccc85;">सीरियल
                            <br />
                                                                                            नंबर  </th>
                                                                                        <th style="border: 1px solid black; border-collapse: collapse; line-height: 25px; height: 40px; padding: 10px; background: #cccccc85;">दस्तावेज़ का
                            <br />
                                                                                            विवरण/</th>
                                                                                        <th style="border: 1px solid black; border-collapse: collapse; line-height: 25px; height: 40px; white-space: nowrap; padding: 10px; background: #cccccc85;">पार्टियों के अनुसार दस्तावेज
                            <br />
                                                                                            दस्तावेजों का  विवरण</th>
                                                                                        <th style="border: 1px solid black; border-collapse: collapse; line-height: 25px; height: 40px; padding: 10px; background: #cccccc85;">एसआर/पीओ का
                            <br />
                                                                                            प्रस्ताव</th>

                                                                                        <th style="border: 1px solid black; border-collapse: collapse; line-height: 25px; height: 40px; padding: 10px; background: #cccccc85;">सी ओ एस
                            <br />
                                                                                            मूल्य</th>
                                                                                        <th style="border: 1px solid black; border-collapse: collapse; line-height: 25px; height: 40px; padding: 10px; background: #cccccc85;">मूल्य का
                            <br />
                                                                                            अंतर</th>
                                                                                        <th style="border: 1px solid black; border-collapse: collapse; line-height: 25px; height: 40px; padding: 10px; background: #cccccc85;">दंड की राशि
                                                                                        </th>

                                                                                    </tr>

                                                                                    <tbody>
                                                                                        <tr>
                                                                                            <td style="border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;">1.
                                                                                            </td>
                                                                                            <td style="border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;">दस्तावेज़ का प्रकार :
                                                                                            </td>
                                                                                            <td style="border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;">
                                                                                                <asp:Label ID="lblDocParty1" runat="server"></asp:Label></td>
                                                                                            <td style="border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;">
                                                                                                <asp:Label ID="lblSRPro1" runat="server"></asp:Label>
                                                                                            </td>
                                                                                            <td style="border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;"></td>
                                                                                            <td style="border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;">
                                                                                                <asp:Label ID="lblDefict1" runat="server" Visible="false"></asp:Label>
                                                                                            </td>

                                                                                            <td style="border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;">
                                                                                                <asp:Label ID="lbldocPenality" runat="server"></asp:Label></td>



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
                                                                                            <td colspan="5" style="border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;">
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
                                                                                                <asp:TextBox ID="txtPartyProGidVale" runat="server" Visible="false"></asp:TextBox>
                                                                                               <asp:Label ID="lblPartyProGidVale" runat="server"></asp:Label>
                                                                                                <%-- <asp:DropDownList runat="server">
                                    <asp:ListItem Text="MORTGAGE DEED"></asp:ListItem>
                                </asp:DropDownList>--%>
                                                                                            </td>
                                                                                            <td style="border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px; min-width: 60px;">
                                                                                                <asp:Label ID="lblGuideDefict1" runat="server"></asp:Label>
                                                                                            </td>

                                                                                            <td style="border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px; min-width: 60px;">
                                                                                                <asp:Label ID="lblGuidePenality" runat="server" Visible="true"></asp:Label></td>



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
                                                                                                <asp:TextBox ID="txtPratifal" runat="server" Visible="false"></asp:TextBox>
                                                                                                  <asp:Label ID="lblPratifal" runat="server"></asp:Label>
                                                                                                <%-- <asp:DropDownList runat="server">
                                    <asp:ListItem Text="MORTGAGE DEED"></asp:ListItem>
                                </asp:DropDownList>--%>
                                                                                            </td>
                                                                                            <td style="border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px; min-width: 60px;">
                                                                                                <asp:Label ID="lblConDefict1" runat="server"></asp:Label>
                                                                                            </td>

                                                                                            <td style="border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px; min-width: 60px;">
                                                                                                <asp:Label ID="lblProPenality" runat="server"></asp:Label></td>



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
                                                                                                <asp:TextBox ID="txtPStampCOS" runat="server" Visible="false"></asp:TextBox>
                                                                                                <asp:Label ID="lblPStampCOS" runat="server" ></asp:Label>
                                                                                                <%-- <asp:DropDownList runat="server">
                                    <asp:ListItem Text="MORTGAGE DEED"></asp:ListItem>
                                </asp:DropDownList>--%>
                                                                                            </td>
                                                                                            <td style="border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px; min-width: 60px;">
                                                                                                <asp:Label ID="lblPrincipledeficit" runat="server"></asp:Label>
                                                                                            </td>

                                                                                            <td style="border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px; min-width: 60px;">
                                                                                                <asp:Label ID="lblStampPenality" runat="server"></asp:Label></td>



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
                                                                                                <asp:TextBox ID="txtStampMuniciple" runat="server" Visible="false"></asp:TextBox>
                                                                                                 <asp:Label ID="lblStampMuniciple" runat="server"></asp:Label>
                                                                                                <%-- <asp:DropDownList runat="server">
                                    <asp:ListItem Text="MORTGAGE DEED"></asp:ListItem>
                                </asp:DropDownList>--%>
                                                                                            </td>
                                                                                            <td style="border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px; min-width: 60px;">
                                                                                                <asp:Label ID="lblMuncipleDeficit" runat="server"></asp:Label>
                                                                                            </td>

                                                                                            <td style="border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px; min-width: 60px;">
                                                                                                <asp:Label ID="lblMunciplePenality" runat="server"></asp:Label></td>



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
                                                                                                <asp:TextBox ID="txtJanpad" runat="server" Visible="false"></asp:TextBox>
                                                                                                 <asp:Label ID="lblJanpadD" runat="server"></asp:Label>
                                                                                                <%-- <asp:DropDownList runat="server">
                                    <asp:ListItem Text="MORTGAGE DEED"></asp:ListItem>
                                </asp:DropDownList>--%>
                                                                                            </td>
                                                                                            <td style="border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px; min-width: 60px;">
                                                                                                <asp:Label ID="lblJanpadDe" runat="server"></asp:Label>
                                                                                            </td>

                                                                                            <td style="border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px; min-width: 60px;">
                                                                                                <asp:Label ID="lblJanpadPenality" runat="server"></asp:Label></td>



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
                                                                                                <asp:TextBox ID="txtupkar" runat="server" Visible="false"></asp:TextBox>
                                                                                                <asp:Label ID="lblupkar" runat="server"></asp:Label>
                                                                                                <%--<asp:DropDownList runat="server">
                                    <asp:ListItem Text="MORTGAGE DEED"></asp:ListItem>
                                </asp:DropDownList>--%>
                                                                                            </td>
                                                                                            <td style="border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px; min-width: 60px;">
                                                                                                <asp:Label ID="lblUpkarDe" runat="server"></asp:Label>
                                                                                            </td>

                                                                                            <td style="border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px; min-width: 60px;">
                                                                                                <asp:Label ID="lblUpkarPenality" runat="server"></asp:Label></td>



                                                                                        </tr>

                                                                                        <tr>
                                                                                            <td style="border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px; min-width: 60px;">9.
                                                                                            </td>
                                                                                            <td style="border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;">कुल स्टाम्प ड्यूटी 
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
                                                                                                <asp:Label ID="lblTStampdeficit2" runat="server"></asp:Label>
                                                                                            </td>

                                                                                            <td style="border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px; min-width: 60px;">
                                                                                                <asp:Label ID="lblTstampPenality" runat="server"></asp:Label></td>



                                                                                        </tr>

                                                                                        <tr>
                                                                                            <td style="border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px; min-width: 60px;">10.
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
                                                                                                <asp:Label ID="lblTRegDeficit2" runat="server"></asp:Label>
                                                                                            </td>

                                                                                            <td style="border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px; min-width: 60px;">
                                                                                                <asp:Label ID="lblRegPenality" runat="server"></asp:Label></td>



                                                                                        </tr>



                                                                                        <tr>
                                                                                            <td style="border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px; min-width: 60px;">11.
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
                                                                                                <asp:Label ID="lblTAmtDeficit2" runat="server"></asp:Label>
                                                                                            </td>

                                                                                            <td style="border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px; min-width: 60px;">
                                                                                                <asp:Label ID="TPenality" runat="server"></asp:Label></td>



                                                                                        </tr>
                                                                                    </tbody>
                                                                                </table>
                                                                            </div>
                                                                        </ContentTemplate>
                                                                    </asp:UpdatePanel>
                                                                    <div style="padding: 20px 0 0 0;">
                                                                        <h6 style="text-align: justify; width: 100%; font-size: 15px;">अंतिम टिप्पणी :</h6>
                                                                        <p id="pContent3">
                                                                        </p>
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
                                                                </div>



                                                            </div>
                                                            <div class="row">
                                                                <div id="hearing" style="width: 100%;">
                                                                    <div class="mycheck mul_check m-2">
                                                                        <h6><b style="padding-right: 12px;">Would you like to add any Copy </b></h6>
                                                                        <div class="custom-control custom-radio mycb">
                                                                            <input class="custom-control-input" type="radio" id="yes" name="customRadio">
                                                                            <label for="yes" class="custom-control-label">Yes</label>
                                                                        </div>
                                                                        <div class="custom-control custom-radio">
                                                                            <input class="custom-control-input" type="radio" id="no" name="customRadio" checked="checked">
                                                                            <label for="no" class="custom-control-label">No</label>
                                                                        </div>
                                                                    </div>

                                                                    <div id="ifselectyes">
                                                                        <div id="addnewrow1">
                                                                            <br>

                                                                            <div class="row">
                                                                                <div class="col-12">
                                                                                    <div class="form-group">
                                                                                        <label>Provide information for a copy to be sent</label>
                                                                                        <%--<asp:TextBox ID="txt" CssClass="form-control" runat="server" placeholder="Enter Content"></asp:TextBox>--%>
                                                                                        <asp:TextBox ID="txtTran" ClientIDMode="Static" runat="server" CssClass="form-control" placeholder="Enter Content" Style="font-family: Verdana; font-size: Medium; height: 79px; "></asp:TextBox>
                                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ValidationGroup="AddCopyValidate" ForeColor="Red"
                                                                                            ControlToValidate="txtTran" ErrorMessage="Content required"></asp:RequiredFieldValidator>
                                                                                    </div>
                                                                                </div>
                                                                            </div>

                                                                            <div class="row mt-xl-n4 mb-xl-n3">
                                                                                <div class="col-12">
                                                                                    <div class="form-group">
                                                                                        <label>Enter details of the person sending the copy</label>
                                                                                    </div>
                                                                                </div>
                                                                            </div>

                                                                            <div class="row">

                                                                                <div class="col-12">
                                                                                    <asp:TextBox ID="txtCopyName" CssClass="form-control" runat="server" placeholder="Enter Name"></asp:TextBox>
                                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ValidationGroup="AddCopyValidate" ForeColor="Red"
                                                                                        ControlToValidate="txtCopyName" ErrorMessage=" Name required"></asp:RequiredFieldValidator>

                                                                                </div>
                                                                               </div>
                                                                                 <div class="row">
                                                                                <div class="col-3">

                                                                                    <asp:TextBox ID="txtCopyEmail" CssClass="form-control" runat="server" TextMode="Email" placeholder="Enter Email ID"></asp:TextBox>
                                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ValidationGroup="AddCopyValidate"
                                                                                        ControlToValidate="txtCopyEmail" ErrorMessage="Email ID required" ForeColor="Red"></asp:RequiredFieldValidator>
                                                                                    <asp:RegularExpressionValidator ID="REVEmail" runat="server" ValidationGroup="AddCopyValidate" ControlToValidate="txtCopyEmail" ErrorMessage="Enter Correct MailID" ForeColor="Red" ValidationExpression="^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$"></asp:RegularExpressionValidator>
                                                                                </div>
                                                                               
                                                                                <div class="col-3">
                                                                                    <%--<asp:TextBox ID="txtMobile" CssClass="form-control" runat="server" MaxLength="10" Enabled="true"  onkeypress="return NumberOnly(event)"  AutoCompleteType="None" autocomplete="off" placeholder="Enter Mobile"></asp:TextBox>--%>
                                                                                    <asp:TextBox ID="txtMobile" CssClass="form-control" runat="server"  MaxLength="10" Enabled="true" ClientIDMode="Static"  AutoCompleteType="None" onkeypress='return restrictAlphabets(event)' autocomplete="off" placeholder="Enter Mobile"></asp:TextBox>
                                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ValidationGroup="AddCopyValidate" ForeColor="Red"
                                                                                        ControlToValidate="txtMobile" ErrorMessage=" Mobile No required"></asp:RequiredFieldValidator>
                                                                                   
                                                                                </div>

                                                                                <div class="col-3">
                                                                                    <asp:TextBox ID="txtWhatsApp" CssClass="form-control" runat="server"  MaxLength="10" Enabled="true" ClientIDMode="Static"  AutoCompleteType="None" onkeypress='return restrictAlphabets(event)' autocomplete="off" placeholder="Enter WhatsApp No"></asp:TextBox>
                                                                                    <%--<asp:TextBox ID="txtWhatsApp" CssClass="form-control" runat="server" MaxLength="10" Enabled="true"  onkeypress="return NumberOnly(event)"  AutoCompleteType="None" autocomplete="off"  placeholder="Enter WhatsApp No"></asp:TextBox>--%>
                                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ValidationGroup="AddCopyValidate" ForeColor="Red"
                                                                                        ControlToValidate="txtWhatsApp" ErrorMessage="WhatsApp NO required"></asp:RequiredFieldValidator>
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

                                                            <div <div class="col-sm-4 ml-lg-auto mt-2" style="text-align: end;">
                                                            <asp:Button ID="btnDraft" runat="server" class="btn btn-success" Text="Draft Final Order" OnClick="btnDraft_Click" />
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
                                                                <asp:Panel ID="pnlSend" runat="server" Visible="false">
                                                                    <div id="">

                                                                        <div class="certi_info bg-light">Digital Signature Certificates </div>
                                                                        <br>
                                                                        <asp:Panel ID="pnlEsignDSC" Visible="true" runat="server">

                                                                            <br />
                                                                            <div class="form-group row">
                                                                                <div class="col-sm-4">
                                                                                    <%-- <select type="text" class="form-control" id="input7">
                                                                                    <option>Select</option>
                                                                                    <option>Aadhar eSign </option>
                                                                                    <option>DSC</option>
                                                                                </select>--%>

                                                                                    <asp:DropDownList ID="ddl_SignOption" runat="server" CssClass="form-control">
                                                                                        <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                                                                        <asp:ListItem Text="eSign (using Aadhaar)" Value="1"></asp:ListItem>
                                                                                        <asp:ListItem Text="DSC" Value="2"></asp:ListItem>
                                                                                        <%--<asp:ListItem Text="(2) - Digital Signature (using DSC Dongle)" Value="2"></asp:ListItem>
                                                                                    <asp:ListItem Text="(3) - Download  Sign and Upload" Value="3"></asp:ListItem>--%>
                                                                                    </asp:DropDownList>
                                                                                </div>
                                                                                <div class="col-sm-4">
                                                                                    <asp:TextBox ID="TxtLast4Digit" Width="250px" placeholder="Enter last 4 digit of Adhar Card" MaxLength="4" Enabled="true" onkeypress="return NumberOnly(event)" CssClass="form-control" runat="server" AutoCompleteType="None" autocomplete="off"></asp:TextBox>
                                                                                </div>
                                                                                <div class="col-sm-4">
                                                                                    <%-- <button type="button" class="btn btn-success">eSign/DSC</button>--%>
                                                                                    <asp:Button ID="btnEsignDSC" runat="server" class="btn btn-success" Text="eSign/DSC" OnClick="btnEsignDSC_Click" />
                                                                                </div>
                                                                            </div>
                                                                        </asp:Panel>
                                                                        <br>
                                                                        <div class="certi_info bg-light">Send Final Order Through </div>

                                                                        <div class="mycheck mul_check " style="margin: 6px 0px 6px 0;">
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

                                                                        <asp:Button ID="btnSendFinalOrder" runat="server" class="btn btn-success" Text="Send Final Order" />
                                                                        <br>
                                                                    </div>
                                                                </asp:Panel>
                                                            </div>


                                                        </div>


                                                        <div class="tab-pane " id="custom-tabs-one-editFinalOrder" role="tabpanel"
                                                            aria-labelledby="Proceeding">
                                                            <div id="editproceeding1">
                                                                <textarea id="summernote" runat="server" clientidmode="Static"></textarea>
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
                                                                <textarea id="txtSRProposal" runat="server" clientidmode="Static"></textarea>
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
                                                                <textarea id="txtCOSDecision" runat="server" clientidmode="Static"></textarea>
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
                                                                <textarea id="txtFinalDecision" runat="server" clientidmode="Static"></textarea>
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

                                                                                <button type="button" class="btn btn-success float-right" data-toggle="modal" data-target="#exampleModalCenter">Attach </button>

                                                                            </div>
                                                                            <div class="tab-pane fade active show" id="custom-tabs-four-profile" role="tabpanel" aria-labelledby="custom-tabs-four-profile-tab">

                                                                                <div>
                                                                                    <%--<img src="../dist/img/Doc_Sample_pic.jpg" style="width: 708px;" />--%>
                                                                                    <iframe id="ifRecent" runat="server" height='750'></iframe>
                                                                                    <iframe id="docPath" runat="server" height='750' visible="false"></iframe>
                                                                                </div>
                                                                            </div>
                                                                            <div class="tab-pane fade" id="custom-tabs-four-messages" role="tabpanel" aria-labelledby="custom-tabs-four-messages-tab">

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
        </div>
    </section>



</asp:Content>
