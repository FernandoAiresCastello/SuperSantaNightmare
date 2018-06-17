using UnityEngine;
using UnityEngine.UI;

public class DialogTrigger : MonoBehaviour
{
    public bool IsNPC = true;
    public string Title;
    public string Message;
    public AudioClip Sound;

    private bool Active = true;
    private DialogBox DialogBox;
    private GameObject NPCCounter;
    private bool Encountered = false;

    void Start()
    {
        DialogBox = GameObject.Find("DialogBox").GetComponent<DialogBox>();
        NPCCounter = GameObject.Find("EncounterText");
    }

    public void OnTriggerEnter(Collider coll)
    {
        if (!coll.gameObject.GetComponent<Collider>().enabled)
            return;

        if (Title == null && Message == null)
        {
            Encounter();
            return;
        }

        if (Active && coll.gameObject.CompareTag("Player"))
        {
            if (Sound != null)
                AudioSource.PlayClipAtPoint(Sound, Camera.main.transform.position);

            DialogBox.ShowDialog(Title + System.Environment.NewLine + Message);
            Encounter();
        }
    }

    void Encounter()
    {
        if (IsNPC && !Encountered)
        {
            Encountered = true;
            NPCCounter.GetComponent<NPCCounter>().IncrementCounter();
        }
    }

    public void OnTriggerExit(Collider coll)
    {
        if (!coll.gameObject.GetComponent<Collider>().enabled)
            return;

        if (Active && coll.gameObject.CompareTag("Player"))
            DialogBox.HideDialog();
    }

    public void SetActive(bool active)
    {
        Active = active;
    }
}
