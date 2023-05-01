using UnityEngine;

public class Ground : MonoBehaviour
{
	Material mat;

	void Start() =>
		// Get my gud material uwu
		mat = GetComponent<MeshRenderer>().sharedMaterial;

	void Update()
	{
		// Get the current offset
		float off = mat.GetFloat("_Offset");

		// Let's make sure it never goes too big to prevent
		// floating point precision issues if the player
		// is way too good.
		if (off > 10.0f) off -= 10.0f;

		// And let's finally move owo
		mat.SetFloat("_Offset",  off + (GameManager.Speed * Time.smoothDeltaTime / 4));
	}
}
