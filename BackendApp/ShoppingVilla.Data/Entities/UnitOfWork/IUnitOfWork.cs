using ShoppingVilla.Data.Data;
using ShoppingVilla.Data.Entities.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ShoppingVilla.Data.Entities.UnitOfWork
{
    public interface IUnitOfWork: IDisposable
    {
        IUserRegisterRepository userRegisterRepository { get; }
        IUserLoginRepository userLoginRepository { get; }
        Task<int> SaveChangesAsync();

    }
}
