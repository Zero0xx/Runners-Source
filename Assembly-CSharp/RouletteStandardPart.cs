using System;
using System.Collections.Generic;
using AnimationOrTween;
using UnityEngine;

// Token: 0x02000512 RID: 1298
public class RouletteStandardPart : RoulettePartsBase
{
	// Token: 0x0600272A RID: 10026 RVA: 0x000F13D8 File Offset: 0x000EF5D8
	protected override void UpdateParts()
	{
		if (this.m_backButtonImg != null)
		{
			if (base.isSpin && base.spinDecisionIndex == -1)
			{
				this.m_backButtonImg.gameObject.SetActive(true);
				if (this.m_parent.spinTime > 10f)
				{
					this.m_backButtonImg.isEnabled = true;
				}
			}
			else
			{
				this.m_backButtonImg.gameObject.SetActive(false);
			}
		}
		if (!string.IsNullOrEmpty(this.m_spinErrorWindow))
		{
			if (GeneralWindow.IsCreated(this.m_spinErrorWindow))
			{
				if (GeneralWindow.IsButtonPressed)
				{
					if (GeneralWindow.IsYesButtonPressed)
					{
						ServerWheelOptionsData.SPIN_BUTTON spinBtn = this.m_spinBtn;
						if (spinBtn != ServerWheelOptionsData.SPIN_BUTTON.RING)
						{
							if (spinBtn == ServerWheelOptionsData.SPIN_BUTTON.RSRING)
							{
								HudMenuUtility.SendMenuButtonClicked(ButtonInfoTable.ButtonType.REDSTAR_TO_SHOP, false);
							}
						}
						else
						{
							HudMenuUtility.SendMenuButtonClicked(ButtonInfoTable.ButtonType.RING_TO_SHOP, false);
						}
					}
					GeneralWindow.Close();
					this.m_spinErrorWindow = null;
				}
			}
			else
			{
				GeneralWindow.Close();
				this.m_spinErrorWindow = null;
			}
		}
		if (RouletteManager.GetCurrentLoading() != null && RouletteManager.GetCurrentLoading().Count > 0)
		{
			if (this.m_frontCollider != null && !this.m_frontCollider.activeSelf)
			{
				this.m_frontCollider.SetActive(true);
			}
			this.m_frontColliderDelay = 0.25f;
		}
		else if (this.m_parent != null && (this.m_parent.isSpinGetWindow || this.m_parent.isWordAnime))
		{
			if (this.m_frontCollider != null && !this.m_frontCollider.activeSelf)
			{
				this.m_frontCollider.SetActive(true);
			}
			this.m_frontColliderDelay = 0.25f;
		}
		else if (base.isDelay)
		{
			if (this.m_frontCollider != null && !this.m_frontCollider.activeSelf)
			{
				this.m_frontCollider.SetActive(true);
			}
			if (this.m_frontColliderDelay < 0.0625f)
			{
				this.m_frontColliderDelay = 0.0625f;
			}
		}
		if (this.m_frontColliderDelay > 0f)
		{
			this.m_frontColliderDelay -= Time.deltaTime / Time.timeScale;
			if (this.m_frontColliderDelay <= 0f)
			{
				if (this.m_frontCollider != null)
				{
					this.m_frontCollider.SetActive(false);
				}
				this.m_frontColliderDelay = 0f;
			}
		}
		if (this.m_animeTime > 0f)
		{
			this.m_animeTime -= Time.deltaTime;
			if (this.m_animeTime <= 0f)
			{
				this.AnimationFinishCallback();
				this.m_animeTime = 0f;
				if (this.m_wordGet != null)
				{
					this.m_wordGet.SetActive(false);
				}
				if (this.m_wordJackpot != null)
				{
					this.m_wordJackpot.SetActive(false);
				}
				if (this.m_wordLavel != null)
				{
					this.m_wordLavel.SetActive(false);
				}
				if (this.m_wordRankup != null)
				{
					this.m_wordRankup.SetActive(false);
				}
				if (this.m_isJackpot)
				{
					this.m_isJackpot = false;
				}
			}
		}
		if (this.m_currentCategory != RouletteCategory.NONE && this.m_currentCategory != RouletteCategory.ITEM && this.m_attentionItemList == null)
		{
			if (RouletteManager.Instance != null && !RouletteManager.Instance.isCurrentPrizeLoading)
			{
				this.m_attentionItemList = base.wheel.GetAttentionItemList();
				if (this.m_attentionItemList != null)
				{
					this.m_eventUiNextUpdate = 0f;
					this.SetEventUI();
				}
			}
		}
		else if (this.m_eventUiNextUpdate > 0f)
		{
			this.m_eventUiNextUpdate -= Time.deltaTime;
			if (this.m_eventUiNextUpdate <= 0f)
			{
				this.m_eventUiNextUpdate = 0f;
				this.SetEventUI();
			}
		}
	}

	// Token: 0x0600272B RID: 10027 RVA: 0x000F17D4 File Offset: 0x000EF9D4
	public override void UpdateEffectSetting()
	{
		this.m_isEffectLock = !base.parent.IsEffect(RouletteTop.ROULETTE_EFFECT_TYPE.BG_PARTICLE);
		bool enabled = base.parent.IsEffect(RouletteTop.ROULETTE_EFFECT_TYPE.SPIN);
		if (this.m_spinButtons != null && this.m_spinButtons.Count > 0)
		{
			int num = 0;
			foreach (GameObject gameObject in this.m_spinButtons)
			{
				UIPlayAnimation[] components = gameObject.GetComponents<UIPlayAnimation>();
				if (components != null)
				{
					foreach (UIPlayAnimation uiplayAnimation in components)
					{
						uiplayAnimation.enabled = enabled;
					}
				}
				num++;
			}
		}
	}

