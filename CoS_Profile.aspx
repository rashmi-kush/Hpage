<%@ Page Title="" Language="C#" MasterPageFile="~/CoS/CoSMaster.Master" AutoEventWireup="true" CodeBehind="CoS_Profile.aspx.cs" Inherits="CMS_Sampada.CoS.CoS_Profile" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .card-header {
    background-color: #e9564e;
    color: white;
}
        .card-body {
            min-height: 0;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

      <section class="content-header">
        <div class="container-fluid">
            <div class="row mb-2">
                <div class="col-sm-6">
                    <h5>Profile</h5>
                </div>
            </div>
        </div>
    </section>


    <section class="content">
        <div class="container-fluid">
            <div class="row">
                <div class="col-md-3">

                    <div class="card card-success card-outline">
                        <div class="card-body box-profile">
                            <div class="text-center" style="display:none">
                                <img class="profile-user-img img-fluid img-circle" src="../../dist/img/user4-128x128.jpg" alt="User profile picture">
                            </div>

                            <h3 class="profile-username text-center"><asp:Label ID="lblName" runat="server"></asp:Label></h3>

                            <p class="text-muted text-center">
                                Collector of Stamp/<asp:Label ID="lblDesignation1" runat="server"></asp:Label></p>

                            <%-- <ul class="list-group list-group-unbordered mb-3">
                                <li class="list-group-item">
                                    <b>Followers</b> <a class="float-right">1,322</a>
                                </li>
                                <li class="list-group-item">
                                    <b>Following</b> <a class="float-right">543</a>
                                </li>
                                <li class="list-group-item">
                                    <b>Friends</b> <a class="float-right">13,287</a>
                                </li>
                            </ul>--%>
                        </div>
                    </div>


                </div>

                <div class="col-md-9">
                    <div class="card">
                        <div class="card-header">
                            <h3 class="card-title">Personal Details</h3>
                        </div>
                        <div class="card-body">

                            <div class="row">
                                <div class="col-sm-4">
                                    <div class="form-group">
                                        <label>Role</label>
                                        <asp:Label ID="lblRoll" runat="server" class="form-control" Text="COS"></asp:Label>
                                    </div>
                                </div>
                                <div class="col-sm-4">
                                    <div class="form-group">
                                        <label>Full Name</label>
                                        <asp:Label ID="lblFullName" runat="server" class="form-control" Text="RRC"></asp:Label>
                                    </div>
                                </div>

                                <div class="col-sm-4">
                                    <div class="form-group">
                                        <label>Designation</label>
                                        <asp:Label ID="lblDesignation" runat="server" class="form-control" Text="RRC"></asp:Label>
                                    </div>
                                </div>

                            </div>

                            <div class="row">
                                <div class="col-sm-4">
                                    <div class="form-group">
                                        <label>Mobile</label>
                                        <asp:Label ID="lblMobile" runat="server" class="form-control" Text="RRC"></asp:Label>
                                    </div>
                                </div>
                                <div class="col-sm-4">
                                    <div class="form-group">
                                        <label>Email</label>
                                        <asp:Label ID="lblEmail" runat="server" class="form-control" Text="RRC"></asp:Label>
                                    </div>
                                </div>
                                <%--<div class="col-sm-4">
                        <h6><b>Profile Pic </b> </h6>
                     <div class="custom-file">
                      <input type="file" class="custom-file-input" id="customFile">
                      <label class="custom-file-label" for="customFile">Choose file</label>
                    </div>
                    </div>--%>
                            </div>

                            <%--<div class="text-center">
                                    <button class="btn btn-primary t-btn">Save</button>
                                </div>--%>
                        </div>
                    </div>

                    <div class="card">
                        <div class="card-header">
                            <h3 class="card-title">Office Details</h3>
                        </div>
                        <div class="card-body">
                           
                                <div class="row">
                                    <div class="col-sm-4">
                                        <div class="form-group">
                                            <label>Assign Role</label>
                                            <asp:Label ID="lblAssignRole" runat="server" class="form-control" ></asp:Label>
                                        </div>
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="form-group">
                                            <label>Office Code</label>
                                            <asp:Label ID="lblOfficeCode" runat="server" class="form-control" ></asp:Label>
                                        </div>
                                    </div>

                                    <div class="col-sm-4">
                                        <div class="form-group">
                                            <label>Office Name</label>
                                             <asp:Label ID="lblOfficeName" runat="server" class="form-control" ></asp:Label>
                                        </div>
                                    </div>

                                </div>

                                <div class="row">
                                    <div class="col-sm-4">
                                        <div class="form-group">
                                            <label>Office Address</label>
                                            <asp:Label ID="lblOfficeAddress" runat="server" class="form-control" ></asp:Label>
                                        </div>
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="form-group">
                                            <label>Office District</label>
                                            <asp:Label ID="lblOfficeDistrict" runat="server" class="form-control" ></asp:Label>
                                        </div>
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="form-group">
                                            <label>Office PIN Code</label>
                                            <asp:Label ID="lblOfficePINCode" runat="server" class="form-control" ></asp:Label>
                                        </div>
                                    </div>
                                </div>

                                <div class="row">
                                    <div class="col-sm-4">
                                        <div class="form-group">
                                            <label>Office Phone</label>
                                            <asp:Label ID="lblOfficePhone" runat="server" class="form-control" ></asp:Label>
                                        </div>
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="form-group">
                                            <label>Office Email</label>
                                            <asp:Label ID="lblOfficeEmail" runat="server" class="form-control" ></asp:Label>
                                        </div>
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="form-group">
                                            <label>Office Level</label>
                                               <asp:Label ID="lblOfficeLevel" runat="server" class="form-control" ></asp:Label>
                                        </div>
                                    </div>
                                </div>

                                <div class="row">
                                    <div class="col-sm-4">
                                        <div class="form-group">
                                            <label>Demography</label>
                                              <asp:Label ID="lblDemography" runat="server" class="form-control" ></asp:Label>

                                        </div>
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="form-group">
                                            <label>Demography Type</label>
                                            <input type="text" class="form-control" readonly="readonly" >
                                        </div>
                                    </div>

                                </div>

                              <%--  <div class="text-center">
                                    <button class="btn btn-primary t-btn">Save</button>
                                </div>--%>
                           
                        </div>
                    </div>
                </div>


            </div>
        </div>
    </section>



</asp:Content>
