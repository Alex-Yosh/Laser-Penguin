using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Cutscene : MonoBehaviour
{
    [SerializeField] AudioSource audio;
    // Start is called before the first frame update


    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return))
        {
            GoToTitle();
        }
    }

    public void GoToTitle()
    {
        SceneManager.LoadScene("Title Screen");
    }

    public void PlaySound()
    {
        audio.Play();
    }
}
