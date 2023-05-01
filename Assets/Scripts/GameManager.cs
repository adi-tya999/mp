using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
	// Static properties that serve as shortcut for variables in the instance.
	public static TextMeshPro ScoreTable { get => instance.scoreTable; set => instance.scoreTable = value; }
	public static float Speed { get => instance.speed; set => instance.speed = value; }
	public static int InnerBounds { get => instance.innerBounds; set => instance.innerBounds = value; }
	public static float PillarBounds { get => instance.pillarBounds; set => instance.pillarBounds = value; }
	public static int Score
	{
		get => instance.score;
		set
		{
			instance.score = value;
			if (value > HighScore) HighScore = value;
			ScoreTable.text = $"Current: {Score}	High: {HighScore}";
		}
	}
	public static int HighScore { get => instance.highScore; set => instance.highScore = value; }

	// Singleton instance.
	public static GameManager instance;


	public TextMeshPro	scoreTable;
	public float		speed			= 4;
	public int			innerBounds		= 16;
	public float		pillarBounds	= 3.5f;
	public int			score = 0;
	public int			highScore = 0;

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

		// Let's make the instance immortal ⚡
		DontDestroyOnLoad(instance);
	}

	void Update()
	{
		var light = FindObjectOfType(typeof(Light)) as Light;
		light.transform.Rotate(Vector3.right * Time.smoothDeltaTime);
	}

	public static void Reset()
	{
		Score = 0;
		Player.Reset();
		Pillar.ResetAll();
	}

	public static void AddScore() => ++Score;
}
