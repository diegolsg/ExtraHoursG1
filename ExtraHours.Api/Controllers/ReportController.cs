using Microsoft.AspNetCore.Mvc;
using ExtraHours.Core.Services;
using ClosedXML.Excel;
using System.IO;

[ApiController]
[Route("api/report")]
public class ReportController : ControllerBase
{
    private readonly ReportHoursService _reportService;

    public ReportController(ReportHoursService reportService)
    {
        _reportService = reportService;
    }

    [HttpGet("download")]
    public async Task<IActionResult> DownloadFullReport()
    {
        var report = await _reportService.GetFullReportAsync();

        using var workbook = new XLWorkbook();
        var worksheet = workbook.Worksheets.Add("Reporte Completo");

        worksheet.Cell(1, 1).Value = "Nombre";
        worksheet.Cell(1, 2).Value = "Código";
        worksheet.Cell(1, 3).Value = "Salario Base";
        worksheet.Cell(1, 4).Value = "Valor Horas Extras";
        worksheet.Cell(1, 5).Value = "Salario Total";

        for (int i = 0; i < report.Count; i++)
        {
            var r = report[i];
            worksheet.Cell(i + 2, 1).Value = r.Name;
            worksheet.Cell(i + 2, 2).Value = r.Code;
            worksheet.Cell(i + 2, 3).Value = r.Salary;
            worksheet.Cell(i + 2, 4).Value = r.TotalExtraValue;
            worksheet.Cell(i + 2, 5).Value = r.TotalSalaryWithExtras;
        }

        using var stream = new MemoryStream();
        workbook.SaveAs(stream);
        stream.Seek(0, SeekOrigin.Begin);

        var fileName = $"Reporte_HorasExtras_{DateTime.Now:yyyyMMddHHmmss}.xlsx";
        return File(stream.ToArray(),
                    "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                    fileName);
    }
}