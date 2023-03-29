﻿#pragma checksum "..\..\FactoriesPage.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "7C9FE80281D6B7F56D29514222979C5FE7ED4F798DF077FCCBE2F190B9A45032"
//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

using DBReader;
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


namespace DBReader {
    
    
    /// <summary>
    /// FactoriesPage
    /// </summary>
    public partial class FactoriesPage : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector {
        
        
        #line 12 "..\..\FactoriesPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid TableDataGrid;
        
        #line default
        #line hidden
        
        
        #line 14 "..\..\FactoriesPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button NextTableButton;
        
        #line default
        #line hidden
        
        
        #line 20 "..\..\FactoriesPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button PreviousTableButton;
        
        #line default
        #line hidden
        
        
        #line 27 "..\..\FactoriesPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button CreateRecordButton;
        
        #line default
        #line hidden
        
        
        #line 28 "..\..\FactoriesPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button DeleteRecord;
        
        #line default
        #line hidden
        
        
        #line 30 "..\..\FactoriesPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ScrollViewer EditScrollViewer;
        
        #line default
        #line hidden
        
        
        #line 50 "..\..\FactoriesPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox AddressTextBox;
        
        #line default
        #line hidden
        
        
        #line 52 "..\..\FactoriesPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox MatSupplierComboBox;
        
        #line default
        #line hidden
        
        
        #line 54 "..\..\FactoriesPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox FurnitureCarriersComboBox;
        
        #line default
        #line hidden
        
        
        #line 56 "..\..\FactoriesPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button ApplyChangesButton;
        
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
            System.Uri resourceLocater = new System.Uri("/DBReader;component/factoriespage.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\FactoriesPage.xaml"
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
            this.TableDataGrid = ((System.Windows.Controls.DataGrid)(target));
            
            #line 12 "..\..\FactoriesPage.xaml"
            this.TableDataGrid.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.TableDataGrid_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 2:
            this.NextTableButton = ((System.Windows.Controls.Button)(target));
            
            #line 14 "..\..\FactoriesPage.xaml"
            this.NextTableButton.Click += new System.Windows.RoutedEventHandler(this.NextTableButton_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            this.PreviousTableButton = ((System.Windows.Controls.Button)(target));
            
            #line 20 "..\..\FactoriesPage.xaml"
            this.PreviousTableButton.Click += new System.Windows.RoutedEventHandler(this.PreviousTableButton_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.CreateRecordButton = ((System.Windows.Controls.Button)(target));
            
            #line 27 "..\..\FactoriesPage.xaml"
            this.CreateRecordButton.Click += new System.Windows.RoutedEventHandler(this.CreateRecordButton_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.DeleteRecord = ((System.Windows.Controls.Button)(target));
            
            #line 28 "..\..\FactoriesPage.xaml"
            this.DeleteRecord.Click += new System.Windows.RoutedEventHandler(this.DeleteRecordButton_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.EditScrollViewer = ((System.Windows.Controls.ScrollViewer)(target));
            return;
            case 7:
            this.AddressTextBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 8:
            this.MatSupplierComboBox = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 9:
            this.FurnitureCarriersComboBox = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 10:
            this.ApplyChangesButton = ((System.Windows.Controls.Button)(target));
            
            #line 56 "..\..\FactoriesPage.xaml"
            this.ApplyChangesButton.Click += new System.Windows.RoutedEventHandler(this.ApplyChangesButton_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}
