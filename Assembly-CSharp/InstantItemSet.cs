using System;
using System.Collections.Generic;
using SaveData;
using UnityEngine;

// Token: 0x02000418 RID: 1048
public class InstantItemSet : MonoBehaviour
{
	// Token: 0x06001F81 RID: 8065 RVA: 0x000BAE94 File Offset: 0x000B9094
	public List<BoostItemType> GetCheckedItemType()
	{
		List<BoostItemType> list = new List<BoostItemType>();
		for (int i = 0; i < 3; i++)
		{
			InstantItemButton instantItemButton = this.m_instantButtons[i];
			if (!(instantItemButton == null))
			{
				if (instantItemButton.IsChecked())
				{
					list.Add((BoostItemType)i);
				}
			}
		}
		return list;
	}

	// Token: 0x06001F82 RID: 8066 RVA: 0x000BAEE8 File Offset: 0x000B90E8
	public void Setup()
	{
		GameObject itemSetRootObject = ItemSetUtility.GetItemSetRootObject();
		this.m_window = GameObjectUtil.FindChildGameObjectComponent<InstantItemWindow>(itemSetRootObject, "info_pla");
		for (int i = 0; i < 3; i++)
		{
			InstantItemButton instantItemButton = GameObjectUtil.FindChildGameObjectComponent<InstantItemButton>(base.gameObject, ItemSetUtility.ButtonObjectName[i]);
			if (!(instantItemButton == null))
			{
				instantItemButton.Setup((BoostItemType)i, new InstantItemButton.ClickCallback(this.OnClickInstantButton));
				this.m_instantButtons[i] = instantItemButton;
			}
		}
		this.m_itemType = BoostItemType.SCORE_BONUS;
		this.m_window.SetWindowActive();
		this.m_window.SetInstantItemType(this.m_itemType);
		this.m_window.SetCheckMark(false);
	}

	// Token: 0x06001F83 RID: 8067 RVA: 0x000BAF90 File Offset: 0x000B9190
	private void Start()
	{
	}

	// Token: 0x06001F84 RID: 8068 RVA: 0x000BAF94 File Offset: 0x000B9194
	private void Update()
	{
	}

	// Token: 0x06001F85 RID: 8069 RVA: 0x000BAF98 File Offset: 0x000B9198
	private void OnEnable()
	{
		if (this.m_window)
		{
			this.m_window.SetInstantItemType(this.m_itemType);
		}
	}

	// Token: 0x06001F86 RID: 8070 RVA: 0x000BAFBC File Offset: 0x000B91BC
	public void ResetCheckMark()
	{
		StageInfo stageInfo = GameObjectUtil.FindGameObjectComponent<StageInfo>("StageInfo");
		if (stageInfo != null)
		{
			int num = stageInfo.BoostItemValid.Length;
			for (int i = 0; i < num; i++)
			{
				stageInfo.BoostItemValid[i] = false;
			}
		}
		if (this.m_window != null)
		{
			this.m_window.SetCheckMark(false);
		}
		for (int j = 0; j < 3; j++)
		{
			if (this.m_instantButtons[j] != null)
			{
				this.m_instantButtons[j].ResetCheckMark();
			}
		}
	}

	// Token: 0x06001F87 RID: 8071 RVA: 0x000BB054 File Offset: 0x000B9254
	public void SetupBoostedItem()
	{
		SaveDataManager instance = SaveDataManager.Instance;
		if (instance != null)
		{
			PlayerData playerData = instance.PlayerData;
			if (playerData != null)
			{
				for (int i = 0; i < 3; i++)
				{
					bool isChecked = playerData.BoostedItem[i];
					if (!(this.m_instantButtons[i] == null))
					{
						this.m_instantButtons[i].SetupBoostedItemButton(isChecked);
					}
				}
			}
		}
	}

	// Token: 0x06001F88 RID: 8072 RVA: 0x000BB0C4 File Offset: 0x000B92C4
	public void UpdateFreeItemList(ServerFreeItemState freeItemState)
	{
		List<ServerItemState> itemList = freeItemState.itemList;
		foreach (InstantItemButton instantItemButton in this.m_instantButtons)
		{
			if (!(instantItemButton == null))
			{
				for (int j = 0; j < itemList.Count; j++)
				{
					if (instantItemButton.boostItemType == itemList[j].GetItem().boostItemType)
					{
						instantItemButton.UpdateFreeItemCount(itemList[j].m_num);
					}
				}
			}
		}
	}

	// Token: 0x06001F89 RID: 8073 RVA: 0x000BB158 File Offset: 0x000B9358
	private void OnClickInstantButton(BoostItemType itemType, bool isChecked)
	{
		if (this.m_window == null)
		{
			return;
		}
		this.m_window.SetWindowActive();
		this.m_window.SetInstantItemType(itemType);
		this.m_window.SetCheckMark(isChecked);
		this.m_itemType = itemType;
		StageInfo stageInfo = GameObjectUtil.FindGameObjectComponent<StageInfo>("StageInfo");
		if (stageInfo != null)
		{
			stageInfo.BoostItemValid[(int)itemType] = isChecked;
		}
		SaveDataManager instance = SaveDataManager.Instance;
		if (instance != null)
		{
			PlayerData playerData = instance.PlayerData;
			if (playerData != null)
			{
				playerData.BoostedItem[(int)itemType] = isChecked;
			}
		}
	}

	// Token: 0x04001C84 RID: 7300
	private InstantItemButton[] m_instantButtons = new InstantItemButton[3];

	// Token: 0x04001C85 RID: 7301
	private InstantItemWindow m_window;

	// Token: 0x04001C86 RID: 7302
	private BoostItemType m_itemType;
}
