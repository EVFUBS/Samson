using Microsoft.Extensions.Configuration;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;

namespace SamsonConsoleApp.Models.Samson
{
    public class SamsonServerCredentials : ISamsonServerCredentials
    {
        public readonly string? emailOrUsername;
        public readonly string? password;
        public string? token;
        private readonly string? url;

        public SamsonServerCredentials(IConfiguration config)
        {
            emailOrUsername = config.GetSection("SamsonServer").GetSection("Credentials").GetValue<string>("emailOrUsername");
            password = config.GetSection("SamsonServer").GetSection("Credentials").GetValue<string>("password");
            url = config.GetSection("SamsonServer").GetValue<string>("SamsonServerUrl");
        }

        public async Task Login()
        {
            var client = new HttpClient();

            var request = new Dictionary<string, string>
            {
                {"emailOrUsername", emailOrUsername},
                {"password", password},
            };
            JsonContent content = JsonContent.Create(request);

            var response = await client.PostAsync(url + "/Users/login", content);
            token = await response.Content.ReadAsStringAsync();
        }
    }
}
