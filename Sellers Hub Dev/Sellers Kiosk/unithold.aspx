<%@ Page Language="VB"
    MasterPageFile="~/sellersHubMasterPage.master"
    AutoEventWireup="false"
    CodeFile="unithold.aspx.vb"
    Inherits="unithold"
    Title="Unit Hold - ORS | Sellers' HUB"%>

<asp:Content id="cphContent" ContentPlaceHolderid="cphContent" runat="Server">

    
    <link  rel="stylesheet" href="css/jquery.fileupload.css"/>
 
    
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
    
     <%--<script type="text/javascript">
        function ScrollDiv(aid)
            {
               var aTag = $("a[name='"+ aid +"']");
               $('html,body,window').animate({scrollTop: aTag.offset().top}, 1000);
            }
    </script>--%>
   
    
     <%------------ Loading Div -------------%>
    <div id="divLoading" style="display:none">
        <div id="progressBackgroundFilter"></div>
        <div id="processMessage">
            <img alt="Loading..." src="images/loading.gif" />
        </div>
    </div>
    
    <div style="text-align:justify">
        
        <div>
            <ul class="breadcrumb">
                <li><a href="">Home</a> <span class="divider">/</span> </li>
                <li><a href="#">Unit Hold</a> 
                 </li>
            </ul>
        </div>
     
     
        <%------------Alert Error Message -------------%>
        <%--<div class="alert alert-error" id="divErrorMsgBox" style="display:none">
            <button type="button" class="close" data-dismiss="alert">
                ×</button>
            <div id="divErrorMsg">
            </div>
        </div>--%>
        
        <div data-div="alert"  data-validgroup="hold" id="divErrorMsgBox"></div>
        <%------------Alert Error Message -------------%>

         
        <div id="divMain">
            <%------------  Status Allocation -------------%>
            <div id="divAllocation" class="row-fluid">
                 <div class="box span12">
                    <div class="box-header well">
                        <h2>
                            <i class="icon-plane"></i>&nbsp;Status Allocation</h2>
                        <div class="box-icon">
                            <a href="#" class="btn btn-minimize btn-round"><i class="icon-chevron-up"></i></a>
                        </div>
                    </div> 
                    <div class="box-content form-horizontal">
                        <%--Status Allocation--%>
                        <div class="control-group  ss-item-required" data-validgroup="hold" >
                            <label for="ddlAllocation" class="control-label" >Status Allocation</label>
                            <div class="controls">
                                <select id="ddlAllocation" name="status allocation" data-rel="chosen" class="chosen-select-deselect" data-placeholder="Select an Option"  >
                                    <option value=""></option>
                                </select> 
                            </div>
                        </div>                        
                    </div>
                </div>
            </div>
            <%------------ Status Allocation-------------%>
            
            <%------------Buyer's Personal Information-------------%>
            <div id="divPersonalInformation"class="row-fluid">
               <div class="box span12">
                    <div class="box-header well">
                        <h2>
                            <i class="icon-user"></i>&nbsp;Buyer's Personal Information</h2>
                        <div class="box-icon">
                            <a href="#" class="btn btn-minimize btn-round"><i class="icon-chevron-up"></i></a>
                        </div>
                    </div>
                    <div class="box-content form-horizontal">
                        <%--Last Name--%>
                        <div class="control-group ss-item-required" data-validgroup="hold" >
                            <label for="txbLastName" class="control-label" >Last Name</label>
                            <div class="controls"> 
                                <input id="txbLastName" name="Last name" type="text" class="input-xxlarge"/>
                            </div> 
                        </div>  
                             
                        <%--First Name--%>
                        <div class="control-group ss-item-required" data-validgroup="hold">
                            <label for="txbFirstName" class="control-label" >First Name</label>
                            <div class="controls"> 
                                <input id="txbFirstName" name="First name" type="text" class="input-xxlarge"/>
                            </div> 
                        </div>  
                             
                        <%--Middle Name--%>
                        <div class="control-group ss-item-required" data-validgroup="hold">
                            <label for="txbMiddleName" class="control-label" >Middle Name</label>
                            <div class="controls"> 
                                <input id="txbMiddleName" name="Middle name" type="text" class="input-xxlarge"/>
                            </div> 
                        </div>       
                              
                        <%--Birth Date--%>
                        <div class="control-group ss-item-required" data-validgroup="hold">
                            <label for="dtpBirthDay" class="control-label" >Birth Date</label>
                            <div class="controls"> 
                                <input id="dtpBirthDay" name="Birth date"  type="text" data-type="date"   class="input-small datepicker" />
                            </div> 
                        </div>   
                        
                        <%--Email Address--%>
                        <div class="control-group ss-item-required"  data-validgroup="hold">
                            <label for="txbEmailAddress" class="control-label" >Email Address</label>
                            <div class="controls"> 
                                <input id="txbEmailAddress" name="Email address" type="email" data-type="email"  class="input-xxlarge" />
                            </div> 
                        </div>  
                    </div>
                </div>
            </div>
            <%------------Buyer's Personal Information-------------%>
            
            <%------------ Buyer's Contact Number(s)-------------%>
            <div id="divBuyerContact" class="row-fluid" >
                 <div class="box span12">
                    <div class="box-header well">
                        <h2>
                            <i class="icon-list-alt"></i>&nbsp;Buyer's Contact Number(s)</h2>
                        <div class="box-icon">
                            <a class="btn btn-minimize btn-round"><i class="icon-chevron-up"></i></a>
                        </div>
                    </div>
                    <div class="box-content form-horizontal">
                        <div class="control-group">
                            <%------------Alert Error Message -------------%>
                            <div data-div="alert"  data-validgroup="contact" id="divErrorMsgBoxContact"></div>
                            <%------------Alert Error Message -------------%>
                        </div>                                    
                               
                        <%-- Contact No. Type--%>
                        <div id="divContactType" data-validgroup="contact" class="control-group ss-item-required">
                            <label class="control-label">Contact No. Type</label>
                            <div class="controls"> 
                                <select id="ddlcontacttype" name="Contact type"  class="chosen-select-deselect" data-placeholder="Select an Option"  >
                                    <option value=""></option>
                                </select>
                            </div>
                        </div>
                        
                         <%-- Area Code--%>
                        <div id="divAreaCode" data-validgroup="contact" class="control-group ss-item-required">
                            <label for="txbAreaCode" class="control-label">Area Code</label>
                            <div class="controls"> 
                                <input id="txbAreaCode" name="Area code" type="text" data-type="number" class="input-small" />
                            </div>
                        </div>
                        
                        <%-- Contact Number--%>
                        <div id="divContactNumber" data-validgroup="contact" class="control-group ss-item-required">
                            <label for="txbcontactnumber" class="control-label">Contact Number</label>
                            <div class="controls">
                                <input id="txbContactNumber" name="Contact number" type="text" data-type="number"  class="input-xxlarge" />
                            </div>
                        </div> 
                        
                        <%-- Add Button Contact Number--%>
                        <div class="form-actions">
                            <a id="btnAddBuyerContact" onclick="this.disabled=true;return false;"  class="btn"><i class="icon-plus"></i> Add Contact Number</a> 
                        </div>
                        
                        <%-- Contact Number List --%>
                        <div id="divContactNumberList" class="well" >
                            <h4>
                                <span>List of Contact Number(s)</span>
                            </h4>
                            <%--table--%>   
                            <table id="tblContactNumberList" class="table table-bordered table-striped table-condensed" cellspacing="0" width="100%">
                                <thead>
                                    <tr>
                                        <th style="width:76px"></th>
                                        <th style="display:none" >Type Code</th>
                                        <th>Contact Type</th>
                                        <th>Contact Number</th> 
                                    </tr>
                                </thead> 
                                <tbody ></tbody>
                            </table>
                        </div>
                    </div>
                </div>
            </div>
            <%------------ Buyer's Contact Number(s)-------------%>
            
            <%------------ Unit Information-------------%>
            <div id="divUnitInformation" class="row-fluid">
                 <div class="box span12">
                    <div class="box-header well">
                        <h2>
                            <i class="icon-home"></i>&nbsp;Unit Information</h2>
                        <div class="box-icon">
                            <a class="btn btn-minimize btn-round"><i class="icon-chevron-up"></i></a>
                        </div>
                    </div>
                    <div class="box-content form-horizontal">
                        <%--Project--%>
                        <div data-validgroup="hold" class="control-group ss-item-required">
                            <label for="ddlProjects" class="control-label">Project</label>
                            <div class="controls">
                                <select id="ddlProjects" name="Project"  class="chosen-select-deselect" data-placeholder="Select an Option"  >
                                    <option value=""></option>
                                </select>
                            </div>
                        </div>
                            
                        <%--PhaseBuilding--%>
                        <div id="divPhaseBuilding" style="display:none" data-validgroup="hold"  class="control-group ss-item-required">
                            <label id="lblPhase" for="ddlPhase" class="control-label">Phase / Building</label>
                            <div class="controls">
                                <select id="ddlPhase" name="Phase"  class="chosen-select-deselect" data-placeholder="Select an Option"  >
                                    <option value=""></option>
                                </select> 
                            </div>
                        </div> 
                        
                        <%--BlockFloorCluster--%>
                        <div id="divBlockFloorCluster" style="display:none" data-validgroup="hold"  class="control-group ss-item-required">
                            <label id="lblBlock" for="ddlBlock" class="control-label">Block / Floor / Cluster</label>
                            <div class="controls">
                                <select id="ddlBlock" name="Block"  class="chosen-select-deselect" data-placeholder="Select an Option"  >
                                    <option value=""></option>
                                </select> 
                            </div>
                        </div>
                        
                        <%--Lot / Unit--%>
                        <div id="divLotUnit" style="display:none" data-validgroup="hold"  class="control-group ss-item-required">
                            <label id="lblLotUnit" for="ddlUnit" class="control-label">Lot / Unit</label>
                            <div class="controls">
                                <select id="ddlUnit" name="Lot / unit"  class="chosen-select-deselect" data-placeholder="Select an Option"  >
                                    <option value=""></option>
                                </select> 
                            </div> 
                        </div>  
                        
                        <%--Adjoining Unit--%>
                        <div id="divAdjoinUnit" style="display:none" class="control-group">
                            <label id="lblAdjoinUnit" class="control-label">Adjoining Unit</label>
                            <%--table--%>
                            <div class="controls">
                                <table id="tblAdjoiningUnit" class="table table-bordered table-striped table-condensed" cellspacing="0" width="100%">
                                    <thead>
                                        <tr>
                                            <th>Block/Floor</th>
                                            <th>Unit Name</th>
                                            <th>Unit Type</th>
                                            <th>Gross Area</th>
                                            <th>ReferenceObject</th>
                                        </tr>
                                    </thead> 
                                    <tbody></tbody> 
                                </table>
                            </div> 
                        </div> 
                    
                        <%--House Model--%>
                        <div id="divHouseModel" style="display:none" class="control-group">
                            <label for="txbHouseModel" class="control-label">House Model</label>
                            <div class="controls">
                                <input id="txbHouseModel" name="House model" type="text" disabled="disabled" class="input-xxlarge uneditable-input" />
                            </div> 
                        </div> 
                        
                        <%--Scheme Type--%>
                        <div id="divSchemeType" style="display:none" data-validgroup="hold"  class="control-group ss-item-required">
                            <label id="lblSchemeType" for="ddlSchemeType" class="control-label">Scheme Type</label>
                            <div class="controls">
                                <select id="ddlSchemeType" name="Scheme type"  class="chosen-select-deselect" data-placeholder="Select an Option"  >
                                    <option value=""></option>
                                </select>  
                            </div> 
                        </div>
                        
                        <%--Payment Option--%>
                        <div id="divPaymentOption" style="display:none" data-validgroup="hold"  class="control-group ss-item-required">
                            <label id="lblPaymentOption" for="ddlPaymentOption" class="control-label">Payment Option</label>
                            <div class="controls">
                                <select id="ddlPaymentOption" name="Payment option"  class="chosen-select-deselect" data-placeholder="Select an Option"  >
                                    <option value=""></option>
                                </select>  
                            </div> 
                        </div> 
                            
                        <%-- Preferred Due Date--%>
                        <div id="divPreferedDueDate" style="display:none" data-validgroup="hold"  class="control-group ss-item-required">
                            <label id="lblPreferredDueDate" for="ddlPreferredDueDate" class="control-label">Preferred Due Date</label>
                            <div class="controls">
                                <select id="ddlPreferredDueDate" name="Preferred due date"  class="chosen-select-deselect" data-placeholder="Select an Option"  >
                                    <option value="" data-num="0" ></option>
                                    <option value="1st" data-num="1" >1st</option>
                                    <option value="2nd" data-num="2" >2nd</option>
                                    <option value="3rd" data-num="3" >3rd</option>
                                    <option value="4th" data-num="4" >4th</option>
                                    <option value="5th" data-num="5" >5th</option>
                                    <option value="6th" data-num="6" >6th</option>
                                    <option value="7th" data-num="7" >7th</option>
                                    <option value="8th" data-num="8" >8th</option>
                                    <option value="9th" data-num="9" >9th</option>
                                    <option value="10th" data-num="10" >10th</option>
                                    <option value="11th" data-num="11" >11th</option>
                                    <option value="12th" data-num="12" >12th</option>
                                    <option value="13th" data-num="13" >13th</option>
                                    <option value="14th" data-num="14" >14th</option>
                                    <option value="15th" data-num="15" >15th</option>
                                    <option value="16th" data-num="16" >16th</option>
                                    <option value="17th" data-num="17" >17th</option>
                                    <option value="18th" data-num="18" >18th</option>
                                    <option value="19th" data-num="19" >19th</option>
                                    <option value="20th" data-num="20" >20th</option>
                                    <option value="21st" data-num="21" >21st</option>
                                    <option value="22nd" data-num="22" >22nd</option>
                                    <option value="23rd" data-num="23" >23rd</option>
                                    <option value="24th" data-num="24" >24th</option>
                                    <option value="25th" data-num="25" >25th</option>
                                    <option value="26th" data-num="26" >26th</option>
                                    <option value="27th" data-num="27" >27th</option>
                                    <option value="28th" data-num="28" >28th</option>
                                    <option value="29th" data-num="29" >29th</option>
                                    <option value="30th" data-num="30" >30th</option>
                                </select> 
                            </div> 
                        </div>
                                    
                        <%--Reservation Fee--%>
                        <div id="divReservationFee" style="display:none" class="control-group">
                            <label for="txbReservationFee" class="control-label">Reservation Fee</label>
                            <div class="controls">
                                <div class="input-prepend">
                                    <span class="add-on">₱</span>
                                    <input id="txbReservationFee" disabled="disabled" class="input-large uneditable-input" value="0.00" />
                                </div>
                            </div>
                        </div>
                         
                        <%--Promo Code--%>
                        <div id="divPromoCode" style="display:none" class="control-group">
                            <label for="lblPromoCode" class="control-label">Promo Code</label>
                            <div class="controls">
                                <label id="lblPromoCode" class="input-large uneditable-input"></label>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <%------------ Unit Information -------------%>
            
            <%------------  Sales Location -------------%>
            <div id="divSalesLocation" class="row-fluid">
                 <div class="box span12">
                    <div class="box-header well">
                        <h2>
                            <i class="icon-globe"></i>&nbsp;Sales Location</h2>
                        <div class="box-icon">
                            <a class="btn btn-minimize btn-round"><i class="icon-chevron-up"></i></a>
                        </div>
                    </div>
                    <div class="box-content form-horizontal">
                        <%--Sales Country--%>
                        <div  class="control-group ss-item-required" data-validgroup="hold" >
                            <label for="ddlSalesCountry" class="control-label">Sales Country</label>
                            <div class="controls">
                                <select id="ddlSalesCountry" name="Sales country"  class="chosen-select-deselect" data-placeholder="Select an Option"  >
                                    <option value=""></option>
                                </select>  
                            </div>
                        </div>
                        <%--Sales Office --%>
                        <div id="divSalesOffice" style="display:none" data-validgroup="hold"   class="control-group ss-item-required">
                            <label  for="ddlSalesOffice" class="control-label">Sales Office</label>
                            <div class="controls">
                                <select id="ddlSalesOffice" name="Sales office"  class="chosen-select-deselect" data-placeholder="Select an Option"  >
                                    <option value=""></option>
                                </select>
                            </div>
                        </div> 
                    </div>
                </div>
            </div>
            <%------------ Sales Location -------------%>
            
            <%------------ Remittance Information -------------%>
            <div id="divRemittance"  class="row-fluid">
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
                            <%--<div class="alert alert-error" id="divErrorMsgBoxRemit" style="display:none">
                                <button type="button" class="close" data-dismiss="alert">×</button>
                                <div id="divErrorMsgRemit" ></div>
                            </div>--%>
                            <div data-div="alert"  data-validgroup="remit" id="divErrorMsgBoxRemit"></div>
                            
                            <%------------Alert Error Message -------------%>
                        </div>
                        
                        <%--Mode of Reservation Payment--%>
                        <div class="control-group ss-item-required" data-validgroup="remit" >
                            <label  for="ddlModeofPayment" class="control-label">Mode of Payment</label>
                            <div class="controls">
                                <select id="ddlModeofPayment" name="Mode of payment"  class="chosen-select-deselect" data-placeholder="Select an Option"  >
                                    <option value=""></option>
                                </select>
                            </div>
                        </div>
                        
                        <%--Remittance Company Name--%>
                        <div id="divRemitCompanyName" class="control-group ss-item-required" data-validgroup="remit">
                            <label  for="txbCompanyName" class="control-label">Remittance Company</label>
                            <div class="controls">
                                <input id="txbCompanyName" name="Remittance company" type="text" class="input-xxlarge" />
                            </div>
                        </div>
                        
                        <%-- Bills Payment Bank--%>
                        <div id="divBillsPaymentBank" class="control-group ss-item-required" data-validgroup="remit">
                            <label  for="ddlBillsPaymentBank" class="control-label">Bills Payment Bank</label>
                            <div class="controls">
                                <select id="ddlBillsPaymentBank" name="Bills payment bank"  class="chosen-select-deselect" data-placeholder="Select an Option"  >
                                    <option value=""></option>
                                </select>
                            </div>
                        </div>
                        
                        <%--Destination FLI Bank--%>
                        <div id="divDestinationBank" class="control-group ss-item-required" data-validgroup="remit">
                            <label  for="ddlDestinationBank" class="control-label">Destination FLI Bank</label>
                            <div class="controls">
                                <select id="ddlDestinationBank" name="Destination FLI bank"  class="chosen-select-deselect" data-placeholder="Select an Option"  >
                                    <option value=""></option>
                                </select>
                            </div>
                        </div>
                        
                        <%--Amount Paid in Peso--%>
                        <div class="control-group ss-item-required" data-validgroup="remit">
                            <label  for="txbRemitAmount" class="control-label">Amount Paid in Peso</label>
                            <div class="controls">
                                <div class="input-prepend">
                                    <span class="add-on">₱</span>
                                    <input id="txbRemitAmount" name="Amount paid" type="text" data-type="number" class="input-large" onblur="this.value=formatCurrency(this.value);" />
                                </div>
                            </div> 
                        </div>
                        
                        <%--Date Paid--%>
                        <div class="control-group ss-item-required" data-validgroup="remit" >
                            <label for="txbDatePaid" class="control-label" >Date Paid</label>
                            <div class="controls">
                                <input id="txbDatePaid" name="Date paid" type="text" data-type="date" data-placeholder="mm/dd/yyyy"  class="input-small datepicker" />
                            </div> 
                        </div>
                        
                        <%--Attach remittance slip--%>
                        <div class="control-group ss-item-required" data-validgroup="remit">
                            <label class="control-label">Attach remittance slip</label>
                            <div class="controls">
                                <div id="fucRemittanceImage" name="Remittance slip" class="fileuploadcustom"></div>
                            </div>
                        </div>
                            
                        <%--Button AddToRemittances--%>
                        <div class="form-actions">
                            <a id="btnAddToRemittances" onclick="this.disabled=true;return false;"  class="btn"><i class="icon-plus"></i> Add Remittance</a> 
                        </div> 
                        
                        <%--Remittance List--%>
                        <div id="divRemittanceList" class="well" >
                            <h4>
                                <span >List of Remittance </span>
                            </h4>                        
                            <table id="tblRemittanceList" class="table table-bordered table-striped table-condensed" cellspacing="0" width="100%">
                                <thead>
                                    <tr>
                                        <th style="width:76px"></th>
                                        <th style="display:none" >ModeofPaymentCode</th>
                                        <th style="display:none" >BankCode</th>
                                        <th>Mode of Payment</th>
                                        <th>Remittance Company</th>
                                        <th>Bills Payment Bank / Destination FLI Bank</th>
                                        <th>Amount Paid in Peso</th>
                                        <th>Date Paid</th> 
                                        <th>Images</th> 
                                    </tr>
                                </thead> 
                                <tbody></tbody> 
                            </table>
                            <%-- Total Remittance in Peso--%>
                            <div class="form-group">
                                <label for="txbTotalRemittanceList" >Total Remittance</label>
                                <div class="form-control">
                                    <div class="input-prepend">
                                        <span class="add-on">₱</span>
                                        <input id="txbTotalRemittanceList" type="text" disabled="disabled"  class="input-large uneditable-input" value="0.00"/>
                                    </div>
                                </div> 
                            </div> 
                        
                        </div>
                    </div>
                </div>
            </div>
            <%------------ Remittance Information -------------%>
            
            <%------------ Documents -------------%>
            <div id="divDocuments" class="row-fluid">
                <div class="box span12">
                    <div class="box-header well">
                        <h2>
                            <i class="icon-picture"></i>&nbsp;Documents Required for Holding</h2>
                        <div class="box-icon">
                            <a class="btn btn-minimize btn-round"><i class="icon-chevron-up"></i></a>
                        </div>
                    </div>
                    <div class="box-content form-horizontal">
                         <div class="control-group">
                            <%------------Alert Error Message -------------%>
                            <%--<div class="alert alert-error" id="divErrorMsgBoxDoc" style="display:none">
                                <button type="button" class="close" data-dismiss="alert">
                                    ×</button>
                                <div id="divErrorMsgDoc" >
                                </div>
                            </div>--%>
                            <div data-div="alert"  data-validgroup="document" id="divErrorMsgBoxDoc"></div>
                            <%------------Alert Error Message -------------%>
                        </div>
                       
                       <%-- Document Type--%>
                        <div class="control-group  ss-item-required" data-validgroup="document">
                            <label for="ddlDocumentType" class="control-label">Document Type</label>
                            <div class="controls">
                                <select id="ddlDocumentType" name="Document type"  class="chosen-select-deselect" data-placeholder="Select an Option"  >
                                    <option value=""></option>
                                </select>
                            </div> 
                        </div>
                        
                        <%-- Documents Image--%>
                        <div class="control-group ss-item-required" data-validgroup="document">
                            <label for="fucDocuments" class="control-label">Documents Image</label>
                            <div class="controls">
                               <div id="fucDocuments" name="Documents Image"class="fileuploadcustom"></div>
                            </div>
                        </div>
                
                        <%--Button Document--%>
                        <div class="form-actions">
                             <a id="btnAddDocument" onclick="this.disabled=true;return false;"  class="btn"><i class="icon-plus"></i> Add Document</a> 
                        </div>
                        
                        <%--Document list--%>
                        <div id="divDocumentsList" class="well">
                            <h4>
                                <span >List of Documents</span>
                            </h4>
                            <table id="tblDocumentsList" class="table table-bordered table-striped table-condensed" cellspacing="0" width="100%">
                                <thead>
                                    <tr>
                                        <th style="width:76px"></th>
                                        <th style="display:none">DocumentCode</th>
                                        <th>Document Type</th>
                                        <th>Images</th> 
                                    </tr>
                                </thead> 
                                <tbody></tbody> 
                            </table>
                        </div>
                    </div>
                </div>
            </div>
            <%------------ Documents Information -------------%>
            <%------------ Seller Information  -------------%>
            <div id="divSellerInformation" class="row-fluid">
               <div class="box span12">
                    <div class="box-header well">
                        <h2>
                            <i class="icon-user"></i>&nbsp;Seller Information</h2>
                        <div class="box-icon">
                            <a class="btn btn-minimize btn-round"><i class="icon-chevron-up"></i></a>
                        </div>
                    </div>
                    <div class="box-content form-horizontal">
                        <div class="control-group">
                            <%------------Alert Error Message -------------%>
                            <%--<div class="alert alert-error" id="divErrorMsgBoxSeller" style="display:none">
                                <button type="button" class="close" data-dismiss="alert">
                                    ×</button>
                                <div id="divErrorMsgSeller" >
                                </div>
                            </div>--%>
                            <div data-div="alert"  data-validgroup="seller" id="divErrorMsgBoxSeller"></div>
                            <%------------Alert Error Message -------------%>
                        </div>
                               
                       <%--Sellers Position--%>
                        <div id="divSellerPosition" class="control-group  ss-item-required" data-validgroup="seller">
                           <label for="ddlSellerPosition" class="control-label">Sellers Position</label>
                            <div class="controls">
                                <select id="ddlSellerPosition" name="Seller position"  class="chosen-select-deselect" data-placeholder="Select an Option"  >
                                    <option value=""></option>
                                </select>
                            </div>
                        </div>
                
                        <%--Is Not yet affiliated?--%>
                        <div id="divIsNotAffiliated" class="control-group">
                            <label class="control-label">Is Not yet affiliated?</label> 
                            <div class="controls">
                                <label >
                                    <input id="chbNotYetAffiliated" type="checkbox" />
                                </label>
                            </div>
                        </div>
                        
                        <%--Seller Name--%>
                        <div id="divSellerAffiliated" class="control-group ss-item-required"  data-validgroup="seller">
                            <label class="control-label">Seller Name</label>
                            <div class="controls">
                                 <select id="ddlSeller" name="Seller name"  class="chosen-select-deselect" data-placeholder="Select an Option"  >
                                    <option value=""></option>
                                </select>
                            </div>
                        </div>
                        
                        <div id="divSellerNotAffiliated" style="display:none"  class="control-group ss-item-required"  data-validgroup="seller">
                            <label class="control-label">Seller Name</label>
                            <div class="controls">
                                  <input id="txbSellerName" name="Seller name" type="text"  class="input-xxlarge"/>
                            </div>
                        </div> 
                        
                        <%--Button Seller--%>
                        <div class="form-actions">
                             <a id="btnAddSellerName" onclick="this.disabled=true;return false;"  class="btn"><i class="icon-plus"></i> Add Seller</a> 
                        </div>
                        <%--Seller list--%>
                        <div id="divSellerList" class="well">
                            <h4>
                                <span >List of Seller</span>
                            </h4>
                            <table id="tblSellerList" class="table table-bordered table-striped table-condensed" cellspacing="0" width="100%">
                                <thead>
                                    <tr>
                                        <th style="width:76px"></th>
                                        <th style="display:none">PositionCode</th>
                                        <th style="display:none">AgentCode</th> 
                                        <th>Position</th>
                                        <th>Not yet affiliated</th>
                                        <th>Seller Name</th>
                                    </tr>
                                </thead> 
                                <tbody></tbody> 
                            </table>
                         
                            <%--Network Structure--%>
                            <div class="form-group">
                                <label>Network Structure</label>
                                <div class="form-control">
                                    <input id="txbNetworkStructure" name="Network structure" type="text" class="input-xxlarge"/>
                                </div>
                            </div> 
                        
                        </div>
                    </div>
                </div>
            </div>
            <%------------ Seller Information -------------%>
            
            <%------------ Save-------------%>
            <div id="divSave" class="form-actions">
                <a id="btnClickToHold" class="btn btn-large btn-success">   Hold the Unit Now   </a> 
            </div> 
            <%------------ Save-------------%>
        </div>
    </div>

    <%--<script type="text/javascript">
        Sys.Browser.WebKit = {};
        if (navigator.userAgent.indexOf('WebKit/') > -1) 
        {
            Sys.Browser.agent = Sys.Browser.WebKit;
            Sys.Browser.version = parseFloat(navigator.userAgent.match(/WebKit\/(\d+(\.\d+)?)/)[1]);
            Sys.Browser.name = 'WebKit';
        };
      
    </script>--%>
 
            
        
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="cphContentBottom" runat="Server">

  
    <script type="text/javascript">
      
    
        var ajaxurlGetData = 'unithold.aspx/GetDataFromDB';
                  
        $(document).ready(function () {
            // Automatic Show/ Hide loading modal div     
            $(document).bind("ajaxSend", function(){
               $("#divLoading").show();
             }).bind("ajaxStop ", function(){
               $("#divLoading").hide();
             });
             
            // Initialize chosen css for selector
            $('.chosen-select-deselect').chosen({ allow_single_deselect: true,width:"100%" }); //,width:"95%"
            
            // Apply chosen function first before hiding to prevent width:0px
            $('#divAllocation').css('display','none');  
            $('#divPersonalInformation').css('display','none');
            $('#divBuyerContact').css('display','none');            
            $('#divUnitInformation').css('display','none');     
            $('#divRemittance').css('display','none');     
            $('#divSalesLocation').css('display','none');     
            $('#divDocuments').css('display','none');     
            $('#divSellerInformation').css('display','none');  
            $('#divSellerNotAffiliated').css('display','none');
            
            //remove padding textbox to align width dropdown width
            //var ddlWidth = $('.chosen-select-deselect').filter('.input-xxlarge').width();
            //$(':text,[type=email]').filter('.input-xxlarge').css('width',ddlWidth);
            
            var DropdownAllocation = $('#ddlAllocation');
            var DropdownProject = $('#ddlProjects');
            var DropdownPhase = $('#ddlPhase');
            var DropdownBlock = $('#ddlBlock');
            var DropdownUnit = $('#ddlUnit');
            var DropdownSchemeType = $('#ddlSchemeType');
            var DropdownPaymentOption = $('#ddlPaymentOption');
            
            var DropdownContactType = $('#ddlcontacttype');
            var DropdownSalesCountry = $('#ddlSalesCountry');
            var DropdownModeofPayment = $('#ddlModeofPayment');
            var DropdownBillsPaymentBank = $('#ddlBillsPaymentBank');
            var DropdownDestinationBank =  $('#ddlDestinationBank');
            var DropdownDocumentType =  $('#ddlDocumentType');
            var DropdownSellerPosition =  $('#ddlSellerPosition');
            var DropdownSeller =  $('#ddlSeller');
            var DropdownSalesOffice =  $('#ddlSalesOffice');
            
            
           //****************************** FUNCTIONS ****************************
           
           //Populate Allocation 
            function PopulateAllocation() {
                var newOption = $('<option value=""></option>');
                DropdownAllocation.empty();
                DropdownAllocation.append(newOption);
                DropdownAllocation.trigger("chosen:updated");
                
                $("#ddlAllocation_chosen > a > span").text("Loading data");  
            
                $.ajax({
                       type: 'POST',
                       url: ajaxurlGetData,
                       dataType: 'json',
                       data: JSON.stringify({Mode:1,Allocation:'',Project:'',Phase:'',Block:'',RefObj:'',SchemeType:'',ContractType:'',SalesCountry:''}),
                       contentType :'application/json; charset=utf-8',
                       success: function (data) {
                                   if ($.parseJSON(data.d) != undefined){
                                       if ($.parseJSON(data.d)[0].ErrorNumber == 101){window.location.reload();};
                                       displayError($.parseJSON(data.d)[0].ErrorMessage); 
                                   }
                                   else 
                                   {   
                                   
                                        DropdownAllocation.empty();
                                        DropdownAllocation.append(new Option("", ""));
                                        $(data[0]).each(function (index, item) {
                                           DropdownAllocation.append(new Option(item.Description, item.Code));                       
                                        });
                                        $('#divAllocation').css('display','block');  
                                            
                                        DropdownAllocation.trigger("chosen:updated");
                                        $("#ddlAllocation_chosen > a > span").text("Select an Option"); 
                                        
                                        // if 1 item only, run LoadInitialField and hide divAllocation
                                        if ($(data[0]).length == 1)
                                        {
                                            //isOneAllocation = 1;
                                            DropdownAllocation.prop('selectedIndex', 1);
                                            DropdownAllocation.trigger("chosen:updated");
                                            $('#divAllocation').css('display','none');  
                                            LoadInitialField(); 
                                        };
                                         
                                    };
                                },
                       error: function (xhr) {
                            var err = $.parseJSON(xhr.responseText);                        
                            displayError('Error loading Location : ' + err.Message); 
                       }
                  });
                  
             };
          
            //Populate Dropdown and Set visible field by selected allocation
            function LoadInitialField(){
                
                $('#divPersonalInformation').css('display','none');
                $('#divBuyerContact').css('display','none');            
                $('#divUnitInformation').css('display','none');     
                $('#divRemittance').css('display','none');     
                $('#divSalesLocation').css('display','none');     
                $('#divDocuments').css('display','none');     
                $('#divSellerInformation').css('display','none');  
                
                if (DropdownAllocation.val() == ''){return false};
                
                $.ajax({
                       type: 'POST',
                       url: ajaxurlGetData,
                       dataType: 'json',
                       data: JSON.stringify({Mode:2,Allocation:DropdownAllocation.val(),Project:'',Phase:'',Block:'',RefObj:'',SchemeType:'',ContractType:'',SalesCountry:''}),
                       contentType :'application/json; charset=utf-8',
                       success: function (data) {
                                   if ($.parseJSON(data.d) != undefined){
                                        //Error
                                       if ($.parseJSON(data.d)[0].ErrorNumber == 101){window.location.reload();};
                                       displayError($.parseJSON(data.d)[0].ErrorMessage); 
                                   }
                                   else 
                                   {   //Success
                                      
                                       // 9 : Show visible field
                                       $(data[9]).each(function (index, item) {
                                           var array = item.VisibleDiv.split('•');
                                           for (var i in array) {
                                               if($("#" + array[i]).length > 0) {
                                                  //it does exist, then show
                                                  $("#" + array[i]).css('display','block');
                                                };
                                            }
                                        });
                                        
                                       // 0 : Populate Project 
                                       DropdownProject.empty();
                                       DropdownProject.append(new Option("", ""));
                                       $(data[0]).each(function (index, item) {
                                           DropdownProject.append(new Option(item.Description, item.Code));                       
                                       });
                                       DropdownProject.trigger("chosen:updated");
                                        
                                       // 1 : Contact Type
                                       DropdownContactType.empty();
                                       DropdownContactType.append(new Option("", ""));
                                       $(data[1]).each(function (index, item) {
                                           DropdownContactType.append(new Option(item.Description, item.Code));                       
                                       });
                                       DropdownContactType.trigger("chosen:updated");
                                      
                                       // 2 : Sales Country
                                       DropdownSalesCountry.empty();
                                       DropdownSalesCountry.append(new Option("", ""));
                                       $(data[2]).each(function (index, item) {
                                           DropdownSalesCountry.append(new Option(item.Description, item.Code));                       
                                       });
                                       
                                       if (DropdownAllocation.val() == '0000000080') //local
                                       { DropdownSalesCountry.val('PHIL');
                                         PopulateSalesOffice();
                                       };
                                   
                                       DropdownSalesCountry.trigger("chosen:updated");
                                        
                                       // 3 : Mode of Payment
                                       DropdownModeofPayment.empty();
                                       DropdownModeofPayment.append(new Option("", ""));
                                       $(data[3]).each(function (index, item) {
                                           DropdownModeofPayment.append(new Option(item.Description, item.Code));                       
                                       });
                                       DropdownModeofPayment.trigger("chosen:updated");
                                        
                                       // 4 : Bills Payment Bank 
                                       DropdownBillsPaymentBank.empty();
                                       DropdownBillsPaymentBank.append(new Option("", ""));
                                       $(data[4]).each(function (index, item) {
                                           DropdownBillsPaymentBank.append(new Option(item.Description, item.Code));                       
                                       });
                                       DropdownBillsPaymentBank.trigger("chosen:updated");
                                       
                                       // 5 : Destination FLI Bank
                                       DropdownDestinationBank.empty();
                                       DropdownDestinationBank.append(new Option("", ""));
                                       $(data[5]).each(function (index, item) {
                                           DropdownDestinationBank.append(new Option(item.Description, item.Code));                       
                                       });
                                       DropdownDestinationBank.trigger("chosen:updated");
                                         
                                       // 6 : Document Type
                                       DropdownDocumentType.empty();
                                       DropdownDocumentType.append(new Option("", ""));
                                       $(data[6]).each(function (index, item) {
                                           DropdownDocumentType.append(new Option(item.Description, item.Code));                       
                                       });
                                       DropdownDocumentType.trigger("chosen:updated");
                                         
                                       // 7 : Seller 
                                       DropdownSeller.empty();
                                       DropdownSeller.append(new Option("", ""));
                                       $(data[7]).each(function (index, item) {
                                           DropdownSeller.append(new Option(item.Description, item.Code));                       
                                       });
                                       DropdownSeller.trigger("chosen:updated");
                                         
                                       // 8 : Seller Position 
                                       DropdownSellerPosition.empty();
                                       DropdownSellerPosition.append(new Option("", ""));
                                       $(data[8]).each(function (index, item) {
                                           DropdownSellerPosition.append(new Option(item.Description, item.Code));                       
                                       });
                                       DropdownSellerPosition.trigger("chosen:updated");
                                          
                                       // set default   PreferredDueDate value to current day
                                       var d = new Date();
                                       var duedate = $('#ddlPreferredDueDate option').filter('[data-num="' + d.getDate() + '"]').val(); 
                                       $('#ddlPreferredDueDate').val(duedate);
                                       $('#ddlPreferredDueDate').trigger("chosen:updated");
                                       
                                       
                                    };
                                },
                       error: function (xhr) {
                            var err = $.parseJSON(xhr.responseText);                        
                            displayError('Error loading Location : ' + err.Message); 
                       }
                  });
            
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
            
            function GetTotalRemittance()
            { var totalRemit = 0;   
              $('#tblRemittanceList tbody tr').each(function (index,item){
                    //totalRemit = totalRemit + $(item).find('.RemitAmount').html();
                    totalRemit = parseFloat(totalRemit) +  parseFloat($(item).find('.RemitAmount').html().replace(',',''))
             
              });
              
              return totalRemit
            };
             
            //****************************** FUNCTIONS ****************************
            
            //******************************  EVENTS  ****************************
          
             //---------------- DROPDOWN -----------------
             // Allocation Event Change
             DropdownAllocation.change(function () {
                LoadInitialField();
             });
             
             // Project Event Change
             DropdownProject.change(function () {
                    $('#divPhaseBuilding').css('display','none')
                    $('#divBlockFloorCluster').css('display','none')
                    $('#divLotUnit').css('display','none')
                    $('#divHouseModel').css('display','none')
                    $('#divAdjoinUnit').css('display','none')
                    $('#divSchemeType').css('display','none')
                    $('#divPaymentOption').css('display','none')
                    $('#divPreferedDueDate').css('display','none')
                    $('#divReservationFee').css('display','none')
                    $('#divPromoCode').css('display','none') 
                    
                    if (DropdownProject.val() == ''){return false};
                
                    $.ajax({
                           type: 'POST',
                           url: ajaxurlGetData,
                           dataType: 'json',
                           data: JSON.stringify({Mode:3,Allocation:DropdownAllocation.val(),Project:DropdownProject.val(),Phase:'',Block:'',RefObj:'',SchemeType:'',ContractType:'',SalesCountry:''}),
                           contentType :'application/json; charset=utf-8',
                           success: function (data) {
                               if ($.parseJSON(data.d) != undefined){
                                   //Error
                                   if ($.parseJSON(data.d)[0].ErrorNumber == 101){window.location.reload();};
                                   displayError($.parseJSON(data.d)[0].ErrorMessage); 
                               }
                               else 
                               {   //Success
                                    $('#divPhaseBuilding').css('display','block');
                                   // 0 : Populate Phase 
                                   DropdownPhase.empty();
                                   DropdownPhase.append(new Option("", ""));
                                   $(data[0]).each(function (index, item) {
                                       DropdownPhase.append(new Option(item.Description, item.Code));                       
                                   });
                                   DropdownPhase.trigger("chosen:updated");
                                   
                                   // 1 : Phase, Block, Unit label name
                                   if ($(data[1]).length > 0 )
                                   {  $('#lblPhase').text(data[1][0].PhaseLabelName);
                                      $('#ddlPhase').attr('name',data[1][0].PhaseLabelName);
                                      
                                      $('#lblBlock').text(data[1][0].BlockFloorLabelName);
                                      $('#ddlBlock').attr('name',data[1][0].BlockFloorLabelName);
                                     
                                      $('#lblLotUnit').text(data[1][0].LotUnitLabelName);
                                      $('#ddlUnit').attr('name',data[1][0].LotUnitLabelName);
                                      
                                   };
                                   
                               }
                           },
                           error: function (xhr) {
                                var err = $.parseJSON(xhr.responseText);                        
                                displayError('Error loading phase : ' + err.Message); 
                           }
                     });             
             });
             
             // Phase Event Change
             DropdownPhase.change(function () {
                    $('#divBlockFloorCluster').css('display','none');
                    $('#divLotUnit').css('display','none');
                    $('#divHouseModel').css('display','none');
                    $('#divAdjoinUnit').css('display','none');
                    $('#divSchemeType').css('display','none');
                    $('#divPaymentOption').css('display','none');
                    $('#divPreferedDueDate').css('display','none');
                    $('#divReservationFee').css('display','none');
                    $('#divPromoCode').css('display','none');
                    
                    if (DropdownPhase.val() == ''){return false};
                
                    $.ajax({
                           type: 'POST',
                           url: ajaxurlGetData,
                           dataType: 'json',
                           data: JSON.stringify({Mode:4,Allocation:DropdownAllocation.val(),Project:DropdownProject.val(),Phase:DropdownPhase.val(),Block:'',RefObj:'',SchemeType:'',ContractType:'',SalesCountry:''}),
                           contentType :'application/json; charset=utf-8',
                           success: function (data) {
                               if ($.parseJSON(data.d) != undefined){
                                   //Error
                                   if ($.parseJSON(data.d)[0].ErrorNumber == 101){window.location.reload();};
                                   displayError($.parseJSON(data.d)[0].ErrorMessage); 
                               }
                               else 
                               {   //Success
                                    $('#divBlockFloorCluster').css('display','block');
                                   // 0 : Populate block 
                                   DropdownBlock.empty();
                                   DropdownBlock.append(new Option("", ""));
                                   $(data[0]).each(function (index, item) {
                                       DropdownBlock.append(new Option(item.Description, item.Code));                       
                                   });
                                   DropdownBlock.trigger("chosen:updated");
                                   
                               }
                           },
                           error: function (xhr) {
                                var err = $.parseJSON(xhr.responseText);                        
                                displayError('Error loading block : ' + err.Message); 
                           }
                     });            
             });
             
             // Block Event Change
             DropdownBlock.change(function () {
                    $('#divLotUnit').css('display','none')
                    $('#divHouseModel').css('display','none')
                    $('#divAdjoinUnit').css('display','none')
                    $('#divSchemeType').css('display','none')
                    $('#divPaymentOption').css('display','none')
                    $('#divPreferedDueDate').css('display','none')
                    $('#divReservationFee').css('display','none')
                    $('#divPromoCode').css('display','none') 
                    
                    if (DropdownBlock.val() == ''){return false};
                
                    $.ajax({
                           type: 'POST',
                           url: ajaxurlGetData,
                           dataType: 'json',
                           data: JSON.stringify({Mode:5,Allocation:DropdownAllocation.val(),Project:DropdownProject.val(),Phase:DropdownPhase.val(),Block:DropdownBlock.val(),RefObj:'',SchemeType:'',ContractType:'',SalesCountry:''}),
                           contentType :'application/json; charset=utf-8',
                           success: function (data) {
                               if ($.parseJSON(data.d) != undefined){
                                   //Error
                                   if ($.parseJSON(data.d)[0].ErrorNumber == 101){window.location.reload();};
                                   displayError($.parseJSON(data.d)[0].ErrorMessage); 
                               }
                               else 
                               {   //Success
                                    $('#divLotUnit').css('display','block');
                                   // 0 : Populate UNit 
                                   DropdownUnit.empty();
                                   DropdownUnit.append(new Option("", ""));
                                   $(data[0]).each(function (index, item) {
                                       DropdownUnit.append(new Option(item.Description, item.Code));                       
                                   });
                                   DropdownUnit.trigger("chosen:updated");
                                   
                               }
                           },
                           error: function (xhr) {
                                var err = $.parseJSON(xhr.responseText);                        
                                displayError('Error loading unit : ' + err.Message); 
                           }
                     });            
             });
            
             // Unit Event Change
             DropdownUnit.change(function () {
                    $('#divHouseModel').css('display','none')
                    $('#divAdjoinUnit').css('display','none')
                    $('#divSchemeType').css('display','none')
                    
                    $('#divPaymentOption').css('display','none')
                    $('#divPreferedDueDate').css('display','none')
                    $('#divReservationFee').css('display','none')
                    $('#divPromoCode').css('display','none') 
                    
                    if (DropdownUnit.val() == ''){return false};
                
                    $.ajax({
                           type: 'POST',
                           url: ajaxurlGetData,
                           dataType: 'json',
                           data: JSON.stringify({Mode:6,Allocation:DropdownAllocation.val(),Project:DropdownProject.val(),Phase:DropdownPhase.val(),Block:DropdownBlock.val(),RefObj:DropdownUnit.val(),SchemeType:'',ContractType:'',SalesCountry:''}),
                           contentType :'application/json; charset=utf-8',
                           success: function (data) {
                               if ($.parseJSON(data.d) != undefined){
                                   //Error
                                   if ($.parseJSON(data.d)[0].ErrorNumber == 101){window.location.reload();};
                                   displayError($.parseJSON(data.d)[0].ErrorMessage); 
                               }
                               else 
                               {   //Success
                                   $('#divSchemeType').css('display','block');
                                   
                                   // 0 : Populate adjoining table
                                   if ($(data[0]).length > 0)
                                   {   $('#divAdjoinUnit').css('display','block');
                                       $('#tblAdjoiningUnit tbody').empty();
                                            
                                            //1 Block/Floor
                                            //2 Unit Name
                                            //3 Unit Type
                                            //4 Gross Area
                                            //5 ReferenceObject
                                       var trUnit = ''     
                                       $(data[0]).each(function (index, item) {
                                           trUnit = trUnit + '<tr>' + '<td>' + item.BlockName + '</td>' 
                                                               + '<td>' + item.LotUnitShareNumber + '</td>' 
                                                               + '<td>' + item.UnitType + '</td>'
                                                               + '<td>' + item.GrossArea + '</td>' 
                                                               + '<td class="ReferenceObject" >' + item.ReferenceObject + '</td>'   
                                                        + '</tr>'
                                       }); 
                                       $('#tblAdjoiningUnit tbody').append(trUnit);
                                   }; 
                                   // 1 : Populate House Model
                                   if ($(data[1]).length > 0)
                                   {   $('#divHouseModel').css('display','block');
                                       $('#txbHouseModel').val(data[1][0].Description);
                                   };
                                   
                                   // 2 : Populate SchemeType Dropdown
                                   DropdownSchemeType.empty();
                                   DropdownSchemeType.append(new Option("", ""));
                                   $(data[2]).each(function (index, item) {
                                       DropdownSchemeType.append(new Option(item.Description, item.Code));                       
                                   });
                                   DropdownSchemeType.trigger("chosen:updated");
                                   
                               };
                              
                           },
                           error: function (xhr) {
                                var err = $.parseJSON(xhr.responseText);                        
                                displayError('Error loading adjoining, house model , scheme : ' + err.Message); 
                           }
                     });     
             }); 
             
             // Scheme Type Event Change
             DropdownSchemeType.change(function () {
                    $('#divPaymentOption').css('display','none')
                    $('#divPreferedDueDate').css('display','none')
                    $('#divReservationFee').css('display','none')
                    $('#divPromoCode').css('display','none') 
                    
                    if (DropdownSchemeType.val() == ''){return false};
                
                    $.ajax({
                           type: 'POST',
                           url: ajaxurlGetData,
                           dataType: 'json',
                           data: JSON.stringify({Mode:7,Allocation:DropdownAllocation.val(),Project:DropdownProject.val(),Phase:DropdownPhase.val(),Block:DropdownBlock.val(),RefObj:DropdownUnit.val(),SchemeType:DropdownSchemeType.val(),ContractType:'',SalesCountry:''}),
                           contentType :'application/json; charset=utf-8',
                           success: function (data) {
                               if ($.parseJSON(data.d) != undefined){
                                   //Error
                                   if ($.parseJSON(data.d)[0].ErrorNumber == 101){window.location.reload();};
                                   displayError($.parseJSON(data.d)[0].ErrorMessage); 
                               }
                               else 
                               {   //Success
                                   $('#divPaymentOption').css('display','block');
                                   // 0 : Populate Payment Option / Contract Type 
                                   DropdownPaymentOption.empty();
                                   DropdownPaymentOption.append(new Option("", ""));
                                   $(data[0]).each(function (index, item) {
                                       DropdownPaymentOption.append(new Option(item.Description, item.Code));                       
                                   });
                                   DropdownPaymentOption.trigger("chosen:updated");
                                  
                                   if (DropdownAllocation.val() != '0000000080') //local
                                   { $('#divPreferedDueDate').css('display','block'); };
                               }
                           },
                           error: function (xhr) {
                                var err = $.parseJSON(xhr.responseText);                        
                                displayError('Error loading unit : ' + err.Message); 
                           }
                     });            
             });
             
             // Promo Code, TCP, Res Fee Event Change
             DropdownPaymentOption.change(function () {
                    $('#divReservationFee').css('display','none');
                    $('#divPromoCode').css('display','none') ;
                    
                    if (DropdownPaymentOption.val() == ''){return false};
                
                    $.ajax({
                           type: 'POST',
                           url: ajaxurlGetData,
                           dataType: 'json',
                           data: JSON.stringify({Mode:8,Allocation:DropdownAllocation.val(),Project:DropdownProject.val(),Phase:DropdownPhase.val(),Block:DropdownBlock.val(),RefObj:DropdownUnit.val(),SchemeType:DropdownSchemeType.val(),ContractType:DropdownPaymentOption.val(),SalesCountry:''}),
                           contentType :'application/json; charset=utf-8',
                           success: function (data) {
                               if ($.parseJSON(data.d) != undefined){
                                   //Error
                                   if ($.parseJSON(data.d)[0].ErrorNumber == 101){window.location.reload();};
                                   displayError($.parseJSON(data.d)[0].ErrorMessage); 
                               }
                               else 
                               {   //Success
                                   // 0 : [TCP] ,RESFee , PromoCode
                                   if ($(data[0]).length > 0)
                                   {   $('#divReservationFee').css('display','block');
                                       var RsFee =  formatCurrency(data[0][0].RESFee);
                                       $('#txbReservationFee').val(RsFee); 
                                       
                                       // Show promo code if exists
                                        if ($(data[0][0].PromoCode).length > 0)
                                        {   $('#divPromoCode').css('display','block');
                                            $('#lblPromoCode').html(data[0][0].PromoCode);
                                        };
                                   };
                               };
                           },
                           error: function (xhr) {
                                var err = $.parseJSON(xhr.responseText);                        
                                displayError('Error loading unit : ' + err.Message); 
                           }
                     });            
             });
            
            function PopulateSalesOffice()
            {
                    $('#divSalesOffice').css('display','none');
                  
                    if (DropdownSalesCountry.val() == ''){return false};
                
                    $.ajax({
                           type: 'POST',
                           url: ajaxurlGetData,
                           dataType: 'json',
                           data: JSON.stringify({Mode:9,Allocation:DropdownAllocation.val(),Project:DropdownProject.val(),Phase:DropdownPhase.val(),Block:DropdownBlock.val(),RefObj:DropdownUnit.val(),SchemeType:DropdownSchemeType.val(),ContractType:DropdownPaymentOption.val(),SalesCountry:DropdownSalesCountry.val()}),
                           contentType :'application/json; charset=utf-8',
                           success: function (data) {
                               if ($.parseJSON(data.d) != undefined){
                                   //Error
                                   if ($.parseJSON(data.d)[0].ErrorNumber == 101){window.location.reload();};
                                   displayError($.parseJSON(data.d)[0].ErrorMessage); 
                               }
                               else 
                               {   //Success
                                   DropdownSalesOffice.empty();
                                   DropdownSalesOffice.append(new Option("", ""));
                                   $(data[0]).each(function (index, item) {
                                       DropdownSalesOffice.append(new Option(item.Description, item.Code));                       
                                   });
                                   DropdownSalesOffice.trigger("chosen:updated");
                                   $('#divSalesOffice').css('display','block'); 
                               };
                           },
                           error: function (xhr) {
                                var err = $.parseJSON(xhr.responseText);                        
                                displayError('Error loading unit : ' + err.Message); 
                           }
                     });   
            };
            
            // Sales Coutry changed: Populate Sales Office
             DropdownSalesCountry.change(function () {
                     PopulateSalesOffice();     
             }); 
             
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
             //---------------- DROPDOWN -----------------
            
             //---------------- BUTTON -----------------
             var btnDeleteRow = '<a class="btn btn-danger delete"><i class="icon-trash icon-white"></i>Delete</a>'
             
             // add Contact
             $('#btnAddBuyerContact').click(function () {
                if (formIsValid('contact') == false)
                {return;};
                
                
                var contactTypeCode = $('#ddlcontacttype').val();
                var contactTypeName = $('#ddlcontacttype').find('option:selected').text();
                var contactValue = $('#txbContactNumber').val();
                var areacode = $.trim($('#txbAreaCode').val());
                if (areacode != '')
                {   contactValue = '(' + areacode + ') ' + contactValue; };
                
                //-- Add Row
                
                // Columns : 
                //Delete Image BUtton
                //Type Code
                //Contact Type
                //Contact Number
                $('#tblContactNumberList tbody')
                        .append('<tr>' + '<td>' + btnDeleteRow + '</td>' 
                                      + '<td class="ContactTypeCode" style="display:none">' + contactTypeCode + '</td>' 
                                      + '<td>' + contactTypeName + '</td>'
                                      + '<td class="ContactValue">' + contactValue + '</td>' 
                                + '</tr>'); 
                                
                 // clear fields
                $('#ddlcontacttype').val('');
                $('#txbContactNumber').val('');
                $('#txbAreaCode').val('');    
                $('#ddlcontacttype').trigger("chosen:updated");
                             
             });
             
             // Add Remittance
             $('#btnAddToRemittances').click(function () {
                if (formIsValid('remit') == false)
                   {return;};
                                   
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
                //Delete
                //ModeofPaymentCode
                //BankCode
                //Mode of Payment
                //Remittance Company
                //Bills Payment Bank / Destination FLI Bank
                //Amount Paid in Peso
                //Date Paid
                //Images
                var imageslist = GetImageGalleryHtml('fucRemittanceImage');
                
                $('#tblRemittanceList tbody')
                        .append('<tr>' + '<td>' + btnDeleteRow + '</td>' 
                                       + '<td class="ModePaymentCode" style="display:none">' + ModePaymentCode + '</td>' 
                                       + '<td class="BankCode" style="display:none">' + BankCode + '</td>' 
                                       + '<td>' + ModePaymentName + '</td>'
                                       + '<td class="RemitCompanyName" >' + RemitCompanyName + '</td>' 
                                       + '<td>' + BankName + '</td>' 
                                       + '<td class="RemitAmount"  >' + $('#txbRemitAmount').val() + '</td>' 
                                       + '<td class="DatePaid"  >' + $('#txbDatePaid').val() + '</td>' 
                                       + '<td class="ImagesList" >' + imageslist + '</td>' 
                                + '</tr>');
                                
                // clear selected file 
                FUC_ClearAllFiles('fucRemittanceImage');
             
                // compute total remittance
                $('#txbTotalRemittanceList').val(formatCurrency(GetTotalRemittance()));
                
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
             });
             
             // Add Documents
             $('#btnAddDocument').click(function () {
             
                if (formIsValid('document') == false)
                   {return;};
                
                var DocumentTypeCode =  DropdownDocumentType.val();
                var DocumentTypeName =  DropdownDocumentType.find('option:selected').text();
                
                //Delete
                //DocumentCode
                //Document Type
                //Images
                var imageslist = GetImageGalleryHtml('fucDocuments');
                
                $('#tblDocumentsList tbody')
                        .append('<tr>' + '<td>' + btnDeleteRow + '</td>' 
                                       + '<td class="DocumentTypeCode" style="display:none">' + DocumentTypeCode + '</td>' 
                                       + '<td >' + DocumentTypeName + '</td>'
                                       + '<td>' + imageslist + '</td>' 
                                + '</tr>'); 
                                
                // clear selected file 
                FUC_ClearAllFiles('fucDocuments');
                
                 // clear fields
                DropdownDocumentType.val('');
                DropdownDocumentType.trigger("chosen:updated");  
              
             });
             
             // Add Seller
             $('#btnAddSellerName').click(function () {
                if (formIsValid('seller') == false)
                   {return;};
                   
                var SellerPositionCode =  DropdownSellerPosition.val();
                var SellerPositionName =  DropdownSellerPosition.find('option:selected').text();
                
                var AgentCode = '';
                var AgentName = '';
                var IsAffli = ''
                
                if ($('#chbNotYetAffiliated').parent('span').attr('class') == 'checked')
                {  AgentName = $('#txbSellerName').val();  
                   IsAffli ='<input type="checkbox"  checked="checked"  disabled="disabled" />'
                }
                else 
                {   AgentCode = DropdownSeller.val();
                    AgentName = DropdownSeller.find('option:selected').text();
                    IsAffli ='<input type="checkbox"disabled="disabled" />'
                };
                
                //Delete
                //PositionCode
                //AgentCode
                //Position
                //Not yet affiliated
                //Seller Name
                
                $('#tblSellerList tbody')
                        .append('<tr>' + '<td>' + btnDeleteRow + '</td>' 
                                       + '<td class="SellerPositionCode" style="display:none">' + SellerPositionCode + '</td>' 
                                       + '<td class="AgentCode" style="display:none">' + AgentCode + '</td>'
                                       + '<td >' + SellerPositionName + '</td>'
                                       + '<td class="IsAffli"  >' + IsAffli + '</td>' 
                                       + '<td class="AgentName" >' + AgentName + '</td>'
                                + '</tr>'); 
                                
                // clear fields
                DropdownSellerPosition.val('');
                DropdownSellerPosition.trigger("chosen:updated");     
                DropdownSeller.val('');
                DropdownSeller.trigger("chosen:updated");     
                $('#txbSellerName').val(''); 
             });    
             
             // Add Hold
             $('#btnClickToHold').click(function () {
                 $('#btnClickToHold').disabled=true;
                
                 var IsBuyerInfoVisible = !($('#divPersonalInformation').css('display') == 'none');
                 var IsDocVisible = !($('#divDocuments').css('display') == 'none');
                 var IsSellerVisible = !($('#divSellerInformation').css('display') == 'none');
                 var IsRemitVisible = !($('#divRemittance').css('display') == 'none');
                
                 
                if (IsBuyerInfoVisible == false)
                {  displayError("Insufficient information.",'divErrorMsgBox');
                   $('#btnClickToHold').disabled=false; 
                   return false;
                }; 
               
                // validate required field 
                if (formIsValid('hold') == false)
                   {$('#btnClickToHold').disabled=false; 
                   return false;};  
                
                var error = ''
                
                 if ($('#txbReservationFee').val() <= 0)
                 { error =  error + 'Reservation Fee cannot be less than or equal to zero(0)' + '<br/>'
                 };
                     
                // validate grids
                 if ($('#tblContactNumberList tbody').find('tr').length == 0)
                 {error = error +  'Add at least one contact number' + '<br/>'
                 };
                 
                 
                 if (IsDocVisible) {
                     //Visible 
                     if ($('#tblDocumentsList tbody').find('tr').length == 0)
                     {error = error + 'Add at least one document' + '<br/>'
                     };
                 };  
                                   
                 if (IsSellerVisible) {
                     //Visible 
                     if ($('#tblSellerList tbody').find('tr').length == 0)
                     {  error = error +  'Add at least one seller' + '<br/>'
                     };
                 }; 
                                   
                 if (IsRemitVisible) {
                     //Visible 
                     if ($('#tblRemittanceList tbody').find('tr').length == 0)
                     {  error  = error + 'Add at least one remittance' + '<br/>'
                     };
                     
                     //Validate Total Remittance
                     var TotalRemittance = 0;
                     TotalRemittance = GetTotalRemittance();
                     
                     if (TotalRemittance <= 0)
                     { error =  error + 'Total Remittance cannot be less than or equal to zero(0)' + '<br/>'
                     };
                     
                     if (TotalRemittance < $('#txbReservationFee').val().replaceAll(',',''))
                     {error =  error + 'Total remittance must be equal or greater than reservation fee ' + $('#txbReservationFee').val()  + '<br/>';
                     }; 
                 };
                 
                 
                if (error != '') 
                 {  displayError(error,'divErrorMsgBox');
                   $('#btnClickToHold').disabled=false; 
                   return false;
                }; 
                 
                  
                 
                 
                 //-------- SAVE ------------
                  // Generate XML data as arguments for saving
                  // Tables 
                  //  BuyerUnitInfo (buyer info, unit info, sales location)
                  //  ContactInfo
                  //  RemitInfo
                  //  RemitImages
                  //  DocInfoImages
                  //  SellerInfo
                  //  AdjoiningUnit
                 var XML = new XMLWriter();
                 
                 XML.BeginNode("Dataset");
                    
                    if (IsBuyerInfoVisible) {
                         XML.BeginNode("BuyerUnitInfo"); //Table  : BuyerUnitInfo, single row
                           // Columns : Buyer
                           XML.Node("Allocation", $('#ddlAllocation').val()); 
                           XML.Node("LastName", $('#txbLastName').val()); 
                           XML.Node("FirstName", $('#txbFirstName').val());
                           XML.Node("MiddleName", $('#txbMiddleName').val()); 
                           XML.Node("BirthDay", $('#dtpBirthDay').val()); 
                           XML.Node("EmailAddress", $('#txbEmailAddress').val()); 
                           
                            // Columns : Unit
                           XML.Node("Project", $('#ddlProjects').val()); 
                           XML.Node("Phase", $('#ddlPhase').val()); 
                           XML.Node("Block", $('#ddlBlock').val());
                           XML.Node("Unit", $('#ddlUnit').val()); 
                           XML.Node("SchemeType", $('#ddlSchemeType').val()); 
                           XML.Node("PaymentOption", $('#ddlPaymentOption').val()); 
                           XML.Node("PreferredDueDate", $('#ddlPreferredDueDate').val()); 
                           XML.Node("ReservationFee", $('#txbReservationFee').val()); 
                           XML.Node("PromoCode", $('#lblPromoCode').val()); 
                           //Remit
                           XML.Node("TotalRemittance", $('#txbTotalRemittanceList').val()); 
                            
                           // Columns : Sales Location
                           XML.Node("SalesCountry", $('#ddlSalesCountry').val()); 
                           XML.Node("SalesOffice", $('#ddlSalesOffice').val()); 
                           
                         XML.EndNode(); //End Table : BuyerUnitInfo
                    };
                    
                    // Contact Multiple Rows 
                    // Sample format : 
                    //  <ContactInfo>
                    //     <RowID>1</RowID>
                    //     <ContactTypeCode>2</ContactTypeCode>
                    //     <ContactValue>3</ContactValue>
                    //  </ContactInfo>
                    $('#tblContactNumberList tbody').find('tr').each(function (index, item ) {
                        XML.BeginNode("ContactInfo"); //Table : ContactInfo
                           // Columns : 
                           XML.Node("RowID", index + '');  
                           XML.Node("ContactTypeCode", $(item).find('.ContactTypeCode').html());  
                           XML.Node("ContactValue", $(item).find('.ContactValue').html()); 
                        XML.EndNode(); //End Table : ContactInfo
                    });
                     
                    //Remittance  
                    if (IsRemitVisible) {
                        //Remittance Info
                        $('#tblRemittanceList tbody').find('tr').each(function (index, item ) {
                            XML.BeginNode("RemitInfo"); //Table : RemitInfo
                               // Columns : 
                               XML.Node("RowID", index + '');  // cast string  
                               XML.Node("ModePaymentCode", $(item).find('.ModePaymentCode').html());  
                               XML.Node("BankCode", $(item).find('.BankCode').html()); 
                               XML.Node("RemitCompanyName", $(item).find('.RemitCompanyName').html()); 
                               XML.Node("RemitAmount", $(item).find('.RemitAmount').html()); 
                               XML.Node("DatePaid", $(item).find('.DatePaid').html()); 
                            XML.EndNode(); //End Table : RemitInfo
                        });
                        
                        //Remittance Images 
                        //Note : Seperate loop to collect all images consecutively 
                        $('#tblRemittanceList tbody').find('tr').each(function (index, item ) {
                            var RowID =  index + '';
                            // Save image to another
                            $(this).find('img').each(function (indexImg, itemImg ) {
                                XML.BeginNode("RemitImages"); //Table : RemitImages
                                   // Columns : 
                                   XML.Node("RowID", RowID); // use index of per remittance
                                   XML.Node("src", $(itemImg).attr('src'));  
                                   XML.Node("title", $(itemImg).attr('title')); 
                                   XML.Node("savedname", $(itemImg).data('savedname'));  
                                XML.EndNode(); //End Table : RemitImages
                            });
                        });
                    };
                    
                    //Documents 
                    if (IsDocVisible) {
                        //Documents Images : DocInfoImages
                       $('#tblDocumentsList tbody').find('tr').each(function (index, item ) {
                            var DocTypeCode = $(item).find('.DocumentTypeCode').html();
                           
                            // Save image to another Table : DocInfoImages
                            $(this).find('img').each(function (indexImg, itemImg ) {
                                XML.BeginNode("DocInfoImages"); 
                                   // Columns : 
                                   XML.Node("DocumentTypeCode", DocTypeCode);
                                   XML.Node("src", $(itemImg).attr('src'));  
                                   XML.Node("title", $(itemImg).attr('title')); 
                                   XML.Node("savedname", $(itemImg).data('savedname'));  
                                XML.EndNode(); //End Table : DocInfoImages 
                            });
                        });
                    };
                    
                    //Seller : SellerInfo
                    if (IsSellerVisible) {
                        //Seller Info
                        $('#tblSellerList tbody').find('tr').each(function (index, item ) {
                            XML.BeginNode("SellerInfo"); //Table : SellerInfo
                               // Columns : 
                               XML.Node("RowID", index + '');  // cast string
                               XML.Node("SellerPositionCode", $(item).find('.SellerPositionCode').html());
                               XML.Node("AgentCode", $(item).find('.AgentCode').html());
                               XML.Node("IsAffli", $(item).find('.IsAffli').html());
                               XML.Node("AgentName", $(item).find('.AgentName').html());
                               XML.Node("NetworkStructure", $('#txbNetworkStructure').val()); // one network structure value only
                            XML.EndNode(); //End Table : SellerInfo
                        }); 
                    };
                     
                    //Adjoining Unit : AdjoiningUnit
                    if (IsSellerVisible) {
                        $('#tblAdjoiningUnit tbody').find('tr').each(function (index, item ) {
                            XML.BeginNode("AdjoiningUnit"); //Table : AdjoiningUnit
                               // Columns : 
                               XML.Node("RowID", index + '');  
                               XML.Node("ReferenceObject", $(item).find('.ReferenceObject').html());
                            XML.EndNode(); //End Table : AdjoiningUnit
                        }); 
                    };
                     
                       
                 XML.Close();//End Dataset or EndNode()
                      
                 // Save using web method
                 $.ajax({
                    url:'unithold.aspx/SaveData',
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
                                       displayError($.parseJSON(data.d)[0].ErrorMessage,'divErrorMsgBox','alert-success'); 
                                      // Hide all div
                                        $('#divPersonalInformation').css('display','none');
                                        $('#divBuyerContact').css('display','none');            
                                        $('#divUnitInformation').css('display','none');     
                                        $('#divRemittance').css('display','none');     
                                        $('#divSalesLocation').css('display','none');     
                                        $('#divDocuments').css('display','none');     
                                        $('#divSellerInformation').css('display','none');
                                        $('#divSave').css('display', 'none');
                                        $('#divAllocation').css('display', 'none');               
                                   }
                                   else
                                   { displayError($.parseJSON(data.d)[0].ErrorMessage); 
                                   }
                                   
                                 }
                             },
                    error: function (xhr) {
                            var err = $.parseJSON(xhr.responseText);                        
                            displayError('Error while holding unit : ' + err.Message); 
                       },
                    complete: function () {$('#btnClickToHold').disabled=false;}
                 }); 
//                 
//                // Hide all controls, leave on div message
//                $('#divMain').css('display','none'); 

                $('#btnClickToHold').disabled=false;    
                return false;
             });
             
             //---------------- BUTTON -----------------
             
             //---------------- CHECKBOX -----------------
             $('#chbNotYetAffiliated').click( function () 
             {
                 if ($('#chbNotYetAffiliated').parent('span').attr('class') == 'checked')
                 {  $('#divSellerNotAffiliated').css('display','block');
                    $('#divSellerAffiliated').css('display','none'); 
                 }
                 else
                 { $('#divSellerNotAffiliated').css('display','none');
                    $('#divSellerAffiliated').css('display','block'); 
                 };
             
             
             });
             
             
             //---------------- CHECKBOX -----------------
            
             //******************************  EVENTS  ****************************
          
          
             // Load First
             PopulateAllocation();
             
             // Bind table row click for delete button
             $('table').on('click', 'td .delete', function (event) {
                // Add or Remove the class on clicking the table row
                $(this).closest('tr').fadeTo(400, 0, function () { 
                    var tableID  = $(this).parents('table').attr('id');
                    $(this).remove(); 
                    //recompute total remittance
                    if (tableID == 'tblRemittanceList')
                    { $('#txbTotalRemittanceList').val(formatCurrency(GetTotalRemittance()));
                    };                  
                    
                });
                return false;
            });  
          }); //End Doc rdy
           
          function GetImageGalleryHtml(fileUploadID)
          {
             var liPix = '';          
              //FUC_GetImagesJSON('fileuploadId') return arraylist ImageName,ImageSrc : function in custom fileupload js
              var imageslist = JSON.parse(FUC_GetImagesJSON(fileUploadID));
              $(imageslist).each(function (index,item){
                   liPix = liPix 
                   + '<li class="thumbnail" style="margin-bottom: 5px !important">' 
                   + '<img src="' + item.ImageSrc + '" ' 
                   + ' title="' + item.ImageName + '" ' 
                   + ' data-savedname="' + item.ImageSavedName + '" style="width:70px; height:70px;" /></li>';
              });
              
              var ulGallery = '';
              if ( liPix != '')
              { var ulGallery =  '<ul class="thumbnails gallery">' + liPix + '</ul>';
               };
               
              return ulGallery 
          }; 
          
     
          
 </script>
 
    
    <!-- The jQuery UI widget factory, can be omitted if jQuery UI is already included -->
    <script type="text/javascript"  src="js/jquery.ui.widget.js"></script>
    <script type="text/javascript"  src="js/jquery.fileupload.js"></script>
    <script type="text/javascript"  src="js/fileupload.custom.js"></script> 
    <script type="text/javascript"  src="js/XMLWriter.js"></script> <!--ref: http://www.codeproject.com/Articles/12504/Writing-XML-using-JavaScript -->
        
</asp:Content>
