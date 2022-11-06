using System;
using System.Collections.Generic;
using Text;
using UnityEngine;

// Token: 0x02000447 RID: 1095
public class HudCharacterPanelUtil
{
	// Token: 0x0600211D RID: 8477 RVA: 0x000C6CE8 File Offset: 0x000C4EE8
	public static void SetImage(CharaType chara_type, GameObject obj)
	{
		if (obj != null)
		{
			UISprite component = obj.GetComponent<UISprite>();
			if (component != null)
			{
				string str = "ui_tex_player_";
				int num = (int)chara_type;
				string spriteName = str + num.ToString("00") + "_" + CharaName.PrefixName[(int)chara_type];
				component.spriteName = spriteName;
			}
		}
	}

	// Token: 0x0600211E RID: 8478 RVA: 0x000C6D40 File Offset: 0x000C4F40
	public static void SetIcon(CharaType chara_type, GameObject obj)
	{
		if (obj != null)
		{
			UISprite component = obj.GetComponent<UISprite>();
			if (component != null)
			{
				string str = "ui_tex_player_set_";
				int num = (int)chara_type;
				string spriteName = str + num.ToString("00") + "_" + CharaName.PrefixName[(int)chara_type];
				component.spriteName = spriteName;
			}
		}
	}

	// Token: 0x0600211F RID: 8479 RVA: 0x000C6D98 File Offset: 0x000C4F98
	public static void SetName(CharaType chara_type, GameObject obj)
	{
		if (obj != null)
		{
			UILabel component = obj.GetComponent<UILabel>();
			if (component != null)
			{
				if (chara_type != CharaType.UNKNOWN)
				{
					component.text = TextUtility.GetCommonText("CharaName", CharaName.Name[(int)chara_type]);
				}
				else
				{
					component.text = string.Empty;
				}
			}
		}
	}

	// Token: 0x06002120 RID: 8480 RVA: 0x000C6DF4 File Offset: 0x000C4FF4
	public static void SetLevel(CharaType chara_type, GameObject obj)
	{
		if (obj != null)
		{
			UILabel component = obj.GetComponent<UILabel>();
			if (component != null)
			{
				component.text = TextUtility.GetTextLevel(SaveDataUtil.GetCharaLevel(chara_type).ToString());
			}
		}
	}

	// Token: 0x06002121 RID: 8481 RVA: 0x000C6E3C File Offset: 0x000C503C
	public static void SetCharaType(CharaType chara_type, GameObject obj)
	{
		if (obj != null)
		{
			if (chara_type == CharaType.UNKNOWN)
			{
				HudCharacterPanelUtil.SetGameObjectActive(obj, false);
			}
			else
			{
				HudCharacterPanelUtil.SetGameObjectActive(obj, true);
				UISprite component = obj.GetComponent<UISprite>();
				if (component != null)
				{
					component.spriteName = HudUtility.GetCharaAttributeSpriteName(chara_type);
				}
			}
		}
	}

	// Token: 0x06002122 RID: 8482 RVA: 0x000C6E90 File Offset: 0x000C5090
	public static void SetTeamType(CharaType chara_type, GameObject obj)
	{
		if (obj != null)
		{
			if (chara_type == CharaType.UNKNOWN)
			{
				HudCharacterPanelUtil.SetGameObjectActive(obj, false);
			}
			else
			{
				HudCharacterPanelUtil.SetGameObjectActive(obj, true);
				UISprite component = obj.GetComponent<UISprite>();
				if (component != null)
				{
					component.spriteName = HudUtility.GetTeamAttributeSpriteName(chara_type);
				}
			}
		}
	}

	// Token: 0x06002123 RID: 8483 RVA: 0x000C6EE4 File Offset: 0x000C50E4
	public static void SetGameObjectActive(GameObject obj, bool active_flag)
	{
		if (obj != null)
		{
			obj.SetActive(active_flag);
		}
	}

	// Token: 0x06002124 RID: 8484 RVA: 0x000C6EFC File Offset: 0x000C50FC
	public static bool CheckValidChara(CharaType chara_type)
	{
		return CharaType.SONIC <= chara_type && chara_type < CharaType.NUM;
	}

	// Token: 0x06002125 RID: 8485 RVA: 0x000C6F10 File Offset: 0x000C5110
	public static void SetChaoImage(int chaoId, GameObject obj)
	{
		if (obj != null)
		{
			UITexture component = obj.GetComponent<UITexture>();
			if (component != null)
			{
				if (!obj.activeSelf)
				{
					obj.SetActive(true);
				}
				ChaoTextureManager.CallbackInfo info = new ChaoTextureManager.CallbackInfo(component, null, true);
				ChaoTextureManager.Instance.GetTexture(chaoId, info);
			}
			else
			{
				obj.SetActive(false);
			}
		}
	}

