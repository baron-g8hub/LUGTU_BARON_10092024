using CsvHelper;
using CsvHelper.TypeConversion;
using FileProcessorAPI.Models;
using System.Globalization;
using System.Text;

namespace FileProcessorAPI.DataProcess
{
    public class UploadFileHandler
    {
        public List<SALE> result = [];

        #region Upload CSV File
        public string UploadFile(IFormFile file)
        {
            try
            {
                //extension
                List<string> validExtensions = new List<string>() { ".csv" };
                string extension = Path.GetExtension(file.FileName);
                if (!validExtensions.Contains(extension))
                {
                    return $"Extension is a .csv file({string.Join(',', validExtensions)})";
                }

                //file name
                // string fileName = Guid.NewGuid().ToString() + extension;
                string fileName = file.FileName;
                string path = Path.Combine(Directory.GetCurrentDirectory(), "UploadedFiles");
                using FileStream stream = new FileStream(Path.Combine(path, fileName), FileMode.Create);
                file.CopyTo(stream);
                stream.Dispose();
                stream.Close();

                // Log this event
                return fileName;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        #endregion

        #region Read CSV by Filename
        public List<SALE> ReadSalesCSV(string fileName)
        {
            string path = Path.Combine(Directory.GetCurrentDirectory(), "UploadedFiles", fileName);
            try
            {
                using (var reader = new StreamReader(path))
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    csv.Read();
                    csv.ReadHeader();
                    while (csv.Read())
                    {
                        var sale = csv.GetRecord<SALE>();
                        result.Add(sale);
                    }
                }
                return result;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        #endregion

        #region Write CSV
        public void WriteSalesCSV(string path, List<SALE> sales, string fileName)
        {
            try
            {
                using (var write = new StreamWriter(path + fileName))
                using (var csv = new CsvWriter(write, CultureInfo.InvariantCulture))
                {
                    csv.WriteRecords(sales);
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        #endregion

        #region Read CSV with Filter
        public SALE ReadCsvFile(Stream fileStream)
        {
            var ret = new SALE();
            try
            {
                using (var reader = new StreamReader(fileStream))
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    csv.Read();
                    csv.ReadHeader();
                    while (csv.Read())
                    {
                        ret = csv.GetRecord<SALE>();
                    }
                    return ret;
                }
            }
            catch (HeaderValidationException ex)
            {
                // Specific exception for header issues
                throw new ApplicationException("CSV file header is invalid.", ex);
            }
            catch (TypeConverterException ex)
            {
                // Specific exception for type conversion issues
                throw new ApplicationException("CSV file contains invalid data format.", ex);
            }
            catch (Exception ex)
            {
                // General exception for other issues
                throw new ApplicationException("Error reading CSV file", ex);
            }
        }
        #endregion

        public List<SALE> GetSalesByFilter(string filterType, string filter)
        {
            var list = new List<SALE>();
            try
            {
                string path = Path.Combine(Directory.GetCurrentDirectory(), "UploadedFiles");
                var files = Directory.EnumerateFiles(path, "*.csv");
                foreach (string file in files)
                {
                    using (FileStream fs = new FileStream(file, FileMode.Open, FileAccess.Read))
                    {
                        // Use the file stream to read data.
                        var sale = ReadCsvFile(fs);
                        switch (filterType.ToUpper())
                        {
                            case "BRANCH":
                                if (sale.Branch == filter)
                                {
                                    list.Add(sale);
                                }
                                break;

                            case "DAY":
                                if (sale.Date.Day.ToString() == filter)
                                {
                                    list.Add(sale);
                                }
                                break;

                            case "MONTH":
                                if (sale.Date.Month.ToString() == filter)
                                {
                                    list.Add(sale);
                                }
                                break;

                            case "YEAR":
                                if (sale.Date.Year.ToString() == filter)
                                {
                                    list.Add(sale);
                                }
                                break;

                            default:
                                break;
                        }
                    }
                }
                return list;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
