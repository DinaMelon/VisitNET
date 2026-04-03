using UnityEngine;

public class MobileInput : MonoBehaviour
{
    public static bool Q;
    public static bool W;
    public static bool E;
    public static bool H;

    void LateUpdate()
    {
        // сбрасываем каждый кадр (как GetKeyDown)
        Q = false;
        W = false;
        E = false;
        H = false;
    }

    public void PressQ()
    {
        Q = true;
    }

    public void PressW()
    {
        W = true;
    }

    public void PressE()
    {
        E = true;
    }

    public void PressH()
    {
        H = true;
    }
}