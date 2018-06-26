using UnityEngine;

public class ActionCell : MonoBehaviour
{
	private SpriteRenderer sr;
	void Start()
	{
		sr = GetComponent<SpriteRenderer>();
	}

	void OnMouseDown()
	{
		if (!Board.Instance.Pause)
		{
			if (sr.tag == "Cell" && !Board.Instance.GameOver && Board.Instance.Moves != 9)
			{
				if (Board.Instance.Turn)
				{
					Move(Board.Instance.X_cell, "X_cell");
					if (!Board.Instance.Turn && !Board.Instance.vsPlayer &&
						!Board.Instance.GameOver && Board.Instance.Moves != 9)
						Board.Instance.MoveAI();
				}
				else if (!Board.Instance.Turn && Board.Instance.vsPlayer)
					Move(Board.Instance.Zero_cell, "Zero_cell");
			}
		}
	}

	void Move(Sprite sp, string cell)
	{
		sr.sprite = sp;
		sr.tag = cell;
		Board.Instance.GameOver = Board.Instance.IsWin(cell);
		++Board.Instance.Moves;
		Board.Instance.Turn = !Board.Instance.Turn;
	}
}
