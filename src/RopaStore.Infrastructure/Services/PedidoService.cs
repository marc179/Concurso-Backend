using Microsoft.EntityFrameworkCore;
using RopaStore.Application.DTOs.Pedido;
using RopaStore.Application.Interfaces;
using RopaStore.Domain.Entities;
using RopaStore.Infrastructure.Data;

public class PedidoService : IPedidoService
{
    private readonly RopaStoreDbContext _context;
    public PedidoService(RopaStoreDbContext context)
    {
        _context = context;
    }

    public async Task<bool> CrearPedidoAsync(CrearPedidoRequest request)
    {
        var usuario = await _context.Usuarios.FindAsync(request.UsuarioId);
        if (usuario == null) return false;

        var pedido = new Pedido
        {
            Id = Guid.NewGuid(),
            UsuarioId = usuario.Id,
            Fecha = DateTime.UtcNow
        };

        decimal subtotal = 0m;

        foreach (var item in request.Productos)
        {
            var producto = await _context.Productos.FindAsync(item.ProductoId);
            if (producto == null || producto.Stock < item.Cantidad)
                return false;

            producto.Stock -= item.Cantidad;

            pedido.Detalles.Add(new DetallePedido
            {
                Id = Guid.NewGuid(),
                ProductoId = producto.Id,
                Cantidad = item.Cantidad,
                PrecioUnitario = producto.Precio
            });

            subtotal += producto.Precio * item.Cantidad;

            // registrar historial
            _context.HistorialProductos.Add(new HistorialProductoCliente
            {
                Id = Guid.NewGuid(),
                UsuarioId = usuario.Id,
                ProductoId = producto.Id,
                Accion = "comprado",
                Calificacion = 0
            });
        }

        decimal descuento = CalcularDescuento(subtotal, out string categoriaCliente);
        decimal total = subtotal - (subtotal * descuento);

        pedido.Factura = new Factura
        {
            Id = Guid.NewGuid(),
            Subtotal = subtotal,
            Descuento = descuento,
            Total = total,
            CategoriaCliente = categoriaCliente
        };

        _context.Pedidos.Add(pedido);
        await _context.SaveChangesAsync();

        return true;
    }

    private decimal CalcularDescuento(decimal subtotal, out string categoria)
    {
        if (subtotal >= 500) { categoria = "Oro"; return 0.15m; }
        if (subtotal >= 200) { categoria = "Plata"; return 0.10m; }
        categoria = "Cobre"; return 0.05m;
    }

    public async Task<List<PedidoDto>> ListarPedidosPorClienteAsync(Guid usuarioId)
    {
        return await _context.Pedidos
            .Include(p => p.Factura)
            .Include(p => p.Detalles).ThenInclude(d => d.Producto)
            .Include(p => p.Usuario)
            .Where(p => p.UsuarioId == usuarioId)
            .Select(p => new PedidoDto
            {
                PedidoId = p.Id,
                Fecha = p.Fecha,
                Cliente = p.Usuario.NombreCompleto,
                Productos = p.Detalles.Select(d => d.Producto.Nombre).ToList(),
                Subtotal = p.Factura!.Subtotal,
                Descuento = p.Factura!.Descuento,
                Total = p.Factura!.Total,
                CategoriaCliente = p.Factura!.CategoriaCliente
            })
            .ToListAsync();
    }

    public async Task<List<PedidoDto>> ListarTodosAsync()
    {
        return await _context.Pedidos
            .Include(p => p.Factura)
            .Include(p => p.Detalles).ThenInclude(d => d.Producto)
            .Include(p => p.Usuario)
            .Select(p => new PedidoDto
            {
                PedidoId = p.Id,
                Fecha = p.Fecha,
                Cliente = p.Usuario.NombreCompleto,
                Productos = p.Detalles.Select(d => d.Producto.Nombre).ToList(),
                Subtotal = p.Factura!.Subtotal,
                Descuento = p.Factura!.Descuento,
                Total = p.Factura!.Total,
                CategoriaCliente = p.Factura!.CategoriaCliente
            })
            .ToListAsync();
    }
}
