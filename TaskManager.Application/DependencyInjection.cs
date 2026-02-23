using Microsoft.Extensions.DependencyInjection;
using TaskManager.Application.Handlers;
using TaskManager.Application.Queries;

namespace TaskManager.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<CreateTodoTaskHandler>();
        services.AddScoped<StartTodoTaskHandler>();
        services.AddScoped<CompleteTodoTaskHandler>();
        services.AddScoped<CancelTodoTaskHandler>();

        services.AddScoped<CreateCategoryHandler>();
        services.AddScoped<UpdateCategoryHandler>();

        services.AddScoped<GetAllTodoTasksQueryHandler>();
        services.AddScoped<GetTodoTaskByIdQueryHandler>();

        services.AddScoped<GetCategoryByIdQueryHandler>();
        services.AddScoped<GetAllCategoriesQueryHandler>();

        return services;
    }
}
