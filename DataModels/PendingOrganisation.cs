using OpenReferrals.Repositories.Common;

namespace OpenReferrals.DataModels
{
    [BsonCollection("pending-organisations")]
    public class PendingOrganisation : Organisation
    {
        public string UserId { get; set; }
        public string UserEmail { get; set; }
    }
}
