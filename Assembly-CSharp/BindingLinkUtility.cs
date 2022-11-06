using System;

// Token: 0x02000108 RID: 264
public class BindingLinkUtility
{
	// Token: 0x060007DA RID: 2010 RVA: 0x0002EE38 File Offset: 0x0002D038
	public static void LongToIntArray(out int[] output, long input)
	{
		output = new int[2];
		int num = 32;
		int num2 = (int)(input >> num);
		int num3 = (int)(input << num >> num);
		output[0] = num2;
		output[1] = num3;
	}

	// Token: 0x040005F6 RID: 1526
	public const int MaxEquipItems = 6;

	// Token: 0x040005F7 RID: 1527
	public const int MaxFriends = 50;

	// Token: 0x040005F8 RID: 1528
	public const int MaxApolloParamCount = 100;
}
