using UnityEngine;
using System.Collections;
using UniPromise;
using UniRx;
using gotanda;

namespace Tweasing {
	public class CanvasGroupAlphaEmersionTweener : DurationScalableEmersionTweener {
		[SerializeField] CanvasGroup canvasGroup;
		[SerializeField] EasingEnum showEasing;
		[SerializeField] EasingEnum hideEasing;
		[SerializeField] float showEasingDuration = 1;
		[SerializeField] float hideEasingDuration = 1;

		protected override void DoForceHide () {
			canvasGroup.alpha = 0;
		}

		protected override void DoForceShow () {
			canvasGroup.alpha = 1;
		}

		protected override Promise<Unit> DoShow (float duration, TweenExecutor executor) {
			return executor.Execute (
				new FloatTween (
					duration, canvasGroup.alpha, 1, showEasing,
					a => canvasGroup.alpha = a, _ => canvasGroup.alpha = 1
				)
			);
		}

		protected override Promise<Unit> DoHide (float duration, TweenExecutor executor) {
			return executor.Execute (
				new FloatTween (
					duration, canvasGroup.alpha, 0, hideEasing,
					a => canvasGroup.alpha = a, _ => canvasGroup.alpha = 0
				)
			);
		}

		protected override float GetDefaultShowEasiongDuration () {
			return showEasingDuration;
		}

		protected override float GetDefaultHideEasiongDuration () {
			return hideEasingDuration;
		}
	}
}
