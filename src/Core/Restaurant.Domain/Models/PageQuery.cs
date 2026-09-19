namespace Restaurant.Domain.Models
{
    public abstract record PageQuery
    {
        public string? Keyword { get; init; }
        public string SortField { get; set; } = "created_at";
        public bool IsAscending { get; set; } = true;
        public int Page { get; init; } = 1;
        public int PageSize { get; init; } = 12;
    }
}
