<%@ Page Language="VB"
    MasterPageFile="~/sellersHubMasterPage.master"
    AutoEventWireup="false"
    CodeFile="invpricelist.aspx.vb"
    Inherits="invpricelist"
    Title="Inventory Pricelist | Sellers' HUB"%>

<asp:Content ID="cphContent" ContentPlaceHolderID="cphContent" runat="Server">
    <%-->>-----Loading Panel-------------%>
    <div id="divLoading" style="display: none">
        <div id="progressBackgroundFilter">
        </div>
        <div id="processMessage">
            <img alt="Loading..." src="images/loading.gif" />
        </div>
    </div>
    <%--<<-----Loading Panel-------------%>

    <div style="text-align:justify">
        <div>
            <ul class="breadcrumb">
                <li><a href="<%=MyDefaultInitialFile %>">Home</a> <span class="divider">/</span> </li>
                <li><a href="#">Download / Print Price List</a> </li>
            </ul>
        </div>

        <%------------Alert Error Message -------------%>
        <div data-div="alert" id="divErrorMsgBox">
        </div>

        <div id="divPriceList"  class="row-fluid">
            <div class="box span12">
                <div class="box-header well">
                    <h2>
                        <i class="icon-print"></i>&nbsp;Download / Print Price List</h2>
                </div>
                <div class="box-content form-horizontal">
                    <div class="alert alert-info">
                        This facility allows the user to either Print and/or Export the Price List to a
                        PDF file.
                    </div>
                     <%-- Allocation --%>
                    <div id="divAllocation" class="control-group  ss-item-required" data-validgroup="pricelist">
                        <label for="ddlAllocation" class="control-label">Allocation Status</label>
                        <div class="controls">
                            <select id="ddlAllocation" name="Allocation Status" class="chosen-select-deselect" data-placeholder="Select an Option"  >
                                <option value=""></option>
                            </select>
                        </div> 
                    </div> 

                    <%-- Project --%>
                    <div class="control-group  ss-item-required" data-validgroup="pricelist">
                        <label for="ddlProjects" class="control-label">Project</label>
                        <div class="controls">
                            <select id="ddlProjects" name="Project" class="chosen-select-deselect" data-placeholder="Select an Option"  >
                                <option value=""></option>
                            </select>
                        </div> 
                    </div> 

                    <%-- Phase/Building --%>
                    <div class="control-group  ss-item-required" data-validgroup="pricelist">
                        <label for="ddlPhaseBuilding" class="control-label">Phase/Building</label>
                        <div class="controls">
                            <select id="ddlPhaseBuilding" name="Phase/Building" class="chosen-select-deselect" data-placeholder="Select an Option"  >
                                <option value=""></option>
                            </select>
                        </div> 
                    </div> 

                    <%-- Scheme Type --%>
                    <div id="divSchemeType" class="control-group  ss-item-required" data-validgroup="pricelist">
                        <label for="ddlSchemeType" class="control-label">Scheme Type</label>
                        <div class="controls">
                            <select id="ddlSchemeType" name="Scheme Type" class="chosen-select-deselect" data-placeholder="Select an Option"  >
                                <option value=""></option>
                            </select>
                        </div> 
                    </div> 
                     
                    <%--Button Generate Price list--%>
                    <div class="form-actions">
                      <a id="btnProceed" class="btn btn-primary">   Generate Price List   </a> 
                    </div>

                     <%--Div price list report--%>
                    <div id="divfocushere" class="box-content form-horizontal">
                        
                        <div id="divPriceListReport" >
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="cphContentBottom" runat="Server">
   
    <script type="text/javascript">

        var ajaxurlGetData = 'invpricelist.aspx/GetDataFromDB';

        $(document).ready(function () {

            // Global variable
            var DropdownAllocation = $('#ddlAllocation');
            var DropdownProjects = $('#ddlProjects');
            var DropdownPhaseBuilding = $('#ddlPhaseBuilding');
            var DropdownSchemeType = $('#ddlSchemeType');

            $('#divSchemeType').css('display', 'none');

            // Automatic Show/ Hide loading modal div     
            $(document).bind("ajaxSend", function () {
                $("#divLoading").show();
            }).bind("ajaxStop ", function () {
                $("#divLoading").hide();
            });

            // Initialize chosen css for selector
            $('.chosen-select-deselect').chosen({ allow_single_deselect: true, width: "100%" }); //,width:"95%"

            //First dropdown item
            var newOption = $('<option value=""></option>');

            // Function for populate dropwdown
            function LoadProjectList() {
                DropdownProjects.empty();
                DropdownProjects.append(newOption);
                DropdownProjects.trigger("chosen:updated");

                DropdownPhaseBuilding.empty();
                DropdownPhaseBuilding.append(newOption);
                DropdownPhaseBuilding.trigger("chosen:updated");

                $('#divSchemeType').css('display', 'none');

                if (DropdownAllocation.val() == '') { return false };

                //Populate drop down project
                $("#ddlProjects_chosen > a > span").text("Loading data");

                $.ajax({
                    type: 'POST',
                    url: ajaxurlGetData,
                    dataType: 'json',
                    data: JSON.stringify({ Mode: 2, Allocation: DropdownAllocation.val(), Project: '', Phase: '', SchemeType: '' }),
                    contentType: 'application/json; charset=utf-8',
                    success: function (data) {
                        if ($.parseJSON(data.d) != undefined) {
                            if ($.parseJSON(data.d)[0].ErrorNumber == 101) { window.location.reload(); };
                            displayError($.parseJSON(data.d)[0].ErrorMessage);
                        }
                        else {

                            DropdownProjects.empty();
                            DropdownProjects.append(new Option("", ""));
                            $(data[0]).each(function (index, item) {
                                DropdownProjects.append(new Option(item.Description, item.Code));
                            });

                            DropdownProjects.trigger("chosen:updated");
                            $("#ddlProjects_chosen > a > span").text("Select an Option");

                            if ($(data[0]).length == 1) {
                                DropdownProjects.prop('selectedIndex', 1);
                                DropdownProjects.trigger("chosen:updated");
                                LoadPhaseList();
                            };
                        };
                    },
                    error: function (xhr) {
                        var err = $.parseJSON(xhr.responseText);
                        displayError('Error loading Location : ' + err.Message);
                    }
                });
            };

            function LoadPhaseList() {
                DropdownPhaseBuilding.empty();
                DropdownPhaseBuilding.append(newOption);
                DropdownPhaseBuilding.trigger("chosen:updated");

                $('#divSchemeType').css('display', 'none');

                if (DropdownProjects.val() == '') { return false };

                //Populate drop down phase
                $("#ddlPhaseBuilding_chosen > a > span").text("Loading data");

                $.ajax({
                    type: 'POST',
                    url: ajaxurlGetData,
                    dataType: 'json',
                    data: JSON.stringify({ Mode: 3, Allocation: DropdownAllocation.val(), Project: DropdownProjects.val(), Phase: '', SchemeType: '' }),
                    contentType: 'application/json; charset=utf-8',
                    success: function (data) {
                        if ($.parseJSON(data.d) != undefined) {
                            if ($.parseJSON(data.d)[0].ErrorNumber == 101) { window.location.reload(); };
                            displayError($.parseJSON(data.d)[0].ErrorMessage);
                        }
                        else {

                            DropdownPhaseBuilding.empty();
                            DropdownPhaseBuilding.append(new Option("", ""));
                            $(data[0]).each(function (index, item) {
                                DropdownPhaseBuilding.append(new Option(item.Description, item.Code));
                            });

                            DropdownPhaseBuilding.trigger("chosen:updated");
                            $("#ddlPhaseBuilding_chosen_chosen > a > span").text("Select an Option");

                            if ($(data[0]).length == 1) {
                                DropdownPhaseBuilding.prop('selectedIndex', 1);
                                DropdownPhaseBuilding.trigger("chosen:updated");
                                LoadSchemeType();
                            };
                        };
                    },
                    error: function (xhr) {
                        var err = $.parseJSON(xhr.responseText);
                        displayError('Error loading Location : ' + err.Message);
                    }
                });
            };
            function LoadSchemeType() {
                DropdownSchemeType.empty();
                DropdownSchemeType.append(newOption);
                DropdownSchemeType.trigger("chosen:updated");

                $('#divSchemeType').css('display', 'none');

                if (DropdownPhaseBuilding.val() == '') { return false };

                //Populate drop down phase
                $("#ddlSchemeType_chosen > a > span").text("Loading data");

                $.ajax({
                    type: 'POST',
                    url: ajaxurlGetData,
                    dataType: 'json',
                    data: JSON.stringify({ Mode: 4, Allocation: DropdownAllocation.val(), Project: DropdownProjects.val(), Phase: DropdownPhaseBuilding.val(), SchemeType: '' }),
                    contentType: 'application/json; charset=utf-8',
                    success: function (data) {
                        if ($.parseJSON(data.d) != undefined) {
                            if ($.parseJSON(data.d)[0].ErrorNumber == 101) { window.location.reload(); };
                            displayError($.parseJSON(data.d)[0].ErrorMessage);
                        }
                        else {

                            if ($(data[0]).length > 0) {
                                $('#divSchemeType').css('display', 'block');
                                DropdownSchemeType.empty();
                                DropdownSchemeType.append(new Option("", ""));
                                $(data[0]).each(function (index, item) {
                                    DropdownSchemeType.append(new Option(item.Description, item.Code));
                                });
                                DropdownSchemeType.trigger("chosen:updated");
                                $("#ddlSchemeType_chosen_chosen > a > span").text("Select an Option");

                                if ($(data[0]).length == 1) {
                                    DropdownSchemeType.prop('selectedIndex', 1);
                                    DropdownSchemeType.trigger("chosen:updated");
                                };
                            };
                        };
                    },
                    error: function (xhr) {
                        var err = $.parseJSON(xhr.responseText);
                        displayError('Error loading Location : ' + err.Message);
                    }
                });
            };


            //Populate Allocation 
            DropdownAllocation.empty();
            DropdownAllocation.append(newOption);
            DropdownAllocation.trigger("chosen:updated");

            $("#ddlAllocation_chosen > a > span").text("Loading data");

            $.ajax({
                type: 'POST',
                url: ajaxurlGetData,
                dataType: 'json',
                data: JSON.stringify({ Mode: 1, Allocation: '', Project: '', Phase: '', SchemeType: '' }),
                contentType: 'application/json; charset=utf-8',
                success: function (data) {
                    if ($.parseJSON(data.d) != undefined) {
                        if ($.parseJSON(data.d)[0].ErrorNumber == 101) { window.location.reload(); };
                        displayError($.parseJSON(data.d)[0].ErrorMessage);
                    }
                    else {

                        DropdownAllocation.empty();
                        DropdownAllocation.append(new Option("", ""));
                        $(data[0]).each(function (index, item) {
                            DropdownAllocation.append(new Option(item.Description, item.Code));
                        });

                        DropdownAllocation.trigger("chosen:updated");
                        $("#ddlAllocation_chosen > a > span").text("Select an Option");

                        // if 1 item only, run LoadInitialField and hide divAllocation
                        if ($(data[0]).length == 1) {
                            //isOneAllocation = 1;
                            DropdownAllocation.prop('selectedIndex', 1);
                            DropdownAllocation.trigger("chosen:updated");
                            $('#divAllocation').css('display', 'none');
                            LoadProjectList();
                        };
                    };
                },
                error: function (xhr) {
                    var err = $.parseJSON(xhr.responseText);
                    displayError('Error loading Location : ' + err.Message);
                }
            });

            
            //Event Allocation 
            DropdownAllocation.change(function () {
                LoadProjectList();
            });
             
            //Event Project 
            DropdownProjects.change(function () {
                LoadPhaseList();
            });

            //Event Phase 
            DropdownPhaseBuilding.change(function () {
                LoadSchemeType();
            });

            // Display Price list 
            $('#btnProceed').click(function () {
                //TO prevent double click
                $('#btnProceed').disabled = true;
                $('#divPriceListReport').css('display', 'none');

                // validate required field 
                if (formIsValid('pricelist') == false) {
                    $('#btnProceed').disabled = false;
                    return false;
                };


                if ($('#divSchemeType').css('display') != 'none') {    //Price list Uploaded 
                    // Get data
                    $.ajax({
                        url: 'invpricelist.aspx/GetDataFromDB',
                        type: "Post",
                        data: JSON.stringify({ Mode: 5, Allocation: DropdownAllocation.val(), Project: DropdownProjects.val(), Phase: DropdownPhaseBuilding.val(), SchemeType: DropdownSchemeType.val() }),
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (data) {
                            if ($.parseJSON(data.d) != undefined) {
                                //Error
                                if ($.parseJSON(data.d)[0].ErrorNumber == 101) { window.location.reload(); };

                                if ($.parseJSON(data.d)[0].ErrorNumber == 8888) {
                                    $('#divPriceListReport').css('display', 'block');
                                    $('#divPriceListReport').html($.parseJSON(data.d)[0].ErrorMessage)
                                    ScrolltoDIV('divfocushere');
                                }
                                else {
                                    displayError($.parseJSON(data.d)[0].ErrorMessage);
                                }

                            }
                        },
                        error: function (xhr) {
                            var err = $.parseJSON(xhr.responseText);
                            displayError('Error while loading price list : ' + err.Message);
                        },
                        complete: function () { $('#btnProceed').disabled = false; }
                    });
                }
                else {
                    // Get data
                    $.ajax({
                        url: 'invpricelist.aspx/ShowPriceList',
                        type: "Post",
                        data: JSON.stringify({ Allocation: DropdownAllocation.val(), Project: DropdownProjects.val(), Phase: DropdownPhaseBuilding.val(), SchemeType: '' }),
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (data) {
                            if ($.parseJSON(data.d) != undefined) {
                                //Error
                                if ($.parseJSON(data.d)[0].ErrorNumber == 101) { window.location.reload(); };

                                if ($.parseJSON(data.d)[0].ErrorNumber == 8888) {    //Success
                                    $('#divPriceListReport').css('display', 'block');
                                    $('#divPriceListReport').html($.parseJSON(data.d)[0].ErrorMessage)
                                    ScrolltoDIV('divfocushere');
                                }
                                else {
                                    displayError($.parseJSON(data.d)[0].ErrorMessage);
                                }

                            }
                        },
                        error: function (xhr) {
                            var err = $.parseJSON(xhr.responseText);
                            displayError('Error while loading price list : ' + err.Message);
                        },
                        complete: function () { $('#btnProceed').disabled = false; }
                    });
                };

                $('#btnProceed').disabled = false;
                return false;
            });

            // Validate User Entry   
            function formIsValid(valigGroupName) {

                var fields = $('[data-validgroup="' + valigGroupName + '"]')
                   .filter(".ss-item-required")
                   .filter(":visible")
                   .find("select, textarea, input[type!='file']");

                var error = ''

                $.each(fields, function (i, field) {
                    // validate each field required field and data type is valid 
                    var name = $(field).attr('name');
                    var value = $(field).val();

                    if (!(!name)) //<-- valid field, should name exists
                    {
                        if (!value) {
                            error = error + name + ' is required' + '<br/>'
                        }
                        else if ($(field).data('type') == 'number') {
                            if (!$.isNumeric(value.replaceAll(',', '')))
                            { error = error + name + ' should be numeric' + '<br/>' }
                            else if (value <= 0)
                            { error = error + name + ' should be greater than zero' + '<br/>' };
                        }
                    }
                });


                var divAlertID = $('[data-validgroup="' + valigGroupName + '"]').filter('[data-div="alert"]').attr('id');

                displayError(error, divAlertID);

                return (error == '');
            };

            // Display Error Message  
            function displayError(error, divID, cssAlert) {
                // clear all display error message
                $('[data-div="alert"]').empty();

                if (error == '')
                { return };

                // get source div to display
                var DivToDisplay = divID ? document.getElementById(divID) : document.getElementById('divErrorMsgBox');

                // div Message style
                var cssMsg = cssAlert ? cssAlert : 'alert-error';

                //create div alert message
                var divError = $('<div/>').addClass('alert ' + cssMsg).html(error);

                // append error message to source div
                $(DivToDisplay).append(divError);
                $('html,body,window').animate({ scrollTop: DivToDisplay.offsetTop }, 500);
            };

            function ScrolltoDIV(divID) {
                var offsetTop = document.getElementById(divID).offsetTop;
                $('html,body,window').animate({ scrollTop: offsetTop }, 500);
            };
         
        });              // end doc ready

       
       
        
    </script>

</asp:Content>