	// Token: 0x06002126 RID: 8486 RVA: 0x000C6F70 File Offset: 0x000C5170
	public static void SetupNoticeView(BonusParamContainer bonus, UILabel detailTextLabel, UISprite detailTextBg)
	{
		string a = string.Empty;
		string text;
		if (bonus.IsDetailInfo(out text) && detailTextLabel != null)
		{
			a = detailTextLabel.text;
			detailTextLabel.text = text;
		}
		if (detailTextBg != null && detailTextLabel != null)
		{
			TweenAlpha component = detailTextBg.GetComponent<TweenAlpha>();
			TweenAlpha component2 = detailTextLabel.GetComponent<TweenAlpha>();
			if (!string.IsNullOrEmpty(text))
			{
				if (component != null && component2 != null)
				{
					if (a != text)
					{
						component.Reset();
						component2.Reset();
						detailTextBg.alpha = 0f;
						detailTextLabel.alpha = 0f;
						component.enabled = true;
						component2.enabled = true;
						component.Play();
						component2.Play();
					}
					else if (!component.enabled)
					{
						detailTextBg.alpha = 0f;
						detailTextLabel.alpha = 0f;
						component.enabled = true;
						component2.enabled = true;
						component.Play();
						component2.Play();
					}
				}
			}
			else
			{
				detailTextBg.alpha = 0f;
				detailTextLabel.alpha = 0f;
				if (component != null && component2 != null)
				{
					component.Reset();
					component2.Reset();
					component.enabled = false;
					component2.enabled = false;
				}
			}
		}
	}

	// Token: 0x06002127 RID: 8487 RVA: 0x000C70C8 File Offset: 0x000C52C8
	public static void SetupAbilityIcon(BonusParamContainer bonus, CharaType playerMain, CharaType playerSub, GameObject player_set)
	{
		if (player_set != null)
		{
			List<UISprite> list = new List<UISprite>();
			List<UISprite> list2 = new List<UISprite>();
			GameObject gameObject = GameObjectUtil.FindChildGameObject(player_set, "Btn_player_main");
			GameObject gameObject2 = GameObjectUtil.FindChildGameObject(player_set, "Btn_player_sub");
			if (gameObject != null)
			{
				GameObject gameObject3 = GameObjectUtil.FindChildGameObject(gameObject, "ability");
				if (gameObject3 != null)
				{
					for (int i = 0; i < 7; i++)
					{
						UISprite uisprite = GameObjectUtil.FindChildGameObjectComponent<UISprite>(gameObject3, "img_ability_icon_" + i);
						if (!(uisprite != null))
						{
							break;
						}
						list.Add(uisprite);
					}
				}
			}
			if (gameObject2 != null)
			{
				GameObject gameObject4 = GameObjectUtil.FindChildGameObject(gameObject2, "ability");
				if (gameObject4 != null)
				{
					for (int j = 0; j < 7; j++)
					{
						UISprite uisprite2 = GameObjectUtil.FindChildGameObjectComponent<UISprite>(gameObject4, "img_ability_icon_" + j);
						if (!(uisprite2 != null))
						{
							break;
						}
						list2.Add(uisprite2);
					}
				}
			}
			if (bonus != null)
			{
				if (list.Count > 0)
				{
					for (int k = 0; k < list.Count; k++)
					{
						list[k].enabled = false;
						list[k].gameObject.SetActive(false);
					}
				}
				if (list2.Count > 0)
				{
					for (int l = 0; l < list2.Count; l++)
					{
						list2[l].enabled = false;
						list2[l].gameObject.SetActive(false);
					}
				}
				BonusParam bonusParam = bonus.GetBonusParam(0);
				BonusParam bonusParam2 = bonus.GetBonusParam(1);
				if (bonusParam != null && bonusParam.GetBonusInfo(BonusParam.BonusTarget.CHARA, true) != null)
				{
					Dictionary<BonusParam.BonusType, float> bonusInfo = bonusParam.GetBonusInfo(BonusParam.BonusTarget.CHARA, true);
					HudCharacterPanelUtil.SetAbilityIconSprite(ref list, bonusInfo, playerMain);
				}
				if (bonusParam2 != null && bonusParam2.GetBonusInfo(BonusParam.BonusTarget.CHARA, true) != null)
				{
					Dictionary<BonusParam.BonusType, float> bonusInfo2 = bonusParam2.GetBonusInfo(BonusParam.BonusTarget.CHARA, true);
					HudCharacterPanelUtil.SetAbilityIconSprite(ref list2, bonusInfo2, playerSub);
				}
			}
			else
			{
				if (list.Count > 0)
				{
					for (int m = 0; m < list.Count; m++)
					{
						list[m].enabled = false;
					}
				}
				if (list2.Count > 0)
				{
					for (int n = 0; n < list2.Count; n++)
					{
						list2[n].enabled = false;
					}
				}
			}
		}
	}

