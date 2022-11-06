using System;
using System.Collections;
using AnimationOrTween;
using Text;
using UnityEngine;

// Token: 0x020003C1 RID: 961
public class HudNetworkConnect : MonoBehaviour
{
	// Token: 0x06001BF8 RID: 7160 RVA: 0x000A6558 File Offset: 0x000A4758
	private void Start()
	{
		this.Setup();
		base.enabled = false;
	}

	// Token: 0x06001BF9 RID: 7161 RVA: 0x000A6568 File Offset: 0x000A4768
	private void Update()
	{
	}

	// Token: 0x06001BFA RID: 7162 RVA: 0x000A656C File Offset: 0x000A476C
	private void OnDestroy()
	{
		global::Debug.Log("HudNetworkConnct is Destroyed");
	}

	// Token: 0x06001BFB RID: 7163 RVA: 0x000A6578 File Offset: 0x000A4778
	public GameObject Setup()
	{
		if (this.m_object != null)
		{
			return this.m_object;
		}
		GameObject cameraUIObject = HudMenuUtility.GetCameraUIObject();
		if (cameraUIObject != null)
		{
			this.m_object = GameObjectUtil.FindChildGameObject(cameraUIObject, "ConnectAlertUI");
		}
		if (this.m_object != null)
		{
			this.m_imgRing = GameObjectUtil.FindChildGameObjectComponent<UISprite>(this.m_object, "img_ring");
			this.m_imgBg = GameObjectUtil.FindChildGameObjectComponent<UISprite>(this.m_object, "img_bg");
			this.m_labelConnect = GameObjectUtil.FindChildGameObjectComponent<UILabel>(this.m_object, "Lbl_conect_condition");
			if (this.m_imgRing != null)
			{
				this.m_imgRing.enabled = false;
			}
			if (this.m_imgBg != null)
			{
				this.m_imgBg.enabled = false;
			}
			if (this.m_labelConnect != null)
			{
				this.m_labelConnect.enabled = false;
				this.m_labelConnectSdw = GameObjectUtil.FindChildGameObjectComponent<UILabel>(this.m_labelConnect.gameObject, "Lbl_conect_condition_sh");
			}
			if (this.m_labelConnectSdw != null)
			{
				this.m_labelConnectSdw.enabled = false;
			}
		}
		return this.m_object;
	}

	// Token: 0x06001BFC RID: 7164 RVA: 0x000A66A8 File Offset: 0x000A48A8
	public void PlayStart(HudNetworkConnect.DisplayType displayType)
	{
		if (this.m_refCount > 0)
		{
			return;
		}
		this.m_refCount++;
		base.enabled = true;
		if (this.m_object == null)
		{
			return;
		}
		this.m_object.SetActive(true);
		this.m_isPlaying = true;
		string text = string.Empty;
		if (displayType == HudNetworkConnect.DisplayType.LOADING)
		{
			text = TextManager.GetText(TextManager.TextType.TEXTTYPE_FIXATION_TEXT, "MainMenuLoading", "Lbl_conect_condition").text;
		}
		else
		{
			text = TextManager.GetText(TextManager.TextType.TEXTTYPE_FIXATION_TEXT, "common", "ui_Lbl_connect").text;
		}
		if (this.m_labelConnect != null)
		{
			this.m_labelConnect.text = text;
			if (this.m_labelConnectSdw != null)
			{
				this.m_labelConnectSdw.text = text;
			}
		}
		UISprite uisprite = GameObjectUtil.FindChildGameObjectComponent<UISprite>(this.m_object, "window_bg");
		BoxCollider boxCollider = GameObjectUtil.FindChildGameObjectComponent<BoxCollider>(this.m_object, "window_bg_colider");
		Animation animation = null;
		GameObject gameObject = GameObjectUtil.FindChildGameObject(this.m_object, "Anchor_9_BR");
		if (gameObject != null)
		{
			animation = GameObjectUtil.FindChildGameObjectComponent<Animation>(gameObject, "Animation");
		}
		if (animation != null)
		{
			ActiveAnimation activeAnimation = ActiveAnimation.Play(animation, Direction.Forward, true);
			if (activeAnimation != null)
			{
				EventDelegate.Add(activeAnimation.onFinished, new EventDelegate.Callback(this.InAnimFinishCallback), true);
			}
			else
			{
				this.InAnimFinishCallback();
			}
		}
		switch (displayType)
		{
		case HudNetworkConnect.DisplayType.ALL:
			if (uisprite != null)
			{
				uisprite.enabled = true;
			}
			if (boxCollider != null)
			{
				boxCollider.enabled = true;
			}
			break;
		case HudNetworkConnect.DisplayType.NO_BG:
			if (uisprite != null)
			{
				uisprite.enabled = false;
			}
			if (boxCollider != null)
			{
				boxCollider.enabled = true;
			}
			break;
		case HudNetworkConnect.DisplayType.ONLY_ICON:
			if (uisprite != null)
			{
				uisprite.enabled = false;
			}
			if (boxCollider != null)
			{
				boxCollider.enabled = false;
			}
			break;
		}
		base.StartCoroutine(this.OnWaitDisplayAnchor9());
	}

