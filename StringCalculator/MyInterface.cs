using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.Serialization.Json;

namespace SocialNetwork {
    public class Calculator
    {
        public static int Add(string input)
        {
            string delimiter = GetDelimiter(input);
            string numbers = RemoveDelimiterFromString(input);
            string exception = GetSomeException(input, delimiter, numbers);
            if (exception.Equals("0")) return 0;
            return ExecuteAddOperation(numbers,delimiter);
        }

        private static string GetSomeException(string input, string delimiter, string numbers)
        {
            if (IsInputStringNullOrEmpty(input)) return "0";
            IsAnyNegativeNumber(numbers, delimiter);
            return "";
        }

        private static void IsAnyNegativeNumber(string numbers, string delimiter)
        {
            int[] numbersArray = ConvertStringToIntArray(numbers, delimiter);
            int[] negativeNumbers = GetNegativeNumbers(numbersArray);
            if(negativeNumbers.Length > 0) GetExceptionWithNegativeNumbers(negativeNumbers);
        }

        private static bool IsInputStringNullOrEmpty(string input)
        {
            return string.IsNullOrEmpty(input);
        }

        private static int ExecuteAddOperation(string numbers, string delimiter)
        {
            int[] numbersArray = ConvertStringToIntArray(numbers, delimiter);
            int result = 0;
            foreach (var num in numbersArray)
            {
                result += num > 1000 ? 0 : num;
            }
            return result;
        }

        private static bool ContainsNegativeNumbers(int [] numbersInput)
        {
            foreach (var number in numbersInput)
            {
                if (number < 0) return true;
            }
            return false;
        }

        private static void GetExceptionWithNegativeNumbers(int[] negativeNumbers)
        {
            if (negativeNumbers.Length > 0)
            {
                string numbers = "";
                foreach (var num in negativeNumbers)
                {
                    numbers += num + ",";
                }
                throw new Exception($"negative numbers not allowed: {numbers.Substring(0, numbers.Length-1)}");
            }
        }

        private static int [] GetNegativeNumbers(int [] numbers)
        {
            List<int> negativeNumbersList = new List<int>();
            foreach (var number in numbers)
            {
                if(number < 0) negativeNumbersList.Add(number);
            }
            return negativeNumbersList.ToArray();
        }

        private static string RemoveDelimiterFromString(string input)
        {
            return IsDelimiterConfigured(input) ? input.Substring(input.IndexOf("\n") + 1) : input;
        }

        private static string GetDelimiter(string input)
        {
            return IsDelimiterConfigured(input)
                ? input.Substring(input.IndexOf("/", 1) + 1, input.IndexOf("\n") - 1).Trim()
                : ",";

        }

        private static bool IsDelimiterConfigured(string input)
        {
            return input.StartsWith("//");
        }

        private static int[] ConvertStringToIntArray(string input, string delimiter)
        {
            input = input.Replace("\n", delimiter);
            string[] numbers = input.Split(delimiter);
            return Array.ConvertAll(numbers, number => int.Parse(number));
        }
    }
}