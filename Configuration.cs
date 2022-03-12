﻿namespace CadastroApi
{
    public static class Configuration
    {
        public static string JwtKey { get; set; }
        public static string BaseUrl { get; set; }
        public static SmtpConfiguration Smtp { get; set; } = new();

        public class SmtpConfiguration
        {
            public string Host { get; set; }
            public int Port { get; set; } = 25;
            public string UserName { get; set; }
            public string Password { get; set; }
        }
    }
}
