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


    }
}