	// Token: 0x06001BFD RID: 7165 RVA: 0x000A68B4 File Offset: 0x000A4AB4
	private IEnumerator OnWaitDisplayAnchor9()
	{
		int count = 1;
		while (count > 0)
		{
			count--;
			yield return null;
		}
		if (this.m_imgRing != null)
		{
			this.m_imgRing.enabled = true;
		}
		if (this.m_imgBg != null)
		{
			this.m_imgBg.enabled = true;
		}
		if (this.m_labelConnectSdw != null)
		{
			this.m_labelConnectSdw.enabled = true;
		}
		if (this.m_labelConnect != null)
		{
			this.m_labelConnect.enabled = true;
		}
		yield break;
	}

	// Token: 0x06001BFE RID: 7166 RVA: 0x000A68D0 File Offset: 0x000A4AD0
	public void PlayEnd()
	{
		this.m_refCount--;
		if (this.m_refCount > 0)
		{
			return;
		}
		this.m_refCount = 0;
		base.StartCoroutine(this.OnPlayEnd());
	}

	// Token: 0x06001BFF RID: 7167 RVA: 0x000A6904 File Offset: 0x000A4B04
	private IEnumerator OnPlayEnd()
	{
		while (this.m_isPlaying)
		{
			yield return null;
		}
		Animation animation = null;
		ActiveAnimation anim = null;
		if (this.m_object != null)
		{
			GameObject anchor9 = GameObjectUtil.FindChildGameObject(this.m_object, "Anchor_9_BR");
			if (anchor9 != null)
			{
				animation = GameObjectUtil.FindChildGameObjectComponent<Animation>(anchor9, "Animation");
				if (animation != null)
				{
					anim = ActiveAnimation.Play(animation, Direction.Reverse, true);
				}
			}
		}
		if (anim != null)
		{
			EventDelegate.Add(anim.onFinished, new EventDelegate.Callback(this.OutAnimFinishCallback), true);
		}
		else
		{
			this.OutAnimFinishCallback();
		}
		base.enabled = false;
		yield break;
	}

	// Token: 0x06001C00 RID: 7168 RVA: 0x000A6920 File Offset: 0x000A4B20
	private void InAnimFinishCallback()
	{
		this.m_isPlaying = false;
	}

	// Token: 0x06001C01 RID: 7169 RVA: 0x000A692C File Offset: 0x000A4B2C
	private void OutAnimFinishCallback()
	{
		if (this.m_object != null)
		{
			this.m_object.SetActive(false);
			this.m_object = null;
		}
	}

	// Token: 0x040019B7 RID: 6583
	private GameObject m_object;

	// Token: 0x040019B8 RID: 6584
	private UISprite m_imgRing;

	// Token: 0x040019B9 RID: 6585
	private UISprite m_imgBg;

	// Token: 0x040019BA RID: 6586
	private UILabel m_labelConnect;

	// Token: 0x040019BB RID: 6587
	private UILabel m_labelConnectSdw;

	// Token: 0x040019BC RID: 6588
	private bool m_isPlaying;

	// Token: 0x040019BD RID: 6589
	private int m_refCount;

	// Token: 0x020003C2 RID: 962
	public enum DisplayType
	{
		// Token: 0x040019BF RID: 6591
		ALL,
		// Token: 0x040019C0 RID: 6592
		NO_BG,
		// Token: 0x040019C1 RID: 6593
		ONLY_ICON,
		// Token: 0x040019C2 RID: 6594
		LOADING
	}
}
