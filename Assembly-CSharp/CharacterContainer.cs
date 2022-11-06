using System;
using Message;
using Player;
using UnityEngine;

// Token: 0x02000966 RID: 2406
public class CharacterContainer : MonoBehaviour
{
	// Token: 0x06003EEB RID: 16107 RVA: 0x00146E44 File Offset: 0x00145044
	private void Start()
	{
	}

	// Token: 0x06003EEC RID: 16108 RVA: 0x00146E48 File Offset: 0x00145048
	private void Update()
	{
		if (this.m_requestChange)
		{
			int current = (this.m_numCurrent != 0) ? 0 : 1;
			this.ChangeCurrentCharacter(current, !this.m_enableChange);
			MsgChangeCharaSucceed msgChangeCharaSucceed = new MsgChangeCharaSucceed();
			msgChangeCharaSucceed.m_disabled = !this.m_enableChange;
			GameObjectUtil.SendMessageFindGameObject("HudCockpit", "OnChangeCharaSucceed", msgChangeCharaSucceed, SendMessageOptions.DontRequireReceiver);
			GameObjectUtil.SendMessageFindGameObject("GameModeStage", "OnChangeCharaSucceed", msgChangeCharaSucceed, SendMessageOptions.DontRequireReceiver);
			GameObjectUtil.SendMessageFindGameObject("StageComboManager", "OnChangeCharaSucceed", msgChangeCharaSucceed, SendMessageOptions.DontRequireReceiver);
			GameObjectUtil.SendDelayedMessageToTagObjects("Boss", "OnChangeCharaSucceed", msgChangeCharaSucceed);
			if (StageItemManager.Instance != null)
			{
				StageItemManager.Instance.OnChangeCharaStart(msgChangeCharaSucceed);
			}
			GameObjectUtil.SendDelayedMessageFindGameObject("StageItemManager", "OnChangeCharaSucceed", msgChangeCharaSucceed);
			this.m_requestChange = false;
		}
	}

	// Token: 0x06003EED RID: 16109 RVA: 0x00146F10 File Offset: 0x00145110
	public void Init()
	{
		this.m_playerInformation = GameObjectUtil.FindGameObjectComponent<PlayerInformation>("PlayerInformation");
		this.m_character = new GameObject[2];
		this.m_character[0] = GameObjectUtil.FindChildGameObject(base.gameObject, "PlayerCharacterMain");
		this.m_character[1] = GameObjectUtil.FindChildGameObject(base.gameObject, "PlayerCharacterSub");
		if (this.m_playerInformation != null && this.m_playerInformation.SubCharacterName == null)
		{
			this.m_character[1] = null;
		}
		this.m_enableChange = (this.m_character[1] != null);
		this.m_requestChange = false;
		for (int i = 0; i < 2; i++)
		{
			this.m_recovered[i] = false;
		}
		this.m_btnCharaChange = false;
		MsgChangeCharaEnable value = new MsgChangeCharaEnable(this.m_enableChange);
		GameObjectUtil.SendMessageFindGameObject("HudCockpit", "OnChangeCharaEnable", value, SendMessageOptions.DontRequireReceiver);
		this.m_numCurrent = 0;
	}

	// Token: 0x06003EEE RID: 16110 RVA: 0x00146FF8 File Offset: 0x001451F8
	public void SetupCharacter()
	{
		int num = 0;
		foreach (GameObject gameObject in this.m_character)
		{
			if (gameObject != null)
			{
				CharacterState component = gameObject.GetComponent<CharacterState>();
				if (component != null)
				{
					component.SetPlayingType((PlayingCharacterType)num);
					component.SetupModelsAndParameter();
				}
			}
			num++;
		}
	}

	// Token: 0x06003EEF RID: 16111 RVA: 0x0014705C File Offset: 0x0014525C
	private GameObject GetCurrentCharacter()
	{
		return this.m_character[this.m_numCurrent];
	}

