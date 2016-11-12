using UnityEngine;
using System.Collections;
using UniRx;
using UniPromise;

namespace Tweasing {
	public class AnimatorEmersionTweener : EmersionTweener {
		[SerializeField] Animator animator;
		Subject<string> animatorEventSubject;

		protected void Awake() {
			animatorEventSubject = new Subject<string> ();
		}
	
		protected override void DoForceHide () {
			animator.ResetTrigger ("TriggerHide");
			animator.ResetTrigger ("TriggerShow");
			animatorEventSubject.OnNext ("Reset");

			animator.Play ("Hidden");
		}

		protected override void DoForceShow () {
			animator.ResetTrigger ("TriggerHide");
			animator.ResetTrigger ("TriggerShow");
			animatorEventSubject.OnNext ("Reset");

			animator.Play ("Shown");
		}

		protected override Promise<Unit> DoShow () {
			animator.ResetTrigger ("TriggerHide");
			animator.ResetTrigger ("TriggerShow");
			animatorEventSubject.OnNext ("Reset");

			var result = new Deferred<Unit> ();
			animator.SetTrigger ("TriggerShow");
			animatorEventSubject.Take (1).Subscribe (e => {
				if(e == "Shown")
					result.Resolve(Unit.Default);
				else
					result.Dispose();
			}).AddTo (this);
			result.Disposed (DoForceShow);
			return result;
		}

		protected override Promise<Unit> DoHide () {
			animator.ResetTrigger ("TriggerHide");
			animator.ResetTrigger ("TriggerShow");
			animatorEventSubject.OnNext ("Reset");

			var result = new Deferred<Unit> ();
			animator.SetTrigger ("TriggerHide");
			animatorEventSubject.Take (1).Subscribe (e => {
				if(e == "Hidden")
					result.Resolve(Unit.Default);
				else
					result.Dispose();
			}).AddTo (this);
			result.Disposed (DoForceHide);
			return result;
		}

		public void CallbackAnimatorEvent(string eventName) {
			animatorEventSubject.OnNext (eventName);
		}
	}
}
