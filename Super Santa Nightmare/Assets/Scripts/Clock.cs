using UnityEngine;
using UnityEngine.UI;

public class Clock : MonoBehaviour
{
    private float Timer;
    private bool Running = true;

    void Update()
    {
        if (Running)
            Timer += Time.deltaTime;
    }

    void OnGUI()
    {
        string minutes = Mathf.Floor(Timer / 60).ToString("00");
        string seconds = (Timer % 60).ToString("00");

        Text clock = GetComponent<Text>();
        clock.text = minutes + ":" + seconds;
    }

    public void Stop()
    {
        Running = false;
    }
}
