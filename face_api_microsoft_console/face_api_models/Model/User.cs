using System;
using System.Collections.Generic;

namespace face_api_commons.Model
{
    public partial class User
    {
        public long Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Roles { get; set; }
    }
}
