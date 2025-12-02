using System.Drawing;
using System.Windows.Forms;

namespace DungeonGenerator
{
    partial class Form4
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            SuspendLayout();
            // 
            // Form4
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.DarkGray;
            ClientSize = new Size(933, 877);
            Margin = new Padding(4, 5, 4, 5);
            Name = "Form4";
            Text = "Подземелье - Уровень 2";
            Load += Form4_Load;
            ResumeLayout(false);
        }
    }
}