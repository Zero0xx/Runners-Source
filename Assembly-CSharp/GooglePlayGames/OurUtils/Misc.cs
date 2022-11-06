using System;
using System.Collections.Generic;

namespace GooglePlayGames.OurUtils
{
	// Token: 0x02000032 RID: 50
	public class Misc
	{
		// Token: 0x060001A3 RID: 419 RVA: 0x00005968 File Offset: 0x00003B68
		public static bool BuffersAreIdentical(byte[] a, byte[] b)
		{
			if (a == b)
			{
				return true;
			}
			if (a == null || b == null)
			{
				return false;
			}
			if (a.Length != b.Length)
			{
				return false;
			}
			for (int i = 0; i < a.Length; i++)
			{
				if (a[i] != b[i])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060001A4 RID: 420 RVA: 0x000059BC File Offset: 0x00003BBC
		public static void CopyList<T>(List<T> destList, List<T> sourceList)
		{
			destList.Clear();
			foreach (T item in sourceList)
			{
				destList.Add(item);
			}
		}

		// Token: 0x060001A5 RID: 421 RVA: 0x00005A24 File Offset: 0x00003C24
		public static byte[] GetSubsetBytes(byte[] array, int offset, int length)
		{
			if (offset == 0 && length == array.Length)
			{
				return array;
			}
			byte[] array2 = new byte[length];
			Array.Copy(array, offset, array2, 0, length);
			return array2;
		}
	}
}
