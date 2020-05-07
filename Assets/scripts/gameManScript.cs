using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using UnityEngine.SceneManagement;

public class gameManScript : MonoBehaviour
{
    public Transform levelText;
    public Transform infoText;
    public Transform scoreText;
    public Transform timeText;
    public static string curWord = "";
    public static ArrayList curWords;
    public static int score = 0;
    public GameObject letterButton;
    public int wordCount = 3;
    public int letterCount = 3;
    public float timeForALetter = 1.5f;
    private static float timeLeft;
    private static int level = 0;
    // Start is called before the first frame update
    void Start()
    {

        setLevel();
        timeLeft = timeForALetter * (float)letterCount * (float)wordCount + 1.0f;

        //letterCount 3'se 3 harflik kelimeler içeren dosyadaki bütün veriler okunup tempList'e kaydedilir.
        ArrayList wordList = getAllWords(letterCount);
        
        //wordList'ten wordCount kadar kelime seçip döndürüyor.
        curWords = selectWords(wordCount, wordList);
        
        //Seçilen kelimelerin herbir harfi tek sefer olacak şekilde rastgele bir şekilde  letterList'e atanıyor.
        ArrayList letterList = findLetterSet(curWords);
        
        //letterList'teki her bir harf için harf butonları oluşturuluyor.
        createLetterButtons(letterList);
        updateLevelText();

    }

    private void updateLevelText()
    {
        string info = "Level " + level + "\n" + curWords.Count + " word";
        int bestScore = 0;
        if (PlayerPrefs.HasKey(level.ToString() + "level"))
            bestScore = PlayerPrefs.GetInt(level.ToString()+ "level");

        info += "\nBest score: " + bestScore;
        levelText.GetComponent<TextMesh>().text = info;
    }

    void setLevel()
    {
        int curLevel;
        if (PlayerPrefs.HasKey("curLevel")) {

             curLevel = PlayerPrefs.GetInt("curLevel");
        } else{
            PlayerPrefs.SetInt("curLevel", 1);
            curLevel = 1;
            
        }
        level = curLevel;
        
        string path = @"c:\Users\asus\Documents\puzzleGame\Assets\scripts\levels.txt";
        using (StreamReader sr = File.OpenText(path))
        {

            ArrayList tempList = new ArrayList();
            string s;
            while ((s = sr.ReadLine()) != null)
            {
                tempList.Add(s);
            }

            if (tempList.Count >= curLevel)
            {
                string[] arr = (tempList[curLevel - 1] as string).Split(' ');
                letterCount = int.Parse(arr[0]);
                wordCount = int.Parse(arr[1]);
                Debug.Log("Level " + curLevel + " yukleniyor.\nwordCount: " + wordCount + "\nletterCount: " + letterCount);
            }
            else
            {
                string[] arr = (tempList[tempList.Count - 1] as string).Split(' ');
                wordCount = int.Parse(arr[0]);
                letterCount = int.Parse(arr[1]);
                Debug.Log("Level " + curLevel + " yukleniyor.\nwordCount: " + wordCount + "\nletterCount: " + letterCount);
            }
        }

    }

    ArrayList getAllWords(int letterCount)
    {
        ArrayList tempList = new ArrayList();
        string path = @"c:\Users\asus\Documents\puzzleGame\Assets\scripts\" + letterCount + ".txt";
        using (StreamReader sr = File.OpenText(path))
        {
            string s;
            while ((s = sr.ReadLine()) != null)
            {
                tempList.Add(s);
            }
        }
        return tempList;
    }

    ArrayList selectWords(int wordCount,ArrayList wordList)
    {

        ArrayList curWords = new ArrayList();

        while (0 < wordCount-- && wordList.Count > 0)
        {
            int tempIndex = (int)UnityEngine.Random.Range(0, wordList.Count);
            curWords.Add(wordList[tempIndex]);
            wordList.RemoveAt(tempIndex);
            Debug.Log(curWords.Count + ". kelime: " + curWords[curWords.Count - 1]);
        }
        wordList.Clear();

        return curWords;
    }

    ArrayList findLetterSet(ArrayList curWords)
    {
        ArrayList letterList = new ArrayList();
        int[] countArr = new int[26];
        foreach (string word in curWords)
        {
            foreach (char c in word)
            {
                countArr[(int)c - 65]++;
                if (countArr[(int)c - 65] == 1)
                {
                    int randomIndex = ((int)c - 65) % (letterList.Count + 1);
                    letterList.Insert(randomIndex, c);
                }
            }
        }

        return letterList;
    }
    

    void createLetterButtons(ArrayList letterList)
    {
        float baseX = -2.2f;
        float baseY = -2.0f;
        foreach (char c in letterList)
        {
            GameObject tempObj = Instantiate(letterButton, new Vector3(baseX, baseY, -28), Quaternion.identity);
            tempObj.GetComponent<TextMesh>().text = c.ToString();

            baseX += 0.75f;

            if (baseX > 2.2f)
            {
                baseX = -2.2f;
                baseY -= 0.75f;
            }
        }
    }

    public void FixedUpdate()
    {
        infoText.GetComponent<TextMesh>().text = curWord;
        scoreText.GetComponent<TextMesh>().text = "Score: " + score;
        timeLeft -= Time.deltaTime;
        timeText.GetComponent<TextMesh>().text = "Timer: " + (int)timeLeft;
    }

    public static void saveProgress()
    {
        Debug.Log("Bolum geçildi \ncurLevel: " + PlayerPrefs.GetInt("curLevel"));
        score += (int)timeLeft;
        if (PlayerPrefs.HasKey("curLevel"))
            PlayerPrefs.SetInt("curLevel", PlayerPrefs.GetInt("curLevel") + 1);
        else
            PlayerPrefs.SetInt("curLevel", 2);

        if(PlayerPrefs.HasKey(level.ToString()+ "level"))
        {
            if (PlayerPrefs.GetInt(level.ToString() + "level") < score)
                PlayerPrefs.SetInt(level.ToString() + "level", score);
        }

        PlayerPrefs.Save();
        ///Save progress
        SceneManager.LoadScene("gameScene", LoadSceneMode.Single);


    }
    
    //y 3.5 dan -1 e kadar
    //x -2 den 2 ye kadar

    /*public void Update()
    {
        timeLeft -= Time.deltaTime;
        timeText.GetComponent<TextMesh>().text = "Timer: " + timeLeft;

    }*/

}
