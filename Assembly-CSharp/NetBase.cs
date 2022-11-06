using System;
using System.Collections.Generic;
using LitJson;
using UnityEngine;

// Token: 0x02000691 RID: 1681
public abstract class NetBase
{
	// Token: 0x06002CE7 RID: 11495 RVA: 0x0010DF64 File Offset: 0x0010C164
	public NetBase()
	{
		this.mEmulation = false;
		this.mRequest = new URLRequest();
		this.paramWriter = new JsonWriter();
		this.state = NetBase.emState.Executing;
		this.mResultJson = null;
		this.mResultParamJson = null;
		this.mActionName = null;
		this.elapsedTime = 0f;
		this.enableUndefinedCompare = true;
		this.responseEvent = null;
	}

	// Token: 0x170005BE RID: 1470
	// (set) Token: 0x06002CE9 RID: 11497 RVA: 0x0010E008 File Offset: 0x0010C208
	public static string undefinedComparer
	{
		set
		{
			NetBase.mUndefinedComparer = value;
		}
	}

	// Token: 0x170005BF RID: 1471
	// (get) Token: 0x06002CEA RID: 11498 RVA: 0x0010E010 File Offset: 0x0010C210
	// (set) Token: 0x06002CEB RID: 11499 RVA: 0x0010E018 File Offset: 0x0010C218
	private protected JsonWriter paramWriter { protected get; private set; }

	// Token: 0x170005C0 RID: 1472
	// (get) Token: 0x06002CEC RID: 11500 RVA: 0x0010E024 File Offset: 0x0010C224
	// (set) Token: 0x06002CED RID: 11501 RVA: 0x0010E02C File Offset: 0x0010C22C
	public NetBase.emState state { get; protected set; }

	// Token: 0x170005C1 RID: 1473
	// (get) Token: 0x06002CEE RID: 11502 RVA: 0x0010E038 File Offset: 0x0010C238
	// (set) Token: 0x06002CEF RID: 11503 RVA: 0x0010E040 File Offset: 0x0010C240
	public int result { get; set; }

	// Token: 0x170005C2 RID: 1474
	// (get) Token: 0x06002CF0 RID: 11504 RVA: 0x0010E04C File Offset: 0x0010C24C
	// (set) Token: 0x06002CF1 RID: 11505 RVA: 0x0010E054 File Offset: 0x0010C254
	public ServerInterface.StatusCode resultStCd
	{
		get
		{
			return (ServerInterface.StatusCode)this.result;
		}
		set
		{
			this.result = (int)value;
		}
	}

	// Token: 0x170005C3 RID: 1475
	// (get) Token: 0x06002CF2 RID: 11506 RVA: 0x0010E060 File Offset: 0x0010C260
	// (set) Token: 0x06002CF3 RID: 11507 RVA: 0x0010E068 File Offset: 0x0010C268
	public string errorMessage { get; private set; }

	// Token: 0x170005C4 RID: 1476
	// (get) Token: 0x06002CF4 RID: 11508 RVA: 0x0010E074 File Offset: 0x0010C274
	// (set) Token: 0x06002CF5 RID: 11509 RVA: 0x0010E07C File Offset: 0x0010C27C
	public int meintenanceValue { get; private set; }

	// Token: 0x170005C5 RID: 1477
	// (get) Token: 0x06002CF6 RID: 11510 RVA: 0x0010E088 File Offset: 0x0010C288
	// (set) Token: 0x06002CF7 RID: 11511 RVA: 0x0010E090 File Offset: 0x0010C290
	public int dataVersion { get; private set; }

	// Token: 0x170005C6 RID: 1478
	// (get) Token: 0x06002CF8 RID: 11512 RVA: 0x0010E09C File Offset: 0x0010C29C
	// (set) Token: 0x06002CF9 RID: 11513 RVA: 0x0010E0A4 File Offset: 0x0010C2A4
	public string meintenanceMessage { get; private set; }

	// Token: 0x170005C7 RID: 1479
	// (get) Token: 0x06002CFA RID: 11514 RVA: 0x0010E0B0 File Offset: 0x0010C2B0
	// (set) Token: 0x06002CFB RID: 11515 RVA: 0x0010E0B8 File Offset: 0x0010C2B8
	public float elapsedTime { get; private set; }

