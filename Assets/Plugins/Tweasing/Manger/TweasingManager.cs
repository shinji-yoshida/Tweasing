
namespace Tweasing {
	public abstract class TweasingManager : ITweasingManager {
		static ITweasingManager soleInstance = new NullTweasingManager();

		public static ITweasingManager Instance {
			get {
				return soleInstance;
			}
		}

		public static void Reset(ITweasingManager manager) {
			soleInstance = manager;
		}

		public abstract TweenExecutor Executor {
			get;
		}

		public abstract bool IsNull {
			get;
		}
	}
}
