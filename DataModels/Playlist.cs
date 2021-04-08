

using OpenReferrals.Repositories.Common;
using OpenReferrals.Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OpenReferrals.DataModels
{
    [BsonCollection("playlists")]
    public class Playlist : IMongoDocument
    {
        public string Id { get; set; }
        public string UserId { get; set; }

        public string[] Services { get; set; }
    }
}
