using Microsoft.EntityFrameworkCore;
using RopaStore.Application.DTOs.Producto;
using RopaStore.Application.Interfaces;
using RopaStore.Domain.Entities;
using RopaStore.Infrastructure.Data;

namespace RopaStore.Infrastructure.Services
{
    public class ProductoService : IProductoService
    {
        private readonly RopaStoreDbContext _context;

        public ProductoService(RopaStoreDbContext context)
        {
            _context = context;
        }

        public async Task<List<ProductoDto>> ListarAsync()
        {
            return await _context.Productos
                .Select(p => new ProductoDto
                {
                    Id = p.Id,
                    Codigo = p.Codigo,
                    Nombre = p.Nombre,
                    Precio = p.Precio,
                    Marca = p.Marca,
                    Stock = p.Stock
                })
                .ToListAsync();
        }

        public async Task<bool> CrearAsync(CrearProductoRequest request)
        {
            var producto = new Producto
            {
                Id = Guid.NewGuid(),
                Codigo = request.Codigo,
                Nombre = request.Nombre,
                Descripcion = request.Descripcion,
                ImagenUrl = request.ImagenUrl,
                Talla = request.Talla,
                Color = request.Color,
                Precio = request.Precio,
                Marca = request.Marca,
                Stock = request.Stock,
                SubcategoriaId = request.SubcategoriaId
            };

            _context.Productos.Add(producto);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ActualizarAsync(ActualizarProductoRequest request)
        {
            var producto = await _context.Productos.FindAsync(request.Id);
            if (producto == null) return false;

            producto.Codigo = request.Codigo;
            producto.Nombre = request.Nombre;
            producto.Descripcion = request.Descripcion;
            producto.ImagenUrl = request.ImagenUrl;
            producto.Talla = request.Talla;
            producto.Color = request.Color;
            producto.Precio = request.Precio;
            producto.Marca = request.Marca;
            producto.Stock = request.Stock;
            producto.SubcategoriaId = request.SubcategoriaId;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> EliminarAsync(Guid id)
        {
            var producto = await _context.Productos.FindAsync(id);
            if (producto == null) return false;

            _context.Productos.Remove(producto);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
