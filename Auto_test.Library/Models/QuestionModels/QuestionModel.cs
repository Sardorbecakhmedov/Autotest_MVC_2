namespace Auto_test.Library.Models.QuestionModels;

public class QuestionModel
{
    public int Id { get; set; }
    public string? Question { get; set; }
    public List<Choices>? Choices { get; set; }
    public Media? Media { get; set; }
    public string? Description { get; set; }
}