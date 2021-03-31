namespace TrendsMicroservice.IntegrationTests.IntegrationTestsUtil
{
    public static class ApiRoutes
    {
        public const string Root = "api";

        public const string Version = "v1";

        public const string Base = Root + "/" + Version;

        public static class Trends
        {
            public const string GetAll = Base + "/trends";

            public const string GetById = Base + "/trends/{trendId}";

            public const string Create = Base + "/trends";

            public const string Update = Base + "/trends/{trendId}";
        }
    }
}
