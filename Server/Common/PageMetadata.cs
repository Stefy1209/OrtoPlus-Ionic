namespace Common;

public class PageMetadata<TEntity> where TEntity : class
{
    public int TotalCount { get; set; }
    public IEnumerable<TEntity> Items { get; set; } = [];
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public int TotalPages => (int)Math.Ceiling((double)TotalCount / PageSize);
}
