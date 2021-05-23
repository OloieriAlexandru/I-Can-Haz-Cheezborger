using BusinessLogic.Abstractions;
using Common.Utils;
using Google.Cloud.Tasks.V2;
using Google.Protobuf;
using Microsoft.Extensions.Options;
using Models.Models;
using System.Text.Json;

namespace BusinessLogic.Implementations
{
    public class GCloudContentScanTaskService : IContentScanTaskService
    {
        private readonly GoogleTasksConfiguration googleTasksConfiguration;

        private readonly CloudTasksClient cloudTasksClient;

        private readonly ServerConfiguration serverConfiguration;

        public GCloudContentScanTaskService(IOptionsMonitor<GoogleTasksConfiguration> optionsMonitor, CloudTasksClient cloudTasksClient,
            IOptionsMonitor<ServerConfiguration> serverConfigurationOptionsMonitor)
        {
            this.googleTasksConfiguration = optionsMonitor.CurrentValue;
            this.cloudTasksClient = cloudTasksClient;
            this.serverConfiguration = serverConfigurationOptionsMonitor.CurrentValue;
        }

        async public void CreateTask(CreateContentScanTaskDto scanTaskDto)
        {
            if (!serverConfiguration.DoSendContentScanningRequest)
            {
                return;
            }

            QueueName parent = new QueueName(googleTasksConfiguration.ProjectId, googleTasksConfiguration.Location, googleTasksConfiguration.QueueName);
            string payload = JsonSerializer.Serialize(scanTaskDto);

            await cloudTasksClient.CreateTaskAsync(new CreateTaskRequest
            {
                Parent = parent.ToString(),
                Task = new Task
                {
                    HttpRequest = new HttpRequest
                    {
                        HttpMethod = HttpMethod.Post,
                        Url = googleTasksConfiguration.GoogleFunctionUrl,
                        Body = ByteString.CopyFromUtf8(payload)
                    }
                }
            });
        }
    }
}
