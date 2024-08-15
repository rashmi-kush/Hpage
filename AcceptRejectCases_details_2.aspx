<%@ Page Title="" Language="C#" MasterPageFile="~/CoS/CoSMaster.Master" AutoEventWireup="true" CodeBehind="AcceptRejectCases_details_2.aspx.cs" Inherits="CMS_Sampada.CoS.AcceptRejectCases_details_2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <section class="content-header">
        <div class="container-fluid">
            <div class="row">
                <div class="col-sm-6">
                    <h5>Proposal No - IGRSCMS1000108 </h5>
                </div>
                <div class="col-sm-6">
                    <ol class="breadcrumb float-sm-right">
                        <li class="breadcrumb-item"><a href="#">Home</a></li>
                        <li class="breadcrumb-item active">proposal List</li>
                    </ol>
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
                                <li class="nav-item"><a class="nav-link" href="#CreateOrderSheet" data-toggle="tab">Create Order Sheet</a></li>
                                <li class="nav-item"><a class="nav-link disabled" href="#Notice" data-toggle="tab">Notice</a></li>
                                <li class="nav-item"><a class="nav-link disabled" href="#SeekReport" data-toggle="tab">Seek
                        Report</a></li>
                                <li class="nav-item"><a class="nav-link disabled" href="#EarlyHearing" data-toggle="tab">Early
                        Hearing</a>
                                </li>
                                <li class="nav-item"><a class="nav-link disabled" href="#SendToAppeal" data-toggle="tab">Send to
                        Appeal</a>
                                </li>
                                <li class="nav-item"><a class="nav-link active" href="#SendBack" data-toggle="tab">Send Back</a>
                                </li>
                                <li class="nav-item"><a class="nav-link disabled" href="#Reply" data-toggle="tab">Reply</a></li>
                                <li class="nav-item"><a class="nav-link disabled" href="#Attachement"
                                    data-toggle="tab">Attachement</a></li>
                            </ul>
                        </div>


                        <div class="card-body">
                            <div class="tab-content">

                                <div class="tab-pane" id="CreateOrderSheet">
                                    <div class="row">

                                        <div class="col-12 col-sm-6">
                                            <div class="card card-primary card-tabs">
                                                <div class="card-header p-0 pt-1">
                                                    <ul class="nav nav-tabs" id="custom-tabs-one-tab" role="tablist">
                                                        <li class="nav-item">
                                                            <a class="nav-link active" id="custom-tabs-one-home-tab" data-toggle="pill"
                                                                href="#custom-tabs-one-ProposalForm" role="tab" aria-controls="custom-tabs-one-home"
                                                                aria-selected="false">Create Order Sheet</a>
                                                        </li>
                                                        <li class="nav-item">
                                                            <a class="nav-link" id="custom-tabs-one-profile-tab" data-toggle="pill"
                                                                href="#custom-tabs-one-RegisteredForm" role="tab"
                                                                aria-controls="custom-tabs-one-profile" aria-selected="true">Edit Proceeding </a>
                                                        </li>
                                                    </ul>
                                                </div>

                                                <div class="card-body">
                                                    <div class="tab-content" id="custom-tabs-one-tabContent">
                                                        <div class="tab-pane active show" id="custom-tabs-one-ProposalForm" role="tabpanel"
                                                            aria-labelledby="custom-tabs-one-home-tab">
                                                            <!-- code here -->
                                                            <table id="example11" class="table table-bordered table-striped">
                                                                <thead>
                                                                    <tr>
                                                                        <th>SNo.</th>
                                                                        <th>Party Type </th>
                                                                        <th>Party Name </th>
                                                                        <th>Owner Applicant Type</th>
                                                                    </tr>
                                                                </thead>
                                                                <tbody>
                                                                    <tr>
                                                                        <td>1</td>
                                                                        <td>Seller </td>
                                                                        <td>Vaishali Rathore</td>
                                                                        <td>Individual</td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>2</td>
                                                                        <td>Seller </td>
                                                                        <td>Mohan Gupta</td>
                                                                        <td>Organisation</td>
                                                                    </tr>
                                                                </tbody>
                                                            </table>

                                                            <div>
                                                                <!-- Doc detail html -->
                                                                <div class="main-box html_box">
                                                                    <h2 class="h-2"> न्यायालय कलेक्टर ऑफ़ स्टाम्प्स, भोपाल (म.प्र.)</h2>
                                                                    <h3 class="h-3"> प्ररूप-अ</h3>
                                                                    <h2 class="h-22"> (परिपत्र दो-1 की कंडिका 1)</h2>
                                                                    <h3 class="h-3"> सेक्शन 40(D) - भारतीय स्टाम्प अधीनियम 1899 की धारा 40 (1)(ख) के अंतर्गत एवं 40 (1)(घ) के पालन में</h3>
                                                                    <h2 class="h-22"> आदेश पत्रक   </h2>
                                                                    <p>&nbsp;</p>

                                                                    <div class="devidetwo">
                                                                        <div>
                                                                            <p class="p1">
                                                                                कलेक्टर ऑफ़ स्टाम्प्स का नाम :
                                                                            </p>
                                                                        </div>
                                                                        <div>
                                                                            <p class="p2">
                                                                                <b>भोपाल जोन नंबर 1  </b>
                                                                            </p>
                                                                        </div>
                                                                    </div>

                                                                    <div class="devidetwo">
                                                                        <div>
                                                                            <p class="p3">
                                                                                प्रकरण  क्रमांक :
                                                                            </p>
                                                                         </div>
                                                                        <div>
                                                                            <p class="p2">
                                                                                <b>000001/B103/32/2022-23  </b>
                                                                            </p>
                                                                        </div>
                                                                    </div>

                                                                    <div class="devidetwo">
                                                                        <div>
                                                                            <p class="p3">
                                                                                विषय :
                                                                            </p>
                                                                        </div>
                                                                        <div>
                                                                            <p class="p2">
                                                                                <b>बाज़ार मूल्य अवधारण / मुद्रांक शुल्क निर्धारण  </b>
                                                                            </p>
                                                                        </div>
                                                                    </div>

                                                                    <p class="p4">
                                                                        <b>पक्षकारों के नाम </b>
                                                                    </p>

                                                                    <div>
                                                                        <div style="float: left;">
                                                                            <p style="font-size: 18px; line-height: 30px; text-align: center; margin: 0; margin-right: 63px">
                                                                                <b>आवेदक :   </b>
                                                                            </p>
                                                                        </div>
                                                                        <table style="width: 100%; border: 1px solid black; border-collapse: collapse; text-align: left;">
                                                                            <tr>

                                                                                <th style="border: 1px solid black; border-collapse: collapse; line-height: 25px; height: 40px; padding: 25px;">क्रमांक    </th>
                                                                                <th style="border: 1px solid black; border-collapse: collapse; line-height: 25px; padding: 0 50px 0 50px; height: 40px; padding: 25px;">नाम </th>
                                                                                <th style="border: 1px solid black; border-collapse: collapse; line-height: 25px; height: 40px; padding: 25px;">पता </th>
                                                                                <th style="border: 1px solid black; border-collapse: collapse; line-height: 25px; height: 40px; padding: 25px;">पिता / पति </th>

                                                                            </tr>

                                                                            <tbody>
                                                                                <tr>
                                                                                    <td style="border: 1px solid black; border-collapse: collapse; height: 40px; padding: 25px;">1.
                                                                                    </td>
                                                                                    <td style="border: 1px solid black; border-collapse: collapse; height: 40px; padding: 25px;">म प्र शासन
                                                                                        <br />
                                                                                    </td>
                                                                                    <td style="border: 1px solid black; border-collapse: collapse; height: 40px; padding: 25px;">संभाग कार्यालय जवाहर नगर रतलाम
                                                                                    </td>

                                                                                    <td style="border: 1px solid black; border-collapse: collapse; height: 40px; padding: 25px;"></td>



                                                                                </tr>

                                                                            </tbody>
                                                                        </table>
                                                                    </div>

                                                                    <div style="margin-top: 50px">
                                                                        <p style="font-size: 18px; line-height: 30px; text-align: center; margin: 0; margin-left: 50px; margin-top: 50px; margin-bottom: 25px;">
                                                                            <b>विरुद्ध   </b>
                                                                        </p>
                                                                        <div style="float: left;">
                                                                            <p style="font-size: 18px; line-height: 30px; text-align: center; margin: 0; margin-right: 50px;">
                                                                                <b>अनावेदक :   </b>
                                                                            </p>
                                                                        </div>
                                                                        <table style="width: 100%; border: 1px solid black; border-collapse: collapse; text-align: left;">
                                                                            <tr>

                                                                                <th style="border: 1px solid black; border-collapse: collapse; line-height: 25px; height: 40px; padding: 25px;">क्रमांक    </th>
                                                                                <th style="border: 1px solid black; border-collapse: collapse; line-height: 25px; padding: 0 50px 0 50px; height: 40px; padding: 25px;">नाम </th>
                                                                                <th style="border: 1px solid black; border-collapse: collapse; line-height: 25px; height: 40px; padding: 25px;">पता </th>
                                                                                <th style="border: 1px solid black; border-collapse: collapse; line-height: 25px; height: 40px; padding: 25px;">पिता / पति </th>

                                                                            </tr>

                                                                            <tbody>
                                                                                <tr>
                                                                                    <td style="border: 1px solid black; border-collapse: collapse; height: 40px; padding: 25px;">1.
                                                                                    </td>
                                                                                    <td style="border: 1px solid black; border-collapse: collapse; height: 40px; padding: 25px;">आकाश शर्मा
                                                                                    </td>
                                                                                    <td style="border: 1px solid black; border-collapse: collapse; height: 40px; padding: 25px;"></td>

                                                                                    <td style="border: 1px solid black; border-collapse: collapse; height: 40px; padding: 25px;"></td>



                                                                                </tr>
                                                                                <tr>
                                                                                    <td style="border: 1px solid black; border-collapse: collapse; height: 40px; padding: 25px;">1.
                                                                                    </td>
                                                                                    <td style="border: 1px solid black; border-collapse: collapse; height: 40px; padding: 25px;">अमन वर्मा
                                                                                    </td>
                                                                                    <td style="border: 1px solid black; border-collapse: collapse; height: 40px; padding: 25px;"></td>

                                                                                    <td style="border: 1px solid black; border-collapse: collapse; height: 40px; padding: 25px;"></td>



                                                                                </tr>
                                                                                <tr>
                                                                                    <td style="border: 1px solid black; border-collapse: collapse; height: 40px; padding: 25px;">1.
                                                                                    </td>
                                                                                    <td style="border: 1px solid black; border-collapse: collapse; height: 40px; padding: 25px;">अंजलि
                                                                                    </td>
                                                                                    <td style="border: 1px solid black; border-collapse: collapse; height: 40px; padding: 25px;"></td>

                                                                                    <td style="border: 1px solid black; border-collapse: collapse; height: 40px; padding: 25px;"></td>
                                                                                </tr>
                                                                            </tbody>
                                                                        </table>

                                                                        <div class="last-sec" style="display: flex; justify-content: start; margin-top: 50px;">
                                                                            <p style="font-size: 18px; line-height: 30px; text-align: left; margin: 0">सब रजिस्ट्रार कार्यालय  :</p>
                                                                            <span style="margin-left: 100px;"><b>हिंगोरिया </b></span>
                                                                        </div>

                                                                        <div class="last-sec" style="display: flex; justify-content: start;">
                                                                            <p style="font-size: 18px; line-height: 30px; text-align: left; margin: 0;">दस्तावेज़ पंजीयन क्रमांक / प्रारंभ आई डी  : </p>
                                                                            <span style="margin-left: 100px;"><b>IGRSCMS1000101 </b></span>
                                                                        </div>

                                                                        <div class="last-sec" style="display: flex; justify-content: start;">
                                                                            <p style="font-size: 18px; line-height: 30px; text-align: left; margin: 0;">जिला : </p>
                                                                            <span style="margin-left: 100px;"><b>नीमच नगर </b></span>
                                                                        </div>

                                                                        <div class="last-sec" style="display: flex; justify-content: start;">
                                                                            <p style="font-size: 18px; line-height: 30px; text-align: left; margin: 0;">पटवारी हल्का क्रमांक : </p>
                                                                            <span style="margin-left: 100px;"><b>पिपल्याबाग़ </b></span>
                                                                        </div>


                                                                        <div class="last-sec" style="display: flex; justify-content: start;">
                                                                            <p style="text-align: left; font-size: 18px; line-height: 30px; margin: 0;">पहली सुनवाई की तारीख  : </p>
                                                                            <span style="margin-left: 100px;">---- </span>
                                                                        </div>

                                                                        <div class="last-sec" style="display: flex; justify-content: start;">
                                                                            <p style="text-align: left; font-size: 18px; line-height: 30px; margin: 0;">प्रकरण का स्रोत  : </p>
                                                                            <span style="margin-left: 100px;">---- </span>
                                                                            <!--<p style="text-align: left;font-size: 18px;line-height: 30px;    width: 50%;">सन : </p>-->
                                                                        </div>
                                                                    </div>





                                                                    <p style="font-size: 18px; line-height: 30px; text-align: center; margin: 0; margin-left: 50px; margin-top: 70px; margin-bottom: 25px;">
                                                                        <b>कागजो की सूची   </b>
                                                                    </p>
                                                                    <table style="width: 100%; border: 1px solid black; border-collapse: collapse; text-align: center;">
                                                                        <tr>
                                                                            <th style="border: 1px solid black; border-collapse: collapse; line-height: 25px; height: 40px; padding: 25px;">पृष्ठों की संख्या     </th>
                                                                            <th style="border: 1px solid black; border-collapse: collapse; line-height: 25px; padding: 0 50px 0 50px; height: 40px; padding: 25px;">दस्तावेजों आदि का विवरण  </th>
                                                                            <!--<th style="border: 1px solid black;
            border-collapse: collapse;line-height: 25px;    height: 40px;padding: 25px;">प्रत्येक कागज के  हटाए जाने की तारीख  </th>
                          <th style="border: 1px solid black;
            border-collapse: collapse;line-height: 25px;    height: 40px;padding: 25px;">किस प्रकार हटाया गया  </th>
                          <th style="border: 1px solid black;
            border-collapse: collapse;line-height: 25px;    height: 40px;padding: 25px;">हटाने वाले अधिकारी के हस्ताक्षार  </th>-->

                                                                        </tr>
                                                                        <tr>
                                                                            <th style="border: 1px solid black; border-collapse: collapse; line-height: 25px; height: 40px;">1    </th>
                                                                            <th style="border: 1px solid black; border-collapse: collapse; line-height: 25px; padding: 0 50px 0 50px; height: 40px;">2 </th>
                                                                            <!--<th style="border: 1px solid black;
            border-collapse: collapse;line-height: 25px;    height: 40px;">3 </th>
                          <th style="border: 1px solid black;
            border-collapse: collapse;line-height: 25px;    height: 40px;">4 </th>
                          <th style="border: 1px solid black;
            border-collapse: collapse;line-height: 25px;    height: 40px;">5</th>-->

                                                                        </tr>
                                                                    </table>

                                                                    <br>
                                                                    <br>
                                                                    <hr>
                                                                    <br>
                                                                    <br>

                                                                    <%-- <div style="width: 100%; margin: 0 auto; text-align: center; padding: 25px; margin-top: 50px;">--%>
                                                                    <h2>न्यायालय कलेक्टर ऑफ़  स्टाम्प्स, भोपाल (म.प्र.)</h2>
                                                                    <p>प्ररूप-अ</p>
                                                                    <p>(परिपत्र दो-1 की कंडिका 1)</p>
                                                                    <h2>राजस्व आदेशपत्र</h2>
                                                                    <h3>कलेक्टर ऑफ़ स्टाम्प, भोपाल के न्यायालय में मामला क्रमांक-   /ब-103/22-23/धारा-33 </h3>

                                                                    <table style="width: 100%; border: 1px solid black; border-collapse: collapse;">
                                                                        <tr>
                                                                            <th style="border: 1px solid black; border-collapse: collapse;">आदेश क्रमांक कार्यवाही की तारीख एवं स्थान   </th>
                                                                            <th style="border: 1px solid black; border-collapse: collapse;">पीठासीन अधिकारी के हस्ताक्षर सहित आदेश पत्र अथवा कार्यवाही
                                                                                    <br />
                                                                                मध्यप्रदेष शासन विरूद्ध</th>
                                                                            <th style="border: 1px solid black; border-collapse: collapse;">पक्षों/वकीलों आदेश पालक लिपिक के हस्ताक्षर</th>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="border: 1px solid black; border-collapse: collapse;">
                                                                                <div class="content" style="padding: 15px">
                                                                                    <input type="date" id="" name="">
                                                                                </div>
                                                                            </td>

                                                                            <td style="border: 1px solid black; border-collapse: collapse;">
                                                                                <div class="content" style="padding: 15px">
                                                                                    <p style="text-align: justify; line-height: 1.6;">
                                                                                        ----------------Case Number : (xxxxxxx)---------------- 
                                                                                            <br />
                                                                                        उप पंजीयक भोपाल-2  द्वारा एक  पंजीकृत दस्तावेज दान पत्र  विलेख क्रमांक डच्059702023।11320013 दिनांक 03.01.2023
                                                                                            <!-- <input type="date" id="" name=""> -->
                                                                                        को न्यून मुद्रांकित मानते हुए उक्त दस्तावेज पर मुद्रांक एवं पंजीयन शुल्क वसूली हेतु भेजा गया है। उप पंजीयक द्वारा दस्तावेज की मूल प्रति प्रेषित की गई है जिसे भारतीय स्टाम्प अधिनियम, 1899 की धारा-33 के अंतर्गत दर्ज किया गया। 
                                                                                    </p>

                                                                                    <p style="text-align: justify; line-height: 1.6;">
                                                                                        प्रकरण शीर्ष ब-103 एवं भारतीय स्टाम्प अधिनियम की धारा-33 के अधीन प्रकरण दर्ज कर अनावेदकों को सूचना पत्र जारी हो।
                                                                                       पेशी दिनांक-
                                                                                        <input type="date" id="" name="">
                                                                                    </p>

                                                                                    <b style="float: right; text-align: center; padding: 30px 0 5px 0;">कलेक्टर ऑफ़ स्टाम्प्स,<br />
                                                                                        भोपाल
                                                                                            <br />
                                                                                        <br />
                                                                                    </b>
                                                                                </div>
                                                                            </td>

                                                                            <td style="border: 1px solid black; border-collapse: collapse;"></td>
                                                                        </tr>
                                                                    </table>

                                                                    <%-- </div>--%>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="tab-pane fade" id="custom-tabs-one-RegisteredForm" role="tabpanel"
                                                            aria-labelledby="custom-tabs-one-profile-tab">
                                                            <!-- table -->
                                                            <textarea id="summernote">
                                                                उप पंजीयन भोपाल -२ द्वारा एक पंजीकृत दस्तावेज दान पत्र विलेख क्रमांक 3 दिनांक 9 -5 -2023 को न्यून मुद्रांकित मानते हुए उक्त दस्तावेज पर मुद्रांक एवं पंजीयन शुल्क वसूली हेतु भेजा गया हैं।  उप पंजीयन द्वारा दस्तावेज की मूल प्रति प्रेषित की गयी है जिसे भारतीय स्टाम्प अधिनियम , 1899 की धरा -33 के अंतर्गत दर्ज किया गया।
                                                                प्रकरण शीर्ष व -103 एवं भारतीय स्टाम्प अधिनियम की धरा -33 के अधीन प्रकरण दर्ज कर अनावेदकों को सुचना पत्र जारी हो।  पेशी दिनांक -16 -05 -2023 
                                                             </textarea>
                                                            <button type="button" class="btn btn-success">Save </button>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="col-12 col-sm-6">
                                            <div class="card card-primary card-tabs">
                                                <div class="card-header p-0 pt-1">
                                                    <ul class="nav nav-tabs" id="custom-tabs-two-tab" role="tablist">
                                                        <li class="nav-item">
                                                            <a class="nav-link disabled" id="custom-tabs-two-home-tab" data-toggle="pill"
                                                                href="#custom-tabs-two-home" role="tab" aria-controls="custom-tabs-two-home"
                                                                aria-selected="true">Document </a>
                                                        </li>
                                                        <li class="nav-item">
                                                            <a class="nav-link active" id="custom-tabs-two-profile-tab" data-toggle="pill"
                                                                href="#custom-tabs-two-profile" role="tab" aria-controls="custom-tabs-two-profile"
                                                                aria-selected="false">Proposal Form</a>
                                                        </li>
                                                    </ul>
                                                </div>

                                                <%--button--%>
                                                <div class="text-right">
                                                    <button type="button" class="btn btn-success">Download </button>
                                                    &nbsp;
                                 <button type="button" class="btn btn-info float-right">Print </button>
                                                    &nbsp;
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
                                                                                <a class="nav-link" id="custom-tabs-four-home-tab" data-toggle="pill" href="#custom-tabs-four-home" role="tab" aria-controls="custom-tabs-four-home" aria-selected="false">TOC</a>
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
                                                                    <div class="card-body d_mycard">
                                                                        <div class="tab-content" id="custom-tabs-four-tabContent">
                                                                            <div class="tab-pane fade" id="custom-tabs-four-home" role="tabpanel" aria-labelledby="custom-tabs-four-home-tab">
                                                                            </div>
                                                                            <div class="tab-pane fade active show" id="custom-tabs-four-profile" role="tabpanel" aria-labelledby="custom-tabs-four-profile-tab">

                                                                                <div class="col-md-12">
                                                                                    <div class="card card-primary card-outline">
                                                                                        <div class="card-header">
                                                                                            <h3 class="card-title">Proposal Sheet</h3>
                                                                                        </div>
                                                                                        <div class="card-body">

                                                                                            <ifram>fghdfhdf</ifram>

                                                                                        </div>

                                                                                    </div>
                                                                                </div>



                                                                            </div>

                                                                            <div class="tab-pane fade" id="custom-tabs-four-messages" role="tabpanel" aria-labelledby="custom-tabs-four-messages-tab">

                                                                                <%--  <div>
                                                                                    <img src="../dist/img/Doc_Sample_pic.jpg" style="width: 708px;" />
                                                                                </div>--%>
                                                                            </div>
                                                                            <div class="tab-pane fade " id="custom-tabs-four-settings" role="tabpanel" aria-labelledby="custom-tabs-four-settings-tab">

                                                                                <%--  <div>
                                                                                    <img src="../dist/img/Doc_Sample_pic.jpg" style="width: 708px;" />
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


                                <%------------------------------------------------------------------------------------------%>

                                <div class="tab-pane active" id="SendBack">
                                    <div class="row">

                                        <div class="col-12 col-sm-6">
                                            <div class="card card-primary card-tabs">
                                                <div class="card-header p-0 pt-1">
                                                    <ul class="nav nav-tabs" id="custom-tabs-one-tab" role="tablist">
                                                        <%--                              <li class="nav-item">
                                  <a class="nav-link disabled" id="custom-tabs-one-home-tab" data-toggle="pill"
                                    href="#custom-tabs-one-ProposalForm" role="tab" aria-controls="custom-tabs-one-home"
                                    aria-selected="false">Proposal Form</a>
                                </li>--%>
                                                        <li class="nav-item">
                                                            <a class="nav-link active" id="custom-tabs-one-profile-tab" data-toggle="pill"
                                                                href="#custom-tabs-one-RegisteredForm" role="tab"
                                                                aria-controls="custom-tabs-one-profile" aria-selected="true">Proposal Form </a>
                                                        </li>
                                                    </ul>
                                                </div>

                                                <div class="card-body">
                                                    <div class="tab-content" id="custom-tabs-one-tabContent">
                                                        <%--    <div class="tab-pane fade" id="custom-tabs-one-ProposalForm" role="tabpanel"
                                  aria-labelledby="custom-tabs-one-home-tab">
                                  <!-- code here -->
                                </div>--%>
                                                        <div class="tab-pane active show" id="custom-tabs-one-RegisteredForm" role="tabpanel"
                                                            aria-labelledby="custom-tabs-one-profile-tab">
                                                            <!-- table -->

                                                            <div class="col-md-12">
                                                                <div class="card card-primary">
                                                                    <div class="card-header">
                                                                        <h3 class="card-title">Party Details </h3>
                                                                        <div class="card-tools">
                                                                            <button type="button" class="btn btn-tool" data-card-widget="collapse">
                                                                                <i class="fas fa-minus"></i>
                                                                            </button>
                                                                        </div>
                                                                    </div>
                                                                    <div class="card-body">
                                                                        <table id="example11" class="table table-bordered table-striped">
                                                                            <thead>
                                                                                <tr>
                                                                                    <th>SNo.</th>
                                                                                    <th>Party Type</th>
                                                                                    <th>Party Name</th>
                                                                                    <th>Gender</th>
                                                                                    <th>Owner/ Applicant type</th>
                                                                                    <th>Email ID</th>
                                                                                    <th>Mobile No</th>
                                                                                    <th>Action</th>
                                                                                </tr>
                                                                            </thead>
                                                                            <tbody>
                                                                                <tr>
                                                                                    <td>1</td>
                                                                                    <td>454544 </td>
                                                                                    <td>gdf</td>
                                                                                    <td>kjhjh</td>
                                                                                    <td>xyz </td>
                                                                                    <td>454544 </td>
                                                                                    <td>dfsdf</td>
                                                                                    <td>
                                                                                        <button type="button" class="btn t-btn" id="viewbtn">View</button>
                                                                                    </td>
                                                                                </tr>
                                                                            </tbody>
                                                                        </table>

                                                                        <br>

                                                                        <div class="col-md-6 col-lg-12">
                                                                            <div class="card card-outline card-success" id="detail_card">
                                                                                <div class="card-tools">
                                                                                    <button type="button" class="btn btn-tool" data-card-widget="remove">
                                                                                        <i
                                                                                            class="fas fa-times"></i>
                                                                                    </button>
                                                                                </div>
                                                                                <div class="card-body">
                                                                                    <div class="row">
                                                                                        <div class="col-sm-4 border-right">
                                                                                            <div class="description-block">
                                                                                                <h5 class="description-header">Transaction through PAO</h5>
                                                                                                <span class="description-text">Power of Attorney Holder </span>
                                                                                            </div>
                                                                                        </div>
                                                                                        <div class="col-sm-4 border-right">
                                                                                            <div class="description-block">
                                                                                                <h5 class="description-header">Father Name </h5>
                                                                                                <span class="description-text">Aman Rathore</span>
                                                                                            </div>
                                                                                        </div>
                                                                                        <div class="col-sm-4">
                                                                                            <div class="description-block">
                                                                                                <h5 class="description-header">Whatsapp No</h5>
                                                                                                <span class="description-text">8985476254</span>
                                                                                            </div>
                                                                                        </div>
                                                                                    </div>
                                                                                    <hr>
                                                                                    <div class="row">
                                                                                        <div class="col-sm-4 border-right">
                                                                                            <div class="description-block">
                                                                                                <h5 class="description-header">Address</h5>
                                                                                                <span class="description-text">Arera volony</span>
                                                                                            </div>
                                                                                        </div>
                                                                                        <div class="col-sm-4 border-right">
                                                                                            <div class="description-block">
                                                                                                <h5 class="description-header">District </h5>
                                                                                                <span class="description-text">Bhopal</span>
                                                                                            </div>
                                                                                        </div>
                                                                                        <div class="col-sm-4">
                                                                                            <div class="description-block">
                                                                                                <h5 class="description-header">Owner Name </h5>
                                                                                                <span class="description-text">Power of Attorney Holder </span>
                                                                                            </div>
                                                                                        </div>
                                                                                    </div>
                                                                                    <hr>
                                                                                    <div class="row">
                                                                                        <div class="col-sm-4 border-right">
                                                                                            <div class="description-block">
                                                                                                <h5 class="description-header">Owner Mobile no</h5>
                                                                                                <span class="description-text">9865231457</span>
                                                                                            </div>
                                                                                        </div>
                                                                                        <div class="col-sm-4 border-right">
                                                                                            <div class="description-block">
                                                                                                <h5 class="description-header">Owner whatsapp no</h5>
                                                                                                <span class="description-text">9658714258</span>
                                                                                            </div>
                                                                                        </div>
                                                                                        <div class="col-sm-4">
                                                                                            <div class="description-block">
                                                                                                <h5 class="description-header">Owner Email ID</h5>
                                                                                                <span class="description-text">xyz@gmail.com</span>
                                                                                            </div>
                                                                                        </div>
                                                                                    </div>

                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                        <hr>
                                                                    </div>
                                                                </div>
                                                            </div>


                                                            <div class="col-md-12">
                                                                <div class="card card-primary collapsed-card">
                                                                    <div class="card-header">
                                                                        <h3 class="card-title">Basis of Impound /Referral</h3>

                                                                        <div class="card-tools">
                                                                            <button type="button" class="btn btn-tool" data-card-widget="collapse">
                                                                                <i class="fas fa-plus"></i>
                                                                            </button>
                                                                        </div>
                                                                    </div>
                                                                    <div class="card-body">
                                                                        <div class="row">
                                                                            <div class="col-12">
                                                                                <div class="card">
                                                                                    <div class="card-body">
                                                                                        Unduly Stamped
                                                                                    </div>
                                                                                </div>
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
                                                                            <table id="example11" class="table table-bordered table-striped">
                                                                                <thead>
                                                                                    <tr>
                                                                                        <th>SNo.</th>
                                                                                        <th>Property Id</th>
                                                                                        <th>Transacting Partial Area of Proporty</th>
                                                                                        <th>Pre Registration Id</th>
                                                                                        <th>Property Type</th>
                                                                                        <th>Property Sub Type</th>
                                                                                        <th>Total Property Area</th>
                                                                                        <th>Total Property Transacting Area</th>
                                                                                        <th>Land Diversion Details</th>
                                                                                        <th>Action</th>
                                                                                    </tr>
                                                                                </thead>
                                                                                <tbody>
                                                                                    <tr>
                                                                                        <td>1</td>
                                                                                        <td>XXXXX12122 </td>
                                                                                        <td>Yes</td>
                                                                                        <td>XXXX22323</td>
                                                                                        <td>Plot </td>
                                                                                        <td>Residential </td>
                                                                                        <td>1250 SQM</td>
                                                                                        <td>625 SQM</td>
                                                                                        <td>XYZ Details</td>
                                                                                        <td>
                                                                                            <button type="button" class="btn t-btn">View</button>
                                                                                        </td>
                                                                                    </tr>
                                                                                </tbody>
                                                                            </table>
                                                                        </div>
                                                                        <br>

                                                                        <div class="row">
                                                                            <div class="col-12">
                                                                                <div class="card">
                                                                                    <div class="card-body">
                                                                                        <form>
                                                                                            <div class="col-12" style="display: flex;">
                                                                                                <div class="col-4 form-group">
                                                                                                    <label for="inputEstimatedBudget">Property Address</label>
                                                                                                    <input type="number" id="inputEstimatedBudget" class="form-control">
                                                                                                </div>
                                                                                                <div class="col-4 form-group">
                                                                                                    <label for="inputEstimatedBudget">Land Diversion</label>
                                                                                                    <input type="number" id="inputEstimatedBudget" class="form-control">
                                                                                                </div>
                                                                                                <div class="col-4 form-group">
                                                                                                    <label for="inputEstimatedBudget">Property Type</label>
                                                                                                    <input type="number" id="inputEstimatedBudget" class="form-control">
                                                                                                </div>
                                                                                            </div>

                                                                                            <div class="col-12" style="display: flex;">
                                                                                                <div class="col-4 form-group">
                                                                                                    <label for="inputEstimatedBudget">Ward Colony/Village colony</label>
                                                                                                    <input type="number" id="inputEstimatedBudget" class="form-control">
                                                                                                </div>
                                                                                                <div class="col-4 form-group">
                                                                                                    <label for="inputEstimatedBudget">Property Image</label>
                                                                                                    <input type="number" id="inputEstimatedBudget" class="form-control">
                                                                                                </div>
                                                                                                <div class="col-4 form-group">
                                                                                                    <label for="inputEstimatedBudget">Map of Property</label>
                                                                                                    <input type="number" id="inputEstimatedBudget" class="form-control">
                                                                                                </div>
                                                                                            </div>

                                                                                        </form>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
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
                                                                            <table id="example11" class="table table-bordered table-striped">
                                                                                <thead>
                                                                                    <tr>
                                                                                        <th>Document Registration Number/Intitation ID</th>
                                                                                        <th>Date of Registry</th>
                                                                                        <th>Date of Execution</th>
                                                                                        <th>Date of Presentation</th>
                                                                                    </tr>
                                                                                </thead>
                                                                                <tbody>
                                                                                    <tr>
                                                                                        <td>12345678987654321788</td>
                                                                                        <td>13-12-2021 </td>
                                                                                        <td>10-01-2022 </td>
                                                                                        <td>13-02-22 </td>
                                                                                    </tr>
                                                                                </tbody>
                                                                            </table>
                                                                        </div>
                                                                        <br>

                                                                        <div class="table-responsive">
                                                                            <table id="example11" class="table table-bordered table-striped">
                                                                                <thead>
                                                                                    <tr>
                                                                                        <th colspan="2">Details of document as presented by party</th>
                                                                                        <th colspan="2">Proposal of registring officer</th>
                                                                                        <th>Deficit Duty / Variation Amount (INR)</th>
                                                                                        <th>Penalty Amount (INR)</th>
                                                                                        <th>Remark</th>
                                                                                    </tr>
                                                                                    <tr class="sub-tr-bc">
                                                                                        <th>Description</th>
                                                                                        <th>Amount (INR)</th>
                                                                                        <th>Description</th>
                                                                                        <th>Amount (INR)</th>
                                                                                        <th></th>
                                                                                        <th></th>
                                                                                        <th></th>
                                                                                    </tr>
                                                                                </thead>
                                                                                <tbody>
                                                                                    <tr>
                                                                                        <td>-</td>
                                                                                        <td>- </td>
                                                                                        <td>- </td>
                                                                                        <td>-</td>
                                                                                        <td>- </td>
                                                                                        <td>-</td>
                                                                                        <td>- </td>
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
                                                                            <table id="example11" class="table table-bordered table-striped">
                                                                                <thead>
                                                                                    <tr>
                                                                                        <th>SRO ID</th>
                                                                                        <th>Sub Registrar Name</th>
                                                                                        <th>SRO Name</th>
                                                                                        <th>Proposal ID</th>
                                                                                        <th>Date of Impound</th>
                                                                                        <th>Head</th>
                                                                                        <th>Section</th>
                                                                                    </tr>
                                                                                </thead>
                                                                                <tbody>
                                                                                    <tr>
                                                                                        <td>SRO101</td>
                                                                                        <td>Sub-Registrar </td>
                                                                                        <td>Sub-Registrar Office Bhopal Zone-I </td>
                                                                                        <td>IGRSCMS1000116</td>
                                                                                        <td>15/03/2022 </td>
                                                                                        <td>
                                                                                            <select class="form-control">
                                                                                                <option>B102</option>
                                                                                                <option>B104</option>
                                                                                            </select>
                                                                                        </td>
                                                                                        <td>
                                                                                            <select class="form-control">
                                                                                                <option>32</option>
                                                                                                <option>33</option>
                                                                                            </select>
                                                                                        </td>
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
                                                                        <h3 class="card-title">Comment</h3>

                                                                        <div class="card-tools">
                                                                            <button type="button" class="btn btn-tool" data-card-widget="collapse">
                                                                                <i class="fas fa-plus"></i>
                                                                            </button>
                                                                        </div>
                                                                    </div>
                                                                    <div class="card-body">
                                                                        <div class="row">
                                                                            <div class="col-12">
                                                                                <textarea id="inputDescription" class="form-control"
                                                                                    rows="3">Property ...................................</textarea>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>

                                                            <div class="text-right">
                                                                <button type="button" class="btn btn-success float-right" id="RegisteredCase">Registered Case</button>
                                                            </div>

                                                        </div>

                                                    </div>
                                                </div>
                                            </div>

                                        </div>



                                        <div class="col-12 col-sm-6">
                                            <div class="card card-primary card-tabs">
                                                <div class="card-header p-0 pt-1">
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

                                                <%--button--%>
                                                <div class="text-right">
                                                    <button type="button" class="btn btn-success">Download </button>
                                                    &nbsp;
                                 <button type="button" class="btn btn-info float-right">Print </button>
                                                    &nbsp;
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
                                                                                <a class="nav-link" id="custom-tabs-four-home-tab" data-toggle="pill" href="#custom-tabs-four-home" role="tab" aria-controls="custom-tabs-four-home" aria-selected="false">TOC</a>
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

                                                                                <h5>List of Documents </h5>
                                                                                <table id="example11" class="table table-bordered table-striped">
                                                                                    <thead>
                                                                                        <tr>
                                                                                            <th>SNo.</th>
                                                                                            <th>Document Name </th>
                                                                                            <th>Provided by</th>
                                                                                            <th>Uploaded by</th>
                                                                                            <th>Uploaded Date</th>
                                                                                        </tr>
                                                                                    </thead>
                                                                                    <tbody>
                                                                                        <tr>
                                                                                            <td>1</td>
                                                                                            <td><a href="#">Registry copy</a> </td>
                                                                                            <td>Party</td>
                                                                                            <td>Party</td>
                                                                                            <td>06-05-2023 </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td>2</td>
                                                                                            <td><a href="#">XYZ </a></td>
                                                                                            <td>SR</td>
                                                                                            <td>SR</td>
                                                                                            <td>02-04-2023 </td>
                                                                                        </tr>
                                                                                    </tbody>
                                                                                </table>

                                                                                <button type="button" class="btn btn-success float-right" data-toggle="modal" data-target="#exampleModalCenter">Attach </button>

                                                                            </div>
                                                                            <div class="tab-pane fade active show" id="custom-tabs-four-profile" role="tabpanel" aria-labelledby="custom-tabs-four-profile-tab">

                                                                                <div>
                                                                                    <img src="../dist/img/Doc_Sample_pic.jpg" style="width: 708px;" />
                                                                                </div>

                                                                            </div>
                                                                            <div class="tab-pane fade" id="custom-tabs-four-messages" role="tabpanel" aria-labelledby="custom-tabs-four-messages-tab">

                                                                                <div>
                                                                                    <img src="../dist/img/Doc_Sample_pic.jpg" style="width: 708px;" />
                                                                                </div>

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



    <!-- Modal -->
    <div class="modal fade" id="exampleModalCenter" tabindex="-1" role="dialog" aria-labelledby="exampleModalCenterTitle" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <div class="form-group">
                        <label for="inputDescription">Additional Reply</label>
                        <textarea id="inputDescription" class="form-control" rows="4"></textarea>
                    </div>

                    <div class="form-group">
                        <label for="inputEstimatedBudget">Additional Document Submitted by </label>
                        <input type="number" id="inputEstimatedBudget" class="form-control">
                    </div>
                    <div class="form-group">
                        <label for="inputEstimatedBudget">Document name  </label>
                        <input type="number" id="inputEstimatedBudget" class="form-control">
                    </div>
                    <div class="form-group">
                        <input type="file" id="inputEstimatedBudget" class="">
                    </div>

                </div>
                <div class="modal-footer">
                    <%-- <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>--%>
                    <button type="button" class="btn btn-primary">Add</button>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
