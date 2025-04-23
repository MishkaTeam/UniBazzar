using System.Linq.Expressions;
using Domain.BuildingBlocks.Data;
using Domain.BuildingBlocks.SeedWork;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Persistence.Exceptions;
using Persistence.Extensions;

namespace BuildingBlocks.Persistence;

public class RepositoryBase<TEntity> : IRepositoryBase<TEntity> 
	where TEntity : class, IEntity
	{
		public RepositoryBase(UniBazzarContext context, IExecutionContextAccessor executionContext)
		{
			DatabaseContext = context ?? throw new ArgumentNullException("databaseContext");
			ExecutionContext = executionContext;
			DbSet = DatabaseContext.Set<TEntity>();
		}

		protected DbSet<TEntity> DbSet { get; }

		protected DbContext DatabaseContext { get; }
		public IExecutionContextAccessor ExecutionContext { get; }

		public virtual async Task AddAsync(TEntity entity, CancellationToken cancellationToken = default)
		{
			if (entity == null)
			{
				throw new ArgumentNullException("entity");
			}

			entity.IncreaseVersion();

			entity.SetInsertBy(ExecutionContext.UserId ?? throw new Exception("User Is Empty"));

			entity.SetOwner(ExecutionContext.UserId ?? throw new Exception("User Is Empty"));

			entity.SetStore(ExecutionContext.StoreId);

			entity.SetInsertDateTime();

			await DbSet.AddAsync(entity, cancellationToken);
		}

		public virtual async Task AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
		{
			if (entities == null)
			{
				throw new ArgumentNullException("entities");
			}

			await DbSet.AddRangeAsync(entities, cancellationToken);
		}

		public virtual async Task RemoveAsync(TEntity entity, CancellationToken cancellationToken = default)
		{
			TEntity entity2 = entity;
			switch (entity)
			{
				case null:
				throw new ArgumentNullException(paramName: nameof(entity));

				case IEntityHasIsDeleted softDeletedEntity:
				softDeletedEntity.Delete();
				softDeletedEntity.SetDeleteDateTime();
				await SoftDeleteAsync(entity, cancellationToken);
				break;

				default:
				await Task.Run(delegate
				{
					DbSet.Remove(entity2);
				}, cancellationToken);
				break;
			}
		}
		private async Task SoftDeleteAsync(TEntity entity, CancellationToken cancellationToken = default)
		{
			DbSet.Update(entity);
		}
		public virtual async Task<bool> RemoveByIdAsync(Guid id, CancellationToken cancellationToken = default)
		{
			TEntity entity = await FindAsync(id, cancellationToken);

			await RemoveAsync(entity, cancellationToken);

			return true;
		}

		public virtual async Task RemoveRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
		{
			if (entities == null)
			{
				throw new ArgumentNullException("entities");
			}

			foreach (TEntity entity in entities)
			{
				await RemoveAsync(entity, cancellationToken);
			}
		}

		public virtual async Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
		{

			TEntity oldentity = await FindAsync(entity.Id, cancellationToken);

			if (oldentity.Version > entity.Version)
				throw new InvalidEntityVersionException();

			entity.SetVersionAndIncrease(oldentity.Version);

			entity.SetUpdateBy(ExecutionContext.UserId ?? throw new Exception("User Is Empty"));

			entity.SetUpdateDateTime();

			DbSet.Update(entity);
		}

		public virtual async Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken = default)
		{
			return await DbSet
							.StoreFilter<TEntity>(tenantId: ExecutionContext.StoreId)
							.AsNoTracking()
							.ToListAsync(cancellationToken);
		}
		

		public virtual async Task<IEnumerable<TEntity>> Find(Expression<Func<TEntity, bool>> predicate
			, CancellationToken cancellationToken = default)
		{
			return await DbSet
			.AsNoTracking()
			.Where(predicate)
			.ToListAsync(cancellationToken);
		}

		public virtual async Task<TEntity> FindAsync(Guid id, CancellationToken cancellationToken = default)
		{
			return await DbSet
				.StoreFilter<TEntity>(tenantId: ExecutionContext.StoreId)
				.FirstOrDefaultAsync(x => x.Id.Equals(id), cancellationToken);
		}

		public virtual async Task<IEnumerable<TEntity>> GetSomeAsync(Expression<Func<TEntity, bool>> predicate
				, CancellationToken cancellationToken = default)
		{
			return await DbSet
			.StoreFilter<TEntity>(tenantId: ExecutionContext.StoreId)
			.AsNoTracking()
			.Where(predicate)
			.ToListAsync(cancellationToken);
		}

		public virtual async Task<TEntity> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
		{
			return await FindAsync(id, cancellationToken);
		}

		public virtual async Task SaveChangesAsync(CancellationToken cancellationToken = default)
		{
			await DatabaseContext.SaveChangesAsync(cancellationToken);
		}

		public virtual async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
		{
			return await DbSet.
			StoreFilter<TEntity>(tenantId: ExecutionContext.StoreId)
			.AsNoTracking()
			.FirstOrDefaultAsync(predicate, cancellationToken);
		}

	}