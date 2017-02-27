using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using System.IO;
using MimeDetective;
using MimeDetective.Utilities;

namespace MimeDetective.Tests.Documents
{
	public class MsOfficeFormats
	{
		public const string DocsPath = "./Data/Documents/test.";

		[Fact]
		public async Task FileInfoDocx()
		{
			var info = new FileInfo(DocsPath + "docx");

			var a = Directory.GetCurrentDirectory();

			var fileInfo = await info.GetFileTypeAsync();

			Assert.True(fileInfo.Extension == "docx");
		}

		[Fact]
		public async Task IsDoc()
		{
			var info = new FileInfo(DocsPath + "doc");

			Assert.True(info.IsWord());

			await TypeComparisions.AssertIsType(info, MimeTypes.WORD);
		}

		[Fact]
		public async Task IsDocx()
		{
			var info = new FileInfo(DocsPath + "docx");

			Assert.True(info.IsWord());

			await TypeComparisions.AssertIsType(info, MimeTypes.WORDX);
		}

		[Fact]
		public async Task IsPowerPoint()
		{
			var info = new FileInfo(DocsPath + "ppt");

			Assert.True(info.IsPowerPoint());

			await TypeComparisions.AssertIsType(info, MimeTypes.PPT);
		}

		[Fact]
		public async Task IsPowerPointX()
		{
			var info = new FileInfo(DocsPath + "pptx");

			Assert.True(info.IsPowerPoint());

			await TypeComparisions.AssertIsType(info, MimeTypes.PPTX);
		}

		[Fact]
		public async Task IsExcel()
		{
			var info = new FileInfo(DocsPath + "xls");

			Assert.True(info.IsExcel());

			await TypeComparisions.AssertIsType(info, MimeTypes.EXCEL);
		}

		[Fact]
		public async Task IsExcelX()
		{
			var info = new FileInfo(DocsPath + "xlsx");

			Assert.True(info.IsExcel());

			await TypeComparisions.AssertIsType(info, MimeTypes.EXCELX);
		}
	}
}
