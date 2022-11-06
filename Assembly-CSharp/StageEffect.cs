using System;
using UnityEngine;

// Token: 0x02000307 RID: 775
public class StageEffect : MonoBehaviour
{
	// Token: 0x060016C7 RID: 5831 RVA: 0x00083128 File Offset: 0x00081328
	private void OnDestroy()
	{
		if (this.m_stageEffect != null)
		{
			UnityEngine.Object.Destroy(this.m_stageEffect);
			this.m_stageEffect = null;
		}
	}

	// Token: 0x060016C8 RID: 5832 RVA: 0x00083150 File Offset: 0x00081350
	private void Update()
	{
		if (this.m_stageEffect != null && this.m_cameraObject != null)
		{
			if (this.m_resetPos)
			{
				base.transform.localPosition = -this.m_cameraObject.transform.position;
			}
			else
			{
				base.transform.localPosition = new Vector3(0f, 0f, -this.m_cameraObject.transform.position.z);
			}
		}
	}

	// Token: 0x060016C9 RID: 5833 RVA: 0x000831E4 File Offset: 0x000813E4
	public void Setup(GameObject originalObj)
	{
		if (this.m_stageEffect == null && originalObj != null)
		{
			GameObject gameObject = UnityEngine.Object.Instantiate(originalObj, Vector3.zero, Quaternion.identity) as GameObject;
			if (gameObject != null)
			{
				gameObject.SetActive(true);
				gameObject.transform.parent = base.gameObject.transform;
				gameObject.transform.localPosition = originalObj.transform.localPosition;
				gameObject.transform.localRotation = originalObj.transform.localRotation;
				this.m_stageEffect = gameObject;
			}
		}
		if (this.m_cameraObject == null)
		{
			this.m_cameraObject = base.transform.parent.gameObject;
		}
	}

	// Token: 0x060016CA RID: 5834 RVA: 0x000832A8 File Offset: 0x000814A8
	public void ResetPos(bool reset)
	{
		this.m_resetPos = reset;
	}

	// Token: 0x060016CB RID: 5835 RVA: 0x000832B4 File Offset: 0x000814B4
	public static StageEffect CreateStageEffect(string stageName)
	{
		StageEffect stageEffect = null;
		if (ResourceManager.Instance != null)
		{
			GameObject gameObject = ResourceManager.Instance.GetGameObject(ResourceCategory.TERRAIN_MODEL, "ef_stage_" + stageName);
			if (gameObject != null)
			{
				GameObject gameObject2 = GameObject.FindGameObjectWithTag("MainCamera");
				if (gameObject2 != null)
				{
					GameObject gameObject3 = new GameObject("StageEffect");
					if (gameObject3 != null)
					{
						gameObject3.SetActive(true);
						gameObject3.transform.parent = gameObject2.transform;
						gameObject3.transform.localPosition = Vector3.zero;
						gameObject3.transform.localRotation = Quaternion.identity;
						stageEffect = gameObject3.AddComponent<StageEffect>();
						if (stageEffect != null)
						{
							stageEffect.Setup(gameObject);
						}
					}
				}
			}
		}
		return stageEffect;
	}

	// Token: 0x0400144A RID: 5194
	private GameObject m_stageEffect;

	// Token: 0x0400144B RID: 5195
	private GameObject m_cameraObject;

	// Token: 0x0400144C RID: 5196
	private bool m_resetPos;
}
