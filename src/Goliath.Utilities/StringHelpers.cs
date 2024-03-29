﻿using System;
using System.Linq.Expressions;

namespace Goliath
{
    /// <summary>
    /// string helper utility class
    /// </summary>
    public static class StringHelpers
    {
        internal const int MaxLength = 16;

        /// <summary>
        /// Creates the name of the database constraint.
        /// </summary>
        /// <param name="tableLeft">The table left.</param>
        /// <param name="leftAbbr">The left abbr.</param>
        /// <param name="column">The column.</param>
        /// <param name="postfix">The postfix.</param>
        /// <returns></returns>
        public static string CreateDatabaseConstraintName(string tableLeft, string leftAbbr, string column, string postfix)
        {
            if (string.IsNullOrWhiteSpace(tableLeft))
                throw new ArgumentNullException("tableLeft");

            if (string.IsNullOrWhiteSpace(column))
                throw new ArgumentNullException("column");

            if (string.IsNullOrWhiteSpace(postfix))
                throw new ArgumentNullException("postfix");

            if (string.IsNullOrWhiteSpace(column))
                column = "_";

            if (postfix.Length > 3)
                postfix = postfix.Substring(0, 3).ToUpper();
            else
                postfix = postfix.ToUpper();

            string finalName = string.Format("{0}{1}{2}", tableLeft, column, postfix);
            if (finalName.Length > 30)
            {
                int length = (MaxLength / 2);
                string leftName = GetTableName(tableLeft, leftAbbr, length);
                string rightName = GetTableName(column, column, length);
                finalName = string.Format("{0}{1}{2}", leftName, rightName, postfix);
            }

            return finalName;

        }

        /// <summary>
        /// Gets the name of the table.
        /// </summary>
        /// <param name="tableName">Name of the table.</param>
        /// <param name="abbr">The abbr.</param>
        /// <param name="length">The length.</param>
        /// <returns></returns>
        internal static string GetTableName(string tableName, string abbr, int length)
        {
            if (!string.IsNullOrWhiteSpace(tableName))
            {
                string leftName = string.Empty;
                if (tableName.Length > length)
                {
                    leftName = tableName.Substring(0, (length / 2));
                    if (string.IsNullOrWhiteSpace(abbr))
                        abbr = Guid.NewGuid()
                            .ToString()
                            .Replace("-", string.Empty)
                            .Substring(0, 5);
                    else if (abbr.Length > 5)
                        abbr = abbr.Substring(0, 5);
                    leftName = leftName + abbr;
                }
                return leftName;
            }
            else
                return string.Empty;
        }

        /// <summary>
        /// Gets the name of the member.
        /// </summary>
        /// <typeparam name="TProperty">The type of the property.</typeparam>
        /// <param name="property">The property.</param>
        /// <returns></returns>
        public static string GetMemberName<TProperty>(this Expression<Func<TProperty>> property)
        {
            var lambda = (LambdaExpression)property;

            MemberExpression memberExpression;
            if (lambda.Body is UnaryExpression)
            {
                var unaryExpression = (UnaryExpression)lambda.Body;
                memberExpression = (MemberExpression)unaryExpression.Operand;
            }
            else memberExpression = (MemberExpression)lambda.Body;

            return memberExpression.Member.Name;
        }


        public static string GetMemberName<T,TProperty>(this Expression<Func<T, TProperty>> property)
        {
            var lambda = (LambdaExpression)property;

            MemberExpression memberExpression;
            if (lambda.Body is UnaryExpression)
            {
                var unaryExpression = (UnaryExpression)lambda.Body;
                memberExpression = (MemberExpression)unaryExpression.Operand;
            }
            else memberExpression = (MemberExpression)lambda.Body;

            return memberExpression.Member.Name;
        }
    }

   
}
