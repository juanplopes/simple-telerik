// (c) Copyright 2002-2010 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

namespace Telerik.Web.Mvc.Extensions
{
    using System;
    using System.Text.RegularExpressions;
    using System.Linq.Expressions;

    public static class ExpressionExtensions
    {
        public static string MemberWithoutInstance(this LambdaExpression expression)
        {
            MemberExpression memberExpression = expression.ToMemberExpression();

            if (memberExpression == null)
            {
                return null;
            }

            if (memberExpression.Expression.NodeType == ExpressionType.MemberAccess)
            {
                MemberExpression innerMemberExpression = (MemberExpression)memberExpression.Expression;
                
                while (innerMemberExpression.Expression.NodeType == ExpressionType.MemberAccess)
                {
                    innerMemberExpression = (MemberExpression)innerMemberExpression.Expression;
                }

                ParameterExpression parameterExpression = (ParameterExpression)innerMemberExpression.Expression;

                return memberExpression.ToString().Substring(parameterExpression.Name.ToString().Length + 1);
            }

            return memberExpression.Member.Name;
        }

        public static MemberExpression ToMemberExpression(this LambdaExpression expression)
        {
            MemberExpression memberExpression = expression.Body as MemberExpression;

            if (memberExpression == null)
            {
                UnaryExpression unaryExpression = expression.Body as UnaryExpression;

                if (unaryExpression != null)
                {
                    memberExpression = unaryExpression.Operand as MemberExpression;
                }
            }

            return memberExpression;
        }
    }
}
