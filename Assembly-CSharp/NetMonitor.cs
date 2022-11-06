using System;
using System.Collections;
using Message;
using Text;
using UnityEngine;

// Token: 0x02000694 RID: 1684
public class NetMonitor : MonoBehaviour
{
	// Token: 0x170005DF RID: 1503
	// (get) Token: 0x06002D50 RID: 11600 RVA: 0x0010F634 File Offset: 0x0010D834
	public static NetMonitor Instance
	{
		get
		{
			return NetMonitor.m_instance;
		}
	}

	// Token: 0x06002D51 RID: 11601 RVA: 0x0010F63C File Offset: 0x0010D83C
	public bool IsIdle()
	{
		return this.m_state == NetMonitor.State.IDLE;
	}

	// Token: 0x06002D52 RID: 11602 RVA: 0x0010F648 File Offset: 0x0010D848
	private void Start()
	{
		if (NetMonitor.m_instance == null)
		{
			UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
			NetMonitor.m_instance = this;
			base.gameObject.AddComponent<HudNetworkConnect>();
			base.gameObject.AddComponent<ServerSessionWatcher>();
		}
		else
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x06002D53 RID: 11603 RVA: 0x0010F6A0 File Offset: 0x0010D8A0
	private void LateUpdate()
	{
		switch (this.m_state)
		{
		case NetMonitor.State.SESSION_TIMEOUT:
			if (NetworkErrorWindow.IsButtonPressed)
			{
				HudMenuUtility.GoToTitleScene();
				this.m_state = NetMonitor.State.IDLE;
			}
			break;
		case NetMonitor.State.WAIT_CONNECTING:
			if (this.m_hudDisplayWait < 0f)
			{
				this.m_state = NetMonitor.State.CONNECTING;
			}
			else
			{
				this.m_currentWait += Time.deltaTime;
				if (this.m_currentWait >= this.m_hudDisplayWait)
				{
					HudNetworkConnect component = base.gameObject.GetComponent<HudNetworkConnect>();
					if (component != null)
					{
						component.Setup();
						component.PlayStart(this.m_ConnectDisplayType);
					}
					this.m_state = NetMonitor.State.CONNECTING;
				}
			}
			break;
		case NetMonitor.State.ERROR_HANDLING:
			if (this.m_errorHandler != null)
			{
				this.m_errorHandler.Update();
				if (this.m_errorHandler.IsEnd())
				{
					this.m_state = NetMonitor.State.IDLE;
					this.m_errorHandler.EndErrorHandle();
					this.m_errorHandler = null;
				}
			}
			break;
		}
	}

	// Token: 0x06002D54 RID: 11604 RVA: 0x0010F7BC File Offset: 0x0010D9BC
	private void OnDestroy()
	{
	}

	// Token: 0x06002D55 RID: 11605 RVA: 0x0010F7C0 File Offset: 0x0010D9C0
	public void PrepareConnect()
	{
		this.PrepareConnect(ServerSessionWatcher.ValidateType.LOGIN_OR_RELOGIN);
	}

	// Token: 0x06002D56 RID: 11606 RVA: 0x0010F7CC File Offset: 0x0010D9CC
	public void PrepareConnect(ServerSessionWatcher.ValidateType validateType)
	{
		this.m_isEndPrepare = false;
		this.m_isSuccessPrepare = false;
		this.m_validateType = validateType;
		base.StartCoroutine(this.PrepareConnectCoroutine(validateType));
	}

	// Token: 0x06002D57 RID: 11607 RVA: 0x0010F7F4 File Offset: 0x0010D9F4
	public IEnumerator PrepareConnectCoroutine(ServerSessionWatcher.ValidateType validateType)
	{
		if (this.m_isServerBusy)
		{
			this.m_isServerBusy = false;
			HudNetworkConnect connect = base.gameObject.GetComponent<HudNetworkConnect>();
			if (connect != null)
			{
				connect.PlayStart(HudNetworkConnect.DisplayType.ALL);
			}
			float currentTime = 0f;
			while (currentTime >= 3f)
			{
				currentTime += Time.unscaledDeltaTime;
				yield return null;
			}
			if (connect != null)
			{
				connect.PlayEnd();
			}
		}
		ServerSessionWatcher watcher = base.gameObject.GetComponent<ServerSessionWatcher>();
		if (watcher != null)
		{
			watcher.ValidateSession(this.m_validateType, new ServerSessionWatcher.ValidateSessionEndCallback(this.ValidateSessionCallback));
		}
		yield break;
	}

	// Token: 0x06002D58 RID: 11608 RVA: 0x0010F810 File Offset: 0x0010DA10
	public bool IsEndPrepare()
	{
		return this.m_isEndPrepare;
	}

	// Token: 0x06002D59 RID: 11609 RVA: 0x0010F818 File Offset: 0x0010DA18
	public bool IsSuccessPrepare()
	{
		return this.m_isSuccessPrepare;
	}

	// Token: 0x06002D5A RID: 11610 RVA: 0x0010F820 File Offset: 0x0010DA20
	public void StartMonitor(ServerRetryProcess process)
	{
		this.StartMonitor(process, 0f, HudNetworkConnect.DisplayType.ALL);
	}

	// Token: 0x06002D5B RID: 11611 RVA: 0x0010F830 File Offset: 0x0010DA30
	public void StartMonitor(ServerRetryProcess process, float hudDisplayWait, HudNetworkConnect.DisplayType displayType)
	{
		this.m_retryProcess = process;
		this.m_hudDisplayWait = hudDisplayWait;
		this.m_currentWait = 0f;
		this.m_ConnectDisplayType = displayType;
		this.m_state = NetMonitor.State.WAIT_CONNECTING;
	}

	// Token: 0x06002D5C RID: 11612 RVA: 0x0010F85C File Offset: 0x0010DA5C
	public void EndMonitorForward(MessageBase msg, GameObject callbackObject, string callbackFuncName)
	{
		if (msg != null)
		{
			if (msg.ID == 61517)
			{
				this.m_connectFailedCount++;
				MsgServerConnctFailed msgServerConnctFailed = msg as MsgServerConnctFailed;
				if (msgServerConnctFailed != null)
				{
					ServerInterface.StatusCode status = msgServerConnctFailed.m_status;
					switch (status + 10)
					{
					case ServerInterface.StatusCode.Ok:
					case (ServerInterface.StatusCode)3:
						if (this.m_connectFailedCount < NetMonitor.CountToAskGiveUp)
						{
							ErrorHandleRetry errorHandleRetry = new ErrorHandleRetry();
							errorHandleRetry.SetRetryProcess(this.m_retryProcess);
							this.m_errorHandler = errorHandleRetry;
						}
						else
						{
							ErrorHandleAskGiveUpRetry errorHandleAskGiveUpRetry = new ErrorHandleAskGiveUpRetry();
							errorHandleAskGiveUpRetry.SetRetryProcess(this.m_retryProcess);
							this.m_errorHandler = errorHandleAskGiveUpRetry;
						}
						break;
					default:
						if (status != ServerInterface.StatusCode.VersionDifference)
						{
							if (status != ServerInterface.StatusCode.ServerSecurityError)
							{
								if (status != ServerInterface.StatusCode.ExpirationSession)
								{
									if (status != ServerInterface.StatusCode.MissingPlayer)
									{
										if (status != ServerInterface.StatusCode.VersionForApplication)
										{
											if (status != ServerInterface.StatusCode.AlreadyInvitedFriend)
											{
												if (status != ServerInterface.StatusCode.ServerMaintenance)
												{
													if (status != ServerInterface.StatusCode.ServerNextVersion)
													{
														if (status != ServerInterface.StatusCode.InvalidReceiptData)
														{
															if (status != ServerInterface.StatusCode.ServerBusy)
															{
																this.m_errorHandler = new ErrorHandleUnExpectedError();
															}
															else
															{
																this.m_errorHandler = new ErrorHandleUnExpectedError();
																this.m_isServerBusy = true;
															}
														}
														else
														{
															this.m_errorHandler = new ErrorHandleInvalidReciept();
														}
													}
													else
													{
														this.m_errorHandler = new ErrorHandleServerNextVersion();
													}
												}
												else
												{
													this.m_errorHandler = new ErrorHandleMaintenance();
												}
											}
											else
											{
												this.m_errorHandler = new ErrorHandleAlreadyInvited();
											}
										}
										else
										{
											ErrorHandleVersionForApplication errorHandleVersionForApplication = new ErrorHandleVersionForApplication();
											errorHandleVersionForApplication.SetRetryProcess(this.m_retryProcess);
											this.m_errorHandler = errorHandleVersionForApplication;
										}
									}
									else
									{
										this.m_errorHandler = new ErrorHandleMissingPlayer();
									}
								}
								else
								{
									ErrorHandleExpirationSession errorHandleExpirationSession = new ErrorHandleExpirationSession();
									errorHandleExpirationSession.SetRetryProcess(this.m_retryProcess);
									errorHandleExpirationSession.SetSessionValidateType(this.m_validateType);
									this.m_errorHandler = errorHandleExpirationSession;
								}
							}
							else
							{
								ErrorHandleSecurityError errorHandleSecurityError = new ErrorHandleSecurityError();
								errorHandleSecurityError.SetRetryProcess(this.m_retryProcess);
								this.m_errorHandler = errorHandleSecurityError;
							}
						}
						else
						{
							this.m_errorHandler = new ErrorHandleVersionDifference();
						}
						break;
					}
				}
			}
			else if (msg.ID == 61520)
			{
				this.m_connectFailedCount++;
				if (this.m_connectFailedCount < NetMonitor.CountToAskGiveUp)
				{
					ErrorHandleRetry errorHandleRetry2 = new ErrorHandleRetry();
					errorHandleRetry2.SetRetryProcess(this.m_retryProcess);
					this.m_errorHandler = errorHandleRetry2;
				}
				else
				{
					ErrorHandleAskGiveUpRetry errorHandleAskGiveUpRetry2 = new ErrorHandleAskGiveUpRetry();
					errorHandleAskGiveUpRetry2.SetRetryProcess(this.m_retryProcess);
					this.m_errorHandler = errorHandleAskGiveUpRetry2;
				}
			}
			else
			{
				this.OnResetConnectFailedCount();
			}
			if (this.m_errorHandler != null)
			{
				this.m_errorHandler.Setup(callbackObject, callbackFuncName, msg);
			}
		}
	}

	// Token: 0x06002D5D RID: 11613 RVA: 0x0010FB0C File Offset: 0x0010DD0C
	public void EndMonitorBackward()
	{
		HudNetworkConnect component = base.gameObject.GetComponent<HudNetworkConnect>();
		if (component != null)
		{
			component.PlayEnd();
		}
		if (this.m_errorHandler != null)
		{
			this.m_errorHandler.StartErrorHandle();
			this.m_state = NetMonitor.State.ERROR_HANDLING;
		}
		else
		{
			this.m_state = NetMonitor.State.IDLE;
		}
	}

	// Token: 0x06002D5E RID: 11614 RVA: 0x0010FB60 File Offset: 0x0010DD60
	public void ServerReLogin_Succeeded()
	{
		this.m_isEndPrepare = true;
		this.m_isSuccessPrepare = true;
	}

	// Token: 0x06002D5F RID: 11615 RVA: 0x0010FB70 File Offset: 0x0010DD70
	private void ValidateSessionCallback(bool isSuccess)
	{
		this.m_isEndPrepare = true;
		this.m_isSuccessPrepare = isSuccess;
		if (!this.m_isSuccessPrepare)
		{
			NetworkErrorWindow.Create(new NetworkErrorWindow.CInfo
			{
				buttonType = NetworkErrorWindow.ButtonType.Ok,
				anchor_path = string.Empty,
				caption = TextManager.GetText(TextManager.TextType.TEXTTYPE_FIXATION_TEXT, "NetworkError", "ui_Lbl_session_timeout_caption").text,
				message = TextManager.GetText(TextManager.TextType.TEXTTYPE_FIXATION_TEXT, "NetworkError", "ui_Lbl_session_timeout_text").text
			});
			this.m_state = NetMonitor.State.SESSION_TIMEOUT;
		}
	}

	// Token: 0x06002D60 RID: 11616 RVA: 0x0010FBF8 File Offset: 0x0010DDF8
	private void OnResetConnectFailedCount()
	{
		this.m_connectFailedCount = 0;
	}

	// Token: 0x040029BC RID: 10684
	private static NetMonitor m_instance;

	// Token: 0x040029BD RID: 10685
	private ErrorHandleBase m_errorHandler;

	// Token: 0x040029BE RID: 10686
	private NetMonitor.State m_state;

	// Token: 0x040029BF RID: 10687
	private ServerRetryProcess m_retryProcess;

	// Token: 0x040029C0 RID: 10688
	private float m_hudDisplayWait;

	// Token: 0x040029C1 RID: 10689
	private float m_currentWait;

	// Token: 0x040029C2 RID: 10690
	private HudNetworkConnect.DisplayType m_ConnectDisplayType;

	// Token: 0x040029C3 RID: 10691
	private ServerSessionWatcher.ValidateType m_validateType;

	// Token: 0x040029C4 RID: 10692
	private int m_connectFailedCount;

	// Token: 0x040029C5 RID: 10693
	private bool m_isEndPrepare;

	// Token: 0x040029C6 RID: 10694
	private bool m_isSuccessPrepare;

	// Token: 0x040029C7 RID: 10695
	private static readonly int CountToAskGiveUp = 3;

	// Token: 0x040029C8 RID: 10696
	private bool m_isServerBusy;

	// Token: 0x02000695 RID: 1685
	private enum State
	{
		// Token: 0x040029CA RID: 10698
		IDLE,
		// Token: 0x040029CB RID: 10699
		PREPARE,
		// Token: 0x040029CC RID: 10700
		SESSION_TIMEOUT,
		// Token: 0x040029CD RID: 10701
		WAIT_CONNECTING,
		// Token: 0x040029CE RID: 10702
		CONNECTING,
		// Token: 0x040029CF RID: 10703
		ERROR_HANDLING
	}
}
