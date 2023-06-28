using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class MenuUIHandler : MonoBehaviour
{
    public TMP_InputField textField;
    public TextMeshProUGUI highScoreText;  
    private GameManager manager;

    // Start is called before the first frame update
    void Start()
    {
        textField.onValueChanged.AddListener(HandleTextInput);
        manager = GameManager.Instance;
        highScoreText.text = "Highscore: " + manager.Name + " - " + manager.HighScore;
        if (manager.CurrentPlayerName != null)
        {
            textField.text = manager.CurrentPlayerName;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartNew() 
    {
        SceneManager.LoadScene(1);
    }

    public void Exit()
    {
        #if UNITY_EDITOR
            EditorApplication.ExitPlaymode();
        #else
            Application.Quit(); // original code to quit Unity player
        #endif
        manager.SaveStats();
    }

    private void HandleTextInput(string text)
    {
        // This method will be called whenever the text in the InputField changes
        manager.CurrentPlayerName = text;
    }
}
