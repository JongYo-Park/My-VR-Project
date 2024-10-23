using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EscapeDoor : MonoBehaviour
{
    private int totalZombies;

    void Start()
    {
        totalZombies = FindObjectsOfType<Zombie>().Length;
    }

    public void ZombieDestroyed()
    {
        totalZombies--;

        if (totalZombies <= 0)
        {
            StartCoroutine(LoadEndingScene(5f));
        }
    }

    private IEnumerator LoadEndingScene(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene("EndingScene");
    }
}
