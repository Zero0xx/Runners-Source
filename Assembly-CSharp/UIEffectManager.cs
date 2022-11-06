using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200056B RID: 1387
public class UIEffectManager : MonoBehaviour
{
	// Token: 0x17000593 RID: 1427
	// (get) Token: 0x06002AAC RID: 10924 RVA: 0x0010841C File Offset: 0x0010661C
	// (set) Token: 0x06002AAD RID: 10925 RVA: 0x00108424 File Offset: 0x00106624
	public static UIEffectManager Instance
	{
		get
		{
			return UIEffectManager.m_instance;
		}
		private set
		{
		}
	}

	// Token: 0x06002AAE RID: 10926 RVA: 0x00108428 File Offset: 0x00106628
	public void AddEffect(UIObjectContainer container)
	{
		if (container == null)
		{
			return;
		}
		HudMenuUtility.EffectPriority priority = container.Priority;
		if (priority >= HudMenuUtility.EffectPriority.Num)
		{
			return;
		}
		this.m_effectList[(int)priority].Add(container);
	}

	// Token: 0x06002AAF RID: 10927 RVA: 0x00108460 File Offset: 0x00106660
	public void RemoveEffect(UIObjectContainer container)
	{
		if (container == null)
		{
			return;
		}
		HudMenuUtility.EffectPriority priority = container.Priority;
		if (priority >= HudMenuUtility.EffectPriority.Num)
		{
			return;
		}
		this.m_effectList[(int)priority].Remove(container);
	}

	// Token: 0x06002AB0 RID: 10928 RVA: 0x00108498 File Offset: 0x00106698
	public void SetActiveEffect(HudMenuUtility.EffectPriority priority, bool isActive)
	{
		for (int i = 0; i <= (int)priority; i++)
		{
			foreach (UIObjectContainer uiobjectContainer in this.m_effectList[i])
			{
				if (!(uiobjectContainer == null))
				{
					uiobjectContainer.SetActive(isActive);
				}
			}
		}
	}

	// Token: 0x06002AB1 RID: 10929 RVA: 0x00108524 File Offset: 0x00106724
	private void Awake()
	{
		if (UIEffectManager.m_instance == null)
		{
			UIEffectManager.m_instance = this;
			this.Init();
			UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
		}
		else
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x06002AB2 RID: 10930 RVA: 0x00108568 File Offset: 0x00106768
	private void Start()
	{
	}

	// Token: 0x06002AB3 RID: 10931 RVA: 0x0010856C File Offset: 0x0010676C
	private void Update()
	{
	}

	// Token: 0x06002AB4 RID: 10932 RVA: 0x00108570 File Offset: 0x00106770
	private void Init()
	{
		this.m_effectList = new List<UIObjectContainer>[4];
		for (int i = 0; i < 4; i++)
		{
			this.m_effectList[i] = new List<UIObjectContainer>();
		}
	}

	// Token: 0x04002626 RID: 9766
	private static UIEffectManager m_instance;

	// Token: 0x04002627 RID: 9767
	private List<UIObjectContainer>[] m_effectList;
}
