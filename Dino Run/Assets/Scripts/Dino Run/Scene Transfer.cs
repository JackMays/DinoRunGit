using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneTransfer : MonoBehaviour
{
    int finalScore = 0;
    int finalJump = 0;
    int finalDodge = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        // dont destroy on load so this carries over to game over scene
        DontDestroyOnLoad(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int GetFinalScore()
    {
        return finalScore;
    }

    public int GetFinalJump()
    { 
        return finalJump; 
    }
    
    public int GetFinalDodge()
    {
        return finalDodge;
    }

    public void SetFinalScore(int score)
    {
        finalScore = score;
    }

    public void SetFinalJump(int jump)
    {
        finalJump = jump;
    }

    public void SetFinalDodge(int dodge)
    {
        finalDodge = dodge;
    }
}
