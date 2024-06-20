
namespace Project.Infrastructure.Abstracts
{
    public interface IPaginate<T>
          where T : class
    {
        int Page { get; }
        int Size { get; }
        int Count { get; }

        IEnumerable<T> Items { get; }

        public int Pages { get; }
        public bool HasPrevious { get; }
        public bool HasNext { get; }
    }
}
