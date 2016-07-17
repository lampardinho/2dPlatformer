using UnityEngine;
using System.Collections;

public class FollowUV : MonoBehaviour {

	public float parralax = 2f;
    public GameObject FollowedGameObject;

	void Update () {

		MeshRenderer mr = GetComponent<MeshRenderer>();

		Material mat = mr.material;

		Vector2 offset = mat.mainTextureOffset;

		offset.x = FollowedGameObject.transform.position.x / transform.localScale.x / parralax;
		offset.y = FollowedGameObject.transform.position.y / transform.localScale.y / parralax;

		mat.mainTextureOffset = offset;

	}

}
