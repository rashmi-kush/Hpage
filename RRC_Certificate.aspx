<%@ Page Title="" Language="C#" MasterPageFile="~/CoS/CoSMaster.Master"  validateRequest="false"  AutoEventWireup="true" CodeBehind="RRC_Certification.aspx.cs" Inherits="CMS_Sampada.CoS.RRC_Certificate" %>


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
    </style>



    <script type="text/javascript"> 
        //function checkDate(sender, args) {

        //    if (sender._selectedDate < new Date()) {
        //        //alert("You cannot select a day earlier than today!");

        //        swal({
        //            title: 'Date can not be less then todays date',
        //            type: 'error'
        //        });
        //        //document.getElementById('txtHearingDate').focus();
        //        return false;
        //        sender._selectedDate = new Date();
        //        // set the date back to the current date 
        //        sender._textbox.set_Value(sender._selectedDate.format(sender._format))
        //    }


        //}

        //function ValidateDate(e) {
        //    //alert("sdfewfwf");
        //    if (document.getElementById('ContentPlaceHolder1_txtHearingDate').value == "") {
        //        //alert("Please select Hearing Date.");
        //        document.getElementById('rdReportYes').checked = false;
        //        ShowMessageNotVerified();
        //        return false;

        //    }
        //    else {
        //        //alert("true");
        //        document.getElementById('rdReportYes').checked = true;
        //        __doPostBack('rdReportYes', e);
        //        //document.getElementById('rdReportYes').click();
        //        //return true;
        //    }
        //}

        function OrderSheetSave() {

            //alert("fghj");
            Swal.fire({
                icon: 'success',
                title: 'RRC Certificate Draft Saved successfully !',
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

            //var HearingDate = $("#ContentPlaceHolder1_txtHearingDate").val();
            //var lblHearingDate = $("#lblHearingDt").val();
            //if (HearingDate == "" && lblHearingDate == "") {
            //    //alert("Please select Hearing date");
            //    Swal.fire({
            //        icon: 'info',
            //        title: 'Please select hearing date'

            //    })
            //    return false;
            //}

            //else if (!confirm("Do you want to proceed ordersheet without seek report ? ")) {

            //    return false;
            //}

        }

        function ShowMessage() {


            Swal.fire({
                icon: 'success',
                title: 'RRC Certificate create successfully',
                showCancelButton: false,
                confirmButtonText: 'OK',
                timer: 10000

            });
            return false;
        }

        function ShowMessageDSC(loc) {

            //alert("hello");
            Swal.fire({
                icon: 'success',
                title: 'Signed RRC Certificate saved Successfully',
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
                    <h5>RRC Certificate </h5>
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
                                        <li class="nav-item"><a class="nav-link active disabled" href="#CreateOrderSheet" data-toggle="tab">RRC Certificate </a></li>
                                        <li class="nav-item"><a class="nav-link disabled" href="#Notice" data-toggle="tab">Notice</a></li>
                                        <li class="nav-item"><a class="nav-link disabled" href="ReportSeeking">Seek Report</a></li>
                                        <%--<li class="nav-item"><a class="nav-link disabled" href="#EarlyHearing" data-toggle="tab">Early
                        Hearing</a>
                                        </li>
                                        <li class="nav-item"><a class="nav-link disabled" href="#SendToAppeal" data-toggle="tab">Send to
                        Appeal</a>
                                        </li>
                                        <li class="nav-item"><a class="nav-link" href="AcceptRejectCases_details">Send Back</a>
                                        </li>
                                        <li class="nav-item"><a class="nav-link disabled" href="#Reply" data-toggle="tab">Reply</a></li>
                                        <li class="nav-item"><a class="nav-link disabled" href="#Attachement"
                                            data-toggle="tab">Attachement</a></li>--%>
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
                                                                        aria-selected="false">RRC Certificate </a>
                                                                </li>
                                                                <li class="nav-item">
                                                                    <a class="nav-link" id="custom_tabs_one_profile_tab" data-toggle="pill" 
                                                                        href="#custom-tabs-one-RegisteredForm" role="tab" onclick="calljqueryfile()"
                                                                        aria-controls="custom-tabs-one-profile" aria-selected="true" runat="server">Edit RRC Certificate </a>

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
                                                                    <div class="">
                                                                        <!-- Doc detail html -->
                                                                        <div class="main-box htmldoc" style="/* height: 766px; */ overflow: auto; width: 100%; margin: 0 auto; text-align: center; padding: 15px 30px 0px 30px; margin: 5px 0 0px 0px;">
                                                                            <h2 style="font-size: 18px; margin: 0; font-weight: 600; padding: 6px 0px;">प्रारूप-19</h2>
                                                                            <h2 style="font-size: 18px; margin: 0; font-weight: 600;">(नियम 14 देखिए ) </h2>
                                                                            <h3 style="font-size: 16px; font-family: fantasy; padding: 6px 0 0 0">मध्यप्रदेश भू –राजस्व संहिता ( भू –राजस्व की उगाही ) नियम, 2020 </h3>

                                                                            <h3 style="font-size: 16px; font-family: fantasy; line-height: 27px;">राशियों की भू –राजस्व के बकाया के तौर पर वसूली के लिए आवेदन पत्र
                                 <br />
                                                                                [मध्यप्रदेश भू –राजस्व संहिता, 1959 की धारा 155 के अधीन ]
                                <br />
                                                                                प्रकरण क्रमांक नंबर :  
                                                                                <asp:Label ID="lblCaseNo" runat="server"></asp:Label>

                                                                            </h3>




                                                                            <div class="m-top">

                                                                                <div style="float: left; text-align: justify;">
                                                                                    <p>
                                                                                        प्रति , 
                                        <br />
                                                                                        अतिरिक्त तहसीलदार / जिला पंजीयक 
                                        <br />
                                                                                        <asp:Label ID="lblDEMOGRAPHY_NAME" runat="server"></asp:Label>
                                                                                    </p>
                                                                                </div>
                                                                                <br />
                                                                                <div style="float: left; text-align: justify;">
                                                                                    <p>यह विनिश्चित किया गया है कि निम्नलिखित राशियों को , जिसका विवरण नीचे दिया गया है , भू –राजस्व के बकाया तौर पर वसूल किया जाना चाहिए । कृपया इन्हें भू –राजस्व के बकाया के तौर पर वसूल करैं तथा नीचे मद क्रमांक 7 में वर्णित लेखा शीर्ष में जमा करें :- </p>
                                                                                </div>
                                                                                <div style="float: left; text-align: justify; margin-bottom: 10px;">
                                                                                    1.	विभाग /समक्ष प्राधिकारी का नाम जिसे कि राशि शोध्य है - पंजीयन विभाग 
                                    <br />
                                                                                    2.	उस व्यक्ति का नाम, पिता का नाम तथा पूरा पता जिससे कि राशियां शोध्य है । 
                                                                                </div>
                                                                                <br />
                                                                            </div>

                                                                            <br />
                                                                            <br />

                                                                            <asp:GridView ID="grdlPartys" CssClass="table table-condensed table-hover dastavej"
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
                                                                                    <asp:TemplateField HeaderText="पार्टी का नाम">
                                                                                        <ItemTemplate>
                                                                                            <%#Eval("Party_Name") %>
                                                                                        </ItemTemplate>
                                                                                        <HeaderStyle Width="10%" HorizontalAlign="Center" />
                                                                                        <ItemStyle Width="10%" HorizontalAlign="Center" />
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="पिता का नाम ">
                                                                                        <ItemTemplate>
                                                                                            <%#Eval("PARTYFATHER_ORHUSBAND_ORGUARDIANNAME") %>
                                                                                        </ItemTemplate>
                                                                                        <HeaderStyle Width="10%" HorizontalAlign="Center" />
                                                                                        <ItemStyle Width="10%" HorizontalAlign="Center" />
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="पता ">
                                                                                        <ItemTemplate>
                                                                                            <%#Eval("Party_Address") %>
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

                                                                            <div style="float: left; text-align: justify;">
                                                                                3.	शोध्य राशियों का विवरण 
                                                                            </div>
                                                                            <br />

                                                                            <asp:GridView ID="grdAmount" CssClass="table table-condensed table-hover dastavej"
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
                                                                                    <asp:TemplateField HeaderText="कमी मुद्रांक शुल्क">
                                                                                        <ItemTemplate>
                                                                                            <%#Eval("TOTAL_DEFICIT_AMOUNT") %>
                                                                                        </ItemTemplate>
                                                                                        <HeaderStyle Width="10%" HorizontalAlign="Center" />
                                                                                        <ItemStyle Width="10%" HorizontalAlign="Center" />
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="अर्थ दंड">
                                                                                        <ItemTemplate>
                                                                                            <%#Eval("TOTAL_PENALTY_AMOUNT") %>
                                                                                        </ItemTemplate>
                                                                                        <HeaderStyle Width="10%" HorizontalAlign="Center" />
                                                                                        <ItemStyle Width="10%" HorizontalAlign="Center" />
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="कुल राशि">
                                                                                        <ItemTemplate>
                                                                                            <%#Eval("FINAL_PAYABLE_AMOUNT") %>
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

                                                                            <div style="float: left; text-align: justify; margin-bottom: 12px;">
                                                                                4.	विधि का वह उपबंध जिसके अधीन राशियां भू -राजस्व के बकाया के तौर पर वसूली योग्य हैं । 
                                 <br />
                                                                                [भारती स्टाम्प अधिनियम 1899 की धारा 48 शुल्कों ओर सस्ती की वसूली के प्रावधान के अनुसार ]
                                 <br />
                                                                                <br />
                                                                                5.	वह आदेशिका जिसके द्वारा राशि वसूल की जा सकेगी । 
                                 <br />
                                                                                [मध्यप्रदेश भू –राजस्व संहिता, 1959 एवं मध्यप्रदेश भू –राजस्व संहिता ( भू –राजस्व की उगाही ) नियम, 2020 के प्रावधान के अनुसार ]
                                  <br />
                                                                                <br />
                                                                                6.	उस संपत्ति का विवरण जिसके विरुद्ध आदेशिका निष्पादित की का सकेगी । 
                                      <br />
                                                                                <br />
                                                                            </div>

                                                                            <asp:GridView ID="grdPropertyDetails" CssClass="table table-condensed table-hover dastavej"
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

                                                                                    <asp:TemplateField HeaderText="संपत्ति आई डी">
                                                                                        <ItemTemplate>
                                                                                            <%#Eval("PROPERTY_GIVEN_ID") %>
                                                                                        </ItemTemplate>
                                                                                        <HeaderStyle Width="10%" HorizontalAlign="Center" />
                                                                                        <ItemStyle Width="10%" HorizontalAlign="Center" />
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="डॉक्यूमेंट रजिस्ट्रेशन नंबर/इनिशीऐशन आई डी">
                                                                                        <ItemTemplate>
                                                                                            <%#Eval("INITIATION_ID") %>
                                                                                        </ItemTemplate>
                                                                                        <HeaderStyle Width="10%" HorizontalAlign="Center" />
                                                                                        <ItemStyle Width="10%" HorizontalAlign="Center" />
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="संपत्ति का प्रकार">
                                                                                        <ItemTemplate>
                                                                                            <%#Eval("PROPERTY_TYPE") %>
                                                                                        </ItemTemplate>
                                                                                        <HeaderStyle Width="10%" HorizontalAlign="Center" />
                                                                                        <ItemStyle Width="10%" HorizontalAlign="Center" />
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="संपत्ति का क्षेत्र ">
                                                                                        <ItemTemplate>
                                                                                            <%#Eval("PROPERTY_AREA") %>
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

                                                                            <br />

                                                                            <div style="float: left; text-align: justify;">
                                                                                7.	वह लेखा शीर्ष जिसमें वसूली के पश्चात धनराशि जमा की जाएगी । 
                                 <br />
                                                                                A76
                                  <br />
                                                                                8.	क्या धार 155 के खण्ड  (घ), (ई), (एक), (जी) अथवा (ज) के अधीन यथास्थिति अपेक्षित प्रमाण -पत्र कुर्की किए गई हैं ?
                                 <br />
                                                                                [हा]

                                 <br />
                                                                                 
                                                                                <p id="pContent" style="text-align: justify;" runat="server" clientidmode="Static"  > 
                                                                                </p>
                                                                            </div>

                                                                            <div style="padding-top: 130px; text-align: justify;">
                                                                                <div style="float: left; text-align: justify;">
                                                                                    <p>
                                                                                        मुद्रा 
                                      <br />
                                                                                        दिनांक
                                                                                        <br />
                                                                                        <asp:Label ID="lblTodaydate" runat="server"></asp:Label>
                                                                                    </p>
                                                                                </div>

                                                                                <div style="float: right;">
                                                                                    <p>जिला पंजीयक
                                                                                          <br />
                                                                                        <asp:Label ID="lblDistrictNAME_EN" runat="server"></asp:Label></p>
                                                                                    <br />
                                                                                        <asp:Label ID="lblDistrict" runat="server"></asp:Label></p>
                                                                                </div>
                                                                            </div>

                                                                        </div>

                                                                    </div>
                                                                    <br>

                                                                    <asp:Panel ID="pnlDraft" Visible="True" runat="server">

                                                                        <asp:Button ID="btnDraftSave" runat="server" Text="Save Draft" class="btn btn-success" OnClick="btnDraftSave_Click" ClientIDMode="static"  />

                                                                    </asp:Panel>

                                                                    <asp:Panel ID="pnlSeekReport" Visible="false" runat="server">

                                                                    </asp:Panel>

                                                                    <asp:Panel ID="pnlBtnSave" runat="server" Visible="false">
                                                                        <div class="form-group row">
                                                                            <div class="col-sm-4 ml-lg-auto mt-2" style="text-align: end;">
                                                                               
                                                                                <asp:Button ID="btnFinalSubmit" runat="server" Text="Submit" class="btn btn-success" OnClick="btnFinalSubmit_Click" />
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
                                                                <div class="tab-pane fade" id="custom-tabs-one-RegisteredForm"
                                                                    aria-labelledby="custom-tabs-one-profile-tab">
                                

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
                                                                                        <a class="nav-link" id="custom-tabs-four-settings-tab"  data-toggle="pill" href="#custom-tabs-four-settings" role="tab" aria-controls="custom-tabs-four-settings" aria-selected="true">Previous Proceeding</a>
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
                                                                                                                    <a href="#" class="gridViewToolTip" id="btnModalPopup" onclick='showPdfBYHendler("<%# Eval("FILE_PATH")%>","<%# Eval("docType")%>")'>

                                                                                                                        <button type="button" class="btn btn-info"><i class="fas fa-eye"></i></button>
                                                                                                                    </a>

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

                                                                                                </ContentTemplate>

                                                                                            </asp:UpdatePanel>


                                                                                        </div>
                                                                                        <div id="View_pnl" style="display: none">
                                                                                            <div id="View_PartyReply">
                                                                                                <textarea id="txtReadOnly" runat="server" style="height: 67px; width: 602px;" adonly="readonly" visible="false"></textarea>
                                                                                            </div>


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

                                                                                   

                                                                                        <iframe id="docPath" runat="server" height='750'></iframe>

 
                                                                                    </div>

                                                                                    <div class="tab-pane fade" id="custom-tabs-four-messages" role="tabpanel" aria-labelledby="custom-tabs-four-messages-tab">

                                                                                
                                                                                        <iframe id="RecentdocPath" runat="server" width='550' height='750'></iframe>
                                                                                        <iframe id="ifProposal1" clientidmode="Static" runat="server" scrolling="auto" height='750'></iframe>
                                                                                        <iframe id="RecentAttachedDoc" runat="server" width='550' height='750'></iframe>
                                                                                        <iframe id="ifPDFViewerAll" clientidmode="Static" runat="server" scrolling="auto" height='750'></iframe>

        
                                                                                    </div>
                                                                                    <div class="tab-pane fade " id="custom-tabs-four-settings" role="tabpanel" aria-labelledby="custom-tabs-four-settings-tab">

                                                                                    

                                                                                        <asp:GridView ID="PreviousDoc" runat="server" AutoGenerateColumns="false">
                                                                                            <Columns>

                                                                                                <%--<asp:BoundField DataField="File_Path" />--%>
                                                                                                <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                                                                                    <ItemTemplate>
                                                                                                        <iframe id="ifDisplayPDF" height='750' runat="server" src='<%#Eval("RRC_CERT_ORDERSHEET_PATH") %>'></iframe>
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
                <asp:HiddenField ID="hdnSRODistNameHi" runat="server" />
                <asp:HiddenField ID="hdnCOSDistNameHi" runat="server" />
                <asp:HiddenField ID="hdnSRONameHi" runat="server" />
                <asp:HiddenField ID="hdnCOSNameHi" runat="server" />
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
            }
            else if (val == 2) {
                document.getElementById("TxtLast4Digit").style.display = "None";
            }
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

        function AddOrdersheet() {

            debugger;
            //alert(document.getElementById("summernote").value);

            document.getElementById("pContent").innerHTML = "9." + document.getElementById("summernote").value;

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
        $("[id*=btnModalPopup]").live("click", function () {
            $("#modal_dialog").dialog({
                title: "jQuery Modal Dialog Popup",
                buttons: {
                    Close: function () {
                        $(this).dialog('close');
                    }
                },
                modal: true
            });
            return false;
        });

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
    <div id="modal_dialog" style="display: none"></div>
</asp:Content>