	// Token: 0x170005C8 RID: 1480
	// (get) Token: 0x06002CFC RID: 11516 RVA: 0x0010E0C4 File Offset: 0x0010C2C4
	// (set) Token: 0x06002CFD RID: 11517 RVA: 0x0010E0CC File Offset: 0x0010C2CC
	public int versionValue { get; private set; }

	// Token: 0x170005C9 RID: 1481
	// (get) Token: 0x06002CFE RID: 11518 RVA: 0x0010E0D8 File Offset: 0x0010C2D8
	// (set) Token: 0x06002CFF RID: 11519 RVA: 0x0010E0E0 File Offset: 0x0010C2E0
	public string versionString { get; private set; }

	// Token: 0x170005CA RID: 1482
	// (get) Token: 0x06002D00 RID: 11520 RVA: 0x0010E0EC File Offset: 0x0010C2EC
	// (set) Token: 0x06002D01 RID: 11521 RVA: 0x0010E0F4 File Offset: 0x0010C2F4
	public int assetVersionValue { get; private set; }

	// Token: 0x170005CB RID: 1483
	// (get) Token: 0x06002D02 RID: 11522 RVA: 0x0010E100 File Offset: 0x0010C300
	// (set) Token: 0x06002D03 RID: 11523 RVA: 0x0010E108 File Offset: 0x0010C308
	public string assetVersionString { get; private set; }

	// Token: 0x170005CC RID: 1484
	// (get) Token: 0x06002D04 RID: 11524 RVA: 0x0010E114 File Offset: 0x0010C314
	// (set) Token: 0x06002D05 RID: 11525 RVA: 0x0010E11C File Offset: 0x0010C31C
	public string infoVersionString { get; private set; }

	// Token: 0x170005CD RID: 1485
	// (get) Token: 0x06002D06 RID: 11526 RVA: 0x0010E128 File Offset: 0x0010C328
	// (set) Token: 0x06002D07 RID: 11527 RVA: 0x0010E130 File Offset: 0x0010C330
	public long serverTime { get; private set; }

	// Token: 0x170005CE RID: 1486
	// (get) Token: 0x06002D08 RID: 11528 RVA: 0x0010E13C File Offset: 0x0010C33C
	// (set) Token: 0x06002D09 RID: 11529 RVA: 0x0010E144 File Offset: 0x0010C344
	public ulong seqNum { get; private set; }

	// Token: 0x170005CF RID: 1487
	// (get) Token: 0x06002D0A RID: 11530 RVA: 0x0010E150 File Offset: 0x0010C350
	// (set) Token: 0x06002D0B RID: 11531 RVA: 0x0010E158 File Offset: 0x0010C358
	public int clientDataVersion { get; private set; }

	// Token: 0x170005D0 RID: 1488
	// (get) Token: 0x06002D0C RID: 11532 RVA: 0x0010E164 File Offset: 0x0010C364
	// (set) Token: 0x06002D0D RID: 11533 RVA: 0x0010E16C File Offset: 0x0010C36C
	private protected bool enableUndefinedCompare { private get; protected set; }

	// Token: 0x170005D1 RID: 1489
	// (get) Token: 0x06002D0E RID: 11534 RVA: 0x0010E178 File Offset: 0x0010C378
	public URLRequest urlRequest
	{
		get
		{
			return this.mRequest;
		}
	}

	// Token: 0x170005D2 RID: 1490
	// (get) Token: 0x06002D0F RID: 11535 RVA: 0x0010E180 File Offset: 0x0010C380
	// (set) Token: 0x06002D10 RID: 11536 RVA: 0x0010E188 File Offset: 0x0010C388
	public Action<NetBase> responseEvent { private get; set; }

	// Token: 0x06002D11 RID: 11537 RVA: 0x0010E194 File Offset: 0x0010C394
	public void Request()
	{
		global::Debug.Log("NetBase.Request [" + this + "]", DebugTraceManager.TraceType.SERVER);
		this.state = NetBase.emState.Executing;
		this.mRequest.beginRequest = new Action(this.BeginRequest);
		this.mRequest.success = new Action<WWW>(this.DidSuccess);
		this.mRequest.failure = new Action<WWW, bool, bool>(this.DidFailure);
		this.mRequest.Emulation = this.mEmulation;
		this.elapsedTime = 0f;
		this.mStartTime = Time.realtimeSinceStartup;
		URLRequestManager.Instance.Request(this.mRequest);
	}

