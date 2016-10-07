using ecrm.Domain.Model;
using ecrm.Infrastructure.Enum;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ecrm.Infrastructure.Repository
{
    public interface ILeadsRepository : IBaseRepository
    {
        Task<Seller> GetSellerByID(int sellerID);
        Task<IList<Seller>> GetDownlinesForSeller(int sellerID);
        Task<LeadsListValueModel> GetDownlinesLeadsForSeller(int sellerID, int? downlineID = null, LeadStatusEnum? status = null, string leadName = null, LeadsListSortFieldsEnum sortBy = LeadsListSortFieldsEnum.ID, bool ascending = true, int currentPage = 1, int pageSize = 10);
        Task<IList<int>> ConvertLeadsToProspectBatch(IList<int> leadIDs);
        Task<Lead> GetLeadInfo(int leadID);
        OfferingsValueModel GetOfferingsForLead(int leadID, string projectName = null, string unitNo = null, OfferingProbabilityEnum? probability = null, string reserveFeeNo = null, LeadOfferingSortFieldEnum sortBy = LeadOfferingSortFieldEnum.ID, bool ascending = true, int currentPage = 1, int pageSize = 10);
        ActivitiesValueModel GetActivitiesForLead(int leadID, ActivityLeadTaskEnum? leadTaskID = null, string projectName = null, ActivityLeadTaskEnum? nextstep = null, ClientFeedbackEnum? cliendFeedback = null, LeadActivitySortFieldEnum sortBy = LeadActivitySortFieldEnum.ID, bool ascending = true, int currentPage = 1, int pageSize = 10);

    }
}
