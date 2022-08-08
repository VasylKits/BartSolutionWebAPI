namespace BartsolutionsWebAPI.DTOs
{
    public class ResponseContact
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string? AccountName { get; set; }
    }
}