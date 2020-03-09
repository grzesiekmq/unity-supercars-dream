using UnityEngine;
using UnityEngine.SceneManagement;

public class Scenes : MonoBehaviour
{
    public void ChangeToMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void ChangeToGarage()
    {
        SceneManager.LoadScene("Garage");
    }

    public void ChangeToLevel1()
    {
        SceneManager.LoadScene("Level1");
    }
}