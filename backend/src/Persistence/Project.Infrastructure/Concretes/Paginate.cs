using Project.Infrastructure.Abstracts;


namespace Project.Infrastructure.Concretes
{
    public class Paginate<T> : IPaginate<T>
           where T : class
    {
        public IEnumerable<T> Items { get;  set; }

        public int Page { get; set; }

        public int Size { get; set; }

        public int Count { get; internal set; }

        public int Pages
        {
            get
            {
                return (int)Math.Ceiling(this.Count * 1D / this.Size);
            }
        }

        public bool HasPrevious { get => this.Page > 1; }
        public bool HasNext { get => this.Page < this.Pages; }

        public Paginate()
        {

        }
        public Paginate(IPageable pageable, int count)
        {
            this.Count = count;
            this.Size = pageable.Size;

            if (pageable.Page <= this.Pages)
            {
                this.Page = pageable.Page;
            }
            else
            {
                this.Page = this.Pages;

                this.Page = this.Page < 1 ? 1 : this.Page;
            }
        }
    }
}
