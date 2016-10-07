using System.Threading.Tasks;
using ecrm.Service.Messages;

namespace ecrm.Service
{
    public interface ILeadsService
    {
        Task<LeadsResponse> GetDownlines(LeadsRequest leadsListingRequest);
        Task<LeadsResponse> GetLeads(LeadsRequest leadsListingRequest);        
        Task<LeadsResponse> AddLeadToSeller(LeadsRequest request);
        Task<LeadsResponse> ConvertLeadsToProspect(LeadsRequest request);
        Task<LeadsResponse> GetLeadInfo(LeadsRequest leadInfoRequest);
        LeadsResponse GetOfferingsForLead(LeadsRequest leadOfferingRequest);
        LeadsResponse GetActivitiesForLead(LeadsRequest leadActivityRequest);
      
    }
}
