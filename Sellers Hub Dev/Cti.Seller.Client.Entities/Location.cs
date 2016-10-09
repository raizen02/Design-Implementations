using System;
using System.Collections.Generic;
using System.Linq;
using Core.Common.Core;
using FluentValidation;

namespace Cti.Seller.Client.Entities
{
     public class Location : ObjectBase
    {
        int _Code;
        string _Barangay;
        string _CityMunicipality;
        string _Region;
 

        public int Code
        {
            get { return _Code; }
            set
            {
                if (_Code != value)
                {
                    _Code = value;
                    OnPropertyChanged(() => Code);
                }
            }
        }
         public string Barangay
        {
            get { return _Barangay; }
            set
            {
                if (_Barangay != value)
                {
                    _Barangay = value;
                    OnPropertyChanged(() => Barangay);
                }
            }
        }
        public string CityMunicipality {
            get { return _CityMunicipality; }
            set
            {
                if (_CityMunicipality != value)
                {
                    _CityMunicipality = value;
                    OnPropertyChanged(() => CityMunicipality);
                }
            }
        }
        public string Region
        {
            get { return _Region; }
            set
            {
                if (_Region != value)
                {
                    _Region = value;
                    OnPropertyChanged(() => Region);
                }
            }
        }

        class LocationValidator : AbstractValidator<Location>
        {
            public LocationValidator()
            {
                RuleFor(obj => obj.Code).NotEmpty();
            }
        }

        protected override IValidator GetValidator()
        {
            return new LocationValidator();
        }
    }



    }



