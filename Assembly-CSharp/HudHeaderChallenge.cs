using System;
using UnityEngine;

// Token: 0x0200044D RID: 1101
public class HudHeaderChallenge : MonoBehaviour
{
	// Token: 0x0600213A RID: 8506 RVA: 0x000C8024 File Offset: 0x000C6224
	private void Start()
	{
		HudMenuUtility.SetTagHudSaveItem(base.gameObject);
	}

	// Token: 0x0600213B RID: 8507 RVA: 0x000C8034 File Offset: 0x000C6234
	private void Update()
	{
		if (this.m_initEnd)
		{
			this.UpdateTimeCountDisplay();
		}
		else if (this.m_calledInit)
		{
			this.m_time -= Time.deltaTime;
			if (this.m_time < 0f)
			{
				this.Initialize();
			}
		}
	}

	// Token: 0x0600213C RID: 8508 RVA: 0x000C808C File Offset: 0x000C628C
	private void Initialize()
	{
		if (this.m_initEnd)
		{
			return;
		}
		if (this.m_energy_storage == null)
		{
			GameObject gameObject = GameObject.Find("EnergyStorage");
			if (gameObject != null)
			{
				this.m_energy_storage = gameObject.GetComponent<EnergyStorage>();
				if (this.m_energy_storage != null)
				{
					this.m_fill_up_flag = this.m_energy_storage.IsFillUpCount();
				}
			}
		}
		GameObject mainMenuCmnUIObject = HudMenuUtility.GetMainMenuCmnUIObject();
		if (mainMenuCmnUIObject != null)
		{
			GameObject gameObject2 = GameObjectUtil.FindChildGameObject(mainMenuCmnUIObject, "Anchor_3_TR");
			if (gameObject2 != null)
			{
				for (int i = 0; i < 3; i++)
				{
					if (this.m_ui_obj[i] == null)
					{
						this.m_ui_obj[i] = GameObjectUtil.FindChildGameObject(gameObject2, this.m_labelName[i]);
						if (this.m_ui_obj[i] != null)
						{
							this.m_ui_label[i] = this.m_ui_obj[i].GetComponent<UILabel>();
						}
					}
				}
				if (this.m_sale_obj == null)
				{
					this.m_sale_obj = GameObjectUtil.FindChildGameObject(gameObject2, "img_sale_icon_challenge");
				}
			}
		}
		this.m_initEnd = true;
		for (int j = 0; j < 3; j++)
		{
			if (this.m_ui_obj[j] == null)
			{
				this.m_initEnd = false;
				break;
			}
		}
		if (this.m_sale_obj == null)
		{
			this.m_initEnd = false;
		}
		if (this.m_energy_storage == null)
		{
			this.m_initEnd = false;
		}
		this.m_calledInit = true;
		this.m_time = 1f;
	}

	// Token: 0x0600213D RID: 8509 RVA: 0x000C822C File Offset: 0x000C642C
	private void SetChallengeCount()
	{
		uint num = 0U;
		SaveDataManager instance = SaveDataManager.Instance;
		if (instance != null)
		{
			num = instance.PlayerData.DisplayChallengeCount;
		}
		if (this.m_ui_label[0] != null)
		{
			this.m_ui_label[0].text = num.ToString();
		}
		if (this.m_ui_label[2])
		{
			this.m_ui_label[2].text = HudUtility.GetFormatNumString<uint>(num);
		}
	}

	// Token: 0x0600213E RID: 8510 RVA: 0x000C82A8 File Offset: 0x000C64A8
	private void SetLabelActive()
	{
		if (this.m_ui_obj[2] != null)
		{
			this.m_ui_obj[2].SetActive(this.m_fill_up_flag);
		}
		if (this.m_ui_obj[0] != null)
		{
			this.m_ui_obj[0].SetActive(!this.m_fill_up_flag);
		}
		if (this.m_ui_obj[1] != null)
		{
			this.m_ui_obj[1].SetActive(!this.m_fill_up_flag);
		}
	}

	// Token: 0x0600213F RID: 8511 RVA: 0x000C8330 File Offset: 0x000C6530
	private void UpdateTimeCountDisplay()
	{
		if (this.m_energy_storage != null)
		{
			this.m_fill_up_flag = this.m_energy_storage.IsFillUpCount();
			if (!this.m_fill_up_flag)
			{
				TimeSpan restTimeForRenew = this.m_energy_storage.GetRestTimeForRenew();
				if (this.m_ui_label[1] != null)
				{
					int num = restTimeForRenew.Seconds;
					int num2 = restTimeForRenew.Minutes;
					if (num < 0 && num2 <= 0)
					{
						num2 = 15;
						num = 0;
					}
					this.m_ui_label[1].text = string.Format("{0}:{1}", num2, num.ToString("D2"));
				}
			}
		}
	}

	// Token: 0x06002140 RID: 8512 RVA: 0x000C83D8 File Offset: 0x000C65D8
	public void OnUpdateSaveDataDisplay()
	{
		this.Initialize();
		if (this.m_energy_storage != null)
		{
			this.m_fill_up_flag = this.m_energy_storage.IsFillUpCount();
		}
		this.SetChallengeCount();
		this.SetLabelActive();
		this.UpdateTimeCountDisplay();
		if (this.m_sale_obj != null)
		{
			bool active = HudMenuUtility.IsSale(Constants.Campaign.emType.PurchaseAddEnergys);
			this.m_sale_obj.SetActive(active);
		}
	}

	// Token: 0x06002141 RID: 8513 RVA: 0x000C8444 File Offset: 0x000C6644
	public void OnUpdateChallengeCountDisply()
	{
		this.OnUpdateSaveDataDisplay();
	}

	// Token: 0x04001E03 RID: 7683
	private string[] m_labelName = new string[]
	{
		"Lbl_challenge",
		"Lbl_time",
		"Lbl_over_challenge"
	};

	// Token: 0x04001E04 RID: 7684
	private GameObject[] m_ui_obj = new GameObject[3];

	// Token: 0x04001E05 RID: 7685
	private UILabel[] m_ui_label = new UILabel[3];

	// Token: 0x04001E06 RID: 7686
	private EnergyStorage m_energy_storage;

	// Token: 0x04001E07 RID: 7687
	private float m_time;

	// Token: 0x04001E08 RID: 7688
	private bool m_fill_up_flag;

	// Token: 0x04001E09 RID: 7689
	private bool m_initEnd;

	// Token: 0x04001E0A RID: 7690
	private bool m_calledInit;

	// Token: 0x04001E0B RID: 7691
	private GameObject m_sale_obj;

	// Token: 0x0200044E RID: 1102
	private enum LabelType
	{
		// Token: 0x04001E0D RID: 7693
		CHALLENGE_COUNT,
		// Token: 0x04001E0E RID: 7694
		TIME_COUNT,
		// Token: 0x04001E0F RID: 7695
		OVER_CHALLENGE,
		// Token: 0x04001E10 RID: 7696
		NUM
	}
}
