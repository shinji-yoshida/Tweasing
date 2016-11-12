using UnityEngine;
using System;
using System.Collections.Generic;
using gotanda;
using UniPromise;
using UniRx;

namespace Tweasing {
	public class HSVColorTween : Tween<HSVColor> {

		public HSVColorTween (float duration, HSVColor tweenFrom, HSVColor tweenTo, EasingEnum easingType, Action<HSVColor> onUpdated, Action<Unit> onCompleted=null)
			: base(duration, tweenFrom, tweenTo.Minus(tweenFrom), easingType, onUpdated, onCompleted)
		{
		}

		protected override HSVColor CalculateValue () {
			return tweenDelta.Mult(easing(t / duration)).Plus(tweenFrom);
		}
	}
}
