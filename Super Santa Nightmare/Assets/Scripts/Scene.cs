using UnityEngine;
using UnityEngine.UI;

public class Scene : MonoBehaviour
{
    public float RotationSpeed = 30.0f;

    //private Quaternion InitialRotation;
    //private Quaternion InitialParentRotation;
    private Transform ParentTransform;
    private float RotationX = 0.0f;
    private int PresentCount = 0;
    private int PresentMax = 0;

    void Start()
    {
        //InitialRotation = transform.rotation;
        //InitialParentRotation = transform.parent.transform.rotation;
        ParentTransform = transform.parent.gameObject.transform;

        CountPresents();
    }

    void CountPresents()
    {
        GameObject[] presents = GameObject.FindGameObjectsWithTag("Present");
        PresentMax = presents.Length;
    }

    public void IncrementPresentCount()
    {
        if (PresentCount < PresentMax)
            PresentCount++;
    }

    public int GetPresentCount()
    {
        return PresentCount;
    }

    public int GetMaxPresentCount()
    {
        return PresentMax;
    }

    public void SetPresentCount(int count)
    {
        PresentCount = count;
    }

    void OnGUI()
    {
        GameObject dialogText = GameObject.Find("CounterText");
        Text msgContainer = dialogText.GetComponent<Text>();
        msgContainer.text = "Presents: " + PresentCount + "/" + PresentMax;
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.A))
            transform.Rotate(0, Time.deltaTime * RotationSpeed, 0);
        else if (Input.GetKey(KeyCode.D))
            transform.Rotate(0, Time.deltaTime * -RotationSpeed, 0);

        if (Input.GetKey(KeyCode.S))
            RotationX += Time.deltaTime * RotationSpeed;
        else if (Input.GetKey(KeyCode.W))
            RotationX += Time.deltaTime * -RotationSpeed;

        RotationX = Mathf.Clamp(RotationX, -10.0f, 50.0f);
        ParentTransform.localEulerAngles = new Vector3(
            RotationX, ParentTransform.localEulerAngles.y, ParentTransform.localEulerAngles.z);
    }

    public void Reset()
    {
        transform.rotation = new Quaternion(0, 0, 0, 0);
        transform.parent.transform.rotation = new Quaternion(0, 0, 0, 0);
        transform.parent.gameObject.transform.position = ParentTransform.position;
    }
}
