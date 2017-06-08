using System;
using System.Collections.Generic;

namespace face_api_models.Model
{
    public partial class UserBusinessClient
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public long BusinessClientId { get; set; }
    }
}
