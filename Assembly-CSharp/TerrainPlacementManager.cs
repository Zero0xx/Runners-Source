using System;
using System.Collections;
using System.Collections.Generic;
using Message;
using UnityEngine;

// Token: 0x02000292 RID: 658
public class TerrainPlacementManager : MonoBehaviour
{
	// Token: 0x06001207 RID: 4615 RVA: 0x0006533C File Offset: 0x0006353C
	private void Start()
	{
		base.tag = "StageManager";
		this.m_defaultLayer = LayerMask.NameToLayer("Terrain");
	}

	// Token: 0x06001208 RID: 4616 RVA: 0x0006535C File Offset: 0x0006355C
	private void Update()
	{
		float x = base.transform.position.x;
		this.CheckRangeIn(x);
		this.CheckRangeOut(x);
	}

	// Token: 0x06001209 RID: 4617 RVA: 0x0006538C File Offset: 0x0006358C
	public void Setup(bool isBossStage)
	{
		this.m_isBossStage = isBossStage;
		GameObject gameObject = ResourceManager.Instance.GetGameObject(ResourceCategory.TERRAIN_MODEL, TerrainXmlData.DataAssetName);
		if (gameObject == null)
		{
			return;
		}
		TerrainXmlData component = gameObject.GetComponent<TerrainXmlData>();
		if (component == null)
		{
			return;
		}
		this.m_terrainList = ImportTerrains.Import(component.TerrainBlock);
		this.m_placementList = new List<TerrainPlacementInfo>();
		StageBlockManager component2 = base.gameObject.GetComponent<StageBlockManager>();
		if (component2 == null)
		{
			return;
		}
		if (this.m_terrainList == null)
		{
			return;
		}
		int terrainCount = this.m_terrainList.GetTerrainCount();
		for (int i = 0; i < terrainCount; i++)
		{
			this.AddTerrainInfo(component2, i);
		}
		for (int j = 91; j <= 99; j++)
		{
			this.AddTerrainInfo(component2, j);
		}
		base.StartCoroutine(this.CreateTerrain());
	}

	// Token: 0x0600120A RID: 4618 RVA: 0x0006546C File Offset: 0x0006366C
	public void ReCreateTerrain()
	{
		this.DeleteTerrain();
		base.StartCoroutine(this.CreateTerrain());
	}

	// Token: 0x0600120B RID: 4619 RVA: 0x00065484 File Offset: 0x00063684
	private void AddTerrainInfo(StageBlockManager blockManager, int index)
	{
		Terrain terrain = this.m_terrainList.GetTerrain(index);
		if (terrain == null)
		{
			return;
		}
		int terrainIndex = int.Parse(terrain.m_name);
		float meter = terrain.m_meter;
		blockManager.AddTerrainInfo(terrainIndex, meter);
	}

	// Token: 0x0600120C RID: 4620 RVA: 0x000654C0 File Offset: 0x000636C0
	public void ActivateTerrain(int terrainIndex, Vector3 originPosition)
	{
		if (this.m_terrainList == null)
		{
			return;
		}
		Terrain terrain = this.m_terrainList.GetTerrain(terrainIndex);
		if (terrain == null)
		{
			return;
		}
		int blockCount = terrain.GetBlockCount();
		for (int i = 0; i < blockCount; i++)
		{
			TerrainBlock block = terrain.GetBlock(i);
			if (block != null)
			{
				string name = block.m_name;
				GameObject gameObject = ResourceManager.Instance.GetGameObject(ResourceCategory.TERRAIN_MODEL, name);
				if (!(gameObject == null))
				{
					TerrainPlacementInfo terrainPlacementInfo = new TerrainPlacementInfo();
					terrainPlacementInfo.m_terrainIndex = terrainIndex;
					Vector3 pos = originPosition + block.m_transform.m_pos;
					Vector3 rot = block.m_transform.m_rot;
					TransformParam transform = new TransformParam(pos, rot);
					terrainPlacementInfo.m_block = new TerrainBlock(block.m_name, transform);
					gameObject.layer = this.m_defaultLayer;
					this.m_placementList.Add(terrainPlacementInfo);
				}
			}
		}
	}

