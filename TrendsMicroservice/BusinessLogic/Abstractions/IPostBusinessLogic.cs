using Models;
using Models.Posts;
using System;
using System.Collections.Generic;

namespace BusinessLogic.Abstractions
{
    public interface IPostBusinessLogic
    {
        ICollection<PostGetAllDto> GetAll(Guid trendId, UserInfoModel userInfo);

        PostGetByIdDto GetById(Guid id, UserInfoModel userInfo);

        PostGetAllDto Create(PostCreateDto post);
        
        void Patch(PostPatchDto post);

        void PatchReact(PostPatchReactDto postPatchReact);

        void Delete(Guid id);
    }
}
