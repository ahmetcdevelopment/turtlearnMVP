using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace turtlearnMVP.Persistance.Configurations
{
    public class SmtpSettings
    {
        public string SmtpServer { get; set; }
        public int SmtpPort { get; set; }
        public string SmtpSenderName { get; set; }
        public string SmtpSenderEmail { get; set; }
        public string SmtpPassword { get; set; }
    }
}
