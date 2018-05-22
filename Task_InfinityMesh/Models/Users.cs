using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Task_InfinityMesh.Models
{
    public class Users
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public int Age { get; set; }
        public string Email { get; set; }


        public List<Blogs> Blogs { get; set; }
    }
}