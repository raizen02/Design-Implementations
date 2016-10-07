using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;


namespace ecrm.Models.LoginViewModels
{
    public class LoginViewModel
    {
        [Display(Name = "Username", ResourceType = typeof(Resources))]
        [RegularExpression(@"^[0-9A-Za-z ]+$", ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "FieldMustBeAlphanumeric")]
        [Required(ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "FieldIsRequired")]
        [StringLength(15, ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "FieldsMustNotExceed15Chars")]
        public string Username { get; set; }

        [Display(Name = "Password", ResourceType = typeof(Resources))]
        [RegularExpression(@"^[0-9A-Za-z ]+$", ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "FieldMustBeAlphanumeric")]
        [Required(ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "FieldIsRequired")]
        [StringLength(15, MinimumLength = 5, ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "FieldMustBe5To15Chars")]
        public string Password { get; set; }
    
    }
}