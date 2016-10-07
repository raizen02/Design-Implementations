using ecrm.Domain.Model;
using ecrm.Infrastructure.Enum;
using ecrm.Infrastructure.Logging;
using Microsoft.Linq.Translations;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System;
using System.Text;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

namespace ecrm.Infrastructure.Repository
{
    public class LeadsRepository : BaseRepository, ILeadsRepository
    {
        public async Task<Seller> GetSellerByID(int sellerID)
        {
            EcrmEventSource.Log.MethodStart(this.GetType().FullName);
            var seller = await _context.Sellers.FindAsync(sellerID);
            EcrmEventSource.Log.MethodStop(this.GetType().FullName);
            return seller;
        }
        public async Task<IList<Seller>> GetDownlinesForSeller(int sellerID)
        {
            EcrmEventSource.Log.MethodStart(this.GetType().FullName);
            var query = (from downline in _context.Sellers
                         where downline.SellerHierarchy.StartsWith((from seller in _context.Sellers
                                                                    where seller.SellerID == sellerID
                                                                    select seller.SellerHierarchy).FirstOrDefault())
                         select downline)
                         .AsNoTracking();
            var downlines = await query.ToListAsync();
            EcrmEventSource.Log.MethodStop(this.GetType().FullName);
            return downlines;
        }

        public async Task<LeadsListValueModel> GetDownlinesLeadsForSeller(int sellerID, int? downlineID = null, LeadStatusEnum? status = null, string leadName = null, LeadsListSortFieldsEnum sortBy = LeadsListSortFieldsEnum.ID, bool ascending = true, int currentPage = 1, int pageSize = 10)
        {
            EcrmEventSource.Log.MethodStart(this.GetType().FullName);
            var query = (from lead in _context.Leads
                         where lead.SellerHierarchy.StartsWith((from seller in _context.Sellers
                                                                where seller.SellerID == sellerID
                                                                select seller.SellerHierarchy).FirstOrDefault())
                         select lead)
                         .AsNoTracking();

            #region Filtering
            if (downlineID != null)
                query = query.Where(l => l.SellerHierarchy.StartsWith((from seller in _context.Sellers
                                                                       where seller.SellerID == downlineID
                                                                       select seller.SellerHierarchy).FirstOrDefault()));

            if (status != 0 && status != null)
                query = query.Where(l => l.Status == (int)status);

            if (leadName != null) { 
                leadName = Regex.Replace(leadName, @"\s+", " ");
                query = query.Where(l => l.Name.Contains(leadName.Trim())).WithTranslations();
            }

            var TotalRecordCount = query.Count();
            #endregion

            #region Sorting
            switch (sortBy)
            {
                case LeadsListSortFieldsEnum.ID:
                    if (ascending)
                        query = query.OrderBy(l => l.LeadID);
                    else
                        query = query.OrderByDescending(l => l.LeadID);
                    break;
                case LeadsListSortFieldsEnum.LeadName:
                    if (ascending)
                        query = query.OrderBy(l => l.Name);
                    else
                        query = query.OrderByDescending(l => l.Name);
                    break;
                case LeadsListSortFieldsEnum.LeadStatus:
                    if (ascending)
                        query = query.OrderBy(l => l.LeadStatus);
                    else
                        query = query.OrderByDescending(l => l.LeadStatus);
                    break;
                case LeadsListSortFieldsEnum.Contacts:
                    if (ascending)
                        query = query.OrderBy(l => l.Contacts);
                    else
                        query = query.OrderByDescending(l => l.Contacts);
                    break;
                case LeadsListSortFieldsEnum.Email:
                    if (ascending)
                        query = query.OrderBy(l => l.Email);
                    else
                        query = query.OrderByDescending(l => l.Email);
                    break;
                case LeadsListSortFieldsEnum.CreatedDate:
                    if (ascending)
                        query = query.OrderBy(l => l.CreatedDate);
                    else
                        query = query.OrderByDescending(l => l.CreatedDate);
                    break;
                default:
                    if (ascending)
                        query = query.OrderBy(l => l.LeadID);
                    else
                        query = query.OrderByDescending(l => l.LeadID);
                    break;
            }
            #endregion

            #region Paging
            var recordsToSkip = (currentPage - 1) * pageSize;
            query = query.Skip(() => recordsToSkip)
                .Take(() => pageSize).WithTranslations();
            #endregion

            var leadsList = new LeadsListValueModel();
            leadsList.TotalRecordCount = TotalRecordCount;
            leadsList.Leads = await query.ToListAsync();

            EcrmEventSource.Log.MethodStop(this.GetType().FullName);

            return leadsList;
        }

