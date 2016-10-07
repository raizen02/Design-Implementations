using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ecrm.Domain.Model
{
    public class Offering : BaseDomainModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OfferingID { get; set; }
        [StringLength(255)]
        public string UnitNo { get; set; }
        [Required]
        public double UnitAmount { get; set; }
        [Required]
        public int Probability { get; set; }
        [StringLength(255)]
        public string ReserveFeeNo { get; set; }
        public int LeadID { get; set; }
        public int ProjectID { get; set; }

        #region Computed properties
        public string OfferProjectName
        {
            get { return this.Project.ProjectName; }
        }
        public string ProbabilityString
        {
            get { return Resources.ResourceManager.GetString("OfferingProbability" + (int)this.Probability); }
        }
        #endregion

        #region Navigation Properties
        public virtual Lead Lead { get; set; }
        public virtual Project Project { get; set; }
        #endregion
    }
}