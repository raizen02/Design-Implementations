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
    public class LeadActivityItemViewModel
    {

        public int ActivityID { get; set; }

       
        [Display(Name = "Activity_Date", ResourceType = typeof(Resources))]
        [Required(ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "FieldIsRequired")]
        public string Activity_Date_2 { get; set; }

        [Display(Name = "Activity_Date", ResourceType = typeof(Resources))]
        [Required(ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "FieldIsRequired")]
        [DataType(DataType.Date)]
        public DateTime Activity_Date { get; set; }

        [Display(Name = "Remarks", ResourceType = typeof(Resources))]
        [RegularExpression(@"^[0-9A-Za-z ]+$", ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "FieldMustBeAlphanumeric")]
        [StringLength(255, MinimumLength = 5, ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "FieldMustBe5To255Chars")]
        public string Activity_Remarks { get; set; }

        public int LeadTaskID { get; set; }

        public int ClientFeedbackID { get; set; }

        public int LeadID { get; set; }

        public int ProjectID { get; set; }


        [Display(Name = "ClientFeedBack", ResourceType = typeof(Resources))]
        [Required(ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "FieldIsRequired")]
        public string ClientFeedbackString { get; set; }

        [Display(Name = "NextStep", ResourceType = typeof(Resources))]
        public string NextStep { get; set; }

        [Display(Name = "ProjectName", ResourceType = typeof(Resources))]
        [Required(ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "FieldIsRequired")]
        public string ActivityProjectName { get; set; }

        [Display(Name = "ActivityDescription", ResourceType = typeof(Resources))]
        [Required(ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "FieldIsRequired")]
        public string LeadTaskString { get; set; }
        
        [Display(Name = "CreatedDate", ResourceType = typeof(Resources))]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime CreatedDate { get; set; }

        
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
        public IEnumerable<SelectListItem> ActivityDescriptions
        {
            get
            {
                IList<SelectListItem> activityDescriptions = new List<SelectListItem>();
                foreach (ActivityLeadTaskEnum activityDescriptionEnum in Enum.GetValues(typeof(ActivityLeadTaskEnum)))
                {
                    enumIntValue = (int)activityDescriptionEnum;
                    activityDescriptions.Add(new SelectListItem { Text = Resources.ResourceManager.GetString("LeadTask" + enumIntValue.ToString()), Value = enumIntValue.ToString() });
                }
                return activityDescriptions;
            }
        }
        public IEnumerable<SelectListItem> ClientFeedbacks
        {
            get
            {
                IList<SelectListItem> clientFeedbacks = new List<SelectListItem>();
                foreach (ClientFeedbackEnum clientFeedbackEnum in Enum.GetValues(typeof(ClientFeedbackEnum)))
                {
                    enumIntValue = (int)clientFeedbackEnum;
                    clientFeedbacks.Add(new SelectListItem { Text = Resources.ResourceManager.GetString("ClientFeedback" + enumIntValue.ToString()), Value = enumIntValue.ToString() });
                }
                return clientFeedbacks;
            }
        }
        public IEnumerable<SelectListItem> NextSteps
        {
            get
            {
                IList<SelectListItem> nextSteps = new List<SelectListItem>();
                foreach (ActivityLeadTaskEnum activityDescriptionEnum in Enum.GetValues(typeof(ActivityLeadTaskEnum)))
                {
                    enumIntValue = (int)activityDescriptionEnum;
                    nextSteps.Add(new SelectListItem { Text = Resources.ResourceManager.GetString("LeadTask" + enumIntValue.ToString()), Value = enumIntValue.ToString() });
                }
                return nextSteps;
            }
        }
        #endregion
    }
}