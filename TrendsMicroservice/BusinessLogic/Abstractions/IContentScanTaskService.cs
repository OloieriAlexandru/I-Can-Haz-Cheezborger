using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Abstractions
{
    public interface IContentScanTaskService
    {
        public void CreateTask(string urlImageForScan, string textForScan, string callbackUrl);
    }
}
