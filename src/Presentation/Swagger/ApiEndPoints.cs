namespace Presentation.Swagger;

public static class ApiEndPoints
{
    private const string ApiBase = "/api";
    
    public static class System
    {
        private const string SystemBase = $"{ApiBase}/System";
        
        public static class V1
        {
            private const string SystemBaseV1 = $"{SystemBase}/V1";
            
            public static class OwnCompany
            {
                private const string OwnCompanyBaseV1 = $"{SystemBaseV1}/OwnCompany";

                public const string GetAll = OwnCompanyBaseV1;
                public const string Create = OwnCompanyBaseV1;
            }
        }
    }
}