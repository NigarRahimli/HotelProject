    using Project.Infrastructure.Concretes;

    namespace Project.Domain.Models.Entities
    {
        public class Property : AuditableEntity
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public int GuestNum { get; set; }
            public int DescriptionId { get; set; }
            public int LocationId { get; set; }
            public bool IsPetFriendly { get; set; }
            public float LongPrice { get; set; }
            public float MedPrice { get; set; }
            public float MinPrice { get; set; }
            public int KindId { get; set; }
            public double Rate { get; set; }

    }
    }
