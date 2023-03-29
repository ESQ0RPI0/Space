using Space.Server.Database.Context;

namespace Space.Server.Services.NewSpace
{
    public class NewSpaceService
    {
        private readonly NewSpaceContext _dc;
        public NewSpaceService(NewSpaceContext dc)
        {
            _dc = dc;
        }
    }
}
