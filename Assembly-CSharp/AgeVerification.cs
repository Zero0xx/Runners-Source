using System;
using Text;
using UnityEngine;

// Token: 0x0200033E RID: 830
public class AgeVerification : MonoBehaviour
{
	// Token: 0x170003B9 RID: 953
	// (get) Token: 0x060018A6 RID: 6310 RVA: 0x0008E728 File Offset: 0x0008C928
	// (set) Token: 0x060018A7 RID: 6311 RVA: 0x0008E764 File Offset: 0x0008C964
	public static bool IsAgeVerificated
	{
		get
		{
			ServerSettingState serverSettingState = null;
			ServerInterface loggedInServerInterface = ServerInterface.LoggedInServerInterface;
			if (loggedInServerInterface != null)
			{
				serverSettingState = ServerInterface.SettingState;
			}
			return !string.IsNullOrEmpty(serverSettingState.m_birthday);
		}
		private set
		{
		}
	}

	// Token: 0x060018A8 RID: 6312 RVA: 0x0008E768 File Offset: 0x0008C968
	public void Setup(string anchorPath)
	{
		this.m_anchorPath = anchorPath;
		this.m_ageVerification = base.gameObject.GetComponent<AgeVerificationWindow>();
		if (this.m_ageVerification == null)
		{
			this.m_ageVerification = base.gameObject.AddComponent<AgeVerificationWindow>();
		}
		this.m_ageVerification.Setup(anchorPath);
	}

	// Token: 0x060018A9 RID: 6313 RVA: 0x0008E7BC File Offset: 0x0008C9BC
	public void PlayStart()
	{
		base.gameObject.SetActive(true);
		this.m_state = AgeVerification.State.APOLLO_SEND_START;
	}

	// Token: 0x170003BA RID: 954
	// (get) Token: 0x060018AA RID: 6314 RVA: 0x0008E7D4 File Offset: 0x0008C9D4
	public bool IsEnd
	{
		get
		{
			return this.m_state == AgeVerification.State.END;
		}
	}

	// Token: 0x060018AB RID: 6315 RVA: 0x0008E7E8 File Offset: 0x0008C9E8
	private void Start()
	{
	}