	// Token: 0x06002D12 RID: 11538 RVA: 0x0010E23C File Offset: 0x0010C43C
	private void BeginRequest()
	{
		this.paramWriter.WriteObjectStart();
		this.SetCommonRequestParam();
		this.DoRequest();
		this.SetRequestUrl();
		this.SetRequestParam();
		this.SetSecureRequestParam();
		this.paramWriter.Reset();
		this.paramWriter = null;
	}

	// Token: 0x06002D13 RID: 11539 RVA: 0x0010E284 File Offset: 0x0010C484
	private void SetRequestUrl()
	{
		this.mRequest.url = NetBaseUtil.ActionServerURL + this.mActionName;
	}

	// Token: 0x06002D14 RID: 11540 RVA: 0x0010E2A4 File Offset: 0x0010C4A4
	private bool IsJsonFromDll()
	{
		return !string.IsNullOrEmpty(this.mJsonFromDll);
	}

	// Token: 0x06002D15 RID: 11541 RVA: 0x0010E2BC File Offset: 0x0010C4BC
	private bool IsSecure()
	{
		if (!this.IsJsonFromDll())
		{
			return false;
		}
		CPlusPlusLink instance = CPlusPlusLink.Instance;
		return !(instance == null) && instance.IsSecure();
	}

	// Token: 0x06002D16 RID: 11542 RVA: 0x0010E2F8 File Offset: 0x0010C4F8
	protected void SetSecureFlag(bool flag)
	{
		this.m_secureFlag = flag;
	}

	// Token: 0x06002D17 RID: 11543 RVA: 0x0010E304 File Offset: 0x0010C504
	private void SetCommonRequestParam()
	{
		ServerLoginState loginState = ServerInterface.LoginState;
		bool isLoggedIn = loginState.IsLoggedIn;
		string sessionId = loginState.sessionId;
		string version = CurrentBundleVersion.version;
		ulong num = loginState.seqNum + 1UL;
		if (isLoggedIn)
		{
			this.WriteActionParamValue("sessionId", sessionId);
		}
		this.WriteActionParamValue("version", version);
		this.WriteActionParamValue("seq", num);
	}

	// Token: 0x06002D18 RID: 11544 RVA: 0x0010E368 File Offset: 0x0010C568
	private void SetRequestParam()
	{
		this.paramWriter.WriteObjectEnd();
		string text = string.Empty;
		bool flag = this.IsJsonFromDll();
		if (flag)
		{
			text = this.mJsonFromDll;
		}
		else
		{
			text = this.paramWriter.ToString();
		}
		if (0 < text.Length)
		{
			this.mRequest.AddParam("param", text);
		}
	}

	// Token: 0x06002D19 RID: 11545 RVA: 0x0010E3C8 File Offset: 0x0010C5C8
	private void SetSecureRequestParam()
	{
		string value = "0";
		string value2 = string.Empty;
		if (this.IsSecure())
		{
			value = "1";
			value2 = CryptoUtility.code;
		}
		this.mRequest.AddParam("secure", value);
		this.mRequest.AddParam("key", value2);
	}

	// Token: 0x06002D1A RID: 11546 RVA: 0x0010E41C File Offset: 0x0010C61C
	public void DidSuccess(WWW www)
	{
		if (www != null)
		{
			this.DidSuccess(www.text);
		}
		else
		{
			this.DidSuccess(string.Empty);
		}
	}

