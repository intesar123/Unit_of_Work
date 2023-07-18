using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingVilla.Data.Entities.Interface
{
    public interface IUserRegisterRepository: IGenericRepository<UserRegister>
    {
        Task<List<UserRegister>> GetAllAsync();
        Task<UserRegister> GetByIdAsync(int id);
        void CreateAsync(UserRegister userRegister);
        void UpdateAsync(UserRegister userRegister);
        void DeleteAsync(UserRegister userRegister);
    }
}
