﻿using System;
using System.Collections.Generic;

namespace face_api_commons.Model
{
    public partial class FaceRepository
    {
        public long Id { get; set; }
        public string FaceRepositoryId { get; set; }
        public string FaceRepositoryName { get; set; }
        public string FaceRepositoryComments { get; set; }
        public long Availiable { get; set; }
    }
}
