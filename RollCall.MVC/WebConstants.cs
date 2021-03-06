using Microsoft.IdentityModel.Protocols;
using System.Configuration;

namespace RollCall.MVC
{
    public class WebConstants
    {
        public static class ErrorMessages
        {
            public const string RequiredField = @"{0} is required";
        }
        public static class Roles
        {
            public const string AdminRole = "Admin";
            public const string StudentRole = "Student";
            public const string TeacherRole = "Teacher";
        }


        public const string TempDataSuccessMessageKey = "SuccessMessage";
        public const string TempDataErrorMessageKey = "ErrorMessage";

        // The window for a student to check in (in minutes)
        public const int TimeToCheckIn = 15;

        // Room code generator
        public const int RoomCodeMin = 1000;
        public const int RoomCodeMax = 9999;

        //Subject Sets
        public const string PastSubjects = "Past Courses";
        public const string OngoingSubjects = "Ongoing Courses";
        public const string UpcomingSubjects = "Upcoming Courses";

        //SchoolClasse Sets
        public const string PastClasses = "Past Classes";
        public const string OngoingClasses = "Ongoing Classes";
        public const string UpcomingClasses = "Upcoming Classes";

    }
}
