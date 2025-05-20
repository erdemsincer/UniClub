namespace NotificationService.Infrastructure.Services
{

    public interface IMembershipInfoService
    {
        Task<List<int>> GetUserIdsByClubIdAsync(int clubId);
    }
}
