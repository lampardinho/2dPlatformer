using UnityEngine;
using System.Collections;

public class FollowUV : MonoBehaviour
{
    [SerializeField]
    private float _parallax;

    [SerializeField]
    private GameObject _followedGameObject;

	void Update ()
    {
		MeshRenderer mr = GetComponent<MeshRenderer>();
		Material mat = mr.material;
		Vector2 offset = mat.mainTextureOffset;

		offset.x = _followedGameObject.transform.position.x / transform.localScale.x / _parallax;
		offset.y = _followedGameObject.transform.position.y / transform.localScale.y / _parallax;

		mat.mainTextureOffset = offset;
	}
}
