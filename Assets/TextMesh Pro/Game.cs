using UnityEngine;


[System.Serializable]
public class Game
{
   public string gameID;
   public string userID;
   public int noOfCorrectAnswers;
   public int noOfWrongAnswers;
   public bool gameCompleted;
   public float accuracyRate;
   public int completionRate;
   public string noOfGame;
}




[System.Serializable]
public class GameContent
{
   public Game[] games;
}





