namespace RopaStore.Domain.Entities
{
    public class Rol
    {
        public Guid Id { get; set; }
        public string Nombre { get; set; } = null!;
 
        public ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
    }
}
