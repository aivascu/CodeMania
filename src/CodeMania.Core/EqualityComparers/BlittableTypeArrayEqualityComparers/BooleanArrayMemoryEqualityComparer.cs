﻿using CodeMania.Core.Internals;
using JetBrains.Annotations;

namespace CodeMania.Core.EqualityComparers.BlittableTypeArrayEqualityComparers
{
	[PublicAPI]
	public sealed unsafe class BooleanArrayMemoryEqualityComparer : BlittableTypeArrayEqualityComparerBase<bool>
	{
		public override bool Equals(bool[] x, bool[] y)
		{
			if (x == null) return y == null;
			if (ReferenceEquals(x, y)) return true;

			if (y != null && x.Length == y.Length)
			{
				if (x.Length == 0 && y.Length == 0) return true;

				fixed (void* xPtr = &x[0])
				fixed (void* yPtr = &y[0])
				{
					return UnsafeNativeMethods.Memcmp(xPtr, yPtr, x.LongLength * sizeof(bool)) == 0;
				}
			}

			return false;
		}

		public override int GetHashCode(bool[] obj)
		{
			if (obj == null || obj.Length == 0) return 0;

			fixed (void* startAddr = &obj[0])
			{
				return GetHashCode((byte*) startAddr, sizeof(bool) * obj.Length);
			}
		}
	}
}