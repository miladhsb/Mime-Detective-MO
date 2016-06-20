using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MimeDetective.Extensions
{
	public static class ByteArrayExtensions
	{
		/// <summary>
		/// Read header of bytes and depending on the information in the header
		/// return object FileType.
		/// Return null in case when the file type is not identified. 
		/// Throws Application exception if the file can not be read or does not exist
		/// </summary>
		/// <remarks>
		/// A temp file is written to get a FileInfo from the given bytes.
		/// If this is not intended use 
		/// 
		///     GetFileType(() => bytes); 
		///     
		/// </remarks>
		/// <param name="file">The FileInfo object.</param>
		/// <returns>FileType or null not identified</returns>
		public static FileType GetFileType(this byte[] bytes)
		{
			return MimeTypes.GetFileType(() => MimeTypes.ReadHeaderFromByteArray(bytes, MimeTypes.MaxHeaderSize), null);
		}

	}
}
