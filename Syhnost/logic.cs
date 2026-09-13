using System;
using System.Collections.Generic;
using System.Linq;

namespace Syhnost
{
    public class Logic
    {
        // Хранилище данных (вместо базы данных)
        private List<Pill> _pills = new List<Pill>();
        private int _nextId = 1; // Счетчик для автоматической выдачи Id

        // 1. СОЗДАНИЕ (Create)
        public void AddPill(Pill pill)
        {
            pill.Id = _nextId++;
            _pills.Add(pill);
        }

        // 2. УДАЛЕНИЕ (Delete)
        public bool DeletePill(int id)
        {
            var pillToRemove = _pills.FirstOrDefault(p => p.Id == id);
            if (pillToRemove != null)
            {
                _pills.Remove(pillToRemove);
                return true;
            }
            return false; // Если таблетка с таким Id не найдена
        }

        // 3. ЧТЕНИЕ (Read)
        public List<Pill> GetAllPills()
        {
            // Возвращаем копию списка, чтобы защитить оригинал от случайного изменения извне
            return new List<Pill>(_pills);
        }

        // 4. ИЗМЕНЕНИЕ (Update)
        public bool UpdatePill(Pill updatedPill)
        {
            var existingPill = _pills.FirstOrDefault(p => p.Id == updatedPill.Id);
            if (existingPill != null)
            {
                existingPill.Name = updatedPill.Name;
                existingPill.Manufacturer = updatedPill.Manufacturer;
                existingPill.Price = updatedPill.Price;
                existingPill.Quantity = updatedPill.Quantity;
                existingPill.IsPrescription = updatedPill.IsPrescription;
                return true;
            }
            return false;
        }

        // --- БИЗНЕС-ФУНКЦИИ ---

        // 5. Бизнес-функция 1: Получить таблетки конкретного производителя
        public List<Pill> GetPillsByManufacturer(string manufacturer)
        {
            return _pills.Where(p => p.Manufacturer.ToLower() == manufacturer.ToLower()).ToList();
        }

        // 6. Бизнес-функция 2: Получить только рецептурные препараты
        public List<Pill> GetPrescriptionPills()
        {
            return _pills.Where(p => p.IsPrescription).ToList();
        }

        // Альтернативная бизнес-функция 2 (если нужна другая): 
        // Подсчет общей стоимости запасов
        public double GetTotalStockValue()
        {
            return _pills.Sum(p => p.Price * p.Quantity);
        }
    }
}






















//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Syhnost
//{
//    internal class logic
//    {
//    }
//}
