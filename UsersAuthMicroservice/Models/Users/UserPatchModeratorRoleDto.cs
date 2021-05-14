using System;

namespace Models.Users
{
    public class UserPatchModeratorRoleDto : UserInfoModel
    {
        public Guid TrendId { get; set; }
    }
}
