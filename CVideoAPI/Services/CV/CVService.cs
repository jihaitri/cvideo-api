using AutoMapper;
using CVideoAPI.Datasets.CV;
using CVideoAPI.Datasets.CV.Section;
using CVideoAPI.Datasets.CV.Section.Field;
using CVideoAPI.Datasets.CV.SectionType;
using CVideoAPI.Datasets.Video;
using CVideoAPI.Models;
using CVideoAPI.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CVideoAPI.Services.CV
{
    public class CVService : ICVService
    {
        private readonly IUnitOfWork _uow;
        private IMapper _mapper;
        public CVService(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<CVDataset> CreateCV(int userId, string cvName, int majorId)
        {
            Models.CV cv = new Models.CV()
            {
                EmployeeId = userId,
                Title = cvName,
                MajorId = majorId
            };
            _uow.CVRepository.Insert(cv);
            if (await _uow.CommitAsync() > 0)
            {
                return _mapper.Map<CVDataset>(await _uow.CVRepository.GetById(cv.CVId));
            }
            return null;
        }

        public async Task<bool> DeleteCV(int id)
        {
            _uow.CVRepository.Delete(id);
            return await _uow.CommitAsync() > 0;
        }

        public async Task<CVDataset> GetCVByName(string cvName, int userId)
        {
            return _mapper.Map<CVDataset>(await _uow.CVRepository.GetFirst(filter: cv => cv.Title == cvName && cv.EmployeeId == userId));
        }

        public async Task<CVDataset> GetCVById(int id, string lang = "vi")
        {
            CVDataset result = null;
            Models.CV cv = await _uow.CVRepository.GetFirst(filter: cv => cv.CVId == id, includeProperties: "Employee,Employee.Account");
            if (cv != null)
            {
                result = _mapper.Map<CVDataset>(cv);
                result.Major = new Datasets.Major.MajorDataset()
                {
                    MajorId = cv.MajorId.GetValueOrDefault(0),
                    MajorName = (await _uow.TranslationRepository.GetFirst(filter: t => t.MajorId == cv.MajorId.GetValueOrDefault(0) && t.Language == lang)).Text
                };
                IEnumerable<Section> sections = await _uow.SectionRepository.Get(filter: section => section.CVId == id, includeProperties: "SectionType");
                IEnumerable<SectionField> fields = await _uow.SectionFieldRepository.Get(filter: field => field.Section.CVId == id, includeProperties: "Section");
                IEnumerable<Models.Video> videos = await _uow.VideoRepository.Get(filter: video => video.Section.CVId == id, includeProperties: "Section,VideoStyle");
                result.Sections = (from section in sections
                                   select new CVSectionDataset()
                                   {
                                       SectionId = section.SectionId,
                                       CVId = id,
                                       SectionTypeId = section.SectionTypeId,
                                       Text = section.Text,
                                       Title = section.DisplayTitle,
                                       Icon = section.SectionType.Image,
                                       Fields = (from field in fields
                                                 where field.SectionId == section.SectionId
                                                 select new CVFieldDataset()
                                                 {
                                                     FieldId = field.FieldId,
                                                     Name = field.FieldTitle,
                                                     Text = field.Text
                                                 }).ToList(),
                                       Videos = (from video in videos
                                                 where video.SectionId == section.SectionId
                                                 select new VideoDataset()
                                                 {
                                                     AspectRatio = video.AspectRatio,
                                                     ThumbUrl = video.ThumbUrl,
                                                     VideoUrl = video.VideoUrl,
                                                     CoverUrl = video.CoverUrl,
                                                     VideoId = video.VideoId,
                                                     VideoStyle = new VideoStyleDataset()
                                                     {
                                                         StyleId = video.VideoStyle.StyleId,
                                                         StyleName = video.VideoStyle.StyleName
                                                     }
                                                 }).ToList()
                                   }).ToList();
            }
            return result;
        }
        public async Task<bool> UpdateCV(NewCVParam param)
        {
            Models.CV cv = await _uow.CVRepository.GetById(param.CVId);
            cv.MajorId = param.MajorId;
            cv.Title = param.Title;
            _uow.CVRepository.Update(cv);
            return await _uow.CommitAsync() > 0;
        }
        public async Task<List<CVSectionTypeDataset>> GetCVSectionTypes(string lang)
        {
            IEnumerable<SectionType> list = await _uow.SectionTypeRepository.Get(orderBy: type => type.OrderBy(type => type.DispOrder));
            IEnumerable<Translation> titles = await _uow.TranslationRepository.Get(filter: trans => trans.Language == lang);
            List<CVSectionTypeDataset> result = (from type in list
                                                 join title in titles on type.SectionTypeId equals title.SectionTypeId
                                                 where title.Language == lang
                                                 select new CVSectionTypeDataset()
                                                 {
                                                     SectionTypeId = type.SectionTypeId,
                                                     DispOrder = type.DispOrder,
                                                     Icon = type.Image,
                                                     SectionTypeName = title.Text
                                                 }).ToList();
            return result;
        }

        public async Task<CVSectionTypeDataset> GetCVSectionType(int id, string lang)
        {
            SectionType type = await _uow.SectionTypeRepository.GetFirst(filter: type => type.SectionTypeId == id);
            if (type != null)
            {
                Translation trans = await _uow.TranslationRepository.GetFirst(filter: trans => trans.SectionTypeId == id);
                return new CVSectionTypeDataset()
                {
                    SectionTypeId = type.SectionTypeId,
                    DispOrder = type.DispOrder,
                    Icon = type.Image,
                    SectionTypeName = trans.Text
                };
            }
            return null;
        }

        public async Task<CVSectionDataset> CreateSection(NewSectionParam section)
        {
            if (await _uow.SectionRepository.GetFirst(filter: s => s.CVId == section.CVId && s.SectionTypeId == section.SectionTypeId) != null)
            {
                throw new Exception("Confict");
            }
            Section newSection = new Section()
            {
                CVId = section.CVId,
                SectionTypeId = section.SectionTypeId,
                DisplayTitle = section.Title
            };
            if (section.Title == null)
            {
                Translation trans = await _uow.TranslationRepository.GetFirst(filter: trans => trans.SectionTypeId == section.SectionTypeId
                                                                                            && trans.Language == "vi");
                newSection.DisplayTitle = trans.Text;
            }
            if (section.Text != null && section.Text != string.Empty)
            {
                newSection.Text = section.Text;
            }
            List<CVFieldDataset> fields = section.Fields;
            if (fields != null && fields.Count > 0)
            {
                for (int i = 0; i < fields.Count; i++)
                {
                    _uow.SectionFieldRepository.Insert(new SectionField()
                    {
                        FieldTitle = fields[i].Name,
                        SectionId = newSection.SectionId,
                        Text = fields[i].Text
                    });
                }
            }
            _uow.SectionRepository.Insert(newSection);
            if (await _uow.CommitAsync() > 0)
            {
                return _mapper.Map<CVSectionDataset>(await _uow.SectionRepository.GetById(newSection.SectionId));
            }
            return null;
        }
        public async Task<List<CVSectionDataset>> GetCVSections(int cvId)
        {
            return _mapper.Map<List<CVSectionDataset>>(await _uow.SectionRepository.Get(filter: section => section.CVId == cvId,
                                                                                        orderBy: section => section.OrderBy(section => section.SectionType.DispOrder),
                                                                                        includeProperties: "SectionType"));
        }
        public async Task<CVSectionDataset> GetCVSection(int cvId, int sectionId)
        {
            Section section = await _uow.SectionRepository.GetFirst(filter: section => section.SectionId == sectionId && section.CVId == cvId);
            section.SectionFields = (await _uow.SectionFieldRepository.Get(filter: field => field.SectionId == section.SectionId,
                                                                            orderBy: field => field.OrderBy(field => field.Created)))
                                                                            .ToList();
            return _mapper.Map<CVSectionDataset>(section);
        }
        public async Task<bool> UpdateSection(NewSectionParam section)
        {
            Section updateSection = await _uow.SectionRepository.GetById(section.SectionId);
            updateSection.DisplayTitle = section.Title;
            updateSection.Text = section.Text;
            _uow.SectionRepository.Update(updateSection);
            return await _uow.CommitAsync() > 0;
        }
        public async Task<bool> DeleteSection(int sectionId)
        {
            _uow.SectionRepository.Delete(sectionId);
            return await _uow.CommitAsync() > 0;
        }

        public async Task<List<CVFieldDataset>> GetSectionFields(int sectionId)
        {
            return _mapper.Map<List<CVFieldDataset>>(await _uow.SectionFieldRepository.Get(filter: field => field.SectionId == sectionId,
                                                                                        orderBy: field => field.OrderBy(field => field.Created)));
        }
        public async Task<CVFieldDataset> GetSectionField(int sectionId, int fieldId)
        {
            return _mapper.Map<CVFieldDataset>(await _uow.SectionFieldRepository.GetFirst(filter: field => field.FieldId == fieldId && field.SectionId == sectionId));
        }

        public async Task<CVFieldDataset> CreateField(int sectionId, CVFieldDataset field)
        {
            SectionField newField = new SectionField()
            {
                FieldTitle = field.Name,
                SectionId = sectionId,
                Text = field.Text
            };
            _uow.SectionFieldRepository.Insert(newField);
            if (await _uow.CommitAsync() > 0)
            {
                return _mapper.Map<CVFieldDataset>(await _uow.SectionFieldRepository.GetById(newField.FieldId));
            }
            return null;
        }

        public async Task<bool> UpdateField(CVFieldDataset field)
        {
            SectionField updateField = await _uow.SectionFieldRepository.GetById(field.FieldId);
            updateField.FieldTitle = field.Name;
            updateField.Text = field.Text;
            _uow.SectionFieldRepository.Update(updateField);
            return await _uow.CommitAsync() > 0;
        }

        public async Task<bool> DeleteField(int fieldId)
        {
            _uow.SectionFieldRepository.Delete(fieldId);
            return await _uow.CommitAsync() > 0;
        }
    }
}
