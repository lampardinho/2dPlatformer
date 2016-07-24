using UnityEngine;

namespace Assets.Scripts
{
    public class FollowUV : MonoBehaviour
    {
        [SerializeField] private GameObject _followedGameObject;

        [SerializeField] private float _parallax;

        private void Update()
        {
            var mr = GetComponent<MeshRenderer>();
            var mat = mr.material;
            var offset = mat.mainTextureOffset;

            offset.x = _followedGameObject.transform.position.x/transform.localScale.x/_parallax;
            offset.y = _followedGameObject.transform.position.y/transform.localScale.y/_parallax;

            mat.mainTextureOffset = offset;
        }
    }
}