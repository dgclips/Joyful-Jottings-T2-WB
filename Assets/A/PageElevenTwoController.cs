using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PageElevenTwoController : MonoBehaviour
{
    [System.Serializable]
    public class ToggleQuestion
    {
        public Toggle toggleA;
        public Toggle toggleB;
        public string correctAnswer; // "A" or "B"
    }

    public List<ToggleQuestion> questions;
    int count;
    void Start()
    {
        foreach (var question in questions)
        {
            question.toggleA.onValueChanged.AddListener((isOn) => OnToggleChanged(question, "A", isOn));
         if (question.toggleB != null) question.toggleB.onValueChanged.AddListener((isOn) => OnToggleChanged(question, "B", isOn));
        }
    }

    void OnToggleChanged(ToggleQuestion question, string selected, bool isOn)
    {
        if (!isOn) return;

        if (question.correctAnswer == selected)
        {
            count++;
            if(count == questions.Count)
            {
                EventManager.GameComplete();
            }
            if (selected == "A")
            {
                question.toggleA.isOn = true;
              if(question.toggleB!=null)  question.toggleB.isOn = false;
               question.toggleA.enabled = false;
            }
            else
            {
            if (question.toggleB != null) question.toggleB.isOn = true; question.toggleB.enabled = false;
                question.toggleA.isOn = false;
            }
        }
        else
        {
            // Deselect if the selected answer is incorrect
            if (selected == "A") question.toggleA.isOn = false;
            else question.toggleB.isOn = false;
        }
    }

    public void ResetAllQuestions()
    {
        foreach (var question in questions)
        {
            question.toggleA.isOn = false;
         if (question.toggleB != null) question.toggleB.isOn = false;
         question.toggleA.enabled = true;
         if (question.toggleB != null) question.toggleB.enabled = true;
        }
        count = 0;
    }
}
