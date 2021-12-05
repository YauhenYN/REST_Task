using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Web
{
    public interface ICRUDAsync<Param, Entity>
    {
        Task<IEnumerable<Entity>> ReadAllAsync();
        Task<IEnumerable<Entity>> ReadAsync(Param p);
        Task CreateAsync(Entity entity);
        Task UpdateAllAsync(Param param, Entity entity);
        Task RemoveAllAsync(Param param);

    }
}