	// Token: 0x06003EF0 RID: 16112 RVA: 0x0014706C File Offset: 0x0014526C
	private GameObject GetNonCurrentCharacter()
	{
		int num = (this.m_numCurrent != 0) ? 0 : 1;
		return this.m_character[num];
	}

	// Token: 0x06003EF1 RID: 16113 RVA: 0x00147094 File Offset: 0x00145294
	private void OnMsgChangeChara(MsgChangeChara msg)
	{
		GameObject currentCharacter = this.GetCurrentCharacter();
		if (currentCharacter != null && this.m_enableChange && !this.m_requestChange)
		{
			if (!currentCharacter.GetComponent<CharacterState>().IsEnableCharaChange(msg.m_changeByMiss))
			{
				return;
			}
			this.m_requestChange = true;
			if (msg.m_changeByMiss)
			{
				this.m_enableChange = false;
			}
			if (msg.m_changeByBtn)
			{
				this.m_btnCharaChange = true;
			}
			msg.m_succeed = true;
		}
	}

	// Token: 0x06003EF2 RID: 16114 RVA: 0x00147114 File Offset: 0x00145314
	private void ChangeCurrentCharacter(int current, bool dead)
	{
		GameObject currentCharacter = this.GetCurrentCharacter();
		this.m_numCurrent = current;
		for (int i = 0; i < this.m_character.Length; i++)
		{
			if (i != this.m_numCurrent)
			{
				this.DeactiveCharacter(i);
			}
		}
		for (int j = 0; j < this.m_character.Length; j++)
		{
			if (j == this.m_numCurrent)
			{
				this.ActivateCharacter(j, currentCharacter.transform.position, currentCharacter.transform.rotation, dead, true);
			}
		}
		if (dead)
		{
			MsgChaoStateUtil.SendMsgChaoState(MsgChaoState.State.STOP_END);
		}
	}

	// Token: 0x06003EF3 RID: 16115 RVA: 0x001471AC File Offset: 0x001453AC
	private void ActivateCharacter(int numPlayer, Vector3 plrPos, Quaternion plrRot, bool dead, bool trampoline)
	{
		Vector3 vector = plrPos;
		Vector3 sideViewPathPos = this.m_playerInformation.SideViewPathPos;
		Vector3 sideViewPathNormal = this.m_playerInformation.SideViewPathNormal;
		Vector3 lhs = plrPos - sideViewPathPos;
		if (Vector3.Dot(lhs, sideViewPathNormal) < 0f)
		{
			plrPos = sideViewPathPos + Vector3.up * 1f;
			plrRot = Quaternion.Euler(new Vector3(0f, 90f, 0f));
		}
		global::Debug.Log(string.Concat(new object[]
		{
			"CharaChange Diactive POS: ",
			vector.x,
			" ",
			vector.y,
			" ",
			vector.z
		}));
		global::Debug.Log(string.Concat(new object[]
		{
			"CharaChange Active   POS: ",
			plrPos.x,
			" ",
			plrPos.y,
			" ",
			plrPos.z
		}));
		this.m_character[numPlayer].GetComponent<CharacterState>().ActiveCharacter(dead, dead, plrPos, plrRot);
		this.m_character[numPlayer].SetActive(true);
		if (trampoline)
		{
			ObjUtil.SendMessageAppearTrampoline();
		}
		if (!this.m_btnCharaChange)
		{
			ObjUtil.SendMessageOnObjectDead();
		}
		this.m_btnCharaChange = false;
	}

	// Token: 0x06003EF4 RID: 16116 RVA: 0x00147314 File Offset: 0x00145514
	private void DeactiveCharacter(int numPlayer)
	{
		this.m_character[numPlayer].GetComponent<CharacterState>().DeactiveCharacter();
		this.m_character[numPlayer].SetActive(false);
	}

	// Token: 0x06003EF5 RID: 16117 RVA: 0x00147344 File Offset: 0x00145544
	private bool IsNowLastChance(int numPlayer)
	{
		return this.m_character[numPlayer].GetComponent<CharacterState>().IsNowLastChance();
	}

