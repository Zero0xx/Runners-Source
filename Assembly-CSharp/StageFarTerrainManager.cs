using System;
using System.Collections.Generic;
using Message;
using UnityEngine;

// Token: 0x0200028E RID: 654
[AddComponentMenu("Scripts/Runners/Game/Level")]
public class StageFarTerrainManager : MonoBehaviour
{
	// Token: 0x060011EC RID: 4588 RVA: 0x00064DAC File Offset: 0x00062FAC
	private void Start()
	{
		this.m_playerInfo = GameObject.Find("PlayerInformation").GetComponent<PlayerInformation>();
		this.m_nowDrawingModel = new List<GameObject>();
	}

	// Token: 0x060011ED RID: 4589 RVA: 0x00064DDC File Offset: 0x00062FDC
	public void SetupModel(string stageName)
	{
		string text = stageName + "_far";
		if (TenseEffectManager.Instance != null)
		{
			TenseEffectManager.Type tenseType = TenseEffectManager.Instance.GetTenseType();
			text += ((tenseType != TenseEffectManager.Type.TENSE_A) ? "B" : "A");
		}
		this.m_nowSpawnedNumModels = 0;
		this.m_originalFarModel = ResourceManager.Instance.GetGameObject(ResourceCategory.TERRAIN_MODEL, text);
		this.InstantiateModel(StageFarTerrainManager.originalPosition, Quaternion.Euler(StageFarTerrainManager.originalRotation));
		Vector3 position = StageFarTerrainManager.originalPosition;
		position.x += 1500f;
		this.InstantiateModel(position, Quaternion.Euler(StageFarTerrainManager.originalRotation));
		this.m_nextSpawnOffset = StageFarTerrainManager.originalPosition.x + 1000f;
	}

	// Token: 0x060011EE RID: 4590 RVA: 0x00064EA0 File Offset: 0x000630A0
	private void Update()
	{
		if (this.m_playerInfo && this.m_originalFarModel && this.m_nowSpawnedNumModels > 0)
		{
			Vector3 position = this.m_playerInfo.Position;
			if (position.x > this.m_nextSpawnOffset)
			{
				Vector3 position2 = StageFarTerrainManager.originalPosition;
				position2.x += 1500f * (float)this.m_nowSpawnedNumModels;
				this.InstantiateModel(position2, Quaternion.Euler(StageFarTerrainManager.originalRotation));
				this.m_nextSpawnOffset += 1000f;
			}
			for (int i = this.m_nowDrawingModel.Count - 1; i >= 0; i--)
			{
				if (position.x - this.m_nowDrawingModel[i].transform.position.x > 1700f)
				{
					UnityEngine.Object.Destroy(this.m_nowDrawingModel[i]);
					this.m_nowDrawingModel.Remove(this.m_nowDrawingModel[i]);
				}
			}
		}
	}

	// Token: 0x060011EF RID: 4591 RVA: 0x00064FB4 File Offset: 0x000631B4
	private void InstantiateModel(Vector3 position, Quaternion rotation)
	{
		if (this.m_originalFarModel == null)
		{
			return;
		}
		GameObject gameObject = UnityEngine.Object.Instantiate(this.m_originalFarModel, position, rotation) as GameObject;
		if (gameObject)
		{
			gameObject.isStatic = true;
			gameObject.SetActive(true);
			this.m_nowDrawingModel.Add(gameObject);
			this.m_nowSpawnedNumModels++;
		}
	}

	// Token: 0x060011F0 RID: 4592 RVA: 0x0006501C File Offset: 0x0006321C
	private void OnMsgStageReplace(MsgStageReplace msg)
	{
		for (int i = this.m_nowDrawingModel.Count - 1; i >= 0; i--)
		{
			UnityEngine.Object.Destroy(this.m_nowDrawingModel[i]);
		}
		this.m_nowDrawingModel.Clear();
		this.SetupModel(msg.m_stageName);
	}

	// Token: 0x04001029 RID: 4137
	private const float nextOffset = 1000f;

	// Token: 0x0400102A RID: 4138
	private const float drawOffset = 1500f;

	// Token: 0x0400102B RID: 4139
	private const float destroyOffset = 1700f;

	// Token: 0x0400102C RID: 4140
	private const string defaultModelName = "_far";

	// Token: 0x0400102D RID: 4141
	public GameObject m_originalFarModel;

	// Token: 0x0400102E RID: 4142
	private List<GameObject> m_nowDrawingModel;

	// Token: 0x0400102F RID: 4143
	private PlayerInformation m_playerInfo;

	// Token: 0x04001030 RID: 4144
	private int m_nowSpawnedNumModels;

	// Token: 0x04001031 RID: 4145
	private float m_nextSpawnOffset;

	// Token: 0x04001032 RID: 4146
	private static readonly Vector3 originalPosition = new Vector3(-1500f, 0f, 7920f) * 0.1f;

	// Token: 0x04001033 RID: 4147
	private static readonly Vector3 originalRotation = new Vector3(0f, 0f, 0f);
}