	// Token: 0x0600272C RID: 10028 RVA: 0x000F18B4 File Offset: 0x000EFAB4
	public override void Setup(RouletteTop parent)
	{
		base.Setup(parent);
		this.m_eventUiCount = 0;
		this.m_eventUiNextUpdate = -1f;
		this.m_isEffectLock = false;
		if (this.m_backButton != null)
		{
			this.m_backButton.SetActive(true);
			this.m_backButtonImg = this.m_backButton.GetComponent<UIImageButton>();
			if (this.m_backButtonImg != null)
			{
				this.m_backButtonImg.isEnabled = false;
			}
		}
		this.m_isJackpot = false;
		this.m_spinErrorWindow = null;
		this.m_frontColliderDelay = 0f;
		if (this.m_frontCollider != null)
		{
			this.m_frontCollider.SetActive(false);
		}
		this.m_animeTime = 0f;
		if (this.m_attentionItemList != null)
		{
			this.m_attentionItemList.Clear();
			this.m_attentionItemList = null;
		}
		if (base.wheel != null)
		{
			base.wheel.ChangeSpinCost(0);
			this.m_attentionItemList = base.wheel.GetAttentionItemList();
			this.m_currentCategory = base.wheel.category;
			if (this.m_attentionItemList != null)
			{
				this.m_eventUiNextUpdate = 0f;
			}
		}
		if (this.m_wordGet != null)
		{
			this.m_wordGet.SetActive(false);
		}
		if (this.m_wordJackpot != null)
		{
			this.m_wordJackpot.SetActive(false);
		}
		if (this.m_wordLavel != null)
		{
			this.m_wordLavel.SetActive(false);
		}
		if (this.m_wordRankup != null)
		{
			this.m_wordRankup.SetActive(false);
		}
		this.SetEventUI();
		this.SetButton();
		this.SetEgg();
		this.UpdateEffectSetting();
	}

	// Token: 0x0600272D RID: 10029 RVA: 0x000F1A68 File Offset: 0x000EFC68
	public override void OnUpdateWheelData(ServerWheelOptionsData data)
	{
		base.OnUpdateWheelData(data);
		this.m_isJackpot = false;
		this.m_spinErrorWindow = null;
		this.m_frontColliderDelay = 0.125f;
		if (this.m_frontCollider != null)
		{
			this.m_frontCollider.SetActive(true);
		}
		this.m_animeTime = 0f;
		if (this.m_attentionItemList != null)
		{
			this.m_attentionItemList.Clear();
			this.m_attentionItemList = null;
		}
		if (base.wheel != null)
		{
			this.m_attentionItemList = base.wheel.GetAttentionItemList();
			this.m_currentCategory = base.wheel.category;
		}
		this.SetEventUI();
		this.SetButton();
		this.SetEgg();
		this.UpdateEffectSetting();
	}

	// Token: 0x0600272E RID: 10030 RVA: 0x000F1B20 File Offset: 0x000EFD20
	private void SetEventAttention()
	{
		if (this.m_eventUI != null && this.m_attentionItemList != null && this.m_eventUiCount >= 0)
		{
			GameObject gameObject = GameObjectUtil.FindChildGameObject(this.m_eventUI, "add_space");
			if (gameObject != null)
			{
				GameObject gameObject2 = GameObjectUtil.FindChildGameObject(gameObject, "chao_set");
				GameObject gameObject3 = GameObjectUtil.FindChildGameObject(gameObject, "item_set");
				GameObject gameObject4 = GameObjectUtil.FindChildGameObject(gameObject, "player_set");
				ServerItem serverItem = this.m_attentionItemList[this.m_eventUiCount % this.m_attentionItemList.Count];
				ServerItem.IdType idType = serverItem.idType;
				if (idType != ServerItem.IdType.EQUIP_ITEM)
				{
					if (idType != ServerItem.IdType.CHARA)
					{
						if (idType != ServerItem.IdType.CHAO)
						{
							if (gameObject3 != null)
							{
								gameObject3.SetActive(true);
								UISprite uisprite = GameObjectUtil.FindChildGameObjectComponent<UISprite>(gameObject3, "img_item");
								if (uisprite != null)
								{
									int id = (int)serverItem.id;
									uisprite.spriteName = "ui_cmn_icon_item_" + id;
								}
							}
							if (gameObject2 != null)
							{
								gameObject2.SetActive(false);
							}
							if (gameObject4 != null)
							{
								gameObject4.SetActive(false);
							}
						}
						else
						{
							if (gameObject2 != null)
							{
								gameObject2.SetActive(true);
								UITexture uitexture = GameObjectUtil.FindChildGameObjectComponent<UITexture>(gameObject2, "img_tex_chao");
								UISprite uisprite2 = GameObjectUtil.FindChildGameObjectComponent<UISprite>(gameObject2, "img_bord_bg");
								if (uitexture != null && ChaoTextureManager.Instance != null)
								{
									ChaoTextureManager.CallbackInfo info = new ChaoTextureManager.CallbackInfo(uitexture, null, true);
									ChaoTextureManager.Instance.GetTexture(serverItem.chaoId, info);
								}
								if (uisprite2 != null)
								{
									if (serverItem.id >= ServerItem.Id.CHAO_BEGIN_SRARE)
									{
										uisprite2.spriteName = "ui_chao_set_bg_m_2";
									}
									else if (serverItem.id >= ServerItem.Id.CHAO_BEGIN_RARE)
									{
										uisprite2.spriteName = "ui_chao_set_bg_m_1";
									}
									else
									{
										uisprite2.spriteName = "ui_chao_set_bg_m_0";
									}
								}
							}
							if (gameObject3 != null)
							{
								gameObject3.SetActive(false);
							}
							if (gameObject4 != null)
							{
								gameObject4.SetActive(false);
							}
						}
					}
					else
					{
						if (gameObject4 != null)
						{
							gameObject4.SetActive(true);
							UITexture uitexture2 = GameObjectUtil.FindChildGameObjectComponent<UITexture>(gameObject4, "img_tex_player");
							if (uitexture2 != null)
							{
								TextureRequestChara request = new TextureRequestChara(serverItem.charaType, uitexture2);
								TextureAsyncLoadManager.Instance.Request(request);
							}
						}
						if (gameObject2 != null)
						{
							gameObject2.SetActive(false);
						}
						if (gameObject3 != null)
						{
							gameObject3.SetActive(false);
						}
					}
				}
				else
				{
					if (gameObject3 != null)
					{
						gameObject3.SetActive(true);
						UISprite uisprite3 = GameObjectUtil.FindChildGameObjectComponent<UISprite>(gameObject3, "img_item");
						if (uisprite3 != null)
						{
							int num = serverItem.id - ServerItem.Id.INVINCIBLE;
							uisprite3.spriteName = "ui_cmn_icon_item_" + num;
						}
					}
					if (gameObject2 != null)
					{
						gameObject2.SetActive(false);
					}
					if (gameObject4 != null)
					{
						gameObject4.SetActive(false);
					}
				}
			}
		}
	}

	// Token: 0x0600272F RID: 10031 RVA: 0x000F1E40 File Offset: 0x000F0040
	private void SetEventUI()
	{
		if (this.m_eventUI != null)
		{
			if (!RouletteUtility.isTutorial || this.m_parent.wheelData.category != RouletteCategory.PREMIUM)
			{
				if (EventUtility.IsEnableRouletteUI())
				{
					bool flag = this.m_attentionItemList != null;
					if (flag && this.m_eventUiNextUpdate >= 0f)
					{
						this.SetEventAttention();
						this.m_eventUiNextUpdate = 10f;
						this.m_eventUiCount++;
					}
					this.m_eventUI.SetActive(flag);
				}
				else
				{
					this.m_eventUI.SetActive(false);
				}
			}
			else
			{
				this.m_eventUI.SetActive(false);
			}
		}
	}

