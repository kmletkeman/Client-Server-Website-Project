using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HelpdeskDAL;

// Generic repository interface for Helpdesk entities
public interface IRepository<T>
{
    // Method signatures for CRUD operations
    Task<List<T>> GetAll();
    Task<List<T>> GetSome(Expression<Func<T, bool>> match);
    Task<T?> GetOne(Expression<Func<T, bool>> match);
    Task<T> Add(T entity);
    Task<UpdateStatus> Update(T enity);
    Task<int> Delete(int i);
}
