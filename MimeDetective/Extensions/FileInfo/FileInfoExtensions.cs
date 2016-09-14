using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using MimeDetective;

namespace MimeDetective
{
	public static partial class FileInfoExtensions
	{
		/// <summary>
		/// Read header of a file and depending on the information in the header
		/// return object FileType.
		/// Return null in case when the file type is not identified. 
		/// Throws Application exception if the file can not be read or does not exist
		/// </summary>
		/// <param name="file">The FileInfo object.</param>
		/// <returns>FileType or null not identified</returns>
		public static FileType GetFileType(this FileInfo file)
		{
			using (var stream = file.OpenRead())
			{
				return MimeTypes.GetFileType(() => MimeTypes.ReadFileHeader(file, MimeTypes.MaxHeaderSize), stream);
			}
		}

		public static async Task<FileType> GetFileTypeAsync(this FileInfo file)
		{
			using (var stream = file.OpenRead())
			{
				return await MimeTypes.GetFileTypeAsync(() => MimeTypes.ReadFileHeaderAsync(file, MimeTypes.MaxHeaderSize), stream);
			}
		}

		/// <summary>
		/// Determines whether provided file belongs to one of the provided list of files
		/// </summary>
		/// <param name="file">The file.</param>
		/// <param name="requiredTypes">The required types.</param>
		/// <returns>
		///   <c>true</c> if file of the one of the provided types; otherwise, <c>false</c>.
		/// </returns>
		public static bool isFileOfTypes(this FileInfo file, List<FileType> requiredTypes)
		{
			FileType currentType = file.GetFileType();

			if (currentType.Mime == null)
				return false;

			return requiredTypes.Contains(currentType);
		}

		/// <summary>
		/// Determines whether provided file belongs to one of the provided list of files,
		/// where list of files provided by string with Comma-Separated-Values of extensions
		/// </summary>
		/// <param name="file">The file.</param>
		/// <param name="requiredTypes">The required types.</param>
		/// <returns>
		///   <c>true</c> if file of the one of the provided types; otherwise, <c>false</c>.
		/// </returns>
		public static bool isFileOfTypes(this FileInfo file, String CSV)
		{
			List<FileType> providedTypes = MimeTypes.GetFileTypesByExtensions(CSV);

			return file.isFileOfTypes(providedTypes);
		}


		#region isType functions

		/// <summary>
		/// Determines whether the specified file is of provided type
		/// </summary>
		/// <param name="file">The file.</param>
		/// <param name="type">The FileType</param>
		/// <returns>
		///   <c>true</c> if the specified file is type; otherwise, <c>false</c>.
		/// </returns>
		public static bool IsType(this FileInfo file, FileType type)
		{
			FileType actualType = GetFileType(file);

			if (actualType.Mime == null)
				return false;

			return (actualType.Equals(type));
		}

		/// <summary>
		/// Determines whether the specified file is MS Excel spreadsheet
		/// </summary>
		/// <param name="fileInfo">The FileInfo</param>
		/// <returns>
		///   <c>true</c> if the specified file info is excel; otherwise, <c>false</c>.
		/// </returns>
		public static bool IsExcel(this FileInfo fileInfo)
		{
			return fileInfo.IsType(MimeTypes.EXCEL);
		}

		/// <summary>
		/// Determines whether the specified file is Microsoft PowerPoint Presentation
		/// </summary>
		/// <param name="fileInfo">The FileInfo object.</param>
		/// <returns>
		///   <c>true</c> if the specified file info is PPT; otherwise, <c>false</c>.
		/// </returns>
		public static bool IsPpt(this FileInfo fileInfo)
		{
			return fileInfo.IsType(MimeTypes.PPT);
		}

		/// <summary>
		/// Checks if the file is executable
		/// </summary>
		/// <param name="fileInfo"></param>
		/// <returns></returns>
		public static bool IsExe(this FileInfo fileInfo)
		{
			return fileInfo.IsType(MimeTypes.DLL_EXE);
		}

		/// <summary>
		/// Check if the file is Microsoft Installer.
		/// Beware, many Microsoft file types are starting with the same header. 
		/// So use this one with caution. If you think the file is MSI, just need to confirm, use this method. 
		/// But it could be MSWord or MSExcel, or Powerpoint... 
		/// </summary>
		/// <param name="fileInfo"></param>
		/// <returns></returns>
		public static bool IsMsi(this FileInfo fileInfo)
		{
			// MSI has a generic DOCFILE header. Also it matches PPT files
			return fileInfo.IsType(MimeTypes.PPT) || fileInfo.IsType(MimeTypes.MSDOC);
		}
		#endregion
	}
}
