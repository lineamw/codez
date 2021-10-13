using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElosztottLabor.DTOs


    public enum QuestionType { FreeText, MultipleChoice, TrueOrFalse }
{
public class AnswerDTO
    {

    public long Id { get; set; }
    //public AnswerType AnswerType { get; set; }
    public string AnswerText { get; set; }
    //public List<string> PossibleAnswers { get; set; }
    public int MaxAnswerLength { get; set; }

    public int questionFormId;
   
    public int questionId;

   
    // Because of the way JSON Deserialization works (first an object is created, then  
    // its properties set), we need a default constructor. 
    public AnswerDTO()
    {

    }

    public AnswerDTO(Answer Answer)
    {
        this.Id = Answer.Id;
        this.AnswerText = Answer.AnswerText;

        if (Answer is MultipleChoiceAnswer)
        {
            MapMultipleChoice(Answer as MultipleChoiceAnswer);
        }
        else if (Answer is TrueOrFalseAnswer)
        {
            MapTrueOrFalse(Answer as TrueOrFalseAnswer);
        }
        else if (Answer is FreeTextAnswer)
        {
            MapFreeTextAnswer(Answer as FreeTextAnswer);
        }
    }

    public Answer ToEntity()
    {
        switch (AnswerType)
        {
            case AnswerType.FreeText:
                return new FreeTextAnswer()
                {
                    Id = this.Id,
                    AnswerText = this.AnswerText,
                    MaxAnswerLength = this.MaxAnswerLength
                };
            case AnswerType.MultipleChoice:
                return new MultipleChoiceAnswer()
                {
                    Id = this.Id,
                    AnswerText = this.AnswerText,
                    PossibleAnswers = this.PossibleAnswers
                };
            case AnswerType.TrueOrFalse:
                return new TrueOrFalseAnswer()
                {
                    Id = this.Id,
                    AnswerText = this.AnswerText
                };
            default:
                throw new NotImplementedException();
        }
    }

    private void MapMultipleChoice(MultipleChoiceAnswer Answer)
    {
        this.AnswerType = AnswerType.MultipleChoice;
        this.PossibleAnswers = Answer.PossibleAnswers;
        this.MaxAnswerLength = 1;
    }

    private void MapTrueOrFalse(TrueOrFalseAnswer trueOrFalseAnswer)
    {
        this.AnswerType = AnswerType.TrueOrFalse;
        this.PossibleAnswers = new List<string>() { "true", "false" };
        this.MaxAnswerLength = 1;
    }

    private void MapFreeTextAnswer(FreeTextAnswer Answer)
    {
        this.AnswerType = AnswerType.FreeText;
        this.MaxAnswerLength = Answer.MaxAnswerLength;
    }
}
}
