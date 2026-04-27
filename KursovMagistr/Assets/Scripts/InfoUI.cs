using UnityEngine;
using UnityEngine.UI; //для ui элементов картинок кнопок и тп
using TMPro; //для текста
using System.Collections;


// класс отвечает за панель с информацией (текст и картинка) которая плавно появляется и исчезает
// и закрывается по нажатию кнопок (E, Q, W )
public class InfoUI : MonoBehaviour
{
    public static InfoUI instance; //статическ синглтон
    // можно вызвать InfoUI.instance.ShowInfo(чтото) из любого другого скрипта без поиска объекта на сцене

    private CanvasGroup canvasGroup; // управляет прозрачностью всей панели с инфо 
    //private TextMeshProUGUI textField;
    //private Image imageField;
    [SerializeField] private Image imageField; //используется [SerializeField] чтобы можно было назначить компоненты прямо в инспекторе Unity
    [SerializeField] private TextMeshProUGUI textField; //выше на какой объект на сцене картинку вставлять и аналогично здесь текст
    public float fadeSpeed = 2f; // скорость плавного появления/исчезновения 
    private bool isVisible = false; // булевое для ответа на вопрос - открыта ли сейчас панель? нужно для Update (чтобы ловить нажатия)

    void Awake() // вызывается при создании объекта до первого кадра Update
    {
        instance = this; // запоминаем себя в статической переменной теперь другие скрипты могут писать InfoUI.instance...
        //textField = GetComponentInChildren<TextMeshProUGUI>();
        //imageField = GetComponentInChildren<Image>();
        // поиск или создание CanvasGroup 
        canvasGroup = GetComponent<CanvasGroup>(); // ищем компонент на том же объекте
        

        if (canvasGroup == null)
            canvasGroup = gameObject.AddComponent<CanvasGroup>(); // если не нашли то создаём 
 // начальное состояние (панель невидимая и не отзывается на нажатия) 
        canvasGroup.alpha = 0; //прозрачность
        canvasGroup.interactable = false; //кликабельность
        canvasGroup.blocksRaycasts = false; //лучи рейкасты проходят сквозь панель 
    }

    void Update()
    { //если панель видна и игрок нажал E, Q, W или их мобильные аналоги (MobileInput.E/Q/W)
        if (isVisible && (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.W)
            || MobileInput.E || MobileInput.Q || MobileInput.W))
        {
            HideInfo(); //скрываем панель
        }
    }
//  вызывается из других скриптов чтобы показать панель с нужным текстом и картинкой
    public void ShowInfo(string text, Sprite image) 
    {
        textField.text = text; //заполняем текст
        imageField.sprite = image; //картинку

        StopAllCoroutines(); //
        StartCoroutine(FadeIn()); // запускаем плавное появление
    }
 // скрывает панель
    public void HideInfo()
    {
        StopAllCoroutines();
        StartCoroutine(FadeOut()); //плавное исчезновение
    }
//корутины для повяления/исчезновения
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
