using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ecrm.Models.LeadsViewModels
{
    public class LeadsListItemViewModel
    {
        [Display(Name = "LeadID", ResourceType = typeof(Resources))]
        public string LeadID { get; set; }

        [Display(Name = "LeadName", ResourceType = typeof(Resources))]
        public string Name { get; set; }

        [Display(Name = "Contacts", ResourceType = typeof(Resources))]
        [DataType(DataType.PhoneNumber)]
        public IList<string> Contacts { get; set; }

        [Display(Name = "Email", ResourceType = typeof(Resources))]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Display(Name = "CreatedDate", ResourceType = typeof(Resources))]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime CreatedDate { get; set; }

        public int Status { get; set; }

        [Display(Name = "LeadStatus", ResourceType = typeof(Resources))]
        public string LeadStatus { get; set; }

        [Display(Name = "LeadAging", ResourceType = typeof(Resources))]
        public int LeadAging { get; set; }
    }
}