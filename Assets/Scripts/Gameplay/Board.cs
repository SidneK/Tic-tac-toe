using UnityEngine;
using UnityEngine.UI;

public class Board : MonoBehaviour
{
	public GameObject PausePop;
	[Header("Text")]
	public Text PrintScoreX;
	public Text PrintScoreZero;
	public Text PrintGameOver;
	[Header("Cells")]
	public Sprite X_cell;
	public Sprite Zero_cell;
	public Sprite Empty_cell;
	[Header("Gameplay")]
	public bool Turn;
	public bool Pause;
	public bool GameOver;
	public bool vsPlayer;
	public int ScoreX;
	public int ScoreZero;
	public int Moves;

	static private Board board;
	static public Board Instance{ get; private set; }
	void Awake()
	{
		PausePop.SetActive(false);
		PrintGameOver.enabled = false;
		Instance = this;
		vsPlayer = ActionButton.choice_mode;
	}

	private SpriteRenderer[] sr;
	void Start()
	{
		sr = GetComponentsInChildren<SpriteRenderer>();
		sr[9].sprite = Turn ? X_cell : Zero_cell;
	}

	private bool May = false; // is used when the game is ended
	void Update()
	{
		if (May && Input.GetMouseButtonDown(0))
		{
			foreach (SpriteRenderer it in sr)
			{
				it.sprite = Empty_cell;
				it.tag = "Cell";
			}
			GameOver = false;
			PrintGameOver.enabled = false;
			May = false;
			Turn = true;
			sr[9].sprite = X_cell;
			Moves = 0;
		}
		if (GameOver && !May)
			InputWinner("Winner - " + (!Turn ? "X" : "Zero") + "\nTouch on the screen\nto start");
		else if (Moves == 9 && !May)
			InputWinner("Draw\nTouch on the screen\nto start");
	}

	void InputWinner(string reason_game_over)
	{
		if (GameOver)
		{
			if (!Turn)
				PrintScoreX.text = (++ScoreX).ToString();
			else
				PrintScoreZero.text = (++ScoreZero).ToString();
		}
		PrintGameOver.enabled = true;
		PrintGameOver.text = reason_game_over;
		May = true;
	}

	public void MoveAI()
	{
		int moveAI = Random.Range(0, 8);
		while (sr[moveAI].tag != "Cell")
			moveAI = Random.Range(0, 8);
		sr[moveAI].sprite = Zero_cell;
		sr[moveAI].tag = "Zero_cell";
		GameOver = IsWin("Zero_cell");
		++Moves;
		Turn = !Turn;
	}

	public bool IsWin(string cell)
	{
		sr[9].sprite = !Turn ? X_cell : Zero_cell;
		if ((sr[0].tag == cell && sr[1].tag == cell && sr[2].tag == cell) ||
			(sr[3].tag == cell && sr[4].tag == cell && sr[5].tag == cell) ||
			(sr[6].tag == cell && sr[7].tag == cell && sr[8].tag == cell))
			return true;
		else if ((sr[0].tag == cell && sr[3].tag == cell && sr[6].tag == cell) ||
			(sr[1].tag == cell && sr[4].tag == cell && sr[7].tag == cell) ||
			(sr[2].tag == cell && sr[5].tag == cell && sr[8].tag == cell))
			return true;
		else if ((sr[0].tag == cell && sr[4].tag == cell && sr[8].tag == cell) ||
			(sr[2].tag == cell && sr[4].tag == cell && sr[6].tag == cell))
			return true;
		return false;
	}
}
