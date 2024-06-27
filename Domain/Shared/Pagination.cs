namespace Domain.Shared;

public class Pagination<T> : List<T> where T : class
{
    public int PageIndex { get; set; }
    public int PageTotal { get; set; }
    public Pagination(List<T> list, int count, int pageIndex, int pageSize)
    {
        PageIndex = pageIndex;
        PageTotal = (int)Math.Ceiling(count / (double)pageSize);
        this.AddRange(list);
    }

    public bool HasPreviousPage => PageIndex > 1;
    public bool HasNextPage => PageIndex < PageTotal;

    public static async Task<Pagination<T>> Get(IQueryable<T> srouce, int pageIndex, int pageSize)
    {
        var list = await Task.Run(() => srouce.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList());
        var count = list.Count;
        return new Pagination<T>(list, count, pageIndex, pageSize);
    }
}