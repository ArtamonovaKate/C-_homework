using System;
using System.Globalization;
using System.IO;
using System.Security.Cryptography.X509Certificates;

namespace LocationCoordinates
{
    /// <summary>
    /// Geographic coordinate processing program
    /// </summary>
    class Program
    {
        /// <summary>
        /// Declaration of variables for the method of entering coordinates, the question of additional input of coordinates, read and output coordinates x and y
        /// </summary>
        static string dop_vvod, command_from;
        static string coor_x, coor_y;
        static decimal x, y;

        static void Main(string[] args)
        {
            /// <summary> A method call that asks for a way to enter coordinates </summary>
            Question_From();
        }

        static void Question_From()
        {/// <summary> Method that asks for a way to enter coordinates </summary>
            Console.WriteLine("Вы хотите ввести координаты или считать их из файла? (1-ввод; 2-файл)");
            command_from = Console.ReadLine();
            Coordinates_From();
        }

        static void Coordinates_From()
        { /// <summary> Method that parses the response and determines how coordinates are entered</summary>
            if (command_from == "1")
            {
                Console.WriteLine("Введем координаты местоположения объекта");
                CoordinateX();
            }

            else
                if (command_from == "2")
            {
                Coordinates_InFile();
            }

            else
            {
                Console.WriteLine("Некорректный ввод!");
                Question_From();
            }
        }

        static void CoordinateX()
        {///<summary> Method that reads the X coordinate from the console window</summary>
            Console.WriteLine("Введите координату X:");
            coor_x = Console.ReadLine();
            coor_x = coor_x.Trim();
            coor_x = coor_x.Replace(' ', ',').Replace('.', ',').Replace('/', ',').Replace(':', ',').Replace(';', ',').Replace('\'', ',').Replace('+', ',');

            CheckX();
        }

        static void CoordinateY()
        {///<summary> Method that reads the Y coordinate from the console window</summary>
            Console.WriteLine("Введите координату Y:");
            coor_y = Console.ReadLine();
            coor_y = coor_y.Trim();
            coor_y = coor_y.Replace(' ', ',').Replace('.', ',').Replace('/', ',').Replace(':', ',').Replace(';', ',').Replace('\'', ',').Replace('+', ',');

            CheckY();

        }

        static void Coordinates_InFile()
        {///<summary> Method that reads coordinates from file</summary>
            string line;
            try
            {
                StreamReader sr = new StreamReader("..\\..\\..\\input_coordinates.txt");
                line = sr.ReadLine();
                coor_x = line;
                while (line != null)
                {
                    line = sr.ReadLine();
                    coor_y = line;
                }
                sr.Close();
                Console.ReadLine();
                
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
            finally
            {
                CheckX();
            }
            
        }

        static void CheckX()
        {///<summary> Method that checks the correctness of the received X coordinate and formats it</summary>
            NumberStyles style;
            CultureInfo culture;
            bool correct = true;

            if (coor_x.Length > 11 | coor_x.Length < 1)
            {
                Console.WriteLine("Некорректный ввод, значение отсутствует или оно слишком длинное!");
                correct = false;
            }

            style = NumberStyles.Number | NumberStyles.AllowCurrencySymbol;
            culture = CultureInfo.CreateSpecificCulture("ru-RU");
            if (Decimal.TryParse(coor_x, style, culture, out x))
                correct = true;
            else
            {
                Console.WriteLine("Некорректный ввод, недопустимы буквы и лишние символы!");
                correct = false;
            }

            if (correct == false)
            {
                if (command_from == "1")
                {
                    CoordinateX();
                }

                else if (command_from == "2")
                {
                    Console.WriteLine("Перепишите координату X в файле");
                    Question_From();
                }
            }

            if (correct == true)
            {
                if (command_from == "1")
                {
                    CoordinateY();
                }

                else if (command_from == "2")
                {
                    CheckY();
                }
            }
            
        }

        static void CheckY()
        {///<summary> Method that checks the correctness of the received Y coordinate and formats it</summary>
            NumberStyles style;
            CultureInfo culture;
            bool correct = true;

            if (coor_y.Length > 11 | coor_y.Length < 1)
            {
                Console.WriteLine("Некорректный ввод, значение отсутствует или оно слишком длинное!");
                correct = false;
            }

            style = NumberStyles.Number | NumberStyles.AllowCurrencySymbol;
            culture = CultureInfo.CreateSpecificCulture("ru-RU");
            if (Decimal.TryParse(coor_y, style, culture, out y))
                correct = true;
            else
            {
                Console.WriteLine("Некорректный ввод, недопустимы буквы и лишние символы!");
                correct = false;
            }

            if (correct == false)
            {
                if (command_from == "1")
                {
                    CoordinateY();
                }

                else if (command_from == "2")
                {
                    Console.WriteLine("Перепишите координату Y в файле");
                    Question_From();
                }
            }

            if (correct == true)
            {
                    Question_New_Coordinates();
            }
        }

        static void Question_New_Coordinates()
        {///<summary> A method that prints the processed coordinates to the screen and asks the user for new input</summary>
            Console.WriteLine("Введенные Вами координаты: X: " + x + " " + "Y: " + y);
            Console.WriteLine("Хотите ли Вы ввести новые координаты? (да или нет)");
            dop_vvod = Console.ReadLine();
            dop_vvod.ToLower();
            Result_Question();
        }

        static void Result_Question()
        {///<summary> Method that handles the user's new input response</summary>

            if (dop_vvod == "да")
            {
                Question_From();
            }

            else
                if (dop_vvod == "нет")
            {
                Console.WriteLine("Спасибо, ввод окончен.");
            }

            else
            {
                Console.WriteLine("Некорректный ввод!");
                Question_New_Coordinates();
            }
        }
    }
}
