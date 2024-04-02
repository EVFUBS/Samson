using Microsoft.Extensions.Configuration;
using System.Net.Http.Json;

namespace SamsonConsoleApp.Models.Samson
{
    public class SamsonServerCredentials : ISamsonServerCredentials
    {
        public readonly string? email;
        public readonly string? password;
        public readonly string? username;
        public string? token;
        private readonly string? url;

        public SamsonServerCredentials(IConfiguration config)
        {
            email = config.GetSection("SamsonServer").GetSection("Credentials").GetValue<string>("email");
            password = config.GetSection("SamsonServer").GetSection("Credentials").GetValue<string>("password");
            username = config.GetSection("SamsonServer").GetSection("Credentials").GetValue<string>("username");
            url = config.GetSection("SamsonServer").GetValue<string>("SamsonServerUrl");
        }

        public async Task Login()
        {
            var client = new HttpClient();

            var request = new Dictionary<string, string?>
            {
                {"email", email},
                {"password", password},
                {"username", username},
            };
            JsonContent content = JsonContent.Create(request);

            var response = await client.PostAsync(url + "/Users/login", content);
            token = await response.Content.ReadAsStringAsync();
        }
    }
}