	// Token: 0x06002730 RID: 10032 RVA: 0x000F1EF8 File Offset: 0x000F00F8
	private void SetEgg()
	{
		if (this.m_parent == null || this.m_parent.wheelData == null)
		{
			return;
		}
		int num = 0;
		bool eggSeting = this.m_parent.wheelData.GetEggSeting(out num);
		if (this.m_Eggs != null && this.m_Eggs.Count > 0)
		{
			bool flag = true;
			if (RouletteUtility.isTutorial && this.m_parent.wheelData.category == RouletteCategory.PREMIUM && this.m_parent.addSpecialEgg)
			{
				flag = false;
			}
			if (flag)
			{
				for (int i = 0; i < this.m_Eggs.Count; i++)
				{
					if (this.m_Eggs[i] != null)
					{
						this.m_Eggs[i].SetActive(num > i);
					}
				}
			}
		}
		if (this.m_spEgg != null)
		{
			this.m_spEgg.SetActive(eggSeting);
		}
	}

	// Token: 0x06002731 RID: 10033 RVA: 0x000F1FF8 File Offset: 0x000F01F8
	private void UpdateButtonCount(int offset)
	{
		if (this.m_spinBtn == ServerWheelOptionsData.SPIN_BUTTON.FREE)
		{
			this.m_remainingOffset = offset;
			GameObject gameObject = GameObjectUtil.FindChildGameObject(this.m_spinButtons[(int)this.m_spinBtn], "img_free_counter_bg");
			if (this.m_remainingNum - this.m_remainingOffset < 0 && gameObject != null)
			{
				gameObject.SetActive(false);
			}
			else if (gameObject != null)
			{
				gameObject.SetActive(true);
				UISprite uisprite = GameObjectUtil.FindChildGameObjectComponent<UISprite>(gameObject, "img_number_00");
				UISprite uisprite2 = GameObjectUtil.FindChildGameObjectComponent<UISprite>(gameObject, "img_number_0");
				if (uisprite != null && uisprite2 != null)
				{
					if (this.m_remainingNum - this.m_remainingOffset >= 100)
					{
						uisprite.spriteName = "ui_roulette_free_counter_number_9";
						uisprite2.spriteName = "ui_roulette_free_counter_number_9";
					}
					else if (this.m_remainingNum - this.m_remainingOffset <= 0)
					{
						uisprite.spriteName = "ui_roulette_free_counter_number_0";
						uisprite2.spriteName = "ui_roulette_free_counter_number_0";
					}
					else
					{
						uisprite.spriteName = "ui_roulette_free_counter_number_" + (this.m_remainingNum - this.m_remainingOffset) / 10 % 10;
						uisprite2.spriteName = "ui_roulette_free_counter_number_" + (this.m_remainingNum - this.m_remainingOffset) % 10;
					}
				}
			}
		}
	}

	// Token: 0x06002732 RID: 10034 RVA: 0x000F214C File Offset: 0x000F034C
	private void SetButton()
	{
		if (this.m_parent == null || this.m_parent.wheelData == null)
		{
			return;
		}
		this.m_campaign = this.m_parent.wheelData.GetCampaign();
		if (this.m_costBase != null && this.m_costList == null)
		{
			this.m_costList = new List<GameObject>();
			for (int i = 0; i < 5; i++)
			{
				GameObject gameObject = GameObjectUtil.FindChildGameObject(this.m_costBase, "roulette_cost_" + i);
				UIButtonMessage uibuttonMessage = GameObjectUtil.FindChildGameObjectComponent<UIButtonMessage>(this.m_costBase, "roulette_cost_" + i);
				if (!(gameObject != null))
				{
					break;
				}
				gameObject.SetActive(false);
				this.m_costList.Add(gameObject);
				if (uibuttonMessage != null)
				{
					uibuttonMessage.functionName = "OnClickSpinCost" + i;
				}
			}
		}
		this.SetCostItem(-1, 0);
		int num = 0;
		bool flag = false;
		this.m_spinBtn = this.m_parent.wheelData.GetSpinButtonSeting(out num, out flag);
		this.m_spinBtnActive = flag;
		flag = true;
		this.m_remainingOffset = 0;
		this.m_remainingNum = num;
		if (this.m_oddsButton != null)
		{
			this.m_oddsButton.SetActive(true);
		}
		this.SetButtonMulti();
		this.SetButtonSpin(num, flag);
		if (this.m_parent != null && this.m_parent.wheelData.category != RouletteCategory.ITEM)
		{
			if (RouletteUtility.isTutorial && this.m_parent.wheelData.category == RouletteCategory.PREMIUM)
			{
				GeneralUtil.SetRouletteBannerBtn(base.gameObject, "Btn_ad", base.gameObject, "OnClickBanner", this.m_parent.wheelData.category, false);
			}
			else
			{
				GeneralUtil.SetRouletteBannerBtn(base.gameObject, "Btn_ad", base.gameObject, "OnClickBanner", this.m_parent.wheelData.category, true);
			}
		}
		else
		{
			GeneralUtil.SetRouletteBannerBtn(base.gameObject, "Btn_ad", base.gameObject, "OnClickBanner", this.m_parent.wheelData.category, false);
		}
	}

	// Token: 0x06002733 RID: 10035 RVA: 0x000F2394 File Offset: 0x000F0594
	private void SetCostItem(int costItemId = -1, int offset = 0)
	{
		this.m_spinCostList = base.wheel.GetSpinCostItemIdList();
		if (this.m_spinCostList != null && this.m_spinCostList.Count > 0)
		{
			if (this.m_costList != null && this.m_costList.Count > 0)
			{
				for (int i = 0; i < this.m_costList.Count; i++)
				{
					GameObject gameObject = this.m_costList[i];
					if (gameObject != null)
					{
						if (i < this.m_spinCostList.Count && this.m_spinCostList[i] != 910000 && this.m_spinCostList[i] != 900000 && this.m_spinCostList[i] > 0)
						{
							gameObject.SetActive(true);
							UISprite uisprite = GameObjectUtil.FindChildGameObjectComponent<UISprite>(gameObject, "img_icon");
							UILabel uilabel = GameObjectUtil.FindChildGameObjectComponent<UILabel>(gameObject, "Lbl_num");
							UILabel uilabel2 = GameObjectUtil.FindChildGameObjectComponent<UILabel>(gameObject, "Lbl_num_sdw");
							if (uisprite != null && uilabel != null && uilabel != null)
							{
								uisprite.spriteName = "ui_cmn_icon_item_" + this.m_spinCostList[i];
								int num = base.wheel.GetSpinCostItemNum(this.m_spinCostList[i]);
								if (costItemId == this.m_spinCostList[i])
								{
									num += offset;
								}
								uilabel.text = HudUtility.GetFormatNumString<int>(num);
								uilabel2.text = HudUtility.GetFormatNumString<int>(num);
							}
						}
						else
						{
							gameObject.SetActive(false);
						}
					}
				}
			}
			else if (this.m_costList != null && this.m_costList.Count > 0)
			{
				for (int j = 0; j < this.m_costList.Count; j++)
				{
					this.m_costList[j].SetActive(false);
				}
			}
		}
		else if (this.m_costList != null && this.m_costList.Count > 0)
		{
			for (int k = 0; k < this.m_costList.Count; k++)
			{
				this.m_costList[k].SetActive(false);
			}
		}
	}

