using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Tracks : MonoBehaviour
{
    public GameObject button;
    public GameObject backButton;
    public RectTransform canvas;
    public GameObject scenes;

    private string[] tracks = {
    "Barcelona",
     "Bathurst",
     "Brands-Hatch",
// 'Circuit de la Sarthe',
     "Hungaroring",
     "Indianapolis",
     "Interlagos",
     "Isle-of-Man",
     "Kyalami",
     "Laguna-Seca",
     "Le-Mans",
     "Magny-Cours",
     "Monaco",
     "Montreal",
     "Monza",
     "Nurburgring",
     "Red-Bull-Ring",
     "Silverstone",
     "Spa-Francorchamps",
     "Suzuka",
     "Zandvoort"
    };

    // Start is called before the first frame update
    private void Start()
    {
        foreach (string track in tracks)
        {
            GameObject goButton = (GameObject)Instantiate(button);

            goButton.GetComponentInChildren<TextMeshProUGUI>().text = track;

            var goButtonComponent = goButton.GetComponent<Button>();
            goButtonComponent.onClick.AddListener(click);

            goButton.transform.SetParent(canvas.transform, false);
        }

        var backBtnComponent = backButton.GetComponent<Button>();
        backBtnComponent.onClick.AddListener(OnBackBtnClick);
    }

    private void OnBackBtnClick()
    {
        var scenesComponent = scenes.GetComponent<Scenes>();

        scenesComponent.ChangeToMenu();
    }

    private void click()
    {
        var scenesComponent = scenes.GetComponent<Scenes>();
        scenesComponent.ChangeToMakes();
    }

    // Update is called once per frame
    private void Update()
    {
    }
}