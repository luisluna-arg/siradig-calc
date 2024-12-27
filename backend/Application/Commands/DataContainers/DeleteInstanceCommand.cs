using System.Linq.Expressions;
using MediatR;
using SiradigCalc.Core.Entities.Base;
using SiradigCalc.Infra.Persistence.DbContexts;

namespace SiradigCalc.Application.Commands.DataContainers;

public abstract class DeleteInstanceCommand<TId>() : IRequest<bool>
{
    public TId Id { get; } = default!;

    public DeleteInstanceCommand(TId id) : this()
    {
        Id = id;
    }
}

public abstract class DeleteInstanceCommandHandler<TCommand, TEntity, TId>(ISolutionDbContext dbContext)
    : IRequestHandler<TCommand, bool>
    where TCommand : DeleteInstanceCommand<TId>
    where TEntity : Entity<TId>, IEntity, new()
{
    public async virtual Task<bool> Handle(TCommand command, CancellationToken cancellationToken)
    {
        var dbSet = dbContext.Set<TEntity>();
        var entity = dbSet.Single(CreateFilterFunc(command));

        dbSet.Remove(entity);

        await dbContext.SaveChangesAsync(cancellationToken);

        return true;
    }

    protected virtual Expression<Func<TEntity, bool>> CreateFilterFunc(TCommand command)
        => (e) => e.Id!.Equals(command.Id);
}
