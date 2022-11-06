using System;
using UnityEngine;

// Token: 0x020002C0 RID: 704
[ExecuteInEditMode]
public class TenseEffectManager : MonoBehaviour
{
	// Token: 0x0600141A RID: 5146 RVA: 0x0006C6D4 File Offset: 0x0006A8D4
	private void Awake()
	{
		this.CheckInstance();
	}

	// Token: 0x0600141B RID: 5147 RVA: 0x0006C6E0 File Offset: 0x0006A8E0
	private void Start()
	{
	}

	// Token: 0x0600141C RID: 5148 RVA: 0x0006C6E4 File Offset: 0x0006A8E4
	private void Update()
	{
	}

	// Token: 0x0600141D RID: 5149 RVA: 0x0006C6E8 File Offset: 0x0006A8E8
	public void SetType(TenseEffectManager.Type t)
	{
		this.m_nowType = t;
	}

	// Token: 0x17000353 RID: 851
	// (get) Token: 0x0600141F RID: 5151 RVA: 0x0006C700 File Offset: 0x0006A900
	// (set) Token: 0x0600141E RID: 5150 RVA: 0x0006C6F4 File Offset: 0x0006A8F4
	public bool NotChangeTense
	{
		get
		{
			return this.m_notChangeTense;
		}
		set
		{
			this.m_notChangeTense = value;
		}
	}

	// Token: 0x06001420 RID: 5152 RVA: 0x0006C708 File Offset: 0x0006A908
	public void FlipTenseType()
	{
		if (!this.NotChangeTense)
		{
			this.m_nowType = ((this.m_nowType != TenseEffectManager.Type.TENSE_A) ? TenseEffectManager.Type.TENSE_A : TenseEffectManager.Type.TENSE_B);
		}
	}

	// Token: 0x06001421 RID: 5153 RVA: 0x0006C730 File Offset: 0x0006A930
	public TenseEffectManager.Type GetTenseType()
	{
		return this.m_nowType;
	}

	// Token: 0x17000354 RID: 852
	// (get) Token: 0x06001422 RID: 5154 RVA: 0x0006C738 File Offset: 0x0006A938
	public static TenseEffectManager Instance
	{
		get
		{
			if (TenseEffectManager.instance == null)
			{
				TenseEffectManager.instance = GameObjectUtil.FindGameObjectComponent<TenseEffectManager>("TenseEffectManager");
			}
			return TenseEffectManager.instance;
		}
	}

	// Token: 0x06001423 RID: 5155 RVA: 0x0006C76C File Offset: 0x0006A96C
	protected bool CheckInstance()
	{
		if (TenseEffectManager.instance == null)
		{
			TenseEffectManager.instance = this;
			return true;
		}
		if (this == TenseEffectManager.Instance)
		{
			return true;
		}
		UnityEngine.Object.Destroy(base.gameObject);
		return false;
	}

	// Token: 0x06001424 RID: 5156 RVA: 0x0006C7B0 File Offset: 0x0006A9B0
	private void OnDestroy()
	{
		if (TenseEffectManager.instance == this)
		{
			TenseEffectManager.instance = null;
		}
	}

	// Token: 0x04001189 RID: 4489
	[SerializeField]
	private TenseEffectManager.Type m_nowType;

	// Token: 0x0400118A RID: 4490
	private bool m_notChangeTense;

	// Token: 0x0400118B RID: 4491
	private static TenseEffectManager instance;

	// Token: 0x020002C1 RID: 705
	public enum Type
	{
		// Token: 0x0400118D RID: 4493
		TENSE_A,
		// Token: 0x0400118E RID: 4494
		TENSE_B
	}
}
