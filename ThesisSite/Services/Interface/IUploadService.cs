using System.Threading.Tasks;
using ThesisSite.ViewModel.Assignments;

namespace ThesisSite.Services.Interface
{
    public interface IUploadService
    {
        Task<string> UploadFile(string userId, UploadSolutionViewModel vm);
    }
}