	// Token: 0x06002D1B RID: 11547 RVA: 0x0010E44C File Offset: 0x0010C64C
	public void DidSuccess(string responseText)
	{
		this.mEndTime = Time.realtimeSinceStartup;
		this.elapsedTime = this.mEndTime - this.mStartTime;
		this.OutputResponseText(responseText);
		this.state = NetBase.emState.Succeeded;
		if (URLRequestManager.Instance.Emulation || this.mEmulation)
		{
			this.DoEmulation();
		}
		else
		{
			if (responseText.Length == 0)
			{
				global::Debug.Log(this + " responce is empty.", DebugTraceManager.TraceType.SERVER);
				this.state = NetBase.emState.UnavailableFailed;
				return;
			}
			if (this.IsUndefinedAction(responseText))
			{
				this.DoEmulation();
			}
			else
			{
				this.mResultJson = JsonMapper.ToObject(responseText);
				bool flag = false;
				if (this.IsJsonFromDll())
				{
					string jsonString = NetUtil.GetJsonString(this.mResultJson, "key");
					CryptoUtility.code = jsonString;
					this.mResultParamJson = this.mResultJson;
					string jsonString2 = NetUtil.GetJsonString(this.mResultJson, "param");
					CPlusPlusLink instance = CPlusPlusLink.Instance;
					if (instance != null && jsonString2 != null)
					{
						global::Debug.Log("CPlusPlusLink.DecodeServerResponseText");
						responseText = instance.DecodeServerResponseText(jsonString2);
						this.mResultParamJson = JsonMapper.ToObject(responseText);
						global::Debug.Log("DecryptString = " + responseText);
						CryptoUtility.code = jsonString2.Substring(0, 16);
						flag = true;
					}
				}
				if (!flag)
				{
					this.mResultParamJson = NetUtil.GetJsonObject(this.mResultJson, "param");
				}
				this.AnalyazeCommonParam(false);
				if (this.IsMaintenance())
				{
					return;
				}
				ServerInterface.StatusCode resultStCd = this.resultStCd;
				switch (resultStCd + 20132)
				{
				case ServerInterface.StatusCode.Ok:
				case (ServerInterface.StatusCode)1:
				case (ServerInterface.StatusCode)2:
					break;
				default:
					switch (resultStCd + 10106)
					{
					case ServerInterface.StatusCode.Ok:
					case (ServerInterface.StatusCode)1:
					case (ServerInterface.StatusCode)2:
						break;
					default:
						if (resultStCd != ServerInterface.StatusCode.AlreadySentEnergy && resultStCd != ServerInterface.StatusCode.AlreadyRequestedEnergy && resultStCd != ServerInterface.StatusCode.AlreadyRemovedPrePurchase && resultStCd != ServerInterface.StatusCode.AlreadyExistedPrePurchase && resultStCd != ServerInterface.StatusCode.VersionForApplication && resultStCd != ServerInterface.StatusCode.ReceiveFailureMessage && resultStCd != ServerInterface.StatusCode.AlreadyInvitedFriend && resultStCd != ServerInterface.StatusCode.RouletteUseLimit && resultStCd != ServerInterface.StatusCode.EnergyLimitPurchaseTrigger && resultStCd != ServerInterface.StatusCode.CharacterLevelLimit)
						{
							if (resultStCd == ServerInterface.StatusCode.TimeOut)
							{
								this.state = NetBase.emState.UnavailableFailed;
								this.result = -7;
								return;
							}
							if (resultStCd != ServerInterface.StatusCode.Ok)
							{
								this.state = NetBase.emState.UnavailableFailed;
								return;
							}
							if (this.mResultParamJson != null)
							{
								this.DoDidSuccess(this.mResultParamJson);
							}
							goto IL_27E;
						}
						break;
					}
					break;
				}
				this.state = NetBase.emState.AvailableFailed;
				if (this.IsEscapeErrorMode())
				{
					this.DoEscapeErrorMode(this.mResultParamJson);
				}
				return;
			}
		}
		IL_27E:
		if (this.responseEvent != null)
		{
			this.responseEvent(this);
		}
	}

