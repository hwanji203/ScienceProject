using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using System.Collections;
using UnityEditor;
using UnityEngine.SceneManagement;

public class Password : MonoBehaviour
{
    [SerializeField] private string password = "1234";
    private string input = "";

    [SerializeField] private Text text;

    private int warning = 0;
    private bool isLocked = false;

    public void Press(int num)
    {
        if (isLocked) return;
        
        if (input.Length < 4)
            input += num.ToString();
    }

    public void Enter()
    {
        if (isLocked) return;
        
        if (input == password)
            StartCoroutine(Correct());
        else
        {
            if (warning > 1)
            {
                StartCoroutine(LockoutRootine());
            }
            else
            {
                StartCoroutine(Wrong());
                warning++;
            }
        }
    }

    public void Backspace()
    {
        if (isLocked) return;
        
        if (input.Length > 0)
            input = input.Substring(0, input.Length - 1);
    }

    private void Update()
    {
        text.text = input;
    }

    private IEnumerator Correct()
    {
        text.fontSize = 90;
        input = "Unlocked";
        
        yield return new WaitForSeconds(2f);
        
        SceneManager.LoadScene(2);
    }
    
    private IEnumerator Wrong()
    {
        text.fontSize = 90;
        input = "Try Again";
        
        yield return new WaitForSeconds(1.5f);
        
        input = "";
        
        yield return new WaitForSeconds(0.1f);
        
        text.fontSize = 120;
    }

    private IEnumerator LockoutRootine()
    {
        isLocked = true;
        text.fontSize = 75;

        int countdown = 30;
        while (countdown > 0)
        {
            input = $"Locked: {countdown}s";
            yield return new WaitForSeconds(1f);
            countdown--;
        }

        warning = 0;
        input = "";
        text.fontSize = 120;
        isLocked = false;
    }
}
