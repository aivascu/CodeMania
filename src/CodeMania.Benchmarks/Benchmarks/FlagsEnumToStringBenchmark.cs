﻿using BenchmarkDotNet.Attributes;
using CodeMania.Core;
using Common.TestData.TestDataTypes;

namespace CodeMania.Benchmarks.Benchmarks
{
	[MemoryDiagnoser]
	[CoreJob(true), RPlotExporter, RankColumn]
	public class FlagsEnumToStringBenchmark
	{
		private Flags fileAccess = Flags.Eight | Flags.Four | Flags.Two;
		private readonly EnumMap<Flags> enumMap = EnumMap<Flags>.Instance;

		[Benchmark(Baseline = true)]
		public string EnumToString() => fileAccess.ToString();

		[Benchmark(Baseline = false)]
		public string EnumMapToString() => enumMap.ToString(fileAccess);
	}
}