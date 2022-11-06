using System;
using System.Collections.Generic;
using App;
using Message;
using UnityEngine;

// Token: 0x0200027C RID: 636
[AddComponentMenu("Scripts/Runners/Game/Level/StageBlockManager")]
[Serializable]
public class StageBlockManager : MonoBehaviour
{
	// Token: 0x0600116A RID: 4458 RVA: 0x00062E60 File Offset: 0x00061060
	private void Start()
	{
		this.m_showStageInfo = false;
		App.Random.ShuffleInt(this.m_highSpeedTable);
	}

	// Token: 0x0600116B RID: 4459 RVA: 0x00062E74 File Offset: 0x00061074
	private void Update()
	{
		if (this.m_playerInformation != null)
		{
			if (this.m_playerInformation.Position.x <= base.transform.position.x)
			{
				return;
			}
			base.transform.position.Set(this.m_playerInformation.Position.x, base.transform.position.y, base.transform.position.z);
			this.m_totalDistance = base.transform.position.x;
			if (this.m_levelInformation != null)
			{
				this.m_levelInformation.DistanceOnStage = this.m_totalDistance;
			}
		}
		this.CheckNextBlock();
	}

	// Token: 0x0600116C RID: 4460 RVA: 0x00062F54 File Offset: 0x00061154
	private void CheckNextBlock()
	{
		if (this.CurrentBlock != null)
		{
			if (this.NextBlock == null)
			{
				if (this.CurrentBlock.IsNearToGoal(this.m_totalDistance, 150f))
				{
					StageBlockManager.BlockOrderInfo nextActivateBlockInfo = this.GetNextActivateBlockInfo();
					if (nextActivateBlockInfo != null)
					{
						Vector3 endPoint = this.CurrentBlock.m_endPoint;
						MsgActivateBlock.CheckPoint checkPoint = MsgActivateBlock.CheckPoint.None;
						if (this.m_orderType == StageBlockManager.OrderType.DEBUG || this.m_orderType == StageBlockManager.OrderType.RANDOM || this.m_orderType == StageBlockManager.OrderType.ASCENDING)
						{
							if (this.m_currentOrderNum == 1 && this.m_playerInformation != null && this.m_playerInformation.SpeedLevel > PlayerSpeed.LEVEL_1)
							{
								checkPoint = MsgActivateBlock.CheckPoint.First;
							}
							else if (this.m_currentOrderNum > 1 && this.m_currentOrderNum % this.m_apeearCheckPointNumber == 1)
							{
								checkPoint = MsgActivateBlock.CheckPoint.Internal;
							}
						}
						this.ActivateBlock(nextActivateBlockInfo, endPoint, false, this.m_orderType, this.m_currentOrderNum, checkPoint);
					}
				}
			}
			else
			{
				StageBlockManager.StageBlockInfo nextBlock = this.NextBlock;
				if (this.CurrentBlock.IsNearToGoal(this.m_totalDistance, 1f) && nextBlock != null)
				{
					this.ChangeCurrentBlock(nextBlock);
					if (this.m_nextBossBlock)
					{
						MsgBossStart value = new MsgBossStart();
						GameObjectUtil.SendMessageFindGameObject("GameModeStage", "OnSendToGameModeStage", value, SendMessageOptions.DontRequireReceiver);
						this.m_nextBossBlock = false;
					}
				}
			}
		}
		this.SearchAndDeleteRangeOutedBlock(this.m_totalDistance);
	}

	// Token: 0x0600116D RID: 4461 RVA: 0x000630AC File Offset: 0x000612AC
	public void Initialize(StageBlockManager.CreateInfo cinfo)
	{
		this.m_quickMode = cinfo.quickMode;
		this.m_stageName = cinfo.stageName;
		this.m_totalDistance = 0f;
		GameObject gameObject = GameObject.Find("PlayerInformation");
		if (gameObject != null && this.m_playerInformation == null)
		{
			this.m_playerInformation = gameObject.GetComponent<PlayerInformation>();
		}
		if (this.m_levelInformation == null)
		{
			this.m_levelInformation = GameObjectUtil.FindGameObjectComponent<LevelInformation>("LevelInformation");
		}
		for (int i = 0; i < this.m_currentBlockLevelData.Length; i++)
		{
			this.m_currentBlockLevelData[i] = new StageBlockManager.BlockLevelData();
			if (this.m_quickMode)
			{
				if (i < this.m_blockLevelDataForQuickMode.Length)
				{
					this.m_blockLevelDataForQuickMode[i].CopyTo(this.m_currentBlockLevelData[i]);
				}
			}
			else if (i < this.m_blockLevelData.Length)
			{
				this.m_blockLevelData[i].CopyTo(this.m_currentBlockLevelData[i]);
			}
		}
		if (this.m_terrainPlacement == null)
		{
			this.m_terrainPlacement = new Dictionary<int, float>();
		}
		if (this.m_activeBlockInfo == null)
		{
			this.m_activeBlockInfo = new Dictionary<int, StageBlockManager.StageBlockInfo>();
		}
		if (cinfo.isSpawnableManager)
		{
			this.m_objectSpawnableManager = base.gameObject.AddComponent<ObjectSpawnManager>();
			this.m_objectSpawnableManager.enabled = false;
		}
		if (cinfo.isTerrainManager)
		{
			this.m_terrainPlacementManager = base.gameObject.AddComponent<TerrainPlacementManager>();
			this.m_terrainPlacementManager.enabled = false;
		}
		if (cinfo.isPathBlockManager && cinfo.pathManager)
		{
			this.m_stagePathManager = base.gameObject.AddComponent<StageBlockPathManager>();
			this.m_stagePathManager.SetPathManager(cinfo.pathManager);
			this.m_stagePathManager.enabled = false;
		}
		this.m_showStageInfo = cinfo.showInfo;
		if (cinfo.bossMode)
		{
			this.m_orderType = StageBlockManager.OrderType.BOSS_SINGLE;
		}
		else
		{
			this.m_orderType = ((!cinfo.randomBlock) ? StageBlockManager.OrderType.ASCENDING : StageBlockManager.OrderType.RANDOM);
		}
		this.m_firstOrderType = this.m_orderType;
		this.m_currentOrderNum = 0;
		this.m_stageRepeatNum = 0;
		if (this.m_levelInformation != null)
		{
			for (int j = 0; j < this.m_currentBlockLevelData.Length; j++)
			{
				if (this.m_currentBlockLevelData[j].repeatNum > 0)
				{
					this.m_nowBlockLevelNum = j;
					break;
				}
			}
			this.m_nextBlockLevelNum = this.m_nowBlockLevelNum;
			this.m_levelInformation.FeverBossCount = this.m_nowBlockLevelNum;
		}
		if (this.m_orderType != StageBlockManager.OrderType.DEBUG)
		{
			this.m_firstBlockNoOnLevel = new int[this.m_currentBlockLevelData.Length];
			int num = 1;
			for (int k = 0; k < this.m_firstBlockNoOnLevel.Length; k++)
			{
				if (this.m_currentBlockLevelData[k] == null)
				{
					this.m_currentBlockLevelData[k] = new StageBlockManager.BlockLevelData();
				}
				this.m_firstBlockNoOnLevel[k] = num;
				if (k < 2)
				{
					num += this.m_currentBlockLevelData[k].numBlock;
				}
			}
		}
	}

