using UnityEngine;
using UnityEngine.InputSystem;


//==================================================================
// СКРИПТ: MoveBetweenPoints
// НАЗНАЧЕНИЕ: 
//   Плавно перемещает пользователя между заданными точками (массив points)
//   и одновременно поворачивает его в сторону соответствующих целей (lookTargets)
// УПРАВЛЕНИЕ:
//   - E следующая точка
//   - Q предыдущая точка
//   - Работает также на мобильных устройствах (MobileInput.E / MobileInput.Q)
//==================================================================


public class MoveBetweenPoints : MonoBehaviour
{
    public Transform[] points; //массив позиций куда будет двигаться объект

    public Transform[] lookTargets;// массив объектов на которые объект будет смотреть

    public int currentPoint = 0;  //номер текущей точки индекс в массиве points
    public float moveSpeed = 3f; //скорость плавного перемещения между точками
    public float rotateSpeed = 5f; //скорость плавного поворота в сторону цели

    void Update()
    {
        // âïåðåä (E)
        if (Keyboard.current.eKey.wasPressedThisFrame || MobileInput.E) //если нажата клавиша E то 
        {
            currentPoint++; //перемещаемся на след точку

            if (currentPoint >= points.Length) //если находимся на последней точке
                currentPoint = 0; //то тлепортируемся к первой точке
        }

        // íàçàä (Q)
        if (Keyboard.current.qKey.wasPressedThisFrame || MobileInput.Q)
        {
            currentPoint--;//перемещаемся на пред точку

            if (currentPoint < 0)   //если ушли в минус  то перекидываем на последний элемент массива
                currentPoint = points.Length - 1; 
        }

        //Lerp (Linear Interpolation) = плавный переход между текущей позицией и целевой
        //Time.deltaTime нужен чтобы скорость не зависела от кадров в секунду
        transform.position = Vector3.Lerp(
            transform.position,
            points[currentPoint].position,
            Time.deltaTime * moveSpeed
        );

          //вычисляем вектор направления от объекта до цели
        //превращаем его в поворот (Quaternion)
        //плавно поворачиваем объект в эту сторону
        Vector3 direction = lookTargets[currentPoint].position - transform.position;
        Quaternion targetRotation = Quaternion.LookRotation(direction);

        transform.rotation = Quaternion.Lerp(
            transform.rotation,
            targetRotation,
            Time.deltaTime * rotateSpeed
        );
    }
}
