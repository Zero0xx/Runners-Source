using System;

// Token: 0x02000964 RID: 2404
public class CharaSEUtil
{
	// Token: 0x06003EE2 RID: 16098 RVA: 0x00146CA0 File Offset: 0x00144EA0
	private static bool EnableChara(CharaType charaType)
	{
		return CharaType.SONIC <= charaType && charaType < CharaType.NUM;
	}

	// Token: 0x06003EE3 RID: 16099 RVA: 0x00146CB4 File Offset: 0x00144EB4
	public static void PlayJumpSE(CharaType charaType)
	{
		if (CharaSEUtil.EnableChara(charaType))
		{
			SoundManager.SePlay(CharaSEUtil.CHARA_SE_TBL[(int)charaType].m_jump, "SE");
		}
		else
		{
			SoundManager.SePlay("act_jump", "SE");
		}
	}

	// Token: 0x06003EE4 RID: 16100 RVA: 0x00146CF0 File Offset: 0x00144EF0
	public static void Play2ndJumpSE(CharaType charaType)
	{
		if (CharaSEUtil.EnableChara(charaType))
		{
			SoundManager.SePlay(CharaSEUtil.CHARA_SE_TBL[(int)charaType].m_jump2, "SE");
		}
		else
		{
			SoundManager.SePlay("act_2ndjump", "SE");
		}
	}

	// Token: 0x06003EE5 RID: 16101 RVA: 0x00146D2C File Offset: 0x00144F2C
	public static string GetSpinDashSEName(CharaType charaType)
	{
		if (CharaSEUtil.EnableChara(charaType))
		{
			return CharaSEUtil.CHARA_SE_TBL[(int)charaType].m_spin;
		}
		return "act_spindash";
	}

	// Token: 0x06003EE6 RID: 16102 RVA: 0x00146D4C File Offset: 0x00144F4C
	public static void PlaySpinDashSE(CharaType charaType)
	{
		if (CharaSEUtil.EnableChara(charaType))
		{
			SoundManager.SePlay(CharaSEUtil.CHARA_SE_TBL[(int)charaType].m_spin, "SE");
		}
		else
		{
			SoundManager.SePlay("act_spindash", "SE");
		}
	}

	// Token: 0x06003EE7 RID: 16103 RVA: 0x00146D88 File Offset: 0x00144F88
	public static void PlayFlySE(CharaType charaType)
	{
		if (CharaSEUtil.EnableChara(charaType))
		{
			SoundManager.SePlay(CharaSEUtil.CHARA_SE_TBL[(int)charaType].m_fly, "SE");
		}
		else
		{
			SoundManager.SePlay("act_flytype_fly", "SE");
		}
	}

	// Token: 0x06003EE8 RID: 16104 RVA: 0x00146DC4 File Offset: 0x00144FC4
	public static void PlayPowerAttackSE(CharaType charaType)
	{
		if (CharaSEUtil.EnableChara(charaType))
		{
			SoundManager.SePlay(CharaSEUtil.CHARA_SE_TBL[(int)charaType].m_attack, "SE");
		}
		else
		{
			SoundManager.SePlay("act_powertype_attack", "SE");
		}
	}

