
namespace Tweasing {
	public class NullTweasingManager : TweasingManager {
		public override TweenExecutor Executor {
			get {
				throw new System.NotImplementedException ();
			}
		}

		public override bool IsNull {
			get {
				return true;
			}
		}
	}
}
