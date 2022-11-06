using System;
using System.Collections;
using AnimationOrTween;
using Text;
using UnityEngine;

// Token: 0x020004B9 RID: 1209
public class PlayerGetWindowUI : MonoBehaviour
{
	// Token: 0x060023DC RID: 9180 RVA: 0x000D7800 File Offset: 0x000D5A00
	public static void OpenWindow(GameObject calledGameObject, ServerItem serverItem)
	{
		GameObject gameObject = GameObject.Find("UI Root (2D)");
		if (gameObject != null)
		{
			GameObject gameObject2 = GameObjectUtil.FindChildGameObject(gameObject, "PlayerGetWindowUI");
			if (gameObject2 != null)
			{
				PlayerGetWindowUI component = gameObject2.GetComponent<PlayerGetWindowUI>();
				if (component != null)
				{
					component.OpenWindowSub(calledGameObject, serverItem);
				}
			}
		}
	}

	// Token: 0x060023DD RID: 9181 RVA: 0x000D7858 File Offset: 0x000D5A58
	private void OpenWindowSub(GameObject calledGameObject, ServerItem serverItem)
	{
		this.m_calledGameObject = calledGameObject;
		base.gameObject.SetActive(true);
		GameObject gameObject = GameObjectUtil.FindChildGameObject(base.gameObject, "player_get_window");
		if (gameObject != null)
		{
			gameObject.SetActive(true);
		}
		base.StartCoroutine(this.LateOpenWindow(serverItem));
	}

	// Token: 0x060023DE RID: 9182 RVA: 0x000D78AC File Offset: 0x000D5AAC
	private IEnumerator LateOpenWindow(ServerItem serverItem)
	{
		yield return null;
		SoundManager.SePlay("sys_window_open", "SE");
		this.UpdateView(serverItem);
		ActiveAnimation.Play(this.m_openAnimation, Direction.Forward);
		yield break;
	}

	// Token: 0x060023DF RID: 9183 RVA: 0x000D78D8 File Offset: 0x000D5AD8
	private void UpdateView(ServerItem serverItem)
	{
		CharaType charaType = serverItem.charaType;
		this.m_nameLabel.text = TextManager.GetText(TextManager.TextType.TEXTTYPE_COMMON_TEXT, "CharaName", CharaName.Name[(int)charaType]).text;
		this.m_levelLabel.text = TextUtility.GetTextLevel(MenuPlayerSetUtil.GetTotalLevel(charaType).ToString("D3"));
		this.m_detailsLabel.text = TextManager.GetText(TextManager.TextType.TEXTTYPE_COMMON_TEXT, "WindowText", "chara_attribute_" + CharaName.Name[(int)charaType]).text;
		this.m_playerSprite.spriteName = HudUtility.MakeCharaTextureName(charaType, HudUtility.TextureType.TYPE_L);
		CharacterAttribute characterAttribute = CharacterAttribute.UNKNOWN;
		TeamAttribute teamAttribute = TeamAttribute.UNKNOWN;
		if (CharacterDataNameInfo.Instance)
		{
			CharacterDataNameInfo.Info dataByID = CharacterDataNameInfo.Instance.GetDataByID(charaType);
			if (dataByID != null)
			{
				characterAttribute = dataByID.m_attribute;
				teamAttribute = dataByID.m_teamAttribute;
			}
		}
		UISprite typeSprite = this.m_typeSprite;
		string str = "ui_mm_player_species_";
		int num = (int)characterAttribute;
		typeSprite.spriteName = str + num.ToString();
		UISprite attrSprite = this.m_attrSprite;
		string str2 = "ui_mm_player_genus_";
		int num2 = (int)teamAttribute;
		attrSprite.spriteName = str2 + num2.ToString();
	}

	// Token: 0x060023E0 RID: 9184 RVA: 0x000D79E4 File Offset: 0x000D5BE4
	private void OnClickOkButton()
	{
		SoundManager.SePlay("sys_window_close", "SE");
	}

	// Token: 0x060023E1 RID: 9185 RVA: 0x000D79F8 File Offset: 0x000D5BF8
	public void OnFinishedCloseAnim()
	{
		if (this.m_calledGameObject != null)
		{
			this.m_calledGameObject.SendMessage("OnClosedCharaGetWindow", SendMessageOptions.DontRequireReceiver);
		}
	}

	// Token: 0x0400208C RID: 8332
	[SerializeField]
	public Animation m_openAnimation;

	// Token: 0x0400208D RID: 8333
	[SerializeField]
	public UILabel m_nameLabel;

	// Token: 0x0400208E RID: 8334
	[SerializeField]
	public UILabel m_levelLabel;

	// Token: 0x0400208F RID: 8335
	[SerializeField]
	public UILabel m_detailsLabel;

	// Token: 0x04002090 RID: 8336
	[SerializeField]
	public UISprite m_playerSprite;

	// Token: 0x04002091 RID: 8337
	[SerializeField]
	public UISprite m_typeSprite;

	// Token: 0x04002092 RID: 8338
	[SerializeField]
	public UISprite m_attrSprite;

	// Token: 0x04002093 RID: 8339
	private GameObject m_calledGameObject;
}