	// Token: 0x06003EF6 RID: 16118 RVA: 0x00147358 File Offset: 0x00145558
	private void SetResetCharacterLastChance(int numPlayer)
	{
		this.m_character[numPlayer].GetComponent<CharacterState>().SetLastChance(false);
		if (this.m_playerInformation != null)
		{
			this.m_playerInformation.SetLastChance(false);
		}
	}

	// Token: 0x06003EF7 RID: 16119 RVA: 0x00147398 File Offset: 0x00145598
	private void SetResetCharacterDead()
	{
		if (this.m_playerInformation != null)
		{
			this.m_playerInformation.SetDead(false);
		}
	}

	// Token: 0x06003EF8 RID: 16120 RVA: 0x001473B8 File Offset: 0x001455B8
	public bool IsEnableChangeCharacter()
	{
		return this.m_enableChange;
	}

	// Token: 0x06003EF9 RID: 16121 RVA: 0x001473C0 File Offset: 0x001455C0
	public bool IsEnableRecovery()
	{
		return !this.m_recovered[this.m_numCurrent] && StageAbilityManager.Instance != null && StageAbilityManager.Instance.HasChaoAbility(ChaoAbility.RECOVERY_FROM_FAILURE);
	}

	// Token: 0x06003EFA RID: 16122 RVA: 0x00147400 File Offset: 0x00145600
	public void PrepareRecovery(bool bossStage)
	{
		this.m_requestChange = false;
		this.m_recovered[this.m_numCurrent] = true;
		this.DeactiveCharacter(this.m_numCurrent);
		this.SetResetCharacterDead();
		this.ActivateCharacter(this.m_numCurrent, this.m_playerInformation.Position, this.m_playerInformation.Rotation, true, false);
		ObjUtil.SendMessageAppearTrampoline();
		MsgChaoStateUtil.SendMsgChaoState(MsgChaoState.State.STOP_END);
		if (bossStage)
		{
			MsgPrepareContinue value = new MsgPrepareContinue(bossStage, false);
			GameObjectUtil.SendMessageToTagObjects("Boss", "OnMsgPrepareContinue", value, SendMessageOptions.DontRequireReceiver);
		}
		ObjUtil.RequestStartAbilityToChao(ChaoAbility.RECOVERY_FROM_FAILURE, false);
	}