	// Token: 0x0600120D RID: 4621 RVA: 0x000655AC File Offset: 0x000637AC
	public void DeactivateTerrain(int terrainIndex, float basePosition)
	{
		for (int i = this.m_placementList.Count - 1; i >= 0; i--)
		{
			TerrainPlacementInfo terrainPlacementInfo = this.m_placementList[i];
			if (terrainPlacementInfo != null)
			{
				if (terrainPlacementInfo.m_terrainIndex == terrainIndex && StageBlockUtil.IsPastPosition(terrainPlacementInfo.m_block.m_transform.m_pos.x, basePosition, 0f))
				{
					if (terrainPlacementInfo.IsReserveTerrain())
					{
						this.ReturnTerrainReserveObject(terrainPlacementInfo);
					}
					else
					{
						this.DestroyTerrain(terrainPlacementInfo);
					}
					this.m_placementList.Remove(terrainPlacementInfo);
				}
			}
		}
	}

	// Token: 0x0600120E RID: 4622 RVA: 0x00065650 File Offset: 0x00063850
	private void DeleteTerrain()
	{
		foreach (TerrainReserveObject terrainReserveObject in this.m_reserveObjectList)
		{
			GameObject gameObject = terrainReserveObject.GetGameObject();
			if (gameObject != null)
			{
				UnityEngine.Object.Destroy(gameObject);
			}
		}
		this.m_reserveObjectList.Clear();
		this.m_isCreateTerrain = false;
	}

	// Token: 0x0600120F RID: 4623 RVA: 0x000656DC File Offset: 0x000638DC
	private IEnumerator CreateTerrain()
	{
		if (!this.m_isCreateTerrain)
		{
			string stageName = "w01";
			GameModeStage gameModeStage = GameObjectUtil.FindGameObjectComponent<GameModeStage>("GameModeStage");
			if (gameModeStage != null)
			{
				stageName = gameModeStage.GetStageName();
			}
			TerrainPlacementManager.TerrainReserveInfo[] tbl = (!this.m_isBossStage) ? this.TERRAIN_MODEL_TBL : this.BOSS_STAGE_TERRAIN_MODEL_TBL;
			foreach (TerrainPlacementManager.TerrainReserveInfo dataInfo in tbl)
			{
				string blockName = dataInfo.GetBlockName(stageName);
				GameObject gameObject = ResourceManager.Instance.GetGameObject(ResourceCategory.TERRAIN_MODEL, blockName);
				if (gameObject != null)
				{
					for (int index = 0; index < dataInfo.m_count; index++)
					{
						GameObject gameObjectCopy = UnityEngine.Object.Instantiate(gameObject, Vector3.zero, Quaternion.identity) as GameObject;
						if (gameObjectCopy != null)
						{
							gameObjectCopy.isStatic = true;
							gameObjectCopy.SetActive(true);
							TerrainReserveObject data = new TerrainReserveObject(gameObjectCopy, blockName, this.m_reserveObjectList.Count);
							if (data != null)
							{
								this.m_reserveObjectList.Add(data);
							}
						}
					}
				}
			}
			if (this.m_reserveObjectList.Count > 0)
			{
				int waiteframe = 1;
				while (waiteframe > 0)
				{
					waiteframe--;
					yield return null;
				}
				foreach (TerrainReserveObject obj in this.m_reserveObjectList)
				{
					if (obj != null)
					{
						obj.ReturnObject();
					}
				}
				this.m_isCreateTerrain = true;
				GC.Collect();
			}
		}
		yield break;
	}

