namespace Auto_test.Library.Models.Tickets;

public class Ticket
{
    public int TicketId { get; set; }
    public int TicketIndex { get; set; }
    public int QuestionsCount { get; set; } = 10;
    public int StarQuestionIndex { get; set; }
    public int CurrentQuestionIndex { get; set; }
    public DateTime CreateDateTime { get; set; }
    public List<TicketQuestionAnswer>? TicketAnswers { get; set; }

    public int? CorrectAnswerCount => TicketAnswers?.Count(answer => answer.IsCorrectAnswer);

    public Ticket()
    {
        TicketAnswers = new List<TicketQuestionAnswer>();
    }   // Default Constructor 

}