using UnityEngine;

public class Pillar : MonoBehaviour
{
	// Shortcut property to my world position
	Vector3 Pos { get => transform.position; set => transform.position = value; }
	float origPos;

	void Start()
	{
		Pos = (Vector3.right * Pos.x) + (Vector3.forward * 64) +
			// Let's put ourselves at a height that makes sense :)
			(Vector3.up * Random.Range(-GameManager.PillarBounds, GameManager.PillarBounds));

		// Let's remember where we started.
		origPos = Pos.x;
	}

	void Update()
	{
		// Let's move to the left. Always. No exception.
		transform.Translate(GameManager.Speed * Time.smoothDeltaTime, 0, 0);

		// Am I *aaaaaaaaallllll* the way to the camera's left?
		if (Pos.x >= GameManager.InnerBounds)
		{
			Pos =
				// Let's snap *aaaaaaaaallllll* the way to the camera's right :)
				(-Vector3.right * GameManager.InnerBounds) +
				// And stay in front of the camera.
				(Vector3.forward * 64) +
				// And put ourselves at a height that makes sense :)
				(Vector3.up * Random.Range(-GameManager.PillarBounds, GameManager.PillarBounds));
		}
	}

	public void Reset()
	{
		Pos =
			// Let's go back to the time before time ✨
			(Vector3.right * origPos) +
			// And stay in front of the camera.
			(Vector3.forward * 64) +
			// And put ourselves at a height that makes sense :)
			(Vector3.up * Random.Range(-GameManager.PillarBounds, GameManager.PillarBounds));
	}

	public static void ResetAll()
	{
		// Listen up everybody!
		Pillar[] pillars = FindObjectsOfType(typeof(Pillar)) as Pillar[];

		// To your positions!
		foreach (Pillar pillar in pillars)
			pillar.Reset();
	}
}
