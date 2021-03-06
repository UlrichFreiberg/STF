﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TableUtils.cs" company="Foobar">
//   2015
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace TableUtils
{
    using System.Collections.Generic;
    using System.Dynamic;

    using Slapper;

    /// <summary>
    /// The web table header description.
    /// </summary>
    public class TableUtils
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TableUtils"/> class. 
        /// </summary>
        /// <param name="header">
        /// The header.
        /// </param>
        public TableUtils(string[] header)
        {
            Columns = new List<TableHeaderColumnInfo>();
            RowType = new ExpandoObject();

            for (var i = 0; i < header.Length; i++)
            {
                var entry = new TableHeaderColumnInfo { Index = i, Name = header[i] };
                Columns.Add(entry);
                AddProperty(entry.Name);
            }
        }

        /// <summary>
        /// Gets or sets the columns.
        /// </summary>
        public List<TableHeaderColumnInfo> Columns { get; set; }

        /// <summary>
        /// Gets or sets the row type.
        /// </summary>
        public dynamic RowType { get; set; }

        /// <summary>
        /// The add property.
        /// </summary>
        /// <param name="propertyName">
        /// The property name.
        /// </param>
        public void AddProperty(string propertyName)
        {
            ((IDictionary<string, object>)RowType).Add(propertyName, string.Empty);
        }

        /// <summary>
        /// The remove property.
        /// </summary>
        /// <param name="propertyName">
        /// The property name.
        /// </param>
        public void RemoveProperty(string propertyName)
        {
            ((IDictionary<string, object>)RowType).Remove(propertyName);
        }

        /// <summary>
        /// The get row type.
        /// </summary>
        /// <returns>
        /// A dynamic record representing the row>.
        /// </returns>
        public dynamic GetRowType()
        {
            return RowType;
        }

        /// <summary>
        /// The get row type.
        /// </summary>
        /// <param name="row">
        /// The row.
        /// </param>
        /// <returns>
        /// A dynamic record representing the row>.
        /// </returns>
        public dynamic GetRowType(string[] row)
        {
            if (Columns.Count != row.Length)
            {
                return null;
            }

            foreach (var column in Columns)
            {
                ((IDictionary<string, object>)RowType)[column.Name] = row[column.Index];
            }

            return RowType;
        }

        /// <summary>
        /// The projection.
        /// </summary>
        /// <typeparam name="T">
        /// The type of the record in the recieving end
        /// </typeparam>
        /// <returns>
        /// The map from the dynamic record into a provided record type.
        /// </returns>
        public T Projection<T>() where T : class
        {
            // Act
            var retVal = AutoMapper.MapDynamic<T>(RowType) as T;
            return retVal;
        }

        /// <summary>
        /// The projection.
        /// </summary>
        /// <param name="row">
        /// The row as a string array
        /// </param>
        /// <typeparam name="T">
        /// The type of the record in the recieving end
        /// </typeparam>
        /// <returns>
        /// The <see cref="T"/>.
        /// </returns>
        public T Projection<T>(string[] row) where T : class
        {
            GetRowType(row);

            var retVal = AutoMapper.MapDynamic<T>(RowType) as T;
            return retVal;
        }
    }
}
