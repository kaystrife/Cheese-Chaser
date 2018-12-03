using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ToNextLevel : MonoBehaviour {

    Button button;

	// Use this for initialization
	void Start () {

        button = GetComponent<Button>();

        if(button!=null)
        {
            button.onClick.AddListener(NextLevel);
        }
	}
	
	void NextLevel()
    {
        AudioManager.Play("Click");
        SceneManager.LoadScene("GameScene");
    }
}
