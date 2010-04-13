using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;

namespace Simple.Web.Mvc.Telerik
{
    public class MapReduceDefinition<T>
    {
        public Expression<Func<IQueryable<T>, IQueryable<T>>> Map { get; protected set; }
        public Expression<Func<IQueryable<T>, IQueryable<T>>> Reduce { get; protected set; }

        public MapReduceDefinition(Expression<Func<IQueryable<T>, IQueryable<T>>> map, Expression<Func<IQueryable<T>, IQueryable<T>>> reduce)
        {
            this.Map = map;
            this.Reduce = reduce;
        }

        public override string ToString()
        {
            return string.Format("{0} [{1}]", Map.Body, Reduce.Body);

        }
    }
}
