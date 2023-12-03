using CABTEX_HFT_2023241.Logic;
using CABTEX_HFT_2023241.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CABTEX_HFT_2023241.Endpoint.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class FriendController : ControllerBase
    {
        IFriendLogic logic;

        public FriendController(IFriendLogic logic)
        {
            this.logic = logic;
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
        }

        [HttpPut]
        public void Put([FromBody] Friend value)
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
