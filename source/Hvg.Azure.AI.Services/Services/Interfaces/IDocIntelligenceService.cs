
using Microsoft.AspNetCore.Http;

namespace Hvg.Azure.AI.Services.Services.Interfaces
{
    public interface IDocIntelligenceService
    {
        public Task<string> IngestDocument(IFormFile formFile);
    }
}
