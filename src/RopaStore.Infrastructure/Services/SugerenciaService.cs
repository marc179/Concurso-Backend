using Microsoft.EntityFrameworkCore;
using RopaStore.Application.DTOs.Sugerencia;
using RopaStore.Application.Interfaces;
using RopaStore.Infrastructure.Data;

namespace RopaStore.Infrastructure.Services
{
    public class SugerenciaService : ISugerenciaService
    {
        private readonly RopaStoreDbContext _context;

        public SugerenciaService(RopaStoreDbContext context)
        {
            _context = context;
        }

        public async Task<List<ProductoSugeridoDto>> ObtenerSugerenciasParaClienteAsync(Guid usuarioId)
        {
            // Obtén subcategorías más compradas por el cliente
            var subcategoriasFavoritas = await _context.HistorialProductos
                .Where(h => h.UsuarioId == usuarioId && h.Accion == "comprado")
                .Join(_context.Productos,
                      h => h.ProductoId,
                      p => p.Id,
                      (h, p) => p.SubcategoriaId)
                .GroupBy(s => s)
                .OrderByDescending(g => g.Count())
                .Select(g => g.Key)
                .Take(2)
                .ToListAsync();

            // Sugerir productos de esas subcategorías que aún no ha comprado
            var yaComprados = await _context.HistorialProductos
                .Where(h => h.UsuarioId == usuarioId)
                .Select(h => h.ProductoId)
                .ToListAsync();

            var sugeridos = await _context.Productos
                .Where(p => subcategoriasFavoritas.Contains(p.SubcategoriaId) && !yaComprados.Contains(p.Id))
                .Select(p => new ProductoSugeridoDto
                {
                    Id = p.Id,
                    Nombre = p.Nombre,
                    Precio = p.Precio,
                    Marca = p.Marca,
                    ImagenUrl = p.ImagenUrl
                })
                .Take(10)
                .ToListAsync();

            return sugeridos;
        }
    }
}
