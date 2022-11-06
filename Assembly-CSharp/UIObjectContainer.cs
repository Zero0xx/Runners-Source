using System;
using UnityEngine;

// Token: 0x02000572 RID: 1394
public class UIObjectContainer : MonoBehaviour
{
	// Token: 0x17000597 RID: 1431
	// (get) Token: 0x06002AD0 RID: 10960 RVA: 0x00108AA8 File Offset: 0x00106CA8
	// (set) Token: 0x06002AD1 RID: 10961 RVA: 0x00108AB0 File Offset: 0x00106CB0
	public HudMenuUtility.EffectPriority Priority
	{
		get
		{
			return this.m_priority;
		}
		set
		{
			this.m_priority = value;
		}
	}

	// Token: 0x17000598 RID: 1432
	// (get) Token: 0x06002AD2 RID: 10962 RVA: 0x00108ABC File Offset: 0x00106CBC
	// (set) Token: 0x06002AD3 RID: 10963 RVA: 0x00108AC4 File Offset: 0x00106CC4
	public GameObject[] Objects
	{
		get
		{
			return this.m_objects;
		}
		set
		{
			this.m_objects = value;
		}
	}

	// Token: 0x06002AD4 RID: 10964 RVA: 0x00108AD0 File Offset: 0x00106CD0
	public void SetActive(bool isActive)
	{
		if (this.m_activeFlags == null)
		{
			return;
		}
		if (this.m_objects.Length != this.m_activeFlags.Length)
		{
			return;
		}
		for (int i = 0; i < this.m_objects.Length; i++)
		{
			if (!(this.m_objects[i] == null))
			{
				if (isActive)
				{
					if (!this.m_objects[i].activeSelf && this.m_activeFlags[i])
					{
						this.m_objects[i].SetActive(true);
					}
				}
				else if (this.m_objects[i].activeSelf)
				{
					this.m_activeFlags[i] = true;
					this.m_objects[i].SetActive(false);
				}
			}
		}
	}

	// Token: 0x06002AD5 RID: 10965 RVA: 0x00108B94 File Offset: 0x00106D94
	private void Start()
	{
		UIEffectManager instance = UIEffectManager.Instance;
		if (instance != null)
		{
			instance.AddEffect(this);
		}
		if (this.m_objects.Length > 0)
		{
			this.m_activeFlags = new bool[this.m_objects.Length];
			if (this.m_activeFlags != null)
			{
				for (int i = 0; i < this.m_activeFlags.Length; i++)
				{
					this.m_activeFlags[i] = false;
				}
			}
		}
		base.enabled = false;
	}

	// Token: 0x06002AD6 RID: 10966 RVA: 0x00108C10 File Offset: 0x00106E10
	private void OnDestroy()
	{
		this.m_objects = null;
		this.m_activeFlags = null;
		UIEffectManager instance = UIEffectManager.Instance;
		if (instance != null)
		{
			instance.RemoveEffect(this);
		}
	}

	// Token: 0x06002AD7 RID: 10967 RVA: 0x00108C44 File Offset: 0x00106E44
	private void Update()
	{
	}

	// Token: 0x0400263E RID: 9790
	[SerializeField]
	private HudMenuUtility.EffectPriority m_priority;

	// Token: 0x0400263F RID: 9791
	[SerializeField]
	private GameObject[] m_objects = new GameObject[0];

	// Token: 0x04002640 RID: 9792
	private bool[] m_activeFlags;
}
