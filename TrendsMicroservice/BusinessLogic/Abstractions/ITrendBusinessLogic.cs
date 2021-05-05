using Models.Trends;
using System;
using System.Collections.Generic;

namespace BusinessLogic.Abstractions
{
    public interface ITrendBusinessLogic
    {
        ICollection<TrendGetAllDto> GetAll();

        ICollection<TrendGetAllDto> GetPopular();

        TrendGetByIdDto GetById(Guid id);

        TrendGetAllDto Create(TrendCreateDto trend);

        void Update(TrendUpdateDto trend);

        void PatchFollow(TrendPatchFollowDto trendPatchFollowDto);

        void Delete(Guid id);
    }
}
