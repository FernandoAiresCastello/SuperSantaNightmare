using UnityEngine;

public class BillboardSprite : MonoBehaviour
{
    private Camera Camera;
    private GameObject Scene;

    void Start()
    {
        Camera = Camera.main;
        Scene = GameObject.Find("Scene");
    }

    void LateUpdate()
    {
        transform.forward = Camera.transform.forward;
        transform.up = Scene.transform.up;
    }
}
