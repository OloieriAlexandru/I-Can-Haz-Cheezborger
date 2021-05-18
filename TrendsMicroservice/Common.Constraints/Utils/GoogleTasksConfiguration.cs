namespace Common.Utils
{
    public class GoogleTasksConfiguration
    {
        public string ProjectId { get; set; }

        public string Location { get; set; }

        public string QueueName { get; set; }

        public string GoogleFunctionUrl { get; set; }

        public string KeyPath { get; set; }

        public string KeyEnvironmentVariableName { get; set; }
    }
}
