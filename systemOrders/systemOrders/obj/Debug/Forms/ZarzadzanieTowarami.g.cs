﻿#pragma checksum "..\..\..\Forms\ZarzadzanieTowarami.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "A00C285C98A0F217B7C6FBF41EE71E48"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18408
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

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
using systemOrders;


namespace systemOrders.Forms {
    
    
    /// <summary>
    /// ZarzadzanieTowarami
    /// </summary>
    public partial class ZarzadzanieTowarami : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 17 "..\..\..\Forms\ZarzadzanieTowarami.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.GroupBox panelTowaru;
        
        #line default
        #line hidden
        
        
        #line 30 "..\..\..\Forms\ZarzadzanieTowarami.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txbNazwa;
        
        #line default
        #line hidden
        
        
        #line 32 "..\..\..\Forms\ZarzadzanieTowarami.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txbCena;
        
        #line default
        #line hidden
        
        
        #line 34 "..\..\..\Forms\ZarzadzanieTowarami.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txbOpis;
        
        #line default
        #line hidden
        
        
        #line 38 "..\..\..\Forms\ZarzadzanieTowarami.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btDodajTowar;
        
        #line default
        #line hidden
        
        
        #line 51 "..\..\..\Forms\ZarzadzanieTowarami.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid dataGridTowary;
        
        #line default
        #line hidden
        
        
        #line 67 "..\..\..\Forms\ZarzadzanieTowarami.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button DodajTowar;
        
        #line default
        #line hidden
        
        
        #line 68 "..\..\..\Forms\ZarzadzanieTowarami.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button EdytujTowar;
        
        #line default
        #line hidden
        
        
        #line 69 "..\..\..\Forms\ZarzadzanieTowarami.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button UsunTowar;
        
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
            System.Uri resourceLocater = new System.Uri("/systemOrders;component/forms/zarzadzanietowarami.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Forms\ZarzadzanieTowarami.xaml"
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
            this.panelTowaru = ((System.Windows.Controls.GroupBox)(target));
            return;
            case 2:
            this.txbNazwa = ((System.Windows.Controls.TextBox)(target));
            return;
            case 3:
            this.txbCena = ((System.Windows.Controls.TextBox)(target));
            return;
            case 4:
            this.txbOpis = ((System.Windows.Controls.TextBox)(target));
            return;
            case 5:
            this.btDodajTowar = ((System.Windows.Controls.Button)(target));
            
            #line 38 "..\..\..\Forms\ZarzadzanieTowarami.xaml"
            this.btDodajTowar.Click += new System.Windows.RoutedEventHandler(this.btDodajTowar_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.dataGridTowary = ((System.Windows.Controls.DataGrid)(target));
            return;
            case 7:
            this.DodajTowar = ((System.Windows.Controls.Button)(target));
            
            #line 67 "..\..\..\Forms\ZarzadzanieTowarami.xaml"
            this.DodajTowar.Click += new System.Windows.RoutedEventHandler(this.DodajTowar_Click);
            
            #line default
            #line hidden
            return;
            case 8:
            this.EdytujTowar = ((System.Windows.Controls.Button)(target));
            
            #line 68 "..\..\..\Forms\ZarzadzanieTowarami.xaml"
            this.EdytujTowar.Click += new System.Windows.RoutedEventHandler(this.EdytujTowar_Click);
            
            #line default
            #line hidden
            return;
            case 9:
            this.UsunTowar = ((System.Windows.Controls.Button)(target));
            
            #line 69 "..\..\..\Forms\ZarzadzanieTowarami.xaml"
            this.UsunTowar.Click += new System.Windows.RoutedEventHandler(this.UsunTowar_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}
