//using System.Linq.Expressions;
//using TurtLearn.Shared.Searching;
//using turtlearnMVP.Application.Persistance.Services;
//using turtlearnMVP.Domain.DTOs;
//using turtlearnMVP.Domain.Entities;
//using static turtlearnMVP.WEB.Search.Course;

//namespace turtlearnMVP.WEB.Search;

////Course ile ilgili Filtrelemeler Buradadır. Yeni bir filtreleme sınıfı yazılırken İlgili Filtreleme adı ile class açılmalı.
//public class Course
//{
//<<<<<<< Updated upstream
//    //filtreleme kriterleri burada belirlenir. isme bir standart getirmek adına bu şekilde yapıldı.
//    public class Criteria
//    {
//        public string Name { get; set; }
//    }

//    //türetilen kriter sınıfı doldurulur ve namespace yazılarak buraya geri gönderilir.
//    public Expression<Func<CourseDTO, bool>> CreateFilter(Criteria criteria)
//    {
//        Expression<Func<CourseDTO, bool>> filter = null;

//        if (!string.IsNullOrEmpty(criteria.Name))
//        {
//            filter = course => course.Name.Contains(criteria.Name);
//        }

//        return filter;
//    }
//=======
//    //Course ile ilgili Filtrelemeler Buradadır. Yeni bir filtreleme sınıfı yazılırken İlgili Filtreleme adı ile class açılmalı.
//    public class Course/* : Search<CourseDTO>*/
//    {
//        //private readonly BaseExpressions<CourseDTO>.StringContains _stringContains;
//        //private readonly AndSearch<CourseDTO> _and;
//        //private readonly ISearchService<CourseDTO> _searchService;
//        public Course(/*BaseExpressions<CourseDTO>.StringContains stringContains, AndSearch<CourseDTO> and*/ /*ISearchService<CourseDTO> searchService*/)
//        {
//            //_stringContains = stringContains;
//            //_and = and;
//            //_searchService = searchService;
//        }
//        //private readonly Criteria _criteria;
//        //public Course(Criteria criteria)
//        //{
//        //    _criteria = criteria ?? throw new ArgumentNullException(nameof(criteria));
//        //}
//        //       public override Expression<Func<CourseDTO, bool>> ToExpression(dynamic arguments)
//        //       {
//        //           var _criteria = arguments.Critera;
//        //           Expression<Func<CourseDTO, bool>> expression = x => true; // Başlangıçta her öğe uyumlu olsun.

//        //           if (_criteria != null)
//        //           {
//        //               if (_criteria.Name != null)
//        //               {
//        //                   expression = 
//        //1               }

//        //               if (_criteria.TeacherId.HasValue)
//        //               {
//        //                   // İfadeyi TeacherId kriterine göre filtrele
//        //                   expression = expression.And(x => x.TeacherId == _criteria.TeacherId);
//        //               }

//        //               if (_criteria.CategoryId.HasValue)
//        //               {
//        //                   // İfadeyi CategoryId kriterine göre filtrele
//        //                   expression = expression.And(x => x.CategoryId == _criteria.CategoryId);
//        //               }

//        //               if (_criteria.TeacherName != null)
//        //               {
//        //                   // İfadeyi TeacherName kriterine göre filtrele
//        //                   expression = expression.And(x => x.TeacherName == _criteria.TeacherName);
//        //               }

//        //               // Diğer kriterler de aynı şekilde eklenmeli.
//        //           }

//        //           return expression;
//        //       }

//        //filtreleme kriterleri burada belirlenir. isme bir standart getirmek adına bu şekilde yapıldı.
//        public class Criteria
//        {
//            public string? Name { get; set; }
//            public int? TeacherId { get; set; }
//            public int? CategoryId { get; set; }
//            public string? TeacherName { get; set; }
//            public int? Quota { get; set; }
//            public decimal? PricePerHourMin { get; set; }//kurs için saatlik ücret.
//            public decimal? PricePerHourMax { get; set; }//kurs için saatlik ücret.
//            public decimal? TotalPriceMin { get; set; }//PricePerHour * TotalHour
//            public decimal? TotalPriceMax { get; set; }//PricePerHour * TotalHour
//            public int? Status { get; set; }
//        }



//        //public class BaseQuery : Search<CourseDTO>
//        //{
//        //    public override Expression<Func<CourseDTO, bool>> ToExpression()
//        //    {
//        //        Expression<Func<CourseDTO, bool>> filter = course => true;

//        //        if (!string.IsNullOrEmpty(criteria.Name))
//        //        {
//        //            filter = filter.Add course => course.Name.Contains(criteria.Name);

//        //        }

//        //    }
//        //}

//        //private readonly ISearch<CourseDTO> _search;
//        //public Course(ISearch<CourseDTO> search)
//        //{
//        //    _search = search;
//        //}

//        //türetilen kriter sınıfı doldurulur ve namespace yazılarak buraya geri gönderilir.
//        public class Filter
//        {

//            private readonly ISearchService<CourseDTO> _searchService;
//            public Filter(/*BaseExpressions<CourseDTO>.StringContains stringContains, AndSearch<CourseDTO> and*/ ISearchService<CourseDTO> searchService)
//            {
//                //_stringContains = stringContains;
//                //_and = and;
//                _searchService = searchService;
//            }
//            public Expression<Func<CourseDTO, bool>> CreateFilter(Criteria criteria)
//            {
//                Expression<Func<CourseDTO, bool>> filter = x => true;