	// Token: 0x06002D1C RID: 11548 RVA: 0x0010E6F0 File Offset: 0x0010C8F0
	public void DidFailure(WWW www, bool timeOut, bool notReachability)
	{
		if (notReachability)
		{
			global::Debug.LogWarning("!!!!!!!!!!!!!!!!!!!!!!!!!!! " + this + ".DidFailure : NotReachability", DebugTraceManager.TraceType.SERVER);
			this.resultStCd = ServerInterface.StatusCode.NotReachability;
		}
		else if (timeOut)
		{
			global::Debug.LogWarning("!!!!!!!!!!!!!!!!!!!!!!!!!!! " + this + ".DidFailure : TimeOut", DebugTraceManager.TraceType.SERVER);
			this.resultStCd = ServerInterface.StatusCode.TimeOut;
		}
		else if (www.error != null)
		{
			global::Debug.LogWarning(string.Concat(new object[]
			{
				"!!!!!!!!!!!!!!!!!!!!!!!!!!! ",
				this,
				".DidFailure : ",
				www.error
			}), DebugTraceManager.TraceType.SERVER);
			bool flag = www.error.Contains("400") || www.error.Contains("401") || www.error.Contains("402") || www.error.Contains("403") || www.error.Contains("404") || www.error.Contains("405") || www.error.Contains("406") || www.error.Contains("407") || www.error.Contains("408") || www.error.Contains("409") || www.error.Contains("410") || www.error.Contains("411") || www.error.Contains("412") || www.error.Contains("413") || www.error.Contains("414") || www.error.Contains("415");
			bool flag2 = www.error.Contains("502") || www.error.Contains("503") || www.error.Contains("504");
			bool flag3 = www.error.Contains("500") || www.error.Contains("501") || www.error.Contains("505");
			if (flag)
			{
				this.resultStCd = ServerInterface.StatusCode.CliendError;
			}
			else if (flag3)
			{
				this.resultStCd = ServerInterface.StatusCode.InternalServerError;
			}
			else if (flag2)
			{
				this.resultStCd = ServerInterface.StatusCode.ServerBusy;
			}
			else if (www.error.Contains("unreachable"))
			{
				this.resultStCd = ServerInterface.StatusCode.NotReachability;
			}
			else
			{
				this.resultStCd = ServerInterface.StatusCode.NotReachability;
			}
		}
		else
		{
			this.resultStCd = ServerInterface.StatusCode.OtherError;
		}
		this.state = NetBase.emState.UnavailableFailed;
	}

	// Token: 0x06002D1D RID: 11549 RVA: 0x0010E9D4 File Offset: 0x0010CBD4
	private void DoEmulation()
	{
		global::Debug.LogWarning(this + ".DidSuccess : Emulation", DebugTraceManager.TraceType.SERVER);
		this.AnalyazeCommonParam(true);
		if (this.resultStCd == ServerInterface.StatusCode.Ok)
		{
			this.DoDidSuccessEmulation();
		}
	}

	// Token: 0x06002D1E RID: 11550 RVA: 0x0010EA00 File Offset: 0x0010CC00
	private bool IsUndefinedAction(string result)
	{
		return this.enableUndefinedCompare && result.Contains(NetBase.mUndefinedComparer);
	}

	// Token: 0x06002D1F RID: 11551 RVA: 0x0010EA30 File Offset: 0x0010CC30
	private void OutputResponseText(string text)
	{
		if (text == null)
		{
			return;
		}
	}

