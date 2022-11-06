using System;
using DataTable;
using Text;
using UI;
using UnityEngine;

// Token: 0x020003D1 RID: 977
public class HudUtility
{
	// Token: 0x06001C5A RID: 7258 RVA: 0x000A8DDC File Offset: 0x000A6FDC
	public static string GetFormatNumString<Type>(Type num)
	{
		string text = string.Format("{0:#,0}", num);
		return text.Replace(",", " ");
	}

	// Token: 0x06001C5B RID: 7259 RVA: 0x000A8E0C File Offset: 0x000A700C
	public static void SetInvalidNGUIMitiTouch()
	{
		GameObject gameObject = GameObject.Find("UI Root (2D)");
		if (gameObject != null)
		{
			UICamera uicamera = GameObjectUtil.FindChildGameObjectComponent<UICamera>(gameObject, "Camera");
			if (uicamera != null)
			{
				uicamera.allowMultiTouch = false;
			}
		}
	}

	// Token: 0x06001C5C RID: 7260 RVA: 0x000A8E50 File Offset: 0x000A7050
	public static string GetEventStageName()
	{
		return HudUtility.GetEventStageName(EventManager.GetSpecificId());
	}

	// Token: 0x06001C5D RID: 7261 RVA: 0x000A8E5C File Offset: 0x000A705C
	public static string GetEventSpObjectName()
	{
		return HudUtility.GetEventSpObjectName(EventManager.GetSpecificId());
	}

	// Token: 0x06001C5E RID: 7262 RVA: 0x000A8E68 File Offset: 0x000A7068
	public static string GetEventStageName(int specificId)
	{
		string cellID = "sp_stage_name_" + specificId.ToString();
		return TextUtility.GetText(TextManager.TextType.TEXTTYPE_EVENT_COMMON_TEXT, "Name", cellID);
	}

	// Token: 0x06001C5F RID: 7263 RVA: 0x000A8E98 File Offset: 0x000A7098
	public static string GetEventSpObjectName(int specificId)
	{
		string cellID = "sp_object_name_" + specificId.ToString();
		return TextUtility.GetText(TextManager.TextType.TEXTTYPE_EVENT_COMMON_TEXT, "Name", cellID);
	}

	// Token: 0x06001C60 RID: 7264 RVA: 0x000A8EC8 File Offset: 0x000A70C8
	public static string MakeCharaTextureName(CharaType chara, HudUtility.TextureType texType)
	{
		string text = "ui_tex_player_";
		switch (texType)
		{
		case HudUtility.TextureType.TYPE_S:
			text += "set_";
			text += string.Format("{0:00}", (int)chara);
			text += "_";
			text += CharaName.PrefixName[(int)chara];
			break;
		case HudUtility.TextureType.TYPE_L:
			text += string.Format("{0:00}", (int)chara);
			text += "_";
			text += CharaName.PrefixName[(int)chara];
			break;
		}
		return text;
	}

	// Token: 0x06001C61 RID: 7265 RVA: 0x000A8F74 File Offset: 0x000A7174
	public static Vector2 GetScreenPosition(Camera camera, Vector3 worldPos)
	{
		Vector2 result = new Vector2(0f, 0f);
		if (camera == null)
		{
			return result;
		}
		result = camera.WorldToScreenPoint(worldPos);
		return result;
	}

	// Token: 0x06001C62 RID: 7266 RVA: 0x000A8FB0 File Offset: 0x000A71B0
	public static Vector2 GetScreenPosition(Camera camera, GameObject chaseObject)
	{
		if (chaseObject == null)
		{
			return new Vector2(0f, 0f);
		}
		return HudUtility.GetScreenPosition(camera, chaseObject.transform.localPosition);
	}

	// Token: 0x06001C63 RID: 7267 RVA: 0x000A8FEC File Offset: 0x000A71EC
	public static GameObject LoadPrefab(string prefabName, string attachAnthorName)
	{
		GameObject gameObject = Resources.Load(prefabName) as GameObject;
		if (gameObject == null)
		{
			return null;
		}
		GameObject gameObject2 = UnityEngine.Object.Instantiate(gameObject, gameObject.transform.localPosition, Quaternion.identity) as GameObject;
		GameObject gameObject3 = GameObject.Find(attachAnthorName);
		if (gameObject3 == null)
		{
			return null;
		}
		Vector3 localPosition = gameObject2.transform.localPosition;
		Vector3 localScale = gameObject2.transform.localScale;
		gameObject2.transform.parent = gameObject3.transform;
		gameObject2.transform.localPosition = localPosition;
		gameObject2.transform.localScale = localScale;
		return gameObject2;
	}

