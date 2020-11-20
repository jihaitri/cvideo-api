using CVideoAPI.Datasets.CV;
using CVideoAPI.Datasets.CV.Section;
using CVideoAPI.Datasets.CV.Section.Field;
using CVideoAPI.Datasets.CV.SectionType;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CVideoAPI.Services.CV
{
    public interface ICVService
    {
        Task<CVDataset> GetCVById(int id, string lang = "vi");
        Task<CVDataset> GetCVByName(string cvName, int userId);
        Task<bool> UpdateCV(NewCVParam param);
        Task<bool> DeleteCV(int id);
        Task<CVDataset> CreateCV(int userId, string cvName, int majorId);
        Task<List<CVSectionTypeDataset>> GetCVSectionTypes(string lang);
        Task<CVSectionTypeDataset> GetCVSectionType(int id, string lang);
        Task<List<CVSectionDataset>> GetCVSections(int cvId);
        Task<CVSectionDataset> GetCVSection(int cvId, int sectionId);
        Task<CVSectionDataset> CreateSection(NewSectionParam section);
        Task<bool> UpdateSection(NewSectionParam section);
        Task<bool> DeleteSection(int sectionId);
        Task<List<CVFieldDataset>> GetSectionFields(int sectionId);
        Task<CVFieldDataset> GetSectionField(int sectionId, int fieldId);
        Task<CVFieldDataset> CreateField(int sectionId, CVFieldDataset field);
        Task<bool> UpdateField(CVFieldDataset field);
        Task<bool> DeleteField(int fieldId);

    }
}
