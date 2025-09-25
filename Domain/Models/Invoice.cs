using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace Domain.Models;

[Table("Invoice")]
public partial class Invoice
{
    [Key]
    [Column("InvoiceID")]
    public int InvoiceId { get; set; }

    [Column("STNO")]
    public int? Stno { get; set; }

    [Column("BOSFX")]
    public int? Bosfx { get; set; }

    [Column("SLSTYP")]
    [StringLength(1)]
    [Unicode(false)]
    public string? Slstyp { get; set; }

    [Column("ENDT8")]
    [StringLength(10)]
    [Unicode(false)]
    public string? Endt8 { get; set; }

    [Column("DOCSU")]
    [StringLength(1)]
    [Unicode(false)]
    public string? Docsu { get; set; }

    [Column("RFDCNO")]
    [StringLength(20)]
    [Unicode(false)]
    public string? Rfdcno { get; set; }

    [Column("LNNO")]
    public int? Lnno { get; set; }

    [Column("SOS1")]
    public int? Sos1 { get; set; }

    [Column("PANO20")]
    [StringLength(50)]
    [Unicode(false)]
    public string? Pano20 { get; set; }

    [Column("DS18")]
    [StringLength(50)]
    [Unicode(false)]
    public string? Ds18 { get; set; }

    [Column("ORQY")]
    public int? Orqy { get; set; }

    [Column("BOIMCT")]
    public int? Boimct { get; set; }

    [Column("SPQTY")]
    public int? Spqty { get; set; }

    [Column("TRXCD")]
    [StringLength(5)]
    [Unicode(false)]
    public string? Trxcd { get; set; }

    [Column("UNCS", TypeName = "decimal(12, 3)")]
    public decimal? Uncs { get; set; }

    [Column("UNLS", TypeName = "decimal(12, 3)")]
    public decimal? Unls { get; set; }

    [Column("UNSEL", TypeName = "decimal(12, 3)")]
    public decimal? Unsel { get; set; }

    [Column("WTUM", TypeName = "decimal(12, 3)")]
    public decimal? Wtum { get; set; }

    [Column("CUNM")]
    [StringLength(100)]
    [Unicode(false)]
    public string? Cunm { get; set; }

    [Column("SPVIDS")]
    [StringLength(50)]
    [Unicode(false)]
    public string? Spvids { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? DateEntry { get; set; }

    public StatusInvoicesE statusInvoices {
        get {
            if(Docsu == "F") {
                return StatusInvoicesE.Invoice;
            } else {
                return StatusInvoicesE.Awaiting;
            }
        } 
    }
}
