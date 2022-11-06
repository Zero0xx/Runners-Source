using System;
using System.Collections;
using System.Collections.Generic;
using Message;
using UnityEngine;

// Token: 0x0200026F RID: 623
[AddComponentMenu("Scripts/Runners/Game/Level")]
public class ObjectSpawnManager : MonoBehaviour
{
	// Token: 0x060010FF RID: 4351 RVA: 0x00060FA8 File Offset: 0x0005F1A8
	private void Start()
	{
	}

	// Token: 0x06001100 RID: 4352 RVA: 0x00060FAC File Offset: 0x0005F1AC
	private void Update()
	{
		if (this.m_playerInformation != null && this.m_playerInformation.Position.x > base.transform.position.x)
		{
			base.transform.position = this.m_playerInformation.Position;
		}
		float x = base.transform.position.x;
		this.CheckRangeIn(x);
		this.CheckRangeOut(x);
	}

	// Token: 0x06001101 RID: 4353 RVA: 0x00061030 File Offset: 0x0005F230
	private void OnDestroy()
	{
		if (this.m_spawnableInfoList == null)
		{
			return;
		}
		foreach (SpawnableInfo info in this.m_spawnableInfoList)
		{
			this.DestroyObject(info);
		}
		this.m_spawnableInfoList.Clear();
	}

	// Token: 0x06001102 RID: 4354 RVA: 0x000610B0 File Offset: 0x0005F2B0
	public void Setup(bool bossStage)
	{
		this.m_resourceManager = ResourceManager.Instance;
		this.m_playerInformation = ObjUtil.GetPlayerInformation();
		this.m_levelInformation = ObjUtil.GetLevelInformation();
		this.m_spawnableInfoList = new List<SpawnableInfo>();
		this.m_onlyOneObjectName = new List<string>();
		this.m_stageSetData = new StageSpawnableParameterContainer();
		this.StockStageObject(bossStage);
		base.StartCoroutine(this.LoadSetTableFirst());
	}

	// Token: 0x06001103 RID: 4355 RVA: 0x00061114 File Offset: 0x0005F314
	private void StockStageObject(bool bossStage)
	{
		if (this.m_stock == null)
		{
			this.m_stock = new GameObject("StockGameObject");
			if (this.m_stock)
			{
				this.m_stock.transform.position = Vector3.zero;
				this.m_stock.transform.rotation = Quaternion.identity;
			}
		}
		bool flag = false;
		if (StageModeManager.Instance != null)
		{
			flag = StageModeManager.Instance.IsQuickMode();
		}
		if (this.m_dicSpawnableObjs.Count == 0)
		{
			for (int i = 0; i < 12; i++)
			{
				StockObjectType stockObjectType = (StockObjectType)i;
				this.m_dicSpawnableObjs.Add(stockObjectType, new ObjectSpawnManager.DepotObjs(this.m_stock, stockObjectType));
			}
		}
		if (ResourceManager.Instance != null)
		{
			for (int j = 0; j < 12; j++)
			{
				if (!bossStage || this.STOCK_DATA_TABLE[j].m_bossStage)
				{
					if (j == 8 || j == 9)
					{
						if (!flag)
						{
							goto IL_1CB;
						}
						if (EventManager.Instance != null && EventManager.Instance.Type != EventManager.EventType.QUICK)
						{
							goto IL_1CB;
						}
					}
					if (j != 10 || !flag)
					{
						GameObject spawnableGameObject = ResourceManager.Instance.GetSpawnableGameObject(this.STOCK_DATA_TABLE[j].m_name);
						int stockCount = this.STOCK_DATA_TABLE[j].m_stockCount;
						for (int k = 0; k < stockCount; k++)
						{
							GameObject gameObject = UnityEngine.Object.Instantiate(spawnableGameObject, Vector3.zero, Quaternion.identity) as GameObject;
							if (gameObject != null)
							{
								gameObject.name = spawnableGameObject.name;
								SpawnableObject component = gameObject.GetComponent<SpawnableObject>();
								if (component != null)
								{
									this.AddSpawnableObject(component);
								}
							}
						}
					}
				}
				IL_1CB:;
			}
		}
	}

