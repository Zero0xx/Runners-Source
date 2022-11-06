using System;
using DataTable;
using Text;
using UnityEngine;

// Token: 0x020003F7 RID: 1015
public class ui_chao_set_cell : MonoBehaviour
{
	// Token: 0x06001E1E RID: 7710 RVA: 0x000B0F4C File Offset: 0x000AF14C
	public static void ResetLastLoadTime()
	{
		ui_chao_set_cell.s_lastLoadTime = 0f;
		ui_chao_set_cell.s_loadLock = true;
	}

	// Token: 0x06001E1F RID: 7711 RVA: 0x000B0F60 File Offset: 0x000AF160
	private void Start()
	{
		Animation animation = GameObjectUtil.FindChildGameObjectComponent<Animation>(base.transform.root.gameObject, "chao_set_window");
		UIPlayAnimation component = base.gameObject.GetComponent<UIPlayAnimation>();
		if (animation != null && component != null)
		{
			component.target = animation;
		}
	}

	// Token: 0x06001E20 RID: 7712 RVA: 0x000B0FB4 File Offset: 0x000AF1B4
	private void Update()
	{
		if (this.m_chaoId >= 0 && !ui_chao_set_cell.s_loadLock)
		{
			if (!this.m_isLoadCmp)
			{
				if (this.m_chaoDefault != null && !this.m_chaoDefault.gameObject.activeSelf && this.m_chaoTex != null && this.m_chaoTex.alpha <= 0f)
				{
					this.m_chaoDefault.gameObject.SetActive(true);
				}
				if (this.m_isLoad)
				{
					this.m_loadingTime += Time.deltaTime;
					if (this.m_loadingTime > 1f)
					{
						ChaoTextureManager.CallbackInfo.LoadFinishCallback callback = new ChaoTextureManager.CallbackInfo.LoadFinishCallback(this.ChaoLoadFinishCallback);
						ChaoTextureManager.CallbackInfo info = new ChaoTextureManager.CallbackInfo(null, callback, false);
						ChaoTextureManager.Instance.GetTexture(this.m_chaoId, info);
						this.m_loadingTime = 0.001f;
					}
				}
			}
			if (!this.m_isDrawChao)
			{
				if (this.m_isLoad && this.m_chaoTextureData != null)
				{
					if (this.IsDisp())
					{
						this.m_drawDelay = 0.02f;
					}
					else
					{
						this.m_drawDelay = 0.04f;
					}
					this.m_isDrawChao = true;
					if (this.m_chaoDefault != null && !this.m_chaoDefault.gameObject.activeSelf)
					{
						this.m_chaoDefault.gameObject.SetActive(true);
					}
				}
				else if (!this.m_isLoad && this.IsDraw())
				{
					float num = 0.05f;
					if (!this.IsDisp())
					{
						num = 0.3f;
					}
					if (ui_chao_set_cell.s_lastLoadTime <= 0f)
					{
						if (ui_chao_set_cell.s_lastLoadTime < 0f)
						{
							ui_chao_set_cell.s_lastLoadTime = Time.realtimeSinceStartup + 1f;
						}
						else
						{
							ui_chao_set_cell.s_lastLoadTime = Time.realtimeSinceStartup + 0.5f;
						}
					}
					else if (ui_chao_set_cell.s_lastLoadTime + num < Time.realtimeSinceStartup)
					{
						ChaoTextureManager.CallbackInfo.LoadFinishCallback callback2 = new ChaoTextureManager.CallbackInfo.LoadFinishCallback(this.ChaoLoadFinishCallback);
						ChaoTextureManager.CallbackInfo info2 = new ChaoTextureManager.CallbackInfo(null, callback2, false);
						ChaoTextureManager.Instance.GetTexture(this.m_chaoId, info2);
						this.m_loadingTime = 0f;
						this.m_isLoad = true;
						this.m_isLoadCmp = false;
						ui_chao_set_cell.s_lastLoadTime = Time.realtimeSinceStartup;
					}
				}
			}
			else if (this.m_drawDelay > 0f)
			{
				this.m_drawDelay -= Time.deltaTime;
				if (this.m_drawDelay <= 0f)
				{
					if (GeneralUtil.CheckChaoTexture(this.m_chaoTextureData, this.m_chaoData.id))
					{
						this.m_drawDelay = -1f;
						this.m_loadingTime = -1f;
						this.m_isLoadCmp = true;
						this.m_checkDelay = 1f;
						this.m_chaoTex.mainTexture = this.m_chaoTextureData;
					}
					else
					{
						ChaoTextureManager.CallbackInfo.LoadFinishCallback callback3 = new ChaoTextureManager.CallbackInfo.LoadFinishCallback(this.ChaoReloadFinishCallback);
						ChaoTextureManager.CallbackInfo info3 = new ChaoTextureManager.CallbackInfo(null, callback3, false);
						ChaoTextureManager.Instance.GetTexture(this.m_chaoData.id, info3);
					}
				}
			}
		}
		if (this.m_isLoadCmp && this.m_checkDelay > 0f)
		{
			this.m_checkDelay -= Time.deltaTime;
			if (this.m_checkDelay <= 0f)
			{
				this.m_checkDelay = 0f;
				if (!GeneralUtil.CheckChaoTexture(this.m_chaoTex, this.m_chaoData.id))
				{
					ChaoTextureManager.CallbackInfo.LoadFinishCallback callback4 = new ChaoTextureManager.CallbackInfo.LoadFinishCallback(this.ChaoReloadFinishCallback);
					ChaoTextureManager.CallbackInfo info4 = new ChaoTextureManager.CallbackInfo(null, callback4, false);
					ChaoTextureManager.Instance.GetTexture(this.m_chaoData.id, info4);
				}
				else
				{
					this.m_chaoTex.alpha = 1f;
					if (this.m_chaoDefault != null)
					{
						this.m_chaoDefault.gameObject.SetActive(false);
					}
				}
			}
		}
	}

