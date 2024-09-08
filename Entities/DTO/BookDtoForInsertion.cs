using System.ComponentModel.DataAnnotations;

namespace Entities.DTO
{
    public record BookDtoForInsertion : BookDtoForManipulation
    {
        [Required(ErrorMessage = "CategoryId is required.")]
        public int CategoryId { get; init; }
    }
}
