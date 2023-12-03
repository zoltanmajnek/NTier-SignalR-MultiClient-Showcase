using CABTEX_HFT_2023241.Logic;
using CABTEX_HFT_2023241.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CABTEX_HFT_2023241.Endpoint.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class ALotOfNonCrudLogicController : ControllerBase
    {
        IFriendLogic friendLogic;
        IMyDiskLogic diskLogic;
        IDiskDirectoryLogic directoryLogic;
        IDiskTypeLogic diskTypelogic;
        ISoftwareLogic softwareLogic;

        public ALotOfNonCrudLogicController(IFriendLogic friendLogic, IMyDiskLogic diskLogic, IDiskDirectoryLogic directoryLogic, IDiskTypeLogic diskTypelogic, ISoftwareLogic softwareLogic)
        {
            this.friendLogic = friendLogic;
            this.diskLogic = diskLogic;
            this.directoryLogic = directoryLogic;
            this.diskTypelogic = diskTypelogic;
            this.softwareLogic = softwareLogic;
        }

        [HttpGet("{address}")]
        public IEnumerable<string> WhoLiveInMyStreet(string address) => friendLogic.WhoLiveInMyStreet(address);

        [HttpGet]
        public string LargestDiskVendorName() => diskTypelogic.LargestDiskVendorName();

        [HttpGet]
        public int? DiskIdWithMaxSoftware() => directoryLogic.DiskIdWithMaxSoftware();

        [HttpGet("{id}")]
        public int? FreeSpace(int id) => diskLogic.FreeSpaceOnTheDisk(id);

        [HttpGet("{swName}")]
        public IEnumerable<MyDisk> ContainsaGivenSoftware(string swName)
        {
            return diskLogic.ContainsaGivenSoftware(swName);
        }

        [HttpGet("{genre}")]
        public IEnumerable<string> Genre(string genre) => softwareLogic.Genre(genre);

        [HttpGet("{genre}")]
        public double? AvgSizeInaGenre(string genre) => softwareLogic.AvgSizeInaGenre(genre);

        [HttpGet]
        public Friend WhoHasTheMostDisks()
        {
            return friendLogic.WhoHasTheMostDisks();
        }

        [HttpGet("{swName}")]
        public IEnumerable<Friend> WhoHasThisSoftware(string swName)
        {
            return friendLogic.WhoHasThisSoftware(swName);
        }

    }
}
