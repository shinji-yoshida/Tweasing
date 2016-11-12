using UnityEngine;
using System.Collections;
using UniPromise;
using UniRx;

namespace Tweasing {
	public class ScalingEmersionTweener : DurationScalableEmersionTweener {
		[SerializeField] float duration = 0.3f;

		protected override void DoForceHide ()
		{
			transform.localScale = Vector3.zero;
		}

		protected override void DoForceShow () {
			transform.localScale = Vector3.one;
		}

		protected override Promise<Unit> DoShow(float duration, TweenExecutor executor) {
			transform.localScale = Vector3.zero;
			return executor.Execute (
				new FloatTween (duration, 0, 1, gotanda.EasingEnum.BackEaseOut, s => transform.localScale = new Vector3 (1, s, 1))
			).AddTo(this);
		}

		protected override Promise<Unit> DoHide(float duration, TweenExecutor executor) {
			transform.localScale = Vector3.one;
			return executor.Execute (
				new FloatTween (duration, 1, 0, gotanda.EasingEnum.BackEaseInOut, s => transform.localScale = new Vector3 (1, s, 1))
			).AddTo(this);
		}

		protected override float GetDefaultShowEasiongDuration () {
			return duration;
		}

		protected override float GetDefaultHideEasiongDuration () {
			return duration;
		}
	}
}
