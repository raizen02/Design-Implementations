using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ecrm.Domain.Model
{
    public class Seller : BaseDomainModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SellerID { get; set; }
        public int UplineID { get; set; }
        [Required]
        [StringLength(255)]
        public string Name { get; set; }
        [Required]
        public int RankLevel { get; set; }
        [Required]
        [StringLength(255)]
        public string Status { get; set; }
        [Required]
        [StringLength(10)]
        public string SellerCode { get; set; }
        [Required]
        [StringLength(10)]
        public string UplineSellerCode { get; set; }
        [Required]
        [StringLength(500)]
        [Index("IX_Sellers_SellerHierarchy")]
        public string SellerHierarchy { get; set; } // Ex. [ASH1][SD1][SM1][SC1]

        #region Computed properties
        public string RankLevelString
        {
            get { return Resources.ResourceManager.GetString("RankLevel" + RankLevel); }
        }
        #endregion

        #region Navigation properties
        [JsonIgnore]
        [ForeignKey("UplineID")]
        public virtual Seller Upline { get; set; }
        [JsonIgnore]
        public virtual IList<Lead> Leads { get; set; }
        #endregion

        #region Class methods
        public Lead AddLead(Lead lead)
        {
            lead.SellerCode = this.SellerCode;
            lead.SellerHierarchy = this.SellerHierarchy;
            lead.CreatedDate = DateTime.Now;
            lead.UpdatedDate = DateTime.Now;
            this.Leads.Add(lead);
            return lead;
        }
        #endregion
    }
}