using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CABTEX_HFT_2023241.Logic;
using CABTEX_HFT_2023241.Models;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CABTEX_HFT_2023241.Endpoint.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class MyDiskController : ControllerBase
    {
        IMyDiskLogic logic;

        public MyDiskController(IMyDiskLogic logic)
        {
            this.logic = logic;
        }

        [HttpGet]
        public IEnumerable<MyDisk> ReadAll()
        {
            return logic.ReadAll();
        }

        [HttpGet("{id}")]
        public MyDisk Read(int id)
        {
            return logic.Read(id);
        }

        [HttpGet("{id}")]
        public int? FreeSpace(int id)
        {
            return logic.FreeSpaceOnTheDisk(id);
        }

        [HttpGet("{swName}")]
        public IEnumerable<MyDisk> ContainsaGivenSoftware(string swName)
        {
            return logic.ContainsaGivenSoftware(swName);
        }

        [HttpPost]
        public void Create([FromBody] MyDisk value)
        {
            logic.Create(value);
        }

        [HttpPut]
        public void Put([FromBody] MyDisk value)
        {
            logic.Update(value);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            logic.Delete(id);
        }
    }
}
