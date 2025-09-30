using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace Domain.Models;

public class Invoice
{
    private int _invoiceId { get; set; }
    private int? _stno { get; set; }
    private int? _bosfx { get; set; }
    private string? _slstyp { get; set; }
    private string? _endt8 { get; set; }
    private string? _docsu;
    private string? _rfdcno { get; set; }
    private int? _lnno { get; set; }
    private int? _sos1 { get; set; }
    private string? _pano20;
    private string? _ds18;
    private int? _orqy;
    private int? _boimct;
    private int? _spqty;
    private string? _trxcd;
    private decimal? _uncs;
    private decimal? _unls;
    private decimal? _unsel;
    private decimal? _wtum;
    private string? _cunm;
    private string? _spvids;
    private DateTime? _dateEntry;


    #region Getters and Setters
    public int InvoiceId
    {
        get => _invoiceId;
        set => _invoiceId = value;
    }

    public int? Stno
    {
        get => _stno;
        set => _stno = value;
    }

    public int? Bosfx
    {
        get => _bosfx;
        set => _bosfx = value;
    }

    public string? Slstyp
    {
        get => _slstyp;
        set => _slstyp = value;
    }

    public string? Endt8
    {
        get => _endt8;
        set => _endt8 = value;
    }

    public string? Docsu
    {
        get => _docsu == "F" ? "Invoice" : "Awaiting";
        set => _docsu = value;
    }

    public string? Rfdcno
    {
        get => _rfdcno;
        set => _rfdcno = value;
    }

    public int? Lnno
    {
        get => _lnno;
        set => _lnno = value;
    }

    public int? Sos1
    {
        get => _sos1;
        set => _sos1 = value;
    }

    public string? Pano20
    {
        get => _pano20;
        set => _pano20 = value;
    }

    public string? Ds18
    {
        get => _ds18;
        set => _ds18 = value;
    }

    public int? Orqy
    {
        get => _orqy;
        set => _orqy = value;
    }

    public int? Boimct
    {
        get => _boimct;
        set => _boimct = value;
    }

    public int? Spqty
    {
        get => _spqty;
        set => _spqty = value;
    }

    public string? Trxcd
    {
        get => _trxcd switch
        {
            "CS" => "Mostrador",
            "SS" => "Taller",
            _ => "Unknown"
        };
        set => _trxcd = value;
    }

    public decimal? Uncs
    {
        get => _uncs;
        set => _uncs = value;
    }

    public decimal? Unls
    {
        get => _unls;
        set => _unls = value;
    }

    public decimal? Unsel
    {
        get => _unsel;
        set => _unsel = value;
    }

    public decimal? Wtum
    {
        get => _wtum;
        set => _wtum = value;
    }

    public string? Cunm
    {
        get => _cunm;
        set => _cunm = value;
    }

    public string? Spvids
    {
        get => _spvids;
        set => _spvids = value;
    }

    public DateTime? DateEntry
    {
        get => _dateEntry;
        set => _dateEntry = value;
    }

    public StatusInvoicesE StatusInvoice
    {
        get => _docsu == "F" ? StatusInvoicesE.Invoice : StatusInvoicesE.Awaiting;
    }
    #endregion

    #region Constructors
    public Invoice()
    {
    }

    public Invoice(int invoiceId, int? stno, int? bosfx, string? slstyp, string? endt8, string? docsu, string? rfdcno, int? lnno, int? sos1, string? pano20, string? ds18, int? orqy, int? boimct, int? spqty, string? trxcd, decimal? uncs, decimal? unls, decimal? unsel, decimal? wtum, string? cunm, string? spvids, DateTime? dateEntry)
    {
        InvoiceId = invoiceId;
        Stno = stno;
        Bosfx = bosfx;
        Slstyp = slstyp;
        Endt8 = endt8;
        Docsu = docsu;
        Rfdcno = rfdcno;
        Lnno = lnno;
        Sos1 = sos1;
        Pano20 = pano20;
        Ds18 = ds18;
        Orqy = orqy;
        Boimct = boimct;
        Spqty = spqty;
        Trxcd = trxcd;
        Uncs = uncs;
        Unls = unls;
        Unsel = unsel;
        Wtum = wtum;
        Cunm = cunm;
        Spvids = spvids;
        DateEntry = dateEntry;
    }
    #endregion

    #region Parttern Builder
    public class Builder
    {
        private Invoice invoice = new Invoice();
        
        public Builder SetInvoiceId(int invoiceId) { invoice.InvoiceId = invoiceId; return this; }
        public Builder SetStno(int? stno) { invoice.Stno = stno; return this; }
        public Builder SetBosfx(int? bosfx) { invoice.Bosfx = bosfx; return this; }
        public Builder SetSlstyp(string? slstyp) { invoice.Slstyp = slstyp; return this; }
        public Builder SetEndt8(string? endt8) { invoice.Endt8 = endt8; return this; }
        public Builder SetDocsu(string? docsu) { invoice.Docsu = docsu; return this; }
        public Builder SetRfdcno(string? rfdcno) { invoice.Rfdcno = rfdcno; return this; }
        public Builder SetLnno(int? lnno) { invoice.Lnno = lnno; return this; }
        public Builder SetSos1(int? sos1) { invoice.Sos1 = sos1; return this; }
        public Builder SetPano20(string? pano20) { invoice.Pano20 = pano20; return this; }
        public Builder SetDs18(string? ds18) { invoice.Ds18 = ds18; return this; }
        public Builder SetOrqy(int? orqy) { invoice.Orqy = orqy; return this; }
        public Builder SetBoimct(int? boimct) { invoice.Boimct = boimct; return this; }
        public Builder SetSpqty(int? spqty) { invoice.Spqty = spqty; return this; }
        public Builder SetTrxcd(string? trxcd) { invoice.Trxcd = trxcd; return this; }
        public Builder SetUncs(decimal? uncs) { invoice.Uncs = uncs; return this; }
        public Builder SetUnls(decimal? unls) { invoice.Unls = unls; return this; }
        public Builder SetUnsel(decimal? unsel) { invoice.Unsel = unsel; return this; }
        public Builder SetWtum(decimal? wtum) { invoice.Wtum = wtum; return this; }
        public Builder SetCunm(string? cunm) { invoice.Cunm = cunm; return this; }
        public Builder SetSpvids(string? spvids) { invoice.Spvids = spvids; return this; }
        public Builder SetDateEntry(DateTime? dateEntry) { invoice.DateEntry = dateEntry; return this; }
        
        public Invoice Build() { return invoice; }
    }
    #endregion
}
