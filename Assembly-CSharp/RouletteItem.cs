using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000510 RID: 1296
public class RouletteItem : MonoBehaviour
{
	// Token: 0x17000527 RID: 1319
	// (get) Token: 0x0600270A RID: 9994 RVA: 0x000EFFE8 File Offset: 0x000EE1E8
	public bool isRank
	{
		get
		{
			bool result = false;
			if (this.m_parent != null && this.m_parent.wheelData != null)
			{
				ServerWheelOptionsData wheelData = this.m_parent.wheelData;
				RouletteUtility.WheelType wheelType = wheelData.wheelType;
				if (wheelType != RouletteUtility.WheelType.Normal)
				{
					if (wheelType == RouletteUtility.WheelType.Rankup)
					{
						if (wheelData.GetCellItem(this.m_cellIndex).idType == ServerItem.IdType.ITEM_ROULLETE_WIN)
						{
							result = true;
						}
					}
				}
				else
				{
					result = false;
				}
			}
			return result;
		}
	}

	// Token: 0x0600270B RID: 9995 RVA: 0x000F0068 File Offset: 0x000EE268
	public void Setup(RouletteBoard parent, int cellIndex)
	{
		this.m_parent = parent;
		this.m_cellIndex = cellIndex;
		this.m_basePos = base.gameObject.transform.parent.localPosition;
		if (this.m_parent != null && this.m_parent.wheelData != null)
		{
			ServerWheelOptionsData wheelData = this.m_parent.wheelData;
			if (wheelData.isGeneral)
			{
				this.SetGeneral(wheelData);
			}
			else
			{
				RouletteUtility.WheelType wheelType = wheelData.wheelType;
				if (wheelType != RouletteUtility.WheelType.Normal)
				{
					if (wheelType == RouletteUtility.WheelType.Rankup)
					{
						this.SetItem(wheelData);
					}
				}
				else
				{
					this.SetEgg(wheelData);
				}
			}
			this.SetTweenDelay();
		}
	}

	// Token: 0x0600270C RID: 9996 RVA: 0x000F011C File Offset: 0x000EE31C
	private void SetTweenDelay()
	{
		TweenColor[] componentsInChildren = base.gameObject.GetComponentsInChildren<TweenColor>();
		TweenRotation[] componentsInChildren2 = base.gameObject.GetComponentsInChildren<TweenRotation>();
		TweenAlpha[] componentsInChildren3 = base.gameObject.GetComponentsInChildren<TweenAlpha>();
		TweenScale[] componentsInChildren4 = base.gameObject.GetComponentsInChildren<TweenScale>();
		TweenPosition[] componentsInChildren5 = base.gameObject.GetComponentsInChildren<TweenPosition>();
		if (componentsInChildren != null)
		{
			foreach (TweenColor tweenColor in componentsInChildren)
			{
				tweenColor.delay = UnityEngine.Random.Range(0f, tweenColor.duration);
			}
		}
		if (componentsInChildren2 != null)
		{
			foreach (TweenRotation tweenRotation in componentsInChildren2)
			{
				tweenRotation.delay = UnityEngine.Random.Range(0f, tweenRotation.duration);
			}
		}
		if (componentsInChildren3 != null)
		{
			foreach (TweenAlpha tweenAlpha in componentsInChildren3)
			{
				tweenAlpha.delay = UnityEngine.Random.Range(0f, tweenAlpha.duration);
			}
		}
		if (componentsInChildren4 != null)
		{
			foreach (TweenScale tweenScale in componentsInChildren4)
			{
				tweenScale.delay = UnityEngine.Random.Range(0f, tweenScale.duration);
			}
		}
		if (componentsInChildren5 != null)
		{
			foreach (TweenPosition tweenPosition in componentsInChildren5)
			{
				tweenPosition.delay = UnityEngine.Random.Range(0f, tweenPosition.duration);
			}
		}
	}

