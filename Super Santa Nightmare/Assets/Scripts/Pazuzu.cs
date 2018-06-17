using UnityEngine;

public class Pazuzu : MonoBehaviour
{
    public AudioClip Sound;
    public float XDistance = 0.0f;
    public float YDistance = 0.0f;
    public float ZDistance = 0.0f;

    public void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.name == "Player")
            AudioSource.PlayClipAtPoint(Sound, Camera.main.transform.position);
    }

    public void OnTriggerStay(Collider coll)
    {
        if (coll.gameObject.name == "Player")
        {
            int chance = Random.Range(0, 12);

            if (chance >= 11)
            {
                float x = Random.Range(0.0f, 0.1f + XDistance);
                float y = Random.Range(0.0f, 0.1f + YDistance);
                float z = Random.Range(0.0f, 0.1f + ZDistance);

                coll.gameObject.transform.Translate(x, y, z);
            }
        }
    }
}
