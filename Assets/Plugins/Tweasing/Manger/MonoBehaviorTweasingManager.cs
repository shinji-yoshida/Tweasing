using UnityEngine;

namespace Tweasing {
	public class MonoBehaviorTweasingManager : MonoBehaviour, ITweasingManager {
		[SerializeField] TweenExecutor executor;

		public TweenExecutor Executor {
			get {
				return executor;
			}
		}

		public bool IsNull {
			get {
				return false;
			}
		}
	}
}