//                //if (!string.IsNullOrEmpty(criteria.Name))
//                //{
//                //    var arguments = new { PropertyName = "Name", Value = criteria.Name };
//                //    var newFilter = _stringContains.ToExpression(arguments);
//                //    filter = _and.And(filter, newFilter).ToExpression(null);
//                //}
//                //if (criteria.TeacherId != null && criteria.TeacherId > 0)
//                //{
//                //    newFilter = course => course.TeacherId == criteria.TeacherId;
//                //    //filter = filter != null ?  : newFilter;
//                //}
//                //if (criteria.CategoryId != null && criteria.CategoryId > 0)
//                //{
//                //    //Expression<Func<CourseDTO, bool>> categoryIdFilter = course => course.CategoryId == criteria.CategoryId;
//                //    //filter = CombineFilters(filter, categoryIdFilter);
//                //    newFilter = course => course.CategoryId == criteria.CategoryId;
//                //    filter = filter != null ? /*burada ve ile birleşmesi gerek*/ : newFilter;
//                //}
//                //if (!string.IsNullOrEmpty(criteria.TeacherName))
//                //{
//                //    newFilter = course => (course.TeacherName + " " + course.TeacherLastName).Contains(criteria.TeacherName);
//                //    filter = filter != null ?  : newFilter;

//                //}
//                //if (criteria.Quota != null && criteria.Quota > 0)
//                //{
//                //    //burayı düşüneceğim sayı aralıkları veya grup ders özel ders gibi seçtirilenbilir
//                //}

//                //#region Saatlik
//                //if (criteria.PricePerHourMin != null && criteria.PricePerHourMin > 0 && criteria.PricePerHourMax != null && criteria.PricePerHourMax > 0)
//                //{
//                //    newFilter = course => course.PricePerHour >= criteria.PricePerHourMin && course.PricePerHour <= criteria.PricePerHourMax;
//                //    filter = filter != null ?  : newFilter;
//                //}
//                //if (criteria.PricePerHourMin != null && criteria.PricePerHourMin > 0)
//                //{
//                //    newFilter = course => course.PricePerHour >= criteria.PricePerHourMin;
//                //    filter = filter != null ?: newFilter;
//                //}
//                //if (criteria.PricePerHourMax != null && criteria.PricePerHourMax > 0)
//                //{
//                //    newFilter = course => course.PricePerHour <= criteria.PricePerHourMax;
//                //    filter = filter != null ? : newFilter;
//                //}
//                //#endregion

//                //#region Total
//                //if (criteria.TotalPriceMin != null && criteria.TotalPriceMin > 0 && criteria.TotalPriceMax != null && criteria.TotalPriceMax > 0)
//                //{
//                //    newFilter = course => course.PricePerHour >= criteria.TotalPriceMin && course.PricePerHour <= criteria.TotalPriceMax;
//                //    filter = filter != null ? : newFilter;
//                //}
//                //if (criteria.TotalPriceMin != null && criteria.TotalPriceMin > 0)
//                //{
//                //    newFilter = course => course.PricePerHour >= criteria.TotalPriceMin;
//                //    filter = filter != null ? : newFilter;
//                //}
//                //if (criteria.TotalPriceMax != null && criteria.TotalPriceMax > 0)
//                //{
//                //    newFilter = course => course.PricePerHour <= criteria.TotalPriceMax;
//                //    filter = filter != null ? : newFilter;
//                //}
//                //#endregion

//                //if (criteria.Status != null && criteria.Status > 0)
//                //{
//                //    newFilter = course => course.Status == criteria.Status;
//                //    filter = filter != null ? : newFilter;
//                //}

//                return filter;
//            }

//        }
//    }



//    /// <summary>
//    /// -------------------------------------------------------------------------------\
//    /// Kullanım örneği:
//    /// 
//    /// using turtlearnMVP.WEB.Search; //ilgili using. daha sonra değiştirilebilir mobile uygun hale getirmek için!!!
//    /// 
//    /// 
//    /// var search = new Search.Course(); //Search klasöründeki Course olduğu belirlenir böylece entity ile karışmaz
//    /// var criteria = new Search.Course.Criteria //Aynı şekilde Search Klasöründeki Course içindeki kriter sınıfı içini doldurmak amacıyla kullanılır
//    /// {
//    ///     Name = "matematik" //kriterler belirlenir
//    /// };
//    /// Expression<Func<CourseDTO, bool>> filter = search.CreateFilter(criteria);  // filtre oluşturmak için yine aynı namespaceden metot kullanılır
//    /// IList<CourseDTO> courses = _courseService.FetchAllDtos(filter).Data; //filtreli data çekilir.
//    /// -------------------------------------------------------------------------------/ 
//    /// </summary>
//>>>>>>> Stashed changes
//}

///// <summary>
///// -------------------------------------------------------------------------------\
///// Kullanım örneği:
///// 
///// using turtlearnMVP.WEB.Search; //ilgili using. daha sonra değiştirilebilir mobile uygun hale getirmek için!!!
///// 
///// 
///// var search = new Search.Course(); //Search klasöründeki Course olduğu belirlenir böylece entity ile karışmaz
///// var criteria = new Search.Course.Criteria //Aynı şekilde Search Klasöründeki Course içindeki kriter sınıfı içini doldurmak amacıyla kullanılır
///// {
/////     Name = "matematik" //kriterler belirlenir
///// };
///// Expression<Func<CourseDTO, bool>> filter = search.CreateFilter(criteria);  // filtre oluşturmak için yine aynı namespaceden metot kullanılır
///// IList<CourseDTO> courses = _courseService.FetchAllDtos(filter).Data; //filtreli data çekilir.
///// -------------------------------------------------------------------------------/ 
///// </summary>

