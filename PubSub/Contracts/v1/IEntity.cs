using System.ComponentModel.DataAnnotations;

namespace PubSubApi.Contracts.v1
{
    public interface IEntity
    {
        [Key]
        int Id { get; }
    }
}