	// Token: 0x06001210 RID: 4624 RVA: 0x000656F8 File Offset: 0x000638F8
	private TerrainReserveObject GetTerrainReserveObject(string blockName)
	{
		foreach (TerrainReserveObject terrainReserveObject in this.m_reserveObjectList)
		{
			if (terrainReserveObject != null && terrainReserveObject.EableReservation && terrainReserveObject.blockName == blockName)
			{
				return terrainReserveObject;
			}
		}
		return null;
	}

	// Token: 0x06001211 RID: 4625 RVA: 0x00065784 File Offset: 0x00063984
	private void ReturnTerrainReserveObject(TerrainPlacementInfo info)
	{
		if (info != null)
		{
			foreach (TerrainReserveObject terrainReserveObject in this.m_reserveObjectList)
			{
				if (terrainReserveObject != null && terrainReserveObject.ReserveIndex == info.ReserveIndex)
				{
					terrainReserveObject.ReturnObject();
					info.DestroyObject();
					break;
				}
			}
		}
	}

	// Token: 0x06001212 RID: 4626 RVA: 0x00065814 File Offset: 0x00063A14
	private void CheckRangeIn(float basePosition)
	{
		if (this.m_placementList == null)
		{
			return;
		}
		List<TerrainPlacementInfo> list = null;
		foreach (TerrainPlacementInfo terrainPlacementInfo in this.m_placementList)
		{
			if (terrainPlacementInfo != null)
			{
				if (!terrainPlacementInfo.Created)
				{
					float x = terrainPlacementInfo.m_block.m_transform.m_pos.x;
					float num = x - basePosition;
					if (num > 200f)
					{
						break;
					}
					if (num < 40f)
					{
						TerrainReserveObject terrainReserveObject = this.GetTerrainReserveObject(terrainPlacementInfo.m_block.m_name);
						if (terrainReserveObject != null)
						{
							terrainPlacementInfo.m_gameObject = terrainReserveObject.ReserveObject();
							terrainPlacementInfo.ReserveIndex = terrainReserveObject.ReserveIndex;
							Vector3 pos = terrainPlacementInfo.m_block.m_transform.m_pos;
							pos.z += 1.5f;
							terrainPlacementInfo.m_gameObject.transform.position = pos;
							terrainPlacementInfo.m_gameObject.transform.rotation = Quaternion.Euler(terrainPlacementInfo.m_block.m_transform.m_rot);
							terrainPlacementInfo.m_gameObject.SetActive(true);
						}
						else
						{
							global::Debug.Log("Terrain Instantiate!!  m_block = " + terrainPlacementInfo.m_block.m_name);
							if (!this.CreateTerrainObject(terrainPlacementInfo))
							{
								if (list == null)
								{
									list = new List<TerrainPlacementInfo>();
								}
								list.Add(terrainPlacementInfo);
							}
						}
					}
				}
			}
		}
		if (list == null)
		{
			return;
		}
		foreach (TerrainPlacementInfo terrainPlacementInfo2 in list)
		{
			if (terrainPlacementInfo2 != null)
			{
				this.m_placementList.Remove(terrainPlacementInfo2);
			}
		}
	}

	// Token: 0x06001213 RID: 4627 RVA: 0x00065A20 File Offset: 0x00063C20
	private void CheckRangeOut(float basePosition)
	{
		if (this.m_placementList == null)
		{
			return;
		}
		List<TerrainPlacementInfo> list = null;
		foreach (TerrainPlacementInfo terrainPlacementInfo in this.m_placementList)
		{
			if (terrainPlacementInfo != null)
			{
				if (terrainPlacementInfo.Created)
				{
					if (!terrainPlacementInfo.Destroyed)
					{
						float num = basePosition - terrainPlacementInfo.m_block.m_transform.m_pos.x;
						if (num < 0f)
						{
							break;
						}
						if (num > 30f)
						{
							if (terrainPlacementInfo.IsReserveTerrain())
							{
								this.ReturnTerrainReserveObject(terrainPlacementInfo);
							}
							else
							{
								this.DestroyTerrain(terrainPlacementInfo);
							}
							if (list == null)
							{
								list = new List<TerrainPlacementInfo>();
							}
							list.Add(terrainPlacementInfo);
						}
					}
				}
			}
		}
		if (list == null)
		{
			return;
		}
		foreach (TerrainPlacementInfo terrainPlacementInfo2 in list)
		{
			if (terrainPlacementInfo2 != null)
			{
				this.m_placementList.Remove(terrainPlacementInfo2);
			}
		}
	}

