<%@ Page Title="" Language="C#" MasterPageFile="~/CoS/CoSMaster.Master" AutoEventWireup="true" CodeBehind="COSLegacyDataFormCreate.aspx.cs" Inherits="CMS_Sampada.CoS.COSLegacyDataFormCreate" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    
       <asp:ScriptManager ID="SCMNG" runat="server"></asp:ScriptManager>
   <section class="content-header">
      <div class="container-fluid">
         <div class="row">
            <div class="col-sm-6">
               <h5>
                  Proposal No - 
                  <asp:Label ID="lblProposalIdHeading" Text="" runat="server"></asp:Label>
               </h5>
            </div>
            <div class="col-sm-4">
               <h5>
                  Proposal Impound Date - 
                  <asp:Label ID="lblProImpoundDt" runat="server"></asp:Label>
               </h5>
            </div>
            <div class="col-sm-4">
               <h5>
                  <asp:Label ID="lblTodate" Visible="false" runat="server"></asp:Label>
               </h5>
            </div>
            <%-- 
               <div class="col-sm-6">
                   <ol class="breadcrumb float-sm-right">
                       <li class="breadcrumb-item">
                           <a href="#">Home</a>
                       </li>
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
                        <li class="nav-item">
                           <a class="nav-link active disabled" href="">Proposal</a>
                        </li>
                        <li class="nav-item">
                           <a class="nav-link disabled" href="OrdersheetPending">Order Sheet</a>
                        </li>
                        <li class="nav-item">
                           <a class="nav-link disabled" href="#Notice" data-toggle="tab">Notice</a>
                        </li>
                        <li class="nav-item">
                           <a class="nav-link disabled" href="ReportSeeking">Seek Report</a>
                        </li>
                        <%--
                           <li class="nav-item">
                               <a class="nav-link disabled" href="#EarlyHearing" data-toggle="tab">Early
                           Hearing</a>
                           </li>
                           <li class="nav-item">
                               <a class="nav-link disabled" href="#SendToAppeal" data-toggle="tab">Send to
                           Appeal</a>
                           </li>
                           <li class="nav-item">
                               <a class="nav-link" href="#SendBack" data-toggle="tab">Send Back</a>
                           </li>
                           <li class="nav-item">
                               <a class="nav-link disabled" href="#Reply" data-toggle="tab">Reply</a>
                           </li>
                           <li class="nav-item">
                               <a class="nav-link disabled" href="#Attachement"
                               data-toggle="tab">Attachement</a>
                           </li>--%>
                     </ul>
                  </div>
                  <div class="card-body">
                     <div class="tab-content">
                        <%------------------------------------------------------------------------------------------%> 
                        <div class="tab-pane active" id="SendBack">
                           <div class="row">
                              <div class="col-12 col-sm-6">
                                 <div class="card card-primary card-tabs h-100">
                                    <div class="card-header p-0 pt-1">
                                       <ul class="nav nav-tabs" id="custom-tabs-one-tab" role="tablist">
                                          <li class="nav-item">
                                             <a class="nav-link active" id="custom-tabs-one-profile-tab" data-toggle="pill" href="#custom-tabs-one-RegisteredForm" role="tab" aria-controls="custom-tabs-one-profile" aria-selected="true">Proposal Form </a>
                                          </li>
                                       </ul>
                                    </div>
                                    <div class="card-body">
                                       <div class="tab-content" id="custom-tabs-one-tabContent">
                                          <div class="tab-pane active show" id="custom-tabs-one-RegisteredForm" role="tabpanel" aria-labelledby="custom-tabs-one-profile-tab">
                                             <!-- table -->
                                             <div class="col-md-12">
                                                <div class="card card-primary">
                                                   <div class="card-header">
                                                      <h3 class="card-title">Case Details</h3>
                                                      <div class="card-tools">
                                                         <button type="button" class="btn btn-tool" data-card-widget="collapse">
                                                         <i class="fas fa-minus"></i>
                                                         </button>
                                                      </div>
                                                   </div>
                                                   <div class="card-body">
                                                       <asp:GridView ID="Product" runat="server" AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="ProductID" EnableViewState="False" class="table table-striped table-bordered table-all-common">
  <Columns>
  <asp:BoundField DataField="serialnumber" HeaderText="S.no" SortExpression="serialnumber">
         <ItemStyle CssClass="GridViewCell" />
            <HeaderStyle CssClass="GridViewHeader" />
    </asp:BoundField>

    <asp:BoundField DataField="Documentnumber" HeaderText="Document No" SortExpression="Documentnumber">
         <ItemStyle CssClass="GridViewCell" />
            <HeaderStyle CssClass="GridViewHeader" />
    </asp:BoundField>



      <asp:BoundField DataField="RegisteredDocument" HeaderText="Registered Document" SortExpression="RegisteredDocument">
         <ItemStyle CssClass="GridViewCell" />
            <HeaderStyle CssClass="GridViewHeader" />
    </asp:BoundField>


    <asp:BoundField DataField="InstrumentName" HeaderText="Instrument Name" SortExpression="InstrumentName">
         <ItemStyle CssClass="GridViewCell" />
            <HeaderStyle CssClass="GridViewHeader" />
    </asp:BoundField>

        <asp:BoundField DataField="DeedName" HeaderText="Deed Name" SortExpression="DeedName">
         <ItemStyle CssClass="GridViewCell" />
            <HeaderStyle CssClass="GridViewHeader" />
    </asp:BoundField>


    <asp:BoundField DataField="Impoundedby" HeaderText="Impounded by" ReadOnly="True" SortExpression="Impoundedby">
        <ItemStyle CssClass="GridViewCell" />
            <HeaderStyle CssClass="GridViewHeader" />
    </asp:BoundField>

    <asp:BoundField DataField="ReasonforImpound" HeaderText="Reason for Impound" ReadOnly="True" SortExpression="ReasonforImpound">
        <ItemStyle CssClass="GridViewCell" />
            <HeaderStyle CssClass="GridViewHeader" />
    </asp:BoundField>

  </Columns>