	// Token: 0x0600116E RID: 4462 RVA: 0x000633B4 File Offset: 0x000615B4
	public void Setup(bool bossStage)
	{
		if (this.m_objectSpawnableManager != null)
		{
			this.m_objectSpawnableManager.Setup(bossStage);
		}
		if (this.m_terrainPlacementManager != null)
		{
			this.m_terrainPlacementManager.Setup(bossStage);
			this.m_terrainPlacementManager.enabled = true;
		}
		if (this.m_stagePathManager != null)
		{
			this.m_stagePathManager.Setup();
			this.m_stagePathManager.enabled = true;
		}
	}

	// Token: 0x0600116F RID: 4463 RVA: 0x00063430 File Offset: 0x00061630
	public bool IsSetupEnded()
	{
		return !this.m_objectSpawnableManager || this.m_objectSpawnableManager.IsDataLoaded();
	}

	// Token: 0x06001170 RID: 4464 RVA: 0x00063450 File Offset: 0x00061650
	public void PauseTerrainPlacement(bool value)
	{
		if (this.m_terrainPlacementManager)
		{
			this.m_terrainPlacementManager.enabled = !value;
		}
	}

	// Token: 0x06001171 RID: 4465 RVA: 0x00063474 File Offset: 0x00061674
	public void AddTerrainInfo(int terrainIndex, float terrainLength)
	{
		if (this.m_terrainPlacement.ContainsKey(terrainIndex))
		{
			return;
		}
		this.m_terrainPlacement.Add(terrainIndex, terrainLength);
	}

	// Token: 0x06001172 RID: 4466 RVA: 0x00063498 File Offset: 0x00061698
	public void DeactivateAll()
	{
		MsgDeactivateAllBlock value = new MsgDeactivateAllBlock();
		base.gameObject.SendMessage("OnDeactivateAllBlock", value, SendMessageOptions.DontRequireReceiver);
		this.m_activeBlockInfo.Clear();
		this.m_numCreateBlock = 0;
		this.m_currentBlock = null;
	}

	// Token: 0x06001173 RID: 4467 RVA: 0x000634D8 File Offset: 0x000616D8
	public void ReCreateTerrain()
	{
		if (this.m_terrainPlacementManager != null)
		{
			this.m_terrainPlacementManager.ReCreateTerrain();
		}
	}

	// Token: 0x06001174 RID: 4468 RVA: 0x000634F8 File Offset: 0x000616F8
	private void ActiveFirstBlock(Vector3 position, Quaternion rotation, bool isGameStartBlock, bool insertStartAct)
	{
		switch (this.m_orderType)
		{
		case StageBlockManager.OrderType.BOSS_SINGLE:
			goto IL_64;
		case StageBlockManager.OrderType.TUTORIAL:
			goto IL_64;
		case StageBlockManager.OrderType.DEBUG:
			this.m_blockOrder = null;
			goto IL_64;
		}
		this.m_blockOrder = null;
		this.MakeOrderTable(this.m_nowBlockLevelNum, this.m_firstBlockNoOnLevel[this.m_nowBlockLevelNum], isGameStartBlock, insertStartAct);
		IL_64:
		if (this.m_blockOrder != null && this.m_blockOrder.Length > 0)
		{
			this.m_currentOrderNum = 0;
			StageBlockManager.StageBlockInfo stageBlockInfo = this.ActivateBlock(this.m_blockOrder[this.m_currentOrderNum], position, true, this.m_orderType, this.m_currentOrderNum, MsgActivateBlock.CheckPoint.None);
			if (stageBlockInfo != null)
			{
				this.ChangeCurrentBlock(stageBlockInfo);
			}
		}
	}

	// Token: 0x06001175 RID: 4469 RVA: 0x000635BC File Offset: 0x000617BC
	private void SetOrderMode(StageBlockManager.OrderType type)
	{
		this.m_orderType = type;
	}

	// Token: 0x06001176 RID: 4470 RVA: 0x000635C8 File Offset: 0x000617C8
	private void SetOrderModeToDefault()
	{
		this.m_orderType = this.m_firstOrderType;
	}

	// Token: 0x06001177 RID: 4471 RVA: 0x000635D8 File Offset: 0x000617D8
	private void ChangeNextOrderModeToFeverBoss()
	{
		this.SetOrderMode(StageBlockManager.OrderType.BOSS_SINGLE);
		this.CreateBlockOrderInfo(1);
		int layer = Mathf.Min(1 + this.m_nowBlockLevelNum, 3);
		this.m_blockOrder[0].SetBlockAndFixedLayer(0, layer);
		this.m_blockOrder[0].isBoss = true;
		this.m_currentOrderNum = 0;
		this.m_nextBossBlock = true;
	}

	// Token: 0x06001178 RID: 4472 RVA: 0x00063630 File Offset: 0x00061830
	private void ResetLevelInformationOnReplace()
	{
		if (this.m_levelInformation != null && this.m_blockOrder != null)
		{
			float num = 0f;
			foreach (StageBlockManager.BlockOrderInfo blockOrderInfo in this.m_blockOrder)
			{
				num += this.GetTerrainLength(blockOrderInfo.blockNo);
			}
			this.m_levelInformation.DistanceToBossOnStart = num;
			this.m_levelInformation.DistanceOnStage = 0f;
		}
	}

