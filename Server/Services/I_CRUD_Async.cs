using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Services
{
    public interface I_CRUD_Async<Entity>
    {
        public Task<IEnumerable<Entity>> GetAllAsync();
        public Task<IEnumerable<Entity>> GetAsync(Func<Entity, bool> predicate);
        public Task CreateAsync(Entity entity);
        public Task UpdateAsync(Predicate<Entity> predicate, Entity entity);
        public Task UpdateAllAsync(Predicate<Entity> predicate, Entity entity);
        public Task RemoveAsync(Predicate<Entity> predicate);
        public Task RemoveAllAsync(Predicate<Entity> predicate);
    }
}
