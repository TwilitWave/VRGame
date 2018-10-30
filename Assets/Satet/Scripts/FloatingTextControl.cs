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
            Debug.Log("The position is " + this.transform.position);
            _textMesh.text = score.ToString();
            _animator.SetBool("IsPopup", true);
        }

        public void OnPopupFinished()
        {
            Debug.Log("The floating test finish popup.");
            Destroy(this.gameObject);
        }
    } 
}

