using System;
using System.Collections.Generic;
using System.Linq;
using Core.Common.Core;
using FluentValidation;

namespace Cti.Seller.Client.Entities
{
     public class Unit : ObjectBase
    {
        int _UnitId;
        string _Name;
        string _Description;
        int _LocationId;
        int _ProjectId;
        public int UnitId
        {
            get { return _UnitId; }
            set
            {
                if (_UnitId != value)
                {
                _UnitId = value;
                    OnPropertyChanged(() => UnitId);
                }
            }
        }
        public string Name
        {
        get { return _Name; }
        set
        {
            if (_Name != value)
            {
                _Name = value;
                OnPropertyChanged(() => Name);
            }
        }
    }
        public string Description
        {
            get { return _Description; }
            set
            {
                if (_Description != value)
                {
                    _Description = value;
                    OnPropertyChanged(() => Description);
                }
            }
        }

        public int LocationId
        {
            get { return _LocationId; }
            set
            {
                if (_LocationId != value)
                {
                    _LocationId = value;
                    OnPropertyChanged(() => LocationId);
                }
            }
        }


        public int ProjectId
        {
            get { return _ProjectId; }
            set
            {
                if (_ProjectId != value)
                {
                    _ProjectId = value;
                    OnPropertyChanged(() => ProjectId);
                }
            }
        }

        //TODO: Apply OnChange

        public int PhaseId { get; set; }
        public string Block { get; set; }
        public string InventoryUnit { get; set; }
        public string ProductType { get; set; }
        public string AllocationStatus { get; set; }
        public string UnitModel { get; set; }





        class UnitValidator : AbstractValidator<Unit>
        {
            public UnitValidator()
            {
                RuleFor(obj => obj.UnitId).NotEmpty();
                RuleFor(obj => obj.Description).NotEmpty();
            }
        }

        protected override IValidator GetValidator()
        {
            return new UnitValidator();
        }
    }

     public class ProjectParams : ObjectBase
    {
        public int LocationId { get; set; }
        public int ProjectId { get; set; }
        public int PhaseId { get; set; }
        public string Block { get; set; }
        public string InventoryUnit { get; set; }
        public string ProductType { get; set; }
        public string AllocationStatus { get; set; }
        public string UnitModel { get; set; }




    }


}
