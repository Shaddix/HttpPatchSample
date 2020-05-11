namespace HttpPatchSample.Swagger
{
    /// <summary>
    /// Defines Swagger section of appsettings.json configuration
    /// </summary>
    public class SwaggerOptions
    {
        public class LicenseOptions
        {
            public string Name { get; set; }
        }

        public class EndpointOptions
        {
            public string UiUrl { get; set; }
            public string Url { get; set; }
            public string Name { get; set; }
        }

        public class ContactOptions
        {
            public string Email { get; set; }
        }

        public bool Enabled { get; set; }

        public string Description { get; set; }
        public string Title { get; set; }
        public string Version { get; set; }
        public string BasePath { get; set; }

        public ContactOptions Contact { get; set; }
        public EndpointOptions Endpoint { get; set; }
        public LicenseOptions License { get; set; }
    }
}