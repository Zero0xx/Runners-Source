using System;
using DataTable;
using Text;
using UnityEngine;

// Token: 0x02000442 RID: 1090
public class HudChaoPanel : MonoBehaviour
{
	// Token: 0x06002106 RID: 8454 RVA: 0x000C62BC File Offset: 0x000C44BC
	private void Start()
	{
		HudMenuUtility.SetTagHudSaveItem(base.gameObject);
		base.enabled = false;
	}

	// Token: 0x06002107 RID: 8455 RVA: 0x000C62D0 File Offset: 0x000C44D0
	private void OnDestroy()
	{
	}

	// Token: 0x06002108 RID: 8456 RVA: 0x000C62D4 File Offset: 0x000C44D4
	private void Initialize()
	{
		this.m_init_flag = true;
		GameObject mainMenuUIObject = HudMenuUtility.GetMainMenuUIObject();
		if (mainMenuUIObject != null)
		{
			GameObject gameObject = GameObjectUtil.FindChildGameObject(mainMenuUIObject, "2_Character");
			if (gameObject != null)
			{
				for (uint num = 0U; num < 4U; num += 1U)
				{
					Transform transform = gameObject.transform.FindChild(this.m_path_name[(int)((UIntPtr)num)]);
					if (transform != null)
					{
						this.m_data_obj[(int)((UIntPtr)num)] = transform.gameObject;
					}
				}
			}
		}
	}

	// Token: 0x06002109 RID: 8457 RVA: 0x000C6354 File Offset: 0x000C4554
	public void OnUpdateSaveDataDisplay()
	{
		if (!this.m_init_flag)
		{
			this.Initialize();
		}
		SaveDataManager instance = SaveDataManager.Instance;
		if (instance != null)
		{
			int mainChaoID = instance.PlayerData.MainChaoID;
			this.SetChaoImage(mainChaoID, this.m_data_obj[(int)((UIntPtr)0)], this.m_data_obj[(int)((UIntPtr)1)]);
			int subChaoID = instance.PlayerData.SubChaoID;
			this.SetChaoImage(subChaoID, this.m_data_obj[(int)((UIntPtr)2)], this.m_data_obj[(int)((UIntPtr)3)]);
		}
	}

	// Token: 0x0600210A RID: 8458 RVA: 0x000C63D0 File Offset: 0x000C45D0
	private void SetChaoImage(int chao_id, GameObject imageObj, GameObject silhouetteObj)
	{
		if (imageObj == null)
		{
			return;
		}
		if (silhouetteObj == null)
		{
			return;
		}
		if (chao_id >= 0)
		{
			imageObj.SetActive(true);
			silhouetteObj.SetActive(false);
			UITexture component = imageObj.GetComponent<UITexture>();
			if (component != null)
			{
				ChaoTextureManager.CallbackInfo info = new ChaoTextureManager.CallbackInfo(component, null, true);
				ChaoTextureManager.Instance.GetTexture(chao_id, info);
				component.enabled = true;
			}
		}
		else
		{
			silhouetteObj.SetActive(true);
			imageObj.SetActive(false);
		}
	}

	// Token: 0x0600210B RID: 8459 RVA: 0x000C6450 File Offset: 0x000C4650
	private void SetChaoName(int chao_id, GameObject obj)
	{
		if (obj != null)
		{
			UILabel component = obj.GetComponent<UILabel>();
			if (component != null)
			{
				if (chao_id >= 0)
				{
					string chaoText = TextUtility.GetChaoText("Chao", "name_for_menu_" + chao_id.ToString("D4"));
					if (chaoText != null)
					{
						component.text = chaoText;
					}
				}
				else
				{
					component.text = string.Empty;
				}
			}
		}
	}

	// Token: 0x0600210C RID: 8460 RVA: 0x000C64C4 File Offset: 0x000C46C4
	private void SetChaoBonusName(int chao_id, GameObject obj)
	{
		if (obj != null)
		{
			UILabel component = obj.GetComponent<UILabel>();
			if (component != null)
			{
				component.text = HudUtility.GetChaoMenuAbilityText(chao_id);
			}
		}
	}

