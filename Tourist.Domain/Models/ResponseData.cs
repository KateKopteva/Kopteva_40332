using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tourist.Domain.Models
{
    public class ResponseData<T>
    {
        public ResponseData()
        { }

        /// Запрашиваемые данные
        public T? Data { get; set; }


        /// Признак успешного завершения запроса
        public bool Success { get; set; } = true;

        /// Сообщение в случае неуспешного завершения
        public string? ErrorMessage { get; set; } = string.Empty;


        /// Фабричный метод: формирует успешный ответ
        /// <param name="data">Передаваемые данные</param>
        /// <returns>Объект ResponseData с Success = true</returns>
        public static ResponseData<T> OK(T? data)
        {
            return new ResponseData<T> { Data = data };
        }

        /// Фабричный метод: формирует ответ с ошибкой
        /// <param name="message">Текст сообщения об ошибке</param>
        /// <returns>Объект ResponseData с Success = false</returns>
        public static ResponseData<T> Error(string message)
        {
            return new ResponseData<T>
            {
                ErrorMessage = message,
                Success = false,
                Data = (T)Activator.CreateInstance(typeof(T))
            };
        }
    }
}
