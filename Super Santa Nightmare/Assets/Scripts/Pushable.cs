using UnityEngine;

public class Pushable : MonoBehaviour
{
    public AudioClip Sound;
    private bool Pushed = false;
    private float Distance = 0.0f;
    private float Speed = 0.05f;

    void Update()
    {
        if (Pushed && Distance < 1.0f)
        {
            Distance += Speed;
            transform.Translate(Speed, 0, 0);
        }
    }

    public void OnTriggerEnter(Collider coll)
    {
        if (!Pushed && coll.gameObject.name == "Player" && 
            (coll.gameObject.transform.position.x - transform.position.x < 0.1))
        {
            Pushed = true;
            AudioSource.PlayClipAtPoint(Sound, Camera.main.transform.position);
        }
    }
}
