using System;

// Token: 0x020003B1 RID: 945
[Serializable]
public class UIInvStat
{
	// Token: 0x06001B98 RID: 7064 RVA: 0x000A2F84 File Offset: 0x000A1184
	public static string GetName(UIInvStat.Identifier i)
	{
		return i.ToString();
	}

	// Token: 0x06001B99 RID: 7065 RVA: 0x000A2F94 File Offset: 0x000A1194
	public static string GetDescription(UIInvStat.Identifier i)
	{
		switch (i)
		{
		case UIInvStat.Identifier.Strength:
			return "Strength increases melee damage";
		case UIInvStat.Identifier.Constitution:
			return "Constitution increases health";
		case UIInvStat.Identifier.Agility:
			return "Agility increases armor";
		case UIInvStat.Identifier.Intelligence:
			return "Intelligence increases mana";
		case UIInvStat.Identifier.Damage:
			return "Damage adds to the amount of damage done in combat";
		case UIInvStat.Identifier.Crit:
			return "Crit increases the chance of landing a critical strike";
		case UIInvStat.Identifier.Armor:
			return "Armor protects from damage";
		case UIInvStat.Identifier.Health:
			return "Health prolongs life";
		case UIInvStat.Identifier.Mana:
			return "Mana increases the number of spells that can be cast";
		default:
			return null;
		}
	}

	// Token: 0x06001B9A RID: 7066 RVA: 0x000A300C File Offset: 0x000A120C
	public static int CompareArmor(UIInvStat a, UIInvStat b)
	{
		int num = (int)a.id;
		int num2 = (int)b.id;
		if (a.id == UIInvStat.Identifier.Armor)
		{
			num -= 10000;
		}
		else if (a.id == UIInvStat.Identifier.Damage)
		{
			num -= 5000;
		}
		if (b.id == UIInvStat.Identifier.Armor)
		{
			num2 -= 10000;
		}
		else if (b.id == UIInvStat.Identifier.Damage)
		{
			num2 -= 5000;
		}
		if (a.amount < 0)
		{
			num += 1000;
		}
		if (b.amount < 0)
		{
			num2 += 1000;
		}
		if (a.modifier == UIInvStat.Modifier.Percent)
		{
			num += 100;
		}
		if (b.modifier == UIInvStat.Modifier.Percent)
		{
			num2 += 100;
		}
		if (num < num2)
		{
			return -1;
		}
		if (num > num2)
		{
			return 1;
		}
		return 0;
	}

	// Token: 0x06001B9B RID: 7067 RVA: 0x000A30E0 File Offset: 0x000A12E0
	public static int CompareWeapon(UIInvStat a, UIInvStat b)
	{
		int num = (int)a.id;
		int num2 = (int)b.id;
		if (a.id == UIInvStat.Identifier.Damage)
		{
			num -= 10000;
		}
		else if (a.id == UIInvStat.Identifier.Armor)
		{
			num -= 5000;
		}
		if (b.id == UIInvStat.Identifier.Damage)
		{
			num2 -= 10000;
		}
		else if (b.id == UIInvStat.Identifier.Armor)
		{
			num2 -= 5000;
		}
		if (a.amount < 0)
		{
			num += 1000;
		}
		if (b.amount < 0)
		{
			num2 += 1000;
		}
		if (a.modifier == UIInvStat.Modifier.Percent)
		{
			num += 100;
		}
		if (b.modifier == UIInvStat.Modifier.Percent)
		{
			num2 += 100;
		}
		if (num < num2)
		{
			return -1;
		}
		if (num > num2)
		{
			return 1;
		}
		return 0;
	}

	// Token: 0x0400192F RID: 6447
	public UIInvStat.Identifier id;

	// Token: 0x04001930 RID: 6448
	public UIInvStat.Modifier modifier;

	// Token: 0x04001931 RID: 6449
	public int amount;

	// Token: 0x020003B2 RID: 946
	public enum Identifier
	{
		// Token: 0x04001933 RID: 6451
		Strength,
		// Token: 0x04001934 RID: 6452
		Constitution,
		// Token: 0x04001935 RID: 6453
		Agility,
		// Token: 0x04001936 RID: 6454
		Intelligence,
		// Token: 0x04001937 RID: 6455
		Damage,
		// Token: 0x04001938 RID: 6456
		Crit,
		// Token: 0x04001939 RID: 6457
		Armor,
		// Token: 0x0400193A RID: 6458
		Health,
		// Token: 0x0400193B RID: 6459
		Mana,
		// Token: 0x0400193C RID: 6460
		Other
	}

	// Token: 0x020003B3 RID: 947
	public enum Modifier
	{
		// Token: 0x0400193E RID: 6462
		Added,
		// Token: 0x0400193F RID: 6463
		Percent
	}
}
