<%@ Page Title="" Language="C#" MasterPageFile="~/CoS/CoSMaster.Master" AutoEventWireup="true" CodeBehind="RRC_Certificate_Proceeding.aspx.cs" ValidateRequest="false" Inherits="CMS_Sampada.CoS.RRC_Certificate_Proceeding" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Styles/sweetalert.css" rel="stylesheet" />
    <link href="../dist/css/Calender-Style.css" rel="stylesheet" />
    <script src="../Scripts/sweetalert.min.js"></script>
    <script src="../assets/js/3.3.1/sweetalert2@11.js"></script>
    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>

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
    </style>

    <script>
        function ShowMessage111() {
            Swal.fire({
                icon: 'success',
                title: 'RRC Certificate Proceeding create successfully',
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




    </script>

    <script type="text/javascript"> 
        function checkDate(sender, args) {
            ;
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

        function OrderSheetSave() {

            //alert("fghj");
            Swal.fire({
                icon: 'success',
                title: 'RRC Certificate Proceeding Draft Saved successfully !',
                showCancelButton: false,
                confirmButtonText: 'OK',
            });
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



        function ShowMessage() {

            Swal.fire({

                icon: 'success',
                title: 'RRC Certificate Proceeding created successfully !',
                showCancelButton: false,
                confirmButtonText: 'OK',
                timer: 10000

            });
            return false;
        }

        function ValidateEsignRecord() {
            ;
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
            //;
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


        function showPdfBYHendler(PROPOSALPATH_FIRSTFORMATE, docType) {

            $("#List_pnl").hide();
            $("#View_pnl").show();

            //$('#ifrDisplay').attr('src', PROPOSALPATH_FIRSTFORMATE);
            var jasonData = '{"PDFPath": "' + PROPOSALPATH_FIRSTFORMATE + '","DocType": "' + docType + '" }';
            $.ajax({
                type: "POST",
                url: '<%=Page.ResolveClientUrl("~/CoS/Ordersheet.aspx/show_Pdf_BY_Hendler_Deed_Doc") %>',

                data: jasonData,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    $('#ifrDisplay').attr('src', msg.d)
                    //setTemlateVal(msg.d);
                },
                error: function (e) {
                    console.log("" + e);
                    //LoadNotice();
                }
            });


        }

    </script>
    >


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="SCMNG" runat="server"></asp:ScriptManager>

    <section class="content-header headercls">
        <div class="container-fluid">
            <div class="row">
                <div class="col-sm-3">
                    <h5>RRC Certificate Proceeding</h5>
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
                                        <li class="nav-item"><a class="nav-link disabled" href="">Proposal</a></li>
                                        <li class="nav-item"><a class="nav-link disabled" href="#CreateOrderSheet" data-toggle="tab">Ordersheet</a></li>
                                        <li class="nav-item"><a class="nav-link disabled" href="#Notice" data-toggle="tab">Notice</a></li>
                                        <li class="nav-item"><a class="nav-link disabled" href="ReportSeeking">Seek Report</a></li>
                                        <li class="nav-item"><a class="nav-link active disabled" href="#Payment" data-toggle="tab">RRC Certficate Proceeding </a></li>

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
                                                                <li class="nav-item">
                                                                    <a class="nav-link active" id="custom-tabs-one-home-tab" data-toggle="pill"
                                                                        href="#custom-tabs-one-ProposalForm" role="tab" aria-controls="custom-tabs-one-home"
                                                                        aria-selected="false">RRC Certificate sheet</a>
                                                                </li>
                                                                <li class="nav-item">
                                                                    <a class="nav-link" id="custom_tabs_one_profile_tab" data-toggle="pill" runat="server" clientidmode="Static" onclick="ShowEditor()" style="display: none"
                                                                        href="#custom-tabs-one-RegisteredForm" role="tab"
                                                                        aria-controls="custom-tabs-one-profile" aria-selected="true">Edit RRC Certificate sheet </a>

                                                                </li>
                                                            </ul>
                                                        </div>

                                                        <div class="card-body">
                                                            <div class="tab-content" id="custom-tabs-one-tabContent">
                                                                <div class="tab-pane active show" id="custom-tabs-one-ProposalForm" role="tabpanel"
                                                                    aria-labelledby="custom-tabs-one-home-tab">
                                                                    <!-- code here -->



                                                                    <div id="pnl_Proceding" runat="server" clientidmode="Static">

                                                                        <div class="displaydiv" id="Pnl_AddProceeding" runat="server">


                                                                            <%--<button type="button" class="btn btn-success" id="add_green_proceeding" href="#custom-tabs-one-RegisteredForm">Add Proceeding </button>--%>
                                                                            <a class="btn btn-success" id="A1" data-toggle="pill" clientidmode="Static"
                                                                                href="#custom-tabs-one-RegisteredForm" role="tab"
                                                                                aria-controls="custom-tabs-one-profile" aria-selected="true" runat="server" enableviewstate="true">Add Proceeding </a>


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
                                                                                                        <asp:Label ID="lblnotice" runat="server" Text='<%# Eval("hearingdate", "{0:dd/MM/yyyy}") %>' Font-Bold="true" />
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
                                                                                                        <asp:Label ID="lblDate" runat="server" Text='<%# Eval("inserteddate", "{0:dd/MM/yyyy}") %>' Font-Bold="true" />
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

                                                                                            <br />
                                                                                            <hr />
                                                                                        </ItemTemplate>
                                                                                        <FooterTemplate>
                                                                                            </table>
                                                                                        </FooterTemplate>

                                                                                    </asp:Repeater>

                                                                                    <br />
                                                                                    <br />

                                                                                </asp:Panel>

                                                                                <asp:Panel ID="pnlHearing" Visible="false" runat="server">

                                                                                    <asp:Repeater ID="RptHearing" runat="server">
                                                                                        <ItemTemplate>
                                                                                            <table>
                                                                                                <tr style="text-align: left">
                                                                                                    <td>Proceeding # <%# Container.ItemIndex + 3 %></td>
                                                                                                </tr>
                                                                                                <tr style="">
                                                                                                    <td>
                                                                                                        <asp:Label ID="lblhearing" runat="server" Text='<%# Eval("hearingdate", "{0:dd/MM/yyyy}") %>' Font-Bold="true" />
                                                                                                        <%--<asp:Label ID="lblhearing" runat="server" Text='<%#Eval("HEARINGDATE") %>' Font-Bold="true" />--%>
                                                                                                    </td>
                                                                                                </tr>
                                                                                            </table>

                                                                                            <span style="padding: 30px"></span>
                                                                                            <table>
                                                                                                <tr>
                                                                                                    <td style="text-align: justify">
                                                                                                        <asp:Label ID="lblCommentH" runat="server" Text='<%#Eval("PROCEEDING") %>' />
                                                                                                    </td>
                                                                                                </tr>
                                                                                            </table>
                                                                                            <p>&nbsp;&nbsp;&nbsp;</p>
                                                                                            <table>
                                                                                                <tr style="text-align: left;">
                                                                                                    <td>पेशी दिनांक:                                                                                                
                                                                                                    <%--                                                                                            <asp:Label ID="lblDate" runat="server" Font-Bold="true" Text='<%#Eval("LMDT") %>' /></td>--%>
                                                                                                        <asp:Label ID="lblDateH" runat="server" Text='<%# Eval("inserteddate", "{0:dd/MM/yyyy}") %>' Font-Bold="true" />
                                                                                                    <td></td>

                                                                                                    <td style="text-align: right; width: 72%;">
                                                                                                        <div>
                                                                                                            कलेक्टर ऑफ़ स्टाम्प्स
                                                                                        <br />
                                                                                                            <asp:Label ID="lblDRofficeH" runat="server"></asp:Label>
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


                                                                                </asp:Panel>

                                                                                <asp:Panel ID="pnlRRC_Certificate" Visible="false" runat="server">

                                                                                    <asp:Repeater ID="RptRRC_Certificate" runat="server">
                                                                                        <ItemTemplate>
                                                                                            <table>
                                                                                                <tr style="text-align: left">
                                                                                                    <td>Proceeding # <%# Container.ItemIndex + 4 %></td>
                                                                                                </tr>
                                                                                                <tr style="">
                                                                                                    <td>
                                                                                                        <asp:Label ID="lblRRC_Certificate" runat="server" Text='<%# Eval("inserteddate", "{0:dd/MM/yyyy}") %>' Font-Bold="true" />
                                                                                                        <%--<asp:Label ID="lblhearing" runat="server" Text='<%#Eval("HEARINGDATE") %>' Font-Bold="true" />--%>
                                                                                                    </td>
                                                                                                </tr>
                                                                                            </table>

                                                                                            <span style="padding: 30px"></span>
                                                                                            <table>
                                                                                                <tr>
                                                                                                    <td style="text-align: justify">
                                                                                                        <asp:Label ID="lblCommentCertificate" runat="server" Text='<%#Eval("PROCEEDING") %>' />
                                                                                                    </td>
                                                                                                </tr>
                                                                                            </table>
                                                                                            <p>&nbsp;&nbsp;&nbsp;</p>
                                                                                            <table>
                                                                                                <tr style="text-align: left;">
                                                                                                    <td>दिनांक:                                                                                                
                                                                                                    <%--                                                                                            <asp:Label ID="lblDate" runat="server" Font-Bold="true" Text='<%#Eval("LMDT") %>' /></td>--%>
                                                                                                        <asp:Label ID="lblDateCertificate" runat="server" Text='<%# Eval("inserteddate", "{0:dd/MM/yyyy}") %>' Font-Bold="true" />
                                                                                                    <td></td>

                                                                                                    <td style="text-align: right; width: 72%;">
                                                                                                        <div>
                                                                                                            कलेक्टर ऑफ़ स्टाम्प्स
                                                                                        <br />
                                                                                                            <asp:Label ID="lblDRofficeCertificate" runat="server"></asp:Label>
                                                                                                        </div>
                                                                                                    </td>

                                                                                                </tr>

                                                                                            </table>

                                                                                            <br />
                                                                                            <hr />
                                                                                        </ItemTemplate>
                                                                                        <FooterTemplate>
                                                                                        </FooterTemplate>

                                                                                    </asp:Repeater>

                                                                                    <br />
                                                                                    <br />
                                                                                </asp:Panel>
                                                                            </div>
                                                                            <br>
                                                                        </div>
                                                                    </div>

                                                                    <br>
                                                                </div>

                                                                <div class="tab-pane fade" id="custom-tabs-one-RegisteredForm"
                                                                    aria-labelledby="custom-tabs-one-profile-tab">
                                                                    <!-- table -->
                                                                    <asp:DropDownList ID="ddlTemplates1" runat="server" OnChange="SetTemplate(this.value)" CssClass="form-control" EnableTheming="false">
                                                                    </asp:DropDownList>


                                                                    <textarea id="summernote" runat="server" clientidmode="Static" style="text-align: justify;"></textarea>
                                                                    <div class="newdiv">

                                                                        <a onclick="AddOrdersheet()" href="#custom-tabs-one-profile-tab" class="btn btn-success">Save Note</a>
                                                                    </div>


                                                                </div>
                                                                <br />
                                                                <br />
                                                                <div id="custom_tabs_one_Display" runat="server" clientidmode="Static">
                                                                    <div class="collect-txt">
                                                                        <div class="main-box htmldoc" style="width: 100%; margin: 0 auto; text-align: center; border: 1px solid #ccc; padding: 30px 30px 30px 30px;">






                                                                            <h2 style="font-size: 18px; margin: 0; font-weight: 600;">न्यायालय कलेक्टर ऑफ़ स्टाम्प्स, 
                                                                                <asp:Label ID="lblHeadingDist" runat="server"></asp:Label>
                                                                                (म.प्र.)</h2>
                                                                            <h3 style="margin: 0; margin: 10px; font-size: 16px;">प्ररूप-अ</h3>
                                                                            <h2 style="font-size: 16px; margin: 0; margin-bottom: 10px;">(परिपत्र दो-1 की कंडिका 1)</h2>
                                                                            <h3 style="margin: 0; margin: 10px; font-size: 16px;">राजस्व आदेशपत्र</h3>
                                                                            <h2 style="font-size: 16px; margin: 0; margin-bottom: 10px;">कलेक्टर ऑफ़ स्टाम्प,
                                                                                <asp:Label ID="lblHeadingDist1" runat="server"></asp:Label>
                                                                                के न्यायालय में प्रकरण क्रमांक-  
                                                                        <asp:Label ID="lblCaseNumber" runat="server"></asp:Label>
                                                                            </h2>
                                                                            <br>

                                                                            <table style="width: 100%; border: 1px solid black; border-collapse: collapse;">
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


                                                                                            <%--<b style="float: left; text-align: center; padding: 2px 0 5px 0;">पेशी दिनांक
                                                                                        <br />--%>
                                                                                            <asp:Label ID="lblHearingDt" runat="server" ClientIDMode="Static"></asp:Label></b>
                                                                                            <p>
                                                                                                <%-- प्रकरण शीर्ष ब-103 एवं भारतीय स्टाम्प अधिनियम की धारा-33 के अधीन प्रकरण दर्ज कर अनावेदकों को सूचना पत्र जारी हो।
                                                                                     पेशी दिनांक---%>
                                                                                            </p>


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

                                                                <asp:Panel ID="pnlDraft" Visible="True" runat="server">

                                                                    <asp:Button ID="btnDraftSave" runat="server" Text="Save Draft" Style="display: none" class="btn btn-success" OnClick="btnDraftSave_Click" ClientIDMode="static" />

                                                                </asp:Panel>
                                                                <br />
                                                                  <div class="form-group row">
                                                                    <div class="col-sm-4 ml-lg-auto mt-2" style="text-align: end;">

                                                                        <asp:Button ID="btnSaveRRCcerti" runat="server" Text="Submit" Style="display: none" class="btn btn-success" OnClick="btnRRC_Certi_Click" ClientIDMode="Static" />
                                                                    </div>

                                                                </div>
                               
                                                                <asp:Panel ID="pnlEsignDSC" Visible="false" runat="server">
                                                                    <div class="certi_info bg-light m16">eSign / DSC</div>
                                                                    <br />
                                                                    <div class="form-group row">
                                                                        <div class="col-sm-3">
                                                                            <%-- <select type="text" class="form-control" id="input7">
                                                                                    <option>Select</option>
                                                                                    <option>Aadhar eSign </option>
                                                                                    <option>DSC</option>
                                                                                </select>--%>

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
                                                                            <asp:DropDownList ID="ddleAuthMode" runat="server" CssClass="form-control">

                                                                                <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                                                                <asp:ListItem Text="OTP" Value="1"></asp:ListItem>
                                                                                <asp:ListItem Text="Biometric" Value="2"></asp:ListItem>
                                                                            </asp:DropDownList>
                                                                            <%-- <asp:Label ID="Label1" runat="server"></asp:Label>
                                                                                <asp:TextBox ID="TextBox1" placeholder="Enter last 4 digit of Adhar Card" MaxLength="4" Enabled="true" onkeypress="return NumberOnly(event)" ClientIDMode="Static" CssClass="form-control" runat="server" AutoCompleteType="None" autocomplete="off"></asp:TextBox>--%>
                                                                        </div>
                                                                        <div class="col-sm-3">
                                                                            <asp:Label ID="hsmMsg" runat="server"></asp:Label>
                                                                            <asp:TextBox ID="TxtLast4Digit" placeholder="Enter last 4 digit of Adhar Card" MaxLength="4" Enabled="true" onkeypress="return NumberOnly(event)" ClientIDMode="Static" CssClass="form-control" runat="server" AutoCompleteType="None" autocomplete="off"></asp:TextBox>
                                                                        </div>
                                                                        <div class="col-sm-3">
                                                                            <%-- <button type="button" class="btn btn-success">eSign/DSC</button>--%>
                                                                            <asp:Button ID="btnEsignDSC" runat="server" OnClick="btnEsignDSC_Click" class="btn btn-success" Text="eSign/DSC" OnClientClick="return ValidateEsignRecord()" />
                                                                        </div>
                                                                    </div>
                                                                </asp:Panel>

                                                            </div>

                                                            <div class="tab-pane fade" id="custom-tabs-one-RegisteredForm2"
                                                                aria-labelledby="custom-tabs-one-profile-tab">


                                                                <textarea id="Textarea1" runat="server" clientidmode="Static" style="text-align: justify;"></textarea>
                                                                <div class="newdiv">
                                                                    <%--<asp:Button ID="btnSave" class="btn btn-success" runat="server" Text="Save" OnClick="btnSave_Click" />--%>
                                                                    <a onclick="AddOrdersheet()" href="#custom-tabs-one-ProposalForm" class="btn btn-success">Save</a>
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
                                                                                        <a class="nav-link" id="custom-tabs-four-settings-tab" data-toggle="pill" href="#custom-tabs-four-settings" role="tab" aria-controls="custom-tabs-four-settings" aria-selected="">Previous Proceeding</a>
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

                                                                                                    <asp:GridView ID="grdSRDoc" CssClass="table table-bordered table-condensed table-striped table-hover"
                                                                                                        DataKeyNames=""
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

                                                                                                            <asp:TemplateField HeaderText="Pages" HeaderStyle-Font-Bold="false">

                                                                                                                <HeaderStyle Width="10%" HorizontalAlign="Center" />

                                                                                                                <ItemStyle Width="10%" HorizontalAlign="Center" />
                                                                                                            </asp:TemplateField>
                                                                                                            <asp:TemplateField HeaderText="View" HeaderStyle-Font-Bold="false">
                                                                                                                <ItemTemplate>
                                                                                                                    <%--  <a href="#" class="gridViewToolTip" id="btnModalPopup" onclick='openSrDoc("<%# Eval("ORDRSHEETPATH")%>")'>
                                                                                                                <button type="button" class="btn btn-info btn-sm"><i class="fas fa-eye"></i></button>
                                                                                                            </a>--%>
                                                                                                                    <a href="#" class="gridViewToolTip" id="btnModalPopup" onclick='showPdfBYHendler("<%# Eval("FILE_PATH")%>","<%# Eval("docType")%>")'>

                                                                                                                        <button type="button" class="btn btn-info"><i class="fas fa-eye"></i></button>
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

                                                                                            <iframe id="ifrDisplay" class="embed-responsive-item" height="750"></iframe>
                                                                                        </div>



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
                                                                                                    <iframe id="RecentdocPath" runat="server" width='550' height='750'></iframe>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td>
                                                                                                    <iframe id="RecentProposalDoc" runat="server" width='550' height='750'></iframe>
                                                                                                </td>
                                                                                            </tr>

                                                                                        </table>
                                                                                        <iframe id="ifPDFViewerAll" clientidmode="Static" runat="server" scrolling="auto" height='750'></iframe>

                                                                                        <%--                                         </div>

                                                                                    </div>
                                                                                </div>--%>
                                                                                    </div>
                                                                                    <div class="tab-pane fade " id="custom-tabs-four-settings" role="tabpanel" aria-labelledby="custom-tabs-four-settings-tab">
                                                                                        <div>

                                                                                            <asp:Label ID="lblPrevious" runat="server" Visible="false"></asp:Label>

                                                                                            <iframe id="IfProceeding" clientidmode="Static" runat="server" scrolling="auto" height='750'></iframe>
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
            <asp:PostBackTrigger ControlID="btnDraftSave" />
            <asp:PostBackTrigger ControlID="btnSaveRRCcerti" />
        </Triggers>
    </asp:UpdatePanel>





    <script type="text/javascript">


        function ShowDocList() {

            $("#List_pnl").show();
            $("#View_pnl").hide();
        }
        function openSrDoc(FILE_PATH) {

            //alert(FILE_PATH);
            ;
            $("#List_pnl").hide();
            $("#View_pnl").show();
            $('#ifrDisplay').attr('src', FILE_PATH);

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

    </script>


    <script type="text/javascript"> 



        //function ShowDocList() {
        //    //alert("Hello");
        //    $("#pnl2").show();
        //    $("#pnl3").hide();
        //}
        //function openSrDoc(FILE_PATH) {

        //    //alert(FILE_PATH);
        //    
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
        function SetTemplate(val) {

            //alert(val);
            //var TemplateType = '{"TemId":"' + val+'"}';
            $.ajax({
                type: "POST",

                url: '<%=Page.ResolveClientUrl("~/CoS/RRC_Certificate_Proceeding.aspx/GetTemplate_Notice") %>',
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

            $(".note-editable.card-block p").html(d);  //show for editore
            //alert(d);
            document.getElementById("summernote").value = d; // set value
            //  alert(document.getElementById("summernote  .note-editing-area p").value);


        }

        function AddOrdersheet() {
            // alert("Hello");

            document.getElementById("pContent").innerHTML = $("#summernote").val();
            //document.getElementById("summernote").value = "";
            document.getElementById("custom_tabs_one_profile_tab").style.display = "block";
            $("#custom_tabs_one_profile_tab").removeClass("active show");
            $('#custom-tabs-one-RegisteredForm').removeClass("active show");
            // console.log("Removed 'active show' from custom-tabs-one-RegisteredForm");


            //$("#custom_tabs_one_profile_tab").addClass("active ");
            $("#custom-tabs-one-home-tab").addClass("active ");

            $('#custom_tabs_one_Display').addClass("active");
            //$('#custom_tabs_one_Display').attr("disabled", true);
            document.getElementById('btnDraftSave').style.display = "block";

            document.getElementById('pnl_Proceding').style.display = "none";
            //document.getElementById('#custom_tabs_one_Display').style.display = "block";


        }
        function DisplayESign() {

            /*$("#custom-tabs-one-RegisteredForm").addClass("active");*/
            $('#custom-tabs-one-RegisteredForm').removeClass("active show");
            $('#custom_tabs_one_Display').addClass("active");
            document.getElementById('pnl_Proceding').style.display = "none";
            document.getElementById("custom_tabs_one_profile_tab").style.display = "block";
            $("#custom_tabs_one_profile_tab").removeClass("active show");
        }
        function ShowEditor() {
            /*$("#custom-tabs-one-RegisteredForm").addClass("active");*/
            $('#custom-tabs-one-RegisteredForm').addClass("active show");
            $('#custom_tabs_one_Display').removeClass("active");
            document.getElementById('btnDraftSave').style.display = "none";
            //document.getElementById('pnl_Proceding').style.display = "none";
            //document.getElementById("custom_tabs_one_profile_tab").style.display = "block";
            //$("#custom_tabs_one_profile_tab").removeClass("active show");
        }




    </script>
    <script type="text/javascript">
        function printPDF() {

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



    <script src="../dist/plugins/jquery/jquery.min.js"></script>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.7.2/jquery.min.js"></script>
    <script src="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.8.9/jquery-ui.js" type="text/javascript"></script>
    <link href="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.8.9/themes/start/jquery-ui.css"
        rel="stylesheet" type="text/css" />


</asp:Content>
