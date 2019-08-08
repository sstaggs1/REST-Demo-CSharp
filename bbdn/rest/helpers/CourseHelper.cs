using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REST_Demo_CSharp.bbdn.rest.helpers
{
    class CourseHelper
    {
        public static Course GenerateObject()
        {
            Course course = new Course
            {
                externalId = Constants.COURSE_ID,
                courseId = Constants.COURSE_ID,
                name = Constants.COURSE_NAME,
                description = Constants.COURSE_DESCRIPTION,
                allowGuests = true,
                readOnly = false,
                termId = Constants.TERM_ID
            };

            return (course);
        }
    }
}
