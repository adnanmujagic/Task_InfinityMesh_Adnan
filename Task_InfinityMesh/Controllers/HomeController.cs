using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Task_InfinityMesh.EF;
using Task_InfinityMesh.Helper;
using Task_InfinityMesh.Models;
using Task_InfinityMesh.ViewModels;

namespace Task_InfinityMesh.Controllers
{
    public class HomeController : Controller
    {
        MojContext ctx = new MojContext();
        // GET: Home
        public ActionResult Index()
        {
            string User = Autentifikacija.GetLogiraniKorisnik(HttpContext);
            if (User == "admin")
            {
                return RedirectToAction("PrikazUsera");
            }
            return View();
        }

        public ActionResult SignIn(string username, string password, bool? zapamtiMe)
        {
            if (username == "admin" && password == "admin")
            {
                Autentifikacija.PokreniNovuSesiju(username, HttpContext, (zapamtiMe == true));
                return RedirectToAction("PrikazUsera");
            }
            return RedirectToAction("Index");
        }

        public ActionResult PrikazUsera(UserListVM Model, int? page)
        {
            if (Model.ItemsPerPage == 0)
            {
                Model = new UserListVM();
                Model.ItemsPerPage = 5;
            }

            Model.ItemsPerPageList = new List<SelectListItem>()
            {
                new SelectListItem { Value = "5", Text = "5" },
                new SelectListItem { Value = "7", Text = "7" },
                new SelectListItem { Value = "10", Text = "10" },
                new SelectListItem { Value = "15", Text = "15" },
            };

            Model.allUsers = ctx.Users
                .Where(x => Model.searchParam == "" || Model.searchParam == null ? true : (x.FirstName.Contains(Model.searchParam) || x.SecondName.Contains(Model.searchParam) || x.Email.Contains(Model.searchParam) || (x.FirstName + " " + x.SecondName).Contains(Model.searchParam)))
                .Select(x => new UserListVM.UserInfo
                {
                    Id = x.Id,
                    Name = x.FirstName + " " + x.SecondName,
                    Age = x.Age,
                    Email = x.Email,
                    Blogs = ctx.Blogs.Count(y => y.UserId == x.Id)
                }).OrderBy(x=>x.Name).ToList();

            Model.allUsersPaged = new PagedList.PagedList<UserListVM.UserInfo>(Model.allUsers, page ?? 1, Model.ItemsPerPage);
            return View("PrikazUsera",Model);
        }


        public ActionResult UserDetails(string searchParam,DateTime? dateFrom, DateTime? dateTo, int _userId, int? page)
        {
            Users user = ctx.Users.Include(x => x.Blogs).Where(x => x.Id == _userId).FirstOrDefault();

            UserDetailsVM Model = new UserDetailsVM()
            {
                _userId = user.Id,
                Age = user.Age,
                FirstName = user.FirstName,
                SecondName = user.SecondName,
                FullName = user.FirstName + " " + user.SecondName
            };

            Model.blogsList = new List<UserDetailsVM.UserBlogs>();
            foreach (Blogs x in user.Blogs.Where(x=> (dateFrom == null ? true : dateFrom<=x.PublishDate) && (dateTo == null?true: dateTo>=x.PublishDate) && (searchParam == null ? true : x.Title.Contains(searchParam) || x.Summary.Contains(searchParam))).OrderBy(x=>x.PublishDate)) //ili OrderByDescending
            {
                Model.blogsList.Add(new UserDetailsVM.UserBlogs
                {
                    Id = x.Id,
                    PublishDate = x.PublishDate,
                    Summary = x.Summary,
                    Title = x.Title,
                    PublishDateConverted = x.PublishDate.ToString("HH:mm, dd MMM yyyy")
                });
            }
            Model.searchParam = searchParam;
            Model.dateFrom = dateFrom;
            Model.dateTo = dateTo;
            Model.blogsListPaged = new PagedList.PagedList<UserDetailsVM.UserBlogs>(Model.blogsList, page ?? 1, 3);

            return View(Model);
        }

        public ActionResult BlogManager(int _BlogId) // for editing blogs
        {
            Blogs blg = ctx.Blogs.Where(x => x.Id == _BlogId).FirstOrDefault();
            BlogVM Model = new BlogVM()
            {
                Id = blg.Id,
                UserId = blg.UserId,
                Title = blg.Title,
                Summary = blg.Summary,
                Content = blg.Content,
                PublishDate = blg.PublishDate
            };
            return View("BlogManager",Model);
        }

        [ValidateInput(false)]
        public ActionResult ValidateBlog(int? _userId, BlogVM Model)
        {
            if (_userId != null)
                Model.UserId = (int)_userId;

            if (!ModelState.IsValid || Model.PublishDate<DateTime.Now)
            {
                if (Model.PublishDate < DateTime.Now)
                {
                    ModelState.AddModelError("PublishDate", "Date cannot be set in past!");
                }
                return View("BlogManager",Model);
            }

            return RedirectToAction("SpremiBlog", Model);
        }

        public ActionResult SpremiBlog(BlogVM Model)
        {
            Blogs blogs;
            if(Model.Id == 0)
            {
                blogs = new Blogs();
                ctx.Blogs.Add(blogs);
            }
            else
            {
                blogs = ctx.Blogs.Where(x => x.Id == Model.Id).FirstOrDefault();
            }
            blogs.Title = Model.Title;
            blogs.Summary = Model.Summary;
            blogs.Content = Model.Content;
            blogs.PublishDate = Model.PublishDate;
            blogs.UserId = Model.UserId;
            ctx.Blogs.Add(blogs);
            ctx.SaveChanges();

            return RedirectToAction("UserDetails",new { _userId = Model.UserId });
        }

        public ActionResult Logout()
        {
            Autentifikacija.PokreniNovuSesiju(null, HttpContext, true);
            return RedirectToAction("Index");
        }

    }
}