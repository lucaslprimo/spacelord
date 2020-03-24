using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransictions : MonoBehaviour
{
    private Animator animatorPanel;

    void Start()
    {
        animatorPanel = GetComponent<Animator>();
    }

    public void LoadScene(string scene)
    {
        StartCoroutine(Transition(scene));
    }


    IEnumerator Transition(string scene)
    {
        animatorPanel.SetTrigger("end");
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(scene);
    }
}
