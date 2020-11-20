using AutoMapper;
using CVideoAPI.Datasets.Question;
using CVideoAPI.Models;
using CVideoAPI.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CVideoAPI.Services.Question
{
    public class QuestionService : IQuestionService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        public QuestionService(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<List<QuestionDataset>> GetQuestions(int? setId)
        {
            IEnumerable<Models.Question> listQ = null;
            if (setId == null)
            {
                listQ = await _uow.QuestionRepository.Get(orderBy: q => q.OrderBy(q => q.DispOrder));
            }
            else
            {
                listQ = await _uow.QuestionRepository.Get(filter: q => q.SetId == setId,
                                                            orderBy: q => q.OrderBy(q => q.DispOrder));
            }
            return _mapper.Map<List<QuestionDataset>>(listQ);
        }
        public async Task<List<QuestionSetDataset>> GetQuestionSets(QuestionSetParam param)
        {
            IEnumerable<Models.QuestionSet> listSet = await _uow.QuestionSetRepository.Get(filter: set => (param.SetName == null || set.SetName.Contains(param.SetName)),
                                                                                            includeProperties: "SectionType",
                                                                                            first: param.Limit,
                                                                                            offset: param.Offset);
            return _mapper.Map<List<QuestionSetDataset>>(listSet);
        }
        public async Task<QuestionSetDataset> GetQuestionSet(int setId)
        {
            QuestionSetDataset result = null;
            QuestionSet set = await _uow.QuestionSetRepository.GetFirst(filter: set => set.SetId == setId, includeProperties: "SectionType");
            if (set != null)
            {
                IEnumerable<Models.Question> questions = await _uow.QuestionRepository.Get(filter: q => q.SetId == setId);
                result = _mapper.Map<QuestionSetDataset>(set);
                result.Questions = _mapper.Map<List<QuestionDataset>>(questions);
                result.SectionType.SectionTypeName = (await _uow.TranslationRepository.GetFirst(filter: type => type.SectionTypeId == result.SectionType.SectionTypeId && type.Language == "en")).Text;
            }
            return result;
        }
        public async Task<QuestionSetDataset> GetQuestionSet(string setName)
        {
            QuestionSetDataset result = null;
            QuestionSet set = await _uow.QuestionSetRepository.GetFirst(filter: set => set.SetName == setName, includeProperties: "SectionType");
            if (set != null)
            {
                IEnumerable<Models.Question> questions = await _uow.QuestionRepository.Get(filter: q => q.SetId == set.SetId);
                result = _mapper.Map<QuestionSetDataset>(set);
                result.Questions = _mapper.Map<List<QuestionDataset>>(questions);
                result.SectionType.SectionTypeName = (await _uow.TranslationRepository.GetFirst(filter: type => type.SectionTypeId == result.SectionType.SectionTypeId && type.Language == "en")).Text;
            }
            return result;
        }
        public async Task<bool> CreateQuestionSet(QuestionSetParam param)
        {
            _uow.QuestionSetRepository.Insert(new QuestionSet()
            {
                SetName = param.SetName,
                SectionTypeId = param.SectionTypeId
            });
            return await _uow.CommitAsync() > 0;
        }

        public async Task<bool> UpdateQuestionSet(QuestionSetParam param)
        {
            QuestionSet entity = await _uow.QuestionSetRepository.GetFirst(filter: set => set.SetId == param.SetId);
            if (entity != null)
            {
                entity.SectionTypeId = param.SectionTypeId;
                entity.SetName = param.SetName;
                _uow.QuestionSetRepository.Update(entity);
            }
            return await _uow.CommitAsync() > 0;
        }

        public async Task<bool> CreateQuestion(QuestionParam param)
        {
            _uow.QuestionRepository.Insert(new Models.Question()
            {
                QuestionContent = param.QuestionContent,
                SetId = param.SetId,
                DispOrder = param.DispOrder,
                QuestionTime = param.QuestionTime
            });
            return await _uow.CommitAsync() > 0;
        }

        public async Task<bool> UpdateQuestion(QuestionParam param)
        {
            Models.Question question = await _uow.QuestionRepository.GetFirst(filter: ques => ques.QuestionId == param.QuestionId);
            if (question != null)
            {
                question.QuestionTime = param.QuestionTime;
                question.QuestionContent = param.QuestionContent;
                question.DispOrder = param.DispOrder;
                question.SetId = param.SetId;
                _uow.QuestionRepository.Update(question);
            }
            return await _uow.CommitAsync() > 0;
        }

        public async Task<bool> DeleteQuestion(int id)
        {
            _uow.QuestionRepository.Delete(id);
            return await _uow.CommitAsync() > 0;
        }

        public async Task<bool> DeleteQuestionSet(int id)
        {
            _uow.QuestionSetRepository.Delete(id);
            return await _uow.CommitAsync() > 0;
        }
        public async Task<QuestionDataset> GetQuestion(int questionId)
        {
            return _mapper.Map<QuestionDataset>(await _uow.QuestionRepository.GetById(questionId));
        }
    }
}
