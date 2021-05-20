using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using INDIAN_STATE_CENSUS_ANALYZER.DTO;
using INDIAN_STATE_CENSUS_ANALYZER.POCO;

namespace INDIAN_STATE_CENSUS_ANALYZER
{
    public class CensusAdapter
    {
        /// <summary>
        /// Gets the census data.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="CensusAnalyserException">
        /// File not found or Invalid file type or Incorrect header in data
        /// </exception>
        public string[] GetCensusData(string csvFilePath, string dataHeaders)
        {
            string[] censusData;
            if (!File.Exists(csvFilePath))
            {
                throw new CensusAnalyserException(CensusAnalyserException.ExceptionType.FILE_NOT_FOUND, "File not found");

            }
            if (Path.GetExtension(csvFilePath) != ".csv")
            {
                throw new CensusAnalyserException(CensusAnalyserException.ExceptionType.INVALID_FILE_TYPE, "Invalid file type");
            }
            censusData = File.ReadAllLines(csvFilePath);
            if (censusData[0] != dataHeaders)
            {
                throw new CensusAnalyserException(CensusAnalyserException.ExceptionType.INCORRECT_HEADER, "Incorrect header in data");
            }
            return censusData;
        }
    }

    public class CensusAnalyserException : Exception
    {
        /// <summary>
        /// This enum ExceptionType for diffrent exception which is constant
        /// </summary>
        public enum ExceptionType
        {
            FILE_NOT_FOUND, INVALID_FILE_TYPE, INCORRECT_HEADER, INCORRECT_DELIMITER, NO_SUCH_COUNTRY
        }

        public ExceptionType exceptionType;

        /// <summary>
        /// This Parametrized Constructor Initialize a new instance of this class.
        /// </summary>

        public CensusAnalyserException(ExceptionType exceptionType, string message) : base(message)
        {
            this.exceptionType = exceptionType;
        }
    }

    public class CensusAnalyser
    {
        /// <summary>
        /// enum Country Constant for diffrent country.
        /// </summary>
        public enum Country
        {
            INDIA, RUSSIA, CANADA 
        }

        Dictionary<string, CensusDTO> dataMap;

        /// <summary>
        /// Loads the census data.
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, CensusDTO> LoadCensusData(Country country, string csvFilePath, string dataHeaders)
        {
            dataMap = new CSVAdapterFactory().LoadCsvData(country, csvFilePath, dataHeaders);
            return dataMap;
        }
    }
    public class CSVAdapterFactory
    {
        /// <summary>
        /// Loads the CSV data.
        /// </summary>
        /// <param name="country">The country.</param>
        /// <param name="csvFilePath">The CSV file path.</param>
        /// <param name="dataHeaders">The data headers.</param>
        /// <returns></returns>
        /// <exception cref="Indian_StatesCensusAnalyser.CensusAnalyserException">No such country</exception>
        public Dictionary<string, CensusDTO> LoadCsvData(CensusAnalyser.Country country, string csvFilePath, string dataHeaders)
        {
            switch (country)
            {
                case (CensusAnalyser.Country.INDIA):
                    return new IndianCensusAdapter().LoadCensusData(csvFilePath, dataHeaders);
                default:
                    throw new CensusAnalyserException(CensusAnalyserException.ExceptionType.NO_SUCH_COUNTRY, "No such country");
            }
        }
    }
    public class IndianCensusAdapter : CensusAdapter
    {
        string[] censusData;
        Dictionary<string, CensusDTO> dataMap;

        /// <summary>
        /// Loads the census data.
        /// </summary>

        public Dictionary<string, CensusDTO> LoadCensusData(string csvFilePath, string dataHeader)
        {
            dataMap = new Dictionary<string, CensusDTO>();
            censusData = GetCensusData(csvFilePath, dataHeader);
            foreach (string data in censusData.Skip(1))
            {
                if (!data.Contains(","))
                {
                    throw new CensusAnalyserException(CensusAnalyserException.ExceptionType.INCORRECT_DELIMITER, "File contains wrong delemiter");
                }
                string[] column = data.Split(',');
                if (csvFilePath.Contains("IndianStateCode.csv"))
                {
                    dataMap.Add(column[1], new CensusDTO(new StateCodeDAO(column[0], column[1], column[2], column[3])));
                }
                if (csvFilePath.Contains("IndianStateCensusData.csv"))
                {
                    dataMap.Add(column[0], new CensusDTO(new CensusDataDAO(column[0], column[1], column[2], column[3])));
                }
            }
            return dataMap.ToDictionary(p => p.Key, p => p.Value);
           
        }
        Console.ReadLine();
    }
  

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("**************Welcome To Indian State Census Analyser******************");
            Console.Read();
        }
    }
}


