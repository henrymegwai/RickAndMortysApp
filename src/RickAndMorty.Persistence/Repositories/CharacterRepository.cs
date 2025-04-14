using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RickAndMorty.Application.Abstractions;
using RickAndMorty.Data.Entities;

namespace RickAndMorty.Persistence.Repositories;

public class CharacterRepository (ApplicationDbContext context, ILogger<CharacterRepository> logger) : ICharacterRepository
{
    private readonly DbSet<Character> _dbSet = context.Set<Character>();
    
    public async Task<List<Character>> GetAllAsync(
        int page, 
        int pageSize, 
        CancellationToken cancellationToken, 
        string? planetName = null)
    {
        var query = _dbSet.AsQueryable();

        if (!string.IsNullOrEmpty(planetName))
        {
            query = query.Where(c => c.LocationName == planetName);
        }
        
        return await query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);
    }
    
    public async Task<List<string?>> GetAllDistinctLocationsAsync(CancellationToken cancellationToken)
    {

        var query = _dbSet.AsQueryable();

        var result = await query
            .Where(x => !string.IsNullOrEmpty(x.LocationName))
            .Select(c => c.LocationName)
            .Distinct()
            .ToListAsync(cancellationToken);
        
        return result;
    }

    public async Task<Character?> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        if (id <= 0)
        {
            return null;
        }
        return await _dbSet.AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }
    
    public async Task<Character?> GetByNameAsync(string name, CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(name))
        {
            return null;
        }
        return await _dbSet.AsNoTracking()
            .FirstOrDefaultAsync(x => x.Name == name, cancellationToken);
    }


    public async Task AddAsync(Character character, CancellationToken cancellationToken)
    {
        await _dbSet.AddAsync(character, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task<List<Character>> AddCollectionAsync(
        List<Character> listOfItems, 
        CancellationToken cancellationToken)
    {
        if (listOfItems.Count == 0)
        {
            return [];
        }
        
        await using var transaction = await context.Database.BeginTransactionAsync(cancellationToken);

        try
        {
            context.Set<Character>().RemoveRange(context.Set<Character>());
            
            await context.Set<Character>().AddRangeAsync(listOfItems, cancellationToken);
            
            await context.SaveChangesAsync(cancellationToken);

            await transaction.CommitAsync(cancellationToken);
        }
        catch(Exception ex)
        {
            // Rollback transaction in case of an error
            await transaction.RollbackAsync(cancellationToken);
            logger.LogError("An error occured while executing transaction: {Message}", ex.Message);
        }
        
        return listOfItems;
    }
}