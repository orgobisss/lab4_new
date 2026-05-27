namespace lab4_new
{
    partial class Form1
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
            this.buttonOpen = new System.Windows.Forms.Button();
            this.buttonSaveSelection = new System.Windows.Forms.Button();
            this.btnAddHorizontalPin = new System.Windows.Forms.Button();
            this.btnAddVerticalPin = new System.Windows.Forms.Button();
            this.btnAddBase = new System.Windows.Forms.Button();
            this.btnAddClamp = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // buttonOpen
            // 
            this.buttonOpen.Location = new System.Drawing.Point(74, 158);
            this.buttonOpen.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.buttonOpen.Name = "buttonOpen";
            this.buttonOpen.Size = new System.Drawing.Size(300, 58);
            this.buttonOpen.TabIndex = 0;
            this.buttonOpen.Text = "Открыть сборку";
            this.buttonOpen.UseVisualStyleBackColor = true;
            this.buttonOpen.Click += new System.EventHandler(this.buttonOpen_Click);
            // 
            // buttonSaveSelection
            // 
            this.buttonSaveSelection.Location = new System.Drawing.Point(74, 248);
            this.buttonSaveSelection.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.buttonSaveSelection.Name = "buttonSaveSelection";
            this.buttonSaveSelection.Size = new System.Drawing.Size(300, 58);
            this.buttonSaveSelection.TabIndex = 1;
            this.buttonSaveSelection.Text = "Сохранить выделение";
            this.buttonSaveSelection.UseVisualStyleBackColor = true;
            this.buttonSaveSelection.Click += new System.EventHandler(this.buttonSaveSelection_Click);
            // 
            // btnAddHorizontalPin
            // 
            this.btnAddHorizontalPin.Location = new System.Drawing.Point(448, 110);
            this.btnAddHorizontalPin.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.btnAddHorizontalPin.Name = "btnAddHorizontalPin";
            this.btnAddHorizontalPin.Size = new System.Drawing.Size(300, 58);
            this.btnAddHorizontalPin.TabIndex = 2;
            this.btnAddHorizontalPin.Text = "Горизонтальный палец";
            this.btnAddHorizontalPin.UseVisualStyleBackColor = true;
            this.btnAddHorizontalPin.Click += new System.EventHandler(this.btnAddHorizontalPin_Click);
            // 
            // btnAddVerticalPin
            // 
            this.btnAddVerticalPin.Location = new System.Drawing.Point(448, 179);
            this.btnAddVerticalPin.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.btnAddVerticalPin.Name = "btnAddVerticalPin";
            this.btnAddVerticalPin.Size = new System.Drawing.Size(300, 58);
            this.btnAddVerticalPin.TabIndex = 3;
            this.btnAddVerticalPin.Text = "Вертикальный палец";
            this.btnAddVerticalPin.UseVisualStyleBackColor = true;
            this.btnAddVerticalPin.Click += new System.EventHandler(this.btnAddVerticalPin_Click);
            // 
            // btnAddBase
            // 
            this.btnAddBase.Location = new System.Drawing.Point(448, 319);
            this.btnAddBase.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.btnAddBase.Name = "btnAddBase";
            this.btnAddBase.Size = new System.Drawing.Size(300, 58);
            this.btnAddBase.TabIndex = 4;
            this.btnAddBase.Text = "Основание";
            this.btnAddBase.UseVisualStyleBackColor = true;
            this.btnAddBase.Click += new System.EventHandler(this.btnAddBase_Click);
            // 
            // btnAddClamp
            // 
            this.btnAddClamp.Location = new System.Drawing.Point(448, 249);
            this.btnAddClamp.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.btnAddClamp.Name = "btnAddClamp";
            this.btnAddClamp.Size = new System.Drawing.Size(300, 58);
            this.btnAddClamp.TabIndex = 5;
            this.btnAddClamp.Text = "Зажим";
            this.btnAddClamp.UseVisualStyleBackColor = true;
            this.btnAddClamp.Click += new System.EventHandler(this.btnAddClamp_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(442, 67);
            this.label1.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(110, 30);
            this.label1.TabIndex = 6;
            this.label1.Text = "Детали:";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(828, 448);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnAddClamp);
            this.Controls.Add(this.btnAddBase);
            this.Controls.Add(this.btnAddVerticalPin);
            this.Controls.Add(this.btnAddHorizontalPin);
            this.Controls.Add(this.buttonSaveSelection);
            this.Controls.Add(this.buttonOpen);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.Text = "Сопряжение деталей";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private System.Windows.Forms.Button buttonOpen;
        private System.Windows.Forms.Button buttonSaveSelection;
        private System.Windows.Forms.Button btnAddHorizontalPin;
        private System.Windows.Forms.Button btnAddVerticalPin;
        private System.Windows.Forms.Button btnAddBase;
        private System.Windows.Forms.Button btnAddClamp;
        private System.Windows.Forms.Label label1;
    }
}