	// Token: 0x06001179 RID: 4473 RVA: 0x000636AC File Offset: 0x000618AC
	private StageBlockManager.StageBlockInfo ActivateBlock(StageBlockManager.BlockOrderInfo orderInfo, Vector3 originPoint, bool replaceStage, StageBlockManager.OrderType orderType, int orderNum, MsgActivateBlock.CheckPoint checkPoint)
	{
		if (orderInfo == null)
		{
			return null;
		}
		float terrainLength = this.GetTerrainLength(orderInfo.blockNo);
		if (App.Math.NearZero(terrainLength, 1E-06f))
		{
			return null;
		}
		StageBlockManager.StageBlockInfo stageBlockInfo = new StageBlockManager.StageBlockInfo();
		this.m_numCreateBlock++;
		stageBlockInfo.m_activateNo = this.m_numCreateBlock;
		stageBlockInfo.m_blockNo = orderInfo.blockNo;
		stageBlockInfo.m_layerNo = orderInfo.GetNewLayerNo();
		stageBlockInfo.m_totalLength = terrainLength;
		stageBlockInfo.m_startPoint = originPoint;
		stageBlockInfo.m_endPoint = originPoint + new Vector3(terrainLength, 0f, 0f);
		stageBlockInfo.startCurrentCallback = orderInfo.startCurrentCallback;
		stageBlockInfo.endCurrentCallback = orderInfo.endCurrentCallback;
		stageBlockInfo.m_orderNum = orderNum;
		stageBlockInfo.m_orderType = orderType;
		this.m_activeBlockInfo.Add(stageBlockInfo.m_activateNo, stageBlockInfo);
		MsgActivateBlock msgActivateBlock = new MsgActivateBlock(this.m_stageName, stageBlockInfo.m_blockNo, stageBlockInfo.m_layerNo, stageBlockInfo.m_activateNo, originPoint, Quaternion.identity);
		msgActivateBlock.m_replaceStage = replaceStage;
		msgActivateBlock.m_checkPoint = checkPoint;
		ObjectSpawnManager component = base.gameObject.GetComponent<ObjectSpawnManager>();
		if (component != null)
		{
			component.OnActivateBlock(msgActivateBlock);
		}
		TerrainPlacementManager component2 = base.gameObject.GetComponent<TerrainPlacementManager>();
		if (component2 != null)
		{
			component2.OnActivateBlock(msgActivateBlock);
		}
		StageBlockPathManager component3 = base.gameObject.GetComponent<StageBlockPathManager>();
		if (component3 != null)
		{
			component3.OnActivateBlock(msgActivateBlock);
		}
		return stageBlockInfo;
	}

	// Token: 0x0600117A RID: 4474 RVA: 0x00063814 File Offset: 0x00061A14
	private void DeactivateBlock(StageBlockManager.StageBlockInfo info, float pos)
	{
		MsgDeactivateBlock value = new MsgDeactivateBlock(info.m_blockNo, info.m_activateNo, pos);
		base.gameObject.SendMessage("OnDeactivateBlock", value, SendMessageOptions.DontRequireReceiver);
		this.m_activeBlockInfo.Remove(info.m_activateNo);
	}

	// Token: 0x0600117B RID: 4475 RVA: 0x00063858 File Offset: 0x00061A58
	private void ChangeCurrentBlock(StageBlockManager.StageBlockInfo nextBlock)
	{
		if (this.CurrentBlock != null && this.CurrentBlock.endCurrentCallback != null)
		{
			this.CurrentBlock.endCurrentCallback(this.CurrentBlock);
		}
		this.CurrentBlock = nextBlock;
		if (this.CurrentBlock != null && this.CurrentBlock.startCurrentCallback != null)
		{
			this.CurrentBlock.startCurrentCallback(this.CurrentBlock);
		}
		MsgChangeCurrentBlock value = new MsgChangeCurrentBlock(this.CurrentBlock.m_blockNo, this.CurrentBlock.m_layerNo, this.CurrentBlock.m_activateNo);
		base.gameObject.SendMessage("OnChangeCurerntBlock", value, SendMessageOptions.DontRequireReceiver);
	}

	// Token: 0x0600117C RID: 4476 RVA: 0x00063908 File Offset: 0x00061B08
	private void SearchAndDeleteRangeOutedBlock(float pos)
	{
		if (this.m_activeBlockInfo == null)
		{
			return;
		}
		List<StageBlockManager.StageBlockInfo> list = null;
		foreach (StageBlockManager.StageBlockInfo stageBlockInfo in this.m_activeBlockInfo.Values)
		{
			if (stageBlockInfo.GetPastDistanceFromEndPoint(pos) > 150f)
			{
				if (list == null)
				{
					list = new List<StageBlockManager.StageBlockInfo>();
				}
				list.Add(stageBlockInfo);
			}
		}
		if (list == null)
		{
			return;
		}
		foreach (StageBlockManager.StageBlockInfo info in list)
		{
			this.DeactivateBlock(info, pos);
		}
	}

	// Token: 0x170002BF RID: 703
	// (get) Token: 0x0600117E RID: 4478 RVA: 0x00063A04 File Offset: 0x00061C04
	// (set) Token: 0x0600117D RID: 4477 RVA: 0x000639F8 File Offset: 0x00061BF8
	private StageBlockManager.StageBlockInfo CurrentBlock
	{
		get
		{
			return this.m_currentBlock;
		}
		set
		{
			this.m_currentBlock = value;
		}
	}

	// Token: 0x170002C0 RID: 704
	// (get) Token: 0x0600117F RID: 4479 RVA: 0x00063A0C File Offset: 0x00061C0C
	private StageBlockManager.StageBlockInfo NextBlock
	{
		get
		{
			StageBlockManager.StageBlockInfo currentBlock = this.CurrentBlock;
			if (currentBlock == null)
			{
				return null;
			}
			StageBlockManager.StageBlockInfo result = null;
			if (this.m_activeBlockInfo.TryGetValue(currentBlock.m_activateNo + 1, out result))
			{
				return result;
			}
			return null;
		}
	}

	// Token: 0x06001180 RID: 4480 RVA: 0x00063A48 File Offset: 0x00061C48
	private float GetTerrainLength(int block)
	{
		float result = 0f;
		if (this.m_terrainPlacement.TryGetValue(block, out result))
		{
			return result;
		}
		return 0f;
	}

