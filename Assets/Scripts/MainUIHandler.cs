#if UNITY_EDITOR
using System.Security.Permissions;
using System.Xml.Linq;
using TMPro;
using UnityEditor;
#endif
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainUIHandler : MonoBehaviour
{
    public TMP_InputField field;

    public void StartNew()
    {
        SceneManager.LoadScene(1);
        NameSelector.Instance.playerName = field.text;
    }
    public void Exit()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit(); // original code to quit Unity player
#endif
    }
}
