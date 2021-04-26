using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace RetailService
{
    public class ControllerFeatureProvider : Microsoft.AspNetCore.Mvc.Controllers.ControllerFeatureProvider
    {
        private readonly IConfiguration _configuration;

        public ControllerFeatureProvider(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override bool IsController(TypeInfo typeInfo)
        {
            var isController = base.IsController(typeInfo);

            

            if (isController)
            {
                var enabledController = _configuration.GetValue<string[]>("EnabledController");

                isController = enabledController.Any(x => typeInfo.Name.Equals(x, StringComparison.InvariantCultureIgnoreCase));
            }

            return isController;
        }
    }
}