	// Token: 0x06001181 RID: 4481 RVA: 0x00063A78 File Offset: 0x00061C78
	private StageBlockManager.BlockOrderInfo GetNextActivateBlockInfo()
	{
		switch (this.m_orderType)
		{
		case StageBlockManager.OrderType.BOSS_SINGLE:
			global::Debug.Log("CheckNextBlock:BOSS_SINGLE  " + this.m_currentBlock.m_blockNo.ToString());
			this.m_currentOrderNum = 0;
			goto IL_C0;
		case StageBlockManager.OrderType.TUTORIAL:
			this.ChangeNextOrderModeToFeverBoss();
			goto IL_C0;
		}
		this.m_currentOrderNum++;
		if (this.m_currentOrderNum >= this.m_nowBlockOrderNum)
		{
			this.ChangeNextOrderModeToFeverBoss();
		}
		else
		{
			global::Debug.Log("CheckNextBlock " + this.m_currentOrderNum.ToString() + " " + this.m_blockOrder[this.m_currentOrderNum].blockNo.ToString());
		}
		IL_C0:
		if (this.m_currentOrderNum < this.m_blockOrder.Length)
		{
			return this.m_blockOrder[this.m_currentOrderNum];
		}
		return null;
	}

	// Token: 0x06001182 RID: 4482 RVA: 0x00063B68 File Offset: 0x00061D68
	private void MakeOrderTable(int numBlockLevelNum, int startBlock, bool isGameStartBlock, bool insertStartAct)
	{
		int num = this.m_currentBlockLevelData[numBlockLevelNum].numPlacement;
		int numBlock = this.m_currentBlockLevelData[numBlockLevelNum].numBlock;
		int num2 = startBlock + numBlock;
		int num3 = 0;
		if (insertStartAct)
		{
			num++;
		}
		this.CreateBlockOrderInfo(num);
		if (insertStartAct)
		{
			this.m_blockOrder[0].SetBlockAndFixedLayer(92, 0);
			StageBlockManager.BlockOrderInfo blockOrderInfo = this.m_blockOrder[0];
			blockOrderInfo.startCurrentCallback = (StageBlockManager.Callback)Delegate.Combine(blockOrderInfo.startCurrentCallback, new StageBlockManager.Callback(this.CallbackOnStageStartAct));
			if (isGameStartBlock)
			{
				StageBlockManager.BlockOrderInfo blockOrderInfo2 = this.m_blockOrder[0];
				blockOrderInfo2.endCurrentCallback = (StageBlockManager.Callback)Delegate.Combine(blockOrderInfo2.endCurrentCallback, new StageBlockManager.Callback(this.CallbackOnStageStartActEnd));
			}
			else
			{
				StageBlockManager.BlockOrderInfo blockOrderInfo3 = this.m_blockOrder[0];
				blockOrderInfo3.endCurrentCallback = (StageBlockManager.Callback)Delegate.Combine(blockOrderInfo3.endCurrentCallback, new StageBlockManager.Callback(this.CallbackOnStageReplaceActEnd));
			}
			num3 = 1;
		}
		StageBlockManager.OrderType orderType = this.m_orderType;
		if (orderType != StageBlockManager.OrderType.ASCENDING)
		{
			if (orderType == StageBlockManager.OrderType.RANDOM)
			{
				if (this.m_highSpeedSetFlag)
				{
					this.MakeOrderHighSpeedTable(numBlockLevelNum, startBlock, num3, isGameStartBlock, insertStartAct);
				}
				else
				{
					this.MakeOrderLowSpeedTable(numBlockLevelNum, startBlock, num3, isGameStartBlock, insertStartAct);
				}
			}
		}
		else
		{
			int num4 = startBlock;
			for (int i = num3; i < this.m_blockOrder.Length; i++)
			{
				this.m_blockOrder[i].SetBlockAndFixedLayer(num4, this.m_currentBlockLevelData[this.m_nowBlockLevelNum].layerNum);
				if (++num4 >= num2)
				{
					num4 = startBlock;
				}
			}
		}
		this.m_currentOrderNum = 0;
		this.m_nowBlockOrderNum = num;
	}

	// Token: 0x06001183 RID: 4483 RVA: 0x00063CF8 File Offset: 0x00061EF8
	private void MakeOrderHighSpeedTable(int numBlockLevelNum, int startBlock, int startOrderNum, bool isGameStartBlock, bool insertStartAct)
	{
		int numPlacement = this.m_currentBlockLevelData[numBlockLevelNum].numPlacement;
		int numBlock = this.m_currentBlockLevelData[numBlockLevelNum].numBlock;
		int num = startBlock + numBlock;
		int num2 = Mathf.Min(this.m_highSpeedCount * 2, 6);
		int num3 = Mathf.Min(num2 * 5, numBlock);
		int[] array = new int[num3];
		int num4 = 0;
		for (int i = 0; i < num2; i++)
		{
			for (int j = 0; j < 5; j++)
			{
				array[num4] = this.m_highSpeedTable[i] + j;
				if (array[num4] >= num)
				{
					array[num4] = startBlock;
				}
				num4++;
				if (num4 >= num3)
				{
					num4 = num3 - 1;
				}
			}
		}
		App.Random.ShuffleInt(array);
		int num5 = 0;
		if (insertStartAct)
		{
			num5++;
		}
		int num6 = 0;
		while (num6 < array.Length && num5 < this.m_blockOrder.Length)
		{
			int fixedLayerNo = this.m_currentBlockLevelData[this.m_nowBlockLevelNum].fixedLayerNo;
			if (fixedLayerNo == -1)
			{
				int layerNum = this.m_currentBlockLevelData[this.m_nowBlockLevelNum].layerNum;
				this.m_blockOrder[num5].SetBlockAndRandomLayer(array[num6], layerNum);
			}
			else
			{
				this.m_blockOrder[num5].SetBlockAndFixedLayer(array[num6], fixedLayerNo);
			}
			num5++;
			num6++;
		}
		this.m_highSpeedCount++;
	}

