using EBSS.Automated.UI.Tests.Utils;

namespace EBSS.Automated.UI.Tests.TestData
{
    public static class NewRegUser
    {
        public static string Username => "NewRegTestUser";
        public static string Password => ConfigurationSetUp.TestPassword;

        public static string TestApplicantUsername => "TestApplicant";
    }

    public class AdminUser
    {
        public static string Username => ConfigurationSetUp.AdminEmail;
        public static string Password => ConfigurationSetUp.AdminPassword;
    }

    public class TestUser
    {
        public static string Username => ConfigurationSetUp.TestEmail;
        public static string Password => ConfigurationSetUp.TestPassword;
        public static string IncorrectPassword => "TestTestTest";
    }

}