	// Token: 0x06001104 RID: 4356 RVA: 0x000612F8 File Offset: 0x0005F4F8
	public IEnumerator LoadSetTableFirst()
	{
		base.enabled = false;
		this.m_setDataLoaded = false;
		yield return base.StartCoroutine(this.LoadSetTable(0));
		yield return base.StartCoroutine(this.LoadSetTable(91));
		this.m_setDataLoaded = true;
		base.enabled = true;
		GC.Collect();
		yield break;
	}

	// Token: 0x06001105 RID: 4357 RVA: 0x00061314 File Offset: 0x0005F514
	public IEnumerator LoadSetTable(int firstBlock, int numBlock)
	{
		this.m_setDataLoaded = false;
		for (int i = firstBlock; i < firstBlock + numBlock; i += 5)
		{
			yield return base.StartCoroutine(this.LoadSetTable(i));
		}
		this.m_setDataLoaded = true;
		base.enabled = true;
		yield break;
	}

	// Token: 0x06001106 RID: 4358 RVA: 0x0006134C File Offset: 0x0005F54C
	public IEnumerator LoadSetTable(int[] blockTable, int readFileCount)
	{
		this.m_setDataLoaded = false;
		int count = Mathf.Min(blockTable.Length, readFileCount);
		for (int index = 0; index < count; index++)
		{
			yield return base.StartCoroutine(this.LoadSetTable(blockTable[index]));
		}
		this.m_setDataLoaded = true;
		base.enabled = true;
		yield break;
	}

	// Token: 0x06001107 RID: 4359 RVA: 0x00061384 File Offset: 0x0005F584
	public bool IsDataLoaded()
	{
		return this.m_setDataLoaded;
	}

	// Token: 0x06001108 RID: 4360 RVA: 0x0006138C File Offset: 0x0005F58C
	public void RegisterOnlyOneObject(SpawnableInfo info)
	{
		if (info != null)
		{
			this.m_onlyOneObjectName.Add(info.m_parameters.ObjectName);
		}
	}

	// Token: 0x06001109 RID: 4361 RVA: 0x000613AC File Offset: 0x0005F5AC
	private IEnumerator LoadSetTable(int blockIndex)
	{
		int arrayIndex = 0;
		if (blockIndex == 0)
		{
			arrayIndex = 0;
		}
		else
		{
			arrayIndex = (blockIndex - 1) / 5 + 1;
			blockIndex = (arrayIndex - 1) * 5 + 1;
		}
		if (this.m_stageSetData.GetBlockData(blockIndex, 0) == null && this.m_stageSetData.GetBlockData(blockIndex, 1) == null)
		{
			GameObject assetObject = this.m_resourceManager.GetGameObject(ResourceCategory.TERRAIN_MODEL, TerrainXmlData.DataAssetName);
			if (assetObject)
			{
				TerrainXmlData terrainData = assetObject.GetComponent<TerrainXmlData>();
				if (terrainData)
				{
					TextAsset[] asset = terrainData.SetData;
					if (asset != null)
					{
						TextAsset xmlText = asset[arrayIndex];
						if (xmlText != null)
						{
							GameObject parserObj = new GameObject();
							SpawnableParser parser = parserObj.AddComponent<SpawnableParser>();
							yield return base.StartCoroutine(parser.CreateSetData(this.m_resourceManager, xmlText, this.m_stageSetData));
							UnityEngine.Object.Destroy(parserObj);
						}
					}
				}
			}
		}
		yield break;
	}