	// Token: 0x06001184 RID: 4484 RVA: 0x00063E60 File Offset: 0x00062060
	private void MakeOrderLowSpeedTable(int numBlockLevelNum, int startBlock, int startOrderNum, bool isGameStartBlock, bool insertStartAct)
	{
		int numPlacement = this.m_currentBlockLevelData[numBlockLevelNum].numPlacement;
		int numBlock = this.m_currentBlockLevelData[numBlockLevelNum].numBlock;
		int num = startBlock + numBlock;
		int num2 = (numBlock <= numPlacement) ? numPlacement : numBlock;
		int num3 = startBlock;
		int fixedBlockNo = this.m_startingBlockInfo.fixedBlockNo;
		if (isGameStartBlock && !this.m_quickMode)
		{
			int num4 = startOrderNum;
			for (int i = 0; i < fixedBlockNo; i++)
			{
				int startingLayerNum = this.m_startingBlockInfo.startingLayerNum;
				this.m_blockOrder[num4].SetBlockAndRandomLayer(startBlock + i, startingLayerNum);
				num4++;
			}
			num3 += fixedBlockNo;
			num2 -= fixedBlockNo;
		}
		int[] array = new int[num2];
		for (int j = 0; j < num2; j++)
		{
			array[j] = num3;
			if (++num3 >= num)
			{
				num3 = startBlock;
			}
		}
		App.Random.ShuffleInt(array);
		int num5 = (!isGameStartBlock || this.m_quickMode) ? 0 : fixedBlockNo;
		if (insertStartAct)
		{
			num5++;
		}
		int num6 = 0;
		while (num6 < array.Length && num5 < this.m_blockOrder.Length)
		{
			int fixedLayerNo = this.m_currentBlockLevelData[this.m_nowBlockLevelNum].fixedLayerNo;
			if (fixedLayerNo == -1)
			{
				int layerNum = this.m_currentBlockLevelData[this.m_nowBlockLevelNum].layerNum;
				this.m_blockOrder[num5].SetBlockAndRandomLayer(array[num6], layerNum);
			}
			else
			{
				this.m_blockOrder[num5].SetBlockAndFixedLayer(array[num6], fixedLayerNo);
			}
			num5++;
			num6++;
		}
	}

	// Token: 0x06001185 RID: 4485 RVA: 0x00064000 File Offset: 0x00062200
	private StageBlockManager.BlockOrderInfo GetCurerntBlockOrderInfo()
	{
		return this.m_blockOrder[this.m_currentOrderNum];
	}

	// Token: 0x170002C1 RID: 705
	// (get) Token: 0x06001186 RID: 4486 RVA: 0x00064010 File Offset: 0x00062210
	public string StageName
	{
		get
		{
			return this.m_stageName;
		}
	}

	// Token: 0x06001187 RID: 4487 RVA: 0x00064018 File Offset: 0x00062218
	public int GetBlockLevel()
	{
		return this.m_nowBlockLevelNum;
	}

	// Token: 0x06001188 RID: 4488 RVA: 0x00064020 File Offset: 0x00062220
	public bool IsBlockLevelUp()
	{
		return this.m_nowBlockLevelNum != this.m_nextBlockLevelNum;
	}

	// Token: 0x06001189 RID: 4489 RVA: 0x00064034 File Offset: 0x00062234
	private void OnMsgPrepareStageReplace(MsgPrepareStageReplace msg)
	{
		if (this.m_objectSpawnableManager == null)
		{
			return;
		}
		switch (this.m_firstOrderType)
		{
		case StageBlockManager.OrderType.BOSS_SINGLE:
			return;
		case StageBlockManager.OrderType.TUTORIAL:
			return;
		case StageBlockManager.OrderType.DEBUG:
			base.StartCoroutine(this.m_objectSpawnableManager.LoadSetTable(1, 91));
			return;
		}
		this.m_highSpeedSetFlag = (this.m_nextBlockLevelNum > 1);
		if (this.m_highSpeedSetFlag)
		{
			int readFileCount = Mathf.Min(this.m_highSpeedCount * 2, 6);
			base.StartCoroutine(this.m_objectSpawnableManager.LoadSetTable(this.m_highSpeedTable, readFileCount));
		}
		else
		{
			int num = Mathf.Min(this.m_nextBlockLevelNum, this.m_firstBlockNoOnLevel.Length - 1);
			base.StartCoroutine(this.m_objectSpawnableManager.LoadSetTable(this.m_firstBlockNoOnLevel[num], this.m_currentBlockLevelData[num].numBlock));
		}
	}

	// Token: 0x0600118A RID: 4490 RVA: 0x00064128 File Offset: 0x00062328
	public void OnMsgStageReplace(MsgStageReplace msg)
	{
		this.m_totalDistance = 0f;
		base.transform.position = msg.m_position;
		this.DeactivateAll();
		this.SetOrderModeToDefault();
		this.m_nowBlockLevelNum = Mathf.Min(this.m_nextBlockLevelNum, this.m_currentBlockLevelData.Length - 1);
		this.m_stageRepeatNum++;
		if (this.m_nowBlockLevelNum < this.m_currentBlockLevelData.Length && this.m_stageRepeatNum >= this.m_currentBlockLevelData[this.m_nowBlockLevelNum].repeatNum)
		{
			this.m_stageRepeatNum = 0;
			this.m_nextBlockLevelNum++;
		}
		bool isGameStartBlock;
		if (this.m_quickMode)
		{
			isGameStartBlock = !this.m_quickFirstReplaceEnd;
			this.m_quickFirstReplaceEnd = true;
		}
		else
		{
			isGameStartBlock = (msg.m_speedLevel == PlayerSpeed.LEVEL_1);
		}
		this.ActiveFirstBlock(msg.m_position, msg.m_rotation, isGameStartBlock, true);
		this.ResetLevelInformationOnReplace();
	}

	// Token: 0x0600118B RID: 4491 RVA: 0x00064214 File Offset: 0x00062414
	private void OnMsgSetStageOnMapBoss(MsgSetStageOnMapBoss msg)
	{
		this.m_totalDistance = 0f;
		base.transform.position = msg.m_position;
		int layer = 0;
		BossCategory bossCategory = BossTypeUtil.GetBossCategory(msg.m_bossType);
		if (bossCategory != BossCategory.MAP)
		{
			if (bossCategory == BossCategory.EVENT)
			{
				layer = 21 + BossTypeUtil.GetLayerNumber(msg.m_bossType);
			}
		}
		else
		{
			layer = 11 + BossTypeUtil.GetLayerNumber(msg.m_bossType);
		}
		this.SetOrderMode(StageBlockManager.OrderType.BOSS_SINGLE);
		this.CreateBlockOrderInfo(1);
		this.m_blockOrder[0].SetBlockAndFixedLayer(0, layer);
		if (this.m_levelInformation != null)
		{
			this.m_levelInformation.DistanceToBossOnStart = 0f;
		}
		this.ActiveFirstBlock(msg.m_position, msg.m_rotation, false, false);
	}

	// Token: 0x0600118C RID: 4492 RVA: 0x000642DC File Offset: 0x000624DC
	public void SetStageOnTutorial(Vector3 position)
	{
		this.m_totalDistance = 0f;
		base.transform.position = position;
		this.m_nowBlockLevelNum = 0;
		this.SetOrderMode(StageBlockManager.OrderType.TUTORIAL);
		this.CreateBlockOrderInfo(1);
		this.m_blockOrder[0].SetBlockAndFixedLayer(91, 0);
		this.ActiveFirstBlock(position, Quaternion.identity, false, false);
		this.ResetLevelInformationOnReplace();
	}

