using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WordMixer : MonoBehaviour
{
    #region(public vars)
    [Header("Insert the switch rate of words")]
    [Range(1, 60)]
    public float speed;

    [Header("Debug valus DON'T TOUCH")]
    public int randNum;

    [Header("Insert the words you want to shuffle")]
    public List<string> Words;
    #endregion

    #region(private vars)
    private TMP_Text myText;
    #endregion


    // Start is called before the first frame update
    void Start()
    {
        myText = gameObject.GetComponent<TMP_Text>();
        InsertWord();
    }

    // Update is called once per frame
    void Update()
    {
        DelayInsertWord(speed);
    }

    public void InsertWord()
    {
        randNum = Random.Range(0, Words.Count);

        myText.text = Words[randNum];
    }

    IEnumerator DelayInsertWord(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);

        InsertWord();
    }
}
