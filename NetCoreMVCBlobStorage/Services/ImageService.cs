using Azure.Storage;
using Azure.Storage.Blobs;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using NetCoreMVCBlobStorage.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreMVCBlobStorage.Services
{
    public class ImageService
    {
        CloudBlobClient blobClient;

        public async Task<Uri> UploadFileToStorage(Stream fileStream, string fileName, StorageConfig storageConfig)
        {
            // Create a URI to the blob
            Uri blobUri = new Uri("https://" +
                                  storageConfig.AccountName +
                                  ".blob.core.windows.net/" +
                                  storageConfig.ImageContainer +
                                  "/" + fileName);

            // Create StorageSharedKeyCredentials object by reading
            // the values from the configuration (appsettings.json)
            StorageSharedKeyCredential storageCredentials =
                new StorageSharedKeyCredential(storageConfig.AccountName, storageConfig.AccountKey);

            // Create the blob client.
            BlobClient blobClient = new BlobClient(blobUri, storageCredentials);

            // Upload the file
            await blobClient.UploadAsync(fileStream);

            return blobUri;
        }

        public string RetriveImages(StorageConfig storageConfig, Uri imageUri)
        {
            Uri blobUri = new Uri("https://" +
                                  storageConfig.AccountName +
                                  ".blob.core.windows.net/" +
                                  storageConfig.ImageContainer);

            StorageSharedKeyCredential storageCredentials =
                new StorageSharedKeyCredential(storageConfig.AccountName, storageConfig.AccountKey);

            // Create the blob client.
            BlobClient blobClient = new BlobClient(blobUri, storageCredentials);

            var sas = new SharedAccessBlobPolicy
            {
                Permissions = SharedAccessBlobPermissions.Read,
                SharedAccessStartTime = DateTime.UtcNow.AddMinutes(-15),
                SharedAccessExpiryTime = DateTime.UtcNow.AddMinutes(15),
            };

            var continer = blobClient.GenerateSasUri(Azure.Storage.Sas.BlobSasPermissions.Read, DateTime.UtcNow.AddMinutes(20));
            

            return "";
        }

    }
}