	// Token: 0x0600118D RID: 4493 RVA: 0x0006433C File Offset: 0x0006253C
	private void OnMsgTutorialResetForRetry(MsgTutorialResetForRetry msg)
	{
		base.transform.position = msg.m_position;
		StageBlockManager.OrderType orderType = this.m_currentBlock.m_orderType;
		Vector3 startPoint = this.m_currentBlock.m_startPoint;
		this.DeactivateAll();
		if (orderType == StageBlockManager.OrderType.TUTORIAL)
		{
			this.SetOrderMode(StageBlockManager.OrderType.TUTORIAL);
			this.CreateBlockOrderInfo(1);
			this.m_blockOrder[0].SetBlockAndFixedLayer(91, 0);
		}
		StageBlockManager.StageBlockInfo stageBlockInfo = this.ActivateBlock(this.m_blockOrder[this.m_currentOrderNum], startPoint, true, this.m_orderType, this.m_currentOrderNum, MsgActivateBlock.CheckPoint.None);
		if (stageBlockInfo != null)
		{
			this.ChangeCurrentBlock(stageBlockInfo);
		}
	}

	// Token: 0x0600118E RID: 4494 RVA: 0x000643D0 File Offset: 0x000625D0
	private void CallbackOnStageStartAct(StageBlockManager.StageBlockInfo info)
	{
		ObjUtil.PushCamera(CameraType.START_ACT, 0.1f);
		ObjUtil.PauseCombo(MsgPauseComboTimer.State.PAUSE, -1f);
	}

	// Token: 0x0600118F RID: 4495 RVA: 0x000643E8 File Offset: 0x000625E8
	private void CallbackOnStageStartActEnd(StageBlockManager.StageBlockInfo info)
	{
		ObjUtil.PopCamera(CameraType.START_ACT, 0f);
		this.SetQuickModeStart();
		ObjUtil.SendStartItemAndChao();
		ObjUtil.PauseCombo(MsgPauseComboTimer.State.PLAY, -1f);
	}

	// Token: 0x06001190 RID: 4496 RVA: 0x0006440C File Offset: 0x0006260C
	private void CallbackOnStageReplaceActEnd(StageBlockManager.StageBlockInfo info)
	{
		ObjUtil.PopCamera(CameraType.START_ACT, 0f);
		this.SetQuickModeStart();
		if (StageItemManager.Instance != null)
		{
			MsgPhatomItemOnBoss msg = new MsgPhatomItemOnBoss();
			StageItemManager.Instance.OnResumeItemOnBoss(msg);
		}
		GameObjectUtil.SendMessageToTagObjects("Chao", "OnResumeChangeLevel", null, SendMessageOptions.DontRequireReceiver);
		ObjUtil.PauseCombo(MsgPauseComboTimer.State.PLAY_RESET, -1f);
	}

	// Token: 0x06001191 RID: 4497 RVA: 0x00064468 File Offset: 0x00062668
	private StageBlockManager.BlockOrderInfo[] CreateBlockOrderInfo(int num)
	{
		this.m_blockOrder = new StageBlockManager.BlockOrderInfo[num];
		for (int i = 0; i < this.m_blockOrder.Length; i++)
		{
			this.m_blockOrder[i] = new StageBlockManager.BlockOrderInfo();
		}
		return this.m_blockOrder;
	}

	// Token: 0x06001192 RID: 4498 RVA: 0x000644B0 File Offset: 0x000626B0
	private bool IsHighSpeed()
	{
		return this.m_nextBlockLevelNum > 1 && this.m_nowBlockLevelNum > 1;
	}

	// Token: 0x06001193 RID: 4499 RVA: 0x000644E0 File Offset: 0x000626E0
	private void SetQuickModeStart()
	{
		if (this.m_quickMode && StageTimeManager.Instance != null)
		{
			StageTimeManager.Instance.PlayStart();
		}
	}

	// Token: 0x06001194 RID: 4500 RVA: 0x00064508 File Offset: 0x00062708
	public StageBlockManager.StageBlockInfo GetCurrenBlockInfo()
	{
		return this.m_currentBlock;
	}

	// Token: 0x06001195 RID: 4501 RVA: 0x00064510 File Offset: 0x00062710
	public float GetBlockLocalDistance()
	{
		if (this.CurrentBlock != null)
		{
			return this.CurrentBlock.GetBlockRunningLength(this.m_totalDistance);
		}
		return 0f;
	}

	// Token: 0x06001196 RID: 4502 RVA: 0x00064540 File Offset: 0x00062740
	public Vector3 GetBlockLocalPosition(Vector3 pos)
	{
		if (this.CurrentBlock != null)
		{
			return pos - this.CurrentBlock.m_startPoint;
		}
		return Vector3.zero;
	}

	// Token: 0x04000FAC RID: 4012
	private const int BLOCK_LEVEL_DATA_NUM = 5;

	// Token: 0x04000FAD RID: 4013
	private const int HIGH_SPEED_FILE_MAX_COUNT = 6;

	// Token: 0x04000FAE RID: 4014
	private const int HIGH_SPEED_FILE_ONCE_READ_LIMIT = 2;

	// Token: 0x04000FAF RID: 4015
	private const int DefaultLayerNo = 0;

	// Token: 0x04000FB0 RID: 4016
	private const float DistanceOfActivateNext = 150f;

	// Token: 0x04000FB1 RID: 4017
	private const float DistanceOfDeactivate = 150f;

	// Token: 0x04000FB2 RID: 4018
	private const float DistanceOfChangeCurrent = 1f;

	// Token: 0x04000FB3 RID: 4019
	public const int BossSingleBlockNo = 0;

	// Token: 0x04000FB4 RID: 4020
	public const int TutorialBlockNo = 91;

	// Token: 0x04000FB5 RID: 4021
	public const int StartActBlockNo = 92;

	// Token: 0x04000FB6 RID: 4022
	[SerializeField]
	public StageBlockManager.DebugBlockInfo[] m_debugBlockInfo;

	// Token: 0x04000FB7 RID: 4023
	private float m_totalDistance;

	// Token: 0x04000FB8 RID: 4024
	private int m_numCreateBlock;

	// Token: 0x04000FB9 RID: 4025
	private PlayerInformation m_playerInformation;

	// Token: 0x04000FBA RID: 4026
	private string m_stageName;

	// Token: 0x04000FBB RID: 4027
	private bool m_showStageInfo;

