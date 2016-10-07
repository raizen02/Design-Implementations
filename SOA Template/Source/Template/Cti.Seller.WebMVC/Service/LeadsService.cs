using ecrm.Domain.Model;
using ecrm.Infrastructure.AutoMapper;
using ecrm.Infrastructure.Enum;
using ecrm.Infrastructure.Logging;
using ecrm.Infrastructure.Repository;
using ecrm.Models.LeadsViewModels;
using ecrm.Service.Messages;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;


namespace ecrm.Service
{
    public class LeadsService : ILeadsService
    {
        private IRepositoryFactory _factory;

        public LeadsService() : this(new RepositoryFactory())
        { }

        public LeadsService(IRepositoryFactory factory)
        {
            _factory = factory;
        }

        public async Task<LeadsResponse> GetDownlines(LeadsRequest leadsRequest)
        {
            EcrmEventSource.Log.MethodStart(this.GetType().FullName);

            IList<SellerViewModel> downlines = null;
            using (var repository = _factory.CreateLeadsRepository())
            {
                var Mapper = AutoMapperConfiguration.MapperConfiguration.CreateMapper();
                var downlinesResult = await repository.GetDownlinesForSeller(leadsRequest.Seller.SellerID);
                downlines = Mapper.Map<IList<Seller>, IList<SellerViewModel>>(downlinesResult);
            }

            LeadsResponse leadsResponse = new LeadsResponse();
            leadsResponse.LeadsList = new LeadsListViewModel();
            leadsResponse.LeadsList.Downlines = new SelectList(downlines, "SellerID", "Name");
            leadsResponse.Success = true;
            leadsResponse.Message = "success";

            EcrmEventSource.Log.MethodStop(this.GetType().FullName);

            return leadsResponse;
        }

        public async Task<LeadsResponse> GetLeads(LeadsRequest leadsRequest)
        {
            EcrmEventSource.Log.MethodStart(this.GetType().FullName);

            IList<LeadsListItemViewModel> leads = null;

            int totalRecordCount = 0;  
            var request = leadsRequest.LeadsList;

            using (var repository = _factory.CreateLeadsRepository())
            {
                var Mapper = AutoMapperConfiguration.MapperConfiguration.CreateMapper();
               
                var leadsResult = await repository.GetDownlinesLeadsForSeller(sellerID: leadsRequest.Seller.SellerID
                                                                            , downlineID: request.DownlineSellerID
                                                                            , status: request.LeadStatus
                                                                            , leadName: request.PartialLeadName
                                                                            , sortBy: request.SortBy
                                                                            , ascending: request.SortAscending
                                                                            , currentPage: request.CurrentPageIndex
                                                                            , pageSize: request.PageSize);
                leads = Mapper.Map<IList<Lead>, IList<LeadsListItemViewModel>>(leadsResult.Leads);
                totalRecordCount = leadsResult.TotalRecordCount;
            }

            LeadsResponse leadsResponse = new LeadsResponse();
            leadsResponse.LeadsList = new LeadsListViewModel();
            leadsResponse.LeadsList.Leads = leads;
            leadsResponse.LeadsList.TotalRecordCount = totalRecordCount;
            leadsResponse.LeadsList.CurrentPageIndex = request.CurrentPageIndex;


            leadsResponse.Success = true;
            leadsResponse.Message = "success";

            EcrmEventSource.Log.MethodStop(this.GetType().FullName);

            return leadsResponse;
        }

        public async Task<LeadsResponse> GetLeadInfo(LeadsRequest leadInfoRequest)
        {
            EcrmEventSource.Log.MethodStart(this.GetType().FullName);

            LeadsResponse leadOptionsResponse = new LeadsResponse();

            LeadInfoViewModel leads = null;
            using (var repository = _factory.CreateLeadsRepository())
            {
                var Mapper = AutoMapperConfiguration.MapperConfiguration.CreateMapper();
                var leadsResult = await repository.GetLeadInfo(leadInfoRequest.LeadInfo.LeadID);
                leads = Mapper.Map<Lead, LeadInfoViewModel>(leadsResult);
            }

            leadOptionsResponse.Success = true;
            leadOptionsResponse.Message = "success";
            leadOptionsResponse.LeadsInfo = leads;
            /// END STATIC VALUES
            EcrmEventSource.Log.MethodStop(this.GetType().FullName);

            return leadOptionsResponse;
        }

        public async Task<LeadsResponse> AddLeadToSeller(LeadsRequest request)
        {
            EcrmEventSource.Log.MethodStart(this.GetType().FullName);
            LeadsResponse response = new LeadsResponse();
            using (var repository = _factory.CreateLeadsRepository())
            {
                var seller = await repository.GetSellerByID(request.Seller.SellerID);
                var Mapper = AutoMapperConfiguration.MapperConfiguration.CreateMapper();
                var lead = Mapper.Map<LeadInfoViewModel, Lead>(request.LeadInfo);
                var newLead = seller.AddLead(lead);
                var result = await repository.SaveChanges();
                var log = await repository.CreateAuditLog(request.Seller.SellerID, AuditLogRecordTypeEnum.Lead, AuditLogTransactionEnum.Add, newLead.LeadID, newLead);

                response.Success = Convert.ToBoolean(result);
                response.LeadsInfo = Mapper.Map<Lead, LeadInfoViewModel>(newLead);
                response.Success = Convert.ToBoolean(result);
                response.LeadsInfo = Mapper.Map<Lead, LeadInfoViewModel>(newLead);

            }
            EcrmEventSource.Log.MethodStop(this.GetType().FullName);
            return response;
        }