        public async Task<IList<int>> ConvertLeadsToProspectBatch(IList<int> leadIDs)
        {
            EcrmEventSource.Log.MethodStart(this.GetType().FullName);

            Guid batchID = await StartBatch(leadIDs);

            var leadIDsToUpdate = await (from l in _context.Leads
                                         join b in _context.BatchProcess on l.LeadID equals b.RowID
                                         where b.BatchID == batchID && l.Status == (int)LeadStatusEnum.ForConversionToProspect
                                         select l.LeadID).ToListAsync();

            var query = new StringBuilder(@"UPDATE l set l.Status = @status, l.UpdatedDate = GETDATE()");
            query = query.Append(@" FROM Leads AS l INNER JOIN BatchProcesses b on l.LeadID = b.RowID");
            query = query.Append(@" WHERE b.BatchID = @batchID AND l.Status = @currStatus");
            var result = await _context.Database.ExecuteSqlCommandAsync(query.ToString()
                , new SqlParameter(@"status", (int)LeadStatusEnum.Prospect)
                , new SqlParameter(@"currStatus", (int)LeadStatusEnum.ForConversionToProspect)
                , new SqlParameter(@"batchID", batchID));

            await EndBatch(batchID);

            EcrmEventSource.Log.MethodStop(this.GetType().FullName);

            return leadIDsToUpdate;
        }

        public async Task<Lead> GetLeadInfo(int leadID)
        {
            EcrmEventSource.Log.MethodStart(this.GetType().FullName);
            var Lead = await _context.Leads.FindAsync(leadID);
            EcrmEventSource.Log.MethodStop(this.GetType().FullName);
            return Lead;
        }

        public OfferingsValueModel GetOfferingsForLead(int leadID, string projectName = null, string unitNo = null, OfferingProbabilityEnum? probability = null, string reserveFeeNo = null, LeadOfferingSortFieldEnum sortBy = LeadOfferingSortFieldEnum.ID, bool ascending = true, int currentPage = 1, int pageSize = 10)
        {
            EcrmEventSource.Log.MethodStart(this.GetType().FullName);

            var query = _context.Offerings.Where(o => o.LeadID == leadID).Include(o => o.Project).AsNoTracking();

            #region Filtering
            if (projectName != null)
                query = query.Where(o => o.Project.ProjectName.Contains(projectName));

            if (unitNo != null)
                query = query.Where(o => o.UnitNo.Contains(unitNo));

            if ((probability != 0)&&(probability != null))
                query = query.Where(o => o.Probability == (int)probability);

            if (reserveFeeNo != null)
                query = query.Where(o => o.ReserveFeeNo.Contains(reserveFeeNo));

            var recordCount = query.Count();
            #endregion

            #region Sorting
            switch (sortBy)
            {
                case LeadOfferingSortFieldEnum.ID:
                    if (ascending)
                        query = query.OrderBy(o => o.OfferingID);
                    else
                        query = query.OrderByDescending(o => o.OfferingID);

                    break;
                case LeadOfferingSortFieldEnum.Project:
                    if (ascending)
                        query = query.OrderBy(o => o.Project.ProjectName);
                    else
                        query = query.OrderByDescending(o => o.Project.ProjectName);

                    break;
                case LeadOfferingSortFieldEnum.UnitAmount:
                    if (ascending)
                        query = query.OrderBy(o => o.UnitAmount);
                    else
                        query = query.OrderByDescending(o => o.UnitAmount);

                    break;
                case LeadOfferingSortFieldEnum.Date:
                    if (ascending)
                        query = query.OrderBy(o => o.CreatedDate);
                    else
                        query = query.OrderByDescending(o => o.CreatedDate);

                    break;
                case LeadOfferingSortFieldEnum.Probability:
                    if (ascending)
                        query = query.OrderBy(o => o.Probability);
                    else
                        query = query.OrderByDescending(o => o.Probability);

                    break;
                default:
                    if (ascending)
                        query = query.OrderBy(o => o.OfferingID);
                    else
                        query = query.OrderByDescending(o => o.OfferingID);

                    break;
            }
            #endregion

            #region Paging
            var recordsToSkip = (currentPage - 1) * pageSize;
            query = query.Skip(() => recordsToSkip)
                .Take(() => pageSize);
            #endregion

            var offerings = new OfferingsValueModel();
            offerings.TotalRecordCount = recordCount;
            offerings.Offerings = query.ToList();

            EcrmEventSource.Log.MethodStop(this.GetType().FullName);

            return offerings;
        }


