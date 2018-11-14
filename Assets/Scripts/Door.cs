using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    //private void OnTriggerStay2D(Collider2D collision)
    //{
    //    if (collision.CompareTag("Player"))
    //    {
    //        if(Input.GetButtonDown("Activate"))
    //        {

    //        Debug.Log("player has door activated");
    //        }
    //    }
    //}
    [SerializeField]
    private string sceneToLoad;
    private bool isPlayerInTrigger;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerInTrigger = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerInTrigger = false;
        }
    }

    private void Update()
    {
        if (Input.GetButtonDown("Activate") && isPlayerInTrigger)
        {
            Debug.Log("player activated trigger");
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}
