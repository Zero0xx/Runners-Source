using System;
using System.Collections;
using System.Collections.Generic;
using AnimationOrTween;
using UnityEngine;

// Token: 0x0200022B RID: 555
public class RaidBosshelpRequestWindow : WindowBase
{
	// Token: 0x06000EE5 RID: 3813 RVA: 0x00056FCC File Offset: 0x000551CC
	public bool isFinished()
	{
		return this.m_btnAct == RaidBosshelpRequestWindow.BUTTON_ACT.END;
	}

	// Token: 0x06000EE6 RID: 3814 RVA: 0x00056FD8 File Offset: 0x000551D8
	private void Start()
	{
	}

	// Token: 0x06000EE7 RID: 3815 RVA: 0x00056FDC File Offset: 0x000551DC
	private void Update()
	{
	}

	// Token: 0x06000EE8 RID: 3816 RVA: 0x00056FE0 File Offset: 0x000551E0
	public void Setup(List<ServerEventRaidBossDesiredState> data)
	{
		base.StartCoroutine(this.OnSetup(data));
	}

	// Token: 0x06000EE9 RID: 3817 RVA: 0x00056FF0 File Offset: 0x000551F0
	public IEnumerator OnSetup(List<ServerEventRaidBossDesiredState> data)
	{
		BackKeyManager.AddWindowCallBack(base.gameObject);
		this.mainPanel.alpha = 1f;
		this.m_close = false;
		this.m_btnAct = RaidBosshelpRequestWindow.BUTTON_ACT.NONE;
		this.m_desiredList = data;
		GameObject ui_button = GameObjectUtil.FindChildGameObject(this.mainPanel.gameObject, "Btn_ok");
		if (ui_button != null)
		{
			UIButtonMessage button_msg = ui_button.GetComponent<UIButtonMessage>();
			if (button_msg != null)
			{
				button_msg.enabled = true;
				button_msg.trigger = UIButtonMessage.Trigger.OnClick;
				button_msg.target = base.gameObject;
				button_msg.functionName = "OnClickOkButton";
			}
		}
		if (this.m_desiredList != null)
		{
			this.m_listPanel.enabled = true;
			this.m_storage = this.m_listPanel.GetComponentInChildren<UIRectItemStorage>();
			if (this.m_storage != null && this.m_desiredList.Count > 0)
			{
				this.m_storage.maxItemCount = (this.m_storage.maxRows = this.m_desiredList.Count);
				this.m_storage.Restart();
				ui_ranking_scroll[] list = this.m_storage.GetComponentsInChildren<ui_ranking_scroll>();
				if (list != null && list.Length > 0)
				{
					for (int i = 0; i < this.m_desiredList.Count; i++)
					{
						if (list.Length <= i)
						{
							break;
						}
						list[i].UpdateViewForRaidbossDesired(this.m_desiredList[i]);
					}
				}
			}
		}
		yield return null;
		if (this.m_animation != null)
		{
			ActiveAnimation activeAnim = ActiveAnimation.Play(this.m_animation, "ui_cmn_window_Anim", Direction.Forward);
			EventDelegate.Add(activeAnim.onFinished, new EventDelegate.Callback(this.WindowAnimationFinishCallback), true);
			SoundManager.SePlay("sys_window_open", "SE");
		}
		yield break;
	}

	// Token: 0x06000EEA RID: 3818 RVA: 0x0005701C File Offset: 0x0005521C
	public void OnClickOkButton()
	{
		this.m_btnAct = RaidBosshelpRequestWindow.BUTTON_ACT.CLOSE;
		this.m_close = true;
		SoundManager.SePlay("sys_window_close", "SE");
		GameObject gameObject = GameObjectUtil.FindChildGameObject(base.gameObject, "Btn_ok");
		UIPlayAnimation component = gameObject.GetComponent<UIPlayAnimation>();
		if (component != null)
		{
			component.Play(true);
			EventDelegate.Add(component.onFinished, new EventDelegate.Callback(this.WindowAnimationFinishCallback), true);
		}
	}

