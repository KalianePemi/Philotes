using Microsoft.Extensions.Options;
using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Philotes.Models;
namespace AspCore_Email.Services
{
    public class EmailSettings
    {
        public String PrimaryDomain { get; set; }
        public int PrimaryPort { get; set; }
        public String UsernameEmail { get; set; }
        public String UsernamePassword { get; set; }
        public String FromEmail { get; set; }
        public String ToEmail { get; set; }
        public String CcEmail { get; set; }
        public String Subject { get; set; }
        public String Message { get; set; }
         
    }

}