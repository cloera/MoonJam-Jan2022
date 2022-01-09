using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextGenerator : MonoBehaviour
{
    // Configs
    [SerializeField] TextAsset markovSeedTextAsset = null;
    [TextArea(4, 12)] [SerializeField] List<string> storyTextAsset = null;
    [SerializeField] TextMeshProUGUI displayOutput = null;

    // Cache
    private string lastGeneratedPrompt = "";

    // State
    private static int currentStoryIndex = 0;
    private static MarkovChainTextGenerator markovChainTextGenerator = null;

    // Start is called before the first frame update
    void Start()
    {
        markovChainTextGenerator = new MarkovChainTextGenerator(markovSeedTextAsset);
        displayOutput.text = lastGeneratedPrompt;
    }

    // Awake is called when the script instance is being loaded.
    void Awake()
    {
        int numberOfGameStatusInstances = FindObjectsOfType<TextGenerator>().Length;

        if (numberOfGameStatusInstances > 1)
        {
            ResetGenerator();
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (PauseMenu.isPaused())
        {
            return;
        }

        displayOutput.text = lastGeneratedPrompt;
    }

    public string GetLastGeneratedPrompt()
    {
        return lastGeneratedPrompt;
    }

    public string GenerateNextTextPrompt(int percentageChanceOfGettingStory, int numberOfWords)
    {
        if (percentageChanceOfGettingStory < 0)
        {
            percentageChanceOfGettingStory = 0;
        }
        else if (100 < percentageChanceOfGettingStory)
        {
            percentageChanceOfGettingStory = 100;
        }

        int randomNumber = UnityEngine.Random.Range(0, 101);

        if ((randomNumber <= percentageChanceOfGettingStory) && (currentStoryIndex < storyTextAsset.Count))
        {
            lastGeneratedPrompt = storyTextAsset.Skip(currentStoryIndex).Take(1).Single();

            currentStoryIndex += 1;
        }
        else
        {

            lastGeneratedPrompt = markovChainTextGenerator.GenerateRandomSentence(numberOfWords);
        }

        return lastGeneratedPrompt;
    }

    private void ResetGenerator()
    {
        gameObject.SetActive(false);

        Destroy(gameObject);
    }
}

class MarkovChainTextGenerator
{
    private static readonly int KEY_SIZE = 3;
    private Dictionary<string, List<string>> markovChain = new Dictionary<string, List<string>>();

    public MarkovChainTextGenerator(TextAsset textAssetSeed)
    {
        string seed = textAssetSeed.text;

        List<string> seedWords = new List<string>(seed.Split());

        markovChain = GetMarkovChain(seedWords);
    }

    public string GenerateRandomSentence(int numberOfWordsForOutput)
    {
        return GetMarkovChainSentence(markovChain, numberOfWordsForOutput);
    }

    private static Dictionary<string, List<string>> GetMarkovChain(List<string> seedWords)
    {
        Dictionary<string, List<string>> markovChain = new Dictionary<string, List<string>>();

        foreach (int seedWordIndex in Enumerable.Range(0, seedWords.Count - KEY_SIZE))
        {
            string key = seedWords.Skip(seedWordIndex).Take(KEY_SIZE).Aggregate(Join);

            int followingWordIndex = seedWordIndex + KEY_SIZE;

            string followingWord = followingWordIndex < seedWords.Count
                ? seedWords[followingWordIndex]
                : "";

            if (markovChain.ContainsKey(key))
            {
                markovChain[key].Add(followingWord);
            }
            else
            {
                markovChain.Add(key, new List<string>() { followingWord });
            }
        }

        return markovChain;
    }

    private static string GetMarkovChainSentence(Dictionary<string, List<string>> markovChain, int numberOfWords)
    {
        int outputNextStartingIndex = 0;
        int randomMarkovKeyIndex = UnityEngine.Random.Range(0, markovChain.Count);

        string prefix = markovChain.Keys.Skip(randomMarkovKeyIndex).Take(1).Single();

        List<string> output = new List<string>(prefix.Split());

        while (true)
        {
            prefix = output.Skip(outputNextStartingIndex).Take(KEY_SIZE).Aggregate(Join);

            List<string> suffix = markovChain[prefix];

            if ((numberOfWords <= output.Count) || (suffix[0] == ""))
            {
                break;
            }

            int randomSuffixIndex = UnityEngine.Random.Range(0, suffix.Count);

            output.Add(suffix[randomSuffixIndex]);

            outputNextStartingIndex += 1;
        }

        return numberOfWords <= output.Count
            ? output.Take(numberOfWords).Aggregate(Join)
            : output.Aggregate(Join);
    }

    private static string Join(string A, string B)
    {
        return A + " " + B;
    }
}