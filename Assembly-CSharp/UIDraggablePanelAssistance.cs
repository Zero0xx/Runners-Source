using System;
using UnityEngine;

// Token: 0x02000A5A RID: 2650
public class UIDraggablePanelAssistance : MonoBehaviour
{
	// Token: 0x06004760 RID: 18272 RVA: 0x00177A30 File Offset: 0x00175C30
	private void Start()
	{
		this.Init();
	}

	// Token: 0x06004761 RID: 18273 RVA: 0x00177A38 File Offset: 0x00175C38
	private void Update()
	{
		if (!this.m_init)
		{
			this.Init();
		}
		if (this.isAssistanceEnable)
		{
			this.Check();
		}
	}

	// Token: 0x06004762 RID: 18274 RVA: 0x00177A68 File Offset: 0x00175C68
	private void Check()
	{
		if (this.m_currentCheckTime <= 0f && !this.autoCheck)
		{
			return;
		}
		bool flag = false;
		if (this.autoCheck && this.m_count % 3L == 0L)
		{
			if (this.m_count % 3L == 0L)
			{
				flag = true;
			}
		}
		else if (this.m_currentCheckTime > 0f)
		{
			flag = true;
		}
		if (flag)
		{
			if (this.m_spring == null)
			{
				this.m_spring = base.gameObject.GetComponent<SpringPanel>();
			}
			if (this.m_spring != null && (this.X_Axis || this.Y_Axis))
			{
				float x = this.m_spring.target.x;
				float y = this.m_spring.target.y;
				float z = this.m_spring.target.z;
				bool flag2 = false;
				if (this.X_Axis && x != this.X)
				{
					this.m_spring.target = new Vector3(this.X, y, z);
					flag2 = true;
				}
				if (this.Y_Axis && y != this.Y)
				{
					this.m_spring.target = new Vector3(x, this.Y, z);
					flag2 = true;
				}
				if (flag2 && !this.m_spring.enabled)
				{
					this.m_spring.enabled = true;
				}
			}
			if (!this.autoCheck)
			{
				this.m_currentCheckTime -= Time.deltaTime;
			}
		}
		this.m_count += 1L;
	}

	// Token: 0x06004763 RID: 18275 RVA: 0x00177C0C File Offset: 0x00175E0C
	private void Init()
	{
		this.m_draggablePanel = base.gameObject.GetComponent<UIDraggablePanel>();
		this.m_init = true;
	}

	// Token: 0x06004764 RID: 18276 RVA: 0x00177C28 File Offset: 0x00175E28
	public void CheckDraggable()
	{
		this.m_currentCheckTime = this.checkTime;
	}

	// Token: 0x1700099C RID: 2460
	// (get) Token: 0x06004765 RID: 18277 RVA: 0x00177C38 File Offset: 0x00175E38
	// (set) Token: 0x06004766 RID: 18278 RVA: 0x00177C68 File Offset: 0x00175E68
	public bool isAssistanceEnable
	{
		get
		{
			bool result = false;
			if (this.m_draggablePanel != null && this.m_isAssistanceEnable)
			{
				result = true;
			}
			return result;
		}
		set
		{
			this.m_isAssistanceEnable = value;
		}
	}

	// Token: 0x04003B71 RID: 15217
	private UIDraggablePanel m_draggablePanel;

	// Token: 0x04003B72 RID: 15218
	private SpringPanel m_spring;

	// Token: 0x04003B73 RID: 15219
	[SerializeField]
	private bool X_Axis = true;

	// Token: 0x04003B74 RID: 15220
	[SerializeField]
	private float X;

	// Token: 0x04003B75 RID: 15221
	[SerializeField]
	private bool Y_Axis = true;

	// Token: 0x04003B76 RID: 15222
	[SerializeField]
	private float Y;

	// Token: 0x04003B77 RID: 15223
	private bool autoCheck = true;

	// Token: 0x04003B78 RID: 15224
	private float checkTime = 0.5f;

	// Token: 0x04003B79 RID: 15225
	private long m_count;

	// Token: 0x04003B7A RID: 15226
	private bool m_isAssistanceEnable = true;

	// Token: 0x04003B7B RID: 15227
	private bool m_init;

	// Token: 0x04003B7C RID: 15228
	private float m_currentCheckTime;
}
