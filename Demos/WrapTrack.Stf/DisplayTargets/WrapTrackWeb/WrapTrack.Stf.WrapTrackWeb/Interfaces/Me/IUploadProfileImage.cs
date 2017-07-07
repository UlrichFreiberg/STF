// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IUploadProfileImage.cs" company="Mir Software">
//   Copyright governed by Artistic license as described here:
//          http://www.perlfoundation.org/artistic_license_2_0
// </copyright>
// <summary>
//   The Me interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace WrapTrack.Stf.WrapTrackWeb.Interfaces.Me
{
    /// <summary>
    /// The UploadProfileImage interface.
    /// </summary>
    public interface IUploadProfileImage
    {
        /// <summary>
        /// Gets or sets the identification.
        /// </summary>
        string Identification { get; set; }

        /// <summary>
        /// Gets or sets the file name.
        /// </summary>
        string FileName { get; set; }

        /// <summary>
        /// The upload.
        /// </summary>
        void Upload();
    }
}