using UnityEngine;
using UniPromise;
using UniRx;

namespace Tweasing {
	public class ScalingEmersionTweener : DurationScalableEmersionTweener {
		[SerializeField] float duration = 0.3f;
		[SerializeField] EasingEnum showEasing = EasingEnum.BackEaseOut;
		[SerializeField] EasingEnum hideEasing = EasingEnum.BackEaseInOut;

		protected override void DoForceHide ()
		{
			transform.localScale = Vector3.zero;
		}

		protected override void DoForceShow () {
			transform.localScale = Vector3.one;
		}

		protected override Promise<CUnit> DoShow(float duration, TweenExecutor executor) {
			transform.localScale = Vector3.zero;
			return executor.Execute (
				new FloatTween (duration, 0, 1, showEasing, s => transform.localScale = new Vector3 (1, s, 1))
			).AddTo(this);
		}

		protected override Promise<CUnit> DoHide(float duration, TweenExecutor executor) {
			transform.localScale = Vector3.one;
			return executor.Execute (
				new FloatTween (duration, 1, 0, hideEasing, s => transform.localScale = new Vector3 (1, s, 1))
			).AddTo(this);
		}

		protected override float GetDefaultShowEasingDuration () {
			return duration;
		}

		protected override float GetDefaultHideEasingDuration () {
			return duration;
		}
	}
}
