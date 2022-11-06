using System;
using System.Collections.Generic;
using System.Diagnostics;
using App;
using Message;
using UnityEngine;

// Token: 0x0200030F RID: 783
[AddComponentMenu("Scripts/Runners/GameMode/Stage")]
public class StageItemManager : MonoBehaviour
{
	// Token: 0x17000371 RID: 881
	// (get) Token: 0x060016E2 RID: 5858 RVA: 0x00083C10 File Offset: 0x00081E10
	public static StageItemManager Instance
	{
		get
		{
			return StageItemManager.instance;
		}
	}

	// Token: 0x17000372 RID: 882
	// (get) Token: 0x060016E3 RID: 5859 RVA: 0x00083C18 File Offset: 0x00081E18
	public float CautionItemTimeRate
	{
		get
		{
			foreach (ItemType itemType in StageItemManager.m_cautionItemTypePriority)
			{
				float num = this.m_items_time[(int)itemType];
				float num2 = this.m_items_fullTime[(int)itemType];
				if (num > 0f && num2 > 0f)
				{
					return num / num2;
				}
			}
			return 0f;
		}
	}

	// Token: 0x17000373 RID: 883
	// (get) Token: 0x060016E4 RID: 5860 RVA: 0x00083C78 File Offset: 0x00081E78
	public float[] ItemsTime
	{
		get
		{
			return this.m_items_time;
		}
	}

	// Token: 0x060016E5 RID: 5861 RVA: 0x00083C80 File Offset: 0x00081E80
	private void Awake()
	{
		this.SetInstance();
	}

	// Token: 0x060016E6 RID: 5862 RVA: 0x00083C88 File Offset: 0x00081E88
	private void Start()
	{
		this.m_items_fullTime = new float[8];
		this.m_items_time = new float[8];
		this.m_items_paused = new bool[8];
		this.m_item_table = new ItemTable();
		this.m_availableItem = false;
		this.m_equipItems = new List<StageItemManager.EquippedItem>();
		this.m_busyItem = ItemType.UNKNOWN;
		this.m_nowPhantom = false;
		this.m_forcePhantomInvalidate = false;
		this.m_equipItemTutorial = false;
	}

	// Token: 0x060016E7 RID: 5863 RVA: 0x00083CF4 File Offset: 0x00081EF4
	private void OnDestroy()
	{
		if (StageItemManager.instance == this)
		{
			StageItemManager.instance = null;
		}
	}

