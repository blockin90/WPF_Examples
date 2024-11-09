namespace CRUD.Model
{
    using System;
    using System.Data.Entity;
    using System.Linq;


    public class UniversityModel : DbContext
    {
        public virtual DbSet<Group> Groups { get; set; }
        public virtual DbSet<Student> Students { get; set; }
        public virtual DbSet<UniversityComponent> UniversityComponents { get; set; }
        public virtual DbSet<CRUD> CRUDs { get; set; }
        public virtual DbSet<User> Users { get; set; }

        static UniversityModel()
        {
            Database.SetInitializer(new ModelInitializer());
        }
        public UniversityModel()
            : base("name=UniversityModel")
        {
            
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CRUD>().HasKey((crud) => new { crud.UniversityComponentId, crud.UserId });
            base.OnModelCreating(modelBuilder);
        }
    }
}