using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repositorios.Scraping.Interfaces
{
    public interface IRepositorioBase<TEntidad> where TEntidad : class
    {
        Task<ICollection<TEntidad>> ListAsync();
        Task<ICollection<TInfo>> ListAsync<TInfo>(
            Expression<Func<TEntidad, bool>> predicado,
            Expression<Func<TEntidad, TInfo>> selector);
        Task<(ICollection<TInfo> Coleccion, int TotalRegistros)> ListAsync<TInfo, TKey>(
            Expression<Func<TEntidad, bool>> predicado,
            Expression<Func<TEntidad, TInfo>> selector,
            Expression<Func<TEntidad, TKey>> orderBy,
            int pagina = 1, int filas = 10);
        Task<TEntidad?> FindByIdAsync(int id);
        Task<TEntidad> AddAsync(TEntidad entidad);
        Task UpdateAsync();
    }
}
