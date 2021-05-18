using System;

namespace Models.Common
{
    public class PatchContentScanTaskApprovalsDto
    {
        public Guid ObjectId { get; set; }

        public bool ApprovedImage { get; set; }

        public bool ApprovedText { get; set; }
    }
}
