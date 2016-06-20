using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using System.IO;
using MimeDetective;

namespace MimeDetectiveTests.Tests.Documents
{
	public class DocumentsTests
	{
		public const string ImagePath = "./Data/Documents/";

		[Fact]
		public void IsDocx()
		{
			var info = new FileInfo(ImagePath + "test.docx");

			Assert.True(info.GetFileType().Mime == MimeTypes.WORDX.Mime);
		}

		[Fact]
		public void IsDoc()
		{
			var info = new FileInfo(ImagePath + "test.doc");

			Assert.True(info.IsWord());
		}
	}
}
