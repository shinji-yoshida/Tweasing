using UnityEngine;
using System.Collections;
using UniPromise;
using UniRx;

namespace Tweasing {
	public abstract class EmersionTweener : MonoBehaviour {
		SerialDisposable _tweening;

		protected SerialDisposable Tweening {
			get {
				if(_tweening == null)
					return _tweening = new SerialDisposable();
				return _tweening;
			}
		}

		public void ForceHide() {
			Tweening.Disposable = Disposable.Empty;
			DoForceHide ();
		}

		protected abstract void DoForceHide ();

		public void ForceShow () {
			Tweening.Disposable = Disposable.Empty;
			DoForceShow ();
		}

		protected abstract void DoForceShow ();

		public virtual Promise<Unit> Show () {
			var result = DoShow ();
			Tweening.Disposable = result;
			return result;
		}

		protected abstract Promise<Unit> DoShow ();

		public virtual Promise<Unit> Hide() {
			var result = DoHide ();
			Tweening.Disposable = result;
			return result;
		}

		protected abstract Promise<Unit> DoHide ();
	}
}
