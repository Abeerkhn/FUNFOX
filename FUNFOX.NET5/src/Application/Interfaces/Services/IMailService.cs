using FUNFOX.NET5.Application.Requests.Mail;
using System.Threading.Tasks;

namespace FUNFOX.NET5.Application.Interfaces.Services
{
    public interface IMailService
    {
        Task SendAsync(MailRequest request);
    }
}