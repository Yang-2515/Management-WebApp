using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WebApp.API.Services.Tasks;
using WebApp.API.Services.Users;
using WebApp.Domain;
using WebApp.Domain.Boards;
using WebApp.Domain.Interfaces;
using WebApp.Domain.IRepositories;
using WebApp.Domain.Labels;
using WebApp.Domain.Tasks;
using WebApp.Domain.ToDos;
using WebApp.Domain.Users;
using WebApp.Infragtructure;
using WebApp.Infragtructure.Repositories;
using WebApp.Service;
using AppContext = WebApp.Infragtructure.AppContext;

namespace WebApp.API.Extensions
{
	public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Add needed instances for database
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddDatabase(this IServiceCollection services)
        {
            // Configure DbContext with Scoped lifetime   
            services.AddDbContext<AppContext>(options =>
                {
                    options.UseSqlServer(AppSettings.ConnectionString,
                        sqlOptions => sqlOptions.CommandTimeout(120));
                    options.UseLazyLoadingProxies();
                }
            );

            services.AddScoped<Func<AppContext>>((provider) => () => provider.GetService<AppContext>());
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            return services
                .AddScoped(typeof(IAsyncRepository<>), typeof(RepositoryBase<>))
                .AddScoped<IBoardRepository, BoardRepository>()
                .AddScoped<IListTaskRepository, ListTaskRepository>()
                .AddScoped<IUserRepository, UserRepository>()
                .AddScoped<IRefreshToken, RefreshTokenRepository>()
                .AddScoped<IBoardMemberRepository, BoardMemberRepository>()
                .AddScoped<IToDoRepository, ToDoRepository>()
                .AddScoped<ITaskRepository, TaskRepository>()
                .AddScoped<ITaskLabelRepository, TaskLabelRepository>()
                .AddScoped<ILabelRepository, LabelRepository>()
                .AddScoped<ITaskMemberRepository, TaskMemberRepository>()
                .AddScoped<IAttackmentRepository, AttackmentRepository>();
        }
        public static IServiceCollection AddManager(this IServiceCollection services)
        {
            return services
                .AddTransient<IBoardManager, BoardManager>()
                .AddTransient<ITaskManager, TaskManager>()
                .AddTransient<IUserManager, UserManager>();
        }

        /// <summary>
        /// Add instances of in-use services
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddBusinessServices(this IServiceCollection services
           )
        {
            return services
                .AddScoped<BoardService>()
                .AddScoped<UserService>()
                .AddScoped<IdentityService>()
                .AddScoped<TaskService>();
        }
        /*        public static IServiceCollection AddServices(this IServiceCollection services)
                {
                    return services.AddScoped<ILabelService, LabelService>()
                        .AddScoped<IUserService, UserService>()
                        .AddScoped<ITaskLabelService, TaskLabelService>()
                        .AddScoped<ITaskService, TaskService>()
                        .AddScoped<IBoardService, BoardService>()
                        .AddScoped<IBoardMemberService, BoardMemberService>()
                        .AddScoped<IListTaskService, ListTaskService>()
                        .AddScoped<IAssignmentService, AssignmentService>()
                        .AddScoped<IAttackmentService, AttackmentService>();
                }*/

        /// <summary>
        /// Add CORS policy to allow external accesses
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        /*        public static IServiceCollection AddCORS(this IServiceCollection services)
                {
                    return // CORS
                        services.AddCors(options => {
                            options.AddPolicy("CorsPolicy",
                                builder => {
                                    builder.WithOrigins(AppSettings.CORS)
                                        .AllowAnyMethod()
                                        .AllowAnyHeader()
                                        .AllowCredentials();
                                });
                        });
                }*/
    }
}
