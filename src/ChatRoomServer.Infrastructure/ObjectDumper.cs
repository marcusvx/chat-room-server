using System.Text;

namespace ChatRoomService.Infrastructure
{

    static class ObjectHelper
    {
        public static string Dump<T>(this T x)
        {
            var t = typeof(T);
            var props = t.GetProperties();
            StringBuilder sb = new StringBuilder();
            foreach (var item in props)
            {
                sb.Append($"{item.Name}:{item.GetValue(x, null)}; ");
            }
            sb.AppendLine();

            return sb.ToString();
        }
    }

}
