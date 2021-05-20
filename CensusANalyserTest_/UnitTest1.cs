using INDIAN_STATE_CENSUS_ANALYZER;
using INDIAN_STATE_CENSUS_ANALYZER.DTO;
using NUnit.Framework;
using System.Collections.Generic;
using static INDIAN_STATE_CENSUS_ANALYZER.CensusAnalyser;

namespace CensusANalyserTest_
{
    public class TestClass
    {
        static string indianStateCensusHeaders = "﻿State,Population,AreaInSqKm,DensityPerSqKm";
        static string wrongStateCensusHeaders = "﻿DensityPerSqKm,Population,AreaInSqKm,State";
        static string indianStateCensusFilePath = @"C:\Users\kholi\source\repos\INDIAN_STATE_CENSUS_ANALYZER\INDIAN_STATE_CENSUS_ANALYZER\CSVfiles\IndiaStateCensusData.csv";
        static string wrongIndianStateCensusFilePath = @"C:\Users\kholi\source\repos\INDIAN_STATE_CENSUS_ANALYZER\INDIAN_STATE_CENSUS_ANALYZER\CSVfiles\WrongIndiaStateCensusData.csv";
        static string wrongIndianStateCensusFileType = @"C:\Users\kholi\source\repos\INDIAN_STATE_CENSUS_ANALYZER\INDIAN_STATE_CENSUS_ANALYZER\CSVfiles\IndiaStateCode.txt";
        static string delimiterIndianCensusData = @"C:\Users\kholi\source\repos\INDIAN_STATE_CENSUS_ANALYZER\INDIAN_STATE_CENSUS_ANALYZER\CSVfiles\DelimiterIndiaStateCode.csv";
        static string headerIndianStateCensusData = @"C:\Users\kholi\source\repos\INDIAN_STATE_CENSUS_ANALYZER\INDIAN_STATE_CENSUS_ANALYZER\CSVfiles\IndiaStateCensusData.csv";

        static string indianStateCodeHeader = "﻿SrNo,State Name,TIN,StateCode";
        static string indianStateCodeFilePath = @"C:\Users\kholi\source\repos\INDIAN_STATE_CENSUS_ANALYZER\INDIAN_STATE_CENSUS_ANALYZER\CSVfiles\IndiaStateCode.csv";
        static string wrongIndianStateCodeFilePath = @"C:\Users\kholi\source\repos\INDIAN_STATE_CENSUS_ANALYZER\INDIAN_STATE_CENSUS_ANALYZER\CSVfiles\WrongIndiaStateCensusData.csv";
        static string delimiterIndianStateCodeFile = @"C:\Users\kholi\source\repos\INDIAN_STATE_CENSUS_ANALYZER\INDIAN_STATE_CENSUS_ANALYZER\CSVfiles\DelimiterIndiaStateCode.csv";
        CensusAnalyser censusAnalyser;
        Dictionary<string, CensusDTO> totalRecord;
        Dictionary<string, CensusDTO> stateRecord;

        [SetUp]
        public void Setup()
        {
            censusAnalyser = new CensusAnalyser();
            totalRecord = new Dictionary<string, CensusDTO>();
            stateRecord = new Dictionary<string, CensusDTO>();
        }

        /// <summary>
        /// Test Case 1.1 Given the indian census data file when reader should return census data count.
        /// </summary>
        [Test]
        public void GivenIndianCensusDataFile_WhenReade_ThenShouldReturnCensusDataCount()
        {
            totalRecord = censusAnalyser.LoadCensusData(Country.INDIA, indianStateCensusFilePath, indianStateCensusHeaders);
            Assert.AreEqual(29, totalRecord.Count);
        }

        /// <summary>
        /// Test Case 1.2 Given the indian census data file when incorrect then return File not found exception.
        /// </summary>
        [Test]
        public void GivenIndianCensusDataFile_WhenIncorrect_ThenShouldReturnFileNotFoundException()
        {
            var censusException = Assert.Throws<CensusAnalyserException>(() => censusAnalyser.LoadCensusData(Country.INDIA, wrongIndianStateCensusFilePath, indianStateCensusHeaders));
            Assert.AreEqual(CensusAnalyserException.ExceptionType.FILE_NOT_FOUND, censusException.exceptionType);
        }

