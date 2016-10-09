<%@ Page Language="VB"
    MasterPageFile="~/sellersHubMasterPage.master"
    AutoEventWireup="false"
    CodeFile="documentsupload.aspx.vb"
    Inherits="documentsupload"
    Title="Re-upload Documents - ORS | Sellers' HUB"%>
 
<asp:Content ID="cphContent" ContentPlaceHolderID="cphContent" runat="Server">
    <%-- File Upload CSS  --%>
    <link rel="stylesheet" href="css/jquery.fileupload.css" />
             
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
                <li><a href="#">Document Re-upload</a> </li>
            </ul>
        </div>
        <%------------Alert Error Message -------------%>
        <div data-div="alert" id="divErrorMsgBox">
        </div>

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
        <%------------ Document Information -------------%>
        <div id="divDocument" class="row-fluid">
            <div class="box span12">
                <div class="box-header well">
                    <h2>
                        <i class="icon-check"></i>&nbsp;Documents Required for Holding</h2>
                    <div class="box-icon">
                        <a class="btn btn-minimize btn-round"><i class="icon-chevron-up"></i></a>
                    </div>
                </div>
                <div class="box-content form-horizontal">
                    <%------------Alert Error Message -------------%>
                     <div data-div="alert"  id="divErrorMsgDocuInfo"> </div>
                    <%------------Alert Error Message -------------%>
                      
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
                    <%--Document List--%>
                    <h4>
                        <span>List of Document </span>
                    </h4>
                    <table id="tblDocumentsList" class="table table-bordered table-striped table-condensed" cellspacing="0" width="100%">
                        <thead>
                            <tr>
                                <th style="display:none">DocumentCode</th>
                                <th>Document Type</th>
                                <th>Images</th> 
                            </tr>
                        </thead> 
                        <tbody></tbody> 
                    </table>
                    
                    <div class="form-actions" style="padding-left: 0px;margin-bottom: 0px;">
                     <h4>
                            <span>Add New Documents</span>
                     </h4>
                    </div>
                    <div class="control-group">
                        <%------------Alert Error Message -------------%>
                        <div data-div="alert" data-validgroup="document" id="divErrorMsgBoxDocu"> </div>
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
                 </div>
                </div>
              </div>
            </div>
        </div>
        <%------------ Documents Information -------------%> 
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="cphContentBottom" runat="Server">
   
    <script type="text/javascript">
       
       var ajaxurlGetData = 'documentsupload.aspx/GetDataFromDB';
       
       $(document).ready(function () {
       
             // Global variable
             var DropdownDocumentType =  $('#ddlDocumentType');
          
            // Automatic Show/ Hide loading modal div     
            $(document).bind("ajaxSend", function(){
               $("#divLoading").show();
             }).bind("ajaxStop ", function(){
               $("#divLoading").hide();
             });
           
            // Initialize chosen css for selector
            $('.chosen-select-deselect').chosen({ allow_single_deselect: true,width:"100%" }); //,width:"95%"
 
            //Hide divdocu first
            $('#divDocument').css('display','none');     
           
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
                                            }  ,
                                      "sDom": 'lfr<"table_overflow"t>ip' 
                                     });
                
                // Display documents info  on selected row                   
                $('#tblSalesList tbody').on( 'click', 'tr', function () {
                    //clear message
                     displayError('');
                
                    //remove all selected row then apply css on selected row
                    $('#tblSalesList tbody tr').removeClass('selected');
                    $(this).addClass('selected');
                    
                    ClearField();
                    //Show and Load docs info
                    $('#divDocument').css('display','block');  
                    
                    var data = tblDataSource.row($(this)).data();
                    
                    //Populate existing documents
                    DisplayInfoOfCon(data.CompanyCode,data.ReservationNo); 
                    
                    $('#txbBuyerName').val(data.BuyerName);
                    $('#txbHideCompanyCode').val(data.CompanyCode);
                    $('#txbHideReservationNo').val(data.ReservationNo);
                    $('#txbResvNumber').val(data.ReservationNo); 
                    
                    $('html,body,window').animate({scrollTop: divDocument.offsetTop}, 500); 
                } );  
                
                //apply table css
                
                $('.table_overflow').css({'overflow':'auto','width':'100%'})
                 
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
                               {    // Table ID  
                                    // 0 : Existing documents of contract
                                    // 1 : Documents images
                                    // 2 : Document Type list for dropdown source
                               
                                    // Existing documents
                                    if ($(data[0]).length > 0) 
                                        // Columns
                                        //,DocumentType
                                        //,DocumentName 
                                        //,ImagesList
                                        $('#tblDocumentsList tbody').empty();

                                        var trRow = ''     
                                        $(data[0]).each(function (index, item) {
                                            // generate imagelist, filter by DocumentType
                                            var docuType = item.DocumentType;
                                            var liPix = '';  
                                            $(data[1]).each(function (index2,item2){
                                                if (docuType == item2.DocumentType)
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
                                        // generate html tablerow
                                        trRow = trRow +'<tr>' 
                                            + '<td class="DocumentCode" style="display:none" >' + item.DocumentType + '</td>' 
                                            + '<td >' + item.DocumentName + '</td>' 
                                            + '<td class="ImagesList" >' + ulGallery + '</td>' 
                                            + '</tr>'
                                    }); 
                                    $('#tblDocumentsList tbody').append(trRow); 
 
                                    // 2 : Document Type
                                    DropdownDocumentType.empty();
                                    DropdownDocumentType.append(new Option("", ""));
                                    $(data[2]).each(function (index, item) {
                                        DropdownDocumentType.append(new Option(item.Description, item.ID));                       
                                    });
                                    DropdownDocumentType.trigger("chosen:updated");
 
                                };
                            },
                       error: function (xhr) {
                            var err = $.parseJSON(xhr.responseText);                        
                            displayError('Error loading document type : ' + err.Message); 
                       }
                  });
            };  
            
            function ClearField()
            {   // clear selected file 
                FUC_ClearAllFiles('fucDocuments');
                // clear fields
                DropdownDocumentType.val('');
                DropdownDocumentType.trigger("chosen:updated");  
            };
            
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
             
            
             // Add Documents
             $('#btnAddDocument').click(function () {
             
                $('#btnAddDocument').disabled=true; 
                 
                // validate required field 
                if (formIsValid('document') == false)
                   {$('#btnAddDocument').disabled=false; 
                   return false;}; 
                 
                // Save Documents
                 var CompanyCode = $('#txbHideCompanyCode').val();
                 var ReservationNo = $('#txbHideReservationNo').val();
                                  
                
                // Generate xml for arguments
                 var XML = new XMLWriter();
                 // Start with beginnode
                 XML.BeginNode("Dataset"); 
                    //Documents Images  
                    var imageslist = JSON.parse(FUC_GetImagesJSON('fucDocuments'));
                    $(imageslist).each(function (index,item){
                         XML.BeginNode("DocInfoImages"); 
                           // Columns : 
                           XML.Node("CompanyCode", CompanyCode);
                           XML.Node("ReservationNo", ReservationNo);
                           XML.Node("DocumentTypeCode", DropdownDocumentType.val());
                           XML.Node("src", item.ImageSrc);  
                           XML.Node("title",  item.ImageName); 
                           XML.Node("savedname", item.ImageSavedName);  
                        XML.EndNode(); //End Table : DocInfoImages 
                      }); 
                 XML.Close();//End Dataset or EndNode()
                 
                  // Save using web method
                 $.ajax({
                    url:'documentsupload.aspx/SaveData',
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
                                   displayError($.parseJSON(data.d)[0].ErrorMessage,'divErrorMsgDocuInfo','alert-success'); 
                                  
                                  //****Refresh sales list, documents list****
                                    // Load first sales list, this will create new row and sort and filter cleared also
                                   PopulateOnHoldList();
                                   //Populate existing documents
                                   DisplayInfoOfCon(CompanyCode,ReservationNo); 
                                   ClearField(); 
                               }
                               else
                               { displayError($.parseJSON(data.d)[0].ErrorMessage,'divErrorMsgBoxDocu'); 
                               }
                             }
                         },
                    error: function (xhr) {
                            var err = $.parseJSON(xhr.responseText);                        
                            displayError('Error while saving document : ' + err.Message,'divErrorMsgBoxDocu'); 
                       },
                    complete: function () {$('#btnAddDocument').disabled=false;}
                 });  

                $('#btnAddDocument').disabled=false;    
                return false; 
             }); 
              
             /// Initialize 
             PopulateOnHoldList();
        
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
