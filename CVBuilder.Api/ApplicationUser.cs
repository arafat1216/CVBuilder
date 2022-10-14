using CVBuilder.Application.Contracts.Authentication;

namespace CVBuilder.Api
{
    public class ApplicationUser : IApplicationUser
    {
        private readonly IHttpContextAccessor contextAccessor;

        public ApplicationUser(IHttpContextAccessor contextAccessor)
        {
            this.contextAccessor = contextAccessor;
        }
        public Guid GetUserId()
        {
            return Guid.Parse(contextAccessor.HttpContext.User.Identity.Name);
        }
    }
}
