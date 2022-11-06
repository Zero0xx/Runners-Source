using System;
using AnimationOrTween;
using UnityEngine;

// Token: 0x020003BE RID: 958
public class HudLoadingWindow : MonoBehaviour
{
	// Token: 0x06001BE7 RID: 7143 RVA: 0x000A5DAC File Offset: 0x000A3FAC
	public void PlayStart()
	{
		TinyFsmEvent signal = TinyFsmEvent.CreateUserEvent(100);
		if (this.m_fsm != null)
		{
			UIPanel component = base.gameObject.GetComponent<UIPanel>();
			if (component != null)
			{
				component.alpha = 1f;
			}
			this.m_fsm.Dispatch(signal);
		}
		this.m_charaInfo.enabled = true;
	}

	// Token: 0x06001BE8 RID: 7144 RVA: 0x000A5E10 File Offset: 0x000A4010
	public void SetLoadingPercentage(float parcentage)
	{
		if (parcentage > 100f)
		{
			parcentage = 100f;
		}
		this.m_parcentage = parcentage;
	}

	// Token: 0x06001BE9 RID: 7145 RVA: 0x000A5E2C File Offset: 0x000A402C
	public void PlayEnd()
	{
		TinyFsmEvent signal = TinyFsmEvent.CreateUserEvent(101);
		if (this.m_fsm != null)
		{
			this.m_fsm.Dispatch(signal);
		}
	}

	// Token: 0x06001BEA RID: 7146 RVA: 0x000A5E60 File Offset: 0x000A4060
	private void Start()
	{
		this.m_lastClick = 0f;
		this.m_fsm = base.gameObject.AddComponent<TinyFsmBehavior>();
		TinyFsmBehavior.Description description = new TinyFsmBehavior.Description(this);
		description.initState = new TinyFsmState(new EventFunction(this.StateSetup));
		description.onFixedUpdate = true;
		this.m_fsm.SetUp(description);
		this.m_charaInfo = base.gameObject.AddComponent<HudLoadingCharaInfo>();
		this.m_charaInfo.enabled = false;
	}

	// Token: 0x06001BEB RID: 7147 RVA: 0x000A5ED8 File Offset: 0x000A40D8
	private void OnDestroy()
	{
		if (this.m_charaInfo != null)
		{
			UnityEngine.Object.Destroy(this.m_charaInfo.gameObject);
			this.m_charaInfo = null;
		}
	}

	// Token: 0x06001BEC RID: 7148 RVA: 0x000A5F10 File Offset: 0x000A4110
	private void Update()
	{
	}

	// Token: 0x06001BED RID: 7149 RVA: 0x000A5F14 File Offset: 0x000A4114
	private TinyFsmState StateSetup(TinyFsmEvent e)
	{
		int signal = e.Signal;
		switch (signal + 4)
		{
		case 0:
			return TinyFsmState.End();
		case 1:
			this.m_lastClick = 0f;
			return TinyFsmState.End();
		case 4:
			base.gameObject.SetActive(false);
			this.SetCharaExplainActive(false);
			this.SetLoadingBarParcentage(this.m_parcentage);
			this.m_fsm.ChangeState(new TinyFsmState(new EventFunction(this.StateIdle)));
			return TinyFsmState.End();
		}
		return TinyFsmState.End();
	}

	// Token: 0x06001BEE RID: 7150 RVA: 0x000A5FA8 File Offset: 0x000A41A8
	private TinyFsmState StateIdle(TinyFsmEvent e)
	{
		int signal = e.Signal;
		switch (signal + 4)
		{
		case 0:
			return TinyFsmState.End();
		case 1:
			return TinyFsmState.End();
		default:
		{
			if (signal != 100)
			{
				return TinyFsmState.End();
			}
			base.gameObject.SetActive(true);
			Animation component = base.gameObject.GetComponent<Animation>();
			if (component != null)
			{
				ActiveAnimation.Play(component, Direction.Forward);
			}
			this.m_currentDisplayTime = this.m_charaDisplayTime;
			this.m_fsm.ChangeState(new TinyFsmState(new EventFunction(this.StateLoading)));
			return TinyFsmState.End();
		}
		case 4:
			return TinyFsmState.End();
		}
	}

	// Token: 0x06001BEF RID: 7151 RVA: 0x000A6058 File Offset: 0x000A4258
	private TinyFsmState StateLoading(TinyFsmEvent e)
	{
		int signal = e.Signal;
		switch (signal + 4)
		{
		case 0:
			return TinyFsmState.End();
		case 1:
			return TinyFsmState.End();
		default:
		{
			if (signal != 101)
			{
				return TinyFsmState.End();
			}
			this.SetLoadingBarParcentage(100f);
			Animation component = base.gameObject.GetComponent<Animation>();
			if (component != null)
			{
				ActiveAnimation.Play(component, Direction.Reverse);
			}
			this.m_fsm.ChangeState(new TinyFsmState(new EventFunction(this.StateIdle)));
			return TinyFsmState.End();
		}
		case 4:
			this.SetLoadingBarParcentage(this.m_parcentage);
			this.m_currentDisplayTime += Time.deltaTime;
			if (this.m_currentDisplayTime >= this.m_charaDisplayTime)
			{
				this.m_currentDisplayTime = this.m_charaDisplayTime;
				bool flag = this.ChangeCharaExplain();
				if (flag)
				{
					this.m_currentDisplayTime = 0f;
				}
			}
			return TinyFsmState.End();
		}
	}

