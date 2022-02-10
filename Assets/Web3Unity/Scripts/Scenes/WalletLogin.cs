using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WalletLogin: MonoBehaviour
{
    public Toggle rememberMe;
    public Text debugText;
    void Start() {
        // if remember me is checked, set the account to the saved account
        if (rememberMe.isOn && PlayerPrefs.GetString("Account") != "")
        {
            debugText.text = "LoggedIn";
            // move to next scene
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    async public void OnLogin()
    {
        // get current timestamp
        int timestamp = (int)(System.DateTime.UtcNow.Subtract(new System.DateTime(1970, 1, 1))).TotalSeconds;
        // set expiration time
        int expirationTime = timestamp + 60;
        // set message
        string message = expirationTime.ToString();
        // sign message
        string signature = await Web3Wallet.Sign(message);
        // verify account
        string account = await EVM.Verify(message, signature);
        debugText.text = "LogginIN";

        int now = (int)(System.DateTime.UtcNow.Subtract(new System.DateTime(1970, 1, 1))).TotalSeconds;
        // validate
        if (account.Length == 42 && expirationTime >= now) {
            // save account
            PlayerPrefs.SetString("Account", account);
            // load next scene
            debugText.text = "LogginIN Account" + account;

            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