	// Token: 0x06001E21 RID: 7713 RVA: 0x000B138C File Offset: 0x000AF58C
	private bool IsDraw()
	{
		bool result = false;
		if (this.m_parentPanel != null)
		{
			if (!this.m_isDraw)
			{
				float num = this.m_parentPanel.transform.localPosition.y * -1f;
				float w = this.m_parentPanel.clipRange.w;
				float num2 = num - w;
				float y = base.gameObject.transform.localPosition.y;
				if (y > num2)
				{
					this.m_isDraw = true;
				}
			}
			result = this.m_isDraw;
		}
		return result;
	}

	// Token: 0x06001E22 RID: 7714 RVA: 0x000B1424 File Offset: 0x000AF624
	private bool IsDisp()
	{
		bool result = false;
		if (this.m_parentPanel != null && this.m_isDraw)
		{
			float num = this.m_parentPanel.transform.localPosition.y * -1f;
			float num2 = this.m_parentPanel.clipRange.w * 1.2f;
			float num3 = num - num2;
			float y = base.gameObject.transform.localPosition.y;
			if (y > num3 && y < num3 + num2)
			{
				result = true;
			}
		}
		return result;
	}

	// Token: 0x06001E23 RID: 7715 RVA: 0x000B14C0 File Offset: 0x000AF6C0
	private void UpdateView(ChaoData chaoData, int mainChaoId, int subChaoId, UIPanel parentPanel)
	{
		ui_chao_set_cell.s_loadLock = false;
		this.m_isDraw = false;
		this.m_drawDelay = -1f;
		this.m_isDrawChao = false;
		this.m_chaoId = -1;
		this.m_loadingTime = 0f;
		this.m_isLoad = false;
		this.m_isLoadCmp = false;
		this.m_chaoTextureData = null;
		this.m_parentPanel = parentPanel;
		if (chaoData != null)
		{
			this.m_chaoId = chaoData.id;
		}
		ChaoTextureManager instance = ChaoTextureManager.Instance;
		Texture texture = null;
		if (instance != null)
		{
			texture = instance.GetLoadedTexture(chaoData.id);
		}
		if (this.m_chaoTex == null)
		{
			this.m_chaoTex = GameObjectUtil.FindChildGameObjectComponent<UITexture>(base.gameObject, "img_chao_tex");
		}
		this.m_chaoDefault = GameObjectUtil.FindChildGameObjectComponent<UISprite>(base.gameObject, "img_chao_default");
		if (this.m_chaoDefault != null)
		{
			this.m_chaoDefault.gameObject.SetActive(true);
		}
		if (texture != null)
		{
			this.m_drawDelay = 0.005f;
			this.m_chaoTextureData = texture;
			this.m_isLoad = true;
			this.m_isDraw = true;
			this.m_isDrawChao = true;
		}
		if (chaoData != null)
		{
			this.m_chaoRankSprite.spriteName = "ui_chao_set_bg_s_" + (int)chaoData.rarity;
		}
		if (chaoData != null && chaoData.IsValidate)
		{
			this.m_chaoData = chaoData;
			this.m_setSprite.spriteName = ((chaoData.id != mainChaoId) ? ((chaoData.id != subChaoId) ? null : "ui_chao_set_cursor_1") : "ui_chao_set_cursor_0");
			this.m_chaoTex.color = Color.white;
			this.m_chaoDefault.color = Color.white;
			this.m_chaoLevelLabel.text = TextUtility.GetTextLevel(chaoData.level.ToString());
			string str = this.m_chaoData.charaAtribute.ToString().ToLower();
			this.m_chaoTypeSprite.spriteName = "ui_chao_set_type_icon_" + str;
			if (this.m_bonusTypeSprite != null)
			{
				this.m_bonusTypeSprite.gameObject.SetActive(false);
			}
			this.m_bonusLabel.enabled = false;
			this.m_disabledSprite.SetActive(false);
			this.m_buttonScale.enabled = true;
		}
		else if (chaoData != null && !chaoData.IsValidate)
		{
			this.m_chaoData = chaoData;
			this.m_setSprite.spriteName = null;
			this.m_chaoTex.color = Color.black;
			this.m_chaoDefault.color = Color.black;
			this.m_chaoLevelLabel.text = string.Empty;
			string str2 = this.m_chaoData.charaAtribute.ToString().ToLower();
			this.m_chaoTypeSprite.spriteName = "ui_chao_set_type_icon_" + str2;
			if (this.m_bonusTypeSprite != null)
			{
				this.m_bonusTypeSprite.gameObject.SetActive(false);
			}
			this.m_bonusLabel.enabled = false;
			this.m_disabledSprite.SetActive(true);
			this.m_buttonScale.enabled = true;
		}
		else
		{
			this.m_chaoData = null;
			this.m_setSprite.spriteName = null;
			this.m_chaoTex.color = Color.black;
			this.m_chaoDefault.color = Color.black;
			this.m_chaoLevelLabel.text = string.Empty;
			this.m_chaoTypeSprite.spriteName = null;
			this.m_bonusTypeSprite.spriteName = null;
			this.m_bonusLabel.text = string.Empty;
			this.m_disabledSprite.SetActive(true);
			this.m_buttonScale.enabled = false;
		}
		this.m_chaoTex.alpha = 0.001f;
	}

