using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class NextLevel : MonoBehaviour
{
    public int num;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Scenes();
        }
        
    }
    private void Scenes()
    {
        switch (num)
        {
            case 1:
                SceneManager.LoadScene("Level2");
                break;
            case 2:
                SceneManager.LoadScene("Level3");
                break;
        }

    }
}
