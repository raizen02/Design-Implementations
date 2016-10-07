using ecrm.Infrastructure.Logging;
using ecrm.Models.LeadsViewModels;
using ecrm.Service;
using ecrm.Service.Messages;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Linq;
using Newtonsoft.Json;
using ecrm.Infrastructure.Enum;

namespace ecrm.Controllers
{
    public class LeadsController : Controller
    {
        private ILeadsService _leadsService;
        private int sellerID = 643; // TODO: Remove hard-coded Seller ID
        public LeadsController() : this(new LeadsService())
        { }

        public LeadsController(ILeadsService leadService)
        {
            _leadsService = leadService;
        }

        #region LEADS LIST - MAIN VIEW
        public async Task<ActionResult> Index()
        {
            EcrmEventSource.Log.MethodStart(this.GetType().FullName);
            LeadsRequest leadsRequest = new LeadsRequest();

            //TODO: HARDCODED REQUEST
            SellerViewModel seller = new SellerViewModel();
            seller.SellerID = sellerID;
            leadsRequest.Seller = seller;
            LeadsListViewModel leadsList = new LeadsListViewModel();
            leadsList.CurrentPageIndex = 1;
            leadsRequest.LeadsList = leadsList;
            //END HARDCODED REQUEST

            LeadsResponse leadsResponse = await _leadsService.GetDownlines(leadsRequest);

            LeadsListViewModel leadsViewModel = new LeadsListViewModel();
            leadsViewModel = leadsResponse.LeadsList;

            EcrmEventSource.Log.MethodStop(this.GetType().FullName);
            return View("ListLeads", leadsViewModel);
        }
        #endregion

        #region LEADS LIST - PARTIAL VIEW
        public async Task<ActionResult> PageView(LeadsListViewModel leadsList)
        {
            EcrmEventSource.Log.MethodStart(this.GetType().FullName);
            LeadsRequest leadsRequest = new LeadsRequest();

            SellerViewModel seller = new SellerViewModel();
            seller.SellerID = sellerID; //TODO: Remove hard coded request
            leadsRequest.Seller = seller;
            leadsRequest.LeadsList = leadsList;

            LeadsResponse leadsResponse = await _leadsService.GetLeads(leadsRequest);

            LeadsListViewModel leadsViewModel = new LeadsListViewModel();

            //RETURNED THE FILTERING PARAMETER
            leadsResponse.LeadsList.LeadStatus = leadsList.LeadStatus;
            leadsResponse.LeadsList.DownlineSellerID = leadsList.DownlineSellerID;
            leadsResponse.LeadsList.PartialLeadName = leadsList.PartialLeadName;
            leadsResponse.LeadsList.SortAscending = leadsList.SortAscending;
            leadsResponse.LeadsList.SortBy = leadsList.SortBy;
            leadsResponse.LeadsList.CurrentPageIndex = leadsList.CurrentPageIndex;

            leadsViewModel = leadsResponse.LeadsList;

            EcrmEventSource.Log.MethodStop(this.GetType().FullName);
            return PartialView("ListLeadsItemsPartial", leadsViewModel);
        }
        #endregion

        #region ADD LEADS - SAVE 
        [ActionName("SaveLeadInfo")]
        [HttpPost]
        [ValidateAntiForgeryToken]       
        public async Task<ActionResult> SaveLeadInfo(LeadInfoViewModel input)
        {
            EcrmEventSource.Log.MethodStart(this.GetType().FullName);
            LeadInfoViewModel addLeadViewModel = new LeadInfoViewModel();
            string viewName = null;

            if (ModelState.IsValid)
            {
                LeadsRequest saveLeadRequest = new LeadsRequest();
                saveLeadRequest.LeadInfo = input;
                SellerViewModel seller = new SellerViewModel();
                seller.SellerID = sellerID; //TODO: Remove hard coded request
                saveLeadRequest.Seller = seller;
                //TODO: IF LEADID IS NOT NULL, CALL EDIT EVENT
                LeadsResponse saveLeadsResponse = await _leadsService.AddLeadToSeller(saveLeadRequest);
                addLeadViewModel = saveLeadsResponse.LeadsInfo;
                ViewBag.Message = Resources.AddLeadSuccess;
                viewName = @"EditLead";
            }
            else
            {
                EcrmEventSource.Log.Error(this.GetType().FullName, @"Invalid input: " + JsonConvert.SerializeObject(input));
                addLeadViewModel = input;
                ViewBag.Error = Resources.AddLeadError;
                viewName = @"AddLead";
            }

            EcrmEventSource.Log.MethodStop(this.GetType().FullName);
            return View(viewName, addLeadViewModel);
        }
        #endregion

