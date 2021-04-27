using System.Collections.Generic;

namespace Models.Auth
{
    public class AuthenticationResponse
    {
        public string Token { get; set; }

        public bool Success { get; set; }

        public List<string> Errors { get; set; }
    }
}
