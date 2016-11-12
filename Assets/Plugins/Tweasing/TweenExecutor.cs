using UnityEngine;
using System;
using System.Collections.Generic;
using UniPromise;
using UniRx;

namespace Tweasing {
	public class TweenExecutor : MonoBehaviour {
		List<Tween> tweens;

		static Stack<List<Tween>> tweenListCache = new Stack<List<Tween>>();

		protected void Awake() {
			tweens = PopTweenListCache ();
		}

		static List<Tween> PopTweenListCache() {
			if (tweenListCache.Count > 0)
				return tweenListCache.Pop ();
			return new List<Tween> ();
		}

		static void PushTweenListCache(List<Tween> tweenList) {
			tweenList.Clear ();
			tweenListCache.Push (tweenList);
		}

		public Promise<Unit> Execute(Tween tween) {
			tweens.Add (tween);
			return tween.PromiseExecution;
		}

		protected void Update() {
			if (tweens.Count == 0)
				return;

			var currentTweens = this.tweens;
			this.tweens = PopTweenListCache ();
			foreach (var each in currentTweens) {
				if (each.Status == StatusEnum.Disposed)
					continue;
				
				each.Update ();

				switch (each.Status) {
				case StatusEnum.Completed:
					each.CallCompleted ();
					break;
				case StatusEnum.Running:
					this.tweens.Add (each);
					break;
				case StatusEnum.Disposed:
					break;
				default:
					throw new Exception (each.Status.ToString ());
				}
			}
			PushTweenListCache (currentTweens);
		}

		protected void OnDestroy() {
			PushTweenListCache (tweens);
			tweens = null;
		}
	}
}
