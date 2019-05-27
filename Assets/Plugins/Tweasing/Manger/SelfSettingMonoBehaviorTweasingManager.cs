using System;

namespace Tweasing
{
	public class SelfSettingMonoBehaviorTweasingManager : MonoBehaviorTweasingManager
	{
		protected void Awake()
		{
			TweasingManager.Reset(this);
		}
	}
}
