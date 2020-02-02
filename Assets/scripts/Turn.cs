using UnityEngine;

public class Turn : MonoBehaviour
{
    // Start is called before the first frame update
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
        float dt = Time.deltaTime;
        float y = 45 * dt;

        this.gameObject.transform.Rotate(0, y, 0, Space.World);
    }
}