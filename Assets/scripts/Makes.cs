using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Makes : MonoBehaviour
{
    public GameObject button;
    public GameObject backButton;

    public RectTransform canvas;
    public GameObject scenes;

    public static string globalMake;

    private readonly string[] makes = {
  "adi",
  "akura",
  "alfa",
  "aston",
  "bentle",
  "bmv",
  "bugati",
  "cadilac",
  "chonda",
  "citron",
  "dodg",
  "ferari",
  "fort",
  "henesey",
  "hevrolet",
  "holdem",
  "hrysler",
  "jagur",
  "konigseg",
  "lambo",
  "lotuz",
  "luxus",
  "maklaren",
  "masda",
  "maybah",
  "mazerati",
  "merc",
  "mitsushi",
  "nisan",
  "opl",
  "other",
  "pahani",
  "pegot",
  "pontiak",
  "porshe",
  "reno",
  "rols_roys",
  "rufe",
  "sab",
  "salen",
  "shelbi",
  "tezla",
  "tojota",
  "twr",
  "vendeta",
  "vw",
  "zenwo",
  "zubaru"
    };

    // Start is called before the first frame update
    private void Start()
    {
        foreach (string make in makes)
        {
            GameObject goButton = Instantiate(button);

            goButton.GetComponentInChildren<TextMeshProUGUI>().text = make;

            var buttonComponent = goButton.GetComponent<Button>();
            buttonComponent.onClick.AddListener(OnButtonClick);

            goButton.transform.SetParent(canvas.transform, false);
        }
    }

    private void OnButtonClick()
    {
        var scenesComponent = scenes.GetComponent<Scenes>();

        globalMake = EventSystem.current.currentSelectedGameObject.GetComponentInChildren<TextMeshProUGUI>().text;
        scenesComponent.ChangeToModels();

        Debug.Log(EventSystem.current.currentSelectedGameObject.GetComponentInChildren<TextMeshProUGUI>().text);
    }

    // Update is called once per frame
    private void Update()
    {
    }
}