using AutoMapper;
using ecrm.Domain.Model;
using ecrm.Models.LeadsViewModels;

namespace ecrm.Infrastructure.AutoMapper
{
    public class AutoMapperConfiguration
    {
        public static MapperConfiguration MapperConfiguration;

        public static void RegisterMappings()
        {
            MapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Lead, LeadsListItemViewModel>().ReverseMap();
                cfg.CreateMap<Lead, LeadInfoViewModel>().ReverseMap();
                cfg.CreateMap<Seller, SellerViewModel>().ReverseMap();
                cfg.CreateMap<Offering, LeadOfferingItemViewModel>().ReverseMap();
                cfg.CreateMap<Activity, LeadActivityItemViewModel>().ReverseMap();
            });
        }
    }
}