using Microsoft.AspNetCore.Mvc.Rendering;
using System.Reflection;
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

        //public static SelectList GetTablesSelectlistAll()
        //{
        //    var entities = GetAllEntities();
        //    var tableObjectList = TableExtensions.GetSpecificTableObjectsList(entities);
        //    var selectList = new SelectList(tableObjectList, "Value", "Text");

        //    return selectList;
        //}


        /// <summary>
        /// örnek kullanım  var selectList = TableHelper.GetTablesSeleclist(new Comment(), new Category(), new SessionRollCall());
        /// </summary>
        public static SelectList GetTablesSeleclist(params IEntity[] entities)
        {
            var tableObjectList = TableExtensions.GetSpecificTableObjectsList(entities);

            var selectList = new SelectList(tableObjectList, "Value", "Text");

            return selectList;
        }

        public static SelectList GetTablesSelectlistAll()
        {
            var entities = TableExtensions.GetAllEntities();
            var tableObjectList = TableExtensions.GetSpecificTableObjectsList(entities);

            var selectList = new SelectList(tableObjectList, "Value", "Text");

            return selectList;
        }

        public static SelectList GetTablesSelectlistAllWithSpecial(params (int Value, string Text)[] specialItems)
        {
            // IEntity arabirimini uygulayan tüm sınıfları al
            var entities = TableExtensions.GetAllEntities();

            // Tablo nesnelerini oluştur
            var tableObjectList = TableExtensions.GetSpecificTableObjectsList(entities);

            // Özel öğeleri seçim listesine ekle
            var selectList = new SelectList(tableObjectList, "Value", "Text");
            foreach (var specialItem in specialItems)
            {
                selectList = AddSpecialItemToSelectList(selectList, specialItem.Value, specialItem.Text);
            }

            return selectList;
        }

        private static SelectList AddSpecialItemToSelectList(SelectList selectList, int value, string text)
        {
            var items = new List<SelectListItem>();

            // SelectList içindeki öğeleri kopyala
            foreach (var item in selectList)
            {
                items.Add(new SelectListItem { Value = item.Value.ToString(), Text = item.Text });
            }

            // Yeni öğeyi ekleyerek SelectList'i güncelle
            items.Insert(0, new SelectListItem { Value = value.ToString(), Text = text });

            return new SelectList(items, "Value", "Text");
        }
    }
}
