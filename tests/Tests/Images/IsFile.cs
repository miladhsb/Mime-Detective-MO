using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using System.IO;
using MimeDetective;
using MimeDetective.Extensions;

namespace MimeDetective.Tests.Images
{
	// This project can output the Class library as a NuGet Package.
	// To enable this option, right-click on the project and select the Properties menu item. In the Build tab select "Produce outputs on build".
	public class IsFile
	{
		public IsFile()
		{
		}

		public const string ImagePath = "./Data/Images/";

		private readonly string BmpMime = MimeTypes.BMP.Mime;

		private readonly string IcoMime = MimeTypes.ICO.Mime;

		[Fact]
		public void IsJpeg()
		{
			var info = new FileInfo(ImagePath + "test.jpg");

			Assert.True(info.IsJpeg());

			//false assertions
			Assert.False(info.IsGif());

			Assert.False(info.IsPng());

			Assert.False(info.GetFileType().Mime == BmpMime);

			Assert.False(info.GetFileType().Mime == IcoMime);
		}

		[Fact]
		public void IsBitmap()
		{
			var info = new FileInfo(ImagePath + "test.bmp");

			Assert.True(info.GetFileType().Mime == BmpMime);

			//false assertions
			Assert.False(info.IsJpeg());

			Assert.False(info.IsGif());

			Assert.False(info.IsPng());

			Assert.False(info.GetFileType().Mime == IcoMime);
		}

		[Fact]
		public void IsPng()
		{
			var info = new FileInfo(ImagePath + "test.png");

			Assert.True(info.IsPng());

			//false assertions
			Assert.False(info.IsGif());

			Assert.False(info.IsJpeg());

			Assert.False(info.GetFileType().Mime == BmpMime);

			Assert.False(info.GetFileType().Mime == IcoMime);
		}

		[Fact]
		public void IsGif()
		{
			var info = new FileInfo(ImagePath + "test.gif");

			Assert.True(info.IsGif());

			//false assertions
			Assert.False(info.IsPng());

			Assert.False(info.IsJpeg());

			Assert.False(info.GetFileType().Mime == BmpMime);

			Assert.False(info.GetFileType().Mime == IcoMime);
		}

		[Fact]
		public void IsIco()
		{
			var info = new FileInfo(ImagePath + "test.ico");

			Assert.True(info.GetFileType().Mime == IcoMime);

			//false assertions
			Assert.False(info.IsPng());

			Assert.False(info.IsGif());

			Assert.False(info.IsJpeg());

			Assert.False(info.GetFileType().Mime == BmpMime);
		}

	}
}
