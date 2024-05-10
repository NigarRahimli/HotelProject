using Microsoft.EntityFrameworkCore;


namespace Project.DataAccessLayer.Contexts
{
     class DataContext:DbContext
    {
        public DataContext(DbContextOptions options) :base(options)
        {
            
        }
      
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(DataContext).Assembly);
        }
        
    }
   
}
