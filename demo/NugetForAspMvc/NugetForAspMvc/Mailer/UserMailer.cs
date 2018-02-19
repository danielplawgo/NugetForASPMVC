using NugetForAspMvc.Models;
using NugetForAspMvc.ViewModels.Users;

namespace NugetForAspMvc.Mailer
{
    public class UserMailer : MailerBase, IUserMailer
    {
        public void Create(User user)
        {
            var email = new CreateEmail()
            {
                Email = user.Email
            };

            Send(email);
        }
    }
}