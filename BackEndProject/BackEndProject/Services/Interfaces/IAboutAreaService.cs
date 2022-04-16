using BackEndProject.Models;
using System.Threading.Tasks;

namespace BackEndProject.Services.Interfaces
{
    public interface IAboutAreaService
    {
       Task<AboutArea> GetAbout();
    }
}
