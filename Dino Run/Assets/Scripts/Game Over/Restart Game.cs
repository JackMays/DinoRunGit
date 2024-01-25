using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartGame : MonoBehaviour
{
    public TextMeshProUGUI finalScoreText;
    
    SceneTransfer transfer = null;
    
    int score = 0;
    int jump = 0;
    int dodge = 0;

    // Start is called before the first frame update
    void Start()
    {
        transfer = GameObject.FindGameObjectWithTag("Transfer").GetComponent<SceneTransfer>();

        score = transfer.GetFinalScore();
        jump = transfer.GetFinalJump();
        dodge = transfer.GetFinalDodge();


        finalScoreText.text = "Score: " + score.ToString() + "\n" +
            "Successful Obstacle Jumps: " + jump.ToString() + "\n" +
            "Total Obstacles Avoided: " + dodge.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RestartDinoRun(int scene)
    {
        // Load DinoRun and ensure no duplicates of Scene Transfer
        Destroy(transfer.gameObject);
        // Start Screen Build Index: 0
        SceneManager.LoadScene(0);
    }
}
