using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject ballPrefab;
    public GameObject playerPrefab;
    public Text ScoreText;
    public Text BallsText;
    public Text LevelsText;
    public Text highscoreText;

    public GameObject panelMenu;
    public GameObject panelPlay;
    public GameObject panelLevelCompleted;
    public GameObject GameOver;

    public GameObject[] levels;

    public static GameManager Instance { get; private set; }

    public enum State { MENU, INITIALIZE, PLAY, LEVELCOMPLETED, LOADLEVEL, GAMEOVER}
    State _state;

    GameObject _currentball;
    GameObject _currentlevel;
    bool _isswitchingstate;

    private int _score;

    public int SCORE
    {
        get { return _score; }
        set { _score = value;
            ScoreText.text = "SCORE: " + _score;
        }
    }

    private int _balls;

    public int BALLS
    {
        get { return _balls; }
        set { _balls = value;
            BallsText.text = "BALLS: " + _balls;
        }
    }

    private int _levels;

    public int LEVELS
    {
        get { return _levels; }
        set { _levels = value;
            LevelsText.text = "Levels: " + _balls;
        }
    }


    public void playclicked()
    {
        SwitchState(State.INITIALIZE);
    }

    void Start()
    {
        Instance = this;
        SwitchState(State.MENU);
     
    }

    public void SwitchState(State newState, float delay = 0)
    {
        StartCoroutine(SwitchDelay(newState, delay));
    }

    IEnumerator SwitchDelay(State newState,float delay)
    {
        _isswitchingstate = true;
        yield return new WaitForSeconds(delay);
        EndState();
        _state = newState;
        BeginState(newState);
        _isswitchingstate = false;
    }

    void BeginState(State newState)
    {
        switch (newState)
        {
            case State.MENU:
                Cursor.visible = true; //Hide mouse
                highscoreText.text = "HIGHSCORE: " + PlayerPrefs.GetInt("highscore");
                panelMenu.SetActive(true);
                break;
            case State.INITIALIZE:
                Cursor.visible = false; //Hide mouse
                panelPlay.SetActive(true);
                SCORE = 0;
                LEVELS = 0;
                BALLS = 3;
                if(_currentlevel != null){
                    Destroy(_currentlevel);
                }
                Instantiate(playerPrefab);
                SwitchState(State.LOADLEVEL);
                break;
            case State.PLAY:
                break;
            case State.LEVELCOMPLETED:
                Destroy(_currentball);
                Destroy(_currentlevel);
                LEVELS++;
                panelLevelCompleted.SetActive(true);
                SwitchState(State.LOADLEVEL,1f);
                break;
            case State.LOADLEVEL:
                if(LEVELS >= levels.Length)
                {
                    SwitchState(State.GAMEOVER);
                }
                else
                {
                    _currentlevel = Instantiate(levels[LEVELS]);
                    SwitchState(State.PLAY);
                }
                break;
            case State.GAMEOVER:
                if(SCORE > PlayerPrefs.GetInt("highscore"))
                {
                    PlayerPrefs.SetInt("highscore", SCORE);
                }
                GameOver.SetActive(true);
                break;
        }

    }
        void Update()
    {
        switch (_state)
        {
            case State.MENU:
                break;
            case State.INITIALIZE:
                break;
            case State.PLAY:
                if(_currentball== null)
                {
                    if(BALLS > 0)
                    {
                        _currentball = Instantiate(ballPrefab);
                    }
                    else
                    {
                        SwitchState(State.GAMEOVER);
                    }
                }
                if (_currentlevel != null && _currentlevel.transform.childCount == 0 && !_isswitchingstate)
                {
                    SwitchState(State.LEVELCOMPLETED);
                }

                break;
            case State.LEVELCOMPLETED:
                break;
            case State.LOADLEVEL:
                break;
            case State.GAMEOVER:
                if (Input.anyKeyDown)
                {
                    SwitchState(State.MENU);
                }
                break;
        }
    }

    void EndState()
    {
        switch (_state)
        {
            case State.MENU:
                panelMenu.SetActive(false);
                break;
            case State.INITIALIZE:
                break;
            case State.PLAY:
                break;
            case State.LEVELCOMPLETED:
                panelLevelCompleted.SetActive(false);
                break;
            case State.LOADLEVEL:
                break;
            case State.GAMEOVER:
                GameOver.SetActive(false);
                panelPlay.SetActive(false);
                break;
        }
    }

    
}