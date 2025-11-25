using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace HelpdeskDAL;

// Generic repository implementation for Helpdesk entities
// Provides methods for CRUD operations
public class HelpdeskRepository<T> : IRepository<T> where T : HelpdeskEntity
{
    // Database context for Helpdesk
    readonly private HelpdeskContext _db;

    // Constructor
    public HelpdeskRepository()
    {
        _db = new HelpdeskContext();
    }

    // Method to get all entities of type T from the database
    public async Task<List<T>> GetAll()
    {
        return await _db.Set<T>().ToListAsync();
    }

    // Method to get entities of type T that match a specified condition
    public async Task<List<T>> GetSome(Expression<Func<T, bool>> match)
    {
        return await _db.Set<T>().Where(match).ToListAsync();
    }

    // Method to get a single entity of type T that matches a specified condition
    public async Task<T?> GetOne(Expression<Func<T, bool>> match)
    {
        return await _db.Set<T>().FirstOrDefaultAsync(match);
    }

    // Method to add a new entity of type T to the database
    public async Task<T> Add(T entity)
    {
        _db.Set<T>().Add(entity);
        await _db.SaveChangesAsync();
        return entity;
    }

    // Method to update an existing entity of type T in the database
    public async Task<UpdateStatus> Update(T updatedEntity)
    {
        // default to failed
        UpdateStatus operationStatus = UpdateStatus.Failed;
        try
        {
            // get current entity from database
            T? currentEntity = await GetOne(ent => ent.Id == updatedEntity.Id);
            // set original timer value to detect concurrency
            _db.Entry(currentEntity!).OriginalValues["Timer"] = updatedEntity.Timer;
            // set all values to updated entity values
            _db.Entry(currentEntity!).CurrentValues.SetValues(updatedEntity);
            if (await _db.SaveChangesAsync() == 1) // should throw exception if stale;
                operationStatus = UpdateStatus.Ok;
        }
        catch (DbUpdateConcurrencyException dbx)
        {
            operationStatus = UpdateStatus.Stale;
            Console.WriteLine("Problem in " + MethodBase.GetCurrentMethod()!.Name + dbx.Message);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Problem in " + MethodBase.GetCurrentMethod()!.Name + ex.Message);
        }
        return operationStatus;
    }

    // Method to delete an entity of type T from the database by ID
    public async Task<int> Delete(int id)
    {
        // get current entity from database
        T? currentEntity = await GetOne(ent => ent.Id == id);
        // remove entity
        _db.Set<T>().Remove(currentEntity!);
        return _db.SaveChanges();
    }
}