	// Token: 0x06002734 RID: 10036 RVA: 0x000F25E0 File Offset: 0x000F07E0
	private void SetButtonSpin(int count, bool btnAct)
	{
		if (this.m_spinButtons != null && this.m_spinButtons.Count > 0)
		{
			for (int i = 0; i < this.m_spinButtons.Count; i++)
			{
				if (this.m_spinButtons[i] != null)
				{
					if (this.m_spinBtn == (ServerWheelOptionsData.SPIN_BUTTON)i)
					{
						this.m_spinButtons[i].SetActive(true);
						UIPlayAnimation[] componentsInChildren = this.m_spinButtons[i].GetComponentsInChildren<UIPlayAnimation>();
						if (componentsInChildren != null && componentsInChildren.Length > 0)
						{
							foreach (UIPlayAnimation uiplayAnimation in componentsInChildren)
							{
								uiplayAnimation.enabled = this.m_spinBtnActive;
							}
						}
						UIImageButton uiimageButton = GameObjectUtil.FindChildGameObjectComponent<UIImageButton>(base.gameObject, this.m_spinButtons[i].name);
						if (uiimageButton != null)
						{
							uiimageButton.isEnabled = btnAct;
						}
						UISprite uisprite = GameObjectUtil.FindChildGameObjectComponent<UISprite>(this.m_spinButtons[i], "img_sale_icon");
						switch (this.m_spinBtn)
						{
						case ServerWheelOptionsData.SPIN_BUTTON.FREE:
						{
							GameObject gameObject = GameObjectUtil.FindChildGameObject(this.m_spinButtons[i], "img_free_counter_bg");
							if (this.m_remainingNum - this.m_remainingOffset < 0 && gameObject != null)
							{
								gameObject.SetActive(false);
							}
							else if (gameObject != null)
							{
								gameObject.SetActive(true);
								UISprite uisprite2 = GameObjectUtil.FindChildGameObjectComponent<UISprite>(gameObject, "img_number_00");
								UISprite uisprite3 = GameObjectUtil.FindChildGameObjectComponent<UISprite>(gameObject, "img_number_0");
								if (uisprite2 != null && uisprite3 != null)
								{
									if (this.m_remainingNum - this.m_remainingOffset >= 100)
									{
										uisprite2.spriteName = "ui_roulette_free_counter_number_9";
										uisprite3.spriteName = "ui_roulette_free_counter_number_9";
									}
									else if (this.m_remainingNum - this.m_remainingOffset <= 0)
									{
										uisprite2.spriteName = "ui_roulette_free_counter_number_0";
										uisprite3.spriteName = "ui_roulette_free_counter_number_0";
									}
									else
									{
										uisprite2.spriteName = "ui_roulette_free_counter_number_" + (this.m_remainingNum - this.m_remainingOffset) / 10 % 10;
										uisprite3.spriteName = "ui_roulette_free_counter_number_" + (this.m_remainingNum - this.m_remainingOffset) % 10;
									}
								}
							}
							if (uisprite != null)
							{
								if (this.m_campaign != null && this.m_campaign.Contains(Constants.Campaign.emType.FreeWheelSpinCount))
								{
									uisprite.gameObject.SetActive(true);
								}
								else
								{
									uisprite.gameObject.SetActive(false);
								}
							}
							break;
						}
						case ServerWheelOptionsData.SPIN_BUTTON.RING:
						case ServerWheelOptionsData.SPIN_BUTTON.RSRING:
						case ServerWheelOptionsData.SPIN_BUTTON.RAID:
						{
							UILabel uilabel = GameObjectUtil.FindChildGameObjectComponent<UILabel>(this.m_spinButtons[i], "Lbl_btn_" + i);
							UILabel uilabel2 = GameObjectUtil.FindChildGameObjectComponent<UILabel>(this.m_spinButtons[i], "Lbl_btn_" + i + "_sdw");
							if (uilabel != null && uilabel2 != null)
							{
								uilabel.text = HudUtility.GetFormatNumString<int>(count);
								uilabel2.text = HudUtility.GetFormatNumString<int>(count);
							}
							if (uisprite != null)
							{
								if (this.m_campaign != null && this.m_campaign.Contains(Constants.Campaign.emType.ChaoRouletteCost))
								{
									uisprite.gameObject.SetActive(true);
								}
								else
								{
									uisprite.gameObject.SetActive(false);
								}
							}
							break;
						}
						case ServerWheelOptionsData.SPIN_BUTTON.TICKET:
						{
							UISprite uisprite4 = GameObjectUtil.FindChildGameObjectComponent<UISprite>(this.m_spinButtons[i], "img_btn_" + i + "_icon");
							UILabel uilabel3 = GameObjectUtil.FindChildGameObjectComponent<UILabel>(this.m_spinButtons[i], "Lbl_btn_" + i);
							UILabel uilabel4 = GameObjectUtil.FindChildGameObjectComponent<UILabel>(this.m_spinButtons[i], "Lbl_btn_" + i + "_sdw");
							if (uisprite4 != null)
							{
								uisprite4.spriteName = base.wheel.GetRouletteTicketSprite();
							}
							if (uilabel3 != null && uilabel4 != null)
							{
								uilabel3.text = string.Empty + HudUtility.GetFormatNumString<int>(count);
								uilabel4.text = string.Empty + HudUtility.GetFormatNumString<int>(count);
							}
							if (uisprite != null)
							{
								uisprite.gameObject.SetActive(false);
							}
							break;
						}
						default:
							this.m_spinButtons[i].SetActive(false);
							break;
						}
					}
					else
					{
						this.m_spinButtons[i].SetActive(false);
					}
				}
			}
		}
	}

