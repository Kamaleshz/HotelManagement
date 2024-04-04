using System.Text.Json;

namespace HotelManagementBackend.Exceptions
{
    public class Error
    {
        public int ID { get; set; }
        public string Message { get; set; }

        public Error(int ID, string Message)
        {
            this.ID = ID;
            this.Message = Message;
        }               

        public override string ToString()
        {
            // Return a JSON representation of the object
            return JsonSerializer.Serialize(this);
        }
    }
}
