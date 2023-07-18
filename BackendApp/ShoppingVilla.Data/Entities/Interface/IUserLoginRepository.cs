using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingVilla.Data.Entities.Interface
{
    public interface IUserLoginRepository
    {
        Task<UserLogin> Get(int id);
        Task<UserLogin> Login(UserLogin userLogin);
        Task<int> Logout(string token);
    }
}
