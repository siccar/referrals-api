using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OpenReferrals.DataModels;
using OpenReferrals.Repositories.OpenReferral;
using OpenReferrals.Sevices;
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
        public async Task<IActionResult> Get()
        {
            var userId = JWTAttributesService.GetSubject(Request);
            var playList = await _playListRepo.FindById(userId);
            return Ok(playList);
        }

        [HttpPost]
        public IActionResult AddPlaylist(Playlist playlist)
        {
            var userId = JWTAttributesService.GetSubject(Request);
            playlist.Id = userId;
            _playListRepo.UpdateOne(playlist);
            return Ok(playlist);
        }
    }
}