using UnityEngine;

public class CageDestroyer : MonoBehaviour
{
    public AudioClip Sound;
    private bool Active = true;

    public void OnTriggerEnter(Collider coll)
    {
        if (Active && coll.gameObject.CompareTag("Player"))
        {
            Active = false;
            AudioSource.PlayClipAtPoint(Sound, Camera.main.transform.position);
            Destroy(GameObject.Find("Cage"));
        }
    }
}
