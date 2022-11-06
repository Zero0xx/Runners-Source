using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000511 RID: 1297
public class RoulettePartsBase : MonoBehaviour
{
	// Token: 0x17000528 RID: 1320
	// (get) Token: 0x06002712 RID: 10002 RVA: 0x000F0EC4 File Offset: 0x000EF0C4
	public RouletteTop parent
	{
		get
		{
			return this.m_parent;
		}
	}

	// Token: 0x17000529 RID: 1321
	// (get) Token: 0x06002713 RID: 10003 RVA: 0x000F0ECC File Offset: 0x000EF0CC
	public bool isDelay
	{
		get
		{
			return this.m_delayTime > 0f;
		}
	}

	// Token: 0x06002714 RID: 10004 RVA: 0x000F0EDC File Offset: 0x000EF0DC
	public void SetDelayTime(float delay = 0.2f)
	{
		if (delay >= 0f)
		{
			if (delay > 10f)
			{
				delay = 10f;
			}
			this.m_delayTime = delay;
		}
		else
		{
			this.m_delayTime = 0f;
		}
	}

	// Token: 0x1700052A RID: 1322
	// (get) Token: 0x06002715 RID: 10005 RVA: 0x000F0F20 File Offset: 0x000EF120
	public bool isSpin
	{
		get
		{
			return !(this.m_parent == null) && this.m_parent.isSpin;
		}
	}

	// Token: 0x1700052B RID: 1323
	// (get) Token: 0x06002716 RID: 10006 RVA: 0x000F0F40 File Offset: 0x000EF140
	public int spinDecisionIndex
	{
		get
		{
			if (this.m_parent == null)
			{
				return -1;
			}
			return this.m_parent.spinDecisionIndex;
		}
	}

	// Token: 0x1700052C RID: 1324
	// (get) Token: 0x06002717 RID: 10007 RVA: 0x000F0F60 File Offset: 0x000EF160
	public ServerWheelOptionsData wheel
	{
		get
		{
			if (this.m_parent == null)
			{
				return null;
			}
			return this.m_parent.wheelData;
		}
	}

	// Token: 0x1700052D RID: 1325
	// (get) Token: 0x06002718 RID: 10008 RVA: 0x000F0F80 File Offset: 0x000EF180
	public List<GameObject> effectList
	{
		get
		{
			return this.m_effectList;
		}
	}

	// Token: 0x06002719 RID: 10009 RVA: 0x000F0F88 File Offset: 0x000EF188
	private void Update()
	{
		this.UpdateParts();
		this.m_roulettePartsTime += Time.deltaTime;
		if (this.m_roulettePartsTime >= 3.4028235E+38f)
		{
			this.m_roulettePartsTime = 1000f;
		}
		if (this.m_delayTime > 0f)
		{
			this.m_delayTime -= Time.deltaTime;
			if (this.m_delayTime <= 0f)
			{
				this.m_delayTime = 0f;
			}
		}
		if (!this.m_isEffectLock)
		{
			if (GeneralWindow.Created || EventBestChaoWindow.Created || this.m_isWindow)
			{
				if (this.m_isEffect)
				{
					this.m_isEffect = false;
					if (this.m_effectList != null && this.m_effectList.Count > 0)
					{
						foreach (GameObject gameObject in this.m_effectList)
						{
							gameObject.SetActive(this.m_isEffect);
						}
					}
				}
			}
			else if (!this.m_isEffect)
			{
				this.m_isEffect = true;
				if (this.m_effectList != null && this.m_effectList.Count > 0)
				{
					foreach (GameObject gameObject2 in this.m_effectList)
					{
						gameObject2.SetActive(this.m_isEffect);
					}
				}
			}
		}
		else if (this.m_isEffect)
		{
			this.m_isEffect = false;
			if (this.m_effectList != null && this.m_effectList.Count > 0)
			{
				foreach (GameObject gameObject3 in this.m_effectList)
				{
					gameObject3.SetActive(this.m_isEffect);
				}
			}
		}
		this.m_partsUpdateCount += 1L;
	}

	// Token: 0x0600271A RID: 10010 RVA: 0x000F11E8 File Offset: 0x000EF3E8
	protected virtual void UpdateParts()
	{
	}

