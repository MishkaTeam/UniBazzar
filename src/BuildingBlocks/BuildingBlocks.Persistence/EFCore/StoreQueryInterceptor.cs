using System.Linq.Expressions;
using BuildingBlocks.Domain.SeedWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace BuildingBlocks.Persistence.EFCore;

public class StoreQueryInterceptor : IQueryExpressionInterceptor
{
    private readonly IExecutionContextAccessor _context;

    public StoreQueryInterceptor(IExecutionContextAccessor context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public Expression QueryCompilationStarting(
        Expression queryExpression,
        QueryExpressionEventData eventData)
    {
        var visitor = new StoreFilterExpressionVisitor(_context);
        return visitor.Visit(queryExpression);
    }

}

public class StoreFilterExpressionVisitor : ExpressionVisitor
{
    private readonly IExecutionContextAccessor _context;
    private static readonly Type _interfaceType = typeof(IEntityHasStore);

    public StoreFilterExpressionVisitor(IExecutionContextAccessor context)
    {
        _context = context;
    }

    protected override Expression VisitMethodCall(MethodCallExpression node)
    {
        if (node.Method.DeclaringType == typeof(Queryable))
        {
            var genericArgs = node.Method.GetGenericArguments();
            if (genericArgs.Length == 1)
            {
                var entityType = genericArgs[0];

                if (_interfaceType.IsAssignableFrom(entityType))
                {
                    if (!typeof(IQueryable).IsAssignableFrom(node.Type))
                        return base.VisitMethodCall(node);

                    if (node.Method.Name == "Where" && node.Arguments.Count == 2)
                    {
                        if (node.Arguments[1] is UnaryExpression unary &&
                            unary.Operand is LambdaExpression whereLambda &&
                            whereLambda.Body.ToString().Contains("StoreId"))
                        {
                            return base.VisitMethodCall(node);
                        }
                    }

                    var parameter = Expression.Parameter(entityType, "e");
                    var storeIdProperty = Expression.Property(parameter, nameof(IEntityHasStore.StoreId));
                    var storeIdValue = Expression.Constant(_context.StoreId);
                    var equal = Expression.Equal(storeIdProperty, storeIdValue);
                    var storeIdFilterLambda = Expression.Lambda(equal, parameter);

                    // Where<TEntity>(IQueryable<TEntity>, Expression<Func<TEntity, bool>>)
                    var whereMethod = typeof(Queryable)
                        .GetMethods()
                        .First(m => m.Name == "Where" && m.GetParameters().Length == 2)
                        .MakeGenericMethod(entityType);

                    var filtered = Expression.Call(whereMethod, node, storeIdFilterLambda);

                    return filtered;
                }
            }
        }

        return base.VisitMethodCall(node);
    }
}
