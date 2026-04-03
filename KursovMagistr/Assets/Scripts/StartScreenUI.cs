using UnityEngine;
using System.Collections;

public class StartScreenUI : MonoBehaviour
{
    [SerializeField] private GameObject textObject;
    [SerializeField] private GameObject closeButton;

    [SerializeField] private float delayBeforeButton = 10f;

    [SerializeField] private MonoBehaviour[] scriptsToDisable;

    void Start()
    {
        textObject.SetActive(true);
        closeButton.SetActive(false);

        // 
        foreach (var script in scriptsToDisable)
        {
            script.enabled = false;
        }

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        StartCoroutine(ShowButtonAfterDelay());
    }

    IEnumerator ShowButtonAfterDelay()
    {
        yield return new WaitForSeconds(delayBeforeButton);

        closeButton.SetActive(true);

        // Разрешаем курсор (только для UI)
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void CloseScreen()
    {
        gameObject.SetActive(false);

        // 
        foreach (var script in scriptsToDisable)
        {
            script.enabled = true;
        }

    }
}