	// Token: 0x0600110A RID: 4362 RVA: 0x000613D8 File Offset: 0x0005F5D8
	private void LoadSetTable()
	{
		GameObject gameObject = this.m_resourceManager.GetGameObject(ResourceCategory.TERRAIN_MODEL, TerrainXmlData.DataAssetName);
		if (gameObject)
		{
			TerrainXmlData component = gameObject.GetComponent<TerrainXmlData>();
			if (component)
			{
				TextAsset[] setData = component.SetData;
				if (setData != null)
				{
					GameObject gameObject2 = new GameObject();
					SpawnableParser spawnableParser = gameObject2.AddComponent<SpawnableParser>();
					spawnableParser.CreateSetData(this.m_resourceManager, setData[0], this.m_stageSetData);
					UnityEngine.Object.Destroy(gameObject2);
				}
			}
		}
	}

	// Token: 0x0600110B RID: 4363 RVA: 0x0006144C File Offset: 0x0005F64C
	public void OnActivateBlock(MsgActivateBlock msg)
	{
		if (msg != null)
		{
			this.ActivateBlock(msg.m_blockNo, msg.m_layerNo, msg.m_activateID, msg.m_originPosition, msg.m_originRotation);
			if (msg.m_checkPoint != MsgActivateBlock.CheckPoint.None)
			{
				ObjectSpawnManager.CheckPointInfo checkPointInfo = new ObjectSpawnManager.CheckPointInfo();
				checkPointInfo.m_activateID = msg.m_activateID;
				checkPointInfo.m_onReplace = (msg.m_checkPoint == MsgActivateBlock.CheckPoint.First);
				this.m_checkPointInfos.Add(checkPointInfo);
			}
			if (msg.m_replaceStage)
			{
				this.CheckRangeIn(msg.m_originPosition.x);
			}
		}
	}

	// Token: 0x0600110C RID: 4364 RVA: 0x000614D8 File Offset: 0x0005F6D8
	private void OnDeactivateBlock(MsgDeactivateBlock msg)
	{
		if (msg != null)
		{
			this.DeactivateBlock(msg.m_activateID, true);
		}
	}

	// Token: 0x0600110D RID: 4365 RVA: 0x000614F0 File Offset: 0x0005F6F0
	private void OnDeactivateAllBlock(MsgDeactivateAllBlock msg)
	{
		foreach (SpawnableInfo info in this.m_spawnableInfoList)
		{
			this.DestroyObject(info);
		}
		this.m_spawnableInfoList.Clear();
		this.m_onlyOneObjectName.Clear();
	}

	// Token: 0x0600110E RID: 4366 RVA: 0x0006156C File Offset: 0x0005F76C
	private void OnChangeCurerntBlock(MsgChangeCurrentBlock msg)
	{
	}

	// Token: 0x0600110F RID: 4367 RVA: 0x00061570 File Offset: 0x0005F770
	private bool CheckAndActivePointMarker(SpawnableObject createdObject, ObjectSpawnManager.CheckPointInfo info)
	{
		if (createdObject && createdObject.name.Contains("ObjPointMarker"))
		{
			MsgActivePointMarker msgActivePointMarker = new MsgActivePointMarker((!info.m_onReplace) ? PointMarkerType.OrderBlock : PointMarkerType.BossEnd);
			createdObject.SendMessage("OnActivePointMarker", msgActivePointMarker, SendMessageOptions.DontRequireReceiver);
			if (msgActivePointMarker.m_activated)
			{
				this.m_checkPointInfos.Remove(info);
			}
			return true;
		}
		return false;
	}

