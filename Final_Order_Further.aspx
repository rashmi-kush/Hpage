<%@ Page Title="" Language="C#" MasterPageFile="~/CoS/CoSMaster.Master" AutoEventWireup="true" CodeBehind="Final_Order_Further.aspx.cs" ValidateRequest="false" Inherits="CMS_Sampada.CoS.Final_Order_Further" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <link href="../Styles/sweetalert.css" rel="stylesheet" />
    <script src="../Scripts/sweetalert.min.js"></script>
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
    </style>

    <script type="text/javascript">
        function ShowDocList() {

            $("#List_pnl").show();
            $("#View_pnl").hide();
        }
        function openSrDoc(FILE_PATH) {

            //alert(FILE_PATH);
            debugger;
            $("#List_pnl").hide();
            $("#View_pnl").show();
            $('#ifrDisplay').attr('src', FILE_PATH);

        }
        function openProposalDoc(PROPOSALPATH_FIRSTFORMATE) {

            //alert(PROPOSALPATH_FIRSTFORMATE);
            debugger;
            $("#List_pnl").hide();
            $("#View_pnl").show();
            $('#ifrDisplay').attr('src', PROPOSALPATH_FIRSTFORMATE);


        }
        function openOrdersheetDoc(DocPath) {
            //alert(DocPath);
            debugger;
            $("#List_pnl").hide();
            $("#View_pnl").show();
            $('#ifrDisplay').attr('src', DocPath);
        }

        function openNoticeDoc(NOTICE_DOCS) {
            //alert(NOTICE_DOCS);
            debugger;
            $("#List_pnl").hide();
            $("#View_pnl").show();
            $('#ifrDisplay').attr('src', NOTICE_DOCS);
        }

    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="SCMNG" runat="server"></asp:ScriptManager>

    <asp:HiddenField ID="hdnfCseNunmber" ClientIDMode="Static" runat="server" />
    <asp:HiddenField ID="hdnfApp_Number" ClientIDMode="Static" runat="server" />
    <asp:HiddenField ID="hdlblHearingDt" ClientIDMode="Static" runat="server" />
     <asp:HiddenField  ID="hdnUserID" runat="server" Value=0 />


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
                        <asp:Label runat="server" Text='<%# (DateTime.Parse(Eval("hearingdate").ToString()).ToString("MM/dd/yyyy")) %>' ID="lblHearingdateHeading"></asp:Label>
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
                                                            <a class="nav-link active" id="Proceeding" data-toggle="pill"
                                                                href="#custom-tabs-one-Proceeding" role="tab" onclick="openPopup()"
                                                                aria-controls="custom-tabs-one-profile" aria-selected="true">Proceeding  </a>
                                                        </li>
                                                        

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

                                                                            <asp:Repeater ID="RepDetails" runat="server">
                                                                                <ItemTemplate>
                                                                                    <table>
                                                                                        <tr style="text-align: left">
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
                                                                                                <asp:Label ID="lblComment" runat="server" Text='<%#Eval("proceeding") %>' />

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

                                                                                            <td style="text-align: right; width: 65%;">
                                                                                                <div>
                                                                                                    Akash Sharma
                                                                                        <br />
                                                                                                    Collector of Stamp<%--<asp:Label ID="lblDate" runat="server" Font-Bold="true" Text='<%#Eval("PostedDate") %>' /></td>--%></div>
                                                                                            </td>

                                                                                        </tr>

                                                                                    </table>


                                                                                </ItemTemplate>
                                                                                <FooterTemplate>
                                                                                    </table>
                                                                                </FooterTemplate>






                                                                            </asp:Repeater>

                                                                            <br />
                                                                            <hr />
                                                                            <br />

                                                                            <asp:Panel ID="PnlNotice" Visible="false" runat="server">

                                                                                <asp:Repeater ID="Repeater_Notice" runat="server">
                                                                                    <ItemTemplate>
                                                                                        <table>
                                                                                            <tr style="text-align: left">
                                                                                                <td>Proceeding # <%# Container.ItemIndex + 1 %></td>
                                                                                            </tr>
                                                                                            <tr style="">
                                                                                                <td>
                                                                                                    <asp:Label ID="lblhearing" runat="server" Text='<%#Eval("HEARINGDATE") %>' Font-Bold="true" />
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
                                                                                            <asp:Label ID="lblDate" runat="server" Font-Bold="true" Text='<%#Eval("inserteddate") %>' /></td>
                                                                                                <td></td>

                                                                                                <td style="text-align: right; width: 65%;">
                                                                                                    <div>
                                                                                                        Akash Sharma
                                                                                        <br />
                                                                                                        Collector of Stamp<%--<asp:Label ID="lblDate" runat="server" Font-Bold="true" Text='<%#Eval("PostedDate") %>' /></td>--%></div>
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
                                                                   <%-- <asp:DropDownList ID="DropDownList" runat="server" OnChange="SetTemplate(this.value)" CssClass="form-control">
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
                                                                <div id="editproceeding">
                                                                    <textarea id="summernote" runat="server" clientidmode="Static"></textarea>
                                                                    <div class="col-sm-4" style="text-align: left;">
                                                                        <a onclick="AddNotice()" data-toggle="pill"
                                                                            href="#custom-tabs-one-Order-Sheet" aria-controls="custom-tabs-one-home" aria-selected="true" class="btn btn-success" role="tabpanel"
                                                                            aria-labelledby="Proceeding">Save</a>
                                                                        <%--<asp:Button ID="btnSubmit" runat="server" Text="Save" class="btn btn-success" OnClick="" ClientIDMode="static" OnClientClick="return ValidateRecord()" />--%>
                                                                    </div>
                                                                </div>




                                                            </div>

                                                            <div class="tab-pane fade" id="custom-tabs-one-Order-Sheet" role="tabpanel"
                                                                aria-labelledby="Order_Sheet">

                                                                <asp:Panel runat="server" ID="HearingPnl">

                                                                    <div class="main-box htmldoc" id="OrdersheetDiv" style="width: 100%; margin: 0 auto; text-align: center; border: 1px solid #ccc; padding: 30px 30px 30px 30px;">

                                                                        <h2 style="font-size: 18px; margin: 0; font-weight: 600;">न्यायालय कलेक्टर ऑफ़ स्टाम्प्स, भोपाल (म.प्र.)</h2>
                                                                        <h3 style="margin: 0; margin: 10px; font-size: 16px;">प्ररूप-अ</h3>
                                                                        <h2 style="font-size: 16px; margin: 0; margin-bottom: 10px;">(परिपत्र दो-1 की कंडिका 1)</h2>
                                                                        <h3 style="margin: 0; margin: 10px; font-size: 16px;">राजस्व आदेशपत्र</h3>
                                                                        <h2 style="font-size: 16px; margin: 0; margin-bottom: 10px;">कलेक्टर ऑफ़ स्टाम्प, भोपाल के न्यायालय में मामला क्रमांक-  
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
                                                                                        <p id="pContent">
                                                                                        </p>

                                                                                        <p id="pContent1">
                                                                                        </p>


                                                                                        <asp:UpdatePanel runat="server">
                                                                                            <ContentTemplate>
                                                                                                <asp:Panel runat="server" ID="CommentAddOnSheetPnl" Visible="false">
                                                                                                    <hr style="width: 100%; text-align: left; margin-left: 0; border: 1px solid black">
                                                                                                    <asp:Label ID="lbltext" runat="server" />
                                                                                                </asp:Panel>
                                                                                                <br />
                                                                                            </ContentTemplate>
                                                                                        </asp:UpdatePanel>


                                                                                        <b style="float: left; text-align: center; padding: 2px 0 5px 0;">आदेश के लिए नियत दिनांक
                                                                                        <br />
                                                                                            <asp:Label ID="lblHearingDt" runat="server" ClientIDMode="Static"></asp:Label>
                                                                                            <%--<asp:Label ID="Label1" runat="server" ClientIDMode="Static"></asp:Label>--%>
                                                                                            <label id="txthearingdate"></label>
                                                                                        </b>
                                                                                        <p>
                                                                                        </p>


                                                                                        <b style="float: right; text-align: center; padding: 2px 0 5px 0;">कलेक्टर ऑफ़ स्टाम्प्स,<br />
                                                                                            भोपाल
                                                                                          <br />
                                                                                            <br />
                                                                                        </b>
                                                                                    </div>
                                                                                </td>

                                                                                <td style="border: 1px solid black; border-collapse: collapse; padding: 5px; font-size: 14px;"></td>
                                                                            </tr>
                                                                        </table>


                                                                        <br />
                                                                        <br />


                                                                    </div>

                                                                     <asp:Button Text="Save And Generate Final Order" ID="btnSaveProceeding" CssClass="btn btn-success btn-sm" OnClick="btnSaveProceeding_Click"  runat="server" />

                                                                </asp:Panel>
                                                                <br />



                                                               

                                                                <div id="S_Notice">
                                                                    <div id="S_Notice2" style="display: none">

                                                                        <div class="certi_info bg-light">Digital Signature Certificates</div>
                                                                        <br>
                                                                        <div class="form-group row">
                                                                            <div class="col-sm-4">
                                                                                <select type="text" class="form-control" id="input7">
                                                                                    <option>Select</option>
                                                                                    <option>Aadhar eSign </option>
                                                                                    <option>DSC</option>
                                                                                </select>
                                                                            </div>
                                                                            <asp:Button Text="Esign and Submit" ID="BtnSendNotice" CssClass="btn btn-success btn-sm"  runat="server" />
                                                                        </div>
                                                                    </div>
                                                                </div>

                                                                <div id="F_Notice">
                                                                    <br>
                                                                   
                                                                    <div id="for_today">
                                                                        <br>
                                                                        <div class="certi_info bg-light">Digital Signature Certificates </div>
                                                                        <%-- <button type="button" class="btn btn-primary" id="dsc_save">DSC & Save</button>--%>
                                                                        <br>
                                                                        <br>
                                                                        <asp:Button Text="Esign for Final Order" ID="BtnFinalOrder" CssClass="btn btn-success btn-sm" OnClick="BtnFinalOrder_Click" runat="server" />
                                                                        <%-- <button type="button" class="btn btn-info" id="Final_Order_Process">Process for Final Order</button>--%>
                                                                    </div>

                                                                    <div id="s_for_later">
                                                                        <br>
                                                                        <div class="certi_info bg-light">Digital Signature Certificates </div>
                                                                        <button type="button" class="btn btn-primary" id="save_dsc">DSC & Save</button>
                                                                        <br>
                                                                        <br>
                                                                        <button type="button" class="btn btn-info" id="Save_Order_Sheet">Save Order Sheet</button>

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
                                                                    aria-selected="true">Document    </a>                    
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
                                                                                <a class="nav-link" id="custom-tabs-four-settings-tab" data-toggle="pill" href="#custom-tabs-four-settings" role="tab" aria-controls="custom-tabs-four-settings" aria-selected="true">Previous Document</a>
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
                                                                                                        
                                                                                                            <itemtemplate>
                                                                                                                <a href="#" class="gridViewToolTip" id="btnModalPopup" onclick='showPdfBYHendler("<%# Eval("FILE_PATH")%>","<%# Eval("docType")%>","<%# Eval("EREG_ID")%>")'>
                                                                                                                    <button type="button" class="btn btn-info">
                                                                                                                        <i class="fas fa-eye"></i>
                                                                                                                    </button>
                                                                                                                </a><%-- 
                                                                                                                        <a href="#" class="gridViewToolTip" id="btnModalPopup" onclick='openPopup("<%# Eval("FILE_PATH")%>")'> <button type="button" class="btn btn-info">
                                                    <i class="fas fa-eye"></i>
                                                  </button>
                                                  </a>--%>
                                                                                                            </itemtemplate>
                                                                                                            <headerstyle width="10%" horizontalalign="Center" />
                                                                                                            <itemstyle width="10%" horizontalalign="Center" />
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


                                                                                <button type="button" class="btn btn-success float-right" data-toggle="modal" data-target="#exampleModalCenter">Attach </button>

                                                                            </div>
                                                                            <div class="tab-pane fade active show" id="custom-tabs-four-profile" role="tabpanel" aria-labelledby="custom-tabs-four-profile-tab">

                                                                                <iframe id="RecentdocPath" runat="server" height='750'></iframe>

                                                                            </div>
                                                                            <div class="tab-pane fade" id="custom-tabs-four-messages" role="tabpanel" aria-labelledby="custom-tabs-four-messages-tab">

                                                                              <iframe id="ifReg" clientidmode="Static" runat="server" width='550' height='750'></iframe>
                                                                                        <iframe id="ifProposal1" clientidmode="Static" runat="server" scrolling="auto" height='750'></iframe>
                                                                                        <iframe id="RecentAttachedDoc" runat="server" width='550' height='750'></iframe>
                                                                                        <iframe id="ifPDFViewerAll" clientidmode="Static" runat="server" scrolling="auto" height='750'></iframe>
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
                                    <th>SNo.</th>
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

    <script type="text/javascript">

        function AddOrderSheet() {
            var HearingDate = $("#ContentPlaceHolder1_txtHearingDate").val();
            //alert(HearingDate);
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


        function AddNotice() {
            var HearingDate = $("#txtHearingDate").val();
            /*alert(HearingDate);*/

            //document.getElementById('txthearingdate').innerText = HearingDate;
            //

            if (HearingDate == "") {
                alert("Please Select  Hearing Date");
                return false;
            }
            else {

                document.getElementById("pContent").innerHTML = document.getElementById("summernote").value;
                document.getElementById("pContent1").innerHTML = document.getElementById("message").value;


                $("#custom-tabs-one-Order-Sheet").addClass("active");

                $("#custom-tabs-one-Proceeding").removeClass("active");
                $("#Proceeding").removeClass("active");


                $("#custom-tabs-one-Next_Hearing_Date").removeClass("active");
                $("#Next_Hearing_Date").removeClass("active");

                $("#Order_Sheet").addClass("active");
            }




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
            document.getElementById("lblHearingDt").value = (dat + '/' + Mon + '/' + year);
            //document.getElementById("divDate").style.visibility = "visible";

        }

        function ShowMessage() {
            swal({
                title: 'Please select Hearing Date',
                type: 'warning'
            });
            return false;
            document.getElementById('txtHearingDate').focus();
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
            var chkAll = $('.header').click(function () {
                //Check header and item's checboxes on click of header checkbox
                chkItem.prop('checked', $(this).is(':checked'));
            });
            var chkItem = $(".item").click(function () {
                //If any of the item's checkbox is unchecked then also uncheck header's checkbox
                chkAll.prop('checked', chkItem.filter(':not(:checked)').length == 0);
            });


            function startCountdown() {
                alert("qqq");
            }


        });


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





        //var seconds = 30;
        //function startCountdown() {
        //    debugger;
        //    alert("test");
        //    var countdownDisplay = document.getElementById('countdown');
        //    countdownDisplay.style.display = 'block';

        //    var interval = setInterval(function () {
        //        countdownDisplay.innerHTML = 'Resend password in ' + seconds + ' seconds';
        //        seconds--;

        //        if (seconds < 0) {
        //            clearInterval(interval);
        //            countdownDisplay.style.display = 'none';
        //        }
        //    }, 1000);
        //}






    </script>

    <script type="text/javascript">

        $("#btnAddParty111").click(function () {
            alert("sdad");
        });


        $(".otpvalidate").click(function () {
            alert("12222");
            //HideLabel();
        });


        function HideLabel() {
            var seconds = 5;
         <%--   setTimeout(function () {
                document.getElementById("<%=lblMessage.ClientID %>").style.display = "none";
            }, seconds * 1000);--%>


            setTimeout(function () {
                alert('hi')
            }, 2000);
        };
    </script>

    <script type="text/javascript">
        function showAlert() {
            $('#hearing').hide();
            $('#OrdersheetDiv').show();
            //$('#DateDiv').hide();
            $('#newhearing').show();
            //document.getElementById("custom-tabs-one-tabContent2").style.display = "block";
            //document.getElementById('custom-tabs-one-tabContent2').style.cssText = "display: block;";
        }
        function TempShow() {
            $('#ifselectyes_Template').show();
        }

    </script>


    <script type="text/javascript">
        function SetTemplate(val) {
            debugger;
            //alert(val);
            //var TemplateType = '{"TemId":"' + val+'"}';
            $.ajax({
                type: "POST",

                url: '<%=Page.ResolveClientUrl("~/CoS/Final_Order_Further.aspx/GetTemplate_Notice") %>',
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


        function ShowOption() {
            //alert("Hello");
            $('#myModal').modal('show');
        }

        function ShowDownloadOption() {
            //alert("Hello");
            $('#myModal_Downlaod').modal('show');
        }

        function printPDF() {

            //debugger;
            //document.getElementById('btnDownload').style.visibility = 'hidden';
            //document.getElementById('btnPrintSeletedPDF').style.visibility = 'visible';
            ShowOption();
        }
        function PrintSeletedPDF() {

            if (document.getElementById('chkRecentDoc').checked) {
                alert("chkRecentDoc");
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




    </script>
     <asp:HiddenField ID="hdnCOSOfficeNameHi1" runat="server" />
        <asp:HiddenField ID="hdnToDate" runat="server" />
        <asp:HiddenField runat="server" ClientIDMode="Static" ID="hdTocan" />
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
                $('#ifReg').attr('src', url);
            }

            BindTable();
            LoadPdfFile();
            ShowMessage();
            // BindOrderSheet();
        });

    </script>
    <div id="modal_dialog" style="display: none"></div>
</asp:Content>
