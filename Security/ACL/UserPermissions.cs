namespace Maksab.Security.ACL
{
    public static class UserPermissions
    {
        public const string Permissions = "permissions";

        public static class Business
        {
            public const int CanView = 100;
            public const int CanAdd = 101;
            public const int CanUpdate = 102;
            public const int CanDelete = 103;
        }

        public static class Reports
        {
            public const int CanView = 200;
        }

        public static class Profile
        {
            public const int CanView = 300;
            public const int CanUpdate = 302;
        }

        public static class UserManagement
        {
            public const int CanView = 400;
            public const int CanAdd = 401;
            public const int CanUpdate = 402;
            public const int CanDelete = 403;
        }

        public static class Requests
        {
            public const int CanView = 500;
            public const int CanUpdate = 502;
        }
        public static class Developers
        {
            public const int CanView = 600;
            public const int CanAdd = 601;
            public const int CanUpdate = 602;
            public const int CanDelete = 603;
        }

        public static class Members
        {
            public const int CanView = 2500;
            public const int CanAdd = 2501;
            public const int CanUpdate = 2502;
            public const int CanDelete = 2503;
        }

        public static class Sama
        {
            public const int CanView = 2700;
            public const int CanAdd = 2701;
            public const int CanUpdate = 2702;
            public const int CanDelete = 2703;
        }

    }
}