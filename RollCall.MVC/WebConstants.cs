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

        public const int TimeToCheckIn = 15;

    }
}
