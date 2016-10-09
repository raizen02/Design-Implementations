function showMessage(isErr, errMsg)
{            
    if (1 == isErr) 
    {
        $('#message').slideDown();
        $('#message').addClass('error');
        $('#message').removeClass('success');
        $('#message').html('<img src="images/error.png" /><span>' + errMsg + '</span>');
    } 
    else 
    {                
        $('#message').slideDown();
        $('#message').addClass('success');
        $('#message').removeClass('error');
        $('#message').html('<img src="images/check.png" /><span>' + errMsg + '</span>');
    }
}

function hideMessage()
{   
    $('.form input').focus(
        function()
        {
            $('#message').slideUp();
        }
    );
}

function redirect()
{
    window.open('selProfileInfo.aspx', '_self');
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