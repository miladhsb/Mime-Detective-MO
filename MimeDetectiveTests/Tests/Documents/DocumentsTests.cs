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
		public const string DocsPath = "./Data/Documents/";

		[Fact]
		public async Task FileInfoDocx()
		{
			var info = new FileInfo(DocsPath + "test.docx");

			var fileInfo = await info.GetFileTypeAsync();

			Assert.True(fileInfo.Extension == "docx");
		}

		[Fact]
		public void IsDocx()
		{
			var info = new FileInfo(DocsPath + "test.docx");

			Assert.True(info.GetFileType().Mime == MimeTypes.WORDX.Mime);
		}

		[Fact]
		public void IsDoc()
		{
			var info = new FileInfo(DocsPath + "test.doc");

			Assert.True(info.IsWord());
		}
	}
}
