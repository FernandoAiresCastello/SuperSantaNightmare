using UnityEngine;
using UnityEngine.UI;

public class NPCCounter : MonoBehaviour
{
    private int Counter = 0;
    private int Total = 0;

    void Start()
    {
        GameObject[] npcs = GameObject.FindGameObjectsWithTag("NPC");
        Total = npcs.Length;
    }

    void OnGUI()
    {
        Text msgContainer = GetComponent<Text>();
        msgContainer.text = "Encounters: " + Counter + "/" + Total;
    }

    public int GetCounter()
    {
        return Counter;
    }

    public bool HasEncounteredAll()
    {
        return Counter >= Total;
    }

    public void IncrementCounter()
    {
        if (Counter < Total)
            Counter++;
    }
}
