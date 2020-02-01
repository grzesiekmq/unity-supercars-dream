using UnityEngine;
using UnityEngine.SceneManagement;

public class Scenes : MonoBehaviour
{
    // Start is called before the first frame update
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
    }

    public void ChangeToMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void ChangeToTracks()
    {
        SceneManager.LoadScene("Tracks");
    }

    public void ChangeToMakes()
    {
        SceneManager.LoadScene("Makes");
    }

    public void ChangeToModels()
    {
        SceneManager.LoadScene("Models");
    }

    public void ChangeToGarage()
    {
        SceneManager.LoadScene("Garage");
    }

    public void ChangeToCarSelect()
    {
        SceneManager.LoadScene("CarSelect");
    }
}