	// Token: 0x0600270D RID: 9997 RVA: 0x000F02B0 File Offset: 0x000EE4B0
	private void SetCampaign(ServerItem.Id itemId)
	{
		bool flag = false;
		bool flag2 = false;
		if (this.m_parent.wheelData.IsCampaign(Constants.Campaign.emType.PremiumRouletteOdds) && ServerInterface.CampaignState != null)
		{
			ServerCampaignData campaignInSession = ServerInterface.CampaignState.GetCampaignInSession(Constants.Campaign.emType.PremiumRouletteOdds, this.m_cellIndex);
			if (campaignInSession != null && campaignInSession.iContent > 0)
			{
				float cellWeight = this.m_parent.wheel.GetCellWeight(this.m_cellIndex);
				if ((float)campaignInSession.iContent > cellWeight)
				{
					flag = true;
				}
			}
		}
		if (this.m_parent.wheelData.IsCampaign(Constants.Campaign.emType.JackPotValueBonus) && ServerInterface.CampaignState != null)
		{
			ServerCampaignData campaignInSession2 = ServerInterface.CampaignState.GetCampaignInSession(Constants.Campaign.emType.JackPotValueBonus, this.m_cellIndex);
			if (campaignInSession2 != null && campaignInSession2.iContent > 0)
			{
				flag2 = true;
			}
		}
		if (this.m_campaign != null)
		{
			bool active = false;
			if ((flag || flag2) && this.m_campaignList != null && this.m_campaignList.Count > 0)
			{
				foreach (GameObject gameObject in this.m_campaignList)
				{
					bool flag3 = false;
					if (gameObject.name.IndexOf("jackpot") != -1)
					{
						if (itemId == ServerItem.Id.JACKPOT && flag2)
						{
							flag3 = true;
						}
					}
					else if (itemId != ServerItem.Id.JACKPOT && flag)
					{
						flag3 = true;
						float num = 50f / this.m_basePos.magnitude;
						float num2 = this.m_basePos.x * num;
						float num3 = this.m_basePos.y * num;
						num3 += 45f + num3 / 10f;
						if (num3 <= 20f && Mathf.Abs(num2) <= 60f)
						{
							if (num2 >= 0f)
							{
								num2 = 60f;
							}
							else
							{
								num2 = -60f;
							}
							num3 = 20f;
						}
						gameObject.gameObject.transform.localPosition = new Vector3(num2, num3, 0f);
					}
					gameObject.SetActive(flag3);
					if (flag3)
					{
						active = true;
						break;
					}
				}
			}
			this.m_campaign.SetActive(active);
		}
	}

	// Token: 0x0600270E RID: 9998 RVA: 0x000F0524 File Offset: 0x000EE724
	private void SetEgg(ServerWheelOptionsData data)
	{
		if (this.m_egg != null)
		{
			this.m_egg.SetActive(true);
			UISprite uisprite = GameObjectUtil.FindChildGameObjectComponent<UISprite>(this.m_egg, "main");
			uisprite.spriteName = "ui_roulette_chao_egg_" + data.GetCellEgg(this.m_cellIndex);
		}
		if (this.m_item != null)
		{
			this.m_item.SetActive(false);
		}
		if (this.m_rank != null)
		{
			this.m_rank.SetActive(false);
		}
		if (this.m_common != null)
		{
			this.m_common.SetActive(false);
		}
		this.SetCampaign(ServerItem.Id.CHAO_BEGIN);
	}

