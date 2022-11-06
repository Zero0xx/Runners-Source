using System;
using System.Collections.Generic;
using GooglePlayGames.BasicApi;
using GooglePlayGames.OurUtils;
using UnityEngine;

namespace GooglePlayGames.Android
{
	// Token: 0x02000034 RID: 52
	internal class AchievementBank
	{
		// Token: 0x060001B1 RID: 433 RVA: 0x00005D10 File Offset: 0x00003F10
		internal AchievementBank()
		{
		}

		// Token: 0x060001B2 RID: 434 RVA: 0x00005D24 File Offset: 0x00003F24
		internal void ProcessBuffer(AndroidJavaObject achBuffer)
		{
			Logger.d("AchievementBank: processing achievement buffer given as Java object.");
			if (achBuffer == null)
			{
				Logger.w("AchievementBank: given buffer was null. Ignoring.");
				return;
			}
			int num = achBuffer.Call<int>("getCount", new object[0]);
			Logger.d("AchievementBank: buffer contains " + num + " achievements.");
			for (int i = 0; i < num; i++)
			{
				Logger.d("AchievementBank: processing achievement #" + i);
				Achievement achievement = new Achievement();
				AndroidJavaObject androidJavaObject = achBuffer.Call<AndroidJavaObject>("get", new object[]
				{
					i
				});
				if (androidJavaObject == null)
				{
					Logger.w("Achievement #" + i + " was null. Ignoring.");
				}
				else
				{
					achievement.Id = androidJavaObject.Call<string>("getAchievementId", new object[0]);
					achievement.IsIncremental = (androidJavaObject.Call<int>("getType", new object[0]) == 1);
					int num2 = androidJavaObject.Call<int>("getState", new object[0]);
					achievement.IsRevealed = (num2 != 2);
					achievement.IsUnlocked = (num2 == 0);
					achievement.Name = androidJavaObject.Call<string>("getName", new object[0]);
					achievement.Description = androidJavaObject.Call<string>("getDescription", new object[0]);
					if (achievement.IsIncremental)
					{
						achievement.CurrentSteps = androidJavaObject.Call<int>("getCurrentSteps", new object[0]);
						achievement.TotalSteps = androidJavaObject.Call<int>("getTotalSteps", new object[0]);
					}
					Logger.d("AchievementBank: processed: " + achievement.ToString());
					if (achievement.Id != null && achievement.Id.Length > 0)
					{
						this.mAchievements[achievement.Id] = achievement;
					}
					else
					{
						Logger.w("Achievement w/ missing ID received. Ignoring.");
					}
				}
			}
			Logger.d("AchievementBank: bank now contains " + this.mAchievements.Count + " entries.");
		}

		// Token: 0x060001B3 RID: 435 RVA: 0x00005F20 File Offset: 0x00004120
		internal Achievement GetAchievement(string id)
		{
			if (this.mAchievements.ContainsKey(id))
			{
				return this.mAchievements[id];
			}
			Logger.w("Achievement ID not found in bank: id " + id);
			return null;
		}

		// Token: 0x060001B4 RID: 436 RVA: 0x00005F54 File Offset: 0x00004154
		internal List<Achievement> GetAchievements()
		{
			List<Achievement> list = new List<Achievement>();
			foreach (Achievement item in this.mAchievements.Values)
			{
				list.Add(item);
			}
			return list;
		}

		// Token: 0x04000090 RID: 144
		private Dictionary<string, Achievement> mAchievements = new Dictionary<string, Achievement>();
	}
}
