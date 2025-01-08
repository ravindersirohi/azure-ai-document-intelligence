# azure-ai-document-intelligence
Function app integrated with Azure AI Document Intelligence service to read legar documents.

## About

Azure function app (http trigger) to post documents (pdf, jpg, jpeg, bmp, png, docx, xlsx, html, pptx and tiff) on Azure AI Document Intelegence server which reads the documents and returns the document content as response.

## Setup Document Intelligence Service in Azrue.

Setup [Document Intelligence resource](https://learn.microsoft.com/en-us/azure/ai-services/document-intelligence/how-to-guides/create-document-intelligence-resource) manually via the Azure Portal, and polpulated the Local setting files with "Key" and "endpoint", for the PoC you can use Free tier.

Function app local setting file.

```
{
  "IsEncrypted": false,
  "Values": {
    "AzureWebJobsStorage": "",
    "FUNCTIONS_WORKER_RUNTIME": "dotnet-isolated",
    "DocIntelligenceKey": "{key from portal}",
    "DocIntelligenceEndpoint": "https://{ your document intelligence from portal}.cognitiveservices.azure.com/"
  }
}

```


## Setup Azure AI Document Intelligence via Terraform

To setup [Document Intelligence](https://registry.terraform.io/modules/Azure/avm-res-cognitiveservices-account/azurerm/0.5.0/examples/Azure-AI-Document-Intelligence) via Terraform.

## Additional documentation
Please find below additional contents.

1. [.NET SDK - REST API](https://docs.azure.cn/en-us/ai-services/document-intelligence/versioning/sdk-overview-v4-0?view=doc-intel-4.0.0&viewFallbackFrom=doc-intel-3.1.0&tabs=csharp)
2. [Studio experience for Document Intelligence](https://learn.microsoft.com/en-us/azure/ai-services/document-intelligence/studio-overview?view=doc-intel-4.0.0&tabs=di-studio)
3. [Document Models via REST](https://learn.microsoft.com/en-us/rest/api/aiservices/document-models/analyze-document?view=rest-aiservices-v4.0%20(2024-11-30)&viewFallbackFrom=rest-aiservices-2023-07-31&preserve-view=true&tabs=HTTP)

