// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ITableUtils.cs" company="Mir Software">
//   Copyright governed by Artistic license as described here:
//          http://www.perlfoundation.org/artistic_license_2_0
// </copyright>
// <summary>
//   The TableUtils interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Mir.Stf.Utilities.Interfaces
{
    using System.Collections.Generic;

    using Mir.Stf.Utilities.TableUtilities;

    /// <summary>
    /// The TableUtils interface.
    /// </summary>
    public interface ITableUtils
    {
        /// <summary>
        /// Gets or sets the columns.
        /// </summary>
        List<TableHeaderColumnInfo> Columns { get; set; }

        /// <summary>
        /// Gets or sets the row type.
        /// </summary>
        dynamic RowType { get; set; }

        /// <summary>
        /// The add property.
        /// </summary>
        /// <param name="propertyName">
        /// The property name.
        /// </param>
        void AddProperty(string propertyName);

        /// <summary>
        /// The remove property.
        /// </summary>
        /// <param name="propertyName">
        /// The property name.
        /// </param>
        void RemoveProperty(string propertyName);

        /// <summary>
        /// The get row type.
        /// </summary>
        /// <returns>
        /// A dynamic record representing the row>.
        /// </returns>
        dynamic GetRowType();

        /// <summary>
        /// The get row type.
        /// </summary>
        /// <param name="row">
        /// The row.
        /// </param>
        /// <returns>
        /// A dynamic record representing the row>.
        /// </returns>
        dynamic GetRowType(string[] row);

        /// <summary>
        /// The projection.
        /// </summary>
        /// <typeparam name="T">
        /// The type of the record in the recieving end
        /// </typeparam>
        /// <returns>
        /// The map from the dynamic record into a provided record type.
        /// </returns>
        T Projection<T>() where T : class;

        /// <summary>
        /// The projection. 
        /// </summary>
        /// <param name="initializeMe">
        /// First argument is usually a containing "this" to initialize the properties for "this".
        /// </param>
        /// <param name="row">
        /// The row as a string array
        /// </param>
        /// <typeparam name="T">
        /// The type of the record in the recieving end
        /// </typeparam>
        /// <returns>
        /// The <see cref="T"/>.
        /// </returns>
        T Projection<T>(T initializeMe, string[] row) where T : class;

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
        T Projection<T>(string[] row) where T : class;
    }
}