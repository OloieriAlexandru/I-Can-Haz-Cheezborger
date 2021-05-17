using BusinessLogic.Abstractions;
using Common.Constraints;
using Google.Cloud.Tasks.V2;
using Google.Protobuf;
using System;
using System.Collections.Generic;

namespace BusinessLogic.Implementations
{
    public class GCloudContentScanTaskService : IContentScanTaskService
    {
        async public void CreateTask(string urlImageForScan, string textForScan, string callbackUrl)
        {
            string payload = ConstructPayload(urlImageForScan, textForScan, callbackUrl);

            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", ContentScanConstraints.keyPath);
            CloudTasksClient client = CloudTasksClient.Create();
            QueueName parent = new QueueName(ContentScanConstraints.projectId, ContentScanConstraints.location, ContentScanConstraints.queue);

            var response = client.CreateTask(new CreateTaskRequest
            {
                Parent = parent.ToString(),
                Task = new Task
                {
                    HttpRequest = new HttpRequest
                    {
                        HttpMethod = HttpMethod.Post,
                        Url = ContentScanConstraints.url,
                        Body = ByteString.CopyFromUtf8(payload)

                    }
                }
            });
            Console.WriteLine($"Created Task {response.Name}");
        }

        private string ConstructPayload(string urlImageForScan, string textForScan, string callbackUrl)
        {
            string payload = "";

            if (urlImageForScan != null && urlImageForScan != "")
                payload += urlImageForScan;
            payload += ",";

            if (textForScan != null && textForScan != "")
                payload += textForScan;
            payload += ",";

            if (callbackUrl != null && callbackUrl != "")
                payload += callbackUrl;

            return payload;
        }
    }
}
