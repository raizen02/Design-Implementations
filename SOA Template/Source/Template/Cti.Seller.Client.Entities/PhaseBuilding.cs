using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Common.Core;
using FluentValidation;
namespace Cti.Seller.Client.Entities
{
    public class PhaseBuilding : ObjectBase
    {
        int _Id;
        string _Name;
        public int Id
        {
            get { return _Id; }
            set
            {
                if (_Id != value)
                {
                    _Id = value;
                    OnPropertyChanged(() => Id);
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
        public List<FloorBlock> FloorBlocks { get; set; }


        //public List<Unit> GetUnits(String CurrentPhase, String CurrentFloor)
        //{
        //    //List<Unit> results = PhaseBuildings.Where(P => P.Name == CurrentPhase)
        //    //                                   .SelectMany(P => P.FloorBlocks)
        //    //                                   .Where(F => F.Name == CurrentFloor)
        //    //                                   .SelectMany(F => F.Units).ToList();


        //    //return results;
        //}
    }
}
