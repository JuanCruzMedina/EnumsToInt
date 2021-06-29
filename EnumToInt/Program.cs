using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnumToInt
{
    static class Program
    {
        static void Main(string[] args)
        {
            var bd = new int[] { 1 };

            List<Fruits> listOfEnums = new List<Fruits>() { Fruits.Apple, Fruits.Blueberry, Fruits.Lemon };

            Console.WriteLine("Values to process...");

            foreach (var item in listOfEnums)
                Console.WriteLine("\t+ " + item);

            Console.WriteLine("\nProcessing List...");

            var number = EnumsToInt<Fruits>(listOfEnums);
            Console.WriteLine("\t + Int Value: " + number);

            Console.WriteLine("\nProcessed Values...");

            foreach (var item in IntToEnums<Fruits>(number))
                Console.WriteLine("\t+ " + item);

            Console.ReadKey();
        }
        public static int EnumsToInt<Enum>(List<Enum> listOfEnums)
        {
            var FruitsArray = new int[typeof(Enum).GetEnumValues().Length];

            listOfEnums.ForEach(x =>
            {
                int index = (int)(object)x;
                FruitsArray[index] = 1;
            });

            long longValue = long.Parse(string.Join("", FruitsArray));

            return BinaryToDecimal(longValue);
        }
        public static List<Enum> IntToEnums<Enum>(int number)
        {
            var listWithResults = new List<Enum>();
            var binaryValueArray = Convert.ToString(number, 2).ToCharArray();

            for (int i = 0; i < binaryValueArray.Length; i++)
                if (binaryValueArray[i] == '1')
                    listWithResults.Add(ToEnum<Enum>(i));

            return listWithResults;
        }

        #region Addional
        public static int BinaryToDecimal(long binary)
        {

            int number = 0;
            int digit;
            const int DIVISOR = 10;

            for (long i = binary, j = 0; i > 0; i /= DIVISOR, j++)
            {
                digit = (int)i % DIVISOR;
                if (digit != 1 && digit != 0)
                {
                    return -1;
                }
                number += digit * (int)Math.Pow(2, j);
            }

            return number;
        }

        /// <summary>
        /// Extension method to return an enum value of type T for the given string.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static T ToEnum<T>(this string value)
        {
            return (T)Enum.Parse(typeof(T), value, true);
        }

        /// <summary>
        /// Extension method to return an enum value of type T for the given int.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static T ToEnum<T>(this int value)
        {
            var name = Enum.GetName(typeof(T), value);
            return ToEnum<T>(name);
        }
        #endregion

        enum Fruits : int
        {
            Apple,
            Banana,
            Blueberry,
            Cherry,
            Lemon,
            Watermelon
        }
    }
}
