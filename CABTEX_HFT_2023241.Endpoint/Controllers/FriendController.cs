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
    public class FriendController : ControllerBase
    {
        IFriendLogic logic;
        IHubContext<SignalRHub> hub;

        public FriendController(IFriendLogic logic, IHubContext<SignalRHub> hub)
        {
            this.logic = logic;
            this.hub = hub;
        }

        [HttpGet]
        public IEnumerable<Friend> ReadAll()
        {
            return logic.ReadAll();
        }

        [HttpGet("{id}")]
        public Friend Read(int id)
        {
            return logic.Read(id);
        }

        [HttpPost]
        public void Create([FromBody] Friend value)
        {
            logic.Create(value);
            hub.Clients.All.SendAsync("FriendCreated", value);
        }

        [HttpPut]
        public void Put([FromBody] Friend value)
        {
            logic.Update(value);
            hub.Clients.All.SendAsync("FriendUpdated", value);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var FriendToDelete = logic.Read(id);
            logic.Delete(id);
            hub.Clients.All.SendAsync("FriendDeleted", FriendToDelete);
        }
    }
}