	// Token: 0x040035DF RID: 13791
	private static readonly CharaSEUtil.CharaSEData[] CHARA_SE_TBL = new CharaSEUtil.CharaSEData[]
	{
		new CharaSEUtil.CharaSEData("act_jump", "act_2ndjump", "act_spindash", "act_flytype_fly", "act_powertype_attack"),
		new CharaSEUtil.CharaSEData("act_jump", "act_2ndjump", "act_spindash", "act_flytype_fly", "act_powertype_attack"),
		new CharaSEUtil.CharaSEData("act_jump", "act_2ndjump", "act_spindash", "act_flytype_fly", "act_powertype_attack"),
		new CharaSEUtil.CharaSEData("act_jump", "act_2ndjump_2", "act_spindash", "act_flytype_fly", "act_powertype_attack"),
		new CharaSEUtil.CharaSEData("act_jump", "act_2ndjump", "act_spindash", "act_flytype_fly", "act_powertype_attack"),
		new CharaSEUtil.CharaSEData("act_jump", "act_2ndjump_2", "act_spindash", "act_flytype_fly", "act_powertype_attack"),
		new CharaSEUtil.CharaSEData("act_jump_large", "act_2ndjump", "act_spindash", "act_flytype_fly", "act_powertype_attack"),
		new CharaSEUtil.CharaSEData("act_jump_large", "act_2ndjump", "act_spindash", "act_flytype_fly", "act_powertype_attack"),
		new CharaSEUtil.CharaSEData("act_jump_large", "act_2ndjump", "act_spindash", "act_flytype_fly", "act_powertype_attack"),
		new CharaSEUtil.CharaSEData("act_jump", "act_2ndjump", "act_spindash", "act_flytype_fly", "act_powertype_attack"),
		new CharaSEUtil.CharaSEData("act_jump", "act_2ndjump", "act_spindash", "act_flytype_fly", "act_powertype_attack"),
		new CharaSEUtil.CharaSEData("act_jump", "act_2ndjump", "act_spindash", "act_flytype_fly", "act_powertype_attack"),
		new CharaSEUtil.CharaSEData("act_jump_large", "act_2ndjump", "act_spindash", "act_flytype_fly", "act_powertype_attack"),
		new CharaSEUtil.CharaSEData("act_jump", "act_2ndjump", "act_spindash", "act_flytype_fly", "act_powertype_attack"),
		new CharaSEUtil.CharaSEData("act_jump", "act_2ndjump", "act_spindash", "act_flytype_fly", "act_powertype_attack"),
		new CharaSEUtil.CharaSEData("act_jump", "act_2ndjump_2", "act_spindash", "act_flytype_fly", "act_powertype_attack"),
		new CharaSEUtil.CharaSEData("act_jump_cla", "act_jump_cla", "act_spindash_cla", "act_flytype_fly", "act_powertype_attack"),
		new CharaSEUtil.CharaSEData("act_jump", "act_2ndjump", "act_spindash", "act_flytype_fly", "act_powertype_attack"),
		new CharaSEUtil.CharaSEData("act_jump", "act_2ndjump_2", "act_spindash", "act_flytype_fly", "act_powertype_attack"),
		new CharaSEUtil.CharaSEData("act_jump", "act_2ndjump", "act_spindash", "act_flytype_fly", "act_powertype_attack"),
		new CharaSEUtil.CharaSEData("act_jump", "act_2ndjump", "act_spindash", "act_flytype_fly", "act_powertype_attack"),
		new CharaSEUtil.CharaSEData("act_jump_large", "act_2ndjump", "act_spindash", "act_flytype_fly", "act_powertype_attack"),
		new CharaSEUtil.CharaSEData("act_jump", "act_2ndjump", "act_spindash", "act_flytype_fly", "act_powertype_attack"),
		new CharaSEUtil.CharaSEData("act_jump", "act_2ndjump", "act_spindash", "act_flight_silver", "act_powertype_attack"),
		new CharaSEUtil.CharaSEData("act_jump", "act_2ndjump", "act_spindash", "act_flytype_fly", "act_powertype_attack"),
		new CharaSEUtil.CharaSEData("act_jump", "act_2ndjump", "act_spindash", "act_flytype_fly", "act_powertype_attack"),
		new CharaSEUtil.CharaSEData("act_jump", "act_2ndjump", "act_spindash", "act_flytype_fly", "act_powertype_attack"),
		new CharaSEUtil.CharaSEData("act_jump", "act_2ndjump", "act_spindash", "act_flytype_fly", "act_powertype_attack"),
		new CharaSEUtil.CharaSEData("act_jump", "act_2ndjump_2", "act_spindash", "act_flytype_fly", "act_powertype_attack")
	};

	// Token: 0x02000965 RID: 2405
	public class CharaSEData
	{
		// Token: 0x06003EE9 RID: 16105 RVA: 0x00146E00 File Offset: 0x00145000
		public CharaSEData(string jump, string jump2, string spin, string fly, string attack)
		{
			this.m_jump = jump;
			this.m_jump2 = jump2;
			this.m_spin = spin;
			this.m_fly = fly;
			this.m_attack = attack;
		}

		// Token: 0x040035E0 RID: 13792
		public string m_jump;

		// Token: 0x040035E1 RID: 13793
		public string m_jump2;

		// Token: 0x040035E2 RID: 13794
		public string m_spin;

		// Token: 0x040035E3 RID: 13795
		public string m_fly;

		// Token: 0x040035E4 RID: 13796
		public string m_attack;
	}
}
