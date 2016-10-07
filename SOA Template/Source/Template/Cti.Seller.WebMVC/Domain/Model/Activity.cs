using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ecrm.Domain.Model
{
    public class Activity : BaseDomainModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ActivityID { get; set; }        
        [Required]
        [StringLength(255)]
        public string Activity_Date_2 { get; set; }
        [Required]
        public DateTime Activity_Date { get; set; }
        [StringLength(255)]
        public string Activity_Remarks { get; set; }
        public int LeadTaskID { get; set; }
        public int NextStepID { get; set; }
        public int ClientFeedbackID { get; set; } 
        public int LeadID { get; set; }
        public int ProjectID { get; set; }

        #region Computed properties              
        public string LeadTaskString {    
            get { return Resources.ResourceManager.GetString("LeadTask" + (int)this.LeadTaskID); }
        }
      
        public string ClientFeedbackString {
            get { return Resources.ResourceManager.GetString("ClientFeedback" + (int)this.ClientFeedbackID); }
        }
  
        public string NextStep {
            get { return Resources.ResourceManager.GetString("LeadTask" + ((int)this.LeadTaskID + 1)); }         
        }
             
        public string ActivityProjectName
        {
            get { return this.Project.ProjectName; }
        }
        #endregion

        #region Navigation Properties
        public virtual Lead Lead { get; set; }
        public virtual Project Project { get; set; }
        #endregion
    }
}