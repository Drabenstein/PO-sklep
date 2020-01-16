using Dapper.Contrib.Extensions;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace PO_sklep.Models
{
    public class Category : IEntity
    {
        [Key]
        [Column("Id_kategorii")]
        public int Id { get; set; }
        [Column("Nazwa_kategorii")]
        public string Name { get; set; }
        [ExplicitKey]
        [Column("Id_nadkategorii")]
        public int? ParentCategoryId { get; set; }

        public Category ParentCategory { get; set; }
        public IList<Category> Subcategories { get; set; }
        public IList<Employee> Employees { get; set; }
        public IList<Product> Products { get; set; }
    }
}