	// Token: 0x06002735 RID: 10037 RVA: 0x000F2A9C File Offset: 0x000F0C9C
	private void SetButtonMulti()
	{
		if (this.m_spinMultiButton == null)
		{
			this.m_spinMultiButton = GameObjectUtil.FindChildGameObject(base.gameObject, "multiple_switch");
		}
		if (this.m_spinMultiButton != null)
		{
			bool flag = false;
			if (base.wheel != null && base.wheel.isGeneral && base.wheel.GetRouletteRank() == RouletteUtility.WheelRank.Normal && (!RouletteUtility.isTutorial || this.m_parent.wheelData.category != RouletteCategory.PREMIUM) && this.m_spinBtn != ServerWheelOptionsData.SPIN_BUTTON.FREE)
			{
				flag = true;
			}
			else if (base.wheel != null && base.wheel.category == RouletteCategory.PREMIUM && (!RouletteUtility.isTutorial || this.m_parent.wheelData.category != RouletteCategory.PREMIUM) && this.m_spinBtn != ServerWheelOptionsData.SPIN_BUTTON.FREE)
			{
				flag = true;
			}
			if (flag)
			{
				this.m_spinMultiButton.SetActive(true);
				UIButtonMessage uibuttonMessage = GameObjectUtil.FindChildGameObjectComponent<UIButtonMessage>(this.m_spinMultiButton.gameObject, "Tgl_multi_0");
				UIButtonMessage uibuttonMessage2 = GameObjectUtil.FindChildGameObjectComponent<UIButtonMessage>(this.m_spinMultiButton.gameObject, "Tgl_multi_1");
				UIButtonMessage uibuttonMessage3 = GameObjectUtil.FindChildGameObjectComponent<UIButtonMessage>(this.m_spinMultiButton.gameObject, "Tgl_multi_2");
				if (uibuttonMessage == null)
				{
					uibuttonMessage = GameObjectUtil.FindChildGameObjectComponent<UIButtonMessage>(this.m_spinMultiButton.gameObject, "Tgl_single");
				}
				if (uibuttonMessage != null)
				{
					uibuttonMessage.functionName = "OnClickSpinMulti0";
					UIImageButton componentInChildren = uibuttonMessage.gameObject.GetComponentInChildren<UIImageButton>();
					if (componentInChildren != null)
					{
						componentInChildren.isEnabled = base.wheel.IsMulti(1);
					}
				}
				if (uibuttonMessage2 != null)
				{
					uibuttonMessage2.functionName = "OnClickSpinMulti1";
					UILabel componentInChildren2 = uibuttonMessage2.gameObject.GetComponentInChildren<UILabel>();
					UIImageButton componentInChildren3 = uibuttonMessage2.gameObject.GetComponentInChildren<UIImageButton>();
					if (componentInChildren2 != null)
					{
						componentInChildren2.text = string.Empty + 3;
					}
					if (componentInChildren3 != null)
					{
						componentInChildren3.isEnabled = base.wheel.IsMulti(3);
					}
				}
				if (uibuttonMessage3 != null)
				{
					uibuttonMessage3.functionName = "OnClickSpinMulti2";
					UILabel componentInChildren4 = uibuttonMessage3.gameObject.GetComponentInChildren<UILabel>();
					UIImageButton componentInChildren5 = uibuttonMessage3.gameObject.GetComponentInChildren<UIImageButton>();
					if (componentInChildren4 != null)
					{
						componentInChildren4.text = string.Empty + 5;
					}
					if (componentInChildren5 != null)
					{
						componentInChildren5.isEnabled = base.wheel.IsMulti(5);
					}
				}
				if (uibuttonMessage != null)
				{
					UIToggle componentInChildren6 = uibuttonMessage.gameObject.GetComponentInChildren<UIToggle>();
					if (componentInChildren6 != null)
					{
						if (base.wheel.multi == 1)
						{
							componentInChildren6.startsActive = true;
							componentInChildren6.SendMessage("Start");
						}
						else
						{
							componentInChildren6.startsActive = false;
						}
					}
				}
				if (uibuttonMessage2 != null)
				{
					UIToggle componentInChildren7 = uibuttonMessage2.gameObject.GetComponentInChildren<UIToggle>();
					if (componentInChildren7 != null)
					{
						if (base.wheel.multi == 3)
						{
							componentInChildren7.startsActive = true;
							componentInChildren7.SendMessage("Start");
						}
						else
						{
							componentInChildren7.startsActive = false;
						}
					}
				}
				if (uibuttonMessage3 != null)
				{
					UIToggle componentInChildren8 = uibuttonMessage3.gameObject.GetComponentInChildren<UIToggle>();
					if (componentInChildren8 != null)
					{
						if (base.wheel.multi == 5)
						{
							componentInChildren8.startsActive = true;
							componentInChildren8.SendMessage("Start");
						}
						else
						{
							componentInChildren8.startsActive = false;
						}
					}
				}
			}
			else
			{
				this.m_spinMultiButton.SetActive(false);
			}
		}
	}

	// Token: 0x06002736 RID: 10038 RVA: 0x000F2E48 File Offset: 0x000F1048
	private void OnClickFront()
	{
		if (this.m_parent == null || base.isDelay)
		{
			return;
		}
		if (base.isSpin && base.spinDecisionIndex != -1)
		{
			this.m_parent.OnRouletteSpinSkip();
		}
	}

	// Token: 0x06002737 RID: 10039 RVA: 0x000F2E98 File Offset: 0x000F1098
	private void OnClickOdds()
	{
		if (this.m_parent == null || base.isDelay || (RouletteUtility.isTutorial && this.m_parent.wheelData.category == RouletteCategory.PREMIUM))
		{
			return;
		}
		if (!RouletteManager.Instance.isCurrentPrizeLoading && !RouletteManager.IsPrizeLoading(this.m_parent.wheelData.category))
		{
			this.m_parent.SetDelayTime(0.2f);
			RouletteManager.RequestRoulettePrize(this.m_parent.wheelData.category, base.gameObject);
		}
	}

	// Token: 0x06002738 RID: 10040 RVA: 0x000F2F38 File Offset: 0x000F1138
	private void OnClickBack()
	{
		if (RouletteManager.IsRouletteEnabled() && base.isSpin && base.spinDecisionIndex == -1)
		{
			RouletteManager.RouletteClose();
		}
	}

