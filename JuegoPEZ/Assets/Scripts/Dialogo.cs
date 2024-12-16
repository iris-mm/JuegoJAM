using UnityEngine;
using System.Collections;
using TMPro;

public class Dialogo : MonoBehaviour
{
    [SerializeField, TextArea(4, 5)] private string[] lineasDialog;
    [SerializeField] private GameObject dialogo;
    [SerializeField] private TMP_Text textoDialog;

    private bool enRango;
    private bool didDialogueStart;
    private int lineIndex;
    private float typingTime = 0.05f;

    public void Update()
    {
        if (enRango && Input.GetKeyDown("c")) {
            
            if (!didDialogueStart)
            {
                StartDialogue();
            }
            else if(textoDialog.text == lineasDialog[lineIndex])
            {
                nextDialogueLine();
            }
            else
            {
                StopAllCoroutines();
                textoDialog.text = lineasDialog[lineIndex];
            }
        }
    }

    private void StartDialogue()
    {
        didDialogueStart = true;
        dialogo.SetActive(true);
        lineIndex = 0;
        Time.timeScale = 0f;
        StartCoroutine(showLine());
    }

    private void nextDialogueLine()
    {
        lineIndex++;
        if(lineIndex < lineasDialog.Length)
        {
            StartCoroutine(showLine());
        }
        else
        {
            didDialogueStart = false;
            dialogo.SetActive(false);
            Time.timeScale = 1f;
        }
    }

    private IEnumerator showLine()
    {
        textoDialog.text = string.Empty;

        foreach (char ch in lineasDialog[lineIndex])
        {
            textoDialog.text += ch;
            yield return new WaitForSecondsRealtime(typingTime);
        }
    } 
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            enRango = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            enRango = false;
        }
    }

}
