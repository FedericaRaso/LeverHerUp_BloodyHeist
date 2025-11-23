using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleScreen : MonoBehaviour
{
    [SerializeField]
    private string sceneToLoad; 

    void Start()
    {
        Button btn = GetComponent<Button>();
        if (btn != null)
        {
            btn.onClick.AddListener(ChangeScene);
        }
    }

    void ChangeScene()
    {
        SceneManager.LoadScene(sceneToLoad);
    }
}