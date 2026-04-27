using UnityEngine;
using UnityEngine.SceneManagement;
public class start : MonoBehaviour
{
    //загружаем сцену с экскурсией
    public void StartGame()
    {
        SceneManager.LoadScene("SampleScene");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
