using CVideoAPI.Datasets.Question;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CVideoAPI.Services.Question
{
    public interface IQuestionService
    {
        Task<List<QuestionSetDataset>> GetQuestionSets(QuestionSetParam param);
        Task<QuestionSetDataset> GetQuestionSet(int setId);
        Task<QuestionSetDataset> GetQuestionSet(string setName);
        Task<bool> CreateQuestionSet(QuestionSetParam param);
        Task<bool> UpdateQuestionSet(QuestionSetParam param);
        Task<bool> DeleteQuestionSet(int id);
        Task<List<QuestionDataset>> GetQuestions(int? setId);
        Task<QuestionDataset> GetQuestion(int questionId);
        Task<bool> CreateQuestion(QuestionParam param);
        Task<bool> UpdateQuestion(QuestionParam param);
        Task<bool> DeleteQuestion(int id);
    }
}
