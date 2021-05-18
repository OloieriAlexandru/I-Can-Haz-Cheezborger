using Models;
using Models.Common;
using Models.Trends;
using System;
using System.Collections.Generic;

namespace BusinessLogic.Abstractions
{
    public interface ITrendBusinessLogic
    {
        ICollection<TrendGetAllDto> GetAll(UserInfoModel userInfo);

        ICollection<TrendGetAllDto> GetPopular();

        TrendGetByIdDto GetById(Guid id);

        TrendGetAllDto Create(TrendCreateDto trend);

        void Update(TrendUpdateDto trend);

        void PatchFollow(TrendPatchFollowDto trendPatchFollowDto);

        void PatchContentScanTaskApprovals(Guid id, PatchContentScanTaskApprovalsDto taskApprovalsDto);

        void Delete(Guid id);
    }
}
