﻿#pragma checksum "..\..\customers ordering.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "867F16CB603C70CE90C60C9E38DE1CE5697F269291DD3537AB20960F518EADD3"
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


namespace MC_Restaurant {
    
    
    /// <summary>
    /// customers_ordering
    /// </summary>
    public partial class customers_ordering : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 19 "..\..\customers ordering.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.CheckBox FilterTypeCheck;
        
        #line default
        #line hidden
        
        
        #line 21 "..\..\customers ordering.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox NumberOfFood;
        
        #line default
        #line hidden
        
        
        #line 22 "..\..\customers ordering.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button mines;
        
        #line default
        #line hidden
        
        
        #line 23 "..\..\customers ordering.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button plus;
        
        #line default
        #line hidden
        
        
        #line 34 "..\..\customers ordering.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock Title;
        
        #line default
        #line hidden
        
        
        #line 38 "..\..\customers ordering.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox listOfFoodCombo;
        
        #line default
        #line hidden
        
        
        #line 40 "..\..\customers ordering.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DatePicker DateList;
        
        #line default
        #line hidden
        
        
        #line 41 "..\..\customers ordering.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button ChangeOrderNumber;
        
        #line default
        #line hidden
        
        
        #line 42 "..\..\customers ordering.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox listOfTypesCombo;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/MC Restaurant;component/customers%20ordering.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\customers ordering.xaml"
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
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.FilterTypeCheck = ((System.Windows.Controls.CheckBox)(target));
            
            #line 19 "..\..\customers ordering.xaml"
            this.FilterTypeCheck.Unchecked += new System.Windows.RoutedEventHandler(this.FilterTypeCheck_Unchecked);
            
            #line default
            #line hidden
            
            #line 19 "..\..\customers ordering.xaml"
            this.FilterTypeCheck.Checked += new System.Windows.RoutedEventHandler(this.FilterTypeCheck_Checked);
            
            #line default
            #line hidden
            return;
            case 2:
            this.NumberOfFood = ((System.Windows.Controls.TextBox)(target));
            return;
            case 3:
            this.mines = ((System.Windows.Controls.Button)(target));
            
            #line 22 "..\..\customers ordering.xaml"
            this.mines.Click += new System.Windows.RoutedEventHandler(this.mines_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.plus = ((System.Windows.Controls.Button)(target));
            
            #line 23 "..\..\customers ordering.xaml"
            this.plus.Click += new System.Windows.RoutedEventHandler(this.plus_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.Title = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 6:
            this.listOfFoodCombo = ((System.Windows.Controls.ComboBox)(target));
            
            #line 38 "..\..\customers ordering.xaml"
            this.listOfFoodCombo.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.listOfFoodCombo_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 7:
            this.DateList = ((System.Windows.Controls.DatePicker)(target));
            
            #line 40 "..\..\customers ordering.xaml"
            this.DateList.SelectedDateChanged += new System.EventHandler<System.Windows.Controls.SelectionChangedEventArgs>(this.DateList_SelectedDateChanged);
            
            #line default
            #line hidden
            return;
            case 8:
            this.ChangeOrderNumber = ((System.Windows.Controls.Button)(target));
            
            #line 41 "..\..\customers ordering.xaml"
            this.ChangeOrderNumber.Click += new System.Windows.RoutedEventHandler(this.ChangeOrderNumber_Click);
            
            #line default
            #line hidden
            return;
            case 9:
            this.listOfTypesCombo = ((System.Windows.Controls.ComboBox)(target));
            
            #line 42 "..\..\customers ordering.xaml"
            this.listOfTypesCombo.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.listOfTypesCombo_SelectionChanged);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

