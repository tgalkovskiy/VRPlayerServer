using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TargetManager : MonoBehaviour
{
    #region(Public variables)
    [Header("Insert the cube objects")]
    public GameObject rightCube;
    public GameObject leftCube;

    [Header("Insert the mood and object images")]
    public List<Texture2D> moods;
    public List<Texture2D> objects;


    [Header("Insert the image UI objects")]
    public List<RawImage> leftCubeImage;
    public List<RawImage> rightCubeImage;

    [Header("Answer values")]
    public AudioSource winAudio;
    public AudioSource loseAudio;


    [HideInInspector]
    public int moodRandom;
    [HideInInspector]
    public int objectRandom;
    #endregion



    public void Clean(float waitTime)
    {
        StartCoroutine(ResetCubes(waitTime));
    }

    public void Flip(float waitTime)
    {
        StartCoroutine(RotateCubes(waitTime));
    }

    public void Shuffle(float waitTime)
    {
        StartCoroutine(SetRound(waitTime));
    }


    #region(IEnumerators)
    public IEnumerator ResetCubes(float waitTime) // Reset the cubes images to blank
    {
        yield return new WaitForSeconds(waitTime);

        // Reset the mood image
        rightCubeImage[0].texture = null;
        leftCubeImage[0].texture = null;

        // Reset the object image
        rightCubeImage[1].texture = null;
        leftCubeImage[1].texture = null;
    }

    public IEnumerator RotateCubes(float waitTime) // Rotate the cubes 180 degrees
    {
        yield return new WaitForSeconds(waitTime);

        rightCube.GetComponent<Animator>().SetTrigger("Rotate");
        leftCube.GetComponent<Animator>().SetTrigger("Rotate");
    }

    public IEnumerator SetRound(float waitTime) // Shuffle the mood images and the object images
    {
        yield return new WaitForSeconds(waitTime);


        // Random the mood and the object images
        moodRandom = Random.Range(0, moods.Count);
        objectRandom = Random.Range(0, objects.Count);

        // Random the left mood picture
        leftCubeImage[0].texture = moods[moodRandom];

        // Checks which side got the happy mood
        if (moodRandom == 0)    // If the left side got the happy mood
        {
            // Random the left object picture
            leftCubeImage[1].texture = objects[objectRandom];

            // Set th right image to sad mood
            rightCubeImage[0].texture = moods[1];

            // Reset the right object image
            rightCubeImage[1].texture = null;
        }
        else    // If the right side got the happy mood
        {
            // Set the right image to happy mood
            rightCubeImage[0].texture = moods[0];

            // Random the right object picture
            rightCubeImage[1].texture = objects[objectRandom];

            // Reset the left object image
            leftCubeImage[1].texture = null;
        }
    }
    #endregion
}
