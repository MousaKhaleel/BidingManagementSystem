
namespace BidingManagementSystem.Application.Notifications
{
	public interface INotificationService
	{
		Task SendEmailAsync(string email, string subject, string body);
	}
}