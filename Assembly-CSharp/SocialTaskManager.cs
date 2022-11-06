using System;
using System.Collections.Generic;

// Token: 0x02000A11 RID: 2577
public class SocialTaskManager
{
	// Token: 0x06004421 RID: 17441 RVA: 0x0015FEF0 File Offset: 0x0015E0F0
	public SocialTaskManager()
	{
		this.m_taskList = new List<SocialTaskBase>();
	}

	// Token: 0x06004422 RID: 17442 RVA: 0x0015FF04 File Offset: 0x0015E104
	public void RequestProcess(SocialTaskBase task)
	{
		if (task == null)
		{
			return;
		}
		string taskName = task.GetTaskName();
		Debug.Log("SocialTaskManager:Request Process  " + taskName);
		this.m_taskList.Add(task);
	}

	// Token: 0x06004423 RID: 17443 RVA: 0x0015FF3C File Offset: 0x0015E13C
	public void Update()
	{
		if (this.m_taskList.Count <= 0)
		{
			return;
		}
		List<SocialTaskBase> list = new List<SocialTaskBase>();
		SocialTaskBase socialTaskBase = this.m_taskList[0];
		socialTaskBase.Update();
		if (socialTaskBase.IsDone())
		{
			string taskName = socialTaskBase.GetTaskName();
			Debug.Log("SocialTaskManager:" + taskName + " is Done");
			list.Add(socialTaskBase);
		}
		if (list.Count > 0)
		{
			foreach (SocialTaskBase item in list)
			{
				this.m_taskList.Remove(item);
			}
			list.Clear();
		}
	}

	// Token: 0x04003979 RID: 14713
	private List<SocialTaskBase> m_taskList;
}
