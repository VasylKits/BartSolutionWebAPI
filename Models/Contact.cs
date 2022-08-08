using System.ComponentModel.DataAnnotations;

namespace BartsolutionsWebAPI.Models
{
    public class Contact
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string? AccountName { get; set; }
        public Account Account { get; set; }

    }
}