	// Token: 0x0600210D RID: 8461 RVA: 0x000C64FC File Offset: 0x000C46FC
	private void SetChaoLevel(int chao_id, GameObject obj)
	{
		if (obj != null)
		{
			UILabel component = obj.GetComponent<UILabel>();
			if (component != null)
			{
				if (chao_id >= 0)
				{
					ChaoData chaoData = ChaoTable.GetChaoData(chao_id);
					if (chaoData != null)
					{
						int level = chaoData.level;
						if (level > -1)
						{
							obj.SetActive(true);
							component.text = TextUtility.GetTextLevel(level.ToString());
						}
					}
				}
				else
				{
					obj.SetActive(false);
				}
			}
		}
	}

	// Token: 0x0600210E RID: 8462 RVA: 0x000C6570 File Offset: 0x000C4770
	private void SetChaoTypeImage(int chao_id, GameObject obj)
	{
		if (obj != null)
		{
			if (chao_id >= 0)
			{
				obj.SetActive(true);
				UISprite component = obj.GetComponent<UISprite>();
				if (component != null)
				{
					ChaoData chaoData = ChaoTable.GetChaoData(chao_id);
					if (chaoData != null)
					{
						string str = chaoData.charaAtribute.ToString().ToLower();
						component.spriteName = "ui_chao_set_type_icon_" + str;
					}
				}
			}
			else
			{
				obj.SetActive(false);
			}
		}
	}

	// Token: 0x0600210F RID: 8463 RVA: 0x000C65EC File Offset: 0x000C47EC
	private void SetRareImage(int chao_id, GameObject obj)
	{
		if (obj != null)
		{
			obj.SetActive(true);
			UISprite component = obj.GetComponent<UISprite>();
			if (component != null)
			{
				if (chao_id >= 0)
				{
					ChaoData chaoData = ChaoTable.GetChaoData(chao_id);
					if (chaoData != null)
					{
						component.spriteName = "ui_chao_set_bg_ll_" + (int)chaoData.rarity;
					}
				}
				else
				{
					component.spriteName = "ui_chao_set_bg_ll_3";
				}
			}
		}
	}

	// Token: 0x06002110 RID: 8464 RVA: 0x000C6660 File Offset: 0x000C4860
	private void SetChaoTexture(GameObject obj, Texture tex)
	{
		if (obj != null)
		{
			obj.SetActive(true);
			UITexture component = obj.GetComponent<UITexture>();
			if (component != null)
			{
				component.enabled = (tex != null);
				if (tex != null)
				{
					component.mainTexture = tex;
				}
			}
		}
	}

	// Token: 0x06002111 RID: 8465 RVA: 0x000C66B4 File Offset: 0x000C48B4
	private void OnSetChaoTexture(ChaoTextureManager.TextureData data)
	{
		SaveDataManager instance = SaveDataManager.Instance;
		if (instance != null)
		{
			if (data.chao_id == instance.PlayerData.MainChaoID)
			{
				this.SetChaoTexture(this.m_data_obj[(int)((UIntPtr)0)], data.tex);
			}
			else if (data.chao_id == instance.PlayerData.SubChaoID)
			{
				this.SetChaoTexture(this.m_data_obj[(int)((UIntPtr)2)], data.tex);
			}
		}
	}

	// Token: 0x04001DC3 RID: 7619
	private string[] m_path_name = new string[]
	{
		"Btn_1_chao/img_chao_main",
		"Btn_1_chao/img_chao_main_default",
		"Btn_1_chao/img_chao_sub",
		"Btn_1_chao/img_chao_sub_default"
	};

	// Token: 0x04001DC4 RID: 7620
	private GameObject[] m_data_obj = new GameObject[4];

	// Token: 0x04001DC5 RID: 7621
	private bool m_init_flag;

	// Token: 0x02000443 RID: 1091
	private enum DataType
	{
		// Token: 0x04001DC7 RID: 7623
		MAIN_IMAGE,
		// Token: 0x04001DC8 RID: 7624
		MAIN_SILHOUETTE,
		// Token: 0x04001DC9 RID: 7625
		SUB_IMAGE,
		// Token: 0x04001DCA RID: 7626
		SUB_SILHOUETTE,
		// Token: 0x04001DCB RID: 7627
		NUM
	}
}
