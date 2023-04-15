using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public Animator animator;
    public float transistionTime = 1f;

    public void LoadNextLevel()
    {

        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));

    }

    IEnumerator LoadLevel(int levelIndex)
    {

        animator.SetTrigger("Start");
        yield return new WaitForSeconds(transistionTime);
        SceneManager.LoadScene(levelIndex);

    }
}
