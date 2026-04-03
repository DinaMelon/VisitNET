using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public GameObject infoPanel;
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI descriptionText;
    public Image image;

    private Transform currentPoint;

    void Awake()
    {
        Instance = this;
    }

    void Update()
    {
        // закрытие по W
        if (Input.GetKeyDown(KeyCode.W))
        {
            CloseInfo();
        }

        // панель следует за точкой
        if (infoPanel.activeSelf && currentPoint != null)
        {
            Vector3 screenPos = Camera.main.WorldToScreenPoint(currentPoint.position);
            infoPanel.transform.position = screenPos;
        }
    }

    public void ShowInfo(string title, string description, Sprite sprite, Transform point)
    {
        currentPoint = point;

        infoPanel.SetActive(true);

        titleText.text = title;
        descriptionText.text = description;
        image.sprite = sprite;
    }

    public void CloseInfo()
    {
        infoPanel.SetActive(false);
        currentPoint = null;
    }
}