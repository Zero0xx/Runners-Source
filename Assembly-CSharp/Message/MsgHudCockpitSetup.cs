using System;

namespace Message
{
	// Token: 0x020005B4 RID: 1460
	public class MsgHudCockpitSetup : MessageBase
	{
		// Token: 0x06002B23 RID: 11043 RVA: 0x00109CEC File Offset: 0x00107EEC
		public MsgHudCockpitSetup(BossType bossType, bool spCrystal, bool animal, bool backMainMenuCheck, bool firstTutorial) : base(49155)
		{
			this.m_bossType = bossType;
			this.m_spCrystal = spCrystal;
			this.m_animal = animal;
			this.m_backMainMenuCheck = backMainMenuCheck;
			this.m_firstTutorial = firstTutorial;
		}

		// Token: 0x040027A3 RID: 10147
		public BossType m_bossType;

		// Token: 0x040027A4 RID: 10148
		public bool m_spCrystal;

		// Token: 0x040027A5 RID: 10149
		public bool m_animal;

		// Token: 0x040027A6 RID: 10150
		public bool m_backMainMenuCheck;

		// Token: 0x040027A7 RID: 10151
		public bool m_firstTutorial;
	}
}
