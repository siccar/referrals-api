using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OpenReferrals.DataModels;
using OpenReferrals.RegisterManagementConnector.Models;
using OpenReferrals.RegisterManagementConnector.ServiceClients;
using OpenReferrals.Repositories.OpenReferral;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OpenReferrals.Controllers
{
    [ApiController]
    [Authorize]
    [Route("[controller]")]
    public class PlaylistController : ControllerBase
    {
        private readonly IPlaylistRepository _playListRepo;

        public PlaylistController(IPlaylistRepository playlistRepo)
        {
            _playListRepo = playlistRepo;
        }

        [HttpGet]
<<<<<<< Updated upstream
        [Route("{userId")]
=======
        [Route("{userId}")]
>>>>>>> Stashed changes
        public IActionResult Get(string userId)
        {
            var playList = _playListRepo.FindById(userId);
            return Ok(playList);
        }

        [HttpPost]
        public IActionResult AddPlaylist(Playlist list)
        {
            _playListRepo.InsertOrUpdateOne(list);
            return Ok(list);
        }
    }
}