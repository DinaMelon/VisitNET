using UnityEngine;

public class InterestPoint : MonoBehaviour
{
    [TextArea(5, 15)]
    public string fullInfo;

    public Sprite image;

    public void ShowInfo()
    {
        if (InfoUI.instance != null)
        {
            if (image == null)
            {
                Debug.LogError("Картинка не назначена для " + gameObject.name);
            }
            InfoUI.instance.ShowInfo(fullInfo, image);
        }
        
    }

    public void HideInfo()
    {
        if (InfoUI.instance != null)
        {
            InfoUI.instance.HideInfo();
        }
    }
}