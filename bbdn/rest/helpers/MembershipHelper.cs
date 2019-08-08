using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REST_Demo_CSharp.bbdn.rest.helpers
{
    class MembershipHelper
    {
        public static Membership GenerateObject()
        {
            Membership membership = new Membership();
            Availability availability = new Availability
            {
                available = "Yes"
            };

            membership.availability = availability;
            membership.courseRoleId = "Instructor";

            return (membership);
        }
    }
}
