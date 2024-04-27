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
    public class DiskTypeController : ControllerBase
    {
        IDiskTypeLogic logic;
        IHubContext<SignalRHub> hub;

        public DiskTypeController(IDiskTypeLogic logic, IHubContext<SignalRHub> hub)
        {
            this.logic = logic;
            this.hub = hub;
        }

        [HttpGet]
        public IEnumerable<DiskType> ReadAll()
        {
            return this.logic.ReadAll();
        }

        [HttpGet("{id}")]
        public DiskType Read(int id)
        {
            return this.logic.Read(id);
        }

        [HttpPost]
        public void Create([FromBody] DiskType value)
        {
            this.logic.Create(value);
            hub.Clients.All.SendAsync("DiskTypeCreated", value);
        }

        [HttpPut]
        public void Put([FromBody] DiskType value)
        {
            this.logic.Update(value);
            hub.Clients.All.SendAsync("DiskTypeUpdated", value);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var DiskTypeToDelete = logic.Read(id);
            logic.Delete(id);
            hub.Clients.All.SendAsync("DiskTypeDeleted", DiskTypeToDelete);
        }
    }
}