        #region CONVERT LEADS TO PROSPECTS - UPDATE 
        [ActionName("ConvertToProspect")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ConvertToProspect(FormCollection form, LeadsListViewModel model)
        {
            EcrmEventSource.Log.MethodStart(this.GetType().FullName);
            LeadsListViewModel leadsListViewModel = new LeadsListViewModel();

            if (ModelState.IsValid)
            {
                LeadsRequest convertToProspectRequest = new LeadsRequest();
                convertToProspectRequest.LeadsList = new LeadsListViewModel();
                convertToProspectRequest.Seller = new SellerViewModel();


                //RETURNED THE MODEL FOR FILTERING PARAMETER
                convertToProspectRequest.LeadsList = model;

                //SET SELLER ID            
                convertToProspectRequest.Seller.SellerID = sellerID;

                //LeadID For Conversion
                var idsToConvertToProspect = form["lead_bulk_action"] != null
                    ? form["lead_bulk_action"].Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(int.Parse)
                    .ToList()
                    : new List<int>();
                convertToProspectRequest.LeadsList.LeadIDs = idsToConvertToProspect;

                //GET RESPONSE 
                LeadsResponse convertToProspectResponse = new LeadsResponse();
                convertToProspectResponse = await _leadsService.ConvertLeadsToProspect(convertToProspectRequest);

                //RETURNED THE FILTERING PARAMETER
                convertToProspectResponse.LeadsList.LeadStatus = model.LeadStatus;
                convertToProspectResponse.LeadsList.DownlineSellerID = model.DownlineSellerID;
                convertToProspectResponse.LeadsList.PartialLeadName = model.PartialLeadName;
                convertToProspectResponse.LeadsList.SortAscending = model.SortAscending;
                convertToProspectResponse.LeadsList.SortBy = model.SortBy;
                convertToProspectResponse.LeadsList.CurrentPageIndex = model.CurrentPageIndex;

                leadsListViewModel = convertToProspectResponse.LeadsList;
                ViewBag.Message = Resources.ConversionToProspectSuccess;
            }
            else
            {
                ViewBag.Message = Resources.ConversionToProspectError;
            }

            EcrmEventSource.Log.MethodStop(this.GetType().FullName);
            // todo: redirect to index or edit view
            return PartialView("ListLeadsItemsPartial", leadsListViewModel);
        }
        #endregion

        #region ADD LEADS - MAIN VIEW
        [ActionName("AddLead")]
        public ActionResult AddLead()
        {
            EcrmEventSource.Log.MethodStart(this.GetType().FullName);

            LeadInfoViewModel addLeadViewModel = new LeadInfoViewModel();

            EcrmEventSource.Log.MethodStop(this.GetType().FullName);
            return View("AddLead", addLeadViewModel);
        }
        #endregion

        #region EDIT LEADS - MAIN VIEW
        [ActionName("EditLead")]
        public async Task<ActionResult> EditLead(int leadID)
        {
            EcrmEventSource.Log.MethodStart(this.GetType().FullName);
            LeadsRequest leadInfoRequest = new LeadsRequest();
            leadInfoRequest.LeadInfo = new LeadInfoViewModel();
            leadInfoRequest.LeadInfo.LeadID = leadID;
            LeadsResponse leadInfoResponse = await _leadsService.GetLeadInfo(leadInfoRequest);

            LeadInfoViewModel addLeadViewModel = new LeadInfoViewModel();
            addLeadViewModel = leadInfoResponse.LeadsInfo;

            EcrmEventSource.Log.MethodStop(this.GetType().FullName);
            return View("EditLead", addLeadViewModel);
        }
        #endregion

        #region TAB LEADS - PARTIAL VIEW 
        public async Task<ActionResult> LeadInfoView(int leadID)
        {
            EcrmEventSource.Log.MethodStart(this.GetType().FullName);
            LeadsRequest leadInfoRequest = new LeadsRequest();

            leadInfoRequest.LeadInfo = new LeadInfoViewModel();
            leadInfoRequest.LeadInfo.LeadID = leadID;
            LeadsResponse leadInfoResponse = await _leadsService.GetLeadInfo(leadInfoRequest);

            LeadInfoViewModel addLeadViewModel = new LeadInfoViewModel();
            addLeadViewModel = leadInfoResponse.LeadsInfo;
            EcrmEventSource.Log.MethodStop(this.GetType().FullName);
            return PartialView("LeadInfoPartial", addLeadViewModel);
        }
        #endregion
         
        #region TAB LEADOFFERING - PARTIAL VIEW 
        public ActionResult LeadOfferingsView(LeadOfferingViewModel leadOfferingModel)
        {
            EcrmEventSource.Log.MethodStart(this.GetType().FullName);
            LeadsRequest leadOfferingRequest = new LeadsRequest();

            leadOfferingRequest.LeadOfferingList = new LeadOfferingViewModel();
            leadOfferingRequest.LeadOfferingList = leadOfferingModel;
           
            LeadsResponse leadInfoResponse = _leadsService.GetOfferingsForLead(leadOfferingRequest);

            //RETURNED THE FILTERING PARAMETER         
            leadInfoResponse.LeadOfferingList.SearchFilter = leadOfferingModel.SearchFilter;
            leadInfoResponse.LeadOfferingList.PartialProjectName = leadOfferingModel.PartialProjectName;
            leadInfoResponse.LeadOfferingList.PartialUnitNo = leadOfferingModel.PartialUnitNo;
            leadInfoResponse.LeadOfferingList.Probability = leadOfferingModel.Probability;
            leadInfoResponse.LeadOfferingList.ReserveFeeNo = leadOfferingModel.ReserveFeeNo;
            leadInfoResponse.LeadOfferingList.SortAscending = leadOfferingModel.SortAscending;
            leadInfoResponse.LeadOfferingList.SortBy = leadOfferingModel.SortBy;
            leadInfoResponse.LeadOfferingList.CurrentPageIndex = leadOfferingModel.CurrentPageIndex;

            LeadOfferingViewModel addLeadViewModel = new LeadOfferingViewModel();

            addLeadViewModel = leadInfoResponse.LeadOfferingList;
            EcrmEventSource.Log.MethodStop(this.GetType().FullName);
            return PartialView("LeadOfferingsPartial", addLeadViewModel);
        }
        #endregion

        #region TAB LEADACTIVITY - PARTIAL VIEW 
        public  ActionResult LeadActivityView(LeadActivityViewModel leadActivityModel)
        {
            EcrmEventSource.Log.MethodStart(this.GetType().FullName);
            LeadsRequest leadActivityRequest = new LeadsRequest();

            leadActivityRequest.LeadActivityList = new LeadActivityViewModel();
            leadActivityRequest.LeadActivityList = leadActivityModel;
      
            LeadsResponse leaActivityResponse = _leadsService.GetActivitiesForLead(leadActivityRequest);
         
            ////RETURNED THE FILTERING PARAMETER         
            leaActivityResponse.LeadActivityList.SearchFilter = leadActivityModel.SearchFilter;
            leaActivityResponse.LeadActivityList.LeadTask = leadActivityModel.LeadTask;
            leaActivityResponse.LeadActivityList.PartialProject = leadActivityModel.PartialProject;
            leaActivityResponse.LeadActivityList.NextStep = leadActivityModel.NextStep;
            leaActivityResponse.LeadActivityList.ClientFeedback = leadActivityModel.ClientFeedback;
            leaActivityResponse.LeadActivityList.SortAscending = leadActivityModel.SortAscending;
            leaActivityResponse.LeadActivityList.SortBy = leadActivityModel.SortBy;
            leaActivityResponse.LeadActivityList.CurrentPageIndex = leadActivityModel.CurrentPageIndex;

            LeadActivityViewModel addActivityViewModel = new LeadActivityViewModel();

            addActivityViewModel = leaActivityResponse.LeadActivityList;
            EcrmEventSource.Log.MethodStop(this.GetType().FullName);
            return PartialView("LeadActivityPartial", addActivityViewModel);
        }
        #endregion

        #region ADD OFFERING - PARTIAL VIEW
        [ActionName("AddOffering")]
        public ActionResult AddOffering(int leadID)
        {
            EcrmEventSource.Log.MethodStart(this.GetType().FullName);

            LeadOfferingItemViewModel addOfferingItemViewModel = new LeadOfferingItemViewModel();

            //PASS LEAD ID TO THE ADD OFFERING FORM
            addOfferingItemViewModel.LeadID = leadID;
            EcrmEventSource.Log.MethodStop(this.GetType().FullName);
            return PartialView("AddOfferingPartial", addOfferingItemViewModel);
        }
        #endregion

        #region EDIT OFFERING - PARTIAL VIEW
        [ActionName("EditOffering")]
        public async Task<ActionResult> EditOffering(int leadID,int offeringID)
        {
            EcrmEventSource.Log.MethodStart(this.GetType().FullName);

            LeadOfferingItemViewModel addOfferingItemViewModel = new LeadOfferingItemViewModel();

            //PASS LEAD ID TO THE ADD OFFERING FORM
            addOfferingItemViewModel.LeadID = leadID;
            addOfferingItemViewModel.OfferingID = offeringID;
            EcrmEventSource.Log.MethodStop(this.GetType().FullName);
            return PartialView("EditOfferingPartial", addOfferingItemViewModel);
        }
        #endregion

        #region ADD OFFERING - SAVE 
        [ActionName("SaveOffering")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SaveOffering(LeadOfferingItemViewModel input)
        {
            EcrmEventSource.Log.MethodStart(this.GetType().FullName);
            LeadsRequest leadOfferingRequest = new LeadsRequest();

            leadOfferingRequest.LeadOfferingList = new LeadOfferingViewModel();
            
            //FETCH UNIT OFFERED LIST 
            leadOfferingRequest.LeadOfferingList.LeadID = input.LeadID;
            LeadsResponse leadInfoResponse = _leadsService.GetOfferingsForLead(leadOfferingRequest);
                       
            if (ModelState.IsValid)
            {
                //EXECUTE SAVE UNIT OFFERED HERE
                //RETURN OFFERING ID HERE AND LEAD ID HERE                
                ViewBag.Message = Resources.SaveSuccess;

                //TAG TO EXECUTE EDIT FORM
                ViewBag.Edit = true;
            }
            else
            {
                ViewBag.Message = Resources.SaveError;                
            }
            
            LeadOfferingViewModel addLeadViewModel = new LeadOfferingViewModel();
            addLeadViewModel = leadInfoResponse.LeadOfferingList;            
            EcrmEventSource.Log.MethodStop(this.GetType().FullName);
            return PartialView("LeadOfferingsPartial", addLeadViewModel);
        }
        #endregion

        #region EDIT OFFERING - SAVE 
        [ActionName("UpdateOffering")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> UpdateOffering(LeadOfferingItemViewModel input)
        {

            EcrmEventSource.Log.MethodStart(this.GetType().FullName);
            LeadsRequest leadOfferingRequest = new LeadsRequest();

            leadOfferingRequest.LeadOfferingList = new LeadOfferingViewModel();

            //FETCH UNIT OFFERED LIST 
            leadOfferingRequest.LeadOfferingList.LeadID = input.LeadID;
            LeadsResponse leadInfoResponse = _leadsService.GetOfferingsForLead(leadOfferingRequest);

            if (ModelState.IsValid)
            {
                //EXECUTE SAVE UNIT OFFERED HERE
                //RETURN OFFERING ID HERE AND LEAD ID HERE                
                ViewBag.Message = Resources.SaveSuccess;

                //TAG TO EXECUTE EDIT FORM
                ViewBag.Edit = true;
            }
            else
            {
                ViewBag.Message = Resources.SaveError;
            }

            LeadOfferingViewModel addLeadViewModel = new LeadOfferingViewModel();
            addLeadViewModel = leadInfoResponse.LeadOfferingList;
            EcrmEventSource.Log.MethodStop(this.GetType().FullName);
            return PartialView("LeadOfferingsPartial", addLeadViewModel);
        }
        #endregion

        #region ADD ACTIVITY - PARTIAL VIEW
        [ActionName("AddActivity")]
        public ActionResult AddActivity()
        {
            EcrmEventSource.Log.MethodStart(this.GetType().FullName);

            LeadActivityItemViewModel addActivityViewModel = new LeadActivityItemViewModel();

            EcrmEventSource.Log.MethodStop(this.GetType().FullName);
            return PartialView("AddActivityPartial", addActivityViewModel);
        }
        #endregion

        #region EDIT ACTIVITY - PARTIAL VIEW
        [ActionName("EditActivity")]
        public async Task<ActionResult> EditActivity(int leadID, int activityID)
        {
            EcrmEventSource.Log.MethodStart(this.GetType().FullName);

            LeadActivityItemViewModel editActivityViewModel = new LeadActivityItemViewModel();

            editActivityViewModel.LeadID = leadID;
            editActivityViewModel.ActivityID = activityID;
            EcrmEventSource.Log.MethodStop(this.GetType().FullName);
            return PartialView("EditActivityPartial", editActivityViewModel);
        }
        #endregion

        #region ADD ACTIVITY - SAVE 
        [ActionName("SaveActivity")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SaveActivity(LeadActivityItemViewModel input)
        {

            EcrmEventSource.Log.MethodStart(this.GetType().FullName);
            LeadsRequest leadActivityRequest = new LeadsRequest();

            leadActivityRequest.LeadActivityInfo = new LeadActivityItemViewModel();
            leadActivityRequest.LeadActivityInfo.LeadID = input.LeadID;
            LeadsResponse leadInfoResponse =  _leadsService.GetActivitiesForLead(leadActivityRequest);

            //TODO: PASS THE CURRENT FORM STATE
            //TODO: REFRESH THE LIST
            LeadActivityItemViewModel addActivityViewModel = new LeadActivityItemViewModel();

            if (ModelState.IsValid)
            {
                ViewBag.Message = Resources.SaveSuccess;
            }
            else
            {
                ViewBag.Message = Resources.SaveError;
            }


            addActivityViewModel = leadInfoResponse.LeadActivityInfo;
            EcrmEventSource.Log.MethodStop(this.GetType().FullName);
            return PartialView("LeadActivityPartial", addActivityViewModel);
        }
        #endregion

        #region EDIT ACTIVITY - SAVE 
        [ActionName("UpdateActivity")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> UpdateActivity(LeadActivityItemViewModel input)
        {

            EcrmEventSource.Log.MethodStart(this.GetType().FullName);
            LeadsRequest leadActivityRequest = new LeadsRequest();

            leadActivityRequest.LeadActivityInfo = new LeadActivityItemViewModel();
            leadActivityRequest.LeadActivityInfo.LeadID = input.LeadID;
            LeadsResponse leadInfoResponse =  _leadsService.GetActivitiesForLead(leadActivityRequest);

            //TODO: PASS THE CURRENT FORM STATE
            //TODO: REFRESH THE LIST
            LeadActivityItemViewModel addActivityViewModel = new LeadActivityItemViewModel();

            if (ModelState.IsValid)
            {
                ViewBag.Message = Resources.SaveSuccess;
            }
            else
            {
                ViewBag.Message = Resources.SaveError;
            }


            addActivityViewModel = leadInfoResponse.LeadActivityInfo;
            EcrmEventSource.Log.MethodStop(this.GetType().FullName);
            return PartialView("LeadActivityPartial", addActivityViewModel);
        }
        #endregion


   
    }




}
