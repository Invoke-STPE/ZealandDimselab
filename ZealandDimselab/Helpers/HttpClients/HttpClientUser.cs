using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using ZealandDimselab.DTO;
using ZealandDimselab.Lib.Models;

namespace ZealandDimselab.Helpers.HttpClients
{
    public class HttpClientUser : IHttpClientUser
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private readonly string _baseUrl = "UserAPI:BaseUrlUser";

        public HttpClientUser(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        public async Task AddUserAsync(string paramEmail)
        {
            UserDto user = new UserDto
            {
                Email = paramEmail,
                Role = "student"
            };
            string url = _configuration.GetValue<string>(_baseUrl);
            string jsonString = JsonSerializer.Serialize(user);
            StringContent stringContent = new StringContent(jsonString, Encoding.UTF8, url);

            await _httpClient.PostAsync(jsonString, stringContent);
        }

        public async Task<UserDto> GetUserByEmailAsync(string email)
        {
            var builder = new UriBuilder(_configuration.GetValue<string>("UserAPI:GetUserByEmail"));
            builder.Query = $"email={email}";
            string url = builder.ToString();
            var response = await _httpClient.GetStreamAsync(url);
            return await JsonSerializer.DeserializeAsync<UserDto>(response);
        }
    }
}
