using ecrm.Infrastructure.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Resources;
using System.Web;
using System.Web.Mvc;

namespace ecrm.Models.LeadsViewModels
{
    public class LeadOfferingItemViewModel
    {
        public int OfferingID { get; set; }

        [Display(Name = "ProjectName", ResourceType = typeof(Resources))]
        [Required(ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "FieldIsRequired")]
        public string OfferProjectName { get; set; }

        [Display(Name = "UnitNo", ResourceType = typeof(Resources))]
        public string UnitNo { get; set; }

        [Display(Name = "UnitAmount", ResourceType = typeof(Resources))]
        [Required(ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "FieldIsRequired")]
        [Range(1, double.MaxValue, ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "FieldsMustBeGreaterThan0")]
        public double UnitAmount { get; set; }

        public int Probability { get; set; }

        [Display(Name = "Probability", ResourceType = typeof(Resources))]
        [Required(ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "FieldIsRequired")]
        public string ProbabilityString { get; set; }

        [Display(Name = "ReserveFeeNo", ResourceType = typeof(Resources))]
        [RegularExpression(@"^[0-9A-Za-z ]+$", ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "FieldMustBeAlphanumeric")]
        public string ReserveFeeNo { get; set; }

        [Display(Name = "CreatedDate", ResourceType = typeof(Resources))]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime CreatedDate { get; set; }

        public int LeadID { get; set; }
        public int ProjectID { get; set; }

        #region Dropdown Lists
        private int enumIntValue;
        public IEnumerable<SelectListItem> ProjectNames
        { //TODO: This should not be here 
            get
            {
                IList<SelectListItem> projectNames = new List<SelectListItem>();
                projectNames.Add(new SelectListItem { Text = "100 West Makati", Value = "1" });
                projectNames.Add(new SelectListItem { Text = "8 Spatial Davao", Value = "2" });
                projectNames.Add(new SelectListItem { Text = "Amare Homes", Value = "3" });
                projectNames.Add(new SelectListItem { Text = "Brentville International", Value = "4" });
                return projectNames;
            }
        }

        public IEnumerable<SelectListItem> Probabilities
        {
            get
            {
                IList<SelectListItem> probabilities = new List<SelectListItem>();
                foreach (OfferingProbabilityEnum probabilitiesEnum in Enum.GetValues(typeof(OfferingProbabilityEnum)))
                {
                    enumIntValue = (int)probabilitiesEnum;
                    probabilities.Add(new SelectListItem { Text = Resources.ResourceManager.GetString("OfferingProbability" + enumIntValue.ToString()), Value = enumIntValue.ToString() });
                }
                return probabilities;
            }
        }
        #endregion
    }
}

