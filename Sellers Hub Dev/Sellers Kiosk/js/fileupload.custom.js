/*
 * jQuery File Upload Custom
 * Based on Jquery file upload (blueimp)
 * 2015
 */
 
  var url = 'HandlerFileUpload.ashx';
         
   $(document).ready(function () { 
        
        // Initialize layout
        var fuc = $('.fileuploadcustom');
        fuc.empty();
        
        // select files button 
        var btnAdd = $('<span/>').addClass('btn fileinput-button').html( '<i class="icon-picture"></i> Select files...').css('margin-bottom','9px');   
        $('<input/>').attr({type : 'file', name : 'files[]', 'multiple':'true'}).appendTo(btnAdd);
        btnAdd.appendTo(fuc);
        
        // alert div
        var alert  = $('<div/>').addClass('alert alert-error').css('display','none') 
        alert.appendTo(fuc);
                
       // progress bar
       var progressbar = $('<div/>').addClass('fade').html( 
                             $('<div/>').addClass('progress progress-striped progress-success active').html(
                                 $('<div/>').addClass('bar')
                             )
                         );
       progressbar.css('display','none');                  
       progressbar.appendTo(fuc);
       
       // files container 
       var files =  $('<div/>').addClass('files');
       files.appendTo(fuc);
       
       var error = '';
       
       // FileUpload events 
       $('.fileuploadcustom').each(function () {
           $(this).fileupload({
                    url: url,
                    dataType: 'json', 
                    start: function(e) {
                        error = ''; 
                        
                        $(this).find('.progress').parent().css('display','block');
                        $(this).find('.progress').parent().addClass('in');
                      
                       },
                    done: function (e, data) {
                        var files = $(this).find('.files');    
                    
                        $(data).each(function (index, item){
                           if (item.result.Value._errorMSG != '' && item.result.Value._errorMSG != null)
                           { error = error + item.result.Value._name + ' : ' + item.result.Value._errorMSG + '<br/>';
                            }
                           else
                           {
                             var btnDelete = $('<button/>', {
                                    'class':'btn btn-danger btn-mini imageitem',
                                    'data-type':'DELETE', 
                                    'data-url':item.result.Value.delete_url,
                                    'data-name':item.result.Value._name,
                                    'data-savedname':item.result.Value._savedname
                             }).html('<i class="icon icon-trash icon-white"></i>');
                             
                             // add delete button per upload file                                     
                             $('<p />').html($('<span />').html(item.result.Value._name).css('padding-left','5px')).prepend(btnDelete).appendTo(files);
                                
                             // add delete button event
                             $(btnDelete).click(function () { 
                                $.ajax({
                                    url:item.result.Value.delete_url,
                                    type: 'POST'
                                });
                                $('[data-url="' + item.result.Value.delete_url +'"]').parent().remove();  
                              });
                            }
                         });
                       },
                    progressall: function (e, data) {
                        var progress = parseInt(data.loaded / data.total * 100, 10);
                        
                        $(this).find('.progress .bar').css( 'width', progress + '%' );
                          
                        if (progress >=100)
                        {
                           $(this).find('.progress').parent().removeClass('in'); 
                           $(this).find('.progress').parent().css('display','none');
                           $(this).find('.progress .bar').css('width', '0%');
                        }
                    },
                    stop: function(e) {
                       if (error != "") 
                       { $(this).find('.alert').html($('<div/>').html(error));
                         $(this).find('.alert').css('display','block');
                       }
                       else
                       { $(this).find('.alert').empty();
                         $(this).find('.alert').css('display','none');
                       }
                    }
                    
                }).prop('disabled', !$.support.fileInput)
                    .parent().addClass($.support.fileInput ? undefined : 'disabled');
                    
         }); 
    });
    
    function ImageItem(ImageName,ImageSrc,ImageSavedName)
     {
        this.ImageName = ImageName;
        this.ImageSrc = ImageSrc; 
        this.ImageSavedName = ImageSavedName; 
     }
     
     function FUC_GetImagesJSON(id)
     {
        var fileupload = document.getElementById(id)
        var arrayList = []; 
        // collect image name and src to arraylist
        $(fileupload).find('.imageitem').each(function (index,item){
           var ImageName = $(this).data('name');
           var ImageSrc = $(this).data('url');
           var ImageSavedName = $(this).data('savedname'); 
           arrayList.push(new ImageItem(ImageName,ImageSrc,ImageSavedName));                   
        }); 
                                    
                                    
        // convert array to json
        console.log(JSON.stringify(arrayList));
        return JSON.stringify(arrayList);
     };
    
     function FUC_ClearAllFiles(id)
     {  var fileupload = document.getElementById(id);
        $(fileupload).find('.files').empty();
     };
