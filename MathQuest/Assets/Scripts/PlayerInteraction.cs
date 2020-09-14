using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerInteraction : MonoBehaviour
{
    public GameObject instructionAccept;
    public GameObject loadingScreen;
    public Animator animator;

    bool readyToFight = false;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Accept") && readyToFight)
        {
            StartFight();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            readyToFight = true;
            instructionAccept.SetActive(true);
            animator.SetBool("Dialogue", true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            readyToFight = false;
            instructionAccept.SetActive(false);
            animator.SetBool("Dialogue", false);
        }
    }

    void StartFight()
    {
        loadingScreen.SetActive(true);
        SceneManager.LoadScene("FightScene");
    }
}
