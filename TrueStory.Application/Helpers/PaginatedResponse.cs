using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrueStory.Application.Helpers;


public class PaginatedResponse<T>
{
    public int Page { get; set; }
    public int PerPage { get; set; }
    public int TotalCount { get; set; }
    public int TotalPages { get; set; }
    public List<T> Items { get; set; } = new();
}
// helper generic class to handle pagination
public static class PaginationExtensions
{
    public static PaginatedResponse<T> ToPaginatedResponse<T>(
        this IEnumerable<T> query,
        int page,
        int perPage)
    {
        var totalCount = query.Count();
        var totalPages = (int)Math.Ceiling(totalCount / (double)perPage);

        var items = query
            .Skip((page - 1) * perPage)
            .Take(perPage)
            .ToList();

        return new PaginatedResponse<T>
        {
            Page = page,
            PerPage = perPage,
            TotalCount = totalCount,
            TotalPages = totalPages,
            Items = items
        };
    }
}


