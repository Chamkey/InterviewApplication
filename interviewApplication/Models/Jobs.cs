using System.Collections.Generic;

namespace interviewApplication.Models
{
    public class getJsonData
    {
        public List<Jobs> jobs { get; set; }
    }
    
    
    public class Jobs
    {

        public string client { get; set; }

        public string jobNumber { get; set; }

        public string jobName { get; set; }
       
        public string due { get; set; }

        public string status { get; set; }
    }


}