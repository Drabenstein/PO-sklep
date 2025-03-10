﻿using Dapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PO_sklep.Helpers
{
    public class ColumnAttributeTypeMapper<T> : FallbackTypeMapper
    {
        public ColumnAttributeTypeMapper()
            : base(new SqlMapper.ITypeMap[]
                {
                new CustomPropertyTypeMap(
                   typeof(T),
                   (type, columnName) =>
                       type.GetProperties().FirstOrDefault(prop =>
                           prop.GetCustomAttributes(false)
                               .OfType<ColumnAttribute>()
                               .Any(attr => attr.Name == columnName)
                           )
                   ),
                new DefaultTypeMap(typeof(T))
                })
        {

        }
    }
}
