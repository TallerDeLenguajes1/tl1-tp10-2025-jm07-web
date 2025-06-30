namespace Usuarios
{
    public class Geo
    {
        public string? Lat { get; set; }
        public string? Lng { get; set; }
    }

    public class Address
    {
        public string? Street { get; set; }
        public string? Suite { get; set; }
        public string? City { get; set; }
        public string? Zipcode { get; set; }
        public Geo? Geo { get; set; }

        // Para mostrar el domicilio de forma legible
        public override string ToString()
        {
            return $"{Street}, {Suite}, {City}, {Zipcode}";
        }
    }

    public class Usuario
    {
        public int? Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public Address? Address { get; set; }
    }
}