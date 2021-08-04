using System.Threading.Tasks;
using Alura.Challenge.Domain.Models.Data;

namespace Alura.Challenge.Domain.Interfaces
{
    public interface IGrantTokenService
    {
        public string GenerateToken(User user);
    }
}