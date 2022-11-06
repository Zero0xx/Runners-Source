using System;
using UnityEngine;

// Token: 0x02000461 RID: 1121
public class HudQuickModeStagePicture
{
	// Token: 0x060021B1 RID: 8625 RVA: 0x000CAD94 File Offset: 0x000C8F94
	public void Initialize(GameObject mainMenuObject)
	{
		if (mainMenuObject == null)
		{
			return;
		}
		this.m_mainMenuObject = mainMenuObject;
		this.UpdateDisplay();
	}

	// Token: 0x060021B2 RID: 8626 RVA: 0x000CADB0 File Offset: 0x000C8FB0
	public void UpdateDisplay()
	{
		if (this.m_mainMenuObject == null)
		{
			return;
		}
		StageModeManager instance = StageModeManager.Instance;
		if (instance == null)
		{
			return;
		}
		CharacterAttribute quickStageCharaAttribute = instance.QuickStageCharaAttribute;
		int quickStageIndex = instance.QuickStageIndex;
		GameObject gameObject = GameObjectUtil.FindChildGameObject(this.m_mainMenuObject, "Anchor_5_MC");
		if (gameObject == null)
		{
			return;
		}
		GameObject gameObject2 = GameObjectUtil.FindChildGameObject(gameObject, "1_Quick");
		if (gameObject2 == null)
		{
			return;
		}
		GameObject gameObject3 = GameObjectUtil.FindChildGameObject(gameObject2, "Btn_3_play");
		if (gameObject3 == null)
		{
			return;
		}
		UITexture uitexture = GameObjectUtil.FindChildGameObjectComponent<UITexture>(gameObject3, "img_tex_next_map");
		UISprite uisprite = GameObjectUtil.FindChildGameObjectComponent<UISprite>(gameObject3, "img_icon_type_1");
		UISprite uisprite2 = GameObjectUtil.FindChildGameObjectComponent<UISprite>(gameObject3, "img_icon_type_2");
		UISprite uisprite3 = GameObjectUtil.FindChildGameObjectComponent<UISprite>(gameObject3, "img_icon_type_3");
		if (uitexture != null)
		{
			TextureRequestStagePicture request = new TextureRequestStagePicture(quickStageIndex, uitexture);
			TextureAsyncLoadManager.Instance.Request(request);
		}
		if (quickStageCharaAttribute == CharacterAttribute.UNKNOWN)
		{
			if (uisprite != null)
			{
				uisprite.gameObject.SetActive(true);
				uisprite.spriteName = HudQuickModeStagePicture.CalcCharaTypeSpriteName(CharacterAttribute.SPEED);
			}
			if (uisprite2 != null)
			{
				uisprite2.gameObject.SetActive(true);
				uisprite2.spriteName = HudQuickModeStagePicture.CalcCharaTypeSpriteName(CharacterAttribute.FLY);
			}
			if (uisprite3 != null)
			{
				uisprite3.gameObject.SetActive(true);
				uisprite3.spriteName = HudQuickModeStagePicture.CalcCharaTypeSpriteName(CharacterAttribute.POWER);
			}
		}
		else
		{
			if (uisprite != null)
			{
				uisprite.gameObject.SetActive(true);
				uisprite.spriteName = HudQuickModeStagePicture.CalcCharaTypeSpriteName(quickStageCharaAttribute);
			}
			if (uisprite2 != null)
			{
				uisprite2.gameObject.SetActive(false);
			}
			if (uisprite3 != null)
			{
				uisprite3.gameObject.SetActive(false);
			}
		}
	}

	// Token: 0x060021B3 RID: 8627 RVA: 0x000CAF88 File Offset: 0x000C9188
	private static string CalcCharaTypeSpriteName(CharacterAttribute charaAttribute)
	{
		string text = "ui_chao_set_type_icon_";
		switch (charaAttribute)
		{
		case CharacterAttribute.SPEED:
			text += "speed";
			break;
		case CharacterAttribute.FLY:
			text += "fly";
			break;
		case CharacterAttribute.POWER:
			text += "power";
			break;
		}
		return text;
	}

	// Token: 0x04001E69 RID: 7785
	private GameObject m_mainMenuObject;
}
