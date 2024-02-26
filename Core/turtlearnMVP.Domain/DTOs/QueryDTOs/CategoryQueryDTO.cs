//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Linq.Expressions;
//using System.Text;
//using System.Threading.Tasks;
//using TurtLearn.Shared.Entities.Abstract;
//using TurtLearn.Shared.Searching;

//namespace turtlearnMVP.Domain.DTOs.QueryDTOs
//{
//    public class CategoryQueryDTO : QueryDto<int, CategoryDTO>
//    {
//        public CategoryQueryDTO(ISearch<CategoryDTO> Search)
//        {
//            _Search = Search;
//        }

//        public override IQueryable<CategoryDTO> GetFilteredData(IQueryable<CategoryDTO> _Query, CategoryDTO Dto)
//        {
//            if (Expressions == null)
//                Expressions = new System.Linq.Expressions.Expression<Func<CategoryDTO, bool>>[_Search.GetPropertyCount()];
//            if (Dto.Id.HasValue)
//                Expressions.Append(c => c.Id == Dto.Id.Value);
//            if (!string.IsNullOrEmpty(Dto.Name))
//                Expressions.Append(c => c.Name.Trim().ToLower().Contains(Dto.Name.Trim().ToLower()));
//            if (!string.IsNullOrEmpty(Dto.Description))
//                Expressions.Append(c => c.Description.Trim().ToLower().Contains(Dto.Description.Trim().ToLower()));
//            if (!string.IsNullOrEmpty(Dto.Content))
//                Expressions.Append(c => c.Content.Trim().ToLower().Contains(Dto.Description.Trim().ToLower()));
//            if (Dto.SinifDuzeyiId.HasValue)
//                Expressions.Append(c => c.SinifDuzeyiId == Dto.SinifDuzeyiId.Value);
//            return _Search.FilterData(_Query, Expressions);
//        }
//    }
//}
