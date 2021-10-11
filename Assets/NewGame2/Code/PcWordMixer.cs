using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Sirenix.OdinInspector;
using System;

public class PcWordMixer : MonoBehaviour
{
    #region(public vars)
    [Header("Insert the switch rate of words")]
    [Range(1, 60)]
    public float speed;

    [Header("Debug valus DON'T TOUCH")]
    public int randNum;
    #endregion

    #region(private vars)
    private TMP_Text myText;
    #endregion


    #region Public Members
    [BoxGroup("PROPERTIES", CenterLabel = true)]
    [ListDrawerSettings(ShowPaging = false, AddCopiesLastElement = true)]
    [Searchable]
    public List<Word_And_Gender_And_Age> word_And_Gender_And_Age_List;
    #endregion

    #region Private Members
    [BoxGroup("Dont TOUCH!!!")]
    public int AgeToTest;

    [BoxGroup("Dont TOUCH!!!")]
    public List<String> All_Gender_All_Age;

    [BoxGroup("Dont TOUCH!!!")]
    public List<String> All_Gender_Adult;

    [BoxGroup("Dont TOUCH!!!")]
    public List<String> All_Gender_Teen;

    [BoxGroup("Dont TOUCH!!!")]
    public List<String> All_Gender_Child;

    [BoxGroup("Dont TOUCH!!!")]
    public List<String> Female_All_Age;

    [BoxGroup("Dont TOUCH!!!")]
    public List<String> Female_Adult;

    [BoxGroup("Dont TOUCH!!!")]
    public List<String> Female_Teen;

    [BoxGroup("Dont TOUCH!!!")]
    public List<String> Female_Child;

    [BoxGroup("Dont TOUCH!!!")]
    public List<String> Male_All_Age;

    [BoxGroup("Dont TOUCH!!!")]
    public List<String> Male_Adult;

    [BoxGroup("Dont TOUCH!!!")]
    public List<String> Male_Teen;

