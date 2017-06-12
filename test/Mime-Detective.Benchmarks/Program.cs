using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Attributes.Jobs;
using BenchmarkDotNet.Running;
using System;
using System.IO;
using System.Security.Cryptography;
using MimeDetective.Extensions;
using MimeDetective;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Environments;
using BenchmarkDotNet.Toolchains.CsProj;

namespace Mime_Detective.Benchmarks
{
	public class MyConfig : ManualConfig
	{
		public MyConfig()
		{
			Add(Job.Default.With(Runtime.Clr)
				.With(CsProjClassicNetToolchain.Net462)
				.With(Jit.RyuJit)
				.With(Platform.X64)
				.WithId("Net462"));

			Add(Job.Default.With(Runtime.Clr)
				.With(CsProjClassicNetToolchain.Net47)
				.With(Jit.RyuJit)
				.With(Platform.X64)
				.WithId("Net462"));

			Add(Job.Default.With(Runtime.Core)
				.With(CsProjCoreToolchain.NetCoreApp11)
				.With(Platform.X64)
				.With(Jit.RyuJit)
				.WithId("NetCore1.1"));

			Add(Job.Default.With(Runtime.Core)
				.With(CsProjCoreToolchain.NetCoreApp20)
				.With(Platform.X64)
				.With(Jit.RyuJit)
				.WithId("NetCore2.0"));
		}
	}

	[Config(typeof(MyConfig)), MemoryDiagnoser]
	public class TypeLookup
	{
		const string GoodFile = "./data/images/test.jpg";

		const string GoodXmlFile = "./data/Documents/test.docx";

		const string GoodZipFile = "./data/images.zip";

		const string BadFile = "./data/empty.jpg";

		[Benchmark(OperationsPerInvoke = 100)]
		public void GoodLookup() => DoNTimes(GoodFile);

		[Benchmark(OperationsPerInvoke = 100)]
		public void BinaryLookup() => DoNTimesBinary(GoodFile);

		[Benchmark(OperationsPerInvoke = 100)]
		public void BadLookup() => DoNTimes(BadFile);


		[Benchmark(OperationsPerInvoke = 100)]
		public void DocxLookup() => DoNTimes(GoodXmlFile);

		[Benchmark(OperationsPerInvoke = 100)]
		public void ZipLookup() => DoNTimes(GoodZipFile);

		private void DoNTimes(string fileName)
		{
			for (int i = 0; i < 100; i++)
			{
				FileType type = (new FileInfo(fileName)).GetFileType();
			}
		}

		private void DoNTimesBinary(string fileName)
		{
			for (int i = 0; i < 100; i++)
			{
				FileType type = (new FileInfo(fileName)).GetFileTypeBinary();
			}
		}
	}

	public class Program
	{
		public static void Main(string[] args)
		{
			var summary = BenchmarkRunner.Run<TypeLookup>(); 
		}
	}
}