	// Token: 0x06002D20 RID: 11552 RVA: 0x0010EA44 File Offset: 0x0010CC44
	private void AnalyazeCommonParam(bool emulation)
	{
		this.result = 0;
		this.errorMessage = string.Empty;
		this.meintenanceValue = 0;
		this.dataVersion = -1;
		this.meintenanceMessage = string.Empty;
		this.versionString = "1.0.0";
		this.infoVersionString = string.Empty;
		this.versionValue = NetBaseUtil.GetVersionValue(this.versionString);
		this.assetVersionString = "1.0.0";
		this.assetVersionValue = NetBaseUtil.GetVersionValue(this.assetVersionString);
		this.serverTime = 0L;
		this.seqNum = 0UL;
		ServerInterface.LoginState.serverVersionValue = this.versionValue;
		if (!emulation)
		{
			this.result = NetUtil.GetJsonInt(this.mResultParamJson, "statusCode");
			this.errorMessage = NetUtil.GetJsonString(this.mResultParamJson, "errorMessage");
			this.meintenanceValue = NetUtil.GetJsonInt(this.mResultParamJson, "maintenance");
			this.dataVersion = NetUtil.GetJsonInt(this.mResultParamJson, "data_version");
			this.meintenanceMessage = NetUtil.GetJsonString(this.mResultParamJson, "maintenance_text");
			this.versionString = NetUtil.GetJsonString(this.mResultParamJson, "version");
			this.versionValue = NetBaseUtil.GetVersionValue(this.versionString);
			this.assetVersionString = NetUtil.GetJsonString(this.mResultParamJson, "assets_version");
			this.assetVersionValue = NetBaseUtil.GetVersionValue(this.assetVersionString);
			this.infoVersionString = NetUtil.GetJsonString(this.mResultParamJson, "info_version");
			this.serverTime = NetUtil.GetJsonLong(this.mResultParamJson, "server_time");
			string jsonString = NetUtil.GetJsonString(this.mResultParamJson, this.m_netBaseStr + "Url");
			string jsonString2 = NetUtil.GetJsonString(this.mResultParamJson, this.m_netBaseStr + this.m_eMsgStr);
			string jsonString3 = NetUtil.GetJsonString(this.mResultParamJson, this.m_netBaseStr + this.m_jMsgStr);
			this.seqNum = (ulong)NetUtil.GetJsonLong(this.mResultParamJson, "seq");
			this.clientDataVersion = NetUtil.GetJsonInt(this.mResultParamJson, "client_data_version");
			NetBase.LastNetServerTime = this.serverTime;
			NetBase.TimeDifference = this.serverTime - (long)NetUtil.GetCurrentUnixTime();
			ServerInterface.LoginState.seqNum = this.seqNum;
			ServerInterface.LoginState.DataVersion = this.dataVersion;
			ServerInterface.LoginState.AssetsVersion = this.assetVersionValue;
			ServerInterface.LoginState.AssetsVersionString = this.assetVersionString;
			ServerInterface.LoginState.InfoVersionString = this.infoVersionString;
			ServerInterface.NextVersionState.m_buyRSRNum = NetUtil.GetJsonInt(this.mResultParamJson, "numBuyRedRingsANDROID");
			ServerInterface.NextVersionState.m_freeRSRNum = NetUtil.GetJsonInt(this.mResultParamJson, "numRedRingsANDROID");
			ServerInterface.NextVersionState.m_userName = NetUtil.GetJsonString(this.mResultParamJson, "userName");
			ServerInterface.NextVersionState.m_url = jsonString;
			ServerInterface.NextVersionState.m_eMsg = jsonString2;
			ServerInterface.NextVersionState.m_jMsg = jsonString3;
		}
		else
		{
			this.serverTime = (long)NetUtil.GetCurrentUnixTime();
			NetBase.LastNetServerTime = this.serverTime;
			NetBase.TimeDifference = this.serverTime - (long)NetUtil.GetCurrentUnixTime();
		}
		if (this.result != 0)
		{
			global::Debug.LogWarning(string.Concat(new object[]
			{
				">>>>>>>>>>>>> ",
				this.ToString(),
				" : Result = ",
				this.result,
				" <<<<<<<<<<<<<"
			}));
		}
	}

	// Token: 0x06002D21 RID: 11553 RVA: 0x0010EDA4 File Offset: 0x0010CFA4
	public bool IsExecuting()
	{
		return this.state == NetBase.emState.Executing;
	}

	// Token: 0x06002D22 RID: 11554 RVA: 0x0010EDB4 File Offset: 0x0010CFB4
	public bool IsSucceeded()
	{
		return this.state == NetBase.emState.Succeeded && !this.IsMaintenance();
	}

	// Token: 0x06002D23 RID: 11555 RVA: 0x0010EDD0 File Offset: 0x0010CFD0
	public bool IsFailed()
	{
		return this.state == NetBase.emState.AvailableFailed || this.state == NetBase.emState.UnavailableFailed;
	}

	// Token: 0x06002D24 RID: 11556 RVA: 0x0010EDF0 File Offset: 0x0010CFF0
	public bool IsAvailableFailed()
	{
		return this.state == NetBase.emState.AvailableFailed;
	}

	// Token: 0x06002D25 RID: 11557 RVA: 0x0010EE04 File Offset: 0x0010D004
	public bool IsUnavailableFailed()
	{
		return this.state == NetBase.emState.UnavailableFailed;
	}