	// Token: 0x06002128 RID: 8488 RVA: 0x000C735C File Offset: 0x000C555C
	private static void SetAbilityIconSprite(ref List<UISprite> icons, Dictionary<BonusParam.BonusType, float> info, CharaType charaType)
	{
		if (info == null || icons == null)
		{
			return;
		}
		string text = "ui_chao_set_ability_icon_{PARAM}";
		if (info.Count > 0)
		{
			int num = 0;
			Dictionary<BonusParam.BonusType, float>.KeyCollection keys = info.Keys;
			List<BonusParam.BonusType> list = new List<BonusParam.BonusType>();
			foreach (BonusParam.BonusType item in keys)
			{
				list.Add(item);
			}
			Dictionary<BonusParam.BonusType, bool> dictionary = BonusUtil.IsTeamBonus(charaType, list);
			foreach (BonusParam.BonusType bonusType in keys)
			{
				if (info[bonusType] != 0f && dictionary != null && dictionary.ContainsKey(bonusType) && dictionary[bonusType])
				{
					switch (bonusType)
					{
					case BonusParam.BonusType.SCORE:
						if (BonusUtil.IsBonusMerit(bonusType, info[bonusType]))
						{
							icons[num].spriteName = text.Replace("{PARAM}", "Uscore");
						}
						else
						{
							icons[num].spriteName = text.Replace("{PARAM}", "Dscore");
						}
						icons[num].enabled = true;
						icons[num].gameObject.SetActive(true);
						num++;
						break;
					case BonusParam.BonusType.RING:
						if (BonusUtil.IsBonusMerit(bonusType, info[bonusType]))
						{
							icons[num].spriteName = text.Replace("{PARAM}", "Uring");
						}
						else
						{
							icons[num].spriteName = text.Replace("{PARAM}", "Dring");
						}
						icons[num].enabled = true;
						icons[num].gameObject.SetActive(true);
						num++;
						break;
					case BonusParam.BonusType.ANIMAL:
						if (BonusUtil.IsBonusMerit(bonusType, info[bonusType]))
						{
							icons[num].spriteName = text.Replace("{PARAM}", "Uanimal");
						}
						else
						{
							icons[num].spriteName = text.Replace("{PARAM}", "Danimal");
						}
						icons[num].enabled = true;
						icons[num].gameObject.SetActive(true);
						num++;
						break;
					case BonusParam.BonusType.DISTANCE:
						if (BonusUtil.IsBonusMerit(bonusType, info[bonusType]))
						{
							icons[num].spriteName = text.Replace("{PARAM}", "Urange");
						}
						else
						{
							icons[num].spriteName = text.Replace("{PARAM}", "Drange");
						}
						icons[num].enabled = true;
						icons[num].gameObject.SetActive(true);
						num++;
						break;
					case BonusParam.BonusType.ENEMY_OBJBREAK:
						if (BonusUtil.IsBonusMerit(bonusType, info[bonusType]))
						{
							icons[num].spriteName = text.Replace("{PARAM}", "Uenemy");
						}
						else
						{
							icons[num].spriteName = text.Replace("{PARAM}", "Denemy");
						}
						icons[num].enabled = true;
						icons[num].gameObject.SetActive(true);
						num++;
						break;
					case BonusParam.BonusType.TOTAL_SCORE:
						if (BonusUtil.IsBonusMerit(bonusType, info[bonusType]))
						{
							icons[num].spriteName = text.Replace("{PARAM}", "Ufscore");
						}
						else
						{
							icons[num].spriteName = text.Replace("{PARAM}", "Dfscore");
						}
						icons[num].enabled = true;
						icons[num].gameObject.SetActive(true);
						num++;
						break;
					case BonusParam.BonusType.SPEED:
						if (BonusUtil.IsBonusMerit(bonusType, info[bonusType]))
						{
							if (info[bonusType] > 100f)
							{
								icons[num].spriteName = text.Replace("{PARAM}", "Uspeed");
							}
							else
							{
								icons[num].spriteName = text.Replace("{PARAM}", "Dspeed");
							}
							icons[num].enabled = true;
							icons[num].gameObject.SetActive(true);
							num++;
						}
						break;
					}
				}
			}
		}
	}
}
