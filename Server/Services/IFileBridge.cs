using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Services
{
    public abstract class IFileBridge<Entity> : IDisposable, I_CRUD_Async<Entity> where Entity : Models.IEntity
    {
        public string Path { get; protected set; }
        public List<Entity> Entities { get; protected set; } //solid
        public event Action OnDispose;

        public IFileBridge(string path)
        {
            Path = path;
        }
        public void Dispose()
        {
            OnDispose?.Invoke();
            Save();
        }
        public abstract void Save();
        public async Task<IEnumerable<Entity>> GetAllAsync() => await Task.FromResult(Entities);
        public async Task<IEnumerable<Entity>> GetAsync(Func<Entity, bool> predicate) => await Task.FromResult(Entities.Where(predicate));
        public async Task CreateAsync(Entity entity)
        {
            Entities.Add(entity);
            await Task.CompletedTask;
        }
        public async Task UpdateAllAsync(Predicate<Entity> predicate, Entity entity)
        {
            for(int step = 0; step < Entities.Count; step++)
            {
                if (predicate(Entities[step])) Entities[step] = entity;
            }
            await Task.CompletedTask;
        }
        public async Task UpdateAsync(Predicate<Entity> predicate, Entity entity)
        {
            Entities[Entities.FindIndex(predicate)] = entity;
            await Task.CompletedTask;
        }
        public async Task RemoveAllAsync(Predicate<Entity> predicate)
        {
            Entities.RemoveAll(predicate);
            await Task.CompletedTask;
        }
        public async Task RemoveAsync(Predicate<Entity> predicate)
        {
            Entities.RemoveAt(Entities.FindIndex(predicate));
            await Task.CompletedTask;
        }
    }
}
