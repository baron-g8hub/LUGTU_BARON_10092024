namespace FileProcessorAPI.DataProcess
{
    public static class Utility
    {
        #region SelfCleaningLogging
        public static void FileLogging(int logtype, string baseFolder, string baseFile, string dataIn)
        {
            //logtype 1=forever, 2=hourly, 3=Day,
            string dataOut = DateTime.Now.ToString() + " " + dataIn + System.Environment.NewLine;
            string fileOut;
            try
            {
                if (logtype == 1)
                {
                    fileOut = baseFolder + @"\" + baseFile + ".txt";
                    System.IO.File.AppendAllText(fileOut, dataOut);
                }
                if (logtype == 2)
                {
                    fileOut = baseFolder + @"\" + baseFile + "_HOUR_" + DateTime.Now.Hour.ToString() + ".txt";

                    if (System.IO.File.Exists(fileOut))
                    {
                        if ((DateTime.Now - System.IO.File.GetLastWriteTime(fileOut)).TotalHours > 20)
                        {
                            System.IO.File.Delete(fileOut);
                        }
                    }
                    System.IO.File.AppendAllText(fileOut, dataOut);
                }
                if (logtype == 3)
                {
                    fileOut = baseFolder + @"\" + baseFile + "_DAY_" + DateTime.Now.Day.ToString() + ".txt";

                    if (System.IO.File.Exists(fileOut))
                    {
                        if ((DateTime.Now - System.IO.File.GetLastWriteTime(fileOut)).TotalDays > 2)
                        {
                            System.IO.File.Delete(fileOut);
                        }
                    }
                    System.IO.File.AppendAllText(fileOut, dataOut);
                }
            }
            catch (Exception)
            {
                //ignore Error
            }
        }
        #endregion
    }
}
