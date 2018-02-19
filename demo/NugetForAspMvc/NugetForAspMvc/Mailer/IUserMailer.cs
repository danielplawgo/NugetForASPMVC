using NugetForAspMvc.Models;
using NugetForAspMvc.ViewModels.Users;

namespace NugetForAspMvc.Mailer
{
    public interface IUserMailer
    {
        void Create(User email);
    }
}