	// Token: 0x06003EFB RID: 16123 RVA: 0x0014748C File Offset: 0x0014568C
	private void OnMsgPrepareContinue(MsgPrepareContinue msg)
	{
		bool flag = this.IsNowLastChance(this.m_numCurrent);
		ItemType itemType = ItemType.UNKNOWN;
		if (StageItemManager.Instance != null)
		{
			itemType = StageItemManager.Instance.GetPhantomItemType();
		}
		bool flag2 = msg.m_timeUp && itemType != ItemType.UNKNOWN && flag;
		this.m_enableChange = (this.m_character[1] != null);
		this.m_requestChange = false;
		for (int i = 0; i < 2; i++)
		{
			this.m_recovered[i] = false;
		}
		if (!flag2)
		{
			this.DeactiveCharacter(this.m_numCurrent);
		}
		this.SetResetCharacterLastChance(this.m_numCurrent);
		this.SetResetCharacterDead();
		if (msg.m_timeUp)
		{
			if (flag)
			{
				MsgEndLastChance value = new MsgEndLastChance();
				GameObjectUtil.SendMessageToTagObjects("Chao", "OnEndLastChance", value, SendMessageOptions.DontRequireReceiver);
				if (this.m_enableChange && this.m_numCurrent == 1)
				{
					this.m_numCurrent = 0;
				}
			}
		}
		else if (this.m_enableChange && this.m_numCurrent == 1)
		{
			this.m_numCurrent = 0;
		}
		if (flag2)
		{
			ObjUtil.SendMessageOnObjectDead();
		}
		else
		{
			this.ActivateCharacter(this.m_numCurrent, this.m_playerInformation.Position, this.m_playerInformation.Rotation, true, false);
		}
		bool flag3 = false;
		if (EventManager.Instance != null)
		{
			flag3 = EventManager.Instance.IsRaidBossStage();
		}
		StageScoreManager instance = StageScoreManager.Instance;
		if (instance != null)
		{
			ObjUtil.SendMessageTransferRingForContinue((!flag3) ? instance.ContinueRing : instance.ContinueRaidBossRing);
		}
		int numRings = this.m_playerInformation.NumRings;
		GameObjectUtil.SendMessageFindGameObject("HudCockpit", "OnAddStockRing", new MsgAddStockRing(numRings), SendMessageOptions.DontRequireReceiver);
		if (!msg.m_bossStage)
		{
			this.SendAddItem(ItemType.BARRIER);
			this.SendAddItem(ItemType.MAGNET);
			this.SendAddItem(ItemType.COMBO);
			this.SendAddDamageTrampoline();
			if (itemType == ItemType.UNKNOWN)
			{
				itemType = StageItemManager.GetRandomPhantomItem();
			}
			if (!this.SendAddItem(itemType))
			{
				this.SendAddColorItem(itemType);
				MsgChaoStateUtil.SendMsgChaoState(MsgChaoState.State.STOP_END);
			}
		}
		else
		{
			this.SendAddItem(ItemType.INVINCIBLE);
			this.SendAddItem(ItemType.BARRIER);
			this.SendAddItem(ItemType.MAGNET);
			MsgChaoStateUtil.SendMsgChaoState(MsgChaoState.State.STOP_END);
		}
		if (this.m_enableChange)
		{
			MsgChangeCharaEnable value2 = new MsgChangeCharaEnable(true);
			GameObjectUtil.SendMessageFindGameObject("HudCockpit", "OnChangeCharaEnable", value2, SendMessageOptions.DontRequireReceiver);
			MsgChangeCharaSucceed value3 = new MsgChangeCharaSucceed();
			GameObjectUtil.SendMessageFindGameObject("StageComboManager", "OnChangeCharaSucceed", value3, SendMessageOptions.DontRequireReceiver);
		}
	}

	// Token: 0x06003EFC RID: 16124 RVA: 0x001476FC File Offset: 0x001458FC
	private bool SendAddItem(ItemType itemType)
	{
		if (StageItemManager.Instance != null)
		{
			MsgAskEquipItemUsed msgAskEquipItemUsed = new MsgAskEquipItemUsed(itemType);
			StageItemManager.Instance.OnAskEquipItemUsed(msgAskEquipItemUsed);
			if (msgAskEquipItemUsed.m_ok)
			{
				StageItemManager.Instance.OnAddItem(new MsgAddItemToManager(itemType));
				return true;
			}
		}
		return false;
	}

	// Token: 0x06003EFD RID: 16125 RVA: 0x0014774C File Offset: 0x0014594C
	private void SendAddColorItem(ItemType itemType)
	{
		if (StageItemManager.Instance != null)
		{
			StageItemManager.Instance.OnAddColorItem(new MsgAddItemToManager(itemType));
		}
	}

	// Token: 0x06003EFE RID: 16126 RVA: 0x0014777C File Offset: 0x0014597C
	private void SendAddDamageTrampoline()
	{
		if (StageItemManager.Instance != null)
		{
			StageItemManager.Instance.OnAddDamageTrampoline();
		}
	}

	// Token: 0x040035E5 RID: 13797
	private int m_numCurrent;

	// Token: 0x040035E6 RID: 13798
	private bool[] m_recovered = new bool[2];

	// Token: 0x040035E7 RID: 13799
	private bool m_btnCharaChange;

	// Token: 0x040035E8 RID: 13800
	private bool m_enableChange;

	// Token: 0x040035E9 RID: 13801
	private bool m_requestChange;

	// Token: 0x040035EA RID: 13802
	private GameObject[] m_character;

	// Token: 0x040035EB RID: 13803
	private PlayerInformation m_playerInformation;

	// Token: 0x02000967 RID: 2407
	private enum Type
	{
		// Token: 0x040035ED RID: 13805
		MAIN,
		// Token: 0x040035EE RID: 13806
		SUB
	}
}
