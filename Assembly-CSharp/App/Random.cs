using System;
using System.Collections.Generic;

namespace App
{
	// Token: 0x02000A4B RID: 2635
	public class Random
	{
		// Token: 0x06004669 RID: 18025 RVA: 0x0016FDA8 File Offset: 0x0016DFA8
		public static void Shuffle<T>(List<T> list)
		{
			Random random = new Random();
			int i = list.Count;
			while (i > 1)
			{
				i--;
				int index = random.Next(i + 1);
				T value = list[index];
				list[index] = list[i];
				list[i] = value;
			}
		}

		// Token: 0x0600466A RID: 18026 RVA: 0x0016FDFC File Offset: 0x0016DFFC
		public static void ShuffleInt(int[] array)
		{
			Random random = new Random();
			int i = array.Length;
			while (i > 1)
			{
				i--;
				int num = random.Next(i + 1);
				int num2 = array[num];
				array[num] = array[i];
				array[i] = num2;
			}
		}
	}
}
