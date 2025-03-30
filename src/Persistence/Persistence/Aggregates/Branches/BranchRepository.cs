using Domain.Aggregates.Branches;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Aggregates.Branches
{
	public class BranchRepository(UniBazzarContext uniBazzarContext) : IBranchReposirtory
	{
		public void AddBranch(Branch entity)
		{
			uniBazzarContext.Add(entity);
		}

		public Task<List<Branch>> GetAllBranchesAsync()
		{
			return uniBazzarContext.Branches.ToListAsync();
		}

		public Task<Branch> GetBranchAsync(Guid id)
		{
			return uniBazzarContext.Branches.FirstOrDefaultAsync(x => x.Id == id);
		}

		public async Task<List<Branch>> GetBranchesOfStore(Guid storeId)
		{
			return await uniBazzarContext.Branches.Where(x=>x.StoreId == storeId).ToListAsync();	
		}

		public void Remove(Branch entity)
		{
			uniBazzarContext.Branches.Remove(entity);
		}
	}
}