        /// <summary>
        /// Test Case 1.3 Given the indian census data csv file when correct but type incoorect then return invalid file type exception.
        /// </summary>
        [Test]
        public void GivenIndianCensusDataFile_WhenCorrect_ThenShouldReturnInvalidFileTypeException()
        {
            var censusException = Assert.Throws<CensusAnalyserException>(() => censusAnalyser.LoadCensusData(Country.INDIA, wrongIndianStateCensusFileType, indianStateCensusHeaders));
            Assert.AreEqual(CensusAnalyserException.ExceptionType.INVALID_FILE_TYPE, censusException.exceptionType);
        }

        /// <summary>
        /// Test Case 1.4 Given the indian census data csv file when correct but delimiter incoorect then return incorrect delimiter exception.
        /// </summary>
        [Test]
        public void GivenIndianCensusDataFileCorrect_WhenDelimiterIncorrect_ThenShouldReturnInvalidDelimiterException()
        {
            var censusException = Assert.Throws<CensusAnalyserException>(() => censusAnalyser.LoadCensusData(Country.INDIA, delimiterIndianCensusData, indianStateCensusHeaders));
            Assert.AreEqual(CensusAnalyserException.ExceptionType.INCORRECT_DELIMITER, censusException.exceptionType);
        }

        /// <summary>
        /// Test Case 1.5 Given the indian census data csv file when correct but header incoorect then return incorrect delimiter exception.
        /// </summary>
        [Test]
        public void GivenIndianCensusDataFileCorrect_WhenHeaderIncorrect_ThenShouldReturnInvalidHeaderException()
        {
            var censusException = Assert.Throws<CensusAnalyserException>(() => censusAnalyser.LoadCensusData(Country.INDIA, headerIndianStateCensusData, wrongStateCensusHeaders));
            Assert.AreEqual(CensusAnalyserException.ExceptionType.INCORRECT_HEADER, censusException.exceptionType);
        }

        /// <summary>
        /// Test Case 2.1 Given the indian state code file when reader should return state code data count.
        /// </summary>
        [Test]
        public void GivenIndianStateCodeCsvFile_WhenRead_ThenShouldReturnStateCodeDataCount()
        {
            stateRecord = censusAnalyser.LoadCensusData(Country.INDIA, indianStateCodeFilePath, indianStateCodeHeader);
            Assert.AreEqual(37, stateRecord.Count);
        }

        /// <summary>
        /// Test Case 2.2 Given the indian state code file when incorrect then return File not found exception.
        /// </summary>
        [Test]
        public void GivenIndianStateCodeCsvFile_WhenIncorrect_ThenShouldReturnFileNotFoundException()
        {
            var censusException = Assert.Throws<CensusAnalyserException>(() => censusAnalyser.LoadCensusData(Country.INDIA, wrongIndianStateCodeFilePath, indianStateCodeHeader));
            Assert.AreEqual(CensusAnalyserException.ExceptionType.FILE_NOT_FOUND, censusException.exceptionType);
        }

        /// <summary>
        /// Test Case 2.3 Given the indian census data csv file when correct but type incoorect then return invalid file type exception.
        /// </summary>
        [Test]
        public void GivenIndianStateCodCsvFileCorrect_WhenFileTypeIncorrect_ThenShouldReturnInvalidFileTypeException()
        {
            var censusException = Assert.Throws<CensusAnalyserException>(() => censusAnalyser.LoadCensusData(Country.INDIA, wrongIndianStateCensusFileType, indianStateCodeHeader));
            Assert.AreEqual(CensusAnalyserException.ExceptionType.INVALID_FILE_TYPE, censusException.exceptionType);
        }

        /// <summary>
        /// Test Case 2.4 Given the indian state code csv file when correct but delimiter incoorect then return incorrect delimiter exception.
        /// </summary>
        [Test]
        public void GivenIndianStateCodeCsvFileCorrect_WhenDelimiterIncorrect_ThenShouldReturnInvalidDelimiterException()
        {
            var censusException = Assert.Throws<CensusAnalyserException>(() => censusAnalyser.LoadCensusData(Country.INDIA, delimiterIndianStateCodeFile, indianStateCodeHeader));
            Assert.AreEqual(CensusAnalyserException.ExceptionType.INCORRECT_DELIMITER, censusException.exceptionType);
        }
    }
   
}