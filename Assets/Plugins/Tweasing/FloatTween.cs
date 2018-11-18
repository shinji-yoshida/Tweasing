using System;
using UniRx;
using UniPromise;

namespace Tweasing {
	[Serializable]
	public class FloatTweenProvider {
		public float duration = 1;
		public EasingEnum easingType;

		public FloatTween Generate(float tweenFrom, float tweenTo, Action<float> onUpdated, Action<CUnit> onCompleted=null) {
			return new FloatTween (duration, tweenFrom, tweenTo, easingType, onUpdated, onCompleted);
		}
	}

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
