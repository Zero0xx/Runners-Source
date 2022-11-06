using System;
using System.Collections.Generic;
using SaveData;
using UnityEngine;

// Token: 0x0200030B RID: 779
public class StageEffectManager : MonoBehaviour
{
	// Token: 0x17000370 RID: 880
	// (get) Token: 0x060016D2 RID: 5842 RVA: 0x0008357C File Offset: 0x0008177C
	public static StageEffectManager Instance
	{
		get
		{
			return StageEffectManager.instance;
		}
	}

	// Token: 0x060016D3 RID: 5843 RVA: 0x00083584 File Offset: 0x00081784
	public void StockStageEffect(bool bossStage)
	{
		if (SystemSaveManager.GetSystemSaveData() != null)
		{
			this.m_lightMode = SystemSaveManager.GetSystemSaveData().lightMode;
		}
		if (this.m_lightMode)
		{
			return;
		}
		if (ResourceManager.Instance != null)
		{
			for (int i = 0; i < this.EFFECT_DATA_TBL.Length; i++)
			{
				bool flag = false;
				StageEffectManager.EffectData effectData = this.EFFECT_DATA_TBL[i];
				if (!bossStage || effectData.m_bossStage)
				{
					EffectPlayType effectPlayType = (EffectPlayType)i;
					GameObject parentObj = this.m_charaParentObj;
					switch (effectData.m_category)
					{
					case StageEffectManager.EffectCategory.ENEMY:
						parentObj = this.m_enemyParentObj;
						break;
					case StageEffectManager.EffectCategory.OBJECT:
						parentObj = this.m_objectParentObj;
						break;
					case StageEffectManager.EffectCategory.SP_EVENT:
						parentObj = this.m_specialEventParentObj;
						flag = true;
						break;
					}
					StageEffectManager.DepotObjs depotObjs = new StageEffectManager.DepotObjs(parentObj, effectPlayType.ToString());
					for (int j = 0; j < effectData.m_stockCount; j++)
					{
						ResourceCategory category = (!flag) ? ResourceCategory.COMMON_EFFECT : ResourceCategory.EVENT_RESOURCE;
						GameObject gameObject = ResourceManager.Instance.GetGameObject(category, effectData.m_name);
						if (gameObject)
						{
							GameObject gameObject2 = UnityEngine.Object.Instantiate(gameObject, Vector3.zero, Quaternion.identity) as GameObject;
							if (gameObject2 != null)
							{
								gameObject2.name = effectData.m_name;
								gameObject2.SetActive(false);
								EffectShareState effectShareState = gameObject2.AddComponent<EffectShareState>();
								if (effectShareState != null)
								{
									effectShareState.m_effectType = effectPlayType;
								}
								ykKillTime component = gameObject2.GetComponent<ykKillTime>();
								if (component != null)
								{
									component.enabled = false;
								}
								EffectPlayTime effectPlayTime = gameObject2.GetComponent<EffectPlayTime>();
								if (effectPlayTime == null)
								{
									effectPlayTime = gameObject2.AddComponent<EffectPlayTime>();
									if (effectPlayTime != null)
									{
										effectPlayTime.m_endTime = effectData.m_endTime;
									}
								}
								depotObjs.Add(gameObject2);
							}
						}
					}
					this.m_depotObjs.Add(effectPlayType, depotObjs);
				}
			}
		}
	}

	// Token: 0x060016D4 RID: 5844 RVA: 0x0008377C File Offset: 0x0008197C
	public void PlayEffect(EffectPlayType type, Vector3 pos, Quaternion rot)
	{
		if (this.m_lightMode)
		{
			return;
		}
		if (this.m_depotObjs.ContainsKey(type))
		{
			GameObject gameObject = this.m_depotObjs[type].Get();
			if (gameObject != null)
			{
				gameObject.transform.position = pos;
				gameObject.transform.rotation = rot;
				EffectPlayTime component = gameObject.GetComponent<EffectPlayTime>();
				if (component != null)
				{
					component.PlayEffect();
					return;
				}
			}
		}
		if (this.IsCreateEffect(type))
		{
			ObjUtil.PlayEffect(this.EFFECT_DATA_TBL[(int)type].m_name, pos, rot, this.EFFECT_DATA_TBL[(int)type].m_endTime, false);
		}
	}

