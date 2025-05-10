using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Logging;
using Tuya.CreditCard.Api.DAL.Contracts;
using Tuya.CreditCard.Api.DAL.Contracts.Entities;

namespace Tuya.CreditCard.Api.DAL
{
    public class CreditCardContext : DbContext, ICreditCardContext
    {
        private readonly DatabaseFacade _database;
        private readonly ILoggerFactory _loggerFactory;

        public DatabaseFacade DataBase => _database;

        public CreditCardContext()
        {
            _database = base.Database;
        }

        public CreditCardContext(DbContextOptions<CreditCardContext> options, ILoggerFactory loggerFactory) : base(options)
        {
            _database = base.Database;
            _loggerFactory = loggerFactory ?? throw new ArgumentNullException(nameof(loggerFactory));
        }

        public virtual DbSet<UserEntity> Users { get; set; }
        public virtual DbSet<CardEntity> Cards { get; set; }
        public virtual DbSet<ProductEntity> Products { get; set; }
        public virtual DbSet<SaleEntity> Sales { get; set; }
        public virtual DbSet<SaleDetailEntity> SaleDetails { get; set; }
        public virtual DbSet<TransactionEntity> Transactions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("ApiConnection");
            }

            if (_loggerFactory != null)
            {
                optionsBuilder.UseLoggerFactory(_loggerFactory);
            }

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //INDEXES
            modelBuilder.Entity<UserEntity>().HasIndex(x => x.UserName).IsUnique(true).HasDatabaseName("IDX_USERNAME_USER_ENTITY");
            modelBuilder.Entity<ProductEntity>().HasIndex(x => x.Reference).IsUnique(true).HasDatabaseName("IDX_REFERENCE_PRODUCT_ENTITY");
            modelBuilder.Entity<TransactionEntity>().HasIndex(x => x.TransactionReference).IsUnique(true).HasDatabaseName("IDX_REFERENCE_TRANSACTION_ENTITY");

            //Disable cascading deletion
            var cascadeFKs = modelBuilder.Model.GetEntityTypes().SelectMany(
                 t => t.GetForeignKeys()).Where(fk => !fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade
             );

            foreach (var fk in cascadeFKs)
            {
                fk.DeleteBehavior = DeleteBehavior.Restrict;
            }

            base.OnModelCreating(modelBuilder);
        }
    }
}
