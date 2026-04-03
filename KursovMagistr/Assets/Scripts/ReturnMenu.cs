using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class ReturnMenu : MonoBehaviour
{
    [SerializeField] private string startSceneName = "Main";
    [SerializeField] private Image fadeImage;
    [SerializeField] private float fadeDuration = 1f;

    private bool isTransitioning = false;
    void Start()
    {
        // Убираем чёрный экран при старте
        fadeImage.gameObject.SetActive(false);
        Color color = fadeImage.color;
        color.a = 0f;
        fadeImage.color = color;
    }
    void Update()
    {
        if (!isTransitioning && (Input.GetKeyDown(KeyCode.H) || GetMobileH()))
        {
            StartCoroutine(Transition());
        }
    }
    bool GetMobileH()
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
        // Сброс состояния
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        // Fade OUT (затемнение)
        yield return StartCoroutine(Fade(0f, 1f));

        // Загрузка сцены
        SceneManager.LoadScene(startSceneName, LoadSceneMode.Single);

        // Ждём 1 кадр после загрузки
        yield return null;

        // проявление)
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