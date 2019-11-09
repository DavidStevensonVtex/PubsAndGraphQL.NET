using System.Security.Claims;

namespace Pubs.Infrastructure
{
    public class GraphQLUserContext
    {
        public ClaimsPrincipal User { get; set; }
    }
}
