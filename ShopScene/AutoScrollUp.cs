using UnityEngine;
using UnityEngine.UI;

public class AutoScrollUp : MonoBehaviour {

    public ScrollRect scrollRect;

    private void OnEnable()
    {
        Debug.Log("Scroll up");
        scrollRect.normalizedPosition = new Vector2(0, 1);
    }
}
