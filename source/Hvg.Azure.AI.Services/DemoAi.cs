

using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;

namespace Hvg.Azure.AI.Services
{
    public class DemoAi
    {
        private readonly ILogger<DemoAi> _logger;
        private readonly IDocIntelligenceService _docIntelligenceService;
        public DemoAi(ILogger<DemoAi> logger, IDocIntelligenceService docIntelligenceService)
        {
            _logger = logger;
            _docIntelligenceService = docIntelligenceService ?? throw new ArgumentNullException(nameof(docIntelligenceService));
        }

        [Function("DemoAi")]
        public IActionResult RunDemoAi([HttpTrigger(AuthorizationLevel.Anonymous, "get")] HttpRequest req)
        {
            try
            {
                _logger.LogInformation("Demo AI trigger function processed a request.");
                return new OkObjectResult("Welcome to Azure Functions DemoAi!");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception in RunDemoAi {Message}", ex.Message);
                throw;
            }
        }

        [Function("IngestDocument")]
        public async Task<IActionResult> RunIngestDocument([HttpTrigger(AuthorizationLevel.Anonymous, "post")] HttpRequest request)
        {
            try
            {
                _logger.LogInformation("Ingest Document request.");
                var formFile = request?.Form?.Files?.Count == 1 ? request.Form.Files[0] : throw new ArgumentException($"'{request?.Form?.Files?.Count}' file(s) found in form-data");

                if (!IsValidFileType(formFile.FileName.ToLower()))
                    return new BadRequestObjectResult("Invalid file type supplied - pdf, jpg, jpeg, bmp, png, docx, xlsx, html, pptx and tiff supported.");
                else
                {
                    var response = await _docIntelligenceService.IngestDocument(formFile);
                    return new OkObjectResult(response);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception in RunDemoAi {Message}", ex.Message);
                throw;
            }
        }

        private static bool IsValidFileType(string fileName)
        {
            return fileName.EndsWith(".pdf")
                || fileName.EndsWith(".jpg")
                || fileName.EndsWith(".jpeg")
                || fileName.EndsWith(".bmp")
                || fileName.EndsWith(".png")
                || fileName.EndsWith(".docx")
                || fileName.EndsWith(".xlsx")
                || fileName.EndsWith(".html")
                || fileName.EndsWith(".pptx")
                || fileName.EndsWith(".tiff");
        }
    }
}
