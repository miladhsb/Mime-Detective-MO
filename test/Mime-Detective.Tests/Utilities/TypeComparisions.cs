using MimeDetective;
using System;
using System.IO;
using System.Threading.Tasks;
using Xunit;

namespace MimeDetective.Utilities
{
	public static class TypeComparisions
	{
		public static async Task AssertIsType(FileInfo info, FileType type)
		{
			Assert.Equal(await info.GetFileTypeAsync(), type);

			Assert.Equal(info.GetFileType(), type);

			Assert.True(info.IsType(type));

			Assert.True(info.GetFileType() == type);

			Assert.False(info.GetFileType() != type);
		}
	}

	/*
	public class MyClass
	{
		[Fact]
		public void test()
		{
			Assert.ThrowsAny<Exception>(() => { TypeComparisions.AssertIsType(null, MimeTypes.AES).Wait(); });
		}
	}
	*/
}