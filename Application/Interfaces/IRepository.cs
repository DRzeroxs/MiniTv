using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IRepository
    {
        Task AddAsync(Object entity);
        Task UpdateAsync(Object entity);
        Task DeleteAsync(Object entity);
    }
}
