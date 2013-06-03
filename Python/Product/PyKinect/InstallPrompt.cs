﻿/* 
 * ****************************************************************************
 *
 * Copyright (c) Microsoft Corporation. 
 *
 * Available under the Microsoft PyKinect 1.0 Alpha license.  See LICENSE.txt
 * for more information.
 *
 * ***************************************************************************/


using System;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Windows.Forms;

namespace Microsoft.Samples {
    public partial class InstallPrompt : Form {
        public InstallPrompt(string installWhat, string installWhere) {
            InitializeComponent();

            _prompt.Text = String.Format("Do you want to install {0} into {1}?", installWhat, installWhere);

            WindowsIdentity id = WindowsIdentity.GetCurrent();
            WindowsPrincipal p = new WindowsPrincipal(id);
            if (!p.IsInRole(WindowsBuiltInRole.Administrator)) {
                _ok.FlatStyle = FlatStyle.System;
                SendMessage(_ok.Handle, BCM_SETSHIELD, 0, 0xFFFFFFFF);
            }
        }

        internal const int BCM_FIRST = 0x1600; //Normal button
        internal const int BCM_SETSHIELD = (BCM_FIRST + 0x000C); //Elevated button

        [DllImport("user32")]
        public static extern UInt32 SendMessage(IntPtr hWnd, UInt32 msg, UInt32 wParam, UInt32 lParam);

        private void _ok_Click(object sender, EventArgs e) {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void _cancel_Click(object sender, EventArgs e) {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}