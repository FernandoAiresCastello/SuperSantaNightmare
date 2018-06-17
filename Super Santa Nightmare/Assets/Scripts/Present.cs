using UnityEngine;

public class Present : MonoBehaviour
{
    public AudioClip Sound;
    private GameObject Scene;

    public void Start()
    {
        Scene = GameObject.Find("Scene");
    }

    public void OnTriggerEnter(Collider coll)
    {
        AudioSource.PlayClipAtPoint(Sound, Camera.main.transform.position);
        Scene.GetComponent<Scene>().IncrementPresentCount();
        Destroy(gameObject);
    }
}
