﻿using System;
using BenchmarkDotNet.Attributes;
using CodeMania.Core;
using Common.TestData.TestDataTypes;

namespace CodeMania.Benchmarks.Benchmarks
{
	[MemoryDiagnoser]
	[CoreJob(true), RPlotExporter, RankColumn]
	public class EnumParseBenchmark
	{
		public string fileAccess = NonFlags.Four.ToString();
		public readonly EnumMap<NonFlags> enumMap = EnumMap<NonFlags>.Instance;

		[Benchmark(Baseline = true)]
		public NonFlags EnumParse() => (NonFlags) Enum.Parse(typeof(NonFlags), fileAccess);

		[Benchmark(Baseline = false)]
		public NonFlags EnumMapParse() => enumMap.Parse(fileAccess);
	}
}