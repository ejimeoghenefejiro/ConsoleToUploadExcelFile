using Excel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SarahGeoDataUpload
{
    class Program
    {
        static void Main(string[] args)
        {
            string excelFile = @"C:\Users\Cyberspace\Desktop\Files\physfact_rev(1).xlsx";

            FileStream stream = new FileStream(excelFile, FileMode.Open);  // Using FileStream as specified  in your file
            IExcelDataReader excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream); //converting the fileStream to ExcelDataReader
            DataSet result = excelReader.AsDataSet();
            using (var db = new DB_GeneralGeographyEntities())
            {
                /* DataClasses1DataContext conn = new DataClasses1DataContext();*/ //Database Connection (DBContext)
                foreach (DataTable table in result.Tables)
                {
                    DataRow row = table.Rows[0];  //Remove header row at the point of loop through
                    table.Rows.Remove(row);
                    foreach (DataRow dr in table.Rows)  //loop through all the row in the excel sheet
                    {
                        physfact_rev addTable = new physfact_rev()
                        {
                            //All the row in the excel show must be matched 
                            Id = Convert.ToInt32(dr[0]),
                            wbcode = Convert.ToString(dr[1]),
                            country = Convert.ToString(dr[2]),
                            areakm2 = Convert.ToString(dr[3]),
                            cen_lat = Convert.ToString(dr[4]),
                            cen_lon = Convert.ToString(dr[5]),
                            elev = Convert.ToString(dr[6]),
                            distcr = Convert.ToString(dr[7]),
                            distc = Convert.ToString(dr[8]),
                            distr = Convert.ToString(dr[9]),
                            tropicar = Convert.ToString(dr[10]),
                            troppop = Convert.ToString(dr[11]),
                            lc100km = Convert.ToString(dr[12]),
                            lcr100km = Convert.ToString(dr[13]),
                            pop95 = Convert.ToString(dr[14]),
                            pdenpavg = Convert.ToString(dr[15]),
                            pop100km = Convert.ToString(dr[16]),
                            pop100cr = Convert.ToString(dr[17]),
                            cen_c = Convert.ToString(dr[18]),
                            cen_cr = Convert.ToString(dr[19])
                        };
                        db.physfact_rev.Add(addTable);
                    }
                }
                db.SaveChanges();
            }
            excelReader.Close();
            stream.Close();

            Console.WriteLine("Your Excel file have been exported to the SQL Server Database Successful by the Csharp(C#) program\n\n");
            Console.WriteLine("press Enter key on your Computer keyboard Twice to exit the C# program");
            Console.ReadLine();
        }
    }
}
