using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Product.Model
{

    [ModelMetadataType(typeof(ProductmasterMetadata))]
    public partial class Productmaster
    {
        public bool CreateProduct(ProductContext dbContext)
        {
            this.Createdate = DateTime.Now;

            string id = Guid.NewGuid().ToString();

            this.Productid = id;

            dbContext.Productmasters.Add(this);
            dbContext.SaveChanges();

            return true;
        }

        public bool EditProduct(ProductContext dbContext)
        {
            this.Updatedate = DateTime.Now;

            dbContext.Productmasters.Update(this);
            dbContext.SaveChanges();
            return true;
        }

        public bool DeleteProduct(ProductContext dbContext)
        {
            this.Productstatus = 9;
            dbContext.Update(this);
            return true;
        }

        public class ProductmasterMetadata
        {

            [Required]
            [StringLength(50)]
            public string Productid { get; set; } = null!;

            [Required]
            [StringLength(50)]
            public string Productname { get; set; } = null!;

            [Required]
            [StringLength(50)]
            public string Producttypeid { get; set; } = null!;

            [Required]
            public int Productstatus { get; set; }

            [StringLength(50)]
            public string Createuser { get; set; } = null!;

            [ValidateNever]
            public DateTime Createdate { get; set; }

            [StringLength(50)]
            public string? Updateuser { get; set; }

            public DateTime? Updatedate { get; set; }
        }
    }
}