	// Token: 0x06001E24 RID: 7716 RVA: 0x000B1874 File Offset: 0x000AFA74
	private void ChaoLoadFinishCallback(Texture tex)
	{
		this.m_chaoTextureData = tex;
	}

	// Token: 0x06001E25 RID: 7717 RVA: 0x000B1880 File Offset: 0x000AFA80
	private void ChaoReloadFinishCallback(Texture tex)
	{
		this.m_chaoTextureData = tex;
		this.m_chaoTex.mainTexture = this.m_chaoTextureData;
		this.m_drawDelay = -1f;
		this.m_loadingTime = -1f;
		this.m_isLoadCmp = true;
		this.m_checkDelay = 1f;
	}

	// Token: 0x06001E26 RID: 7718 RVA: 0x000B18D0 File Offset: 0x000AFAD0
	public void UpdateView(int id, int mainChaoId, int subChaoId, UIPanel parentPanel)
	{
		this.UpdateView(ChaoTable.GetChaoData(id), mainChaoId, subChaoId, parentPanel);
	}

	// Token: 0x06001E27 RID: 7719 RVA: 0x000B18E4 File Offset: 0x000AFAE4
	private void OnClick()
	{
		if (this.m_chaoData == null)
		{
			return;
		}
		ChaoSetWindowUI window = ChaoSetWindowUI.GetWindow();
		if (window != null)
		{
			ChaoSetWindowUI.ChaoInfo chaoInfo = new ChaoSetWindowUI.ChaoInfo(this.m_chaoData);
			if (!this.m_chaoData.IsValidate)
			{
				chaoInfo.level = 0;
				chaoInfo.detail = this.m_chaoData.GetDetailLevelPlusSP(0);
				chaoInfo.name = TextManager.GetText(TextManager.TextType.TEXTTYPE_MILEAGE_MAP_COMMON, "Name", "name_question").text;
				chaoInfo.onMask = true;
				window.OpenWindow(chaoInfo, ChaoSetWindowUI.WindowType.WINDOW_ONLY);
			}
			else
			{
				window.OpenWindow(chaoInfo, ChaoSetWindowUI.WindowType.WITH_BUTTON);
			}
		}
	}

