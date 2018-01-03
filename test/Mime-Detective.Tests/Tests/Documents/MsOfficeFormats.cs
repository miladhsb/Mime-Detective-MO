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
		public const string DocsPath = "./Data/Documents/";

		private static FileInfo GetFileInfo(string file, string ext)
		{
			return new FileInfo(Path.Combine(DocsPath, file+ext));
		}

		[Theory]
		[InlineData("DocWord2016")]
		public async Task IsDoc(string filePath)
		{
			var info = GetFileInfo(filePath, ".doc");

			Assert.True(info.IsWord());

			await TypeComparisions.AssertIsType(info, MimeTypes.WORD);
		}

		[Theory]
		[InlineData("DocxWord2016")]
		[InlineData("StrictOpenXMLWord2016")]
		public async Task IsDocx(string filePath)
		{
			var info = GetFileInfo(filePath, ".docx");

			Assert.True(info.IsWord());

			await TypeComparisions.AssertIsType(info, MimeTypes.WORDX);
		}

		[Theory]
		[InlineData("RichTextWord2016")]
		public async Task IsRTF(string filePath)
		{
			var info = GetFileInfo(filePath, ".rtf");

			Assert.True(info.IsRtf());

			await TypeComparisions.AssertIsType(info, MimeTypes.RTF);
		}

		[Theory]
		[InlineData("OpenDocWord2016")]
		public async Task IsOpenDoc(string filePath)
		{
			var info = GetFileInfo(filePath, ".odt");

			await TypeComparisions.AssertIsType(info, MimeTypes.ODT);
		}

		[Theory]
		[InlineData("PptPowerpoint2016")]
		public async Task IsPowerPoint(string filePath)
		{
			var info = GetFileInfo(filePath, ".ppt");

			Assert.True(info.IsPowerPoint());

			await TypeComparisions.AssertIsType(info, MimeTypes.PPT);
		}

		[Theory]
		[InlineData("PptxPowerpoint2016")]
		public async Task IsPowerPointX(string filePath)
		{
			var info = GetFileInfo(filePath, ".pptx");

			Assert.True(info.IsPowerPoint());

			await TypeComparisions.AssertIsType(info, MimeTypes.PPTX);
		}

		[Theory]
		[InlineData("XlsExcel2016")]
		public async Task IsExcel(string filePath)
		{
			var info = GetFileInfo(filePath, ".xls");

			Assert.True(info.IsExcel());

			await TypeComparisions.AssertIsType(info, MimeTypes.EXCEL);
		}

		[Theory]
		[InlineData("XlsxExcel2016")]
		public async Task IsExcelX(string filePath)
		{
			var info = GetFileInfo(filePath, ".xlsx");

			Assert.True(info.IsExcel());

			await TypeComparisions.AssertIsType(info, MimeTypes.EXCELX);
		}
	}
}