	// Token: 0x06001214 RID: 4628 RVA: 0x00065B90 File Offset: 0x00063D90
	private bool CreateTerrainObject(TerrainPlacementInfo info)
	{
		if (info == null)
		{
			return false;
		}
		string name = info.m_block.m_name;
		GameObject gameObject = ResourceManager.Instance.GetGameObject(ResourceCategory.TERRAIN_MODEL, name);
		if (gameObject == null)
		{
			return false;
		}
		Vector3 pos = info.m_block.m_transform.m_pos;
		pos.z += 1.5f;
		Vector3 rot = info.m_block.m_transform.m_rot;
		Quaternion rotation = Quaternion.Euler(rot.x, rot.y, rot.z);
		GameObject gameObject2 = UnityEngine.Object.Instantiate(gameObject, pos, rotation) as GameObject;
		if (gameObject2 == null)
		{
			return false;
		}
		gameObject2.isStatic = true;
		gameObject2.SetActive(true);
		info.m_gameObject = gameObject2;
		return true;
	}

	// Token: 0x06001215 RID: 4629 RVA: 0x00065C58 File Offset: 0x00063E58
	private void DestroyTerrain(TerrainPlacementInfo info)
	{
		UnityEngine.Object.Destroy(info.m_gameObject);
		info.DestroyObject();
	}

	// Token: 0x06001216 RID: 4630 RVA: 0x00065C6C File Offset: 0x00063E6C
	public void OnActivateBlock(MsgActivateBlock msg)
	{
		if (msg != null)
		{
			this.ActivateTerrain(msg.m_blockNo, msg.m_originPosition);
		}
	}

	// Token: 0x06001217 RID: 4631 RVA: 0x00065C88 File Offset: 0x00063E88
	private void OnDeactivateBlock(MsgDeactivateBlock msg)
	{
		if (msg != null)
		{
			this.DeactivateTerrain(msg.m_blockNo, msg.m_distance);
		}
	}

	// Token: 0x06001218 RID: 4632 RVA: 0x00065CA4 File Offset: 0x00063EA4
	private void OnDeactivateAllBlock(MsgDeactivateAllBlock msg)
	{
		foreach (TerrainPlacementInfo terrainPlacementInfo in this.m_placementList)
		{
			if (terrainPlacementInfo.IsReserveTerrain())
			{
				this.ReturnTerrainReserveObject(terrainPlacementInfo);
			}
			else
			{
				this.DestroyTerrain(terrainPlacementInfo);
			}
		}
		this.m_placementList.Clear();
	}

	// Token: 0x06001219 RID: 4633 RVA: 0x00065D2C File Offset: 0x00063F2C
	private void OnMsgExitStage(MsgExitStage msg)
	{
		base.enabled = false;
	}

	// Token: 0x0400103D RID: 4157
	private const float RangeInDistance = 40f;

	// Token: 0x0400103E RID: 4158
	private const float RangeOutDistance = 30f;

	// Token: 0x0400103F RID: 4159
	private const float MaxOfRange = 200f;

	// Token: 0x04001040 RID: 4160
	private const int SystemBlockIndexStart = 91;

	// Token: 0x04001041 RID: 4161
	private const int SystemBlockIndexEnd = 99;

