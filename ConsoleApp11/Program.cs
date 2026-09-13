using System;
using System.Collections.Generic;
using Syhnost; // Подключаем нашу библиотеку

namespace ConsoleApp11
{
    class Program
    {
        static void Main(string[] args)
        {
            Logic logic = new Logic(); // Создаем объект бизнес-логики
            bool exit = false;

            while (!exit)
            {
                Console.Clear();
                Console.WriteLine("=== АПТЕКА (Управление таблетками) ===");
                Console.WriteLine("1. Добавить таблетку");
                Console.WriteLine("2. Показать все таблетки");
                Console.WriteLine("3. Удалить таблетку");
                Console.WriteLine("4. Изменить таблетку");
                Console.WriteLine("5. Показать таблетки по производителю");
                Console.WriteLine("6. Показать только рецептурные таблетки");
                Console.WriteLine("0. Выход");
                Console.Write("Выберите пункт: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        AddPillUI(logic);
                        break;
                    case "2":
                        ShowAllPills(logic);
                        break;
                    case "3":
                        DeletePillUI(logic);
                        break;
                    case "4":
                        UpdatePillUI(logic);
                        break;
                    case "5":
                        SearchByManufacturerUI(logic);
                        break;
                    case "6":
                        ShowPrescriptionPills(logic);
                        break;
                    case "0":
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Неверный ввод. Нажмите любую клавишу...");
                        Console.ReadKey();
                        break;
                }
            }
        }

        // --- Вспомогательные методы для интерфейса ---

        static void AddPillUI(Logic logic)
        {
            Console.WriteLine("\n--- Добавление таблетки ---");
            Console.Write("Название: ");
            string name = Console.ReadLine();
            Console.Write("Производитель: ");
            string manufacturer = Console.ReadLine();
            Console.Write("Цена: ");
            double price = Convert.ToDouble(Console.ReadLine());
            Console.Write("Количество: ");
            int quantity = Convert.ToInt32(Console.ReadLine());
            Console.Write("По рецепту? (да/нет): ");
            bool isPrescription = Console.ReadLine().ToLower() == "да";

            Pill newPill = new Pill(name, manufacturer, price, quantity, isPrescription);
            logic.AddPill(newPill);

            Console.WriteLine("Таблетка успешно добавлена! Нажмите любую клавишу...");
            Console.ReadKey();
        }

        static void ShowAllPills(Logic logic)
        {
            Console.WriteLine("\n--- Список всех таблеток ---");
            List<Pill> pills = logic.GetAllPills();
            if (pills.Count == 0)
            {
                Console.WriteLine("Список пуст.");
            }
            else
            {
                foreach (var pill in pills)
                {
                    Console.WriteLine($"ID: {pill.Id} | {pill.Name} | Производитель: {pill.Manufacturer} | Цена: {pill.Price} | Кол-во: {pill.Quantity} | Рецепт: {(pill.IsPrescription ? "Да" : "Нет")}");
                }
            }
            Console.WriteLine("\nНажмите любую клавишу...");
            Console.ReadKey();
        }

        static void DeletePillUI(Logic logic)
        {
            Console.Write("\nВведите ID таблетки для удаления: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                if (logic.DeletePill(id))
                    Console.WriteLine("Удалено успешно!");
                else
                    Console.WriteLine("Таблетка с таким ID не найдена.");
            }
            else
            {
                Console.WriteLine("Ошибка: нужно ввести число.");
            }
            Console.ReadKey();
        }

        static void UpdatePillUI(Logic logic)
        {
            Console.Write("\nВведите ID таблетки для изменения: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                // Проверяем, существует ли такая таблетка
                var pills = logic.GetAllPills();
                var existing = pills.Find(p => p.Id == id);

                if (existing == null)
                {
                    Console.WriteLine("Таблетка с таким ID не найдена.");
                    Console.ReadKey();
                    return;
                }

                Console.Write("Новое название: ");
                string name = Console.ReadLine();
                Console.Write("Новый производитель: ");
                string manufacturer = Console.ReadLine();
                Console.Write("Новая цена: ");
                double price = Convert.ToDouble(Console.ReadLine());
                Console.Write("Новое количество: ");
                int quantity = Convert.ToInt32(Console.ReadLine());
                Console.Write("По рецепту? (да/нет): ");
                bool isPrescription = Console.ReadLine().ToLower() == "да";

                Pill updatedPill = new Pill(name, manufacturer, price, quantity, isPrescription);
                updatedPill.Id = id; // Обязательно сохраняем старый ID!

                if (logic.UpdatePill(updatedPill))
                    Console.WriteLine("Изменено успешно!");
                else
                    Console.WriteLine("Ошибка при изменении.");
            }
            Console.ReadKey();
        }

        static void SearchByManufacturerUI(Logic logic)
        {
            Console.Write("\nВведите производителя для поиска: ");
            string manufacturer = Console.ReadLine();
            List<Pill> result = logic.GetPillsByManufacturer(manufacturer);

            Console.WriteLine($"\nНайдено {result.Count} таблеток:");
            foreach (var pill in result)
            {
                Console.WriteLine($"- {pill.Name} (Цена: {pill.Price})");
            }
            Console.ReadKey();
        }

        static void ShowPrescriptionPills(Logic logic)
        {
            Console.WriteLine("\n--- Рецептурные препараты ---");
            List<Pill> result = logic.GetPrescriptionPills();
            foreach (var pill in result)
            {
                Console.WriteLine($"- {pill.Name} (Производитель: {pill.Manufacturer})");
            }
            Console.ReadKey();
        }
    }
}