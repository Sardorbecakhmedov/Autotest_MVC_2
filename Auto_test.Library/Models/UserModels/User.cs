using Auto_test.Library.Models.Tickets;


namespace Auto_test.Library.Models.UserModels;

public class User : UserParent
{
    public string? UserId { get; set; }
    public string? ImagePath { get; set; }
    public int? CurrentTicketIndex { get; set; }
    public int IsCompletedTicketCount { get; set; }
    public int TotalCorrectAnswerCount { get; set; }
    public bool IsExam { get; set; }
    public int ExamCurrentTicketIndex { get; set; }
    public virtual List<Ticket>? UserTickets { get; set; }
    public Ticket? CurrentTicket { get; set; }
}