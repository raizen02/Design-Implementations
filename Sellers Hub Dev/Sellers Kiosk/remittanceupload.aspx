<%@ Page Language="VB" 
    MasterPageFile="~/sellersHubMasterPage.master" 
    AutoEventWireup="false"
    CodeFile="remittanceupload.aspx.vb" 
    Inherits="remittanceupload" 
    Title="Re-upload Remittance - ORS | Sellers' HUB" %>

<asp:Content ID="cphContent" ContentPlaceHolderID="cphContent" runat="Server">
    <%-- File Upload CSS  --%>
    <link rel="stylesheet" href="css/jquery.fileupload.css" />
    
    <%--Format Number--%>
    <script type="text/javascript">
    
         function formatCurrency(num)
            {
                num = num.toString().replace(/\$|\,/g,'');
                
                if(isNaN(num))
                    num = "0";
                    
                sign = (num == (num = Math.abs(num)));
                num = Math.floor(num*100+0.50000000001);
                cents = num%100;
                num = Math.floor(num/100).toString();
                
                if(cents<10)
                    cents = "0" + cents;
                    
                for (var i = 0; i < Math.floor((num.length-(1+i))/3); i++)
                    num = num.substring(0,num.length-(4*i+3))+','+
                    num.substring(num.length-(4*i+3));
                    return (((sign)?'':'-')  + num + '.' + cents);
            }
    </script>
    
    <%-->>-----Loading Panel-------------%>
    <div id="divLoading" style="display: none">
        <div id="progressBackgroundFilter">
        </div>
        <div id="processMessage">
            <img alt="Loading..." src="images/loading.gif" />
        </div>
    </div>
    <%--<<-----Loading Panel-------------%>
    <div style="text-align: justify">
        <div>
            <ul class="breadcrumb">
                <li><a href="<%=MyDefaultInitialFile %>">Home</a> <span class="divider">/</span> </li>
                <li><a href="#">Remittance Re-upload</a> </li>
            </ul>
        </div>
        <%------------Alert Error Message -------------%>
        <div data-div="alert" id="divErrorMsgBox">
        </div>
        <%------------Alert Error Message -------------%>
        <%-->>---------- Seller's Remitted sales -------------%>
        <div class="row-fluid">
            <div class="box span12">
                <div class="box-header well">
                    <h2>
                        <i class="icon-list-alt"></i>&nbsp;Units Currently On-Hold</h2>
                    <div class="box-icon">
                        <a class="btn btn-minimize btn-round"><i class="icon-chevron-up"></i></a>
                    </div>
                </div>
                <div class="box-content form-horizontal">
                    <%--table--%>
                    <table id="tblSalesList" class="table table-bordered table-condensed"
                        cellspacing="0" width="100%">
                        <thead>
                            <tr>
                                <th>Loading data</th>
                                <%--<th style="display: none">Reservation #</th>
                                <th style="display: none">Reservation Amount</th>
                                <th>Date Placed On Hold</th>
                                <th>Buyer Name</th>
                                <th>Reservation #</th>
                                <th>Status Allocation</th>
                                <th>Current Hold Code</th>
                                <th>Project</th>
                                <th>Phase</th>
                                <th>Block</th>
                                <th>Lot</th>
                                <th>RU Number</th>
                                <th>Reservation Amount</th>
                                <th>Total Remitted Amount</th>
                                <th>Total Amount With OR</th>--%>
                            </tr>
                        </thead>
                        <tbody>
                        </tbody>
                    </table>
                </div>
            </div>
        </div>
        <%---<<--------- Seller's Remitted sales -------------%>
        <%------------ Remittance Information -------------%>
        <div id="divRemittance" style="display:none"  class="row-fluid">
            <div class="box span12">
                <div class="box-header well">
                    <h2>
                        <i class="icon-check"></i>&nbsp;Remittance Information</h2>
                    <div class="box-icon">
                        <a class="btn btn-minimize btn-round"><i class="icon-chevron-up"></i></a>
                    </div>
                </div>
                <div class="box-content form-horizontal">
                  <div class="control-group">
                     <%------------Alert Error Message -------------%>
                     <div data-div="alert"  id="divErrorMsgRemitInfo"> </div>
                    <%------------Alert Error Message -------------%>
                    </div>
                    
                    <%-- Buyer's Name--%>
                    <div id="divBuyerName" class="control-group">
                        <label for="txbBuyerName" class="control-label">
                            Buyer's Name</label>
                        <div class="controls">
                            <input id="txbBuyerName" name="Buyer Name" type="text" disabled="disabled" class="input-xxlarge uneditable-input" />
                        </div>
                    </div>
                    <%-- ID --%>
                    <div style="display:none">
                        <input id="txbHideCompanyCode" name="CompanyCode" type="text" disabled="disabled"/>
                        <input id="txbHideReservationNo" name="ReservationNo" type="text" disabled="disabled"/>
                    </div> 
                    <%--  Reservation Number --%>
                    <div id="divResvNumber"class="control-group">
                        <label for="txbResvNumber" class="control-label">
                            Reservation Number</label>
                        <div class="controls">
                            <input id="txbResvNumber" name="Reservation Number" type="text" disabled="disabled"
                                class="input-xxlarge uneditable-input" />
                        </div>
                    </div>
                    <%--Remittance List--%>
                    <h4>
                            <span>List of Remittance</span>
                     </h4> 
                    <table id="tblRemittanceList" class="table table-bordered table-condensed"
                        cellspacing="0" width="100%">
                        <thead>
                            <tr>
                                <th style="width: 76px"></th>
                                <th>Status</th>
                                <th>Mode of Payment</th>
                                <th>Remittance Company</th>
                                <th>Bank</th>
                                <th>Amount Paid (Php)</th>
                                <th>Date Paid</th>
                                <th>Date Created</th>
                                <th>Is Tagged</th>
                                <th>Amount Received (Php)</th>
                                <th>OR Number</th>
                                <th>OR Date Tagged</th>
                                <th>OR Tagged By</th>
                                <th>Cancelled Remarks</th>
                                <th>Cancelled By</th>  
                                <th style="display:none" >RemitCode</th> 
                                <th>Images</th> 
                            </tr>
                        </thead>
                        <tbody>
                        </tbody>
                    </table>
                    <%-- Reservation Fee --%>
                    <div class="control-group">
                        <label id="lblResFee" class="control-label">
                            Reservation Fee
                        </label>
                        <div class="controls">
                            <div class="input-prepend">
                                <span class="add-on">₱</span>                                
                                <input id="txbReservationFee" name="Reservation Fee" type="text" disabled="disabled"
                                    class="input-large uneditable-input" value="0.00" />
                                    
                            </div>
                        </div>
                    </div>
                    <%-- Total Remittance in Peso--%>
                    <div class="control-group">
                        <label for="txbTotalRemittanceList" class="control-label">
                            Total Remittance To-Date (Php)</label>
                        <div class="controls">
                            <div class="input-prepend">
                                <span class="add-on">₱</span>
                                <input id="txbTotalRemittanceList" name="Total Remittance" type="text" disabled="disabled"
                                    class="input-large uneditable-input" value="0.00" />
                            </div>
                        </div>
                    </div>
                    <%--Total Remittance with OR--%>
                    <div class="control-group">
                        <label class="control-label">
                            Total Amount (Php)&nbsp; with Official Receipt</label>
                        <div class="controls">
                            <div class="input-prepend">
                                <span class="add-on">₱</span>
                                <input id="txbTotalRemittanceWithOR" name="Total Remittance" type="text" disabled="disabled"
                                    class="input-large uneditable-input" value="0.00" />
                            </div>
                        </div>
                    </div> 
                     <div class="form-actions" style="padding-left: 0px;margin-bottom: 0px;">
                         <h4>
                                <span>Add New Remittance</span>
                         </h4>
                    </div>
                   <div class="control-group">
                        <%------------Alert Error Message -------------%>
                        <div data-div="alert" data-validgroup="remit" id="divErrorMsgBoxRemit"> </div>
                        <%------------Alert Error Message -------------%>
                    </div>
                    
                    <%--Mode of Reservation Payment--%>
                    <div class="control-group ss-item-required" data-validgroup="remit">
                        <label for="ddlModeofPayment" class="control-label">
                            Mode of Payment</label>
                        <div class="controls">
                            <select id="ddlModeofPayment" name="Mode of payment" class="chosen-select-deselect"
                                data-placeholder="Select an Option">
                                <option value=""></option>
                            </select>
                        </div>
                    </div>
                    <%--Remittance Company Name--%>
                    <div id="divRemitCompanyName" class="control-group ss-item-required" data-validgroup="remit">
                        <label for="txbCompanyName" class="control-label">
                            Remittance Company</label>
                        <div class="controls">
                            <input id="txbCompanyName" name="Remittance company" type="text" class="input-xxlarge" />
                        </div>
                    </div>
                    <%-- Bills Payment Bank--%>
                    <div id="divBillsPaymentBank" class="control-group ss-item-required" data-validgroup="remit">
                        <label for="ddlBillsPaymentBank" class="control-label">
                            Bills Payment Bank</label>
                        <div class="controls">
                            <select id="ddlBillsPaymentBank" name="Bills payment bank" class="chosen-select-deselect"
                                data-placeholder="Select an Option">
                                <option value=""></option>
                            </select>
                        </div>
                    </div>
                    <%--Destination FLI Bank--%>
                    <div id="divDestinationBank" class="control-group ss-item-required" data-validgroup="remit">
                        <label for="ddlDestinationBank" class="control-label">
                            Destination FLI Bank</label>
                        <div class="controls">
                            <select id="ddlDestinationBank" name="Destination FLI bank" class="chosen-select-deselect"
                                data-placeholder="Select an Option">
                                <option value=""></option>
                            </select>
                        </div>
                    </div>
                    <%--Amount Paid in Peso--%>
                    <div class="control-group ss-item-required" data-validgroup="remit">
                        <label for="txbRemitAmount" class="control-label">
                            Amount Paid in Peso</label>
                        <div class="controls">
                            <div class="input-prepend">
                                <span class="add-on">₱</span>
                                <input id="txbRemitAmount" name="Amount paid" type="text" data-type="number" class="input-large"
                                    onblur="this.value=formatCurrency(this.value);" />
                            </div>
                        </div>
                    </div>
                    <%--Date Paid--%>
                    <div class="control-group ss-item-required" data-validgroup="remit">
                        <label for="txbDatePaid" class="control-label">
                            Date Paid</label>
                        <div class="controls">
                            <input id="txbDatePaid" name="Date paid" type="text" data-type="date" data-placeholder="mm/dd/yyyy"
                                class="input-small datepicker" />
                        </div>
                    </div>
                    <%--Attach remittance slip--%>
                    <div class="control-group ss-item-required" data-validgroup="remit">
                        <label class="control-label">
                            Attach remittance slip</label>
                        <div class="controls">
                            <div id="fucRemittanceImage" name="Remittance slip" class="fileuploadcustom">
                            </div>
                        </div>
                    </div>
                    <%--Button AddToRemittances--%>
                    <div class="form-actions">
                        <a id="btnAddToRemittances" onclick="this.disabled=true;return false;" class="btn"><i
                            class="icon-plus"></i>Add Remittance</a>
                    </div>
                </div>
            </div>
        </div>
        <%------------ Remittance Information -------------%>
    </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphContentBottom" runat="Server">

    <script type="text/javascript">
    
        var ajaxurlGetData = 'remittanceupload.aspx/GetDataFromDB';
        var btnDeleteRow = '<a class="btn btn-danger delete"><i class="icon-trash icon-white"></i>Delete</a>'
             
        $(document).ready(function () {
            // Automatic Show/ Hide loading modal div     
            $(document).bind("ajaxSend", function(){
               $("#divLoading").show();
             }).bind("ajaxStop ", function(){
               $("#divLoading").hide();
             });
             
            // Initialize chosen css for selector
            $('.chosen-select-deselect').chosen({ allow_single_deselect: true,width:"100%" }); //,width:"95%"
       
            var DropdownModeofPayment = $('#ddlModeofPayment');
            var DropdownBillsPaymentBank = $('#ddlBillsPaymentBank');
            var DropdownDestinationBank =  $('#ddlDestinationBank');
            
            //Hide divRemit first
            $('#divRemittance').css('display','none');     
                        
            //Populate On Hold accounts
            function PopulateOnHoldList() { 
                
                //Use function datatable as datasource of tblSalesList
                var tblDataSource =  $('#tblSalesList').DataTable();
                tblDataSource.destroy();
                $('#tblSalesList').empty();
                   
                tblDataSource =  $('#tblSalesList').DataTable( {
                                       ajax: { url: ajaxurlGetData ,
                                               dataType: 'json',
                                               contentType :'application/json; charset=utf-8',
                                               type: 'POST',
                                               data:  function ( d ) {
                                                        return JSON.stringify({Mode:1,ComCode:'',ConNum:''})
                                                        },
                                               dataSrc: function (data) {
                                                            if ($.parseJSON(data.d) != undefined)
                                                            {   if ($.parseJSON(data.d)[0].ErrorNumber == 101){window.location.reload();};
                                                                displayError($.parseJSON(data.d)[0].ErrorMessage); 
                                                                return '';
                                                            }
                                                            else 
                                                            {    return data[0];
                                                            }}, 
                                               error: function (xhr) {
                                                        var err = $.parseJSON(xhr.responseText);
                                                        displayError('Error loading sales list : ' + err.Message)}, 
                                                       },
                                       columns:[ { data: 'CompanyCode',
                                                  title: 'Company Code',
                                                  visible: false },
                                                 { data: 'ReservationNo',
                                                  title: 'Reservation #' },
                                                 { data: 'DatePlacedOnHold',
                                                  title: 'Date Placed On Hold' },
                                                { data: 'BuyerName',
                                                  title: 'Buyer Name' },
                                                { data: 'StatusAllocation',
                                                  title: 'Status Allocation' },
                                                { data: 'CurrentHoldCode',
                                                  title: 'Current Hold Code' }, 
                                                { data: 'Project',
                                                  title: 'Project' },
                                                { data: 'Phase',
                                                  title: 'Phase' },
                                                { data: 'Block',
                                                  title: 'Block' },
                                                { data: 'Unit',
                                                  title: 'Unit' },
                                                { data: 'ReservationAmount',
                                                  title: 'Reservation Amount' },
                                                { data: 'TotalRemittedAmount',
                                                  title: 'Total Remitted Amount' },
                                                 { data: 'TotalAmountWithOR',
                                                  title: 'Total Amount With OR' }
                                                 ],
                                     "createdRow": function ( row, data, index ) {
                                             $(row).css('cursor','pointer');
                                            }   ,
                                      "sDom": 'lfr<"table_overflow"t>ip' 
                                     });
              
                
                // Display remittance info  on selected row                   
                $('#tblSalesList tbody').on( 'click', 'tr', function () {
                     //clear message
                     displayError('');
                     
                    //remove all selected row then apply css on selected row
                    $('#tblSalesList tbody tr').removeClass('selected');
                    $(this).addClass('selected');
                    
                    ClearField();
                    //Show and Load remittance info
                    $('#divRemittance').css('display','block');  
                    
                    var data = tblDataSource.row($(this)).data();
                    
                    //Populate exiting remittance
                    DisplayInfoOfCon(data.CompanyCode,data.ReservationNo); 
                    
                    $('#txbReservationFee').val(data.ReservationAmount);
                    $('#txbBuyerName').val(data.BuyerName);
                    $('#txbHideCompanyCode').val(data.CompanyCode);
                    $('#txbHideReservationNo').val(data.ReservationNo);
                    $('#txbResvNumber').val(data.ReservationNo);
                    $('#txbTotalRemittanceList').val(data.TotalRemittedAmount);
                    $('#txbTotalRemittanceWithOR').val(data.TotalAmountWithOR); 
                    
                    $('html,body,window').animate({scrollTop: divRemittance.offsetTop}, 500); 
                } );  
                
                //apply table css
                $('.table_overflow').css({'overflow':'auto','width':'100%'})
                
             };
            
            // Populate drop down list source 
             function PopulateDropDownSource() { 
                 $.ajax({
                           type: 'POST',
                           url: ajaxurlGetData,
                           dataType: 'json',          
                           data:  JSON.stringify({Mode:1,ComCode:'',ConNum:''}) ,
                           contentType :'application/json; charset=utf-8',
                           success: function (data) {
                                       if ($.parseJSON(data.d) != undefined){
                                           if ($.parseJSON(data.d)[0].ErrorNumber == 101){window.location.reload();};
                                           displayError($.parseJSON(data.d)[0].ErrorMessage); 
                                       }
                                       else 
                                       {   
                                           // 1 : Mode of Payment
                                           DropdownModeofPayment.empty();
                                           DropdownModeofPayment.append(new Option("", ""));
                                           $(data[1]).each(function (index, item) {
                                               DropdownModeofPayment.append(new Option(item.Description, item.Code));                       
                                           });
                                           DropdownModeofPayment.trigger("chosen:updated");
                                            
                                           // 2 : Bills Payment Bank 
                                           DropdownBillsPaymentBank.empty();
                                           DropdownBillsPaymentBank.append(new Option("", ""));
                                           $(data[2]).each(function (index, item) {
                                               DropdownBillsPaymentBank.append(new Option(item.Description, item.Code));                       
                                           });
                                           DropdownBillsPaymentBank.trigger("chosen:updated");
                                           
                                           // 3 : Destination FLI Bank
                                           DropdownDestinationBank.empty();
                                           DropdownDestinationBank.append(new Option("", ""));
                                           $(data[3]).each(function (index, item) {
                                               DropdownDestinationBank.append(new Option(item.Description, item.Code));                       
                                           });
                                           DropdownDestinationBank.trigger("chosen:updated"); 
                                             
                                        };
                                    },
                           error: function (xhr) {
                                var err = $.parseJSON(xhr.responseText);                        
                                displayError('Error loading dropdown : ' + err.Message); 
                           }
                      });
             };
             
             function ClearField()
             {
                // clear fields
                DropdownModeofPayment.val('');
                DropdownModeofPayment.trigger("chosen:updated");
                DropdownBillsPaymentBank.val('');
                DropdownBillsPaymentBank.trigger("chosen:updated");
                DropdownDestinationBank.val('');
                DropdownDestinationBank.trigger("chosen:updated");
                $('#txbCompanyName').val('');
                $('#txbRemitAmount').val('');
                $('#txbDatePaid').val(''); 
                
                // clear selected file 
                FUC_ClearAllFiles('fucRemittanceImage');
             
             };
            
            // Display Error Message  
            function displayError(error, divID, cssAlert) {
                  // clear all display error message
                  $('[data-div="alert"]').empty();
                  
                  if (error == '')
                  {return};
                  
                  // get source div to display
                  var DivToDisplay = divID ? document.getElementById(divID) : document.getElementById('divErrorMsgBox') ;
                  
                  // div Message style
                  var cssMsg = cssAlert ? cssAlert : 'alert-error';
                  
                  //create div alert message
                  var divError =  $('<div/>').addClass('alert ' + cssMsg).html(error);
                  
                  // append error message to source div
                  $(DivToDisplay).append(divError); 
                  $('html,body,window').animate({scrollTop: DivToDisplay.offsetTop}, 500); 
            };
            
             
             // IsDate Validate function
            function isValidDateFormat(inputDate){
                var validformat=/^\d{2}\/\d{2}\/\d{4}$/; //Basic check for format validity
                var returnval='';
                var minYear = 1902;
                var maxYear = 9999;
                
                if (!validformat.test(inputDate))
                {  returnval = "invalid date format"
                }
                else{ //Detailed check for valid date ranges
                    
                    var monthfield=inputDate.split("/")[0]
                    var dayfield=inputDate.split("/")[1]
                    var yearfield=inputDate.split("/")[2]
                    
                    if(dayfield < 1 || dayfield > 31) {
                      returnval = "invalid value for day: " + dayfield;
                    } else if(monthfield < 1 || monthfield > 12) {
                      returnval = "invalid value for month: " + monthfield ;
                    } else if(yearfield < minYear || yearfield > maxYear) {
                      returnval = "invalid value for year: " + yearfield + " - must be between " + minYear + " and " + maxYear;
                    } 
                } 
                return returnval
            }
 
            String.prototype.replaceAll = function(target, replacement) {
              return this.split(target).join(replacement);
            };
 
            
            // Is Email valid function            
            function isEmailValid(email){
                var filter=/^([\w-]+(?:\.[\w-]+)*)@((?:[\w-]+\.)*\w[\w-]{0,66})\.([a-z]{2,6}(?:\.[a-z]{2})?)$/i
                
                if (filter.test(email))
                     { return true }
                else { return false };
            }

              // Validate User Entry   
             function formIsValid(valigGroupName) { 
             
               var fields = $('[data-validgroup="' + valigGroupName + '"]')
                   .filter(".ss-item-required")
                   .filter(":visible" )    
                   .find("select, textarea, input[type!='file']");
               
               var error  = ''
               
               $.each(fields, function(i, field) {
                  // validate each field required field and data type is valid 
                  var name = $(field).attr('name');
                  var value = $(field).val();
                  
                  if (!(!name)) //<-- valid field, should name exists
                  {   if (!value)
                      {error = error + name + ' is required' + '<br/>'
                      }
                      else if ($(field).data('type') == 'number')
                      {  if (!$.isNumeric(value.replaceAll(',','')))
                         { error = error + name + ' should be numeric' + '<br/>'}
                         else if (value <= 0)
                         { error = error + name + ' should be greater than zero' + '<br/>' };
                      }
                      else if ($(field).data('type') == 'date')
                      {   var dateErrMsg = isValidDateFormat(value);
                          if (dateErrMsg != '')
                          {  error = error + name + ' ' + dateErrMsg + '<br/>'};
                      }
                      else if ($(field).data('type') == 'email')
                      {   if (!isEmailValid(value))
                          {  error = error + name + ' invalid email format' + '<br/>'};
                      };
                  }
                  
               }); 
               
               // Validate custom fileupload
               var fields = $('[data-validgroup="' + valigGroupName + '"]')
                   .filter(".ss-item-required")
                   .filter(":visible" )    
                   .find(".fileuploadcustom");
                   
                $.each(fields, function(i, field) {
                  // validate each field required field and data type is valid 
                  var name = $(field).attr('name');
                  var fuID = field.id;
                  
                  if (!(!name)) //<-- valid field, should name exists
                  {  var imageslist = JSON.parse(FUC_GetImagesJSON(fuID));
                     if (imageslist.length <= 0)
                     {  error = error + name + ' upload at least one image' + '<b-r/>'
                     }; 
                  };
               }); 
                   
               var divAlertID = $('[data-validgroup="' + valigGroupName + '"]').filter('[data-div="alert"]').attr('id');
                   
               displayError(error,divAlertID); 
                   
               return (error == ''); 
            };
            
            
            function DisplayInfoOfCon(comcode, contcode)
            {   
                $.ajax({
                       type: 'POST',
                       url: ajaxurlGetData,
                       dataType: 'json',          
                       data:  JSON.stringify({Mode:2,ComCode:comcode,ConNum:contcode}),
                       contentType :'application/json; charset=utf-8',
                       success: function (data) {
                               if ($.parseJSON(data.d) != undefined){
                                   if ($.parseJSON(data.d)[0].ErrorNumber == 101){window.location.reload();};
                                   displayError($.parseJSON(data.d)[0].ErrorMessage); 
                               }
                               else 
                               {    // 0 : Populate existing remittance
                                   if ($(data[0]).length > 0)
                                   {  
                                   
                                    //,Delete
                                    //,ModeofPayment
                                    //,CompanyName
                                    //,BankBranch
                                    //,AmountPaid
                                    //,Date Paid
                                    //,Date Created
                                    //,is Tagged
                                    //,Status
                                    //,AmountReceived
                                    //,ORNumber
                                    //,ORNumberDateTagged
                                    //,ORNumberTaggedBy
                                    //,CancelledRemarks
                                    //,CancelledBy
                                    //,RemittanceReferenceCode
                                   
                                       $('#tblRemittanceList').css('display','block');
                                       $('#tblRemittanceList tbody').empty();
                                             
                                       var trRow = ''     
                                       $(data[0]).each(function (index, item) {
                                       
                                         
                                          // generate imagelist, filter by remittance code
                                          var remitcode = item.RemittanceReferenceCode;
                                          var liPix = '';          
                                          $(data[1]).each(function (index2,item2){
                                               if (remitcode == item2.RemittanceReferenceCode)
                                                  {liPix = liPix 
                                                       + '<li class="thumbnail" style="margin-bottom: 5px !important">' 
                                                       + '<img src="' + item2.UrlFileName + '" ' 
                                                       + ' title="' + item2.FileName + '" ' 
                                                       + 'style="width:70px; height:70px;" /></li>';
                                                       };
                                          });
                                          var ulGallery = '';
                                          if ( liPix != '')
                                          {   var ulGallery =  '<ul class="thumbnails gallery">' + liPix + '</ul>';
                                          };
                                          
                                          var btnDeleteRowFinal = ''
                                          if (item.isTagged == false)
                                          { btnDeleteRowFinal = btnDeleteRow
                                          } 
                                          
                                          // generate html tablerow
                                           trRow = trRow +'<tr>' + '<td>' + btnDeleteRowFinal + '</td>' 
                                                           + '<td class="Status">' + item.Status + '</td>' 
                                                           + '<td>' + item.ModeofPayment + '</td>' 
                                                           + '<td>' + item.CompanyName + '</td>' 
                                                           + '<td>' + item.BankBranch + '</td>'
                                                           + '<td class="AmountPaid" >' + item.AmountPaid + '</td>' 
                                                           + '<td>' + item.DatePaid + '</td>' 
                                                           + '<td>' + item.DateCreated + '</td>' 
                                                           + '<td>' + item.isTagged + '</td>' 
                                                           + '<td class="AmountReceived"  >' + item.AmountReceived + '</td>' 
                                                           + '<td>' + item.ORNumber + '</td>' 
                                                           + '<td>' + item.ORNumberDateTagged + '</td>' 
                                                           + '<td>' + item.ORNumberTaggedBy + '</td>' 
                                                           + '<td>' + item.CancelledRemarks + '</td>' 
                                                           + '<td>' + item.CancelledBy + '</td>' 
                                                           + '<td class="RemittanceReferenceCode"  style="display:none" >' + item.RemittanceReferenceCode + '</td>' 
                                                           + '<td class="ImagesList" >' + ulGallery + '</td>' 
                                                    + '</tr>'
                                                    
                                                    
                                       }); 
                                       $('#tblRemittanceList tbody').append(trRow);
                                       
                                       // compute total based on remittance list 
                                       ComputeTotalRemittance();
                                       
                                   };  
                                };
                            },
                       error: function (xhr) {
                            var err = $.parseJSON(xhr.responseText);                        
                            displayError('Error loading Location : ' + err.Message); 
                       }
                  });
            }; 
            
             // Remit : Mode of payment changed
             DropdownModeofPayment.change(function () {
                    $('#divRemitCompanyName').css('display','none');
                    $('#divDestinationBank').css('display','none');
                    $('#divBillsPaymentBank').css('display','none');
                    
                    if (DropdownModeofPayment.val() == ''){return false};
                       
                    if (DropdownModeofPayment.val() == 'BILLS')
                    {   $('#divBillsPaymentBank').css('display','block');                    
                    }; 
                     
                    if (DropdownModeofPayment.val() == 'REMITTANCE')
                    {   $('#divRemitCompanyName').css('display','block');
                        $('#divDestinationBank').css('display','block'); 
                    };   
             }); 
             
             
             // Add Remittance
             $('#btnAddToRemittances').click(function () {
             
                $('#btnAddToRemittances').disabled=true; 
                 
                // validate required field 
                if (formIsValid('remit') == false)
                   {$('#btnAddToRemittances').disabled=false; 
                   return false;};
                                   
                var ModePaymentCode =  DropdownModeofPayment.val();
                var ModePaymentName =  DropdownModeofPayment.find('option:selected').text();
                
                var RemitCompanyName = '';
                var BankCode = '';
                var BankName = '';
                
                if (ModePaymentCode == 'BILLS')
                {   BankCode =  DropdownBillsPaymentBank.val();
                    BankName =  DropdownBillsPaymentBank.find('option:selected').text();
                }; 
                
                if (ModePaymentCode == 'REMITTANCE')
                {   RemitCompanyName = $('#txbCompanyName').val();
                    BankCode =  DropdownDestinationBank.val();
                    BankName =  DropdownDestinationBank.find('option:selected').text();
                }; 
                 
                // Save Remittance
                // Generate xml for arguments
                 var XML = new XMLWriter();
                 // Start with beginnode
                 XML.BeginNode("Dataset");
                     //Remittance Info
                     // one row only... 
                     XML.BeginNode("RemitInfo"); //Table : RemitInfo
                       // Columns : 
                       XML.Node("CompanyCode", $('#txbHideCompanyCode').val());  
                       XML.Node("ContractNumber", $('#txbHideReservationNo').val());  
                       XML.Node("ModePaymentCode", ModePaymentCode);  
                       XML.Node("BankCode", BankCode); 
                       XML.Node("RemitCompanyName", RemitCompanyName); 
                       XML.Node("RemitAmount", $('#txbRemitAmount').val()); 
                       XML.Node("DatePaid", $('#txbDatePaid').val()); 
                    XML.EndNode(); //End Table : RemitInfo
                                 
                    //Remittance Images  
                    var imageslist = JSON.parse(FUC_GetImagesJSON('fucRemittanceImage'));
                    $(imageslist).each(function (index,item){
                        XML.BeginNode("RemitImages"); //Table : RemitImages
                           // Columns : 
                           XML.Node("src", item.ImageSrc);  
                           XML.Node("title",  item.ImageName); 
                           XML.Node("savedname", item.ImageSavedName);  
                        XML.EndNode(); //End Table : RemitImages
                      }); 
                    
                 XML.Close();//End Dataset or EndNode()
                 
                  // Save using web method
                 $.ajax({
                    url:'remittanceupload.aspx/SaveData',
                    type: "Post",
                    data: "{'Data': '" + XML.XML.join("") + "'}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (data) {
                              if ($.parseJSON(data.d) != undefined){
                                   //Error
                                   if ($.parseJSON(data.d)[0].ErrorNumber == 101){window.location.reload();};
                                  
                                   if ($.parseJSON(data.d)[0].ErrorNumber == 8888)
                                   { //Success
                                       displayError($.parseJSON(data.d)[0].ErrorMessage,'divErrorMsgRemitInfo','alert-success'); 
                                      
                                      //****Refresh sales list, remittace list and total amount****
                                      
                                       var CompanyCode = $('#txbHideCompanyCode').val();
                                       var ReservationNo = $('#txbHideReservationNo').val();
                                       
                                       // Load first sales list, this will create new row and sort and filter cleared also
                                       PopulateOnHoldList();
                                        //Populate existing remittance
                                        DisplayInfoOfCon(CompanyCode,ReservationNo); 

                                        ClearField(); 
                                   }
                                   else
                                   { displayError($.parseJSON(data.d)[0].ErrorMessage,'divErrorMsgBoxRemit'); 
                                   }
                                   
                                 }
                             },
                    error: function (xhr) {
                            var err = $.parseJSON(xhr.responseText);                        
                            displayError('Error while saving remittance : ' + err.Message,'divErrorMsgBoxRemit'); 
                       },
                    complete: function () {$('#btnAddToRemittances').disabled=false;}
                 });  

                $('#btnAddToRemittances').disabled=false;    
                return false; 
             }); 
             
             //**** Delete Remittance ********
             // Bind table row click for delete button
             $('table').on('click', 'td .delete', function (event) {
                var isConfirm = confirm('Are you sure you want to delete this record?');
                
                // validate if final answer
                if (isConfirm == false)
                { return false
                }; 
                
                var  RemittanceReferenceCode =  $(this).closest('tr').find('.RemittanceReferenceCode').html()
           
                 // Save using web method
                 $.ajax({
                    url:'remittanceupload.aspx/DeleteData',
                    type: "Post",
                    data: "{'RemitID': '" + RemittanceReferenceCode + "'}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (data) {
                              if ($.parseJSON(data.d) != undefined){
                                   //Error
                                   if ($.parseJSON(data.d)[0].ErrorNumber == 101){window.location.reload();};
                                  
                                   if ($.parseJSON(data.d)[0].ErrorNumber == 8888)
                                   { //Success
                                   
                                       // remove row
                                       $(this).closest('tr').fadeTo(400, 0, function () { 
                                            $(this).remove(); 
                                       });
                                        
                                       displayError($.parseJSON(data.d)[0].ErrorMessage,'divErrorMsgRemitInfo','alert-success'); 
                                      
                                      //**** Recompute total amount****
                                      
                                       var CompanyCode = $('#txbHideCompanyCode').val();
                                       var ReservationNo = $('#txbHideReservationNo').val();
                                       
                                       // Load first sales list, this will create new row and sort and filter cleared also
                                       PopulateOnHoldList();
                                        //Populate existing remittance
                                        DisplayInfoOfCon(CompanyCode,ReservationNo); 
                                        ClearField(); 
                                   }
                                   else
                                   { displayError($.parseJSON(data.d)[0].ErrorMessage,'divErrorMsgRemitInfo'); 
                                   }
                                   
                                 }
                             },
                    error: function (xhr) {
                            var err = $.parseJSON(xhr.responseText);                        
                            displayError('Error while deleting remittance : ' + err.Message,'divErrorMsgRemitInfo'); 
                       },
                    complete: function () {$('#btnAddToRemittances').disabled=false;}
                 });  
                  
                return false;
            });  
            
            
            function ComputeTotalRemittance()
            { var totalAmountPaid = 0;   
              var totalAmountReceived = 0;   
              $('#tblRemittanceList tbody tr').each(function (index,item){
                    var Status =  $(item).find('.AmountPaid').html() ;
                    if (Status != 'Cancelled')
                    { totalAmountPaid = parseFloat(totalAmountPaid) +  parseFloat($(item).find('.AmountPaid').html().replace(',',''))
                      totalAmountReceived = parseFloat(totalAmountReceived) +  parseFloat($(item).find('.AmountReceived').html().replace(',',''))
                    }
              });
              
              $('#txbTotalRemittanceList').val(formatCurrency(totalAmountPaid));
              $('#txbTotalRemittanceWithOR').val(formatCurrency(totalAmountReceived)); 
                        
            };
            
             
            //---------------- Initialize ---------------
            // Load initial display
             PopulateOnHoldList();
             PopulateDropDownSource();
        }); // end doc ready   
        
    </script>

    <!--js required for File upload >
    <!-- The jQuery UI widget factory, can be omitted if jQuery UI is already included -->

    <script type="text/javascript" src="js/jquery.ui.widget.js"></script>

    <script type="text/javascript" src="js/jquery.fileupload.js"></script>

    <script type="text/javascript" src="js/fileupload.custom.js"></script>

    <script type="text/javascript" src="js/XMLWriter.js"></script>

    <!--ref: http://www.codeproject.com/Articles/12504/Writing-XML-using-JavaScript -->
</asp:Content>
