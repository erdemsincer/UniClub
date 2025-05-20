using System.Net.Http.Json;

namespace NotificationService.Infrastructure.Services
{
    public class UserInfoService : IUserInfoService
    {
        private readonly HttpClient _httpClient;

        public UserInfoService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string?> GetEmailByUserIdAsync(int userId)
        {
            var response = await _httpClient.GetAsync($"/api/Auth/{userId}");

            if (!response.IsSuccessStatusCode)
                return null;

            var json = await response.Content.ReadFromJsonAsync<UserResponse>();
            return json?.Email;
        }

        private class UserResponse
        {
            public string Email { get; set; } = string.Empty;
        }
    }
}
