using ecrm.Infrastructure.Enum;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ecrm.Models.LeadsViewModels
{
    public class LeadsListViewModel
    {
        public LeadsListViewModel()
        {
            CurrentPageIndex = CurrentPageIndex > 0 ? CurrentPageIndex : 1;
        }
        public IList<LeadsListItemViewModel> Leads { get; set; }
        public IEnumerable<SelectListItem> Downlines { get; set; }

        #region Filtering-related properties
        public int? DownlineSellerID { get; set; }
        public string PartialLeadName { get; set; }
        public LeadStatusEnum LeadStatus { get; set; }
        #endregion

        #region Sorting-related properties
        public LeadsListSortFieldsEnum SortBy { get; set; }

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

        #region Bulk action properties
        public IList<int> LeadIDs { get; set; }
        #endregion
    }
}