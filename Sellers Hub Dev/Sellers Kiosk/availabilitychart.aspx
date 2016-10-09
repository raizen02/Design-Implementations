<%@ Page Language="VB" 
    MasterPageFile="~/sellersHubMasterPage.master" 
    AutoEventWireup="false"
    CodeFile="availabilitychart.aspx.vb" 
    Inherits="availabilitychart"
    Title="Availability Chart | Sellers' HUB" %>


<asp:Content ID="Content1" ContentPlaceHolderID="cphContent" runat="Server">
    <div style="text-align: justify">  
        <div>
            <ul class="breadcrumb">
                <li><a href="<%=MyDefaultInitialFile%>">Home</a> <span class="divider">/</span> </li>
                <li><a href="#">Availability Chart</a> </li>
            </ul>
        </div>
        <div class="alert alert-info">
            This facility allows the user to search the available for sale property.
        </div>
        <div class="alert alert-error" id="divErrorMsgBox" style="display: none">
            <button type="button" class="close" data-dismiss="alert">
                ×</button>
            <div id="divErrorMsg">
            </div>
        </div>
        <div id="divLoading" style="display:none" >
            <div id="progressBackgroundFilter">
            </div>
            <div id="processMessage">
                <img alt="Loading..." src="images/loading.gif" />
            </div>
        </div>     
        
        <%------------ User Entry Field for Filter  ---------------%>
        <div class="row-fluid">
            <div class="box span6" id="divFilterFields" >
                  
                <div class="box-header well">
                    <h2><i class="icon-search"></i>&nbsp;Availability Chart</h2>
                </div>
                <%------------ ComboBox for filter---------------%>
                <div class="box-content form-horizontal">
                    <form role="form">              
                        <div class="form-group ss-item-required">
                            <label for="ddlLocation" >Location</label>
                            <select id="ddlLocation" name="Location" class="chosen-select-deselect form-control"  data-placeholder="Select an Option" >
                                <option value=""></option>
                            </select> 
                        </div>
                        <div class="form-group ss-item-required">
                            <label for="ddlProject" >Project</label>
                            <select id="ddlProject" name="Project" class="chosen-select-deselect form-control" data-placeholder="Select an Option" >
                                <option value=""></option>
                            </select> 
                        </div>
                        <div class="form-group" id="divPhase" style="display:none"  >
                            <label for="ddlPhase">Phase/Building</label>
                            <select id="ddlPhase"  class="chosen-select-deselect form-control" data-placeholder="Select an Option" >
                                <option value=""></option>
                            </select> 
                        </div>
                        <div class="form-group" id="divBlock" style="display:none">
                            <label for="ddlBlock">Block/Floor</label>
                            <select id="ddlBlock"    class="chosen-select-deselect form-control" data-placeholder="Select an Option"  >
                                <option value=""></option>
                            </select> 
                        </div>
                        <div class="form-group"  id="divUnit" style="display:none">
                            <label for="ddlInventoryUnit">Lot / Unit</label>
                            <select id="ddlInventoryUnit"    class="chosen-select-deselect  form-control" data-placeholder="Select an Option" >
                                <option value=""></option>
                            </select> 
                        </div>
                        <div class="form-group" id="divSubType" style="display:none">
                            <label for="ddlInventoryUnit">Product Sub-Type</label>
                            <div class="checkbox" class="form-control" id="divCheckboxSubType"> </div>                        
                        </div>
                    </form> 
                </div>
               <%------------ ComboBox for filter---------------%>
               
               <%------------ Reset,Filter Buttons ---------------%>
                 <div class="form-actions">
                    <a  id="btnFilter" onclick="return false"  class="btn btn-primary">Generate</a> 
                    <a  id="btnReset" onclick="return false" class="btn" >Reset</a> 
                </div>
                <%------------ Reset,Filter Buttons ---------------%>
            </div>
        <%--</div>--%>
        <%------------ User Entry Field for Filter  ---------------%>
       
       
        <%------------ Result  ---------------%>
        <%--<div class="row-fluid" >--%>
            <div class="box span9" id="divResult" style="display:none" >
                <div class="box-header well">
                    <h2>
                        <i class="icon-list-alt"></i>&nbsp;Result</h2>
                </div>
                <div class="box-content form-horizontal">                
                    <div class="box-content">
                        <ul class="nav nav-tabs" id="myTab1" >
                            <li class="active" id="aUnitResultTab"  ><a href="#divUnitResultTab" data-toggle="tab" >Available Unit List</a></li>
                            <li  id="aUnitDetailsTab" ><a href="#divUnitDetailsTab" data-toggle="tab" >Unit Details</a></li>
                        </ul>
                        <div id="myResultContent" class="tab-content">
                            <%------------ Tab Page 1 : Filter Result  ---------------%>
                            <div class="tab-pane active" id="divUnitResultTab" >
                             <div class="tab-pane-content"  > 
                                <table id="tblUnits" class="table table-bordered table-striped table-condensed" cellspacing="0" width="100%">
                                    <thead>
                                        <tr>
                                            <th>Phase</th>
                                            <th>Block</th>
                                            <th>UnitName</th>
                                            <th>Product Type</th>
                                            <th>Product Sub-Type</th>
                                            <th>      </th>
                                        </tr>
                                    </thead>
                                    <%--<tfoot>
                                        <tr>
                                            <th>Phase</th>
                                            <th>Block</th>
                                            <th>UnitName</th>
                                            <th>Product Type</th>
                                            <th>Product Sub-Type</th>
                                            <th>      </th>
                                        </tr>
                                    </tfoot>--%>
                                </table>
                            </div>
                            </div>
                            <%------------ Tab Page 1 : Filter Result  ---------------%>
                            <%------------ Tab Page 2 : Unit Details and Temp Computation  ---------------%>
                            <div class="tab-pane" id="divUnitDetailsTab" >
                             <div class="tab-pane-content" > 
                                <%-- Unit Information--%>
                                <legend>
                                    <h3>
                                        Unit Information</h3>
                                </legend>
                                
                                <div class="box-content form-horizontal">
                                    <div class="control-group">
                                        <label class="control-label">
                                            Project :</label>
                                        <div class="controls" id="spnUnitInfoProject" style="padding-top: 5px;font-weight:bold" ></div>
                                    </div>
                                   <div class="control-group">
                                        <label class="control-label">
                                            Phase/Building :</label>
                                        <div class="controls" id="spnUnitInfoPhase" style="padding-top: 5px;font-weight:bold"></div>
                                    </div>
                                    <div class="control-group">
                                        <label class="control-label">
                                            Block/Floor :</label>
                                        <div class="controls" id="spnUnitInfoBlock" style="padding-top: 5px;font-weight:bold"></div>
                                    </div>                                    
                                    <div class="control-group">
                                        <label class="control-label" >
                                            Inventory Unit :</label>
                                        <div class="controls" id="spnUnitInfoInventoryUnit" style="padding-top: 5px;font-weight:bold" ></div>
                                    </div>
                                    
                                    <div id="divOtherInfo"></div> 
                                </div>
                                <%-- Pricelist Information--%>
                                <legend>
                                    <h3>
                                        Pricelist Information</h3>
                                </legend>
                                
                                <%--Price List Info--%>
                                <%--System Generated HTML Script inside of divPriceListInfo, sample only --%>
                                <div id="divPriceListInfo" class="box-content form-horizontal">
                                    <%--NESTOR--%>
                                    <%--Start Div Tab Page--%>
                                    <ul class="nav nav-tabs" id="myTab">
                                        <li class="active"><a href="#FS01Z004F005" data-toggle="tab" >In-House Regular</a></li>
                                        <li class=""><a href="#FS02Z004F005" data-toggle="tab" >In-House Regular (Promo)</a></li>
                                    </ul>
                                    <%--Start Tab content--%>
                                    <div id="myTabContent" class="tab-content">
                                        <div class="tab-pane active" id="FS01Z004F005">
                                            <div class="control-group">
                                                <h5>
                                                    In-House Regular</h5>
                                            </div>
                                            <div class="control-group">
                                                <label class="control-label">
                                                    Total Contract Price :
                                                </label>
                                                <div class="controls">
                                                    <span class="input-large uneditable-input">622,800.00</span></div>
                                            </div>
                                            <div class="control-group">
                                                <label class="control-label">
                                                    Reservation Fee :
                                                </label>
                                                <div class="controls">
                                                    <span class="input-large uneditable-input">20,000.00</span></div>
                                            </div>
                                            <div class="control-group" style="width: 887px">
                                                <label class="control-label">
                                                    DP (Downpayment)
                                                </label>
                                                <div class="controls">
                                                    <span class="input-large uneditable-input">104,560.00</span></div>
                                            </div>
                                            <div class="control-group">
                                                <label class="control-label">
                                                    DP Period (No. of months)
                                                </label>
                                                <div class="controls">
                                                    <span class="input-large uneditable-input">24 months</span></div>
                                            </div>
                                            <div class="control-group">
                                                <label class="control-label">
                                                    DP Monthly Amortization
                                                </label>
                                                <div class="controls">
                                                    <span class="input-large uneditable-input">4,356.67</span></div>
                                            </div>
                                            <div class="control-group">
                                                <label class="control-label">
                                                    DP Interest Rate
                                                </label>
                                                <div class="controls">
                                                    <span class="input-large uneditable-input">0.00</span></div>
                                            </div>
                                            <div class="control-group">
                                                <label class="control-label">
                                                    Loanable Amount
                                                </label>
                                                <div class="controls">
                                                    <span class="input-large uneditable-input">498,240.00</span></div>
                                            </div>
                                            <div class="control-group">
                                                <label class="control-label">
                                                    Loan Balance Terms
                                                </label>
                                                <div class="controls">
                                                    <h3>
                                                        <small>60 months</small></h3>
                                                    <table class="table table-bordered table-striped table-condensed ">
                                                        <tr>
                                                            <th>
                                                                Min Term</th>
                                                            <th>
                                                                Max Term</th>
                                                            <th>
                                                                Monthly Amortization</th>
                                                            <th>
                                                                Interest Rate</th>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                1</td>
                                                            <td>
                                                                60</td>
                                                            <td>
                                                                11,464.43</td>
                                                            <td>
                                                                13.50%</td>
                                                        </tr>
                                                    </table>
                                                    <h3>
                                                        <small>84 months</small></h3>
                                                    <table class="table table-bordered table-striped table-condensed ">
                                                        <tr>
                                                            <th>
                                                                Min Term</th>
                                                            <th>
                                                                Max Term</th>
                                                            <th>
                                                                Monthly Amortization</th>
                                                            <th>
                                                                Interest Rate</th>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                1</td>
                                                            <td>
                                                                84</td>
                                                            <td>
                                                                10,181.94</td>
                                                            <td>
                                                                17.00%</td>
                                                        </tr>
                                                    </table>
                                                    <h3>
                                                        <small>120 months</small></h3>
                                                    <table class="table table-bordered table-striped table-condensed ">
                                                        <tr>
                                                            <th>
                                                                Min Term</th>
                                                            <th>
                                                                Max Term</th>
                                                            <th>
                                                                Monthly Amortization</th>
                                                            <th>
                                                                Interest Rate</th>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                1</td>
                                                            <td>
                                                                120</td>
                                                            <td>
                                                                9,300.76</td>
                                                            <td>
                                                                19.00%</td>
                                                        </tr>
                                                    </table>
                                                </div>
                                            </div>
                                        </div>
                                        <%--END TabPane--%>
                                        <div class="tab-pane " id="FS02Z004F005">
                                            <div class="control-group">
                                                <h5>
                                                    In-House Regular (Promo)</h5>
                                            </div>
                                            <div class="control-group">
                                                <label class="control-label">
                                                    Total Contract Price :
                                                </label>
                                                <div class="controls">
                                                    <span class="input-large uneditable-input">593,600.00</span></div>
                                            </div>
                                            <div class="control-group">
                                                <label class="control-label">
                                                    Reservation Fee :
                                                </label>
                                                <div class="controls">
                                                    <span class="input-large uneditable-input">15,000.00</span></div>
                                            </div>
                                            <div class="control-group">
                                                <label class="control-label">
                                                    DP (Downpayment)
                                                </label>
                                                <div class="controls">
                                                    <span class="input-large uneditable-input">103,720.00</span></div>
                                            </div>
                                            <div class="control-group">
                                                <label class="control-label">
                                                    DP Period (No. of months)
                                                </label>
                                                <div class="controls">
                                                    <span class="input-large uneditable-input">24 months</span></div>
                                            </div>
                                            <div class="control-group">
                                                <label class="control-label">
                                                    DP Monthly Amortization
                                                </label>
                                                <div class="controls">
                                                    <span class="input-large uneditable-input">4,321.67</span></div>
                                            </div>
                                            <div class="control-group">
                                                <label class="control-label">
                                                    DP Interest Rate
                                                </label>
                                                <div class="controls">
                                                    <span class="input-large uneditable-input">0.00</span></div>
                                            </div>
                                            <div class="control-group">
                                                <label class="control-label">
                                                    Loanable Amount
                                                </label>
                                                <div class="controls">
                                                    <span class="input-large uneditable-input">474,880.00</span></div>
                                            </div>
                                            <div class="control-group">
                                                <label class="control-label">
                                                    Loan Balance Terms
                                                </label>
                                                <div class="controls">
                                                    <h3>
                                                        <small>60 months</small></h3>
                                                    <table class="table table-bordered table-striped table-condensed ">
                                                        <tr>
                                                            <th>
                                                                Min Term</th>
                                                            <th>
                                                                Max Term</th>
                                                            <th>
                                                                Monthly Amortization</th>
                                                            <th>
                                                                Interest Rate</th>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                1</td>
                                                            <td>
                                                                60</td>
                                                            <td>
                                                                10,926.92</td>
                                                            <td>
                                                                13.50%</td>
                                                        </tr>
                                                    </table>
                                                    <h3>
                                                        <small>84 months | 13.50% (Flexi term)</small></h3>
                                                    <table class="table table-bordered table-striped table-condensed ">
                                                        <tr>
                                                            <th>
                                                                Min Term</th>
                                                            <th>
                                                                Max Term</th>
                                                            <th>
                                                                Monthly Amortization</th>
                                                            <th>
                                                                Interest Rate</th>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                1</td>
                                                            <td>
                                                                60</td>
                                                            <td>
                                                                8,768.61</td>
                                                            <td>
                                                                13.50%</td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                61</td>
                                                            <td>
                                                                84</td>
                                                            <td>
                                                                9,251.59</td>
                                                            <td>
                                                                19.00%</td>
                                                        </tr>
                                                    </table>
                                                    <h3>
                                                        <small>120 months | 13.50% (Flexi term)</small></h3>
                                                    <table class="table table-bordered table-striped table-condensed ">
                                                        <tr>
                                                            <th>
                                                                Min Term</th>
                                                            <th>
                                                                Max Term</th>
                                                            <th>
                                                                Monthly Amortization</th>
                                                            <th>
                                                                Interest Rate</th>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                1</td>
                                                            <td>
                                                                60</td>
                                                            <td>
                                                                7,231.20</td>
                                                            <td>
                                                                13.50%</td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                61</td>
                                                            <td>
                                                                120</td>
                                                            <td>
                                                                8,152.22</td>
                                                            <td>
                                                                19.00%</td>
                                                        </tr>
                                                    </table>
                                                </div>
                                            </div>
                                        </div>
                                        <%--END TabPane--%>
                                    </div>
                                    <%--END Tab content--%>
                                    <%--END Tab Page--%>
                                </div>
                                <%-- DIV divUnitDetailsTab--%>
                                </div>
                             </div>
                            <%------------ Tab Page 2 : Unit Details and Temp Computation  ---------------%>
                            
                        </div>
                    </div>
                </div>
            </div>
        </div>
    
        
    </div>
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="cphContentBottom" runat="Server">

    <script type="text/javascript">
    
        var ajaxurl = 'availabilitychart.aspx/DataFromDB';
                  
        $(document).ready(function () {

            // Automatic Show/ Hide loading modal div     
            $(document).bind("ajaxSend", function(){
               $("#divLoading").show();
             }).bind("ajaxStop", function(){
               $("#divLoading").hide();
             });
                  
            // Initialize chosen css for selector
            $('.chosen-select-deselect').chosen({ allow_single_deselect: true ,width:"95%"});
           
           
            displayError('');
            var DropdownLoc = $('#ddlLocation');
            var DropdownProj = $('#ddlProject');
            var DropdownPhase = $('#ddlPhase');
            var DropdownBlock = $('#ddlBlock');
            var DropdownUnit = $('#ddlInventoryUnit');
                   
            var divPhase =  $('#divPhase');
            var divBlock =  $('#divBlock');
            var divUnit =  $('#divUnit');
            var divSubType = $('#divSubType');
            var divResult = $('#divResult');
            var divPriceListInfo = $('#divPriceListInfo'); 
            
            
            divPhase.css('display','none');
            divBlock.css('display','none');
            divUnit.css('display','none');
            divSubType.css('display','none');
            divResult.css('display','none');
            divPriceListInfo.html('');
            
            // Location 
            function LoadLocation() {
                var newOption = $('<option value=""></option>');
                DropdownLoc.empty();
                DropdownLoc.append(newOption);
                DropdownLoc.trigger("chosen:updated");
                
                $("#ddlLocation_chosen > a > span").text("Loading data");  
                
                $.ajax({
                       type: 'POST',
                       url: ajaxurl,
                       dataType: 'json',
                       data: JSON.stringify({Mode:1,Location:'',Project:'',Phase:'',Block:'',RefObj:'',SubType:''}),
                       contentType :'application/json; charset=utf-8',
                       success: function (data) {
                                   if ($.parseJSON(data.d) != undefined){
                                       if ($.parseJSON(data.d)[0].ErrorNumber == 101){window.location.reload();};
                                       displayError($.parseJSON(data.d)[0].ErrorMessage); 
                                   }
                                   else 
                                   {   
                                        DropdownLoc.empty();
                                        DropdownLoc.append(new Option("", ""));
                                        $(data).each(function (index, item) {
                                           DropdownLoc.append(new Option(item.Description, item.Code));                       
                                       });
                                        DropdownLoc.trigger("chosen:updated");
                                        $("#ddlLocation_chosen > a > span").text("Select an Option");  
                                    };
                                },
                       error: function (xhr) {
                            var err = JSON.parse(xhr.responseText);                        
                            displayError('Error loading Location : ' + err.Message); 
                       }
                  });
             };
             
           //Call load location LoadLocation
           LoadLocation();
        
           
          //Project
           DropdownLoc.change(function () {
                displayError('');
                divPhase.css('display','none');
                divBlock.css('display','none');
                divUnit.css('display','none');
                divSubType.css('display','none');
                divResult.css('display', 'none');
                
                var newOption = $('<option value=""></option>');
                DropdownProj.empty();
                DropdownProj.append(newOption);
                DropdownProj.trigger("chosen:updated");
               
                DropdownPhase.empty();
                DropdownPhase.append(newOption);
                DropdownPhase.trigger("chosen:updated");
               
                DropdownBlock.empty();
                DropdownBlock.append(newOption);
                DropdownBlock.trigger("chosen:updated");
                
                DropdownUnit.empty();
                DropdownUnit.append(newOption);
                DropdownUnit.trigger("chosen:updated");
                
                if (DropdownLoc.val() == '')
                { return ;
                }
                
                $("#ddlProject_chosen > a > span").text("Loading data");  
                              
                $.ajax({
                       type: 'POST',
                       url: ajaxurl,
                       dataType: 'json',
                       data: JSON.stringify({Mode:2,Location:DropdownLoc.val(),Project:'',Phase:'',Block:'',RefObj:'',SubType:''}),
                       contentType :'application/json; charset=utf-8',
                       success: function (data) {
                                   if ($.parseJSON(data.d) != undefined){
                                       if ($.parseJSON(data.d)[0].ErrorNumber == 101){window.location.reload();};
                                       displayError($.parseJSON(data.d)[0].ErrorMessage); 
                                   }
                                   else 
                                   {   DropdownProj.empty();
                                       DropdownProj.append(new Option("", ""));
                                       $(data).each(function (index, item) {
                                           DropdownProj.append(new Option(item.Description, item.Code));                       
                                       });
                                       DropdownProj.trigger("chosen:updated");
                                       $("#ddlProject_chosen > a > span").text("Select an Option");  
                                   };                         
                               },
                       error: function (xhr, status, error) {
                                var err = $.parseJSON(xhr.responseText);                        
                                displayError('Error loading Project : ' + err.Message); 
                              }
                    });
             });
             
          
          //Phase
           DropdownProj.change(function () {
                displayError('');
                divPhase.css('display','block');
                divBlock.css('display','none');
                divUnit.css('display','none');
                divSubType.css('display','none');
                divResult.css('display', 'none');
                
                
                var newOption = $('<option value=""></option>');
                DropdownPhase.empty();
                DropdownPhase.append(newOption);
                DropdownPhase.trigger("chosen:updated");
               
                DropdownBlock.empty();
                DropdownBlock.append(newOption);
                DropdownBlock.trigger("chosen:updated");
                
                DropdownUnit.empty();
                DropdownUnit.append(newOption);
                DropdownUnit.trigger("chosen:updated");
                
                if (DropdownProj.val() == '')
                { return ;
                }
                
                $("#ddlPhase_chosen > a > span").text("Loading data");                
                $.ajax({
                       type: 'POST',
                       url: ajaxurl,
                       dataType: 'json',
                       data: JSON.stringify({Mode:3,Location:DropdownLoc.val(),Project:DropdownProj.val(),Phase:'',Block:'',RefObj:'',SubType:''}),
                       contentType :'application/json; charset=utf-8',
                       success: function (data) {
                                  if ($.parseJSON(data.d) != undefined){
                                     if ($.parseJSON(data.d)[0].ErrorNumber == 101){window.location.reload();};
                                     displayError($.parseJSON(data.d)[0].ErrorMessage);  
                                   }
                                   else 
                                   {
                                       DropdownPhase.empty();
                                     
                                       DropdownPhase.append(new Option("", ""));
                                       $(data).each(function (index, item) {
                                          DropdownPhase.append(new Option(item.Description, item.Code));
                                       }); 
                                       DropdownPhase.trigger("chosen:updated");
                                       $("#ddlPhase_chosen > a > span").text("Select an Option");
                                       populateChbSubType();
                                   };
                               },
                       error: function (xhr, status, error) {
                                var err = $.parseJSON(xhr.responseText);                        
                                displayError('Error loading Phase : ' + err.Message); 
                            }
                   }); 
                   
            });
            
            //Block
           DropdownPhase.change(function () {
                displayError('');
                divPhase.css('display','block');
                divBlock.css('display','block');
                divUnit.css('display','none');  
                divSubType.css('display','none');
                divResult.css('display', 'none');
                
                var newOption = $('<option value=""></option>');
                DropdownBlock.empty();
                DropdownBlock.append(newOption);
                DropdownBlock.trigger("chosen:updated");
                
                DropdownUnit.empty();
                DropdownUnit.append(newOption);
                DropdownUnit.trigger("chosen:updated");
                
                if (DropdownPhase.val() == '')
                { return ;
                }
                
                $("#ddlBlock_chosen > a > span").text("Loading data");                
                $.ajax({
                       type: 'POST',
                       url: ajaxurl,
                       dataType: 'json',
                       data: JSON.stringify({Mode:4,Location:DropdownLoc.val(),Project:DropdownProj.val(),Phase:DropdownPhase.val(),Block:'',RefObj:'',SubType:GetCheckedSubType()}),
                       contentType :'application/json; charset=utf-8',
                       success: function (data) {
                                   if ($.parseJSON(data.d) != undefined){
                                       if ($.parseJSON(data.d)[0].ErrorNumber == 101){window.location.reload();};
                                        displayError($.parseJSON(data.d)[0].ErrorMessage); 
                                   }
                                   else 
                                   {   DropdownBlock.empty();
                                       DropdownBlock.append(new Option("", ""));
                                       $(data).each(function (index, item) {
                                          DropdownBlock.append(new Option(item.Description, item.Code));
                                       }); 
                                       DropdownBlock.trigger("chosen:updated");
                                       $("#ddlBlock_chosen > a > span").text("Select an Option"); 
                                        populateChbSubType();
                                   };
                               },
                       error: function (xhr, status, error) {
                                var err = $.parseJSON(xhr.responseText);                        
                                displayError('Error loading Block : ' + err.Message); 
                             }
                    });
                  
           }); 
           
           //Lot
           DropdownBlock.change(function () {
               displayError('');
                divPhase.css('display','block');
                divBlock.css('display','block');
                divUnit.css('display','block');
                divSubType.css('display','none');
                divResult.css('display', 'none');
                
                var newOption = $('<option value=""></option>');
                DropdownUnit.empty();
                DropdownUnit.append(newOption);
                DropdownUnit.trigger("chosen:updated");
                
                if (DropdownBlock.val() == '')
                { return ;}
                
                $("#ddlInventoryUnit_chosen > a > span").text("Loading data");                
                $.ajax({
                       type: 'POST',
                       url: ajaxurl,
                       dataType: 'json',
                       data: JSON.stringify({Mode:5,Location:DropdownLoc.val(),Project:DropdownProj.val(),Phase:DropdownPhase.val(),Block:DropdownBlock.val(),RefObj:'',SubType:GetCheckedSubType()}),
                       contentType :'application/json; charset=utf-8',
                       success: function (data) {
                                   if ($.parseJSON(data.d) != undefined){
                                       if ($.parseJSON(data.d)[0].ErrorNumber == 101){window.location.reload();};
                                       displayError($.parseJSON(data.d)[0].ErrorMessage); 
                                   }
                                   else 
                                   {  DropdownUnit.empty();
                                      DropdownUnit.append(new Option("", ""));
                                       $(data).each(function (index, item) {
                                          DropdownUnit.append(new Option(item.Description, item.Code));
                                       }); 
                                      DropdownUnit.trigger("chosen:updated");
                                      $("#ddlInventoryUnit_chosen > a > span").text("Select an Option"); 
                                      populateChbSubType();
                                   };
                               },
                       error: function (xhr, status, error) {
                                var err = $.parseJSON(xhr.responseText);                        
                                displayError('Error loading Block : ' + err.Message); 
                            }
                    }); 
           }); 
           
            //Populate Result Table
           DropdownUnit.change(function () {
                    displayError('');
                    PopulateGrid();
                 });
           
           //Button Reset
           $('#btnReset').click(function () {
                
                $("#ddlLocation").val("").trigger("chosen:updated");

                var newOption = $('<option value=""></option>');
                DropdownProj.empty();
                DropdownProj.append(newOption);
                DropdownProj.trigger("chosen:updated");
                
                displayError('');
                
                divPhase.css('display','none');
                divBlock.css('display','none');
                divUnit.css('display','none');
                divSubType.css('display','none');
                divResult.css('display','none');
                
            });
                       
                 
           function populateChbSubType() {
                 divSubType.css('display','none');
                 $('#divCheckboxSubType').empty();
                 $.ajax({
                           type: 'POST',
                           url: ajaxurl,
                           dataType: 'json',
                           data: JSON.stringify({Mode:6,Location:DropdownLoc.val(),Project:DropdownProj.val(),Phase:DropdownPhase.val(),Block:DropdownBlock.val(),RefObj:DropdownUnit.val(),SubType:''}),
                           contentType :'application/json; charset=utf-8',
                           success: function (data) { 
                                       if ($.parseJSON(data.d) != undefined){
                                          if ($.parseJSON(data.d)[0].ErrorNumber == 101){window.location.reload();};
                                          displayError($.parseJSON(data.d)[0].ErrorMessage); 
                                       }
                                       else 
                                       { 
                                          if (data.length > 2) // start 'coz first row is blank
                                          {
                                              for (var i = 1; i < data.length; i++) {
                                                  var lang = $('<lang>').
                                                    html( data[i].Description);
                                                  $('<label>').
                                                    append('<input type="checkbox" value="' + data[i].Code + '" >').
                                                    append(lang).
                                                    appendTo('#divCheckboxSubType');
                                               };
                                               
                                              divSubType.css('display','block');
                                          };
                                       };
                                   },
                           error: function (xhr, status, error) {
                                    var err = $.parseJSON(xhr.responseText);                        
                                    displayError('Error loading Block : ' + err.Message);                             
                                }
                    });
            };
            
            
       //Button Fitler
       $('#btnFilter').click(function () {
            displayError('');
            if (formIsValid() == true)
                {PopulateGrid();
                };
          });
          
       // Validate User Entry   
       function formIsValid() {
           var fields = $(".ss-item-required")
                .find("select, textarea, input").serializeArray();
            var error  = ''
              $.each(fields, function(i, field) {
                if (!field.value)
                 error = error  + field.name + ' is required' + '<br/>';
               }); 
               displayError(error);              
               //console.log(fields);
              
               return (error == '');
        };
       
       // Display Error Message  
       function displayError(error) {
          $('#divErrorMsg').html(error);
              if (error != "") 
                {$('#divErrorMsgBox').css('display','block');
                 $('html,body,window').animate({scrollTop: divErrorMsgBox.offsetTop}, 500); }
              else
                {$('#divErrorMsgBox').css('display','none');}
         };
        
       // Get sub type delimited  
       function  GetCheckedSubType() {
            var code = '';
            $('#divCheckboxSubType input:checkbox:checked').each(function () {
                 code = code + ',' + $(this).val();
            });
            return code;
         };

       // Get Populate Grid                     
       function  PopulateGrid() {
           
           // Validation User session 
          $.ajax({
                   type: 'POST',
                   url: ajaxurl,
                   dataType: 'json',
                   data: JSON.stringify({Mode:1,Location:'',Project:'',Phase:'',Block:'',RefObj:'',SubType:''}),
                   contentType :'application/json; charset=utf-8',
                   success: function (data) {
                               if ($.parseJSON(data.d) != undefined){
                                   if ($.parseJSON(data.d)[0].ErrorNumber == 101){window.location.reload();};
                                   displayError($.parseJSON(data.d)[0].ErrorMessage); 
                                   return false
                               }                      
                           },
                   error: function (xhr, status, error) {
                            var err = $.parseJSON(xhr.responseText);                        
                            displayError('Error loading details : ' + err.Message); 
                            return false
                          }
                });
       
            divResult.css('display','block');
           
            $('#divFilterFields').removeClass();
            $('#divFilterFields').addClass('box span3');
            
            //focus tab details
            $('[href="#divUnitResultTab"]').tab('show');
            
            var tblDataSource =  $('#tblUnits').DataTable();
            tblDataSource.destroy();
            $('#tblUnits').empty();
             
            tblDataSource =  $('#tblUnits').DataTable( {
                                   ajax: { url: ajaxurl ,
                                           dataType: 'json',
                                           contentType :'application/json; charset=utf-8',
                                           type: 'POST',
                                           data:  function ( d ) {
                                                    return JSON.stringify({Mode:7,Location:DropdownLoc.val(),Project:DropdownProj.val(),Phase:DropdownPhase.val(),Block:DropdownBlock.val(),RefObj:DropdownUnit.val(),SubType:GetCheckedSubType()}) ;
                                                    },
                                           dataSrc: function (data) {
                                                            if ($.parseJSON(data.d) != undefined)
                                                            {   if ($.parseJSON(data.d)[0].ErrorNumber == 101){window.location.reload();};
                                                                displayError($.parseJSON(data.d)[0].ErrorMessage); 
                                                                return '';
                                                            }
                                                            else 
                                                            {    return data;
                                                            }}, 
                                            error: function (xhr) {
                                                    var err = $.parseJSON(xhr.responseText);
                                                    displayError('Error loading unit list : ' + err.Message)}, 
                                                    },
                                   columns:[{ data: 'Phase',
                                              title: 'Phase' },
                                            { data: 'Block',
                                              title: 'Block' },
                                            { data: 'UnitName',
                                              title: 'Unit Name' },
                                            { data: 'MarketProductType',
                                              title: 'Product Type' },
                                            { data: 'MarketProductSubType',
                                              title: 'Product Sub-Type' },
                                            { data: null,
                                              title: '',
                                              defaultContent: '<button class="btn btn-success" style="white-space: nowrap;" >Get Price</button>',
                                              targets: -1,
                                              searchable: false,
                                              sortable: false }
                                             ]
                                      });

            //Display Selected Project
            $('#spnUnitInfoProject').text($("#ddlProject_chosen > a > span").text());
            
            // Display info and price list temp computation of selected unit
            $('#tblUnits tbody').on( 'click', 'button', function () {
                    var data = tblDataSource.row( $(this).parents('tr') ).data();
                    
                    LoadUnitDetails(data.Phase,data.Block,data.UnitName,data.ReferenceObject); 
                                        
                    return false; //prevent postback
                } );
             
             // Show details if dropdown unit selected
             if (DropdownUnit.val() != null && DropdownUnit.val() != '')   
             {
                
                LoadUnitDetails($("#ddlPhase_chosen > a > span").text(),
                                $("#ddlBlock_chosen > a > span").text(),
                                $("#ddlInventoryUnit_chosen > a > span").text(),
                                DropdownUnit.val() ); 
               
             }
             
             $('html,body,window').animate({scrollTop: divResult.offsetTop}, 500);
             
        } ; 
        
        
        function LoadUnitDetails(Phase,Block,UnitName,ReferenceObject) {
        
            $('#spnUnitInfoPhase').text(Phase);
            $('#spnUnitInfoBlock').text(Block);
            $('#spnUnitInfoInventoryUnit').text(UnitName);
            
            
            $('#divOtherInfo').html('');
            $('#divPriceListInfo').html('');
            displayError(''); 
            
            // Display Temp computation 
            $.ajax({
               type: 'POST',
               url: ajaxurl,
               dataType: 'json',
               data: JSON.stringify({Mode:8,Location:'',Project:'',Phase:'',Block:'',RefObj:ReferenceObject,SubType:''}),
               contentType :'application/json; charset=utf-8',
               success: function (data) {
                           if ($.parseJSON(data.d) != undefined){
                               if ($.parseJSON(data.d)[0].ErrorNumber == 101){window.location.reload();};
                               displayError($.parseJSON(data.d)[0].ErrorMessage); 
                           }
                           else 
                           {
                              $(data).each(function (index, item) {
                                  $('#divOtherInfo').html(item.UnitInfo);
                                  $('#divPriceListInfo').html(item.PricingInfo);
                               }); 
                           };
                           
                             //focus tab details
                            $('[href="#divUnitDetailsTab"]').tab('show');
                            $('html,body,window').animate({scrollTop: divResult.offsetTop}, 500);
                            $("#divUnitDetailsTab a[data-toggle=tab]").click(function(){
                                     $($(this).attr('href')).find(".tab-pane-content").fadeOut(0).fadeIn(500);
                              });
                           
                       },
               error: function (xhr, status, error) {
                        var err = $.parseJSON(xhr.responseText);                        
                        displayError('Error loading Unit Info and Pricing Computation : ' + err.Message); 
                    }
            });
        };
        
        // add effect display tab page content, on tab change
         $("a[data-toggle=tab]").click(function(){
             $($(this).attr('href')).find(".tab-pane-content").fadeOut(0).fadeIn(500);
         });
         
          
     });  //END : document ready function () 
      
     
  
      
    </script> 
</asp:Content>
