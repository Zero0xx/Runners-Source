using System;
using System.Collections.Generic;
using Text;
using UnityEngine;

// Token: 0x020003FF RID: 1023
public class DailyInfoChallenge : MonoBehaviour
{
	// Token: 0x06001E7C RID: 7804 RVA: 0x000B47E4 File Offset: 0x000B29E4
	public void Setup(DailyInfo parent)
	{
		this.m_parent = parent;
		this.m_info = daily_challenge.GetInfoFromSaveData(-1L);
		if (this.m_info != null)
		{
			UIButtonMessage uibuttonMessage = GameObjectUtil.FindChildGameObjectComponent<UIButtonMessage>(base.gameObject, "Btn_detail");
			UILabel uilabel = GameObjectUtil.FindChildGameObjectComponent<UILabel>(base.gameObject, "Lbl_days");
			GameObject gameObject = GameObjectUtil.FindChildGameObject(base.gameObject, "today_set");
			GameObject gameObject2 = GameObjectUtil.FindChildGameObject(base.gameObject, "tomorrow_set");
			if (uibuttonMessage != null)
			{
				uibuttonMessage.target = this.m_parent.gameObject;
				uibuttonMessage.functionName = "OnClickChallenge";
			}
			if (uilabel != null)
			{
				uilabel.text = TextUtility.Replaces(TextManager.GetText(TextManager.TextType.TEXTTYPE_COMMON_TEXT, "DailyMission", "day").text, new Dictionary<string, string>
				{
					{
						"{DAY}",
						(this.m_info.DayMax - this.m_info.DayIndex).ToString()
					}
				});
			}
			if (gameObject != null)
			{
				UISprite uisprite = GameObjectUtil.FindChildGameObjectComponent<UISprite>(gameObject, "img_clear");
				UILabel uilabel2 = GameObjectUtil.FindChildGameObjectComponent<UILabel>(gameObject, "Lbl_daily_challenge");
				UISlider uislider = GameObjectUtil.FindChildGameObjectComponent<UISlider>(gameObject, "Pgb_attainment");
				GameObject target = GameObjectUtil.FindChildGameObject(gameObject, "item_today");
				this.SetupItem(target, this.m_info.IsClearTodayMission);
				if (uisprite != null)
				{
					uisprite.gameObject.SetActive(this.m_info.IsClearTodayMission);
				}
				if (uilabel2 != null)
				{
					uilabel2.text = TextUtility.Replaces(this.m_info.TodayMissionText, new Dictionary<string, string>
					{
						{
							"{QUOTA}",
							this.m_info.TodayMissionQuota.ToString()
						}
					});
				}
				if (uislider != null)
				{
					float num = (float)this.m_info.TodayMissionClearQuota / (float)this.m_info.TodayMissionQuota;
					if (num > 1f)
					{
						num = 1f;
					}
					else if (num < 0f)
					{
						num = 0f;
					}
					uislider.value = num;
				}
			}
			if (gameObject2 != null)
			{
				GameObject target2 = GameObjectUtil.FindChildGameObject(gameObject2, "item_tomorrow");
				this.SetupItem(target2, false);
			}
		}
	}

	// Token: 0x06001E7D RID: 7805 RVA: 0x000B4A20 File Offset: 0x000B2C20
	private void SetupItem(GameObject target, bool isClear)
	{
		if (target != null && this.m_info != null)
		{
			UITexture uitexture = GameObjectUtil.FindChildGameObjectComponent<UITexture>(target, "img_chao");
			UISprite uisprite = GameObjectUtil.FindChildGameObjectComponent<UISprite>(target, "img_chara");
			UISprite uisprite2 = GameObjectUtil.FindChildGameObjectComponent<UISprite>(target, "img_daily_item");
			UISprite uisprite3 = GameObjectUtil.FindChildGameObjectComponent<UISprite>(target, "img_check");
			if (uisprite3 != null)
			{
				uisprite3.gameObject.SetActive(isClear);
			}
			int num = this.m_info.DayIndex;
			if (num >= this.m_info.InceniveIdTable.Length)
			{
				num = this.m_info.InceniveIdTable.Length - 1;
			}
			int num2 = this.m_info.InceniveIdTable[num];
			int num3 = Mathf.FloorToInt((float)num2 / 100000f);
			if (uisprite2 != null)
			{
				uisprite2.gameObject.SetActive(num3 == 0);
				if (num3 == 0)
				{
					uisprite2.spriteName = ((num >= this.m_info.InceniveIdTable.Length - 1) ? "ui_cmn_icon_item_9" : ("ui_cmn_icon_item_" + num2));
				}
			}
			if (uisprite != null)
			{
				uisprite.gameObject.SetActive(num3 == 3);
				if (num3 == 3)
				{
					UISprite uisprite4 = uisprite;
					string str = "ui_tex_player_";
					ServerItem serverItem = new ServerItem((ServerItem.Id)num2);
					uisprite4.spriteName = str + CharaTypeUtil.GetCharaSpriteNameSuffix(serverItem.charaType);
				}
			}
			if (uitexture != null)
			{
				uitexture.gameObject.SetActive(num3 == 4);
				if (num3 == 4)
				{
					ChaoTextureManager instance = ChaoTextureManager.Instance;
					int chao_id = num2 - 400000;
					if (instance != null)
					{
						Texture loadedTexture = instance.GetLoadedTexture(chao_id);
						if (loadedTexture != null)
						{
							uitexture.mainTexture = loadedTexture;
						}
						else
						{
							ChaoTextureManager.CallbackInfo info = new ChaoTextureManager.CallbackInfo(uitexture, null, true);
							instance.GetTexture(chao_id, info);
						}
					}
				}
			}
		}
	}

	// Token: 0x04001BE2 RID: 7138
	private DailyInfo m_parent;

	// Token: 0x04001BE3 RID: 7139
	private daily_challenge.DailyMissionInfo m_info;
}
