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
    public class DiskDirectoryController : ControllerBase
    {
        IDiskDirectoryLogic logic;
        IHubContext<SignalRHub> hub;

        public DiskDirectoryController(IDiskDirectoryLogic logic, IHubContext<SignalRHub> hub)
        {
            this.logic = logic;
            this.hub = hub;
        }

        [HttpGet]
        public IEnumerable<DiskDirectory> ReadAll()
        {
            return this.logic.ReadAll();
        }

        [HttpGet("{id}")]
        public DiskDirectory Read(int id)
        {
            return this.logic.Read(id);
        }

        [HttpPost]
        public void Create([FromBody] DiskDirectory value)
        {
            this.logic.Create(value);
            hub.Clients.All.SendAsync("DiskDirectoryCreated", value);
        }

        [HttpPut]
        public void Put([FromBody] DiskDirectory value)
        {
            this.logic.Update(value);
            hub.Clients.All.SendAsync("DiskDirectoryUpdated", value);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var DiskDirectoryToDelete = logic.Read(id);
            logic.Delete(id);
            hub.Clients.All.SendAsync("DiskDirectoryDeleted", DiskDirectoryToDelete);
        }
    }
}
