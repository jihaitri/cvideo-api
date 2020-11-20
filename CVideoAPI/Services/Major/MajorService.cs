using AutoMapper;
using CVideoAPI.Datasets.Major;
using CVideoAPI.Models;
using CVideoAPI.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CVideoAPI.Services.Major
{
    public class MajorService : IMajorService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        public MajorService(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }
        public async Task<List<MajorDataset>> GetMajors(string lang)
        {
            IEnumerable<Models.Major> majors = await _uow.MajorRepository.Get();
            IEnumerable<Translation> trans = await _uow.TranslationRepository.Get(filter: t => t.MajorId != null && t.Language == lang); ;
            List<MajorDataset> result = (from major in majors
                                         join tran in trans on major.MajorId equals tran.MajorId
                                         select new MajorDataset()
                                         {
                                             MajorId = major.MajorId,
                                             MajorName = tran.Text
                                         }).ToList();
            return result;
        }
    }
}
