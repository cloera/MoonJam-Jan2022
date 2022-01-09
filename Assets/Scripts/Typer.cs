using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Typer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI textOutput = null;

    // Cache
    private TextGenerator textGenerator = null;

    // State
    [SerializeField] private List<char> charsNeeded = new List<char>();
    private string typedSoFar = "";
    [SerializeField] private string generatedPrompt = "";
    private static bool isFinished = false;

    // Start is called before the first frame update
    void Start()
    {
        textGenerator = FindObjectOfType<TextGenerator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (PauseMenu.isPaused())
        {
            return;
        }

        if (PromptHasChanged())
        {
            generatedPrompt = textGenerator.GetLastGeneratedPrompt();

            charsNeeded = new List<char>(generatedPrompt);

            typedSoFar = "";
        }

        isFinished = typedSoFar == generatedPrompt;
    }

    private bool PromptHasChanged()
    {
        return textGenerator.GetLastGeneratedPrompt() != generatedPrompt;
    }

    public static bool IsFinished()
    {
        return isFinished;
    }
}
