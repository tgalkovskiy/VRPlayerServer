using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class PrintWords : MonoBehaviour
{

    public enum WordsLists
    {
        None,
        Target_Strings,
        Glass_Strings,
        Zeplin_Strings
    }

    public enum PrintType
    {
        None,
        Both,
        Good,
        Bad
    }

    #region Public Vars
    public TargetStrings TargetSourceList = null;
    public GlassTextPick GlassSourceList = null;
    public PcWordMixer ZeplinSourceList = null;

    [Header("Name the txt file")]
    public string fileName;

    [Header("Set the file header")]
    public string header;
    #endregion

    #region Private vars
    [Header("Set the File types")]
    [SerializeField]
    private WordsLists WordsType;

    [SerializeField]
    private PrintType ListType;

    private List<string> goodWordsToPrint;
    private List<string> badWordsToPrint;
    #endregion


    // Start is called before the first frame update
    void Start()
    {
        //switch (WordsType)
        //{
        //    case WordsLists.Target_Strings:

        //        Debug.Log("WTF1");

        //        goodWordsToPrint = TargetSourceList.goodWords;
        //        badWordsToPrint = TargetSourceList.badWords;
        //        CreateText();
        //        break;
        //    case WordsLists.Glass_Strings:
        //        badWordsToPrint = GlassSourceList.maleBadTexts;
        //        CreateText();
        //        break;
        //    case WordsLists.Zeplin_Strings:
        //        goodWordsToPrint = ZeplinSourceList.maleWords;
        //        CreateText();
        //        break;
        //    default:
        //        break;
        //}
    }

    void CreateText()
    {
        Debug.Log("WTF2");
        //Path of the file
        string path = Application.dataPath + "/" +header+".txt";

        //Create File if it doesn't exist
        if (!File.Exists(path))
        {
            Debug.Log("WTF2.5");

            File.WriteAllText(path, header + "\n\n");
        }

        /////////////////////////////////////////////////////////////

        switch (ListType)
        {
            case PrintType.Both:
                Debug.Log("WTF3");
                for (int i = 0; i < goodWordsToPrint.Count; i++)
                {
                    Debug.Log("WTF3.1");
                    //Content of the file
                    string content = i + 1 + "- " + goodWordsToPrint[i] + "\n ";

                    //Add some to text to it
                    File.AppendAllText(path, content);
                }

                Debug.Log("WTF4");
                for (int i = 0; i < badWordsToPrint.Count; i++)
                {
                    Debug.Log("WTF4.1");
                    //Content of the file
                    string content = i + 1 + "- " + badWordsToPrint[i] + "\n ";

                    //Add some to text to it
                    File.AppendAllText(path, content);
                }

                break;
            case PrintType.Good:
                for (int i = 0; i < goodWordsToPrint.Count; i++)
                {
                    //Content of the file
                    string content = i + 1 + "- " + goodWordsToPrint[i] + "\n ";

                    //Add some to text to it
                    File.AppendAllText(path, content);
                }

                break;
            case PrintType.Bad:
                for (int i = 0; i < badWordsToPrint.Count; i++)
                {
                    //Content of the file
                    string content = i + 1 + "- " + badWordsToPrint[i] + "\n ";

                    //Add some to text to it
                    File.AppendAllText(path, content);
                }

                break;
            default:
                break;
        }
    }
}
