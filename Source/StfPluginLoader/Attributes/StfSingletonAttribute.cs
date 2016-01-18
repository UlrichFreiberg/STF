// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StfSingletonAttribute.cs" company="Mir Software">
//   Copyright governed by Artistic license as described here:
//          http://www.perlfoundation.org/artistic_license_2_0
// </copyright>
// <summary>
//   The stf singleton attribute.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;

namespace Mir.Stf.Utilities.Attributes
{
    /// <summary>
    /// The stf singleton attribute. If applied to a class implementing
    /// IStfPlugin interface, the type will be registered with the container
    /// as a singleton
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface)]
    public class StfSingletonAttribute : Attribute
    {
    }
}