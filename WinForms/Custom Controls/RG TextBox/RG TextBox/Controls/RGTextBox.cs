// <copyright file="RGTextBox.cs" company="None"> Copyright (c) Ricardo Garcia. </copyright>
// <author> Ricardo Garcia </author>
// <date> 2022, 10.10 </date>

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace RG_TextBox.Controls
{
    public class RGTextBox : TextBox
    {
        // Last Edited: 2022, 10.10

        #region <Developer Notes>
        /*
         * [Info]
         * ---------------------------------------------------------------------------------------
         * 
         * This is an Improved Version of the Default Control, by Implementing Missing Input Type 
         * Features. 
         * 
         * This TextBox can be set to the Following Input Types:
         *
         *  1. Default (ASCII)
         *          -> Allows Char Limiting
         *          -> Allows the use of a Placeholder
         *  
         *  2. Whole Numbers (Numeric)
         *          -> Filters the Input to Accept Numbers (ONLY).
         *          -> Values are ALWAYS whole Numbers (No Decimals)
         *  
         *  3. Decimal Numbers (Numeric)
         *          -> Values are ALWAYS Decimal
         *          -> Possibility to use as Currency (Show or Hide Currency Designator)
         *          -> Possibility to Set the Currency Designator Char (Before or After the Value)
         *             
         *  4. IPv4
         *          -> Formats the Value to become a IPv4 Value.
         * 
         * 
         * [Known Issues]
         * ---------------------------------------------------------------------------------------
         * 
         *          [0]. There may be Some Code Smell. Clean On-the-Go.
         *          [1]. Placeholder is not beint Set upon App Start
         *          [2]. Whole Number TextBox does NOT Validate Properly
         *          
         *          
         * [Ideas to Implement]
         * ---------------------------------------------------------------------------------------
         *          [1]. Implement Highlight
         *          
         * 
         */
        #endregion


        #region <Constructor>
        public RGTextBox()
        {
            // -> TextBox (Default Initial Configuration)
            ForeColor = Color.Gainsboro;
            BackColor = Color.FromArgb(255, 25, 25, 25);
            BorderStyle = BorderStyle.FixedSingle;
        }
        #endregion


        #region <Fields>
        // -> Number & Decimals (Configuration)
        private const int WM_PASTE = 0x0302;                // Used to Validate Clipboard Data.
        private string allowedNumbersChars = "0123456789";        // Filter to Determine if String Contains Valid Number Chars
        private string allowedSymbolsChars = ".";                 // Filter to Determine if String Contains Valid Symbol Chars
        private string decimalFormat = "0.00";              // Initial (Selected) Decimal Format.
        #endregion


        #region <Custom Properties> : (Base Font)
        private Color baseForeColor = Color.White;
        [Category("1. Custom Properties"), DisplayName("00. Base ForeColor")]
        [Description("Set Base ForeColor.")]
        [Browsable(true)]
        public Color BaseForeColor
        {
            get { return baseForeColor; }
            set
            {
                baseForeColor = value;

                Invalidate();
            }
        }


        private Font baseFont = new Font("Arial", 8);
        [Category("1. Custom Properties"), DisplayName("00. Base Font")]
        [Description("Set Base Font.")]
        [Browsable(true)]
        public Font BaseFont
        {
            get { return baseFont; }
            set
            {
                baseFont = value;

                Invalidate();
            }
        }
        #endregion

        #region <Custom Properties> : (1. Input Type)
        /// <summary> TextBox Text Input Type (Normal, Numeric, IPV4). </summary>
        public enum TextInputType { Default_ASCII, WholeNumbers, DecimalNumbers, IPV4 }

        private TextInputType inputType = TextInputType.Default_ASCII;
        [Category("1. Custom Properties"), DisplayName("01. Input Type")]
        [Description("Select Control Input Type (Normal, Numeric or Currency).")]
        [Bindable(true)] /* Required for Enum Types */
        [Browsable(true)]
        public TextInputType TextBoxType
        {
            get { return inputType; }
            set
            {
                inputType = value;

                Text = String.Empty;

                TogglePlaceholder();

                FormatValue();

                Invalidate();
            }
        }
        #endregion

        #region <Custom Properties> : (2. Curency)
        private bool isCurrency = false;
        [Category("1. Custom Properties"), DisplayName("02. Is Currency")]
        [Description("Set the Control Text to be used as Coin Currency")]
        [Browsable(true)]
        public bool IsCurrency
        {
            get { return isCurrency; }
            set
            {
                isCurrency = value;

                FormatValue();

                Invalidate();
            }
        }


        private string currencyDesignator = "€";
        [Category("1. Custom Properties"), DisplayName("03. Currency Designator")]
        [Description("Set Currency Symbol or Designator.\n\n i.e: €, Eur, Euros")]
        [Browsable(true)]
        public string CurrencyDesignator
        {
            get { return currencyDesignator; }
            set
            {
                // 1. Remove Current Currency Designator Before Updating to a New Value.
                RemoveCurrency();

                // 2. Set the New Currency Designator
                currencyDesignator = value;

                // 3. Format the Currency Text.
                FormatValue();

                Invalidate();
            }
        }


        public enum DesignatorAlignment { Left, Right }
        private DesignatorAlignment designatorAlignment = DesignatorAlignment.Right;
        [Category("1. Custom Properties"), DisplayName("04. Designator Location")]
        [Description("Select Currency Designator Location")]
        [Bindable(true)] /* Required for Enum Types */
        [Browsable(true)]
        public DesignatorAlignment DesignatorLocation
        {
            get { return designatorAlignment; }
            set
            {
                designatorAlignment = value;

                FormatValue();

                Invalidate();
            }
        }
        #endregion

        #region <Custom Properties> : (3. Decimals)
        private int decimalPlaces = 2;
        [Category("1. Custom Properties"), DisplayName("06. Decimal Places")]
        [Description("Select wether to use Whole Number or a Decimal Number.")]
        [Browsable(true)]
        public int DecimalPlaces
        {
            get { return decimalPlaces; }
            set
            {
                if (value >= 1 & value <= 4)
                {
                    decimalPlaces = value;

                    // Aet Decimal Format
                    switch (decimalPlaces)
                    {
                        case 1: decimalFormat = "0.0"; break;
                        case 2: decimalFormat = "0.00"; break;
                        case 3: decimalFormat = "0.000"; break;
                        case 4: decimalFormat = "0.0000"; break;
                    }
                }

                FormatValue();

                Invalidate();
            }
        }
        #endregion

        #region <Custom Properties> : (4. Char Limiter)
        private bool limitedChars = false;
        [Category("1. Custom Properties"), DisplayName("07. Limited Chars")]
        [Description("Toggle Character Input Limiting.")]
        [Browsable(true)]
        public bool LimitedChars
        {
            get { return limitedChars; }
            set { limitedChars = value; }
        }


        private int maximumChars = 5;
        [Category("1. Custom Properties"), DisplayName("08. Maximum Chars")]
        [Description("Limit the Maximum Number of Chars Allowed on the Control.")]
        [Browsable(true)]
        public int MaximumChars
        {
            get { return maximumChars; }
            set { maximumChars = value; }
        }
        #endregion

        #region <Custom Properties> : (5. Placeholder)
        // -> Placeholder
        /// <summary> Determines if the Control Text Contains the Placeholder Text. <br/>
        /// <strong>Note:</strong> This is the Placeholder Enabled State (Do NOT Confuse with Placeholder Enabled!)
        /// </summary>
        private bool placeholderActive { get; set; }


        private bool placeholderEnabled = false;
        [Category("1. Custom Properties"), DisplayName("09. Placeholder Enabled")]
        [Description("Toggle TextBox Placeholder Function.")]
        [Browsable(true)]
        public bool PlaceholderEnabled
        {
            get { return placeholderEnabled; }
            set
            {
                placeholderEnabled = value;

                TogglePlaceholder();

                Invalidate();
            }
        }

        private string placeHolderText = "Enter Text";
        [Category("1. Custom Properties"), DisplayName("10. Placeholder Text")]
        [Description("Set Placeholder Text.")]
        [Browsable(true)]
        public string PlaceholderText
        {
            get { return placeHolderText; }
            set
            {
                placeHolderText = value;
                Text = value;

                Invalidate();
            }
        }

        private Color placeholderForeColor = Color.DimGray;
        [Category("1. Custom Properties"), DisplayName("11. Placeholder ForeColor")]
        [Description("Set Placeholder Text.")]
        [Browsable(true)]
        public Color PlaceholderForeColor
        {
            get { return placeholderForeColor; }
            set
            {
                placeholderForeColor = value;

                Invalidate();
            }
        }

        private Font placeHolderFont = new Font("Consolas", 8, FontStyle.Italic);
        [Category("1. Custom Properties"), DisplayName("12. Placeholder Font")]
        [Description("Set Placeholder Font.")]
        [Browsable(true)]
        public Font PlaceholderFont
        {
            get { return placeHolderFont; }
            set
            {
                placeHolderFont = value;

                Invalidate();
            }
        }
        #endregion


        #region <Overriden Events> : (Mouse Events)
        /// <summary> Occurs when the Mouse Selects the Control. </summary>
        /// <param name="e"></param>
        protected override void OnMouseCaptureChanged(EventArgs e)
        {
            // [i]: Same Code as 'OnEnter' Event

            base.OnMouseCaptureChanged(e);

            if (!DesignMode)
            {
                RemovePlaceholder();

                // Format the String Value
                FormatValue();
                RemoveCurrency();
                RemoveWhiteSpaces();

                SelectAll();
            }
        }
        #endregion

        #region <Overriden Events> : (Keyboard Events)
        /// <summary> Occurs when a Keyboard Key is Pressed. </summary>
        /// <param name="e"></param>
        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            // Note: If having Problems with "e.Handled":
            //       Add a "e.Handled" Line for each Conditional

            base.OnKeyPress(e);

            if (!e.KeyChar.Equals((char)Keys.Back))
            {
                switch (inputType)
                {
                    case TextInputType.Default_ASCII: 
                        // -> Stop Key if Max. Text Length is Reached.
                        e.Handled = HasMaximumChars(Text.Length); 
                        break;

                    case TextInputType.WholeNumbers:
                        // -> Stop Key Input If:
                        //          - Key is Invalid Char
                        //          - Max. Amount of Chars has been Reached.
                        e.Handled = !IsValidNumericChar(e.KeyChar) | HasMaximumChars(Text.Length);
                        break;

                    case TextInputType.DecimalNumbers:
                        // -> Stop Key Input If:
                        //          - Key is Invalid Char (Number or Symbol).
                        //          - Max. Amount of Chars has been Reached.
                        //          - Max. Amount of '.' Chars has been Reached (Max. 1un).
                        e.Handled = !IsValidNumericChar(e.KeyChar) && !HasValidNumericSymbolChar(e.KeyChar);
                        
                        // -> Prevent Over Typing '.' Char More than Once (DO NOT Change this Line)
                        if (e.KeyChar.Equals('.') && NrCharOccurrences('.') >= 1) { e.Handled = true; }
                        break;
                }
            }
        }
        #endregion


        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            FormatValue();
        }





        #region <Overriden Events> : (Control Events)
        /// <summary> Occurs Before the Control Stops Being the Active Control. </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void OnValidating(CancelEventArgs e)
        {
            base.OnValidating(e);

            if (!DesignMode) { FormatValue(); }
        }

        /// <summary> Occurs when the Control Becomes the Active Control. </summary>
        /// <param name="e"></param>
        protected override void OnEnter(EventArgs e)
        {
            // [i]: Same Code as 'OnMouseCaptureChanged' Event

            base.OnEnter(e);

            if (!DesignMode)
            {
                RemovePlaceholder();

                // Format the String Value (Formats According to TextBox Input Type)
                FormatValue();
                RemoveCurrency();
                RemoveWhiteSpaces();

                SelectAll();
            }
        }

        /// <summary> Occurs when the Control Stops Being the Active Control. </summary>
        /// <param name="e"></param>
        protected override void OnLeave(EventArgs e)
        {
            base.OnLeave(e);

            if (!DesignMode)
            {
                InsertPlaceholder();

                FormatValue();
            }
        }
        #endregion


        #region <Methods> : (Placeholder)
        /// <summary> Toggle Placeholder. </summary>
        private void TogglePlaceholder()
        {
            if (placeholderEnabled)
            {
                var hasEmptyText = string.IsNullOrEmpty(Text)/* & string.IsNullOrWhiteSpace(Text)*/;

                switch (hasEmptyText)
                {
                    // Set the Placeholder
                    case true: InsertPlaceholder(); break;

                    // Remove the Placeholder
                    case false: RemovePlaceholder(); break;
                }
            }
        }

        /// <summary> Separately Inserts the Placeholder when Conditionals are Met. </summary>
        private void InsertPlaceholder()
        {
            if (inputType.Equals(TextInputType.Default_ASCII) ^ inputType.Equals(TextInputType.WholeNumbers))
            {
                if (placeholderEnabled & !placeholderActive)
                {
                    var hasEmptyText = string.IsNullOrEmpty(Text)/* & string.IsNullOrWhiteSpace(Text)*/;

                    if (hasEmptyText)
                    {
                        placeholderActive = true;
                        ForeColor = placeholderForeColor;
                        Text = placeHolderText;
                        Font = placeHolderFont;

                        // -> Text Alignment
                        //      - When Placeholder is Shown; Always Set its Text Alignment to the Left.
                        if (placeholderEnabled & placeholderActive) { TextAlign = HorizontalAlignment.Left; }
                    }
                }
            }
        }

        /// <summary> Separately Removes the Placeholder when Conditionals are Met. </summary>
        private void RemovePlaceholder()
        {
            if (inputType.Equals(TextInputType.Default_ASCII) ^ inputType.Equals(TextInputType.WholeNumbers))
            {
                if (placeholderEnabled & placeholderActive)
                {
                    placeholderActive = false;
                    Text = string.Empty;
                    ForeColor = baseForeColor;
                    Font = baseFont;

                    // -> Text Alignment
                    //      - When Input Type is 'WholeNumbers'; Rever the Text Alignment to the Right.
                    if (inputType.Equals(TextInputType.WholeNumbers)) { TextAlign = HorizontalAlignment.Right; }
                }
            }
        }
        #endregion

        #region <Methods> : (Char Manipulation)
        /// <summary> Calculates the Nr. of Occurrences for the Specified Char Parameter. </summary>
        /// <param name="char"></param>
        /// <returns> The Number of the Received Char Parameter Occurrences Found in the TextBox Text. </returns>
        private int NrCharOccurrences(char @char)
        {
            // Default
            return Text.Split(@char).Length - 1;
        }
        #endregion

        #region [!] <Methods> : (Text Formatting)
        /// <summary> Formats the Text with Proper Formatting for the Selected Input Type. </summary>
        private void FormatValue()
        {
            switch (inputType)
            {

                case TextInputType.WholeNumbers:    // TEST : 13.09
                    ConvertToWholeNumber();
                    TextAlign = HorizontalAlignment.Right;
                    break;
                //-------


                case TextInputType.DecimalNumbers:

                    ConvertToDecimalNumber();   // Toggle Decimals
                    SetCurrencyDesignator();    // Toggle Currency


                    // -> Text Alignment
                    TextAlign = HorizontalAlignment.Right;

                    break;

                case TextInputType.IPV4:

                    Text = HasValidIPv4(Text) ? Text : "0.0.0.0";
                    RemoveWhiteSpaces();

                    // -> Text Alignment
                    TextAlign = HorizontalAlignment.Center;

                    break;
            }
        }

        /// <summary> Converts the Text Value as a Whole Number. Converts to 0 if String is Invalid. </summary>
        private void ConvertToWholeNumber()
        {
            var stringValue = ExtractNumericString();

            if (!string.IsNullOrEmpty(stringValue) ^ !string.IsNullOrWhiteSpace(stringValue))
            {
                // Set the Text Value as a Whole Number (int)
                Text = $"{stringValue}";
            }

            else { Text = "0"; }
        }

        /// <summary> Convert the Number to a Decimal Value. Sets to 0 if String is Invalid. </summary>
        private void ConvertToDecimalNumber()
        {
            var stringValue = ExtractNumericString();

            // String Contains a Value:
            if (!string.IsNullOrEmpty(stringValue) & !string.IsNullOrWhiteSpace(stringValue))
            {
                decimal decVal = -1;

                // Success:
                // [Reference]: if (decimal.TryParse(Text, out decVal)) { val = decVal.ToString("0.00"); }
                if (decimal.TryParse(stringValue, out decVal)) { stringValue = decVal.ToString(decimalFormat); }
                
                // Fail:
                // else { /* FAIL */ }
            }

            // String is Null, Empty or White Space:
            else
            {
                decimal decVal = -1;

                // Success:
                // [Reference]: if (decimal.TryParse(Text, out decVal)) { val = decVal.ToString("0.00"); }
                if (decimal.TryParse("0", out decVal)) { stringValue = decVal.ToString(decimalFormat); }

                // Fail:
                // else { /* FAIL */ }
            }

            Text = stringValue;
        }

        /// <summary> Sets and Updates the Currency Designator </summary>
        private void SetCurrencyDesignator()
        {
            // [!] Criteria: Ensure Selected TextBox Input Type Must be Numeric Before Calling this Method.
            var stringValue = ExtractNumericString();

            switch (inputType)
            {
                case TextInputType.DecimalNumbers:

                    if (!string.IsNullOrEmpty(stringValue) & !string.IsNullOrWhiteSpace(stringValue))
                    {
                        if (IsCurrency)
                        {
                            switch (designatorAlignment)
                            {
                                case DesignatorAlignment.Left:
                                    //if (!Text.StartsWith(currencyDesignator))
                                    //{ Text = $"{currencyDesignator} {GetNumericValue()}"; }
                                    Text = $"{currencyDesignator} {stringValue}";

                                    break;

                                case DesignatorAlignment.Right:
                                    //if (!Text.EndsWith(currencyDesignator))
                                    //{ Text = $"{GetNumericValue()} {currencyDesignator}"; }
                                    Text = $"{stringValue} {currencyDesignator}";
                                    break;
                            }
                        }
                    }

                    break;
            }
        }

        /// <summary> Removes 'White' Spaces from TextBox Text. </summary>
        private void RemoveWhiteSpaces()
        {
            // Current
            switch (inputType)
            {
                case TextInputType.WholeNumbers:
                case TextInputType.DecimalNumbers:
                case TextInputType.IPV4:

                    Text = Text.Replace(" ", string.Empty);

                    break;
            }

            // Previous
            //if (inputType.Equals(TextBoxInputType.Numeric))
            //{
            //    Text = Text.Replace(" ", string.Empty);
            //}
        }

        /// <summary> Removes Currency Designator from TextBox Text. </summary>
        private void RemoveCurrency()
        {
            if (inputType.Equals(TextInputType.DecimalNumbers) & Text.Contains(currencyDesignator))
            {
                Text = Text.Replace(currencyDesignator, "");
            }
        }
        #endregion



        #region <Methods> : (Text Validation)

        public bool ContainsWholeNumber(string str) { return int.TryParse(str, out int nr); }
        public bool ContainsDecimalNumber(string str) { return double.TryParse(str, out double nr); }


        /// <summary> Check if Specified Char Paramter is a Valid Number Character. </summary>
        /// <param name="char"></param>
        /// <returns> true if Received Char is a Number. </returns>
        private bool IsValidNumericChar(char @char)
        {
            bool val = allowedNumbersChars.Contains(@char); // | @char.Equals((char)Keys.Back);
            return val;
        }

        /// <summary> Check if Specified Char Paramter is a Valid Numeric Symbol Char. </summary>
        /// <param name="char"></param>
        /// <returns> true if Received Char is a Valid Numeric Symbol. </returns>
        private bool HasValidNumericSymbolChar(char @char)
        {
            return allowedSymbolsChars.Contains(@char); // | @char.Equals((char)Keys.Back);
        }

        /// <summary> Checks if Received String Parameter is a Number. </summary>
        /// <param name="value"></param>
        /// <returns> True if Received String Parameter is a Number. </returns>
        private bool IsNumericString(string value)
        {
            bool isNumeric = false;

            switch (inputType)
            {
                case TextInputType.WholeNumbers: isNumeric = ContainsWholeNumber(Text); break;

                case TextInputType.DecimalNumbers: isNumeric = ContainsDecimalNumber(Text); break;
            }

            return isNumeric;
        }

        /// <summary> Checks if the TextBox is Limiting the Maximum Number of Characters. </summary>
        /// <param name="textLength"></param>
        /// <returns> True if the TextBox is Limiting the Maximum Number Chars </returns>
        private bool HasMaximumChars(int textLength)
        {
            bool val = false;

            if (limitedChars)
            {
                switch (inputType)
                {
                    case TextInputType.Default_ASCII:
                    case TextInputType.WholeNumbers:

                        val = textLength.Equals(maximumChars);

                        break;


                        // [i]: Keep for Reference (ONLY)!
                        //      Only 'Default ASCII' and 'Whole Numbers' Input Types, Should Allow Character Limiting.

                        //case TextBoxInputType.DecimalNumbers:

                        //    // Note: '+1' Refers to the '.' that Separates the Decimals
                        //    val = textLength.Equals(maximumChars + decimalPlaces + 1);

                        //    break;
                }
            }

            return val;
        }
        #endregion


        #region <Methods> : (Text Validation : Handle Clipboard Content)
        /// <summary> Checks if the Clipboard Content Value is Valid. </summary>
        /// <param name="val"></param>
        /// <returns> True if Clipboard Content Matches the TextBox Input Requirements. </returns>
        private bool HasValidClipboardContent(string val)
        {
            bool isValid = false;

            switch (inputType)
            {
                case TextInputType.Default_ASCII: isValid = !HasMaximumChars(val.Length); break;

                case TextInputType.WholeNumbers:
                    isValid = !HasMaximumChars(val.Length) && ContainsWholeNumber(val);
                    break;

                case TextInputType.DecimalNumbers:
                    isValid = !HasMaximumChars(val.Length) && ContainsDecimalNumber(val);
                    break;

                case TextInputType.IPV4:
                    isValid = HasValidIPv4(val);
                    break;
            }

            return isValid;
        }

        protected override void WndProc(ref Message m)
        {
            /*
             * Remarks: Handling Clipboard Data (Validate Data on Paste).
             * Adapted Code from: 'Thorarin'.
             * Source: https://stackoverflow.com/questions/15987712/handle-a-paste-event-in-c-sharp
             */

            // 1. Handle All Other Messages Normally.
            if (m.Msg != WM_PASTE) { base.WndProc(ref m); }

            // 2. Handle Clipboard Data (On Paste).
            else
            {
                if (Clipboard.ContainsText())
                {
                    string val = Clipboard.GetText();

                    if (HasValidClipboardContent(val)) { Text = val; }

                    // Note(s):
                    // Text Validation for Each Input Type, Occurs under Control Leave Event.

                    // Clipboard.Clear(); --> You can use this if you Wish to Clear the Clipboard after Pasting the Value
                }
            }
        }
        #endregion

        #region <Methods> : (Text Validation : IP Validation)
        /// <summary> Checks if String Contains a Valid IPv4 Address. </summary>
        /// <returns> True if the IPv4 Address is Valid. </returns>
        private bool HasValidIPv4(string value)
        {
            // Remarks:
            // Code based on Yiannis Leoussis Approach.
            // Using a 'for' Loop instead of 'foreach'.
            // Link: https://stackoverflow.com/questions/11412956/what-is-the-best-way-of-validating-an-ip-address

            bool isValid = true;

            if (string.IsNullOrWhiteSpace(Text)) { isValid = false; }

            //  Split string by ".", check that array length is 4
            string[] arrOctets = Text.Split('.');

            if (arrOctets.Length != 4) { isValid = false; }

            // Check Each Sub-String (Ensure that it Parses to byte)
            byte obyte = 0;

            for (int i = 0; i < arrOctets.Length; i++)
            {
                string strOctet = arrOctets[i];

                if (!byte.TryParse(strOctet, out obyte)) { isValid = false; }
            }

            // Set Default TextBox Text if IP is Invalid:
            // if (!isValid) { Text_FormatValue(); } // <-- DUPLICATE METHOD ENTRY ------------

            return isValid;
        }
        #endregion



        #region <Methods> : (Text Fn)
        /// <summary> Extracts a Numeric String Value from Whole or Decimal TextBox Modes. </summary>
        /// <returns> 0 if String Value is Invalid. </returns>
        private string ExtractNumericString()
        {
            // -> Remove White Spaces from TextBox Text.
            RemoveWhiteSpaces();

            // -> Remove Currency Character from TextBox Text.
            RemoveCurrency();

            // -> Allocate the TextBox Text
            string val = Text;

            // Ensure the Text Value is a Number

            switch (inputType)
            {
                case TextInputType.WholeNumbers:
                    if (ContainsWholeNumber(val)) { val = $"{int.Parse(Text)}"; }
                    else { val = "0"; }
                    break;

                case TextInputType.DecimalNumbers:
                    if (ContainsDecimalNumber(val)) { val = $"{double.Parse(Text)}"; }
                    else { val = "0"; }
                    break;
            }

            return val;
        }
        #endregion

    }
}
