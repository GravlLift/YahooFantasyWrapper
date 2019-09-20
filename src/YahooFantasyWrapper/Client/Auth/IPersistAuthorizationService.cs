using System.Threading.Tasks;
using YahooFantasyWrapper.Models;

namespace YahooFantasyWrapper.Client
{
    public interface IPersistAuthorizationService
    {
        Task<AuthModel> GetAuthModelAsync();
        Task UpdateAuthModelAsync(AuthModel authModel);
    }
}
