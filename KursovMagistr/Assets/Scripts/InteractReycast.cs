using UnityEngine;

// класс отвечает за взаимодействие с объектами через луч из центра камеры (Raycast
// клик по объекту для показа информации и подсветка при наведении
public class InteractReycast : MonoBehaviour
{
    public float distance = 5f;  //макс дистанция на которой можно взаимодействовать с объектом

    private NETObject currentObject;//ссылка на объект на который сейчас наведён курсор 

    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); // луч идёт из центра камеры (ScreenPointToRay) в направлении курсора мыши
        RaycastHit hit;//для хранения информации о том во что попал луч

        if (Physics.Raycast(ray, out hit, distance))   //Physics.Raycast пускает луч и проверяет есть ли на расстоянии какой-то коллайдер
        {
            NETObject obj = hit.collider.GetComponent<NETObject>();   //пытаемся найти на объекте в который попали скрипт NETObject

            if (obj != null) // если такой компонент есть то объект интерактивный
            {
                if (currentObject != obj) //если навели на новый объект (не тот на который смотрели до этого)
                {
                    if (currentObject != null) // убираем подсветку со старого объекта (если он был)
                        currentObject.RemoveHighlight();

                    currentObject = obj; // запоминаем новый объект как текущий
                    currentObject.Highlight(); // вкл подсветку на новом объекте
                }

                if (Input.GetMouseButtonDown(0))  //если игрок нажал левую кнопку мыши
                {
                    currentObject.ShowInfo();  //показываем инфо об объекте
                }

                return;
            }
        }

        if (currentObject != null)  //если до этого был подсвечен какой-то объект то убираем подсветку и обнуляем ссылку
        {
            currentObject.RemoveHighlight();
            currentObject = null;
        }
    }
}
