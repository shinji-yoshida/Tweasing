using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniPromise;
using UnityEngine.UI;
using UniRx;

namespace Tweasing {
	public class LayoutElementEmersionTweener : DurationScalableEmersionTweener {
		public enum TweenTarget {
			PreferredHeight, PreferredWidth
		}
		[SerializeField] float duration = 0.3f;
		[SerializeField] float shownSize = 100;
		[SerializeField] TweenTarget tweenTarget = TweenTarget.PreferredHeight;
		[SerializeField] LayoutElement layoutElement;

		protected override void DoForceHide () {
			SetSize (0);
			layoutElement.gameObject.SetActive (false);
		}

		protected override void DoForceShow () {
			SetSize (shownSize);
			layoutElement.gameObject.SetActive (true);
		}

		protected override Promise<CUnit> DoShow (float duration, TweenExecutor executor) {
			SetSize (0);
			layoutElement.gameObject.SetActive (true);
			return executor.Execute (
				new FloatTween (duration, 0, shownSize, EasingEnum.QuadEaseOut, s => SetSize(s))
			).AddTo(this);
		}

		protected override Promise<CUnit> DoHide (float duration, TweenExecutor executor) {
			SetSize (shownSize);
			return executor.Execute (
				new FloatTween (duration, shownSize, 0, EasingEnum.QuadEaseOut, s => SetSize(s), _ => layoutElement.gameObject.SetActive (false))
			).AddTo(this);
		}

		void SetSize (float size) {
			switch (tweenTarget) {
			case TweenTarget.PreferredHeight:
				layoutElement.preferredHeight = size;
				return;
			case TweenTarget.PreferredWidth:
				layoutElement.preferredWidth = size;
				return;
			default:
				throw new System.Exception (tweenTarget.ToString ());
			}
		}

		protected override float GetDefaultShowEasingDuration () {
			return duration;
		}

		protected override float GetDefaultHideEasingDuration () {
			return duration;
		}
	}
}
