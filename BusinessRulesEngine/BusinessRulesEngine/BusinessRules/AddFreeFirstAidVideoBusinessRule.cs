using System;
using System.Collections.Generic;
using System.Text;
using BusinessRulesEngine.Interfaces;
using BusinessRulesEngine.Models;

namespace BusinessRulesEngine.BusinessRules
{
    public class AddFreeFirstAidVideoBusinessRule: IBusinessRule
    {

        public string SlipDestination { get; set; }

        public AddFreeFirstAidVideoBusinessRule(string slipDestination)
        {
            SlipDestination = slipDestination;
        }

        public void ExecuteBusinessRule(Order order)
        {
            var video = new Video() {Title = "First Aid", IsPhysical = true};
            var packagingSlip = new PackagingSlip(SlipDestination);
            packagingSlip.ProductsToPack.Add(video);
            order.PackagingSlips.Add(packagingSlip);
            
        }
    }
}
