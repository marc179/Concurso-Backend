using Microsoft.EntityFrameworkCore;
using RopaStore.Application.DTOs.Subcategoria;
using RopaStore.Application.Interfaces;
using RopaStore.Domain.Entities;
using RopaStore.Infrastructure.Data;

namespace RopaStore.Infrastructure.Services
{
    public class SubcategoriaService : ISubcategoriaService
    {
        private readonly RopaStoreDbContext _context;

        public SubcategoriaService(RopaStoreDbContext context)
        {
            _context = context;
        }

        public async Task<List<SubcategoriaDto>> ListarTodasAsync()
        {
            return await _context.Subcategorias
                .Include(sc => sc.Categoria)
                .Select(sc => new SubcategoriaDto
                {
                    Id = sc.Id,
                    Nombre = sc.Nombre,
                    CategoriaId = sc.CategoriaId,
                    CategoriaNombre = sc.Categoria.Nombre
                })
                .ToListAsync();
        }

        public async Task<List<SubcategoriaDto>> ListarPorCategoriaAsync(Guid categoriaId)
        {
            return await _context.Subcategorias
                .Where(sc => sc.CategoriaId == categoriaId)
                .Include(sc => sc.Categoria)
                .Select(sc => new SubcategoriaDto
                {
                    Id = sc.Id,
                    Nombre = sc.Nombre,
                    CategoriaId = sc.CategoriaId,
                    CategoriaNombre = sc.Categoria.Nombre
                })
                .ToListAsync();
        }

        public async Task<bool> CrearAsync(CrearSubcategoriaRequest request)
        {
            var existe = await _context.Subcategorias
                .AnyAsync(sc => sc.Nombre == request.Nombre && sc.CategoriaId == request.CategoriaId);

            if (existe) return false;

            var subcategoria = new Subcategoria
            {
                Id = Guid.NewGuid(),
                Nombre = request.Nombre,
                CategoriaId = request.CategoriaId
            };

            _context.Subcategorias.Add(subcategoria);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ActualizarAsync(Guid id, CrearSubcategoriaRequest request)
        {
            var subcategoria = await _context.Subcategorias.FindAsync(id);
            if (subcategoria == null) return false;

            subcategoria.Nombre = request.Nombre;
            subcategoria.CategoriaId = request.CategoriaId;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> EliminarAsync(Guid id)
        {
            var subcategoria = await _context.Subcategorias
                .Include(sc => sc.Productos)
                .FirstOrDefaultAsync(sc => sc.Id == id);

            if (subcategoria == null || subcategoria.Productos.Any())
                return false;

            _context.Subcategorias.Remove(subcategoria);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
