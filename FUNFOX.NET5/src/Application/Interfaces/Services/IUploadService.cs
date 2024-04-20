using FUNFOX.NET5.Application.Requests;

namespace FUNFOX.NET5.Application.Interfaces.Services
{
    public interface IUploadService
    {
        string UploadAsync(UploadRequest request);
    }
}