    [BoxGroup("Dont TOUCH!!!")]
    public List<String> Male_Child;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        CreateLists();
        myText = gameObject.GetComponent<TMP_Text>();
        AssignSentenceFromList();
    }

    // Update is called once per frame
    void Update()
    {
        Delay_AssignSentenceFromList(speed);
    }

    IEnumerator Delay_AssignSentenceFromList(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);

        AssignSentenceFromList();
    }

    [Serializable]
    public class Word_And_Gender_And_Age
    {
        [Title("__________________________________________")]

        [EnumPaging]
        public GlassTextPick.GenderTypes GenderType;

        [ListDrawerSettings(ShowPaging = false, AddCopiesLastElement = true)]
        [Searchable]
        public List<Age_And_Word> Age_And_word_List;
    }

    [Serializable]
    public class Age_And_Word
    {
        [EnumPaging]
        public GlassTextPick.AgeTypes AgeType;

        public List<string> Sentence_List;

    }

    public void CreateLists()
    {
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////        
        All_Gender_All_Age = Get_Sentence_List_Out_Of_word_And_Gender_And_Age_List(word_And_Gender_And_Age_List, GlassTextPick.GenderTypes.Both, GlassTextPick.AgeTypes.All);
        All_Gender_Adult = Get_Sentence_List_Out_Of_word_And_Gender_And_Age_List(word_And_Gender_And_Age_List, GlassTextPick.GenderTypes.Both, GlassTextPick.AgeTypes.All);
        All_Gender_Teen = Get_Sentence_List_Out_Of_word_And_Gender_And_Age_List(word_And_Gender_And_Age_List, GlassTextPick.GenderTypes.Both, GlassTextPick.AgeTypes.All);
        All_Gender_Child = Get_Sentence_List_Out_Of_word_And_Gender_And_Age_List(word_And_Gender_And_Age_List, GlassTextPick.GenderTypes.Both, GlassTextPick.AgeTypes.All);

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////        
        Female_All_Age = Get_Sentence_List_Out_Of_word_And_Gender_And_Age_List(word_And_Gender_And_Age_List, GlassTextPick.GenderTypes.Female, GlassTextPick.AgeTypes.All);
        Female_Adult = Get_Sentence_List_Out_Of_word_And_Gender_And_Age_List(word_And_Gender_And_Age_List, GlassTextPick.GenderTypes.Female, GlassTextPick.AgeTypes.Adult);
        Female_Teen = Get_Sentence_List_Out_Of_word_And_Gender_And_Age_List(word_And_Gender_And_Age_List, GlassTextPick.GenderTypes.Female, GlassTextPick.AgeTypes.Teen);
        Female_Child = Get_Sentence_List_Out_Of_word_And_Gender_And_Age_List(word_And_Gender_And_Age_List, GlassTextPick.GenderTypes.Female, GlassTextPick.AgeTypes.Child);

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////        
        Male_All_Age = Get_Sentence_List_Out_Of_word_And_Gender_And_Age_List(word_And_Gender_And_Age_List, GlassTextPick.GenderTypes.Male, GlassTextPick.AgeTypes.All);
        Male_Adult = Get_Sentence_List_Out_Of_word_And_Gender_And_Age_List(word_And_Gender_And_Age_List, GlassTextPick.GenderTypes.Male, GlassTextPick.AgeTypes.Adult);
        Male_Teen = Get_Sentence_List_Out_Of_word_And_Gender_And_Age_List(word_And_Gender_And_Age_List, GlassTextPick.GenderTypes.Male, GlassTextPick.AgeTypes.Teen);
        Male_Child = Get_Sentence_List_Out_Of_word_And_Gender_And_Age_List(word_And_Gender_And_Age_List, GlassTextPick.GenderTypes.Male, GlassTextPick.AgeTypes.Child);

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////        
    }

    #region Public Getters
    public List<string> Get_Sentence_List_Out_Of_word_And_Gender_And_Age_List(List<Word_And_Gender_And_Age> word_And_Gender_And_Age, GlassTextPick.GenderTypes gender, GlassTextPick.AgeTypes age)
    {
        List<string> Sentences_To_Return = new List<string>();

        // For Gender types
        for (int i = 0; i < word_And_Gender_And_Age.Count; i++)
        {
            // Insert All Non Gendered
            if (word_And_Gender_And_Age[i].GenderType == GlassTextPick.GenderTypes.Both)
            {
                for (int z = 0; z < word_And_Gender_And_Age[i].Age_And_word_List.Count; z++)
                {
                    switch (word_And_Gender_And_Age[i].Age_And_word_List[z].AgeType)
                    {
                        case GlassTextPick.AgeTypes.Child:
                            if (age == GlassTextPick.AgeTypes.Child)
                            {
                                Sentences_To_Return.AddRange(word_And_Gender_And_Age[i].Age_And_word_List[z].Sentence_List);
                            }
                            break;
                        case GlassTextPick.AgeTypes.Teen:
                            if (age == GlassTextPick.AgeTypes.Teen)
                            {
                                Sentences_To_Return.AddRange(word_And_Gender_And_Age[i].Age_And_word_List[z].Sentence_List);
                            }
                            break;
                        case GlassTextPick.AgeTypes.Adult:
                            if (age == GlassTextPick.AgeTypes.Teen)
                            {
                                Sentences_To_Return.AddRange(word_And_Gender_And_Age[i].Age_And_word_List[z].Sentence_List);
                            }
                            break;
                        case GlassTextPick.AgeTypes.All:
                            Sentences_To_Return.AddRange(word_And_Gender_And_Age[i].Age_And_word_List[z].Sentence_List);
                            break;
                        default:
                            break;
                    }
                }
            }

            if (word_And_Gender_And_Age[i].GenderType == gender)
            {
                // For Age types
                for (int j = 0; j < word_And_Gender_And_Age[i].Age_And_word_List.Count; j++)
                {
                    GlassTextPick.AgeTypes ageType = word_And_Gender_And_Age[i].Age_And_word_List[j].AgeType;

                    List<string> m_Sentence_List = word_And_Gender_And_Age[i].Age_And_word_List[j].Sentence_List;

                    if (ageType == GlassTextPick.AgeTypes.All)
                    {
                        Sentences_To_Return.AddRange(m_Sentence_List);
                    }

                    if (age == GlassTextPick.AgeTypes.All)
                    {
                        Sentences_To_Return.AddRange(m_Sentence_List);
                    }
                    else if (ageType == age)
                    {
                        Sentences_To_Return.AddRange(m_Sentence_List);
                    }
                }
            }
        }
        return Sentences_To_Return;
    }
    #endregion

    public void AssignSentenceFromList()
    {
        // If male
        if (GameManager.maleGender)
        {
            // If Child
            if (LevelsSettingsManager.Age < GlassTextPick.AgeTypes.Teen.GetHashCode())
            {
                gameObject.GetComponent<TMP_Text>().text = Male_Child[UnityEngine.Random.Range(0, Male_Child.Count)];
            }
            // If Teen
            if ((LevelsSettingsManager.Age < GlassTextPick.AgeTypes.Adult.GetHashCode()) && (LevelsSettingsManager.Age > GlassTextPick.AgeTypes.Child.GetHashCode()))
            {
                if (Male_Teen != null)
                {
                    gameObject.GetComponent<TMP_Text>().text = Male_Teen[UnityEngine.Random.Range(0, Male_Teen.Count)];
                }
                else
                {
                    gameObject.GetComponent<TMP_Text>().text = Male_Child[UnityEngine.Random.Range(0, Male_Teen.Count)];
                }
            }
            // If Adult
            if (LevelsSettingsManager.Age >= GlassTextPick.AgeTypes.Adult.GetHashCode())
            {
                if (Male_Adult != null)
                {
                    gameObject.GetComponent<TMP_Text>().text = Male_Adult[UnityEngine.Random.Range(0, Male_Adult.Count)];
                }
                else
                {
                    gameObject.GetComponent<TMP_Text>().text = Male_Teen[UnityEngine.Random.Range(0, Male_Teen.Count)];
                }
            }
        }
        // If Female
        else
        {
            // If Child
            if (LevelsSettingsManager.Age < GlassTextPick.AgeTypes.Teen.GetHashCode())
            {
                gameObject.GetComponent<TMP_Text>().text = Female_Child[UnityEngine.Random.Range(0, Female_Child.Count)];
            }
            // If Teen
            if ((LevelsSettingsManager.Age < GlassTextPick.AgeTypes.Adult.GetHashCode()) && (LevelsSettingsManager.Age > GlassTextPick.AgeTypes.Child.GetHashCode()))
            {
                if (Female_Teen != null)
                {
                    gameObject.GetComponent<TMP_Text>().text = Female_Teen[UnityEngine.Random.Range(0, Female_Teen.Count)];
                }
                else
                {
                    gameObject.GetComponent<TMP_Text>().text = Female_Child[UnityEngine.Random.Range(0, Female_Teen.Count)];
                }
            }
            // If Adult
            if (LevelsSettingsManager.Age >= GlassTextPick.AgeTypes.Adult.GetHashCode())
            {
                if (Female_Adult != null)
                {
                    gameObject.GetComponent<TMP_Text>().text = Female_Adult[UnityEngine.Random.Range(0, Female_Adult.Count)];
                }
                else
                {
                    gameObject.GetComponent<TMP_Text>().text = Female_Teen[UnityEngine.Random.Range(0, Female_Adult.Count)];
                }
            }
        }
    }
}
