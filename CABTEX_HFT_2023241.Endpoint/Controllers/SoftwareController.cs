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
    [Route("[controller]")]
    [ApiController]
    public class SoftwareController : ControllerBase
    {
        ISoftwareLogic logic;

        public SoftwareController(ISoftwareLogic logic)
        {
            this.logic = logic;
        }

        [HttpGet]
        public IEnumerable<Software> ReadAll()
        {
            return this.logic.ReadAll();
        }

        [HttpGet("{id}")]
        public Software Read(int id)
        {
            return this.logic.Read(id);
        }

        [HttpPost]
        public void Create([FromBody] Software value)
        {
            this.logic.Create(value);
        }

        [HttpPut]
        public void Put([FromBody] Software value)
        {
            this.logic.Update(value);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            this.logic.Delete(id);
        }
    }
}