	// Token: 0x04001042 RID: 4162
	private const float ZOffset = 1.5f;

	// Token: 0x04001043 RID: 4163
	public TextAsset m_setData;

	// Token: 0x04001044 RID: 4164
	private TerrainList m_terrainList;

	// Token: 0x04001045 RID: 4165
	private List<TerrainPlacementInfo> m_placementList;

	// Token: 0x04001046 RID: 4166
	private List<TerrainReserveObject> m_reserveObjectList = new List<TerrainReserveObject>();

	// Token: 0x04001047 RID: 4167
	private int m_defaultLayer;

	// Token: 0x04001048 RID: 4168
	private bool m_isBossStage;

	// Token: 0x04001049 RID: 4169
	private bool m_isCreateTerrain;

	// Token: 0x0400104A RID: 4170
	private TerrainPlacementManager.TerrainReserveInfo[] TERRAIN_MODEL_TBL = new TerrainPlacementManager.TerrainReserveInfo[]
	{
		new TerrainPlacementManager.TerrainReserveInfo("_pf_002m", 8),
		new TerrainPlacementManager.TerrainReserveInfo("_pf_006m", 4),
		new TerrainPlacementManager.TerrainReserveInfo("_pf_012m", 3),
		new TerrainPlacementManager.TerrainReserveInfo("_pf_024m", 3),
		new TerrainPlacementManager.TerrainReserveInfo("_pfloop_014m", 3),
		new TerrainPlacementManager.TerrainReserveInfo("_pfslopedown_012m", 3),
		new TerrainPlacementManager.TerrainReserveInfo("_pfslopedown_014m", 3),
		new TerrainPlacementManager.TerrainReserveInfo("_pfslopedown_018m", 2),
		new TerrainPlacementManager.TerrainReserveInfo("_pfslopeup_012m", 3),
		new TerrainPlacementManager.TerrainReserveInfo("_pfslopeup_014m", 3),
		new TerrainPlacementManager.TerrainReserveInfo("_pfslopeup_018m", 2),
		new TerrainPlacementManager.TerrainReserveInfo("_pfslope_down_012m", 3),
		new TerrainPlacementManager.TerrainReserveInfo("_pfslope_down_014m", 3),
		new TerrainPlacementManager.TerrainReserveInfo("_pfslope_down_018m", 2),
		new TerrainPlacementManager.TerrainReserveInfo("_pfslope_up_012m", 3),
		new TerrainPlacementManager.TerrainReserveInfo("_pfslope_up_014m", 3),
		new TerrainPlacementManager.TerrainReserveInfo("_pfslope_up_018m", 2)
	};

	// Token: 0x0400104B RID: 4171
	private TerrainPlacementManager.TerrainReserveInfo[] BOSS_STAGE_TERRAIN_MODEL_TBL = new TerrainPlacementManager.TerrainReserveInfo[]
	{
		new TerrainPlacementManager.TerrainReserveInfo("_pf_002m", 4),
		new TerrainPlacementManager.TerrainReserveInfo("_pf_006m", 4),
		new TerrainPlacementManager.TerrainReserveInfo("_pf_012m", 3),
		new TerrainPlacementManager.TerrainReserveInfo("_pf_024m", 3)
	};

	// Token: 0x02000293 RID: 659
	public class TerrainReserveInfo
	{
		// Token: 0x0600121A RID: 4634 RVA: 0x00065D38 File Offset: 0x00063F38
		public TerrainReserveInfo(string name, int count)
		{
			this.m_baseName = name;
			this.m_count = count;
		}

		// Token: 0x0600121B RID: 4635 RVA: 0x00065D50 File Offset: 0x00063F50
		public string GetBlockName(string stageName)
		{
			return stageName + this.m_baseName;
		}

		// Token: 0x0400104C RID: 4172
		public string m_baseName;

		// Token: 0x0400104D RID: 4173
		public int m_count;
	}
}
