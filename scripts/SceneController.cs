using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TechXR.Core.Sense;
using UnityEngine.SceneManagement;


public class SceneController : MonoBehaviour
{
    private void Start()
    {
        Debug.Log(" play");
    }

    // Update is called once per frame
    public void Update()
    {
        if (SenseInput.GetButtonDown("L"))
        {
            Debug.Log("L is pressed");
            SceneManager.LoadScene("bunkerWarehouse");
        }
        if (SenseInput.GetButtonDown("U"))
        {
            SceneManager.LoadScene("Subway");
        }
        if (SenseInput.GetButtonDown("B"))
        {
            Application.Quit();
        }

    }
    //public void subway()
    //{
    //    SceneManager.LoadScene("Subway");
    //}
   
}
