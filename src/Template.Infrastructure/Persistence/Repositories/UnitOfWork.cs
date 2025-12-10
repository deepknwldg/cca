using Microsoft.EntityFrameworkCore.Storage;
using Template.Application.Abstractions.Persistence.Repositories;

namespace Template.Infrastructure.Persistence.Repositories;

/// <summary>
/// Реализация шаблона Unit‑of‑Work. Управляет транзакциями EF Core
/// и гарантирует атомарность групп операций репозитория.
/// </summary>
public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _db;
    private IDbContextTransaction? _transaction;
    private int _transactionDepth;

    /// <summary>
    /// Конструктор, получает <see cref="ApplicationDbContext"/> через DI.
    /// </summary>
    /// <param name="db">Контекст базы данных.</param>
    public UnitOfWork(ApplicationDbContext db)
    {
        _db = db;
    }

    /// <inheritdoc />
    public async Task BeginTransactionAsync(CancellationToken cancellationToken = default)
    {
        if (_transactionDepth == 0)
        {
            _transaction = await _db.Database.BeginTransactionAsync(cancellationToken);
        }

        _transactionDepth++;
    }

    /// <inheritdoc />
    public async Task CommitAsync(CancellationToken cancellationToken = default)
    {
        if (_transactionDepth == 0)
        {
            return;
        }

        _transactionDepth--;

        if (_transactionDepth == 0 && _transaction != null)
        {
            await _transaction.CommitAsync(cancellationToken);
            await _transaction.DisposeAsync();
            _transaction = null;
        }
    }

    /// <inheritdoc />
    public async Task RollbackAsync(CancellationToken cancellationToken = default)
    {
        if (_transaction == null)
        {
            return;
        }

        _transactionDepth = 0;

        await _transaction.RollbackAsync(cancellationToken);
        await _transaction.DisposeAsync();

        _transaction = null;
    }

    /// <inheritdoc />
    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await _db.SaveChangesAsync(cancellationToken);
    }
}
