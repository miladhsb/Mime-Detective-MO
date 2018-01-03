using System.IO;
using System.Threading.Tasks;
using Xunit;

namespace MimeDetective.Tests.Tests.Documents
{
	public class PdfFormats
	{
		public const string DocsPath = "./Data/Documents/";

		[Theory]
		[InlineData("MicrosoftPrintToPdf.pdf")]
		[InlineData("GithubTestPdf2.pdf")]
		[InlineData("PdfWord2016.pdf")]
		public async Task FileInfoPDF(string testPdf)
		{
			var info = new FileInfo(Path.Combine(DocsPath, testPdf));

			var a = Directory.GetCurrentDirectory();

			var fileInfo = await info.GetFileTypeAsync();

			Assert.Equal(fileInfo, MimeTypes.PDF);
		}
	}
}