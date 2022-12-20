using System.ComponentModel.DataAnnotations;

namespace _Utils.Contracts.v1
{
    public interface IEntity
    {
        [Key]
        int Id { get; }
    }
}
