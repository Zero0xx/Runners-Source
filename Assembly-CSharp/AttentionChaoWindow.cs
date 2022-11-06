using System;
using System.Collections.Generic;
using AnimationOrTween;
using DataTable;
using UnityEngine;

// Token: 0x02000209 RID: 521
public class AttentionChaoWindow : MonoBehaviour
{
	// Token: 0x06000DBD RID: 3517 RVA: 0x000503F8 File Offset: 0x0004E5F8
	public void Setup(List<int> chaoIds)
	{
		this.m_animation = base.GetComponentInChildren<Animation>();
		if (this.m_animation != null)
		{
			ActiveAnimation activeAnimation = ActiveAnimation.Play(this.m_animation, "ui_cmn_window_Anim", Direction.Forward);
			EventDelegate.Add(activeAnimation.onFinished, new EventDelegate.Callback(this.WindowAnimationFinishCallback), true);
			SoundManager.SePlay("sys_window_open", "SE");
		}
		UILabel uilabel = GameObjectUtil.FindChildGameObjectComponent<UILabel>(base.gameObject, "Lbl_caption");
		UIPlayAnimation uiplayAnimation = GameObjectUtil.FindChildGameObjectComponent<UIPlayAnimation>(base.gameObject, "blinder");
		UIButtonMessage uibuttonMessage = GameObjectUtil.FindChildGameObjectComponent<UIButtonMessage>(base.gameObject, "blinder");
		if (uiplayAnimation != null && uibuttonMessage != null)
		{
			uiplayAnimation.enabled = false;
			uibuttonMessage.enabled = false;
		}
		this.m_chaoWindow = new GameObject[3];
		for (int i = 0; i < 3; i++)
		{
			this.m_chaoWindow[i] = GameObjectUtil.FindChildGameObject(base.gameObject, "chao_window_" + i);
			this.m_chaoWindow[i].SetActive(false);
		}
		if (uilabel != null)
		{
			uilabel.text = "今週の目玉";
			uilabel.text = ObjectUtility.SetColorString(uilabel.text, 0, 0, 0);
		}
		if (chaoIds != null)
		{
			this.m_attentionChaoIds = chaoIds;
			int count = this.m_attentionChaoIds.Count;
			if (count > 0)
			{
				this.m_chaoData = ChaoTable.GetChaoData(this.m_attentionChaoIds);
				this.SetChao(0);
			}
		}
		this.m_mode = AttentionChaoWindow.Mode.Wait;
	}

	// Token: 0x06000DBE RID: 3518 RVA: 0x00050578 File Offset: 0x0004E778
	private bool SetChao(int offset)
	{
		bool result = false;
		if (this.m_chaoData != null && this.m_chaoData.Count > 0 && offset >= 0 && this.m_chaoWindow != null)
		{
			int chaoLevel = ChaoTable.ChaoMaxLevel();
			for (int i = 0; i < 3; i++)
			{
				int num = i + 3 * offset;
				if (num < this.m_chaoData.Count)
				{
					this.m_chaoWindow[i].SetActive(true);
					ChaoData chaoData = this.m_chaoData[num];
					if (chaoData != null)
					{
						UILabel uilabel = GameObjectUtil.FindChildGameObjectComponent<UILabel>(this.m_chaoWindow[i].gameObject, "Lbl_name");
						UILabel uilabel2 = GameObjectUtil.FindChildGameObjectComponent<UILabel>(this.m_chaoWindow[i].gameObject, "Lbl_text");
						UISprite uisprite = GameObjectUtil.FindChildGameObjectComponent<UISprite>(this.m_chaoWindow[i].gameObject, "Img_bg");
						UITexture uitexture = GameObjectUtil.FindChildGameObjectComponent<UITexture>(this.m_chaoWindow[i].gameObject, "Tex_chao");
						if (uilabel != null)
						{
							uilabel.text = chaoData.nameTwolines;
							uilabel.text = ObjectUtility.SetColorString(uilabel.text, 0, 0, 0);
						}
						if (uilabel2 != null)
						{
							uilabel2.text = chaoData.GetDetailsLevel(chaoLevel);
							uilabel2.text = ObjectUtility.SetColorString(uilabel2.text, 0, 0, 0);
						}
						if (uisprite != null)
						{
							switch (chaoData.rarity)
							{
							case ChaoData.Rarity.NORMAL:
								uisprite.spriteName = "ui_tex_chao_bg_0";
								break;
							case ChaoData.Rarity.RARE:
								uisprite.spriteName = "ui_tex_chao_bg_1";
								break;
							case ChaoData.Rarity.SRARE:
								uisprite.spriteName = "ui_tex_chao_bg_2";
								break;
							}
						}
						if (uitexture != null)
						{
							ChaoTextureManager.CallbackInfo info = new ChaoTextureManager.CallbackInfo(uitexture, null, true);
							ChaoTextureManager.Instance.GetTexture(chaoData.id, info);
						}
					}
				}
				else
				{
					this.m_chaoWindow[i].SetActive(false);
				}
			}
		}
		return result;
	}

