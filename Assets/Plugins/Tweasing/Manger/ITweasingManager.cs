using UnityEngine;
using System.Collections;

namespace Tweasing {
	public interface ITweasingManager {
		TweenExecutor Executor {
			get;
		}

		bool IsNull {
			get;
		}
	}
}