        public LeadsResponse GetOfferingsForLead(LeadsRequest offeringInfo)
        {
            EcrmEventSource.Log.MethodStart(this.GetType().FullName);

            LeadOfferingViewModel leadOfferingViewModel = new LeadOfferingViewModel();
            LeadsResponse leadOfferingResponse = new LeadsResponse();
                 
            using (var repo = _factory.CreateLeadsRepository())
            {
                var result = repo.GetOfferingsForLead(leadID: offeringInfo.LeadOfferingList.LeadID
                                                        , projectName: offeringInfo.LeadOfferingList.PartialProjectName
                                                        , unitNo: offeringInfo.LeadOfferingList.PartialUnitNo
                                                        , probability: offeringInfo.LeadOfferingList.Probability
                                                        , reserveFeeNo: offeringInfo.LeadOfferingList.ReserveFeeNo
                                                        , sortBy: offeringInfo.LeadOfferingList.SortBy
                                                        , ascending: offeringInfo.LeadOfferingList.SortAscending
                                                        , currentPage: offeringInfo.LeadOfferingList.CurrentPageIndex
                                                        , pageSize: offeringInfo.LeadOfferingList.PageSize);
                                
                var Mapper = AutoMapperConfiguration.MapperConfiguration.CreateMapper();
                var offerings = Mapper.Map<IList<Offering>, IList<LeadOfferingItemViewModel>>(result.Offerings);

                leadOfferingViewModel.LeadID = offeringInfo.LeadOfferingList.LeadID;
                leadOfferingViewModel.LeadOfferings = offerings;
                leadOfferingViewModel.CurrentPageIndex = offeringInfo.LeadOfferingList.CurrentPageIndex;
                leadOfferingViewModel.TotalRecordCount = result.TotalRecordCount;

            
            }

            leadOfferingResponse.Success = true;
            leadOfferingResponse.Message = "success";
            leadOfferingResponse.LeadOfferingList = leadOfferingViewModel;

            EcrmEventSource.Log.MethodStop(this.GetType().FullName);

            return leadOfferingResponse;
        }

        public LeadsResponse GetActivitiesForLead(LeadsRequest activityInfo)
        {
            EcrmEventSource.Log.MethodStart(this.GetType().FullName);

            LeadActivityViewModel leadActivityViewModel = new LeadActivityViewModel();
            LeadsResponse leadActivityResponse = new LeadsResponse();
               
            using (var repo = _factory.CreateLeadsRepository())
            {
                var result = repo.GetActivitiesForLead(leadID: activityInfo.LeadActivityList.LeadID
                                                        , leadTaskID: activityInfo.LeadActivityList.LeadTask
                                                        , projectName: activityInfo.LeadActivityList.PartialProject
                                                        , nextstep: activityInfo.LeadActivityList.NextStep
                                                        , cliendFeedback: activityInfo.LeadActivityList.ClientFeedback
                                                        , sortBy: activityInfo.LeadActivityList.SortBy
                                                        , ascending: activityInfo.LeadActivityList.SortAscending
                                                        , currentPage: activityInfo.LeadActivityList.CurrentPageIndex
                                                        , pageSize: activityInfo.LeadActivityList.PageSize);
                var Mapper = AutoMapperConfiguration.MapperConfiguration.CreateMapper();
                var activities = Mapper.Map<IList<Activity>, IList<LeadActivityItemViewModel>>(result.Activities);

                leadActivityViewModel.LeadID = activityInfo.LeadActivityList.LeadID;
                leadActivityViewModel.LeadActivities = activities;
                leadActivityViewModel.CurrentPageIndex = activityInfo.LeadActivityList.CurrentPageIndex;
                leadActivityViewModel.TotalRecordCount = result.TotalRecordCount;


            }

            leadActivityResponse.Success = true;
            leadActivityResponse.Message = "success";
            leadActivityResponse.LeadActivityList = leadActivityViewModel;

            EcrmEventSource.Log.MethodStop(this.GetType().FullName);

            return leadActivityResponse;
        }

        public async Task<LeadsResponse> ConvertLeadsToProspect(LeadsRequest request)
        {
            EcrmEventSource.Log.MethodStart(this.GetType().FullName);
            LeadsResponse response = new LeadsResponse();
            using (var repository = _factory.CreateLeadsRepository())
            {
                var recordsAffected = await repository.ConvertLeadsToProspectBatch(request.LeadsList.LeadIDs);
                var result = await repository.SaveChanges();
                var message = string.Format(Resources.AuditLogUpdateStatus, "{0}", (int)LeadStatusEnum.Prospect);
                await repository.CreateAuditLogBatch(request.Seller.SellerID, AuditLogRecordTypeEnum.Lead, AuditLogTransactionEnum.ConvertToProspect, request.LeadsList.LeadIDs, message);
            }
            EcrmEventSource.Log.MethodStop(this.GetType().FullName);
            return await GetLeads(request);
        }
    }
}