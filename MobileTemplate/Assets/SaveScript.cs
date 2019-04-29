using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class SaveScript : MonoBehaviour
{
    // Start is called before the first frame update
public void CheckAchiev(int score)
    {
        int totalDeaths = 0, totalScore = 0;
        if(PlayerPrefs.HasKey("Deaths"))
        {
            totalDeaths = PlayerPrefs.GetInt("Deaths");
            if(totalDeaths >= 5)
            {
                //wywoływanie achika--------------------------------------------------

                /*
                if(PlayerPrefs.HasKey("Achi1"))
                {
                    if(PlayerPrefs.GetInt("Achi1") == 0)
                    {
                        PlayerPrefs.SetInt("Achi1", 1);
                    }
                }*/
            }
            if (totalDeaths >= 10)
            {
                //wywoływanie achika---------------------------------------------


            }


        }
        if (PlayerPrefs.HasKey("TotalScore"))
        {
            totalScore = PlayerPrefs.GetInt("TotalScore");
            if(totalScore >= 15)
            {
                //wywoływanie achika--------------------------------------------------

            }

            if (totalScore >= 100)
            {
                //wywoływanie achika--------------------------------------------------

            }
        }
        if(score >= 5)
        {
            //wywoływanie achika--------------------------------------------------

        }
    }
}


/* public class SaveScript : MonoBehaviour
{
    
    void Start()
    {
        
    }

    public void SaveGame(int points) {
        if(PlayerPrefs.HasKey("Deaths")) {
            PlayerPrefs.SetInt("Deats", PlayerPrefs.GetInt("Deats") + 1);
        } else {
            PlayerPrefs.SetInt("Deats", 1);
        }
        
        if(PlayerPrefs.HasKey("TotalPoints")) {
            PlayerPrefs.SetInt("TotalPoints", PlayerPrefs.GetInt("TotalPoints") + points);
        } else {
            PlayerPrefs.SetInt("TotalPoints", points);
        }
        
    }

    public void CheckAchivment(int points) {
        //1 - 5 pkt - 
        //2 - 5 death
        //3 - 15 pkt
        //4 - 8 śmierci
        //5 - 30 pkt

        int deaths = PlayerPrefs.GetInt("Deats");
    }

    
}*/