	// Token: 0x06001BF0 RID: 7152 RVA: 0x000A6150 File Offset: 0x000A4350
	private void SetLoadingBarParcentage(float parcentage)
	{
		UISlider uislider = GameObjectUtil.FindChildGameObjectComponent<UISlider>(base.gameObject, "Progress_Bar");
		if (uislider != null)
		{
			uislider.value = parcentage / 100f;
		}
		UILabel uilabel = GameObjectUtil.FindChildGameObjectComponent<UILabel>(base.gameObject, "Lbl_progress");
		if (uilabel != null)
		{
			uilabel.text = ((int)parcentage).ToString() + "%";
		}
	}

	// Token: 0x06001BF1 RID: 7153 RVA: 0x000A61C0 File Offset: 0x000A43C0
	private bool ChangeCharaExplain()
	{
		if (this.m_charaInfo == null)
		{
			return false;
		}
		if (!this.m_charaInfo.IsReady())
		{
			return false;
		}
		this.SetCharaExplainActive(true);
		UITexture uitexture = GameObjectUtil.FindChildGameObjectComponent<UITexture>(base.gameObject, "img_event_tex");
		if (uitexture != null)
		{
			Texture2D charaPicture = this.m_charaInfo.GetCharaPicture();
			if (charaPicture != null)
			{
				bool flag = false;
				if (charaPicture.width < 150 || charaPicture.height < 150)
				{
					flag = true;
				}
				if (!flag)
				{
					uitexture.mainTexture = charaPicture;
					uitexture.width = (int)((float)uitexture.mainTexture.width * 0.859375f);
					uitexture.height = (int)((float)uitexture.mainTexture.height * 0.859375f);
				}
				else
				{
					uitexture.mainTexture = this.defultImage;
					uitexture.width = 220;
					uitexture.height = 220;
				}
				UISprite uisprite = GameObjectUtil.FindChildGameObjectComponent<UISprite>(base.gameObject, "img_tex_flame");
				if (uitexture != null)
				{
					uisprite.width = uitexture.width + 4;
					uisprite.height = uitexture.height + 6;
				}
			}
		}
		UILabel uilabel = GameObjectUtil.FindChildGameObjectComponent<UILabel>(base.gameObject, "Lbl_name");
		if (uilabel != null)
		{
			uilabel.text = this.m_charaInfo.GetCharaName();
		}
		UILabel uilabel2 = GameObjectUtil.FindChildGameObjectComponent<UILabel>(base.gameObject, "Lbl_help");
		if (uilabel2 != null)
		{
			uilabel2.text = this.m_charaInfo.GetCharaExplain();
		}
		UILabel uilabel3 = GameObjectUtil.FindChildGameObjectComponent<UILabel>(base.gameObject, "Lbl_caption_sub");
		if (uilabel3 != null)
		{
			uilabel3.text = this.m_charaInfo.GetCharaExplainCaption();
		}
		this.m_charaInfo.GoNext();
		return true;
	}

	// Token: 0x06001BF2 RID: 7154 RVA: 0x000A6390 File Offset: 0x000A4590
	private void SetCharaExplainActive(bool isActive)
	{
		GameObject gameObject = GameObjectUtil.FindChildGameObject(base.gameObject, "img_event_tex");
		if (gameObject != null)
		{
			gameObject.SetActive(isActive);
		}
		GameObject gameObject2 = GameObjectUtil.FindChildGameObject(base.gameObject, "Lbl_name");
		if (gameObject2 != null)
		{
			gameObject2.SetActive(isActive);
		}
		GameObject gameObject3 = GameObjectUtil.FindChildGameObject(base.gameObject, "Lbl_help");
		if (gameObject3 != null)
		{
			gameObject3.SetActive(isActive);
		}
		GameObject gameObject4 = GameObjectUtil.FindChildGameObject(base.gameObject, "Lbl_caption_sub");
		if (gameObject4 != null)
		{
			gameObject4.SetActive(isActive);
		}
	}

	// Token: 0x06001BF3 RID: 7155 RVA: 0x000A6430 File Offset: 0x000A4630
	public void OnPressBg()
	{
		if (Time.realtimeSinceStartup >= this.m_lastClick + 0.05f && this.m_currentDisplayTime > 0.5f)
		{
			this.m_currentDisplayTime = this.m_charaDisplayTime;
			this.m_lastClick = Time.realtimeSinceStartup;
		}
	}

	// Token: 0x040019A8 RID: 6568
	public const float TEXTURE_IMAGE_SCALE = 0.859375f;

	// Token: 0x040019A9 RID: 6569
	private TinyFsmBehavior m_fsm;

	// Token: 0x040019AA RID: 6570
	private float m_lastClick;

	// Token: 0x040019AB RID: 6571
	private float m_parcentage;

	// Token: 0x040019AC RID: 6572
	[SerializeField]
	private float m_charaDisplayTime = 10f;

	// Token: 0x040019AD RID: 6573
	private float m_currentDisplayTime;

	// Token: 0x040019AE RID: 6574
	private HudLoadingCharaInfo m_charaInfo;

	// Token: 0x040019AF RID: 6575
	[SerializeField]
	private Texture2D defultImage;

	// Token: 0x020003BF RID: 959
	private enum EventSignal
	{
		// Token: 0x040019B1 RID: 6577
		SIG_PLAYSTART = 100,
		// Token: 0x040019B2 RID: 6578
		SIG_PLAYEND
	}
}
