﻿namespace BlogBackend.Application.DTOs.Requests
{
    [Serializable]
    public class RefreshRequest
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}
