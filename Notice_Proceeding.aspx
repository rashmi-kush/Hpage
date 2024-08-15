<%@ Page Title="" Language="C#" MasterPageFile="~/CoS/CoSMaster.Master" AutoEventWireup="true" CodeBehind="Notice_Proceeding.aspx.cs" ValidateRequest="false" Inherits="CMS_Sampada.CoS.Notice_Proceeding" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <link href="../Styles/sweetalert.css" rel="stylesheet" />
    <script src="../Scripts/sweetalert.min.js"></script>
    <script src="../assets/js/3.3.1/sweetalert2@11.js"></script>
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript" src="https://unpkg.com/sweetalert/dist/sweetalert.min.js"></script>
    <style>
        .note-editing-area {
            height: 350px;
        }
    </style>
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

        function showAlert() {

            $('#custom-tabs-one-Order-Sheet').show();
            $('#S_Notice').show();



        }
        function ConfirmMsg() {



            Swal.fire({
                icon: 'success',
                title: 'Record Saved Successfully',
                showCancelButton: false,
                confirmButtonText: 'OK',





            });
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
        function SetTemplate(val) {
            /* debugger;*/
            //alert(val);
            //var TemplateType = '{"TemId":"' + val+'"}';
            $.ajax({
                type: "POST",
                <%--url: '<%=Page.ResolveClientUrl("~/RRCDR/Ordersheet.aspx/GetTemplate") %>',--%>
                url: '<%=Page.ResolveClientUrl("~/CoS/Notice_Proceeding.aspx/GetTemplate_Notice") %>',
                data: '{"TemId":' + val + '}',
                /*data: '{"TemId": "' + val + '", "hdnUserID": ' + $("#hdnUserID").val() + '}',*/

                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    //  debugger;
                    // var data = JSON.stringify(msg);
                    //alert(msg.d);
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
            //alert($(".note-editable.card-block p").html(d));
            document.getElementById("summernote").value = d; // set value
            //  alert(document.getElementById("summernote  .note-editing-area p").value);


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

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%--<asp:ScriptManager ID="SCMNG" runat="server"></asp:ScriptManager>--%>
    <asp:ScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ScriptManager>
    <asp:HiddenField ID="hdnfCseNunmber" ClientIDMode="Static" runat="server" />
    <asp:HiddenField ID="hdnfApp_Number" ClientIDMode="Static" runat="server" />
    <asp:HiddenField runat="server" ClientIDMode="Static" ID="hdTocan" />


    <section class="content-header headercls">
        <div class="container-fluid">
            <div class="row">
                <div class="col-sm-3">
                    <h5>Notice Proceeding</h5>
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
                                <li class="nav-item"><a class="nav-link disabled" href="#">Proposal</a></li>
                                <li class="nav-item"><a class="nav-link disabled" href="#">Order Sheet</a></li>
                                <li class="nav-item"><a class="nav-link disabled" href="#">Notice</a></li>
                                <li class="nav-item"><a class="nav-link disabled" href="#">Seek Report</a></li>
                                <li class="nav-item"><a class="nav-link active disabled" href="#">Notice Proceeding</a></li>
                                <li class="nav-item"><a class="nav-link disabled" href="#">Hearing</a></li>
                                <li class="nav-item"><a class="nav-link disabled" href="#">Final Order</a></li>
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
                                                                href="#custom-tabs-one-Order-Sheet" role="tab" onclick="openPopup()"
                                                                aria-controls="custom-tabs-one-profile" aria-selected="true">Proceeding</a>
                                                        </li>


                                                        <li class="nav-item">
                                                            <a class="nav-link" id="Edit" data-toggle="pill" style="display: none;"
                                                                href="#custom-tabs-one-Proceeding" role="tab"
                                                                aria-controls="custom-tabs-one-profile" aria-selected="true" runat="server">Edit Proceeding </a>
                                                        </li>

                                                        <%-- <li class="nav-item">
                                                            <a class="nav-link" id="Order_Sheet" data-toggle="pill"
                                                                href="#custom-tabs-one-Order-Sheet" role="tab" aria-controls="custom-tabs-one-home"
                                                                aria-selected="false">Order Sheet </a>
                                                        </li>--%>

                                                        <%-- <li class="nav-item">
                                                            <a class="nav-link" id="Next_Hearing_Date" data-toggle="pill"
                                                                href="#custom-tabs-one-Next_Hearing_Date" role="tab" aria-controls="custom-tabs-one-home"
                                                                aria-selected="false">Next Hearing Date </a>
                                                        </li>--%>
                                                        <%-- <li class="nav-item">
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

                                                                        <asp:Button ID="btnSkip" class="btn btn-success" Text="Skip Proceeding" runat="server" OnClick="btnSkip_Click" Visible="true" />
                                                                        <asp:Button ID="btnCloseProce" class="btn btn-success" Text="Close" runat="server" OnClick="btnCloseProce_Click" Visible="false" />
                                                                        <div class="main-box html_box htmldoc greenbox height-100" style="background: rgba(202, 249, 130, 0.67);">
                                                                            <h4>न्यायालय कलेक्टर ऑफ़ स्टाम्प्स,
                                                                                <asp:Label ID="lblDRoffice" runat="server"></asp:Label>
                                                                                (म.प्र.)</h4>
                                                                            <br />
                                                                            <br />

                                                                            <%-- <asp:Repeater ID="RepDetails" runat="server">
                                                                                <ItemTemplate>--%>
                                                                            <table>
                                                                                <%-- <h2 style="font-size: 18px; margin: 0; font-weight: 600;">न्यायालय कलेक्टर ऑफ़ स्टाम्प्स, भोपाल (म.प्र.)</h2>
                                                                                        <br />--%>
                                                                                <tr style="text-align: left;">
                                                                                    <td>Proceeding # 1</td>
                                                                                </tr>
                                                                                <tr style="">
                                                                                    <td>
                                                                                        <%--<asp:Label ID="lblhearing" runat="server" Text='<%#Eval("hearingdate") %>' Font-Bold="true" />--%>
                                                                                        <asp:Label ID="lblDate" runat="server" Font-Bold="true" />

                                                                                    </td>
                                                                                </tr>
                                                                            </table>

                                                                            <span style="padding: 30px"></span>
                                                                            <table>
                                                                                <tr>
                                                                                    <td style="text-align: justify">
                                                                                        <asp:Label ID="lblComment" runat="server" />

                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                            <p>&nbsp;&nbsp;&nbsp;</p>
                                                                            <table>
                                                                                <tr style="text-align: left;">
                                                                                    <td>पेशी दिनांक:                                                                                                
                                                                                            <%--<asp:Label ID="lblDate" runat="server" Font-Bold="true" Text='<%#Eval("inserteddate") %>' /></td>--%>

                                                                                        <asp:Label ID="lblhearing" runat="server" Font-Bold="true" />
                                                                                    <td></td>

                                                                                    <td style="text-align: right; width: 72%;">
                                                                                        <div>
                                                                                            कलेक्टर ऑफ़ स्टाम्प्स
                                                                                        <br />
                                                                                            <asp:Label ID="lblDRoffice4" runat="server"></asp:Label>
                                                                                            <%--<asp:Label ID="lblDate" runat="server" Font-Bold="true" Text='<%#Eval("PostedDate") %>' /></td>--%>
                                                                                        </div>
                                                                                    </td>

                                                                                </tr>

                                                                            </table>
                                                                            <br />
                                                                            <hr />
                                                                            <br />
                                                                            <asp:Panel ID="pnlNoticeProceeding" runat="server" Visible="false">
                                                                                <table>
                                                                                    <%-- <h2 style="font-size: 18px; margin: 0; font-weight: 600;">न्यायालय कलेक्टर ऑफ़ स्टाम्प्स, भोपाल (म.प्र.)</h2>
                                                                                        <br />--%>
                                                                                    <tr style="text-align: left;">
                                                                                        <td>Proceeding # 2</td>
                                                                                    </tr>
                                                                                    <tr style="">
                                                                                        <td>
                                                                                            <%--<asp:Label ID="lblhearing" runat="server" Text='<%#Eval("hearingdate") %>' Font-Bold="true" />--%>

                                                                                            <asp:Label ID="lblDate2" runat="server" Font-Bold="true" />
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>

                                                                                <span style="padding: 30px"></span>
                                                                                <table>
                                                                                    <tr>
                                                                                        <td style="text-align: justify">
                                                                                            <asp:Label ID="lblComment2" runat="server" />

                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                                <p>&nbsp;&nbsp;&nbsp;</p>
                                                                                <table>
                                                                                    <tr style="text-align: left;">
                                                                                        <td>पेशी दिनांक:                                                                                                
                                                                                            <%--<asp:Label ID="lblDate" runat="server" Font-Bold="true" Text='<%#Eval("inserteddate") %>' /></td>--%>
                                                                                            <asp:Label ID="lblhearing2" runat="server" Font-Bold="true" />
                                                                                        <td></td>

                                                                                        <td style="text-align: right; width: 72%;">
                                                                                            <div>
                                                                                                कलेक्टर ऑफ़ स्टाम्प्स
                                                                                        <br />
                                                                                                <asp:Label ID="lblDRoffice5" runat="server"></asp:Label>
                                                                                                <%--<asp:Label ID="lblDate" runat="server" Font-Bold="true" Text='<%#Eval("PostedDate") %>' /></td>--%>
                                                                                            </div>
                                                                                        </td>

                                                                                    </tr>

                                                                                </table>
                                                                            </asp:Panel>




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
                                                                                                    <asp:Label ID="lblhearing" runat="server" Text='<%#Eval("HEARINGDATE") %>' Font-Bold="true" />
                                                                                                </td>
                                                                                            </tr>
                                                                                        </table>

                                                                                        <span style="padding: 30px"></span>
                                                                                        <table>
                                                                                            <tr>
                                                                                                <td style="text-align: justify">
                                                                                                    <asp:Label ID="lblComment" runat="server" Text='<%#Eval("ADDITIONAL_PROCEEDING") %>' />
                                                                                                </td>
                                                                                            </tr>
                                                                                        </table>
                                                                                        <p>&nbsp;&nbsp;&nbsp;</p>
                                                                                        <table>
                                                                                            <tr style="text-align: left;">
                                                                                                <td>पेशी दिनांक:                                                                                                
                                                                                            <asp:Label ID="lblDate" runat="server" Font-Bold="true" Text='<%#Eval("LMDT") %>' /></td>
                                                                                                <td></td>

                                                                                                <td style="text-align: right; width: 65%;">
                                                                                                    <div>
                                                                                                        Akash Sharma
                                                                                        <br />
                                                                                                        Collector of Stamp<%--<asp:Label ID="lblDate" runat="server" Font-Bold="true" Text='<%#Eval("PostedDate") %>' /></td>--%>
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
                                                                    <%-- <asp:DropDownList ID="DropDownList" runat="server" OnChange="SetTemplate(this.value)" CssClass="form-control">
                                                                        <asp:ListItem Text="-Select Template-" Value="0"></asp:ListItem>
                                                                        <asp:ListItem Text="Template 1" Value="1"></asp:ListItem>
                                                                        <asp:ListItem Text="Template 2" Value="2"></asp:ListItem>
                                                                        <asp:ListItem Text="Template 3" Value="3"></asp:ListItem>
                                                                        <asp:ListItem Text="Template 4" Value="4"></asp:ListItem>
                                                                        <asp:ListItem Text="Template 5" Value="5"></asp:ListItem>
                                                                    </asp:DropDownList>--%>
                                                                    <asp:DropDownList ID="ddlTemplates" runat="server" OnChange="SetTemplate(this.value)" CssClass="form-control" EnableTheming="false">
                                                                    </asp:DropDownList>
                                                                </div>
                                                                <div id="editproceeding">
                                                                    <textarea id="summernote" runat="server" clientidmode="Static"></textarea>
                                                                    <div class="col-sm-4"></div>

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

                                                                        <h2 style="font-size: 18px; margin: 0; font-weight: 600;">न्यायालय कलेक्टर ऑफ़ स्टाम्प्स,
                                                                            <asp:Label ID="lblDRoffice1" runat="server"></asp:Label>
                                                                            (म.प्र.)</h2>
                                                                        <h3 style="margin: 0; margin: 10px; font-size: 16px;">प्रारूप-अ</h3>
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



                                                                                        <div id="DivTodayDetails">
                                                                                            <asp:UpdatePanel runat="server">
                                                                                                <ContentTemplate>
                                                                                                    <asp:Panel runat="server" ID="pnlTodayDetails">
                                                                                                        <hr style="width: 100%; text-align: left; margin-left: 0; border: 1px solid black">
                                                                                                        <asp:Label ID="lblTodayDetails" runat="server" />
                                                                                                    </asp:Panel>
                                                                                                    <br />
                                                                                                </ContentTemplate>


                                                                                            </asp:UpdatePanel>

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
                                                                                            <asp:Label ID="lblDRoffice3" runat="server"></asp:Label>
                                                                                            <br />
                                                                                            <br />
                                                                                        </b>
                                                                                    </div>
                                                                                </td>

                                                                                <td style="border: 1px solid black; border-collapse: collapse; padding: 5px; font-size: 14px;"></td>
                                                                            </tr>
                                                                        </table>





                                                                    </div>


                                                                    <br />


                                                                    <asp:UpdatePanel ID="upnlDateCalender" runat="server">

                                                                        <ContentTemplate>

                                                                            <div class="col-sm-6">

                                                                                <asp:Button ID="BtnSave" class="btn btn-success" Text="Save" runat="server" OnClick="BtnSave_Click" Visible="true" />

                                                                            </div>
                                                                            <div id="S_Notice">

                                                                                <asp:Panel ID="pnlEsignDSC" runat="server" Visible="false">
                                                                                    <div class="certi_info bg-light m16">eSign / DSC</div>
                                                                                    <br />
                                                                                    <div class="form-group row">
                                                                                        <div class="col-sm-5">
                                                                                            <%-- <select type="text" class="form-control" id="input7">
                                                                                    <option>Select</option>
                                                                                    <option>Aadhar eSign </option>
                                                                                    <option>DSC</option>
                                                                                </select>--%>
                                                                                            <asp:Label runat="server" Visible="false" ID="lblStatus" Text=""></asp:Label>
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
                                                                            </div>
                                                                        </ContentTemplate>
                                                                        <Triggers>

                                                                            <asp:PostBackTrigger ControlID="btnEsignDSC" />

                                                                            <%--<asp:PostBackTrigger ControlID="Calendar1" />--%>
                                                                        </Triggers>
                                                                    </asp:UpdatePanel>
                                                                    <br />
                                                                    <br />




                                                                </asp:Panel>
                                                                <br />
                                                    </asp:Panel>







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
                                                    <%-- <li class="nav-item">
                                                            <a class="nav-link" id="custom-tabs-two-profile-tab" data-toggle="pill"
                                                            href="#custom-tabs-two-profile" role="tab" aria-controls="custom-tabs-two-profile"
                                                            aria-selected="false">Profile</a>
                                                            </li> --%>
                                                </ul>
                                            </div>
                                            <div class="col-sm-4">
                                                <div class="align-items-end d-flex justify-content-end mr-3 w-100">
                                                    <a href="#"><i class="fa fa-download mr-3 "></i></a>
                                                    <a href="#"><i class="fa fa-print"></i></a>
                                                </div>
                                            </div>
                                        </div>

                                        <%--button--%>
                                        <%--    <div class="text-right mt-2 mr-4">
                                                    <button type="button" class="btn btn-success btn-sm">Download </button>
                                                    &nbsp;
                                 <button type="button" class="btn btn-info float-right btn-sm">Print </button>
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
                                                                        <a class="nav-link" id="custom_tabs_four_settings_tab" style="pointer-events: none" data-toggle="pill" href="#custom-tabs-four-settings" role="tab" aria-controls="custom-tabs-four-settings" aria-selected="true" runat="server">Previous Proceeding</a>
                                                                    </li>
                                                                </ul>
                                                            </div>
                                                            <div class="card-body">
                                                                <div class="tab-content" id="custom-tabs-four-tabContent">
                                                                    <div class="tab-pane fade" id="custom-tabs-four-home" role="tabpanel" aria-labelledby="custom-tabs-four-home-tab">

                                                                        <h5>List of Documents </h5>
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

                                                                        <div>
                                                                            <iframe id="ifRecent" runat="server" height='750'></iframe>
                                                                        </div>

                                                                    </div>
                                                                    <div class="tab-pane fade" id="custom-tabs-four-messages" role="tabpanel" aria-labelledby="custom-tabs-four-messages-tab">

                                                                        <div id="DocPdfShowTab">
                                                                            <iframe id="RecentdocPath" clientidmode="Static" runat="server" width='550' height='750'></iframe>
                                                                            <iframe id="ifProposal1" clientidmode="Static" runat="server" scrolling="auto" height='750'></iframe>
                                                                            <iframe id="RecentAttachedDoc" clientidmode="Static" runat="server" width='550' height='750'></iframe>
                                                                            <iframe id="ifPDFViewerAll" clientidmode="Static" runat="server" scrolling="auto" height='750'></iframe>
                                                                        </div>

                                                                    </div>
                                                                    <div class="tab-pane fade " id="custom-tabs-four-settings" role="tabpanel" aria-labelledby="custom_tabs_four_settings_tab">

                                                                        <div>
                                                                            <%--<img src="../dist/img/Doc_Sample_pic.jpg" style="width: 708px;" />--%>
                                                                            <asp:Label ID="lblPrevious" runat="server" Visible="false"></asp:Label>
                                                                            <asp:Label ID="lblOrderProcceding" runat="server" Visible="false"></asp:Label>
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
    <asp:HiddenField ID="hdnUserID" runat="server" Value="0" />
    <asp:HiddenField ID="hdnbase64" ClientIDMode="Static" runat="server" />
    <script type="text/javascript">
        function DisableAddProceeding() {
            //alert("hello");
            document.getElementById("add_green_proceeding").disabled = true;
            //document.getElementById("add_green_proceeding").addClass("disabled");
            var element = document.getElementById("add_green_proceeding");
            element.classList.add("disabled");

        }
        function AddOrderSheet() {
            var HearingDate = $("#ContentPlaceHolder1_txtHearingDate").val();
            //alert(HearingDate);
            if (HearingDate == "") {
                alert("Please Select  Hearing Date");
                return false;
            }
            else {
                $("#hearing").show();

            }
        }



        function AddNotice() {
            
            // var HearingDate = $("#txtHearingDate").val();
            /*alert(HearingDate);*/

            //document.getElementById('txthearingdate').innerText = HearingDate;
            //



            document.getElementById("pContent").innerHTML = document.getElementById("summernote").value;

            $("#Edit").show();


            $("#custom-tabs-one-Order-Sheet").addClass("active");
            $("#custom-tabs-one-Order-Sheet").addClass("show");

            $("#custom-tabs-one-Proceeding").removeClass("active");
            $("#Proceeding").addClass("active");
            $("#Edit").removeClass("active");



            //$("#custom-tabs-one-Next_Hearing_Date").removeClass("active");
            //$("#Next_Hearing_Date").removeClass("active");







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

        //function ShowMessage() {
        //    swal({
        //        icon: 'success',
        //        title: 'Record Saved Successfully',
        //        type: 'Success'
        //    });
        //    return false;
        //    document.getElementById('txtHearingDate').focus();
        //    return false;
        //}


        function ShowMessage() {
            //var case_number = document.getElementById('hdnfldCase').value;
            //alert(case_number);

            Swal.fire({
                icon: 'success',
                title: 'Add Proceeding Successfully',
                showCancelButton: false,
                confirmButtonText: 'OK',
            }).then((result) => {
                if (result.value) {
                    window.location = 'AcceptRejectCases.aspx';
                }
                else {
                    alert('user click on Cancel button');
                }
            });

        }

        function ShowMessageYesNo() {

            Swal.fire({
                title: 'Do you want to skip proceeding?',
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

                    window.location = 'AcceptRejectCases.aspx';

                }

            })
        }

        function ShowMessageCloseYesNo() {

            Swal.fire({
                title: 'Do you want to close?',
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

                    window.location = 'AcceptRejectCases.aspx';

                }

            })
        }


        function ValidateRecord() {
            var HearingDate = $("#ContentPlaceHolder1_txtHearingDate").val();

            // alert(HearingDate);

            if (HearingDate == "") {
                alert("Please Select  Hearing Date");
                return false;
            }

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
        });

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



            // BindOrderSheet();
        });
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
    <div id="modal_dialog" style="display: none"></div>

</asp:Content>
