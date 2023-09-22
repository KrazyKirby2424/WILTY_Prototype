using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using UnityEngine.Windows;
using Button = UnityEngine.UI.Button;

public class GameController : MonoBehaviour
{
    [SerializeField]
    TMP_Text playerName;

    [SerializeField]
    TMP_Text truthOrLie;

    [SerializeField]
    TMP_Text timer;

    [SerializeField]
    Button truth;
    [SerializeField]
    Button lie;
    [SerializeField]
    Button truthCheck;
    [SerializeField]
    Button lieCheck;

    List<Player> players;
    List<string> lies;
    List<string> truths;
    int playerIndex;
    bool answer; //true for truth, false for lie

    bool countDown = false;
    float time;

    private int rounds;
    private int curRound;

    private void Awake()
    {
        players = GameInfo.instance.GetPlayers();
        lies = Lies();
        rounds = GameInfo.instance.GetRounds();
        curRound= 0;
    }

    //randomly choose a player
    //set that player name
    //50/50 truth or lie
    //set the truth or lie

    // Start is called before the first frame update
    void Start()
    {
        SetRound();
        SetTeams();
    }

    [SerializeField]
    GameObject prefabUI;
    [SerializeField]
    Transform team1Content;
    [SerializeField]
    Transform team2Content;

    List<Player> teamOne = new List<Player>();
    List<Player> teamTwo = new List<Player>();
    void SetTeams()
    {
        List<Player> teams = new List<Player>();
        foreach (Player p in GameInfo.instance.GetPlayers()) 
        {
            teams.Add(p);
        }

        bool team1 = true; //true team 1 false team 2
        while(teams.Count>0)
        {
            int index = Random.Range(0, teams.Count);
            if(team1)
            {
                teamOne.Add(teams[index]);
                teams.RemoveAt(index);
                team1 = !team1;
            }
            else
            {
                teamTwo.Add(teams[index]);
                teams.RemoveAt(index);
                team1 = !team1;
            }
        }
        foreach(Player player in teamOne) 
        {
            GameObject playerUI = Instantiate(prefabUI, team1Content);
            playerUI.GetComponentInChildren<TMP_Text>().text = player.player_Name;
        }
        foreach (Player player in teamTwo)
        {
            GameObject playerUI = Instantiate(prefabUI, team2Content);
            playerUI.GetComponentInChildren<TMP_Text>().text = player.player_Name;
        }


    }
    [SerializeField]
    TMP_Text gameOver;
    [SerializeField]
    Button home;
    void SetRound()
    {
        if(curRound<rounds)
        {
            truthOrLie.enabled = true;
            timer.gameObject.SetActive(false);
            truth.gameObject.SetActive(true);
            lie.gameObject.SetActive(true);
            truthCheck.gameObject.SetActive(false);
            lieCheck.gameObject.SetActive(false);
            //randomly set player and truths list
            playerIndex = Random.Range(0, players.Count);
            playerName.text = players[playerIndex].GetName();
            truths = players[playerIndex].GetTruths();
            //randomly set truth or lie
            int tOrL = Random.Range(0, 2);
            int selectedPrompt;
            if (tOrL == 0 && truths.Count > 0) //truth
            {
                selectedPrompt = Random.Range(0, truths.Count);
                truthOrLie.text = truths[selectedPrompt];
                truths.Remove(truths[selectedPrompt]);
                answer = true;
            }
            else //lie
            {
                selectedPrompt = Random.Range(0, lies.Count);
                truthOrLie.text = lies[selectedPrompt];
                lies.Remove(lies[selectedPrompt]);
                answer = false;
            }
            curRound++;
        }
        else
        {
            //Set Game Over text
            //Check winner
            truthCheck.gameObject.SetActive(false);
            lieCheck.gameObject.SetActive(false);
            home.gameObject.SetActive(true);
            if (score1>score2)
            {
                gameOver.text = "Game Over: Team One Wins";
            }
            else if(score2>score1)
            {
                gameOver.text = "Game Over: Team Two Wins";
            }
            else // tie - note should not occur unless even rounds is implemented 
            {
                gameOver.text = "Game Over: It's a draw";
            }
        }
    }