	// Token: 0x0600270F RID: 9999 RVA: 0x000F05E4 File Offset: 0x000EE7E4
	private void SetItem(ServerWheelOptionsData data)
	{
		if (this.m_egg != null)
		{
			this.m_egg.SetActive(false);
		}
		if (this.m_item != null)
		{
			this.m_item.SetActive(false);
		}
		if (this.m_rank != null)
		{
			this.m_rank.SetActive(false);
		}
		if (this.m_common != null)
		{
			this.m_common.SetActive(false);
		}
		int num;
		ServerItem cellItem = data.GetCellItem(this.m_cellIndex, out num);
		if (cellItem.serverItemNum > 0)
		{
			num *= cellItem.serverItemNum;
		}
		ServerItem.IdType idType = cellItem.idType;
		switch (idType)
		{
		case ServerItem.IdType.RSRING:
		case ServerItem.IdType.RING:
		case ServerItem.IdType.ENERGY:
			break;
		default:
			if (idType != ServerItem.IdType.EQUIP_ITEM)
			{
				if (idType == ServerItem.IdType.ITEM_ROULLETE_WIN)
				{
					if (this.m_rank != null)
					{
						base.transform.localScale = new Vector3(1f / base.transform.parent.transform.localScale.x, 1f / base.transform.parent.transform.localScale.x, 1f);
						this.m_rank.SetActive(true);
						UISprite uisprite = GameObjectUtil.FindChildGameObjectComponent<UISprite>(this.m_rank, "jack");
						UISprite uisprite2 = GameObjectUtil.FindChildGameObjectComponent<UISprite>(this.m_rank, "big");
						UISprite uisprite3 = GameObjectUtil.FindChildGameObjectComponent<UISprite>(this.m_rank, "super");
						if (uisprite != null && uisprite2 != null && uisprite3 != null)
						{
							uisprite.gameObject.SetActive(false);
							uisprite2.gameObject.SetActive(false);
							uisprite3.gameObject.SetActive(false);
							switch (data.GetRouletteRank())
							{
							case RouletteUtility.WheelRank.Normal:
								uisprite2.gameObject.SetActive(true);
								break;
							case RouletteUtility.WheelRank.Big:
								uisprite3.gameObject.SetActive(true);
								break;
							case RouletteUtility.WheelRank.Super:
							{
								uisprite.gameObject.SetActive(true);
								UILabel uilabel = GameObjectUtil.FindChildGameObjectComponent<UILabel>(uisprite.gameObject, "ring");
								if (uilabel != null)
								{
									int num2 = RouletteManager.numJackpotRing;
									if (num2 <= 0)
									{
										num2 = 30000;
									}
									uilabel.text = HudUtility.GetFormatNumString<int>(num2);
								}
								break;
							}
							}
						}
					}
					goto IL_433;
				}
				if (idType != ServerItem.IdType.CHARA && idType != ServerItem.IdType.CHAO)
				{
					base.transform.localScale = new Vector3(1f, 1f, 1f);
					if (this.m_item != null)
					{
						this.m_item.SetActive(true);
						UISprite uisprite4 = GameObjectUtil.FindChildGameObjectComponent<UISprite>(this.m_item, "main");
						uisprite4.spriteName = "ui_cmn_icon_item_" + cellItem.id;
					}
					if (this.m_common != null)
					{
						this.m_common.SetActive(true);
						UILabel uilabel2 = GameObjectUtil.FindChildGameObjectComponent<UILabel>(this.m_common, "num");
						uilabel2.text = "×" + num;
					}
					goto IL_433;
				}
				if (this.m_egg != null)
				{
					this.m_egg.SetActive(true);
					UISprite uisprite5 = GameObjectUtil.FindChildGameObjectComponent<UISprite>(this.m_egg, "main");
					uisprite5.spriteName = "ui_roulette_chao_egg_" + data.GetCellEgg(this.m_cellIndex);
				}
				goto IL_433;
			}
			break;
		}
		base.transform.localScale = new Vector3(1f, 1f, 1f);
		if (this.m_item != null)
		{
			this.m_item.SetActive(true);
			UISprite uisprite6 = GameObjectUtil.FindChildGameObjectComponent<UISprite>(this.m_item, "main");
			uisprite6.spriteName = cellItem.serverItemSpriteNameRoulette;
		}
		if (this.m_common != null)
		{
			this.m_common.SetActive(true);
			UILabel uilabel3 = GameObjectUtil.FindChildGameObjectComponent<UILabel>(this.m_common, "num");
			uilabel3.text = "×" + num;
		}
		IL_433:
		this.SetCampaign(cellItem.id);
	}

