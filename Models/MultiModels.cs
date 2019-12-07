using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SME_Hub.Models
{
    public class MultiModels
    {
        public MyAccount MyAccount { get; set; }
        public MyAccountViewModel MyAccountViewModel { get; set; }
        public Message Message { get; set; }
        public IEnumerable<MyAccount> MyAccounts { get; set; }
        public IEnumerable<Message> Messages { get; set; }
        public IEnumerable<MyAccount> RelatedArtisans { get; set; }
        public CommentsViewModel CommentsViewModel { get; set; }
        public IEnumerable<Comment> CommentsForService { get; set; }
    }
}