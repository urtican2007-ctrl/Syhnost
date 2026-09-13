using System;

namespace Syhnost // Проверь, чтобы название совпадало с названием твоего проекта
{
    public class Pill
    {
        public int Id { get; set; }
        public string Name { get; set; }         // Название таблетки
        public string Manufacturer { get; set; } // Производитель
        public double Price { get; set; }        // Цена
        public int Quantity { get; set; }        // Количество на складе
        public bool IsPrescription { get; set; } // По рецепту (true) или без (false)

        // Пустой конструктор нужен для удобства создания объекта
        public Pill() { }

        // Конструктор для быстрого создания
        public Pill(string name, string manufacturer, double price, int quantity, bool isPrescription)
        {
            Name = name;
            Manufacturer = manufacturer;
            Price = price;
            Quantity = quantity;
            IsPrescription = isPrescription;
        }
    }
}
































































//namespace Syhnost
//{
//    public class pill
//    {

//    }
//}