	// Token: 0x06001C64 RID: 7268 RVA: 0x000A9088 File Offset: 0x000A7288
	public static string GetCharaAttributeSpriteName(CharaType charaType)
	{
		CharacterDataNameInfo instance = CharacterDataNameInfo.Instance;
		if (instance == null)
		{
			return string.Empty;
		}
		CharacterDataNameInfo.Info dataByID = instance.GetDataByID(charaType);
		if (dataByID == null)
		{
			return string.Empty;
		}
		int attribute = (int)dataByID.m_attribute;
		return "ui_mm_player_species_" + attribute.ToString();
	}

	// Token: 0x06001C65 RID: 7269 RVA: 0x000A90DC File Offset: 0x000A72DC
	public static string GetTeamAttributeSpriteName(CharaType charaType)
	{
		CharacterDataNameInfo instance = CharacterDataNameInfo.Instance;
		if (instance == null)
		{
			return string.Empty;
		}
		CharacterDataNameInfo.Info dataByID = instance.GetDataByID(charaType);
		if (dataByID == null)
		{
			return string.Empty;
		}
		int teamAttribute = (int)dataByID.m_teamAttribute;
		return "ui_mm_player_genus_" + teamAttribute.ToString();
	}

	// Token: 0x06001C66 RID: 7270 RVA: 0x000A9130 File Offset: 0x000A7330
	public static int GetMixedStringToInt(string s)
	{
		string text = string.Empty;
		if (s == null)
		{
			return 0;
		}
		foreach (char c in s)
		{
			if (c >= '0' && c <= '9')
			{
				text += c;
			}
		}
		int result = 0;
		int.TryParse(text, out result);
		return result;
	}

	// Token: 0x06001C67 RID: 7271 RVA: 0x000A9198 File Offset: 0x000A7398
	public static int GetAge(DateTime birthDate, DateTime nowDate)
	{
		int num = nowDate.Year - birthDate.Year;
		if (nowDate.Month < birthDate.Month)
		{
			num--;
		}
		global::Debug.Log(string.Concat(new object[]
		{
			"age=",
			nowDate,
			"-",
			birthDate,
			"=",
			num
		}));
		return num;
	}

	// Token: 0x06001C68 RID: 7272 RVA: 0x000A9210 File Offset: 0x000A7410
	public static bool CheckPurchaseOver(string birthday, int monthPurchase, int addPurchase)
	{
		RegionManager instance = RegionManager.Instance;
		if (instance != null && !instance.IsJapan())
		{
			return false;
		}
		int num = 1;
		if (!string.IsNullOrEmpty(birthday))
		{
			num = HudUtility.GetAge(DateTime.Parse(birthday), NetUtil.GetCurrentTime());
		}
		global::Debug.Log("CheckPurchaseOver.birthday" + birthday);
		global::Debug.Log("CheckPurchaseOver.monthPurchase" + monthPurchase.ToString());
		global::Debug.Log("CheckPurchaseOver.addPurchase" + addPurchase.ToString());
		global::Debug.Log("CheckPurchaseOver.age" + num.ToString());
		foreach (HudUtility.PurchaseLimit purchaseLimit in HudUtility.s_purchaseLimits)
		{
			if (num < purchaseLimit.m_underAge && monthPurchase + addPurchase > purchaseLimit.m_maxPurchaseOfMonth)
			{
				global::Debug.Log("CheckPurchaseOver.OverPurchase");
				return true;
			}
		}
		global::Debug.Log("CheckPurchaseOver.InPurchase");
		return false;
	}

	// Token: 0x06001C69 RID: 7273 RVA: 0x000A9300 File Offset: 0x000A7500
	public static string GetChaoAbilityText(int chao_id, int level = -1)
	{
		ChaoData chaoData = ChaoTable.GetChaoData(chao_id);
		if (chaoData != null)
		{
			if (level == -1)
			{
				level = chaoData.level;
			}
			return chaoData.GetDetailLevelPlusSP(level);
		}
		return string.Empty;
	}

	// Token: 0x06001C6A RID: 7274 RVA: 0x000A9338 File Offset: 0x000A7538
	public static string GetChaoGrowAbilityText(int chao_id, int level = -1)
	{
		ChaoData chaoData = ChaoTable.GetChaoData(chao_id);
		if (chaoData != null)
		{
			if (level == -1)
			{
				level = chaoData.level;
			}
			return chaoData.GetGrowDetailLevelPlusSP(level);
		}
		return string.Empty;
	}

