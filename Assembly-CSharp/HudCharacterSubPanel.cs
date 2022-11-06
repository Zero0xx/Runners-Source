using System;
using UnityEngine;

// Token: 0x02000448 RID: 1096
public class HudCharacterSubPanel : MonoBehaviour
{
	// Token: 0x0600212A RID: 8490 RVA: 0x000C7868 File Offset: 0x000C5A68
	private void Start()
	{
		HudMenuUtility.SetTagHudSaveItem(base.gameObject);
		base.enabled = false;
	}

	// Token: 0x0600212B RID: 8491 RVA: 0x000C787C File Offset: 0x000C5A7C
	private void Initialize()
	{
		this.m_init_flag = true;
		GameObject mainMenuUIObject = HudMenuUtility.GetMainMenuUIObject();
		if (mainMenuUIObject != null)
		{
			for (uint num = 0U; num < 2U; num += 1U)
			{
				Transform transform = mainMenuUIObject.transform.FindChild("Anchor_5_MC/2_Character/Btn_2_player/" + this.m_path_name[(int)((UIntPtr)num)]);
				if (transform != null)
				{
					this.m_data_obj[(int)((UIntPtr)num)] = transform.gameObject;
				}
				else
				{
					this.m_init_flag = false;
				}
			}
		}
	}

	// Token: 0x0600212C RID: 8492 RVA: 0x000C78FC File Offset: 0x000C5AFC
	public void OnUpdateSaveDataDisplay()
	{
		if (!this.m_init_flag)
		{
			this.Initialize();
		}
		SaveDataManager instance = SaveDataManager.Instance;
		if (instance != null)
		{
			CharaType subChara = instance.PlayerData.SubChara;
			if (HudCharacterPanelUtil.CheckValidChara(subChara))
			{
				HudCharacterPanelUtil.SetGameObjectActive(this.m_data_obj[(int)((UIntPtr)0)], true);
				HudCharacterPanelUtil.SetGameObjectActive(this.m_data_obj[(int)((UIntPtr)1)], false);
				if (this.m_charaType != subChara)
				{
					this.m_charaType = subChara;
					if (this.m_textureRequest != null)
					{
					}
					UITexture component = this.m_data_obj[(int)((UIntPtr)0)].GetComponent<UITexture>();
					if (component != null)
					{
						this.m_textureRequest = new TextureRequestChara(subChara, component);
						TextureAsyncLoadManager.Instance.Request(this.m_textureRequest);
					}
				}
			}
			else
			{
				HudCharacterPanelUtil.SetGameObjectActive(this.m_data_obj[(int)((UIntPtr)1)], true);
				HudCharacterPanelUtil.SetGameObjectActive(this.m_data_obj[(int)((UIntPtr)0)], false);
			}
		}
	}

	// Token: 0x0600212D RID: 8493 RVA: 0x000C79DC File Offset: 0x000C5BDC
	private void SetCheckFlag(bool check_flag)
	{
		if (this.m_toggle != null)
		{
			this.m_toggle.value = check_flag;
		}
	}

	// Token: 0x04001DE6 RID: 7654
	private const string COMMON_PATH = "Anchor_5_MC/2_Character/Btn_2_player/";

	// Token: 0x04001DE7 RID: 7655
	private string[] m_path_name = new string[]
	{
		"img_player_sub",
		"img_player_sub_default"
	};

	// Token: 0x04001DE8 RID: 7656
	private GameObject[] m_data_obj = new GameObject[2];

	// Token: 0x04001DE9 RID: 7657
	private UIToggle m_toggle;

	// Token: 0x04001DEA RID: 7658
	private bool m_init_flag;

	// Token: 0x04001DEB RID: 7659
	private CharaType m_charaType = CharaType.UNKNOWN;

	// Token: 0x04001DEC RID: 7660
	private TextureRequestChara m_textureRequest;

	// Token: 0x02000449 RID: 1097
	private enum DataType
	{
		// Token: 0x04001DEE RID: 7662
		IMAGE,
		// Token: 0x04001DEF RID: 7663
		IMAGE_SILHOUETTE,
		// Token: 0x04001DF0 RID: 7664
		NUM
	}
}
