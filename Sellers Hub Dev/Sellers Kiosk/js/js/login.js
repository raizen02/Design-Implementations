function showMessage(isErr, errMsg)
{            
    if (1 == isErr) 
    {
        $('#divMessage').slideDown();
        $('#divMessage').addClass('error');
        $('#divMessage').removeClass('success');
        $('#divMessage').html('<img src="images/error.png" /><span>' + errMsg + '</span>');
    } 
    else 
    {                
        $('#divMessage').slideDown();
        $('#divMessage').addClass('success');
        $('#divMessage').removeClass('error');
        $('#divMessage').html('<img src="images/check.png" /><span>' + errMsg + '</span>');
    }
}

function hideMessage()
{   
    $('.form input').focus(
        function()
        {
            $('#divMessage').slideUp();
        }
    );
}

function placeholder()
{
            $('[placeholder]').focus(function() {
            var input = $(this);
            if (input.val() == input.attr('placeholder')) {
                input.val('');
                input.removeClass('placeholder');

                if (input.attr('placeholder') == 'Password')
                {   
                    this.type='password';
                }                
            }
        })
        
        .blur(function() {
            var input = $(this);
            if (input.val() == '' || input.val() == input.attr('placeholder')) {
                input.addClass('placeholder');
                input.val(input.attr('placeholder'));
                this.type='text';
            }                   
            else if (input.attr('placeholder') == 'Password' && input.val() != input.attr('placeholder'))
            {   
                this.type='password';
            }                
        })
        
        .blur();
}