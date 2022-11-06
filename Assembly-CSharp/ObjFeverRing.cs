using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020008FF RID: 2303
public class ObjFeverRing : MonoBehaviour
{
	// Token: 0x06003CC3 RID: 15555 RVA: 0x0013F3FC File Offset: 0x0013D5FC
	private void Update()
	{
		if (this.m_count > 0)
		{
			if (this.m_createCount < this.m_count)
			{
				this.CreateRing(this.m_createCount, Mathf.Min(this.m_createCount + 5, this.m_count));
			}
			this.m_time += Time.deltaTime;
			if (this.m_time > 6f)
			{
				foreach (SpawnableObject spawnableObject in this.m_rivivalObj)
				{
					if (spawnableObject.Share)
					{
						spawnableObject.Sleep = true;
						GameObject gameObject = GameObjectUtil.FindChildGameObject(spawnableObject.gameObject, "MotorThrow(Clone)");
						if (gameObject != null)
						{
							UnityEngine.Object.Destroy(gameObject);
						}
						spawnableObject.SetSleep();
					}
				}
				UnityEngine.Object.Destroy(base.gameObject);
			}
		}
	}

	// Token: 0x06003CC4 RID: 15556 RVA: 0x0013F500 File Offset: 0x0013D700
	public void Setup(int ringCount, int in_superRing, int in_redStarRing, int in_bronzeTimer, int in_silverTimer, int in_goldTimer, BossType type)
	{
		this.m_playerInformation = ObjUtil.GetPlayerInformation();
		this.m_levelInformation = ObjUtil.GetLevelInformation();
		this.m_stageBlockManager = GameObjectUtil.FindGameObjectWithTag("StageManager", "StageBlockManager");
		int num = in_redStarRing;
		if (StageModeManager.Instance != null && StageModeManager.Instance.FirstTutorial)
		{
			num = 0;
		}
		this.m_bossType = (int)type;
		this.m_add_player_speed = ObjUtil.GetPlayerAddSpeed();
		this.m_time = 0f;
		this.m_count = ringCount;
		if (this.m_count > 0)
		{
			this.m_info = new ObjFeverRing.FeverRingInfo[this.m_count];
			int num2 = UnityEngine.Random.Range(0, ObjFeverRing.FEVERRING_PARAM.Length);
			float num3 = 0f;
			int num4 = num2;
			int num5 = num2;
			for (int i = 0; i < this.m_count; i++)
			{
				this.m_info[i].m_type = ObjFeverRing.Type.RING;
				this.m_info[i].m_param = new ObjFeverRing.FeverRingParam(0f, 0f);
				if (num4 >= ObjFeverRing.FEVERRING_PARAM.Length)
				{
					num3 = (float)(num5 / ObjFeverRing.FEVERRING_PARAM.Length);
					num4 = 0;
				}
				this.m_info[i].m_param.m_add_x = ObjFeverRing.FEVERRING_PARAM[num4].m_add_x + num3 * 0.05f;
				this.m_info[i].m_param.m_add_y = ObjFeverRing.FEVERRING_PARAM[num4].m_add_y + num3 * 0.05f;
				switch (this.m_bossType)
				{
				case 1:
				case 3:
					this.m_info[i].m_param.m_add_x *= 0.5f;
					break;
				}
				num4++;
				num5++;
			}
			float num6 = (float)in_superRing * 0.01f;
			int num7 = (int)((float)this.m_count * num6);
			num7 = Mathf.Min(num7, this.m_info.Length);
			for (int j = 0; j < num7; j++)
			{
				this.m_info[j].m_type = ObjFeverRing.Type.SUPER_RING;
			}
			if (this.IsEnableCreateTimer() && this.m_info.Length > 1)
			{
				int randomRange = ObjUtil.GetRandomRange100();
				int num8 = in_bronzeTimer + in_silverTimer;
				int num9 = num8 + in_goldTimer;
				if (randomRange < in_bronzeTimer)
				{
					this.m_info[this.m_info.Length - 2].m_type = ObjFeverRing.Type.BRONZE_TIMER;
				}
				else if (randomRange < num8)
				{
					this.m_info[this.m_info.Length - 2].m_type = ObjFeverRing.Type.SILVER_TIMER;
				}
				else if (randomRange < num9)
				{
					this.m_info[this.m_info.Length - 2].m_type = ObjFeverRing.Type.GOLD_TIMER;
				}
			}
			int randomRange2 = ObjUtil.GetRandomRange100();
			if (randomRange2 < num)
			{
				this.m_info[this.m_info.Length - 1].m_type = ObjFeverRing.Type.REDSTAR_RING;
			}
			this.CreateRing(0, Mathf.Min(5, this.m_count));
		}
	}

