using System;

namespace MimeDetective
{
	/// <summary>
	/// Little data structure to hold information about file types.
	/// Holds information about binary header at the start of the file
	/// </summary>
	public class FileType
	{
		public byte?[] Header { get; }

		public ushort HeaderOffset { get; }

		public string Extension { get; }

		public string Mime { get; }

		/// <summary>
		/// Initializes a new instance of the <see cref="FileType"/> class
		/// Takes the details of offset for the header
		/// </summary>
		/// <param name="header">Byte array with header.</param>
		/// <param name="offset">The header offset - how far into the file we need to read the header</param>
		/// <param name="extension">String with extension.</param>
		/// <param name="mime">The description of MIME.</param>
		public FileType(byte?[] header, string extension, string mime, ushort offset = 0)
		{
			Header = header ?? throw new ArgumentNullException(nameof(header), $"cannot be null, {nameof(FileType)} needs file header data");

			HeaderOffset = offset;
			Extension = extension;
			Mime = mime;
		}

		public static bool operator == (FileType a, FileType b)
		{
			if (a is null && b is null)
				return true;

			if (b is null)
				return a.Equals(b);
			else
				return b.Equals(a);
		}

		public static bool operator !=(FileType a, FileType b) => !(a == b);

		public override bool Equals(object other)
		{
			if (other == null)
				return false;

			if(other is FileType type)
			{
				if (Extension.Equals(type.Extension) && Mime.Equals(type.Mime))
					return true;

				return base.Equals(other);
			}

			return false;
		}

		public override int GetHashCode() => (base.GetHashCode() ^ Header.GetHashCode() ^ HeaderOffset ^ Extension.GetHashCode() ^ Mime.GetHashCode());

		public override string ToString() => Extension;
	}
}