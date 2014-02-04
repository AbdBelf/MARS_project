using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace MARS_Expert.Manager.ExpertManager
{
    class Check
    {
        public static bool IsWord(string input)
        {
            return Regex.IsMatch(input, "^[a-zA-Z ]+$");
        }

        public static bool IsEmailAddress(string input)
        {
            Regex regex = new Regex(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$");
            return regex.IsMatch(input);
        }

        public static bool IsAlphaNumeric(string input)
        {
            return Regex.IsMatch(input, "^[0-9a-zA-Z]+$");
        }

        /// <summary>
        /// check if the input string is a valid alegrian phone number.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool IsPhoneNumber(string input)
        {
            //"^0[0-9]{8}$"  //Land Phone Number
            //"^05[0-9]{8}$" //Nedjma Phone Number
            //"^06[0-9]{8}$" //Mobilis Phone Number
            //"^07[0-9]{8}$" //Djezzy Phone Number

            //Generic phone number
            return Regex.IsMatch(input, "^0[0-9]{9,10}$");
        }

        /// <summary>
        /// Check if the input string contains any special character (arithmetic and logical operators, separators..).
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool ContainsSpecialCharacter(string input)
        {
            return Regex.IsMatch(input, @"[-=+*\/^%\(\)|&!<>\[\]{}@#?;:]");
        }

        /// <summary>
        /// Check if the input string contains a backslash(\), a quote(') or a double quote(") character.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool ContainsInvalidChar(string input)
        {
            return Regex.IsMatch(input, @"[\\']") || Regex.IsMatch(input, "[\"]");
        }
    }
}
