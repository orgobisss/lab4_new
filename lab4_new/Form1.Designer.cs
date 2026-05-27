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
            this.buttonSaveSelection1 = new System.Windows.Forms.Button();
            this.buttonSaveSelection2 = new System.Windows.Forms.Button();
            this.btnAddHorizontalPin = new System.Windows.Forms.Button();
            this.btnAddVerticalPin = new System.Windows.Forms.Button();
            this.btnAddBase = new System.Windows.Forms.Button();
            this.btnAddClamp = new System.Windows.Forms.Button();
            this.labelStage1 = new System.Windows.Forms.Label();
            this.labelStage2 = new System.Windows.Forms.Label();
            this.labelComponents = new System.Windows.Forms.Label();
            this.panelStage1 = new System.Windows.Forms.Panel();
            this.panelComponents = new System.Windows.Forms.Panel();
            this.panelStage2 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.panelStage1.SuspendLayout();
            this.panelComponents.SuspendLayout();
            this.panelStage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonOpen
            // 
            this.buttonOpen.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(118)))), ((int)(((byte)(200)))));
            this.buttonOpen.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.buttonOpen.ForeColor = System.Drawing.Color.White;
            this.buttonOpen.Location = new System.Drawing.Point(458, 30);
            this.buttonOpen.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.buttonOpen.Name = "buttonOpen";
            this.buttonOpen.Size = new System.Drawing.Size(400, 77);
            this.buttonOpen.TabIndex = 0;
            this.buttonOpen.Text = "📂 Открыть сборку";
            this.buttonOpen.UseVisualStyleBackColor = false;
            this.buttonOpen.Click += new System.EventHandler(this.buttonOpen_Click);
            // 
            // buttonSaveSelection1
            // 
            this.buttonSaveSelection1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(175)))), ((int)(((byte)(80)))));
            this.buttonSaveSelection1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.buttonSaveSelection1.ForeColor = System.Drawing.Color.White;
            this.buttonSaveSelection1.Location = new System.Drawing.Point(18, 155);
            this.buttonSaveSelection1.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.buttonSaveSelection1.Name = "buttonSaveSelection1";
            this.buttonSaveSelection1.Size = new System.Drawing.Size(360, 96);
            this.buttonSaveSelection1.TabIndex = 1;
            this.buttonSaveSelection1.Text = "✓ Сохранить 5 граней\n(этап 1)";
            this.buttonSaveSelection1.UseVisualStyleBackColor = false;
            this.buttonSaveSelection1.Click += new System.EventHandler(this.buttonSaveSelection1_Click);
            // 
            // buttonSaveSelection2
            // 
            this.buttonSaveSelection2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(175)))), ((int)(((byte)(80)))));
            this.buttonSaveSelection2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.buttonSaveSelection2.ForeColor = System.Drawing.Color.White;
            this.buttonSaveSelection2.Location = new System.Drawing.Point(18, 121);
            this.buttonSaveSelection2.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.buttonSaveSelection2.Name = "buttonSaveSelection2";
            this.buttonSaveSelection2.Size = new System.Drawing.Size(360, 96);
            this.buttonSaveSelection2.TabIndex = 7;
            this.buttonSaveSelection2.Text = "✓ Сохранить 3 грани\n(этап 2)";
            this.buttonSaveSelection2.UseVisualStyleBackColor = false;
            this.buttonSaveSelection2.Click += new System.EventHandler(this.buttonSaveSelection2_Click);
            // 
            // btnAddHorizontalPin
            // 
            this.btnAddHorizontalPin.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(152)))), ((int)(((byte)(0)))));
            this.btnAddHorizontalPin.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.btnAddHorizontalPin.ForeColor = System.Drawing.Color.White;
            this.btnAddHorizontalPin.Location = new System.Drawing.Point(18, 74);
            this.btnAddHorizontalPin.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.btnAddHorizontalPin.Name = "btnAddHorizontalPin";
            this.btnAddHorizontalPin.Size = new System.Drawing.Size(360, 67);
            this.btnAddHorizontalPin.TabIndex = 4;
            this.btnAddHorizontalPin.Text = "➕ PalecHorizontal";
            this.btnAddHorizontalPin.UseVisualStyleBackColor = false;
            this.btnAddHorizontalPin.Click += new System.EventHandler(this.btnAddHorizontalPin_Click);
            // 
            // btnAddVerticalPin
            // 
            this.btnAddVerticalPin.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(152)))), ((int)(((byte)(0)))));
            this.btnAddVerticalPin.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.btnAddVerticalPin.ForeColor = System.Drawing.Color.White;
            this.btnAddVerticalPin.Location = new System.Drawing.Point(18, 151);
            this.btnAddVerticalPin.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.btnAddVerticalPin.Name = "btnAddVerticalPin";
            this.btnAddVerticalPin.Size = new System.Drawing.Size(360, 67);
            this.btnAddVerticalPin.TabIndex = 5;
            this.btnAddVerticalPin.Text = "➕ PalecVertical";
            this.btnAddVerticalPin.UseVisualStyleBackColor = false;
            this.btnAddVerticalPin.Click += new System.EventHandler(this.btnAddVerticalPin_Click);
            // 
            // btnAddBase
            // 
            this.btnAddBase.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(67)))), ((int)(((byte)(54)))));
            this.btnAddBase.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.btnAddBase.ForeColor = System.Drawing.Color.White;
            this.btnAddBase.Location = new System.Drawing.Point(18, 227);
            this.btnAddBase.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.btnAddBase.Name = "btnAddBase";
            this.btnAddBase.Size = new System.Drawing.Size(360, 67);
            this.btnAddBase.TabIndex = 8;
            this.btnAddBase.Text = "➕ Base (основание)";
            this.btnAddBase.UseVisualStyleBackColor = false;
            this.btnAddBase.Click += new System.EventHandler(this.btnAddBase_Click);
            // 
            // btnAddClamp
            // 
            this.btnAddClamp.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(152)))), ((int)(((byte)(0)))));
            this.btnAddClamp.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.btnAddClamp.ForeColor = System.Drawing.Color.White;
            this.btnAddClamp.Location = new System.Drawing.Point(18, 228);
            this.btnAddClamp.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.btnAddClamp.Name = "btnAddClamp";
            this.btnAddClamp.Size = new System.Drawing.Size(360, 67);
            this.btnAddClamp.TabIndex = 6;
            this.btnAddClamp.Text = "➕ Clamp";
            this.btnAddClamp.UseVisualStyleBackColor = false;
            this.btnAddClamp.Click += new System.EventHandler(this.btnAddClamp_Click);
            // 
            // labelStage1
            // 
            this.labelStage1.AutoSize = true;
            this.labelStage1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.labelStage1.Location = new System.Drawing.Point(21, 19);
            this.labelStage1.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.labelStage1.Name = "labelStage1";
            this.labelStage1.Size = new System.Drawing.Size(120, 31);
            this.labelStage1.TabIndex = 0;
            this.labelStage1.Text = "Этап 1: ";
            // 
            // labelStage2
            // 
            this.labelStage2.AutoSize = true;
            this.labelStage2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.labelStage2.Location = new System.Drawing.Point(12, 19);
            this.labelStage2.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.labelStage2.Name = "labelStage2";
            this.labelStage2.Size = new System.Drawing.Size(112, 31);
            this.labelStage2.TabIndex = 2;
            this.labelStage2.Text = "Этап 2:";
            // 
            // labelComponents
            // 
            this.labelComponents.AutoSize = true;
            this.labelComponents.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.labelComponents.Location = new System.Drawing.Point(20, 19);
            this.labelComponents.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.labelComponents.Name = "labelComponents";
            this.labelComponents.Size = new System.Drawing.Size(297, 31);
            this.labelComponents.TabIndex = 3;
            this.labelComponents.Text = "Добавление деталей";
            // 
            // panelStage1
            // 
            this.panelStage1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelStage1.Controls.Add(this.label1);
            this.panelStage1.Controls.Add(this.labelStage1);
            this.panelStage1.Controls.Add(this.buttonSaveSelection1);
            this.panelStage1.Location = new System.Drawing.Point(30, 135);
            this.panelStage1.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.panelStage1.Name = "panelStage1";
            this.panelStage1.Size = new System.Drawing.Size(398, 344);
            this.panelStage1.TabIndex = 1;
            // 
            // panelComponents
            // 
            this.panelComponents.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelComponents.Controls.Add(this.labelComponents);
            this.panelComponents.Controls.Add(this.btnAddHorizontalPin);
            this.panelComponents.Controls.Add(this.btnAddVerticalPin);
            this.panelComponents.Controls.Add(this.btnAddClamp);
            this.panelComponents.Location = new System.Drawing.Point(460, 135);
            this.panelComponents.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.panelComponents.Name = "panelComponents";
            this.panelComponents.Size = new System.Drawing.Size(398, 344);
            this.panelComponents.TabIndex = 2;
            // 
            // panelStage2
            // 
            this.panelStage2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelStage2.Controls.Add(this.label2);
            this.panelStage2.Controls.Add(this.labelStage2);
            this.panelStage2.Controls.Add(this.buttonSaveSelection2);
            this.panelStage2.Controls.Add(this.btnAddBase);
            this.panelStage2.Location = new System.Drawing.Point(890, 135);
            this.panelStage2.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.panelStage2.Name = "panelStage2";
            this.panelStage2.Size = new System.Drawing.Size(398, 344);
            this.panelStage2.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(21, 61);
            this.label1.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(320, 31);
            this.label1.TabIndex = 2;
            this.label1.Text = "Выбор базовых граней";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.label2.Location = new System.Drawing.Point(12, 61);
            this.label2.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(350, 31);
            this.label2.TabIndex = 9;
            this.label2.Text = "Выбор граней основания";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.ClientSize = new System.Drawing.Size(1320, 519);
            this.Controls.Add(this.panelStage2);
            this.Controls.Add(this.panelComponents);
            this.Controls.Add(this.panelStage1);
            this.Controls.Add(this.buttonOpen);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.Text = "Сопряжение деталей - SOLIDWORKS Assembly Builder";
            this.panelStage1.ResumeLayout(false);
            this.panelStage1.PerformLayout();
            this.panelComponents.ResumeLayout(false);
            this.panelComponents.PerformLayout();
            this.panelStage2.ResumeLayout(false);
            this.panelStage2.PerformLayout();
            this.ResumeLayout(false);

        }

        private System.Windows.Forms.Button buttonOpen;
        private System.Windows.Forms.Button buttonSaveSelection1;
        private System.Windows.Forms.Button buttonSaveSelection2;
        private System.Windows.Forms.Button btnAddHorizontalPin;
        private System.Windows.Forms.Button btnAddVerticalPin;
        private System.Windows.Forms.Button btnAddBase;
        private System.Windows.Forms.Button btnAddClamp;
        private System.Windows.Forms.Label labelStage1;
        private System.Windows.Forms.Label labelStage2;
        private System.Windows.Forms.Label labelComponents;
        private System.Windows.Forms.Panel panelStage1;
        private System.Windows.Forms.Panel panelComponents;
        private System.Windows.Forms.Panel panelStage2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}