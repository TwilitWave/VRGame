using UnityEngine;

namespace SSR.Mummy
{
    public class FloatingTextControl : MonoBehaviour
    {
        private Animator _animator;

        private TextMesh _textMesh;

        private void Awake()
        {
            _animator = this.GetComponent<Animator>();
            _textMesh = this.GetComponent<TextMesh>();

        }

        public void Popup(int score)
        {
            _textMesh.text = score.ToString();
            _animator.SetBool("IsPopup", true);
        }

        public void OnPopupFinished()
        {
            Destroy(this.gameObject);
        }
    } 
}

