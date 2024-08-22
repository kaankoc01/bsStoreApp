namespace Entities.DTO
{
    //   [Serializable]
    public record BookDto()
    {
        public int Id { get; init; }
        public string Title { get; init; }
        public decimal Price { get; init; }
    }
}