	// Token: 0x06001110 RID: 4368 RVA: 0x000615E0 File Offset: 0x0005F7E0
	private void ActivateBlock(int block, int layer, int activateID, Vector3 originPoint, Quaternion originRotation)
	{
		Vector3 position = base.transform.position;
		Quaternion rotation = base.transform.rotation;
		base.transform.position = originPoint;
		base.transform.rotation = originRotation;
		BlockSpawnableParameterContainer blockData = this.m_stageSetData.GetBlockData(block, layer);
		if (blockData != null)
		{
			foreach (SpawnableParameter spawnableParameter in blockData.GetParameters())
			{
				SpawnableInfo spawnableInfo = new SpawnableInfo();
				spawnableInfo.m_block = block;
				spawnableInfo.m_blockActivateID = activateID;
				spawnableInfo.m_parameters = spawnableParameter;
				spawnableInfo.m_position = base.transform.TransformPoint(spawnableParameter.Position);
				spawnableInfo.m_rotation = rotation * spawnableParameter.Rotation;
				spawnableInfo.m_manager = this;
				this.m_spawnableInfoList.Add(spawnableInfo);
			}
		}
		base.transform.position = position;
		base.transform.rotation = rotation;
		GC.Collect();
	}

	// Token: 0x06001111 RID: 4369 RVA: 0x00061704 File Offset: 0x0005F904
	private void DeactivateBlock(int blockActivateID, bool ignoreNotRangeOut)
	{
		int i = this.m_spawnableInfoList.Count - 1;
		while (i >= 0)
		{
			SpawnableInfo spawnableInfo = this.m_spawnableInfoList[i];
			if (spawnableInfo.m_blockActivateID != blockActivateID || !spawnableInfo.m_object)
			{
				goto IL_71;
			}
			if (!spawnableInfo.NotRangeOut || !ignoreNotRangeOut)
			{
				this.DestroyObject(spawnableInfo);
				this.m_spawnableInfoList.Remove(this.m_spawnableInfoList[i]);
				goto IL_71;
			}
			IL_C5:
			i--;
			continue;
			IL_71:
			foreach (ObjectSpawnManager.CheckPointInfo checkPointInfo in this.m_checkPointInfos)
			{
				if (checkPointInfo.m_activateID == blockActivateID)
				{
					this.m_checkPointInfos.Remove(checkPointInfo);
					break;
				}
			}
			goto IL_C5;
		}
	}

	// Token: 0x06001112 RID: 4370 RVA: 0x00061800 File Offset: 0x0005FA00
	public SpawnableObject GetSpawnableObject(StockObjectType type)
	{
		if (this.m_dicSpawnableObjs.ContainsKey(type))
		{
			return this.m_dicSpawnableObjs[type].Get();
		}
		return null;
	}

	// Token: 0x06001113 RID: 4371 RVA: 0x00061834 File Offset: 0x0005FA34
	private void AddSpawnableObject(SpawnableObject spawnableObject)
	{
		if (spawnableObject != null && spawnableObject.IsStockObject())
		{
			spawnableObject.AttachModelObject();
			StockObjectType stockObjectType = spawnableObject.GetStockObjectType();
			if (this.m_dicSpawnableObjs.ContainsKey(stockObjectType))
			{
				this.m_dicSpawnableObjs[stockObjectType].Add(spawnableObject);
				this.m_dicSpawnableObjs[stockObjectType].Sleep(spawnableObject);
			}
		}
	}

	// Token: 0x06001114 RID: 4372 RVA: 0x0006189C File Offset: 0x0005FA9C
	public void SleepSpawnableObject(SpawnableObject spawnableObject)
	{
		if (spawnableObject != null && spawnableObject.Share)
		{
			StockObjectType stockObjectType = spawnableObject.GetStockObjectType();
			if (this.m_dicSpawnableObjs.ContainsKey(stockObjectType))
			{
				this.m_dicSpawnableObjs[stockObjectType].Sleep(spawnableObject);
			}
		}
	}

