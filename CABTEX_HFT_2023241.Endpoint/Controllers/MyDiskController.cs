using CABTEX_HFT_2023241.Endpoint.Services;
using CABTEX_HFT_2023241.Logic;
using CABTEX_HFT_2023241.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.Collections.Generic;

namespace CABTEX_HFT_2023241.Endpoint.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class MyDiskController : ControllerBase
    {
        IMyDiskLogic logic;
        IHubContext<SignalRHub> hub;

        public MyDiskController(IMyDiskLogic logic, IHubContext<SignalRHub> hub)
        {
            this.logic = logic;
            this.hub = hub;
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

        [HttpPost]
        public void Create([FromBody] MyDisk value)
        {
            logic.Create(value);
            hub.Clients.All.SendAsync("MyDiskCreated", value);
        }

        [HttpPut]
        public void Put([FromBody] MyDisk value)
        {
            logic.Update(value);
            hub.Clients.All.SendAsync("MyDiskUpdated", value);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var MyDiskToDelete = logic.Read(id);
            logic.Delete(id);
            hub.Clients.All.SendAsync("MyDiskDeleted", MyDiskToDelete);
        }
    }
}