</asp:GridView>
                                                    
                                                   </div>
                                                </div>
                                             </div>
                                             <div class="col-md-12">
                                                <div class="card card-primary collapsed-card">
                                                   <div class="card-header">
                                                      <h3 class="card-title">Party Details</h3>
                                                      <div class="card-tools">
                                                         <button type="button" class="btn btn-tool" data-card-widget="collapse">
                                                         <i class="fas fa-plus"></i>
                                                         </button>
                                                      </div>
                                                   </div>
                                                   <div class="card-body">

                                                      <table class="table table-striped table-bordered table-all-common">
                                                            <thead>
                                                                <tr>
                                                                    <th>S.no</th>
                                                                    <th>Party Name</th>
                                                                    <th>Phone No</th>
                                                                    <th>Email ID</th>
                                                                    <th>Party Type</th>
                                                                    <th>Qwner/Applicant Type</th>
                                                                    <th>Father Name</th>
                                                                    <th>Address</th>
                                                                </tr>
                                                            </thead>

                                                            <tbody>
                                                                <tr>
                                                                    <td>1</td>
                                                                    <td>Vijay</td>
                                                                    <td>09872987398</td>
                                                                    <td>xyz@gmail.com</td>
                                                                    <td>Buyer</td>
                                                                    <td>Individual</td>
                                                                    <td>Manoj</td>
                                                                    <td>Arera colony</td>
                                                                </tr>
                                                            </tbody>
                                                        </table>
                                                      <div class="row">
                                                         <div class="col-12">
                                                            <%--Unduly Stamped--%> 
                                                            <asp:Label ID="lblReasonImpound" runat="server" Text=""></asp:Label>
                                                         </div>
                                                      </div>
                                                   </div>
                                                </div>
                                             </div>
                                             <div class="col-md-12">
                                                <div class="card card-primary collapsed-card">
                                                   <div class="card-header">
                                                      <h3 class="card-title">Property Details</h3>
                                                      <div class="card-tools">
                                                         <button type="button" class="btn btn-tool" data-card-widget="collapse">
                                                         <i class="fas fa-plus"></i>
                                                         </button>
                                                      </div>
                                                   </div>
                                                   <div class="card-body">
                                                      <div class="table-responsive">

                                                        <table class="table table-striped table-bordered table-all-common">
                                                            <thead>
                                                                <tr>
                                                                    <th>S.no</th>
                                                                    <th>Property Id</th>
                                                                    <th>Property Type</th>
                                                                    <th>Property Sub Type</th>
                                                                    <th>Property Transacting Area</th>
                                                                    <th>Total Property Area</th>
                                                                    <th>Transacting Partial Area of Proporty</th>
                                                                    <th>Property Address</th>
                                                                </tr>
                                                            </thead>

                                                            <tbody>
                                                                <tr>
                                                                    <td>1</td>
                                                                    <td>1001896724</td>
                                                                    <td>BUILDING</td>
                                                                    <td>Independent Building for building</td>
                                                                    <td>NO</td>
                                                                    <td>1500</td>
                                                                    <td>1500</td>
                                                                    <td>PRATAP COLONY Harda</td>
                                                                </tr>
                                                            </tbody>
                                                        </table>
                                                        
                                                      </div>
                                                   </div>
                                                </div>
                                             </div>
                                             <div class="col-md-12">
                                                <div class="card card-primary collapsed-card">
                                                   <div class="card-header">
                                                      <h3 class="card-title">Proposal Details</h3>
                                                      <div class="card-tools">
                                                         <button type="button" class="btn btn-tool" data-card-widget="collapse">
                                                         <i class="fas fa-plus"></i>
                                                         </button>
                                                      </div>
                                                   </div>
                                                   <div class="card-body">
                                                      <div class="table-responsive">
                                                          <table class="table table-striped table-bordered table-all-common">
                                                            <thead>
                                                                <tr>
                                                                    <th>Registry Initiation Date</th>
                                                                    <th>Date of Execution</th>
                                                                    <th>Date of Presentation</th>
                                                                    <th>Document Ragistration Date</th>
                                                                    <th>Registered Document No</th>
                                                                </tr>
                                                            </thead>

                                                            <tbody>
                                                                <tr>
                                                                    <td>--</td>
                                                                    <td>--</td>
                                                                    <td>--</td>
                                                                    <td>--</td>
                                                                    <td>--</td>
                                                                </tr>
                                                            </tbody>
                                                        </table>
                                                      </div>
                                                   </div>
                                                </div>
                                             </div>
                                             <div class="col-md-12">
                                                <div class="card card-primary collapsed-card">
                                                   <div class="card-header">
                                                      <h3 class="card-title">Proposal Details</h3> 
                                                    
                                                      <div class="card-tools">
                                                         <button type="button" class="btn btn-tool" data-card-widget="collapse">
                                                         <i class="fas fa-plus"></i>
                                                         </button>
                                                      </div>
                                                   </div>
                                                   <div class="card-body">
                                                      <div class="table-responsive">
                                                          <table class="table table-striped table-bordered table-all-common">
                                                            <thead>
                                                                <tr>
                                                                    <th>Particulars</th>
                                                                    <th>Details of Document as Presented by Party</th>
                                                                    <th>Proposal of Registering Officer</th>
                                                                    <th>Deficit Duty / Variation</th>
                                                                    <th>Remark</th>
                                                                </tr>
                                                            </thead>

                                                            <tbody>
                                                                <tr>
                                                                    <td>Nature of Document</td>
                                                                    <td>Partnership with property excluding Cash</td>
                                                                    <td>Partnership with property excluding Cash</td>
                                                                    <td></td>
                                                                    <td></td>
                                                                </tr>

                                                                <%-- 2 --%>

                                                                  <tr>
                                                                    <td>Consideration Value (₹)</td>
                                                                    <td>4400</td>
                                                                    <td>4400</td>
                                                                    <td></td>
                                                                    <td></td>
                                                                </tr>

                                                                <%-- 3 --%>

                                                                 <tr>
                                                                    <td>Guideline Value of Property (₹)</td>
                                                                    <td>316500000</td>
                                                                    <td>316500000</td>
                                                                    <td></td>
                                                                    <td></td>
                                                                </tr>

                                                                <%-- 4 --%>

                                                                <tr>
                                                                    <td>Stamp Duty Bifurcation</td>
                                                                    <td></td>
                                                                    <td></td>
                                                                    <td></td>
                                                                    <td></td>
                                                                </tr>

                                                                <%-- 5 --%>
                                                                 <tr>
                                                                    <td>Janpad (₹)</td>
                                                                    <td>0</td>
                                                                    <td>0</td>
                                                                    <td></td>
                                                                    <td></td>
                                                                </tr>

                                                                <%-- 6 --%>
                                                                 <tr>
                                                                    <td>Upkar (₹)</td>
                                                                    <td>0</td>
                                                                    <td>0</td>
                                                                    <td></td>
                                                                    <td></td>
                                                                </tr>

                                                                <%-- 7 --%>
                                                                 <tr>
                                                                    <td>Muncipal (₹)</td>
                                                                    <td>0</td>
                                                                    <td>0</td>
                                                                    <td></td>
                                                                    <td></td>
                                                                </tr>
                                                                <%-- 8 --%>
                                                                   <tr>
                                                                    <td>Principle (₹)</td>
                                                                    <td>6330000</td>
                                                                    <td>6330000</td>
                                                                    <td>100000</td>
                                                                    <td></td>
                                                                </tr>

                                                                <%-- 9 --%>
                                                                   <tr>
                                                                    <td>Exempted Amount (₹)</td>
                                                                    <td>0</td>
                                                                    <td></td>
                                                                    <td></td>
                                                                    <td></td>
                                                                </tr>

                                                                <%-- 10 --%>
                                                                  <tr>
                                                                    <td>Net Stamp Duty (₹)</td>
                                                                    <td>6330000</td>
                                                                    <td>6430000</td>
                                                                    <td></td>
                                                                    <td></td>
                                                                </tr>

                                                                <%-- 11 --%>
                                                                <tr>
                                                                    <td>Registration Fee Bifurcation</td>
                                                                    <td></td>
                                                                    <td></td>
                                                                    <td></td>
                                                                    <td></td>
                                                                </tr>

                                                                <%-- 12 --%>
                                                                 <tr>
                                                                    <td>Registration Fee (₹)</td>
                                                                    <td>2532000</td>
                                                                    <td>2532000</td>
                                                                    <td></td>
                                                                    <td></td>
                                                                </tr>

                                                                <%-- 13 --%>
                                                                <tr>
                                                                    <td>Exempted Amount (₹)</td>
                                                                    <td>0</td>
                                                                    <td></td>
                                                                    <td></td>
                                                                    <td></td>
                                                                </tr>
                                                                <%-- 14 --%>
                                                                  <tr>
                                                                    <td>Net Registration Fees (₹)</td>
                                                                    <td>2532000</td>
                                                                    <td>2532000</td>
                                                                    <td></td>
                                                                    <td></td>
                                                                </tr>
                                                                <%-- 15 --%>
                                                                 <tr>
                                                                    <td>Total (Stamp Duty+ Registration fees (₹)</td>
                                                                    <td>8862000</td>
                                                                    <td>8962000</td>
                                                                    <td>100000</td>
                                                                    <td></td>
                                                                </tr>
                                                            </tbody>
                                                        </table>
                                                      </div>
                                                     
                                                   </div>
                                                </div>
                                             </div>

                                               <div class="col-md-12">
                                                <div class="card card-primary collapsed-card">
                                                   <div class="card-header">
                                                      <h3 class="card-title">Sub Registrar Details</h3>
                                                      <div class="card-tools">
                                                         <button type="button" class="btn btn-tool" data-card-widget="collapse">
                                                         <i class="fas fa-plus"></i>
                                                         </button>
                                                      </div>
                                                   </div>
                                                   <div class="card-body">
                                                      <div class="table-responsive">
                                                          <table class="table table-striped table-bordered table-all-common">
                                                            <thead>
                                                                <tr>
                                                                    <th>SRO Id</th>
                                                                    <th>Sub Registrar Name</th>
                                                                    <th>SRO Name</th>
                                                                </tr>
                                                            </thead>

                                                            <tbody>
                                                                <tr>
                                                                    <td>--</td>
                                                                    <td>--</td>
                                                                    <td>--</td>
                                                                    <td>--</td>
                                                                    <td>--</td>
                                                                </tr>
                                                            </tbody>
                                                        </table>
                                                      </div>
                                                   </div>
                                                </div>
                                             </div>


                                               <div class="col-md-12">
                                                <div class="card card-primary collapsed-card">
                                                   <div class="card-header">
                                                      <h3 class="card-title">Case Status    </h3>
                                                      <div class="card-tools">
                                                         <button type="button" class="btn btn-tool" data-card-widget="collapse">
                                                         <i class="fas fa-plus"></i>
                                                         </button>
                                                      </div>
                                                   </div>
                                                   <div class="card-body">
                                                      <div class="table-responsive">
                                                          <table class="table table-striped table-bordered table-all-common">
                                                            <thead>
                                                                <tr>
                                                                    <th>No of Notice Send</th>
                                                                    <th>Date of Impound</th>
                                                                    <th>Next Hearing date</th>
                                                                    <th>Year Case Registered</th>
                                                                    <th>Head</th>
                                                                    <th>Section</th>
                                                                </tr>
                                                            </thead>

                                                            <tbody>
                                                                <tr>
                                                                    <td>5</td>
                                                                    <td>25-5-2023</td>
                                                                    <td>12-05-2024</td>
                                                                    <td>2023</td>
                                                                    <td>--</td>
                                                                </tr>
                                                            </tbody>
                                                        </table>
                                                      </div>
                                                   </div>
                                                </div>
                                             </div>

                                             <div class="col-md-12">
                                                 <div class="row">
                                                     <div class="col-md-12">
                                                         <div class="form-group">
                                                             <input type="text" class="form-control" placeholder="The case impounded on " name="">
                                                         </div>
                                                     </div>

                                                     

                                                     <div class="col-md-12">
                                                         <div class="form-group">
                                                             <input type="text" class="form-control" placeholder="Enter Comment " name="">
                                                         </div>
                                                     </div>
                                                 </div>
                                             </div>

                                            
                                           <div class="col-md-12">
                                               <div class="row">
                                                 

                                                <div class="col-md-12 d-flex align-items-center mt-3 justify-content-end">
                                                      <div class="text-right mr-2">
                                                <asp:Button ID="btnAccept" class="btn btn-success float-right" runat="server" OnClientClick="return validateSection()" Text="Register Case" />
                                                <%--
                                                   <button type="button" class="btn btn-success float-right" id="RegisteredCase">Registered Case</button>--%><%--
                                                   <button onclick="myFunction()">Try it</button>--%>
                                             </div>
                                                </div>
                                               </div>
                                           </div> 
                                          </div>
                                       </div>
                                    </div>
                                 </div>
                              </div>
                              <asp:HiddenField ID="hdnfldHead" ClientIDMode="Static" runat="server" />
                              <asp:HiddenField ID="hdnfldSection" ClientIDMode="Static" runat="server" />
                              <asp:HiddenField ID="hdnfldCase" ClientIDMode="Static" runat="server" />
                              <asp:HiddenField ID="hdnAppID" ClientIDMode="Static" runat="server" />
                              <div class="col-12 col-sm-6">
                                 <div class="card card-primary card-tabs h-100">
                                    <div class="card-header p-0 pt-1 d-flex align-items-center">
                                       <div class="col-sm-8 p-0">
                                          <ul class="nav nav-tabs" id="custom-tabs-two-tab" role="tablist">
                                             <li class="nav-item">
                                                <a class="nav-link active" id="custom-tabs-two-home-tab" data-toggle="pill" href="#custom-tabs-two-home" role="tab" aria-controls="custom-tabs-two-home" aria-selected="true">Document </a>
                                             </li>
                                             <!-- <li class="nav-item"><a class="nav-link" id="custom-tabs-two-profile-tab" data-toggle="pill"
                                                href="#custom-tabs-two-profile" role="tab" aria-controls="custom-tabs-two-profile"
                                                aria-selected="false">Profile</a></li> -->
                                          </ul>
                                       </div>
                                       <div class="col-sm-4">
                                          <div class="align-items-end d-flex justify-content-end mr-3 w-100">
                                             <a href="#" id="btnShowDownloadOption" onclick="ShowDownloadOption()">
                                             <i class="fa fa-download mr-3 "></i>
                                             </a>
                                             <a href="#" onclick="printPDF()">
                                             <i class="fa fa-print"></i>
                                             </a>
                                          </div>
                                       </div>
                                    </div>
                                    <%--    
                                       <div class="text-right newbtndp">
                                           <button type="button"  class="btn btn-secondary btn-sm" >Download </button>
                                       &nbsp;
                                       
                                           <button type="button" class="btn btn-info btn-sm float-right" >Print </button>
                                       &nbsp;
                                       
                                       </div>--%> 
                                    <div class="card-body">
                                       <div class="tab-content" id="custom-tabs-two-tabContent">
                                          <div class="tab-pane fade show active" id="custom-tabs-two-home" role="tabpanel" aria-labelledby="custom-tabs-two-home-tab">
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
                                                         <li class="nav-item"><%--
                                                            <a class="nav-link" id="custom-tabs-four-messages-tab" data-toggle="pill" href="#custom-tabs-four-messages" role="tab" aria-controls="custom-tabs-four-messages" aria-selected="false">All</a>--%> <a class="nav-link" id="custom-tabs-four-messages-tab" data-toggle="pill" href="#custom-tabs-four-profile" role="tab" aria-controls="custom-tabs-four-messages" aria-selected="false">All</a>
                                                         </li>
                                                         <li class="nav-item">
                                                            <a class="nav-link" id="custom-tabs-four-settings-tab" style="pointer-events: none;" data-toggle="pill" href="#custom-tabs-four-settings" role="tab" aria-controls="custom-tabs-four-settings" aria-selected="true">Previous Proceeding</a>
                                                         </li>
                                                      </ul>
                                                   </div>
                                                   <div class="card-body">
                                                      <div class="tab-content" id="custom-tabs-four-tabContent">
                                                         <div class="tab-pane fade active show" id="custom-tabs-four-home" role="tabpanel" aria-labelledby="custom-tabs-four-home-tab">
                                                         
                                                            <h5 style="text-align: center">List of Documents </h5>
                                                            
                                                           <table class="table table-striped table-bordered table-all-common">
                                                             <thead>
                                                                   <tr>
                                                                   <th>S.no</th>
                                                                   <th>Document Name</th>
                                                                   <th>Uploaded by</th>
                                                                   <th>Attached on</th>
                                                                   <th>View</th>
                                                               </tr>
                                                             </thead>

                                                             <tbody>
                                                                 <tr>
                                                                     <td>1</td>
                                                                     <td>Registry Copy</td>
                                                                     <td>Sampada 1.0</td>
                                                                     <td>05-06-2024</td>
                                                                     <td>
                                                                         <button class="btn btn-secondary"><i class="fa fa-user"></i></button>
                                                                     </td>
                                                                 </tr>

                                                                  <tr>
                                                                     <td>2</td>
                                                                     <td>Order Sheet</td>
                                                                     <td>Reader- Aman</td>
                                                                     <td>05-06-2024</td>
                                                                     <td>
                                                                         <button class="btn btn-secondary"><i class="fa fa-user"></i></button>
                                                                     </td>
                                                                 </tr>

                                                                 <%-- 3 --%>

                                                                  <tr>
                                                                     <td>3</td>
                                                                     <td>Proposal of Sub Registrar</td>
                                                                     <td>Reader- Aman</td>
                                                                     <td>05-06-2024</td>
                                                                     <td>
                                                                         <button class="btn btn-secondary"><i class="fa fa-user"></i></button>
                                                                     </td>
                                                                 </tr>

                                                                 <%-- 4 --%>
                                                                  <tr>
                                                                     <td>4</td>
                                                                     <td>Notice</td>
                                                                     <td>Reader- Aman</td>
                                                                     <td>05-06-2024</td>
                                                                     <td>
                                                                         <button class="btn btn-secondary"><i class="fa fa-user"></i></button>
                                                                     </td>
                                                                 </tr>

                                                                   <tr>
                                                                     <td>5</td>
                                                                     <td>Registered Document -Certified Copy</td>
                                                                     <td>Reader- Aman</td>
                                                                     <td>05-06-2024</td>
                                                                     <td>
                                                                         <button class="btn btn-secondary"><i class="fa fa-user"></i></button>
                                                                     </td>
                                                                 </tr>

                                                                   <tr>
                                                                     <td>6</td>
                                                                     <td>Document on the bases Case Registered</td>
                                                                     <td>Reader- Aman</td>
                                                                     <td>05-06-2024</td>
                                                                     <td>
                                                                         <button class="btn btn-secondary"><i class="fa fa-user"></i></button>
                                                                     </td>
                                                                 </tr>

                                                                 <%-- 7 --%>

                                                                  <tr>
                                                                     <td>7</td>
                                                                     <td>Reply & other relevant document</td>
                                                                     <td>Reader- Aman</td>
                                                                     <td>05-06-2024</td>
                                                                     <td>
                                                                         <button class="btn btn-secondary"><i class="fa fa-user"></i></button>
                                                                     </td>
                                                                 </tr>
                                                             </tbody>
                                                           </table>
                                                         </div>
                                                         <div class="tab-pane fade" id="custom-tabs-four-profile" role="tabpanel" aria-labelledby="custom-tabs-four-profile-tab">
                                                            tab second
                                                         </div>
                                                         <div class="tab-pane fade" id="custom-tabs-four-messages" role="tabpanel" aria-labelledby="custom-tabs-four-messages-tab">
                                                            tab three
                                                         </div>
                                                         <div class="tab-pane fade " id="custom-tabs-four-settings" role="tabpanel" aria-labelledby="custom-tabs-four-settings-tab">
                                                            <div>
                                                               <img src="../dist/img/Doc_Sample_pic.jpg" style="width: 708px;" />
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
