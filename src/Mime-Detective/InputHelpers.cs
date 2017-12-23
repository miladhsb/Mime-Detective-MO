using System;
using System.Buffers;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace MimeDetective
{
	internal static class InputHelpers
	{
		internal struct ReadResult
		{
			public readonly byte[] Array;
			public readonly Stream Source;
			public readonly uint ReadLength;
			public readonly bool IsArrayRented;

			public ReadResult(byte[] array, Stream source, uint readLength, bool isArrayRented)
			{
				this.Array = array;
				this.Source = source;
				this.ReadLength = readLength;
				this.IsArrayRented = isArrayRented;
			}
		}

		/// <summary>
		/// Reads the file header - first (16) bytes from the file
		/// </summary>
		/// <param name="file">The file to work with</param>
		/// <returns>Array of bytes</returns>
		internal static ReadResult ReadFileHeader(FileStream fileStream, ushort maxHeaderSize)
		{
			byte[] header = ArrayPool<byte>.Shared.Rent(maxHeaderSize);

			// read first symbols from file into array of bytes.
			int bytesRead = fileStream.Read(header, 0, maxHeaderSize);

			return new ReadResult(header, fileStream, (uint)bytesRead, isArrayRented: true);
		}

		internal static async Task<ReadResult> ReadFileHeaderAsync(FileStream fileStream, ushort maxHeaderSize)
		{
			byte[] header = ArrayPool<byte>.Shared.Rent(maxHeaderSize);

			// read first symbols from file into array of bytes.
			int bytesRead = await fileStream.ReadAsync(header, 0, maxHeaderSize);

			return new ReadResult(header, fileStream, (uint)bytesRead, isArrayRented: true);
		}

		/// <summary>
		/// Takes a stream does, not dispose of stream, resets read position to beginning though
		/// </summary>
		/// <param name="stream"></param>
		/// <param name="MaxHeaderSize"></param>
		/// <returns></returns>
		//TODO streamread result
		internal static ReadResult ReadHeaderFromStream(Stream stream, ushort MaxHeaderSize)
		{
			if (!stream.CanRead)
				throw new IOException("Could not read from Stream");

			if (stream.Position > 0)
				stream.Seek(0, SeekOrigin.Begin);

			byte[] header = ArrayPool<byte>.Shared.Rent(MaxHeaderSize);

			//TODO return number of bytes read for all methods
			int bytesRead = stream.Read(header, 0, MaxHeaderSize);

			return new ReadResult(header, stream, (uint)bytesRead, isArrayRented: true);
		}

		/// <summary>
		/// Takes a stream does, not dispose of stream, resets read position to beginning though
		/// </summary>
		/// <param name="stream"></param>
		/// <param name="MaxHeaderSize"></param>
		/// <returns></returns>
		//TODO throw helper [MethodImpl(MethodImplOptions.NoInlining)]
		internal static async Task<ReadResult> ReadHeaderFromStreamAsync(Stream stream, ushort MaxHeaderSize)
		{
			if (!stream.CanRead)
				throw new IOException("Could not read from Stream");

			if (stream.Position > 0)
				stream.Seek(0, SeekOrigin.Begin);

			byte[] header = ArrayPool<byte>.Shared.Rent(MaxHeaderSize);

			int bytesRead = await stream.ReadAsync(header, 0, MaxHeaderSize);

			return new ReadResult(header, stream, (uint)bytesRead, isArrayRented: true);
		}
	}
}
