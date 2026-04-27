using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

//==================================================================
// СКРИПТ: ReturnMenu
// НАЗНАЧЕНИЕ: Возврат в главное меню при нажатии кнопки H
//==================================================================

public class ReturnMenu : MonoBehaviour
{
    [SerializeField] private string startSceneName = "Main"; // имя сцены куда возвращаемся
    [SerializeField] private Image fadeImage; // чёрное изображение на весь экран для затемнения
    [SerializeField] private float fadeDuration = 1f;// Сколько секунд длится затемнение

    private bool isTransitioning = false;// true = уже идёт перехо чтобы не запустить его дважды
    void Start()
    {
         //делаем чёрный экран неактивным прячем его
        fadeImage.gameObject.SetActive(false);
        Color color = fadeImage.color;
        color.a = 0f;
        fadeImage.color = color;
    }
    void Update()
    {//если переход ещё не начался и (нажата клавиша H на клавиатуре или нажата мобильная кнопка H)
        if (!isTransitioning && (Input.GetKeyDown(KeyCode.H) || GetMobileH()))
        {
            StartCoroutine(Transition());// запускаем корутину чтобы сделать плавную анимацию
        }
    }
    bool GetMobileH() // проверяет нажата ли виртуальная кнопка H 
    {
        if (MobileInput.H)
        {
            MobileInput.H = false;
            return true;
        }

        return false;
    }
    IEnumerator Transition()
    {
        isTransitioning = true;
        fadeImage.gameObject.SetActive(true);
        // Ñáðîñ ñîñòîÿíèÿ
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        // Fade OUT (çàòåìíåíèå)
        yield return StartCoroutine(Fade(0f, 1f));

        // Çàãðóçêà ñöåíû
        SceneManager.LoadScene(startSceneName, LoadSceneMode.Single);

        // Æä¸ì 1 êàäð ïîñëå çàãðóçêè
        yield return null;

        // ïðîÿâëåíèå)
        yield return StartCoroutine(Fade(1f, 0f));
        fadeImage.gameObject.SetActive(false);
        isTransitioning = false;
    }

    IEnumerator Fade(float start, float end)
    {
        float time = 0f;
        Color color = fadeImage.color;

        while (time < fadeDuration)
        {
            float t = time / fadeDuration;
            color.a = Mathf.Lerp(start, end, t);
            fadeImage.color = color;

            time += Time.unscaledDeltaTime;
            yield return null;
        }

        color.a = end;
        fadeImage.color = color;
    }
}
