using System.Linq.Expressions;
using turtlearnMVP.Domain.DTOs;

namespace turtlearnMVP.WEB.Search
{
    //Course ile ilgili Filtrelemeler Buradadır. Yeni bir filtreleme sınıfı yazılırken İlgili Filtreleme adı ile class açılmalı.
    public class Course
    {
        //filtreleme kriterleri burada belirlenir. isme bir standart getirmek adına bu şekilde yapıldı.
        public class Criteria
        {
            public string Name { get; set; }
        }

        //türetilen kriter sınıfı doldurulur ve namespace yazılarak buraya geri gönderilir.
        public Expression<Func<CourseDTO, bool>> CreateFilter(Criteria criteria)
        {
            Expression<Func<CourseDTO, bool>> filter = null;

            if (!string.IsNullOrEmpty(criteria.Name))
            {
                filter = course => course.Name.Contains(criteria.Name);
            }

            return filter;
        }
    }

    /// <summary>
    /// -------------------------------------------------------------------------------\
    /// Kullanım örneği:
    /// 
    /// using turtlearnMVP.WEB.Search; //ilgili using. daha sonra değiştirilebilir mobile uygun hale getirmek için!!!
    /// 
    /// 
    /// var search = new Search.Course(); //Search klasöründeki Course olduğu belirlenir böylece entity ile karışmaz
    /// var criteria = new Search.Course.Criteria //Aynı şekilde Search Klasöründeki Course içindeki kriter sınıfı içini doldurmak amacıyla kullanılır
    /// {
    ///     Name = "matematik" //kriterler belirlenir
    /// };
    /// Expression<Func<CourseDTO, bool>> filter = search.CreateFilter(criteria);  // filtre oluşturmak için yine aynı namespaceden metot kullanılır
    /// IList<CourseDTO> courses = _courseService.FetchAllDtos(filter).Data; //filtreli data çekilir.
    /// -------------------------------------------------------------------------------/ 
    /// </summary>
}
