using Project.Infrastructure.Abstracts;


namespace Project.Infrastructure.Concretes
{
    public class Pageable : IPageable
    {
        public int Page { get; set; }
        public virtual int Size { get; set; }
    }
}
