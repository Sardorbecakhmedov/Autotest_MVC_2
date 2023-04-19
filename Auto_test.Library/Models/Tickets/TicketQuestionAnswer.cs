
namespace Auto_test.Library.Models.Tickets;

public class TicketQuestionAnswer
{
    public int Id { get; set; }
    public int QuestionIndex { get; set; }
    public int ChoiceIndex { get; set; }
    public int CorrectAnswerIndex { get; set; }
    public bool IsCorrectAnswer => ChoiceIndex == CorrectAnswerIndex;
    public string? UserId { get; set; }
}