	// Token: 0x06002739 RID: 10041 RVA: 0x000F2F64 File Offset: 0x000F1164
	private void OnClickSpin()
	{
		if (base.isSpin || this.m_parent == null || base.wheel == null || base.isDelay)
		{
			return;
		}
		if (!GeneralUtil.IsNetwork())
		{
			GeneralUtil.ShowNoCommunication("SpinNoCommunication");
			return;
		}
		if (this.m_spinBtnActive || (RouletteUtility.isTutorial && this.m_parent.wheelData.category == RouletteCategory.PREMIUM))
		{
			if (this.m_backButtonImg != null)
			{
				this.m_backButtonImg.isEnabled = false;
			}
			int spinCostItemId = base.wheel.GetSpinCostItemId();
			int spinCostItemCost = base.wheel.GetSpinCostItemCost(spinCostItemId);
			this.SetCostItem(spinCostItemId, spinCostItemCost * -1 * base.wheel.multi);
			this.m_isJackpot = false;
			base.wheel.PlaySe(ServerWheelOptionsData.SE_TYPE.Spin, 0f);
			this.m_parent.OnRouletteSpinStart(this.m_parent.wheelData, base.wheel.multi);
		}
		else
		{
			base.wheel.PlaySe(ServerWheelOptionsData.SE_TYPE.SpinError, 0f);
			if (this.m_spinBtn != ServerWheelOptionsData.SPIN_BUTTON.FREE)
			{
				this.m_spinErrorWindow = this.m_parent.wheelData.ShowSpinErrorWindow();
			}
			else
			{
				this.m_spinErrorWindow = this.m_parent.wheelData.ShowSpinErrorWindow();
			}
		}
	}

	// Token: 0x0600273A RID: 10042 RVA: 0x000F30BC File Offset: 0x000F12BC
	private void OnClickSpin0()
	{
		this.OnClickSpin();
	}

	// Token: 0x0600273B RID: 10043 RVA: 0x000F30C4 File Offset: 0x000F12C4
	private void OnClickSpin1()
	{
		this.OnClickSpin();
	}

	// Token: 0x0600273C RID: 10044 RVA: 0x000F30CC File Offset: 0x000F12CC
	private void OnClickSpin2()
	{
		this.OnClickSpin();
	}

	// Token: 0x0600273D RID: 10045 RVA: 0x000F30D4 File Offset: 0x000F12D4
	private void OnClickSpin3()
	{
		this.OnClickSpin();
	}

	// Token: 0x0600273E RID: 10046 RVA: 0x000F30DC File Offset: 0x000F12DC
	private void OnClickSpin4()
	{
		this.OnClickSpin();
	}

	// Token: 0x0600273F RID: 10047 RVA: 0x000F30E4 File Offset: 0x000F12E4
	private void OnClickSpin5()
	{
		this.OnClickSpin();
	}

	// Token: 0x06002740 RID: 10048 RVA: 0x000F30EC File Offset: 0x000F12EC
	private void OnClickSpinCost(int index)
	{
		if (base.wheel != null && this.m_spinCostList != null && this.m_spinCostList.Count > 1 && (!RouletteUtility.isTutorial || this.m_parent.wheelData.category != RouletteCategory.PREMIUM))
		{
			int spinCostItemId = base.wheel.GetSpinCostItemId();
			if (spinCostItemId != this.m_spinCostList[index] && base.wheel.ChangeSpinCost(index + 1))
			{
				base.wheel.ChangeMulti(base.wheel.multi);
				base.wheel.PlaySe(ServerWheelOptionsData.SE_TYPE.Click, 0f);
				this.SetButton();
			}
		}
	}

	// Token: 0x06002741 RID: 10049 RVA: 0x000F31A0 File Offset: 0x000F13A0
	private void OnClickSpinCost0()
	{
		this.OnClickSpinCost(0);
	}

	// Token: 0x06002742 RID: 10050 RVA: 0x000F31AC File Offset: 0x000F13AC
	private void OnClickSpinCost1()
	{
		this.OnClickSpinCost(1);
	}

	// Token: 0x06002743 RID: 10051 RVA: 0x000F31B8 File Offset: 0x000F13B8
	private void OnClickSpinCost2()
	{
		this.OnClickSpinCost(2);
	}

	// Token: 0x06002744 RID: 10052 RVA: 0x000F31C4 File Offset: 0x000F13C4
	private void OnClickSpinCost3()
	{
		this.OnClickSpinCost(3);
	}

	// Token: 0x06002745 RID: 10053 RVA: 0x000F31D0 File Offset: 0x000F13D0
	private void onClickInfoButton()
	{
		if (!RouletteManager.Instance.isCurrentPrizeLoading)
		{
			if (this.m_attentionItemList == null)
			{
				this.m_attentionItemList = base.wheel.GetAttentionItemList();
			}
			if (this.m_attentionItemList != null)
			{
				EventBestChaoWindow window = EventBestChaoWindow.GetWindow();
				if (window != null)
				{
					window.OpenWindow(this.m_attentionItemList);
				}
			}
		}
	}

	// Token: 0x06002746 RID: 10054 RVA: 0x000F3234 File Offset: 0x000F1434
	private void OnClickBanner()
	{
		if (this.m_parent != null && this.m_parent.wheelData != null)
		{
			this.m_parent.OnClickCurrentRouletteBanner();
		}
		global::Debug.Log("OnClickBanner !");
	}

	// Token: 0x06002747 RID: 10055 RVA: 0x000F3278 File Offset: 0x000F1478
	private void OnClickSpinMulti0()
	{
		if (base.wheel.ChangeMulti(1))
		{
			this.SetButton();
			base.wheel.PlaySe(ServerWheelOptionsData.SE_TYPE.Click, 0f);
		}
	}

	// Token: 0x06002748 RID: 10056 RVA: 0x000F32B0 File Offset: 0x000F14B0
	private void OnClickSpinMulti1()
	{
		if (base.wheel.ChangeMulti(3))
		{
			this.SetButton();
			base.wheel.PlaySe(ServerWheelOptionsData.SE_TYPE.Click, 0f);
		}
	}

	// Token: 0x06002749 RID: 10057 RVA: 0x000F32E8 File Offset: 0x000F14E8
	private void OnClickSpinMulti2()
	{
		if (base.wheel.ChangeMulti(5))
		{
			this.SetButton();
			base.wheel.PlaySe(ServerWheelOptionsData.SE_TYPE.Click, 0f);
		}
	}

	// Token: 0x0600274A RID: 10058 RVA: 0x000F3320 File Offset: 0x000F1520
	private void AnimationFinishCallback()
	{
		if (this.m_parent != null)
		{
			this.m_parent.OnRouletteWordAnimeEnd();
		}
	}

	// Token: 0x0600274B RID: 10059 RVA: 0x000F3340 File Offset: 0x000F1540
	private void FadeAnimationFinishCallback()
	{
		if (this.m_parent != null)
		{
			this.m_parent.OnRouletteSpinEnd();
			ActiveAnimation activeAnimation = ActiveAnimation.Play(this.m_fadeAnime, "ui_simple_load_outro_Anim2", Direction.Forward);
			EventDelegate.Add(activeAnimation.onFinished, new EventDelegate.Callback(this.FadeOut), true);
		}
	}

