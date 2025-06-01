using Microsoft.EntityFrameworkCore;
using RopaStore.Application.DTOs.Categoria;
using RopaStore.Application.Interfaces;
using RopaStore.Domain.Entities;
using RopaStore.Infrastructure.Data;

namespace RopaStore.Infrastructure.Services
{
    public class CategoriaService : ICategoriaService
    {
        private readonly RopaStoreDbContext _context;

        public CategoriaService(RopaStoreDbContext context)
        {
            _context = context;
        }

        public async Task<List<CategoriaDto>> ListarAsync()
        {
            return await _context.Categorias
                .Select(c => new CategoriaDto
                {
                    Id = c.Id,
                    Nombre = c.Nombre
                })
                .ToListAsync();
        }

        public async Task<bool> CrearAsync(CrearCategoriaRequest request)
        {
            var existe = await _context.Categorias.AnyAsync(c => c.Nombre == request.Nombre);
            if (existe) return false;

            var categoria = new Categoria
            {
                Id = Guid.NewGuid(),
                Nombre = request.Nombre
            };

            _context.Categorias.Add(categoria);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ActualizarAsync(Guid id, CrearCategoriaRequest request)
        {
            var categoria = await _context.Categorias.FindAsync(id);
            if (categoria == null) return false;

            categoria.Nombre = request.Nombre;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> EliminarAsync(Guid id)
        {
            var categoria = await _context.Categorias
                .Include(c => c.Subcategorias)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (categoria == null || categoria.Subcategorias.Any())
                return false;

            _context.Categorias.Remove(categoria);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
