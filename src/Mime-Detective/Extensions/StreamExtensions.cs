using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using MimeDetective;

namespace MimeDetective.Extensions
{
	public static class StreamExtensions
	{
		/// <summary>
		/// Read header of a stream and depending on the information in the header
		/// return object FileType.
		/// Return null in case when the file type is not identified.
		/// Throws Application exception if the file can not be read or does not exist
		/// </summary>
		/// <param name="file">The FileInfo object.</param>
		/// <returns>FileType or null not identified</returns>
		public static FileType GetFileType(this Stream stream) 
			=> MimeTypes.GetFileType
				(MimeTypes.ReadHeaderFromStream
					(stream, MimeTypes.MaxHeaderSize),
						stream, null, shouldDisposeStream: false);

		/// <summary>
		/// Read header of a stream and depending on the information in the header
		/// return object FileType.
		/// Return null in case when the file type is not identified.
		/// Throws Application exception if the file can not be read or does not exist
		/// </summary>
		/// <param name="file">The FileInfo object.</param>
		/// <returns>FileType or null not identified</returns>
		public static FileType GetFileType(this Stream stream, bool shouldDisposeStream = false)
			=> MimeTypes.GetFileType
				(MimeTypes.ReadHeaderFromStream
					(stream, MimeTypes.MaxHeaderSize),
						stream, null, shouldDisposeStream);
		/// <summary>
		/// Read header of a stream and depending on the information in the header
		/// return object FileType.
		/// Return null in case when the file type is not identified.
		/// Throws Application exception if the file can not be read or does not exist
		/// </summary>
		/// <param name="file">The FileInfo object.</param>
		/// <returns>FileType or null not identified</returns>
		public static async Task<FileType> GetFileTypeAsync(this Stream stream)
			=> MimeTypes.GetFileType
				(await MimeTypes.ReadHeaderFromStreamAsync(stream, MimeTypes.MaxHeaderSize),
					stream, null, shouldDisposeStream: false);

		/// <summary>
		/// Read header of a stream and depending on the information in the header
		/// return object FileType.
		/// Return null in case when the file type is not identified.
		/// Throws Application exception if the file can not be read or does not exist
		/// </summary>
		/// <param name="file">The FileInfo object.</param>
		/// <returns>FileType or null not identified</returns>
		public static async Task<FileType> GetFileTypeAsync(this Stream stream, bool shouldDisposeStream = false)
			=> MimeTypes.GetFileType
				(await MimeTypes.ReadHeaderFromStreamAsync(stream, MimeTypes.MaxHeaderSize),
					stream, null, shouldDisposeStream);
	}
}
