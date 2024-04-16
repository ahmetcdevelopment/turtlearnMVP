using TurtLearn.Shared.Entities.Dtos;
using TurtLearn.Shared.Utilities.Extensions;
using TurtLearn.Shared.Utilities.Results.Abstract;
using TurtLearn.Shared.Utilities.Results.ComplexTypes;
using TurtLearn.Shared.Utilities.Results.Concrete;
using turtlearnMVP.WEB.Helpers.Abstract;

namespace turtlearnMVP.WEB.Helpers.Concrete
{
    public class ImageHelper : IImageHelper
    {
        private readonly IWebHostEnvironment _env;

        public ImageHelper(IWebHostEnvironment env)
        {
            _env = env;
        }

        private readonly string wwwroot = "wwwroot";
        private readonly string imgFolder = "images";
        public IDataResult<ImageDeletedDto> DeleteImg(string pictureName)
        {
            var fileToDelete = Path.Combine($"{wwwroot}/{imgFolder}/", pictureName);
            if (System.IO.File.Exists(fileToDelete))
            {
                var fileInfo = new FileInfo(fileToDelete);//FileInfon'nun çalışması için dosyanın bulunması lazım
                var imageDeletedDto = new ImageDeletedDto// o yüzden fileInfo'daki bilgileri burada yedekliyorum
                {
                    FullName = pictureName,
                    Extensions = fileInfo.Extension,
                    Path = fileInfo.FullName,
                    Size = fileInfo.Length
                };
                System.IO.File.Delete(fileToDelete);
                return new DataResult<ImageDeletedDto>(ResultStatus.Success, imageDeletedDto);
            }
            else
            {
                return new DataResult<ImageDeletedDto>(ResultStatus.Error, $"Böyle bir resim bulunamadı.", null);
            }
        }

        public async Task<IDataResult<ImageUploadedDto>> UploadUserImg(string userName, IFormFile pictureFile, string folderName = "avatar")
        {
            if (!Directory.Exists($"{wwwroot}/{imgFolder}/{folderName}"))
            {
                Directory.CreateDirectory($"{wwwroot}/{imgFolder}/{folderName}");
            }
            // ~/img/user,Picture şeklinde kullanacağız
            //ahmetciftci
            string oldFileName = Path.GetFileNameWithoutExtension(pictureFile.FileName);
            //.png
            string fileExtensions = Path.GetExtension(pictureFile.FileName);
            DateTime dateTime = DateTime.Now;
            string newFileName = $"{userName}-{dateTime.FullDateAndTimeStringWithUnderScore()}{fileExtensions}";
            var path = Path.Combine($"{wwwroot}/{imgFolder}/{folderName}", newFileName);
            await using (var stream = new FileStream(path, FileMode.Create))
            {
                await pictureFile.CopyToAsync(stream);
            }

            return new DataResult<ImageUploadedDto>(ResultStatus.Success, $"{userName} adlı kullanıcının resmi başarıyla güncellenmiştir.", new ImageUploadedDto
            {
                FullName = $"{folderName}/{newFileName}",
                FileName = newFileName,
                OldName = oldFileName,
                Extension = fileExtensions,
                FolderName = folderName,
                Path = path,
                Size = pictureFile.Length

            }); //ahmetciftci_564_5_38_26_10_22.png
        }
    }
}
