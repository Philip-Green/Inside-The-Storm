using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangingScript : MonoBehaviour
{
    public void LoadSceneWithIntegerID(int whichScene)
    {
        SceneManager.LoadScene(whichScene);
    }
} 
