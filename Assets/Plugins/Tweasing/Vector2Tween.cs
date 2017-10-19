using UnityEngine;
using System;
using UniRx;
using UniPromise;


namespace Tweasing {
	[Serializable]
	public class Vector2TweenProvider {
		public float duration = 1;
		public EasingEnum easingType;

		public Vector2Tween Generate(Vector2 tweenFrom, Vector2 tweenTo, Action<Vector2> onUpdated, Action<CUnit> onCompleted=null) {
			return new Vector2Tween (duration, tweenFrom, tweenTo, easingType, onUpdated, onCompleted);
		}
	}

	public class Vector2Tween : Tween<Vector2> {

		public Vector2Tween (float duration, Vector2 tweenFrom, Vector2 tweenTo, EasingEnum easingType, Action<Vector2> onUpdated, Action<CUnit> onCompleted=null)
			: base(duration, tweenFrom, tweenTo - tweenFrom, easingType, onUpdated, onCompleted)
		{
		}

		protected override Vector2 CalculateValue () {
			return easing(t / duration) * tweenDelta + tweenFrom;
		}
	}
}