#region License
// Copyright (c) 2019 Michael T. Russin
//
// Permission is hereby granted, free of charge, to any person
// obtaining a copy of this software and associated documentation
// files (the "Software"), to deal in the Software without
// restriction, including without limitation the rights to use,
// copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the
// Software is furnished to do so, subject to the following
// conditions:
//
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES
// OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT
// HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY,
// WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
// FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
// OTHER DEALINGS IN THE SOFTWARE.
#endregion
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Windows.Forms;
using MtrDev.WebView2.Interop;
using MtrDev.WinForms.Properties;

namespace MtrDev.WinForms
{
    public class WebView2ControlBase : Control
    {
        #region Public Unavailable Methods
        //DrawToBitmap doesn't work for this control, so we should hide it.  We'll
        //still call base so that this has a chance to work if it can.
        [EditorBrowsable(EditorBrowsableState.Never)]
        new public void DrawToBitmap(Bitmap bitmap, Rectangle targetBounds)
        {
            base.DrawToBitmap(bitmap, targetBounds);
        }

        #endregion

        #region Public Unavailable Proprties
        // -------------------------------------------------------------
        //
        // Properties blocked at design time and run time:
        //

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never),
         DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override Color BackColor
        {
            get
            {
                if (DesignMode)
                    return SystemColors.ControlDark;
                return base.BackColor;
            }
            set
            {
                base.BackColor = value;
            }
        }


        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override Font Font
        {
            get
            {
                return base.Font;
            }
            set
            {
                base.Font = value;
            }
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never),
         DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override Color ForeColor
        {
            get
            {
                return base.ForeColor;
            }
            set
            {
                base.ForeColor = value;
            }
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never),
         DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        new public ImeMode ImeMode
        {
            get
            {
                return base.ImeMode;
            }
            set
            {
                base.ImeMode = value;
            }
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never),
         DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override bool AllowDrop
        {
            get
            {
                return base.AllowDrop;
            }
            set
            {
                throw new NotSupportedException(Resources.WebView2AllowDropNotSupport);
            }
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never),
         DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override Image BackgroundImage
        {
            get
            {
                return base.BackgroundImage;
            }
            set
            {
                throw new NotSupportedException(Resources.WebView2BackgroundImageNotSupported);
            }
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override ImageLayout BackgroundImageLayout
        {
            get
            {
                return base.BackgroundImageLayout;
            }
            set
            {
                throw new NotSupportedException(Resources.WebView2BackgroundImageLayoutNotSupported);
            }
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override Cursor Cursor
        {
            get
            {
                return base.Cursor;
            }
            set
            {
                throw new NotSupportedException(Resources.WebView2CursorNotSupported);
            }
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never),
       DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        new public bool Enabled
        {
            get
            {
                return base.Enabled;
            }
            set
            {
                throw new NotSupportedException(Resources.WebView2EnabledNotSupported);
            }
        }

        /// <summary>
        /// This property is not meaningful for this control.
        /// </summary>
        [Browsable(false),
         EditorBrowsable(EditorBrowsableState.Never),
         DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new Padding Padding
        {
            get { return base.Padding; }
            set { base.Padding = value; }
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), Localizable(false)]
        public override RightToLeft RightToLeft
        {
            get
            {
                return RightToLeft.No;
            }
            set
            {
                throw new NotSupportedException(Resources.WebView2RightToLeftNotSupported);
            }
        }

        [
            Browsable(false),
            EditorBrowsable(EditorBrowsableState.Never),
            DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden),
            Bindable(false)
        ]
        public override string Text
        {
            get
            {
                return "";
            }
            set
            {
                throw new NotSupportedException(Resources.WebView2TextNotSupported);
            }
        }

        public new bool UseWaitCursor
        {
            get
            {
                return base.UseWaitCursor;
            }
            set
            {
                throw new NotSupportedException(Resources.WebView2UseWaitCursorNotSupported);
            }
        }

        #endregion

        #region Unavailable Events
        //////////////////////////////////////
        //
        // Unavailable events
        //

        private string FormatEventMessage(string name)
        {
            return string.Format(Resources.EventNotSupported, name);
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        new public event EventHandler BackgroundImageLayoutChanged
        {
            add
            {
                throw new NotSupportedException(FormatEventMessage("BackgroundImageLayoutChanged"));
            }
            remove
            {
            }
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        new public event EventHandler Enter
        {
            add
            {
                throw new NotSupportedException(FormatEventMessage("Enter"));
            }
            remove
            {
            }
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        new public event EventHandler Leave
        {
            add
            {
                throw new NotSupportedException(FormatEventMessage("Leave"));
            }
            remove
            {
            }
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        new public event EventHandler MouseCaptureChanged
        {
            add
            {
                throw new NotSupportedException(FormatEventMessage("MouseCaptureChanged"));
            }
            remove
            {
            }
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        new public event MouseEventHandler MouseClick
        {
            add
            {
                throw new NotSupportedException(FormatEventMessage("MouseClick"));
            }
            remove
            {
            }
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        new public event MouseEventHandler MouseDoubleClick
        {
            add
            {
                throw new NotSupportedException(FormatEventMessage("MouseDoubleClick"));
            }
            remove
            {
            }
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        new public event EventHandler BackColorChanged
        {
            add
            {
                throw new NotSupportedException(FormatEventMessage("BackColorChanged"));
            }
            remove
            {
            }
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        new public event EventHandler BackgroundImageChanged
        {
            add
            {
                throw new NotSupportedException(FormatEventMessage("BackgroundImageChanged"));
            }
            remove
            {
            }
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        new public event EventHandler BindingContextChanged
        {
            add
            {
                throw new NotSupportedException(FormatEventMessage("BindingContextChanged"));
            }
            remove
            {
            }
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        new public event EventHandler CursorChanged
        {
            add
            {
                throw new NotSupportedException(FormatEventMessage("CursorChanged"));
            }
            remove
            {
            }
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        new public event EventHandler EnabledChanged
        {
            add
            {
                throw new NotSupportedException(FormatEventMessage("EnabledChanged"));
            }
            remove
            {
            }
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        new public event EventHandler FontChanged
        {
            add
            {
                throw new NotSupportedException(FormatEventMessage("FontChanged"));
            }
            remove
            {
            }
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        new public event EventHandler ForeColorChanged
        {
            add
            {
                throw new NotSupportedException(FormatEventMessage("ForeColorChanged"));
            }
            remove
            {
            }
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        new public event EventHandler RightToLeftChanged
        {
            add
            {
                throw new NotSupportedException(FormatEventMessage("RightToLeftChanged"));
            }
            remove
            {
            }
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        new public event EventHandler TextChanged
        {
            add
            {
                throw new NotSupportedException(FormatEventMessage("TextChanged"));
            }
            remove
            {
            }
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        new public event EventHandler Click
        {
            add
            {
                throw new NotSupportedException(FormatEventMessage("Click"));
            }
            remove
            {
            }
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        new public event DragEventHandler DragDrop
        {
            add
            {
                throw new NotSupportedException(FormatEventMessage("DragDrop"));
            }
            remove
            {
            }
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        new public event DragEventHandler DragEnter
        {
            add
            {
                throw new NotSupportedException(FormatEventMessage("DragEnter"));
            }
            remove
            {
            }
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        new public event DragEventHandler DragOver
        {
            add
            {
                throw new NotSupportedException(FormatEventMessage("DragOver"));
            }
            remove
            {
            }
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        new public event EventHandler DragLeave
        {
            add
            {
                throw new NotSupportedException(FormatEventMessage("DragLeave"));
            }
            remove
            {
            }
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        new public event GiveFeedbackEventHandler GiveFeedback
        {
            add
            {
                throw new NotSupportedException(FormatEventMessage("GiveFeedback"));
            }
            remove
            {
            }
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        [SuppressMessage("Microsoft.Design", "CA1009:DeclareEventHandlersCorrectly")] //Everett
        new public event HelpEventHandler HelpRequested
        {
            add
            {
                throw new NotSupportedException(FormatEventMessage("HelpRequested"));
            }
            remove
            {
            }
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        new public event PaintEventHandler Paint
        {
            add
            {
                throw new NotSupportedException(FormatEventMessage("Paint"));
            }
            remove
            {
            }
        }
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        new public event QueryContinueDragEventHandler QueryContinueDrag
        {
            add
            {
                throw new NotSupportedException(FormatEventMessage("QueryContinueDrag"));
            }
            remove
            {
            }
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        new public event QueryAccessibilityHelpEventHandler QueryAccessibilityHelp
        {
            add
            {
                throw new NotSupportedException(FormatEventMessage("QueryAccessibilityHelp"));
            }
            remove
            {
            }
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        new public event EventHandler DoubleClick
        {
            add
            {
                throw new NotSupportedException(FormatEventMessage("DoubleClick"));
            }
            remove
            {
            }
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        new public event EventHandler ImeModeChanged
        {
            add
            {
                throw new NotSupportedException(FormatEventMessage("ImeModeChanged"));
            }
            remove
            {
            }
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        new public event KeyEventHandler KeyDown
        {
            add
            {
                throw new NotSupportedException(FormatEventMessage("KeyDown"));
            }
            remove
            {
            }
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        new public event KeyPressEventHandler KeyPress
        {
            add
            {
                throw new NotSupportedException(FormatEventMessage("KeyPress"));
            }
            remove
            {
            }
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        new public event KeyEventHandler KeyUp
        {
            add
            {
                throw new NotSupportedException(FormatEventMessage("KeyUp"));
            }
            remove
            {
            }
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        new public event LayoutEventHandler Layout
        {
            add
            {
                throw new NotSupportedException(FormatEventMessage("Layout"));
            }
            remove
            {
            }
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        new public event MouseEventHandler MouseDown
        {
            add
            {
                throw new NotSupportedException(FormatEventMessage("MouseDown"));
            }
            remove
            {
            }
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        new public event EventHandler MouseEnter
        {
            add
            {
                throw new NotSupportedException(FormatEventMessage("MouseEnter"));
            }
            remove
            {
            }
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        new public event EventHandler MouseLeave
        {
            add
            {
                throw new NotSupportedException(FormatEventMessage("MouseLeave"));
            }
            remove
            {
            }
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        new public event EventHandler MouseHover
        {
            add
            {
                throw new NotSupportedException(FormatEventMessage("MouseHover"));
            }
            remove
            {
            }
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        new public event MouseEventHandler MouseMove
        {
            add
            {
                throw new NotSupportedException(FormatEventMessage("MouseMove"));
            }
            remove
            {
            }
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        new public event MouseEventHandler MouseUp
        {
            add
            {
                throw new NotSupportedException(FormatEventMessage("MouseUp"));
            }
            remove
            {
            }
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        new public event MouseEventHandler MouseWheel
        {
            add
            {
                throw new NotSupportedException(FormatEventMessage("MouseWheel"));
            }
            remove
            {
            }
        }

        [
        Browsable(false),
        EditorBrowsable(EditorBrowsableState.Never),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden),
        ]
        public new event EventHandler PaddingChanged
        {
            add
            {
                base.PaddingChanged += value;
            }
            remove
            {
                base.PaddingChanged -= value;
            }
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        new public event UICuesEventHandler ChangeUICues
        {
            add
            {
                throw new NotSupportedException(FormatEventMessage("ChangeUICues"));
            }
            remove
            {
            }
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        new public event EventHandler StyleChanged
        {
            add
            {
                throw new NotSupportedException(FormatEventMessage("StyleChanged"));
            }
            remove
            {
            }
        }

        #endregion
    }
}
