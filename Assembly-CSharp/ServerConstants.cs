using System;

// Token: 0x020007C8 RID: 1992
public class ServerConstants
{
	// Token: 0x04002C38 RID: 11320
	public const int MaxCharacters = 29;

	// Token: 0x04002C39 RID: 11321
	public const int MaxEquipItems = 3;

	// Token: 0x04002C3A RID: 11322
	public const int MaxActiveMissions = 3;

	// Token: 0x04002C3B RID: 11323
	public const int MaxSpecialEgg = 10;

	// Token: 0x04002C3C RID: 11324
	public const int MaxWheelItems = 8;

	// Token: 0x04002C3D RID: 11325
	public const int MaxContinues = 3;

	// Token: 0x04002C3E RID: 11326
	public const int NumberOfRunModifiers = 5;

	// Token: 0x020007C9 RID: 1993
	public enum RunModifierType
	{
		// Token: 0x04002C40 RID: 11328
		SpringBonus,
		// Token: 0x04002C41 RID: 11329
		RingStreak,
		// Token: 0x04002C42 RID: 11330
		EnemyCombo,
		// Token: 0x04002C43 RID: 11331
		FeverTime,
		// Token: 0x04002C44 RID: 11332
		GoldenEnemy
	}

	// Token: 0x020007CA RID: 1994
	public enum NumType
	{
		// Token: 0x04002C46 RID: 11334
		Number,
		// Token: 0x04002C47 RID: 11335
		Coefficient
	}
}
