using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CharacterController))]
public class Player : MonoBehaviour
{
    public bool Debug = false;
    public float Speed = 6.0F;
    public float JumpSpeed = 8.0F;
    public float Gravity = 20.0F;
    public float MinHeight = -50.0f;

    public Sprite DeathSprite;
    public AudioClip JumpSound;
    public AudioClip FallSound;

    private Vector3 MoveDirection = Vector3.zero;
    private Vector3 InitialPosition;
    private Quaternion InitialRotation;
    private GameObject Scene;
    private GameObject DialogBox;
    private bool Finished = false;
    private SpriteRenderer Renderer;
    private int FallCount = 0;
    private bool Active = true;

    void Start()
    {
        Scene = GameObject.Find("Scene");
        DialogBox = GameObject.Find("DialogBox");
        GameObject.Find("GoodEndingLabel").GetComponent<CanvasRenderer>().SetAlpha(0.0f);

        InitialPosition = transform.position;
        InitialRotation = transform.rotation;
        Renderer = GetComponent<SpriteRenderer>();

        ShowCredits(false);
    }

    void Update()
    {
        if (!Active)
            return;

        if (!Finished)
        {
            if (Debug && Input.GetKeyDown(KeyCode.F1))
                Scene.GetComponent<Scene>().IncrementPresentCount();
            if (Debug && Input.GetKeyDown(KeyCode.F2))
                GameObject.Find("EncounterText").GetComponent<NPCCounter>().IncrementCounter();
            if (Debug && Input.GetKeyDown(KeyCode.F3))
                GameObject.Find("GoodEndingLabel").GetComponent<CanvasRenderer>().SetAlpha(100.0f);

            CharacterController controller = GetComponent<CharacterController>();

            if (controller.isGrounded)
            {
                MoveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
                MoveDirection = transform.TransformDirection(MoveDirection);
                MoveDirection *= Speed;

                if (Input.GetButtonDown("Jump"))
                {
                    MoveDirection.y = JumpSpeed;
                    AudioSource.PlayClipAtPoint(JumpSound, Camera.main.transform.position);
                }
            }

            MoveDirection.y -= Gravity * Time.deltaTime;
            controller.Move(MoveDirection * Time.deltaTime);
        }
        else // Finished!
        {
            GameObject boy = GameObject.Find("Jimmy");
            if (boy != null)
                Destroy(boy);

            GetComponent<BoxCollider>().enabled = false;

            transform.Translate(0, 0, -0.5f);
            Scene.GetComponent<Scene>().transform.Rotate(0, Time.deltaTime * 50.0f * 4.0f, 0);

            string extra = "";
            if (GameObject.Find("EncounterText").GetComponent<NPCCounter>().HasEncounteredAll() &&
                GameObject.Find("GoodEndingLabel").GetComponent<CanvasRenderer>().GetAlpha() >= 100.0f)
                extra = " *** BUT YOU ARE SUPER PLAYER! ***";

            DialogBox.GetComponent<DialogBox>().ShowDialog(
                "Jimmy says:" + System.Environment.NewLine +
                "Finally! Next time bring my presents BEFORE christmas, are you listening?! Shoo, I'm going home! " +
                "And yes, this is a nightmare, but now you're DEAD! Hah hah! (You've been bamboozled to death!)" +
                System.Environment.NewLine + System.Environment.NewLine + "GAME OVER" + extra);
        }
    }

    void ShowCredits(bool show)
    {
        GameObject box = GameObject.Find("CreditsBox");
        box.GetComponent<CanvasRenderer>().SetAlpha(show ? 100.0f : 0.0f);
        foreach (Transform child in box.transform)
            child.gameObject.GetComponent<CanvasRenderer>().SetAlpha(show ? 100.0f : 0.0f);
    }

    public bool IsFinished()
    {
        return Finished;
    }

    void LateUpdate()
    {
        if (transform.position.y < MinHeight / 2)
            AudioSource.PlayClipAtPoint(FallSound, Camera.main.transform.position);

        if (!Finished && (transform.position.y < MinHeight))
            Reset();
    }

    public void Reset()
    {
        Scene.GetComponent<Scene>().Reset();
        transform.position = InitialPosition;
        transform.rotation = InitialRotation;
        FallCount++;
    }

    public void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.name == "Jimmy")
        {
            if (Scene.GetComponent<Scene>().GetPresentCount() < Scene.GetComponent<Scene>().GetMaxPresentCount())
            {
                DialogBox.GetComponent<DialogBox>().ShowDialog(
                    "Jimmy says:" + System.Environment.NewLine + 
                    "Santa, where are my presents?! I want my presents and I want them NOW! Go, go, go, you're freakin' late! " +
                    "After you find all of my presents bring them to me, but I won't forgive you, no, NOT again! Move with ARROW keys, derp! " +
                    "(Jimmy looks impatient)");
            }
            else
            {
                Finished = true;
                Renderer.sprite = DeathSprite;
                GetComponent<SimpleFlipAnimator>().Active = false;
                GameObject.Find("ClockText").GetComponent<Clock>().Stop();

                ShowCredits(true);
            }
        }
        else if (coll.gameObject.name == "Pushable")
        {
            coll.gameObject.transform.Translate(1, 0, 0);
        }
        else if (coll.gameObject.name == "Princess")
        {
            GameObject.Find("GoodEndingLabel").GetComponent<CanvasRenderer>().SetAlpha(100.0f);
        }
    }

    public void OnTriggerExit(Collider coll)
    {
        if (coll.gameObject.name == "Jimmy")
        {
            if (Finished)
                Destroy(coll.gameObject);
            else
                DialogBox.GetComponent<DialogBox>().HideDialog();
        }
    }

    void OnGUI()
    {
        GameObject dialogText = GameObject.Find("FallText");
        Text msgContainer = dialogText.GetComponent<Text>();
        msgContainer.text = "Falls: " + FallCount;
    }
}
