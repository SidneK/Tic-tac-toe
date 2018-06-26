using UnityEngine;
using UnityEngine.SceneManagement;

public class ActionButton : MonoBehaviour
{
	private SpriteRenderer sr;
	void Start()
	{
		sr = GetComponent<SpriteRenderer>();	
	}

	static public bool choice_mode;
	public void UseButton(string Name)
	{
		if (Name == "Play")
			SceneManager.LoadScene("Mode");
		else if (Name == "Pause")
		{
			Board.Instance.Pause = true;
			Board.Instance.PausePop.SetActive(true);
		}
		else if (Name == "Resume")
		{
			Board.Instance.Pause = false;
			Board.Instance.PausePop.SetActive(false);
		}
		else if (Name == "PvsP" || Name == "PvsC")
		{
			choice_mode = Name == "PvsP";
			SceneManager.LoadScene("Main");
		}
		else if (Name == "About")
			SceneManager.LoadScene("About");
		else if (Name == "BackToMenu")
			SceneManager.LoadScene("Menu");
		else if (Name == "Exit")
			Application.Quit();
	}
}