    [SerializeField]
    TMP_Text scoreOne;
    [SerializeField]
    TMP_Text scoreTwo;
    int score1 = 0;
    int score2 = 0;
    // Update is called once per frame
    void Update()
    {
        if(countDown) //timer should run
        {
            if(time > 0)
            {
                time -= Time.deltaTime;
                Timer(time);
            }
            else //loss
            {
                time = 0;
                countDown = false;

                //if(roundCount != roundlimit)
                SetRound();
                if (teamOne.Contains(players[playerIndex]))//if player was on team one and time expires add points for team 1
                {
                    score1++;
                    scoreOne.text = "Score: " + score1;
                }
                else //was on team 2 so team 2 gets points
                {
                    score2++;
                    scoreTwo.text = "Score: " + score2;
                }
                //Loss calculations
            }
        }
    }
    void Timer(float t)
    {
        t += 1;
        float minutes = Mathf.FloorToInt(t / 60);
        float seconds = Mathf.FloorToInt(t % 60);
        timer.text = string.Format("{0:00}:{1:00}", minutes, seconds);

    }

    public void True()
    {
        truthOrLie.enabled = false;
        answer = true;
        countDown = true;
        timer.gameObject.SetActive(true);
        time = 300;
        truth.gameObject.SetActive(false);
        lie.gameObject.SetActive(false);
        truthCheck.gameObject.SetActive(true);
        lieCheck.gameObject.SetActive(true);
    }
    public void False()
    {
        truthOrLie.enabled = false;
        answer = false;
        countDown = true;
        timer.gameObject.SetActive(true);
        time = 300;
        truth.gameObject.SetActive(false);
        lie.gameObject.SetActive(false);
        truthCheck.gameObject.SetActive(true);
        lieCheck.gameObject.SetActive(true);
    }

    public void CheckTrue()
    {
        countDown = false;
        if (answer) //Correct answer was a truth
        {
            //add win points
            if (teamOne.Contains(players[playerIndex]))//if player was on team one and team 2 got it right add points for team 2
            {
                score2++;
                scoreTwo.text = "Score: " + score2;
            }
            else //was on team 2 so team 1 gets points for getting it right
            {
                score1++;
                scoreOne.text = "Score: " + score1;
            }
        }
        else
        {
            //add loss
            if (teamOne.Contains(players[playerIndex]))//if player was on team one and team 2 got it wrong add points for team 1
            {
                score1++;
                scoreOne.text = "Score: " + score1;
            }
            else //was on team 2 so team 2 gets points because team one got it wrong
            {
                score2++;
                scoreTwo.text = "Score: " + score2;
            }
        }
        //if(roundCount != roundlimit)
        SetRound();
    }
    public void CheckFalse()
    {
        countDown = false;
        if (!answer) //correct answer was a lie
        {
            //add win
            if (teamOne.Contains(players[playerIndex]))//if player was on team one and team 2 got it right add points for team 2
            {
                score2++;
                scoreTwo.text = "Score: " + score2;
            }
            else //was on team 2 so team 1 gets points for getting it right
            {
                score1++;
                scoreOne.text = "Score: " + score1;
            }
        }
        else
        {
            //add loss
            if (teamOne.Contains(players[playerIndex]))//if player was on team one and team 2 got it wrong add points for team 1
            {
                score1++;
                scoreOne.text = "Score: " + score1;
            }
            else //was on team 2 so team 2 gets points because team one got it wrong
            {
                score2++;
                scoreTwo.text = "Score: " + score2;
            }
        }
        //if(roundCount != roundlimit)
        SetRound();
    }


    List<string> Lies()
    {
        List<string> l = new List<string>();
        l.Add("I was bitten by a shark while swimming in the ocean.");
        l.Add("I've been kicked by a kangaroo.");
        l.Add("I was locked inside an airport bathroom for 2 hours.");
        l.Add("I broke my foot while stepping off a curb.");
        l.Add("English is my family’s second language.");
        l.Add("I was named my high school's spelling bee champion.");
        l.Add("I learned how to speak Elvish after reading The Lord of the Rings.");
        l.Add("I can always tell when someone is telling a lie.");
        l.Add("I trained my cat to use the toilet.");
        l.Add("I skipped a grade in middle school.");
        l.Add("I can hold my breath underwater for two minutes.");
        l.Add("I am a speed reader.");
        l.Add("I'm related to a very famous person.");
        l.Add("I still own a huge collection of Beanie Babies.");
        l.Add("My favorite animal is the platypus.");
        l.Add("I don't know how to write in cursive.");
        l.Add("I'm color-blind.");
        l.Add("I've never gotten a speeding ticket.");
        l.Add("I still sleep with the stuffed teddy bear I was given as a kid.");
        l.Add("I am deathly afraid of clowns.");
        return l;
    }
}
