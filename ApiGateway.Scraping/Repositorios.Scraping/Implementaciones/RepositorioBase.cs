using AccesoDatos.Scraping.Contexto;
using Microsoft.EntityFrameworkCore;
using Repositorios.Scraping.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repositorios.Scraping.Implementaciones
{
    public class RepositorioBase<TEntidad> : IRepositorioBase<TEntidad> where TEntidad : class
    {
        private readonly ScraperdbContext Contexto;

        public RepositorioBase(ScraperdbContext _contexto)
        {
            Contexto = _contexto;
        }

        public async Task<ICollection<TEntidad>> ListAsync()
        {
            return await Contexto.Set<TEntidad>()
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<ICollection<TInfo>> ListAsync<TInfo>(
            Expression<Func<TEntidad, bool>> predicado,
            Expression<Func<TEntidad, TInfo>> selector)
        {
            var resultado = await Contexto.Set<TEntidad>()
                .Where(predicado)
                .AsNoTracking()
                .Select(selector)
                .ToListAsync();
            return resultado;
        }

        public async Task<(ICollection<TInfo> Coleccion, int TotalRegistros)> ListAsync<TInfo, TKey>(
            Expression<Func<TEntidad, bool>> predicado,
            Expression<Func<TEntidad, TInfo>> selector,
            Expression<Func<TEntidad, TKey>> orderBy,
            int pagina = 1, int filas = 10)
        {
            var resultado = await Contexto.Set<TEntidad>()
                .Where(predicado)
                .AsNoTracking()
                .OrderBy(orderBy)
                .Skip((pagina - 1) * filas)
                .Take(filas)
                .Select(selector)
                .ToListAsync();

            var total = await Contexto.Set<TEntidad>().Where(predicado).CountAsync();

            return (resultado, total);
        }

        public async Task<TEntidad?> FindByIdAsync(int id)
        {
            return await Contexto.Set<TEntidad>().FindAsync(id);
        }

        public async Task<TEntidad> AddAsync(TEntidad entidad)
        {
            var resultado = await Contexto.Set<TEntidad>().AddAsync(entidad);
            await Contexto.SaveChangesAsync();
            return resultado.Entity;
        }

        public async Task UpdateAsync()
        {
            await Contexto.SaveChangesAsync();
        }
       
    }
}
