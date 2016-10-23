using UnityEngine;
using System.Collections;

public class Goal : MonoBehaviour {

    public int LevelToLoad;
    private gameMaster gm;

    void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GameMaster").GetComponent<gameMaster>();
    }
    

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            SaveScore();
            Application.LoadLevel(LevelToLoad);
        }
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            SaveScore();
            Application.LoadLevel(LevelToLoad);
        }
    }

    void SaveScore()
    {
        PlayerPrefs.SetInt("Points", gm.points);

    }
}
