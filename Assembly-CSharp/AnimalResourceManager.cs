using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000830 RID: 2096
public class AnimalResourceManager : MonoBehaviour
{
	// Token: 0x17000872 RID: 2162
	// (get) Token: 0x060038D8 RID: 14552 RVA: 0x0012D848 File Offset: 0x0012BA48
	public static AnimalResourceManager Instance
	{
		get
		{
			return AnimalResourceManager.s_instance;
		}
	}

	// Token: 0x060038D9 RID: 14553 RVA: 0x0012D850 File Offset: 0x0012BA50
	public GameObject GetStockAnimal(AnimalType type)
	{
		if (this.m_depotObjs.ContainsKey(type))
		{
			return this.m_depotObjs[type].Get();
		}
		return null;
	}

	// Token: 0x060038DA RID: 14554 RVA: 0x0012D884 File Offset: 0x0012BA84
	public void SetSleep(AnimalType type, GameObject obj)
	{
		if (this.m_depotObjs.ContainsKey(type))
		{
			this.m_depotObjs[type].Sleep(obj);
		}
	}

	// Token: 0x060038DB RID: 14555 RVA: 0x0012D8AC File Offset: 0x0012BAAC
	public void StockAnimalObject(bool bossStage)
	{
		if (bossStage)
		{
			return;
		}
		for (int i = 0; i < 8; i++)
		{
			if (this.CheckAblity(AnimalResourceManager.STOCK_PARAM[i].m_chaoAbility))
			{
				AnimalType animalType = (AnimalType)i;
				GameObject gameObject = new GameObject(animalType.ToString());
				if (gameObject != null)
				{
					gameObject.transform.parent = base.gameObject.transform;
					AnimalResourceManager.DepotObjs depotObjs = new AnimalResourceManager.DepotObjs();
					for (int j = 0; j < AnimalResourceManager.STOCK_PARAM[i].m_stockCount; j++)
					{
						depotObjs.Add(ObjAnimalUtil.CreateStockAnimal(gameObject, animalType));
					}
					this.m_depotObjs.Add(animalType, depotObjs);
				}
			}
		}
	}

	// Token: 0x060038DC RID: 14556 RVA: 0x0012D960 File Offset: 0x0012BB60
	private bool CheckAblity(ChaoAbility ability)
	{
		return ability == ChaoAbility.UNKNOWN || (StageAbilityManager.Instance != null && StageAbilityManager.Instance.HasChaoAbility(ability));
	}

	// Token: 0x060038DD RID: 14557 RVA: 0x0012D994 File Offset: 0x0012BB94
	protected void Awake()
	{
		this.SetInstance();
	}

	// Token: 0x060038DE RID: 14558 RVA: 0x0012D99C File Offset: 0x0012BB9C
	private void Start()
	{
		base.enabled = false;
	}

	// Token: 0x060038DF RID: 14559 RVA: 0x0012D9A8 File Offset: 0x0012BBA8
	private void OnDestroy()
	{
		if (AnimalResourceManager.s_instance == this)
		{
			AnimalResourceManager.s_instance = null;
		}
	}

	// Token: 0x060038E0 RID: 14560 RVA: 0x0012D9C0 File Offset: 0x0012BBC0
	private void SetInstance()
	{
		if (AnimalResourceManager.s_instance == null)
		{
			AnimalResourceManager.s_instance = this;
		}
		else
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x04002F91 RID: 12177
	private static AnimalResourceManager s_instance = null;

	// Token: 0x04002F92 RID: 12178
	private static readonly AnimalResourceManager.StockParam[] STOCK_PARAM = new AnimalResourceManager.StockParam[]
	{
		new AnimalResourceManager.StockParam(8, ChaoAbility.SPECIAL_ANIMAL),
		new AnimalResourceManager.StockParam(4, ChaoAbility.UNKNOWN),
		new AnimalResourceManager.StockParam(4, ChaoAbility.UNKNOWN),
		new AnimalResourceManager.StockParam(4, ChaoAbility.UNKNOWN),
		new AnimalResourceManager.StockParam(4, ChaoAbility.UNKNOWN),
		new AnimalResourceManager.StockParam(4, ChaoAbility.UNKNOWN),
		new AnimalResourceManager.StockParam(4, ChaoAbility.UNKNOWN),
		new AnimalResourceManager.StockParam(8, ChaoAbility.SPECIAL_ANIMAL_PSO2)
	};

	// Token: 0x04002F93 RID: 12179
	private Dictionary<AnimalType, AnimalResourceManager.DepotObjs> m_depotObjs = new Dictionary<AnimalType, AnimalResourceManager.DepotObjs>();

	// Token: 0x02000831 RID: 2097
	private class StockParam
	{
		// Token: 0x060038E1 RID: 14561 RVA: 0x0012D9F4 File Offset: 0x0012BBF4
		public StockParam(int stockCount, ChaoAbility chaoAbility)
		{
			this.m_stockCount = stockCount;
			this.m_chaoAbility = chaoAbility;
		}

		// Token: 0x04002F94 RID: 12180
		public int m_stockCount;

		// Token: 0x04002F95 RID: 12181
		public ChaoAbility m_chaoAbility;
	}

	// Token: 0x02000832 RID: 2098
	public class DepotObjs
	{
		// Token: 0x060038E3 RID: 14563 RVA: 0x0012DA20 File Offset: 0x0012BC20
		public void Add(GameObject obj)
		{
			if (obj != null)
			{
				this.m_objList.Add(obj);
			}
		}

		// Token: 0x060038E4 RID: 14564 RVA: 0x0012DA3C File Offset: 0x0012BC3C
		public GameObject Get()
		{
			foreach (GameObject gameObject in this.m_objList)
			{
				ObjAnimalBase component = gameObject.GetComponent<ObjAnimalBase>();
				if (component != null && component.IsSleep())
				{
					component.Sleep = false;
					component.OnRevival();
					return gameObject;
				}
			}
			return null;
		}

		// Token: 0x060038E5 RID: 14565 RVA: 0x0012DAD0 File Offset: 0x0012BCD0
		public void Sleep(GameObject obj)
		{
			if (obj != null)
			{
				ObjAnimalBase component = obj.GetComponent<ObjAnimalBase>();
				if (component != null)
				{
					component.Sleep = true;
				}
			}
		}

		// Token: 0x04002F96 RID: 12182
		private List<GameObject> m_objList = new List<GameObject>();
	}
}
