using System;
using UnityEngine;

// Token: 0x020004C1 RID: 1217
public class AbilityButtonParams
{
	// Token: 0x170004D7 RID: 1239
	// (get) Token: 0x0600240A RID: 9226 RVA: 0x000D82F0 File Offset: 0x000D64F0
	// (set) Token: 0x0600240B RID: 9227 RVA: 0x000D82F8 File Offset: 0x000D64F8
	public CharaType Character
	{
		get
		{
			return this.m_charaType;
		}
		set
		{
			this.m_charaType = value;
		}
	}

	// Token: 0x170004D8 RID: 1240
	// (get) Token: 0x0600240C RID: 9228 RVA: 0x000D8304 File Offset: 0x000D6504
	// (set) Token: 0x0600240D RID: 9229 RVA: 0x000D830C File Offset: 0x000D650C
	public AbilityType Ability
	{
		get
		{
			return this.m_abilityType;
		}
		set
		{
			this.m_abilityType = value;
		}
	}

	// Token: 0x040020A9 RID: 8361
	private CharaType m_charaType;

	// Token: 0x040020AA RID: 8362
	private AbilityType m_abilityType;

	// Token: 0x040020AB RID: 8363
	private GameObject m_buttonObject;
}
