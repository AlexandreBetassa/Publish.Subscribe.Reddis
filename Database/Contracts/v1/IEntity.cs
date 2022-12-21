using System.ComponentModel.DataAnnotations;

namespace DatabaseAPI.Contracts.v1
{
    public interface IEntity
    {
        [Key]
        int Id { get; set; }
    }
}