	// Token: 0x060016D5 RID: 5845 RVA: 0x00083824 File Offset: 0x00081A24
	private bool IsCreateEffect(EffectPlayType type)
	{
		return EffectPlayType.ENEMY_S <= type && type <= EffectPlayType.AIR_TRAP;
	}

	// Token: 0x060016D6 RID: 5846 RVA: 0x00083838 File Offset: 0x00081A38
	public void SleepEffect(GameObject obj)
	{
		if (this.m_lightMode)
		{
			return;
		}
		if (obj != null)
		{
			EffectShareState component = obj.GetComponent<EffectShareState>();
			if (component != null && this.m_depotObjs.ContainsKey(component.m_effectType))
			{
				this.m_depotObjs[component.m_effectType].Sleep(obj);
			}
		}
	}

	// Token: 0x060016D7 RID: 5847 RVA: 0x000838A0 File Offset: 0x00081AA0
	private void Awake()
	{
		this.SetInstance();
	}

	// Token: 0x060016D8 RID: 5848 RVA: 0x000838A8 File Offset: 0x00081AA8
	private void Start()
	{
	}

	// Token: 0x060016D9 RID: 5849 RVA: 0x000838AC File Offset: 0x00081AAC
	private void OnDestroy()
	{
		if (StageEffectManager.instance == this)
		{
			StageEffectManager.instance = null;
		}
	}

