using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace RPedretti.RazorComponents.BingMap.Entities.Polygon
{
    public class BaseBingMapEntityConverter : JsonConverter<BaseBingMapEntity>
    {
        public override BaseBingMapEntity Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }

        public override void Write(Utf8JsonWriter writer, BaseBingMapEntity value, JsonSerializerOptions options)
        {
            JsonSerializer.Serialize(writer, value, value.GetType(), options);
        }
    }
}
