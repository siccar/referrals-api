using MongoDB.Driver;
using OpenReferrals.DataModels;
using OpenReferrals.Repositories.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OpenReferrals.Repositories.OpenReferral
{
    public class PlaylistRepository : IPlaylistRepository
    {
        private IMongoRepository<Playlist> _repo;

        public PlaylistRepository(IMongoRepository<Playlist> repo)
        {
            _repo = repo;
        }

        public async Task<Playlist> FindById(string userId)
        {
            var playlist = await _repo.FindOneAsync(x => x.Id == userId);

            if (playlist == null)
            {
                playlist = new Playlist { Id = userId, Services = new List<string>() };
                await _repo.InsertOneAsync(playlist);
            }

            return playlist;
        }

        public async Task UpdateOne(Playlist playList)
        {
            if (!DuplicateItemCheck(playList))
            {
                await _repo.ReplaceOneAsync(playList);
            }
            else
            {
                var fixedPlayList = RemoveDuplicateItems(playList);
                await _repo.ReplaceOneAsync(fixedPlayList);
            }
        }

        private bool DuplicateItemCheck (Playlist playlist)
        {
            if (playlist.Services.Count != playlist.Services.Distinct().Count())
            {
                return true;
            }

            else { return false; }
        }
            
        private Playlist RemoveDuplicateItems(Playlist playList) 
        {
            var uniqueServices = playList.Services.Distinct().ToList();
            playList.Services = uniqueServices;
            return playList;
        }
    }
}