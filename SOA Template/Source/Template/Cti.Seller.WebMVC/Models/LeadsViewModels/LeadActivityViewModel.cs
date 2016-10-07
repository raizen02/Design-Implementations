using ecrm.Infrastructure.Enum;
using System;
using System.Collections.Generic;
using System.Configuration;

namespace ecrm.Models.LeadsViewModels
{
    public class LeadActivityViewModel
    {
        public LeadActivityViewModel()
        {
            CurrentPageIndex = CurrentPageIndex > 0 ? CurrentPageIndex : 1;
        }

        public int LeadID { get; set; }
        public int ActivityID { get; set; }
        public IList<LeadActivityItemViewModel> LeadActivities { get; set; }

        #region Filtering-related properties
        public ActivityLeadTaskEnum LeadTask { get; set; }
        public string PartialProject { get; set; }  
        public ActivityLeadTaskEnum NextStep { get; set; }
        public ClientFeedbackEnum ClientFeedback { get; set; }
        public LeadActivitySearchFilterFieldEnum SearchFilter { get; set; }
        #endregion

        #region Sorting-related properties
        public LeadActivitySortFieldEnum SortBy { get; set; }

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