	// Token: 0x04001B70 RID: 7024
	private const float IMAGE_LOAD_DELAY = 0.02f;

	// Token: 0x04001B71 RID: 7025
	private static float s_lastLoadTime = -1f;

	// Token: 0x04001B72 RID: 7026
	private static bool s_loadLock = true;

	// Token: 0x04001B73 RID: 7027
	[SerializeField]
	private UISprite m_setSprite;

	// Token: 0x04001B74 RID: 7028
	[SerializeField]
	private UISprite m_chaoRankSprite;

	// Token: 0x04001B75 RID: 7029
	[SerializeField]
	private UILabel m_chaoLevelLabel;

	// Token: 0x04001B76 RID: 7030
	[SerializeField]
	private UISprite m_chaoTypeSprite;

	// Token: 0x04001B77 RID: 7031
	[SerializeField]
	private UISprite m_bonusTypeSprite;

	// Token: 0x04001B78 RID: 7032
	[SerializeField]
	private UILabel m_bonusLabel;

	// Token: 0x04001B79 RID: 7033
	[SerializeField]
	private GameObject m_disabledSprite;

	// Token: 0x04001B7A RID: 7034
	[SerializeField]
	private UIButtonScale m_buttonScale;

	// Token: 0x04001B7B RID: 7035
	private ChaoData m_chaoData;

	// Token: 0x04001B7C RID: 7036
	private bool m_isLoad;

	// Token: 0x04001B7D RID: 7037
	private bool m_isLoadCmp;

	// Token: 0x04001B7E RID: 7038
	private bool m_isDraw;

	// Token: 0x04001B7F RID: 7039
	private bool m_isDrawChao;

	// Token: 0x04001B80 RID: 7040
	private float m_loadingTime;

	// Token: 0x04001B81 RID: 7041
	private float m_drawDelay = -1f;

	// Token: 0x04001B82 RID: 7042
	private float m_checkDelay;

	// Token: 0x04001B83 RID: 7043
	private int m_chaoId = -1;

	// Token: 0x04001B84 RID: 7044
	private UITexture m_chaoTex;

	// Token: 0x04001B85 RID: 7045
	private Texture m_chaoTextureData;

	// Token: 0x04001B86 RID: 7046
	private UISprite m_chaoDefault;

	// Token: 0x04001B87 RID: 7047
	private UIPanel m_parentPanel;
}
