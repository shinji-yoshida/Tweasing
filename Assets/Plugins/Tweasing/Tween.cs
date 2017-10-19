using UnityEngine;
using System;
using UniPromise;
using UniRx;

namespace Tweasing {
	public enum StatusEnum
	{
		Running, Completed, Disposed
	}

	public interface Tween {
		Promise<CUnit> PromiseExecution { get; }
		StatusEnum Status { get; }
		void Update ();
		void CallCompleted();
	}

	public abstract class Tween<T> : IDisposable, Tween {

		protected float duration;
		protected float t;
		protected T tweenFrom;
		protected T tweenDelta;
		protected NormalizedEasing easing;
		Action<T> onUpdated;
		StatusEnum status;
		Deferred<CUnit> deferredCompleted;

		public Tween (float duration, T tweenFrom, T tweenDelta, EasingEnum easingType, Action<T> onUpdated, Action<CUnit> onCompleted=null) {
			this.duration = duration;
			this.tweenFrom = tweenFrom;
			this.tweenDelta = tweenDelta;
			this.easing = easingType.NormalizedFunc();
			this.onUpdated = onUpdated;

			deferredCompleted = new Deferred<CUnit> ();
			if (onCompleted != null)
				deferredCompleted.Done (onCompleted);
			deferredCompleted.Disposed (() => this.Dispose ());

			status = StatusEnum.Running;
		}

		public Promise<CUnit> PromiseExecution {
			get {
				return deferredCompleted;
			}
		}

		public void Update () {
			t = Mathf.Min(t + Time.unscaledDeltaTime, duration);
			onUpdated (CalculateValue());
			if (t >= duration)
				status = StatusEnum.Completed;
		}

		protected abstract T CalculateValue();

		public StatusEnum Status {
			get {
				return status;
			}
		}

		public void CallCompleted() {
			deferredCompleted.Resolve (CUnit.Default);
		}

		public void Dispose () {
			status = StatusEnum.Disposed;
		}
	}
}
