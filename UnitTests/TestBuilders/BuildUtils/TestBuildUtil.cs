using Moq;
using System.Linq.Expressions;

namespace UnitTests.TestBuilders.BuildUtils;
public static class TestBuildUtil
{
    public static Expression<Func<TEntity, bool>> BuildExpressionFunc<TEntity>()
        where TEntity : class
        =>
        It.IsAny<Expression<Func<TEntity, bool>>>();
}