	// Token: 0x04000FBC RID: 4028
	private Rect m_window;

	// Token: 0x04000FBD RID: 4029
	private Dictionary<int, float> m_terrainPlacement;

	// Token: 0x04000FBE RID: 4030
	private Dictionary<int, StageBlockManager.StageBlockInfo> m_activeBlockInfo;

	// Token: 0x04000FBF RID: 4031
	private StageBlockManager.StageBlockInfo m_currentBlock;

	// Token: 0x04000FC0 RID: 4032
	private StageBlockManager.OrderType m_orderType;

	// Token: 0x04000FC1 RID: 4033
	private StageBlockManager.OrderType m_firstOrderType;

	// Token: 0x04000FC2 RID: 4034
	private StageBlockManager.BlockOrderInfo[] m_blockOrder;

	// Token: 0x04000FC3 RID: 4035
	private int m_currentOrderNum;

	// Token: 0x04000FC4 RID: 4036
	private int m_nowBlockOrderNum;

	// Token: 0x04000FC5 RID: 4037
	private int m_stageRepeatNum;

	// Token: 0x04000FC6 RID: 4038
	private ObjectSpawnManager m_objectSpawnableManager;

	// Token: 0x04000FC7 RID: 4039
	private TerrainPlacementManager m_terrainPlacementManager;

	// Token: 0x04000FC8 RID: 4040
	private StageBlockPathManager m_stagePathManager;

	// Token: 0x04000FC9 RID: 4041
	private LevelInformation m_levelInformation;

	// Token: 0x04000FCA RID: 4042
	private int[] m_highSpeedTable = new int[]
	{
		21,
		26,
		31,
		36,
		41,
		46
	};

	// Token: 0x04000FCB RID: 4043
	private int m_highSpeedCount = 1;

	// Token: 0x04000FCC RID: 4044
	private bool m_highSpeedSetFlag;

	// Token: 0x04000FCD RID: 4045
	[SerializeField]
	private StageBlockManager.BlockLevelData[] m_blockLevelData = new StageBlockManager.BlockLevelData[5];

	// Token: 0x04000FCE RID: 4046
	[SerializeField]
	private StageBlockManager.BlockLevelData[] m_blockLevelDataForQuickMode = new StageBlockManager.BlockLevelData[5];

	// Token: 0x04000FCF RID: 4047
	private StageBlockManager.BlockLevelData[] m_currentBlockLevelData = new StageBlockManager.BlockLevelData[5];

	// Token: 0x04000FD0 RID: 4048
	private int m_nowBlockLevelNum;

	// Token: 0x04000FD1 RID: 4049
	private int m_nextBlockLevelNum;

	// Token: 0x04000FD2 RID: 4050
	[SerializeField]
	private int m_apeearCheckPointNumber = 3;

	// Token: 0x04000FD3 RID: 4051
	[SerializeField]
	private StageBlockManager.StartingBlockInfo m_startingBlockInfo = new StageBlockManager.StartingBlockInfo();

	// Token: 0x04000FD4 RID: 4052
	[SerializeField]
	private bool m_useDebugOrder;

	// Token: 0x04000FD5 RID: 4053
	private int[] m_firstBlockNoOnLevel;

	// Token: 0x04000FD6 RID: 4054
	private bool m_nextBossBlock;

	// Token: 0x04000FD7 RID: 4055
	private bool m_quickMode;

	// Token: 0x04000FD8 RID: 4056
	private bool m_quickFirstReplaceEnd;

	// Token: 0x0200027D RID: 637
	public class StageBlockInfo
	{
		// Token: 0x170002C2 RID: 706
		// (get) Token: 0x06001198 RID: 4504 RVA: 0x00064578 File Offset: 0x00062778
		// (set) Token: 0x06001199 RID: 4505 RVA: 0x00064580 File Offset: 0x00062780
		public int m_activateNo { get; set; }

		// Token: 0x170002C3 RID: 707
		// (get) Token: 0x0600119A RID: 4506 RVA: 0x0006458C File Offset: 0x0006278C
		// (set) Token: 0x0600119B RID: 4507 RVA: 0x00064594 File Offset: 0x00062794
		public int m_blockNo { get; set; }

		// Token: 0x170002C4 RID: 708
		// (get) Token: 0x0600119C RID: 4508 RVA: 0x000645A0 File Offset: 0x000627A0
		// (set) Token: 0x0600119D RID: 4509 RVA: 0x000645A8 File Offset: 0x000627A8
		public int m_layerNo { get; set; }

		// Token: 0x170002C5 RID: 709
		// (get) Token: 0x0600119E RID: 4510 RVA: 0x000645B4 File Offset: 0x000627B4
		// (set) Token: 0x0600119F RID: 4511 RVA: 0x000645BC File Offset: 0x000627BC
		public float m_totalLength { get; set; }

		// Token: 0x170002C6 RID: 710
		// (get) Token: 0x060011A0 RID: 4512 RVA: 0x000645C8 File Offset: 0x000627C8
		// (set) Token: 0x060011A1 RID: 4513 RVA: 0x000645D0 File Offset: 0x000627D0
		public Vector3 m_startPoint { get; set; }

		// Token: 0x170002C7 RID: 711
		// (get) Token: 0x060011A2 RID: 4514 RVA: 0x000645DC File Offset: 0x000627DC
		// (set) Token: 0x060011A3 RID: 4515 RVA: 0x000645E4 File Offset: 0x000627E4
		public Vector3 m_endPoint { get; set; }

		// Token: 0x170002C8 RID: 712
		// (get) Token: 0x060011A4 RID: 4516 RVA: 0x000645F0 File Offset: 0x000627F0
		// (set) Token: 0x060011A5 RID: 4517 RVA: 0x000645F8 File Offset: 0x000627F8
		public StageBlockManager.OrderType m_orderType { get; set; }

		// Token: 0x170002C9 RID: 713
		// (get) Token: 0x060011A6 RID: 4518 RVA: 0x00064604 File Offset: 0x00062804
		// (set) Token: 0x060011A7 RID: 4519 RVA: 0x0006460C File Offset: 0x0006280C
		public int m_orderNum { get; set; }

		// Token: 0x170002CA RID: 714
		// (get) Token: 0x060011A8 RID: 4520 RVA: 0x00064618 File Offset: 0x00062818
		// (set) Token: 0x060011A9 RID: 4521 RVA: 0x00064620 File Offset: 0x00062820
		public StageBlockManager.Callback startCurrentCallback { get; set; }

