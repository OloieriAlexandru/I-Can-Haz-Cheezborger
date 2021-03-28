using Models;
using System;
using System.Collections.Generic;

namespace BusinessLogic.Abstractions
{
    public interface ITrendBusinessLogic
    {
        ICollection<TrendDto> GetAll();

        TrendDto GetById(Guid id);

        void Create(TrendDto trend);

        void Update(TrendDto trend);
    }
}