	// Token: 0x06000DBF RID: 3519 RVA: 0x00050770 File Offset: 0x0004E970
	public bool IsEnd()
	{
		return this.m_mode != AttentionChaoWindow.Mode.Wait;
	}

	// Token: 0x06000DC0 RID: 3520 RVA: 0x00050784 File Offset: 0x0004E984
	public void OnClickNoButton()
	{
		this.m_close = true;
		SoundManager.SePlay("sys_window_close", "SE");
		GameObject gameObject = GameObjectUtil.FindChildGameObject(base.gameObject, "Btn_close");
		UIPlayAnimation component = gameObject.GetComponent<UIPlayAnimation>();
		if (component != null)
		{
			EventDelegate.Add(component.onFinished, new EventDelegate.Callback(this.WindowAnimationFinishCallback), true);
			component.Play(true);
		}
	}

	// Token: 0x06000DC1 RID: 3521 RVA: 0x000507EC File Offset: 0x0004E9EC
	public void OnClickNoBgButton()
	{
		this.m_close = true;
		SoundManager.SePlay("sys_window_close", "SE");
		GameObject gameObject = GameObjectUtil.FindChildGameObject(base.gameObject, "Btn_close");
		UIPlayAnimation component = gameObject.GetComponent<UIPlayAnimation>();
		if (component != null)
		{
			EventDelegate.Add(component.onFinished, new EventDelegate.Callback(this.WindowAnimationFinishCallback), true);
			component.Play(true);
		}
	}

	// Token: 0x06000DC2 RID: 3522 RVA: 0x00050854 File Offset: 0x0004EA54
	private void WindowAnimationFinishCallback()
	{
		if (this.m_close)
		{
			this.m_mode = AttentionChaoWindow.Mode.End;
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x06000DC3 RID: 3523 RVA: 0x00050874 File Offset: 0x0004EA74
	public static AttentionChaoWindow Create(List<int> chaoIds)
	{
		GameObject cameraUIObject = HudMenuUtility.GetCameraUIObject();
		if (cameraUIObject != null)
		{
			GameObject gameObject = GameObjectUtil.FindChildGameObject(cameraUIObject, "AttentionChaoWindowUI(Clone)");
			if (gameObject != null)
			{
				UnityEngine.Object.Destroy(gameObject);
			}
			GameObject original = Resources.Load("Prefabs/UI/AttentionChaoWindowUI") as GameObject;
			GameObject gameObject2 = UnityEngine.Object.Instantiate(original, Vector3.zero, Quaternion.identity) as GameObject;
			gameObject2.SetActive(true);
			gameObject2.transform.parent = cameraUIObject.transform;
			gameObject2.transform.localScale = new Vector3(1f, 1f, 1f);
			gameObject2.transform.localPosition = new Vector3(0f, 0f, 0f);
			AttentionChaoWindow attentionChaoWindow = null;
			GameObject gameObject3 = GameObjectUtil.FindChildGameObject(gameObject2, "attention_window");
			if (gameObject3 != null)
			{
				attentionChaoWindow = gameObject3.AddComponent<AttentionChaoWindow>();
				if (attentionChaoWindow != null)
				{
					attentionChaoWindow.Setup(chaoIds);
				}
			}
			return attentionChaoWindow;
		}
		return null;
	}

	// Token: 0x04000BB3 RID: 2995
	private const int MAX_CHAO = 3;

	// Token: 0x04000BB4 RID: 2996
	private List<int> m_attentionChaoIds;

	// Token: 0x04000BB5 RID: 2997
	private AttentionChaoWindow.Mode m_mode;

	// Token: 0x04000BB6 RID: 2998
	private Animation m_animation;

	// Token: 0x04000BB7 RID: 2999
	private bool m_close;

	// Token: 0x04000BB8 RID: 3000
	private List<ChaoData> m_chaoData;

	// Token: 0x04000BB9 RID: 3001
	private GameObject[] m_chaoWindow;

	// Token: 0x0200020A RID: 522
	private enum BUTTON_ACT
	{
		// Token: 0x04000BBB RID: 3003
		CLOSE,
		// Token: 0x04000BBC RID: 3004
		NONE
	}

	// Token: 0x0200020B RID: 523
	private enum Mode
	{
		// Token: 0x04000BBE RID: 3006
		Idle,
		// Token: 0x04000BBF RID: 3007
		Wait,
		// Token: 0x04000BC0 RID: 3008
		End
	}
}