	// Token: 0x060016E8 RID: 5864 RVA: 0x00083D0C File Offset: 0x00081F0C
	private void SetInstance()
	{
		if (StageItemManager.instance == null)
		{
			StageItemManager.instance = this;
		}
		else
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x060016E9 RID: 5865 RVA: 0x00083D40 File Offset: 0x00081F40
	private void UpdateStockItem(List<ItemType> itemList)
	{
		if (itemList.Count > 0)
		{
			if (itemList[0] != ItemType.UNKNOWN)
			{
				if (!this.m_nowPhantom && (this.m_phantomUseType != StageItemManager.PhantomUseType.DISABLE || Array.IndexOf<ItemType>(StageItemManager.m_phantomItemTypes, itemList[0]) < 0) && !this.m_playerInformation.IsDead() && this.IsAskEquipItemUsed(itemList[0]))
				{
					this.AddItem(itemList[0], false, false, null);
					itemList.RemoveAt(0);
				}
			}
			else
			{
				itemList.RemoveAt(0);
			}
		}
	}

	// Token: 0x060016EA RID: 5866 RVA: 0x00083DDC File Offset: 0x00081FDC
	private void Update()
	{
		if (this.m_levelInformation == null)
		{
			this.m_levelInformation = GameObjectUtil.FindGameObjectComponent<LevelInformation>("LevelInformation");
		}
		if (this.m_playerInformation == null)
		{
			this.m_playerInformation = GameObjectUtil.FindGameObjectComponent<PlayerInformation>("PlayerInformation");
		}
		if (this.m_phantomUseType == StageItemManager.PhantomUseType.ATTACK_PAUSE && this.m_levelInformation != null && this.m_levelInformation.NowBoss)
		{
			this.m_phantomUseType = StageItemManager.PhantomUseType.DISABLE;
		}
		this.CheckDisablePhantom();
		this.UpdateStockItem(this.m_stockColorItems);
		this.UpdateStockItem(this.m_stockItems);
		for (int i = 0; i < 8; i++)
		{
			if (this.m_items_time[i] > 0f && !this.m_items_paused[i])
			{
				if (i == 4)
				{
					if (this.m_niths_combo_time > Time.deltaTime)
					{
						this.m_niths_combo_time -= Time.deltaTime;
					}
					else
					{
						this.m_niths_combo_time = 0f;
					}
					if (this.m_item_combo_time > Time.deltaTime)
					{
						this.m_item_combo_time -= Time.deltaTime;
					}
					else
					{
						this.m_item_combo_time = 0f;
					}
				}
				if (this.m_items_time[i] > Time.deltaTime)
				{
					this.m_items_time[i] -= Time.deltaTime;
				}
				else
				{
					this.m_items_time[i] = 0f;
					this.TimeOutItem((ItemType)i);
				}
			}
		}
		if (this.IsActiveAltitudeTrampoline())
		{
			this.UpdateAltitudeTrampoline();
		}
	}

	// Token: 0x060016EB RID: 5867 RVA: 0x00083F6C File Offset: 0x0008216C
	private void CheckDisablePhantom()
	{
		if (this.m_disable_equipItem && this.m_playerInformation != null && this.m_playerInformation.PhantomType != PhantomType.NONE && !this.m_forcePhantomInvalidate)
		{
			ItemType phantomItemType = this.GetPhantomItemType();
			if (phantomItemType != ItemType.UNKNOWN)
			{
				this.m_items_paused[(int)phantomItemType] = true;
				MsgInvalidateItem value = new MsgInvalidateItem(this.GetPhantomItemType());
				GameObjectUtil.SendMessageToTagObjects("Player", "OnInvalidateItem", value, SendMessageOptions.DontRequireReceiver);
				GameObjectUtil.SendMessageFindGameObject("HudCockpit", "OnPauseItemOnBoss", null, SendMessageOptions.DontRequireReceiver);
				this.m_forcePhantomInvalidate = true;
			}
		}
	}

	// Token: 0x060016EC RID: 5868 RVA: 0x00084000 File Offset: 0x00082200
	private GameObject GetPlayerObject()
	{
		return GameObject.FindWithTag("Player");
	}

	// Token: 0x060016ED RID: 5869 RVA: 0x0008400C File Offset: 0x0008220C
	public int GetComboScoreRate()
	{
		int num = 1;
		if (this.m_item_combo_time > 0f)
		{
			num = 2;
		}
		if (this.m_niths_combo_time > 0f)
		{
			num *= (int)this.m_chaoAblityComboUpRate;
		}
		return Mathf.Min(num, 10);
	}

	// Token: 0x060016EE RID: 5870 RVA: 0x00084054 File Offset: 0x00082254
	public ItemTable GetItemTable()
	{
		return this.m_item_table;
	}

	// Token: 0x060016EF RID: 5871 RVA: 0x0008405C File Offset: 0x0008225C
	public void OnRequestItemUse(MsgAskEquipItemUsed msg)
	{
		bool flag = false;
		int num = -1;
		for (int i = 0; i < this.m_equipItems.Count; i++)
		{
			if (this.m_equipItems[i].item == msg.m_itemType)
			{
				num = i;
				if (!this.m_equipItems[i].revivedFlag && StageAbilityManager.Instance != null && StageAbilityManager.Instance.HasChaoAbility(ChaoAbility.ITEM_REVIVE))
				{
					int num2 = (int)StageAbilityManager.Instance.GetChaoAbilityValue(ChaoAbility.ITEM_REVIVE);
					if (num2 >= ObjUtil.GetRandomRange100())
					{
						flag = true;
						this.m_equipItems[i].revivedFlag = true;
					}
				}
				break;
			}
		}
		if (num >= 0)
		{
			if (this.m_equipItems[num].item == ItemType.UNKNOWN)
			{
				this.m_equipItems.RemoveRange(num, 1);
			}
			else
			{
				msg.m_ok = this.IsAskEquipItemUsed(this.m_equipItems[num].item);
				if (msg.m_ok)
				{
					this.AddItem(this.m_equipItems[num].item, true, flag, null);
					if (flag)
					{
						ObjUtil.RequestStartAbilityToChao(ChaoAbility.ITEM_REVIVE, false);
					}
					else
					{
						this.m_equipItems.RemoveRange(num, 1);
					}
				}
			}
		}
	}

	// Token: 0x060016F0 RID: 5872 RVA: 0x000841A4 File Offset: 0x000823A4
	public void OnAddItem(MsgAddItemToManager msg)
	{
		if (!this.IsAskEquipItemUsed(msg.m_itemType))
		{
			return;
		}
		this.AddItem(msg.m_itemType, false, false, null);
	}

	// Token: 0x060016F1 RID: 5873 RVA: 0x000841C8 File Offset: 0x000823C8
	public void OnAddEquipItem()
	{
		if (this.m_playerInformation != null)
		{
			if (this.m_playerInformation.IsDead())
			{
				return;
			}
			if (this.m_playerInformation.IsNowLastChance())
			{
				return;
			}
		}
		if (this.m_equipItems.Count < 3)
		{
			List<ItemType> list = new List<ItemType>();
			for (int i = 0; i < 8; i++)
			{
				bool flag = false;
				for (int j = 0; j < this.m_equipItems.Count; j++)
				{
					if (i == (int)this.m_equipItems[j].item)
					{
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					list.Add((ItemType)i);
				}
			}
			if (list.Count > 1)
			{
				ItemType item = list[UnityEngine.Random.Range(0, list.Count)];
				StageItemManager.EquippedItem equippedItem = new StageItemManager.EquippedItem();
				equippedItem.item = item;
				equippedItem.index = this.m_equipItems.Count;
				this.m_equipItems.Add(equippedItem);
				ItemType[] array = new ItemType[this.m_equipItems.Count];
				for (int k = 0; k < this.m_equipItems.Count; k++)
				{
					array[k] = this.m_equipItems[k].item;
				}
				MsgSetEquippedItem msgSetEquippedItem = new MsgSetEquippedItem(array);
				bool flag2 = false;
				if (Array.IndexOf<ItemType>(StageItemManager.m_phantomItemTypes, this.m_busyItem) >= 0)
				{
					flag2 = true;
				}
				msgSetEquippedItem.m_enabled = (!this.m_disable_equipItem && !flag2);
				GameObjectUtil.SendMessageFindGameObject("HudCockpit", "OnSetPresentEquippedItem", msgSetEquippedItem, SendMessageOptions.DontRequireReceiver);
			}
		}
	}

	// Token: 0x060016F2 RID: 5874 RVA: 0x00084364 File Offset: 0x00082564
	public void OnChangeItem()
	{
		int count = this.m_equipItems.Count;
		if (count > 0)
		{
			this.m_displayEquipItems.Clear();
			App.Random.ShuffleInt(this.m_itemChangeTable);
			for (int i = 0; i < count; i++)
			{
				this.m_equipItems[i].item = (ItemType)this.m_itemChangeTable[i];
				this.m_displayEquipItems.Add(this.m_equipItems[i].item);
			}
			GameObjectUtil.SendMessageFindGameObject("HudCockpit", "OnChangeItem", new MsgSetEquippedItem(this.m_displayEquipItems.ToArray()), SendMessageOptions.DontRequireReceiver);
		}
	}

	// Token: 0x060016F3 RID: 5875 RVA: 0x00084404 File Offset: 0x00082604
	public void OnAddColorItem(MsgAddItemToManager msg)
	{
		if (this.IsAskEquipItemUsed(msg.m_itemType))
		{
			this.AddItem(msg.m_itemType, false, false, null);
		}
		else
		{
			ItemType phantomItemType = this.GetPhantomItemType();
			if (phantomItemType == msg.m_itemType)
			{
				float itemTimeFromChara = StageItemManager.GetItemTimeFromChara(phantomItemType);
				this.m_items_time[(int)phantomItemType] = Mathf.Max(this.m_items_time[(int)phantomItemType], itemTimeFromChara);
				this.m_items_fullTime[(int)phantomItemType] = this.m_items_time[(int)phantomItemType];
				this.CountdownMeter();
			}
			else if (this.IsAskItemStock(msg.m_itemType))
			{
				this.m_stockColorItems.Add(msg.m_itemType);
			}
		}
	}

	// Token: 0x060016F4 RID: 5876 RVA: 0x000844A4 File Offset: 0x000826A4
	public void OnAddDamageTrampoline()
	{
		if (this.IsAskEquipItemUsed(ItemType.TRAMPOLINE))
		{
			this.AddItem(ItemType.TRAMPOLINE, false, false, null);
		}
		else if (this.m_items_time[3] > 0f)
		{
			float itemTimeFromChara = StageItemManager.GetItemTimeFromChara(ItemType.TRAMPOLINE);
			this.m_items_time[3] = Mathf.Max(this.m_items_time[3], itemTimeFromChara);
			this.m_items_fullTime[3] = this.m_items_time[3];
			this.CountdownMeter();
		}
		else if (this.IsAskItemStock(ItemType.TRAMPOLINE))
		{
			this.m_stockItems.Add(ItemType.TRAMPOLINE);
		}
	}

	// Token: 0x060016F5 RID: 5877 RVA: 0x00084530 File Offset: 0x00082730
	public bool IsEquipItem()
	{
		return this.m_equipItems.Count > 0;
	}

	// Token: 0x060016F6 RID: 5878 RVA: 0x00084540 File Offset: 0x00082740
	public bool IsActiveTrampoline()
	{
		return this.m_activeTrampoline;
	}

	// Token: 0x060016F7 RID: 5879 RVA: 0x00084548 File Offset: 0x00082748
	private bool IsActiveAltitudeTrampoline()
	{
		return this.m_activeAltitudeTrampoline;
	}

	// Token: 0x060016F8 RID: 5880 RVA: 0x00084550 File Offset: 0x00082750
	public void SetActiveAltitudeTrampoline(bool on)
	{
		this.m_activeAltitudeTrampoline = on;
	}

	// Token: 0x060016F9 RID: 5881 RVA: 0x0008455C File Offset: 0x0008275C
	public static float GetItemTimeFromChara(ItemType itemType)
	{
		StageAbilityManager stageAbilityManager = StageAbilityManager.Instance;
		switch (itemType)
		{
		case ItemType.INVINCIBLE:
		case ItemType.MAGNET:
		case ItemType.TRAMPOLINE:
		case ItemType.COMBO:
			return (!(stageAbilityManager != null)) ? 3f : stageAbilityManager.GetItemTimePlusAblityBonus(itemType);
		case ItemType.BARRIER:
			return 0f;
		case ItemType.LASER:
		case ItemType.DRILL:
		case ItemType.ASTEROID:
			return (!(stageAbilityManager != null)) ? 6f : stageAbilityManager.GetItemTimePlusAblityBonus(itemType);
		default:
			return 0f;
		}
	}

	// Token: 0x060016FA RID: 5882 RVA: 0x000845E4 File Offset: 0x000827E4
	public void SetEquipItemTutorial(bool equipItemTutorial)
	{
		this.m_equipItemTutorial = equipItemTutorial;
	}

	// Token: 0x060016FB RID: 5883 RVA: 0x000845F0 File Offset: 0x000827F0
	public void SetEquippedItem(ItemType[] items)
	{
		if (items == null)
		{
			return;
		}
		if (items.Length == 0)
		{
			return;
		}
		for (int i = 0; i < items.Length; i++)
		{
			this.m_displayEquipItems.Add(items[i]);
		}
		GameObjectUtil.SendMessageFindGameObject("HudCockpit", "OnSetEquippedItem", new MsgSetEquippedItem(this.m_displayEquipItems.ToArray()), SendMessageOptions.DontRequireReceiver);
	}

	// Token: 0x060016FC RID: 5884 RVA: 0x00084650 File Offset: 0x00082850
	private void AddItem(ItemType itemType, bool equipped, bool revive = false, StageItemManager.ChaoAbilityOption option = null)
	{
		if (option != null)
		{
			if (itemType == ItemType.MAGNET)
			{
				float num = Mathf.Max(this.m_items_time[(int)itemType], 0f);
				this.m_items_time[(int)itemType] = num + option.m_specifiedTime;
			}
			else if (itemType == ItemType.COMBO)
			{
				this.m_niths_combo_time = option.m_specifiedTime;
				this.m_items_time[(int)itemType] = Mathf.Max(this.m_item_combo_time, this.m_niths_combo_time);
			}
		}
		else if (itemType == ItemType.COMBO)
		{
			this.m_item_combo_time = StageItemManager.GetItemTimeFromChara(itemType);
			this.m_items_time[(int)itemType] = Mathf.Max(this.m_item_combo_time, this.m_niths_combo_time);
		}
		else
		{
			float itemTimeFromChara = StageItemManager.GetItemTimeFromChara(itemType);
			this.m_items_time[(int)itemType] = Mathf.Max(this.m_items_time[(int)itemType], itemTimeFromChara);
		}
		this.m_items_paused[(int)itemType] = false;
		switch (itemType)
		{
		case ItemType.INVINCIBLE:
		case ItemType.MAGNET:
		case ItemType.LASER:
		case ItemType.DRILL:
		case ItemType.ASTEROID:
		{
			GameObject playerObject = this.GetPlayerObject();
			if (playerObject != null)
			{
				MsgUseItem value = new MsgUseItem(itemType, -1f);
				playerObject.SendMessage("OnUseItem", value, SendMessageOptions.DontRequireReceiver);
			}
			break;
		}
		case ItemType.BARRIER:
		{
			GameObject playerObject2 = this.GetPlayerObject();
			if (playerObject2 != null)
			{
				MsgUseItem value2 = new MsgUseItem(itemType, -1f);
				playerObject2.SendMessage("OnUseItem", value2, SendMessageOptions.DontRequireReceiver);
			}
			break;
		}
		case ItemType.TRAMPOLINE:
		{
			GameObject[] array = GameObject.FindGameObjectsWithTag("Gimmick");
			MsgUseItem value3 = new MsgUseItem(itemType);
			foreach (GameObject gameObject in array)
			{
				gameObject.SendMessage("OnUseItem", value3, SendMessageOptions.DontRequireReceiver);
			}
			GameObject playerObject3 = this.GetPlayerObject();
			if (playerObject3 != null)
			{
				MsgUseItem value4 = new MsgUseItem(itemType, -1f);
				playerObject3.SendMessage("OnUseItem", value4, SendMessageOptions.DontRequireReceiver);
			}
			ObjUtil.RequestStartAbilityToChao(ChaoAbility.TRAMPOLINE_TIME, true);
			ObjUtil.RequestStartAbilityToChao(ChaoAbility.ITEM_TIME, true);
			this.m_activeTrampoline = true;
			break;
		}
		case ItemType.COMBO:
		{
			GameObject playerObject4 = this.GetPlayerObject();
			if (playerObject4 != null)
			{
				MsgUseItem value5 = new MsgUseItem(itemType, -1f);
				playerObject4.SendMessage("OnUseItem", value5, SendMessageOptions.DontRequireReceiver);
			}
			break;
		}
		}
		if (equipped && !revive)
		{
			StageItemManager.SendUsedItemMessageToCockpit(itemType);
		}
		if (itemType == ItemType.BARRIER)
		{
			itemType = ItemType.UNKNOWN;
		}
		if (itemType < ItemType.NUM)
		{
			this.m_items_fullTime[(int)itemType] = this.m_items_time[(int)itemType];
		}
		this.SetupBusyItem();
		HudTutorial.SendItemTutorial(itemType);
	}

	// Token: 0x060016FD RID: 5885 RVA: 0x000848C0 File Offset: 0x00082AC0
	private void SetupBusyItem()
	{
		ItemType busyItem = ItemType.UNKNOWN;
		foreach (ItemType itemType in StageItemManager.m_cautionItemTypePriority)
		{
			float num = this.m_items_time[(int)itemType];
			float num2 = this.m_items_fullTime[(int)itemType];
			if (num > 0f && num2 > 0f)
			{
				busyItem = itemType;
				break;
			}
		}
		this.m_busyItem = busyItem;
		this.CountdownMeter();
	}

	// Token: 0x060016FE RID: 5886 RVA: 0x00084930 File Offset: 0x00082B30
	private void CountdownMeter()
	{
		MsgCaution caution = new MsgCaution(HudCaution.Type.COUNTDOWN, this.CautionItemTimeRate);
		HudCaution.Instance.SetCaution(caution);
	}

	// Token: 0x060016FF RID: 5887 RVA: 0x00084958 File Offset: 0x00082B58
	private void TimeOutItem(ItemType itemType)
	{
		switch (itemType)
		{
		case ItemType.INVINCIBLE:
		case ItemType.MAGNET:
		case ItemType.COMBO:
		case ItemType.LASER:
		case ItemType.DRILL:
		case ItemType.ASTEROID:
		{
			GameObject playerObject = this.GetPlayerObject();
			if (playerObject != null)
			{
				MsgInvalidateItem value = new MsgInvalidateItem(itemType);
				playerObject.SendMessage("OnInvalidateItem", value, SendMessageOptions.DontRequireReceiver);
			}
			break;
		}
		case ItemType.TRAMPOLINE:
		{
			GameObject[] array = GameObject.FindGameObjectsWithTag("Gimmick");
			MsgInvalidateItem value2 = new MsgInvalidateItem(itemType);
			foreach (GameObject gameObject in array)
			{
				gameObject.SendMessage("OnInvalidateItem", value2, SendMessageOptions.DontRequireReceiver);
			}
			GameObject playerObject2 = this.GetPlayerObject();
			if (playerObject2 != null)
			{
				playerObject2.SendMessage("OnInvalidateItem", value2, SendMessageOptions.DontRequireReceiver);
			}
			this.m_activeTrampoline = false;
			break;
		}
		}
		this.InvalidateItem(itemType);
	}

	// Token: 0x06001700 RID: 5888 RVA: 0x00084A40 File Offset: 0x00082C40
	private void ResetItemTime(ItemType itemType)
	{
		this.m_items_time[(int)itemType] = 0f;
		if (itemType == ItemType.TRAMPOLINE)
		{
			this.m_activeTrampoline = false;
		}
	}

	// Token: 0x06001701 RID: 5889 RVA: 0x00084A60 File Offset: 0x00082C60
	private void OnInvalidateItem(MsgInvalidateItem msg)
	{
		if (this.m_items_time[(int)msg.m_itemType] > 0f)
		{
			this.ResetItemTime(msg.m_itemType);
			this.m_items_paused[(int)msg.m_itemType] = false;
			this.InvalidateItem(msg.m_itemType);
		}
	}

	// Token: 0x06001702 RID: 5890 RVA: 0x00084AA0 File Offset: 0x00082CA0
	public void OnTerminateItem(MsgTerminateItem msg)
	{
		if (this.m_items_paused[(int)msg.m_itemType])
		{
			return;
		}
		if (this.m_items_time[(int)msg.m_itemType] > 0f)
		{
			this.ResetItemTime(msg.m_itemType);
			this.InvalidateItem(msg.m_itemType);
		}
	}

	// Token: 0x06001703 RID: 5891 RVA: 0x00084AF0 File Offset: 0x00082CF0
	public void OnPauseItemOnBoss(MsgPauseItemOnBoss msg)
	{
		this.m_disable_equipItem = true;
		this.m_forcePhantomInvalidate = false;
		for (int i = 0; i < 8; i++)
		{
			ItemType itemType = (ItemType)i;
			if (!this.IsBossNoPauseItem(itemType))
			{
				float num = this.m_items_time[i];
				if (num > 0f)
				{
					if (this.m_items_time[i] < 2f)
					{
						this.ResetItemTime(itemType);
						this.InvalidateItem(itemType);
						MsgInvalidateItem value = new MsgInvalidateItem(itemType);
						GameObjectUtil.SendMessageToTagObjects("Player", "OnInvalidateItem", value, SendMessageOptions.DontRequireReceiver);
					}
					else
					{
						this.m_items_paused[i] = true;
						MsgInvalidateItem value2 = new MsgInvalidateItem(itemType);
						GameObjectUtil.SendMessageToTagObjects("Player", "OnInvalidateItem", value2, SendMessageOptions.DontRequireReceiver);
					}
				}
			}
		}
		GameObjectUtil.SendMessageFindGameObject("HudCockpit", "OnPauseItemOnBoss", null, SendMessageOptions.DontRequireReceiver);
	}

	// Token: 0x06001704 RID: 5892 RVA: 0x00084BB4 File Offset: 0x00082DB4
	public void OnPauseItemOnChangeLevel(MsgPauseItemOnChageLevel msg)
	{
		for (int i = 0; i < 8; i++)
		{
			ItemType itemType = (ItemType)i;
			if (this.IsBossNoPauseItem(itemType))
			{
				float num = this.m_items_time[i];
				if (num > 0f)
				{
					if (this.m_items_time[i] < 2f)
					{
						this.ResetItemTime(itemType);
						this.InvalidateItem(itemType);
						MsgInvalidateItem value = new MsgInvalidateItem(itemType);
						GameObjectUtil.SendMessageToTagObjects("Player", "OnInvalidateItem", value, SendMessageOptions.DontRequireReceiver);
					}
					else
					{
						this.m_items_paused[i] = true;
						MsgInvalidateItem value2 = new MsgInvalidateItem(itemType);
						GameObjectUtil.SendMessageToTagObjects("Player", "OnInvalidateItem", value2, SendMessageOptions.DontRequireReceiver);
					}
				}
			}
		}
	}

	// Token: 0x06001705 RID: 5893 RVA: 0x00084C58 File Offset: 0x00082E58
	public void OnResumeItemOnBoss(MsgPhatomItemOnBoss msg)
	{
		bool flag = false;
		for (int i = 0; i < 8; i++)
		{
			ItemType itemType = (ItemType)i;
			float num = this.m_items_time[i];
			if (num > 0f && this.m_items_paused[i])
			{
				if (itemType == ItemType.ASTEROID || itemType == ItemType.LASER || itemType == ItemType.DRILL)
				{
					flag = true;
				}
				this.m_items_paused[i] = false;
				MsgUseItem value = new MsgUseItem(itemType, -1f);
				GameObjectUtil.SendMessageToTagObjects("Player", "OnUseItem", value, SendMessageOptions.DontRequireReceiver);
			}
		}
		this.m_disable_equipItem = false;
		this.m_phantomUseType = StageItemManager.PhantomUseType.ATTACK_PAUSE;
		GameObjectUtil.SendMessageFindGameObject("HudCockpit", "OnResumeItemOnBoss", flag, SendMessageOptions.DontRequireReceiver);
	}

	// Token: 0x06001706 RID: 5894 RVA: 0x00084D00 File Offset: 0x00082F00
	public void OnDisableEquipItem(MsgDisableEquipItem msg)
	{
		this.m_disable_equipItem = msg.m_disable;
	}

	// Token: 0x06001707 RID: 5895 RVA: 0x00084D10 File Offset: 0x00082F10
	private bool IsBossNoPauseItem(ItemType itemType)
	{
		return Array.IndexOf<ItemType>(StageItemManager.m_feverBossNoPauseItemTypes, itemType) >= 0;
	}

	// Token: 0x06001708 RID: 5896 RVA: 0x00084D28 File Offset: 0x00082F28
	private void InvalidateItem(ItemType itemType)
	{
		bool flag = false;
		foreach (StageItemManager.EquippedItem equippedItem in this.m_equipItems)
		{
			if (equippedItem.item == itemType)
			{
				flag = true;
				break;
			}
		}
		if (!flag)
		{
			StageItemManager.SendUsedItemMessageToCockpit(itemType);
		}
		this.SetupBusyItem();
	}

	// Token: 0x06001709 RID: 5897 RVA: 0x00084DB0 File Offset: 0x00082FB0
	public void OnUseEquipItem(MsgUseEquipItem msg)
	{
		if (this.m_availableItem)
		{
			return;
		}
		this.m_availableItem = true;
		this.m_equipItems.Clear();
		if (this.m_displayEquipItems.Count == 0)
		{
			return;
		}
		foreach (ItemType itemType in this.m_displayEquipItems)
		{
			if (itemType != ItemType.UNKNOWN)
			{
				StageItemManager.EquippedItem equippedItem = new StageItemManager.EquippedItem();
				equippedItem.item = itemType;
				this.m_equipItems.Add(equippedItem);
			}
		}
		if (this.m_equipItems.Count == 0)
		{
			return;
		}
		int num = 0;
		foreach (StageItemManager.EquippedItem equippedItem2 in this.m_equipItems)
		{
			equippedItem2.index = num;
			num++;
		}
		if (this.m_equipItemTutorial)
		{
			this.SendItemBtnTutorial();
		}
		if (this.m_levelInformation != null && !this.m_levelInformation.NowBoss)
		{
			MsgItemButtonEnable value = new MsgItemButtonEnable(true);
			GameObjectUtil.SendMessageFindGameObject("HudCockpit", "OnItemEnable", value, SendMessageOptions.DontRequireReceiver);
		}
	}

	// Token: 0x0600170A RID: 5898 RVA: 0x00084F1C File Offset: 0x0008311C
	private void SendItemBtnTutorial()
	{
		GameObjectUtil.SendMessageFindGameObject("GameModeStage", "OnMsgTutorialItemButton", new MsgTutorialItemButton(), SendMessageOptions.DontRequireReceiver);
		GameObjectUtil.SendMessageFindGameObject("HudCockpit", "OnStartTutorial", null, SendMessageOptions.DontRequireReceiver);
	}

	// Token: 0x0600170B RID: 5899 RVA: 0x00084F54 File Offset: 0x00083154
	public void OnMsgBossCheckState(MsgBossCheckState msg)
	{
		bool flag = msg.IsAttackOK();
		this.m_phantomUseType = ((!flag) ? StageItemManager.PhantomUseType.DISABLE : StageItemManager.PhantomUseType.ENABLE);
		if (this.m_levelInformation != null && this.m_levelInformation.NowBoss)
		{
			MsgItemButtonEnable value = new MsgItemButtonEnable(flag);
			GameObjectUtil.SendMessageFindGameObject("HudCockpit", "OnItemEnable", value, SendMessageOptions.DontRequireReceiver);
		}
		if (this.m_bossItemTutorial && flag)
		{
			this.m_bossItemTutorial = false;
			this.SendItemBtnTutorial();
		}
	}

	// Token: 0x0600170C RID: 5900 RVA: 0x00084FD4 File Offset: 0x000831D4
	private static void SendUsedItemMessageToCockpit(ItemType itemType)
	{
		GameObjectUtil.SendMessageFindGameObject("HudCockpit", "OnUsedItem", new MsgUsedItem(itemType), SendMessageOptions.DontRequireReceiver);
	}

	// Token: 0x0600170D RID: 5901 RVA: 0x00084FF0 File Offset: 0x000831F0
	public void OnAskEquipItemUsed(MsgAskEquipItemUsed msg)
	{
		msg.m_ok = this.IsAskEquipItemUsed(msg.m_itemType);
	}

	// Token: 0x0600170E RID: 5902 RVA: 0x00085004 File Offset: 0x00083204
	public ItemType GetPhantomItemType()
	{
		if (this.m_items_time[5] > 0f)
		{
			return ItemType.LASER;
		}
		if (this.m_items_time[6] > 0f)
		{
			return ItemType.DRILL;
		}
		if (this.m_items_time[7] > 0f)
		{
			return ItemType.ASTEROID;
		}
		return ItemType.UNKNOWN;
	}

	// Token: 0x0600170F RID: 5903 RVA: 0x00085044 File Offset: 0x00083244
	private bool IsAskEquipItemUsed(ItemType itemType)
	{
		if (this.m_playerInformation != null)
		{
			if (this.m_playerInformation.IsDead())
			{
				return false;
			}
			if (this.m_playerInformation.IsNowLastChance())
			{
				return false;
			}
		}
		if (this.m_characterChange)
		{
			return false;
		}
		if (!this.m_availableItem)
		{
			return false;
		}
		if (itemType >= ItemType.NUM)
		{
			return false;
		}
		if (this.m_items_paused[(int)itemType])
		{
			return false;
		}
		if (this.m_disable_equipItem && !this.IsBossNoPauseItem(itemType))
		{
			return false;
		}
		bool flag = false;
		if (Array.IndexOf<ItemType>(StageItemManager.m_phantomItemTypes, this.m_busyItem) >= 0)
		{
			flag = true;
		}
		switch (itemType)
		{
		case ItemType.INVINCIBLE:
			if (flag)
			{
				return false;
			}
			break;
		case ItemType.LASER:
		case ItemType.DRILL:
		case ItemType.ASTEROID:
			if (flag && this.m_busyItem != itemType)
			{
				return false;
			}
			if (this.m_phantomUseType == StageItemManager.PhantomUseType.DISABLE)
			{
				return false;
			}
			break;
		}
		return true;
	}

	// Token: 0x06001710 RID: 5904 RVA: 0x00085154 File Offset: 0x00083354
	private bool IsAskItemStock(ItemType itemType)
	{
		if (this.m_playerInformation != null)
		{
			if (this.m_playerInformation.IsDead())
			{
				return false;
			}
			if (this.m_playerInformation.IsNowLastChance())
			{
				return false;
			}
		}
		return !this.m_characterChange && this.m_availableItem && itemType < ItemType.NUM;
	}

	// Token: 0x06001711 RID: 5905 RVA: 0x000851BC File Offset: 0x000833BC
	private void UpdateAltitudeTrampoline()
	{
		if (this.m_playerInformation != null)
		{
			float y = this.m_playerInformation.SideViewPathPos.y;
			float y2 = this.m_playerInformation.Position.y;
			if (y + 6f < y2)
			{
				this.m_Altitude = true;
			}
			else
			{
				if (this.m_Altitude)
				{
					GameObject[] array = GameObject.FindGameObjectsWithTag("Gimmick");
					foreach (GameObject gameObject in array)
					{
						MsgUseItem value = new MsgUseItem(ItemType.TRAMPOLINE);
						gameObject.SendMessage("OnUseItem", value, SendMessageOptions.DontRequireReceiver);
					}
				}
				this.m_Altitude = false;
			}
		}
	}

	// Token: 0x06001712 RID: 5906 RVA: 0x00085274 File Offset: 0x00083474
	public void OnTransformPhantom(MsgTransformPhantom msg)
	{
		this.m_nowPhantom = true;
	}

	// Token: 0x06001713 RID: 5907 RVA: 0x00085280 File Offset: 0x00083480
	public void OnReturnFromPhantom(MsgReturnFromPhantom msg)
	{
		this.m_nowPhantom = false;
	}

	// Token: 0x06001714 RID: 5908 RVA: 0x0008528C File Offset: 0x0008348C
	public void OnChangeCharaStart(MsgChangeCharaSucceed msg)
	{
		this.m_characterChange = true;
	}

	// Token: 0x06001715 RID: 5909 RVA: 0x00085298 File Offset: 0x00083498
	private void OnChangeCharaSucceed(MsgChangeCharaSucceed msg)
	{
		this.m_characterChange = false;
	}

	// Token: 0x06001716 RID: 5910 RVA: 0x000852A4 File Offset: 0x000834A4
	private void OnChaoAbilityLoopComboUp()
	{
		StageAbilityManager stageAbilityManager = StageAbilityManager.Instance;
		if (stageAbilityManager != null && this.IsAskEquipItemUsed(ItemType.COMBO))
		{
			float num = stageAbilityManager.GetChaoAbilityValue(ChaoAbility.LOOP_COMBO_UP);
			num = Mathf.Max(num, 1f);
			StageItemManager.ChaoAbilityOption chaoAbilityOption = new StageItemManager.ChaoAbilityOption();
			chaoAbilityOption.m_specifiedTime = num;
			this.m_chaoAblityComboUpRate = stageAbilityManager.GetChaoAbilityExtraValue(ChaoAbility.LOOP_COMBO_UP, ChaoType.MAIN);
			this.m_chaoAblityComboUpRate += stageAbilityManager.GetChaoAbilityExtraValue(ChaoAbility.LOOP_COMBO_UP, ChaoType.SUB);
			this.AddItem(ItemType.COMBO, false, false, chaoAbilityOption);
		}
	}

	// Token: 0x06001717 RID: 5911 RVA: 0x00085320 File Offset: 0x00083520
	private void OnChaoAbilityLoopMagnet()
	{
		StageAbilityManager stageAbilityManager = StageAbilityManager.Instance;
		if (stageAbilityManager != null && this.IsAskEquipItemUsed(ItemType.MAGNET))
		{
			float num = stageAbilityManager.GetChaoAbilityValue(ChaoAbility.LOOP_MAGNET);
			num = Mathf.Max(num, 1f);
			this.AddItem(ItemType.MAGNET, false, false, new StageItemManager.ChaoAbilityOption
			{
				m_specifiedTime = num
			});
		}
	}

	// Token: 0x06001718 RID: 5912 RVA: 0x00085378 File Offset: 0x00083578
	private void OnMsgExitStage(MsgExitStage msg)
	{
		base.enabled = false;
	}

	// Token: 0x06001719 RID: 5913 RVA: 0x00085384 File Offset: 0x00083584
	public static ItemType GetRandomPhantomItem()
	{
		int num = UnityEngine.Random.Range(0, StageItemManager.m_phantomItemTypes.Length);
		return StageItemManager.m_phantomItemTypes[num];
	}

	// Token: 0x0600171A RID: 5914 RVA: 0x000853A8 File Offset: 0x000835A8
	[Conditional("DEBUG_INFO")]
	private static void DebugLog(string s)
	{
		global::Debug.Log("@ms " + s);
	}

	// Token: 0x0600171B RID: 5915 RVA: 0x000853BC File Offset: 0x000835BC
	[Conditional("DEBUG_INFO")]
	private static void DebugLogWarning(string s)
	{
		global::Debug.LogWarning("@ms " + s);
	}

	// Token: 0x0400147C RID: 5244
	private const float DEBUG_TIME = -1f;

	// Token: 0x0400147D RID: 5245
	private const float VALIDATE_TIME = 3f;

	// Token: 0x0400147E RID: 5246
	private const float PHANTOM_VALIDATE_TIME = 6f;

	// Token: 0x0400147F RID: 5247
	private const float ALTITUDE_TRAMPOLINE_OFFSET = 6f;

	// Token: 0x04001480 RID: 5248
	private const float INVALID_TIME_ON_PAUSED = 2f;

	// Token: 0x04001481 RID: 5249
	private const int ITEM_COMBO_SCORE_RATE = 2;

	// Token: 0x04001482 RID: 5250
	private const int ITEM_COMBO_SCORE_MAX_RATE = 10;

	// Token: 0x04001483 RID: 5251
	private const int EQUIP_ITEM_MAX_COUNT = 3;

	// Token: 0x04001484 RID: 5252
	[SerializeField]
	public bool m_debugEquipItem;

	// Token: 0x04001485 RID: 5253
	[SerializeField]
	public ItemType[] m_debugEquipItemTypes = new ItemType[3];

	// Token: 0x04001486 RID: 5254
	private float[] m_items_fullTime = new float[8];

	// Token: 0x04001487 RID: 5255
	private float[] m_items_time = new float[8];

	// Token: 0x04001488 RID: 5256
	private bool[] m_items_paused;

	// Token: 0x04001489 RID: 5257
	private float m_chaoAblityComboUpRate = 1f;

	// Token: 0x0400148A RID: 5258
	private float m_niths_combo_time;

	// Token: 0x0400148B RID: 5259
	private float m_item_combo_time;

	// Token: 0x0400148C RID: 5260
	private ItemTable m_item_table;

	// Token: 0x0400148D RID: 5261
	private bool m_disable_equipItem;

	// Token: 0x0400148E RID: 5262
	private bool m_nowPhantom;

	// Token: 0x0400148F RID: 5263
	private bool m_characterChange;

	// Token: 0x04001490 RID: 5264
	private bool m_bossStage;

	// Token: 0x04001491 RID: 5265
	private bool m_forcePhantomInvalidate;

	// Token: 0x04001492 RID: 5266
	private bool m_equipItemTutorial;

	// Token: 0x04001493 RID: 5267
	public static Dictionary<ItemType, AbilityType> s_dicItemTypeToCharAbilityType = new Dictionary<ItemType, AbilityType>
	{
		{
			ItemType.INVINCIBLE,
			AbilityType.INVINCIBLE
		},
		{
			ItemType.BARRIER,
			AbilityType.NUM
		},
		{
			ItemType.MAGNET,
			AbilityType.MAGNET
		},
		{
			ItemType.TRAMPOLINE,
			AbilityType.TRAMPOLINE
		},
		{
			ItemType.COMBO,
			AbilityType.COMBO
		},
		{
			ItemType.LASER,
			AbilityType.LASER
		},
		{
			ItemType.DRILL,
			AbilityType.DRILL
		},
		{
			ItemType.ASTEROID,
			AbilityType.ASTEROID
		}
	};

	// Token: 0x04001494 RID: 5268
	private static StageItemManager instance = null;

	// Token: 0x04001495 RID: 5269
	private bool m_availableItem;

	// Token: 0x04001496 RID: 5270
	private List<ItemType> m_displayEquipItems = new List<ItemType>();

	// Token: 0x04001497 RID: 5271
	private List<StageItemManager.EquippedItem> m_equipItems = new List<StageItemManager.EquippedItem>();

	// Token: 0x04001498 RID: 5272
	private ItemType m_busyItem = ItemType.UNKNOWN;

	// Token: 0x04001499 RID: 5273
	private int[] m_itemChangeTable = new int[]
	{
		0,
		1,
		2,
		3,
		4,
		5,
		6,
		7
	};

	// Token: 0x0400149A RID: 5274
	private static ItemType[] m_phantomItemTypes = new ItemType[]
	{
		ItemType.LASER,
		ItemType.DRILL,
		ItemType.ASTEROID
	};

	// Token: 0x0400149B RID: 5275
	private static ItemType[] m_feverBossNoPauseItemTypes = new ItemType[]
	{
		ItemType.BARRIER,
		ItemType.MAGNET,
		ItemType.COMBO
	};

	// Token: 0x0400149C RID: 5276
	private StageItemManager.PhantomUseType m_phantomUseType;

	// Token: 0x0400149D RID: 5277
	private LevelInformation m_levelInformation;

	// Token: 0x0400149E RID: 5278
	private PlayerInformation m_playerInformation;

	// Token: 0x0400149F RID: 5279
	private List<ItemType> m_stockColorItems = new List<ItemType>();

	// Token: 0x040014A0 RID: 5280
	private List<ItemType> m_stockItems = new List<ItemType>();

	// Token: 0x040014A1 RID: 5281
	private bool m_activeTrampoline;

	// Token: 0x040014A2 RID: 5282
	private bool m_activeAltitudeTrampoline;

	// Token: 0x040014A3 RID: 5283
	private bool m_Altitude;

	// Token: 0x040014A4 RID: 5284
	private bool m_bossItemTutorial;

	// Token: 0x040014A5 RID: 5285
	private static ItemType[] m_cautionItemTypePriority = new ItemType[]
	{
		ItemType.LASER,
		ItemType.DRILL,
		ItemType.ASTEROID,
		ItemType.INVINCIBLE,
		ItemType.TRAMPOLINE,
		ItemType.MAGNET,
		ItemType.COMBO
	};

	// Token: 0x02000310 RID: 784
	private class EquippedItem
	{
		// Token: 0x040014A6 RID: 5286
		public ItemType item;

		// Token: 0x040014A7 RID: 5287
		public int index;

		// Token: 0x040014A8 RID: 5288
		public bool revivedFlag;
	}

	// Token: 0x02000311 RID: 785
	private class ChaoAbilityOption
	{
		// Token: 0x040014A9 RID: 5289
		public float m_specifiedTime;
	}

	// Token: 0x02000312 RID: 786
	private enum PhantomUseType
	{
		// Token: 0x040014AB RID: 5291
		ATTACK_PAUSE,
		// Token: 0x040014AC RID: 5292
		DISABLE,
		// Token: 0x040014AD RID: 5293
		ENABLE
	}
}
