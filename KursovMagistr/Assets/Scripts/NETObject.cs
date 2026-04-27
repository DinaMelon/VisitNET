using UnityEngine;

//==================================================================
// СКРИПТ: NETObject
// НАЗНАЧЕНИЕ: Управляет подсветкой и отображением информации
//              об интерактивном объекте 
//==================================================================

// этот скрипт вешается на любой объект с которым может взаимодействовать игрок
public class NETObject : MonoBehaviour
{
    private Outline outline;   //компонент Outline из пакета Quick Outline. контур вокруг объекта

    // public InterestPoint interestPoint; //точка интереса  показывает окно с текстом и картинкой

    private static NETObject currentActive; //какой объект сейчас показывает информацию



    void Start()
    {
        outline = GetComponent<Outline>();  // ищем компонент Outline на этом же объекте

        if (outline != null) // Если Outline найден  выключаем его сразу
            outline.enabled = false;
    }

    void Update()
    {
 //если этот объект является активным (открыта его подсказка)
        // и если игрок нажал одну из кнопок закрытия
        if (currentActive == this && (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.W)
            || MobileInput.E || MobileInput.Q || MobileInput.W))
        {
            // HideInfo();  // закрываем инфо
            currentActive = null; //обнуляем ссылку на активный объект 
        }
    }
//когда игрок навёлся на объект
    public void Highlight()
    {
        if (outline != null)
            outline.enabled = true;// включаем свечение по контуру
    }

    public void RemoveHighlight()
    {
        if (outline != null)
            outline.enabled = false;// выключаем свечение по контуру
    }

    // public void ShowInfo()
    // {
    //     if (currentActive == this)
    //         return;

    //     if (currentActive != null)
    //         currentActive.HideInfo();

    //     if (interestPoint != null)
    //         interestPoint.ShowInfo();

    //     currentActive = this;
    // }

    // public void HideInfo()
    // {
    //     if (interestPoint != null)
    //         interestPoint.HideInfo();
    // }
}
