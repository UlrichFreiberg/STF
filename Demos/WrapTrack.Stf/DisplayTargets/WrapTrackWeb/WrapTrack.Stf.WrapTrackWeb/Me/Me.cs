// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Me.cs" company="Mir Software">
//   Copyright governed by Artistic license as described here:
//          http://www.perlfoundation.org/artistic_license_2_0
// </copyright>
// <summary>
//   The Me interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace WrapTrack.Stf.WrapTrackWeb.Me
{
    using System;

    using WrapTrack.Stf.WrapTrackWeb.Interfaces.Me;

    /// <summary>
    /// The Me interface.
    /// </summary>
    public class Me : IMe
    {
        /// <summary>
        /// The upload profile image.
        /// </summary>
        /// <returns>
        /// The <see cref="IUploadProfileImage"/>.
        /// </returns>
        public IUploadProfileImage UploadProfileImage()
        {
            throw new NotImplementedException();
        }
    }
}