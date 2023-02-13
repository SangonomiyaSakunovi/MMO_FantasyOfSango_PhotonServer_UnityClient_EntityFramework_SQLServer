using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OfficeOpenXml;

//Developer : SangonomiyaSakunovi
//Discription:

namespace Common.Tools
{
    public class ReadFile
    {
        /// <summary>
        /// ReadExcel with the excelLocation, only can read the first worksheet, need Plugins: EPPlus, return is [List[List[string]]]
        /// </summary>
        /// <param name="excelLocation"></param>
        /// <returns></returns>
        public static List<List<string>> ReadExcel(string excelLocation)
        {
            List<List<string>> excelInfoList = new List<List<string>>();
            using (var package = new ExcelPackage(new FileInfo(excelLocation)))
            {
                ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
                using (ExcelWorksheet worksheet = package.Workbook.Worksheets[0])
                {
                    int rowCount = worksheet.Dimension.Rows;
                    int columnCount = worksheet.Dimension.Columns;
                    for (int row = 1; row <= rowCount; row++)
                    {
                        List<string> tempList = new List<string>();
                        for (int column = 1; column <= columnCount; column++)
                        {
                            tempList.Add(worksheet.Cells[row, column].Value.ToString());
                        }
                        excelInfoList.Add(tempList);
                    }
                }
            }
            return excelInfoList;
        }
        /// <summary>
        /// ReadExcel with the excelLocation, only can read the first worksheet,and it will ignore the first row, need Plugins: EPPlus, return is [List[List[string]]]
        /// </summary>
        /// <param name="excelLocation"></param>
        /// <returns></returns>
        public static List<List<string>> ReadExcelFromSecondRow(string excelLocation)
        {
            List<List<string>> excelInfoList = new List<List<string>>();
            using (var package = new ExcelPackage(new FileInfo(excelLocation)))
            {
                ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
                using (ExcelWorksheet worksheet = package.Workbook.Worksheets[0])
                {
                    int rowCount = worksheet.Dimension.Rows;
                    int columnCount = worksheet.Dimension.Columns;
                    for (int row = 2; row <= rowCount; row++)
                    {
                        List<string> tempList = new List<string>();
                        for (int column = 1; column <= columnCount; column++)
                        {
                            tempList.Add(worksheet.Cells[row, column].Value.ToString());
                        }
                        excelInfoList.Add(tempList);
                    }
                }
            }
            return excelInfoList;
        }
    }
}
