using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200031D RID: 797
public class StageTimeManager : MonoBehaviour
{
	// Token: 0x1700039E RID: 926
	// (get) Token: 0x06001799 RID: 6041 RVA: 0x00087438 File Offset: 0x00085638
	public float Time
	{
		get
		{
			return this.m_time;
		}
	}

	// Token: 0x1700039F RID: 927
	// (get) Token: 0x0600179A RID: 6042 RVA: 0x00087440 File Offset: 0x00085640
	public static StageTimeManager Instance
	{
		get
		{
			return StageTimeManager.s_instance;
		}
	}

	// Token: 0x0600179B RID: 6043 RVA: 0x00087448 File Offset: 0x00085648
	private void Awake()
	{
		if (StageTimeManager.s_instance == null)
		{
			StageTimeManager.s_instance = this;
		}
		else
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x0600179C RID: 6044 RVA: 0x0008747C File Offset: 0x0008567C
	private void Start()
	{
	}

	// Token: 0x0600179D RID: 6045 RVA: 0x00087480 File Offset: 0x00085680
	private void Update()
	{
		if (this.m_playing && !this.m_phantomPause && this.m_time > 0f)
		{
			this.m_time -= UnityEngine.Time.deltaTime;
			if (this.m_time < 0f)
			{
				this.m_time = 0f;
			}
			this.m_playTime.Set(this.m_playTime.Get() + (long)(UnityEngine.Time.deltaTime * 1000f));
		}
	}

	// Token: 0x0600179E RID: 6046 RVA: 0x00087504 File Offset: 0x00085704
	public void SetTable()
	{
		StageTimeTable stageTimeTable = GameObjectUtil.FindGameObjectComponent<StageTimeTable>("StageTimeTable");
		if (stageTimeTable != null)
		{
			this.m_stageTimeTable = stageTimeTable;
		}
		this.SetTime();
	}

	// Token: 0x0600179F RID: 6047 RVA: 0x00087538 File Offset: 0x00085738
	private void SetTime()
	{
		float num = 0f;
		ServerPlayerState playerState = ServerInterface.PlayerState;
		if (playerState != null)
		{
			SaveDataManager instance = SaveDataManager.Instance;
			if (instance != null && instance.PlayerData != null)
			{
				ServerCharacterState serverCharacterState = playerState.CharacterState(instance.PlayerData.MainChara);
				if (serverCharacterState != null)
				{
					float quickModeTimeExtension = serverCharacterState.QuickModeTimeExtension;
					num += quickModeTimeExtension;
					this.m_mainCharaExtend.Set((int)quickModeTimeExtension);
				}
				if (StageAbilityManager.Instance != null && StageAbilityManager.Instance.BoostItemValidFlag[2])
				{
					ServerCharacterState serverCharacterState2 = playerState.CharacterState(instance.PlayerData.SubChara);
					if (serverCharacterState2 != null)
					{
						float quickModeTimeExtension2 = serverCharacterState2.QuickModeTimeExtension;
						num += quickModeTimeExtension2;
						this.m_subCharaExtend.Set((int)quickModeTimeExtension2);
					}
				}
			}
		}
		this.m_extendParams.Clear();
		if (!this.m_debugFlag && this.m_stageTimeTable != null && this.m_stageTimeTable.IsSetupEnd())
		{
			this.m_extendedLimit = (float)this.m_stageTimeTable.GetTableItemData(StageTimeTableItem.ItemExtendedLimit);
			this.m_charaOverlapBonus = (float)this.m_stageTimeTable.GetTableItemData(StageTimeTableItem.OverlapBonus);
			this.m_extendParams.Add(StageTimeManager.ExtendPattern.CONTINUE, (float)this.m_stageTimeTable.GetTableItemData(StageTimeTableItem.Continue));
			this.m_extendParams.Add(StageTimeManager.ExtendPattern.BRONZE_WATCH, (float)this.m_stageTimeTable.GetTableItemData(StageTimeTableItem.BronzeWatch));
			this.m_extendParams.Add(StageTimeManager.ExtendPattern.SILVER_WATCH, (float)this.m_stageTimeTable.GetTableItemData(StageTimeTableItem.SilverWatch));
			this.m_extendParams.Add(StageTimeManager.ExtendPattern.GOLD_WATCH, (float)this.m_stageTimeTable.GetTableItemData(StageTimeTableItem.GoldWatch));
			this.m_time = (float)this.m_stageTimeTable.GetTableItemData(StageTimeTableItem.StartTime) + num;
		}
		else if (this.m_debugFlag)
		{
			this.m_extendParams.Add(StageTimeManager.ExtendPattern.CONTINUE, this.m_debugContinue);
			this.m_extendParams.Add(StageTimeManager.ExtendPattern.BRONZE_WATCH, this.m_debugBronzeWatch);
			this.m_extendParams.Add(StageTimeManager.ExtendPattern.SILVER_WATCH, this.m_debugSilverWatch);
			this.m_extendParams.Add(StageTimeManager.ExtendPattern.GOLD_WATCH, this.m_debugGoldWatch);
			this.m_time = this.m_debugStartTime + num;
		}
		else
		{
			this.m_extendParams.Add(StageTimeManager.ExtendPattern.CONTINUE, 60f);
			this.m_extendParams.Add(StageTimeManager.ExtendPattern.BRONZE_WATCH, 1f);
			this.m_extendParams.Add(StageTimeManager.ExtendPattern.SILVER_WATCH, 5f);
			this.m_extendParams.Add(StageTimeManager.ExtendPattern.GOLD_WATCH, 10f);
			this.m_time = this.m_debugStartTime + num;
		}
		this.m_totalTime.Set((int)this.m_time);
		this.m_mainCharaExtend = default(StageScoreManager.MaskedInt);
		this.m_subCharaExtend = default(StageScoreManager.MaskedInt);
	}

	// Token: 0x060017A0 RID: 6048 RVA: 0x000877C4 File Offset: 0x000859C4
	public void PlayStart()
	{
		this.m_playing = true;
		this.m_phantomPause = false;
	}

	// Token: 0x060017A1 RID: 6049 RVA: 0x000877D4 File Offset: 0x000859D4
	public void PlayEnd()
	{
		this.m_playing = false;
	}

	// Token: 0x060017A2 RID: 6050 RVA: 0x000877E0 File Offset: 0x000859E0
	public void Pause()
	{
		this.m_playing = false;
	}

	// Token: 0x060017A3 RID: 6051 RVA: 0x000877EC File Offset: 0x000859EC
	public void Resume()
	{
		this.m_playing = true;
	}

	// Token: 0x060017A4 RID: 6052 RVA: 0x000877F8 File Offset: 0x000859F8
	public void PhantomPause(bool pause)
	{
		this.m_phantomPause = pause;
	}

	// Token: 0x060017A5 RID: 6053 RVA: 0x00087804 File Offset: 0x00085A04
	public int GetTakeTimerCount(StageTimeManager.ExtendPattern pattern)
	{
		switch (pattern)
		{
		case StageTimeManager.ExtendPattern.BRONZE_WATCH:
			return this.m_bronzeCount.Get();
		case StageTimeManager.ExtendPattern.SILVER_WATCH:
			return this.m_silverCount.Get();
		case StageTimeManager.ExtendPattern.GOLD_WATCH:
			return this.m_goldCount.Get();
		default:
			return 0;
		}
	}

	// Token: 0x060017A6 RID: 6054 RVA: 0x00087854 File Offset: 0x00085A54
	public void ExtendTime(StageTimeManager.ExtendPattern pattern)
	{
		if (this.m_extendParams.ContainsKey(pattern))
		{
			this.m_time += this.m_extendParams[pattern];
			switch (pattern)
			{
			case StageTimeManager.ExtendPattern.CONTINUE:
				this.m_continuCount.Set(this.m_continuCount.Get() + 1);
				break;
			case StageTimeManager.ExtendPattern.BRONZE_WATCH:
				this.m_bronzeCount.Set(this.m_bronzeCount.Get() + 1);
				this.m_extendedTime += this.m_extendParams[pattern];
				break;
			case StageTimeManager.ExtendPattern.SILVER_WATCH:
				this.m_silverCount.Set(this.m_silverCount.Get() + 1);
				this.m_extendedTime += this.m_extendParams[pattern];
				break;
			case StageTimeManager.ExtendPattern.GOLD_WATCH:
				this.m_goldCount.Set(this.m_goldCount.Get() + 1);
				this.m_extendedTime += this.m_extendParams[pattern];
				break;
			}
			this.m_totalTime.Set(this.m_totalTime.Get() + (int)this.m_extendParams[pattern]);
		}
	}

	// Token: 0x060017A7 RID: 6055 RVA: 0x00087990 File Offset: 0x00085B90
	public void ReserveExtendTime(StageTimeManager.ExtendPattern pattern)
	{
		if (this.m_extendParams.ContainsKey(pattern))
		{
			switch (pattern)
			{
			case StageTimeManager.ExtendPattern.BRONZE_WATCH:
			case StageTimeManager.ExtendPattern.SILVER_WATCH:
			case StageTimeManager.ExtendPattern.GOLD_WATCH:
				this.m_reservedExtendedTime += this.m_extendParams[pattern];
				break;
			}
		}
	}

	// Token: 0x060017A8 RID: 6056 RVA: 0x000879E8 File Offset: 0x00085BE8
	public void CancelReservedExtendTime(StageTimeManager.ExtendPattern pattern)
	{
		if (this.m_extendParams.ContainsKey(pattern))
		{
			switch (pattern)
			{
			case StageTimeManager.ExtendPattern.BRONZE_WATCH:
			case StageTimeManager.ExtendPattern.SILVER_WATCH:
			case StageTimeManager.ExtendPattern.GOLD_WATCH:
				this.m_reservedExtendedTime -= this.m_extendParams[pattern];
				break;
			}
		}
	}

	// Token: 0x060017A9 RID: 6057 RVA: 0x00087A40 File Offset: 0x00085C40
	public float GetExtendTime(StageTimeManager.ExtendPattern pattern)
	{
		if (this.m_extendParams.ContainsKey(pattern))
		{
			return this.m_extendParams[pattern];
		}
		return 0f;
	}

	// Token: 0x060017AA RID: 6058 RVA: 0x00087A68 File Offset: 0x00085C68
	public void CheckResultTimer()
	{
		CPlusPlusLink instance = CPlusPlusLink.Instance;
		if (instance != null)
		{
			instance.CheckNativeQuickModeResultTimer(this.m_goldCount.Get(), this.m_silverCount.Get(), this.m_bronzeCount.Get(), this.m_continuCount.Get(), this.m_mainCharaExtend.Get(), this.m_subCharaExtend.Get(), this.m_totalTime.Get(), this.m_playTime.Get());
		}
	}

	// Token: 0x060017AB RID: 6059 RVA: 0x00087AE8 File Offset: 0x00085CE8
	public bool IsTimeUp()
	{
		return this.m_time <= 0f;
	}

	// Token: 0x060017AC RID: 6060 RVA: 0x00087AFC File Offset: 0x00085CFC
	public bool IsReachedExtendedLimit()
	{
		return this.m_extendedTime + this.m_reservedExtendedTime >= this.m_extendedLimit;
	}

	// Token: 0x0400150F RID: 5391
	private Dictionary<StageTimeManager.ExtendPattern, float> m_extendParams = new Dictionary<StageTimeManager.ExtendPattern, float>();

	// Token: 0x04001510 RID: 5392
	private bool m_playing;

	// Token: 0x04001511 RID: 5393
	private bool m_phantomPause;

	// Token: 0x04001512 RID: 5394
	private float m_time = 60f;

	// Token: 0x04001513 RID: 5395
	private float m_extendedTime;

	// Token: 0x04001514 RID: 5396
	private float m_cotinueTime;

	// Token: 0x04001515 RID: 5397
	private float m_reservedExtendedTime;

	// Token: 0x04001516 RID: 5398
	private float m_charaOverlapBonus = 6f;

	// Token: 0x04001517 RID: 5399
	private float m_extendedLimit = 480f;

	// Token: 0x04001518 RID: 5400
	private StageScoreManager.MaskedInt m_bronzeCount = default(StageScoreManager.MaskedInt);

	// Token: 0x04001519 RID: 5401
	private StageScoreManager.MaskedInt m_silverCount = default(StageScoreManager.MaskedInt);

	// Token: 0x0400151A RID: 5402
	private StageScoreManager.MaskedInt m_goldCount = default(StageScoreManager.MaskedInt);

	// Token: 0x0400151B RID: 5403
	private StageScoreManager.MaskedInt m_continuCount = default(StageScoreManager.MaskedInt);

	// Token: 0x0400151C RID: 5404
	private StageScoreManager.MaskedInt m_mainCharaExtend = default(StageScoreManager.MaskedInt);

	// Token: 0x0400151D RID: 5405
	private StageScoreManager.MaskedInt m_subCharaExtend = default(StageScoreManager.MaskedInt);

	// Token: 0x0400151E RID: 5406
	private StageScoreManager.MaskedInt m_totalTime = default(StageScoreManager.MaskedInt);

	// Token: 0x0400151F RID: 5407
	private StageScoreManager.MaskedLong m_playTime = default(StageScoreManager.MaskedLong);

	// Token: 0x04001520 RID: 5408
	private StageTimeTable m_stageTimeTable;

	// Token: 0x04001521 RID: 5409
	[Header("DebugFlag にチェックを入れると、下記項目にて設定した値が適応されます")]
	[SerializeField]
	private bool m_debugFlag;

	// Token: 0x04001522 RID: 5410
	[SerializeField]
	[Header("開始時の残り時間")]
	private float m_debugStartTime = 60f;

	// Token: 0x04001523 RID: 5411
	[SerializeField]
	[Header("アイテムの効果")]
	private float m_debugBronzeWatch = 1f;

	// Token: 0x04001524 RID: 5412
	[SerializeField]
	private float m_debugSilverWatch = 5f;

	// Token: 0x04001525 RID: 5413
	[SerializeField]
	private float m_debugGoldWatch = 10f;

	// Token: 0x04001526 RID: 5414
	[SerializeField]
	private float m_debugContinue = 60f;

	// Token: 0x04001527 RID: 5415
	private static StageTimeManager s_instance;

	// Token: 0x0200031E RID: 798
	public enum ExtendPattern
	{
		// Token: 0x04001529 RID: 5417
		UNKNOWN,
		// Token: 0x0400152A RID: 5418
		CONTINUE,
		// Token: 0x0400152B RID: 5419
		BRONZE_WATCH,
		// Token: 0x0400152C RID: 5420
		SILVER_WATCH,
		// Token: 0x0400152D RID: 5421
		GOLD_WATCH
	}
}