	// Token: 0x060018AC RID: 6316 RVA: 0x0008E7EC File Offset: 0x0008C9EC
	private void Update()
	{
		switch (this.m_state)
		{
		case AgeVerification.State.APOLLO_SEND_START:
		{
			string[] value = new string[1];
			SendApollo.GetTutorialValue(ApolloTutorialIndex.START_STEP3, ref value);
			this.m_sendApollo = SendApollo.CreateRequest(ApolloType.TUTORIAL_START, value);
			this.m_state = AgeVerification.State.APOLLO_SEND_START_WAIT;
			break;
		}
		case AgeVerification.State.APOLLO_SEND_START_WAIT:
			if (this.m_sendApollo != null && this.m_sendApollo.IsEnd())
			{
				UnityEngine.Object.Destroy(this.m_sendApollo.gameObject);
				this.m_sendApollo = null;
				this.m_state = AgeVerification.State.CAUTION_AGE_VERIFICATION;
			}
			break;
		case AgeVerification.State.CAUTION_AGE_VERIFICATION:
			GeneralWindow.Create(new GeneralWindow.CInfo
			{
				caption = TextUtility.GetCommonText("Shop", "gw_age_verification_caption"),
				message = TextUtility.GetCommonText("Shop", "gw_age_verification_text"),
				buttonType = GeneralWindow.ButtonType.Ok
			});
			this.m_state = AgeVerification.State.CAUTION_AGE_VERIFICATION_WAIT;
			break;
		case AgeVerification.State.CAUTION_AGE_VERIFICATION_WAIT:
			if (GeneralWindow.IsOkButtonPressed)
			{
				GeneralWindow.Close();
				this.m_state = AgeVerification.State.AGE_VERIFICATION;
			}
			break;
		case AgeVerification.State.AGE_VERIFICATION:
			if (this.m_ageVerification != null && this.m_ageVerification.IsReady)
			{
				this.m_ageVerification.PlayStart();
				this.m_state = AgeVerification.State.AGE_VERIFICATION_WAIT;
			}
			break;
		case AgeVerification.State.AGE_VERIFICATION_WAIT:
			if (this.m_ageVerification != null && this.m_ageVerification.IsEnd)
			{
				this.m_state = AgeVerification.State.APOLLO_SEND_END;
			}
			break;
		case AgeVerification.State.APOLLO_SEND_END:
		{
			string[] value2 = new string[1];
			SendApollo.GetTutorialValue(ApolloTutorialIndex.START_STEP3, ref value2);
			this.m_sendApollo = SendApollo.CreateRequest(ApolloType.TUTORIAL_END, value2);
			this.m_state = AgeVerification.State.APOLLO_SEND_END_WAIT;
			break;
		}
		case AgeVerification.State.APOLLO_SEND_END_WAIT:
			if (this.m_sendApollo != null && this.m_sendApollo.IsEnd())
			{
				UnityEngine.Object.Destroy(this.m_sendApollo.gameObject);
				this.m_sendApollo = null;
				this.m_state = AgeVerification.State.FINISHED_AGE_VERIFICATION;
			}
			break;
		case AgeVerification.State.FINISHED_AGE_VERIFICATION:
			GeneralWindow.Create(new GeneralWindow.CInfo
			{
				caption = TextUtility.GetCommonText("Shop", "gw_age_verification_success_caption"),
				message = TextUtility.GetCommonText("Shop", "gw_age_verification_success_text"),
				anchor_path = this.m_anchorPath,
				buttonType = GeneralWindow.ButtonType.Ok
			});
			this.m_state = AgeVerification.State.FINISHED_AGE_VERIFICATION_WAIT;
			break;
		case AgeVerification.State.FINISHED_AGE_VERIFICATION_WAIT:
			if (GeneralWindow.IsOkButtonPressed)
			{
				GeneralWindow.Close();
				base.gameObject.SetActive(false);
				this.m_state = AgeVerification.State.END;
			}
			break;
		}
	}

	// Token: 0x04001611 RID: 5649
	private SendApollo m_sendApollo;

	// Token: 0x04001612 RID: 5650
	private string m_anchorPath;

	// Token: 0x04001613 RID: 5651
	private AgeVerificationWindow m_ageVerification;

	// Token: 0x04001614 RID: 5652
	private AgeVerification.State m_state;

	// Token: 0x0200033F RID: 831
	private enum State
	{
		// Token: 0x04001616 RID: 5654
		NONE = -1,
		// Token: 0x04001617 RID: 5655
		IDLE,
		// Token: 0x04001618 RID: 5656
		APOLLO_SEND_START,
		// Token: 0x04001619 RID: 5657
		APOLLO_SEND_START_WAIT,
		// Token: 0x0400161A RID: 5658
		CAUTION_AGE_VERIFICATION,
		// Token: 0x0400161B RID: 5659
		CAUTION_AGE_VERIFICATION_WAIT,
		// Token: 0x0400161C RID: 5660
		AGE_VERIFICATION,
		// Token: 0x0400161D RID: 5661
		AGE_VERIFICATION_WAIT,
		// Token: 0x0400161E RID: 5662
		APOLLO_SEND_END,
		// Token: 0x0400161F RID: 5663
		APOLLO_SEND_END_WAIT,
		// Token: 0x04001620 RID: 5664
		FINISHED_AGE_VERIFICATION,
		// Token: 0x04001621 RID: 5665
		FINISHED_AGE_VERIFICATION_WAIT,
		// Token: 0x04001622 RID: 5666
		END,
		// Token: 0x04001623 RID: 5667
		COUNT
	}
}
