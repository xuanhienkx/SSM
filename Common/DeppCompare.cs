using System;
using System.Collections.Generic;
using Newtonsoft.Json;


namespace SSM.Common
{
    public static class DeppCompare
    {
        public static bool DeepEquals<T>(this IEnumerable<T> obj, IEnumerable<T> another, out List<string> listMessage)
        {
            listMessage = new List<string>();
            if (ReferenceEquals(obj, another)) return true;
            if ((obj == null) || (another == null)) return false;

            bool result = true;

            //Duyệt từng phần tử trong 2 list đưa vào
            using (IEnumerator<T> enumerator1 = obj.GetEnumerator())
            using (IEnumerator<T> enumerator2 = another.GetEnumerator())
            {
                while (true)
                {
                    bool hasNext1 = enumerator1.MoveNext();
                    bool hasNext2 = enumerator2.MoveNext();

                    //Nếu có 1 list hết, hoặc 2 phần tử khác nhau, thoát khoải vòng lặp
                    if (hasNext1 != hasNext2 || !enumerator1.Current.DeepEquals(enumerator2.Current, out listMessage))
                    {
                        result = false;
                        break;
                    }

                    //Dừng vòng lặp khi 2 list đều hết
                    if (!hasNext1) break;
                }
            }

            return result;
        }
        private static readonly List<string> ListMessageDeepEquals= new List<string>(); 
        public static bool DeepEquals(this object obj, object another, out List<string> listMessage)
        {
            listMessage =   new List<string>();
            if (ReferenceEquals(obj, another)) return true;
            if ((obj == null) || (another == null)) return false;
            if (obj.GetType() != another.GetType()) return false;

            //Nếu property không phải class, chỉ là int, double, DateTime v...v
            //Gọi hàm equal thông thường
            if (!obj.GetType().IsClass) return obj.Equals(another);

            var result = true;
            try
            {
                foreach (var property in obj.GetType().GetProperties())
                {
                    var objValue = property.GetValue(obj, null);
                    var anotherValue = property.GetValue(another, null);
                    //Tiếp tục đệ quy 
                    if (objValue.DeepEquals(anotherValue, out  listMessage)) continue;
                    var message = string.Format("- {0} had change value from {1} to {2}", property.Name, objValue,
                        anotherValue);
                    listMessage.Add(message);
                    ListMessageDeepEquals.Add(message);
                    result = false;
                }
               
                listMessage = ListMessageDeepEquals;
            }
            catch (Exception ex)
            {

                var errorM = ex.Message;
                Logger.LogError(ex);
            }
            return result;
        }
        public static bool JSONEquals(this object obj, object another)
        {
            if (ReferenceEquals(obj, another)) return true;
            if ((obj == null) || (another == null)) return false;
            if (obj.GetType() != another.GetType()) return false;

            var objJson = JsonConvert.SerializeObject(obj);
            var anotherJson = JsonConvert.SerializeObject(another);

            return objJson == anotherJson;
        }

    }
}