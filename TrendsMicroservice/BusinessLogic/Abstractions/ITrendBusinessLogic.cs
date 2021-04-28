using Models.Trends;
using System;
using System.Collections.Generic;

namespace BusinessLogic.Abstractions
{
    public interface ITrendBusinessLogic
    {
        ICollection<TrendGetAllDto> GetAll();

        TrendGetByIdDto GetById(Guid id);

        TrendGetAllDto Create(TrendCreateDto trend, string username);

        void Update(TrendUpdateDto trend);

        void Delete(Guid id);
    }
}
