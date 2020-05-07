using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnswerControl : MonoBehaviour
{
    

    private void OnMouseDown()
    {
        string answer = gameManScript.curWord;
        int tempScore = gameManScript.score;
        
        foreach (string word in gameManScript.curWords)
        {
            if(answer.Equals(word))
            {
                Debug.Log("Correct Answer: " + word);
                gameManScript.curWords.Remove(word);
                gameManScript.score += answer.Length;
                //Debug.Log("Score: " + gameManScript.score);
                break;
            }
        }

        if(tempScore == gameManScript.score)
        {
            gameManScript.score -= answer.Length;
            //Debug.Log("Score: " + gameManScript.score);
        }

        if(gameManScript.curWords.Count == 0)
        {
            gameManScript.saveProgress();
        }
            
        gameManScript.curWord = "";
        
    }
}
