using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public SoundManager soundManager = null;
    
    public Vector3 floorSpawnPos = Vector3.zero;
    //public Vector3 obstacleScale = Vector3.one;
    
    public float runSpeed = 0.0f;
    public float speedMultiplier = 0;
    /*public float obstacleXOffset = 0.0f;
    public float obstacleYOffset = 0.0f;*/
    public float obstacleZOffset = 0.0f;
    public float offsetSpacingIncrement = 0.0f;
    public int jumpScore = 0;
    public int dodgeScore = 0;
    public int speedScore = 0;
    public int speedInterval = 0;

    public GameObject goScore = null;
    
    public List<GameObject> floorList = new List<GameObject>();
    public List<GameObject> obstaclePrefabList = new List<GameObject>();


    public GameObject floorPrefab = null;
    

    public SceneTransfer transfer = null;

    Player player = null;
    OptionsManager options = null;

    TextMeshProUGUI scoreText = null;

    int score = 0;
    int jumpCounter = 0;
    int dodgeCounter = 0;
    int lastRandom = 0;
    float speedCounter = 0.0f;
    float unalteredZoffset = 0.0f;

    

    /*bool isIncrementMiddle = false;
    bool isIncrementRight = false;*/



    // Start is called before the first frame update
    void Start()
    {
        unalteredZoffset = obstacleZOffset;

        options = GameObject.FindGameObjectWithTag("Options").GetComponent<OptionsManager>();

        soundManager.SetSFX(options.hasSFXOption());
        
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        player.InitialisePlayer(runSpeed, options.GetSelectedDinoMaterial(), soundManager);

        

        scoreText = goScore.GetComponent<TextMeshProUGUI>();
        
        //Debug.Log("start: " + debugText);

        // destroy options manager to avoid duplicates on restarts
        if (options != null)
        {
            Destroy(options.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        MoveFloor();
        IncrementScore();
        SpeedUp();

        if (player.HasJumpScore())
        {
            AddJumpScore();
        }

        if (player.HasDied())
        {

            transfer.SetFinalScore(score);
            transfer.SetFinalJump(jumpCounter);
            transfer.SetFinalDodge(dodgeCounter);

            // Load GameOver: Build Index 2
            SceneManager.LoadScene(2);
        }
        
    }

    // copy new floor and delete old one with obstacles
    public void CycleNewFloor(/*GameObject go*/)
    {
        
        
        int randomObstacle = Random.Range(0, obstaclePrefabList.Count);

        GameObject newFloor = Instantiate(floorPrefab, floorSpawnPos, Quaternion.identity);
        GameObject newObstacle = Instantiate(obstaclePrefabList[randomObstacle], floorSpawnPos, Quaternion.identity);

        float prefabXPos = obstaclePrefabList[randomObstacle].transform.position.x;
        float prefabYPos = obstaclePrefabList[randomObstacle].transform.position.y;

        // a Z offset Variable is used instead of the prefab base so it can be modified multiple time during runtime
        Vector3 obstacleLocalPosition = new Vector3(prefabXPos, prefabYPos, obstacleZOffset);

        newObstacle.transform.parent = newFloor.transform;

        newObstacle.transform.localPosition = obstacleLocalPosition;

        GameObject obstacleJumpBox = newObstacle.transform.GetChild(0).gameObject;

        Vector3 jumpBoxPosition = new Vector3(obstacleJumpBox.transform.localPosition.x, player.GetJumpHeight(), obstacleJumpBox.transform.localPosition.z);

        obstacleJumpBox.transform.SetLocalPositionAndRotation(jumpBoxPosition, Quaternion.identity);

        // cuirrently three instances of objects
        // int overload of this function is max exclusive, so putting a 3 here will mean the third instance would never proc
        int random = Random.Range(1, 4);

        // ensure random does not produce numbers equal to the last
        while (random == lastRandom)
        {
            random = Random.Range(1, 4);
        }

        lastRandom = random;

        Debug.Log("random: " + random.ToString());


        // change the offsets by an increment so that the floors spawn in a different position next time
        if (random == 1)
        {
            // spawn next one a  bit less i.e a bit to the left
            obstacleZOffset = unalteredZoffset + offsetSpacingIncrement;


            
        }
        else if (random == 2)
        {
            // a little more i,e a bit more to the right

            obstacleZOffset = unalteredZoffset + (offsetSpacingIncrement * 2);

            
        }
        else 
        {
            obstacleZOffset = unalteredZoffset;

            
        }  
        

        

        //Debug.Log("Spawn");

        
        floorList.Add(newFloor);
    }

    public void DestroyFloor(GameObject go)
    {
        floorList.Remove(go);

        Destroy(go);
    }

    public void AddDodgeScore()
    {
        score += dodgeScore;
        ++dodgeCounter;

        UpdateScoreText();
    }

    void AddJumpScore()
    {
        score += jumpScore;
        ++jumpCounter;

        UpdateScoreText();

        player.ResetJumpScore();
    }

    void AddSpeedScore()
    {
        score += speedScore;
        //Debug.Log("Bonus: " + speedScore);

        UpdateScoreText();
    }

    void UpdateScoreText()
    {
        scoreText.text = "Score: " + "\n" + score + "\n" +
            "Jumps: " + "\n" + jumpCounter + "\n" +
            "Dodges: " + "\n" + dodgeCounter;
    }

    void MoveFloor()
    {
        // go through game objects and translate them by speed variable
        for (int i = 0; i < floorList.Count; i++)
        {
            GameObject go = floorList[i];
            float finalSpeed = runSpeed * Time.deltaTime;
            Vector3 newPosition = new Vector3(finalSpeed, 0.0f);

            if (go != null)
            {

                go.transform.Translate(newPosition);
            }


        }
    }

    void IncrementScore()
    {
        ++score;

        UpdateScoreText();

    }

    void SpeedUp()
    {
        speedCounter += 1 * Time.deltaTime;

        if (speedCounter >= speedInterval) 
        {
            speedCounter = 0;

            runSpeed *= speedMultiplier;
            //runSpeed += speedMultiplier;

            player.UpdateSpeed(speedMultiplier);

            AddSpeedScore();

            Debug.Log("Speed UP!");

        }
    }
}
