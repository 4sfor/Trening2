using System;
using System.IO;
namespace Trening2
{
    class Program
    {
        class PersonName
        {
            string name;
            public string _Name { get { return name; } }

            public string EntryName()
            {
                Console.Write("Введите ваше ФИО: "); name = Console.ReadLine();
                return name;
            }
            public void NamePrint() { Console.WriteLine("ФИО:" + name); }
        }
        class Password
        {
            Random rnd = new Random();
            public string _Rnd { get { return rnd.ToString(); } }
            public void PasswordPrint() { Console.WriteLine("Ваш временный пароль:" + rnd.Next()); }
        }
        class DateTimePrinter
        {
            public void PrintCurrentDateTime()
            { Console.WriteLine("Дата и время:" + DateTime.Now); }

        }
        class Test
        {
            private string path;
            public string _path { get { return path; } }
            public Test(string path) { this.path = path; }
        }
        class Answer
        {
            private string pathAnswer;
            public string _pathAnswer { get { return pathAnswer; } }
            public Answer(string pathAnswer) { this.pathAnswer = pathAnswer; }

        }
        class TestReader
        {
            Test pathTest;
            Answer pathAnswer;
            public TestReader(Test pathTest, Answer pathAnswer)
            {
                this.pathTest = pathTest;
                this.pathAnswer = pathAnswer;
            }
            float result = 0;
            string finish = "";
            string ent;
            public float _Result { get { return result; } }
            public string _Finish { get { return finish; } }
            public void Content()
            {
                string[] readText = null;
                string[] readTextAnswers = null;

                try
                {
                    readText = File.ReadAllLines(pathTest._path, System.Text.Encoding.UTF8);
                    readTextAnswers = File.ReadAllLines(pathAnswer._pathAnswer, System.Text.Encoding.UTF8);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Ошибка при чтении файла: " + ex.Message);
                    return;
                }
                    EnterAnswer enterAnswer = new EnterAnswer(pathTest);
                Console.WriteLine(readText[0]);
                for (int i = 1; i < readText.Length; i++)
                {
                    Console.WriteLine(readText[i]);
                    ent = enterAnswer.Enter();
                    if (ent=="0")
                    {

                        finish = "Завершен досрочно";
                        break;
                        
                    }

                    if (ent == readTextAnswers[i - 1])
                    {
                        result = result + enterAnswer.RasPercent();

                    }

                }
                Console.WriteLine("Ваш результат: " + result + "%");

            }


        }
        class EnterAnswer
        {

            public float percent;
            Test pathTest;
            public EnterAnswer(Test pathTest) { this.pathTest = pathTest; }


            public string Enter()
            {
                Console.Write("Ответ: ");
                string userEnter = Console.ReadLine().ToLower();
                return userEnter;
            }
            public float RasPercent()
            {
                string[] readText = File.ReadAllLines(pathTest._path, System.Text.Encoding.UTF8);
                percent = 100 / (readText.Length - 1);

                return percent;

            }
            class SaveResult
            {
                public static void SaveTestResult(string fileName, string fullName, float result, string finish)
                {
                    try
                    {
                        using (StreamWriter sw = new StreamWriter(fileName, true))
                        {
                            sw.WriteLine("Дата прохождения: " + DateTime.Now);
                            sw.WriteLine("ФИО: " + fullName);
                            //sw.WriteLine("Временный пароль: " + password);
                            sw.WriteLine("Результат теста: " + result + "%");
                            sw.WriteLine(finish);
                            sw.WriteLine("-----------------------------------");    
                        }
                        Console.WriteLine("Результаты сохранены в файл: " + fileName);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Ошибка при сохранении результатов: " + ex.Message);
                    }
                }


            }



            static void Main(string[] agrs)
            {
                Test testPath = new Test(@"\trening2\trening2\биология\Методы исселдования в биологии.txt");
                Answer answerPath = new Answer(@"\trening2\trening2\биология\ответы.txt");
                PersonName name = new PersonName();
                name.EntryName();
                Console.Clear();
                name.NamePrint();
                Password password = new Password();
                password.PasswordPrint();
                DateTimePrinter dt = new DateTimePrinter();
                dt.PrintCurrentDateTime();
                TestReader reader = new TestReader(testPath, answerPath);
                reader.Content();
                SaveResult.SaveTestResult(@"\trening2\trening2\биология\результаты.txt", name._Name,  reader._Result, reader._Finish);


            }
        }
    }
}
