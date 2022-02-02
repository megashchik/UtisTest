using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Data;

namespace WpfClient.Tools
{
    internal class SexToStringConverter : IValueConverter
    {
        public string Male { get; set; } = string.Empty;
        public string Female { get; set; } = string.Empty;
        public string Default { get; set; } = string.Empty;

        string splitter = ", ";

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch (value)
            {
                case Model.Sex.Male:
                    return Male.Split(splitter).FirstOrDefault() ?? Enum.GetName(Model.Sex.Male) ?? string.Empty;
                case Model.Sex.Female:
                    return Female.Split(splitter).FirstOrDefault() ?? Enum.GetName(Model.Sex.Female) ?? string.Empty;
                default:
                    return Default.Split(splitter).FirstOrDefault() ?? Enum.GetName(Model.Sex.Default) ?? string.Empty;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch(value)
            {
                case string male when Male.ToUpperInvariant().Split(splitter).Contains(male.ToUpperInvariant()):
                    return Model.Sex.Male;
                case string female when Female.ToUpperInvariant().Split(splitter).Contains(female.ToUpperInvariant()):
                    return Model.Sex.Female;
                default:
                    return Model.Sex.Default;
            }
        }
    }
}
