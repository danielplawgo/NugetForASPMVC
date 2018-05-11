using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Autofac;
using AutoMapper;
using Hangfire;
using NugetForAspMvc.Mailer;
using NugetForAspMvc.Models;
using NugetForAspMvc.ViewModels.Users;
using StackExchange.Profiling;

namespace NugetForAspMvc.Controllers
{
    public partial class UsersController : Controller
    {
        private DataContext db = new DataContext();

        protected IMapper Mapper
        {
            get { return MvcApplication.Container.Resolve<IMapper>(); }
        }

        protected IUserMailer UserMailer
        {
            get { return MvcApplication.Container.Resolve<IUserMailer>(); }
        }

        // GET: Users
        public virtual ActionResult Index()
        {
            return View(db.Users.ToList());
        }

        public virtual ActionResult Index2()
        {
            using (MiniProfiler.Current.Step("Index2 action"))
            {
                IEnumerable<UserViewModel> viewModels;
                var models = db.Users.ToList();

                using (MiniProfiler.Current.Step("Mapping with automapper"))
                {
                    viewModels = Mapper.Map<List<UserViewModel>>(models);
                }

                return View(viewModels);
            }
        }

        public virtual ActionResult Index3()
        {
            using (MiniProfiler.Current.Step("Index3 action"))
            {
                IEnumerable<UserViewModel> viewModels;
                var models = db.Users.ToList();

                using (MiniProfiler.Current.Step("Mapping without automapper"))
                {
                    viewModels = models.Select(u => new UserViewModel()
                    {
                        Id = u.Id,
                        Email = u.Email,
                        FirstName = u.FirstName,
                        LastName = u.LastName,
                        CreateInvoice = u.CreateInvoice,
                        UserName = u.UserName,
                        Nip = u.Nip
                    }).ToList();
                }

                return View(Views.Index2, viewModels);
            }
        }

        // GET: Users/Details/5
        public virtual ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // GET: Users/Create
        public virtual ActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Create(UserViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var user = Mapper.Map<User>(viewModel);

                db.Users.Add(user);
                db.SaveChanges();

                //UserMailer.Create(user);

                #region TODO
                BackgroundJob.Enqueue(() => UserMailer.Create(user));
                #endregion

                return RedirectToAction(ActionNames.Index);
            }

            return View(viewModel);
        }

        // GET: Users/Edit/5
        public virtual ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Edit([Bind(Include = "Id,FirstName,LastName,UserName,Email,CreateInvoice,Nip")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction(ActionNames.Index);
            }
            return View(user);
        }

        // GET: Users/Delete/5
        public virtual ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public virtual ActionResult DeleteConfirmed(int id)
        {
            User user = db.Users.Find(id);
            db.Users.Remove(user);
            db.SaveChanges();
            return RedirectToAction(ActionNames.Index);
        }

        public virtual ActionResult Confirm()
        {
            return View();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
