using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Product.Model;

[Table("PRODUCTMASTER")]
public partial class Productmaster
{
    [Key]
    [Column("PRODUCTID")]
    [StringLength(50)]
    [Unicode(false)]
    public string Productid { get; set; } = null!;

    [Column("PRODUCTNAME")]
    [StringLength(50)]
    [Unicode(false)]
    public string Productname { get; set; } = null!;

    [Column("PRODUCTTYPEID")]
    [StringLength(50)]
    [Unicode(false)]
    public string Producttypeid { get; set; } = null!;

    [Column("PRODUCTSTATUS")]
    public int Productstatus { get; set; }

    [Column("CREATEUSER")]
    [StringLength(50)]
    [Unicode(false)]
    public string Createuser { get; set; } = null!;

    [Column("CREATEDATE", TypeName = "datetime")]
    public DateTime Createdate { get; set; }

    [Column("UPDATEUSER")]
    [StringLength(50)]
    [Unicode(false)]
    public string? Updateuser { get; set; }

    [Column("UPDATEDATE", TypeName = "datetime")]
    public DateTime? Updatedate { get; set; }

    [ForeignKey("Producttypeid")]
    [InverseProperty("Productmasters")]
    public virtual Producttype Producttype { get; set; } = null!;
}
