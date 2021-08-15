using System;
using System.Collections.Generic;
using System.Text;
using BusinessRulesEngine.Interfaces;
using BusinessRulesEngine.Models;

namespace BusinessRulesEngine.BusinessRules
{
    public class AddFreeFirstAidVideoBusinessRule: IBusinessRule
    {
        public PackagingSlip PackagingSlip;

        public AddFreeFirstAidVideoBusinessRule(string slipDestination)
        {
            PackagingSlip = new PackagingSlip(slipDestination);
        }

        public void ExecuteBusinessRule(Product product)
        {
            var video = new Video() {Title = "First Aid", IsPhysical = true};
            PackagingSlip.ProductsToPack.Add(video);
        }
    }
}
