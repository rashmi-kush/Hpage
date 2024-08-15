<%@ Page Title="" Language="C#" MasterPageFile="~/CoS/CoSMaster.Master" AutoEventWireup="true" CodeBehind="PendingForNoticeDetails.aspx.cs" Inherits="CMS_Sampada.CoS.PendingForNoticeDetails" ValidateRequest="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <script src="EngToHin_Js/google_jsapi.js" type="text/javascript"></script>
    <script src="EngToHin_Js/jquery.min.js" type="text/javascript"></script>
     <link href="../Styles/sweetalert.css" rel="stylesheet" />
    <script src="../Scripts/sweetalert.min.js"></script>
    <script src="../assets/js/3.3.1/sweetalert2@11.js"></script>
    <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.7/jquery.min.js" type="text/javascript">

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
            // debugger;
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
        //function openSrDoc(FILE_PATH) {

        //    //alert(FILE_PATH);
        //    debugger;
        //    $("#List_pnl").hide();
        //    $("#View_pnl").show();
        //    $('#ifrDisplay').attr('src', FILE_PATH);

        //}
        function ValidateAddCopy() {

            //alert("Please Enter Copy Add Details...");
            Swal.fire({

                icon: 'error',
                title: 'Please Enter Copy Add Details !',
                showCancelButton: false,
                confirmButtonText: 'OK',





            });


        }
        function NoPartySelect() {

            //alert("Please Enter Copy Add Details...");
            Swal.fire({

                icon: 'info',
                title: 'Please Select Atleast One Party !',
                showCancelButton: false,
                confirmButtonText: 'OK',





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
            else if (document.getElementById("TxtLast4Digit").value == "") {

                Swal.fire({
                    icon: 'info',
                    title: 'Please enter last 4 digit of Adhar Card'

                })
                return false;

            }


        }
        function AddNotice() {


            document.getElementById("pContent").innerHTML = document.getElementById("summernote").value;
            $("#custom-tabs-one-ProposalForm").addClass("active");
            $("#custom-tabs-one-edit_notice").removeClass("active");
            $("#custom-tabs-one-profile-tab").removeClass("active");
            $("#custom-tabs-one-createNotice").addClass("active");

        }
        function CheckAll(Checkbox) {
            var GridVwHeaderCheckbox = document.getElementById("<%=grdCaseList.ClientID %>");
            for (i = 1; i < GridVwHeaderCheckbox.rows.length; i++) {
                GridVwHeaderCheckbox.rows[i].cells[0].getElementsByTagName("INPUT")[0].checked = Checkbox.checked;


            }
        }
        function ShowParties(thus) {
            var counter = 0;
            $("#<%=grdCaseList.ClientID%> input[id*='chkBoxGrdParty']:checkbox").each(function (index) {
                if ($(this).is(':checked'))
                    counter++;
            });
            alert(counter);


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

                control.makeTransliteratable(['txt']);
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

                control.makeTransliteratable(['txt']);
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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <asp:ScriptManager ID="SCMNG" runat="server"></asp:ScriptManager>
    <section class="content-header headercls">
        <div class="container-fluid">
            <div class="row">
                <div class="col-sm-3">
                    <h5>Notice </h5>
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
                                        <li class="nav-item"><a class="nav-link disabled" href="">Proposal</a></li>
                                        <li class="nav-item"><a class="nav-link disabled" href="#CreateOrderSheet" data-toggle="tab">Order Sheet</a></li>
                                        <li class="nav-item"><a class="nav-link active disabled" href="#Notice" data-toggle="tab">Notice</a></li>
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




                                <%------------------------------------------------------------------------------------------%>
                         

                       <div class="tab-pane active" id="Hearing">
                                    <div class="row">

                                        <div class="col-12 col-sm-6">
                                            <div class="card card-primary card-tabs h-100">
                                                <div class="card-header p-0 pt-1">
                                                    <ul class="nav nav-tabs" id="custom-tabs-one-tab" role="tablist">

                                                     <%--   <li class="nav-item">
                                                            <a class="nav-link" id="Order_Sheet" data-toggle="pill"
                                                                href="#custom-tabs-one-Order_Sheet" role="tab"
                                                                aria-controls="custom-tabs-one-profile" aria-selected="true">Order Sheet </a>
                                                        </li>--%>
                                                        <li class="nav-item">
                                                            <a class="nav-link active" id="createNotice" data-toggle="pill"
                                                                href="#custom-tabs-one-createNotice" role="tab" aria-controls="custom-tabs-one-home"
                                                                aria-selected="false">Create Notice </a>
                                                        </li>
                                                        <li class="nav-item">
                                                            <a class="nav-link" style="" id="edit_notice" runat="server" data-toggle="pill"
                                                                href="#custom-tabs-one-edit_notice" role="tab" aria-controls="custom-tabs-one-home"
                                                                aria-selected="false">Edit Notice </a>
                                                        </li>

                                                    </ul>
                                                </div>

                                                <div class="card-body">

                                                    <div class="tab-content" id="custom-tabs-one-tabContent">
                                                        <div class="tab-pane fade " id="custom-tabs-one-Order_Sheet" role="tabpanel"
                                                            aria-labelledby="Order_Sheet">

                                                            <div class="displaydiv">

                                                                <div class="main-box html_box htmldoc">
                                                                    <asp:Repeater ID="RptrProcedding" runat="server">
                                                                        <ItemTemplate>
                                                                            <div>
                                                                                <table>
                                                                                    <tr>
                                                                                        <th>Proceeding#</th>
                                                                                    </tr>
                                                                                    <tr>

                                                                                        <td style="text-align: justify; width: 100%;"><%#Eval("proceeding")%></td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td style="text-align: left;"><%#Eval("hearingdate")%></td>


                                                                                        <td style="text-align: right; width: 150px;"><b>Collector of Stamp</b></td>

                                                                                    </tr>



                                                                                </table>
                                                                            </div>
                                                                        </ItemTemplate>
                                                                    </asp:Repeater>

                                                                </div>
                                                                <br>
                                                            </div>


                                                        </div>

                                                        <div class="tab-pane active show" id="custom-tabs-one-createNotice" role="tabpanel"
                                                            aria-labelledby="createNotice">
                                                            <!-- table -->
                                                            <%--<div class="certi_info bg-light">Make a Notice</div>--%>
                                                            <%--<b>Notice to Party <span style="color: red;">*</span></b>--%>
                                                            <div class="table-responsive listtabl">


                                                                <asp:GridView ID="grdCaseList" CssClass="table table-bordered table-condensed table-striped table-hover"
                                                                    DataKeyNames="Party_ID,Party_Name,PartyFather_orHusband_orGuardianName,Party_Address,Application_NO,App_ID"
                                                                    runat="server" CellPadding="4" ForeColor="#333333" GridLines="None"
                                                                    AutoGenerateColumns="False" Visible="true" Width="100%" EmptyDataText="No record found" OnRowCommand="grdCaseList_RowCommand1">
                                                                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                                                    <Columns>
                                                                        <asp:TemplateField HeaderText="SNo." HeaderStyle-VerticalAlign="Top" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="false">
                                                                            <ItemTemplate>
                                                                                <itemtemplate><%#Container.DataItemIndex+1  %> </itemtemplate>
                                                                            </ItemTemplate>
                                                                            <ItemStyle Width="4%" HorizontalAlign="Center" />
                                                                            <HeaderStyle Width="4%" HorizontalAlign="Center" />
                                                                        </asp:TemplateField>
                                                                        <asp:BoundField Visible="false" HeaderText="Party ID" DataField="Party_ID" HeaderStyle-Font-Bold="false"/>
                                                                        <asp:BoundField HeaderText="Party Name" DataField="Party_Name" HeaderStyle-Font-Bold="false"/>
                                                                        <asp:BoundField HeaderText="Father Name" DataField="PartyFather_orHusband_orGuardianName" HeaderStyle-Font-Bold="false"/>
                                                                        <asp:BoundField HeaderText="Address" DataField="Party_Address" HeaderStyle-Font-Bold="false"/>
                                                                        <asp:TemplateField HeaderText="Party Type" HeaderStyle-Font-Bold="false">
                                                                            <ItemTemplate>
                                                                                <%#Eval("PARTY_TYPE_NAME_EN") %>
                                                                                <asp:Label ID="lblPartyId" Visible="false" runat="server" Text='<%#Eval("Party_ID") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                            <HeaderStyle Width="10%" HorizontalAlign="Left" />
                                                                            <ItemStyle Width="10%" HorizontalAlign="Left" />
                                                                        </asp:TemplateField>


                                                                      <%--  <asp:TemplateField HeaderText="Party Name">
                                                                            <ItemTemplate>
                                                                                <%#Eval("Party_Name") %>
                                                                            </ItemTemplate>
                                                                            <HeaderStyle Width="10%" HorizontalAlign="Center" />
                                                                            <ItemStyle Width="10%" HorizontalAlign="Left" />
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Owner Applicant Type">
                                                                            <ItemTemplate>
                                                                                <%#Eval("PARTY_TYPE_NAME_EN") %>
                                                                            </ItemTemplate>
                                                                            <HeaderStyle Width="10%" HorizontalAlign="Center" />
                                                                            <ItemStyle Width="10%" HorizontalAlign="Left" />
                                                                        </asp:TemplateField>--%>

                                                                        <asp:TemplateField HeaderText="Select">
                                                                            <HeaderTemplate>
                                                                                <asp:CheckBox ID="chkAllSelect" runat="server" AutoPostBack="true" OnCheckedChanged="chkAllSelect_CheckedChanged" />
                                                                            </HeaderTemplate>
                                                                            <ItemTemplate>
                                                                                <%--<asp:LinkButton ID="lnkSelect" runat="server" CommandArgument='<%#Eval("App_ID")+","+Eval("Application_no")+","+Eval("Case_Number") %>' CommandName="SelectApplication" CssClass="btn btn-secondary">Select</asp:LinkButton>--%>
                                                                                <asp:CheckBox ID="chkParty" CommandArgument='<%#Eval("Party_ID") %>' runat="server" />
                                                                                <%--<asp:Label ID="lblCount" runat="server" Text='<%#Eval("NoticeCount") %>' Visible="false"></asp:Label>--%>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
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
                                                            <div class="text-right">
                                                            <asp:Button ID="btnCreateNotice" runat="server" class="btn btn-success" Text="Create Notice"  OnClick="btnCreateNotice_Click" />
                                                           </div>
                                                                <br />

                                                            <div>
                                                                <asp:Panel ID="pnlNotice" runat="server" Visible="false">
                                                                    <div class="row">
                                                                    <div class="main-box html_box htmldoc">
                                                                        <h2 style="font-size: 18px; margin: 0; font-weight: 600;">कार्यालय जिला पंजीयक एवं न्यायालय कलेक्टर ऑफ स्टाम्प जिला भोपाल (म.प्र.)</h2>
                                                                        <h3 class="h-3">आई.एस.बी.टी. परिसर, मेजनाईन फ्लोर, हबीबगंज भोपाल
                                                                        <br>
                                                                            ई-मेल- igrs@igrs.gov.in</h3>
                                                                        <h2 class="h-22"><b>अधिनियम 1899 की धारा 33 के स्टाम्प प्रकरणों की सुनवाई हेतु सूचना पत्र
                                                                        <br>
                                                                            प्रकरण क्रमांक- 
                                                                        <asp:Label ID="lblCaseNo" runat="server"></asp:Label>
                                                                            धारा-33  </b></h2>
                                                                        <h2 class="h-22">मध्यप्रदेश शासन</h2>
                                                                        <h2 class="h-22">विरुद्ध</h2>
                                                                        <br>
                                                                        <h3 class="h-3 text-left">
                                                                            <asp:Label ID="lblRecord" runat="server" Visible="false" />
                                                                              <asp:GridView ID="grdSelectedParties" BorderStyle="None" CssClass="table table-bordered table-condensed table-hover"
                                                                                runat="server" CellPadding="4" GridLines="None" AutoGenerateColumns="False" Visible="true" Width="100%" EmptyDataText="No recoed found">
                                                                                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                                                                <Columns>
                                                                                    <asp:TemplateField HeaderText="क्रमांक" HeaderStyle-VerticalAlign="Top" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="true">
                                                                                        <ItemTemplate>
                                                                                            <itemtemplate><%#Container.DataItemIndex+1  %> </itemtemplate>
                                                                                        </ItemTemplate>
                                                                                        <ItemStyle  HorizontalAlign="Center" />
                                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="नाम" HeaderStyle-Font-Bold="true"> 
                                                                                        <ItemTemplate>
                                                                                            <%#Eval("Name") %>
                                                                                        </ItemTemplate>
                                                                                        <HeaderStyle  HorizontalAlign="Center" />
                                                                                        <ItemStyle  HorizontalAlign="Center" />
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="पिता का नाम" HeaderStyle-Font-Bold="true">  
                                                                                        <ItemTemplate>
                                                                                            <%#Eval("FatherName") %>
                                                                                        </ItemTemplate>
                                                                                        <HeaderStyle  HorizontalAlign="Center" />
                                                                                        <ItemStyle  HorizontalAlign="Center" />
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="पता" HeaderStyle-Font-Bold="true">  
                                                                                        <ItemTemplate>
                                                                                            <%#Eval("Address") %>
                                                                                        </ItemTemplate>
                                                                                        <HeaderStyle  HorizontalAlign="Center" />
                                                                                        <ItemStyle  HorizontalAlign="Center" />
                                                                                    </asp:TemplateField>


                                                                                </Columns>
                                                                             
                                                                            </asp:GridView>
                                                                            <br />
                                                                            <b>आवेदक  (प्रथम पक्षकार)</b>
                                                                            <br />
                                                                            <br />
                                                                            <br />
                                                                            <asp:Label ID="lblDepartment" runat="server"></asp:Label>
                                                                            <br>
                                                                            <br>
                                                                            <b>अनावेदक (द्वितीय पक्षकार)</b>
                                                                            <br />
                                                                            
                                                                        </h3>


                                                                        <br>
                                                                        <div class="devidetwo">
                                                                            <div>

                                                                                <p id="pContent">
                                                                                </p>

                                                                            </div>

                                                                        </div>
                                                                        <br /><br />
                                                                        <b class="stamp">स्थान- जिला पंजीयक कार्यालय, भोपाल-2
                                                                        <br />
                                                                            जारी दिनांक :
                                                                        <asp:Label ID="lblTodate" runat="server" ></asp:Label>
                                                                            <br />
                                                                            <br />
                                                                        </b>


                                                                        <div>
                                                                        </div>
                                                                        <br />
                                                                        <br />
                                                                        <br />
                                                                        <asp:Panel ID="PnlPratilipi" runat="server" Visible="false" class="text-left">
                                                                            <b>
                                                                                <asp:Label ID="lblPratilip" runat="server" Text="प्रतिलिपि"></asp:Label></b>
                                                                            <asp:GridView ID="GrdAddCopy_Details" BorderStyle="None" CssClass="table table-bordered table-condensed  table-hover"
                                                                                runat="server" CellPadding="4"  GridLines="None" AutoGenerateColumns="False" Visible="true" Width="100%" EmptyDataText="No recoed found">
                                                                                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                                                                <Columns>
                                                                                    <asp:TemplateField HeaderText="क्र." HeaderStyle-VerticalAlign="Top" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="true">
                                                                                        <ItemTemplate>
                                                                                            <itemtemplate><%#Container.DataItemIndex+1  %> </itemtemplate>
                                                                                        </ItemTemplate>
                                                                                        <ItemStyle  HorizontalAlign="Center" />
                                                                                        <HeaderStyle  HorizontalAlign="Center" />
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="सूचनार्थ प्रेषित/विवरण" HeaderStyle-Font-Bold="true">
                                                                                        <ItemTemplate>
                                                                                            <%#Eval("CopyContent") %>
                                                                                        </ItemTemplate>
                                                                                        <HeaderStyle  HorizontalAlign="Center" />
                                                                                        <ItemStyle  HorizontalAlign="Left" />
                                                                                    </asp:TemplateField>


                                                                                </Columns>
                                                                            
                                                                            </asp:GridView>
                                                                        </asp:Panel>

                                                                    </div>
                                                                    </div>
                                                                    <br />
                                                                     <asp:Panel ID="pnlSaveDraft" runat="server" Visible="false">
                                                                  <div class="row">
                                                                         <div id="hearing" style="width: 100%;">
                                                                        <div class="mycheck mul_check">
                                                                            <h6><b style="padding-right: 12px;">क्या आप कोई प्रतिलिपि जोड़ना चाहेंगे।</b></h6>
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
<label>प्रतिलिपि प्रेषित किये जाने हेतु जानकारी दें</label>
       <%-- <asp:TextBox ID="txt" CssClass="form-control" runat="server" placeholder="Enter Content"></asp:TextBox>--%>
               <asp:TextBox TextMode="MultiLine" ID="txt" ClientIDMode="Static" runat="server" CssClass="form-control"  placeholder="प्रतिलिपि प्रेषित करने सम्बंदित का नाम एवं सम्बंधित विवरण लिखे" Style="font-family: Verdana; font-size: Medium; "></asp:TextBox>
          <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ValidationGroup="AddCopyValidate" ForeColor="Red"
            ControlToValidate="txt" ErrorMessage="Content required"></asp:RequiredFieldValidator>
    </div> </div>
                                                                                        </div>
                                                                                       
                                                                                 <div class="row mt-xl-n4 mb-xl-n3">
                                                                                     <div class="col-12">
                                                                                         <div class="form-group">
                                                                                        <label>प्रतिलिपि भेजने वाले व्यक्ति का विवरण दर्ज करें।</label>
                                                                                     </div>
                                                                                         </div>
                                                                                 </div>

        <div class="row">
           
            <div class="col-6">
                <asp:TextBox ID="txtCopyName" CssClass="form-control" runat="server" placeholder="Enter Name"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ValidationGroup="AddCopyValidate" ForeColor="Red"
                    ControlToValidate="txtCopyName" ErrorMessage=" Name required"></asp:RequiredFieldValidator>
            
                </div>
            <div class="col-6">

                <asp:TextBox ID="txtCopyEmail" CssClass="form-control" runat="server" TextMode="Email" placeholder="Enter Email ID"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ValidationGroup="AddCopyValidate"
                    ControlToValidate="txtCopyEmail" ErrorMessage="Email ID required"  ForeColor="Red"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="REVEmail" runat="server" ValidationGroup="AddCopyValidate" ControlToValidate="txtCopyEmail"  ErrorMessage="Enter Correct MailID"  ForeColor="Red" ValidationExpression="^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$"></asp:RegularExpressionValidator>
            </div>
            </div>
<div class="row">
            <div class="col-3">
                <%--<asp:TextBox ID="txtMobile" CssClass="form-control" runat="server" TextMode="Number" MaxLength="10" placeholder="Enter Mobile"></asp:TextBox>--%>
                 <asp:TextBox ID="txtMobile" CssClass="form-control" runat="server"  MaxLength="10" Enabled="true" ClientIDMode="Static"  AutoCompleteType="None" onkeypress='return restrictAlphabets(event)' autocomplete="off" placeholder="Enter Mobile"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ValidationGroup="AddCopyValidate" ForeColor="Red"
                    ControlToValidate="txtMobile" ErrorMessage=" Mobile No required"></asp:RequiredFieldValidator>
            </div>

            <div class="col-3">
                <%--<asp:TextBox ID="txtWhatsApp" CssClass="form-control" runat="server" TextMode="Number" MaxLength="10" placeholder="Enter WhatsApp No"></asp:TextBox>--%>
                <asp:TextBox ID="txtWhatsApp" CssClass="form-control" runat="server"  MaxLength="10" Enabled="true" ClientIDMode="Static"  AutoCompleteType="None" onkeypress='return restrictAlphabets(event)' autocomplete="off" placeholder="Enter WhatsApp No"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ValidationGroup="AddCopyValidate" ForeColor="Red"
                    ControlToValidate="txtWhatsApp" ErrorMessage="WhatsApp NO required"></asp:RequiredFieldValidator>
            </div>
            <div class="col-3">
                <asp:Button ID="btnSaveCopy" runat="server" class="btn btn-success" ValidationGroup="AddCopyValidate" Text="Add" OnClick="btnSaveCopy_Click"/>
                <%--<button type="button" class="btn btn-success">Save</button>--%>
                <%--<button type="button" class="btn btn-info float-right ">Add More </button>--%>
            </div>
        </div>                                                                            </div>


                                                                        </div>
                                                                    </div>
                                                                  </div>
                                                                    <%----------------------------%>
                                                                   
                                                                    <br />
                                                                         <%--<div class="row">
                                                                    <div class="form-group">--%>
                                                                          <div class="text-right">
                                                                            <asp:Button ID="BtnSaveDraft" class="btn btn-success" Text="Save Notice Draft" runat="server" OnClick="BtnSaveDraft_Click" />

                                                                    </div>
                                                                             <%--</div>--%>
                                                                         </asp:Panel>
                                                                    <br>
                                                                   <%-- <asp:Panel ID="pnlSign" runat="server" Visible="false">
                                                                        <div class="certi_info bg-light">eSign / DSC</div>
                                                                        <br>
                                                                        <div class="form-group row">
                                                                            <div class="col-sm-4">
                                                                                <select type="text" class="form-control" id="input7">
                                                                                    <option>Select</option>
                                                                                    <option>Aadhar eSign </option>
                                                                                    <option>DSC</option>
                                                                                </select>
                                                                            </div>
                                                                           
                                                                            <asp:Button ID="btnEsignDSC" runat="server" Text="eSign/DSC" class="btn btn-success" OnClick="btnEsignDSC_Click" />
                                                                        </div>
                                                                    </asp:Panel>--%>

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
                                                                                
                                                                                <asp:Button ID="btnEsignDSC" runat="server" OnClick="btnEsignDSC_Click" class="btn btn-success" Text="eSign/DSC" OnClientClick="return ValidateEsignRecord()" />
                                                                            </div>
                                                                        </div>
                                                                    </asp:Panel>

                                                                   
                                                                </asp:Panel>
                                                                 <asp:Panel ID="pnlSend" runat="server" Visible="false">
                                                                         <h6><b>Send Notice Through</b></h6>
                                                                        <div class="mycheck mul_check">
                                                                            <div class="custom-control custom-checkbox mycb">
                                                                               <%-- <input class="custom-control-input" type="checkbox" id="customCheckbox01" value="option1">
                                                                                <label for="customCheckbox01" class="custom-control-label">Mobile No</label>--%>
                                                                                 <asp:CheckBox ID="checksms" runat="server" Text="SMS" Checked="true"  />
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
                                                                                DataKeyNames="Party_ID"
                                                                                runat="server" CellPadding="4" ForeColor="#333333" GridLines="None"
                                                                                AutoGenerateColumns="False" Visible="true" Width="100%" EmptyDataText="No record found" OnRowCommand="grdCaseList_RowCommand1">
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
                                                                            <asp:GridView ID="GridView1" CssClass="table table-bordered table-condensed table-striped table-hover"
                                                                                runat="server" CellPadding="4" ForeColor="#333333" GridLines="None"
                                                                                AutoGenerateColumns="False" Visible="true" Width="100%" EmptyDataText="No record found" OnRowCommand="grdCaseList_RowCommand1">
                                                                                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                                                                <Columns>
                                                                                    <asp:TemplateField HeaderText="SNo." HeaderStyle-VerticalAlign="Top" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="false">
                                                                                        <ItemTemplate>
                                                                                            <itemtemplate><%#Container.DataItemIndex+1  %> </itemtemplate>
                                                                                        </ItemTemplate>
                                                                                        <ItemStyle Width="4%" HorizontalAlign="Center" />
                                                                                        <HeaderStyle Width="4%" HorizontalAlign="Center" />
                                                                                    </asp:TemplateField>

                                                                                    <asp:BoundField HeaderText="Name" DataField="Sender_Name" HeaderStyle-Font-Bold="false" />
                                                                                    <asp:BoundField HeaderText="SMS" DataField="mobileno" HeaderStyle-Font-Bold="false" />
                                                                                    <asp:BoundField HeaderText="WhatsAPP" DataField="whatsappno" HeaderStyle-Font-Bold="false" />
                                                                                    <asp:BoundField HeaderText="Email" DataField="emailID" HeaderStyle-Font-Bold="false" />



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
                                                                        <asp:Button ID="btnSendNotice" runat="server" class="btn btn-success" Text="Send Notice" OnClick="btnSendNotice_Click" />
                                                                   </asp:Panel>
                                                            </div>
                                                        </div>
                                                        <div class="tab-pane fade" id="custom-tabs-one-edit_notice"
                                                            aria-labelledby="custom-tabs-one-profile-tab">

                                                            <%--<div class="tab-pane fade " id="custom-tabs-one-edit_notice" role="tabpanel"
                                                            aria-labelledby="edit_notice">--%>
                                                            <textarea id="summernote" clientidmode="Static" runat="server">
                                                             
                                                                               
                                                                                
                                                                            


                                                           </textarea>
                                                            <a onclick="AddNotice()" href="#custom-tabs-one-ProposalForm" class="btn btn-success">Save</a>
                                                            <%--<button type="button" class="btn btn-success">Save </button>--%>
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
                                                       <a href="#" onclick="printPDF()"> <i class="fa fa-print"></i></a>
                                                    </div></div>
                                                </div>

                                                <%--button--%>
                                             <%--   <div class="text-right">
                                                    <button type="button" class="btn btn-success">Download </button>
                                                    &nbsp;
                                 <button type="button" class="btn btn-info float-right">Print </button>
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


                                                                                <%--<button type="button" class="btn btn-success float-right" data-toggle="modal"  data-target="#exampleModalCenter">Attach </button>--%>

                                                                            </div>
                                                                            <div class="tab-pane fade active show" id="custom-tabs-four-profile" role="tabpanel" aria-labelledby="custom-tabs-four-profile-tab">

                                                                                <div>
                                                                                    <%--<img src="../dist/img/Doc_Sample_pic.jpg" style="width: 708px;" />--%>
                                                                                    <iframe id="ifRecent" runat="server" height='750' visible="false"></iframe>
                                                                                    <iframe id="docPath" runat="server" height='750'></iframe>
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



                        <asp:GridView ID="grdSendNoticeParty" CssClass="table table-bordered table-condensed table-striped table-hover"
                            runat="server" CellPadding="4" ForeColor="#333333" GridLines="None" AutoGenerateColumns="False" Visible="true" Width="100%" EmptyDataText="No recoed found">
                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                            <Columns>
                                <asp:TemplateField HeaderText="S.No" HeaderStyle-VerticalAlign="Top" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="false">
                                    <ItemTemplate>
                                        <itemtemplate><%#Container.DataItemIndex+1  %> </itemtemplate>
                                    </ItemTemplate>
                                    <ItemStyle Width="2%" HorizontalAlign="Center" />
                                    <HeaderStyle Width="2%" HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Name">
                                    <ItemTemplate>
                                        <%#Eval("Copyname") %>
                                    </ItemTemplate>
                                    <HeaderStyle Width="10%" HorizontalAlign="Left" />
                                    <ItemStyle Width="10%" HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Mobile">
                                    <ItemTemplate>
                                        <%#Eval("CopyEmail") %>
                                    </ItemTemplate>
                                    <HeaderStyle Width="10%" HorizontalAlign="Left" />
                                    <ItemStyle Width="10%" HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="E-Mail">
                                    <ItemTemplate>
                                        <%#Eval("CopyMob") %>
                                    </ItemTemplate>
                                    <HeaderStyle Width="10%" HorizontalAlign="Left" />
                                    <ItemStyle Width="10%" HorizontalAlign="Left" />
                                </asp:TemplateField>



                            </Columns>
                            <EditRowStyle BackColor="#999999" />
                            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle BackColor="#0090a2" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                            <SortedAscendingCellStyle BackColor="#E9E7E2" />
                            <SortedAscendingHeaderStyle BackColor="#506C8C" />
                            <SortedDescendingCellStyle BackColor="#FFFDF8" />
                            <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                        </asp:GridView>

                    </div>

                </div>
                <div class="modal-footer">
                    <%-- <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>--%>
                    <button type="button" class="btn btn-primary">Done</button>
                </div>
            </div>
        </div>
    </div>
    <asp:HiddenField ID="hdnfParty_Name" ClientIDMode="Static" runat="server" />
    <asp:HiddenField ID="hdnDepartment" ClientIDMode="Static" runat="server" />

    <script>

        function ShowMessageSendNotice() {
            //var case_number = document.getElementById('hdnfldCase').value;
            //alert(case_number);

            Swal.fire({
                icon: 'success',
                title: 'Notice send successfully',
                showCancelButton: false,
                confirmButtonText: 'OK',
            }).then((result) => {
                if (result.value) {
                    window.location = 'Ordersheet.aspx?Case_Number=' + case_number;
                }
                else {
                    alert('user click on Cancel button');
                }
            });

        }
    </script>
</asp:Content>
