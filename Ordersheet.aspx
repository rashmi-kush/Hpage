<%@ Page Title="" Language="C#" MasterPageFile="~/CoS/CoSMaster.Master" AutoEventWireup="true" CodeBehind="Ordersheet.aspx.cs" ValidateRequest="false" Inherits="CMS_Sampada.CoS.Ordersheet" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Styles/sweetalert.css" rel="stylesheet" />
    <link href="../dist/css/Calender-Style.css" rel="stylesheet" />
    <script src="../Scripts/sweetalert.min.js"></script>
    <script src="../assets/js/3.3.1/sweetalert2@11.js"></script>

    <style type="text/css">
        .deepak table {
            height: 900px;
        }
    </style>

   

    <script type="text/javascript"> 
        function checkDate(sender, args) {

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
                title: 'Ordersheet draft saved successfully !',
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


        function ValidateRecord() {

            var HearingDate = $("#ContentPlaceHolder1_txtHearingDate").val();
            var lblHearingDate = $("#lblHearingDt").val();
            if (HearingDate == "" && lblHearingDate == "") {
                //alert("Please select Hearing date");
                Swal.fire({
                    icon: 'info',
                    title: 'Please select hearing date'

                })
                return false;
            }
            else if (!document.getElementById('rdReportYes').checked) {

                if (!confirm("Do you want to proceed ordersheet without Seek report ? ")) {

                    return false;
                }
            }
            //else if (!confirm("Do you want to proceed ordersheet without seek report ? ")) {

            //    return false;
            //}

        }
        function ValidateRecord_Reader() {

            var HearingDate = $("#ContentPlaceHolder1_txtHearingDate").val();
            var lblHearingDate = $("#lblHearingDt").val();
            if (HearingDate == "" && lblHearingDate == "") {
                //alert("Please select Hearing date");
                Swal.fire({
                    icon: 'info',
                    title: 'Please select hearing date'

                })
                return false;
            }

            //else if (!confirm("Do you want to proceed ordersheet without seek report ? ")) {

            //    return false;
            //}

        }

        function ShowMessage() {


            Swal.fire({
                icon: 'success',
                title: 'Ordersheet create successfully',
                showCancelButton: false,
                confirmButtonText: 'OK',
            }).then((result) => {
                if (result.value) {
                    window.location = 'Notice.aspx?Case_Number=' + case_number;
                }
                else {
                    alert('user click on cancel button');
                }
            });
        }

        function ShowMessageDSC(loc) {

            //alert("hello");
            Swal.fire({
                icon: 'success',
                title: 'Signed ordersheet saved successfully',
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

            var SignOption = $("#ContentPlaceHolder1_ddl_SignOption").val();

            if (SignOption == "0") {

                Swal.fire({
                    icon: 'info',
                    title: 'Please select eSign or DSC in dropdown'

                })
                return false;
            }
            else if ((SignOption == "1") && document.getElementById("TxtLast4Digit").value == "") {

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


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="SCMNG" runat="server"></asp:ScriptManager>

    <section class="content-header headercls">
        <div class="container-fluid">
            <div class="row">
                <div class="col-sm-3">
                    <h5>Ordersheet </h5>
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
                                        <li class="nav-item"><a class="nav-link active disabled" href="#">Order Sheet</a></li>
                                        <li class="nav-item"><a class="nav-link disabled" href="#">Notice</a></li>
                                        <li class="nav-item"><a class="nav-link disabled" href="#">Seek Report</a></li>
                                        <li class="nav-item"><a class="nav-link disabled" href="#">Notice Proceeding</a></li>
                                        <li class="nav-item"><a class="nav-link disabled" href="#">Hearing</a></li>
                                        <li class="nav-item"><a class="nav-link disabled" href="#">Payment</a></li>
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
                                                                <li class="nav-item">
                                                                    <a class="nav-link active" id="custom-tabs-one-home-tab" data-toggle="pill"
                                                                        href="#custom-tabs-one-ProposalForm" role="tab" aria-controls="custom-tabs-one-home"
                                                                        aria-selected="false">Order Sheet</a>
                                                                </li>
                                                                <li class="nav-item">
                                                                    <a class="nav-link" id="custom_tabs_one_profile_tab" data-toggle="pill"
                                                                        href="#custom-tabs-one-RegisteredForm" role="tab" onclick="calljqueryfile()"
                                                                        aria-controls="custom-tabs-one-profile" aria-selected="true" runat="server">Edit Ordersheet </a>

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
                                                                         
                                                                        </div>
                                                                    </asp:Panel>
                                                                    <div class="">
                                                                        <!-- Doc detail html -->
                                                                        <div class="main-box htmldoc" style="width: 100%; margin: 0 auto; text-align: center; border: 1px solid #ccc; padding: 30px 30px 30px 30px;">
                                                                            <h2 style="font-size: 18px; margin: 0; font-weight: 600;">न्यायालय कलेक्टर ऑफ़ स्टाम्प्स, 
                                                                                <asp:Label ID="lblHeadingDist" runat="server"></asp:Label>
                                                                                (म.प्र.)</h2>
                                                                            <h3 style="margin: 0; margin: 10px; font-size: 16px;">प्रारूप-अ</h3>
                                                                            <h2 style="font-size: 16px; margin: 0; margin-bottom: 10px;">(परिपत्र दो-1 की कंडिका 1)</h2>
                                                                            <h3 style="margin: 0; margin: 10px; font-size: 16px;">सेक्शन 40(D) - भारतीय स्टाम्प अधीनियम 1899 की धारा 40 (1)(ख) के अंतर्गत एवं 40 (1)(घ) के पालन में</h3>
                                                                            <h2 style="font-size: 16px; margin: 0; margin-bottom: 10px;">आदेश पत्रक</h2>
                                                                            <br>
                                                                            <div style="display: flex; justify-content: start; align-items: baseline;">
                                                                                <div>
                                                                                    <p style="font-size: 15px; line-height: 22px; text-align: left; width: 190px; margin: 0">
                                                                                        कलेक्टर ऑफ़ स्टाम्प्स का नाम :
                                                                                    </p>
                                                                                </div>
                                                                                <div>
                                                                                    <p style="font-size: 15px; line-height: 22px; text-align: left; margin: 0; margin-left: 50px">
                                                                                        <asp:Label ID="lblCOSOfficeNameHi" runat="server"></asp:Label>

                                                                                    </p>
                                                                                </div>
                                                                            </div>

                                                                            <div style="display: flex; justify-content: start; align-items: baseline;">
                                                                                <div>
                                                                                    <p style="font-size: 15px; line-height: 22px; text-align: left; width: 190px; margin: 0;">
                                                                                        प्रकरण  क्रमांक :
                                                                                    </p>
                                                                                </div>
                                                                                <div>
                                                                                    <p style="font-size: 15px; line-height: 22px; text-align: left; margin: 0; margin-left: 50px">

                                                                                        <asp:Label ID="lblCaseNo" runat="server"></asp:Label>

                                                                                    </p>
                                                                                </div>
                                                                            </div>

                                                                            <div style="display: flex; justify-content: start; align-items: baseline;">
                                                                                <div>
                                                                                    <p style="font-size: 15px; line-height: 22px; text-align: left; width: 190px; margin: 0;">
                                                                                        विषय :
                                                                                    </p>
                                                                                </div>
                                                                                <div>
                                                                                    <p style="font-size: 15px; line-height: 22px; text-align: left; margin: 0; margin-left: 50px">
                                                                                        बाज़ार मूल्य अवधारण / मुद्रांक शुल्क निर्धारण
                                                                                    </p>
                                                                                </div>
                                                                            </div>

                                                                            <p style="font-size: 15px; line-height: 30px; text-align: center; margin: 0; margin-top: 50px; margin-bottom: 25px;">
                                                                                <b>पक्षकारों के नाम </b>
                                                                            </p>

                                                                            <div>
                                                                                <div style="float: left;">
                                                                                    <p class="p5">
                                                                                        <b>आवेदक :   </b>
                                                                                    </p>
                                                                                </div>
                                                                                <table style="width: 100%; border: 1px solid black; border-collapse: collapse;">
                                                                                    <tr>
                                                                                        <th style="border: 1px solid black; border-collapse: collapse; line-height: 20px; /* height: 31px; */
        padding: 5px; font-size: 14px;">क्रमांक    </th>
                                                                                        <th class="new_th" style="border: 1px solid black; border-collapse: collapse; line-height: 20px; /* height: 31px; */
        padding: 0 50px 0 50px !important; font-size: 14px;">नाम </th>
                                                                                        <th style="border: 1px solid black; border-collapse: collapse; line-height: 20px; /* height: 31px; */
        padding: 5px; font-size: 14px;">पता </th>
                                                                                       
                                                                                    </tr>

                                                                                    <tbody>
                                                                                        <tr>
                                                                                            <td style="border: 1px solid black; border-collapse: collapse; /*    height: 40px; */ padding: 5px; font-size: 14px;">1.</td>
                                                                                            <td style="border: 1px solid black; border-collapse: collapse; /*    height: 40px; */ padding: 5px; font-size: 14px;">म प्र शासन<br />
                                                                                            </td>
                                                                                            <td style="border: 1px solid black; border-collapse: collapse; /*    height: 40px; */ padding: 5px; font-size: 14px;">
                                                                                                <asp:Label ID="lblSRONameHi" runat="server"></asp:Label>
                                                                                            </td>
                                                                                            
                                                                                        </tr>
                                                                                    </tbody>
                                                                                </table>
                                                                            </div>

                                                                            <div class="m-top">
                                                                                <p style="font-size: 15px; /* line-height: 5px; */
        text-align: center; margin: 0; /* margin-left: 0px; */
        margin-top: 15px; margin-bottom: 10px;">
                                                                                    <b>विरुद्ध   </b>
                                                                                </p>
                                                                                <div style="float: left;">
                                                                                    <p style="font-size: 17px; line-height: 30px; text-align: center; margin: 0; margin-right: 50px;">
                                                                                        <b>अनावेदक :   </b>
                                                                                    </p>

                                                                                </div>
                                                                               

                                                                                <asp:GridView ID="grdlPartys" CssClass="table table-condensed table-hover dastavej"
                                                                                    runat="server" CellPadding="4" BorderStyle="Solid" BorderColor="Black" BorderWidth="1px" AutoGenerateColumns="false" Visible="true" EmptyDataText="No record found">
                                                                                    <AlternatingRowStyle BackColor="White" />
                                                                                    <Columns>
                                                                                        <asp:TemplateField HeaderText="क्रमांक" HeaderStyle-VerticalAlign="Top" HeaderStyle-HorizontalAlign="Center">
                                                                                            <ItemTemplate>
                                                                                                <itemtemplate><%#Container.DataItemIndex+1 %> </itemtemplate>
                                                                                            </ItemTemplate>
                                                                                            <ItemStyle Width="2%" HorizontalAlign="Center" />
                                                                                            <HeaderStyle Width="2%" HorizontalAlign="Center" />
                                                                                        </asp:TemplateField>
                                                                                         <asp:TemplateField HeaderText="पक्षकार का प्रकार">
                                                                                            <ItemTemplate>
                                                                                                <%#Eval("TYPE") %>
                                                                                            </ItemTemplate>
                                                                                            <HeaderStyle Width="10%" HorizontalAlign="Center" />
                                                                                            <ItemStyle Width="10%" HorizontalAlign="Center" />
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="नाम">
                                                                                            <ItemTemplate>
                                                                                                <%#Eval("Name") %>
                                                                                            </ItemTemplate>
                                                                                            <HeaderStyle Width="10%" HorizontalAlign="Center" />
                                                                                            <ItemStyle Width="10%" HorizontalAlign="Center" />
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="पता">
                                                                                            <ItemTemplate>
                                                                                                <%#Eval("Address") %>
                                                                                            </ItemTemplate>
                                                                                            <HeaderStyle Width="10%" HorizontalAlign="Center" />
                                                                                            <ItemStyle Width="10%" HorizontalAlign="Center" />
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="पिता / पति">
                                                                                            <ItemTemplate>
                                                                                                <%#Eval("Father Name") %>
                                                                                            </ItemTemplate>
                                                                                            <HeaderStyle Width="10%" HorizontalAlign="Center" />
                                                                                            <ItemStyle Width="10%" HorizontalAlign="Center" />
                                                                                        </asp:TemplateField>


                                                                                    </Columns>
                                                                                    <%--  <EditRowStyle BackColor="#999999" />--%>
                                                                                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                                                                    <HeaderStyle ForeColor="black" Font-Size="14px" />
                                                                                   
                                                                                </asp:GridView>



                                                                                <div style="display: flex; justify-content: start; align-items: baseline;">
                                                                                    <div>
                                                                                        <p style="font-size: 15px; line-height: 22px; text-align: left;  margin: 0">
                                                                                            सब रजिस्ट्रार कार्यालय  :
                                                                                        </p>
                                                                                    </div>
                                                                                    <div>
                                                                                        <p style="font-size: 15px; line-height: 22px; text-align: left; margin: 0; margin-left: 150px">
                                                                                            <asp:Label ID="lblSROffice" runat="server"></asp:Label>
                                                                                        </p>
                                                                                    </div>
                                                                                </div>

                                                                              

                                                                                <div style="display: flex; justify-content: start; align-items: baseline;">
                                                                                    <div>
                                                                                        <p style="font-size: 15px; line-height: 22px; text-align: left;  margin: 0">
                                                                                            दस्तावेज़ पंजीयन क्रमांक / प्रारंभ आई डी  :
                                                                                        </p>
                                                                                    </div>
                                                                                    <div>
                                                                                        <p style="font-size: 15px; line-height: 22px; text-align: left; margin: 0; margin-left: 50px">
                                                                                            <asp:Label ID="lblProposal" runat="server"></asp:Label>
                                                                                        </p>
                                                                                    </div>
                                                                                </div>



                                                                                <div style="display: flex; justify-content: start; align-items: baseline;">
                                                                                    <div>
                                                                                        <p style="font-size: 15px; line-height: 22px; text-align: left;  margin: 0">
                                                                                            जिला :
                                                                                        </p>
                                                                                    </div>
                                                                                    <div>
                                                                                        <p style="font-size: 15px; line-height: 22px; text-align: left; margin: 0; margin-left: 246px">
                                                                                            <asp:Label ID="lblDistrict" runat="server"></asp:Label>
                                                                                        </p>
                                                                                    </div>
                                                                                </div>

                                                                              

                                                                                <div style="display: flex; justify-content: start; align-items: baseline;">
                                                                                    <div>
                                                                                        <p style="font-size: 15px; line-height: 22px; text-align: left;  margin: 0">
                                                                                            पहली सुनवाई की तारीख  :
                                                                                        </p>
                                                                                    </div>
                                                                                    <div>
                                                                                        <p style="font-size: 15px; line-height: 22px; text-align: left; margin: 0; margin-left: 140px">
                                                                                            <asp:Label ID="lblHearingDt1" ClientIDMode="Static" runat="server"></asp:Label>
                                                                                        </p>
                                                                                    </div>
                                                                                </div>


                                                                                <div style="display: flex; justify-content: start; align-items: baseline;">
                                                                                    <div>
                                                                                        <p style="font-size: 15px; line-height: 22px; text-align: left; margin: 0">
                                                                                            प्रकरण परिबद्ध करने वाले अधिकारी का नाम   :
                                                                                        </p>
                                                                                    </div>
                                                                                    <div>
                                                                                        <p style="font-size: 15px; line-height: 22px; text-align: left; margin: 0; margin-left: 30px">
                                                                                            <asp:Label ID="lblSource" runat="server"></asp:Label>
                                                                                        </p>
                                                                                    </div>
                                                                                </div>

                                                                            </div>





                                                                            <p style="font-size: 15px; line-height: 30px; text-align: center; margin: 0; margin-left: 0px; margin-top: 15px; margin-bottom: 3px;">
                                                                                <b>दस्तावेजों की सूची</b>
                                                                            </p>

                                                                            <div class="row">
                                                                                <div class="col-lg-12 p0">

                                                                                    <asp:GridView ID="grdDocFile" CssClass="table table-condensed table-hover dastavej"
                                                                                        runat="server" CellPadding="4" BorderStyle="Solid" BorderColor="Black" BorderWidth="1px" AutoGenerateColumns="false" Visible="true" EmptyDataText="No record found">
                                                                                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                                                                        <Columns>
                                                                                            <asp:TemplateField HeaderText="क्रमांक" HeaderStyle-VerticalAlign="Top" HeaderStyle-HorizontalAlign="Center">
                                                                                                <ItemTemplate>
                                                                                                    <itemtemplate><%#Container.DataItemIndex+1  %> </itemtemplate>
                                                                                                </ItemTemplate>
                                                                                                <ItemStyle Width="2%" HorizontalAlign="Center" />
                                                                                                <HeaderStyle Width="2%" HorizontalAlign="Center" />
                                                                                            </asp:TemplateField>

                                                                                            <asp:TemplateField HeaderText="प्रस्ताब के साथ दस्तावेजों की सूची " HeaderStyle-HorizontalAlign="Center">
                                                                                                <ItemTemplate>
                                                                                                    <%#Eval("E_REGISTRY_DEED_DOC_NAME") %>
                                                                                                </ItemTemplate>
                                                                                                <HeaderStyle Width="12%" HorizontalAlign="Center" />
                                                                                                <ItemStyle Width="12%" HorizontalAlign="Center" />
                                                                                            </asp:TemplateField>
                                                                                            <asp:TemplateField HeaderText="पृष्ठों की संख्या " HeaderStyle-HorizontalAlign="Center">
                                                                                                <ItemTemplate>
                                                                                                    <%#Eval("DocCount") %>
                                                                                                </ItemTemplate>
                                                                                                <HeaderStyle Width="3%" HorizontalAlign="Center" />
                                                                                                <ItemStyle Width="3%" HorizontalAlign="Center" />
                                                                                            </asp:TemplateField>
                                                                                        </Columns>
                                                                                        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                                                                        <HeaderStyle ForeColor="black" Font-Size="14px" />
                                                                                       
                                                                                    </asp:GridView>

                                                                                </div>
                                                                            </div>
                                                                            <br>
                                                                            <hr>
                                                                            <br>



                                                                            <h2 style="font-size: 18px; margin: 0; font-weight: 600;">न्यायालय कलेक्टर ऑफ़ स्टाम्प्स, 
                                                                                <asp:Label ID="lblHeadingDist2" runat="server"></asp:Label>
                                                                                (म.प्र.)</h2>
                                                                            <h3 style="margin: 0; margin: 10px; font-size: 16px;">प्रारूप-अ</h3>
                                                                            <h2 style="font-size: 16px; margin: 0; margin-bottom: 10px;">(परिपत्र दो-1 की कंडिका 1)</h2>
                                                                            <h3 style="margin: 0; margin: 10px; font-size: 16px;">राजस्व आदेशपत्र</h3>
                                                                            <h2 style="font-size: 16px; margin: 0; margin-bottom: 10px;">कलेक्टर ऑफ़ स्टाम्प,
                                                                                <asp:Label ID="lblCOSOfficeNameHi1" runat="server"></asp:Label>
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

                                                                                    
                                                                                <style type="text/css">
                                                                                    .mok p {
                                                                                        text-align:justify;
                                                                                    }
                                                                                </style>
                                                                                    <td style="border: 1px solid black; border-collapse: collapse; /*    height: 40px; */ padding: 5px; font-size: 14px;">
                                                                                        <div style="padding: 2px;" class="mok">
                                                                                            <p style="text-align:center !important;">
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

                                                                                            <b style="float: left; text-align: center; padding: 2px 0 5px 0;">पेशी दिनांक
                                                                                        <br />
                                                                                                <asp:Label ID="lblHearingDt" runat="server" ClientIDMode="Static"></asp:Label></b>



                                                                                            <b style="float: right; text-align: center; padding: 2px 0 5px 0;">कलेक्टर ऑफ़ स्टाम्प्स,<br />
                                                                                                <asp:Label ID="lblCOSOfficeNameHi2" runat="server"></asp:Label>

                                                                                                <br />
                                                                                                <br />
                                                                                            </b>
                                                                                        </div>
                                                                                    </td>

                                                                                    <td style="border: 1px solid black; border-collapse: collapse; /*    height: 40px; */ padding: 5px; font-size: 14px;"></td>
                                                                                </tr>
                                                                            </table>

                                                                            <!-- <%-- </div>--%> -->
                                                                        </div>
                                                                    </div>
                                                                    <br>

                                                                    <asp:Panel ID="pnlHearingDate" Visible="True" runat="server">
                                                                        <div class="form-group row pl-2">
                                                                            <label for="inputEmail3" class="col-sm-3 col-form-label">Select Hearing Date</label>
                                                                            <div class="col-sm-3">
                                                                              
                                                                                <asp:TextBox ID="txtHearingDate" CssClass="form-control" runat="server" onChange="SetHearingDate(this)" onfocus="generateCalenderByCode()" placeHolder="dd/mm/yyyy"></asp:TextBox>
                                                                               
                                                                            </div>
                                                                            <asp:Button ID="btnDraftSave" runat="server" Text="Save Ordersheet Draft" class="btn btn-success" OnClick="btnDraftSave_Click" />
                                                                        </div>
                                                                        <div id="divCalender" class="calendar-section" runat="server" style="display: none">
                                                                            <div class="calendar-inner-sec">
                                                                                <span class="der"></span>
                                                                                <asp:Calendar ID="Calendar1" runat="server" ClientIDMode="Static" OnVisibleMonthChanged="Calendar1_VisibleMonthChanged" OnDayRender="Calendar1_DayRender" OnSelectionChanged="Calendar1_SelectionChanged"></asp:Calendar>
                                                                            </div>
                                                                        </div>

                                                                        <div class="form-group row pl-2">
                                                                        </div>

                                                                    </asp:Panel>

                                                                    <asp:Panel ID="pnlSeekReport" Visible="false" runat="server">



                                                                        <div class="mycheck mul_check">
                                                                            <h6><b style="padding-right: 12px;">Do you want to seek additional report?  </b></h6>
                                                                            <div class="custom-control custom-radio mycb">
                                                                                <asp:RadioButton ID="rdReportYes" runat="server" GroupName="rdReportSeek" ClientIDMode="Static" Text="Yes" OnCheckedChanged="rdReportYes_CheckedChanged" AutoPostBack="true" OnClientClick="Loader();" />

                                                                            </div>
                                                                            <div class="custom-control custom-radio mycb">
                                                                                <asp:RadioButton ID="rdReportNo" runat="server" GroupName="rdReportSeek" Checked="true" Text="No" />

                                                                            </div>
                                                                        </div>

                                                                    </asp:Panel>

                                                                    <asp:Panel ID="pnlBtnSave" runat="server" Visible="false">
                                                                        <div class="form-group row">
                                                                            <div class="col-sm-4 ml-lg-auto mt-2" style="text-align: end;">
                                                                                <%--<button type="button" class="btn btn-success">Submit </button>--%>
                                                                                <asp:Button ID="btnFinalSubmit" runat="server" Text="Save Ordersheet" class="btn btn-success" OnClick="btnFinalSubmit_Click" />
                                                                                <asp:Button ID="btnSubmit" Visible="false" runat="server" Text="Save" class="btn btn-success" OnClick="btnSubmit_Click" ClientIDMode="static" OnClientClick="return ValidateRecord()" />
                                                                            </div>

                                                                        </div>
                                                                    </asp:Panel>
                                                                    <asp:Panel ID="pnlEsignDSC" Visible="false" runat="server">
                                                                        <div class="certi_info bg-light m16">eSign / DSC</div>
                                                                        <br />
                                                                        <div class="form-group row">
                                                                            <div class="col-sm-3">
                                                                               

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
                                                                <div class="tab-pane fade" id="custom-tabs-one-RegisteredForm"
                                                                    aria-labelledby="custom-tabs-one-profile-tab">
                                                                    <!-- table -->


                                                                   

                                                                    <textarea id="summernote" runat="server" clientidmode="Static" style="text-align: justify;"></textarea>
                                                                    <div class="newdiv">
                                                                        <%--<asp:Button ID="btnSave" class="btn btn-success" runat="server" Text="Save" OnClick="btnSave_Click" />--%>
                                                                        <a onclick="AddOrdersheet()" href="#custom-tabs-one-ProposalForm" class="btn btn-success">Save</a>
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
                                                                                                            <asp:TemplateField HeaderText="View" HeaderStyle-Font-Bold="false">
                                                                                                                <ItemTemplate>
                                                                                                                    <a href="#" class="gridViewToolTip" id="btnModalPopup" onclick='showPdfBYHendler("<%# Eval("FILE_PATH")%>","<%# Eval("docType")%>","<%# Eval("EREG_ID")%>")'>
                                                                                                                        <button type="button" class="btn btn-info">
                                                                                                                            <i class="fas fa-eye"></i>
                                                                                                                        </button>
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

                                                                                       

                                                                                        <iframe id="docPath" runat="server" visible="true" height='750'></iframe>
                                                                                        <iframe id="IfProceeding" clientidmode="Static" runat="server" visible="false" scrolling="auto" height='750'></iframe>
                                                                                        <iframe id="IfProceedingCrnt" clientidmode="Static" runat="server" visible="false" scrolling="auto" height='750'></iframe>
                                                                                        <iframe id="IfProceedingOther" clientidmode="Static" runat="server" visible="false" scrolling="auto" height='750'></iframe>

                                                                                        <%--                        </div>

                                                                                    </div>
                                                                                </div>--%>
                                                                                    </div>

                                                                                    <div class="tab-pane fade" id="custom-tabs-four-messages" role="tabpanel" aria-labelledby="custom-tabs-four-messages-tab">

                                                                                      
                                                                                        <iframe id="RecentdocPath" clientidmode="Static" runat="server" width='550' height='750'></iframe>
                                                                                        <iframe id="ifProposal1" clientidmode="Static" runat="server" scrolling="auto" height='750'></iframe>
                                                                                        <iframe id="RecentAttachedDoc" runat="server" width='550' height='750'></iframe>
                                                                                        <iframe id="ifPDFViewerAll" clientidmode="Static" runat="server" scrolling="auto" height='750'></iframe>

                                                                                        <%--                                         </div>

                                                                                    </div>
                                                                                </div>--%>
                                                                                    </div>
                                                                                    <asp:HiddenField ID="hdnbase64" ClientIDMode="Static" runat="server" />
                                                                                    <div class="tab-pane fade " id="custom-tabs-four-settings" role="tabpanel" aria-labelledby="custom-tabs-four-settings-tab">

                                                                                     

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


                    <%--<div id="popupdiv" title="Basic modal dialog" style="display: none">
            </div>--%>
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
                <asp:HiddenField ID="hdnDesignation" runat="server" />
                <asp:HiddenField ID="hdnDistNameEN" runat="server" />
                <asp:HiddenField ID="hdnSRODistNameHi" runat="server" />
                <asp:HiddenField ID="hdnCOSDistNameHi" runat="server" />
                <asp:HiddenField ID="hdnSRONameHi" runat="server" />
                <asp:HiddenField ID="hdnCOSNameHi" runat="server" />
                <asp:HiddenField ID="hdTocan" ClientIDMode="Static" runat="server" />
            </section>
        </ContentTemplate>
        <Triggers>

            <asp:PostBackTrigger ControlID="btnEsignDSC" />
            <asp:PostBackTrigger ControlID="btnSubmit" />
            <asp:PostBackTrigger ControlID="btnDraftSave" />
            <%--<asp:PostBackTrigger ControlID="Calendar1" />--%>
        </Triggers>
    </asp:UpdatePanel>

    <script type="text/javascript">



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

        function ShowDocList() {

            $("#List_pnl").show();
            $("#View_pnl").hide();
        }
        function openSrDoc(FILE_PATH) {

            //alert(FILE_PATH);

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



        function generateCalenderByCode() {
           
            document.getElementById('ContentPlaceHolder1_divCalender').style.display = "block";
            //document.getElementById('calendar-container').style.display = "block";

        }



        function openPopup(PROPOSALPATH_FIRSTFORMATE) {
          

            $("#pnl2").hide();
            $("#pnl3").show();
            $('#ifrDisplay').attr('src', PROPOSALPATH_FIRSTFORMATE);


          


        }

        function ShowDownloadOption() {
            //alert("Hello");
            $('#myModal').modal('show');
        }

        function AddOrdersheet() {

            //debugger;
            //alert(document.getElementById("summernote").value);
            document.getElementById("pContent").innerHTML = document.getElementById("summernote").value;
            //document.getElementById("summernote").value = "";

            $("#custom-tabs-one-ProposalForm").addClass("active");
            //$("#custom-tabs-one-RegisteredForm").removeClass("active");
            //$("#custom_tabs_one_profile_tab").removeClass("active");
            $("#custom-tabs-one-home-tab").addClass("active");
            $('#ContentPlaceHolder1_custom_tabs_one_profile_tab').removeClass("active");
            $('#custom-tabs-one-RegisteredForm').removeClass("active show");


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
    <script type="text/javascript">
      

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
        function Loader() {
            AmagiLoader.show();
        }
        function Loaderstop() {
            AmagiLoader.hide();
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

            BindTable();
            LoadPdfFile();
            ShowMessage();
            // BindOrderSheet();
        });

        function pageLoad(sender, args) {
            if (args.get_isPartialLoad()) {
                $('#summernote').summernote();
                //AddNotice();
            }
        }
    </script>
    <div id="modal_dialog" style="display: none"></div>

</asp:Content>
