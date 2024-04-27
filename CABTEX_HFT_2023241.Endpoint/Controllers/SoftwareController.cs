using CABTEX_HFT_2023241.Endpoint.Services;
using CABTEX_HFT_2023241.Logic;
using CABTEX_HFT_2023241.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.Collections.Generic;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CABTEX_HFT_2023241.Endpoint.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class SoftwareController : ControllerBase
    {
        ISoftwareLogic logic;
        IHubContext<SignalRHub> hub;

        public SoftwareController(ISoftwareLogic logic, IHubContext<SignalRHub> hub)
        {
            this.logic = logic;
            this.hub = hub;
        }

        [HttpGet]
        public IEnumerable<Software> ReadAll()
        {
            return logic.ReadAll();
        }

        [HttpGet("{nameOrId}")]
        public Software Read(string nameOrId)
        {
            if (int.TryParse(nameOrId, out int id)) return logic.Read(id);
            return logic.ReadByName(nameOrId);
        }

        [HttpPost]
        public void Create([FromBody] Software value)
        {
            logic.Create(value);
            hub.Clients.All.SendAsync("SoftwareCreated", value);
        }

        [HttpPut]
        public void Put([FromBody] Software value)
        {
            logic.Update(value);
            hub.Clients.All.SendAsync("SoftwareUpdated", value);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var SoftwareToDelete = logic.Read(id);
            logic.Delete(id);
            hub.Clients.All.SendAsync("SoftwareDeleted", SoftwareToDelete);
        }
    }
}
