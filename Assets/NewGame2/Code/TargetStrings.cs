using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TargetStrings : MonoBehaviour
{
    #region(Public variables)
    [Header("Insert the target objects")]
    public GameObject trash;
    public GameObject present;

    public Transform[] switchPos;

    [Header("Insert the cubes objects")]
    public GameObject rightTranceCube;
    public GameObject leftTranceCube;

    [Header("Insert the cubes Canvas images")]
    public RawImage rightImage;
    public RawImage leftImage;

    [Header("Insert the mood images")]
    public Texture2D[] moods;

    [Header("Insert the target animator objects")]
    public Animator trashBin;
    public Animator gift;

    [Header("Insert the texts")]
    public List<TMP_Text> words;

    public List<string> goodWords = new List<string>();
    public List<string> badWords = new List<string>();

    [Header("Answer values")]
    public AudioSource winAudio;
    public AudioSource loseAudio;

    [Header("Debug values, DON'T TOUCH")]
    public int CharachterNum;

    #endregion

    [HideInInspector]
    public int moodRandom;
    [HideInInspector]
    public int goodTextRandom;
    [HideInInspector]
    public int badTextRandom;
    [HideInInspector]
    public int randNum;

    public void Flip(float waitTime)
    {
        StartCoroutine(FlipLid(waitTime));
    }

    public void ShuffleWords(float waitTime)
    {
        StartCoroutine(SetRoundWords(waitTime));
    }

    public void CleanWords(float waitTime)
    {
        StartCoroutine(ResetWords(waitTime));
    }
    
    public void RotateImages(float waitTime)
    {
        StartCoroutine(RotateCubes(waitTime));
    }

    public void SetPosition(float waitTime)
    {
        StartCoroutine(LocationTargets(waitTime));
    }

    public void ImageSet(float waitTime)
    {
        StartCoroutine(MoodShuffle(waitTime));
    }

    public void RandValue()
    {
        randNum = Random.Range(0, moods.Length);
    }


    #region(IEnumerators)
    public IEnumerator RotateCubes(float waitTime)  // Rotate the cubes 180 degrees
    {
        yield return new WaitForSeconds(waitTime);

        rightTranceCube.GetComponent<Animator>().SetTrigger("Rotate");
        leftTranceCube.GetComponent<Animator>().SetTrigger("Rotate");
    }

    public IEnumerator SetRoundWords(float waitTime)     // Shuffle the mood images and the text
    {
        yield return new WaitForSeconds(waitTime);

        // Random the text
        goodTextRandom = Random.Range(0, goodWords.Count);
        badTextRandom = Random.Range(0, badWords.Count);

        CharachterNum = goodWords[goodTextRandom].Length;

        // Random the left good
        words[0].text = goodWords[goodTextRandom];

        // Random the right good
        words[1].text = badWords[badTextRandom];
    }

    public IEnumerator ResetWords(float waitTime) // Reset the words images to blank
    {
        yield return new WaitForSeconds(waitTime);

        // Reset the text
        words[0].text = null;
        words[1].text = null;
    }

    public IEnumerator FlipLid(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);

        trashBin.SetTrigger("Flip");
        gift.SetTrigger("Flip");
    }

    public IEnumerator LocationTargets(float waitTime)  // Set the location of the targets
    {
        yield return new WaitForSeconds(waitTime);
        Vector3 presentPos = present.transform.position;
        Vector3 trashPos = trash.transform.position;


        // If the position is left
        if (randNum == 0)
        {
            presentPos.x = switchPos[0].position.x;
            trashPos.x = switchPos[1].position.x;
        }
        else
        {
            presentPos.x = switchPos[1].position.x;
            trashPos.x = switchPos[0].position.x;
        }

        present.transform.position = presentPos;
        trash.transform.position = trashPos;
    }

    public IEnumerator MoodShuffle(float waitTime)  // set the mood image
    {
        yield return new WaitForSeconds(waitTime);

        // the left is the Happy
        if (randNum == 0)
        {
            leftImage.texture = moods[0];
            rightImage.texture = moods[1];
        }
        else
        {
            leftImage.texture = moods[1];
            rightImage.texture = moods[0];
        }
    }

    #endregion
}
