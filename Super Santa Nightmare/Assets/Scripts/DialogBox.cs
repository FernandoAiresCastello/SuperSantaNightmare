using UnityEngine;
using UnityEngine.UI;

public class DialogBox : MonoBehaviour
{
    private CanvasRenderer Renderer;
    private GameObject Text;

    public void Start()
    {
        Renderer = gameObject.GetComponent<CanvasRenderer>();
        Text = GameObject.Find("DialogText");

        HideDialog();
    }

    public void HideDialog()
    {
        Renderer.SetAlpha(0.0f);
        Text.GetComponent<CanvasRenderer>().SetAlpha(0.0f);
    }

    public void ShowDialog(string msg)
    {
        Renderer.SetAlpha(100.0f);
        Text.GetComponent<CanvasRenderer>().SetAlpha(100.0f);

        GameObject dialogText = GameObject.Find("DialogText");
        Text msgContainer = dialogText.GetComponent<Text>();
        msgContainer.text = msg;
    }
}
