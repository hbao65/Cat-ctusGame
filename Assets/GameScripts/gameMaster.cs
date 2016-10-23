using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class gameMaster : MonoBehaviour {

    public int points;

    public Text pointsText;

    void Start()
    {
        if(PlayerPrefs.HasKey("Points"))
        {
            if(Application.loadedLevel == 0)
            {
                PlayerPrefs.DeleteKey("Points");
                points = 0;
            }
            else
            {
                points = PlayerPrefs.GetInt("Points");
            }
        }
    }

    void Update()
    {
        pointsText.text = ("Score: " + points);
    }
}
