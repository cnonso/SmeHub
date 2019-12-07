using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SME_Hub.Models
{
    public class _MultiModels
    {
        public MyAccount MyAccount { get; set; }
        public Message Message { get; set; }
        public IEnumerable<MyAccount> MyAccounts { get; set; }
        public IEnumerable<Message> Messages { get; set; }
        public LoginViewModel LoginViewModel { get; set; }
        public KeyWordModel KeyWordModel { get; set; }
        
    }
}