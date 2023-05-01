using UnityEngine;

public class Player : MonoBehaviour
{
	[GradientUsage(true)]
	public Gradient
		highRamp,
		lowRamp;
	public float impulse = 4;
	public float target = 11;

	float speed = .05f;
	Material mat;

	public static Player instance;

	void Awake()
	{
		// Is the instance empty?
		if (instance == null)
			// *I'm* gonna be the instance >:)
			instance = this;
		// No then?
		else
			// imuseless:(
			Destroy(gameObject);
	}

    void Start() =>
		// Get my gud material uwu
		mat = GetComponent<MeshRenderer>().sharedMaterial;

	void Update()
	{
		// Move! Move! Move!
		transform.position += speed * Time.smoothDeltaTime * Vector3.up;
		speed = Mathf.Lerp(speed, -target, .025f);

		// I jumped, actually it was super easy, barely an inconvenience.
		if (Input.GetKeyDown(KeyCode.Space))
			speed = impulse;


		// Get the angle of my speed.
		var theta = Mathf.Atan2(-speed, 1.5f) * Mathf.Rad2Deg;
		// Let's t u r n a r o u n d
		transform.rotation = Quaternion.AngleAxis(theta, Vector3.forward);


		// Change color based on distance to sky and ground.
		var prox = transform.position.y / 9.5f;
		mat.SetColor("_Color1", highRamp.Evaluate(prox));
		mat.SetColor("_Color2", lowRamp.Evaluate(prox));
	}

	void OnTriggerEnter(Collider c)
	{
		switch (c.gameObject.layer)
		{
			case 6:	// Envinroment layer
				GameManager.Reset();
				break;
			case 7:	// Scores layer
				GameManager.AddScore();
				break;
			default:
				Debug.Log("waitwhat");
				break;
		}
	}

	public static void Reset()
	{
		instance.transform.position = new Vector3(5, 4.5f, 64);
		instance.speed = .05f;
	}
}
