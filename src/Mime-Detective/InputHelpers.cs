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
			public Stream Source;
			public readonly int ReadLength;
			public readonly bool IsArrayRented;
			public bool ShouldDisposeStream;

			/// <summary>
			/// Non rented array input, Array is Input
			/// </summary>
			/// <param name="array"></param>
			/// <param name="readLength"></param>
			public ReadResult(byte[] array, int readLength)
			{
				this.Array = array;
				this.Source = null;
				this.ReadLength = readLength;
				this.IsArrayRented = false;
				this.ShouldDisposeStream = true;
			}

			public ReadResult(byte[] array, Stream source, int readLength, bool isArrayRented, bool shouldDisposeStream = true)
			{
				this.Array = array;
				this.Source = source;
				this.ReadLength = readLength;
				this.IsArrayRented = isArrayRented;
				this.ShouldDisposeStream = shouldDisposeStream;
			}

			public void CreateMemoryStreamIfSourceIsNull()
			{
				if (Source is null)
				{
					Source = new MemoryStream(Array, 0, (int)ReadLength);
					ShouldDisposeStream = true;
				}
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

			int bytesRead = fileStream.Read(header, 0, maxHeaderSize);

			return new ReadResult(header, fileStream, bytesRead, isArrayRented: true, shouldDisposeStream: true);
		}

		internal static async Task<ReadResult> ReadFileHeaderAsync(FileStream fileStream, ushort maxHeaderSize)
		{
			byte[] header = ArrayPool<byte>.Shared.Rent(maxHeaderSize);

			int bytesRead = await fileStream.ReadAsync(header, 0, maxHeaderSize);

			return new ReadResult(header, fileStream, bytesRead, isArrayRented: true, shouldDisposeStream: true);
		}

		/// <summary>
		/// Takes a stream does, not dispose of stream, resets read position to beginning though
		/// </summary>
		/// <param name="stream"></param>
		/// <param name="MaxHeaderSize"></param>
		/// <returns></returns>
		//TODO streamread result
		internal static ReadResult ReadHeaderFromStream(Stream stream, ushort MaxHeaderSize, bool shouldDisposeStream)
		{
			if (!stream.CanRead)
				throw new IOException("Could not read from Stream");

			if (stream.Position > 0)
				stream.Seek(0, SeekOrigin.Begin);

			byte[] header = ArrayPool<byte>.Shared.Rent(MaxHeaderSize);

			int bytesRead = stream.Read(header, 0, MaxHeaderSize);

			return new ReadResult(header, stream, bytesRead, isArrayRented: true, shouldDisposeStream);
		}

		/// <summary>
		/// Takes a stream does, not dispose of stream, resets read position to beginning though
		/// </summary>
		/// <param name="stream"></param>
		/// <param name="MaxHeaderSize"></param>
		/// <returns></returns>
		//TODO throw helper [MethodImpl(MethodImplOptions.NoInlining)]
		internal static async Task<ReadResult> ReadHeaderFromStreamAsync(Stream stream, ushort MaxHeaderSize, bool shouldDisposeStream)
		{
			if (!stream.CanRead)
				throw new IOException("Could not read from Stream");

			if (stream.Position > 0)
				stream.Seek(0, SeekOrigin.Begin);

			byte[] header = ArrayPool<byte>.Shared.Rent(MaxHeaderSize);

			int bytesRead = await stream.ReadAsync(header, 0, MaxHeaderSize);

			return new ReadResult(header, stream, bytesRead, isArrayRented: true, shouldDisposeStream);
		}
	}
}