	// Token: 0x0600274C RID: 10060 RVA: 0x000F3394 File Offset: 0x000F1594
	private void FadeOut()
	{
	}

	// Token: 0x0600274D RID: 10061 RVA: 0x000F3398 File Offset: 0x000F1598
	private void RequestRoulettePrize_Succeeded(ServerPrizeState prize)
	{
		if (this.m_parent == null)
		{
			return;
		}
		this.m_parent.OpenOdds(prize, null);
	}

	// Token: 0x0600274E RID: 10062 RVA: 0x000F33C8 File Offset: 0x000F15C8
	private void RequestRoulettePrize_Failed()
	{
		global::Debug.Log("RequestRoulettePrize_Failed !!!");
	}

	// Token: 0x0600274F RID: 10063 RVA: 0x000F33D4 File Offset: 0x000F15D4
	public override void OnSpinStart()
	{
		this.m_frontColliderDelay = 5f;
		if (this.m_frontCollider != null)
		{
			this.m_frontCollider.SetActive(true);
		}
		if (this.m_spinBtn == ServerWheelOptionsData.SPIN_BUTTON.FREE || this.m_spinBtn == ServerWheelOptionsData.SPIN_BUTTON.TICKET)
		{
			this.UpdateButtonCount(1);
		}
	}

	// Token: 0x06002750 RID: 10064 RVA: 0x000F3428 File Offset: 0x000F1628
	public override void OnSpinSkip()
	{
		if (this.m_frontCollider != null)
		{
			this.m_frontColliderDelay = 5f;
			this.m_frontCollider.SetActive(true);
		}
	}

	// Token: 0x06002751 RID: 10065 RVA: 0x000F3460 File Offset: 0x000F1660
	public override void OnSpinDecision()
	{
		if (this.m_frontCollider != null)
		{
			this.m_frontColliderDelay = 5f;
			this.m_frontCollider.SetActive(true);
		}
	}

	// Token: 0x06002752 RID: 10066 RVA: 0x000F3498 File Offset: 0x000F1698
	public override void OnSpinDecisionMulti()
	{
		if (this.m_fadeAnime != null)
		{
			ActiveAnimation activeAnimation = ActiveAnimation.Play(this.m_fadeAnime, "ui_simple_load_intro_Anim2", Direction.Forward);
			EventDelegate.Add(activeAnimation.onFinished, new EventDelegate.Callback(this.FadeAnimationFinishCallback), true);
			base.wheel.PlaySe(ServerWheelOptionsData.SE_TYPE.Multi, 0f);
		}
		else
		{
			this.m_parent.OnRouletteSpinEnd();
		}
	}

	// Token: 0x06002753 RID: 10067 RVA: 0x000F3504 File Offset: 0x000F1704
	public override void OnSpinEnd()
	{
		global::Debug.Log("RouletteStandardPart OnSpinEnd !!!!!");
		if (this.m_frontCollider != null)
		{
			this.m_frontColliderDelay = 0.5f;
			this.m_frontCollider.SetActive(true);
		}
		if (this.m_spinBtn == ServerWheelOptionsData.SPIN_BUTTON.FREE && this.m_parent.wheelData.isRemainingRefresh)
		{
			this.UpdateButtonCount(0);
		}
		this.m_isJackpot = false;
		if (this.m_wordAnim != null && this.m_parent != null)
		{
			RouletteManager instance = RouletteManager.Instance;
			ServerWheelOptionsData wheelData = this.m_parent.wheelData;
			bool flag = true;
			if (wheelData != null && instance != null)
			{
				ServerSpinResultGeneral result = instance.GetResult();
				ServerChaoSpinResult resultChao = instance.GetResultChao();
				if (this.m_wordGet != null)
				{
					this.m_wordGet.SetActive(false);
				}
				if (this.m_wordJackpot != null)
				{
					this.m_wordJackpot.SetActive(false);
				}
				if (this.m_wordLavel != null)
				{
					this.m_wordLavel.SetActive(false);
				}
				if (this.m_wordRankup != null)
				{
					this.m_wordRankup.SetActive(false);
				}
				if (result != null)
				{
					if (result != null)
					{
						if (result.ItemWon >= 0)
						{
							int num = 0;
							ServerItem cellItem = wheelData.GetCellItem(result.ItemWon, out num);
							if (cellItem.idType == ServerItem.IdType.ITEM_ROULLETE_WIN)
							{
								if (wheelData.GetRouletteRank() != RouletteUtility.WheelRank.Super)
								{
									if (this.m_wordRankup != null)
									{
										this.m_wordRankup.SetActive(true);
									}
									base.wheel.PlaySe(ServerWheelOptionsData.SE_TYPE.GetRankup, 0.3f);
									this.m_frontColliderDelay = 0.125f;
								}
								else
								{
									if (this.m_wordJackpot != null)
									{
										this.m_wordJackpot.SetActive(true);
										this.m_isJackpot = true;
									}
									base.wheel.PlaySe(ServerWheelOptionsData.SE_TYPE.GetJackpot, 0.3f);
									this.m_frontColliderDelay = 0.125f;
								}
							}
							else
							{
								if (this.m_wordGet != null)
								{
									this.m_wordGet.SetActive(true);
								}
								bool flag2 = false;
								if (cellItem.idType == ServerItem.IdType.CHARA)
								{
									flag2 = true;
								}
								else if (cellItem.idType == ServerItem.IdType.CHAO && cellItem.chaoId >= 1000)
								{
									flag2 = true;
								}
								if (flag2)
								{
									base.wheel.PlaySe(ServerWheelOptionsData.SE_TYPE.GetRare, 0.3f);
								}
								else
								{
									base.wheel.PlaySe(ServerWheelOptionsData.SE_TYPE.GetItem, 0.3f);
								}
							}
						}
						else
						{
							flag = false;
						}
					}
				}
				else if (wheelData.wheelType == RouletteUtility.WheelType.Normal)
				{
					if (this.m_wordGet != null)
					{
						this.m_wordGet.SetActive(true);
					}
					bool flag3 = false;
					if (resultChao != null)
					{
						Dictionary<int, ServerItemState>.KeyCollection keys = resultChao.ItemState.Keys;
						foreach (int key in keys)
						{
							ServerItem item = resultChao.ItemState[key].GetItem();
							if (item.idType == ServerItem.IdType.CHARA)
							{
								flag3 = true;
								break;
							}
							if (item.idType == ServerItem.IdType.CHAO && item.chaoId >= 1000)
							{
								flag3 = true;
								break;
							}
						}
					}
					if (flag3)
					{
						base.wheel.PlaySe(ServerWheelOptionsData.SE_TYPE.GetRare, 0.3f);
					}
					else
					{
						base.wheel.PlaySe(ServerWheelOptionsData.SE_TYPE.GetItem, 0.3f);
					}
				}
				else
				{
					global::Debug.Log("RouletteStandardPart OnSpinEnd error?");
					if (wheelData.itemWonData.idType == ServerItem.IdType.ITEM_ROULLETE_WIN)
					{
						if (wheelData.GetRouletteRank() != RouletteUtility.WheelRank.Super)
						{
							if (this.m_wordRankup != null)
							{
								this.m_wordRankup.SetActive(true);
							}
							base.wheel.PlaySe(ServerWheelOptionsData.SE_TYPE.GetRankup, 0.3f);
						}
						else
						{
							if (this.m_wordJackpot != null)
							{
								this.m_wordJackpot.SetActive(true);
								this.m_isJackpot = true;
							}
							base.wheel.PlaySe(ServerWheelOptionsData.SE_TYPE.GetJackpot, 0.3f);
						}
					}
					else
					{
						if (this.m_wordGet != null)
						{
							this.m_wordGet.SetActive(true);
						}
						base.wheel.PlaySe(ServerWheelOptionsData.SE_TYPE.GetItem, 0.3f);
					}
				}
			}
			this.m_wordAnim.Stop("ui_menu_roulette_word_Anim");
			if (flag)
			{
				if (this.m_isJackpot)
				{
					this.m_animeTime = 3f;
				}
				else
				{
					this.m_animeTime = 1.1f;
				}
				ActiveAnimation.Play(this.m_wordAnim, "ui_menu_roulette_word_Anim", Direction.Forward);
			}
			else
			{
				this.m_animeTime = 0f;
				this.AnimationFinishCallback();
			}
		}
	}

