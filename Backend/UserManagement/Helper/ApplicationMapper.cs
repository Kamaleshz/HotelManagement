using UserManagement.Models.DTO;
using AutoMapper;

using UserManagement.Models;
namespace UserManagement.Helper
{
    public class ApplicationMapper : Profile
    {
        public ApplicationMapper()
        {
            CreateMap<UserDTO, User>().ReverseMap();
        }
    }
}