		// Token: 0x170002CB RID: 715
		// (get) Token: 0x060011AA RID: 4522 RVA: 0x0006462C File Offset: 0x0006282C
		// (set) Token: 0x060011AB RID: 4523 RVA: 0x00064634 File Offset: 0x00062834
		public StageBlockManager.Callback endCurrentCallback { get; set; }

		// Token: 0x060011AC RID: 4524 RVA: 0x00064640 File Offset: 0x00062840
		public float GetBlockRunningLength(float totalLength)
		{
			return totalLength - this.m_startPoint.x;
		}

		// Token: 0x060011AD RID: 4525 RVA: 0x00064660 File Offset: 0x00062860
		public float GetPastDistanceFromEndPoint(float pos)
		{
			return pos - this.m_endPoint.x;
		}

		// Token: 0x060011AE RID: 4526 RVA: 0x00064680 File Offset: 0x00062880
		public bool IsNearToGoal(float nowLength, float remainLength)
		{
			float num = this.m_endPoint.x - nowLength;
			return remainLength > num;
		}
	}

	// Token: 0x0200027E RID: 638
	private class BlockOrderInfo
	{
		// Token: 0x060011B0 RID: 4528 RVA: 0x000646AC File Offset: 0x000628AC
		public void SetBlockAndFixedLayer(int block, int layer)
		{
			this.blockNo = block;
			this.fixedLayerNo = true;
			this.layerNo = layer;
		}

		// Token: 0x060011B1 RID: 4529 RVA: 0x000646C4 File Offset: 0x000628C4
		public void SetBlockAndRandomLayer(int block, int rndNum)
		{
			this.blockNo = block;
			this.fixedLayerNo = false;
			this.rndLayerNo = rndNum;
		}

		// Token: 0x060011B2 RID: 4530 RVA: 0x000646DC File Offset: 0x000628DC
		public int GetNewLayerNo()
		{
			if (this.fixedLayerNo)
			{
				return this.layerNo;
			}
			return UnityEngine.Random.Range(0, this.rndLayerNo);
		}

		// Token: 0x04000FE3 RID: 4067
		public int blockNo;

		// Token: 0x04000FE4 RID: 4068
		public bool fixedLayerNo;

		// Token: 0x04000FE5 RID: 4069
		public int layerNo;

		// Token: 0x04000FE6 RID: 4070
		public int rndLayerNo;

		// Token: 0x04000FE7 RID: 4071
		public bool isBoss;

		// Token: 0x04000FE8 RID: 4072
		public StageBlockManager.Callback startCurrentCallback;

		// Token: 0x04000FE9 RID: 4073
		public StageBlockManager.Callback endCurrentCallback;
	}

	// Token: 0x0200027F RID: 639
	[Serializable]
	public class BlockLevelData
	{
		// Token: 0x060011B4 RID: 4532 RVA: 0x00064728 File Offset: 0x00062928
		public void CopyTo(StageBlockManager.BlockLevelData dst)
		{
			dst.numBlock = this.numBlock;
			dst.numPlacement = this.numPlacement;
			dst.layerNum = this.layerNum;
			dst.repeatNum = this.repeatNum;
			dst.fixedLayerNo = this.fixedLayerNo;
		}

		// Token: 0x04000FEA RID: 4074
		public int numBlock = 5;

		// Token: 0x04000FEB RID: 4075
		public int numPlacement = 5;

		// Token: 0x04000FEC RID: 4076
		public int layerNum = 1;

		// Token: 0x04000FED RID: 4077
		public int repeatNum = 1;

		// Token: 0x04000FEE RID: 4078
		public int fixedLayerNo = -1;
	}

	// Token: 0x02000280 RID: 640
	[Serializable]
	public class StartingBlockInfo
	{
		// Token: 0x04000FEF RID: 4079
		public int fixedBlockNo = 2;

		// Token: 0x04000FF0 RID: 4080
		public int startingLayerNum = 1;
	}

	// Token: 0x02000281 RID: 641
	[Serializable]
	public class DebugBlockInfo
	{
		// Token: 0x04000FF1 RID: 4081
		public int block;

		// Token: 0x04000FF2 RID: 4082
		public int layer;
	}

	// Token: 0x02000282 RID: 642
	public enum FixedLayerNumber
	{
		// Token: 0x04000FF4 RID: 4084
		SYSTEM,
		// Token: 0x04000FF5 RID: 4085
		FEVER_BOSS_1,
		// Token: 0x04000FF6 RID: 4086
		FEVER_BOSS_2,
		// Token: 0x04000FF7 RID: 4087
		FEVER_BOSS_3,
		// Token: 0x04000FF8 RID: 4088
		MAP_BOSS_1 = 11,
		// Token: 0x04000FF9 RID: 4089
		MAP_BOSS_2,
		// Token: 0x04000FFA RID: 4090
		MAP_BOSS_3,
		// Token: 0x04000FFB RID: 4091
		EVENT_BOSS_1 = 21,
		// Token: 0x04000FFC RID: 4092
		EVENT_BOSS_2,
		// Token: 0x04000FFD RID: 4093
		EVENT_BOSS_3
	}

	// Token: 0x02000283 RID: 643
	public struct CreateInfo
	{
		// Token: 0x04000FFE RID: 4094
		public string stageName;

		// Token: 0x04000FFF RID: 4095
		public bool isSpawnableManager;

		// Token: 0x04001000 RID: 4096
		public bool isTerrainManager;

		// Token: 0x04001001 RID: 4097
		public bool isPathBlockManager;

		// Token: 0x04001002 RID: 4098
		public PathManager pathManager;

		// Token: 0x04001003 RID: 4099
		public bool showInfo;

		// Token: 0x04001004 RID: 4100
		public bool randomBlock;

		// Token: 0x04001005 RID: 4101
		public bool bossMode;

		// Token: 0x04001006 RID: 4102
		public bool quickMode;
	}

	// Token: 0x02000284 RID: 644
	public enum OrderType
	{
		// Token: 0x04001008 RID: 4104
		BOSS_SINGLE,
		// Token: 0x04001009 RID: 4105
		ASCENDING,
		// Token: 0x0400100A RID: 4106
		RANDOM,
		// Token: 0x0400100B RID: 4107
		TUTORIAL,
		// Token: 0x0400100C RID: 4108
		DEBUG
	}

	// Token: 0x02000A7E RID: 2686
	// (Invoke) Token: 0x06004832 RID: 18482
	public delegate void Callback(StageBlockManager.StageBlockInfo blockInfo);
}
