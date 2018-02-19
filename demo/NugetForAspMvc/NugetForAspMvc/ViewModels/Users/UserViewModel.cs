using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NugetForAspMvc.ViewModels.Users
{
    public class UserViewModel
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }

        public bool CreateInvoice { get; set; }

        public string Nip { get; set; }
    }
}