	// Token: 0x06000EEB RID: 3819 RVA: 0x0005708C File Offset: 0x0005528C
	private void WindowAnimationFinishCallback()
	{
		if (this.m_close)
		{
			RaidBosshelpRequestWindow.BUTTON_ACT btnAct = this.m_btnAct;
			if (btnAct != RaidBosshelpRequestWindow.BUTTON_ACT.CLOSE)
			{
				if (btnAct != RaidBosshelpRequestWindow.BUTTON_ACT.INFO)
				{
					base.gameObject.SetActive(false);
				}
				else
				{
					base.gameObject.SetActive(false);
				}
			}
			else
			{
				this.m_btnAct = RaidBosshelpRequestWindow.BUTTON_ACT.END;
				base.gameObject.SetActive(false);
			}
		}
	}

	// Token: 0x06000EEC RID: 3820 RVA: 0x000570F8 File Offset: 0x000552F8
	public override void OnClickPlatformBackButton(WindowBase.BackButtonMessage msg)
	{
		if (msg != null)
		{
			msg.StaySequence();
		}
		this.OnClickOkButton();
	}

	// Token: 0x06000EED RID: 3821 RVA: 0x0005710C File Offset: 0x0005530C
	public static RaidBosshelpRequestWindow Create(List<ServerEventRaidBossDesiredState> data)
	{
		if (RaidBosshelpRequestWindow.s_instance != null)
		{
			RaidBosshelpRequestWindow.s_instance.gameObject.SetActive(true);
			RaidBosshelpRequestWindow.s_instance.Setup(data);
			return RaidBosshelpRequestWindow.s_instance;
		}
		return null;
	}

	// Token: 0x17000235 RID: 565
	// (get) Token: 0x06000EEE RID: 3822 RVA: 0x0005714C File Offset: 0x0005534C
	private static RaidBosshelpRequestWindow Instance
	{
		get
		{
			return RaidBosshelpRequestWindow.s_instance;
		}
	}

	// Token: 0x06000EEF RID: 3823 RVA: 0x00057154 File Offset: 0x00055354
	private void Awake()
	{
		this.SetInstance();
		base.gameObject.SetActive(false);
	}

	// Token: 0x06000EF0 RID: 3824 RVA: 0x00057168 File Offset: 0x00055368
	private void OnDestroy()
	{
		if (RaidBosshelpRequestWindow.s_instance == this)
		{
			RaidBosshelpRequestWindow.s_instance = null;
		}
	}

	// Token: 0x06000EF1 RID: 3825 RVA: 0x00057180 File Offset: 0x00055380
	private void SetInstance()
	{
		if (RaidBosshelpRequestWindow.s_instance == null)
		{
			RaidBosshelpRequestWindow.s_instance = this;
		}
		else
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x04000CC9 RID: 3273
	private const float UPDATE_TIME = 0.25f;

	// Token: 0x04000CCA RID: 3274
	[SerializeField]
	private UIPanel mainPanel;

	// Token: 0x04000CCB RID: 3275
	[SerializeField]
	private Animation m_animation;

	// Token: 0x04000CCC RID: 3276
	[SerializeField]
	private UIDraggablePanel m_listPanel;

	// Token: 0x04000CCD RID: 3277
	private bool m_close;

	// Token: 0x04000CCE RID: 3278
	private RaidBosshelpRequestWindow.BUTTON_ACT m_btnAct = RaidBosshelpRequestWindow.BUTTON_ACT.NONE;

	// Token: 0x04000CCF RID: 3279
	private UIRectItemStorage m_storage;

	// Token: 0x04000CD0 RID: 3280
	private List<ServerEventRaidBossDesiredState> m_desiredList;

	// Token: 0x04000CD1 RID: 3281
	private static RaidBosshelpRequestWindow s_instance;

	// Token: 0x0200022C RID: 556
	private enum BUTTON_ACT
	{
		// Token: 0x04000CD3 RID: 3283
		CLOSE,
		// Token: 0x04000CD4 RID: 3284
		INFO,
		// Token: 0x04000CD5 RID: 3285
		END,
		// Token: 0x04000CD6 RID: 3286
		NONE
	}
}
