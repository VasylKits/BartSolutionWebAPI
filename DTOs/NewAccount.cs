using BartsolutionsWebAPI.Models;

namespace BartsolutionsWebAPI.DTOs
{
    public class NewAccount
    {
        public string Name { get; set; }
        public Guid ContactId { get; set; }
    }
}