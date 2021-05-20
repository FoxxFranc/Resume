using System;
using System.Collections.Generic;
using System.Linq;

/*
У вас есть список больных(минимум 10 записей)
Класс больного состоит из полей: ФИО, возраст, заболевание.
Требуется написать программу больницы, в которой перед пользователем будет меню со следующими пунктами:
1)Отсортировать всех больных по фио
2)Отсортировать всех больных по возрасту
3)Вывести больных с определенным заболеванием
 */

namespace HospitalLINQ
{
    namespace Hospital
    {
        class Program
        {
            static void Main(string[] args)
            {
                bool isWorking = true;
                DataBase patientsDataBase = new DataBase();
                string neededDiseade;

                while (isWorking)
                {
                    Console.WriteLine("Введите команду: 1 - вывести всех больных; 2 - вывести список больных отсортированный по ФИО; 3 - вывести список больных отсортированный по возрасту: 4 - вывести больных с конкретным забболеванием на экран; esc - выйти");
                    switch (Console.ReadLine())
                    {
                        case "1":
                            patientsDataBase.ShowAllPatients();
                            break;
                        case "2":
                            patientsDataBase.ShowSortByName();
                            break;
                        case "3":
                            patientsDataBase.ShowSortByAge();
                            break;
                        case "4":
                            Console.Write("Введите болезнь - ");
                            neededDiseade = Console.ReadLine();
                            patientsDataBase.ShowPatientsWith(neededDiseade);
                            break;
                        case "esc":
                            break;
                    }
                    Console.ReadKey();
                    Console.Clear();
                }
            }
        }

        class DataBase
        {
            private List<Patient> _allPatients;

            public DataBase()
            {
                _allPatients = new List<Patient>();

                _allPatients.Add(new Patient("Иван", 20, "Простуда"));
                _allPatients.Add(new Patient("Андрей", 19, "Биполярное расстройство"));
                _allPatients.Add(new Patient("Николай", 24, "Биполярное расстройство"));
                _allPatients.Add(new Patient("Виктор", 17, "COVID-19"));
                _allPatients.Add(new Patient("Илья", 21, "Простуда"));
                _allPatients.Add(new Patient("Константин", 22, "Простуда"));
                _allPatients.Add(new Patient("Милла", 18, "COVID-19"));
                _allPatients.Add(new Patient("Тамара", 24, "БАР"));
                _allPatients.Add(new Patient("Семен", 23, "БАР"));
                _allPatients.Add(new Patient("Пахтеев", 24, "Простуда"));
            }

            public void ShowAllPatients()
            {
                Console.WriteLine("Пациенты, записанные на прием");
                ShowAll(_allPatients);
            }

            public void ShowSortByName()
            {
                var sortedPatients = _allPatients.OrderBy(patient => patient.Name).ToList();
                Console.WriteLine("Пациенты отсортированы по имени:");
                ShowAll(sortedPatients);
            }

            public void ShowSortByAge()
            {
                var sortedPatients = _allPatients.OrderBy(patient => patient.Age).ToList();
                Console.WriteLine("Пациенты отсортрованные по возрасту");
                ShowAll(sortedPatients);
            }


            private void ShowAll(List<Patient> patients)
            {
                foreach (var patient in patients)
                {
                    patient.ShowInfo();
                }
            }

            public void ShowPatientsWith(string disease)
            {
                var nededPatients = _allPatients.Where(patient => patient.Disease.Equals(disease)).ToList();
                if (nededPatients.Count > 0)
                {
                    nededPatients = nededPatients.ToList();
                    ShowAll(nededPatients);
                }
                else
                {
                    Console.WriteLine("Пациентов с такой болезнью нет");
                }
            }
        }

        class Patient
        {
            public string Name { get; private set; }
            public int Age { get; private set; }
            public string Disease { get; private set; }

            public Patient(string name, int age, string disease)
            {
                Name = name;
                Age = age;
                Disease = disease;
            }

            public void ShowInfo()
            {
                Console.WriteLine($"Больной - {Name}, возраст - {Age}, заболевание - {Disease}");
            }
        }
    }
