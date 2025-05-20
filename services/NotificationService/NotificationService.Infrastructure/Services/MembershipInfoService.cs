using System.Net.Http.Json;

namespace NotificationService.Infrastructure.Services
{
    public class MembershipInfoService : IMembershipInfoService
    {
        private readonly HttpClient _httpClient;

        public MembershipInfoService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<int>> GetUserIdsByClubIdAsync(int clubId)
        {
            var response = await _httpClient.GetAsync($"/api/membership/club/{clubId}");

            if (!response.IsSuccessStatusCode)
                return new List<int>();

            var result = await response.Content.ReadFromJsonAsync<List<ClubMemberDto>>();
            return result?.Select(x => x.UserId).ToList() ?? new List<int>();
        }

        private class ClubMemberDto
        {
            public int UserId { get; set; }
        }
    }
}
