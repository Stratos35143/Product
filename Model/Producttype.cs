using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Product.Model;

[Table("PRODUCTTYPE")]
public partial class Producttype
{
    [Key]
    [Column("PRODUCTTYPEID")]
    [StringLength(50)]
    [Unicode(false)]
    public string Producttypeid { get; set; } = null!;

    [Column("PRODUCTTYPENAME")]
    [StringLength(50)]
    [Unicode(false)]
    public string Producttypename { get; set; } = null!;

    [InverseProperty("Producttype")]
    public virtual ICollection<Productmaster> Productmasters { get; set; } = new List<Productmaster>();
}
