using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Aggregates.Branches
{
	public interface IBranchReposirtory
	{
		void AddBranch(Branch entity);
		
		Task<List<Branch>> GetAllBranchesAsync();
		Task<Branch> GetBranchAsync(Guid id);
		Task<List<Branch>> GetBranchesOfStore(Guid storeId);
		void Remove(Branch entity);

	}
}