	// Token: 0x06003CC5 RID: 15557 RVA: 0x0013F80C File Offset: 0x0013DA0C
	private bool IsEnableCreateTimer()
	{
		return StageModeManager.Instance != null && StageModeManager.Instance.IsQuickMode() && ObjTimerUtil.IsEnableCreateTimer();
	}

	// Token: 0x06003CC6 RID: 15558 RVA: 0x0013F840 File Offset: 0x0013DA40
	private void CreateRing(int startCount, int endCount)
	{
		if (endCount > this.m_info.Length)
		{
			return;
		}
		GameObject gameObject = ResourceManager.Instance.GetGameObject(ResourceCategory.OBJECT_PREFAB, "MotorThrow");
		if (gameObject != null)
		{
			float num = this.m_add_player_speed * 0.2f;
			for (int i = startCount; i < endCount; i++)
			{
				string text = string.Empty;
				ResourceCategory category = ResourceCategory.UNKNOWN;
				uint type = (uint)this.m_info[i].m_type;
				if ((ulong)type < (ulong)((long)ObjFeverRing.OBJDATA_PARAMS.Length))
				{
					text = ObjFeverRing.OBJDATA_PARAMS[(int)((UIntPtr)type)].m_name;
					category = ObjFeverRing.OBJDATA_PARAMS[(int)((UIntPtr)type)].m_category;
				}
				GameObject gameObject2 = ObjUtil.GetChangeObject(ResourceManager.Instance, this.m_playerInformation, this.m_levelInformation, text);
				if (gameObject2 == null)
				{
					gameObject2 = ResourceManager.Instance.GetGameObject(category, text);
				}
				if (gameObject2 != null)
				{
					Vector3 vector = base.transform.position + new Vector3(1f + num, 0f, 0f);
					SpawnableObject spawnableObject = this.GetReviveSpawnableObject(gameObject2);
					bool flag = false;
					GameObject gameObject3;
					if (spawnableObject != null)
					{
						this.SetRevivalSpawnableObject(spawnableObject, vector);
						this.m_rivivalObj.Add(spawnableObject);
						gameObject3 = spawnableObject.gameObject;
						flag = true;
					}
					else
					{
						gameObject3 = (UnityEngine.Object.Instantiate(gameObject2, vector, base.transform.rotation) as GameObject);
					}
					GameObject gameObject4 = UnityEngine.Object.Instantiate(gameObject, vector, base.transform.rotation) as GameObject;
					if (gameObject3 && gameObject4)
					{
						gameObject3.gameObject.SetActive(true);
						gameObject3.transform.parent = base.gameObject.transform;
						if (!flag)
						{
							spawnableObject = gameObject3.GetComponent<SpawnableObject>();
							spawnableObject.AttachModelObject();
							ObjTimerBase component = gameObject3.GetComponent<ObjTimerBase>();
							if (component != null)
							{
								component.SetMoveType(ObjTimerBase.MoveType.Bound);
							}
						}
						gameObject4.gameObject.SetActive(true);
						gameObject4.transform.parent = gameObject3.transform;
						MotorThrow component2 = gameObject4.GetComponent<MotorThrow>();
						if (component2)
						{
							component2.Setup(new MotorThrow.ThrowParam
							{
								m_obj = gameObject3,
								m_speed = 6f,
								m_gravity = -6.1f,
								m_add_x = this.m_info[i].m_param.m_add_x + num,
								m_add_y = this.m_info[i].m_param.m_add_y,
								m_up = base.transform.up,
								m_forward = base.transform.right,
								m_rot_angle = Vector3.zero,
								m_rot_speed = 0f,
								m_rot_downspeed = 0f,
								m_bound = true,
								m_bound_pos_y = 0f,
								m_bound_add_y = 1.8f + this.m_info[i].m_param.m_add_y * 1f,
								m_bound_down_x = 0.5f,
								m_bound_down_y = 0.01f
							});
						}
					}
				}
				this.m_createCount++;
			}
		}
	}

