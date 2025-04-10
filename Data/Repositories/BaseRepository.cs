using System.Diagnostics;
using System.Linq.Expressions;
using Data.Context;
using Data.Interfaces;
using Data.Models;
using Domain.Extensions;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Data.Repositories;

public abstract class BaseRepository<TEntity, TModel> : IBaseRepository<TEntity, TModel> where TEntity : class
{

    protected readonly AppDBContext _context;
    protected readonly DbSet<TEntity> _dbSet;

    protected BaseRepository(AppDBContext context)
    {
        _context = context;
        _dbSet = _context.Set<TEntity>();
    }


    //Create
    public virtual async Task<RepositoryResult<bool>> CreateAsync(TEntity entity)
    {
        if (entity == null)
            return new RepositoryResult<bool> { IsSuccess = false, StatusCode = 400, ErrorMessage = "Entity is null" };
        try
        {
            _dbSet.Add(entity);
            await _context.SaveChangesAsync();
            return new RepositoryResult<bool> { IsSuccess = true, StatusCode = 201 };
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error Creating {nameof(TEntity)} entity :: {ex.Message}");
            return new RepositoryResult<bool> { IsSuccess = false, StatusCode = 500, ErrorMessage = ex.Message };
        }

    }

    //Read
    public virtual async Task<RepositoryResult<IEnumerable<TModel>>> GetAllAsync(bool orderByDescending = false, Expression<Func<TEntity, object>>? sortBy = null, Expression<Func<TEntity, bool>>? filterBy = null, params Expression<Func<TEntity, object>>[] includes)
    {
        IQueryable<TEntity> query = _dbSet;

        if (filterBy != null)
            query = query.Where(filterBy);

        if (includes != null && includes.Length != 0)
            foreach (var include in includes)
                query = query.Include(include);

        if (sortBy != null)
            query = orderByDescending ? query.OrderByDescending(sortBy) : query.OrderBy(sortBy);

        var enteties = await query.ToListAsync();

        var result = enteties.Select(entity => entity.MapTo<TModel>());
        return new RepositoryResult<IEnumerable<TModel>>() { IsSuccess = true, StatusCode = 200, Result = result };
    }


    public virtual async Task<RepositoryResult<IEnumerable<TSelect>>> GetAllAsync<TSelect>(Expression<Func<TEntity, TSelect>> selector, bool orderByDescending = false, Expression<Func<TEntity, object>>? sortBy = null, Expression<Func<TEntity, bool>>? filterBy = null, params Expression<Func<TEntity, object>>[] includes)
    {
        IQueryable<TEntity> query = _dbSet;

        if (filterBy != null)
            query = query.Where(filterBy);

        if (includes != null && includes.Length != 0)
            foreach (var include in includes)
                query = query.Include(include);

        if (sortBy != null)
            query = orderByDescending ? query.OrderByDescending(sortBy) : query.OrderBy(sortBy);

        var enteties = await query.Select(selector).ToListAsync();

        var result = enteties.Select(entity => entity!.MapTo<TSelect>());
        return new RepositoryResult<IEnumerable<TSelect>>() { IsSuccess = true, StatusCode = 200, Result = result };
    }


    public virtual async Task<RepositoryResult<TModel>> GetAsync(Expression<Func<TEntity, bool>> filterBy, params Expression<Func<TEntity, object>>[] includes)
    {

        IQueryable<TEntity> query = _dbSet;

        if (includes != null && includes.Length != 0)
            foreach (var include in includes)
                query = query.Include(include);

        var entity = await query.FirstOrDefaultAsync(filterBy);

        if (entity == null)
            return new RepositoryResult<TModel> { IsSuccess = false, StatusCode = 404, ErrorMessage = "Entity not found" };

        var result = entity.MapTo<TModel>();
        return new RepositoryResult<TModel> { IsSuccess = true, StatusCode = 200, Result = result };
    }

    public virtual async Task<RepositoryResult<bool>> ExistsAsync(Expression<Func<TEntity, bool>> findBy)
    {
        var exists = await _dbSet.AnyAsync(findBy);
        return !exists
            ? new RepositoryResult<bool> { IsSuccess = false, StatusCode = 404, ErrorMessage = "Entity not found" }
            : new RepositoryResult<bool> { IsSuccess = true, StatusCode = 200 };
    }
    //Update

    public virtual async Task<RepositoryResult<bool>> UpdateAsync(TEntity entity)
    {
        if (entity == null)
            return new RepositoryResult<bool> { IsSuccess = false, StatusCode = 400, ErrorMessage = "Entity is null" };
        try
        {
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
            return new RepositoryResult<bool> { IsSuccess = true, StatusCode = 200 };
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error Updating {nameof(TEntity)} entity :: {ex.Message}");
            return new RepositoryResult<bool> { IsSuccess = false, StatusCode = 500, ErrorMessage = ex.Message };
        }
    }

    //Delete

    public virtual async Task<RepositoryResult<bool>> DeleteAsync(TEntity entity)
    {
        if (entity == null)
            return new RepositoryResult<bool> { IsSuccess = false, StatusCode = 400, ErrorMessage = "Entity is null" };
        try
        {
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
            return new RepositoryResult<bool> { IsSuccess = true, StatusCode = 200 };
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error Deleting {nameof(TEntity)} entity :: {ex.Message}");
            return new RepositoryResult<bool> { IsSuccess = false, StatusCode = 500, ErrorMessage = ex.Message };
        }
    }

}

