using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Task_InfinityMesh.ViewModels
{
    public class UserListVM
    {
        public class UserInfo
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public int Age{ get; set; }
            public string Email { get; set; }
            public int Blogs { get; set; }

        }

        public List<UserInfo> allUsers { get; set; }
        public PagedList<UserInfo> allUsersPaged { get; set; }
        public string searchParam { get; set; }
        public int ItemsPerPage { get; set; }
        public List<SelectListItem> ItemsPerPageList { get; set; }
    }
}