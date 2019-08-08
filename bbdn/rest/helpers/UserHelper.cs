using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REST_Demo_CSharp.bbdn.rest.helpers
{
    class UserHelper
    {
        public static User GenerateObject()
        {
            User user = new User();
            Availability availability = new Availability();
            Name name = new Name();
            Contact contact = new Contact();

            availability.available = "Yes";

            name.given = Constants.USER_FIRST;
            name.family = Constants.USER_LAST;

            contact.email = Constants.USER_EMAIL;

            user.externalId = Constants.USER_ID;
            user.userName = Constants.USER_NAME;
            user.password = Constants.USER_PASS;
            user.availability = availability;
            user.name = name;
            user.contact = contact;
            
            return (user);
        }
    }
}
