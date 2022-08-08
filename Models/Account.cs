using ServiceStack.DataAnnotations;

namespace BartsolutionsWebAPI.Models
{
    public class Account
    {
        [Unique]
        public string Name { get; set; }

        public string? IncidentName { get; set; }
        public Incident Incident { get; set; }

        public ICollection<Contact> Contacts { get; set; }
    }
}