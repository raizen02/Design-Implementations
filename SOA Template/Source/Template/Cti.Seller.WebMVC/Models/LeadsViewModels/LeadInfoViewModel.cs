using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System;
using ecrm.Infrastructure.Enum;
using System.Resources;

namespace ecrm.Models.LeadsViewModels
{
    public class LeadInfoViewModel
    {
        #region Lead Info Properties
        public int LeadID { get; set; }

        [Display(Name = "FirstName", ResourceType = typeof(Resources))]
        [RegularExpression(@"^[0-9A-Za-z ]+$", ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "FieldMustBeAlphanumeric")]
        [Required(ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "FieldIsRequired")]
        [StringLength(30, MinimumLength = 2, ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "FieldMustBe2To30Chars")]
        public string FirstName { get; set; }

        [Display(Name = "MiddleName", ResourceType = typeof(Resources))]
        [RegularExpression(@"^[0-9A-Za-z ]+$", ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "FieldMustBeAlphanumeric")]
        [Required(ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "FieldIsRequired")]
        [StringLength(30, MinimumLength = 2, ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "FieldMustBe2To30Chars")]
        public string MiddleName { get; set; }

        [Display(Name = "LastName", ResourceType = typeof(Resources))]
        [RegularExpression(@"^[0-9A-Za-z ]+$", ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "FieldMustBeAlphanumeric")]
        [Required(ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "FieldIsRequired")]
        [StringLength(30, MinimumLength = 2, ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "FieldMustBe2To30Chars")]
        public string LastName { get; set; }

        [Display(Name = "MobileNo", ResourceType = typeof(Resources))]
        [Required(ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "FieldIsRequired")]
        [RegularExpression("^[0-9()+ /-]+$", ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "FieldsMustBeValidContactNo")]
        [StringLength(30, MinimumLength = 10, ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "FieldMustBe10To30Chars")]
        public string MobileNo { get; set; }

        [Display(Name = "LandlineNo", ResourceType = typeof(Resources))]
        [RegularExpression("^[0-9()+ /-]+$", ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "FieldsMustBeValidContactNo")]
        [StringLength(30, MinimumLength = 10, ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "FieldMustBe10To30Chars")]
        public string LandlineNo { get; set; }

        [Display(Name = "Email", ResourceType = typeof(Resources))]
        [DataType(DataType.EmailAddress, ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "FieldMustBeValidEmail")]
        [StringLength(50, ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "FieldsMustNotExceed50Chars")]
        public string Email { get; set; }

        [Display(Name = "Company", ResourceType = typeof(Resources))]
        [RegularExpression(@"^[0-9A-Za-z -.,]+$", ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "FieldMustBeAlphanumeric")]
        [StringLength(50, ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "FieldsMustNotExceed50Chars")]
        public string Company { get; set; }

        [Display(Name = "Position", ResourceType = typeof(Resources))]
        [RegularExpression(@"^[0-9A-Za-z ]+$", ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "FieldMustBeAlphanumeric")]
        [StringLength(50, ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "FieldsMustNotExceed50Chars")]
        public string Position { get; set; }

        [Display(Name = "Street", ResourceType = typeof(Resources))]
        [Required(ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "FieldIsRequired")]
        [StringLength(100, ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "FieldsMustNotExceed100Chars")]
        public string Street { get; set; }

        [Display(Name = "City", ResourceType = typeof(Resources))]
        [Required(ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "FieldIsRequired")]
        [StringLength(30, ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "FieldsMustNotExceed30Chars")]
        public string City { get; set; }

        [Display(Name = "Province", ResourceType = typeof(Resources))]
        [StringLength(30, ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "FieldsMustNotExceed30Chars")]
        public string Province { get; set; }

        [Display(Name = "Country", ResourceType = typeof(Resources))]
        [Required(ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "FieldIsRequired")]
        [StringLength(30, ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "FieldsMustNotExceed30Chars")]
        public string Country { get; set; }

        [Display(Name = "LeadSource", ResourceType = typeof(Resources))]
        [Range(1, int.MaxValue, ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "FieldIsRequired")]
        public int LeadSource { get; set; }


        public string LeadSourceString { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "FieldIsRequired")]
        [Range(1, int.MaxValue, ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "FieldIsRequired")]
        [Display(Name = "LeadTouchpoint", ResourceType = typeof(Resources))]
        public int LeadTouchpoint { get; set; }


        public string LeadTouchpointString { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "FieldIsRequired")]
        public int Status { get; set; }

        [Display(Name = "LeadStatus", ResourceType = typeof(Resources))]
        public string LeadStatus { get; set; }
        #endregion

        #region Dropdown lists     
        private int enumIntValue;
        public IEnumerable<SelectListItem> LeadSources
        {
            get
            {
                IList<SelectListItem> leadSource = new List<SelectListItem>();
                foreach (LeadSourceEnum leadSourceEnum in Enum.GetValues(typeof(LeadSourceEnum)))
                {
                    enumIntValue = (int)leadSourceEnum;
                    leadSource.Add(new SelectListItem { Text = Resources.ResourceManager.GetString("LeadSource" + enumIntValue.ToString()), Value = enumIntValue.ToString() });
                }
                return leadSource;
            }
        }
    
        public IEnumerable<SelectListItem> LeadTouchpoints
        {
            get
            {
                IList<SelectListItem> leadTouchpoints = new List<SelectListItem>();
                foreach (LeadTouchpointEnum leadTouchpointEnum in Enum.GetValues(typeof(LeadTouchpointEnum)))
                {
                    enumIntValue = (int)leadTouchpointEnum;
                    leadTouchpoints.Add(new SelectListItem { Text = Resources.ResourceManager.GetString("LeadTouchPoint" + enumIntValue.ToString()), Value = enumIntValue.ToString() });
                }
                return leadTouchpoints;
            }
        }
        public IEnumerable<SelectListItem> LeadStatuses
        {
            get
            {
                IList<SelectListItem> leadStatuses = new List<SelectListItem>();
                foreach (LeadStatusEnum leadStatusEnum in Enum.GetValues(typeof(LeadStatusEnum)))
                {
                    //TODO: Set the options during login and save in User object
                    enumIntValue = (int)leadStatusEnum;
                    if (leadStatusEnum != LeadStatusEnum.Prospect)
                    {
                        leadStatuses.Add(new SelectListItem { Text = Resources.ResourceManager.GetString("LeadStatus" + enumIntValue.ToString()), Value = enumIntValue.ToString() });
                    }
                }
                return leadStatuses;
            }
        }
        #endregion
    }
}