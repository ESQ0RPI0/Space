using Space.Server.Database.Context;

namespace Space.Server.Services.SpaceFund
{
    public class SpaceFundService
    {
        private readonly SpaceFundContext _dc;

        public SpaceFundService(SpaceFundContext spaceFundContext)
        {
            _dc = spaceFundContext;
        }
    }
}
