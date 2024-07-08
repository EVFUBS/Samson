using Microsoft.Extensions.Configuration;
using System.Net.Http.Json;

namespace SamsonLocal.Models.Samson
{
    public class SamsonServerCredentials(IConfiguration config) : ISamsonServerCredentials
    {
        private readonly string? _email = config.GetSection("SamsonServer").GetSection("Credentials").GetValue<string>("email");
        private readonly string? _password = config.GetSection("SamsonServer").GetSection("Credentials").GetValue<string>("password");
        private readonly string? _username = config.GetSection("SamsonServer").GetSection("Credentials").GetValue<string>("username");
        private readonly string? _url = config.GetSection("SamsonServer").GetValue<string>("SamsonServerUrl");
        public string? Token;

        public async Task Login()
        {
            var client = new HttpClient();

            var request = new Dictionary<string, string?>
            {
                {"email", _email},
                {"password", _password},
                {"username", _username},
            };
            JsonContent content = JsonContent.Create(request);

            var response = await client.PostAsync(_url + "/Users/login", content);
            Token = await response.Content.ReadAsStringAsync();
        }
    }
}
