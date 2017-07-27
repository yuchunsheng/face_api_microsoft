using System;
using System.Collections.Generic;

namespace face_api_commons.Model
{
    public partial class FaceDocRepository
    {
        public long Id { get; set; }
        public string FaceRepositoryId { get; set; }
        public string FaceDocId { get; set; }
    }
}