	// Token: 0x06002710 RID: 10000 RVA: 0x000F0A34 File Offset: 0x000EEC34
	private void SetGeneral(ServerWheelOptionsData data)
	{
		if (this.m_egg != null)
		{
			this.m_egg.SetActive(false);
		}
		if (this.m_item != null)
		{
			this.m_item.SetActive(false);
		}
		if (this.m_rank != null)
		{
			this.m_rank.SetActive(false);
		}
		if (this.m_common != null)
		{
			this.m_common.SetActive(false);
		}
		int num;
		ServerItem cellItem = data.GetCellItem(this.m_cellIndex, out num);
		if (cellItem.serverItemNum > 0)
		{
			num *= cellItem.serverItemNum;
		}
		ServerItem.IdType idType = cellItem.idType;
		switch (idType)
		{
		case ServerItem.IdType.RSRING:
		case ServerItem.IdType.RING:
		case ServerItem.IdType.ENERGY:
			break;
		default:
			if (idType != ServerItem.IdType.EQUIP_ITEM)
			{
				if (idType == ServerItem.IdType.ITEM_ROULLETE_WIN)
				{
					if (this.m_rank != null)
					{
						base.transform.localScale = new Vector3(1f / base.transform.parent.transform.localScale.x, 1f / base.transform.parent.transform.localScale.x, 1f);
						this.m_rank.SetActive(true);
						UISprite uisprite = GameObjectUtil.FindChildGameObjectComponent<UISprite>(this.m_rank, "jack");
						UISprite uisprite2 = GameObjectUtil.FindChildGameObjectComponent<UISprite>(this.m_rank, "big");
						UISprite uisprite3 = GameObjectUtil.FindChildGameObjectComponent<UISprite>(this.m_rank, "super");
						if (uisprite != null && uisprite2 != null && uisprite3 != null)
						{
							uisprite.gameObject.SetActive(false);
							uisprite2.gameObject.SetActive(false);
							uisprite3.gameObject.SetActive(false);
							switch (data.GetRouletteRank())
							{
							case RouletteUtility.WheelRank.Normal:
								uisprite2.gameObject.SetActive(true);
								break;
							case RouletteUtility.WheelRank.Big:
								uisprite3.gameObject.SetActive(true);
								break;
							case RouletteUtility.WheelRank.Super:
							{
								uisprite.gameObject.SetActive(true);
								UILabel uilabel = GameObjectUtil.FindChildGameObjectComponent<UILabel>(uisprite.gameObject, "ring");
								if (uilabel != null)
								{
									int num2 = RouletteManager.numJackpotRing;
									if (num2 <= 0)
									{
										num2 = 30000;
									}
									uilabel.text = HudUtility.GetFormatNumString<int>(num2);
								}
								break;
							}
							}
						}
					}
					goto IL_46B;
				}
				if (idType != ServerItem.IdType.CHARA && idType != ServerItem.IdType.CHAO)
				{
					base.transform.localScale = new Vector3(1f, 1f, 1f);
					if (this.m_item != null)
					{
						this.m_item.SetActive(true);
						UISprite uisprite4 = GameObjectUtil.FindChildGameObjectComponent<UISprite>(this.m_item, "main");
						uisprite4.spriteName = "ui_cmn_icon_item_" + cellItem.id;
					}
					if (this.m_common != null)
					{
						this.m_common.SetActive(true);
						UILabel uilabel2 = GameObjectUtil.FindChildGameObjectComponent<UILabel>(this.m_common, "num");
						uilabel2.text = "×" + num;
					}
					goto IL_46B;
				}
				if (this.m_egg != null)
				{
					this.m_egg.SetActive(true);
					UISprite uisprite5 = GameObjectUtil.FindChildGameObjectComponent<UISprite>(this.m_egg, "main");
					if (uisprite5 != null)
					{
						if (cellItem.idType == ServerItem.IdType.CHARA)
						{
							uisprite5.spriteName = "ui_roulette_chao_egg_100";
						}
						else
						{
							int id = (int)cellItem.id;
							int num3 = id / 1000 % 10;
							uisprite5.spriteName = "ui_roulette_chao_egg_" + num3;
						}
					}
				}
				goto IL_46B;
			}
			break;
		}
		base.transform.localScale = new Vector3(1f, 1f, 1f);
		if (this.m_item != null)
		{
			this.m_item.SetActive(true);
			UISprite uisprite6 = GameObjectUtil.FindChildGameObjectComponent<UISprite>(this.m_item, "main");
			uisprite6.spriteName = cellItem.serverItemSpriteNameRoulette;
		}
		if (this.m_common != null)
		{
			this.m_common.SetActive(true);
			UILabel uilabel3 = GameObjectUtil.FindChildGameObjectComponent<UILabel>(this.m_common, "num");
			uilabel3.text = "×" + num;
		}
		IL_46B:
		this.SetCampaign(cellItem.id);
	}

	// Token: 0x0400236F RID: 9071
	[SerializeField]
	private GameObject m_common;

	// Token: 0x04002370 RID: 9072
	[SerializeField]
	private GameObject m_egg;

	// Token: 0x04002371 RID: 9073
	[SerializeField]
	private GameObject m_item;

	// Token: 0x04002372 RID: 9074
	[SerializeField]
	private GameObject m_rank;

	// Token: 0x04002373 RID: 9075
	[SerializeField]
	private GameObject m_campaign;

	// Token: 0x04002374 RID: 9076
	[SerializeField]
	private List<GameObject> m_campaignList;

	// Token: 0x04002375 RID: 9077
	private RouletteBoard m_parent;

	// Token: 0x04002376 RID: 9078
	private int m_cellIndex;

	// Token: 0x04002377 RID: 9079
	private Vector3 m_basePos = new Vector3(0f, 0f, 0f);
}
