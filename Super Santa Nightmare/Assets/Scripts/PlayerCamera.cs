using UnityEngine;
using System.Collections;

public class PlayerCamera : MonoBehaviour
{
    private GameObject Player;
    private GameObject Scene;
    private Vector3 Offset;

    void Start()
    {
        Player = GameObject.Find("Player");
        Offset = transform.position - Player.transform.position;
    }

    void LateUpdate()
    {
        transform.position = Player.transform.position + Offset;
    }
}
