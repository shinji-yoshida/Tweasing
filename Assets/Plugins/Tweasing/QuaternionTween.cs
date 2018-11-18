using UnityEngine;
using System;
using UniPromise;

namespace Tweasing {
	[Serializable]
	public class QuaternionTweenProvider {
		public float duration = 1;
		public EasingEnum easingType;

		public QuaternionTween Generate(Quaternion tweenFrom, Quaternion tweenTo, Action<Quaternion> onUpdated, Action<CUnit> onCompleted=null) {
			return new QuaternionTween (duration, tweenFrom, tweenTo, easingType, onUpdated, onCompleted);
		}
	}

	public class QuaternionTween : Tween<Quaternion> {
		Quaternion tweenTo;

		public QuaternionTween (
			float duration, Quaternion tweenFrom, Quaternion tweenTo, EasingEnum easingType, Action<Quaternion> onUpdated, Action<CUnit> onCompleted
		)
			: base (duration, tweenFrom, Quaternion.identity, easingType, onUpdated, onCompleted)
		{
			this.tweenTo = tweenTo;
		}
		
		protected override Quaternion CalculateValue () {
			return Quaternion.SlerpUnclamped (tweenFrom, tweenTo, easing (t / duration));
		}
	}
}
