using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Data;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Threading.Tasks;
using WebApp.Domain.Boards;
using WebApp.Domain.Histories;
using WebApp.Domain.Labels;
using WebApp.Domain.ListTasks;
using WebApp.Domain.Tasks;
using WebApp.Domain.ToDos;
using WebApp.Domain.Users;
using Task = WebApp.Domain.Tasks.Task;

namespace WebApp.Infragtructure
{
    public partial class AppContext : DbContext
    {
        //private readonly IMediator _mediator;
        private readonly IMediator _mediator;
        private IDbContextTransaction _currentTransaction;
        public IDbContextTransaction GetCurrentTransaction() => _currentTransaction;

        public bool HasActiveTransaction => _currentTransaction != null;

        public AppContext()
        {
        }

        public AppContext(DbContextOptions<AppContext> options)
            : base(options)
        {
        }
        /*public AppContext(DbContextOptions<AppContext> options, IMediator mediator) : base(options)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));


            System.Diagnostics.Debug.WriteLine("OrderingContext::ctor ->" + this.GetHashCode());
        }*/

        public virtual DbSet<TaskMember> Assignments { get; set; }
        public virtual DbSet<Attackment> Attackments { get; set; }
        public virtual DbSet<Board> Boards { get; set; }
        public virtual DbSet<BoardMember> BoardMembers { get; set; }
        public virtual DbSet<Label> Labels { get; set; }
        public virtual DbSet<ListTask> ListTasks { get; set; }
        public virtual DbSet<Task> Tasks { get; set; }
        public virtual DbSet<TaskLabel> TaskLabels { get; set; }
        public virtual DbSet<AppUser> Users { get; set; }
        public virtual DbSet<ToDo> ToDos { get; set; }
        public virtual DbSet<History> Histories { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"Server=localhost;Database=WebApp;Trusted_Connection=True;");
            }
        }
        public async Task<int> SaveEntitiesAsync()

        {

            // Dispatch Domain Events collection.

            // Choices:

            // A) Right BEFORE committing data (EF SaveChanges) into the DB will make a single transaction including

            // side effects from the domain event handlers which are using the same DbContext with Scope lifetime

            // B) Right AFTER committing data (EF SaveChanges) into the DB. will make multiple transactions.

            // You will need to handle eventual consistency and compensatory actions in case of failures.



            await _mediator.DispatchDomainEventsAsync(this);



            // After executing this line all the changes (from the Command Handler and Domain Event Handlers)

            // performed thought the DbContext will be commited

            var result = await base.SaveChangesAsync();
            return result;

        }
        public async Task<IDbContextTransaction> BeginTransactionAsync()
        {
            if (_currentTransaction != null) return null;

            _currentTransaction = await Database.BeginTransactionAsync(IsolationLevel.ReadCommitted);

            return _currentTransaction;
        }

        public void RollbackTransaction()
        {
            try
            {
                _currentTransaction?.Rollback();
            }
            finally
            {
                if (_currentTransaction != null)
                {
                    _currentTransaction.Dispose();
                    _currentTransaction = null;
                }
            }
        }
    }
}
