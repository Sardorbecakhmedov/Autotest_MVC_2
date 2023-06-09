﻿using Auto_test.Library.Models.QuestionModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Autotest_MVC_2.Services.QuestionServices;

public class QuestionService
{
    public List<QuestionModel>? Questions { get; set; }
    public int QuestionCount => 10;
    public int TotalQuestionCount => Questions!.Count;
    public int TicketCount => Questions!.Count / 10;

    public QuestionService()
    {
        Questions ??= new List<QuestionModel>();
        GetAllQuestionsAsync().Wait();
    }


    public async Task GetAllQuestionsAsync(string language = "uzlotin")
    {
        var filePath = $@"..\Auto_test.Library\Models\QuestionsJson\{language}.json";

        using var streamReader = new StreamReader(filePath);
        var jsonString = await streamReader.ReadToEndAsync();
        Questions = JsonConvert.DeserializeObject<List<QuestionModel>>(jsonString);
    }
    
}