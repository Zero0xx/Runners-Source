using System;
using AnimationOrTween;
using UnityEngine;

// Token: 0x020003C3 RID: 963
public class pause_window : MonoBehaviour
{
	// Token: 0x06001C04 RID: 7172 RVA: 0x000A6988 File Offset: 0x000A4B88
	private void Start()
	{
		if (ServerInterface.LoggedInServerInterface != null)
		{
			this.m_imageButtonBack = GameObjectUtil.FindChildGameObjectComponent<UIImageButton>(base.gameObject, "Btn_back_mainmenu");
			if (this.m_imageButtonBack != null)
			{
				this.m_imageButtonBack.isEnabled = true;
			}
		}
		Component[] componentsInChildren = base.gameObject.GetComponentsInChildren<Renderer>(true);
		foreach (Renderer renderer in componentsInChildren)
		{
			renderer.enabled = false;
		}
		componentsInChildren = base.gameObject.GetComponentsInChildren<MeshRenderer>(true);
		foreach (MeshRenderer meshRenderer in componentsInChildren)
		{
			meshRenderer.enabled = false;
		}
	}

	// Token: 0x06001C05 RID: 7173 RVA: 0x000A6A48 File Offset: 0x000A4C48
	private void OnMsgNotifyStartPause()
	{
		GameObject gameObject = GameObjectUtil.FindChildGameObject(base.gameObject, "Btn_continue");
		if (gameObject != null)
		{
			this.m_continueAnim = gameObject.GetComponent<UIPlayAnimation>();
			if (this.m_continueAnim != null && this.m_continueAnim.onFinished.Count == 0)
			{
				EventDelegate.Add(this.m_continueAnim.onFinished, new EventDelegate.Callback(this.OnFinishedContinueAnimationCallback), true);
			}
		}
		GameObject gameObject2 = GameObjectUtil.FindChildGameObject(base.gameObject, "Btn_back_mainmenu");
		if (gameObject2 != null)
		{
			this.m_backAnim = gameObject2.GetComponent<UIPlayAnimation>();
			if (this.m_backAnim != null)
			{
				this.m_backAnim.enabled = false;
			}
		}
	}

	// Token: 0x06001C06 RID: 7174 RVA: 0x000A6B08 File Offset: 0x000A4D08
	private void OnFinishedContinueAnimationCallback()
	{
		GameObjectUtil.SendMessageFindGameObject("HudCockpit", "OnFinishedContinueAnimation", null, SendMessageOptions.DontRequireReceiver);
		GameObject gameObject = base.transform.FindChild("pause_Anim").gameObject;
		if (gameObject != null)
		{
			gameObject.SetActive(false);
		}
	}

	// Token: 0x06001C07 RID: 7175 RVA: 0x000A6B50 File Offset: 0x000A4D50
	private void OnContinueAnimation()
	{
		if (this.m_continueAnim != null)
		{
			this.m_continueAnim.enabled = true;
			Animation target = this.m_continueAnim.target;
			if (target != null)
			{
				target.Rewind(pause_window.OUTANIM1_NAME);
				ActiveAnimation activeAnimation = ActiveAnimation.Play(target, pause_window.OUTANIM1_NAME, Direction.Forward, true);
				if (activeAnimation != null && activeAnimation.onFinished.Count == 0)
				{
					EventDelegate.Add(activeAnimation.onFinished, new EventDelegate.Callback(this.OnFinishedContinueAnimationCallback), true);
				}
			}
		}
	}

	// Token: 0x06001C08 RID: 7176 RVA: 0x000A6BE0 File Offset: 0x000A4DE0
	private void OnBackMainMenuAnimation()
	{
		if (this.m_backAnim != null)
		{
			this.m_backAnim.enabled = true;
			Animation target = this.m_backAnim.target;
			if (target != null)
			{
				target.Rewind(pause_window.OUTANIM2_NAME);
				ActiveAnimation.Play(target, pause_window.OUTANIM2_NAME, Direction.Forward, true);
			}
		}
	}

	// Token: 0x06001C09 RID: 7177 RVA: 0x000A6C3C File Offset: 0x000A4E3C
	private void OnSetFirstTutorial()
	{
		if (this.m_imageButtonBack != null)
		{
			this.m_imageButtonBack.isEnabled = false;
		}
	}

	// Token: 0x040019C3 RID: 6595
	public static string INANIM_NAME = "ui_pause_intro_Anim";

	// Token: 0x040019C4 RID: 6596
	public static string OUTANIM1_NAME = "ui_pause_outro_Anim";

	// Token: 0x040019C5 RID: 6597
	public static string OUTANIM2_NAME = "ui_pause_outro_title_Anim";

	// Token: 0x040019C6 RID: 6598
	private UIPlayAnimation m_backAnim;

	// Token: 0x040019C7 RID: 6599
	private UIPlayAnimation m_continueAnim;

	// Token: 0x040019C8 RID: 6600
	private UIImageButton m_imageButtonBack;
}