	// Token: 0x06003CC7 RID: 15559 RVA: 0x0013FB88 File Offset: 0x0013DD88
	private void SetRevivalSpawnableObject(SpawnableObject spawnableObject, Vector3 pos)
	{
		if (spawnableObject != null)
		{
			spawnableObject.Sleep = false;
			spawnableObject.gameObject.transform.position = pos;
			spawnableObject.gameObject.transform.rotation = base.transform.rotation;
			spawnableObject.OnRevival();
		}
	}

	// Token: 0x06003CC8 RID: 15560 RVA: 0x0013FBDC File Offset: 0x0013DDDC
	private SpawnableObject GetReviveSpawnableObject(GameObject srcObj)
	{
		if (srcObj == null)
		{
			return null;
		}
		SpawnableObject component = srcObj.GetComponent<SpawnableObject>();
		if (component == null)
		{
			return null;
		}
		if (this.m_stageBlockManager != null)
		{
			ObjectSpawnManager component2 = this.m_stageBlockManager.GetComponent<ObjectSpawnManager>();
			if (component2 != null && component.IsStockObject())
			{
				return component2.GetSpawnableObject(component.GetStockObjectType());
			}
		}
		return null;
	}

	// Token: 0x040034DC RID: 13532
	private const float END_TIME = 6f;

	// Token: 0x040034DD RID: 13533
	private const float RING_SPEED = 6f;

	// Token: 0x040034DE RID: 13534
	private const float RING_GRAVITY = -6.1f;

	// Token: 0x040034DF RID: 13535
	private const float BOUND_Y = 1f;

	// Token: 0x040034E0 RID: 13536
	private const float BOUND_DOWN_X = 0.5f;

	// Token: 0x040034E1 RID: 13537
	private const float BOUND_DOWN_Y = 0.01f;

	// Token: 0x040034E2 RID: 13538
	private const float BOUND_MIN = 1.8f;

	// Token: 0x040034E3 RID: 13539
	private const int CREATE_MAX = 5;

	// Token: 0x040034E4 RID: 13540
	private static readonly ObjFeverRing.FeverRingData[] OBJDATA_PARAMS = new ObjFeverRing.FeverRingData[]
	{
		new ObjFeverRing.FeverRingData("ObjRing", ResourceCategory.OBJECT_PREFAB),
		new ObjFeverRing.FeverRingData("ObjSuperRing", ResourceCategory.OBJECT_PREFAB),
		new ObjFeverRing.FeverRingData("ObjRedStarRing", ResourceCategory.OBJECT_PREFAB),
		new ObjFeverRing.FeverRingData("ObjTimerBronze", ResourceCategory.OBJECT_PREFAB),
		new ObjFeverRing.FeverRingData("ObjTimerSilver", ResourceCategory.OBJECT_PREFAB),
		new ObjFeverRing.FeverRingData("ObjTimerGold", ResourceCategory.OBJECT_PREFAB)
	};

