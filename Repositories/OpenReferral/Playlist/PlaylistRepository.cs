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

        public Task<Playlist> FindById(string userId)
        {
            return _repo.FindOneAsync(x => x.UserId == userId);
        }

        public async Task InsertOrUpdateOne(Playlist playList)
        {
            var x = _repo.FindOneAsync(x => x.UserId == playList.UserId);
            if (x == null )
            {
                await _repo.InsertOneAsync(playList);
            }
            else
            {
                await _repo.ReplaceOneAsync(playList);
            }
        }
    }
}