using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Windows.Media;

namespace H.Controls.TagBox
{
    public class Tag : ITag
    {
        [Required]
        [Display(Name = "名称")]
        public string Name { get; set; }

        [JsonConverter(typeof(BrushJsonConverter))]
        [Display(Name = "颜色")]
        public Brush Background { get; set; }

        [Display(Name = "说明")]
        public string Description { get; set; }
        public override string ToString() => this.Name;
    }

    public class BrushJsonConverter : JsonConverter<Brush>
    {
        public override Brush Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            string value = reader.GetString();
            return value.TryChangeType<Brush>();
        }

        public override void Write(Utf8JsonWriter writer, Brush value, JsonSerializerOptions options)
        {
            if (value == null)
            {
                writer.WriteNull(string.Empty);
                return;
            }
            string txt = value.TryConvertToString();
            writer.WriteStringValue(txt);
        }
    }
}
