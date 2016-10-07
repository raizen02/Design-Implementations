using ecrm.Infrastructure.Enum;
using System;
using System.Collections.Generic;
using System.Configuration;

namespace ecrm.Models.LeadsViewModels
{
    public class LeadOfferingViewModel
    {
        public LeadOfferingViewModel()
        {
            CurrentPageIndex = CurrentPageIndex > 0 ? CurrentPageIndex : 1;
        }

        public int LeadID { get; set; }
        public int OfferingID { get; set; }
        public IList<LeadOfferingItemViewModel> LeadOfferings { get; set; }

        #region Filtering-related properties
        public string PartialProjectName { get; set; }
        public string PartialUnitNo { get; set; }
        public OfferingProbabilityEnum Probability { get; set; }
        public string ReserveFeeNo { get; set; }
        public LeadOfferingSearchFilterFieldEnum SearchFilter { get; set; }
        #endregion

        #region Sorting-related properties
        public LeadOfferingSortFieldEnum SortBy { get; set; }

        public bool SortAscending { get; set; }
        #endregion

        #region Paging-related properties
        public int CurrentPageIndex { get; set; }
        public int TotalRecordCount { get; set; }
        public int PageSize
        {
            get { return Convert.ToInt32(ConfigurationManager.AppSettings["PageSize"]); }
        }
        public int PageCount
        {
            get
            {
                var count = PageSize != 0 ? (TotalRecordCount / PageSize) + 1 : 0;
                return count;
            }
        }
        #endregion
    }
}