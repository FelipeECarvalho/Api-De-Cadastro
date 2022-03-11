namespace CadastroApi.Models
{
    public class Address
    {
        public int Id { get; set; }
        public string ZipCode { get; set; }
        public string Street { get; set; }
        public string District { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public User User { get; set; }
    }
}