	// Token: 0x0600271B RID: 10011 RVA: 0x000F11EC File Offset: 0x000EF3EC
	public virtual void UpdateEffectSetting()
	{
	}

	// Token: 0x0600271C RID: 10012 RVA: 0x000F11F0 File Offset: 0x000EF3F0
	public virtual void Setup(RouletteTop parent)
	{
		this.m_isWindow = false;
		this.m_parent = parent;
		this.m_roulettePartsTime = 0f;
		this.m_delayTime = 0f;
		this.m_isEffect = true;
		if (this.m_isEffectLock)
		{
			this.m_isEffect = false;
		}
		if (this.m_effectList != null && this.m_effectList.Count > 0)
		{
			foreach (GameObject gameObject in this.m_effectList)
			{
				gameObject.SetActive(this.m_isEffect);
			}
		}
		this.m_partsUpdateCount = 0L;
	}

	// Token: 0x0600271D RID: 10013 RVA: 0x000F12BC File Offset: 0x000EF4BC
	public virtual void OnUpdateWheelData(ServerWheelOptionsData data)
	{
		this.m_roulettePartsTime = 0f;
	}

	// Token: 0x0600271E RID: 10014 RVA: 0x000F12CC File Offset: 0x000EF4CC
	public virtual void DestroyParts()
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x0600271F RID: 10015 RVA: 0x000F12DC File Offset: 0x000EF4DC
	public virtual void OnSpinStart()
	{
	}

	// Token: 0x06002720 RID: 10016 RVA: 0x000F12E0 File Offset: 0x000EF4E0
	public virtual void OnSpinSkip()
	{
	}

	// Token: 0x06002721 RID: 10017 RVA: 0x000F12E4 File Offset: 0x000EF4E4
	public virtual void OnSpinDecision()
	{
	}

	// Token: 0x06002722 RID: 10018 RVA: 0x000F12E8 File Offset: 0x000EF4E8
	public virtual void OnSpinDecisionMulti()
	{
	}

	// Token: 0x06002723 RID: 10019 RVA: 0x000F12EC File Offset: 0x000EF4EC
	public virtual void OnSpinEnd()
	{
	}

	// Token: 0x06002724 RID: 10020 RVA: 0x000F12F0 File Offset: 0x000EF4F0
	public virtual void OnSpinError()
	{
	}

	// Token: 0x06002725 RID: 10021 RVA: 0x000F12F4 File Offset: 0x000EF4F4
	public virtual void OnRouletteClose()
	{
	}

	// Token: 0x06002726 RID: 10022 RVA: 0x000F12F8 File Offset: 0x000EF4F8
	public virtual void windowOpen()
	{
		this.m_isWindow = true;
	}

	// Token: 0x06002727 RID: 10023 RVA: 0x000F1304 File Offset: 0x000EF504
	public virtual void windowClose()
	{
		this.m_isWindow = false;
		if (!GeneralWindow.Created && !EventBestChaoWindow.Created && !this.m_isEffectLock)
		{
			this.m_isEffect = true;
			if (this.m_effectList != null && this.m_effectList.Count > 0)
			{
				foreach (GameObject gameObject in this.m_effectList)
				{
					gameObject.SetActive(this.m_isEffect);
				}
			}
		}
	}

	// Token: 0x06002728 RID: 10024 RVA: 0x000F13B8 File Offset: 0x000EF5B8
	public virtual void PartsSendMessage(string mesage)
	{
	}

	// Token: 0x04002378 RID: 9080
	[SerializeField]
	private List<GameObject> m_effectList;

	// Token: 0x04002379 RID: 9081
	protected RouletteTop m_parent;

	// Token: 0x0400237A RID: 9082
	protected float m_roulettePartsTime;

	// Token: 0x0400237B RID: 9083
	protected bool m_isWindow;

	// Token: 0x0400237C RID: 9084
	protected bool m_isEffectLock;

	// Token: 0x0400237D RID: 9085
	protected long m_partsUpdateCount;

	// Token: 0x0400237E RID: 9086
	private float m_delayTime;

	// Token: 0x0400237F RID: 9087
	private bool m_isEffect;
}
