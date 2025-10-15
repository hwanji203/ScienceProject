using System;
using UnityEngine;

public class Trigger : MonoBehaviour
{
    [SerializeField] private Word myType;
    [SerializeField] private GameObject password;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        TutorialManager.Instance.Talk(myType);

        switch (myType)
        {
            case Word.GravityDir:
                password.SetActive(true);
                break;
        }
    }
}
