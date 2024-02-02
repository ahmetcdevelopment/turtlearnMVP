using Microsoft.AspNetCore.Mvc.Rendering;
using TurtLearn.Shared.Entities.Abstract;
using TurtLearn.Shared.Entities.Dtos;
using TurtLearn.Shared.Utilities.Extensions;
using TurtLearn.Shared.Utilities.Results.Concrete;

namespace turtlearnMVP.WEB.Helpers
{
    public class TableHelper
    {
        public static string GetTableTitle<TTable>() where TTable : class, IEntity, new()
        {
            return TableExtensions.GetTableTitle<TTable>();
        }

        public static int GetTableId<TTable>() where TTable : class, IEntity, new()
        {
            return TableExtensions.GetTableId<TTable>();
        }

        /// <summary>
        /// örnek kullanım  var selectList = TableHelper.GetTablesSeleclist(new Comment(), new Category(), new SessionRollCall());
        /// </summary>
        public static SelectList GetTablesSeleclist(params IEntity[] entities)
        {
            var tableObjectList = TableExtensions.GetSpecificTableObjectsList(entities);

            var selectList = new SelectList(tableObjectList, "Value", "Text");

            return selectList;
        }
    }
}