        public ActivitiesValueModel GetActivitiesForLead(int leadID, ActivityLeadTaskEnum? leadTaskID = null, string projectName = null, ActivityLeadTaskEnum? nextstep = null, ClientFeedbackEnum? cliendFeedback = null, LeadActivitySortFieldEnum sortBy = LeadActivitySortFieldEnum.ID, bool ascending = true, int currentPage = 1, int pageSize = 10)
        {
            EcrmEventSource.Log.MethodStart(this.GetType().FullName);

            var query = _context.Activities.Where(o => o.LeadID == leadID).Include(o => o.Project).AsNoTracking();

            #region Filtering
            if ((leadTaskID != 0) && (leadTaskID != null))
                query = query.Where(o => o.LeadTaskID == (int)leadTaskID);

            if (projectName != null)
                query = query.Where(o => o.Project.ProjectName.Contains(projectName));

            if ((nextstep != 0) && (nextstep != null))
                query = query.Where(o => o.NextStepID == (int)nextstep);

            if ((cliendFeedback != 0) && (cliendFeedback != null))
                query = query.Where(o => o.ClientFeedbackID == (int)cliendFeedback);

          

            var recordCount = query.Count();
            #endregion

            #region Sorting
            switch (sortBy)
            {
                case LeadActivitySortFieldEnum.ID:
                    if (ascending)
                        query = query.OrderBy(o => o.ActivityID);
                    else
                        query = query.OrderByDescending(o => o.ActivityID);

                    break;
                case LeadActivitySortFieldEnum.Activity:
                    if (ascending)
                        query = query.OrderBy(o => o.LeadTaskID);
                    else
                        query = query.OrderByDescending(o => o.LeadTaskID);

                    break;
                case LeadActivitySortFieldEnum.Project:
                    if (ascending)
                        query = query.OrderBy(o => o.Project.ProjectName);
                    else
                        query = query.OrderByDescending(o => o.Project.ProjectName);

                    break;
                case LeadActivitySortFieldEnum.ActivityDate:
                    if (ascending)
                        query = query.OrderBy(o => o.Activity_Date);
                    else
                        query = query.OrderByDescending(o => o.Activity_Date);

                    break;
                case LeadActivitySortFieldEnum.ClientFeedback:
                    if (ascending)
                        query = query.OrderBy(o => o.ClientFeedbackID);
                    else
                        query = query.OrderByDescending(o => o.ClientFeedbackID);
                    break;
                case LeadActivitySortFieldEnum.NextStep:
                    if (ascending)
                        query = query.OrderBy(o => o.NextStep);
                    else
                        query = query.OrderByDescending(o => o.NextStep);
                    break;
                default:
                    if (ascending)
                        query = query.OrderBy(o => o.ActivityID);
                    else
                        query = query.OrderByDescending(o => o.ActivityID);

                    break;
            }
            #endregion

            #region Paging
            var recordsToSkip = (currentPage - 1) * pageSize;
            query = query.Skip(() => recordsToSkip)
                .Take(() => pageSize);
            #endregion

            var activities = new ActivitiesValueModel();
            activities.TotalRecordCount = recordCount;
            activities.Activities = query.ToList();

            EcrmEventSource.Log.MethodStop(this.GetType().FullName);

            return activities;
        }
    }
}