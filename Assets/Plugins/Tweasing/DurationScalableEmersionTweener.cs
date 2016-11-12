using UnityEngine;
using System.Collections;
using UniPromise;
using UniRx;

namespace Tweasing {
	public abstract class DurationScalableEmersionTweener : EmersionTweener {
		public override Promise<Unit> Show () {
			return Show (GetDefaultShowEasiongDuration (), GetExecutor ());
		}

		public Promise<Unit> Show (float duration) {
			return Show (duration, GetExecutor ());
		}

		public Promise<Unit> Show (TweenExecutor executor) {
			return Show (GetDefaultShowEasiongDuration(), executor);
		}

		public Promise<Unit> Show (float duration, TweenExecutor executor) {
			var result = DoShow (duration, executor);
			Tweening.Disposable = result;
			return result;
		}

		protected abstract Promise<Unit> DoShow (float duration, TweenExecutor executor);

		public override Promise<Unit> Hide () {
			return Hide (GetDefaultHideEasiongDuration (), GetExecutor ());
		}

		public Promise<Unit> Hide (float duration) {
			return Hide (duration, GetExecutor ());
		}

		public Promise<Unit> Hide (TweenExecutor executor) {
			return Hide (GetDefaultHideEasiongDuration(), executor);
		}

		public Promise<Unit> Hide(float duration, TweenExecutor executor) {
			var result = DoHide (duration, executor);
			Tweening.Disposable = result;
			return result;
		}

		protected abstract Promise<Unit> DoHide (float duration, TweenExecutor executor);

		protected TweenExecutor GetExecutor() {
			return TweasingManager.Instance.Executor;
		}

		protected virtual float GetDefaultShowEasiongDuration() {
			throw new System.NotImplementedException ();
		}

		protected virtual float GetDefaultHideEasiongDuration () {
			throw new System.NotImplementedException ();
		}

		protected override Promise<Unit> DoShow () {
			throw new System.NotImplementedException ("never be called");
		}

		protected override Promise<Unit> DoHide () {
			throw new System.NotImplementedException ("never be called");
		}
	}
}
