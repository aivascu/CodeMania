﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Text;
using NUnit.Framework;

namespace CodeMania.UnitTests.EqualityComparers
{
	[TestFixture]
	public abstract class ArrayMemoryEqualityComparerTestBase<T> : EqualityComparerTestsBase<T[]>
		where T : struct
	{
		public class ArrayComparisionTestCase : TestCaseData
		{
			public ArrayComparisionTestCase(T[] first, T[] second, bool areEquals)
				: base(first, second, areEquals)
			{
			}
		}

		public static string GetPrintableArrayContent(T[] array)
		{
			if (array == null) return "null";
			if (array.Length == 0) return "empty";

			var builder = new StringBuilder();

			builder.AppendFormat("new {0} { ", typeof(T).Name);


			for (var i = 0; i < array.Length; i++)
			{
				var item = array[i];
				builder.Append(string.Format(CultureInfo.InvariantCulture, "{0}", item) + (i < array.Length - 1 ? ", " : string.Empty));
			}

			builder.Append(" }");

			return builder.ToString();
		}

		public static unsafe string GetPrintableArrayHexadecimalContent(T[] array, int tSize)
		{
			if (array == null) return "null";
			if (array.Length == 0) return "empty";


			var gcHandle = GCHandle.Alloc(array, GCHandleType.Pinned);
			try
			{
				byte* start = (byte*) gcHandle.AddrOfPinnedObject().ToPointer();
				byte* ptr = start;

				var builder = new StringBuilder().Append("0x");

				var arrSize = tSize * array.Length;

				for (; ptr < start + arrSize; ptr++)
				{
					builder.Append(Convert.ToString(*ptr, 16).PadLeft(2, '0'));
				}

				return builder.ToString();
			}
			finally
			{
				gcHandle.Free();
			}
		}

		protected ArrayMemoryEqualityComparerTestBase(IEqualityComparer<T[]> equalityComparer)
			: base(equalityComparer)
		{
		}

		protected static T[] Clone(T[] source)
		{
			var result = new T[source.Length];

			for (var i = 0; i < source.Length; i++)
			{
				result[i] = source[i];
			}

			return result;
		}
	}
}