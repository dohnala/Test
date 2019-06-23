using UnityEngine;

namespace Util
{
    public class DestroyAfterAnimation : MonoBehaviour
    {
        private void Start()
        {
            var animator = GetComponent<Animator>();
            var clipInfo = animator.GetCurrentAnimatorClipInfo(0);
            var clipLength = clipInfo[0].clip.length;

            Destroy(gameObject, clipLength);
        }
    }
}