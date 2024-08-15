<%@ Page Title="" Language="C#" MasterPageFile="~/CoS/CoSMaster.Master" AutoEventWireup="true" CodeBehind="Payment_Details.aspx.cs" ValidateRequest="false" Inherits="CMS_Sampada.CoS.Payment_Details" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Styles/sweetalert.css" rel="stylesheet" />
    <link href="../dist/css/Calender-Style.css" rel="stylesheet" />
    <script src="../Scripts/sweetalert.min.js"></script>
    <script src="../assets/js/3.3.1/sweetalert2@11.js"></script>

    <style type="text/css">
        .deepak table {
            height: 900px;
        }


        #custom_tabs_one_Display.active .collect-txt {
            display: block !important;
        }

        #custom_tabs_one_Display .collect-txt {
            display: none;
        }

        li.tab-two {
            display: none;
        }

        .classadd li.nav-item.tab-two {
            display: block;
        }
    </style>

    <script>
        function ShowMessage111() {
            Swal.fire({
                icon: 'success',
                title: 'Payment Ordersheet create successfully',
                showCancelButton: false,
                confirmButtonText: 'OK',
            });

        }


        function ShowMessage_esign() {
            Swal.fire({
                icon: 'success',
                title: 'eSigned Successfully',
                showCancelButton: false,
                confirmButtonText: 'OK',
            });

        }

        function ShowMessage_Endorsement() {
            var hdnmsg = document.getElementById('hdnmsg').value;
            //alert(case_number);
            //debugger
            Swal.fire({
                icon: 'info',
                title: hdnmsg,
                showCancelButton: false,
                confirmButtonText: 'OK',
            }).then((result) => {
                if (result.value) {
                    AmagiLoader.show();
                    window.location = 'Payment_List.aspx';
                }
                else {
                    AmagiLoader.show();
                    window.location = 'Payment_List.aspx';
                }
            });

        }
        function ShowMessageDSC(loc) {

            //alert("hello");
            Swal.fire({
                icon: 'success',
                title: 'Signed ordersheet saved Successfully',
                showCancelButton: false,
                confirmButtonText: 'OK',
            }).then((result) => {
                if (result.value) {
                    window.location = loc;
                }
                else {
                    alert('user click on Cancel button');
                }
            });
        }


    </script>

    <script type="text/javascript"> 
        function checkDate(sender, args) {
            //debugger;
            if (sender._selectedDate < new Date()) {
                //alert("You cannot select a day earlier than today!");

                swal({
                    title: 'Date can not be less then todays date',
                    type: 'error'
                });
                //document.getElementById('txtHearingDate').focus();
                return false;
                sender._selectedDate = new Date();
                // set the date back to the current date 
                sender._textbox.set_Value(sender._selectedDate.format(sender._format))
            }


        }

        function ValidateDate(e) {
            //alert("sdfewfwf");
            if (document.getElementById('ContentPlaceHolder1_txtHearingDate').value == "") {
                //alert("Please select Hearing Date.");
                document.getElementById('rdReportYes').checked = false;
                ShowMessageNotVerified();
                return false;

            }
            else {
                //alert("true");
                document.getElementById('rdReportYes').checked = true;
                __doPostBack('rdReportYes', e);
                //document.getElementById('rdReportYes').click();
                //return true;
            }
        }

        function ShowMessageNotVerified() {



            Swal.fire({

                icon: 'info',
                title: 'Please select hearing date !',
                showCancelButton: false,
                confirmButtonText: 'OK',
                timer: 10000

            });
            return false;
        }

        function ShowConfirmationMsg() {


            //alert(AppNum);
            Swal.fire({
                title: 'Do you want to Submit Ordersheet?',
                showDenyButton: true,
                /* showCancelButton: true,*/
                confirmButtonText: 'Yes',
                /*denyButtonText: 'No',*/
                customClass: {
                    actions: 'my-actions',
                    cancelButton: 'order-1 right-gap',
                    confirmButton: 'order-2',
                    denyButton: 'order-3',
                }
            }).then((result) => {
                if (result.isConfirmed) {



                    AddOrdersheet();
                    document.getElementById("btnSubmit").click();

                }

            })
        }


        //function ValidateRecord() {
        //    debugger;
        //    var HearingDate = $("#ContentPlaceHolder1_txtHearingDate").val();
        //    var lblHearingDate = $("#lblHearingDt").val();
        //    if (HearingDate == "" && lblHearingDate == "") {
        //        //alert("Please select Hearing date");
        //        Swal.fire({
        //            icon: 'info',
        //            title: 'Please select hearing date'

        //        })
        //        return false;
        //    }
        //    else if (!document.getElementById('rdReportYes').checked) {

        //        if (!confirm("Do you want to proceed ordersheet without Seek report ? ")) {

        //            return false;
        //        }
        //    }
        //    //else if (!confirm("Do you want to proceed ordersheet without seek report ? ")) {

        //    //    return false;
        //    //}

        //}


        function ShowMessage() {

            Swal.fire({

                icon: 'success',
                title: 'Payment Ordersheet create successfully !',
                showCancelButton: false,
                confirmButtonText: 'OK',
                timer: 10000

            });
            return false;
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
            else if (document.getElementById("TxtLast4Digit").value == "") {

                Swal.fire({
                    icon: 'info',
                    title: 'Please enter last 4 digit of Adhar Card'

                })
                return false;

            }


        }
    </script>



    <script type="text/javascript">
        function ValidateInput() {
            //debugger;
            //alert("hello");
           <%-- var CHK = document.getElementById("<%=chkblPartys.ClientID%>");--%>

            var checkbox = CHK.getElementsByTagName("input");

            var counter = 0;

            for (var i = 0; i < checkbox.length; i++) {

                if (checkbox[i].checked) {

                    counter++;

                }

            }

            if (counter == 0) {

               <%-- alert("Please select atleast 1 item ");
                document.getElementById("<%=chkblPartys.ClientID%>").focus();

                return false;--%>

            }

            else if (document.getElementById('txtHearingDate').value == '') {

                swal({
                    title: 'Please select Hearing Date',
                    type: 'warning'
                });
                document.getElementById('txtHearingDate').focus();
                return false;
            }

            //return true;

        }

    </script>
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



    </script>



