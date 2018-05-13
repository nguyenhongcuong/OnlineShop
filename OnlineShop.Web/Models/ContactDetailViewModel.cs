namespace OnlineShop.Web.Models
{
    public class ContactDetailViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Website { get; set; }
        public string Address { get; set; }
        public string Other { get; set; }
        public bool Status { get; set; }
        public double? Lat { get; set; }
        public double? Lng { get; set; }
    }
}