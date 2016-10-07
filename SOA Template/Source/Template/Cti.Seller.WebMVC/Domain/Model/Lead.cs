using Microsoft.Linq.Translations;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Resources;

namespace ecrm.Domain.Model
{
    public class Lead : BaseDomainModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int LeadID { get; set; }
        [Required]
        [StringLength(30)]
        public string FirstName { get; set; }
        [Required]
        [StringLength(30)]
        public string MiddleName { get; set; }
        [Required]
        [StringLength(30)]
        public string LastName { get; set; }
        [Required]
        [StringLength(30)]
        public string MobileNo { get; set; }
        [StringLength(30)]
        public string LandlineNo { get; set; }
        [StringLength(255)]
        public string Email { get; set; }
        [StringLength(50)]
        public string Company { get; set; }
        [StringLength(50)]
        public string Position { get; set; }
        [Required]
        [StringLength(30)]
        public string Street { get; set; }
        [Required]
        [StringLength(100)]
        public string City { get; set; }
        [StringLength(30)]
        public string Province { get; set; }
        [Required]
        [StringLength(30)]
        public string Country { get; set; }
        [Required]
        public int LeadSource { get; set; }
        [Required]
        public int LeadTouchpoint { get; set; }
        [Required]
        public int Status { get; set; }
        [Required]
        public int SellerID { get; set; }
        [Required]
        [StringLength(10)]
        public string SellerCode { get; set; }
        [Required]
        [StringLength(500)]
        public string SellerHierarchy { get; set; }

        #region Computed properties
        private static readonly CompiledExpression<Lead, string> fullNameExpression = DefaultTranslationOf<Lead>.Property(l => l.Name).Is(l => l.FirstName.Trim() + " " + (l.MiddleName.Trim() != null ? l.MiddleName.Trim() : "") + " " + l.LastName.Trim());

        public string Name
        {
            get { return fullNameExpression.Evaluate(this); }
        }

        public IList<string> Contacts
        {
            get { return new List<string> { MobileNo, LandlineNo }; }
        }

        public string LeadStatus
        {
            get { return Resources.ResourceManager.GetString("LeadStatus" + Status); }
        }

        public int LeadAging
        {
            get { return (int)(DateTime.Now - CreatedDate).TotalDays; }
        }

        public string LeadSourceString
        {
            get { return Resources.ResourceManager.GetString("LeadSource" + LeadSource); }
        }

        public string LeadTouchpointString
        {
            get { return Resources.ResourceManager.GetString("LeadTouchpoint" + LeadTouchpoint); }
        }
        #endregion

        #region Navigation properties
        [JsonIgnore]
        public virtual Seller Seller { get; set; }
        [JsonIgnore]
        public virtual IList<Offering> Offerings { get; set; }
        [JsonIgnore]
        public virtual IList<Activity> Activities { get; set; }
        #endregion

        #region Class methods
        #endregion
    }
}