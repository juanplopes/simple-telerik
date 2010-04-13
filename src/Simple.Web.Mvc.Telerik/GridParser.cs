using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using Telerik.Web.Mvc;

namespace Simple.Web.Mvc.Telerik
{
    public class GridParser
    {
        public static MapReduceDefinition<T> Parse<T>(GridCommand command, int pageSize)
        {
            var param = Expression.Parameter(typeof(IQueryable<T>), "q");
            
            int pageNumber = command.Page - 1;
            if (pageNumber < 0) pageNumber = 0;

            var mapLambda = CreateMapExpression<T>(param, command);
            var reduceLambda = CreateReduceExpression<T>(param, pageSize, pageNumber);

            return new MapReduceDefinition<T>(mapLambda, reduceLambda);
        }

        private static Expression<Func<IQueryable<T>, IQueryable<T>>> CreateReduceExpression<T>(ParameterExpression param, int pageSize, int pageNumber)
        {
            var reduce = param as Expression;
            reduce = new SkipAndTakeVisitor<T>().Visit(param, pageNumber, pageSize);
            var reduceLambda = Expression.Lambda<Func<IQueryable<T>, IQueryable<T>>>(reduce, param);
            return reduceLambda;
        }

        private static Expression<Func<IQueryable<T>, IQueryable<T>>> CreateMapExpression<T>(ParameterExpression param, GridCommand command)
        {
            var sortDescriptors = GetSortDescriptors(command);

            var map = param as Expression;
            map = new FilterVisitor<T>().Visit(map, command.FilterDescriptors);
            map = new SortVisitor<T, IDescriptor>().Visit(map, sortDescriptors);
            var mapLambda = Expression.Lambda<Func<IQueryable<T>, IQueryable<T>>>(map, param);
            return mapLambda;
        }

        private static List<IDescriptor> GetSortDescriptors(GridCommand command)
        {
            var hashSet = new HashSet<string>(command.GroupDescriptors.Select(x => x.Member));
            var sorts = command.SortDescriptors.Where(x => !hashSet.Contains(x.Member)).OfType<IDescriptor>();
            var sortDescriptors = command.GroupDescriptors.OfType<IDescriptor>().Union(sorts).ToList();
            return sortDescriptors;
        }
    }
}