</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="SCMNG" runat="server"></asp:ScriptManager>

    <section class="content-header headercls">
        <div class="container-fluid">
            <div class="row">
                <div class="col-sm-3">
                    <h5>Payment Details </h5>
                </div>
            </div>
        </div>
    </section>

    <section class="content-header">
        <div class="container-fluid">
            <div class="row">
                <div class="col-sm-4">
                    <h6>Proposal No -
                        <asp:Label ID="lblProposalIdHeading" Text="" runat="server"></asp:Label></h6>
                </div>
                <div class="col-sm-4">
                    <h6>Case No -  
                        <asp:Label ID="lblCase_Number" runat="server"></asp:Label>
                    </h6>
                </div>
                <div class="col-sm-4">
                    <h6>Case Registered Date -  
                        <asp:Label ID="lblRegisteredDate" runat="server"></asp:Label>
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


    <asp:UpdatePanel ID="upnlDateCalender" runat="server">
        <ContentTemplate>
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
                                        <li class="nav-item"><a class="nav-link disabled" href="#">Final Order</a>
                                        <li class="nav-item"><a class="nav-link active disabled" href="#">Payment</a></li>
                                        <li class="nav-item"><a class="nav-link disabled" href="#">Closed Cases</a></li>

                                    </ul>
                                </div>
                                <div class="card-body">
                                    <div class="tab-content">
                                        <div class="tab-pane active" id="CreateOrderSheet">
                                            <div class="row">

                                                <div class="col-12 col-sm-6">
                                                    <div class="card card-primary card-tabs h-100">
                                                        <div class="card-header p-0 pt-1">
                                                            <ul class="nav nav-tabs" id="custom-tabs-one-tab" role="tablist">
                                                                <li class="nav-item tab-one">
                                                                    <a class="nav-link active" id="custom-tabs-one-home-tab" data-toggle="pill"
                                                                        href="#custom-tabs-one-ProposalForm" role="tab" aria-controls="custom-tabs-one-home"
                                                                        aria-selected="false">Payment</a>
                                                                </li>
                                                                <li class="nav-item tab-two">
                                                                    <a class="nav-link" id="custom_tabs_one_profile_tab" data-toggle="pill"
                                                                        href="#custom-tabs-one-RegisteredForm" role="tab"
                                                                        aria-controls="custom-tabs-one-profile" aria-selected="true" runat="server">Edit Payment sheet </a>

                                                                </li>
                                                            </ul>
                                                        </div>

                                                        <div class="card-body">
                                                            <div class="tab-content" id="custom-tabs-one-tabContent">
                                                                <div class="tab-pane active show" id="custom-tabs-one-ProposalForm" role="tabpanel"
                                                                    aria-labelledby="custom-tabs-one-home-tab">
                                                                    <!-- code here -->
                                                                    <asp:HiddenField ID="hdnfApp_Id" ClientIDMode="Static" runat="server" />
                                                                    <asp:HiddenField ID="hdnfCseNunmber" ClientIDMode="Static" runat="server" />
                                                                    <asp:HiddenField ID="hdnDistrict" ClientIDMode="Static" runat="server" />
                                                                    <asp:HiddenField ID="hdnSOOffice" ClientIDMode="Static" runat="server" />
                                                                    <asp:HiddenField ID="hdnSource" ClientIDMode="Static" runat="server" />
                                                                    <asp:HiddenField ID="hdnProposal" ClientIDMode="Static" runat="server" />
                                                                    <asp:HiddenField ID="hdnPartyName1" ClientIDMode="Static" runat="server" />
                                                                    <asp:HiddenField ID="hdnPartyFather_Name" ClientIDMode="Static" runat="server" />
                                                                    <asp:HiddenField ID="hdnParty_Address" ClientIDMode="Static" runat="server" />
                                                                    <asp:HiddenField ID="hdpartyList" ClientIDMode="Static" runat="server" />
                                                                    <asp:HiddenField ID="hdpartyDoc" ClientIDMode="Static" runat="server" />
                                                                    <asp:HiddenField ID="hdnSeekContent" ClientIDMode="Static" runat="server" />
                                                                    <asp:HiddenField ID="hdnSeekContent_Current" ClientIDMode="Static" runat="server" />
                                                                    <asp:Panel ID="pnlEditOrderSheet" runat="server" Visible="false">

                                                                        <div class="">
                                                                            <!-- Doc detail html -->
                                                                            <div class="main-box html_box htmldoc">

                                                                                <div class="form-group">
                                                                                    <asp:TextBox ID="txtNotice" TextMode="MultiLine" ClientIDMode="Static" runat="server" Width="100%" Height="650px" Rows="20"></asp:TextBox>
                                                                                    <asp:HtmlEditorExtender ID="HtmlEditorExtender1" runat="server" TargetControlID="txtNotice" EnableSanitization="false"></asp:HtmlEditorExtender>
                                                                                </div>

                                                                                <div class="form-group" style="display: none;">
                                                                                    <asp:TextBox ID="txtNotice2" TextMode="MultiLine" ClientIDMode="Static" runat="server" Width="100%" Height="650px" Rows="20"></asp:TextBox>
                                                                                    <asp:HtmlEditorExtender ID="HtmlEditorExtender3" runat="server" TargetControlID="txtNotice2" EnableSanitization="false"></asp:HtmlEditorExtender>
                                                                                </div>
                                                                                <!-- <%-- </div>--%> -->
                                                                            </div>
                                                                        </div>
                                                                    </asp:Panel>

                                                                    <div id="pnl_Proceding" runat="server">

                                                                        <div class="displaydiv">


                                                                            <%--<button type="button" class="btn btn-success" id="add_green_proceeding" href="#custom-tabs-one-RegisteredForm">Add Proceeding </button>--%>
                                                                            <a class="btn btn-success" id="A1" data-toggle="pill"
                                                                                href="#custom-tabs-one-RegisteredForm" role="tab"
                                                                                aria-controls="custom-tabs-one-profile" aria-selected="true" runat="server">Add Proceeding </a>


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
                                                                                                    <asp:Label ID="lblhearing" runat="server" Text='<%# Eval("hearingdate", "{0:dd/MM/yyyy}") %>' Font-Bold="true" />

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
                                                                                                    <asp:Label ID="lblDate" runat="server" Text='<%# Eval("inserteddate", "{0:dd/MM/yyyy}") %>' Font-Bold="true" />


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


                                                                            </div>
                                                                            <br>
                                                                        </div>
                                                                    </div>

                                                                    <br>

                                                                    <%--start--%>
                                                                </div>
                                                                <div class="tab-pane fade" id="custom-tabs-one-RegisteredForm"
                                                                    aria-labelledby="custom-tabs-one-profile-tab">
                                                                    <!-- table -->



                                                                    <textarea id="summernote" runat="server" clientidmode="Static" style="text-align: justify;"></textarea>
                                                                    <div class="newdiv">

                                                                        <a onclick="AddOrdersheet()" class="btn btn-success">Save</a>

                                                                    </div>



                                                                </div>
                                                               
                                                                <div id="custom_tabs_one_Display" runat="server" clientidmode="Static">
                                                                    <div class="collect-txt">
                                                                        <div class="main-box htmldoc" style="width: 100%; margin: 0 auto; text-align: center; border: 1px solid #ccc; padding: 30px 30px 30px 30px;">


                                                                            <h2 style="font-size: 18px; margin: 0; font-weight: 600;">न्यायालय कलेक्टर ऑफ़ स्टाम्प्स, 
                                                                                <asp:Label ID="lblHeadingDist" runat="server"></asp:Label>
                                                                                (म.प्र.)</h2>
                                                                            <h3 style="margin: 0; margin: 10px; font-size: 16px;">प्रारूप-अ</h3>
                                                                            <h2 style="font-size: 16px; margin: 0; margin-bottom: 10px;">(परिपत्र दो-1 की कंडिका 1)</h2>
                                                                            <h3 style="margin: 0; margin: 10px; font-size: 16px;">राजस्व आदेशपत्र</h3>
                                                                            <h2 style="font-size: 16px; margin: 0; margin-bottom: 10px;">कलेक्टर ऑफ़ स्टाम्प,
                                                                                <asp:Label ID="lblHeadingDist1" runat="server"></asp:Label>
                                                                                के न्यायालय में प्रकरण क्रमांक-  
                                                                        <asp:Label ID="lblCaseNumber" runat="server"></asp:Label>
                                                                            </h2>
                                                                            <br>

                                                                            <table style="width: 100%; border: 1px solid black; border-collapse: collapse;" >
                                                                                <tr>
                                                                                    <th style="border: 1px solid black; border-collapse: collapse; line-height: 20px; /* height: 31px; */
        padding: 5px; font-size: 14px;">आदेश क्रमांक कार्यवाही
                                                                                <br>
                                                                                        की तारीख एवं स्थान    </th>
                                                                                    <th style="border: 1px solid black; border-collapse: collapse; line-height: 20px; /* height: 31px; */
        padding: 5px; font-size: 14px;">पीठासीन अधिकारी के हस्ताक्षर सहित आदेश पत्र अथवा कार्यवाही
                                                                                  <br />
                                                                                        मध्यप्रदेश शासन विरूद्ध 
                                                                                <asp:Label ID="lblPartyName" runat="server" Visible="false"></asp:Label>
                                                                                    </th>
                                                                                    <th style="border: 1px solid black; border-collapse: collapse; line-height: 20px; /* height: 31px; */
        padding: 5px; font-size: 14px;">पक्षों/वकीलों
                                                                                <br>
                                                                                        आदेश  पालक  लिपिक के हस्ताक्षर</th>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td style="border: 1px solid black; border-collapse: collapse; /*    height: 40px; */ padding: 5px; font-size: 14px;">
                                                                                        <div class="content" style="padding: 15px">
                                                                                            <asp:Label ID="lblToday" runat="server"></asp:Label>
                                                                                        </div>
                                                                                    </td>

                                                                                    <td style="border: 1px solid black; border-collapse: collapse; /*    height: 40px; */ padding: 5px; font-size: 14px;">
                                                                                        <div style="padding: 2px;">
                                                                                            <p>
                                                                                                प्रकरण क्रमांक : (<asp:Label ID="lblCNo" runat="server"></asp:Label>)
                                                                                         
                                                                                          <br />
                                                                                                <%-- उप पंजीयक भोपाल-2  द्वारा एक  पंजीकृत दस्तावेज दान पत्र  विलेख क्रमांक--%>
                                                                                                <asp:Label ID="lblAppID" runat="server" Visible="false"></asp:Label>
                                                                                                <%--   दिनांक--%>
                                                                                                <asp:Label ID="lblTodatDt" runat="server" Visible="false"></asp:Label>
                                                                                                <!-- <input type="date" id="" name=""> -->
                                                                                                <%--   को न्यून मुद्रांकित मानते हुए उक्त दस्तावेज पर मुद्रांक एवं पंजीयन शुल्क वसूली हेतु भेजा गया है। उप पंजीयक द्वारा दस्तावेज की मूल प्रति प्रेषित की गई है जिसे भारतीय स्टाम्प अधिनियम, 1899 की धारा-33 के अंतर्गत दर्ज किया गया। --%>
                                                                                            </p>
                                                                                            <p id="pContent" style="text-align: justify;" runat="server" clientidmode="Static">
                                                                                            </p>
                                                                                            <table class="table table-bordered">
                                                                                                <tr>
                                                                                                    <td>
                                                                                                        <label for="inputEmail3" >भुगतान की राशि</label></td>
                                                                                                    <td>
                                                                                                        <label for="inputEmail3" >भुगतान की गई राशि</label></td>
                                                                                                    <td>
                                                                                                        <label for="inputEmail3" >भुगतान की तिथि</label></td>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                    <td>
                                                                                                        <asp:Label ID="txtFinalAmount" runat="server"></asp:Label></td>
                                                                                                    <td>
                                                                                                        <asp:Label ID="txtPaidAmount" runat="server"></asp:Label></td>
                                                                                                    <td>
                                                                                                        <asp:Label ID="txtPaidDate" runat="server"></asp:Label></td>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                    <td>
                                                                                                        <label for="inputEmail3">यू आर एन नंबर</label></td>
                                                                                                    <td>
                                                                                                        <label for="inputEmail3">भुगतान की स्थिति</label></td>
                                                                                                    <td>
                                                                                                        <label for="inputEmail3">राशि का प्रकार</label></td>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                    <td>
                                                                                                        <asp:Label ID="txtURN" runat="server"></asp:Label></td>
                                                                                                    <td>
                                                                                                        <asp:Label ID="txtPaymentStatus" runat="server"></asp:Label></td>
                                                                                                    <td>
                                                                                                       <asp:Label ID="txtAmountType"  runat="server"></asp:Label></td> 
                                                                                                </tr>
                                                                                                <tr>
                                                                                                    <td colspan="3">
                                                                                                        <label for="inputEmail3">उद्देश्य</label></td>
                                                                                                    
                                                                                                </tr>
                                                                                                <tr>
                                                                                                    
                                                                                                   <td colspan="3">
                                                                                                        <asp:Label ID="txtPurpose" runat="server"></asp:Label></td>
                                                                                                </tr>

                                                                                            </table>
                                                                                            <br />
                                                                                                <br />

                                                                                            <b style="float: right; text-align: center; padding: 2px 0 5px 0;">कलेक्टर ऑफ़ स्टाम्प्स,<br />
                                                                                                <asp:Label ID="lblHeadingDist2" runat="server"></asp:Label>
                                                                                                <br />
                                                                                                <br />
                                                                                            </b>
                                                                                        </div>
                                                                                    </td>

                                                                                    <td style="border: 1px solid black; border-collapse: collapse; /*    height: 40px; */ padding: 5px; font-size: 14px;"></td>
                                                                                </tr>
                                                                            </table>
                                                                            <br />
                                                                            <br />


                                                                            <!-- <%-- </div>--%> -->
                                                                        </div>
                                                                    </div>
                                                                </div>




                                                            </div>

                                                            <div class="form-group row">
                                                                <div class="col-sm-4 ml-lg-auto mt-2" style="text-align: end;">
                                                                    <%--<button type="button" class="btn btn-success">Submit </button>--%>
                                                                    <asp:Button ID="btnSavePayment" runat="server" ClientIDMode="Static" Style="display: none" Text="Save" class="btn btn-success dt-show" OnClick="btnSavePayment_Click" />
                                                                </div>

                                                            </div>
                                                            <asp:Panel ID="pnlEsignDSC" Visible="false" runat="server">
                                                                <div class="certi_info bg-light m16">eSign / DSC</div>
                                                                <br />
                                                                <div class="form-group row">
                                                                    <div class="col-sm-5">
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
                                                                    <div class="col-sm-5">
                                                                        <asp:TextBox ID="TxtLast4Digit" placeholder="Enter last 4 digit of Adhar Card" MaxLength="4" Enabled="true" onkeypress="return NumberOnly(event)" ClientIDMode="Static" CssClass="form-control" runat="server" AutoCompleteType="None" autocomplete="off"></asp:TextBox>
                                                                    </div>
                                                                    <div class="col-sm-2">
                                                                        <%-- <button type="button" class="btn btn-success">eSign/DSC</button>--%>
                                                                        <asp:Button ID="btnEsignDSC" runat="server" OnClick="btnEsignDSC_Click" class="btn btn-success" Text="eSign/DSC" OnClientClick="return ValidateEsignRecord()" />
                                                                    </div>
                                                                </div>
                                                            </asp:Panel>

                                                            <asp:Panel ID="pnlBtnEndo" Visible="false" runat="server">
                                                                <div class="form-group row">
                                                                    
                                                                    <h6><b style="padding-right: 12px;">Apply DSC to make endorsement</b></h6>
                                                                       
                                                                    <div class="col-sm-4 ml-lg-auto mt-2" style="text-align: end;">
                                                                        <%--<button type="button" class="btn btn-success">Submit </button>--%>
                                                                        <asp:Button ID="btnendorsement" runat="server" Text="DSC " class="btn btn-success" OnClick="btnendorsement_Click" ClientIDMode="static" OnClientClick="return ValidateRecord()" />
                                                                    </div>

                                                                </div>
                                                            </asp:Panel>
                                                        </div>
                                                    </div>
                                                </div>

                                                <asp:HiddenField ID="hdnmsg" ClientIDMode="Static" runat="server" />

                                                <div class="col-12 col-sm-6">
                                                    <div class="card card-primary card-tabs h-100">
                                                        <div class="card-header p-0 pt-1 d-flex align-items-center">
                                                            <div class="col-sm-8 p-0">
                                                                <ul class="nav nav-tabs" id="custom-tabs-two-tab" role="tablist">
                                                                    <%-- <li class="nav-item">
                                                                        <a class="nav-link disabled" id="custom-tabs-two-home-tab" data-toggle="pill"
                                                                            href="#custom-tabs-two-home" role="tab" aria-controls="custom-tabs-two-home"
                                                                            aria-selected="true">Document </a>
                                                                    </li>--%>
                                                                    <li class="nav-item">
                                                                        <a class="nav-link active" id="custom-tabs-two-profile-tab" data-toggle="pill"
                                                                            href="#custom-tabs-two-profile" role="tab" aria-controls="custom-tabs-two-profile"
                                                                            aria-selected="false">Document</a>
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

                                                        <!-- <%--button--%> -->


                                                        <%--   <div class="text-right newbtndp">
                                                    <button type="button"  class="btn btn-secondary btn-sm" onclick="ShowDownloadOption()">Download </button>
                                                    &nbsp;
                                                    <button type="button" class="btn btn-info btn-sm float-right" onclick="printPDF()">Print </button>
                                                    &nbsp;
                                                </div>--%>

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
                                                                                        <a class="nav-link" id="custom-tabs-four-settings-tab" style="pointer-events: none;" data-toggle="pill" href="#custom-tabs-four-settings" role="tab" aria-controls="custom-tabs-four-settings" aria-selected="true">Previous Proceeding</a>
                                                                                    </li>
                                                                                </ul>
                                                                            </div>
                                                                            <div class="card-body d_mycard">
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

                                                                                        <%-- <div id="pnl3" style="display: none">
                                                                                            <iframe id="ifrDisplay" class="embed-responsive-item" height="750"></iframe>
                                                                                        </div>--%>


                                                                                        <asp:Panel ID="pnlDocView" runat="server" Visible="false">
                                                                                            <asp:Button class="btn btn-primary" runat="server" Text="X" ID="btnClose" OnClick="btnClose_Click" />
                                                                                            <div class="row">
                                                                                                <div class="col-lg-12 text-center">
                                                                                                    <%--<iframe runat="server" id="ifrDisplay" style="width: 100%; height: 500px; border: 1px solid black;"></iframe>--%>
                                                                                                </div>
                                                                                            </div>
                                                                                        </asp:Panel>

                                                                                    </div>
                                                                                    <div class="tab-pane fade active show" id="custom-tabs-four-profile" role="tabpanel" aria-labelledby="custom-tabs-four-profile-tab">

                                                                                        <%--<div class="col-md-12">
                                                                                    <div class="card card-primary card-outline newclr">
                                                                                        <div class="card-header">
                                                                                            <h3 class="card-title">Proposal Sheet</h3>
                                                                                        </div>
                                                                                        <div class="card-body">--%>

                                                                                        <iframe id="docPath" runat="server" height='750'></iframe>


                                                                                        <%--                        </div>

                                                                                    </div>
                                                                                </div>--%>
                                                                                    </div>

                                                                                    <div class="tab-pane fade" id="custom-tabs-four-messages" role="tabpanel" aria-labelledby="custom-tabs-four-messages-tab">

                                                                                        <%--<div class="col-md-12">
                                                                                    <div class="card card-primary card-outline newclr">
                                                                                        <div class="card-header">
                                                                                            <h3 class="card-title">Proposal Sheet</h3>
                                                                                        </div>
                                                                                        <div class="card-body">--%>
                                                                                        <table style="width: 100%">
                                                                                            <tr>
                                                                                                <td>
                                                                                                    <iframe id="RecentdocPath" runat="server" clientidmode="Static" width='550' height='750'></iframe>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td>
                                                                                                    <iframe id="RecentProposalDoc" runat="server" clientidmode="Static" width='550' height='750'></iframe>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td>
                                                                                                    <iframe id="RecentAttachedDoc" runat="server" clientidmode="Static" width='550' height='750'></iframe>
                                                                                                </td>
                                                                                            </tr>

                                                                                        </table>
                                                                                        <iframe id="ifPDFViewerAll" clientidmode="Static" runat="server" scrolling="auto" height='750'></iframe>

                                                                                        <%--                                         </div>

                                                                                    </div>
                                                                                </div>--%>
                                                                                    </div>
                                                                                    <div class="tab-pane fade " id="custom-tabs-four-settings" role="tabpanel" aria-labelledby="custom-tabs-four-settings-tab">

                                                                                        <%--<div class="col-md-12">
                                                                                    <div class="card card-primary card-outline newclr">
                                                                                        <div class="card-header">
                                                                                            <h3 class="card-title">Proposal Sheet</h3>
                                                                                        </div>
                                                                                        <div class="card-body">--%>

                                                                                        <asp:GridView ID="PreviousDoc" runat="server" AutoGenerateColumns="false">
                                                                                            <Columns>

                                                                                                <%--<asp:BoundField DataField="File_Path" />--%>
                                                                                                <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                                                                                    <ItemTemplate>
                                                                                                        <iframe id="ifDisplayPDF" height='750' runat="server" src='<%#Eval("File_Path") %>'></iframe>
                                                                                                        <%--<iframe id="ifDisplayPDF" width='500' height='400' runat="server" src='<%# "../../../Documents/" + Eval("File_Path")  %>'></iframe>--%>
                                                                                                    </ItemTemplate>
                                                                                                </asp:TemplateField>
                                                                                            </Columns>
                                                                                        </asp:GridView>

                                                                                        <%--                   </div>

                                                                                    </div>
                                                                                </div>--%>
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
                                    <table>
                                        <tr>
                                            <td>
                                                <asp:CheckBox ID="chkRecentDoc" CssClass="form-check" runat="server" Text="Recent" />
                                            </td>
                                            <td>
                                                <asp:CheckBox ID="chkAll" CssClass="form-check" runat="server" Text="All" />
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                                <div class="modal-footer">
                                    <asp:Button ID="btnDownload" runat="server" ClientIDMode="Static" class="btn btn-primary" OnClick="btnDownload_Click" Text="Submit" />
                                    <input type="button" value="Print" id="btnPrintSeletedPDF" onclick="PrintSeletedPDF()" />


                                </div>
                            </div>
                        </div>
                    </div>
                </div>



                <div class="modal fade" id="myModalDoc1" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
                    <div class="modal-dialog">
                        <div class="modal-content">
                            <%-- <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>--%>
                            <div class="modal-header">

                                <h4 class="modal-title" id="myModalLabel1" style="float: left">Document View</h4>
                                <button type="button" data-dismiss="modal" style="float: right; font-size: x-large">x</button>

                            </div>
                            <div class="modal-body">
                                <table>
                                    <tr>
                                        <td>


                                            <iframe id="ifrDisplay1" class="embed-responsive-item" style="height: 600px; width: 450px"></iframe>

                                        </td>

                                    </tr>
                                </table>
                            </div>

                        </div>
                    </div>
                </div>

            </section>
        </ContentTemplate>
        <Triggers>

            <asp:PostBackTrigger ControlID="btnEsignDSC" />
            <%--<asp:PostBackTrigger ControlID="btnSubmit" />--%>
            <%--<asp:PostBackTrigger ControlID="Calendar1" />--%>
        </Triggers>
    </asp:UpdatePanel>





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
        //function openSrDoc(FILE_PATH) {

        //    //alert(FILE_PATH);
        //    debugger;
        //    $("#List_pnl").hide();
        //    $("#View_pnl").show();
        //    $('#ifrDisplay').attr('src', FILE_PATH);

        //}
        function generateCalenderByCode() {
            ///// alert("hello");
            //var today = new Date();
            //generateCalendar(today.getFullYear(), today.getMonth());
            //populateYearDropdown();
            //var y = document.getElementById('ddlyear').value;
            //var m = document.getElementById('ddlmonth').value;
            ////alert(y + '  ' + m);
            //generateCalendar(y, m)
            document.getElementById('ContentPlaceHolder1_divCalender').style.display = "block";
            //document.getElementById('calendar-container').style.display = "block";

        }



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

        function ShowDownloadOption() {
            //alert("Hello");
            $('#myModal').modal('show');
        }

        function AddOrdersheet() {
           
            document.getElementById('btnSavePayment').style.display = "block";
            //debugger;
            //alert(document.getElementById("summernote").value);
            document.getElementById("pContent").innerHTML = document.getElementById("summernote").value;
            //document.getElementById("summernote").value = "";

            $("#custom-tabs-one-ProposalForm").removeClass("active show");
            //$("#custom-tabs-one-RegisteredForm").removeClass("active");
            //$("#custom_tabs_one_profile_tab").removeClass("active");
            $("#custom-tabs-one-home-tab").removeClass("active show");
            $('#ContentPlaceHolder1_custom-tabs-one-home-tab').removeClass("active");
            $('#custom-tabs-one-RegisteredForm').removeClass("active show");

            $("#custom_tabs_one_profile_tab").addClass("active");
            $('#custom_tabs_one_Display').addClass("active");
            $('#custom-tabs-one-tabContent').addClass("classadd");
            $('#custom-tabs-one-tabContent').addClass("newclass");

            $('#custom-tabs-one-tab').addClass("nav-item.tab-two.rt");



            //if (document.getElementById('ContentPlaceHolder1_txtFinalAmount').value == document.getElementById('ContentPlaceHolder1_txtPaidAmount').value) {

            //    //document.getElementById('btnSavePayment').style.visibility = 'visible';
            //    $("#pnlBtnSave").show();

            //}
            //else {
            //    //document.getElementById('btnSavePayment').style.visibility = 'hidden';
            //    $("#pnlBtnSave").hide();

            //}


        }
        function AddSeekContent() {

            var Seek = document.getElementById('hdnSeekContent').value;
            var Seek_Current = document.getElementById('hdnSeekContent_Current').value;




            //document.getElementById("lblSeekContent").innerHTML = document.getElementById("hdnSeekContent").value;

            //document.getElementById("lblSeekContent").value = Seek;
            document.getElementById("lblSeekContent").value = Seek;
            document.getElementById("lblSeekContent_Curent").value = Seek_Current;

            alert(document.getElementById("lblSeekContent").value);
            alert(document.getElementById("lblSeekContent_Curent").value);

            $('#lblSeekContent').text(Seek);
            $('#lblSeekContent_Curent').text(Seek_Current);

        }
        function SetHearingDate(ths) {
            //alert(ths);
            document.getElementById("lblHearingDt").value = "";

            var year = ths.value.split("-")[0];
            var Mon = ths.value.split("-")[1];
            var dat = ths.value.split("-")[2];

            var changeDate = (dat + '-' + Mon + '-' + year).toString();
            //alert(changeDate);
            $('#lblHearingDt').text(changeDate);
            $('#lblHearingDt1').text(changeDate);
            document.getElementById("lblHearingDt").value = (dat + '/' + Mon + '/' + year);
            document.getElementById("lblHearingDt1").value = (dat + '/' + Mon + '/' + year);
            //document.getElementById("divDate").style.visibility = "visible";

        }

        function dsign() {
            //debugger
            var SignatureNm = $('#hfjuric').val();
            var today = new Date();
            var dd = String(today.getDate()).padStart(2, '0');
            var mm = String(today.getMonth() + 1).padStart(2, '0'); //January is 0!
            var yyyy = today.getFullYear();

            today = dd + '/' + mm + '/' + yyyy;

            var base64 = $('#ContentPlaceHolder1_HFBase64').val();
            var dataString = { 'ServerDateTime': today, 'DocumentType': 'PDF', 'Data': base64, 'SignatureName': SignatureNm, 'Reason': 'Notice', 'PageNo': '1', 'Location': 'SAMAPADA_CMS' };
            if ($('#HFBase64').length > 0) {
                var base64 = $('#HFBase64').val();
                var dataString = { 'ServerDateTime': '15/04/2019', 'DocumentType': 'PDF', 'Data': base64, 'SignatureName': 'Preciding Officer', 'Reason': 'I approve this document', 'Location': 'MADHYA PRADESH COMMERCIAL TAX' };
                $.ajax({
                    type: 'POST',
                    url: 'http://localhost:8060/jsonsigner/Sign',
                    data: dataString,
                    dataType: "json",
                    async: false,
                    success: function (html) {
                        //debugger
                        if (html.SignedData) {
                            //$('#Div1').html(html.SignedData);
                            $('#HFSData').val(html.SignedData);
                            $("#btnSaveFile").trigger("click");
                        }
                    },
                    error: function (jqXHR, textStatus, errorThrown) {
                        //debugger
                        alert("Digital Sign Cancelled")
                    }
                });
            }
        }


    </script>
    <script type="text/javascript">
        function printPDF() {
            //debugger;
            document.getElementById('btnDownload').style.visibility = 'hidden';
            document.getElementById('btnPrintSeletedPDF').style.visibility = 'visible';
            ShowDownloadOption();
        }
        function PrintSeletedPDF() {
            var frame = document.getElementById('ifPDFViewerAll');
            frame.contentWindow.focus();
            frame.contentWindow.print();

        }




    </script>

    <%-- <link href="http://code.jquery.com/ui/1.11.4/themes/ui-lightness/jquery-ui.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="http://code.jquery.com/jquery-1.8.2.js"></script>
    <script type="text/javascript" src="http://code.jquery.com/ui/1.10.3/jquery-ui.js"></script>
    <script type="text/javascript">
        function openPopup() {


            $("#popupdiv").dialog({
                title: "jQuery Show Gridview Row Details in Popup",
                width: 300,
                height: 250,
                modal: true,
                buttons: {
                    Close: function () {
                        $(this).dialog('close');
                    }
                }
            });
        }
    </script>--%>

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


    </script>
    <div id="modal_dialog" style="display: none"></div>
    <asp:HiddenField ID="hdnCOSOfficeNameHi1" runat="server" />
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.6.4/jquery.min.js"></script>
    <asp:HiddenField runat="server" ClientIDMode="Static" ID="hdappid" />
    <asp:HiddenField runat="server" ClientIDMode="Static" ID="hdAppno" />
    <asp:HiddenField runat="server" ClientIDMode="Static" ID="hdTocan" />
    <asp:HiddenField ID="hdnbase64" ClientIDMode="Static" runat="server" />
    <script type="text/javascript">
        $(document).ready(function () {
            //debugger;
            if ($("#hdnbase64").val().length > 0) {
                var base64Pdf = $("#hdnbase64").val();
                console.log("hdnbase64----2378428   " + base64Pdf);
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

        function test() {
            //alert("hi");
        }

        function test1() {
            // alert("hello");
        }

        function ChangeAncureText() {
            // $("#A1").text('Edit Proceeding');
            document.getElementById('A1').hide();

            //var vid_id = document.getElementById('A1');
            //var anchor = vid_id.getElementsByTagName('a');
            //anchor[0].innerHTML = "Edit Proceeding";

        }

        function ShowMessageCaseReg() {
            var case_number = document.getElementById('hdnfldCase').value;
            var App_ID = document.getElementById('hdnAppID').value;
            //alert(case_number);
            Swal.fire({
                icon: 'success',
                title: 'Case Registered Successfully,<br/><br/> Case Number is : ' + case_number,
                showCancelButton: false,
                confirmButtonText: 'OK',
                customClass: "casefontcustm"
            }).then((result) => {
                if (result.value) {
                    AmagiLoader.show();
                    window.location = 'Ordersheet.aspx';
                } else {
                    AmagiLoader.show();
                    window.location = 'Ordersheet.aspx';
                }
            });
        }
    </script>
</asp:Content>
