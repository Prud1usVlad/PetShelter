//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PetShelter.Model
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.ModelConfiguration;

    public partial class DataContext : DbContext
    {
        public DataContext()
            : base("name=PetShelterEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Animals>()
                .HasMany(a => a.StateValues)
        }

        public virtual DbSet<Animals> Animals { get; set; }
        public virtual DbSet<Caretakers> Caretakers { get; set; }
        public virtual DbSet<Clients> Clients { get; set; }
        public virtual DbSet<Contracts> Contracts { get; set; }
        public virtual DbSet<Groups> Groups { get; set; }
        public virtual DbSet<InfoDepEmploees> InfoDepEmploees { get; set; }
        public virtual DbSet<Producers> Producers { get; set; }
        public virtual DbSet<Rooms> Rooms { get; set; }
        public virtual DbSet<States> States { get; set; }
        public virtual DbSet<StateValues> StateValues { get; set; }
        public virtual DbSet<Vaccinations> Vaccinations { get; set; }
        public virtual DbSet<Vaccines> Vaccines { get; set; }
    }
}
