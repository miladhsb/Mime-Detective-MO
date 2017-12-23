using System.IO;
using System.Threading.Tasks;
using Xunit;

namespace MimeDetective.Tests.Tests.Documents
{
	public class PdfFormats
	{
		public const string DocsPath = "./Data/Documents/";

		[Theory]
		[InlineData("microsoftPrintToPdf.pdf")]
		[InlineData("pdf-test2.pdf")]
		[InlineData("word2016Test.pdf")]
		public async Task FileInfoPDF(string testPdf)
		{
			var info = new FileInfo(Path.Combine(DocsPath, testPdf));

			var a = Directory.GetCurrentDirectory();

			var fileInfo = await info.GetFileTypeAsync();

			Assert.True(fileInfo.Extension == "pdf");
		}
	}
}