using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MimeDetective;
using Xunit;
using System.IO;

namespace MimeDetectiveTests.Tests.Text
{
	public class TextTests
	{
		public const string TextPath = "./Data/Text/";

		public const string TextFile = "test.txt";

		private readonly static string TxtMime = MimeTypes.TXT.Mime;

		[Fact]
		public void IsTxt()
		{
			var info = new FileInfo(TextPath + TextFile);

			Assert.True(info.GetFileType().Mime == TxtMime);
		}
	}
}