	// Token: 0x06001C6B RID: 7275 RVA: 0x000A9370 File Offset: 0x000A7570
	public static string GetChaoLoadingAbilityText(int chao_id)
	{
		ChaoData chaoData = ChaoTable.GetChaoData(chao_id);
		if (chaoData != null)
		{
			return chaoData.GetLoadingDetailsLevel(chaoData.level);
		}
		return string.Empty;
	}

	// Token: 0x06001C6C RID: 7276 RVA: 0x000A939C File Offset: 0x000A759C
	public static string GetChaoSPLoadingAbilityText(int chao_id)
	{
		ChaoData chaoData = ChaoTable.GetChaoData(chao_id);
		if (chaoData != null)
		{
			return chaoData.GetSPLoadingDetailsLevel(chaoData.level);
		}
		return string.Empty;
	}

	// Token: 0x06001C6D RID: 7277 RVA: 0x000A93C8 File Offset: 0x000A75C8
	public static string GetChaoMenuAbilityText(int chao_id)
	{
		ChaoData chaoData = ChaoTable.GetChaoData(chao_id);
		if (chaoData != null)
		{
			string spmainMenuDetailsLevel = chaoData.GetSPMainMenuDetailsLevel(chaoData.level);
			if (!string.IsNullOrEmpty(spmainMenuDetailsLevel))
			{
				return spmainMenuDetailsLevel;
			}
		}
		return string.Empty;
	}

	// Token: 0x06001C6E RID: 7278 RVA: 0x000A9404 File Offset: 0x000A7604
	public static string GetChaoCountBonusText(float value)
	{
		return TextUtility.Replace(TextManager.GetText(TextManager.TextType.TEXTTYPE_COMMON_TEXT, "ChaoSet", "bonus_percent").text, "{BONUS}", value.ToString("F1"));
	}

	// Token: 0x06001C6F RID: 7279 RVA: 0x000A943C File Offset: 0x000A763C
	public static void SetChaoTexture(UITexture uiTex, int chaoId, bool refreshFlag)
	{
		if (uiTex == null)
		{
			return;
		}
		if (chaoId >= 0)
		{
			ChaoTextureManager instance = ChaoTextureManager.Instance;
			if (instance != null)
			{
				ChaoTextureManager.CallbackInfo info = new ChaoTextureManager.CallbackInfo(uiTex, null, true);
				ChaoTextureManager.Instance.GetTexture(chaoId, info);
			}
			uiTex.gameObject.SetActive(true);
		}
		else
		{
			uiTex.gameObject.SetActive(false);
		}
	}

	// Token: 0x06001C70 RID: 7280 RVA: 0x000A94A4 File Offset: 0x000A76A4
	public static void SetupUILabelText(GameObject obj)
	{
		if (obj != null)
		{
			UILocalizeText component = obj.GetComponent<UILocalizeText>();
			if (component != null)
			{
				component.SetUILabelText();
			}
		}
	}

	// Token: 0x04001A57 RID: 6743
	private const int PURCHASE_AGE_0 = 13;

	// Token: 0x04001A58 RID: 6744
	private const int PURCHASE_MONEY_0 = 5000;

	// Token: 0x04001A59 RID: 6745
	private const int PURCHASE_AGE_1 = 20;

	// Token: 0x04001A5A RID: 6746
	private const int PURCHASE_MONEY_1 = 20000;

	// Token: 0x04001A5B RID: 6747
	private static HudUtility.PurchaseLimit[] s_purchaseLimits = new HudUtility.PurchaseLimit[]
	{
		new HudUtility.PurchaseLimit(13, 5000),
		new HudUtility.PurchaseLimit(20, 20000)
	};

	// Token: 0x020003D2 RID: 978
	public enum TextureType
	{
		// Token: 0x04001A5D RID: 6749
		TYPE_S,
		// Token: 0x04001A5E RID: 6750
		TYPE_M,
		// Token: 0x04001A5F RID: 6751
		TYPE_L
	}

	// Token: 0x020003D3 RID: 979
	private class PurchaseLimit
	{
		// Token: 0x06001C71 RID: 7281 RVA: 0x000A94E0 File Offset: 0x000A76E0
		public PurchaseLimit(int underAge, int maxMoney)
		{
			this.m_underAge = underAge;
			this.m_maxPurchaseOfMonth = maxMoney;
		}

		// Token: 0x04001A60 RID: 6752
		public int m_underAge;

		// Token: 0x04001A61 RID: 6753
		public int m_maxPurchaseOfMonth;
	}
}
