using UnityEngine;

public class InteractReycast : MonoBehaviour
{
    public float distance = 5f;

    private NETObject currentObject;

    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, distance))
        {
            NETObject obj = hit.collider.GetComponent<NETObject>();

            if (obj != null)
            {
                if (currentObject != obj)
                {
                    if (currentObject != null)
                        currentObject.RemoveHighlight();

                    currentObject = obj;
                    currentObject.Highlight();
                }

                if (Input.GetMouseButtonDown(0))
                {
                    currentObject.ShowInfo();
                }

                return;
            }
        }

        if (currentObject != null)
        {
            currentObject.RemoveHighlight();
            currentObject = null;
        }
    }
}