	// Token: 0x060016DA RID: 5850 RVA: 0x000838C4 File Offset: 0x00081AC4
	private void SetInstance()
	{
		if (StageEffectManager.instance == null)
		{
			StageEffectManager.instance = this;
		}
		else
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x04001468 RID: 5224
	private static StageEffectManager instance;

	// Token: 0x04001469 RID: 5225
	private StageEffectManager.EffectData[] EFFECT_DATA_TBL = new StageEffectManager.EffectData[]
	{
		new StageEffectManager.EffectData(StageEffectManager.EffectCategory.CHARACTER, "ef_pl_fog_jump_st01", 1f, 3, true),
		new StageEffectManager.EffectData(StageEffectManager.EffectCategory.CHARACTER, "ef_pl_fog_jump_ed01", 1f, 3, true),
		new StageEffectManager.EffectData(StageEffectManager.EffectCategory.CHARACTER, "ef_pl_fog_run01", 1.5f, 8, true),
		new StageEffectManager.EffectData(StageEffectManager.EffectCategory.CHARACTER, "ef_pl_fog_speedrun01", 1.5f, 8, true),
		new StageEffectManager.EffectData(StageEffectManager.EffectCategory.ENEMY, "ef_en_dead_s01", 1f, 10, false),
		new StageEffectManager.EffectData(StageEffectManager.EffectCategory.ENEMY, "ef_en_dead_m01", 1f, 10, false),
		new StageEffectManager.EffectData(StageEffectManager.EffectCategory.ENEMY, "ef_en_dead_l01", 1f, 10, false),
		new StageEffectManager.EffectData(StageEffectManager.EffectCategory.ENEMY, "ef_en_guard01", 1f, 3, false),
		new StageEffectManager.EffectData(StageEffectManager.EffectCategory.ENEMY, "ef_com_explosion_m01", 1f, 10, false),
		new StageEffectManager.EffectData(StageEffectManager.EffectCategory.ENEMY, "ef_com_explosion_s01", 1f, 10, false),
		new StageEffectManager.EffectData(StageEffectManager.EffectCategory.OBJECT, "ef_ob_get_ring01", 1f, 10, true),
		new StageEffectManager.EffectData(StageEffectManager.EffectCategory.OBJECT, "ef_ob_get_superring01", 1f, 10, true),
		new StageEffectManager.EffectData(StageEffectManager.EffectCategory.OBJECT, "ef_ob_get_animal01", 1f, 6, false),
		new StageEffectManager.EffectData(StageEffectManager.EffectCategory.OBJECT, "ef_ob_get_crystal_s01", 1f, 10, false),
		new StageEffectManager.EffectData(StageEffectManager.EffectCategory.OBJECT, "ef_ob_get_crystal_gr_s01", 1f, 10, false),
		new StageEffectManager.EffectData(StageEffectManager.EffectCategory.OBJECT, "ef_ob_get_crystal_rd_s01", 1f, 10, false),
		new StageEffectManager.EffectData(StageEffectManager.EffectCategory.OBJECT, "ef_ob_get_crystal_l01", 1f, 10, false),
		new StageEffectManager.EffectData(StageEffectManager.EffectCategory.OBJECT, "ef_ob_get_crystal_gr_l01", 1f, 10, false),
		new StageEffectManager.EffectData(StageEffectManager.EffectCategory.OBJECT, "ef_ob_get_crystal_rd_l01", 1f, 10, false)
	};

	// Token: 0x0400146A RID: 5226
	[SerializeField]
	private GameObject m_charaParentObj;

	// Token: 0x0400146B RID: 5227
	[SerializeField]
	private GameObject m_enemyParentObj;

	// Token: 0x0400146C RID: 5228
	[SerializeField]
	private GameObject m_objectParentObj;

	// Token: 0x0400146D RID: 5229
	[SerializeField]
	private GameObject m_specialEventParentObj;

	// Token: 0x0400146E RID: 5230
	private Dictionary<EffectPlayType, StageEffectManager.DepotObjs> m_depotObjs = new Dictionary<EffectPlayType, StageEffectManager.DepotObjs>();

	// Token: 0x0400146F RID: 5231
	private bool m_lightMode;

	// Token: 0x0200030C RID: 780
	public enum EffectCategory
	{
		// Token: 0x04001471 RID: 5233
		CHARACTER,
		// Token: 0x04001472 RID: 5234
		ENEMY,
		// Token: 0x04001473 RID: 5235
		OBJECT,
		// Token: 0x04001474 RID: 5236
		SP_EVENT
	}

	// Token: 0x0200030D RID: 781
	public class EffectData
	{
		// Token: 0x060016DB RID: 5851 RVA: 0x000838F8 File Offset: 0x00081AF8
		public EffectData(StageEffectManager.EffectCategory category, string name, float endTime, int stockCount, bool bossStage)
		{
			this.m_category = category;
			this.m_name = name;
			this.m_endTime = endTime;
			this.m_stockCount = stockCount;
			this.m_bossStage = bossStage;
		}

		// Token: 0x04001475 RID: 5237
		public StageEffectManager.EffectCategory m_category;

		// Token: 0x04001476 RID: 5238
		public float m_endTime;

		// Token: 0x04001477 RID: 5239
		public int m_stockCount;

		// Token: 0x04001478 RID: 5240
		public string m_name;

		// Token: 0x04001479 RID: 5241
		public bool m_bossStage;
	}

	// Token: 0x0200030E RID: 782
	public class DepotObjs
	{
		// Token: 0x060016DC RID: 5852 RVA: 0x00083928 File Offset: 0x00081B28
		public DepotObjs(GameObject parentObj, string folderName)
		{
			this.m_folderObj = new GameObject();
			if (this.m_folderObj != null)
			{
				this.m_folderObj.name = folderName;
				if (parentObj != null)
				{
					this.m_folderObj.transform.parent = parentObj.transform;
				}
			}
		}

		// Token: 0x060016DD RID: 5853 RVA: 0x00083990 File Offset: 0x00081B90
		public void Add(GameObject obj)
		{
			if (obj != null)
			{
				this.m_objList.Add(obj);
				if (this.m_folderObj != null)
				{
					obj.transform.parent = this.m_folderObj.transform;
				}
			}
		}

		// Token: 0x060016DE RID: 5854 RVA: 0x000839DC File Offset: 0x00081BDC
		public GameObject Get()
		{
			foreach (GameObject gameObject in this.m_objList)
			{
				EffectShareState component = gameObject.GetComponent<EffectShareState>();
				if (component != null && component.IsSleep())
				{
					component.SetState(EffectShareState.State.Active);
					return gameObject;
				}
			}
			return null;
		}

		// Token: 0x060016DF RID: 5855 RVA: 0x00083A6C File Offset: 0x00081C6C
		public void Sleep(GameObject obj)
		{
			if (obj != null)
			{
				EffectShareState component = obj.GetComponent<EffectShareState>();
				if (component != null)
				{
					component.SetState(EffectShareState.State.Sleep);
					obj.SetActive(false);
					if (this.m_folderObj != null)
					{
						obj.transform.parent = this.m_folderObj.transform;
					}
				}
			}
		}

		// Token: 0x0400147A RID: 5242
		private List<GameObject> m_objList = new List<GameObject>();

		// Token: 0x0400147B RID: 5243
		private GameObject m_folderObj;
	}
}
