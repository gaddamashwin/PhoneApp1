using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.Text;
using System.Web;

namespace PhoneService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "MembershipService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select MembershipService.svc or MembershipService.svc.cs at the Solution Explorer and start debugging.
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
    public class MembershipService : IMembershipService
    {
        public Boolean IsAuthenticated()
        {
            if (HttpContext.Current.User.Identity.IsAuthenticated)
                return true;
            else
                return false;
        }
    }
}
