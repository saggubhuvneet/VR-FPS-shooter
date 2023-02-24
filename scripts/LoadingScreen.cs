using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TechXR.Core.Sense;

public class LoadingScreen : MonoBehaviour
{
    private void Update()
    {
        StartCoroutine(StartGame());
    }
    IEnumerator StartGame()
    {
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("MAinMenu");
    }
}
