using Microsoft.AspNetCore.Mvc;
//using Microsoft.Extensions.Logging;
using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

namespace counter_service.Controllers
{

    [ApiController]
    [Route("[controller]")]
    
    public class thecounter : ControllerBase
    {
        private void add() 
        { 
            int num = int.Parse((System.IO.File.ReadAllText("counter.txt")));
            num++;
            string line = num.ToString();
            System.IO.File.WriteAllText("counter.txt",line);
        }
        private string show()
        {
            return (System.IO.File.ReadAllText("counter.txt"));

        }

        [HttpGet]
        public string Get()
        {

           return show();
        }
        [HttpPost]
        public string Post () {


            add();
            return "a post request register seccssufuly ";
        
        
        }
   
    }
}
