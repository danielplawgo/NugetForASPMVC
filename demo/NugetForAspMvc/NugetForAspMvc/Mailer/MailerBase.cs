using System.IO;
using System.Web.Hosting;
using System.Web.Mvc;
using Postal;

namespace NugetForAspMvc.Mailer
{
    public class MailerBase
    {
        protected void Send(Email email)
        {
            var viewsPath = Path.GetFullPath(HostingEnvironment.MapPath(@"~/Views/Emails"));
            var engines = new ViewEngineCollection();
            engines.Add(new FileSystemRazorViewEngine(viewsPath));

            var emailService = new EmailService(engines);

            emailService.Send(email);
        }
    }
}