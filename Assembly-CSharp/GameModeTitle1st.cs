using System;
using UnityEngine;

// Token: 0x02000339 RID: 825
[AddComponentMenu("Scripts/Runners/GameMode/title1st")]
public class GameModeTitle1st : MonoBehaviour
{
	// Token: 0x06001884 RID: 6276 RVA: 0x0008DF00 File Offset: 0x0008C100
	private void Start()
	{
		this.m_texture = new Texture2D(32, 32, TextureFormat.ARGB32, false);
		this.m_texture.SetPixel(0, 0, Color.white);
		this.m_texture.Apply();
		this.m_fsm = (base.gameObject.AddComponent(typeof(TinyFsmBehavior)) as TinyFsmBehavior);
		if (this.m_fsm != null)
		{
			TinyFsmBehavior.Description description = new TinyFsmBehavior.Description(this);
			description.initState = new TinyFsmState(new EventFunction(this.StateIdle));
			description.onFixedUpdate = true;
			this.m_fsm.SetUp(description);
		}
		SoundManager.AddTitleCueSheet();
		SoundManager.BgmPlay("bgm_sys_title", "BGM", false);
	}

	// Token: 0x06001885 RID: 6277 RVA: 0x0008DFB4 File Offset: 0x0008C1B4
	private void OnDestroy()
	{
		if (this.m_fsm)
		{
			this.m_fsm.ShutDown();
			this.m_fsm = null;
		}
	}

	// Token: 0x06001886 RID: 6278 RVA: 0x0008DFE4 File Offset: 0x0008C1E4
	private void OnGUI()
	{
	}

	// Token: 0x06001887 RID: 6279 RVA: 0x0008DFE8 File Offset: 0x0008C1E8
	private void FixedUpdate()
	{
	}

	// Token: 0x06001888 RID: 6280 RVA: 0x0008DFEC File Offset: 0x0008C1EC
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
			if (signal != 100)
			{
				return TinyFsmState.End();
			}
			this.m_fsm.ChangeState(new TinyFsmState(new EventFunction(this.StateFading)));
			return TinyFsmState.End();
		case 4:
			return TinyFsmState.End();
		}
	}

	// Token: 0x06001889 RID: 6281 RVA: 0x0008E064 File Offset: 0x0008C264
	private TinyFsmState StateFading(TinyFsmEvent e)
	{
		int signal = e.Signal;
		switch (signal + 4)
		{
		case 0:
			return TinyFsmState.End();
		case 1:
			this.m_alpha = 0f;
			return TinyFsmState.End();
		case 4:
			this.m_alpha = Mathf.Clamp(this.m_alpha + Time.deltaTime, 0f, 1f);
			if (this.m_alpha >= 1f)
			{
				Application.LoadLevel(1);
			}
			return TinyFsmState.End();
		}
		return TinyFsmState.End();
	}

	// Token: 0x0600188A RID: 6282 RVA: 0x0008E0F4 File Offset: 0x0008C2F4
	private void OnTouchTitle()
	{
		if (this.m_fsm != null)
		{
			TinyFsmEvent signal = TinyFsmEvent.CreateUserEvent(100);
			this.m_fsm.Dispatch(signal);
		}
	}

	// Token: 0x04001604 RID: 5636
	private const int NextSceneIndex = 1;

	// Token: 0x04001605 RID: 5637
	private float m_alpha = 1f;

	// Token: 0x04001606 RID: 5638
	private Texture2D m_texture;

	// Token: 0x04001607 RID: 5639
	private TinyFsmBehavior m_fsm;

	// Token: 0x0200033A RID: 826
	private enum EventSignal
	{
		// Token: 0x04001609 RID: 5641
		SIG_ONTOUCHED = 100
	}
}