	// Token: 0x06001115 RID: 4373 RVA: 0x000618EC File Offset: 0x0005FAEC
	private bool CreateObject(SpawnableInfo info)
	{
		if (this.m_onlyOneObjectName.IndexOf(info.m_parameters.ObjectName) != -1)
		{
			return false;
		}
		GameObject gameObject = ObjUtil.GetChangeObject(this.m_resourceManager, this.m_playerInformation, this.m_levelInformation, info.m_parameters.ObjectName);
		if (gameObject == null)
		{
			gameObject = this.m_resourceManager.GetSpawnableGameObject(info.m_parameters.ObjectName);
		}
		if (gameObject != null)
		{
			SpawnableObject component = gameObject.GetComponent<SpawnableObject>();
			if (component != null && !component.IsValid())
			{
				return false;
			}
		}
		SpawnableObject spawnableObject = this.GetReviveSpawnableObject(gameObject);
		if (spawnableObject != null)
		{
			spawnableObject.Sleep = false;
			spawnableObject.gameObject.transform.position = info.m_position;
			spawnableObject.gameObject.transform.rotation = info.m_rotation;
			spawnableObject.gameObject.SetActive(true);
			spawnableObject.gameObject.transform.parent = this.m_stock.transform;
			info.SpawnedObject(spawnableObject);
			spawnableObject.AttachSpawnableInfo(info);
			SpawnableBehavior component2 = spawnableObject.gameObject.GetComponent<SpawnableBehavior>();
			if (component2)
			{
				component2.SetParameters(info.m_parameters);
			}
			spawnableObject.OnRevival();
			return true;
		}
		if (gameObject != null)
		{
			GameObject gameObject2 = UnityEngine.Object.Instantiate(gameObject, info.m_position, info.m_rotation) as GameObject;
			if (gameObject2)
			{
				gameObject2.gameObject.SetActive(true);
				spawnableObject = gameObject2.GetComponent<SpawnableObject>();
				if (spawnableObject != null)
				{
					info.SpawnedObject(spawnableObject);
					spawnableObject.AttachSpawnableInfo(info);
				}
				SpawnableBehavior component3 = gameObject2.GetComponent<SpawnableBehavior>();
				if (component3)
				{
					component3.SetParameters(info.m_parameters);
				}
				if (spawnableObject != null)
				{
					spawnableObject.AttachModelObject();
					spawnableObject.OnCreate();
				}
				ObjectSpawnManager.CheckPointInfo checkPointInfo = null;
				if (this.IsCreatePointmarkerBlock(info.m_blockActivateID, ref checkPointInfo) && checkPointInfo != null)
				{
					this.CheckAndActivePointMarker(spawnableObject, checkPointInfo);
				}
				return true;
			}
		}
		return false;
	}

