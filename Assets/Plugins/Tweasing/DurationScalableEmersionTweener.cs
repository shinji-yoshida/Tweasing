using UnityEngine;
using System.Collections;
using UniPromise;
using UniRx;

namespace Tweasing {
	public abstract class DurationScalableEmersionTweener : EmersionTweener {
		public override Promise<CUnit> Show () {
			return Show (GetDefaultShowEasingDuration (), GetExecutor ());
		}

		public Promise<CUnit> Show (float duration) {
			return Show (duration, GetExecutor ());
		}

		public Promise<CUnit> Show (TweenExecutor executor) {
			return Show (GetDefaultShowEasingDuration(), executor);
		}

		public Promise<CUnit> Show (float duration, TweenExecutor executor) {
			var result = DoShow (duration, executor);
			Tweening.Disposable = result;
			return result;
		}

		protected abstract Promise<CUnit> DoShow (float duration, TweenExecutor executor);

		public override Promise<CUnit> Hide () {
			return Hide (GetDefaultHideEasingDuration (), GetExecutor ());
		}

		public Promise<CUnit> Hide (float duration) {
			return Hide (duration, GetExecutor ());
		}

		public Promise<CUnit> Hide (TweenExecutor executor) {
			return Hide (GetDefaultHideEasingDuration(), executor);
		}

		public Promise<CUnit> Hide(float duration, TweenExecutor executor) {
			var result = DoHide (duration, executor);
			Tweening.Disposable = result;
			return result;
		}

		protected abstract Promise<CUnit> DoHide (float duration, TweenExecutor executor);

		protected TweenExecutor GetExecutor() {
			return TweasingManager.Instance.Executor;
		}

		protected virtual float GetDefaultShowEasingDuration() {
			throw new System.NotImplementedException ();
		}

		protected virtual float GetDefaultHideEasingDuration () {
			throw new System.NotImplementedException ();
		}

		protected override Promise<CUnit> DoShow () {
			throw new System.NotImplementedException ("never be called");
		}

		protected override Promise<CUnit> DoHide () {
			throw new System.NotImplementedException ("never be called");
		}
	}
}
