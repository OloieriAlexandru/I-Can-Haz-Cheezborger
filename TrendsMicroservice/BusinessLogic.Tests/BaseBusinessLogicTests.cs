using AutoMapper;
using BusinessLogic.Profiles;

namespace BusinessLogic.Tests
{
    public abstract class BaseBusinessLogicTests
    {
        protected IMapper mapper;

        protected BaseBusinessLogicTests()
        {
            MapperConfiguration configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new TrendProfile());
                cfg.AddProfile(new PostProfile());
                cfg.AddProfile(new CommentProfile());
            });

            mapper = new Mapper(configuration);
        }
    }
}
