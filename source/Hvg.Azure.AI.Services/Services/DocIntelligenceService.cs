
namespace Hvg.Azure.AI.Services.Services
{
    public class DocIntelligenceService : IDocIntelligenceService
    {
        private readonly ILogger<DocIntelligenceService> _logger;
        private DocumentIntelligenceClient? client;
        public DocIntelligenceService(ILogger<DocIntelligenceService> logger)
        {
            _logger = logger;
            SetClient();
        }
        public async Task<string> IngestDocument(IFormFile formFile)
        {
            try
            {
                var source = BinaryData.FromBytes(GetBase64Source(formFile));
                AnalyzeDocumentContent content = new()
                {
                    Base64Source = source
                };

                Operation<AnalyzeResult> operation = await client.AnalyzeDocumentAsync(WaitUntil.Completed, "prebuilt-layout", content);

                AnalyzeResult result = operation.Value;
                
                //Extend the method to return page, table etc.
                if (result != null)
                {
                    return result.Content;
                }
                return string.Empty;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception in IngestDocument {Message}", ex.Message);
                throw;
            }
        }

        private void SetClient()
        {
            var key = Environment.GetEnvironmentVariable("DocIntelligenceKey");
            var endpoint = Environment.GetEnvironmentVariable("DocIntelligenceEndpoint");
            if (key != null && endpoint != null)
            {
                client = new DocumentIntelligenceClient(new Uri(endpoint), new AzureKeyCredential(key));
            }
        }

        private static byte[] GetBase64Source(IFormFile file)
        {
            var stream = file.OpenReadStream();
            byte[] bytes = [];
            using (var memoryStream = new MemoryStream())
            {
                stream.CopyTo(memoryStream);
                bytes = memoryStream.ToArray();
            }
            return bytes;
        }
    }
}
