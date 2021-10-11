using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Sirenix.OdinInspector;
using System;

public class GlassTextPick : MonoBehaviour
{
    #region Public Members
    [BoxGroup("PROPERTIES", CenterLabel =true)]
    [ListDrawerSettings(ShowPaging = false, AddCopiesLastElement =true)]
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

    [Button]
    public void DoIt()
    {
        CreateLists();
        AssignSentenceFromList();
    }

    #region START
    void Start()
    {
        //ClearLists();

        CreateLists();

        AssignSentenceFromList();
    }
    #endregion

    #region Public Getters
    public List<string> Get_Sentence_List_Out_Of_word_And_Gender_And_Age_List(List<Word_And_Gender_And_Age> word_And_Gender_And_Age, GenderTypes gender, AgeTypes age)
    {
        List<string> Sentences_To_Return = new List<string>();

        // For Gender types
        for (int i = 0; i < word_And_Gender_And_Age.Count; i++)
        {
            // Insert All Non Gendered
            if (word_And_Gender_And_Age[i].GenderType == GenderTypes.Both)
            {
                for (int z = 0; z < word_And_Gender_And_Age[i].Age_And_word_List.Count; z++)
                {
                    switch (word_And_Gender_And_Age[i].Age_And_word_List[z].AgeType)
                    {                        
                        case AgeTypes.Child:
                            if (age == AgeTypes.Child)
                            {
                                Sentences_To_Return.AddRange(word_And_Gender_And_Age[i].Age_And_word_List[z].Sentence_List);
                            }
                            break;
                        case AgeTypes.Teen:
                            if (age == AgeTypes.Teen)
                            {
                                Sentences_To_Return.AddRange(word_And_Gender_And_Age[i].Age_And_word_List[z].Sentence_List);
                            }
                            break;
                        case AgeTypes.Adult:
                            if (age == AgeTypes.Teen)
                            {
                                Sentences_To_Return.AddRange(word_And_Gender_And_Age[i].Age_And_word_List[z].Sentence_List);
                            }
                            break;
                        case AgeTypes.All:
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
                    AgeTypes ageType = word_And_Gender_And_Age[i].Age_And_word_List[j].AgeType;

                    List<string> m_Sentence_List = word_And_Gender_And_Age[i].Age_And_word_List[j].Sentence_List;

                    if (ageType == AgeTypes.All)
                    {
                        Sentences_To_Return.AddRange(m_Sentence_List);
                    }

                    if (age == AgeTypes.All)
                    {
                        Sentences_To_Return.AddRange(m_Sentence_List);
                    }
                    else if(ageType == age)
                    {
                        Sentences_To_Return.AddRange(m_Sentence_List);
                    }
                }
            }
        }
        return Sentences_To_Return;
    }
    #endregion

    #region Public classes
    [Serializable]
    public class Word_And_Gender_And_Age
    {
        [Title("__________________________________________")]

        [EnumPaging]
        public GenderTypes GenderType;

        [ListDrawerSettings(ShowPaging = false, AddCopiesLastElement = true)]
        [Searchable]
        public List<Age_And_Word> Age_And_word_List;
    }

    [Serializable]
    public class Age_And_Word
    {
        [EnumPaging]
        public AgeTypes AgeType;

        public List <string> Sentence_List;

    }


    public void ClearLists()
    {
        Debug.Log("Cleared");

        All_Gender_All_Age.Clear();
        All_Gender_Adult.Clear();
        All_Gender_Teen.Clear();
        All_Gender_Child.Clear();
        Female_All_Age.Clear();
        Female_Adult.Clear();
        Female_Teen.Clear();
        Female_Child.Clear();
        Male_All_Age.Clear();
        Male_Adult.Clear();
        Male_Teen.Clear();
        Male_Child.Clear();
    }

    public void CreateLists()
    {
        LevelsSettingsManager.Age = AgeToTest;

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////        
        All_Gender_All_Age = Get_Sentence_List_Out_Of_word_And_Gender_And_Age_List(word_And_Gender_And_Age_List, GenderTypes.Both, AgeTypes.All);
        All_Gender_Adult = Get_Sentence_List_Out_Of_word_And_Gender_And_Age_List(word_And_Gender_And_Age_List, GenderTypes.Both, AgeTypes.All);
        All_Gender_Teen = Get_Sentence_List_Out_Of_word_And_Gender_And_Age_List(word_And_Gender_And_Age_List, GenderTypes.Both, AgeTypes.All);
        All_Gender_Child = Get_Sentence_List_Out_Of_word_And_Gender_And_Age_List(word_And_Gender_And_Age_List, GenderTypes.Both, AgeTypes.All);
        
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////        
        Female_All_Age = Get_Sentence_List_Out_Of_word_And_Gender_And_Age_List(word_And_Gender_And_Age_List, GenderTypes.Female, AgeTypes.All);
        Female_Adult = Get_Sentence_List_Out_Of_word_And_Gender_And_Age_List(word_And_Gender_And_Age_List, GenderTypes.Female, AgeTypes.Adult);
        Female_Teen = Get_Sentence_List_Out_Of_word_And_Gender_And_Age_List(word_And_Gender_And_Age_List, GenderTypes.Female, AgeTypes.Teen);
        Female_Child = Get_Sentence_List_Out_Of_word_And_Gender_And_Age_List(word_And_Gender_And_Age_List, GenderTypes.Female, AgeTypes.Child);
        
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////        
        Male_All_Age = Get_Sentence_List_Out_Of_word_And_Gender_And_Age_List(word_And_Gender_And_Age_List, GenderTypes.Male, AgeTypes.All);
        Male_Adult = Get_Sentence_List_Out_Of_word_And_Gender_And_Age_List(word_And_Gender_And_Age_List, GenderTypes.Male, AgeTypes.Adult);
        Male_Teen = Get_Sentence_List_Out_Of_word_And_Gender_And_Age_List(word_And_Gender_And_Age_List, GenderTypes.Male, AgeTypes.Teen);
        Male_Child = Get_Sentence_List_Out_Of_word_And_Gender_And_Age_List(word_And_Gender_And_Age_List, GenderTypes.Male, AgeTypes.Child);

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////        
    }
    
    public void AssignSentenceFromList()
    {
        // If male
        if (GameManager.maleGender)
        {
            // If Child
            if (LevelsSettingsManager.Age < AgeTypes.Teen.GetHashCode())
            {
                gameObject.GetComponent<TMP_Text>().text = Male_Child[UnityEngine.Random.Range(0, Male_Child.Count)];
            }
            // If Teen
            if ((LevelsSettingsManager.Age < AgeTypes.Adult.GetHashCode()) && (LevelsSettingsManager.Age > AgeTypes.Child.GetHashCode()))
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
            if (LevelsSettingsManager.Age >= AgeTypes.Adult.GetHashCode())
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
            if (LevelsSettingsManager.Age < AgeTypes.Teen.GetHashCode())
            {
                gameObject.GetComponent<TMP_Text>().text = Female_Child[UnityEngine.Random.Range(0, Female_Child.Count)];
            }
            // If Teen
            if ((LevelsSettingsManager.Age < AgeTypes.Adult.GetHashCode()) && (LevelsSettingsManager.Age > AgeTypes.Child.GetHashCode()))
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
            if (LevelsSettingsManager.Age >= AgeTypes.Adult.GetHashCode())
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

    #endregion

    #region enums
    public enum GenderTypes
    {
        None,
        Female,
        Male,
        Both
    }
    public enum AgeTypes
    {
        None = 0,
        Child = 13,
        Teen = 14,
        Adult = 18,
        All = 120

    }
    #endregion
}
