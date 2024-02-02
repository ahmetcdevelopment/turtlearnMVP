using TurtLearn.Shared.Entities.Dtos;
using TurtLearn.Shared.Utilities.Results.Abstract;

namespace turtlearnMVP.WEB.Helpers.Abstract
{
    /// <summary>
    /// Resim ile ilgili işlemlerin manage edileceği sınıf.
    /// </summary>
    public interface IImageHelper
    {
        Task<IDataResult<ImageUploadedDto>> UploadUserImg(string userName, IFormFile pictureFile, string folderName = "userImages");
        IDataResult<ImageDeletedDto> DeleteImg(string pictureName);
    }
}
