    namespace Project.Infrastructure.Abstracts
    {
        public interface IPageable
        {
            int Page { get; set; }
            int Size { get; set; }
        }
    }
