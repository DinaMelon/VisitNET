using UnityEngine;

public class NETObject : MonoBehaviour
{
    private Outline outline;

    public InterestPoint interestPoint;

    private static NETObject currentActive;


    void Start()
    {
        outline = GetComponent<Outline>();

        if (outline != null)
            outline.enabled = false;
    }

    void Update()
    {

        if (currentActive == this && (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.W)
            || MobileInput.E || MobileInput.Q || MobileInput.W))
        {
            HideInfo();
            currentActive = null;
        }
    }

    public void Highlight()
    {
        if (outline != null)
            outline.enabled = true;
    }

    public void RemoveHighlight()
    {
        if (outline != null)
            outline.enabled = false;
    }

    public void ShowInfo()
    {
        if (currentActive == this)
            return;

        if (currentActive != null)
            currentActive.HideInfo();

        if (interestPoint != null)
            interestPoint.ShowInfo();

        currentActive = this;
    }

    public void HideInfo()
    {
        if (interestPoint != null)
            interestPoint.HideInfo();
    }
}