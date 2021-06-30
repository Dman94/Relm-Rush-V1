using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Bank : MonoBehaviour
{
    [SerializeField] int StartingBalance = 150;
    [SerializeField] int currentBalance;
    

    [SerializeField] TextMeshProUGUI displayBalance;

    public int CurrentBalance { get { return currentBalance; } }



    private void Awake()
    {
        currentBalance = StartingBalance;
        updateDisplay();
    }
    public void Deposit(int Amount)
    {
        currentBalance += Mathf.Abs(Amount);
        updateDisplay();
    }
    public void Withdraw(int Amount)
    {
        currentBalance -= Mathf.Abs(Amount);
        updateDisplay();

        if (currentBalance < 0)
        {
            ReloadScene();
        }
    }



    void updateDisplay()
    {
        displayBalance.text = "Gold:" + currentBalance
;    }
   void ReloadScene()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.buildIndex);
    }


}
