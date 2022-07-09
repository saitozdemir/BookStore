using AutoMapper;
using WebApi.DbOperations;

namespace TestsSetup
{
    public class CommonTestFixture{
        public BookStoreDbContext Context { get; set; }
        public IMapper Mapper { get; set; }
    }
}