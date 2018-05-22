using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Task_InfinityMesh.Models
{
    public class Blogs
    {
        public int Id { get; set; }
        public string Title { get; set; }//64 velicina
        public string Summary { get; set; } //350 velicina
        public string Content { get; set; } //3500 velicina
        public DateTime PublishDate { get; set; }
        public int UserId { get; set; }
        public Users User { get; set; }

    }
}