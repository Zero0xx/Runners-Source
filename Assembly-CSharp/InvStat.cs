using System;

// Token: 0x0200005A RID: 90
[Serializable]
public class InvStat
{
	// Token: 0x060002C1 RID: 705 RVA: 0x0000C17C File Offset: 0x0000A37C
	public static string GetName(InvStat.Identifier i)
	{
		return i.ToString();
	}

	// Token: 0x060002C2 RID: 706 RVA: 0x0000C18C File Offset: 0x0000A38C
	public static string GetDescription(InvStat.Identifier i)
	{
		switch (i)
		{
		case InvStat.Identifier.Strength:
			return "Strength increases melee damage";
		case InvStat.Identifier.Constitution:
			return "Constitution increases health";
		case InvStat.Identifier.Agility:
			return "Agility increases armor";
		case InvStat.Identifier.Intelligence:
			return "Intelligence increases mana";
		case InvStat.Identifier.Damage:
			return "Damage adds to the amount of damage done in combat";
		case InvStat.Identifier.Crit:
			return "Crit increases the chance of landing a critical strike";
		case InvStat.Identifier.Armor:
			return "Armor protects from damage";
		case InvStat.Identifier.Health:
			return "Health prolongs life";
		case InvStat.Identifier.Mana:
			return "Mana increases the number of spells that can be cast";
		default:
			return null;
		}
	}

	// Token: 0x060002C3 RID: 707 RVA: 0x0000C204 File Offset: 0x0000A404
	public static int CompareArmor(InvStat a, InvStat b)
	{
		int num = (int)a.id;
		int num2 = (int)b.id;
		if (a.id == InvStat.Identifier.Armor)
		{
			num -= 10000;
		}
		else if (a.id == InvStat.Identifier.Damage)
		{
			num -= 5000;
		}
		if (b.id == InvStat.Identifier.Armor)
		{
			num2 -= 10000;
		}
		else if (b.id == InvStat.Identifier.Damage)
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
		if (a.modifier == InvStat.Modifier.Percent)
		{
			num += 100;
		}
		if (b.modifier == InvStat.Modifier.Percent)
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

	// Token: 0x060002C4 RID: 708 RVA: 0x0000C2D8 File Offset: 0x0000A4D8
	public static int CompareWeapon(InvStat a, InvStat b)
	{
		int num = (int)a.id;
		int num2 = (int)b.id;
		if (a.id == InvStat.Identifier.Damage)
		{
			num -= 10000;
		}
		else if (a.id == InvStat.Identifier.Armor)
		{
			num -= 5000;
		}
		if (b.id == InvStat.Identifier.Damage)
		{
			num2 -= 10000;
		}
		else if (b.id == InvStat.Identifier.Armor)
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
		if (a.modifier == InvStat.Modifier.Percent)
		{
			num += 100;
		}
		if (b.modifier == InvStat.Modifier.Percent)
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

	// Token: 0x04000167 RID: 359
	public InvStat.Identifier id;

	// Token: 0x04000168 RID: 360
	public InvStat.Modifier modifier;

	// Token: 0x04000169 RID: 361
	public int amount;

	// Token: 0x0200005B RID: 91
	public enum Identifier
	{
		// Token: 0x0400016B RID: 363
		Strength,
		// Token: 0x0400016C RID: 364
		Constitution,
		// Token: 0x0400016D RID: 365
		Agility,
		// Token: 0x0400016E RID: 366
		Intelligence,
		// Token: 0x0400016F RID: 367
		Damage,
		// Token: 0x04000170 RID: 368
		Crit,
		// Token: 0x04000171 RID: 369
		Armor,
		// Token: 0x04000172 RID: 370
		Health,
		// Token: 0x04000173 RID: 371
		Mana,
		// Token: 0x04000174 RID: 372
		Other
	}

	// Token: 0x0200005C RID: 92
	public enum Modifier
	{
		// Token: 0x04000176 RID: 374
		Added,
		// Token: 0x04000177 RID: 375
		Percent
	}
}
