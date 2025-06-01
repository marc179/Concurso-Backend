using Microsoft.EntityFrameworkCore;

using RopaStore.Domain.Entities;
 
namespace RopaStore.Infrastructure.Data

{

    public class RopaStoreDbContext : DbContext

    {

        public RopaStoreDbContext(DbContextOptions<RopaStoreDbContext> options)

            : base(options)

        {

        }
 
        public DbSet<Producto> Productos => Set<Producto>();

        public DbSet<Categoria> Categorias => Set<Categoria>();

        public DbSet<Subcategoria> Subcategorias => Set<Subcategoria>();

        public DbSet<Usuario> Usuarios => Set<Usuario>();

        public DbSet<Rol> Roles => Set<Rol>();

        public DbSet<Pedido> Pedidos => Set<Pedido>();

        public DbSet<DetallePedido> DetallesPedido => Set<DetallePedido>();

        public DbSet<Factura> Facturas => Set<Factura>();

        public DbSet<HistorialProductoCliente> HistorialProductos => Set<HistorialProductoCliente>();
 
        protected override void OnModelCreating(ModelBuilder modelBuilder)

        {

            base.OnModelCreating(modelBuilder);
 
            // Relaciones y restricciones

            modelBuilder.Entity<Rol>()

                .HasMany(r => r.Usuarios)

                .WithOne(u => u.Rol)

                .HasForeignKey(u => u.RolId);
 
            modelBuilder.Entity<Categoria>()

                .HasMany(c => c.Subcategorias)

                .WithOne(sc => sc.Categoria)

                .HasForeignKey(sc => sc.CategoriaId);
 
            modelBuilder.Entity<Subcategoria>()

                .HasMany(sc => sc.Productos)

                .WithOne(p => p.Subcategoria)

                .HasForeignKey(p => p.SubcategoriaId);
 
            modelBuilder.Entity<Usuario>()

                .HasMany(u => u.Pedidos)

                .WithOne(p => p.Usuario)

                .HasForeignKey(p => p.UsuarioId);
 
            modelBuilder.Entity<Pedido>()

                .HasMany(p => p.Detalles)

                .WithOne(d => d.Pedido)

                .HasForeignKey(d => d.PedidoId);
 
            modelBuilder.Entity<Pedido>()

                .HasOne(p => p.Factura)

                .WithOne(f => f.Pedido)

                .HasForeignKey<Factura>(f => f.PedidoId);
 
            modelBuilder.Entity<DetallePedido>()

                .HasOne(d => d.Producto)

                .WithMany()

                .HasForeignKey(d => d.ProductoId);
 
            modelBuilder.Entity<HistorialProductoCliente>()

                .HasOne(h => h.Usuario)

                .WithMany(u => u.Historial)

                .HasForeignKey(h => h.UsuarioId);
 
            modelBuilder.Entity<HistorialProductoCliente>()

                .HasOne(h => h.Producto)

                .WithMany()

                .HasForeignKey(h => h.ProductoId);

        }

    }

}

 
 