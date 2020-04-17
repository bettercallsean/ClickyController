using ClickyControllerGUI.ViewModels;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClickyControllerGUI.Utilities
{
    class CommandViewModelConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return (objectType == typeof(CommandViewModel));
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            JObject jo = JObject.Load(reader);
            if (jo["ViewModel"].Value<string>() == "MouseClickViewModel")
                return jo.ToObject<MouseClickViewModel>(serializer);

            if (jo["ViewModel"].Value<string>() == "MouseMoveViewModel")
                return jo.ToObject<MouseMoveViewModel>(serializer);

            if (jo["ViewModel"].Value<string>() == "KeyboardCharacterInputViewModel")
                return jo.ToObject<KeyboardCharacterInputViewModel>(serializer);

            if (jo["ViewModel"].Value<string>() == "KeyboardTextInputViewModel")
                return jo.ToObject<KeyboardTextInputViewModel>(serializer);

            if (jo["ViewModel"].Value<string>() == "WaitViewModel")
                return jo.ToObject<WaitViewModel>(serializer);

            return null;
        }

        public override bool CanWrite
        {
            get { return false; }
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
