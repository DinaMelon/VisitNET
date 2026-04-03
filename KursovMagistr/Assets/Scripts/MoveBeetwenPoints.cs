using UnityEngine;
using UnityEngine.InputSystem;

public class MoveBetweenPoints : MonoBehaviour
{
    public Transform[] points;
    public Transform[] lookTargets;

    public int currentPoint = 0;
    public float moveSpeed = 3f;
    public float rotateSpeed = 5f;

    void Update()
    {
        // вперед (E)
        if (Keyboard.current.eKey.wasPressedThisFrame || MobileInput.E)
        {
            currentPoint++;

            if (currentPoint >= points.Length)
                currentPoint = 0;
        }

        // назад (Q)
        if (Keyboard.current.qKey.wasPressedThisFrame || MobileInput.Q)
        {
            currentPoint--;

            if (currentPoint < 0)
                currentPoint = points.Length - 1;
        }

        // движение
        transform.position = Vector3.Lerp(
            transform.position,
            points[currentPoint].position,
            Time.deltaTime * moveSpeed
        );

        // поворот к объекту
        Vector3 direction = lookTargets[currentPoint].position - transform.position;
        Quaternion targetRotation = Quaternion.LookRotation(direction);

        transform.rotation = Quaternion.Lerp(
            transform.rotation,
            targetRotation,
            Time.deltaTime * rotateSpeed
        );
    }
}