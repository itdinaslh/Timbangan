﻿using DocumentFormat.OpenXml.Bibliography;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Timbangan.Domain.Repositories;
using Timbangan.Helpers;
using Timbangan.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.SignalR;
using Timbangan.Hubs;

namespace Timbangan.Controllers;

public class TransactionController : Controller
{
    private readonly ITransaction repo;
    private readonly IKendaraan kRepo;
    private readonly ISpjPKM spjRepo;
    private readonly IHubContext<PrintHub> _context;

    public TransactionController(ITransaction repo, IKendaraan kRepo, IHubContext<PrintHub> hubContext, ISpjPKM spjRepo)
    {
        this.repo = repo;
        this.kRepo = kRepo;
        this._context = hubContext;
        this.spjRepo = spjRepo;
    }

    [HttpGet("/transaction/masuk")]
    [Authorize(Roles = "OpMasuk")]
    public IActionResult Index()
    {
        return View("~/Views/Transaction/Masuk/Masuk.cshtml");
    }

    [HttpPost("/transaction/masuk/store")]
    [Authorize(Roles = "SysAdmin, OpMasuk")]
    public async Task<IActionResult> StoreMasuk(string noRFID, string berat)
    {
        string rf = noRFID;
        if (rf is not null)
        {
            Kendaraan? truk = await kRepo.Kendaraans
                .Include(x => x.AreaKerja.Penugasan)
                .FirstOrDefaultAsync(x => x.RFID == rf);

            if (truk is not null)
            {
                Transaction trans = new();

                trans.NoPolisi = truk.NoPolisi;
                trans.NoPintu = truk.NoPintu;
                trans.BeratMasuk = Convert.ToInt32(berat);
                trans.TglMasuk = DateOnly.FromDateTime(DateTime.Now);
                trans.JamMasuk = TimeOnly.FromDateTime(DateTime.Now);
                trans.InDateTime = DateTime.Now;
                trans.CreatedBy = User.Identity!.Name;
                trans.KendaraanID = truk.KendaraanID;
                trans.StatusID = 1;
                trans.RFID = truk.RFID;
                trans.AreaKerja = truk.AreaKerja.NamaArea;
                trans.Penugasan = truk.AreaKerja.Penugasan.NamaPenugasan;
                trans.UpdatedBy = User.Identity!.Name;
                trans.UpdatedAt = DateTime.Now;

                await repo.AddDataAsync(trans);

                return Json(Result.SuccessMasuk(berat, truk.NoPintu, truk.NoPolisi));
            }
        }

        return Json(Result.Failed());
    }

    [HttpPost("/transaction/keluar/store")]
    [Authorize(Roles = "SysAdmin, OpKeluar")]
    public async Task<IActionResult> StoreKeluar(string noRFID, string berat)
    {
        string rf = noRFID;

        Kendaraan? truk = await kRepo.Kendaraans.FirstOrDefaultAsync(x => x.RFID == rf);

        if (truk is not null)
        {
            Transaction? trans = await repo.Transactions
                .Where(x => x.KendaraanID == truk!.KendaraanID)
                .Where(x => x.StatusID == 1)
                .FirstOrDefaultAsync();

            if (trans is not null)
            {
                trans.BeratKeluar = Convert.ToInt32(berat);
                trans.UpdatedBy = User.Identity!.Name;                
                trans.TglKeluar = DateOnly.FromDateTime(DateTime.Now);
                trans.JamKeluar = TimeOnly.FromDateTime(DateTime.Now);
                trans.OutDateTime = DateTime.Now;

                int? nett = trans.BeratMasuk - trans.BeratKeluar;

                await repo.UpdateAsync(trans);

                //Dictionary<string, string> query = new Dictionary<string, string>
                //{
                //    { "TransactionID", trans.TransactionID.ToString() },
                //    { "NoPolisi", trans.NoPolisi },
                //    { "NoPintu", trans.NoPintu },
                //    { "PenugasanName", trans.PenugasanName! },
                //    { "TglMasuk", trans .InDateTime.ToString("dd-MM-yyyy HH:mm:ss") },
                //    { "TglKeluar", trans.OutDateTime.Value.ToString("dd-MM-yyyy HH:mm:ss") },
                //    { "BeratMasuk", trans.BeratMasuk.ToString() },
                //    { "BeratKeluar", trans.BeratKeluar.ToString() },
                //    { "Nett", nett.ToString() },

                //};

#nullable disable
                PrintStruk struk = new PrintStruk { 
                    TransactionID = trans.TransactionID.ToString(),
                    NoPolisi = trans.NoPolisi,
                    NoPintu = trans.NoPintu,
                    PenugasanName = trans.Penugasan!,
                    TglMasuk = trans.InDateTime.ToString("dd-MM-yyyy HH:mm:ss"),
                    TglKeluar = trans.OutDateTime.Value.ToString("dd-MM-yyyy HH:mm:ss"),
                    BeratMasuk = trans.BeratMasuk.ToString(),
                    BeratKeluar = trans.BeratKeluar.Value.ToString(),
                    Nett = nett.Value.ToString()
                };

#nullable enable

                await _context.Clients.All.SendAsync("PrintStruk", struk);

                return Json(Result.SuccessKeluar(berat, truk.NoPintu, truk.NoPolisi));
            }
        }

        return Json(Result.Failed());
    }

    [HttpPost("/transaction/keluar/spj/update")]
    public async Task<IActionResult> UpdateSPJ(string NoSPJ, string NoStruk, int BeratNett) {
        var data = await spjRepo.SpjAngkuts.Where(x => x.NoSPJ == NoSPJ).FirstOrDefaultAsync();

        if (data is not null) {
            data.NoStruk = NoStruk;
            data.TonaseTimbangan = BeratNett;
            data.IsFinished = true;

            await spjRepo.UpdateSPJ(data);

            return Ok();
        }

        return NotFound();
    }
}

public class PrintStruk
{
    public string TransactionID { get; set; } = "Test";

    public string NoPolisi { get; set; } = "Test";

    public string NoPintu { get; set; } = "Test";

    public string PenugasanName { get; set; } = "Test";

    public string TglMasuk { get; set; } = "Test";

    public string TglKeluar { get; set; } = "Test";

    public string BeratMasuk { get; set; } = "Test";

    public string BeratKeluar { get; set; } = "Test";

    public string Nett { get; set; } = "Test";
}