	// Token: 0x040034E5 RID: 13541
	private static readonly ObjFeverRing.FeverRingParam[] FEVERRING_PARAM = new ObjFeverRing.FeverRingParam[]
	{
		new ObjFeverRing.FeverRingParam(4f, 0.81f),
		new ObjFeverRing.FeverRingParam(3.5f, 1.22f),
		new ObjFeverRing.FeverRingParam(2.5f, 0.23f),
		new ObjFeverRing.FeverRingParam(4.4f, 0.44f),
		new ObjFeverRing.FeverRingParam(3.8f, 1.35f),
		new ObjFeverRing.FeverRingParam(5f, 0.56f),
		new ObjFeverRing.FeverRingParam(2f, 1.17f),
		new ObjFeverRing.FeverRingParam(4f, 1.58f),
		new ObjFeverRing.FeverRingParam(3.5f, 1.39f),
		new ObjFeverRing.FeverRingParam(3f, 0.31f),
		new ObjFeverRing.FeverRingParam(1.3f, 0.72f),
		new ObjFeverRing.FeverRingParam(3.2f, 0.43f),
		new ObjFeverRing.FeverRingParam(2.4f, 1.24f),
		new ObjFeverRing.FeverRingParam(4.3f, 0.55f),
		new ObjFeverRing.FeverRingParam(3.2f, 1.26f),
		new ObjFeverRing.FeverRingParam(3.6f, 1.27f),
		new ObjFeverRing.FeverRingParam(2.4f, 0.88f),
		new ObjFeverRing.FeverRingParam(4.2f, 0.49f),
		new ObjFeverRing.FeverRingParam(3f, 1.21f),
		new ObjFeverRing.FeverRingParam(4.1f, 0.82f)
	};

	// Token: 0x040034E6 RID: 13542
	private int m_count;

	// Token: 0x040034E7 RID: 13543
	private int m_createCount;

	// Token: 0x040034E8 RID: 13544
	private float m_time;

	// Token: 0x040034E9 RID: 13545
	private float m_add_player_speed;

	// Token: 0x040034EA RID: 13546
	private ObjFeverRing.FeverRingInfo[] m_info;

	// Token: 0x040034EB RID: 13547
	private int m_bossType;

	// Token: 0x040034EC RID: 13548
	private PlayerInformation m_playerInformation;

	// Token: 0x040034ED RID: 13549
	private LevelInformation m_levelInformation;

	// Token: 0x040034EE RID: 13550
	private List<SpawnableObject> m_rivivalObj = new List<SpawnableObject>();

	// Token: 0x040034EF RID: 13551
	private GameObject m_stageBlockManager;

	// Token: 0x02000900 RID: 2304
	private enum Type
	{
		// Token: 0x040034F1 RID: 13553
		RING,
		// Token: 0x040034F2 RID: 13554
		SUPER_RING,
		// Token: 0x040034F3 RID: 13555
		REDSTAR_RING,
		// Token: 0x040034F4 RID: 13556
		BRONZE_TIMER,
		// Token: 0x040034F5 RID: 13557
		SILVER_TIMER,
		// Token: 0x040034F6 RID: 13558
		GOLD_TIMER,
		// Token: 0x040034F7 RID: 13559
		NUM
	}

	// Token: 0x02000901 RID: 2305
	private class FeverRingData
	{
		// Token: 0x06003CC9 RID: 15561 RVA: 0x0013FC50 File Offset: 0x0013DE50
		public FeverRingData(string name, ResourceCategory category)
		{
			this.m_name = name;
			this.m_category = category;
		}

		// Token: 0x040034F8 RID: 13560
		public string m_name;

		// Token: 0x040034F9 RID: 13561
		public ResourceCategory m_category;
	}

	// Token: 0x02000902 RID: 2306
	private class FeverRingParam
	{
		// Token: 0x06003CCA RID: 15562 RVA: 0x0013FC68 File Offset: 0x0013DE68
		public FeverRingParam(float add_x, float add_y)
		{
			this.m_add_x = add_x;
			this.m_add_y = add_y;
		}

		// Token: 0x040034FA RID: 13562
		public float m_add_x;

		// Token: 0x040034FB RID: 13563
		public float m_add_y;
	}

	// Token: 0x02000903 RID: 2307
	private struct FeverRingInfo
	{
		// Token: 0x040034FC RID: 13564
		public ObjFeverRing.FeverRingParam m_param;

		// Token: 0x040034FD RID: 13565
		public ObjFeverRing.Type m_type;
	}
}
