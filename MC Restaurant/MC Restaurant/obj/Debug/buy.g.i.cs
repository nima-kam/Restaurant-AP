// Updated by XamlIntelliSenseFileGenerator 7/17/2020 12:25:48 PM
#pragma checksum "..\..\buy.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "2C387B30B62870216F480CCBBD4D1B879704CC8D94AA7D36DF957F9402011650"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using MC_Restaurant;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace MC_Restaurant
{


    /// <summary>
    /// buy
    /// </summary>
    public partial class buy : System.Windows.Window, System.Windows.Markup.IComponentConnector
    {

#line default
#line hidden


#line 17 "..\..\buy.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button ShowItemButton;

#line default
#line hidden

        private bool _contentLoaded;

        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent()
        {
            if (_contentLoaded)
            {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/MC Restaurant;component/buy.xaml", System.UriKind.Relative);

#line 1 "..\..\buy.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);

#line default
#line hidden
        }

        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target)
        {
            switch (connectionId)
            {
                case 1:
                    this.filter_combo = ((System.Windows.Controls.ComboBox)(target));
                    return;
                case 2:
                    this.namefilter = ((System.Windows.Controls.ComboBoxItem)(target));
                    return;
                case 3:
                    this.typefilter = ((System.Windows.Controls.ComboBoxItem)(target));
                    return;
                case 4:
                    this.ShowItemButton = ((System.Windows.Controls.Button)(target));

#line 17 "..\..\buy.xaml"
                    this.ShowItemButton.Click += new System.Windows.RoutedEventHandler(this.ShowItemButton_Click);

#line default
#line hidden
                    return;
            }
            this._contentLoaded = true;
        }

        internal System.Windows.Controls.Button AddFoodButton;
        internal System.Windows.Controls.Button EditButton;
        internal System.Windows.Controls.ListBox ChangeFoodList;
        internal System.Windows.Controls.DatePicker MenuDate;
        internal System.Windows.Controls.CheckBox AllOrderCheck;
        internal System.Windows.Controls.CheckBox FilterNameCheck;
        internal System.Windows.Controls.CheckBox FilterTypeCheck;
        internal System.Windows.Controls.TextBox DiscountCodeBox;
        internal System.Windows.Controls.CheckBox DiscountCheck;
    }
}

