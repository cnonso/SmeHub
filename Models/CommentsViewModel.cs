using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SME_Hub.Models
{
    public class CommentsViewModel
    {
        public string Name { get; set; }
        public string Content { get; set; }
        public HttpPostedFileBase Icon { get; set; }
    }
}