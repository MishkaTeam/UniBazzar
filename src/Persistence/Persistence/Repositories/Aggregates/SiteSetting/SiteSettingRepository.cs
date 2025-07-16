using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Aggregates.SiteSettings;
using System.Linq.Expressions;
namespace Persistence.Repositories.Aggregates.SiteSetting
{
    public class SiteSettingRepository(UniBazzarContext uniBazzarContext, IMemoryCache memoryCache) : ISiteSettingRepository
    {
        public Task AddAsync(Domain.Aggregates.SiteSettings.SiteSetting entity, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task AddRangeAsync(IEnumerable<Domain.Aggregates.SiteSettings.SiteSetting> entities, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Domain.Aggregates.SiteSettings.SiteSetting>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<Domain.Aggregates.SiteSettings.SiteSetting> GetAsync(Expression<Func<Domain.Aggregates.SiteSettings.SiteSetting, bool>> predicate, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<Domain.Aggregates.SiteSettings.SiteSetting> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Domain.Aggregates.SiteSettings.SiteSetting>> GetSomeAsync(Expression<Func<Domain.Aggregates.SiteSettings.SiteSetting, bool>> predicate, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task RemoveAsync(Domain.Aggregates.SiteSettings.SiteSetting entity, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<bool> RemoveByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task RemoveRangeAsync(IEnumerable<Domain.Aggregates.SiteSettings.SiteSetting> entities, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Domain.Aggregates.SiteSettings.SiteSetting entity, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
