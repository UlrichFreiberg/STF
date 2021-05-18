// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IFolderUtils.cs" company="Mir Software">
//   Copyright governed by Artistic license as described here:
//          http://www.perlfoundation.org/artistic_license_2_0
// </copyright>
// <summary>
//   Defines the FileFolder type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Mir.Stf.Utilities.Interfaces
{
    public interface IFolderUtils
    {
        string FolderUtilsRoot { get; }

        bool Create(string folderPath);
        bool Delete(string folderPath, bool recursive = false);
        bool Exists(string folderPath);
        string GetAbsolutePathFor(string folderPath);
        string GetTempFolder();
        bool MirrorFolder(string sourceDirname, string destinationDirname);
        bool InitializeTempResult(string folderPath = null);
    }
}