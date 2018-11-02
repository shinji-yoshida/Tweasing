using UnityEngine;
using System.Collections;
using UniPromise;
using UniRx;

namespace Tweasing {
	public abstract class EmersionTweener : MonoBehaviour {
		public enum EmersionState {
			Shown, Hidden, Showing, Hiding
		}

		SerialDisposable _tweening;
		EmersionState state;

		protected SerialDisposable Tweening {
			get {
				if(_tweening == null)
					return _tweening = new SerialDisposable();
				return _tweening;
			}
		}

		public EmersionState State {
			get {
				return state;
			}
		}

		public void ForceHide() {
			Tweening.Disposable = Disposable.Empty;
			DoForceHide ();
			state = EmersionState.Hidden;
		}

		protected abstract void DoForceHide ();

		public void ForceShow () {
			Tweening.Disposable = Disposable.Empty;
			DoForceShow ();
			state = EmersionState.Shown;
		}

		protected abstract void DoForceShow ();

		public virtual Promise<CUnit> Show () {
			state = EmersionState.Showing;
			var result = DoShow ().Done(_ => state = EmersionState.Shown);
			Tweening.Disposable = result;
			return result;
		}

		protected abstract Promise<CUnit> DoShow ();

		public virtual Promise<CUnit> Hide() {
			state = EmersionState.Hiding;
			var result = DoHide ().Done(_ => state = EmersionState.Hidden);
			Tweening.Disposable = result;
			return result;
		}

		protected abstract Promise<CUnit> DoHide ();
	}
}
