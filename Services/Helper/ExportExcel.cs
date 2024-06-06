using ClosedXML.Excel;
using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Services.Data;
using ServicesContract.Model;
using ServicesContracts.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Services.Helper
{
    public class ExportExcel
    {
        private readonly MyDBContext _myDBContext;
        private readonly ILogger<ExportExcel> _logger;

        public ExportExcel(MyDBContext myDBContext,ILogger<ExportExcel> logger)
        {
            _myDBContext = myDBContext;
            _logger = logger;
        }


        /// <summary>
        /// Creates and exports an Excel sheet based on the provided ExcelExportModel.
        /// </summary>
        /// <param name="excelExportModel">The model containing export parameters.</param>
        /// <returns>A FileContentResult containing the exported Excel sheet.</returns>
        public async Task<FileContentResult> ExportExcelHelperAsync(ExcelExportModel Category)
        
        {
            try
            {
                //For Browser
                var data =await _myDBContext.ExportExcelCategoryAsync(Category);
                //For PostMan
                //var data = _myDBContext.sp_ExportExcelCategory(excelExportModel);


                if (data != null && data.Count > 0)
                {
                    using (XLWorkbook wb = new XLWorkbook())
                    {
                        wb.Worksheets.Add(ToConvertDataTable(data));
                        using (MemoryStream memoryStream = new MemoryStream())
                        {
                            wb.SaveAs(memoryStream);
                            var content = memoryStream.ToArray();
                            return new FileContentResult(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
                            {
                                FileDownloadName = "ExcelExport.xlsx"
                            };
                        }
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogDebug("This is a debug message: ", ex.Message);
                throw;
            }
        }
    

        /// <summary>
        /// Converts a list of items to a DataTable.
        /// </summary>
        /// <typeparam name="T">The type of items in the list.</typeparam>
        /// <param name="items">The list of items to convert.</param>
        /// <returns>A DataTable containing the converted data.</returns>
      
        public DataTable ToConvertDataTable<T>(List<T> items)
        {
            DataTable dt = new DataTable(typeof(T).Name);
            PropertyInfo[] properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo property in properties)
            {
                dt.Columns.Add(property.Name);
            }
            foreach (T item in items)
            {
                var value = new object[properties.Length];
                for (int i = 0; i < properties.Length; i++)
                {
                    value[i] = properties[i].GetValue(item, null);
                }
                dt.Rows.Add(value);
            }
            return dt;
        }
    }
}


