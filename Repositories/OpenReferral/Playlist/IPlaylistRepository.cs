using OpenReferrals.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OpenReferrals.Repositories.OpenReferral
{
    public interface IPlaylistRepository
    {
        Task InsertOrUpdateOne(Playlist org);
        Task<Playlist> FindById(string userId);
    }
}
