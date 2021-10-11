using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Genderer : MonoBehaviour
{
    public void GenderIs(bool isMale)
    {
        GameManager.maleGender = isMale;
    }
}
