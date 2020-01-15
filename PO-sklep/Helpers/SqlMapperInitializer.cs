using Dapper;
using PO_sklep.Models;
using System;
using System.Linq;

namespace PO_sklep.Helpers
{
    public static class SqlMapperInitializer
    {
        public static void InitializeColumnMappings()
        {
            var currentAssembly = typeof(SqlMapperInitializer).Assembly;
            var entityTypes = currentAssembly
                .DefinedTypes
                .Where(type => type.ImplementedInterfaces.Contains(typeof(IEntity)))
                .ToList();

            foreach (var entityType in entityTypes)
            {
                SqlMapper.SetTypeMap(
                      entityType,
                      GetCustomMapperFor(entityType)
                      );
            }
        }

        private static SqlMapper.ITypeMap GetCustomMapperFor(Type t)
        {
            var mapperType = typeof(ColumnAttributeTypeMapper<>).MakeGenericType(t);
            return Activator.CreateInstance(mapperType) as SqlMapper.ITypeMap;
        }
    }
}
