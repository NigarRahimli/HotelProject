using Microsoft.EntityFrameworkCore;
using Project.Infrastructure.Abstracts;
using Project.Infrastructure.Concretes;


namespace Project.Infrastructure.Extensions
{
    static public partial class Extension
    {

        public static async Task<IPaginate<T>> ToPaginateAsync<T>(this IOrderedQueryable<T> query, IPageable pageable, CancellationToken cancellation = default)
            where T : class
        {
            var count = await query.CountAsync(cancellation);

            var response = new Paginate<T>(pageable, count);

            response.Items = await query.Skip((pageable.Page - 1) * pageable.Size).Take(pageable.Size).ToListAsync(cancellation);

            return response;
        }
    }
}
