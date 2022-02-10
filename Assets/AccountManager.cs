using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class AccountManager : MonoBehaviour
{
    public GameObject tostPrefab;
    public Transform mainCanvas;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void logOut()
    {
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene(0);
    }
    public void copyAddress(Text address)
    {
       if( ClipboardExtension.CopyToClipboard(address.text))
        {
         GameObject ob=   Instantiate(tostPrefab, mainCanvas);
            Destroy(ob, 1f);
        }
    }
   
}


public static class ClipboardExtension
{
    /// <summary>
    /// Puts the string into the Clipboard.
    /// </summary>
    public static bool CopyToClipboard(this string str)
    {
        GUIUtility.systemCopyBuffer = str;
        return true;
    }
   
}