	// Token: 0x06001116 RID: 4374 RVA: 0x00061AF8 File Offset: 0x0005FCF8
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
		if (component.IsStockObject())
		{
			return this.GetSpawnableObject(component.GetStockObjectType());
		}
		return null;
	}

	// Token: 0x06001117 RID: 4375 RVA: 0x00061B44 File Offset: 0x0005FD44
	private void DestroyObject(SpawnableInfo info)
	{
		if (info.Spawned)
		{
			if (info.m_object.Share)
			{
				info.m_object.SetSleep();
			}
			else
			{
				UnityEngine.Object.Destroy(info.m_object.gameObject);
			}
			info.DestroyedObject();
		}
	}

	// Token: 0x06001118 RID: 4376 RVA: 0x00061B94 File Offset: 0x0005FD94
	public void DetachObject(SpawnableInfo info)
	{
		info.RequestDestroy = true;
		info.DestroyedObject();
		this.m_spawnableInfoList.Remove(info);
	}

	// Token: 0x06001119 RID: 4377 RVA: 0x00061BB0 File Offset: 0x0005FDB0
	public void DetachInfoList(SpawnableInfo info)
	{
		this.m_spawnableInfoList.Remove(info);
	}

	// Token: 0x0600111A RID: 4378 RVA: 0x00061BC0 File Offset: 0x0005FDC0
	private void CheckRangeIn(float basePosition)
	{
		List<SpawnableInfo> list = null;
		foreach (SpawnableInfo spawnableInfo in this.m_spawnableInfoList)
		{
			if (!spawnableInfo.Spawned && !spawnableInfo.Destroyed)
			{
				float x = spawnableInfo.m_position.x;
				float num = x - basePosition;
				if (x - basePosition < spawnableInfo.m_parameters.RangeIn && !this.CreateObject(spawnableInfo))
				{
					if (list == null)
					{
						new List<SpawnableInfo>();
					}
					if (list != null)
					{
						list.Add(spawnableInfo);
					}
				}
				if (num > 200f)
				{
					break;
				}
			}
		}
		if (list != null)
		{
			foreach (SpawnableInfo item in list)
			{
				this.m_spawnableInfoList.Remove(item);
			}
		}
	}

	// Token: 0x0600111B RID: 4379 RVA: 0x00061CF8 File Offset: 0x0005FEF8
	private void CheckRangeOut(float basePosition)
	{
		List<SpawnableInfo> list = null;
		foreach (SpawnableInfo spawnableInfo in this.m_spawnableInfoList)
		{
			if (spawnableInfo.Spawned)
			{
				if (!spawnableInfo.NotRangeOut)
				{
					float num = basePosition - spawnableInfo.m_position.x;
					if (num < 0f)
					{
						break;
					}
					if (num > spawnableInfo.m_parameters.RangeOut)
					{
						if (list == null)
						{
							new List<SpawnableInfo>();
						}
						if (list != null)
						{
							list.Add(spawnableInfo);
						}
						this.DestroyObject(spawnableInfo);
					}
				}
			}
		}
		if (list != null)
		{
			foreach (SpawnableInfo item in list)
			{
				this.m_spawnableInfoList.Remove(item);
			}
		}
	}

	// Token: 0x0600111C RID: 4380 RVA: 0x00061E28 File Offset: 0x00060028
	private void DrawObjectInfo(string infoname)
	{
		global::Debug.Log(infoname + "Start");
		foreach (SpawnableInfo spawnableInfo in this.m_spawnableInfoList)
		{
			string text = spawnableInfo.m_parameters.ObjectName + ":";
			text += ((!spawnableInfo.Spawned) ? "NotSpawned" : "Spawned ");
			string text2 = text;
			text = string.Concat(new string[]
			{
				text2,
				"pos ",
				spawnableInfo.m_position.x.ToString("F2"),
				" ",
				spawnableInfo.m_position.y.ToString("F2"),
				" ",
				spawnableInfo.m_position.z.ToString("F2")
			});
			global::Debug.Log(text + "\n");
		}
		global::Debug.Log(infoname + "End");
	}

	// Token: 0x0600111D RID: 4381 RVA: 0x00061F60 File Offset: 0x00060160
	private bool IsCreatePointmarkerBlock(int activateID, ref ObjectSpawnManager.CheckPointInfo retInfo)
	{
		foreach (ObjectSpawnManager.CheckPointInfo checkPointInfo in this.m_checkPointInfos)
		{
			if (checkPointInfo.m_activateID == activateID)
			{
				retInfo = checkPointInfo;
				return true;
			}
		}
		return false;
	}

	// Token: 0x04000F70 RID: 3952
	private const float MaxOfRange = 200f;

	// Token: 0x04000F71 RID: 3953
	private readonly ObjectSpawnManager.StockData[] STOCK_DATA_TABLE = new ObjectSpawnManager.StockData[]
	{
		new ObjectSpawnManager.StockData("ObjRing", 50, true),
		new ObjectSpawnManager.StockData("ObjSuperRing", 20, true),
		new ObjectSpawnManager.StockData("ObjCrystal_A", 40, false),
		new ObjectSpawnManager.StockData("ObjCrystal_B", 40, false),
		new ObjectSpawnManager.StockData("ObjCrystal_C", 40, false),
		new ObjectSpawnManager.StockData("ObjCrystal10_A", 40, false),
		new ObjectSpawnManager.StockData("ObjCrystal10_B", 40, false),
		new ObjectSpawnManager.StockData("ObjCrystal10_C", 40, false),
		new ObjectSpawnManager.StockData("ObjEventCrystal", 50, false),
		new ObjectSpawnManager.StockData("ObjEventCrystal10", 40, false),
		new ObjectSpawnManager.StockData("ObjAirTrap", 40, false),
		new ObjectSpawnManager.StockData("ObjTrap", 10, false)
	};

	// Token: 0x04000F72 RID: 3954
	private PlayerInformation m_playerInformation;

	// Token: 0x04000F73 RID: 3955
	private LevelInformation m_levelInformation;

	// Token: 0x04000F74 RID: 3956
	private StageSpawnableParameterContainer m_stageSetData;

	// Token: 0x04000F75 RID: 3957
	private List<SpawnableInfo> m_spawnableInfoList;

	// Token: 0x04000F76 RID: 3958
	private List<string> m_onlyOneObjectName;

	// Token: 0x04000F77 RID: 3959
	private ResourceManager m_resourceManager;

	// Token: 0x04000F78 RID: 3960
	private List<ObjectSpawnManager.CheckPointInfo> m_checkPointInfos = new List<ObjectSpawnManager.CheckPointInfo>();

	// Token: 0x04000F79 RID: 3961
	private GameObject m_stock;

	// Token: 0x04000F7A RID: 3962
	private Dictionary<StockObjectType, ObjectSpawnManager.DepotObjs> m_dicSpawnableObjs = new Dictionary<StockObjectType, ObjectSpawnManager.DepotObjs>();

	// Token: 0x04000F7B RID: 3963
	private bool m_setDataLoaded;

	// Token: 0x02000270 RID: 624
	public class StockData
	{
		// Token: 0x0600111E RID: 4382 RVA: 0x00061FD8 File Offset: 0x000601D8
		public StockData(string name, int stockCount, bool bossStage)
		{
			this.m_name = name;
			this.m_stockCount = stockCount;
			this.m_bossStage = bossStage;
		}

		// Token: 0x04000F7C RID: 3964
		public string m_name;

		// Token: 0x04000F7D RID: 3965
		public int m_stockCount;

		// Token: 0x04000F7E RID: 3966
		public bool m_bossStage;
	}

	// Token: 0x02000271 RID: 625
	private class CheckPointInfo
	{
		// Token: 0x04000F7F RID: 3967
		public int m_activateID;

		// Token: 0x04000F80 RID: 3968
		public bool m_onReplace;
	}

	// Token: 0x02000272 RID: 626
	public class DepotObjs
	{
		// Token: 0x06001120 RID: 4384 RVA: 0x00062000 File Offset: 0x00060200
		public DepotObjs(GameObject parentObj, StockObjectType type)
		{
			this.m_depot = new GameObject();
			this.m_depot.name = type.ToString();
			this.m_depot.transform.parent = parentObj.transform;
		}

		// Token: 0x06001121 RID: 4385 RVA: 0x00062058 File Offset: 0x00060258
		public void Add(SpawnableObject obj)
		{
			obj.Share = true;
			this.m_objList.Add(obj);
		}

		// Token: 0x06001122 RID: 4386 RVA: 0x00062070 File Offset: 0x00060270
		public SpawnableObject Get()
		{
			foreach (SpawnableObject spawnableObject in this.m_objList)
			{
				if (spawnableObject.Sleep)
				{
					spawnableObject.Sleep = false;
					return spawnableObject;
				}
			}
			return null;
		}

		// Token: 0x06001123 RID: 4387 RVA: 0x000620EC File Offset: 0x000602EC
		public void Sleep(SpawnableObject obj)
		{
			obj.Sleep = true;
			if (obj.gameObject != null && this.m_depot != null)
			{
				obj.gameObject.transform.parent = this.m_depot.transform;
				if (obj.gameObject.activeSelf)
				{
					obj.gameObject.SetActive(false);
				}
			}
		}

		// Token: 0x04000F81 RID: 3969
		private GameObject m_depot;

		// Token: 0x04000F82 RID: 3970
		private List<SpawnableObject> m_objList = new List<SpawnableObject>();
	}
}