	// Token: 0x06002D26 RID: 11558 RVA: 0x0010EE18 File Offset: 0x0010D018
	public bool IsNotReachability()
	{
		return this.IsFailed() && this.resultStCd == ServerInterface.StatusCode.NotReachability;
	}

	// Token: 0x06002D27 RID: 11559 RVA: 0x0010EE38 File Offset: 0x0010D038
	public bool IsNoAccessTimeOut()
	{
		return this.IsFailed() && (this.resultStCd == ServerInterface.StatusCode.ExpirationSession || this.resultStCd == ServerInterface.StatusCode.TimeOut);
	}

	// Token: 0x06002D28 RID: 11560 RVA: 0x0010EE68 File Offset: 0x0010D068
	public bool IsNeededLoginFailed()
	{
		if (this.IsFailed())
		{
		}
		return false;
	}

	// Token: 0x06002D29 RID: 11561 RVA: 0x0010EE78 File Offset: 0x0010D078
	public bool IsMaintenance()
	{
		return 0 != this.meintenanceValue;
	}

	// Token: 0x06002D2A RID: 11562 RVA: 0x0010EE88 File Offset: 0x0010D088
	protected void SetAction(string action)
	{
		this.mActionName = action + "/?";
	}

	// Token: 0x06002D2B RID: 11563 RVA: 0x0010EE9C File Offset: 0x0010D09C
	protected void WriteActionParamValue(string propertyName, object value)
	{
		NetUtil.WriteValue(this.paramWriter, propertyName, value);
	}

	// Token: 0x06002D2C RID: 11564 RVA: 0x0010EEAC File Offset: 0x0010D0AC
	protected void WriteActionParamObject(string objectName, Dictionary<string, object> properties)
	{
		NetUtil.WriteObject(this.paramWriter, objectName, properties);
	}

	// Token: 0x06002D2D RID: 11565 RVA: 0x0010EEBC File Offset: 0x0010D0BC
	protected void WriteActionParamArray(string arrayName, List<object> objects)
	{
		NetUtil.WriteArray(this.paramWriter, arrayName, objects);
	}

	// Token: 0x06002D2E RID: 11566 RVA: 0x0010EECC File Offset: 0x0010D0CC
	protected void WriteJsonString(string jsonString)
	{
		this.mJsonFromDll = jsonString;
	}

	// Token: 0x06002D2F RID: 11567 RVA: 0x0010EED8 File Offset: 0x0010D0D8
	protected void DisplayDebugInfo()
	{
		this.mDebugLogDisplayLevel = 0;
		this.DebugLogInner(this.mResultParamJson);
	}

	// Token: 0x06002D30 RID: 11568 RVA: 0x0010EEF0 File Offset: 0x0010D0F0
	private void DebugLogInner(JsonData jdata)
	{
		this.mDebugLogDisplayLevel++;
		string text = string.Empty;
		for (int i = 0; i < this.mDebugLogDisplayLevel; i++)
		{
			text += "  ";
		}
		string text2 = string.Empty;
		string text3 = string.Empty;
		int count = jdata.Count;
		for (int j = 0; j < count; j++)
		{
			JsonData jsonData = jdata[j];
			if (jsonData.IsArray)
			{
				text2 = jdata.GetKey(j);
				global::Debug.Log(text + "ARRAY  key[" + text2 + "]", DebugTraceManager.TraceType.SERVER);
			}
			else if (jsonData.IsInt)
			{
				text2 = jdata.GetKey(j);
				text3 = (string)jdata[j];
				global::Debug.Log(string.Concat(new string[]
				{
					text,
					"INT    key[",
					text2,
					"]  value[]",
					text3
				}), DebugTraceManager.TraceType.SERVER);
			}
			else if (jsonData.IsLong)
			{
				text2 = jdata.GetKey(j);
				text3 = (string)jdata[j];
				global::Debug.Log(string.Concat(new string[]
				{
					text,
					"LONG   key[",
					text2,
					"]  value[]",
					text3
				}), DebugTraceManager.TraceType.SERVER);
			}
			else if (jsonData.IsObject)
			{
				global::Debug.Log(string.Concat(new object[]
				{
					text,
					"OBJECT[",
					j,
					"]"
				}), DebugTraceManager.TraceType.SERVER);
			}
			else if (jsonData.IsString)
			{
				text2 = jdata.GetKey(j);
				text3 = (string)jdata[j];
				global::Debug.Log(string.Concat(new string[]
				{
					text,
					"STRING :  key[",
					text2,
					"]  value[",
					text3,
					"]"
				}), DebugTraceManager.TraceType.SERVER);
			}
		}
		this.mDebugLogDisplayLevel--;
	}

