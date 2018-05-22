using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Task_InfinityMesh.ViewModels
{
    public class UserDetailsVM
    {
        public int _userId { get; set; }
        public string FullName { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public int Age { get; set; }
        public List<UserBlogs> blogsList { get; set; }
        public PagedList<UserBlogs> blogsListPaged { get; set; }
        public string searchParam { get; set; }
        public DateTime? dateFrom { get; set; }
        public DateTime? dateTo { get; set; }


        public class UserBlogs
        {
            public int Id { get; set; }
            public string Title { get; set; }
            public string Summary { get; set; }
            public string PublishDateConverted { get; set; }
            public DateTime PublishDate { get; set; }
        }
    }
}