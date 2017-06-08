using System;
using System.Collections.Generic;

namespace face_api_models.Model
{
    public partial class FaceRepositoryBusinessClient
    {
        public long Id { get; set; }
        public long FaceReposityId { get; set; }
        public long BusinessClientId { get; set; }
    }
}
