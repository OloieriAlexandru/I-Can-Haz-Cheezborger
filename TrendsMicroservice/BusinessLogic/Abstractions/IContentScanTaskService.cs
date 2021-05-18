using Models.Models;

namespace BusinessLogic.Abstractions
{
    public interface IContentScanTaskService
    {
        void CreateTask(CreateContentScanTaskDto scanTaskDto);
    }
}