	// Token: 0x06002D31 RID: 11569 RVA: 0x0010F0EC File Offset: 0x0010D2EC
	protected bool GetFlag(JsonData jdata, string key)
	{
		return NetUtil.GetJsonInt(jdata, key) != 0;
	}

	// Token: 0x06002D32 RID: 11570 RVA: 0x0010F10C File Offset: 0x0010D30C
	protected virtual bool IsEscapeErrorMode()
	{
		return false;
	}

	// Token: 0x06002D33 RID: 11571 RVA: 0x0010F110 File Offset: 0x0010D310
	protected virtual void DoEscapeErrorMode(JsonData jdata)
	{
	}

	// Token: 0x06002D34 RID: 11572
	protected abstract void DoRequest();

	// Token: 0x06002D35 RID: 11573
	protected abstract void DoDidSuccess(JsonData jdata);

	// Token: 0x06002D36 RID: 11574
	protected abstract void DoDidSuccessEmulation();

	// Token: 0x170005D3 RID: 1491
	// (get) Token: 0x06002D37 RID: 11575 RVA: 0x0010F114 File Offset: 0x0010D314
	// (set) Token: 0x06002D38 RID: 11576 RVA: 0x0010F11C File Offset: 0x0010D31C
	public static long LastNetServerTime { get; set; }

	// Token: 0x170005D4 RID: 1492
	// (get) Token: 0x06002D39 RID: 11577 RVA: 0x0010F124 File Offset: 0x0010D324
	// (set) Token: 0x06002D3A RID: 11578 RVA: 0x0010F12C File Offset: 0x0010D32C
	private static long TimeDifference { get; set; }

	// Token: 0x06002D3B RID: 11579 RVA: 0x0010F134 File Offset: 0x0010D334
	public static DateTime GetCurrentTime()
	{
		return NetUtil.GetLocalDateTime((long)NetUtil.GetCurrentUnixTime() + NetBase.TimeDifference);
	}

	// Token: 0x0400298C RID: 10636
	private static string mUndefinedComparer = "ERROR_CODE(32)";

	// Token: 0x0400298D RID: 10637
	private string m_netBaseStr = "close";

	// Token: 0x0400298E RID: 10638
	private string m_jMsgStr = "MessageJP";

	// Token: 0x0400298F RID: 10639
	private string m_eMsgStr = "MessageEN";

	// Token: 0x04002990 RID: 10640
	protected bool mEmulation;

	// Token: 0x04002991 RID: 10641
	private URLRequest mRequest;

	// Token: 0x04002992 RID: 10642
	private JsonData mResultJson;

	// Token: 0x04002993 RID: 10643
	private JsonData mResultParamJson;

	// Token: 0x04002994 RID: 10644
	private string mActionName;

	// Token: 0x04002995 RID: 10645
	private string mJsonFromDll = string.Empty;

	// Token: 0x04002996 RID: 10646
	private int mDebugLogDisplayLevel;

	// Token: 0x04002997 RID: 10647
	private bool mMaintenance;

	// Token: 0x04002998 RID: 10648
	private float mStartTime;

	// Token: 0x04002999 RID: 10649
	private float mEndTime;

	// Token: 0x0400299A RID: 10650
	private bool m_secureFlag = true;

	// Token: 0x02000692 RID: 1682
	public enum emState
	{
		// Token: 0x040029B0 RID: 10672
		Executing,
		// Token: 0x040029B1 RID: 10673
		Succeeded,
		// Token: 0x040029B2 RID: 10674
		AvailableFailed,
		// Token: 0x040029B3 RID: 10675
		UnavailableFailed
	}
}
