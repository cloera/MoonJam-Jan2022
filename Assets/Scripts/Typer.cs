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
    private List<char> charsNeeded = new List<char>();
    private string typedSoFar = "";
    private string generatedPrompt = "";
    private bool messedUp = false;
    private int numberOfMessUps = 0;

    private static readonly string GREEN_TEXT_PREFIX = "<color=#137E02>";
    private static readonly string RED_TEXT_PREFIX = "<color=red>";
    private static readonly string BLACK_TEXT_PREFIX = "<color=black>";

    // Start is called before the first frame update
    void Start()
    {
        textGenerator = FindObjectOfType<TextGenerator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameState.GetGameIsPaused())
        {
            return;
        }

        if (PromptHasChanged())
        {
            generatedPrompt = textGenerator.GetLastGeneratedPrompt();

            charsNeeded = new List<char>(generatedPrompt);

            typedSoFar = "";
        }

        if (Input.anyKeyDown)
        {
            string keysPressed = Input.inputString;

            if (keysPressed.Length == 1)
            {
                EnterLetter(keysPressed[0]);
            }
        }

        OutputTypedSoFar();
    }

    public bool IsFinished()
    {
        return typedSoFar == generatedPrompt;
    }

    public bool MessedUp()
    {
        return messedUp;
    }

    public int GetNumberOfMessUps()
    {
        return numberOfMessUps;
    }

    private void EnterLetter(char letter)
    {
        if (IsCorrectLetter(letter))
        {
            messedUp = false;

            typedSoFar += charsNeeded[0];
            charsNeeded.RemoveAt(0);
        }
        else
        {
            messedUp = true;
            numberOfMessUps += 1;
        }
    }

    private bool IsCorrectLetter(char letter)
    {
        return charsNeeded[0] == letter;
    }

    private bool PromptHasChanged()
    {
        return textGenerator.GetLastGeneratedPrompt() != generatedPrompt;
    }

    private void OutputTypedSoFar()
    {
        string outputText = GREEN_TEXT_PREFIX + typedSoFar;

        if (messedUp)
        {
            char missedChar = charsNeeded[0] == ' ' ? '_' : charsNeeded[0];

            outputText += RED_TEXT_PREFIX + missedChar;
        }
        else if (1 <= charsNeeded.Count)
        {

            outputText += BLACK_TEXT_PREFIX + charsNeeded[0];
        }

        if (2 <= charsNeeded.Count)
        {
            string restOfString = new string(charsNeeded.GetRange(1, charsNeeded.Count - 1).ToArray());

            outputText += BLACK_TEXT_PREFIX + restOfString;
        }

        textOutput.text = outputText;
    }
}
