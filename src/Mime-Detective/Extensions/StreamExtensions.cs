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

		//todo fix so doesn't create temp file
		/// <summary>
		/// Read header of a stream and depending on the information in the header
		/// return object FileType.
		/// Return null in case when the file type is not identified.
		/// Throws Application exception if the file can not be read or does not exist
		/// </summary>
		/// <param name="file">The FileInfo object.</param>
		/// <returns>FileType or null not identified</returns>
		public static FileType GetFileType(this Stream stream)
		{
			return MimeTypes.GetFileType(() => MimeTypes.ReadHeaderFromStream(stream, MimeTypes.MaxHeaderSize), stream);
		}

		//todo fix so doesn't create temp file
		/// <summary>
		/// Read header of a stream and depending on the information in the header
		/// return object FileType.
		/// Return null in case when the file type is not identified.
		/// Throws Application exception if the file can not be read or does not exist
		/// </summary>
		/// <param name="file">The FileInfo object.</param>
		/// <returns>FileType or null not identified</returns>
		public static Task<FileType> GetFileTypeAsync(this Stream stream)
		{
			return MimeTypes.GetFileTypeAsync(() => MimeTypes.ReadHeaderFromStreamAsync(stream, MimeTypes.MaxHeaderSize), stream);
		}

		//todo fix so doesn't create temp file
		/// <summary>
		/// Read header of a stream and depending on the information in the header
		/// return object FileType.
		/// Return null in case when the file type is not identified.
		/// Throws Application exception if the file can not be read or does not exist
		/// </summary>
		/// <param name="file">The FileInfo object.</param>
		/// <returns>FileType or null not identified</returns>
		/*
		public static FileType GetFileTypeViaTempFile(this Stream stream)
		{
			FileType fileType = null;
			var fileName = Path.GetTempFileName();

			try
			{
				using (var fileStream = File.Create(fileName))
				{
					stream.Seek(0, SeekOrigin.Begin);
					stream.CopyTo(fileStream);
				}

				fileType = FileInfoExtensions.GetFileType(new FileInfo(fileName));
			}
			finally
			{
				File.Delete(fileName);
			}
			return fileType;
		}
		*/
	}
}
