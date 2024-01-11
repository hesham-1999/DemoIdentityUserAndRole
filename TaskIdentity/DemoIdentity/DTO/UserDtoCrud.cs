namespace DemoIdentity.DTO
{
    public class UserDtoCrud
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public IFormFile Image { get; set; }
    }
}
