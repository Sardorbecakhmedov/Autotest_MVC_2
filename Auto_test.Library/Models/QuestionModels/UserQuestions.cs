namespace Auto_test.Library.Models.QuestionModels;

public class UserQuestions
{
    public List<QuestionModel>? Questions { get; set; }


    /*  public async Task GetAllQuestionsAsync(string language)
      {
          var filePath = $@"C:\C#\WepProjects\Autotest_MVC_2\Auto_test.Library\Models\QuestionsJson\{language}.json";

          var jsonString = await File.ReadAllTextAsync(filePath);

          Questions = JsonConvert.DeserializeObject<List<QuestionModel>>(filePath);
      }*/

    /*    public async Task GetAllQuestionsAsync(string language)
    {
        var filePath = $"QuestionsJson/{language}.json";

        using (var streamReader = new StreamReader(filePath))
        {
            var jsonString = await streamReader.ReadToEndAsync();
            Questions = JsonConvert.DeserializeObject<List<QuestionModel>>(jsonString);
        }
    }*/
}
