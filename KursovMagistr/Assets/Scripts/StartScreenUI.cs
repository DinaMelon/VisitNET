using UnityEngine;
using System.Collections;

//==================================================================
// СКРИПТ: StartScreenUI
// НАЗНАЧЕНИЕ: Управляет стартовым экраном с текстом
//              - Показывает текст в начале игры
//              - Блокирует управление игрой на время показа
//              - Через 10 секунд появляется кнопка Закрыть
//              - После закрытия возвращает управление игроку
//==================================================================

public class StartScreenUI : MonoBehaviour
{
    [SerializeField] private GameObject textObject; //текст
    [SerializeField] private GameObject closeButton; // кнопка закрыть

    [SerializeField] private float delayBeforeButton = 10f; //время которое должно пройти 10 сек

    [SerializeField] private MonoBehaviour[] scriptsToDisable; // скрипты которые нужно выключить на время показа экрана

    void Start()
    {// вкл текст выключаем кнопку 
        textObject.SetActive(true);
        closeButton.SetActive(false);

        //откл все скрипты движения/управления переданные в массиве через инспектор юнити
        foreach (var script in scriptsToDisable)
        {
            script.enabled = false;
        }

        Cursor.lockState = CursorLockMode.Locked;// Locked  курсор заперт в центре и невидим 
        Cursor.visible = false;

        StartCoroutine(ShowButtonAfterDelay());// запускаем корутину которая через delayBeforeButton секунд покажет кнопку закрытия
    }

    IEnumerator ShowButtonAfterDelay()
    {
        yield return new WaitForSeconds(delayBeforeButton);

        closeButton.SetActive(true);

            // теперь курсор свободно двигается по экрану и виден
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void CloseScreen()
    {
        gameObject.SetActive(false); // отключаем панель стартового экрана (текст и кнопка исчезают)

         //возвращаем управление игроку включаем все скрипты которые отключили в Start()
        foreach (var script in scriptsToDisable)
        {
            script.enabled = true;
        }

    }
}
