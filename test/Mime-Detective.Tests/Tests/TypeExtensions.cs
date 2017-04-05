using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MimeDetective.Extensions;
using MimeDetective;
using Xunit;

namespace MimeDetective.Tests
{
	public class TypeExtensions
	{
		const string GoodFile = "./data/images/test.jpg";

		const string GoodXmlFile = "./data/Documents/test.docx";

		const string GoodZipFile = "./data/images.zip";

		const string BadFile = "./data/empty.jpg";

		const string NonexistentFile = "./data/nonexistent.jpg";

		//load from fileinfo
		//attempt to load from real file
		//attempt to load from nonexistent file /badfile
		[Fact]
		public async Task FromFile()
		{
			var fileInfo = new FileInfo(GoodFile);

			var fileType = await fileInfo.GetFileTypeAsync();

			Assert.True(fileType == MimeTypes.JPEG);
		}

		[Fact]
		public void FromFileSync()
		{
			var fileInfo = new FileInfo(GoodFile);

			Assert.True(fileInfo.IsJpeg());
		}

		//test shouldn't fail, an empty file can be valid input
		[Fact]
		public async Task FromEmptyFile()
		{
			var fileInfo = new FileInfo(BadFile);

			var type = await fileInfo.GetFileTypeAsync();

			//no match so return type is null
			Assert.Null(type);
		}

		[Fact]
		public async Task FromNonExistentFile()
		{
			var fileInfo = new FileInfo(NonexistentFile);

			await Assert.ThrowsAnyAsync<Exception>(() => fileInfo.GetFileTypeAsync());
		}

		//load from stream
			//attempt to load from good stream
			//attempt to load from empty stream
			//stream shouldn't be closed

		[Fact]
		public async Task FromStream()
		{
			var fileInfo = new FileInfo(GoodFile);

			using (var fileStream = fileInfo.OpenRead())
			{
				var fileType = await fileStream.GetFileTypeAsync();

				Assert.NotNull(fileType);

				Assert.Equal(MimeTypes.JPEG, fileType);
			}
		}

		[Fact]
		public void FromStreamSync()
		{
			var fileInfo = new FileInfo(GoodFile);

			using (var fileStream = fileInfo.OpenRead())
			{
				var fileType = fileStream.GetFileType();

				Assert.NotNull(fileType);

				Assert.Equal(MimeTypes.JPEG, fileType);
			}
		}

		[Fact]
		public async Task FromEmptyStream()
		{
			var emptyStream = System.IO.Stream.Null;

			var nullReturn = await emptyStream.GetFileTypeAsync();

			Assert.Null(nullReturn);
		}

		[Theory]
		[InlineData(GoodFile, "jpg")]
		[InlineData(GoodXmlFile, "docx")]
		[InlineData(GoodZipFile, "zip")]
		public async Task StreamShouldStillBeOpen(string path, string ext)
		{
			var fileInfo = new FileInfo(path);

			var fileStream = fileInfo.OpenRead();
			
			var fileType = await fileStream.GetFileTypeAsync();

			Assert.NotNull(fileType);

			Assert.Equal(ext, fileType.Extension);

			Assert.True(fileStream.CanRead);

			Assert.True(fileStream.CanSeek);

			Assert.False(fileStream.CanWrite);

			fileStream.Dispose();

			Assert.False(fileStream.CanRead);

			Assert.False(fileStream.CanSeek);

			Assert.False(fileStream.CanWrite);
		}

		//load from byte array
			//load from good byte array
			//attempt to load from empty byte array
		[Fact]
		public async Task FromByteArray()
		{
			var fileInfo = new FileInfo(GoodFile);

			//560 is the max file header size
			byte[] byteArray = new byte[560];

			using (var fileStream = fileInfo.OpenRead())
			{
				await fileStream.ReadAsync(byteArray, 0, 560);
			}

			var mimeType = byteArray.GetFileType();

			Assert.NotNull(mimeType);

			Assert.Equal(MimeTypes.JPEG, mimeType);
		}

		[Fact]
		public void FromEmptyByteArray()
		{
			var zerodByteArray = new byte[560];

			var zerodResult = zerodByteArray.GetFileType();

			Assert.Null(zerodResult);

			var emptyBtyeArray = new byte[0];

			Assert.ThrowsAny<Exception>(() => emptyBtyeArray.GetFileType());
		}
	}
}
