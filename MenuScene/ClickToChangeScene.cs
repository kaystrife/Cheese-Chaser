using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ClickToChangeScene : MonoBehaviour {

    Button button;

    public string sceneName;

	// Use this for initialization
	void Start () {
        button = GetComponent<Button>();
        button.onClick.AddListener(ChangeScene);
	}
	
    void ChangeScene()
    {
        AudioManager.Play("Click");
        SceneManager.LoadScene(sceneName);
    }
}
