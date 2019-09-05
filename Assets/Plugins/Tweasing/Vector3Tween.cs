using System;
using UniPromise;
using UnityEngine;

namespace Tweasing
{
	[Serializable]
	public class Vector3TweenProvider {
		public float duration = 1;
		public EasingEnum easingType;

		public Vector3Tween Generate(Vector3 tweenFrom, Vector3 tweenTo, Action<Vector3> onUpdated, Action<CUnit> onCompleted=null) {
			return new Vector3Tween (duration, tweenFrom, tweenTo, easingType, onUpdated, onCompleted);
		}
	}

	public class Vector3Tween : Tween<Vector3> {

		public Vector3Tween (float duration, Vector3 tweenFrom, Vector3 tweenTo, EasingEnum easingType, Action<Vector3> onUpdated, Action<CUnit> onCompleted=null)
			: base(duration, tweenFrom, tweenTo - tweenFrom, easingType, onUpdated, onCompleted)
		{
		}

		protected override Vector3 CalculateValue () {
			return easing(t / duration) * tweenDelta + tweenFrom;
		}
	}
}
