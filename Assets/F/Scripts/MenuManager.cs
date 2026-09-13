using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    void Start()
    {
        
    }
    void Update()
    {
        
    }

    public void Play()
    {
        SceneManager.LoadScene("First level");
    }
}
