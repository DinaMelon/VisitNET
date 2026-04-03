using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class InfoUI : MonoBehaviour
{
    public static InfoUI instance;

    private CanvasGroup canvasGroup;
    //private TextMeshProUGUI textField;
    //private Image imageField;
    [SerializeField] private Image imageField;
    [SerializeField] private TextMeshProUGUI textField;
    public float fadeSpeed = 2f;
    private bool isVisible = false;

    void Awake()
    {
        instance = this;
        //textField = GetComponentInChildren<TextMeshProUGUI>();
        //imageField = GetComponentInChildren<Image>();
        canvasGroup = GetComponent<CanvasGroup>();
        

        if (canvasGroup == null)
            canvasGroup = gameObject.AddComponent<CanvasGroup>();

        canvasGroup.alpha = 0;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }

    void Update()
    {
        if (isVisible && (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.W)
            || MobileInput.E || MobileInput.Q || MobileInput.W))
        {
            HideInfo();
        }
    }

    public void ShowInfo(string text, Sprite image)
    {
        textField.text = text;
        imageField.sprite = image;

        StopAllCoroutines();
        StartCoroutine(FadeIn());
    }

    public void HideInfo()
    {
        StopAllCoroutines();
        StartCoroutine(FadeOut());
    }

    IEnumerator FadeIn()
    {
        isVisible = true;

        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;

        while (canvasGroup.alpha < 1f)
        {
            canvasGroup.alpha += Time.deltaTime * fadeSpeed;
            yield return null;
        }

        canvasGroup.alpha = 1f;
    }

    IEnumerator FadeOut()
    {
        isVisible = false;

        while (canvasGroup.alpha > 0f)
        {
            canvasGroup.alpha -= Time.deltaTime * fadeSpeed;
            yield return null;
        }

        canvasGroup.alpha = 0f;

        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }
}