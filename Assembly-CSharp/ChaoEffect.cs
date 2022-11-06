using System;
using System.Collections.Generic;
using DataTable;
using UnityEngine;

// Token: 0x0200012C RID: 300
public class ChaoEffect : MonoBehaviour
{
	// Token: 0x17000171 RID: 369
	// (get) Token: 0x060008FD RID: 2301 RVA: 0x00033350 File Offset: 0x00031550
	public static ChaoEffect Instance
	{
		get
		{
			return ChaoEffect.instance;
		}
	}

	// Token: 0x060008FE RID: 2302 RVA: 0x00033358 File Offset: 0x00031558
	protected void Awake()
	{
		this.SetInstance();
	}

	// Token: 0x060008FF RID: 2303 RVA: 0x00033360 File Offset: 0x00031560
	private void Start()
	{
		base.enabled = false;
	}

	// Token: 0x06000900 RID: 2304 RVA: 0x0003336C File Offset: 0x0003156C
	private void OnDestroy()
	{
		if (ChaoEffect.instance == this)
		{
			ChaoEffect.instance = null;
		}
	}

	// Token: 0x06000901 RID: 2305 RVA: 0x00033384 File Offset: 0x00031584
	private void SetInstance()
	{
		if (ChaoEffect.instance == null)
		{
			ChaoEffect.instance = this;
		}
		else if (this != ChaoEffect.Instance)
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x06000902 RID: 2306 RVA: 0x000333C8 File Offset: 0x000315C8
	private ChaoData.Rarity GetRarity(ChaoType chaotype)
	{
		SaveDataManager saveDataManager = SaveDataManager.Instance;
		if (saveDataManager != null)
		{
			int id = (chaotype != ChaoType.MAIN) ? saveDataManager.PlayerData.SubChaoID : saveDataManager.PlayerData.MainChaoID;
			ChaoData chaoData = ChaoTable.GetChaoData(id);
			if (chaoData != null)
			{
				return chaoData.rarity;
			}
		}
		return ChaoData.Rarity.NORMAL;
	}

	// Token: 0x06000903 RID: 2307 RVA: 0x00033420 File Offset: 0x00031620
	private string GetEffectName(ChaoData.Rarity rarity, ChaoAbility ability)
	{
		if (ability == ChaoAbility.UNKNOWN || ability >= ChaoAbility.NUM)
		{
			return null;
		}
		if (rarity == ChaoData.Rarity.NORMAL)
		{
			return this.m_effectTable[(int)ability].m_normal;
		}
		if (rarity == ChaoData.Rarity.RARE)
		{
			return this.m_effectTable[(int)ability].m_rare;
		}
		return this.m_effectTable[(int)ability].m_srare;
	}

	// Token: 0x06000904 RID: 2308 RVA: 0x00033474 File Offset: 0x00031674
	private float GetEffectPlayTime(ChaoAbility ability)
	{
		if (ability != ChaoAbility.UNKNOWN && ability < ChaoAbility.NUM)
		{
			return this.m_effectTable[(int)ability].m_time;
		}
		return 0f;
	}

	// Token: 0x06000905 RID: 2309 RVA: 0x000334A4 File Offset: 0x000316A4
	private bool GetEffectPlayLoop(ChaoAbility ability)
	{
		return ability != ChaoAbility.UNKNOWN && ability < ChaoAbility.NUM && this.m_effectTable[(int)ability].m_loop;
	}

	// Token: 0x06000906 RID: 2310 RVA: 0x000334C4 File Offset: 0x000316C4
	private void PlayEffect(GameObject chaoObj, ChaoAbility ability, ChaoType chaoType)
	{
		if (chaoObj != null)
		{
			string effectName = this.GetEffectName(this.GetRarity(chaoType), ability);
			if (effectName != null)
			{
				if (this.GetEffectPlayLoop(ability))
				{
					GameObject gameObject = ObjUtil.PlayChaoEffect(chaoObj, effectName, -1f);
					if (gameObject != null)
					{
						ChaoEffect.LoopEffetData item = new ChaoEffect.LoopEffetData(gameObject, ability, chaoType);
						this.m_loopDataList.Add(item);
					}
				}
				else
				{
					float effectPlayTime = this.GetEffectPlayTime(ability);
					ObjUtil.PlayChaoEffect(chaoObj, effectName, effectPlayTime);
				}
			}
		}
	}

	// Token: 0x06000907 RID: 2311 RVA: 0x00033544 File Offset: 0x00031744
	private void StopEffect(ChaoAbility ability, ChaoType chaoType)
	{
		foreach (ChaoEffect.LoopEffetData loopEffetData in this.m_loopDataList)
		{
			if (loopEffetData.m_ability == ability && loopEffetData.m_chaoType == chaoType)
			{
				UnityEngine.Object.Destroy(loopEffetData.m_obj);
				this.m_loopDataList.Remove(loopEffetData);
				break;
			}
		}
	}

	// Token: 0x06000908 RID: 2312 RVA: 0x000335D8 File Offset: 0x000317D8
	private void PlayChaoSE()
	{
		SoundManager.SePlay("act_chao_effect", "SE");
	}

	// Token: 0x06000909 RID: 2313 RVA: 0x000335EC File Offset: 0x000317EC
	public void RequestPlayChaoEffect(ChaoEffect.TargetType target, ChaoAbility ability)
	{
		if (ability != ChaoAbility.UNKNOWN && ability < ChaoAbility.NUM)
		{
			switch (target)
			{
			case ChaoEffect.TargetType.MainChao:
				this.PlayEffect(this.GetChaoObject(ChaoType.MAIN), ability, ChaoType.MAIN);
				break;
			case ChaoEffect.TargetType.SubChao:
				this.PlayEffect(this.GetChaoObject(ChaoType.SUB), ability, ChaoType.SUB);
				break;
			case ChaoEffect.TargetType.BothChao:
				this.PlayEffect(this.GetChaoObject(ChaoType.MAIN), ability, ChaoType.MAIN);
				this.PlayEffect(this.GetChaoObject(ChaoType.SUB), ability, ChaoType.SUB);
				break;
			}
			this.PlayChaoSE();
		}
	}

	// Token: 0x0600090A RID: 2314 RVA: 0x00033674 File Offset: 0x00031874
	public void RequestStopChaoEffect(ChaoEffect.TargetType target, ChaoAbility ability)
	{
		if (ability != ChaoAbility.UNKNOWN && ability < ChaoAbility.NUM)
		{
			if (target == ChaoEffect.TargetType.MainChao)
			{
				this.StopEffect(ability, ChaoType.MAIN);
			}
			else if (target == ChaoEffect.TargetType.SubChao)
			{
				this.StopEffect(ability, ChaoType.SUB);
			}
			else if (target == ChaoEffect.TargetType.BothChao)
			{
				this.StopEffect(ability, ChaoType.MAIN);
				this.StopEffect(ability, ChaoType.SUB);
			}
		}
	}

	// Token: 0x0600090B RID: 2315 RVA: 0x000336D0 File Offset: 0x000318D0
	private GameObject GetChaoObject(ChaoType chaotype)
	{
		Transform parent = base.transform.parent;
		if (!(parent != null))
		{
			return null;
		}
		GameObject gameObject = GameObjectUtil.FindChildGameObject(parent.gameObject, this.ChaoTypeName[(int)chaotype]);
		if (gameObject != null && gameObject.activeSelf)
		{
			return gameObject;
		}
		return null;
	}

	// Token: 0x040006AB RID: 1707
	public readonly ChaoEffect.DataInfo[] m_effectTable = new ChaoEffect.DataInfo[]
	{
		new ChaoEffect.DataInfo("ef_ch_bonus_all_01", null, "ef_ch_bonus_all_sr01", -1f, false),
		new ChaoEffect.DataInfo("ef_ch_bonus_score_01", "ef_ch_bonus_score_r01", null, -1f, false),
		new ChaoEffect.DataInfo("ef_ch_bonus_ring_01", "ef_ch_bonus_ring_r01", "ef_ch_bonus_ring_sr01", -1f, false),
		new ChaoEffect.DataInfo(),
		new ChaoEffect.DataInfo("ef_ch_bonus_animal_01", "ef_ch_bonus_animal_r01", null, -1f, false),
		new ChaoEffect.DataInfo("ef_ch_bonus_run_01", "ef_ch_bonus_run_r01", null, -1f, false),
		new ChaoEffect.DataInfo(null, "ef_ch_sp_score_spitem_r01", "ef_ch_sp_score_spitem_sr01", -1f, false),
		new ChaoEffect.DataInfo("ef_ch_raid_up_ring_c01", "ef_ch_raid_up_ring_r01", "ef_ch_raid_up_ring_sr01", -1f, false),
		new ChaoEffect.DataInfo(),
		new ChaoEffect.DataInfo(null, "ef_ch_combo_crystal_s_r01", "ef_ch_combo_crystal_s_sr01", -1f, false),
		new ChaoEffect.DataInfo(null, "ef_ch_combo_crystal_s_r01", null, -1f, false),
		new ChaoEffect.DataInfo(null, "ef_ch_combo_crystal_l_r01", "ef_ch_combo_crystal_l_sr01", -1f, false),
		new ChaoEffect.DataInfo(null, "ef_ch_combo_crystal_l_r01", null, -1f, false),
		new ChaoEffect.DataInfo(null, "ef_ch_combo_enemy_g_r01", "ef_ch_combo_enemy_g_sr01", -1f, false),
		new ChaoEffect.DataInfo(),
		new ChaoEffect.DataInfo(null, "ef_ch_combo_animal_r01", null, -1f, false),
		new ChaoEffect.DataInfo(null, "ef_ch_bomber_bullet_r01", null, 6f, true),
		new ChaoEffect.DataInfo(),
		new ChaoEffect.DataInfo(null, "ef_ch_combo_ring10_r01", null, -1f, false),
		new ChaoEffect.DataInfo(null, "ef_ch_combo_combo_up_r01", null, -1f, false),
		new ChaoEffect.DataInfo("ef_ch_sp_combo_crystal_sp_c01", "ef_ch_sp_combo_crystal_sp_r01", null, -1f, false),
		new ChaoEffect.DataInfo("ef_ch_combo_brk_trap_c01", null, null, -1f, false),
		new ChaoEffect.DataInfo(),
		new ChaoEffect.DataInfo(),
		new ChaoEffect.DataInfo(),
		new ChaoEffect.DataInfo(),
		new ChaoEffect.DataInfo(null, null, "ef_ch_score_cpower_sr01", -1f, false),
		new ChaoEffect.DataInfo(null, "ef_ch_score_asteroid_r01", null, -1f, false),
		new ChaoEffect.DataInfo(null, "ef_ch_score_drill_r01", null, -1f, false),
		new ChaoEffect.DataInfo(null, "ef_ch_score_laser_r01", null, -1f, false),
		new ChaoEffect.DataInfo(null, "ef_ch_time_cpower_r01", null, -1f, false),
		new ChaoEffect.DataInfo(null, "ef_ch_time_item_r01", null, -1f, false),
		new ChaoEffect.DataInfo("ef_ch_time_combo_c01", "ef_ch_time_combo_r01", null, -1f, false),
		new ChaoEffect.DataInfo("ef_ch_time_trampoline_c01", "ef_ch_time_trampoline_r01", null, -1f, false),
		new ChaoEffect.DataInfo("ef_ch_time_magnet_c01", "ef_ch_time_magnet_r01", null, -1f, false),
		new ChaoEffect.DataInfo("ef_ch_time_asteroid_01", null, null, -1f, false),
		new ChaoEffect.DataInfo("ef_ch_time_drill_01", null, null, -1f, false),
		new ChaoEffect.DataInfo("ef_ch_time_laser_01", null, null, -1f, false),
		new ChaoEffect.DataInfo(null, null, "ef_ch_magic_atk_st_sr01", -1f, false),
		new ChaoEffect.DataInfo(),
		new ChaoEffect.DataInfo(null, null, "ef_ch_beam_atk_st_sr01", -1f, false),
		new ChaoEffect.DataInfo(null, null, "ef_ch_beam_atk_st_sr02", -1f, false),
		new ChaoEffect.DataInfo("ef_ch_up_rareenemy_c01", "ef_ch_up_rareenemy_sr01", "ef_ch_up_rareenemy_sr01", -1f, false),
		new ChaoEffect.DataInfo("ef_ch_sp_up_spitem_c01", "ef_ch_sp_up_spitem_r01", "ef_ch_sp_up_spitem_sr01", -1f, false),
		new ChaoEffect.DataInfo(null, null, "ef_ch_lastchance_sr01", 1f, true),
		new ChaoEffect.DataInfo("ef_ch_ring_absorb_c01", "ef_ch_ring_absorb_r01", "ef_ch_ring_absorb_sr01", -1f, false),
		new ChaoEffect.DataInfo(),
		new ChaoEffect.DataInfo(null, null, "ef_ch_magic_atk_st_sr01", -1f, false),
		new ChaoEffect.DataInfo("ef_ch_check_combo_01", "ef_ch_check_combo_r01", null, -1f, false),
		new ChaoEffect.DataInfo(),
		new ChaoEffect.DataInfo("ef_ch_random_magnet_01", null, null, -1f, false),
		new ChaoEffect.DataInfo("ef_ch_random_jump_01", null, null, -1f, false),
		new ChaoEffect.DataInfo("ef_ch_bonus_rsr_01", null, "ef_ch_bonus_rsr_sr01", -1f, false),
		new ChaoEffect.DataInfo(null, "ef_ch_up_magnet_r01", null, -1f, false),
		new ChaoEffect.DataInfo(null, "ef_ch_raid_up_atk_r01", null, -1f, false),
		new ChaoEffect.DataInfo("ef_ch_canon_magnet_c01", "ef_ch_canon_magnet_r01", null, -1f, false),
		new ChaoEffect.DataInfo("ef_ch_dashring_magnet_c01", null, null, -1f, false),
		new ChaoEffect.DataInfo("ef_ch_jumpboard_magnet_c01", "ef_ch_jumpboard_magnet_r01", null, -1f, false),
		new ChaoEffect.DataInfo(),
		new ChaoEffect.DataInfo(),
		new ChaoEffect.DataInfo(null, "ef_ch_change_rareanimal_r01", null, -1f, false),
		new ChaoEffect.DataInfo(null, "ef_ch_change_rappy_r01", null, -1f, false),
		new ChaoEffect.DataInfo(),
		new ChaoEffect.DataInfo(),
		new ChaoEffect.DataInfo(),
		new ChaoEffect.DataInfo(),
		new ChaoEffect.DataInfo(),
		new ChaoEffect.DataInfo(),
		new ChaoEffect.DataInfo(),
		new ChaoEffect.DataInfo(),
		new ChaoEffect.DataInfo(),
		new ChaoEffect.DataInfo(),
		new ChaoEffect.DataInfo(),
		new ChaoEffect.DataInfo(),
		new ChaoEffect.DataInfo(),
		new ChaoEffect.DataInfo(),
		new ChaoEffect.DataInfo(),
		new ChaoEffect.DataInfo(),
		new ChaoEffect.DataInfo(),
		new ChaoEffect.DataInfo(),
		new ChaoEffect.DataInfo(),
		new ChaoEffect.DataInfo(),
		new ChaoEffect.DataInfo(),
		new ChaoEffect.DataInfo(),
		new ChaoEffect.DataInfo(),
		new ChaoEffect.DataInfo(),
		new ChaoEffect.DataInfo(),
		new ChaoEffect.DataInfo(),
		new ChaoEffect.DataInfo(),
		new ChaoEffect.DataInfo(),
		new ChaoEffect.DataInfo(),
		new ChaoEffect.DataInfo(),
		new ChaoEffect.DataInfo(),
		new ChaoEffect.DataInfo()
	};

	// Token: 0x040006AC RID: 1708
	private List<ChaoEffect.LoopEffetData> m_loopDataList = new List<ChaoEffect.LoopEffetData>();

	// Token: 0x040006AD RID: 1709
	private readonly string[] ChaoTypeName = new string[]
	{
		"MainChao",
		"SubChao"
	};

	// Token: 0x040006AE RID: 1710
	private static ChaoEffect instance;

	// Token: 0x0200012D RID: 301
	public enum TargetType
	{
		// Token: 0x040006B0 RID: 1712
		MainChao,
		// Token: 0x040006B1 RID: 1713
		SubChao,
		// Token: 0x040006B2 RID: 1714
		BothChao,
		// Token: 0x040006B3 RID: 1715
		Unknown
	}

	// Token: 0x0200012E RID: 302
	public class DataInfo
	{
		// Token: 0x0600090C RID: 2316 RVA: 0x00033728 File Offset: 0x00031928
		public DataInfo()
		{
			this.m_normal = null;
			this.m_rare = null;
			this.m_srare = null;
			this.m_time = 0f;
			this.m_loop = false;
		}

		// Token: 0x0600090D RID: 2317 RVA: 0x00033758 File Offset: 0x00031958
		public DataInfo(string normal, string rare, string srare, float time, bool loop)
		{
			this.m_normal = normal;
			this.m_rare = rare;
			this.m_srare = srare;
			this.m_time = time;
			this.m_loop = loop;
		}

		// Token: 0x040006B4 RID: 1716
		public string m_normal;

		// Token: 0x040006B5 RID: 1717
		public string m_rare;

		// Token: 0x040006B6 RID: 1718
		public string m_srare;

		// Token: 0x040006B7 RID: 1719
		public float m_time;

		// Token: 0x040006B8 RID: 1720
		public bool m_loop;
	}

	// Token: 0x0200012F RID: 303
	public class LoopEffetData
	{
		// Token: 0x0600090E RID: 2318 RVA: 0x00033788 File Offset: 0x00031988
		public LoopEffetData(GameObject obj, ChaoAbility ability, ChaoType chaoType)
		{
			this.m_obj = obj;
			this.m_ability = ability;
			this.m_chaoType = chaoType;
		}

		// Token: 0x040006B9 RID: 1721
		public GameObject m_obj;

		// Token: 0x040006BA RID: 1722
		public ChaoAbility m_ability;

		// Token: 0x040006BB RID: 1723
		public ChaoType m_chaoType;
	}
}
