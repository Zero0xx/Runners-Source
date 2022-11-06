using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200038C RID: 908
public class HudEventQuota : MonoBehaviour
{
	// Token: 0x06001AE1 RID: 6881 RVA: 0x0009EA30 File Offset: 0x0009CC30
	public void AddQuota(QuotaInfo info)
	{
		if (this.m_quotaList == null)
		{
			return;
		}
		this.m_quotaList.Add(info);
	}

	// Token: 0x06001AE2 RID: 6882 RVA: 0x0009EA4C File Offset: 0x0009CC4C
	public void ClearQuota()
	{
		if (this.m_quotaList == null)
		{
			return;
		}
		this.m_quotaList.Clear();
	}

	// Token: 0x06001AE3 RID: 6883 RVA: 0x0009EA68 File Offset: 0x0009CC68
	public void Setup(GameObject rootObject, Animation animation, string swapAnimName1, string swapAnimName2)
	{
		this.m_rootObject = rootObject;
		this.m_animation = animation;
		if (this.m_prefabObject == null)
		{
			this.m_prefabObject = HudEventQuota.FindPrefabObject();
		}
		int count = this.m_quotaList.Count;
		if (count <= 0)
		{
			GameObject gameObject = GameObjectUtil.FindChildGameObject(this.m_rootObject, "next_arrow1");
			if (gameObject != null)
			{
				gameObject.SetActive(false);
			}
			return;
		}
		GameObject gameObject2 = GameObjectUtil.FindChildGameObject(this.m_rootObject, "slot1");
		if (gameObject2 != null)
		{
			GameObject quotaPlate = HudEventQuota.CopyAttachPrefab(gameObject2, this.m_prefabObject);
			this.m_quotaList[0].Setup(quotaPlate, this.m_animation, string.Empty);
			this.m_quotaList[0].SetupDisplay();
		}
		if (count >= 2)
		{
			GameObject gameObject3 = GameObjectUtil.FindChildGameObject(this.m_rootObject, "slot2");
			if (gameObject3 != null)
			{
				GameObject quotaPlate2 = HudEventQuota.CopyAttachPrefab(gameObject3, this.m_prefabObject);
				this.m_quotaList[1].Setup(quotaPlate2, this.m_animation, swapAnimName1);
			}
			for (int i = 2; i < count; i++)
			{
				GameObject quotaPlate3 = this.m_quotaList[i - 2].QuotaPlate;
				if (!(quotaPlate3 == null))
				{
					string animClipName = string.Empty;
					if (i % 2 == 0)
					{
						animClipName = swapAnimName2;
					}
					else
					{
						animClipName = swapAnimName1;
					}
					this.m_quotaList[i].Setup(quotaPlate3, this.m_animation, animClipName);
				}
			}
		}
	}

	// Token: 0x06001AE4 RID: 6884 RVA: 0x0009EBF4 File Offset: 0x0009CDF4
	public void PlayStart(HudEventQuota.PlayEndCallback callback)
	{
		if (this.m_quotaList.Count <= 0)
		{
			this.m_isPlayEnd = true;
			callback();
			return;
		}
		this.m_callback = callback;
		this.m_isPlayEnd = false;
		this.m_currentAnimIndex = 0;
		this.m_quotaList[this.m_currentAnimIndex].PlayStart();
	}

	// Token: 0x06001AE5 RID: 6885 RVA: 0x0009EC4C File Offset: 0x0009CE4C
	public void PlayStop()
	{
		this.m_isPlayEnd = true;
	}

	// Token: 0x170003E2 RID: 994
	// (get) Token: 0x06001AE6 RID: 6886 RVA: 0x0009EC58 File Offset: 0x0009CE58
	// (set) Token: 0x06001AE7 RID: 6887 RVA: 0x0009EC60 File Offset: 0x0009CE60
	public bool IsPlayEnd
	{
		get
		{
			return this.m_isPlayEnd;
		}
		private set
		{
		}
	}

	// Token: 0x06001AE8 RID: 6888 RVA: 0x0009EC64 File Offset: 0x0009CE64
	private void Start()
	{
	}

	// Token: 0x06001AE9 RID: 6889 RVA: 0x0009EC68 File Offset: 0x0009CE68
	private void Update()
	{
		if (this.m_isPlayEnd)
		{
			return;
		}
		if (this.m_quotaList.Count <= this.m_currentAnimIndex)
		{
			return;
		}
		QuotaInfo quotaInfo = this.m_quotaList[this.m_currentAnimIndex];
		if (quotaInfo == null)
		{
			return;
		}
		quotaInfo.Update();
		if (quotaInfo.IsPlayEnd())
		{
			this.m_currentAnimIndex++;
			if (this.m_quotaList.Count <= this.m_currentAnimIndex)
			{
				this.m_isPlayEnd = true;
				if (this.m_callback != null)
				{
					this.m_callback();
				}
			}
			else
			{
				this.m_quotaList[this.m_currentAnimIndex].PlayStart();
			}
		}
	}

	// Token: 0x06001AEA RID: 6890 RVA: 0x0009ED20 File Offset: 0x0009CF20
	private static GameObject FindPrefabObject()
	{
		GameObject result = null;
		GameObject gameObject = GameObject.Find("ResourceManager");
		if (gameObject == null)
		{
			return result;
		}
		GameObject gameObject2 = GameObjectUtil.FindChildGameObject(gameObject, "EventResourceStage");
		if (gameObject2 == null)
		{
			return result;
		}
		return GameObjectUtil.FindChildGameObject(gameObject2, "ui_event_mission_scroll");
	}

	// Token: 0x06001AEB RID: 6891 RVA: 0x0009ED70 File Offset: 0x0009CF70
	private static GameObject CopyAttachPrefab(GameObject parentObject, GameObject prefabObject)
	{
		GameObject gameObject = null;
		if (parentObject == null)
		{
			return gameObject;
		}
		if (prefabObject == null)
		{
			return gameObject;
		}
		gameObject = (GameObject)UnityEngine.Object.Instantiate(prefabObject);
		Vector3 localPosition = gameObject.transform.localPosition;
		Vector3 localScale = gameObject.transform.localScale;
		gameObject.transform.parent = parentObject.transform;
		gameObject.transform.localPosition = localPosition;
		gameObject.transform.localScale = localScale;
		gameObject.SetActive(true);
		return gameObject;
	}

	// Token: 0x04001842 RID: 6210
	private List<QuotaInfo> m_quotaList = new List<QuotaInfo>();

	// Token: 0x04001843 RID: 6211
	private GameObject m_rootObject;

	// Token: 0x04001844 RID: 6212
	[SerializeField]
	private GameObject m_prefabObject;

	// Token: 0x04001845 RID: 6213
	private Animation m_animation;

	// Token: 0x04001846 RID: 6214
	private int m_currentAnimIndex;

	// Token: 0x04001847 RID: 6215
	private bool m_isPlayEnd = true;

	// Token: 0x04001848 RID: 6216
	private HudEventQuota.PlayEndCallback m_callback;

	// Token: 0x02000A81 RID: 2689
	// (Invoke) Token: 0x0600483E RID: 18494
	public delegate void PlayEndCallback();
}
