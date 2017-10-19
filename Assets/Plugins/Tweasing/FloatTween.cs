using System;
using UniRx;
using UniPromise;

namespace Tweasing {
	public class FloatTween : Tween<float> {

		public FloatTween (float duration, float tweenFrom, float tweenTo, EasingEnum easingType, Action<float> onUpdated, Action<CUnit> onCompleted=null)
			: base(duration, tweenFrom, tweenTo - tweenFrom, easingType, onUpdated, onCompleted)
		{
		}

		protected override float CalculateValue () {
			return easing(t / duration) * tweenDelta + tweenFrom;
		}
	}
}
