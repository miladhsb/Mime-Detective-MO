using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace MimeDetective.Tests
{
	public class FileTypeTests
	{
		[Fact]
		public void Constructors()
		{
			var info = new FileType(new byte?[] { 0x12, 0x14, 0x13, 0x15, 0x16 }, "png", "image/png", 4);

			Assert.Throws(typeof(ArgumentNullException), () => { var a = new FileType(null, "png", "image/png", 4); });
		}

		[Fact]
		public void Equals()
		{
			Assert.True(MimeTypes.ELF.Equals(MimeTypes.ELF));

			Assert.False(MimeTypes.ELF.Equals(MimeTypes.DLL_EXE));
		}

		[Fact]
		public void EqualsOperatorOverloads()
		{
			var elf = MimeTypes.ELF;

			Assert.True(elf == MimeTypes.ELF);

			Assert.False(elf != MimeTypes.DLL_EXE);
		}

		[Fact]
		public void ToStringReturns()
		{
			var elf = MimeTypes.ELF;

			Assert.Equal(elf.ToString(), MimeTypes.ELF.Extension);

			Assert.NotEqual(elf.ToString(), MimeTypes.ELF.GetType().ToString());
		}
	}
}
