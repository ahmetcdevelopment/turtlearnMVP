using STM.Core.Utilities.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TurtLearn.Shared.Entities.Abstract;
using TurtLearn.Shared.Entities.Dtos;
using TurtLearn.Shared.Utilities.Attributes;

namespace TurtLearn.Shared.Utilities.Extensions
{
    public static class TableExtensions
    {
        public static string GetTableTitle<TTable>() where TTable : class, IEntity, new()
        {
            Type entityType = typeof(TTable);
            var titleAttribute = entityType.GetCustomAttribute<TableInfoAttribute>().Title;
            return !string.IsNullOrEmpty(titleAttribute) ? titleAttribute : string.Empty;
        }

        public static int GetTableId<TTable>() where TTable : class, IEntity, new()
        {
            Type entityType = typeof(TTable);
            var titleAttribute = entityType.GetCustomAttribute<TableInfoAttribute>().Id;
            return titleAttribute;
        }

        public static List<TableObjectDto> GetSpecificTableObjectsList(params object[] entities)
        {
            List<TableObjectDto> specificTableObjectsList = new List<TableObjectDto>();

            foreach (var entity in entities)
            {
                Type entityType = entity.GetType();
                TableInfoAttribute tableInfoAttribute = entityType.GetCustomAttribute<TableInfoAttribute>();

                if (tableInfoAttribute != null)
                {
                    int id = tableInfoAttribute.Id;
                    string title = tableInfoAttribute.Title;

                    TableObjectDto specificTableObjectDTO = new TableObjectDto
                    {
                        Value = id,
                        Text = title
                    };

                    specificTableObjectsList.Add(specificTableObjectDTO);
                }
                else
                {
                    TableObjectDto specificTableObjectDTO = new TableObjectDto
                    {
                        Value = 0,
                        Text = "Bulunamadı"
                    };

                    specificTableObjectsList.Add(specificTableObjectDTO);
                }
            }

            return specificTableObjectsList;
        }

        public static IEntity[] GetAllEntities()
        {
            var entityTypes = new List<IEntity>();

            // Mevcut assembly'nin yanı sıra diğer assembly'lerden de tüm tipleri al
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            foreach (var assembly in assemblies)
            {
                var types = assembly.GetTypes();
                foreach (var type in types)
                {
                    // IEntity arabirimini uygulayan tüm tipleri ekle
                    if (typeof(IEntity).IsAssignableFrom(type) && type.IsClass && !type.IsAbstract)
                    {
                        // Tipi oluştur ve IEntity olarak dönüştürerek listeye ekle
                        var entityInstance = (IEntity)Activator.CreateInstance(type);
                        entityTypes.Add(entityInstance);
                    }
                }
            }

            return entityTypes.ToArray();
        }

        public static string GetTitleForId(int id)
        {
            IEntity[] allEntities = GetAllEntities();

            foreach (var entity in allEntities)
            {
                Type entityType = entity.GetType();
                var tableInfoAttribute = entityType.GetCustomAttribute<TableInfoAttribute>();

                if (tableInfoAttribute != null && tableInfoAttribute.Id == id)
                {
                    return tableInfoAttribute.Title;
                }
            }

            return "Bulunamadı"; // Eğer ID'ye karşılık gelen başlık bulunamazsa null döndür
        }
    }
}