	// Token: 0x06002754 RID: 10068 RVA: 0x000F39E0 File Offset: 0x000F1BE0
	public override void OnSpinError()
	{
		this.m_frontColliderDelay = 0f;
		if (this.m_frontCollider != null)
		{
			this.m_frontCollider.SetActive(false);
		}
	}

	// Token: 0x06002755 RID: 10069 RVA: 0x000F3A18 File Offset: 0x000F1C18
	public override void windowClose()
	{
		base.windowClose();
		if (this.m_frontCollider != null && !this.m_frontCollider.activeSelf)
		{
			this.m_frontCollider.SetActive(true);
		}
		this.m_frontColliderDelay = 0.25f;
	}

	// Token: 0x06002756 RID: 10070 RVA: 0x000F3A64 File Offset: 0x000F1C64
	public override void PartsSendMessage(string mesage)
	{
		if (!string.IsNullOrEmpty(mesage) && mesage.IndexOf("CostItemUpdate") != -1)
		{
			this.SetEgg();
			this.SetCostItem(-1, 0);
			this.SetButton();
		}
	}

	// Token: 0x04002380 RID: 9088
	private const string FADE_ANIM_INTRO = "ui_simple_load_intro_Anim2";

	// Token: 0x04002381 RID: 9089
	private const string FADE_ANIM_OUTRO = "ui_simple_load_outro_Anim2";

	// Token: 0x04002382 RID: 9090
	private const float FRONT_COLLIDER_DELAY = 0.25f;

	// Token: 0x04002383 RID: 9091
	private const float EVENT_UI_UPDATE_TIME = 10f;

	// Token: 0x04002384 RID: 9092
	[SerializeField]
	private Animation m_wordAnim;

	// Token: 0x04002385 RID: 9093
	[SerializeField]
	private GameObject m_wordGet;

	// Token: 0x04002386 RID: 9094
	[SerializeField]
	private GameObject m_wordRankup;

	// Token: 0x04002387 RID: 9095
	[SerializeField]
	private GameObject m_wordJackpot;

	// Token: 0x04002388 RID: 9096
	[SerializeField]
	private GameObject m_wordLavel;

	// Token: 0x04002389 RID: 9097
	[SerializeField]
	private GameObject m_spEgg;

	// Token: 0x0400238A RID: 9098
	[SerializeField]
	private List<GameObject> m_Eggs;

	// Token: 0x0400238B RID: 9099
	[SerializeField]
	private GameObject m_backButton;

	// Token: 0x0400238C RID: 9100
	[SerializeField]
	private GameObject m_oddsButton;

	// Token: 0x0400238D RID: 9101
	[SerializeField]
	private List<GameObject> m_spinButtons;

	// Token: 0x0400238E RID: 9102
	[SerializeField]
	private GameObject m_costBase;

	// Token: 0x0400238F RID: 9103
	[SerializeField]
	private GameObject m_eventUI;

	// Token: 0x04002390 RID: 9104
	[SerializeField]
	private GameObject m_frontCollider;

	// Token: 0x04002391 RID: 9105
	[SerializeField]
	private Animation m_fadeAnime;

	// Token: 0x04002392 RID: 9106
	private float m_frontColliderDelay;

	// Token: 0x04002393 RID: 9107
	private int m_remainingNum;

	// Token: 0x04002394 RID: 9108
	private int m_remainingOffset;

	// Token: 0x04002395 RID: 9109
	private float m_animeTime;

	// Token: 0x04002396 RID: 9110
	private ServerWheelOptionsData.SPIN_BUTTON m_spinBtn = ServerWheelOptionsData.SPIN_BUTTON.NONE;

	// Token: 0x04002397 RID: 9111
	private bool m_spinBtnActive;

	// Token: 0x04002398 RID: 9112
	private string m_spinErrorWindow;

	// Token: 0x04002399 RID: 9113
	private UIImageButton m_backButtonImg;

	// Token: 0x0400239A RID: 9114
	private List<int> m_spinCostList;

	// Token: 0x0400239B RID: 9115
	private List<ServerItem> m_attentionItemList;

	// Token: 0x0400239C RID: 9116
	private bool m_isJackpot;

	// Token: 0x0400239D RID: 9117
	private GameObject m_spinMultiButton;

	// Token: 0x0400239E RID: 9118
	private List<GameObject> m_costList;

	// Token: 0x0400239F RID: 9119
	private List<Constants.Campaign.emType> m_campaign;

	// Token: 0x040023A0 RID: 9120
	private RouletteCategory m_currentCategory;

	// Token: 0x040023A1 RID: 9121
	private int m_eventUiCount;

	// Token: 0x040023A2 RID: 9122
	private float m_eventUiNextUpdate = -1f;
}
