using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace BakeSchoolAdmin_DatabaseConnection.Converter
{
    /// <summary>
    /// Provides a way to convert a <see cref="System.Boolean"/> value into
    /// a <see cref="System.Windows.Visibility"/> value.
    /// </summary>
    [ValueConversion(typeof(bool), typeof(Visibility))]
    class BoolToVisibiltyConverter : IValueConverter
    {
        #region ======================================== Fields,Constants,Delegates,Events ============================================== 

        /// <summary>
        /// Value to be set if the binding source value is <c>true</c>.
        /// </summary>
        private Visibility valueWhenTrue = Visibility.Visible;

        /// <summary>
        /// Value to be set if the binding source value is <c>false</c>.
        /// </summary>
        private Visibility valueWhenFalse = Visibility.Collapsed;

        /// <summary>
        /// Value to be set if the binding source value is <c>null</c>.
        /// </summary>
        private Visibility valueWhenNull = Visibility.Collapsed;

        #endregion

        #region ======================================== Properties, Indexer =========================================================== 
        /// <summary>
        /// Gets or sets the value to be applied when the binding source is <c>true</c>
        /// </summary>
        public Visibility ValueWhenTrue
        {
            get
            {
                return this.valueWhenTrue;
            }
            set
            {
                this.valueWhenTrue = value;
            }
        }

        /// <summary>
        /// Gets or sets the value to be applied when the binding source is <c>false</c>
        /// </summary>
        public Visibility ValueWhenFalse
        {
            get
            {
                return this.valueWhenFalse;
            }
            set
            {
                this.valueWhenFalse = value;
            }
        }

        /// <summary>
        /// Gets or sets the value to be applied when the binding source is <c>null</c>
        /// </summary>
        public Visibility ValueWhenNull
        {
            get
            {
                return this.valueWhenNull;
            }
            set
            {
                this.valueWhenNull = value;
            }
        }
        #endregion

        #region ======================================== IValueConverter Implementation ===============================================
        /// <summary>
        /// Converts a value
        /// </summary>
        /// <param name="value">The value produced by the binding source.</param>
        /// <param name="targetType">The type of the binding target property.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        /// <returns>the conevrted value</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            //// das is probiert zu konvertieren
            if (value == null || value is bool == false)
            {
                return this.ValueWhenNull;
            }

            return (bool)value ? this.ValueWhenTrue : this.ValueWhenFalse;
        }


        /// <summary>
        /// Converts a value back
        /// </summary>
        /// <param name="value">The value produced by the binding target.</param>
        /// <param name="targetType">The type of the binding target property.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        /// <returns>the conevrted value</returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}

